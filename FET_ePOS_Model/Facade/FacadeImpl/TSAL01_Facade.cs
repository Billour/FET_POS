using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
	public class TSAL01_Facade : BaseClass
	{
		#region Public Static Method : List<string> get_on_hand_qty(string STOCK_ID, string PRODNO, string STORE_NO)
		/// <summary>
		/// 讀取庫存
		/// </summary>
		/// <param name="STOCK_ID">倉別</param>
		/// <param name="PRODNO">商品料號</param>
		/// <param name="STORE_NO">商店料號</param>
		/// <returns>List [0]庫存量 [1]安全量</returns>
		public static List<string> get_on_hand_qty(string STOCK_ID, string PRODNO, string STORE_NO)
		{
			List<string> list = new List<string>();
			OracleConnection conn = null;
			try
			{
				conn = OracleDBUtil.GetConnection();
				string sqlstr = "select ON_HAND_QTY,BOOKED_QTY from INV_ON_HAND_CURRENT where STOCK_ID = :STOCK_ID and PRODNO = :PRODNO and STORE_NO = :STORE_NO";
				OracleCommand cmd = new OracleCommand(sqlstr, conn);
				cmd.Parameters.Add(":STOCK_ID", OracleType.NVarChar, 32).Value = STOCK_ID;
				cmd.Parameters.Add(":PRODNO", OracleType.NVarChar, 20).Value = PRODNO;
				cmd.Parameters.Add(":STORE_NO", OracleType.NVarChar, 32).Value = STORE_NO;

				OracleDataReader dr = cmd.ExecuteReader();

				if (dr.Read())
				{
					list.Add(dr[0].ToString());
					list.Add(dr[1].ToString());
				}
				dr.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (conn.State == System.Data.ConnectionState.Open) conn.Close();
				OracleConnection.ClearAllPools();
			}
			return list;
		}
		#endregion

		#region Public Static Method : void Delete_SALE_IMEI_LOG(string SALE_DETAIL_ID, string IMEI, OracleTransaction trans)
		/// <summary>
		/// 刪除 IMEI 快取(或銷售)紀錄
		/// </summary>
		/// <param name="SALE_DETAIL_ID">SALE_DETAIL.ID</param>
		/// <param name="IMEI"></param>
		/// <param name="trans"></param>
		public static void Delete_SALE_IMEI_LOG(string SALE_DETAIL_ID, string IMEI, OracleTransaction trans)
		{
			string sqlstr = string.Format("delete from SALE_IMEI_LOG where SALE_DETAIL_ID = {0} and IMEI = {1}", OracleDBUtil.SqlStr(SALE_DETAIL_ID), OracleDBUtil.SqlStr(IMEI));
			OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
			cmd.ExecuteNonQuery();
		}
		#endregion

		#region Public Static Method : string CheckINV_IMEI(string IMEI, string SALE_DETAIL_ID)
		/// <summary>
		/// INSERT 時檢查資料存在否
		/// </summary>
		/// <param name="IMEI"></param>
		/// <returns></returns>
		public static string CheckINV_IMEI(string IMEI, string SALE_DETAIL_ID)
		{
			string result = "";
			string sqlStr = @"SELECT SALE_DETAIL_ID FROM SALE_IMEI_LOG WHERE SALE_STATUS = '1' AND IMEI = " + OracleDBUtil.SqlStr(IMEI);
			OracleConnection objConn = null;
			try
			{
				objConn = OracleDBUtil.GetConnection();
				OracleCommand cmd = new OracleCommand(sqlStr, objConn);

				result = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}

			return result;
		}
		#endregion

        public static string CheckIMEI_Detail_ID(string detail_id)
        {
            string result = "";
            string sqlStr = @"SELECT ID FROM SALE_DETAIL WHERE ID = " + OracleDBUtil.SqlStr(detail_id);
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlStr, objConn);

                result = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return result;
        }

        public static bool CheckGUARANTEE(string prodno)
        {
            bool result = false;
            string sqlStr = "SELECT GUARANTEE_ID FROM GUARANTEE_PROD_MAPPING WHERE GUARANTEE_PRODNO = " + OracleDBUtil.SqlStr(prodno);
            OracleConnection objConn = null;
            try
            {
                objConn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlStr, objConn);
                OracleDataReader dr = cmd.ExecuteReader();
                result = dr.HasRows;
                dr.Close();
  
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }

            return result;
        }

		#region Public Static Method : DataTable getApprove_AllowIMEI(string StoreNo, string Location, string PRODNO, string IMEI)
		/// <summary>
		/// 查詢可以銷售的IMEI清單
		/// </summary>
		/// <param name="StoreNo">店點</param>
		/// <param name="Location">倉別</param>
		/// <param name="PRODNO">商品料號</param>
		/// <param name="IMEI">IMEI值</param>
		/// <returns>DataTable</returns>
		public static DataTable getApprove_AllowIMEI(string StoreNo, string Location, string PRODNO, string IMEI)
		{
			string sqlStr = "";
			sqlStr = @"SELECT IMEI AS IMEI FROM IMEI WHERE IVRCODE = " + OracleDBUtil.SqlStr(StoreNo) + " And LOC = "
					   + OracleDBUtil.SqlStr(Location) + " And PRODNO = " + OracleDBUtil.SqlStr(PRODNO) + " And IMEI = "
					   + OracleDBUtil.SqlStr(IMEI) + " And STATUS = '2' And CHANNEL = 'RETAIL'";
			OracleConnection objConn = null;
			DataTable dt = null;
			try
			{
				objConn = OracleDBUtil.GetConnection();
				dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
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

		#region Public Static Method : void InsertINV_IMEI(string SALE_DETAIL_ID, string PRODNO, string IMEI, string MODI_USER)
		/// <summary>
		/// 新增IMEI
		/// </summary>
		/// <param name="TableName"></param>
		/// <param name="RefId"></param>
		/// <param name="PRODNO"></param>
		/// <param name="IMEI"></param>
		/// <param name="MODI_USER"></param>
		public static void InsertINV_IMEI(string SALE_DETAIL_ID, string PRODNO, string IMEI, string MODI_USER)
		{
			OracleConnection objConn = null;
			try
			{
				string sqlStr = @" Insert into SALE_IMEI_LOG(ID, IMEI, SALE_DETAIL_ID, CREATE_USER, CREATE_DTM
                                          ,MODI_USER, MODI_DTM, SALE_STATUS) Values('" + Advtek.Utility.GuidNo.getUUID().ToString() + "', " + OracleDBUtil.SqlStr(IMEI)
								  + ", " + OracleDBUtil.SqlStr(SALE_DETAIL_ID) + ", " + OracleDBUtil.SqlStr(MODI_USER)
								 + ", sysdate, " + OracleDBUtil.SqlStr(MODI_USER) + ", sysdate, '1')";

				objConn = OracleDBUtil.GetConnection();
				OracleDBUtil.ExecuteSql(objConn, sqlStr);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion


        public static void UpdateINV_IMEI(string SALE_DETAIL_ID, string OLD_DETAIL_ID,string imei,string MODI_USER)
        {
            OracleConnection objConn = null;
            try
            {
                string sqlStr = @" Update SALE_IMEI_LOG set  SALE_DETAIL_ID='{0}', MODI_USER = '{1}' , MODI_DTM = sysdate where SALE_DETAIL_ID='{2}' and IMEI = '{3}'";
                sqlStr = string.Format(sqlStr, SALE_DETAIL_ID, MODI_USER, OLD_DETAIL_ID, imei);
                objConn = OracleDBUtil.GetConnection();
                OracleDBUtil.ExecuteSql(objConn, sqlStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

		#region Public Static Method : bool haveGarbageIMEIRec(string IMEI)
		/// <summary>
		/// Clone from SAL01_Facade.haveGarbageIMEIRec
		/// 判斷SALE_IMEI_LOG中是否有交易已經被捨棄的IMEI紀錄, 鎖定 1 小時
		/// (SALE_DETAIL_ID存在於SALE_IMEI_LOG中,而不存在於SALE_DETAIL中)
		/// </summary>
		/// <param name="IMEI">IMEI值</param>
		/// <returns>true/false</returns>
		public static bool haveGarbageIMEIRec(string IMEI)
		{
			string sqlStr = @"Select SALE_DETAIL_ID From SALE_IMEI_LOG Where SALE_STATUS = '1' AND IMEI = " + OracleDBUtil.SqlStr(IMEI)
							+ " And to_char(sysdate, 'YYYY-MM-DD HH24:MI:SS') > to_char(CREATE_DTM + interval '1' hour, 'YYYY-MM-DD HH24:MI:SS')";

			OracleConnection objConn = null;
			bool ret = false;
			try
			{
				objConn = Advtek.Utility.OracleDBUtil.GetConnection();
				DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
				if (dt != null && dt.Rows.Count > 0)
				{
					sqlStr = @"SELECT ID From SALE_DETAIL Where ID in (Select SALE_DETAIL_ID From SALE_IMEI_LOG Where SALE_STATUS = '1' AND IMEI = "
								+ OracleDBUtil.SqlStr(IMEI)
								+ " And to_char(sysdate, 'YYYY-MM-DD HH24:MI:SS') > to_char(CREATE_DTM + interval '1' hour, 'YYYY-MM-DD HH24:MI:SS'))";
					DataTable dtDetail = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
					if (dtDetail == null || dtDetail.Rows.Count == 0)
						ret = true;
				}
			}
			catch //(Exception ex)
			{

			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
			return ret;
		}
		#endregion

		#region Public Static Method : DataTable getPaid_Detail(string POSUUID_MASTER)
		/// <summary>
		/// 取得支付明細
		/// </summary>
		/// <param name="POSUUID_MASTER"></param>
		/// <returns></returns>
		public static DataTable getPaid_Detail(string POSUUID_MASTER)
		{
			//2011-02-15 新增paid_mode '8' 找零 for 分錄帳平用, 不顯示在畫面上
			return getPaid_Detail(POSUUID_MASTER, false);
		}
		#endregion

		#region Public Static Method : DataTable getPaid_Detail(string POSUUID_MASTER, bool isAll)
		/// <summary>
		/// 取得支付明細
		/// </summary>
		/// <param name="POSUUID_MASTER"></param>
		/// <param name="isAll">是否讀取所有支付項目, 包含找零</param>
		/// <returns></returns>
		public static DataTable getPaid_Detail(string POSUUID_MASTER, bool isAll)
		{
			string sqlStr = string.Format("SELECT * FROM  PAID_DETAIL P WHERE POSUUID_MASTER={0}", OracleDBUtil.SqlStr(POSUUID_MASTER));
			//2011-02-15 新增paid_mode '8' 找零 for 分錄帳平用, 不顯示在畫面上
			if (!isAll)
				sqlStr += " And PAID_MODE != '8' ";
			sqlStr += " Order By PAID_MODE";
			OracleConnection objConn = null;
			try
			{
				objConn = Advtek.Utility.OracleDBUtil.GetConnection();
				return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Method Method : string InsertPaid_Detail(string PAID_MODE, string DESC, string AMOUNT, string POSUUID_MASTER)
		/// <summary>
		/// 新增支付明細
		/// </summary>
		/// <param name="PAID_MODE"></param>
		/// <param name="DESC"></param>
		/// <param name="AMOUNT"></param>
		/// <param name="POSUUID_MASTER"></param>
		/// <returns></returns>
		//public static string InsertPaid_Detail(string PAID_MODE, string DESC, string AMOUNT, string POSUUID_MASTER)
		public static string InsertPaid_Detail(string data, string DESC, string POSUUID_MASTER)
		{
           
			string[] dat = data.Split(':');
			string posuuid = GuidNo.getUUID();
			OracleConnection conn = null;
			OracleTransaction trans = null;
			try
			{
				conn = OracleDBUtil.GetConnection();
                OracleDBUtil.ExecuteSql(
                     conn,
                     @"INSERT INTO SYS_PROCESS_LOG(
                  FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER
                  )VALUES(
                  'TSAL01_Facade', 'InsertPaid_Detail', SYSDATE, ' DATA:" +data + "')");
				trans = conn.BeginTransaction();
                string sqlstr = "";
                if (dat[0] == "9")
                {
                    sqlstr = string.Format("INSERT INTO PAID_DETAIL(ID,PAID_MODE,PAID_AMOUNT,DESCRIPTION,POSUUID_MASTER,CREATE_DTM) values('{0}',{1},{2},'{3}','{4}',SYSDATE)", posuuid, dat[0], dat[5], DESC, POSUUID_MASTER);
                }
                else
                {
                     sqlstr = string.Format("INSERT INTO PAID_DETAIL(ID,PAID_MODE,PAID_AMOUNT,DESCRIPTION,POSUUID_MASTER,CREATE_DTM) values('{4}',{0},{1},'{2}','{3}',SYSDATE)", dat[0], dat[1], DESC, POSUUID_MASTER, posuuid);
                }
				OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
				cmd.ExecuteNonQuery();
				sqlstr = string.Empty;
				int type = Convert.ToInt32(dat[0]);
				switch (type)
				{
					case 2:
					case 3:
					case 4:
						{
							#region 信用卡相關
                            string creditCardTypeName = CreditCard_Facade.CheckCardType(dat[2].Trim());
                            double charge_rate = 0.0, creditCardFee = 0.0;
							sqlstr = "Update PAID_DETAIL Set ";
							sqlstr += "CREDIT_CARD_NO=:CREDIT_CARD_NO, CREDID_CARD_TYPE_NAME=:CREDID_CARD_TYPE_NAME, CREDIT_TYPE=:CREDIT_TYPE, ";
							sqlstr += "CREDIT_INSTALLMENT=:CREDIT_INSTALLMENT, CREDIT_BANK_ID=:CREDIT_BANK_ID, CREDIT_CARD_AUTH_NO=:CREDIT_CARD_AUTH_NO, ";
                            sqlstr += "CREDIT_CARD_TYPE_ID=:CREDIT_CARD_TYPE_ID, CREDIT_CARD_CHARGE_RATE=:CREDIT_CARD_CHARGE_RATE, CREDIT_CARD_FEE=:CREDIT_CARD_FEE, ";
                            sqlstr += "INSTALLMENT_ID=:INSTALLMENT_ID, INTEREST_RATE=:INTEREST_RATE, ";
                            sqlstr += "STORE_SETTLEMENT_RATE=:STORE_SETTLEMENT_RATE, STORE_SETTLEMENT_AMOUNT=:STORE_SETTLEMENT_AMOUNT ";
							sqlstr += "Where ID=:ID";
							cmd = new OracleCommand(sqlstr, conn, trans);
							cmd.Parameters.Add(new OracleParameter(":ID", posuuid));
							cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_NO", dat[2].Trim()));
                            cmd.Parameters.Add(new OracleParameter(":CREDID_CARD_TYPE_NAME", creditCardTypeName));
                            SAL01_Facade SAL01Facade = new SAL01_Facade();
                            DataTable dtCardType = SAL01Facade.getCreditCardType(creditCardTypeName);
                            if (dtCardType != null && dtCardType.Rows.Count > 0 && dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"] != null)
                            {
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_TYPE_ID", StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"])));
                                DataTable dtRate = SAL01Facade.getCreditDivRate(StringUtil.CStr(dtCardType.Rows[0]["CREDIT_CARD_TYPE_ID"]));
                                if (dtRate != null && dtRate.Rows.Count > 0 && dtRate.Rows[0]["charge_rate"] != null &&
                                    NumberUtil.IsNumeric(StringUtil.CStr(dtRate.Rows[0]["charge_rate"])))
                                {
                                    charge_rate = double.Parse(StringUtil.CStr(dtRate.Rows[0]["charge_rate"]));
                                    if (dat[1] != null && dat[1] != "" && NumberUtil.IsNumeric(dat[1]))
                                        creditCardFee = Math.Round(int.Parse(dat[1]) * double.Parse(StringUtil.CStr(dtRate.Rows[0]["charge_rate"])) / 100, 0);
                                }
                            }
                            else
                            {
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_TYPE_ID", DBNull.Value));
                            }
							if (type == 2)
							{
								// 一般
								cmd.Parameters.Add(new OracleParameter(":CREDIT_TYPE", 1));
								cmd.Parameters.Add(new OracleParameter(":CREDIT_INSTALLMENT", DBNull.Value));
								cmd.Parameters.Add(new OracleParameter(":CREDIT_BANK_ID", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_AUTH_NO", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_CHARGE_RATE", charge_rate));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_FEE", creditCardFee));
                                cmd.Parameters.Add(new OracleParameter(":INSTALLMENT_ID", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":INTEREST_RATE", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_RATE", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_AMOUNT", DBNull.Value));
							}
							else if (type == 3)
							{
								// 離線
								cmd.Parameters.Add(new OracleParameter(":CREDIT_TYPE", 3));
								cmd.Parameters.Add(new OracleParameter(":CREDIT_INSTALLMENT", DBNull.Value));
								cmd.Parameters.Add(new OracleParameter(":CREDIT_BANK_ID", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_AUTH_NO", dat[3].Trim()));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_CHARGE_RATE", charge_rate));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_FEE", creditCardFee));
                                cmd.Parameters.Add(new OracleParameter(":INSTALLMENT_ID", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":INTEREST_RATE", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_RATE", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_AMOUNT", DBNull.Value));
							}
							else if (type == 4)
							{
								// 分期
								cmd.Parameters.Add(new OracleParameter(":CREDIT_TYPE", 2));
								cmd.Parameters.Add(new OracleParameter(":CREDIT_INSTALLMENT", dat[7]));
								cmd.Parameters.Add(new OracleParameter(":CREDIT_BANK_ID", dat[5]));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_AUTH_NO", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_CHARGE_RATE", DBNull.Value));
                                cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_FEE", DBNull.Value));
                                DataTable dtInstallmentId = SAL01Facade.getCreditCardInstallmentId(dat[5], int.Parse(dat[7]));
                                if (dtInstallmentId != null && dtInstallmentId.Rows.Count > 0)
                                {
                                    string INSTALLMENT_ID = StringUtil.CStr(dtInstallmentId.Rows[0]["INSTELLMENT_ID"]);
                                    cmd.Parameters.Add(new OracleParameter(":INSTALLMENT_ID", INSTALLMENT_ID));
                                    if (INSTALLMENT_ID != "")
                                    {
                                        DataTable dtInterestRate = SAL01Facade.getCreditCardInterestRate(INSTALLMENT_ID);
                                        if (dtInterestRate != null && dtInterestRate.Rows.Count > 0 && dtInterestRate.Rows[0]["seqment_rate"] != null
                                            && NumberUtil.IsNumeric(StringUtil.CStr(dtInterestRate.Rows[0]["seqment_rate"])))
                                        {
                                            cmd.Parameters.Add(new OracleParameter(":INTEREST_RATE", double.Parse(StringUtil.CStr(dtInterestRate.Rows[0]["seqment_rate"]))));
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add(new OracleParameter(":INTEREST_RATE", DBNull.Value));
                                        }

                                        DataTable dtSettlementRate = SAL01Facade.getCreditCardSettlementRate(INSTALLMENT_ID);
                                        if (dtSettlementRate != null && dtSettlementRate.Rows.Count > 0 && dtSettlementRate.Rows[0]["settlement_rate"] != null
                                            && NumberUtil.IsNumeric(StringUtil.CStr(dtSettlementRate.Rows[0]["settlement_rate"])))
                                        {
                                            cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_RATE", double.Parse(StringUtil.CStr(dtSettlementRate.Rows[0]["settlement_rate"]))));
                                            if (dat[1] != null && dat[1] != "" && NumberUtil.IsNumeric(dat[1]))
                                                cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_AMOUNT", Math.Round(int.Parse(dat[1]) * double.Parse(StringUtil.CStr(dtSettlementRate.Rows[0]["settlement_rate"])) / 100, 2)));
                                        }
                                        else
                                        {
                                            cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_RATE", DBNull.Value));
                                            cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_AMOUNT", DBNull.Value));
                                        }
                                    }
                                }
                                else
                                {
                                    cmd.Parameters.Add(new OracleParameter(":INSTALLMENT_ID", DBNull.Value));
                                    cmd.Parameters.Add(new OracleParameter(":INTEREST_RATE", DBNull.Value));
                                    cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_RATE", DBNull.Value));
                                    cmd.Parameters.Add(new OracleParameter(":STORE_SETTLEMENT_AMOUNT", DBNull.Value));
                                }

							}
							cmd.ExecuteNonQuery();
                         
							break;
							#endregion
						}
                    case 9:
                        {
                            sqlstr = "Update PAID_DETAIL Set ";
                            sqlstr += "CREDIT_CARD_NO=:CREDIT_CARD_NO Where ID=:ID";
                            cmd = new OracleCommand(sqlstr, conn, trans);
                            cmd.Parameters.Add(new OracleParameter(":ID", posuuid));
                            cmd.Parameters.Add(new OracleParameter(":CREDIT_CARD_NO", dat[1].Trim()));
                            cmd.ExecuteNonQuery();
                            break;
                        }
					default:
						break;
				}
				trans.Commit();
			}
			catch (Exception ex)
			{
				trans.Rollback();
				throw ex;
			}
			finally
			{
				if (conn.State == ConnectionState.Open) 
					conn.Close();
				OracleConnection.ClearAllPools();
			}
			return posuuid;
		}
		#endregion

		#region Public Static Method : void DetelePaid_detail(string ID, string POSUUID_MASTER, OracleTransaction trans)
		/// <summary>
		/// 刪除支付明細
		/// </summary>
		/// <param name="ID">明細編號, PAID_DETAIL.ID</param>
		/// <param name="POSUUID_MASTER"></param>
		/// <param name="trans"></param>
		public static void DetelePaid_detail(string ID, string POSUUID_MASTER, OracleTransaction trans)
		{
			string sqlstr = string.Format("delete from PAID_DETAIL where ID ='{0}' and POSUUID_MASTER = '{1}'", ID, POSUUID_MASTER);
			OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
			cmd.ExecuteNonQuery();
		}
		#endregion

		#region Public Method Method : string InsertHappyGoPaid_Detail(string[] args, string POSUUID_MASTER)
		/// <summary>
		/// 新增支付明細
		/// </summary>
		/// <param name="DESC"></param>
		/// <param name="AMOUNT"></param>
		/// <param name="POSUUID_MASTER"></param>
		/// <returns></returns>
		public static string InsertHappyGoPaid_Detail(string[] args, string POSUUID_MASTER)
		{
			string Desc = string.Format("HG卡號:{0},兌換點數:{1},剩餘點數:{2}", args[2], args[3], args[4]);
			string id = GuidNo.getUUID();
			OracleConnection conn = null;
			try
			{
				conn = OracleDBUtil.GetConnection();

				string sqlstr = "INSERT INTO PAID_DETAIL(ID, PAID_MODE, PAID_AMOUNT, DESCRIPTION, HG_CARD_NO, HG_REDEEM_POINT, HG_LEFT_POINT, HG_RULE, POSUUID_MASTER, CREATE_DTM) ";
				sqlstr += string.Format("values('{0}','7','{1}','{2}','{3}','{4}','{5}','{6}','{7}',SYSDATE)", id, args[1], Desc, args[2], args[3], args[4], args[5], POSUUID_MASTER);
				OracleCommand cmd = new OracleCommand(sqlstr, conn);
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (conn.State == ConnectionState.Open) conn.Close();
				OracleConnection.ClearAllPools();
			}
			return id;
		}
		#endregion

		#region Public Static Method : bool ValidateCompanyId(string id)
		/// <summary>
		/// 檢核統一編號是否有效
		/// </summary>
		/// <param name="id">統一編號</param>
		/// <returns>True:有效; False:無效</returns>
		public static bool ValidateCompanyId(string id)
		{
			// REF: http://herolin.mine.nu/entry/is-valid-TW-company-ID
			// 檢核是否為有效統編
			// 檢核統編的做法大致上為1至8位數分別乘上1,2,1,2,1,2,4,1，將八個相乘結果的十位數與個位數相加在一起，
			// 除10除得盡就OK，若除不盡餘9且第7位是7的話，也算過關。

			byte[] companyIdChkFac = { 1, 2, 1, 2, 1, 2, 4, 1 };
			if (string.IsNullOrEmpty(id))
				return false;
			if (!Regex.IsMatch(id, "\\d{8}"))
				return false;
			else
			{
				int sum = 0;
				for (int i = 0; i < 8; i++)
				{
					int d = (id[i] - 48) * companyIdChkFac[i];
					sum += d / 10 + d % 10;
				}
				if (!(sum % 10 == 0 || id[6] == '7' && (sum + 1) % 10 == 0))
					return false;
			}
			return true;
		}
		#endregion

		#region Public Static Method : int getFETCLowLimitAmt()
		/// <summary>
		/// 取得最低 ETC 加值金額(Clone from SAL01_Facade)
		/// </summary>
		/// <returns>DataTable</returns>
		public static int getFETCLowLimitAmt()
		{
			OracleConnection objConn = null;
			try
			{
				objConn = OracleDBUtil.GetConnection();
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				sb.Append("select para_value from sys_para where para_key = 'ETC_RECV_LIMIT' ");

				DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

				if (dt != null && dt.Rows.Count > 0)
					return Convert.ToInt32(dt.Rows[0][0]);
				else
					return 0;
			}
			catch //(Exception ex)
			{
				return 0;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : String getFETCProuductNo()
		/// <summary>
		/// 取得 ETC 料號(Clone from SAL01_Facade)
		/// </summary>
		/// <returns>DataTable</returns>
		public static String getFETCProuductNo()
		{
			OracleConnection objConn = null;
			try
			{
				objConn = OracleDBUtil.GetConnection();
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select para_value from sys_para where para_key = 'ETC_CHARGE_BILLING_ITEM_CODE' ");

				DataTable dt = OracleDBUtil.GetDataSet(objConn, sb.ToString()).Tables[0];

				if (dt != null && dt.Rows.Count > 0)
					return dt.Rows[0][0].ToString();
				else
					return "";
			}
			catch //(Exception ex)
			{
				return "";
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : int getCreditDivLimitAmount()
		/// <summary>
		/// 信用卡分期支付消費門檻(Clone from SAL01_Facade)
		/// </summary>
		/// <returns>int</returns>
		public static int getCreditDivLimitAmount()
		{
			OracleConnection objConn = null;
			try
			{
				objConn = OracleDBUtil.GetConnection();
				string sqlStr = " SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='CREDIT_CARD_PAY_LIMIT' ";

				DataTable dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];

				int limitAmount = 0;
				if (dt != null && dt.Rows.Count > 0)
					if (dt.Rows[0][0] != null && NumberUtil.IsNumeric(dt.Rows[0][0].ToString()) && dt.Rows[0][0].ToString() != "")
						limitAmount = int.Parse(dt.Rows[0][0].ToString());
				return limitAmount;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : bool chkINVSetting(string STORE_NO)
		/// <summary>
		/// 檢查門市發票設定(Clone from SAL01_Facade)
		/// </summary>
		public static bool chkINVSetting(string STORE_NO)
		{
            if (STORE_NO == "HQ") return true;
			string sqlStr = @"SELECT invoice_no FROM invoice_no_pool
                                WHERE Trunc(SYSDATE) BETWEEN to_date(s_use_ym||'/01','YYYY/MM/DD') AND to_date(e_use_ym||'/01','YYYY/MM/DD') + interval '1' month - 1
                                AND inv_date IS NULL    AND store_no = " + OracleDBUtil.SqlStr(STORE_NO) + " AND ROWNUM = 1 ORDER BY invoice_no";
			OracleConnection objConn = null;
			try
			{
				objConn = Advtek.Utility.OracleDBUtil.GetConnection();
				DataTable dt = Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
				return (dt != null && dt.Rows.Count != 0);
			}
			catch (Exception ex)
			{
                Logger.Log.Error(ex.Message);
                return false;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : string getStoreDiscountProdNoAndName()
		/// <summary>
		/// 取得店長折扣料號與品名
		/// </summary>
		/// <returns>string</returns>
		public static string getStoreDiscountProdNoAndName()
		{
			OracleConnection objConn = null;
			try
			{
				objConn = OracleDBUtil.GetConnection();
				string sqlStr = " SELECT PARA_VALUE FROM SYS_PARA WHERE PARA_KEY='STORE_SPECIAL_DIS_CODE' ";

				DataTable dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];

				string result = string.Empty;
				if (dt != null && dt.Rows.Count > 0)
					if (dt.Rows[0][0] != null)
						result = dt.Rows[0][0].ToString();
				sqlStr = "SELECT PRODNAME FROM PRODUCT WHERE PRODNO='" + result + "'";
				dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
				if (dt != null && dt.Rows.Count > 0)
					if (dt.Rows[0][0] != null)
						result += "," + dt.Rows[0][0].ToString();

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : int CancelOuterSystem(...)
		/// <summary>
		/// Cancel 外部系統交易(Clone from SAL01_Facade)
		/// </summary>
		/// <param name="posuuid_detail">未結交易表頭檔主鍵</param>
		/// <param name="service_type">交易類型</param>
		/// <param name="service_sys_id">外部系統主鍵</param>
		/// <param name="bundle_id">bundle_id</param>
		/// <param name="store_no">交易店點</param>
		/// <param name="sale_person">交易人員</param>
		/// <param name="barcode1">barcode1</param>
		/// <param name="barcode2">barcode2</param>
		/// <param name="barcode3">barcode3</param>
		/// <param name="amount">單筆交易金額</param>
		/// <returns>結果:0 成功, -1 失敗</returns>
		public static int CancelOuterSystem(string posuuid_detail, string service_type, string service_sys_id, string bundle_id,
										string store_no, string sale_person, string barcode1, string barcode2, string barcode3, string amount)
		{
			string outerConnStr = "";
			string BOUouterConnStr = "";
			string outerCmd = "";
			string strMCode = "";
			string SYSID = "";
			string BouCmd = "";
			string BouID = "";
			string CheckFlag = "";
			//string SYSOK = "";
			OracleConnection objConn = null;
			OracleCommand oraCmd = null;
			OracleConnection BouConn = null;
			OracleCommand BouoraCmd = null;

			string deletedBoundleId = "";

			try
			{
				bool cancelOk = true;
				if (posuuid_detail != null && (!string.IsNullOrEmpty(posuuid_detail)))
				{
					if (service_type != null && (!string.IsNullOrEmpty(service_type)))
					{
						switch (service_type)
						{
							case "1":   //IA
								outerConnStr = OracleDBUtil.GetIAConnectionStringByTNSName();
								SYSID = "IA";
								break;
							case "2":   //LOY
								outerConnStr = OracleDBUtil.GetLOYConnectionStringByTNSName();
								SYSID = "LOY";
								break;
							case "4":   //SSI
								outerConnStr = OracleDBUtil.GetSSIConnectionStringByTNSName();
								SYSID = "SSI";
								break;
							case "3":   //PAYMENT
								outerConnStr = OracleDBUtil.GetPAYMENTConnectionStringByTNSName();
								SYSID = "PY";
								break;
							case "10":   //E-Store
								outerConnStr = OracleDBUtil.GetEStoreConnectionStringByTNSName();
								SYSID = "ES";
								break;
							default:
								break;
						}

						objConn = Advtek.Utility.OracleDBUtil.GetConnection();

						if (bundle_id != "" && deletedBoundleId.IndexOf(bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3) < 0)
						{

							BOUouterConnStr = OracleDBUtil.GetBOUConnectionStringByTNSName();
							BouConn = OracleDBUtil.GetConnectionByConnString(BOUouterConnStr);

							//BouCmd = "SP_POS_Feedback_Cancel";
							BouID = "BOU";

							string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(BouID + "_CANCEL");
							DataTable dt1 = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
							if (dt1 != null && dt1.Rows.Count > 0)
								if (dt1.Rows[0][0] != null)
									BouCmd = dt1.Rows[0][0].ToString();
							BouoraCmd = new OracleCommand(BouCmd);
							BouoraCmd.CommandType = System.Data.CommandType.StoredProcedure;
						}

						if (SYSID != "")
						{
							string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(SYSID + "_CANCEL");
							DataTable dtSys = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
							if (dtSys != null && dtSys.Rows.Count > 0)
								if (dtSys.Rows[0][0] != null)
									outerCmd = dtSys.Rows[0][0].ToString();
							objConn = OracleDBUtil.GetConnectionByConnString(outerConnStr);
						}



						//}

						try
						{
							oraCmd = new OracleCommand(outerCmd);
							oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

							switch (SYSID)
							{
								case "IA":
									oraCmd.Parameters.Add(new OracleParameter("ACTI_NO", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("UUID_DETAILS", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("ERR_CODE", OracleType.Number)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("ERR_MESG", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
									break;
								case "LOY":
									oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("msg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									break;
								case "SSI":
									oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									break;
								case "PY":
									oraCmd.Parameters.Add(new OracleParameter("SYS_KEY", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("POSUUID_DETAILS", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("BARCODE1", OracleType.VarChar, 2000)).Value = barcode1;
									oraCmd.Parameters.Add(new OracleParameter("BARCODE2", OracleType.VarChar, 2000)).Value = barcode2;
									oraCmd.Parameters.Add(new OracleParameter("BARCODE3", OracleType.VarChar, 2000)).Value = barcode3;
									oraCmd.Parameters.Add(new OracleParameter("PAY_AMOUNT", OracleType.VarChar, 2000)).Value = amount;
									oraCmd.Parameters.Add(new OracleParameter("STOREID", OracleType.VarChar, 2000)).Value = store_no;
									oraCmd.Parameters.Add(new OracleParameter("EMPLOYEE_ID", OracleType.VarChar, 2000)).Value = sale_person;
									oraCmd.Parameters.Add(new OracleParameter("RTN_SYS_KEY", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("RTN_POSUUID_DETAILS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									break;
								case "ES":
									oraCmd.Parameters.Add(new OracleParameter("POSuuid_details", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("package_id", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("employee_Id", OracleType.VarChar, 2000)).Value = sale_person;
									oraCmd.Parameters.Add(new OracleParameter("Store_Id", OracleType.VarChar, 2000)).Value = store_no;
									oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("ERROR_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("ERROR_DESCRIPTION", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									break;

							}




							// oraCmd.Parameters.Add(new OracleParameter(SOURCE_REFERENCE_Name, OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
							//  oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;

							if (oraCmd != null && oraCmd.ToString() != "")
							{
								oraCmd.Connection = objConn;
								oraCmd.ExecuteNonQuery();
							}
							switch (SYSID)
							{
								case "IA":
									strMCode = "SP_IA_CANCEL_POS,ERR_CODE=" + oraCmd.Parameters["ERR_CODE"].Value.ToString() + ",ERR_MESG=" + oraCmd.Parameters["ERR_MESG"].Value.ToString();
									if (oraCmd.Parameters["ERR_CODE"].Value.ToString() == "9999")
										cancelOk = false;
									break;
								case "LOY":
									strMCode = "SP_LOY_CANCEL_POS,msg=" + oraCmd.Parameters["msg"].Value.ToString();
									if (oraCmd.Parameters["msg"].Value.ToString() == "9999")
										cancelOk = false;
									break;
								case "SSI":
									strMCode = "SP_SSI_CANCEL_POS,result=" + oraCmd.Parameters["result"].Value.ToString();
									if (oraCmd.Parameters["result"].Value.ToString() == "9999")
										cancelOk = false;
									break;
								case "PY":
									strMCode = "SP_POS2PAYMENT_CANCEL_DATA,RTN_SYS_KEY=" + oraCmd.Parameters["RTN_SYS_KEY"].Value.ToString() + ",RTN_POSUUID_DETAILS=" + oraCmd.Parameters["RTN_POSUUID_DETAILS"].Value.ToString();
									if (oraCmd.Parameters["RTN_SYS_KEY"].Value.ToString() == "")
										cancelOk = false;
									break;
								case "ES":
									strMCode = "SP_POS4eStore_CancelOrder,ERROR_CODE=" + oraCmd.Parameters["ERROR_CODE"].Value.ToString() + ",ERROR_DESCRIPTION=" + oraCmd.Parameters["ERROR_DESCRIPTION"].Value.ToString();
									if (oraCmd.Parameters["ERROR_CODE"].Value.ToString() == "9999")
										cancelOk = false;
									break;

							}


							//if (SYSID == "BOU")
							//{
							//    oraCmd = new OracleCommand("SP_Bundle_Cancel");
							//    oraCmd.Parameters.Add(new OracleParameter("v_Bundle_Id", OracleType.VarChar, 2000)).Value = bundle_id;
							//    oraCmd.Parameters.Add(new OracleParameter("O_Result", OracleType.Number)).Direction = ParameterDirection.Output;
							//    oraCmd.Parameters.Add(new OracleParameter("O_Description", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
							//    oraCmd.Connection = objConn;
							//    oraCmd.ExecuteNonQuery();
							//    strMCode += ";SP_Bundle_Cancel,O_Result=" + oraCmd.Parameters["O_Result"].Value.ToString()
							//                + ", O_Description=" + oraCmd.Parameters["O_Description"].Value.ToString();
							//    if (oraCmd.Parameters["O_Result"].Value.ToString() == "9999")
							//        cancelOk = false;
							//}
							if (SYSID != "")
							{
								Logger.Log.Info("POS通知服務系統取消交易完成: SP=" + outerCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
												+ ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
												+ ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
												+ ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
												+ ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
												+ ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
												+ ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(strMCode));
								CheckFlag = "1";
							}


							if (bundle_id != "" && deletedBoundleId.IndexOf(bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3) < 0)
							{

								BouoraCmd.Parameters.Add(new OracleParameter("BundleID", OracleType.VarChar, 2000)).Value = bundle_id;
								BouoraCmd.Parameters.Add(new OracleParameter("ReturnCode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
								BouoraCmd.Parameters.Add(new OracleParameter("ReturnMsg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
								deletedBoundleId += "," + bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3;

								BouoraCmd.Connection = BouConn;
								BouoraCmd.ExecuteNonQuery();

								strMCode = "SP_POS_Feedback_Cancel,ReturnMsg=" + BouoraCmd.Parameters["ReturnMsg"].Value.ToString();
								if (BouoraCmd.Parameters["ReturnCode"].Value.ToString() == "9999")
									cancelOk = false;

								Logger.Log.Info("POS通知服務系統取消交易完成: SP=" + BouCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
										 + ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
										 + ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
										 + ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
										 + ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
										 + ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
										 + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(strMCode));

							}
						}
						catch (Exception ex)
						{
							string msg = ex.Message;
							cancelOk = false;
							if (SYSID != "" && CheckFlag != "1")
							{
								Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + outerCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
												+ ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
												+ ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
												+ ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
												+ ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
												+ ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
												+ ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(msg));
							}
							if (bundle_id != "")
							{
								Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + BouCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
											 + ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
											 + ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
											 + ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
											 + ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
											 + ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
											 + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(msg));
							}
						}
						finally
						{
							//  oraCmd.Dispose();
						}
					}
				}
				if (cancelOk)
					return 0;
				else
					return -1;
			}
			catch (Exception ex)
			{
				if (SYSID != "")
				{
					Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + outerCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
												+ ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
												+ ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
												+ ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
												+ ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
												+ ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
												+ OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
				}
				if (bundle_id != "")
				{
					Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + BouCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
											  + ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
											  + ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
											  + ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
											  + ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
											  + ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
											  + OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
				}
				return -1;
			}
			finally
			{
				if (oraCmd != null)
					oraCmd.Dispose();

				if (objConn != null && objConn.State == ConnectionState.Open)
					objConn.Close();

				if (objConn != null)
					objConn.Dispose();

				if (BouoraCmd != null)
					BouoraCmd.Dispose();

				if (BouConn != null && BouConn.State == ConnectionState.Open)
					BouConn.Close();

				if (BouConn != null)
					BouConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : int delTO_CLOSE(StringBuilder posuuid_detailList)
		/// <summary>
		/// 刪除未結清單資料(Clone from SAL01_Facade)
		/// </summary>
		/// <param name="posuuid_detailList">未結清單表頭檔主鍵值群</param>
		/// <returns></returns>
		public static int delTO_CLOSE(StringBuilder posuuid_detailList)
		{
			OracleConnection objConn = null;
			string where = "";
			if (posuuid_detailList.ToString().Substring(posuuid_detailList.ToString().Length - 1, 1) == ",")
				where = posuuid_detailList.ToString().Substring(0, posuuid_detailList.ToString().Length - 1);
			else
				where = posuuid_detailList.ToString();
			string sqlStr = @"Delete FROM TO_CLOSE_ITEM WHERE POSUUID_DETAIL IN (" + where + ") ";
			try
			{
				objConn = OracleDBUtil.GetConnection();
				if (Advtek.Utility.OracleDBUtil.ExecuteSql(objConn, sqlStr) > -1)
				{
					sqlStr = @"Delete FROM TO_CLOSE_DISCOUNT WHERE POSUUID_DETAIL IN (" + where + ") ";
					if (Advtek.Utility.OracleDBUtil.ExecuteSql(objConn, sqlStr) > -1)
					{
						sqlStr = @"Delete FROM TO_CLOSE_HEAD where POSUUID_DETAIL IN (" + where + ") ";
						int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objConn, sqlStr);
						if (ret > -1)
						{
							return ret;
						}
						else
						{
							return -1;
						}
					}
					else
					{
						return -1;
					}
				}
				else
				{
					return -1;
				}
			}
			catch (Exception)
			{
				return -1;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : void InsertDataUploadLog(string possuuid_detail)
		/// <summary>
		/// 新增取消交易失敗外部交易到log檔中(Clone from SAL01_Facade)
		/// </summary>
		public static void InsertDataUploadLog(string possuuid_detail)
		{
			string uid = Advtek.Utility.GuidNo.getUUID().ToString();
			string strSQL = @"Insert into data_upload_log(id, posuuid_detail, data_type, scan_count, status, cancel_date) 
                                Values(" + OracleDBUtil.SqlStr(uid) + ", " + OracleDBUtil.SqlStr(possuuid_detail) + ", '1', 0, '1', sysdate)";
			OracleConnection objConn = null;
			DataTable dtTo_Close_Head = null;
			try
			{
				objConn = OracleDBUtil.GetConnection();
				dtTo_Close_Head = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
				int ret = 0; int i = 0;
				while (ret == 0 && i < 3)
				{
					ret = OracleDBUtil.ExecuteSql(objConn, strSQL);
					i++;
				}
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
		}
		#endregion

		#region Public Static Method : DataTable getSale_Head(string POSUUID_MASTER)
		/// <summary>
		/// 取得銷售檔頭
		/// </summary>
		/// <param name="POSUUID_MASTER"></param>
		/// <returns></returns>
		public static DataTable getSale_Head(string POSUUID_MASTER)
		{
			//string sqlStr = @"SELECT '' VOUCHER_TYPE,'' HOST_ID, h.* FROM  SALE_HEAD h WHERE POSUUID_MASTER = '" + POSUUID_MASTER + "'";

			string sqlStr = @" SELECT VR.VOUCHER_TYPE AS VOUCHER_TYPE,'' HOST_ID, H.*  ";
			sqlStr += "  ,DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_NO,'2',MIH.INVOICE_NO,'3',RH.RECEIPT_NO) AS INVOICE_NO ";
			sqlStr += "  ,DECODE(VR.VOUCHER_TYPE,'1',IH.INVOICE_DATE,'2',MIH.INVOICE_DATE,'3',RH.INVOICE_DATE) AS INVOICE_DATE ";
			sqlStr += "  ,DECODE(VR.VOUCHER_TYPE,'1','發票','2',MIH.INVOICE_TYPE,'3',RH.RECEIPT_TYPE) AS INVOICE_TYPE ";
			sqlStr += " FROM SALE_HEAD H, VOUCHER_RELATION VR, MANUAL_INVOICE_HEAD MIH, INVOICE_HEAD IH, RECEIPT_HEAD RH ";
			//Tina 2010/12/20：將join VOUCHER_RELATION 從 INNER 改成 LEFT, 避免在取得CATCH資料時, SELECT 無資料,由於CATCH資料尚未產生發票或收據.
			sqlStr += " Where H.POSUUID_MASTER = VR.SALE_HEAD_ID(+)  ";
			sqlStr += " And H.POSUUID_MASTER = MIH.POSUUID_MASTER(+) ";
			sqlStr += " And H.POSUUID_MASTER = IH.POSUUID_MASTER(+)  ";
			sqlStr += " And H.POSUUID_MASTER = RH.POSUUID_MASTER(+)  ";
			sqlStr += " And H.POSUUID_MASTER = '" + POSUUID_MASTER + "'";

			OracleConnection objConn = null;
			try
			{
				objConn = Advtek.Utility.OracleDBUtil.GetConnection();
				return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : DataTable getSale_Detail(string POSUUID_MASTER, string ITEM_TYPE)
		/// <summary>
		/// 取得銷售明細
		/// </summary>
		/// <param name="POSUUID_MASTER"></param>
		/// <param name="ITEM_TYPE"></param>
		/// <returns></returns>
		public static DataTable getSale_Detail(string POSUUID_MASTER, string ITEM_TYPE)
		{
			string sqlStr = @"SELECT ROWNUM ITEMS,0 IMEI_QTY,
                                     DECODE(ITEM_TYPE,'1','單',--直接輸入
                                                      '2','促',--未結清單而來
                                                      '3','預',--預收轉採購
                                                      '4','折',--HAPPY GO 折扣
                                                      '5','折',--銷售折扣
                                                      '6','折' --店長折扣
                                            ) ITEM_TYPE_NAME,
                                 MM.PROMO_NAME,
                                 P.PRODNAME,
                                 NVL(P.ISSTOCK,0) ISSTOCK,
                                 '' IMEI,
                             d.* FROM  SALE_DETAIL d ,
                             (Select * from MM Where Sysdate Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) And 
                                NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM, PRODUCT P
                          WHERE d.PROMOTION_CODE = MM.PROMO_NO(+)  
                            AND d.PRODNO = P.PRODNO(+)    
                            AND POSUUID_MASTER = '" + POSUUID_MASTER + "'";


			string where = "";
			if (ITEM_TYPE != "") //未結折扣資料要另外放
			{
				ITEM_TYPE = "'" + ITEM_TYPE.Replace(",", "','") + "'";
				where += " AND ITEM_TYPE IN(" + ITEM_TYPE + ")";
			}
			sqlStr += where;

			OracleConnection objConn = null;
			try
			{
				objConn = Advtek.Utility.OracleDBUtil.GetConnection();
				return Advtek.Utility.OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion

		#region Public Static Method : string[] getAllowPayMode()
		/// <summary>
		/// 取得支付方式使用權限
		/// </summary>
		/// <returns>int[]</returns>
		public static string[] getAllowPayMode()
		{
			OracleConnection objConn = null;
			List<string> ids = new List<string>();
			try
			{
				objConn = OracleDBUtil.GetConnection();
				string sqlStr = "select PAY_MODE_ID From PAYMENT_METHOD_SET Where SYSDATE >= NVL(S_DATE, SYSDATE) AND SYSDATE <= NVL(E_DATE+1, SYSDATE) Order By PAY_MODE_ID";

				DataTable dt = OracleDBUtil.GetDataSet(objConn, sqlStr).Tables[0];
				foreach (DataRow dr in dt.Rows)
					ids.Add(dr["PAY_MODE_ID"].ToString());
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (objConn.State == ConnectionState.Open)
					objConn.Close();
				objConn.Dispose();
				OracleConnection.ClearAllPools();
			}
			return ids.ToArray();
		}
		#endregion


		#region Cancel 外部系統交易 Public Static Method : DataTable getSale_Detail(string POSUUID_MASTER, string ITEM_TYPE)
		/// Cancel 外部系統交易
		/// </summary>
		/// <param name="posuuid_detail">未結交易表頭檔主鍵</param>
		/// <param name="service_type">交易類型</param>
		/// <param name="service_sys_id">外部系統主鍵</param>
		/// <param name="bundle_id">bundle_id</param>
		/// <param name="store_no">交易店點</param>
		/// <param name="sale_person">交易人員</param>
		/// <param name="barcode1">barcode1</param>
		/// <param name="barcode2">barcode2</param>
		/// <param name="barcode3">barcode3</param>
		/// <param name="amount">單筆交易金額</param>
		/// <returns>結果:0 成功, -1 失敗</returns>
		public int CancelOuterSystem(string posuuid_detail, string service_type, string service_sys_id, string bundle_id,
										string store_no, string sale_person, string barcode1, string barcode2, string barcode3, string amount, OracleTransaction trans)
		{
			string outerConnStr = "";
			string BOUouterConnStr = "";
			string outerCmd = "";
			string strMCode = "";
			string SYSID = "";
			string BouCmd = "";
			string BouID = "";
			string CheckFlag = "";
			//string SYSOK = "";
			OracleConnection objConn = null;
			OracleCommand oraCmd = null;
			OracleConnection BouConn = null;
			OracleCommand BouoraCmd = null;

			string deletedBoundleId = "";

			try
			{
				bool cancelOk = true;
				if (posuuid_detail != null && (!string.IsNullOrEmpty(posuuid_detail)))
				{
					if (service_type != null && (!string.IsNullOrEmpty(service_type)))
					{
						switch (service_type)
						{
							case "1":   //IA
								outerConnStr = OracleDBUtil.GetIAConnectionStringByTNSName();
								SYSID = "IA";
								break;
							case "2":   //LOY
								outerConnStr = OracleDBUtil.GetLOYConnectionStringByTNSName();
								SYSID = "LOY";
								break;
							case "4":   //SSI
								outerConnStr = OracleDBUtil.GetSSIConnectionStringByTNSName();
								SYSID = "SSI";
								break;
							case "3":   //PAYMENT
								outerConnStr = OracleDBUtil.GetPAYMENTConnectionStringByTNSName();
								SYSID = "PY";
								break;
							case "10":   //E-Store
								outerConnStr = OracleDBUtil.GetEStoreConnectionStringByTNSName();
								SYSID = "ES";
								break;
							default:
								break;
						}

						objConn = trans.Connection;

						if (bundle_id != "" && deletedBoundleId.IndexOf(bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3) < 0)
						{

							BOUouterConnStr = OracleDBUtil.GetBOUConnectionStringByTNSName();
							BouConn = OracleDBUtil.GetConnectionByConnString(BOUouterConnStr);

							//BouCmd = "SP_POS_Feedback_Cancel";
							BouID = "BOU";

							string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(BouID + "_CANCEL");
							DataTable dt1 = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
							if (dt1 != null && dt1.Rows.Count > 0)
								if (dt1.Rows[0][0] != null)
									BouCmd = dt1.Rows[0][0].ToString();
							BouoraCmd = new OracleCommand(BouCmd);
							BouoraCmd.CommandType = System.Data.CommandType.StoredProcedure;
						}

						if (SYSID != "")
						{
							string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(SYSID + "_CANCEL");
							DataTable dtSys = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
							if (dtSys != null && dtSys.Rows.Count > 0)
								if (dtSys.Rows[0][0] != null)
									outerCmd = dtSys.Rows[0][0].ToString();
							objConn = OracleDBUtil.GetConnectionByConnString(outerConnStr);
						}



						//}

						try
						{
							oraCmd = new OracleCommand(outerCmd);
							oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

							switch (SYSID)
							{
								case "IA":
									oraCmd.Parameters.Add(new OracleParameter("ACTI_NO", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("UUID_DETAILS", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("ERR_CODE", OracleType.Number)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("ERR_MESG", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
									break;
								case "LOY":
									oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("msg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									break;
								case "SSI":
									oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									break;
								case "PY":
									oraCmd.Parameters.Add(new OracleParameter("SYS_KEY", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("POSUUID_DETAILS", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("BARCODE1", OracleType.VarChar, 2000)).Value = barcode1;
									oraCmd.Parameters.Add(new OracleParameter("BARCODE2", OracleType.VarChar, 2000)).Value = barcode2;
									oraCmd.Parameters.Add(new OracleParameter("BARCODE3", OracleType.VarChar, 2000)).Value = barcode3;
									oraCmd.Parameters.Add(new OracleParameter("PAY_AMOUNT", OracleType.VarChar, 2000)).Value = amount;
									oraCmd.Parameters.Add(new OracleParameter("STOREID", OracleType.VarChar, 2000)).Value = store_no;
									oraCmd.Parameters.Add(new OracleParameter("EMPLOYEE_ID", OracleType.VarChar, 2000)).Value = sale_person;
									oraCmd.Parameters.Add(new OracleParameter("RTN_SYS_KEY", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("RTN_POSUUID_DETAILS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									break;
								case "ES":
									oraCmd.Parameters.Add(new OracleParameter("POSuuid_details", OracleType.VarChar, 2000)).Value = posuuid_detail;
									oraCmd.Parameters.Add(new OracleParameter("package_id", OracleType.VarChar, 2000)).Value = service_sys_id;
									oraCmd.Parameters.Add(new OracleParameter("employee_Id", OracleType.VarChar, 2000)).Value = sale_person;
									oraCmd.Parameters.Add(new OracleParameter("Store_Id", OracleType.VarChar, 2000)).Value = store_no;
									oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("ERROR_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									oraCmd.Parameters.Add(new OracleParameter("ERROR_DESCRIPTION", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
									break;

							}




							// oraCmd.Parameters.Add(new OracleParameter(SOURCE_REFERENCE_Name, OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
							//  oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;

							if (oraCmd != null && oraCmd.ToString() != "")
							{
								oraCmd.Connection = objConn;
								oraCmd.ExecuteNonQuery();
							}
							switch (SYSID)
							{
								case "IA":
									strMCode = "SP_IA_CANCEL_POS,ERR_CODE=" + oraCmd.Parameters["ERR_CODE"].Value.ToString() + ",ERR_MESG=" + oraCmd.Parameters["ERR_MESG"].Value.ToString();
									if (oraCmd.Parameters["ERR_CODE"].Value.ToString() == "9999")
										cancelOk = false;
									break;
								case "LOY":
									strMCode = "SP_LOY_CANCEL_POS,msg=" + oraCmd.Parameters["msg"].Value.ToString();
									if (oraCmd.Parameters["msg"].Value.ToString() == "9999")
										cancelOk = false;
									break;
								case "SSI":
									strMCode = "SP_SSI_CANCEL_POS,result=" + oraCmd.Parameters["result"].Value.ToString();
									if (oraCmd.Parameters["result"].Value.ToString() == "9999")
										cancelOk = false;
									break;
								case "PY":
									strMCode = "SP_POS2PAYMENT_CANCEL_DATA,RTN_SYS_KEY=" + oraCmd.Parameters["RTN_SYS_KEY"].Value.ToString() + ",RTN_POSUUID_DETAILS=" + oraCmd.Parameters["RTN_POSUUID_DETAILS"].Value.ToString();
									if (oraCmd.Parameters["RTN_SYS_KEY"].Value.ToString() == "")
										cancelOk = false;
									break;
								case "ES":
									strMCode = "SP_POS4eStore_CancelOrder,ERROR_CODE=" + oraCmd.Parameters["ERROR_CODE"].Value.ToString() + ",ERROR_DESCRIPTION=" + oraCmd.Parameters["ERROR_DESCRIPTION"].Value.ToString();
									if (oraCmd.Parameters["ERROR_CODE"].Value.ToString() == "9999")
										cancelOk = false;
									break;

							}


							//if (SYSID == "BOU")
							//{
							//    oraCmd = new OracleCommand("SP_Bundle_Cancel");
							//    oraCmd.Parameters.Add(new OracleParameter("v_Bundle_Id", OracleType.VarChar, 2000)).Value = bundle_id;
							//    oraCmd.Parameters.Add(new OracleParameter("O_Result", OracleType.Number)).Direction = ParameterDirection.Output;
							//    oraCmd.Parameters.Add(new OracleParameter("O_Description", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
							//    oraCmd.Connection = objConn;
							//    oraCmd.ExecuteNonQuery();
							//    strMCode += ";SP_Bundle_Cancel,O_Result=" + oraCmd.Parameters["O_Result"].Value.ToString()
							//                + ", O_Description=" + oraCmd.Parameters["O_Description"].Value.ToString();
							//    if (oraCmd.Parameters["O_Result"].Value.ToString() == "9999")
							//        cancelOk = false;
							//}
							if (SYSID != "")
							{
								Logger.Log.Info("POS通知服務系統取消交易完成: SP=" + outerCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
												+ ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
												+ ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
												+ ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
												+ ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
												+ ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
												+ ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(strMCode));
								CheckFlag = "1";
							}


							if (bundle_id != "" && deletedBoundleId.IndexOf(bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3) < 0)
							{

								BouoraCmd.Parameters.Add(new OracleParameter("BundleID", OracleType.VarChar, 2000)).Value = bundle_id;
								BouoraCmd.Parameters.Add(new OracleParameter("ReturnCode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
								BouoraCmd.Parameters.Add(new OracleParameter("ReturnMsg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
								deletedBoundleId += "," + bundle_id + "^" + barcode1 + "^" + barcode2 + "^" + barcode3;

								BouoraCmd.Connection = BouConn;
								BouoraCmd.ExecuteNonQuery();

								strMCode = "SP_POS_Feedback_Cancel,ReturnMsg=" + BouoraCmd.Parameters["ReturnMsg"].Value.ToString();
								if (BouoraCmd.Parameters["ReturnCode"].Value.ToString() == "9999")
									cancelOk = false;

								Logger.Log.Info("POS通知服務系統取消交易完成: SP=" + BouCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
										 + ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
										 + ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
										 + ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
										 + ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
										 + ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
										 + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(strMCode));

							}
						}
						catch (Exception ex)
						{
							string msg = ex.Message;
							cancelOk = false;
							if (SYSID != "" && CheckFlag != "1")
							{
								Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + outerCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
												+ ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
												+ ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
												+ ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
												+ ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
												+ ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
												+ ", connection string[" + outerConnStr + "]," + OracleDBUtil.SqlStr(msg));
							}
							if (bundle_id != "")
							{
								Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + BouCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
											 + ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
											 + ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
											 + ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
											 + ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
											 + ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
											 + ", connection string[" + BOUouterConnStr + "]," + OracleDBUtil.SqlStr(msg));
							}
						}
						finally
						{
							//  oraCmd.Dispose();
						}
					}
				}
				if (cancelOk)
					return 0;
				else
					return -1;
			}
			catch (Exception ex)
			{
				if (SYSID != "")
				{
					Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + outerCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
												+ ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
												+ ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
												+ ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
												+ ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
												+ ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
												+ OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
				}
				if (bundle_id != "")
				{
					Logger.Log.Info("POS通知服務系統取消交易失敗: SP=" + BouCmd + ",POSuuid_Details=" + OracleDBUtil.SqlStr(posuuid_detail)
											  + ",image_key=" + OracleDBUtil.SqlStr(service_sys_id) + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id)
											  + ", Barcode1=" + OracleDBUtil.SqlStr(barcode1) + ", Barcode2=" + OracleDBUtil.SqlStr(barcode2)
											  + ", Barcode3=" + OracleDBUtil.SqlStr(barcode3)
											  + ", Pay_Amount=" + OracleDBUtil.SqlStr(amount) + ", Store_Id=" + OracleDBUtil.SqlStr(store_no)
											  + ", Employee_Id=" + OracleDBUtil.SqlStr(sale_person)
											  + OracleDBUtil.SqlStr(ex.Message.Replace("'", "-").Replace("\"", " ")));
				}
				return -1;
			}
			finally
			{
				if (oraCmd != null)
					oraCmd.Dispose();

				if (objConn != null && objConn.State == ConnectionState.Open)
					objConn.Close();

				if (objConn != null)
					objConn.Dispose();

				if (BouoraCmd != null)
					BouoraCmd.Dispose();

				if (BouConn != null && BouConn.State == ConnectionState.Open)
					BouConn.Close();

				if (BouConn != null)
					BouConn.Dispose();
				OracleConnection.ClearAllPools();
			}
		}
		#endregion


		/// <summary>
		/// 刪除尚未結帳銷售資料
		/// </summary>
		/// <param name="POSUUID_MASTER">銷售表頭檔鍵值</param>
		/// <returns></returns>
		public int delSaleData(string POSUUID_MASTER, OracleTransaction objTX)
		{
			OracleConnection objConn = null;


			objConn = objTX.Connection;


			string sqlStrPaid = @"Delete FROM PAID_DETAIL WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
			OracleDBUtil.ExecuteSql(objTX, sqlStrPaid);

			string sqlStr = @"Delete FROM SALE_DETAIL WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);


			if (OracleDBUtil.ExecuteSql(objTX, sqlStr) > -1)
			{
				sqlStr = @"Delete FROM SALE_HEAD WHERE POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
				int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(objTX, sqlStr);
				if (ret > -1)
				{

					return ret;
				}
				else
				{

					return -1;
				}
			}
			else
			{

				return -1;
			}
		}




		/// <summary>
		/// 更新未結表頭檔
		/// </summary>
		public void UpdateUnCloseHead(string posuuid_detail, string possuid_master, string status, string modifyUser, OracleTransaction trans)
		{
			string strSQL = @"Update TO_CLOSE_HEAD Set POSUUID_MASTER = " + OracleDBUtil.SqlStr(possuid_master)
								+ ", STATUS = " + OracleDBUtil.SqlStr(status) + ", MODI_USER = "
								+ OracleDBUtil.SqlStr(modifyUser) + ", MODI_DTM = sysdate Where POSUUID_DETAIL = "
								+ OracleDBUtil.SqlStr(posuuid_detail);
			OracleDBUtil.ExecuteSql(trans, strSQL);

		}



		/// <summary>
		/// 取得未結清單資料 for 取消交易用
		/// </summary>
		/// <param name="POSUUID_MASTER">銷售主檔鍵值</param>
		/// <returns></returns>
		public DataTable getCancleTO_CLOSE_DATA(string POSUUID_MASTER, OracleTransaction trans)
		{

			string sqlStr = @"Select h.POSUUID_DETAIL, h.SERVICE_TYPE, h.SERVICE_SYS_ID, h.BUNDLE_ID, h.TOTAL_AMOUNT, h.STORE_NO, h.SALE_PERSON 
                                    , i.BARCODE1, i.BARCODE2, i.BARCODE3, i.AMOUNT 
                                FROM TO_CLOSE_HEAD h, 
                                (Select POSUUID_DETAIL, BARCODE1, BARCODE2, BARCODE3, AMOUNT From TO_CLOSE_ITEM 
                                 Where BARCODE1 is not null or BARCODE2 is not null or BARCODE3 is not null 
                                 And POSUUID_DETAIL IN (Select POSUUID_DETAIL From TO_CLOSE_HEAD where POSUUID_MASTER = "
									+ OracleDBUtil.SqlStr(POSUUID_MASTER) + ")) i Where h.POSUUID_DETAIL = i.POSUUID_DETAIL(+) And h.POSUUID_MASTER = "
									+ OracleDBUtil.SqlStr(POSUUID_MASTER);
			DataTable dt = dt = OracleDBUtil.GetDataSet(trans, sqlStr).Tables[0];

			return dt;
		}


		/// <summary>
		/// 刪除未結清單資料
		/// </summary>
		/// <param name="posuuid_detailList">未結清單表頭檔主鍵值群</param>
		/// <returns></returns>
		public int delTO_CLOSE(StringBuilder posuuid_detailList, OracleTransaction trans)
		{

			string where = "";
			if (posuuid_detailList.ToString().Substring(posuuid_detailList.ToString().Length - 1, 1) == ",")
				where = posuuid_detailList.ToString().Substring(0, posuuid_detailList.ToString().Length - 1);
			else
				where = posuuid_detailList.ToString();
			string sqlStr = @"Delete FROM TO_CLOSE_ITEM WHERE POSUUID_DETAIL IN (" + where + ") ";
			try
			{

				if (Advtek.Utility.OracleDBUtil.ExecuteSql(trans, sqlStr) > -1)
				{
					sqlStr = @"Delete FROM TO_CLOSE_DISCOUNT WHERE POSUUID_DETAIL IN (" + where + ") ";
					if (Advtek.Utility.OracleDBUtil.ExecuteSql(trans, sqlStr) > -1)
					{
						sqlStr = @"Delete FROM TO_CLOSE_HEAD where POSUUID_DETAIL IN (" + where + ") ";
						int ret = Advtek.Utility.OracleDBUtil.ExecuteSql(trans, sqlStr);
						if (ret > -1)
						{
							return ret;
						}
						else
						{
							return -1;
						}
					}
					else
					{
						return -1;
					}
				}
				else
				{
					return -1;
				}
			}
			catch (Exception)
			{
				return -1;
			}

		}


		/// <summary>
		/// 新增取消交易失敗外部交易到log檔中
		/// </summary>
		public void InsertDataUploadLog(string possuuid_detail, OracleTransaction trans)
		{
      
            string strSQL = "";
			string uid = Advtek.Utility.GuidNo.getUUID().ToString();
			strSQL = @"Insert into data_upload_log(id, posuuid_detail, data_type, scan_count, status, cancel_date) 
                                Values(" + OracleDBUtil.SqlStr(uid) + ", " + OracleDBUtil.SqlStr(possuuid_detail) + ", '1', 0, '1', sysdate)";
            OracleCommand cmd = new OracleCommand(strSQL, trans.Connection, trans);

            int ret = cmd.ExecuteNonQuery();

		}


        public DataTable getSale_Detail(string POSUUID_MASTER, string ITEM_TYPE, OracleTransaction trans)
        {
            string sqlStr = @"SELECT ROWNUM ITEMS,0 IMEI_QTY,
                                     DECODE(ITEM_TYPE,'1','單',--直接輸入
                                                      '2','促',--未結清單而來
                                                      '3','預',--預收轉採購
                                                      '4','折',--HAPPY GO 折扣
                                                      '5','折',--銷售折扣
                                                      '6','折' --店長折扣
                                            ) ITEM_TYPE_NAME,
                                 MM.PROMO_NAME,
                                 P.PRODNAME,
                                 NVL(P.ISSTOCK,0) ISSTOCK,
                                 '' IMEI,
                             d.* FROM  SALE_DETAIL d ,
                             (Select * from MM Where Sysdate Between NVL(B_DATE, to_date('2000/01/01','YYYY/MM/DD')) And 
                                NVL(E_DATE,to_date('3000/01/01','YYYY/MM/DD')) And PROMO_STATUS = '1') MM, PRODUCT P
                          WHERE d.PROMOTION_CODE = MM.PROMO_NO(+)  
                            AND d.PRODNO = P.PRODNO(+)    
                            AND POSUUID_MASTER = '" + POSUUID_MASTER + "'";


            string where = "";
            if (ITEM_TYPE != "") //未結折扣資料要另外放
            {
                ITEM_TYPE = "'" + ITEM_TYPE.Replace(",", "','") + "'";
                where += " AND ITEM_TYPE IN(" + ITEM_TYPE + ")";
            }
            sqlStr += where;


            return Advtek.Utility.OracleDBUtil.GetDataSet(trans, sqlStr).Tables[0];
        }



        /// <summary>
        /// Commit外部系統交易狀態
        /// </summary>
        /// <param name="SYSID">系統別</param>
        /// <param name="SERVICE_SYS_ID">外部系統主鍵值</param>
        /// <param name="POSUUID_MASTER">銷售主鍵值</param>
        /// <param name="POSUUID_DETAIL">未結清單主鍵值</param>
        /// <returns>結果:0 成功, -1 失敗</returns>
        public int CommitOuterSystem(string SYSID, string SERVICE_SYS_ID, string POSUUID_MASTER, string POSUUID_DETAIL, string Barcode1,
                                        string EMPLOYEE_ID, string STORE_ID, string bundle_id, string barcode2, string barcode3)
        {
            string outerConnStr = "";
            string BOUouterConnStr = "";
            string outerCmd = "";
            string strMCode = "";
            string BouCmd = "";
            string BouID = "";
            string CheckFlag = "";
            //string SYSOK = "";
            OracleConnection objConn = null;
            OracleCommand oraCmd = null;
            OracleConnection BouConn = null;
            OracleCommand BouoraCmd = null;
            try
            {
                objConn = Advtek.Utility.OracleDBUtil.GetConnection();
                if (SYSID != "")
                {
                    string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(SYSID + "_COMMIT");
                    DataTable dtSys = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                    if (dtSys != null && dtSys.Rows.Count > 0)
                        if (dtSys.Rows[0][0] != null)
                            outerCmd = dtSys.Rows[0][0].ToString();
                }

                switch (SYSID)
                {
                    case "IA":
                        outerConnStr = OracleDBUtil.GetIAConnectionStringByTNSName();
                        break;
                    case "LOY":
                        outerConnStr = OracleDBUtil.GetLOYConnectionStringByTNSName();
                        break;
                    case "SSI":
                        outerConnStr = OracleDBUtil.GetSSIConnectionStringByTNSName();
                        break;
                    case "PY":
                        outerConnStr = OracleDBUtil.GetPAYMENTConnectionStringByTNSName();
                        break;
                    case "OLR":
                        outerConnStr = OracleDBUtil.GetOLRConnectionStringByTNSName();
                        break;
                    case "ES":
                        outerConnStr = OracleDBUtil.GetEStoreConnectionStringByTNSName();
                        break;
                }

                if (bundle_id != "")
                {


                    BOUouterConnStr = OracleDBUtil.GetBOUConnectionStringByTNSName();

                    BouID = "BOU";
                    string strSQL = "Select para_value from sys_para where para_key = " + OracleDBUtil.SqlStr(BouID + "_COMMIT");
                    DataTable dt1 = OracleDBUtil.GetDataSet(objConn, strSQL).Tables[0];
                    if (dt1 != null && dt1.Rows.Count > 0)
                        if (dt1.Rows[0][0] != null)
                            BouCmd = dt1.Rows[0][0].ToString();

                    BouConn = OracleDBUtil.GetConnectionByConnString(BOUouterConnStr);
                    BouoraCmd = new OracleCommand(BouCmd);
                    BouoraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                }

                if (outerCmd != "" || BouCmd != "")
                {
                    objConn = OracleDBUtil.GetConnectionByConnString(outerConnStr);
                    oraCmd = new OracleCommand(outerCmd);
                    oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                    switch (SYSID)
                    {
                        case "IA":
                            oraCmd.Parameters.Add(new OracleParameter("ACTI_NO", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("UUID_MASTER", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                            oraCmd.Parameters.Add(new OracleParameter("ERR_CODE", OracleType.Number)).Direction = ParameterDirection.Output;
                            oraCmd.Parameters.Add(new OracleParameter("ERR_MESG", OracleType.VarChar, 4000)).Direction = ParameterDirection.Output;
                            break;
                        case "LOY":
                            oraCmd.Parameters.Add(new OracleParameter("POSuuid_Details", OracleType.VarChar, 2000)).Value = POSUUID_DETAIL;
                            oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("vPOSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("msg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            break;
                        case "SSI":
                            oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            break;
                        case "PY":
                            oraCmd.Parameters.Add(new OracleParameter("SYS_KEY", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("POSUUID_MASTER", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("BARCODE1", OracleType.VarChar, 2000)).Value = Barcode1;
                            oraCmd.Parameters.Add(new OracleParameter("RTN_POSUUID_MASTER", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            break;
                        case "OLR":
                            oraCmd.Parameters.Add(new OracleParameter("INPUTTXNO", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("POSUUID2", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("po_err_msg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            break;
                        case "ES":
                            oraCmd.Parameters.Add(new OracleParameter("package_id", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                            oraCmd.Parameters.Add(new OracleParameter("POSuuid_details", OracleType.VarChar, 2000)).Value = POSUUID_DETAIL;
                            oraCmd.Parameters.Add(new OracleParameter("POSuuid_master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                            oraCmd.Parameters.Add(new OracleParameter("employee_Id", OracleType.VarChar, 2000)).Value = EMPLOYEE_ID;
                            oraCmd.Parameters.Add(new OracleParameter("Store_Id", OracleType.VarChar, 2000)).Value = STORE_ID;
                            oraCmd.Parameters.Add(new OracleParameter("STATUS", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            oraCmd.Parameters.Add(new OracleParameter("ERROR_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            oraCmd.Parameters.Add(new OracleParameter("ERROR_DESCRIPTION", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            break;

                    }



                    // oraCmd.Parameters.Add(new OracleParameter(SOURCE_REFERENCE_NAME, OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
                    //  oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
                    if (SYSID != "")
                    {
                        oraCmd.Connection = objConn;
                        oraCmd.ExecuteNonQuery();

                    }


                    string ERR_CODE = "";
                    string ERR_MESG = "";
                    string msg = "";
                    string result = "";
                    string ERROR_CODE = "";
                    string ERROR_DESCRIPTION = "";
                    switch (SYSID)
                    {
                        case "IA":
                            ERR_CODE = oraCmd.Parameters["ERR_CODE"].Value.ToString();
                            ERR_MESG = oraCmd.Parameters["ERR_MESG"].Value.ToString();
                            strMCode = ",ERR_CODE=" + ERR_CODE + ",ERR_MESG=" + ERR_MESG;
                            if (ERROR_CODE == "-1")
                            {
                                throw new Exception(strMCode);
                            }
                            break;
                        case "LOY":
                            msg = oraCmd.Parameters["msg"].Value.ToString();
                            strMCode = ",msg=" + msg;
                            break;
                        case "SSI":
                            result =  oraCmd.Parameters["result"].Value.ToString();
                            oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                            strMCode = ",result=" + result;
                            break;
                        case "ES":
                            ERROR_CODE = oraCmd.Parameters["ERROR_CODE"].Value.ToString();
                            ERROR_DESCRIPTION = oraCmd.Parameters["ERROR_DESCRIPTION"].Value.ToString();
                            strMCode = ",ERROR_CODE=" + ERROR_CODE + ",ERROR_DESCRIPTION=" + ERROR_DESCRIPTION;
                            break;

                    }
                    if (SYSID != "")
                    {
                        Logger.Log.Info("POS通知服務系統結帳完成: SP=" + outerCmd + ",POSuuid_Details=" + POSUUID_DETAIL + ",image_key=" + SERVICE_SYS_ID +
                                            ", POSuuid_Master=" + POSUUID_MASTER + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id) + ", Barcode1=" + Barcode1 + ", EMPLOYEE_ID=" + EMPLOYEE_ID +
                                            ", Barcode2=" + OracleDBUtil.SqlStr(barcode2) + ", Barcode3=" + OracleDBUtil.SqlStr(barcode3) +
                                            ", STORE_ID=" + STORE_ID + ", connection string[" + outerConnStr + "]," + strMCode);
                        CheckFlag = "1";

                    }
                    if (bundle_id != "")
                    {
                        //SYSOK = "1";
                        BouoraCmd.Parameters.Add(new OracleParameter("BundleID", OracleType.VarChar, 2000)).Value = bundle_id;
                        BouoraCmd.Parameters.Add(new OracleParameter("ReturnCode", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                        BouoraCmd.Parameters.Add(new OracleParameter("ReturnMsg", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                        BouoraCmd.Connection = BouConn;
                        BouoraCmd.ExecuteNonQuery();
                        strMCode = "SP_POS_Feedback_Apply,ReturnMsg=" + BouoraCmd.Parameters["ReturnMsg"].Value.ToString();

                        Logger.Log.Info("POS通知服務系統結帳完成: SP=" + BouCmd + ",POSuuid_Details=" + POSUUID_DETAIL + ",image_key=" + SERVICE_SYS_ID +
                                            ", POSuuid_Master=" + POSUUID_MASTER + ", Boundle_Id=" + OracleDBUtil.SqlStr(bundle_id) + ", Barcode1=" + Barcode1 + ", EMPLOYEE_ID=" + EMPLOYEE_ID +
                                            ", Barcode2=" + OracleDBUtil.SqlStr(barcode2) + ", Barcode3=" + OracleDBUtil.SqlStr(barcode3) +
                                            ", STORE_ID=" + STORE_ID + ", connection string[" + BOUouterConnStr + "]," + strMCode);


                    }
                    return 0;
                }
                else
                {
                    if (SYSID != "")
                    {

                        Logger.Log.Info("POS通知服務系統結帳失敗: SYSID=" + OracleDBUtil.SqlStr(SYSID) + " 在系統中找不到參數[" + OracleDBUtil.SqlStr(SYSID) + "_COMMIT]");
                    }
                    if (bundle_id != "")
                    {
                        Logger.Log.Info("POS通知服務系統結帳失敗: SYSID=" + OracleDBUtil.SqlStr(BouID) + " 在系統中找不到參數[" + OracleDBUtil.SqlStr(BouID) + "_COMMIT]");
                    }
                    return -1;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //string exmsgg = "";
                if (SYSID != "" && CheckFlag != "1")
                {
                    Logger.Log.Info("POS通知服務系統結帳失敗: SP=" + outerCmd + ",POSuuid_Details=" + POSUUID_DETAIL + ",image_key=" + SERVICE_SYS_ID +
                                        ", POSuuid_Master=" + POSUUID_MASTER + ", Boundle_Id=" + bundle_id + ", Barcode1=" + Barcode1 + ", Barcode2=" + barcode2 + ", Barcode3=" + barcode3 + ", EMPLOYEE_ID=" + EMPLOYEE_ID +
                                        ", STORE_ID=" + STORE_ID + ", connection string[" + outerConnStr + "]," + msg);
                }

                if (bundle_id != "")
                {
                    Logger.Log.Info("POS通知服務系統結帳失敗: SP=" + BouCmd + ",POSuuid_Details=" + POSUUID_DETAIL + ",image_key=" + SERVICE_SYS_ID +
                                      ", POSuuid_Master=" + POSUUID_MASTER + ", Boundle_Id=" + bundle_id + ", Barcode1=" + Barcode1 + ", Barcode2=" + barcode2 + ", Barcode3=" + barcode3 + ", EMPLOYEE_ID=" + EMPLOYEE_ID +
                                      ", STORE_ID=" + STORE_ID + ", connection string[" + BOUouterConnStr + "]," + msg);


                }
                return -1;
            }
            finally
            {
                if (oraCmd != null)
                    oraCmd.Dispose();

                if (objConn != null && objConn.State == ConnectionState.Open)
                    objConn.Close();

                if (objConn != null)
                    objConn.Dispose();

                if (BouoraCmd != null)
                    BouoraCmd.Dispose();

                if (BouConn != null && BouConn.State == ConnectionState.Open)
                    BouConn.Close();

                if (BouConn != null)
                    BouConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string get_sale_remark(string posuuid_master)
        {
            string result = "";

            string sqlstr = string.Format("select CREDIT_CARD_NO from paid_detail where CREDIT_CARD_NO is not null and posuuid_master = '{0}'", posuuid_master);
            DataTable dt = OracleDBUtil.Query_Data(sqlstr);
            foreach (DataRow row in dt.Rows)
            {
                string CARD_NO = StringUtil.CStr(row[0]);
                CARD_NO = CARD_NO.Substring(CARD_NO.Length - 4).PadLeft(CARD_NO.Length - 4, '*');
                result +=CARD_NO  +",";
            }
            result.TrimEnd(',');
            return result;
        }

        public static DataTable get_discount(string prodno, string store_no, string total_amount,int quantity,string posuuid_detail)
        {
            Discount_Facade.Discount_Conditions conditions = new Discount_Facade.Discount_Conditions();
            conditions.prodno = prodno;
            conditions.store_id = store_no;
            conditions.total_amount = total_amount;
            conditions.posuuid_detail = posuuid_detail;
            DataTable dt = Discount_Facade.get_Discount(conditions,quantity);


            return dt;
        }

        public static string get_sale_type_by_sale_head(string posuuid_master)
        {
            string sale_type = "1";
            OracleConnection conn = null;
            string sqlstr = string.Format("select sale_type from sale_head where  posuuid_master =  '{0}'", posuuid_master);
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    sale_type =StringUtil.CStr(dr[0]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }
            return sale_type;
        }

        public static string get_sale_type(string posuuid_detail)
        {
            posuuid_detail = posuuid_detail.TrimEnd(';').Replace(";", "','");
         
            string sale_type = "1";
            OracleConnection conn = null;
            string sqlstr = string.Format("select * from to_close_head where service_type = 3 and posuuid_detail in  ('{0}') ",posuuid_detail);
            try
            {
                conn = OracleDBUtil.GetConnection();
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    sale_type = "2";
                }
                dr.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                OracleConnection.ClearPool(conn);
            }
            return sale_type;
        }

        public static bool IsInventory(string posuuid_master)
        {
            bool result = false;
            OracleConnection conn = null;
            try
            {
                conn = OracleDBUtil.GetConnection();
                string sqlstr = "select invoice_no from invoice_head where posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master) ;
                OracleCommand cmd = new OracleCommand(sqlstr, conn);
                OracleDataReader dr = cmd.ExecuteReader();
                result = dr.HasRows;
                dr.Close();
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

        public static void UpdateMYNO(string posuuid_master)
        {
            OracleConnection conn = null;

            OracleTransaction trans = null;
            try
            {
                conn = OracleDBUtil.GetConnection();
                trans = conn.BeginTransaction();
                UpdateMYNO(posuuid_master, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        public static void UpdateMYNO(string posuuid_master, OracleTransaction trans)
        {
            string sqlstr = "";
            OracleCommand cmd = null;
            OracleDataAdapter da = null;
            DataTable dt = null;
            //更新LINE_SEQ
            sqlstr = string.Format("select id from sale_detail where posuuid_master = {0} order by item_type,seqno", OracleDBUtil.SqlStr(posuuid_master));
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            da = new OracleDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            sqlstr = "update SALE_DETAIL set LINE_SEQ = :LINE_SEQ where ID=:ID";
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            cmd.Parameters.Add(":ID", OracleType.NVarChar);
            cmd.Parameters.Add(":LINE_SEQ", OracleType.Number);
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                cmd.Parameters[":ID"].Value = StringUtil.CStr(row[0]);
                cmd.Parameters[":LINE_SEQ"].Value = i++;
                cmd.ExecuteNonQuery();
            }

            //產生MYNO
            sqlstr = "update SALE_DETAIL sd2 set MYNO= (select  'R' || STORE_NO || '_' || TO_CHAR(TRADE_DATE,'YYYYMMDD-hh24miss') || '_' || MACHINE_ID || '_' || SUBSTR(SALE_NO,-8,8) || '_' || LPAD(sd.LINE_SEQ,3,'0') from sale_head sh join sale_detail sd on sh.posuuid_master = SD.posuuid_master where sd2.id = sd.id) where  posuuid_master =" + OracleDBUtil.SqlStr(posuuid_master);
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            cmd.ExecuteNonQuery();
        }
    }
      
}
