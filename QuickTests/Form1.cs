﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickTests
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1);
            }

            watch.Stop();
            Debug.WriteLine($"Elipsed:{watch.ElapsedMilliseconds}");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            this.Visible = true;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
        }


        bool[] bitData = new bool[32];
                
        private void button2_Click(object sender, EventArgs e)
        {
            //Prepare data
            bitData[0] = true;
            bitData[1] = true;
            bitData[31] = true;


            string s = BoolArrayToHexString(bitData);

            bitData=HexStringToBoolArray(s);
        }


      


    }
}
