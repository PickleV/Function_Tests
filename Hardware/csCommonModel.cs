﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware
{
    class csCommonModel
    {

    }

    public class csMotherBoard
    {
        [CustomEditor(_editorType.Text)]
        public double Temp { get; set; }
    }

    public class csDisk
    {
        public string DiskID { get; set; }

        [CustomEditor(_editorType.Text)]
        public string FirstDiskID { get; set; }
    }

}
