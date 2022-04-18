using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using FET.POS.Model.Helper;
using FET.POS.Model.DTO;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class ORD09_Facade
    {
        private DateTime SysDate = DateTime.Now;

        /// <summary>
        /// 新增DropShipment主動配貨主檔
        ///     DropShipment主動配貨明細檔
        /// </summary>
        /// <param name="sdt"></param>
        /// <param name="MODI_USER"></param>
        public static DataTable SP_CHECK_DISTRIBUTES_ORDER(string I_BATCH_NO, string I_USER_ID, string I_FINC_ID)
        {
            DataTable O_DATA = new DataTable();
            using (OracleConnection oConn = OracleDBUtil.GetConnection())
            {
                OracleCommand oraCmd = new OracleCommand("SP_CHECK_DISTRIBUTES_ORDER");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_BATCH_NO", OracleType.VarChar, 2000)).Value = I_BATCH_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_USER_ID", OracleType.VarChar, 2000)).Value = I_USER_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_FINC_ID", OracleType.VarChar, 2000)).Value = I_FINC_ID;
                oraCmd.Parameters.Add(new OracleParameter("O_DATA", OracleType.Cursor)).Direction = ParameterDirection.Output;
                oraCmd.Connection = oConn;
                oraCmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(oraCmd);
                da.Fill(O_DATA);
                return O_DATA;
            }
            
        }

        public static DataTable geteditTable(string UUID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from upload_temp ");
            sb.Append(" WHERE 1=1 ");
            if (UUID != "")
            {
                sb.Append(" AND BATCH_NO = " + OracleDBUtil.SqlStr(UUID));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public void UpdateOne_UPLOAD(ORD09_DropShipment ds)
        {
            OracleDBUtil.UPDDATEByUUID(ds.Tables["UPLOAD_TEMP"], "F6");
        }

        public static DataTable checknum(string I_Batch_NO)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  select batch_no, f1, f3, count(f2) as cnt from upload_temp ");
            sb.Append(" WHERE 1=1 ");
            if (I_Batch_NO != "")
            {
                sb.Append(" AND batch_no = " + OracleDBUtil.SqlStr(I_Batch_NO));
            }
            sb.Append(" group by batch_no, f1, f3 ");
            sb.Append(" having count(f2) > 1 ");

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());
            return dt;
        }

        public string GetQTY(string PRODNO)
        {
            string str = "0";

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ATRQTY ");
            sb.Append("FROM   POS_ATR ");
            sb.Append(" WHERE 1=1 and DS_FLAG='Y' and TO_CHAR(DWNDATE,'yyyy/mm/dd') = '" + DateTime.Now.ToString("yyyy/MM/dd") + "' ");
            if (PRODNO != "")
            {
                sb.Append(" AND PRODNO = " + OracleDBUtil.SqlStr(PRODNO));
            }

            DataTable dt = OracleDBUtil.Query_Data(sb.ToString());

            foreach (DataRow dr in dt.Rows)
            {
                str = dr["ATRQTY"].ToString();
                if (str == "")
                {
                    str = "0";
                }
            }

            return str;
        }

        public void updateNO(string Serialno)
        {
            //string str = "0";
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();
              //  string checkpara = Serialno.Substring(2, 8);
                int xx = Convert.ToInt32(Serialno.Substring(8, 2));
                string serialno =  Convert.ToString(Convert.ToInt32(Serialno.Substring(8, 2)) + 1);
                serialno = serialno.Length < 2 ? "0" + serialno : serialno;
                Serialno = Serialno.Substring(0, 8) + serialno.Substring(serialno.Length - 2);
                StringBuilder sb = new StringBuilder();
                sb.Append("update SYS_PARA ");
                sb.Append("set PARA_VALUE =" + OracleDBUtil.SqlStr(Serialno) + " ");
                sb.Append(" WHERE PARA_KEY = 'HR_ORDER_NO' ");

                objConn = OracleDBUtil.GetConnection();
                //DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];
                OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                objTX.Commit();
               // return str;
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
        /// <summary>
        /// 編訂單編號HRyymmdd+二碼流水號
        /// </summary>
        /// <param name="PRODNO"></param>
        /// <returns></returns>
        public string GetSysPara(string PRODNO)
        {
            string str = "0";
            OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT PARA_VALUE ");
                sb.Append("FROM   SYS_PARA ");
                sb.Append(" WHERE PARA_KEY = 'HR_ORDER_NO' ");
               
                objConn = OracleDBUtil.GetConnection();
                DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    str = dr["PARA_VALUE"].ToString();
                    if (str == "")
                    {
                        str = "0";
                    }
                }
                
                string checkpara = str.Substring(2, 6);
                string today = DateTime.Now.ToString("yyyyMMdd").Substring(2,6);
           
                string reallystr = "";
                if (checkpara != today)
                {
                    reallystr = str.Substring(0, 2) + DateTime.Now.ToString("yyyyMMdd").Substring(2,6) + "01";
                    updateNO(reallystr);
                    str = reallystr;
                }
                else
                {
                  //  int xx = Convert.ToInt32(str.Substring(11, 3));
                  //  string serialno = "00" + Convert.ToString(Convert.ToInt32(str.Substring(11, 3)) + 1);
                  //  serialno = str.Substring(0,2) + checkpara + "-" + serialno.Substring(serialno.Length - 3);
                    updateNO(str);
                }

                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public void Insert_DISTRIBUTES_ORDER(DataTable sdt, string MODI_USER)
        {
            if (sdt.Rows.Count > 0)//如果有資料才新增
            {
                OracleConnection objConn = null;
                OracleTransaction objTX = null;
                try
                {
                    objConn = OracleDBUtil.GetConnection();
                    objTX = objConn.BeginTransaction();
                    ORD09_Facade facade09 = new ORD09_Facade();
                //    string Syspara = facade09.GetSysPara("");
                  

                    ORD09_DISTRIBUTES_ORDER.HQ_DS_ORDER_MDataTable dtm = new ORD09_DISTRIBUTES_ORDER.HQ_DS_ORDER_MDataTable();
                    ORD09_DISTRIBUTES_ORDER.HQ_DS_ORDER_MRow dtmr = dtm.NewHQ_DS_ORDER_MRow();
                    dtmr.HQ_ORDER_M_ID = Advtek.Utility.GuidNo.getUUID().ToString(); //主KEY值
                    //**2011/04/17 Tina：DS單號的由SEQSEGMENT TABLE計算而來。
                    dtmr.HQ_DS_ORDER_NO = SerialNo.GenNo("DRORDER");// GetSysPara("");
                    dtmr.STATUS = "2";
                    dtmr.CREATE_DTM = SysDate;
                    dtmr.CREATE_USER = MODI_USER;
                    dtmr.MODI_DTM = SysDate;
                    dtmr.MODI_USER = MODI_USER;
                    dtm.AddHQ_DS_ORDER_MRow(dtmr);
                    dtm.AcceptChanges();

                    ORD09_DISTRIBUTES_ORDER.HQ_DS_ORDER_DDataTable dtd = new ORD09_DISTRIBUTES_ORDER.HQ_DS_ORDER_DDataTable();
                    foreach (DataRow dr in sdt.Rows)
                    {
                        ORD09_DISTRIBUTES_ORDER.HQ_DS_ORDER_DRow dtdr = dtd.NewHQ_DS_ORDER_DRow();
                        
                        dtdr.HQ_ORDER_M_ID = dtmr.HQ_ORDER_M_ID; //表頭主KEY
                        dtdr.HQ_ORDER_D = Advtek.Utility.GuidNo.getUUID().ToString(); // 表身主KEY
                        dtdr.STORE_NO = dr["STORENO"].ToString();
                        dtdr.DIS_QTY = Convert.ToDecimal(dr["DIS_QTY"]);
                        dtdr.PRODNO = dr["PRODNO"].ToString();
                        dtdr.AUTO_DIS_FLAG = "Y";//drop shirpment;
                        dtdr.CREATE_DTM = SysDate;
                        dtdr.CREATE_USER = MODI_USER;
                        dtdr.MODI_DTM = SysDate;
                        dtdr.MODI_USER = MODI_USER;
                        dtdr.ATR_QTY = Convert.ToDecimal(GetQTY(dr["PRODNO"].ToString()));
                        dtd.AddHQ_DS_ORDER_DRow(dtdr);
                        dtd.AcceptChanges();
                    }
                    OracleDBUtil.Insert(objTX, dtm);
                    OracleDBUtil.Insert(objTX, dtd);
                    objTX.Commit();
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
                    throw ex;
                }
                finally
                {
                    objTX = null;

                    if (objConn.State == ConnectionState.Open) objConn.Close();
                    objConn.Dispose();
                    OracleConnection.ClearAllPools();
                }
            }
        }

        public void AddNew_UPLoad(ORD09_DropShipment ds)
        {
            OracleDBUtil.Insert(ds.Tables["UPLOAD_TEMP"]);
        }
        

    }

}
