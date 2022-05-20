﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardControl
{
    public partial class Form1 : Form
    {

        public bool PageChangeEnable { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wizardControl1.UseAcceptButton = true; //Disable enter button to go to next page
            WizardPage3.AllowFinish = false;
            wizardPage1.PageValidating += WizardPage1_PageValidating;//Only works in normal mode, do't use in async mode


            wizardControl1.SelectedPageChanging += WizardControl1_SelectedPageChanging;
            wizardControl1.CancelClick += WizardControl1_CancelClick;
        }

        private void WizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private async void WizardControl1_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            if (PageChangeEnable) return;

            if (e.PrevPage == WizardPage0)
            {
                e.Cancel = true;
                await Task.Delay(1000);

                //Dosth
                if (true)
                {
                    PageChangeEnable = true;
                    wizardControl1.SelectedPage = wizardPage1;
                    PageChangeEnable = false;
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }

        private void WizardPage1_PageValidating(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
        {
            ////Test only
            //if (false)
            //{
            //    e.ErrorText = $"Error Message! 123";
            //    e.ErrorIconType = MessageBoxIcon.Error;
            //    e.Valid = false;
            //}

        }

        private void cbTest_CheckedChanged(object sender, EventArgs e)
        {

            WizardPage3.AllowFinish = cbTest.Checked;
            WizardPage3.AllowCancel = true;

        }
    }
}
