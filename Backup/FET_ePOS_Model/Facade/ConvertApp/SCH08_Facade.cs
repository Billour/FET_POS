using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.ConvertApp;
using FET.POS.Model.DTO.ConvertApp;
using FET.POS.Model.Common;

namespace FET.POS.Model.Facade.ConvertApp
{
    public class SCH08_Facade : BaseClass
    {
        public string ConvertProductType()
        {
            OracleConnection objConn = null;
            OracleConnection objConn2 = null;
            OracleTransaction objTX2 = null;
            string sRet = "";
            try
            {
                #region 取得所有的PRODTYPE
                SCH08_Product_Type_DTO.PRODTYPEDataTable dt = 
                    new SCH08_Product_Type_DTO.PRODTYPEDataTable();
            
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("SELECT * ");
                sb.AppendLine("FROM PRODTYPE ");

                objConn = OracleDBUtil.GetERPPOSConnection();                

                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));
                #endregion

                if (dt.Rows.Count > 0)
                {
                    #region 將所有的PRODTYPE寫入TEMP
                    SCH08_Product_Type_DTO.PRODTYPE_TEMPDataTable dt2 =
                        new SCH08_Product_Type_DTO.PRODTYPE_TEMPDataTable();
                    
                    foreach (SCH08_Product_Type_DTO.PRODTYPERow dr in dt.Rows)
                    {
                        SCH08_Product_Type_DTO.PRODTYPE_TEMPRow dr2 = dt2.NewPRODTYPE_TEMPRow();

                        dr2.PRODTYPENO = dr.PRODTYPENO;
                        dr2.PRODTYPENAME = dr.PRODTYPENAME;
                        dr2.COMPANYCODE = dr.COMPANYCODE;
                        dt2.AddPRODTYPE_TEMPRow(dr2);
                    }                    

                    OracleDBUtil.Insert(dt2);
                    #endregion

                    #region 由SP_SCH08做新增修改

                    objConn2 = OracleDBUtil.GetConnection();
                    objTX2 = objConn2.BeginTransaction();
                    OracleParameter op = new OracleParameter("outMessage", OracleType.VarChar, 2000);
                    op.Direction = ParameterDirection.Output;
                    OracleDBUtil.ExecuteSql_SP(
                        objTX2
                        , "SP_SCH08",op
                        );

                    objTX2.Commit();
                    sRet = op.Value.ToString();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                objTX2.Rollback();
                throw ex;
            }
            finally
            {
                objTX2.Dispose();
                if (objConn2.State == ConnectionState.Open) objConn2.Close();
                objConn2.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();

                OracleConnection.ClearAllPools();
            }
            return sRet;
        }

        public int so_update_log()
        {
            
            OracleConnection objConn = null;
            OracleConnection connNewPos = null;
            OracleTransaction OldPosTrans = null;
            int iRet = 0;
            try {

                objConn = OracleDBUtil.GetERPPOSConnection();

                //從舊的POS取出
                SCH08_Product_Type_DTO.SO_UPDATE_LOG_OLDDataTable dt =
                        new SCH08_Product_Type_DTO.SO_UPDATE_LOG_OLDDataTable();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("SELECT * ");
                sb.AppendLine("FROM SO_UPDATE_LOG WHERE DWNFLAG=0 ");

                dt.Load(OracleDBUtil.GetDataReader(objConn, sb.ToString()));

                if (dt.Rows.Count > 0)
                {
                    SCH08_Product_Type_DTO.SO_UPDATE_LOGDataTable dt2 =
                        new SCH08_Product_Type_DTO.SO_UPDATE_LOGDataTable();

                    foreach (SCH08_Product_Type_DTO.SO_UPDATE_LOG_OLDRow dr in dt.Rows)
                    {
                        SCH08_Product_Type_DTO.SO_UPDATE_LOGRow dr2 = dt2.NewSO_UPDATE_LOGRow();

                        dr2.CREATE_DTM = dr.CREATE_DTM;
                        dr2.CREATE_USER = dr.CREATE_USER;
                        dr2.DWNDATE = dr.DWNDATE;
                        dr2.DWNFLAG = "1";
                        dr2.MODI_DTM = dr.MODI_DTM;
                        dr2.MODI_USER = dr.MODI_USER;
                        dr2.SENDDATE = dr.SENDDATE;
                        dr2.SOID = dr.SOID;
                        dr2.SONO = dr.SONO;
                        dr2.SONOX = dr.SONOX;
                        dr2.STATUS = dr.STATUS;
                        dr2.TRANS_DATE = dr.TRANS_DATE;

                        dt2.AddSO_UPDATE_LOGRow(dr2);
                    }

                    OracleDBUtil.Insert(dt2);

                    OracleDBUtil.ExecuteSql(objConn,
                        @"UPDATE SO_UPDATE_LOG
                      SET DWNFLAG=1
                      WHERE DWNFLAG=0");
                }

                //從新的POS取出
                SCH08_Product_Type_DTO.SO_UPDATE_LOGDataTable dtNewPosLog =
                        new SCH08_Product_Type_DTO.SO_UPDATE_LOGDataTable();

                connNewPos = OracleDBUtil.GetConnection();

                dtNewPosLog.Load(OracleDBUtil.GetDataReader(
                    connNewPos,
                    "SELECT * FROM SO_UPDATE_LOG WHERE DWNFLAG=0"
                    ));

                if (dtNewPosLog.Rows.Count > 0)
                {
                    SCH08_Product_Type_DTO.SO_UPDATE_LOG_OLDDataTable dtOldPosLog =
                        new SCH08_Product_Type_DTO.SO_UPDATE_LOG_OLDDataTable();

                    foreach (SCH08_Product_Type_DTO.SO_UPDATE_LOGRow dr in dtNewPosLog.Rows)
                    {
                        SCH08_Product_Type_DTO.SO_UPDATE_LOG_OLDRow dr2 =
                            dtOldPosLog.NewSO_UPDATE_LOG_OLDRow();

                        dr2.CREATE_DTM = dr.CREATE_DTM;
                        dr2.CREATE_USER = dr.CREATE_USER;
                        dr2.DWNDATE = dr.DWNDATE;
                        dr2.DWNFLAG = "1";
                        dr2.MODI_DTM = dr.MODI_DTM;
                        dr2.MODI_USER = dr.MODI_USER;
                        dr2.SENDDATE = dr.SENDDATE;
                        dr2.SOID = dr.SOID;
                        dr2.SONO = dr.SONO;
                        dr2.SONOX = dr.SONOX;
                        dr2.STATUS = dr.STATUS;
                        dr2.TRANS_DATE = dr.TRANS_DATE;

                        dtOldPosLog.AddSO_UPDATE_LOG_OLDRow(dr2);
                    }

                    OldPosTrans = objConn.BeginTransaction();

                    try
                    {
                        dtOldPosLog.TableName = "SO_UPDATE_LOG";
                        OracleDBUtil.Insert(OldPosTrans, dtOldPosLog);
                        OldPosTrans.Commit();
                    }
                    catch
                    {
                        OldPosTrans.Rollback();
                    }

                    iRet=OracleDBUtil.ExecuteSql(connNewPos,
                    @"UPDATE SO_UPDATE_LOG
                      SET DWNFLAG=1
                      WHERE DWNFLAG=0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                OldPosTrans.Dispose();

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();

                if (connNewPos.State == ConnectionState.Open) connNewPos.Close();
                connNewPos.Dispose();
                OracleConnection.ClearAllPools();
            }
            return iRet;
            
        }
    }
}
