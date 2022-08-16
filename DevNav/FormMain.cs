﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars.Navigation;


public partial class FormMain : DevExpress.XtraEditors.XtraForm
{
    /// <summary>
    /// Used for selection area display
    /// </summary>
    public SkinElement skinElement;
    private AccordionControlElement clickedGroupElement;

    public FormMain()
    {
        InitializeComponent();
        this.Load += FormMain_Load;
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
        this.LookAndFeel.SetSkinStyle(SkinStyle.WXICompact, SkinSvgPalette.WXICompact.Clearness);

        //Init variables
        //skinElement = SkinManager.GetSkinElement(SkinProductId.AccordionControl, UserLookAndFeel.Default, "Item");
        skinElement = SkinManager.GetSkinElement(SkinProductId.AccordionControl, this.LookAndFeel, "Item");


        //Init according control
        accordionControl1.AllowItemSelection = true;//Item can be selected
        accordionControl1.ShowFilterControl = ShowFilterControl.Always; //Enable search function
        accordionControl1.CustomDrawElement += AccordionControl1_CustomDrawElement;
        accordionControl1.MouseClick += AccordionControl1_MouseClick;

    }

    private void AccordionControl1_MouseClick(object sender, MouseEventArgs e)
    {
        var hitInfo = accordionControl1.CalcHitInfo(e.Location);
        if (hitInfo.HitTest == AccordionControlHitTest.Item || hitInfo.HitTest == AccordionControlHitTest.Group)
        {
            var element = hitInfo.ItemInfo.Element;
            element.Tag = MouseEventType.Click;


            if (clickedGroupElement != null && clickedGroupElement != element)
            {
                clickedGroupElement.Tag = MouseEventType.Normal;
            }


            if (hitInfo.HitTest == AccordionControlHitTest.Group)
            {
                accordionControl1.SelectedElement = null;
                clickedGroupElement = element;
            }
        }
    }

    private void AccordionControl1_CustomDrawElement(object sender, DevExpress.XtraBars.Navigation.CustomDrawElementEventArgs e)
    {
        if (!(e.Element.Tag is MouseEventType))
            return;

        var eventType = (MouseEventType)e.Element.Tag;

        //Draw group element selection
        if (e.Element.Style == ElementStyle.Group)
        {
            var info = new SkinElementInfo(skinElement, e.ObjectInfo.HeaderBounds) { Cache = e.Cache, ImageIndex = (int)eventType };
            SkinElementPainter.Default.DrawObject(info);

            e.DrawExpandCollapseButton();
            e.DrawText();
            e.Handled = true;
        }
    }





    private enum MouseEventType
    {
        Normal,
        Hover,
        Click
    }


}

