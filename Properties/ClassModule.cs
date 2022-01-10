﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using DevExpress.XtraVerticalGrid;
using DevExpress.Utils.Serializing;
using System.Drawing.Design;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace Properties
{
    class Student
    {
        [Category("Test")]
        [DisplayName("s Name"), Description("Description Method 1 Name of this student.")]
        //Directly use custom editor, Type and base type
        [Editor(typeof(FilteredFileNameEditor),typeof(UITypeEditor))]
        public string Name { get; set; }

        //User to address editor manually
        [CustomEditor(EditorType.Number)]
        [Category("Test")]
        [DisplayName("S Age"), Description("Description Method 2, manual address")]
        public int Age { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))] //Show sub class properties
        [ExpandableRowSettings(false)] //Hide class root editor
        public Certificate Cert { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))] //Show sub class properties
        [ExpandableRowSettings(false)] //Hide class root editor
        public CertificateBase Cert2 { get; set; }

        [CustomEditor(EditorType.ToggleSwitch)]
        [Category("Test")]
        [DisplayName("Toggle Switch"), Description("Description Method 2, manual address")]
        public bool ToggleSwitch { get; set; }

        /// <summary>
        /// Must be public to be able to display on property
        /// </summary>
        [CustomEditor(EditorType.ToggleSwitchList)]
        [DisplayName("List"), Description("List Test")]
        [ExpandableRowSettings(false)] //Hide class root editor
    
        public bool[] List { get; set; }

        public Student()
        {
            Cert = new Certificate();
            Cert2 = new Certificate();
            List = new bool[3];
        }
    }

    public class Certificate: CertificateBase
    {
        [DisplayName("certID"), Description("Student Age1")]
        [CustomEditor(EditorType.Number)]
        public int CertificateID { get; set; }
        public string Name { get; set; }

        public bool IsOK { get; set; }
    }

    public class CertificateBase
    {

    }

    

 

    public class FilteredFileNameEditor : UITypeEditor
    {
        private OpenFileDialog ofd = new OpenFileDialog();
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context,IServiceProvider provider,object value)
        {
            ofd.FileName = value.ToString();
            ofd.Filter = "Text File|*.txt|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            return base.EditValue(context, provider, value);
        }
    }




}
