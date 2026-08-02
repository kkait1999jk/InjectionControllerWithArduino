using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InjectionController
{
    class Program
    {
        static void Main(string[] arg)
        {
            /*
            String[] _PortName = new String[3] { "COM3", "COM5", "COM7" };
            int[] duty = new int[3] { 0, 128, 255 };
            bool ExecuteSprayWithFanOFF = false;
            short deviceID = 0;
            byte tmp = 2;
            double duration = 64;
            int repeat = 2;
            double interval = 10000;
            String portName = _PortName[1];
            int baudRate = 9600;
            int Duty = duty[2];
            Injection.ExecuteSprayWithFan(ExecuteSprayWithFanOFF, deviceID, tmp, duration, interval, repeat, portName, baudRate, Duty);
            */

            short deviceID = 0;
            byte tmp = 14;
            double duration = 50000;
            int repeat = 1;
            double interval = 0;
            Injection.ExecuteSpray(deviceID, tmp, duration, interval, repeat);
        }
    }
}
