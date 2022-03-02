﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_Ink
{
    class InkSystemModel
    {
        public static string RegDeviceList = "^[0-9]{1,2},I$";
    }

    public class ResponseString
    {
        public static string Processed = ",A";
        public static string Success = ",C";
    }

    public enum InkSystemCommandType
    {
        GetNetworkDevices,
        SetNetworkID,
        GetMeniscusPressure,
        SetMeniscusPressure,
        GetMeniscusPumpLoad,
        SetMeniscusPumpLoad,
        GetRecircMeniscusPressure,
        SetRecircMeniscusPressure,
        GetNonRecircMeniscusPressure,
        SetNonRecircMeniscusPressure,
        GetReturnPressure,
        SetReturnPressure,
        GetReturnPumpLoad,
        SetReturnPumpLoad,
        GetHeater,
        SetHeater,
        GetFillPumpSpeed,
        SetFillPumpSpeed,
        GetFillPumpTimeout,
        SetFillPumpTimeout,
        GetPurgeTime,
        SetPurgeTime,
        GetPurgePressure,
        SetPurgePressure,
        GetStartupDelay,
        SetStartupDelay,
        GetBypassTime,
        SetBypassTime,
        GetDeviceStatus
    }


    /// <summary>
    /// Buffer settings of the ink system
    /// </summary>
    public class InkSystemDataBuffer: InkDeviceData
    {
        public List<int> Devices { get; set; }
        public InkSystemDataBuffer()
        {
            Devices = new List<int>();
        }


    }

    public class InkDeviceData
    {
        public double BackPressure { get; set; }
        
        public double RecirculationPressure { get; set; }
        public double HeaterTemp { get; set; }
        public double InkTempreture { get; set; }
        public double StatusBits { get; set; }
        public double Alarm { get; set; }
        public double MeniscusPressureSetPoint { get; set; }


        public void CopyDeviceData(ref InkDeviceData deviceData)
        {
            deviceData.BackPressure = BackPressure;
            deviceData.RecirculationPressure = RecirculationPressure;
            deviceData.HeaterTemp = HeaterTemp;
            deviceData.InkTempreture = InkTempreture;
            deviceData.StatusBits = StatusBits;
            deviceData.Alarm = Alarm;
        }
    }

    /// <summary>
    /// Ink system commands status
    /// </summary>
    public class InkSystemCommand
    {
        /// <summary>
        /// Inidcate whether a response received from ink device
        /// </summary>
        public bool IsReplied { get; set; }

        /// <summary>
        /// Indicate if the command is successfully finished
        /// </summary>
        public bool IsSuccess { get; set; }

        public int NetworkID { get; set; }

        /// <summary>
        /// Command error code
        /// </summary>
        public byte ErrorCode { get; set; }
        public InkSystemCommandType Type { get; set; }
        /// <summary>
        /// command to be sent
        /// </summary>
        public string CommandString { get; set; }
        public int IntValue { get; set; }
        public double DoubleValue { get; set; }
        /// <summary>
        /// String result of current command
        /// </summary>
        public string StrValue { get; set; }
        /// <summary>
        /// Time spent on this command
        /// </summary>
        private Stopwatch CMDTime { get; set; }

        /// <summary>
        /// Time span in ms
        /// </summary>
        public long SpanMs => CMDTime.ElapsedMilliseconds;

        public InkSystemCommand()
        {
            CMDTime = new Stopwatch();
        }

        public void Init(InkSystemCommandType Command, int iNetwork = 0)
        {
            IsReplied = false;
            IsSuccess = false;
            Type = Command;
            IntValue = -1;
            StrValue = null;
            NetworkID = iNetwork;
            CommandString = csInkSystem.GetCommandString(Command, iNetwork);
            CMDTime.Restart();
        }

        /// <summary>
        /// Reset operation
        /// </summary>
        public void Reset()
        {
            IsReplied = false;
            Type = 0;
            IntValue = -1;
            StrValue = null;
            CMDTime.Reset();
        }
    }
}
