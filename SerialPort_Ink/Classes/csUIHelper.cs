﻿using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPort_Ink
{
    public class csUIHelper
    {
        public static void InitGridview(GridView View, bool EnableEdit = false, bool ShowNewRow = false)
        {
            //Basic settings
            View.OptionsView.ShowGroupPanel = false; //User don't see group panel
            View.OptionsView.ShowIndicator = false; //Hide row header
            View.OptionsCustomization.AllowSort = false;//Disable sort
            View.OptionsCustomization.AllowFilter = false;//distable filter
            View.OptionsCustomization.AllowQuickHideColumns = false;//User can't drag and hide the column
            View.OptionsBehavior.AlignGroupSummaryInGroupRow = DefaultBoolean.True;//Display the summary in column lane         
            View.OptionsDetail.EnableMasterViewMode = false; //Disable group
            View.OptionsMenu.EnableColumnMenu = false; //Hide column menu

            //Editable
            //Disable edit(Notice: All editors in grid will be affected include Buttons, ComboBox)
            //To set editable for seperate items, enable edit first, then disable edit in specific column
            //nameColumn.OptionsColumn.AllowEdit = false;
            View.OptionsBehavior.Editable = EnableEdit;

            //New row
            if (ShowNewRow)
            {
                View.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                View.OptionsView.ShowIndicator = true; //User can distinguish new row
            }
            else
            {
                View.OptionsView.NewItemRowPosition = NewItemRowPosition.None; //Hide new function row
                View.OptionsView.ShowIndicator = false; //Hide row header
            }
        }

        public static void ShowCommandsDialog(BindingList<CommandInfo> commands)
        {
            using (FormCommands winCommands = new FormCommands(commands))
            {
                winCommands.StartPosition = FormStartPosition.CenterParent;
                winCommands.ShowDialog();
            }
        }
    }
}
