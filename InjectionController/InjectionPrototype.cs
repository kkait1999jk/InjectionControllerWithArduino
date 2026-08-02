using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace InjectionController
{
    class InjectionPrototype
    {
        static public void Spray(byte tmp, double msec)
        {
            Stopwatch stopwatch = new Stopwatch();
            TUSBKRL_Import.Class1.TUSBKRL_Device_Open(0);
            TUSBKRL_Import.Class1.TUSBKRL_Set(0, tmp);
            stopwatch.Start();
            //Elapsed(TimeSpan)からの経過時間を取得してその値がmsecより小さいかどうか.
            while (stopwatch.Elapsed.TotalMilliseconds < msec)
            {
                //何も処理を行わない
            }
            stopwatch.Stop();
            tmp -= tmp; 
            TUSBKRL_Import.Class1.TUSBKRL_Set(0, tmp);
            TUSBKRL_Import.Class1.TUSBKRL_Device_Close(0);
        }
    }
}
