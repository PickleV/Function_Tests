﻿
namespace Properties
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pg1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pd1 = new DevExpress.XtraVerticalGrid.PropertyDescriptionControl();
            this.lb1 = new System.Windows.Forms.ListBox();
            this.te1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pg1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.te1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pg1
            // 
            this.pg1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg1.Location = new System.Drawing.Point(0, 0);
            this.pg1.Name = "pg1";
            this.pg1.OptionsBehavior.AutoPostEditorDelay = 1000;
            this.pg1.OptionsCollectionEditor.AllowMultiSelect = false;
            this.pg1.Size = new System.Drawing.Size(295, 515);
            this.pg1.TabIndex = 0;
            this.pg1.DataSourceChanged += new System.EventHandler(this.pg1_DataSourceChanged);
            this.pg1.Click += new System.EventHandler(this.propertyGridControl1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pd1);
            this.panel1.Controls.Add(this.pg1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(503, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 515);
            this.panel1.TabIndex = 1;
            // 
            // pd1
            // 
            this.pd1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.pd1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pd1.Location = new System.Drawing.Point(0, 415);
            this.pd1.Name = "pd1";
            this.pd1.Size = new System.Drawing.Size(295, 100);
            this.pd1.TabIndex = 1;
            this.pd1.TabStop = false;
            // 
            // lb1
            // 
            this.lb1.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb1.FormattingEnabled = true;
            this.lb1.Location = new System.Drawing.Point(0, 0);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(149, 515);
            this.lb1.TabIndex = 2;
            this.lb1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // te1
            // 
            this.te1.Location = new System.Drawing.Point(183, 33);
            this.te1.Name = "te1";
            this.te1.Size = new System.Drawing.Size(100, 20);
            this.te1.TabIndex = 3;
            this.te1.EditValueChanged += new System.EventHandler(this.textEdit1_EditValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 515);
            this.Controls.Add(this.te1);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pg1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.te1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraVerticalGrid.PropertyGridControl pg1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraVerticalGrid.PropertyDescriptionControl pd1;
        private System.Windows.Forms.ListBox lb1;
        private DevExpress.XtraEditors.TextEdit te1;
    }
}

