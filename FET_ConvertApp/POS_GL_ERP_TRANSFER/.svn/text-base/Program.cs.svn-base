using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;


namespace POS_GL_ERP_TRANSFER
{
    class Program
    {
        static string sMsg;
        static void Main(string[] args)
        {
            sMsg = "";
            //初始化LOG
            Console.WriteLine("POS_GL_ERP_TRANSFER");
            Console.WriteLine("1.初始化LOG");
            ConvertLog con_log = new ConvertLog("POS_GL_ERP_TRANSFER");

            try
            {
                //GET NEW POS DATA
                Console.WriteLine("2.查詢POS_GL(WEB)");
                DataTable dtNEW = Query_POS_GL();
                sMsg += "POS_GL(WEB):查詢筆數:" + dtNEW.Rows.Count.ToString() + "\n";
                UpdateAndInsert_Store(dtNEW);

                //成功資訊
                Console.WriteLine("3.執行結束，寫入LOG");
                con_log.Success(sMsg);
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                //失敗資訊
                Console.WriteLine("例外產生");
                con_log.Fail( ex.Message);
                Console.WriteLine(ex.Message);
                Thread.Sleep(10000);
            }
        }

        public static void UpdateAndInsert_Store(DataTable dtAdd)
        {
            OracleConnection objConnOld = null;
            OracleConnection objConnNew = null;
            OracleTransaction objTXOld = null;
            OracleTransaction objTXNew = null;

            try
            {
                objConnOld = OracleDBUtil.GetERPPOSConnection();//OLD
                objConnNew = OracleDBUtil.GetConnection();//NEW

                objTXOld = objConnOld.BeginTransaction();
                objTXNew = objConnNew.BeginTransaction();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (dtAdd.Rows.Count > 0)
                {
                    //OLD POS
                    foreach (DataRow dr in dtAdd.Rows)
                    {
                        sb.Length = 0;
                        sb.AppendLine(
                            @"Insert into ERP_GL
                          ( MYNO, COMPANYCODE, 
                            ACCDATE, ACCTYPE, 
                            GLCODE1, GLCODE2, GLCODE3, 
                            GLCODE4, GLCODE5, GLCODE6, 
                            AMOUNT)
                          Values
                          ( [MYNO], [COMPANYCODE], 
                            [ACCDATE], [ACCTYPE], 
                            [GLCODE1], [GLCODE2], [GLCODE3], 
                            [GLCODE4], [GLCODE5], [GLCODE6], 
                            [AMOUNT])"
                        );
                        sb.Replace("[MYNO]", OracleDBUtil.SqlStr(dr["MYNO"].ToString())); 
                        sb.Replace("[COMPANYCODE]", "'01'"); 
                        sb.Replace("[ACCDATE]", "to_date("+OracleDBUtil.SqlStr(dr["GL_DATE"].ToString()) + ",'YYYYMMDD')" ); 
                        sb.Replace("[ACCTYPE]", OracleDBUtil.SqlStr(dr["DRCR"].ToString()));
                        string sAccountCode = dr["ACCOUNT_CODE"].ToString();
                        string s01 = sAccountCode.Substring(0, 2) ;
                        string s02 = sAccountCode.Substring(2, 3) ;
                        string s03 = dr["STORE_NO"].ToString();
                        string s04 = sAccountCode.Substring(9, 6) ;
                        string s05 = sAccountCode.Substring(15, 4) ;
                        string s06 = sAccountCode.Substring(19) ;
                        sb.Replace("[GLCODE1]", OracleDBUtil.SqlStr(s01));
                        sb.Replace("[GLCODE2]", OracleDBUtil.SqlStr(s02));
                        sb.Replace("[GLCODE3]", OracleDBUtil.SqlStr(s03));
                        sb.Replace("[GLCODE4]", OracleDBUtil.SqlStr(s04));
                        sb.Replace("[GLCODE5]", OracleDBUtil.SqlStr(s05));
                        sb.Replace("[GLCODE6]", OracleDBUtil.SqlStr(s06));
                        sb.Replace("[AMOUNT]", OracleDBUtil.SqlStr(dr["AMT"].ToString()));
                        OracleDBUtil.ExecuteSql(objTXOld, sb.ToString());
                    }

                    //New POS update status 
                    sb.Length = 0;
                    sb.Append("UPDATE POS_GL ");
                    sb.Append("   SET CC2ERP_FLAG='Y',MODI_USER='CONVERT',MODI_DTM=SYSDATE,CC2ERP_ID='CONVERT',CC2ERP_DTM=SYSDATE ");
                    sb.Append(" WHERE CC2ERP_FLAG='T'  ");
                    OracleDBUtil.ExecuteSql(objTXNew, sb.ToString());

                    objTXOld.Commit();
                    objTXNew.Commit();
                }
                sMsg += "ERP_GL(WEB):新增筆數:" + dtAdd.Rows.Count.ToString() + "\n";
                
            }
            catch (Exception ex)
            {
                if (objTXOld != null) objTXOld.Rollback();
                if (objTXNew != null) objTXNew.Rollback();
                sMsg += "UpdateAndInsert_Store(DataTable dtAdd)執行發生錯誤\n";
                throw ex;
            }
            finally
            {
                if (objConnOld.State == ConnectionState.Open) objConnOld.Close();
                objConnOld.Dispose();
                objTXNew.Dispose();
                if (objConnNew.State == ConnectionState.Open) objConnNew.Close();
                objConnNew.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        public static DataTable Query_POS_GL()
        {
            OracleConnection oCon = null;
            try
            {
                oCon = OracleDBUtil.GetConnection();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("UPDATE POS_GL ");
                sb.Append("   SET CC2ERP_FLAG='T' ");
                sb.Append(" WHERE CC2ERP_FLAG is null or CC2ERP_FLAG='N' and ACCOUNT_CODE is not null  ");
                OracleDBUtil.ExecuteSql(oCon, sb.ToString());

                sb.Length = 0;
                sb.Append("SELECT * ");
                sb.Append("FROM POS_GL ");
                sb.Append("WHERE CC2ERP_FLAG='T'");

                DataTable dt = OracleDBUtil.GetDataSet(oCon, sb.ToString()).Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                sMsg += "Query_POS_GL()執行發生錯誤\n";
                throw ex;
            }
            finally
            {
                if (oCon.State == ConnectionState.Open) oCon.Close();
                oCon.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        

        #region 備份20110125
        //static void Main(string[] args)
        //{
        //    //初始化LOG
        //    ConvertLog con_log = new ConvertLog("POS_GL_ERP_TRANSFER");

        //    try 
        //    {
        //        //GET NEW POS DATA
        //        DataTable dtNEW = GetOLdPosData();

        //        POS_GL_ERP_TRANSFER_Facade cFacade = new POS_GL_ERP_TRANSFER_Facade();

        //        Dictionary<string, string> dic = new Dictionary<string, string>();
        //        //< NEW_COLUMN_NAME,OLD_COLUMN_NAME >
        //        //dic.Add("GL_DATE", "ACCDATE");String -> Date
        //        dic.Add("DRCR", "ACCTYPE");
        //        dic.Add("AMT", "AMOUNT");
        //        dic.Add("POS_GL_ID", "MYNO");

        //        POS_GL_ERP_TRANSFER_DTO dsAdd = new POS_GL_ERP_TRANSFER_DTO();
        //        POS_GL_ERP_TRANSFER_DTO.ERP_GLDataTable dtAdd = dsAdd.ERP_GL;
        //        //POS_GL_ERP_TRANSFER_DTO dsUpd = new POS_GL_ERP_TRANSFER_DTO();
        //        //STORE_PORTAL_UPDATE_DTO.STOREDataTable dtUpd = dsUpd.STORE;
        //        foreach (DataColumn dc in dtAdd.Columns)
        //        {
        //            dc.AllowDBNull = true;
        //        }

        //        for (int i = 0; i < dtNEW.Rows.Count; i++)
        //        {
        //            DataRow drNew = dtNEW.Rows[i];
        //            POS_GL_ERP_TRANSFER_DTO.ERP_GLRow drAdd = dtAdd.NewERP_GLRow();
        //            foreach (KeyValuePair<string, string> pair in dic)
        //            {
        //                drAdd[pair.Value] = drNew[pair.Key];
        //            }
        //            drAdd["COMPANYCODE"] = "01";
        //            string YYYYMMDD = drNew["GL_DATE"].ToString();
        //            DateTime dTmp = Convert.ToDateTime(YYYYMMDD.Substring(0, 4) + "/" + YYYYMMDD.Substring(4, 2) + "/" + YYYYMMDD.Substring(6, 2));
        //            drAdd["ACCDATE"] = dTmp;
        //            string sAccountCode = drNew["ACCOUNT_CODE"].ToString();
        //            string s01 = sAccountCode.Substring(0, 2);
        //            string s02 = sAccountCode.Substring(2, 3);
        //            //string s03 = sAccountCode.Substring(5, 4);
        //            string s03 = drNew["STORE_NO"].ToString();
        //            string s04 = sAccountCode.Substring(9, 6);
        //            string s05 = sAccountCode.Substring(15, 4);
        //            string s06 = sAccountCode.Substring(19, 4);
        //            drAdd["GLCODE1"] = s01;
        //            drAdd["GLCODE2"] = s02;
        //            drAdd["GLCODE3"] = s03;
        //            drAdd["GLCODE4"] = s04;
        //            drAdd["GLCODE5"] = s05;
        //            drAdd["GLCODE6"] = s06;

        //            dtAdd.Rows.Add(drAdd);
        //        }
        //        dtAdd.AcceptChanges();

        //        cFacade.UpdateAndInsert_Store(dtAdd);

        //        //成功資訊
        //        con_log.Success("POS_GL_ERP_TRANSFER:ERP_GL異動筆數"+dtAdd.Rows.Count.ToString());
        //    }
        //    catch (Exception ex) 
        //    {
        //        //失敗資訊
        //        con_log.Fail(ex.Message);

        //        throw ex; 
        //    }
        //}

        //static DataTable GetOLdPosData()
        //{
        //    DataTable dt = new DataTable();
        //    dt = new POS_GL_ERP_TRANSFER_Facade().Query_POS_GL();
        //    return dt;
        //}
        #endregion
    }

    
}
