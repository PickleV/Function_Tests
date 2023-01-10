﻿using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeList
{
    public partial class PartialModification : DevExpress.XtraEditors.XtraForm
    {

        public BindingList<StudentTree> students = new BindingList<StudentTree>();

        public PartialModification()
        {
            InitializeComponent();
        }

        private void PartialModification_Load(object sender, EventArgs e)
        {
            treeList1.KeyFieldName = nameof(StudentTree.TreeID);
            treeList1.ParentFieldName = nameof(StudentTree.TreeParentID);

            for (int i = 0; i < 3; i++)
            {
                var sItem = new StudentTree()
                {
                    TreeID = i,
                    TreeParentID = -1,
                    Icon = imageCollection1.Images[0],
                    name = $"S_{i}"
                };

                students.Add(sItem);
            }

            treeList1.DataSource = students;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode != null)
            {
                //Get node parent
                var parentID = (int)treeList1.FocusedNode.GetValue(nameof(StudentTree.TreeID));

                var newItem = new StudentTree()
                {
                    TreeID = students.Count,
                    TreeParentID = parentID,
                    Icon = imageCollection1.Images[0],
                    name = $"Add_{students.Count}"
                };

                //Directly add
                students.Add(newItem);
                treeList1.RefreshDataSource();
            }


        }

        private void GetValidID()
        {

        }

        private void AddNode_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode != null)
            {
                //Get node parent
                var parentID = (int)treeList1.FocusedNode.GetValue(nameof(StudentTree.TreeID));

                var newItem = new StudentTree()
                {
                    TreeID = students.Count,
                    TreeParentID = parentID,
                    Icon = imageCollection1.Images[0],
                    name = $"Add_{students.Count}"
                };

                //Directly add
                //treeList1.FocusedNode.Nodes.Add(newItem);
                var nodeValues = new object[] { newItem.TreeID, newItem.TreeParentID, newItem.Icon, newItem.name, newItem.id, newItem.age }; //Tree node only accept this
                //treeList1.AppendNode(nodeValues, treeList1.FocusedNode);//Same effects
                treeList1.FocusedNode.Nodes.Add(nodeValues);                
            }

        }
    }


    public class StudentTree : Student
    {

        [Display(AutoGenerateField = false)]
        public int TreeID { get; set; }


        /// <summary>
        /// -1 is root object
        /// </summary>
        [Display(AutoGenerateField = false)]
        public int TreeParentID { get; set; }

        public Image Icon { get; set; }

        public StudentTree()
        {

        }
    }
}