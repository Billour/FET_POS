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
    public class ORD10_Facade
    {
        private DateTime SysDate = DateTime.Now;

        public DataTable ExportWeightDistribute(string Zone, string StoreNo)
        {
            string sqlStr = @"SELECT S.STORE_NO 門市編號,
                W.WEIGHT 比率 
                FROM STORE_WEIGHT_DISTRIBUTE W,STORE S 
                WHERE S.STORE_NO = W.STORE_NO(+) 
                AND TO_DATE(NVL(CLOSEDATE,'99991231'),'YYYYMMDD') >= TRUNC(SYSDATE)";

            if (!string.IsNullOrEmpty(Zone))
            {
                sqlStr += " AND S.Zone = " + OracleDBUtil.SqlStr(Zone);
            }
            if (!string.IsNullOrEmpty(StoreNo))
            {
                sqlStr += " AND S.Store_No = " + OracleDBUtil.SqlStr(StoreNo);
            }

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public void ImportWeightDistribute(DataTable dt, string MODI_USER)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ORD10_WeightDistribute_DTO.STORE_WEIGHT_DISTRIBUTEDataTable fdt = new ORD10_WeightDistribute_DTO.STORE_WEIGHT_DISTRIBUTEDataTable();
                ORD10_WeightDistribute_DTO.STORE_WEIGHT_DISTRIBUTERow fdr = fdt.NewSTORE_WEIGHT_DISTRIBUTERow();

                fdt.CREATE_DTMColumn.AllowDBNull = true;
                fdt.CREATE_USERColumn.AllowDBNull = true;
                fdr.MODI_DTM = SysDate;
                fdr.MODI_USER = MODI_USER;
                fdr.STORE_NO = dr["門市編號"].ToString();
                DataTable StoreDT = new Store_Facade().Query_StoreInfo(fdr.STORE_NO);//取得門市資料
                if (StoreDT.Rows.Count > 0)
                {
                    fdr.ZONE = StoreDT.Rows[0]["ZONE"].ToString();
                }
                fdr.WEIGHT = Convert.ToInt16(dr["比率"].ToString());
                if (CheckWeightExist(fdr.STORE_NO))//資料已存更新
                {
                    fdt.AddSTORE_WEIGHT_DISTRIBUTERow(fdr);
                    fdt.AcceptChanges();
                    OracleDBUtil.UPDDATEByUUID(fdt, "STORE_NO");
                }
                else //不存在新增
                {
                    fdr.CREATE_USER = MODI_USER;
                    fdr.CREATE_DTM = SysDate;
                    fdt.AddSTORE_WEIGHT_DISTRIBUTERow(fdr);
                    fdt.AcceptChanges();
                    OracleDBUtil.Insert(fdt);
                }
            }
        }

        public int SaveWeightDistribute(ORD10_WeightDistribute_DTO.STORE_WEIGHT_DISTRIBUTEDataTable STORE_WD)
        {
            int intResult = 0;
            OracleConnection objConn = null;
            OracleTransaction objTx = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTx = objConn.BeginTransaction();
                OracleDBUtil.ExecuteSql(objTx, @"delete STORE_WEIGHT_DISTRIBUTE");

                intResult += OracleDBUtil.Insert(objTx, STORE_WD);
                objTx.Commit();
            }

            catch (Exception ex)
            {
                objTx.Rollback();
                throw ex;
            }
            finally
            {
                objTx.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return intResult;
        }

        private bool CheckWeightExist(string Store_No)
        {
            bool r = false;
            string sqlStr = "SELECT * FROM STORE_WEIGHT_DISTRIBUTE WHERE STORE_NO = " + OracleDBUtil.SqlStr(Store_No);

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            if (dt.Rows.Count > 0)
            {
                r = true;
            }
            return r;
        }

        public DataTable GetWeightDistributeMethodData(string Zone, string StoreNo)
        {
            string sqlStr = @"SELECT ROWNUM ITEMS , S.ZONE,nvl((SELECT ZONE_NAME FROM ZONE Z where Z.ZONE=S.ZONE),'') ZONE_NAME,S.STORE_NO,S.STORENAME,W.WEIGHT || '%' WEIGHT FROM  STORE_WEIGHT_DISTRIBUTE W ,STORE S
                              WHERE S.STORE_NO= W.STORE_NO(+) AND TO_DATE(NVL(CLOSEDATE,'99991231'),'YYYYMMDD') >= TRUNC(SYSDATE) ";
            if (!string.IsNullOrEmpty(Zone))
            {
                sqlStr += " AND S.Zone = " + OracleDBUtil.SqlStr(Zone);
            }
            if (!string.IsNullOrEmpty(StoreNo))
            {
                sqlStr += " AND S.Store_No = " + OracleDBUtil.SqlStr(StoreNo);
            }
            sqlStr += " ORDER BY S.ZONE ASC,S.STORE_NO ASC";

            DataTable dt = OracleDBUtil.Query_Data(sqlStr);
            return dt;
        }

        public ORD10_WeightDistribute_DTO.UPLOAD_TEMPDataTable GetTemp(string BATCH_NO)
        {
            ORD10_WeightDistribute_DTO.UPLOAD_TEMPDataTable dt = new ORD10_WeightDistribute_DTO.UPLOAD_TEMPDataTable();
            OracleConnection objConn = null;

            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(@"SELECT M.F1 STORE_NO
                                    , S.STORENAME
                                    , M.F2 WEIGHT_NUMBER
                                    , M.F2 ||'%' WEIGHT
                                    , M.F3 RESULT
                                FROM UPLOAD_TEMP M, STORE S
                                WHERE M.F1 = S.STORE_NO(+) 
                                  AND BATCH_NO = " + OracleDBUtil.SqlStr(BATCH_NO.ToString()) + @" 
                                ORDER BY F1
                            ");       //依門市編號排序

                objConn = OracleDBUtil.GetConnection();
                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
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

            return dt;
        }

        public void ImportTemp(DataTable dt, string BATCH_NO)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                OracleDBUtil.ExecuteSql(objTX,
                    @"delete UPLOAD_TEMP
                    where nvl(STATUS,'T')<>'C' " +
                                              "and FINC_ID  = " + OracleDBUtil.SqlStr(dt.Rows[0]["FINC_ID"].ToString()) +
                                              "and USER_ID  = " + OracleDBUtil.SqlStr(dt.Rows[0]["USER_ID"].ToString()));

                OracleDBUtil.Insert(objTX, dt);

                OracleDBUtil.ExecuteSql_SP(
                     objTX
                     , "PK_Upload_Check.ORD10_CHECK"
                     , new OracleParameter("p_BATCH_NO", BATCH_NO)
                     );


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

}
