﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionTool
{
    public partial class FormAesConfig : DevExpress.XtraEditors.XtraForm
    {
        public FormAesConfig()
        {
            InitializeComponent();
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAesConfig_Load(object sender, EventArgs e)
        {

        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            string sInput = XtraInputBox.Show("Please add a description","Add encryption","Default");
            if (string.IsNullOrWhiteSpace(sInput)) return;
 

        }

        private void bDelete_Click(object sender, EventArgs e)
        {

        }
    }
}