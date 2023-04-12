import sys
import xmlrpclib
import ssl
import logging
import ast
import traceback


RFIDConnected = False
RFIDTagReady = False
RFIDEPCTagInfo = ''
uid = 0
password = ""
db = ""
models = 0
product_id = ""
production_id = ""
manufacture_order_list = ""




class XmlRpc:

     # Query list of open Manufacture Orders from Odoo.
    def fetchManufactureOrders(self):
        global manufacture_order_list
        manufacture_order_list = models.execute_kw(db, uid, password, 'mrp.production', 'search_read',
                                               [[('state', 'in', ['confirmed', 'progress']), ('product_id', 'ilike', 'S2')]],
                                               {'fields': ['id', 'name', 'product_id', 'product_qty']})
        manufacture_orders = []
        
        if manufacture_order_list:
            try:
                for order in manufacture_order_list:
                    _product_id = order.get('product_id')
                    _production_id = order.get('id')
                    _production_name = order.get('name')
                    _product_qty = order.get('product_qty')

                    product_rec = models.execute_kw(db, uid, password, 'product.product', 'search_read',
                                                    [[['id', '=', _product_id[0]]]],
                                                    {'fields': ['engineering_code']})
                    eng_code = product_rec[0].get('engineering_code')

                    manufacture_orders.append([_production_id, _production_name, _product_id[0], eng_code, _product_id[1], _product_qty])
            except Exception:
                return -1 # Failed to fetch MO
        return manufacture_orders

        #Read order detail
    def getOrderDetail(self, orderID):
        # 0: Result
        # 1: ink volume
        # 2: expering date
        # 3: message
        orderInfo = [-1,0,0,'']
        try:
            #Get ink volumn
            ink_volume = models.execute_kw(db, uid, password, 'mrp.production', 'get_rfid_ink_volume', [orderID])
            orderInfo[1] = ink_volume
            #Get expiring date
            expiry_date = models.execute_kw(db, uid, password, 'mrp.production', 'get_rfid_expiry_date', [orderID])
            orderInfo[2] = expiry_date
            #Set success
            orderInfo[0]=1;
        except Exception as ex:
            print(ex.faultString)
            orderInfo[3]= ex.faultString
        return orderInfo


    
    # Pass the ID of finished MO and a list of Serial Numbers used to Odoo
    def validateMO(self, mo_id, serial_number_list):
        result=[-1,""]
        try:
            #parse string to python format
            serial_number_list = ast.literal_eval(serial_number_list)
            print('Sync order:' + str(mo_id) + ', Tag count:' + str(len(serial_number_list)))
            result[0] = models.execute_kw(db, uid, password, 'mrp.production', 'action_mass_produce_RFID', [[mo_id], serial_number_list])
            return result
        except Exception as ex:
            print('validateMO:' + str(ex))
            result[0]=-1
            result[1]=ex.faultString
            print(ex.faultString)
            #traceback.print_exc()
            return result # Odoo Server Error.
    
    # Request the next RFID sequence ('PS####') from Odoo and increase the
    # counter by one.
    def getNextSequenceNumber(self):
        sequence = models.execute_kw(db, uid, password, 'ir.sequence', 'next_by_code', [['rfid']])
        return sequence

    # Request sequence numbers in range
    def getNextRange(self,your_order_qty_here):
        try:
            sequence = models.execute_kw(db, uid, password, 'ir.sequence', 'search_read', [[['code', '=', 'rfid']]], {'fields': ['number_next_actual']})
            sequence_id = sequence[0]['id']
            sequence_number = sequence[0]['number_next_actual']
            new_sequence_number = sequence_number + your_order_qty_here
            models.execute_kw(db, uid, password, 'ir.sequence', 'write', [sequence_id, {'number_next_actual': new_sequence_number}])
        except Exception as ex:
            print('Get sequence number range:' + str(ex))
            return -1 # Log-in Failed
        return sequence_number


    def getUserName(self):
        user_info = models.execute_kw(db, uid, password, 'res.users', 'search_read', [[['id', '=', uid]]], {'fields': ['name']})
        return user_info[0].get('name')

    # Log in Odoo
    def login(self, url, _db, username, _password):
        try:
            common = xmlrpclib.ServerProxy('{}/xmlrpc/common'.format(url),use_datetime=True, context=ssl._create_unverified_context())

            global uid
            uid = common.authenticate(_db, username, _password, {})

            global db
            db = _db

            global password
            password = _password

            global RFIDTagReady
            RFIDTagReady = True

            global models
            models = xmlrpclib.ServerProxy('{}/xmlrpc/2/object'.format(url),use_datetime=True,context=ssl._create_unverified_context())

        except Exception as ex:
            print('Login exception:' + str(ex))
            return -1 # Log-in Failed
                
        # Check user's access rights
        # TODO: Return a value
        try:
            can_mfg = models.execute_kw(db, uid, password, 'res.users', 'get_mfg_rights', [uid])
            #can_test = models.execute_kw(db, uid, password, 'res.users',
            #'get_test_rights', [uid])
            #can_debug = models.execute_kw(db, uid, password, 'res.users',
            #'get_debug_rights', [uid])
            #can_debug=True;
            #iValue=can_mfg+can_test*2+can_debug*4
            iValue = int(can_mfg)
            return iValue
        except Exception as ex:
            print('Login exception:' + str(ex))
            return -2 # Failed to check Access Rights
        return -3 # User logged in, but he/she has no rights to perform any action