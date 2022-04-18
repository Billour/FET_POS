using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using Advtek.Utility;

namespace FET.POS.Model.Facade.FacadeImpl
{
	public class CreditCard_Facade
	{
		#region Public Static Method : string CheckCardType(string cardNo)
		/// <summary>
		/// 傳入信用卡號, 傳回信用卡別
		/// </summary>
		/// <param name="cardNo">信用卡號</param>
		/// <returns>卡別: VISA / MASTER / AE / JCB; 卡號錯誤時,傳回空字串</returns>
		public static string CheckCardType(string cardNo)
		{
			string result = string.Empty;
			if (cardNo.Length == 16)
			{
				// VISA, MASTER, JCB
				if (Convert.ToInt32(cardNo.Substring(0, 1)) == 4)
					result = "VISA";
				else if (Convert.ToInt32(cardNo.Substring(0, 2)) >= 51 && Convert.ToInt32(cardNo.Substring(0, 2)) <= 55)
					result = "MASTER";
				else if (Convert.ToInt32(cardNo.Substring(0, 3)) >= 300 && Convert.ToInt32(cardNo.Substring(0, 3)) <= 399)
					result = "JCB";
			}
			else if (cardNo.Length == 15)
			{
				// American Express(AE), JCB
				if (Convert.ToInt32(cardNo.Substring(0, 3)) >= 340 && Convert.ToInt32(cardNo.Substring(0, 3)) <= 379)
					result = "AE";
				else if (Convert.ToInt32(cardNo.Substring(0, 4)) == 1800 || Convert.ToInt32(cardNo.Substring(0, 4)) == 2131)
					result = "JCB";
			}
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
                  'CreditCard_Facade', 'CheckCardType', SYSDATE, ' CardNo:" + cardNo + " CardType:" + result + "')");
                trans = conn.BeginTransaction();
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
			return result;
		}
		#endregion

		#region Public Static Method :  bool CheckCreditCardNo(string cardNo)
		/// <summary>
		/// 檢查信用卡號是否合法
		/// </summary>
		/// <param name="cardNo">信用卡號</param>
		/// <returns>True: 合法, False: 非法卡號</returns>
		public static bool CheckCreditCardNo(string cardNo)
		{
			// 16 碼的加權數字為 : Math.Abs(index % 2 - 2)
			// 15 碼的加權數字為 : Math.Abs((index + 1) % 2 - 2)
			if (cardNo.Length != 15 && cardNo.Length != 16)
				return false;
			int total = 0;
			for (int i = 0; i < cardNo.Length - 1; i++)
			{
				int v = 0;
				if (cardNo.Length == 16)
					v = Math.Abs(i % 2 - 2) * Convert.ToInt32(cardNo.Substring(i, 1));
				else
					v = Math.Abs((i + 1) % 2 - 2) * Convert.ToInt32(cardNo.Substring(i, 1));
				if (v >= 10)
					v = v / 10 + v % 10;
				total += v;
			}
			return ((Convert.ToInt32(cardNo.Substring(cardNo.Length - 1)) + total) % 10 == 0);
		}
		#endregion
	}
}
