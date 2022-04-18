using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace REASON_IMPORT
{
    class Program
    {
        private static ConvertLog con_log;
        private static string sMSG="";
        static void Main(string[] args)
        {

            OutputMsg("1.初始化LOG");
            con_log = new ConvertLog("REASON_IMPORT");
            
            try
            {
                //** CLEAR WEBPOS.DEPT_TEMP
                //** FETDB01T.REASON -> WEBPOS.REASON_TEMP
                OutputMsg("2.FETDB01T.REASON -> WEBPOS.REASON_TEMP");
                FETDB01T_REASON_WEBPOS_REASON_TEMP();
                
                //**003比對 WEBPOS.DEPT_TEMP WEBPOS.DEPT
                //Console.WriteLine("   3.比對 WEBPOS.Reason_TEMP WEBPOS.Reason");
                //string sMsg = ReasonTemp2Reason();
               
                //成功資訊
                OutputMsg("3.<=執行完畢，寫入成功資訊");
                con_log.Success(sMSG);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                //失敗資訊
                //OutputMsg(ex.Message);
                con_log.Fail(sMSG);
                Thread.Sleep(3000);
            }
        }

        static void FETDB01T_REASON_WEBPOS_REASON_TEMP()
        {
            DataTable dt = new DataTable();
            OracleConnection objConn = null;
            OracleTransaction objTX = null;
            OracleConnection objConn_erp = null;
            try
            {
                OutputMsg("2.1 建立新舊DB連線");
                objConn = OracleDBUtil.GetConnection();
                objTX = objTX = objConn.BeginTransaction();
                objConn_erp = OracleDBUtil.GetERPPOSConnection();

                OutputMsg("2.2 查詢ERP.REASON");
                dt = OracleDBUtil.GetDataSet(
                    objConn_erp,
                    @"SELECT  *
                    FROM REASON
                    WHERE REASONTYPE='2' AND (COMPANYCODE='01'or COMPANYCODE='02') ").Tables[0]; //COMPANYCODE='02' 增加，但SP中只處理01資料

                if (dt.Rows.Count > 0)
                {
                    OutputMsg("2.3 清除REASON_TEMP(WEB)");
                    OracleDBUtil.ExecuteSql(objTX, "DELETE REASON_TEMP");

                    #region insert temp
                    OutputMsg("2.4 寫入REASON_TEMP");
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(
                            @"Insert into REASON_TEMP
                       (REASONTYPE, REASON, COMPANYCODE, 
                        CEASEDATE, ACCNO, UPDDATE, 
                        UPDUSRNO, ULSNO, REASONID, 
                        STATUS)
                        Values
                       (:A_REASONTYPE, :B_REASON, :COMPANYCODE, 
                        :CEASEDATE, :ACCNO, :UPDDATE, 
                        :UPDUSRNO, :ULSNO, :C_REASONID, 
                        :STATUS)");

                        sb.Replace(":A_REASONTYPE", OracleDBUtil.SqlStr(dr["REASONTYPE"].ToString()));
                        sb.Replace(":B_REASON", OracleDBUtil.SqlStr(dr["REASON"].ToString()));
                        sb.Replace(":COMPANYCODE", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                        sb.Replace(":CEASEDATE", OracleDBUtil.SqlStr(dr["CEASEDATE"].ToString()));
                        sb.Replace(":ACCNO", OracleDBUtil.SqlStr(dr["ACCNO"].ToString()));
                        sb.Replace(":UPDDATE", OracleDBUtil.SqlStr(dr["UPDDATE"].ToString()));
                        sb.Replace(":UPDUSRNO", OracleDBUtil.SqlStr(dr["UPDUSRNO"].ToString()));
                        sb.Replace(":ULSNO", OracleDBUtil.SqlStr(dr["ULSNO"].ToString()));
                        sb.Replace(":C_REASONID", OracleDBUtil.SqlStr(dr["REASONID"].ToString()));
                        sb.Replace(":STATUS", OracleDBUtil.SqlStr("T"));
                       
                        OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                    }

                    OutputMsg("2.5 查詢ERP.RETURN_REASON");
                    DataTable dt2 = OracleDBUtil.GetDataSet(
                                    objConn_erp,
                                  @"SELECT RETURN_REASON_CODE, COMPANYCODE, RETURN_DESCRIPTION
                                    FROM RETURN_REASON WHERE COMPANYCODE in ('01','02') ").Tables[0];
                    if (dt2.Rows.Count > 0) 
                    {
                        OutputMsg("2.6 清除RETURN_REASON_TEMP(WEB)");
                        OracleDBUtil.ExecuteSql(objTX, "DELETE RETURN_REASON_TEMP");
                        StringBuilder sb = new StringBuilder();
                        OutputMsg("2.7 寫入RETURN_REASON_TEMP");
                        foreach (DataRow dr in dt2.Rows)
                        {
                            sb.Length = 0;
                            sb.AppendLine(
                                @"Insert into RETURN_REASON_TEMP
                                  (RETURN_REASON_CODE, COMPANYCODE, RETURN_DESCRIPTION)
                                   Values
                                  ([RETURN_REASON_CODE], [COMPANYCODE], [RETURN_DESCRIPTION])");

                            sb.Replace("[RETURN_REASON_CODE]", OracleDBUtil.SqlStr(dr["RETURN_REASON_CODE"].ToString()));
                            sb.Replace("[COMPANYCODE]", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                            sb.Replace("[RETURN_DESCRIPTION]", OracleDBUtil.SqlStr(dr["RETURN_DESCRIPTION"].ToString()));
                            OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                        }

                    }

                    OutputMsg("2.8 查詢 ERP.AFTER_PROCESS");
                    DataTable dt3 = OracleDBUtil.GetDataSet(
                                    objConn_erp,
                                  @"SELECT AFTER_PROCESS_CODE, COMPANYCODE, DESCRIPTION
                                    FROM AFTER_PROCESS ").Tables[0];
                    if (dt3.Rows.Count > 0)
                    {
                        OutputMsg("2.9 清除 AFTER_PROCESS_TEMP(WEB)");
                        OracleDBUtil.ExecuteSql(objTX, "DELETE AFTER_PROCESS_TEMP");
                        StringBuilder sb = new StringBuilder();
                        OutputMsg("2.10 寫入 AFTER_PROCESS_TEMP");
                        foreach (DataRow dr in dt3.Rows)
                        {
                            sb.Length = 0;
                            sb.AppendLine(
                                @"Insert into AFTER_PROCESS_TEMP
                                  (AFTER_PROCESS_CODE, COMPANYCODE, DESCRIPTION)
                                   Values
                                  ([AFTER_PROCESS_CODE], [COMPANYCODE], [DESCRIPTION])");

                            sb.Replace("[AFTER_PROCESS_CODE]", OracleDBUtil.SqlStr(dr["AFTER_PROCESS_CODE"].ToString()));
                            sb.Replace("[COMPANYCODE]", OracleDBUtil.SqlStr(dr["COMPANYCODE"].ToString()));
                            sb.Replace("[DESCRIPTION]", OracleDBUtil.SqlStr(dr["DESCRIPTION"].ToString()));
                            OracleDBUtil.ExecuteSql(objTX, sb.ToString());
                        }

                    }
                    #endregion
                    OutputMsg("2.11 PK_CONVERT.SP_REASON_IMPORT");
                    OracleParameter op1 = new OracleParameter("outCODE", OracleType.VarChar, 100);
                    OracleParameter op2 = new OracleParameter("outMessage", OracleType.VarChar, 2000);
                    op1.Direction = ParameterDirection.Output;
                    op2.Direction = ParameterDirection.Output;
                    OracleDBUtil.ExecuteSql_SP(
                    objTX,
                    "PK_CONVERT.SP_REASON_IMPORT", op1, op2
                    );

                    objTX.Commit();
                    OutputMsg(op2.Value.ToString());
                    //OutputMsg("執行成功,寫入log");
                    ////成功資訊
                    //con_log.Success(sMSG);
                }
                else
                {
                    OutputMsg("2.2 REASON(ERP)來源無資料");
                }
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                OutputMsg(ex.Message);
                throw ex;
                ////失敗資訊
                //con_log.Fail(sMSG);
                //myPause();

            }
            finally
            {
                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                if (objConn_erp.State == ConnectionState.Open) objConn_erp.Close();
                objConn_erp.Dispose();
                OracleConnection.ClearAllPools();
            }
           
        }

        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }   

        #region 備份20110117
        //static void Main(string[] args)
        //{
        //    #region 測試資訊
        //    //Console.WriteLine("REASON_IMPORT 啟動=>");
        //    ////初始化LOG
        //    //string scon = "data source=POSTEST; user id=WEBPOS; pwd=webpos;Pooling=true;";
        //    //using (OracleConnection con = new OracleConnection(scon)) 
        //    //{
        //    //    string sql="select 1 from dual";
        //    //    using (OracleCommand cmd = new OracleCommand(sql,con)) {
        //    //        using (OracleDataAdapter da = new OracleDataAdapter(cmd)) 
        //    //        {
        //    //            using (DataTable dt = new DataTable()) 
        //    //            {
        //    //                da.Fill(dt);
        //    //                if (dt.Rows.Count > 0)
        //    //                    Console.WriteLine(dt.Rows[0][0].ToString());
        //    //            }
        //    //        }
                
        //    //    }

        //    //}
        //    #endregion
        //    ConvertLog con_log = new ConvertLog("REASON_IMPORT");
        //    Console.WriteLine("   1.初始化LOG");
        //    try
        //    {
        //        //** CLEAR WEBPOS.DEPT_TEMP
        //        //** FETDB01T.REASON -> WEBPOS.REASON_TEMP
        //        FETDB01T_REASON_WEBPOS_REASON_TEMP();
        //        Console.WriteLine("   2.FETDB01T.REASON -> WEBPOS.REASON_TEMP");
        //        //**003比對 WEBPOS.DEPT_TEMP WEBPOS.DEPT
        //        string sMsg=ReasonTemp2Reason();
        //        Console.WriteLine("   3.比對 WEBPOS.Reason_TEMP WEBPOS.Reason");
        //        //成功資訊
        //        con_log.Success(sMsg);
        //        Console.WriteLine("   4.<=執行完畢，寫入成功資訊");
        //        //Console.ReadKey(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        //失敗資訊
        //        Console.WriteLine(ex.Message);
        //        con_log.Fail(ex.Message);
        //        Console.ReadKey(true);
        //    }
        //}

        //static void FETDB01T_REASON_WEBPOS_REASON_TEMP()
        //{
        //    APP03_Facade cAPP = new APP03_Facade();
            
        //    APP03_Reason_Import_DTO dsTEMP = new APP03_Reason_Import_DTO();
        //    try
        //    {
        //        //Clear DEPT_TEMP
        //        DataTable dtTmp = cAPP.Query_One_Reason_TEMP();
        //        OracleDBUtil.DELETE(dtTmp, "");
        //        dtTmp.Dispose();

        //        ////**002FETDB01T.PRODUCT -> WEBPOS.PRODUCT_TEMP
        //        Dictionary<string, string> dic = new Dictionary<string, string>();
        //        dic.Add("REASONTYPE", "REASONTYPE");
        //        dic.Add("REASON", "REASON");
        //        dic.Add("COMPANYCODE", "COMPANYCODE");
        //        dic.Add("CEASEDATE", "CEASEDATE");
        //        dic.Add("ACCNO", "ACCNO");
        //        dic.Add("UPDDATE", "UPDDATE");
        //        dic.Add("UPDUSRNO", "UPDUSRNO");
        //        dic.Add("ULSNO", "ULSNO");
        //        dic.Add("REASONID", "REASONID");

        //        //DataTable dtOld = cAPP.Query_OLD_Dept();
        //        DataTable dtOld = cAPP.Query_OLD_REASON();


        //        //APP02_DeptImport_DTO.DEPT_TEMPDataTable dtTEMP = dsTEMP.DEPT_TEMP;
        //        APP03_Reason_Import_DTO.REASON_TEMPDataTable dtTEMP = dsTEMP.REASON_TEMP;
        //        foreach (DataColumn dc in dtTEMP.Columns)
        //            dc.AllowDBNull = true;

        //        for (int i = 0; i < dtOld.Rows.Count; i++)
        //        {
        //            //APP02_DeptImport_DTO.DEPT_TEMPRow drTEMP = dtTEMP.NewDEPT_TEMPRow();
        //            APP03_Reason_Import_DTO.REASON_TEMPRow drTEMP = dtTEMP.NewREASON_TEMPRow();
        //            DataRow drOld = dtOld.Rows[i];

        //            //common
        //            foreach (KeyValuePair<string, string> pair in dic)
        //            {
        //                drTEMP[pair.Value] = drOld[pair.Key];
        //            }

        //            dtTEMP.Rows.Add(drTEMP);
        //        }
        //        dtTEMP.AcceptChanges();

        //        cAPP.Add_REASON_TEMP(dsTEMP);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //static string ReasonTemp2Reason()
        //{
        //    //比對 WEBPOS.DEPT_TEMP WEBPOS.DEPT
        //    //存在:UPDATE 
        //    //不存在:INSERT
        //    APP03_Facade cAPP = new APP03_Facade();
        //    DataTable dtTempNew = cAPP.Query_REASON_TEMP_NEW();
        //    DataTable dtTempUpd = cAPP.Query_REASON_TEMP_Update();

        //    APP03_Reason_Import_DTO dsAdd = new APP03_Reason_Import_DTO();
        //    APP03_Reason_Import_DTO dsUpd = new APP03_Reason_Import_DTO();
        //    //DEPT_TEMP 新增
        //    APP03_Reason_Import_DTO.REASONDataTable dtAdd = dsAdd.REASON;
        //    foreach (DataColumn dc in dtAdd.Columns)
        //        dc.AllowDBNull = true;

        //    ////** FETDB01T.PRODUCT -> WEBPOS.PRODUCT_TEMP
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    dic.Add("REASONTYPE", "REASONTYPE");
        //    dic.Add("REASON", "REASON");
        //    dic.Add("COMPANYCODE", "COMPANYCODE");
        //    dic.Add("CEASEDATE", "CEASEDATE");
        //    dic.Add("ACCNO", "ACCNO");
        //    dic.Add("UPDDATE", "UPDDATE");
        //    dic.Add("UPDUSRNO", "UPDUSRNO");
        //    dic.Add("ULSNO", "ULSNO");
        //    dic.Add("REASONID", "REASONID");

        //    for (int i = 0; i < dtTempNew.Rows.Count; i++)
        //    {
        //        //dtAdd.LoadDataRow(dtTempNew.Rows[i].ItemArray, false);
        //        APP03_Reason_Import_DTO.REASONRow drAdd = dtAdd.NewREASONRow();
        //        DataRow drNew = dtTempNew.Rows[i];
        //        foreach (KeyValuePair<string, string> pair in dic)
        //        {
        //            drAdd[pair.Value] = drNew[pair.Key];
        //        }
               
        //        dtAdd.Rows.Add(drAdd);
        //    }
        //    dtAdd.AcceptChanges();


        //    //DEPT_TEMP修改
        //    APP03_Reason_Import_DTO.REASONDataTable dtUpd = dsUpd.REASON;
        //    foreach (DataColumn dc in dtUpd.Columns)
        //        dc.AllowDBNull = true;


        //    for (int i = 0; i < dtTempUpd.Rows.Count; i++)
        //    {
        //        //dtUpd.LoadDataRow(dtTempUpd.Rows[i].ItemArray, false);
        //        APP03_Reason_Import_DTO.REASONRow drUpd = dtUpd.NewREASONRow();
        //        DataRow drNew = dtTempUpd.Rows[i];
        //        foreach (KeyValuePair<string, string> pair in dic)
        //        {
        //            drUpd[pair.Value] = drNew[pair.Key];
        //        }
        //        dtUpd.Rows.Add(drUpd);
        //    }
        //    dtUpd.AcceptChanges();


        //    //寫入REASON
        //    cAPP.Update_Add_REASON(dsAdd, dsUpd);

        //    return "REASON，新增筆數:" + dtAdd.Rows.Count.ToString() + "更新筆數:" + dtUpd.Rows.Count.ToString();

        //}
        #endregion
    }
}
