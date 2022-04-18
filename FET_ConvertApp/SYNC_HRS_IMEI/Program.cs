using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using System.Threading;

namespace SYNC_HRS_IMEI
{
    class Program
    {
        static OracleConnection pcon=null;
        static OracleConnection wcon=null;
        static string sMSG = "";
        static void Main(string[] args)
        {
            // DEF_LOADDATA
            // IMEI_NOKIA_HCT
            // EPOS_IMEI_CHANGED

            //初始化LOG
            OutputMsg("1.SYNC_HRS_IMEI開始");
            OutputMsg("2.初始化LOG");
            ConvertLog con_log = new ConvertLog("SYNC_HRS_IMEI");

            try {
                OutputMsg("3.建立新舊連線");
                pcon = getOldConnection();
                wcon=OracleDBUtil.GetConnection();

                // DEF_LOADDATA
                OutputMsg("4.DEF_LOADDATA(POS) 寫入 DEF_LOADDATA_TEMP(WEB)");
                insertDEF_LOADDATA();

                // IMEI_NOKIA_HCT
                OutputMsg("5.IMEI_NOKIA_HCT(POS) 寫入 IMEI_NOKIA_HCT_TEMP(WEB)");
                insertIMEI_NOKIA_HCT();

                //2011/3/28待調整
                //EPOS_IMEI_CHANGED
                //OutputMsg("6.EPOS_IMEI_CHANGED(POS) 寫入 EPOS_IMEI_CHANGED_TEMP(WEB)");
                //insertEPOS_IMEI_CHANGED();

                OracleTransaction wotx = wcon.BeginTransaction();
                OutputMsg("7.由SP_IMEI_TEMP_IMEI做新增修改");
                OracleParameter op = new OracleParameter("outMessage", OracleType.VarChar);
                op.Size = 2000;
                op.Direction = ParameterDirection.Output;
                OracleDBUtil.ExecuteSql_SP(
                    wotx
                    , "SP_IMEI_TEMP_IMEI"
                    , op
                    );

                wotx.Commit();
                OutputMsg(op.Value.ToString());

                OutputMsg("7.執行結束，寫入LOG");
                con_log.Success(sMSG);
                //DataTable dt = OracleDBUtil.GetDataSet(pcon, "select * from DEF_LOADDATA").Tables[0];
                //string s = "";
            }
            catch (Exception ex) {
                OutputMsg(ex.Message);
                con_log.Fail(sMSG);
            }
            finally {
                if (pcon != null) {
                    if (pcon.State == ConnectionState.Open) pcon.Close();
                    pcon.Dispose();
                }
                if (wcon != null)
                {
                    if (wcon.State == ConnectionState.Open) wcon.Close();
                    wcon.Dispose();
                }
                OracleConnection.ClearAllPools();
            }
        }

        static OracleConnection getOldConnection()
        {
           return OracleDBUtil.GetConnectionByParaKey("HRS_POS_USER", "HRS_POS_PW","HRS_POS_HOST", "HRS_POS_DBSID", "HRS_POS_DBPORT");
        }

        static void insertIMEI_NOKIA_HCT() {
            string sSeqNo = getMaxSeqNo("IMEI_NOKIA_HCT_TEMP");
            OutputMsg("SeqNo:" + sSeqNo);
            string sql = string.Format(@"SELECT * 
                                    FROM IMEI_NOKIA_HCT 
                                    WHERE (
                                    (LENGTH(STORECODE)=4) OR 
                                    (LENGTH(STORECODE)=5 AND UPPER(SUBSTR(STORECODE,1,1))='R'))
                                    AND IMEISEQ>{0} 
                                    ORDER BY IMEISEQ ", sSeqNo);
            //IMEI_NOKIA_HCT
            insertTEMP_TABLE("IMEI_NOKIA_HCT"
                , @"Insert into IMEI_NOKIA_HCT_TEMP ( 
                     IMEISEQ, IMEI, ITEMCODE, 
                     ITEMDESC, IN_DATE, STORECODE, 
                     STORENAME, RGSNAME, RGSDATE, 
                     UPDTNAME, UPDTDATE, UPDSEQNO, 
                     STATUS, STATUS_DATE, FETITEMNO )
                    Values
                    ([IMEISEQ], [IMEI], [ITEMCODE], 
                     [ITEMDESC], [IN_DATE], [STORECODE], 
                     [STORENAME], [RGSNAME], [RGSDATE], 
                     [UPDTNAME], [UPDTDATE], [UPDSEQNO], 
                     [STATUS], [STATUS_DATE], [FETITEMNO]); ", sql);
        }

        static void insertDEF_LOADDATA() {
            OutputMsg("4.1 DEF_LOADDATA_TEMP取得目前最大SeqNo");
            string sSeqNo = getMaxSeqNo("DEF_LOADDATA_TEMP");
            OutputMsg("SeqNo:" + sSeqNo);
            //2011/3/28:兩種編號都有
            string sql = string.Format(@"SELECT * FROM DEF_LOADDATA 
                                        WHERE 
                                        (LENGTH(OUT_STORE)=4 OR 
                                         (LENGTH(OUT_STORE)=5 AND UPPER(SUBSTR(OUT_STORE,1,1))='R')) 
                                        AND DATASEQ> {0}
                                        ORDER BY DATASEQ ", sSeqNo); 
            OutputMsg("4.2 寫入DEF_LOADDATA_TEMP");
            insertTEMP_TABLE("DEF_LOADDATA"
                , @"Insert into DEF_LOADDATA_TEMP ( 
                     DATASEQ, IMEI, PARTNO, 
                     PARTNAME, IN_DATE, IN_VENDER, 
                     OUT_DATE, OUT_STORE, STORENAME, 
                     COMP, FILETYPE, RGSNAME, 
                     RGSDATE, UPDTNAME, UPDTDATE, 
                     UPDSEQNO, IMODE3G, STATUS, 
                     STATUS_DATE, FETITEMNO )
                    Values
                    ([DATASEQ], [IMEI], [PARTNO], 
                     [PARTNAME], [IN_DATE], [IN_VENDER], 
                     [OUT_DATE], [OUT_STORE], [STORENAME], 
                     [COMP], [FILETYPE], [RGSNAME], 
                     [RGSDATE], [UPDTNAME], [UPDTDATE], 
                     [UPDSEQNO], [IMODE3G], [STATUS], 
                     [STATUS_DATE], [FETITEMNO]); ", sql);
        }

        static void insertEPOS_IMEI_CHANGED()
        {
            string sSeqNo = getMaxSeqNo("EPOS_IMEI_CHANGED_TEMP");
            OutputMsg("SeqNo:" + sSeqNo);
            string sql = string.Format("select * from EPOS_IMEI_CHANGED where ID>{0}", sSeqNo);
            insertTEMP_TABLE("EPOS_IMEI_CHANGED"
                    , @"Insert into EPOS_IMEI_CHANGED_TEMP ( 
                         ID, ORGIMEI, CHANGEDIMEI, 
                         SOURCE, STATUS, RGSDATE, 
                         UPDDATE )
                        Values
                        ([ID], [ORGIMEI], [CHANGEDIMEI], 
                         [SOURCE], [STATUS], [RGSDATE], 
                         [UPDDATE]); ", sql);
        }

        static void insertTEMP_TABLE(string TableName,string SqlTemplate,string sql)
        {
            OracleTransaction wotx = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                OutputMsg("****************************");
                OutputMsg(string.Format("{0}(POS)，查詢資料", TableName));
                DataTable dtPOS = SelectTable(sql, pcon);

                int iTEN = 0;
                int iCount = 0;
                if (dtPOS.Rows.Count > 0)
                {
                    OutputMsg(string.Format("{0}_TEMP(WEB)，TEMP寫入資料", TableName));
                    wotx = wcon.BeginTransaction();
                    StringBuilder sb2 = new StringBuilder();//local StringBuilder
                    sb.Length = 0;
                    foreach (DataRow dr in dtPOS.Rows)
                    {
                        if (iTEN > 200)
                        {
                            sb.Insert(0, "BEGIN ");
                            sb.Append(" END;");
                            ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wotx);
                            
                            sb.Length = 0;
                            iTEN = 0;

                        }

                        sb2.Length = 0;
                        sb2.Append(SqlTemplate);
                        foreach (DataColumn dc in dtPOS.Columns)
                        {
                            string dcname = dc.ColumnName;
                            switch (dc.DataType.ToString().ToUpper())
                            {
                                case "SYSTEM.STRING":
                                    if (!dcname.ToUpper().Equals("STATUS"))
                                    {
                                        sb2.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                    }
                                    else {
                                        sb2.Replace("[" + dcname + "]", "'T'");
                                    }
                                    
                                    break;
                                case "SYSTEM.DECIMAL":
                                    sb2.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? dr[dcname].ToString() : "0");
                                    break;
                                case "SYSTEM.DATETIME":
                                    sb2.Replace("[" + dcname + "]", (dr[dcname] != DBNull.Value) ? OracleDBUtil.DateFormate(dr[dcname]) : "null");
                                    break;
                                default:
                                    sb2.Replace("[" + dcname + "]", OracleDBUtil.SqlStr(dr[dcname].ToString()));
                                    break;
                            }
                        }
                        
                        //sb.AppendLine(sb2.ToString());
                        sb.Append(sb2.ToString());
                        iTEN++;
                        iCount++;
                    }
                    if (sb.Length > 0)
                    {
                        sb.Insert(0, "BEGIN ");
                        sb.Append(" END;");
                        ExeSql(sb.Replace("\r", " ").Replace("\n", " ").ToString(), wotx);
                        
                        sb.Length = 0;
                        iTEN = 0;
                    }
                    wotx.Commit();
                    OutputMsg(string.Format("{0}(WEB)，新增筆數:{1}", TableName,iCount.ToString()));
                }
                else
                {
                    //NO DATA
                    OutputMsg(string.Format("{0}(WEB)，更新筆數:0", TableName));
                }
                OutputMsg("****************************");
            }
            catch (Exception ex)
            {
                //if (wotx != null) wotx.Rollback();
                OutputMsg(string.Format("{0}(WEB)，寫入TEMP資料時，產生例外。", TableName));
                OutputMsg(sb.ToString());
                throw ex;
            }
            finally
            {
                if (wotx != null) wotx.Dispose();
            }
        }

        static string getMaxSeqNo(string TableName) 
        {
            string sRet = "";
            try { 
                string sql="select nvl(max({0}),0) from {1}";
                switch (TableName) 
                {
                    case "IMEI_NOKIA_HCT_TEMP":
                        sql = string.Format(sql, "IMEISEQ", TableName);
                        break;
                    case "DEF_LOADDATA_TEMP":
                        sql = string.Format(sql, "DATASEQ", TableName);
                        break;
                    case "EPOS_IMEI_CHANGED_TEMP":
                        sql = string.Format(sql, "ID", TableName);
                        break;
                }
                DataTable dt = SelectTable(sql, wcon);
                if (dt.Rows.Count > 0)
                    sRet = dt.Rows[0][0].ToString();
                else
                    throw new Exception(string.Format("{0}，查無資料", sql));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sRet;
        }

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

        static int ExeSql(string sql,OracleConnection con)
        {
            OracleTransaction otx = null;
            int i = 0;
            try
            {
                otx = con.BeginTransaction();
                i=OracleDBUtil.ExecuteSql(otx, sql);
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
