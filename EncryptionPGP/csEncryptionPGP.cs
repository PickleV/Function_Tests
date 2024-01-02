﻿using DevExpress.XtraGrid;
using Org.BouncyCastle.Utilities.IO;
using PGP.PGPDecryption;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EncryptionPGP
{

    /// <summary>
    /// Data file result
    /// </summary>
    public class csEncryptionPGP
    {
        public byte[] fileBytes;

        public string PlainText;

        public GeneralResult DecryptionProcedure(string sFilePath)
        {
            var result = new GeneralResult();
            fileBytes = null;

            //Init file type
            bool isBinaryFile = false;
            if (csPublic.IsBinaryFile(sFilePath) == true) isBinaryFile = true;


            //License function allowd and user enabled this function
            //User can only run encrypted file
            if (!isBinaryFile)
            {
                result.Message = "File is not encrtpted.";
                return result;
            }


            //Get cetificate
            csPGPCertificate cetificate = new csPGPCertificate();
 
            //Load keys
            var certResult = cetificate.LoadConfig(csConfigHelper.Config);
            if (!certResult.IsSuccess)
            {
                result.Message = certResult.Message;
                return result;
            }

            try
            {

                //Decryption
                using (MemoryStream ms = new MemoryStream())
                using (StreamWriter sw = new StreamWriter(ms, Encoding.Default))
                {
                    sw.Write(cetificate.PrivateKey);
                    sw.Flush();
                    ms.Position = 0;

                    using (MemoryOutputStream outputStream = (MemoryOutputStream)PGPDecrypt.Decrypt2(sFilePath, ms, cetificate.Passphrase))
                    {
                        fileBytes = outputStream.ToArray();
                    }

                }

                //Decryption finished
                if (fileBytes != null && fileBytes.Length > 0)
                {
                    using (MemoryStream memStream = new MemoryStream(fileBytes))
                    {
                        using (StreamReader reader = new StreamReader(memStream))
                        {
                            PlainText = reader.ReadToEnd();
                        }
                    }
                }

                //Pass all steps
                result.IsSuccess = true;
                return result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DecryptProcedure.Exception:\r\n{ex.Message}");
                fileBytes = null;
                result.Message = ex.Message;
                return result;
            }

        }


     



 







    }

}
