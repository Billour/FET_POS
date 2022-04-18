using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.OracleClient; 
using Advtek.Utility;
using System.Threading;

namespace STORE_IMPORT
{
    class Program
    {
        static string sMSG = "";       
        static void Main(string[] args)
        {
            //初始化LOG
            OutputMsg("1.STORE_IMPORT");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("STORE_IMPORT");

            OracleConnection wcon = null;
            OracleConnection pcon = null;
            try
            {
                //建立新舊線
                OutputMsg("3.建立新舊線");
                wcon = OracleDBUtil.GetConnection();
                pcon = OracleDBUtil.GetERPPOSConnection();
                //**001CLEAR WEBPOS.DEPT_TEMP
                //**002FETDB01T.DEPT -> WEBPOS.DEPT_TEMP
                OutputMsg("4.DEPT(POS) => DEPT_TEMP(WEB)");
                FETDB01T_DEPT_WEBPOS_DEPT_TEMP(wcon,pcon);

                //**003比對 WEBPOS.DEPT_TEMP WEBPOS.DEPT
                OutputMsg("5.DEPT_TEMP(WEB) => DEPT(WEB)");
                DeptTemp2Dept(wcon);

                //成功資訊
                OutputMsg("6.執行結束，寫入LOG");
                con_log.Success(sMSG);
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                //失敗資訊
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
                Thread.Sleep(3000);
            }
            finally 
            {
                if (wcon != null) {
                    if (wcon.State == ConnectionState.Open) wcon.Close();
                    wcon.Dispose();
                }
                if (pcon != null)
                {
                    if (pcon.State == ConnectionState.Open) pcon.Close();
                    pcon.Dispose();
                }
                OracleConnection.ClearAllPools();
            }

        }

        static void FETDB01T_DEPT_WEBPOS_DEPT_TEMP(OracleConnection wcon,OracleConnection pcon)
        {
            StringBuilder sb = new StringBuilder();
            OracleTransaction wtx = null;
            try
            {
                //Clear DEPT_TEMP
                sb.Length = 0;
                sb.Append("Delete from DEPT_TEMP");
                wtx = wcon.BeginTransaction();
                ExeSql(sb.ToString(), wtx);
                wtx.Commit();

                sb.Length = 0;
                
                //////**002FETDB01T.PRODUCT -> WEBPOS.PRODUCT_TEMP
                //Dictionary<string, string> dic = new Dictionary<string, string>();
                //dic.Add("DEPTNO", "DEPTNO");
                //dic.Add("DEPTNAME", "DEPT_NAME");
                //dic.Add("UPDUSRNO", "MODI_USER");
                //dic.Add("UPDDATE", "MODI_DTM");
                //dic.Add("ISACTIVE", "IS_DEL");
                wtx = wcon.BeginTransaction();
                DataTable dtOld = new DataTable();
                dtOld = SelectTable("select DEPTNO, COMPANYCODE, DEPTNAME,COSTCENTER, UPDDATE, UPDUSRNO,ULSNO, ISACTIVE from dept", pcon);
                int iCount = 0;
                int iCount2 = 0;
                sb.Length = 0;
                StringBuilder sb2 = new StringBuilder();
                string sTemplate=@"insert into DETP_TEMP (DEPT_NO, DEPT_NAME, IS_DEL, CREATE_DTM, 
 CREATE_USER, MODI_DTM, MODI_USER, 
 DEL_DTM, DEL_USER, STATUS) 
 values ([DEPT_NO], [DEPT_NAME], [IS_DEL], [CREATE_DTM], 
 [CREATE_USER], [MODI_DTM], [MODI_USER], 
 [DEL_DTM], [DEL_USER], [STATUS] ); ";
                foreach (DataRow dr in dtOld.Rows) 
                {
                    if (iCount2 > 200) 
                    {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.ToString(), wtx);
                        sb.Length = 0;
                        iCount2 = 0;
                    }
                    sb2.Length = 0;
                    sb2.Append(sTemplate);
                    sb2.Replace("[DEPT_NO]", OracleDBUtil.SqlStr(dr["DEPTNO"].ToString()));
                    sb2.Replace("[DEPT_NAME]", OracleDBUtil.SqlStr(dr["DEPTNAME"].ToString()));
                    sb2.Replace("[IS_DEL]", OracleDBUtil.SqlStr(dr["ISACTIVE"].ToString()));
                    sb2.Replace("[CREATE_DTM]", "SYSDATE");
                    sb2.Replace("[CREATE_USER]", "'CONVERT'");
                    sb2.Replace("[MODI_DTM]", "TO_DATE(" + OracleDBUtil.SqlStr(dr["DEPTNO"].ToString())  + ",'YYYYMMDD')");
                    sb2.Replace("[MODI_USER]", OracleDBUtil.SqlStr(dr["UPDUSRNO"].ToString()));
                    sb.Append(sb2.ToString());
                    iCount2++;
                    iCount++;
                }

                if (sb.Length > 0) {
                    sb.Insert(0, "BEGIN ");
                    sb.Append(" END;");
                    ExeSql(sb.ToString(), wtx);
                    sb.Length = 0;
                    iCount2 = 0;
                }
                wtx.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #region 舊程式備份 20110315
        //static void FETDB01T_DEPT_WEBPOS_DEPT_TEMP()
        //{
            
        //    APP02_Facade cAPP= new APP02_Facade();
           
        //    ////App cApp01 = new APP01_Facade();
        //    APP02_DeptImport_DTO dsTEMP = new APP02_DeptImport_DTO();
        //    //APP01_ProductImport dsTEMP = new APP01_ProductImport();
        //    try
        //    {
        //        //Clear DEPT_TEMP
        //        DataTable dtTmp = cAPP.Query_One_Dept_TEMP();
        //        OracleDBUtil.DELETE(dtTmp, "");
        //        dtTmp.Dispose();

        //        ////**002FETDB01T.PRODUCT -> WEBPOS.PRODUCT_TEMP
        //        Dictionary<string, string> dic = new Dictionary<string, string>();
        //        dic.Add("DEPTNO", "DEPTNO");
        //        dic.Add("DEPTNAME", "DEPT_NAME");
        //        dic.Add("UPDUSRNO", "MODI_USER");
        //        dic.Add("UPDDATE", "MODI_DTM");
        //        dic.Add("ISACTIVE", "IS_DEL");
        //        DataTable dtOld = cAPP.Query_OLD_Dept();


        //        //APP01_ProductImport.PRODUCT_TEMPDataTable dtTEMP = dsTEMP.PRODUCT_TEMP;
        //        APP02_DeptImport_DTO.DEPT_TEMPDataTable dtTEMP = dsTEMP.DEPT_TEMP;
        //        foreach (DataColumn dc in dtTEMP.Columns)
        //            dc.AllowDBNull = true;

        //        for (int i = 0; i < dtOld.Rows.Count; i++)
        //        {
        //            //APP01_ProductImport.PRODUCT_TEMPRow drTEMP = dtTEMP.NewPRODUCT_TEMPRow();
        //            APP02_DeptImport_DTO.DEPT_TEMPRow drTEMP = dtTEMP.NewDEPT_TEMPRow();
        //            //DataRow drProduct = dtOldProduct.Rows[i];
        //            DataRow drOld = dtOld.Rows[i];
        //            //common
        //            foreach (KeyValuePair<string, string> pair in dic)
        //            {
        //                if (pair.Key == "DEPTNO")
        //                {
        //                    drTEMP[pair.Value] = OracleDBUtil.SqlStr(drOld[pair.Key].ToString()).Replace("'", "");
        //                }
        //                else if (pair.Key == "ISACTIVE")
        //                {
        //                    if (drOld[pair.Key].ToString() == "1") 
        //                        drTEMP[pair.Value] = "Y";
        //                    else
        //                        drTEMP[pair.Value] = "N";
        //                }
        //                else
        //                {
        //                    drTEMP[pair.Value] = drOld[pair.Key];
        //                }

        //            }
        //            //special
        //            string sDate = drOld["UPDDATE"].ToString();
        //            drTEMP["MODI_DTM"] = Convert.ToDateTime(sDate.Substring(0, 4) + "/" + sDate.Substring(4, 2) + "/" + sDate.Substring(6, 2));
        //            drTEMP["CREATE_USER"] = "SYSTEM";
        //            drTEMP["CREATE_DTM"] = DateTime.Now;
        //            dtTEMP.Rows.Add(drTEMP);
        //        }
        //        dtTEMP.AcceptChanges();

        //        #region remark
        //        ////WEBPOS.DISCOUNT_MASTER -> WEBPOS.PRODUCT_TEMP
        //        //DataTable dtDiscount = cApp01.Query_Discount_Master_Today_Yesterday();
        //        //for (int i = 0; i < dtDiscount.Rows.Count; i++)
        //        //{
        //        //    APP01_ProductImport.PRODUCT_TEMPRow drTEMP = dtTEMP.NewPRODUCT_TEMPRow();
        //        //    drTEMP["PRODNO"] = dtDiscount.Rows[i]["DISCOUNT_CODE"];
        //        //    drTEMP["PRODNAME"] = dtDiscount.Rows[i]["DISCOUNT_NAME"];
        //        //    drTEMP["CREATE_USER"] = dtDiscount.Rows[i]["CREATE_USER"];
        //        //    drTEMP["CREATE_DTM"] = dtDiscount.Rows[i]["CREATE_DTM"];
        //        //    drTEMP["MODI_USER"] = dtDiscount.Rows[i]["MODI_USER"];
        //        //    drTEMP["MODI_DTM"] = dtDiscount.Rows[i]["MODI_DTM"];
        //        //    drTEMP["S_DATE"] = dtDiscount.Rows[i]["S_DATE"];
        //        //    drTEMP["E_DATE"] = dtDiscount.Rows[i]["E_DATE"];
        //        //    if (dtDiscount.Rows[i]["E_DATE"] == DBNull.Value)
        //        //        drTEMP["CEASEDATE"] = "99991231";
        //        //    else
        //        //        drTEMP["CEASEDATE"] = ((DateTime)dtDiscount.Rows[i]["E_DATE"]).ToString("yyyyMMdd");

        //        //    drTEMP["ACCOUNTCODE"] = dtDiscount.Rows[i]["ACCOUNT_CODE"];
        //        //    drTEMP["DEL_FLAG"] = dtDiscount.Rows[i]["DEL_FLAG"];
        //        //    drTEMP["STATUS"] = "0";
        //        //    drTEMP["IS_DISCOUNT"] = "Y";
        //        //    dtTEMP.Rows.Add(drTEMP);
        //        //}
        //        //dtTEMP.AcceptChanges();

        //        #endregion
                
        //        ////Insert WEBPOS.PRODUCT_TEMP
        //        cAPP.Add_DEPT_TEMP(dsTEMP);

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #endregion

        static void DeptTemp2Dept(OracleConnection wcon)
        {
            OracleTransaction wotx=null;
            try {
                wotx = wcon.BeginTransaction();
                OutputMsg("7.由SP_STORE_IMPORT做新增修改");
                OracleParameter op = new OracleParameter("outMESSAGE", OracleType.VarChar);
                op.Size = 2000;
                op.Direction = ParameterDirection.Output;
                OracleDBUtil.ExecuteSql_SP(
                    wotx
                    , "PK_CONVERT.SP_STORE_IMPORT"
                    , op
                    );

                wotx.Commit();
                OutputMsg(op.Value.ToString());
            }
            catch (Exception ex) {
                if (wotx != null) {
                    wotx.Rollback();
                    wotx.Dispose();
                } 
                throw ex;
            }


        }
        #region 程式備份 20110315
        //static void DeptTemp2Dept()
        //{
        //    //比對 WEBPOS.DEPT_TEMP WEBPOS.DEPT
        //    //存在:UPDATE 
        //    //不存在:INSERT
        //    APP02_Facade cAPP = new APP02_Facade();
        //    DataTable dtTempNew = cAPP.Query_DEPT_TEMP_NEW();
        //    DataTable dtTempUpd = cAPP.Query_Dept_TEMP_Update();

        //    APP02_DeptImport_DTO dsAdd = new APP02_DeptImport_DTO();
        //    APP02_DeptImport_DTO dsUpd = new APP02_DeptImport_DTO();
        //    //DEPT_TEMP 新增
        //    APP02_DeptImport_DTO.DEPTDataTable dtAdd = dsAdd.DEPT;
        //    foreach (DataColumn dc in dtAdd.Columns)
        //        dc.AllowDBNull = true;
        //    for (int i = 0; i < dtTempNew.Rows.Count; i++)
        //    {
        //        dtAdd.LoadDataRow(dtTempNew.Rows[i].ItemArray, false);
        //    }
        //    dtAdd.AcceptChanges();


        //    //DEPT_TEMP修改
        //    APP02_DeptImport_DTO.DEPTDataTable dtUpd = dsUpd.DEPT;
        //    foreach (DataColumn dc in dtUpd.Columns)
        //        dc.AllowDBNull = true;
        //    for (int i = 0; i < dtTempUpd.Rows.Count; i++)
        //    {
        //        dtUpd.LoadDataRow(dtTempUpd.Rows[i].ItemArray, false);
        //    }
        //    dtUpd.AcceptChanges();


        //    //寫入DEPT
        //    cAPP.Update_Add_DEPT(dsAdd, dsUpd);

        //}
        #endregion

        static DataTable SelectTable(string sql, OracleConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = OracleDBUtil.GetDataSet(con, sql).Tables[0];
            }
            catch (Exception ex) { throw ex; }
            return dt;
        }

        static int ExeSql(string sql, OracleConnection con)
        {
            OracleTransaction otx = null;
            int i = 0;
            try
            {
                otx = con.BeginTransaction();
                i = OracleDBUtil.ExecuteSql(otx, sql);
                otx.Commit();
            }
            catch (Exception ex)
            {
                otx.Rollback();
                throw ex;
            }
            finally
            {
                if (otx != null) otx.Dispose();
            }
            return i;
        }

        static int ExeSql(string sql, OracleTransaction otx)
        {
            int i = 0;
            try
            {
                i = OracleDBUtil.ExecuteSql(otx, sql);
            }
            catch (Exception ex)
            {
                otx.Rollback();
                throw ex;
            }
            return i;
        }


        static void OutputMsg(string s)
        {
            Console.WriteLine(s);
            sMSG += s + "\r\n";
        }   
    }

}
