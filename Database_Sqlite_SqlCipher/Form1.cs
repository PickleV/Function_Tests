﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Database_Sqlite_SqlCipher
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bInitDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            csSqlHelper.InitDataBase();
        }
    }
}