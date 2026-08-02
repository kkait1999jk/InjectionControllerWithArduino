using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;

namespace InjectionController
{
    class FanController
    {
        private static SerialPort serialPort;

        //Arduinoとのシリアル通信に必要な設定
        //このDLL内の関数を使用するときに一番最初に呼び出す
        public static void OpenSerialPort(String portName, int baudRate)
        {
            serialPort = new SerialPort();
            serialPort.BaudRate = baudRate;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;
            serialPort.PortName = portName;  //PC環境によって変化する
            serialPort.Open();
        }

        //Duty比を引数として受取る、128 - 255(50% - 100%)の間で受け取る
        //受取った値のDuty比でファンを出力する、停止命令があるまで出力し続ける
        public static void MoveFan(int Duty)
        {
            serialPort.Write(Duty + "\n");
        }

        //ファンを停止させる
        public static void StopFan()
        {
            serialPort.Write("0\n");
            CloseSerialPort();
        }

        private static void CloseSerialPort()
        {
            if (serialPort != null)
            {
                serialPort.Close();
                serialPort.Dispose();
            }
        }
    }
}