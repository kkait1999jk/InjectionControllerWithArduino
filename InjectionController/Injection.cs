using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Media;

namespace InjectionController
{
    class Injection
    {
        static private SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.jihou);
        public static bool ExecuteSprayWithFan(bool ExecuteSprayWithFanOFF, short deviceID, byte tmp, double msec, double interval, int repeat, String port_name, int baud_rate, int duty)
        {
            //short deviceID_copy = deviceID;
            //byte tmp_copy = tmp;
            if (!ExecuteSprayWithFanOFF)
            {
                ExecuteSprayWithFanOFF = true;
                const int min_value = 1;
                //msec, 射出時間
                //interval, インターバル
                //repeat, Spray()関数を繰り返す回数
                bool SprayOFF = false; //初期値
                int SprayCounter = 0; //初期値
                int IntervalCounter = 0; //初期値

                if (repeat < min_value)
                {
                    Debug.WriteLine("Sprayは呼び出されません");
                    return ExecuteSprayWithFanOFF;
                }
                else
                {
                    FanController.OpenSerialPort(port_name, baud_rate);
                    FanController.MoveFan(duty);
                    if (interval < min_value)
                    {
                        msec *= repeat;
                        SprayCounter = repeat;
                        IntervalCounter = repeat;
                    }
                    
                    do
                    {
                        //電圧を印加するケーブルを切り替える処理orインタフェースボックスの切り替え
                        /*
                        if (SprayCounter % 2 == 0)
                        {
                            //偶数だった場合
                            tmp = tmp_copy;
                        }
                        else
                        {
                            //奇数だった場合
                            deviceID = 1;
                            tmp = 2;
                        }
                        */
                        //Debug.WriteLine("tmp: {0}", tmp);
                        SprayOFF = Spray(deviceID, tmp, SprayOFF, msec, interval, ref SprayCounter, ref IntervalCounter);
                        //Debug.WriteLine("SprayCounter: {0}", SprayCounter);
                    } while (IntervalCounter < repeat);
                    FanController.StopFan();
                    ExecuteSprayWithFanOFF = false;
                    Debug.WriteLine("Complete!");
                }
            }
            //ExecuteSprayWithFanOFFの状態によってExecuteSprayWithFan()関数の中の処理を行うか判定する
            return ExecuteSprayWithFanOFF;
        }

        public static bool ExecuteSprayWithBool(bool ExecuteSprayOFF, short deviceID, byte tmp, double msec, double interval, int repeat)
        {
            //short deviceID_copy = deviceID;
            //byte tmp_copy = tmp;
            if (!ExecuteSprayOFF)
            {
                ExecuteSprayOFF = true;
                const int min_value = 1;
                //msec, 射出時間
                //interval, インターバル
                //repeat, Spray()関数を繰り返す回数
                bool SprayOFF = false; //初期値
                int SprayCounter = 0; //初期値
                int IntervalCounter = 0; //初期値

                if (repeat < min_value)
                {
                    Debug.WriteLine("Sprayは呼び出されません");
                    return ExecuteSprayOFF;
                }
                else
                {
                    if (interval < min_value)
                    {
                        msec *= repeat;
                        SprayCounter = repeat;
                        IntervalCounter = repeat;
                    }
                    do
                    {
                        //電圧を印加するケーブルを切り替える処理orインタフェースボックスの切り替え
                        /*
                        if (SprayCounter % 2 == 0)
                        {
                            //偶数だった場合
                            tmp = tmp_copy;
                        }
                        else
                        {
                            //奇数だった場合
                            deviceID = 1;
                        }
                        */
                        //Debug.WriteLine("tmp: {0}", tmp);
                        SprayOFF = Spray(deviceID, tmp, SprayOFF, msec, interval, ref SprayCounter, ref IntervalCounter);
                        //Debug.WriteLine("SprayCounter: {0}", SprayCounter);
                    } while (IntervalCounter < repeat);
                    ExecuteSprayOFF = false;
                    Debug.WriteLine("Complete!");
                }
            }
            //ExecuteSprayOFFの状態によってExecuteSprayWithBool()関数の中の処理を行うか判定する
            return ExecuteSprayOFF;
        }

        public static void ExecuteSpray(short deviceID, byte tmp, double msec, double interval, int repeat)
        {
            //short deviceID_copy = deviceID;
            //byte tmp_copy = tmp;
            const int min_value = 1;
            //msec, 射出時間
            //interval, インターバル
            //repeat, Spray()関数を繰り返す回数
            bool SprayOFF = false; //初期値
            int SprayCounter = 0; //初期値
            int IntervalCounter = 0; //初期値

            if (repeat < min_value)
            {
                Debug.WriteLine("Sprayは呼び出されません");
                return;
            }
            else
            {
                if (interval < min_value)
                {
                    msec *= repeat;
                    SprayCounter = repeat;
                    IntervalCounter = repeat;
                }
                do
                {
                    //電圧を印加するケーブルを切り替える処理orインタフェースボックスの切り替え
                    /*
                    if (SprayCounter % 2 == 0)
                    {
                        //偶数だった場合
                        tmp = tmp_copy;
                    }
                    else
                    {
                        //奇数だった場合
                        deviceID = 1;
                    }
                    */
                    //Debug.WriteLine("tmp: {0}", tmp);
                    SprayOFF = Spray(deviceID, tmp, SprayOFF, msec, interval, ref SprayCounter, ref IntervalCounter);
                    //Debug.WriteLine("SprayCounter: {0}", SprayCounter);
                } while (IntervalCounter < repeat);
                Debug.WriteLine("Complete!");
            }
        }

        static private bool Spray(short deviceID,byte tmp, bool sprayOff, double msec, double interval, ref int spray_counter, ref int inter_counter)
        {
            if (!sprayOff)
            {//射出前に約3000ミリ秒間のビープ音を鳴らす
                double loading_time = 3000;
                soundPlayer.Play();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                //Elapsed(TimeSpan)からの経過時間を取得してその値がloadingTimeより小さいかどうか.
                while (stopwatch.Elapsed.TotalMilliseconds < loading_time)
                {
                    //何も処理を行わない
                }
                stopwatch.Restart();
                spray_counter++;
                //Stopwatch stopwatch = new Stopwatch();
                TUSBKRL_Import.Class1.TUSBKRL_Device_Open(deviceID);
                TUSBKRL_Import.Class1.TUSBKRL_Set(deviceID, tmp);
                stopwatch.Start();
                //Elapsed(TimeSpan)からの経過時間を取得してその値がmsecより小さいかどうか.
                while (stopwatch.Elapsed.TotalMilliseconds < msec)
                {
                    //何も処理を行わない
                }
                stopwatch.Stop();
                tmp -= tmp;
                TUSBKRL_Import.Class1.TUSBKRL_Set(deviceID, tmp);
                TUSBKRL_Import.Class1.TUSBKRL_Device_Close(deviceID);
                sprayOff = true;
            }
            else
            {
                inter_counter++;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                //Elapsed(TimeSpan)からの経過時間を取得してその値がintervalより小さいかどうか.
                while (stopwatch.Elapsed.TotalMilliseconds < interval)
                {
                    //何も処理を行わない
                }
                stopwatch.Stop();
                sprayOff = false;
            }
            return sprayOff;
        }
    }
}
