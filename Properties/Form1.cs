﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Events;
using DevExpress.XtraVerticalGrid.Rows;

namespace Properties
{
    public partial class Form1 : Form
    {
        List<Student> sList;
        public csPropertyHelper propertyHelper { get; set; }
        public string ErrorMessage { get; set; }



        public Form1()
        {
            InitializeComponent();
        }

        private void propertyGridControl1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Init helper
            propertyHelper = new csPropertyHelper(pg1);
            pg1.RowHeaderWidth = 30; //Manual set captain row width

            //Init property grid events
            pg1.ValidatingEditor += Pg1_ValidatingEditor;
            pg1.CustomRecordCellEdit += PropertyGridControl1_CustomRecordCellEdit; //Constantly trigger!!!!, avoid
            pg1.SelectedChanged += Pg1_SelectedChanged;
            pg1.CellValueChanged += Pg1_CellValueChanged; //Happen before Pg1_ValidatingEditor!!!
            pg1.EditorKeyDown += Pg1_EditorKeyDown;
            pg1.CustomDrawRowHeaderCell += Pg1_CustomDrawRowHeaderCell;
           
            //pg1.KeyPress += Pg1_KeyPress;
            sList = new List<Student>();
            for (int i = 0; i < 5; i++)
            {
                Student sTemp = new Student();
                sTemp.Name = $"S_{i + 1}";
                sTemp.Age = i + 1;
                sList.Add(sTemp);
                lb1.Items.Add(i + 1);
            }

            lb1.DataSource = sList.Select(x => x.Name).ToList();
        }

        private void Pg1_CustomDrawRowHeaderCell(object sender, CustomDrawRowHeaderCellEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Near; //Set captain alignment
        }

        private void Pg1_CustomRecordCellEdit(object sender, GetCustomRowCellEditEventArgs e)
        {
            Debug.WriteLine("Pg1_CustomRecordCellEdit");
        }

        /// <summary>
        /// Use key down instead, key press trigger when press and realse combined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pg1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("Pg1_KeyPress:" + (Keys)e.KeyChar);
        }

        /// <summary>
        ///Notice, return(enter) key happens after validation and value change.
        ///Rest of the key inputs happen before validation.
        ///So a manual validate required when enter pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pg1_EditorKeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("Pg1_EditorKeyPress:" + (Keys)e.KeyChar);

            //Press enter when inside the editor
            //TriggerPropertyValidate(e.KeyChar == (char)Keys.Return);


        }


        /// <summary>
        /// Input trigger before validation, but 
        /// Enter event trigger after validation!!!! 
        /// Do not use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pg1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("Pg1_EditorKeyDown:" + e.KeyCode);
        }


        private void Pg1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            Debug.WriteLine($"Cell Value changed.{e.Value}");
        }

        private void pg1_DataSourceChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("SourceChange:" + pg1.Rows.Count);

            //recreate all rows
            propertyHelper.ReloadAll();
        }


        private void Pg1_SelectedChanged(object sender, SelectedChangedEventArgs e)
        {
            Debug.WriteLine($"SelectionChange:Row:{e.Row},Cell:{e.Cell},Record:{e.Record}");
        }

        private void Pg1_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            Debug.WriteLine("Validating Edit Trigger");

            //Init variables
            string sFieldName = pg1.FocusedRow.Properties.FieldName;

            if (sFieldName == nameof(Student.Age))
            {
                VerifyRange(e, 1, 50);
            }
            else if (sFieldName == "Cert.CertificateID")
            {
                VerifyRange(e, 2, 50);
            }
            else if (sFieldName == $"{nameof(Student.Cert)}.{nameof(Certificate.IsOK)}")
            {
                NotifyUserAndUpdate(e);
            }
            else if (sFieldName==nameof(Student.Text1Normal))
            {
                VerifyLength(e,1,5);
            }
        }


        private void NotifyUserAndUpdate(BaseContainerValidateEditorEventArgs e)
        {
            if (!(e.Value is bool)) return;

            //Get value
            if (!(bool)e.Value)
            {

                //Dev message box
                XtraMessageBox.Show("Dev info", "Dev title", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);


                //Show confirmmation
                var result = MessageBox.Show("This is confirmation", "Test title", MessageBoxButtons.OKCancel);

                if (result == DialogResult.OK)
                {
                    //Change value and redraw
                    Debug.WriteLine("Change value and redraw");
                }
                else
                {
                    e.Value = true;//Change value back, this will avoid validation error notice
                    return;
                }
            }
        }




        private void VerifyRange(BaseContainerValidateEditorEventArgs e, float fMin, float fMax)
        {
            if (e.Value == null)
            {
                e.Valid = false;
                return;
            }

            if (!float.TryParse(e.Value.ToString(), out float fValue))
            {
                e.ErrorText = $"Invalid input.";
                e.Valid = false;
                return;
            }

            if (fValue > fMax || fValue < fMin)
            {
                e.ErrorText = $"Value should be in range {fMin} and {fMax}";
                e.Valid = false;
                return;
            }

            //pass all steps
            e.Valid = true;
        }

        private void VerifyLength(BaseContainerValidateEditorEventArgs e, int iMin, int iMax=0)
        {
            if (e.Value == null)
            {
                if (iMin != 0)
                {
                    e.Valid = false;
                    e.ErrorText = $"Invalid input.";
                    return;
                }
                else
                {
                    e.Valid = true;
                    return;
                }
            }

            //Check min length
            int iLength = e.Value.ToString().Length;
            if (iLength < iMin)
            {
                e.Valid = false;
                e.ErrorText = $"Length must be no less than {iMin}";
                return;
            }

            //Check max length
            if (iMax!=0 && iLength>iMax)
            {
                e.Valid = false;
                e.ErrorText = $"Length must be no more than {iMax}";
                return;
            }

            //pass all steps
            e.Valid = true;
        }


        private void PropertyGridControl1_CustomRecordCellEdit(object sender, GetCustomRowCellEditEventArgs e)
        {
            //Trigger too many times!!!, do not use
            //Debug.WriteLine($"CustomRecordCellEdit Trigger:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            //Properties.FieldName=Actual Code name
            //Properties.Caption=Property Name (Defined name,description)
            

            //e.RepositoryItem.Appearance.TextOptions.HAlignment = HorzAlignment.Near;

            //Default editors:
            //https://docs.devexpress.com/WindowsForms/DevExpress.XtraEditors.Repository

            //pg1.CustomRecordCellEdit -= PropertyGridControl1_CustomRecordCellEdit; //Constantly trigger!!!!, avoid


            //if (pg1.SelectedObject is Student && pg1.Rows.Count>0)
            //{
            //    foreach (var row in pg1.Rows[0].ChildRows) //Rows in first catagory
            //    {
            //        if (row.Properties.FieldName== nameof(Student.Name)) 
            //        {
            //            if (row.Properties.Value.ToString()=="S_2")
            //            {
            //                Debug.WriteLine("S2");
            //                RepositoryItemRichTextEdit edit = new RepositoryItemRichTextEdit();
            //                edit.ReadOnly = false;
            //                edit.CustomHeight = 50;


            //                row.Properties.RowEdit = edit;

            //                break;
            //            }
            //        }
            //    }
            //}

            //pg1.CustomRecordCellEdit += PropertyGridControl1_CustomRecordCellEdit; //Constantly trigger!!!!, avoid

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Skip

            //Verify index
            if (lb1.SelectedIndex < 0) return;
            pg1.SelectedObject = sList[lb1.SelectedIndex];


            //Get all rows
            //var rows = GetAllPropertyRows();

            ////Set editor
            //foreach (var row in rows)
            //{
            //    var editor = GetEditorType(row);
            //    if (editor == null) continue;

            //    switch (editor.Editor)
            //    {
            //        case EditorType.Number:
            //            RepositoryItemCalcEdit repositoryCalcEdit = new RepositoryItemCalcEdit();
            //            row.Properties.RowEdit = repositoryCalcEdit;
            //            break;
            //        default:
            //            break;
            //    }
            //}
            Debug.WriteLine(pg1.Rows.Count);
        }






        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayValue();
        }


        private void DisplayValue()
        {
            if (pg1.SelectedObject is Student)
            {
                Student s1 = (Student)pg1.SelectedObject;
                Debug.WriteLine("DisplayValue:\r\n" + s1.TextFolder);
            }
        }
    }
}
