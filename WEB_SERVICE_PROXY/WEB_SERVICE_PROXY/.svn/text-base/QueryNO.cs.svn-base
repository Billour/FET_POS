using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Web;

namespace WEB_SERVICE_PROXY
{
    public class QueryNO
    {

        /// <summary>
        /// 取得銷售單號
        /// </summary>
        /// <param name="PA_Sheet_Type">"SALE"</param>
        /// <param name="STORE_NO">門市編號</param>
        /// <param name="MACHINE_ID">機台編號</param>
        /// <returns>銷售單號</returns>
        public string GetSALE_No(string PA_Sheet_Type, string STORE_NO, string MACHINE_ID)
        {
            string No = "";
            //**如果呼叫WebService異常發生時，則去呼叫專案本身的Method，以保持能夠正常取號。
            try
            {
                wsGetNo.ServiceGetNOSoapClient ws = new wsGetNo.ServiceGetNOSoapClient();
                No = ws.GetSALE_No(PA_Sheet_Type, STORE_NO, MACHINE_ID);
            }
            catch
            {
                ServiceGetNO service = new ServiceGetNO();
                No = service.GetSALE_No(PA_Sheet_Type, STORE_NO, MACHINE_ID);
            }

            return No;
        }

        /// <summary>
        /// 取得發票號碼
        /// </summary>
        /// <param name="STORE_NO">門市編號</param>
        /// <returns>發票號碼</returns>
        public string GetINVOICE_NO(string STORE_NO)
        {
            string No = "";
            //**如果呼叫WebService異常發生時，則去呼叫專案本身的Method，以保持能夠正常取號。
            try
            {
                wsGetNo.ServiceGetNOSoapClient ws = new wsGetNo.ServiceGetNOSoapClient();
                No = ws.GetINVOICE_NO(STORE_NO);
            }
            catch
            {
                ServiceGetNO service = new ServiceGetNO();
                No = service.GetINVOICE_NO(STORE_NO);
            }

            return No;
        }

        /// <summary>
        /// 取得收據號碼
        /// </summary>
        /// <param name="PA_Sheet_Type">"RECITT"</param>
        /// <returns>收據編號</returns>
        public string GetRECITT_NO(string PA_Sheet_Type)
        {
            string No = "";
            //**如果呼叫WebService異常發生時，則去呼叫專案本身的Method，以保持能夠正常取號。
            try
            {

                wsGetNo.ServiceGetNOSoapClient ws = new wsGetNo.ServiceGetNOSoapClient();
                No = ws.GetRECITT_NO(PA_Sheet_Type);
            }
            catch
            {
                ServiceGetNO service = new ServiceGetNO();
                No = service.GetRECITT_NO(PA_Sheet_Type);
            }

            return No;
        }

    }
}
