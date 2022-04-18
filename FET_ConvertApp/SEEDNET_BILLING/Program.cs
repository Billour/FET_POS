using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Threading;

namespace SEEDNET_BILLING
{
    class Program
    {
        static void Main(string[] args)
        {


            //2.初始化LOG
            ConvertLog log = new ConvertLog("SEEDNET_BILLING");
            SEEDNET_BILLING sb = new SEEDNET_BILLING(log);
            try
            {
                Console.WriteLine("1.Update SEEDNET_BILLING_M SEND_STATUS '0' -> 'T' ");
                sb.Init();
                Console.WriteLine("2.讀取資料");
                sb.Load(); //讀取資料           
                Console.WriteLine("3.產生實體檔案");
                sb.ExportFile();//產生實體檔案
                Console.WriteLine("4.Update SEEDNET_BILLING_M SEND_STATUS 'T' -> '1' ");
                sb.Update();
                sb.Commit();
                sb.RunFTP();
                Console.WriteLine("5.SEEDNET_BILLING 處理完成 ");
                log.Success("SEEDNET_BILLING 處理完成");
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                sb.Rollback();
                Console.WriteLine(ex.Message);
                //Console.ReadKey();
                Thread.Sleep(3000);
                log.Fail(ex.Message);
            }
            finally
            {
                sb.Close();
            }


        }
    }



}
