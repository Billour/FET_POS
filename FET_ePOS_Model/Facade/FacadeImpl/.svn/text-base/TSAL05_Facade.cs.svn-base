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
using System.Collections.Specialized;

namespace FET.POS.Model.Facade.FacadeImpl
{
	public class TSAL05_Facade : BaseClass
	{
		#region Public Static Method : void updateCancelTrancation(List<object> POSUUID_DETAILs, object MODI_USER)
		/// <summary>
		/// 取消交易(Clone from SAL05_Facade)
		/// </summary>
		/// <param name="NewValues"></param>
		/// <param name="MODI_USER"></param>
		public static void updateCancelTrancation(List<object> POSUUID_DETAILs, object MODI_USER)
		{
			OracleConnection objConn = null;
			OracleTransaction objTX = null;
			try
			{
				//表頭資料
				objConn = OracleDBUtil.GetConnection();
				objTX = objConn.BeginTransaction();
				SAL05_TO_CLOSE_DTO.TO_CLOSE_HEADDataTable dtm = new SAL05_TO_CLOSE_DTO.TO_CLOSE_HEADDataTable();
				foreach (DataColumn dc in dtm.Columns) dc.AllowDBNull = true;
				foreach (object POSUUID_DETAIL in POSUUID_DETAILs)
				{
					SAL05_TO_CLOSE_DTO.TO_CLOSE_HEADRow dtrm = dtm.NewTO_CLOSE_HEADRow();
					dtrm.POSUUID_DETAIL = POSUUID_DETAIL.ToString();
					dtrm.STATUS = "3";//取消
					dtrm.MODI_USER = MODI_USER.ToString();
					dtrm.MODI_DTM = DateTime.Now;
					dtm.AddTO_CLOSE_HEADRow(dtrm);
					dtm.AcceptChanges();
				}

				OracleDBUtil.UPDDATEByUUID(dtm, "POSUUID_DETAIL");
				objTX.Commit();
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
		#endregion

		#region Public Static Method : DataTable getTO_CLOSE_HEAD(OrderedDictionary args)
		/// <summary>
		/// 取得表頭(Clone from SAL05_Facade)
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static DataTable getTO_CLOSE_HEAD(OrderedDictionary args)
		{
			OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
			DataTable dt = new DataTable();

			string where = "";
			string sqlStr = @"SELECT ROWNUM ITEMS, DECODE(h.STATUS,1,'未結',
                                                    2,'已結',
                                                    3,'取消',
                                                    h.STATUS) STATUS_NAME , ";
			sqlStr += "DECODE(h.SERVICE_TYPE,1,'新啟用',2,'續約',3,'代收',4,'變更促代',5,'線上儲值',6,'維修',10,'網購',h.SERVICE_TYPE) SERVICE_TYPENAME ,h.* ";
			sqlStr += string.Format("FROM {0} h WHERE 1 = 1  ", (Convert.ToInt32(args["STATUS"]) == 1) ? "TO_CLOSE_HEAD" : "TO_CLOSE_HEAD_LOG");

			//sqlStr += "DECODE(h.SERVICE_TYPE,1,'新啟用',2,'續約',3,'2轉3',4,'換補卡',5,'代收',6,'維修',7,'網購',8,'預購',9,'MNP(IA)',10,'特殊授權(IA)',11,'變更促代-換貨(SSI)',12,'變更促代-不換貨(SSI)',h.SERVICE_TYPE) SERVICE_TYPENAME ,h.* FROM TO_CLOSE_HEAD h WHERE 1 = 1  ";
			if (args["STORE_NO"].ToString() != "") //門市代碼
				sqlStr += " AND h.STORE_NO = " + OracleDBUtil.SqlStr(args["STORE_NO"].ToString());
			if (args["STATUS"].ToString() != "") //狀態
				sqlStr += " AND h.STATUS = " + OracleDBUtil.SqlStr(args["STATUS"].ToString());
			if (args["S_DATE"].ToString() != "")  //申請日期 起
				sqlStr += " AND trunc(h.APPLY_DATE) >= " + OracleDBUtil.DateStr(args["S_DATE"].ToString());
			if (args["E_DATE"].ToString() != "")  //申請日期 止
				sqlStr += " AND trunc(h.APPLY_DATE) <= " + OracleDBUtil.DateStr(args["E_DATE"].ToString());
			if (args["SERVICE_TYPE"].ToString() != "")  //服務類別
				sqlStr += " AND h.SERVICE_TYPE = " + OracleDBUtil.SqlStr(args["SERVICE_TYPE"].ToString());
			if (args["MSISDN"].ToString() != "")  //客戶門號
				where += getTO_CLOSE_ITEMByPOSUUID_DETAIL(args["MSISDN"].ToString(), Convert.ToInt32(args["STATUS"]));
			if (args["SALE_PERSON"] != null && args["SALE_PERSON"].ToString() != "")  //銷售人員
				sqlStr += " AND h.SALE_PERSON = " + OracleDBUtil.SqlStr(args["SALE_PERSON"].ToString());
			if (where != "")
			{
				sqlStr += " AND h.POSUUID_DETAIL in(" + where + ")";
			}

			try
			{
				advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
				dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
			}
			catch //(Exception ex)
			{
			}
			finally
			{
				if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
				advtekUtilityOracleDBUtilGetConnection.Dispose();
				OracleConnection.ClearAllPools();
			}

			return dt;
		}
		#endregion

		#region Public Static Method : DataTable getTO_CLOSE_HEADByUUID(string Detail_UUID, string Store_No)
		/// <summary>
		/// 取得表頭by Detail_UUID
		/// </summary>
		/// <param name="Detail_UUID"></param>
		/// <param name="Store_No"></param>
		/// <param name="status">1: 未結, 2: 已結</param>
		/// <returns></returns>
		public static DataTable getTO_CLOSE_HEADByUUID(string Detail_UUID, string Store_No, int status)
		{
			OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
			DataTable dt = new DataTable();

			string sqlStr = @"SELECT ROWNUM ITEMS, DECODE(h.STATUS,1,'未結',
                                                    2,'已結',
                                                    3,'取消',
                                                    h.STATUS) STATUS_NAME ,DECODE(h.SERVICE_TYPE,1,'新啟用',2,'續約',3,'2轉3',4,'換補卡',5,'代收',6,'維修',7,'網購',8,'預購',9,'MNP(IA)',10,'特殊授權(IA)',11,'變更促代-換貨(SSI)',12,'變更促代-不換貨(SSI)',h.SERVICE_TYPE) SERVICE_TYPENAME,h.* ";
			sqlStr += string.Format("FROM {0} h WHERE 1 = 1  ", (status == 1) ? "TO_CLOSE_HEAD" : "TO_CLOSE_HEAD_LOG");
			if (!string.IsNullOrEmpty(Detail_UUID))
			{
				sqlStr += " AND h.POSUUID_DETAIL = " + OracleDBUtil.SqlStr(Detail_UUID);
			}

			if (!string.IsNullOrEmpty(Store_No))
			{
				sqlStr += " AND h.STORE_NO = " + OracleDBUtil.SqlStr(Store_No);
			}

			try
			{
				advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
				dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
			}
			catch //(Exception ex)
			{
			}
			finally
			{
				if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
				advtekUtilityOracleDBUtilGetConnection.Dispose();
				OracleConnection.ClearAllPools();
			}
			return dt;
		}
		#endregion

		#region Public Static Method : DataTable getCancleTO_CLOSE_DATA(List<object> POSUUID_DETAILs, int status)
		/// <summary>
		/// 取得未結清單資料 for 取消交易用(Clone from SAL05_Facade)
		/// </summary>
		/// <param name="POSUUID_DETAILs">未結清單表頭檔鍵值</param>
		/// <param name="status">1: 未結, 2: 已結</param>
		/// <returns></returns>
		public static DataTable getCancleTO_CLOSE_DATA(List<object> POSUUID_DETAILs, int status)
		{
			OracleConnection objConn = null;
			StringBuilder where = new StringBuilder("");
			foreach (object POSUUID_DETAIL in POSUUID_DETAILs)
			{
				if (where.ToString().IndexOf(POSUUID_DETAIL.ToString()) < 0)
					where.Append("'").Append(POSUUID_DETAIL.ToString()).Append("',");
			}
			string sqlStr = @"Select h.POSUUID_DETAIL, h.SERVICE_TYPE, h.SERVICE_SYS_ID, h.BUNDLE_ID, h.TOTAL_AMOUNT, h.STORE_NO, h.SALE_PERSON 
                                    , i.BARCODE1, i.BARCODE2, i.BARCODE3, i.AMOUNT ";
			sqlStr += string.Format("FROM {0} h, ", (status == 1) ? "TO_CLOSE_HEAD" : "TO_CLOSE_HEAD_LOG");
            sqlStr += string.Format("(Select POSUUID_DETAIL, BARCODE1, BARCODE2, BARCODE3, AMOUNT From {0} ", (status == 1) ? "TO_CLOSE_ITEM" : "TO_CLOSE_ITEM_LOG");
			sqlStr += string.Format("Where BARCODE1 is not null or BARCODE2 is not null or BARCODE3 is not null And POSUUID_DETAIL IN ({0})) i ", where.ToString().TrimEnd(','));
			sqlStr += string.Format("Where h.POSUUID_DETAIL = i.POSUUID_DETAIL(+) And h.POSUUID_DETAIL in ({0}) ", where.ToString().TrimEnd(','));
			DataTable dt = null;
			try
			{
				objConn = OracleDBUtil.GetConnection();
				dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
			}
			catch
			{

			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
			return dt;
		}
		#endregion

		#region Public Static Method : DataTable getEmployee(string STORENO, string loginEmpNo)
		/// <summary>
		/// 取得門市員工資料
		/// </summary>
		/// <param name="STORENO"></param>
		/// <param name="loginEmpNo"></param>
		/// <returns></returns>
		public static DataTable getEmployee(string STORENO, string loginEmpNo)
		{
			OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
			DataTable dt = new DataTable();

			string sqlStr = @"Select Distinct Sale_Person as EMPNO From Sale_Head 
                                Where STORE_NO = " + OracleDBUtil.SqlStr(STORENO)
								+ " And Sale_Person = " + OracleDBUtil.SqlStr(loginEmpNo);

			try
			{
				advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
				dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
				if (dt != null && dt.Rows.Count > 0)
				{
					sqlStr = @"SELECT * FROM EMPLOYEE WHERE EMPNO in 
                                (Select Distinct Sale_Person as EMPNO From Sale_Head 
                                Where STORE_NO = " + OracleDBUtil.SqlStr(STORENO) + ")";
					dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
				}
				else
				{
					sqlStr = @"SELECT * FROM EMPLOYEE WHERE EMPNO = " + OracleDBUtil.SqlStr(loginEmpNo);
					dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
					if (dt != null && dt.Rows.Count > 0)
					{
						sqlStr = @"SELECT * FROM EMPLOYEE WHERE EMPNO in 
                                (Select Distinct Sale_Person as EMPNO From Sale_Head 
                                Where STORE_NO = " + OracleDBUtil.SqlStr(STORENO) + ") Union "
								+ " SELECT * FROM EMPLOYEE WHERE EMPNO = " + OracleDBUtil.SqlStr(loginEmpNo);
						dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
					}
					else
					{
						sqlStr = @"SELECT EMPNO, EMPNAME FROM EMPLOYEE WHERE EMPNO in 
                                (Select Distinct Sale_Person as EMPNO From Sale_Head 
                                Where STORE_NO = " + OracleDBUtil.SqlStr(STORENO) + ") Union "
								+ " SELECT Distinct " + OracleDBUtil.SqlStr(loginEmpNo) + " as EMPNO, " + OracleDBUtil.SqlStr(loginEmpNo)
								+ " as EMPNAME FROM EMPLOYEE WHERE STORENO = " + OracleDBUtil.SqlStr(STORENO);
						dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
					}
				}
			}
			catch //(Exception ex)
			{

			}
			finally
			{
				if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
				advtekUtilityOracleDBUtilGetConnection.Dispose();
				OracleConnection.ClearAllPools();
			}
			return dt;
		}
		#endregion

		#region Public Static Method : string getTO_CLOSE_ITEMByPOSUUID_DETAIL(string MSISDN, int status)
		/// <summary>
		/// 取得表身UUID(Clone from SAL05_Facade)
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string getTO_CLOSE_ITEMByPOSUUID_DETAIL(string MSISDN, int status)
		{
			OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
			DataTable dtd = new DataTable();

			string sqlStr = string.Format("SELECT POSUUID_DETAIL FROM {0} WHERE MSISDN = '{1}'", (status == 1) ? "TO_CLOSE_ITEM" : "TO_CLOSE_ITEM_LOG", MSISDN);
			try
			{
				advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
				dtd = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
			}
			catch //(Exception ex)
			{
			}
			finally
			{
				if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
				advtekUtilityOracleDBUtilGetConnection.Dispose();
				OracleConnection.ClearAllPools();
			}

			string strUUIDs = "";
			foreach (DataRow dtr in dtd.Rows)
			{
				strUUIDs += "'" + dtr["POSUUID_DETAIL"].ToString() + "',";
			}
			if (strUUIDs.Length > 0)
				strUUIDs = strUUIDs.Substring(0, strUUIDs.Length - 1);
			return strUUIDs == "" ? "''" : strUUIDs;
		}
		#endregion

		#region Public Static Method : DataTable getTO_CLOSE_DISCOUNT(string POSUUID_DETAIL)
		/// <summary>
		/// 取得未結折扣(Clone from TSAL05_Facade And Update)
		/// </summary>
		/// <param name="POSUUID_DETAIL"></param>
		/// <param name="status">1: 未結, 2: 已結</param>
		/// <returns></returns>
		public static DataTable getTO_CLOSE_DISCOUNT(string POSUUID_DETAIL, int status)
		{
			OracleConnection advtekUtilityOracleDBUtilGetConnection = null;
			DataTable dt = new DataTable();

			string sqlStr = "SELECT d.*,M.DISCOUNT_NAME, (SELECT PROMO_NAME FROM VW_DISCOUNTQUERY v WHERE ROWNUM = 1 AND v.DISCOUNT_MASTER_ID = d.DISCOUNT_ID) PROMO_NAME ";
            sqlStr += string.Format("FROM {0} d,discount_master m ", (status == 1) ? "TO_CLOSE_DISCOUNT" : "TO_CLOSE_DISCOUNT_LOG");
			sqlStr += string.Format("WHERE D.DISCOUNT_ID=M.DISCOUNT_CODE(+) AND d.POSUUID_DETAIL = '{0}'", POSUUID_DETAIL);
			try
			{
				advtekUtilityOracleDBUtilGetConnection = OracleDBUtil.GetConnection();
				dt = OracleDBUtil.GetDataSet(advtekUtilityOracleDBUtilGetConnection, sqlStr).Tables[0];
			}
			catch //(Exception ex)
			{
			}
			finally
			{
				if (advtekUtilityOracleDBUtilGetConnection.State == ConnectionState.Open) advtekUtilityOracleDBUtilGetConnection.Close();
				advtekUtilityOracleDBUtilGetConnection.Dispose();
				OracleConnection.ClearAllPools();
			}
			return dt;
		}
		#endregion

        public static void get_possuid_master(string sUUID, out string posuuid_master)
        {
            OracleConnection conn = null;
            posuuid_master = "";
            try
            {
                conn = OracleDBUtil.GetConnection();
                string sqlstr = string.Format("select posuuid_master from to_close_head where posuuid_detail = {0}", OracleDBUtil.SqlStr(sUUID));
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    posuuid_master = dr[0].ToString();
                }
                dr.Close();
                OracleDBUtil.ExecuteSql(
                           conn,
                           @"INSERT INTO SYS_PROCESS_LOG(
                 		 FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                			  )VALUES(
                  'EntryPoint', 'OnLoad', SYSDATE, " + OracleDBUtil.SqlStr("SQL:" + sqlstr + ";POSUUID_MASTER=" + posuuid_master) + ")");
                //posuuid_master = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

                //判斷SALE_HEAD的status
                bool hasRow = false;
                string sale_status = "";
                string original_id = "";
                sqlstr = "select sale_status,original_id from sale_head where posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
                cmd = new OracleCommand(sqlstr, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    sale_status = StringUtil.CStr(dr[0]);
                    original_id = StringUtil.CStr(dr[1]);
                    hasRow = true;
                }

                dr.Close();

                if (hasRow)
                {
                    //如果不是已結帳
                    if (sale_status != "2")
                    {
                        sqlstr = "select POSUUID_MASTER from SALE_HEAD where SALE_STATUS = '2' and  ORIGINAL_ID = " + OracleDBUtil.SqlStr(original_id);
                        cmd = new OracleCommand(sqlstr, conn);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            posuuid_master = dr.GetString(0);
                        }
                        else
                        {
                            throw new Exception("POSUUID_MASTER查無資料");
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string sqlstrs = string.Format(
                      @"INSERT INTO SYS_PROCESS_LOG(
                 		 FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                			  )VALUES(
                  'TSAL05', 'Error', SYSDATE, 'getPosuuidMaster PosuuidDetail  : {1},Error:{0}')", ex.Message, sUUID);
                OracleDBUtil.ExecuteSql(
                           conn, sqlstrs);
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }

        }

        public static bool CheckOtherDetail(string posuuid_detail, string posuuid_master)
        {
            bool result = false;
            OracleConnection conn = null;
            OracleCommand cmd = null;
            OracleDataReader dr = null;
            string sqlstr = "";

            //先抓出msisdn
            try
            {
                conn = OracleDBUtil.GetConnection();
                //利用posuuid_master判斷是否有其他的detail 
                if (!string.IsNullOrEmpty(posuuid_master) && !string.IsNullOrEmpty(posuuid_detail))
                {
                    sqlstr = string.Format("select * from SALE_DETAIL where posuuid_master = '{0}' and posuuid_detail != '{1}' and item_type in ('1','2','7','13','14')", posuuid_master, posuuid_detail);
                    cmd = new OracleCommand(sqlstr, conn);
                    dr = cmd.ExecuteReader();
                    bool hasRow = dr.HasRows;
                    dr.Close();
                    //如果有資料就是要換貨
                    if (hasRow)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }
            return result;
        }
	}
}
