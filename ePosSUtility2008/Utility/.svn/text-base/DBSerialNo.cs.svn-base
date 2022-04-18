namespace Advtek.Utility
{ 
    using System.Data.OracleClient;
    //using Advtek.Utility;
    using Advtek.Utility.UserException;
	using log4net;
	using System;
	using System.Collections;
	using System.Data;
   	using System.Runtime.CompilerServices;
	using System.Text;
	using System.Configuration;
   

	public sealed class SerialNo
	{
     
       
        private static readonly string CONNECT_STRING = ConfigurationManager.AppSettings["Connection_String"];//ConfigurationSettings.AppSettings["Connection_String"];
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SerialNo));
        private static SerialNo singleton;

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static string GenNo(OracleConnection  conn, string PA_Sheet_Type, ArrayList aryKey, ArrayList arySegmentValue)
		{
			StringBuilder strSql = null;
			string strResult = "";
			string strSegment = "";
			Hashtable htlSegment = null;
			string strSegType = "";
			string strFixString = "";
			string strSegLength = "";
			string strResetType = "";
			string strPadChar = "";
			string strSerialNo = "";
			string strScript = "";
			try
			{
				strSql = new StringBuilder();
				strSql.Append("Select * From SeqSegment ");
				strSql.Append(" Where SheetType=" + OracleDBUtil.SqlStr(PA_Sheet_Type));
				strSql.Append(" Order By SeqNo ");
				htlSegment = OracleDBUtil.GetData(conn, strSql.ToString());
				if (htlSegment.Count > 0)
				{
					for (int i = 0; i < htlSegment.Count; i++)
					{
						Hashtable htlSegment_Row = (Hashtable) htlSegment[i];
						strSegType = StringUtil.CStr(htlSegment_Row["SEGTYPE"]).ToUpper();
						if (strSegType.Equals("FIX"))
						{
							strFixString = StringUtil.CStr(htlSegment_Row["SEGFIXSTRING"]);
							Logger.Info("取得固定文字:" + strFixString);
							strSegment = strFixString;
						}
						else if (strSegType.ToUpper().StartsWith("USER"))
						{
							if (arySegmentValue == null)
							{
								Logger.Info("將SegType設為User, 但傳入的參數是null");
								throw new SegmentValueNullPointerException();
							}
							strSegment = GetUserValue(strSegType, arySegmentValue);
						}
						else if (strSegType.Equals("YYYY"))
						{
							strSegment = StringUtil.Pad(DateUtil.Year(DateUtil.Now()), "0", 4, "L");
						}
						else if (strSegType.Equals("YY"))
						{
							strSegment = StringUtil.Pad(DateUtil.Year(DateUtil.Now()).Substring(2, 2), "0", 2, "L");
						}
						else if (strSegType.Equals("CYY"))
						{
							int intYear = StringUtil.CInt(DateUtil.Year(DateUtil.Now())) - 0x777;
							strSegment = StringUtil.Pad(StringUtil.CStr(intYear), "0", 3, "L");
						}
						else if (strSegType.Equals("MM"))
						{
							strSegment = StringUtil.Pad(DateUtil.Month(DateUtil.Now()), "0", 2, "L");
						}
						else if (strSegType.Equals("DD"))
						{
							strSegment = StringUtil.Pad(DateUtil.Day(DateUtil.Now()), "0", 2, "L");
						}
						else if (strSegType.Equals("HH"))
						{
							strSegment = StringUtil.Pad(DateUtil.Hour(DateUtil.Now()), "0", 2, "L");
						}
						else if (strSegType.Equals("NN"))
						{
							strSegment = StringUtil.Pad(DateUtil.Minute(DateUtil.Now()), "0", 2, "L");
						}
						else if (strSegType.Equals("SS"))
						{
							strSegment = StringUtil.Pad(DateUtil.Second(DateUtil.Now()), "0", 2, "L");
						}
						else if (strSegType.Equals("NUMBER"))
						{
							strSegLength = StringUtil.CStr(htlSegment_Row["SEGLENGTH"]);
							strResetType = StringUtil.CStr(htlSegment_Row["RESETTYPE"]);
							strPadChar = StringUtil.CStr(htlSegment_Row["PADCHAR"]);
							if (strSegLength.ToUpper().StartsWith("USER"))
							{
								strSegLength = GetUserValue(strSegLength, arySegmentValue);
								Logger.Info("使用者自訂流水號長度:" + strSegLength);
							}
							strSerialNo = GetSerialNo(conn, PA_Sheet_Type, aryKey, arySegmentValue, strResetType, htlSegment_Row);
							Logger.Info("取得的序號=" + strSerialNo);
							strSegment = StringUtil.Pad(strSerialNo, strPadChar, StringUtil.CInt(strSegLength), "L");
						}
						else
						{
							Logger.Info("指定的區段類型錯誤, 請檢查SeqSegment Table的內容");
							throw new SegmentTypeDefineException();
						}
						strScript = StringUtil.CStr(htlSegment_Row["POSTSCRIPT"]);
						if (!strScript.Equals(""))
						{
							//strSegment = CodeUtil.Eval(strScript.Replace("#VAL#", strSegment));
						}
						strResult = strResult + strSegment;
					}
					return strResult;
				}
				Logger.Info("指定的單據類型在SeqSegment Table裡找不到任何資料");
				throw new SheetTypeNotFoundException();
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strResult;
		}

		private static string GetCriteria(ArrayList aryKey)
		{
			StringBuilder strSql = null;
			string strTemp = "";
			try
			{
				strSql = new StringBuilder();
				for (int i = 0; i < aryKey.Count; i++)
				{
					strTemp = StringUtil.CStr(aryKey[i]);
					if (strTemp.Equals("null"))
					{
						strTemp = " is null";
					}
					else
					{
						strTemp = "=" + OracleDBUtil.SqlStr(strTemp);
					}
					strSql.Append(" And AddField" + StringUtil.CStr(i + 1) + strTemp);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strSql.ToString();
		}

		private static string GetEndValue(Hashtable PA_Segment, ArrayList arySegmentValue)
		{
			string strEndValue = "";
			try
			{
				strEndValue = StringUtil.CStr(PA_Segment["ENDVALUE"]);
				if (strEndValue.ToUpper().StartsWith("USER"))
				{
					strEndValue = GetUserValue(strEndValue, arySegmentValue);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strEndValue;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
        public static SerialNo GetInstance()
		{
			if (singleton == null)
			{
                singleton = new SerialNo();
			}
			Logger.Info("取得SerualNo的Instance");
			return singleton;
		}

		private static string GetSerialNo(OracleConnection conn, string PA_Sheet_Type, ArrayList aryKey, ArrayList arySegmentValue, string PA_ResetType, Hashtable PA_Segment)
		{
			string strResult = "";
			OracleTransaction tx = null;
			int intCriteriaCount = 5;
			StringBuilder strSql = null;
			Hashtable htlSeqNo = null;
			string strUpdateDate = "";
			string strCurrTime = "";
			string strResetType = PA_ResetType.ToUpper();
			bool blnDiff = false;
			string strCriteria = "";
			string strStartValue = "";
			string strEndValue = "";
			string strNextValue = "";
			try
			{
				tx = conn.BeginTransaction(IsolationLevel.Serializable);
				strEndValue = GetEndValue(PA_Segment, arySegmentValue);
				strSql = new StringBuilder();
				strSql.Append("Select * From SeqNo ");
				strSql.Append(" Where SheetType=" + OracleDBUtil.SqlStr(PA_Sheet_Type));
				for (int i = aryKey.Count; i < intCriteriaCount; i++)
				{
					aryKey.Add("null");
				}
				strCriteria = GetCriteria(aryKey);
				strSql.Append(strCriteria);
				Logger.Info("判斷是否已有記錄=" + strSql.ToString());
				htlSeqNo = OracleDBUtil.GetData(tx, strSql.ToString());
				if (htlSeqNo.Count > 0)
				{
					Logger.Info("原本有記錄");
					Hashtable htlSeqNo_Row = (Hashtable) htlSeqNo[0];
					strUpdateDate = DateUtil.DtStr(StringUtil.CStr(htlSeqNo_Row["UPDATEDATE"]), "YMDHNS");
					strCurrTime = DateUtil.Now();
					if (strResetType.Equals("BYDAY"))
					{
						string strDATE1 = DateUtil.Year(strUpdateDate) + "/" + DateUtil.Month(strUpdateDate) + "/" + DateUtil.Day(strUpdateDate);
						string strDATE2 = DateUtil.Year(strCurrTime) + "/" + DateUtil.Month(strCurrTime) + "/" + DateUtil.Day(strCurrTime);
						Logger.Info("ResetType=" + strResetType + " strDATE1=" + strDATE1 + " strDATE2=" + strDATE2);
						if (!strDATE1.Equals(strDATE2))
						{
							blnDiff = true;
						}
					}
					else if (strResetType.Equals("BYMONTH"))
					{
						string strDATE1 = DateUtil.Year(strUpdateDate) + "/" + DateUtil.Month(strUpdateDate);
						string strDATE2 = DateUtil.Year(strCurrTime) + "/" + DateUtil.Month(strCurrTime);
						Logger.Info("ResetType=" + strResetType + " strDATE1=" + strDATE1 + " strDATE2=" + strDATE2);
						if (!strDATE1.Equals(strDATE2))
						{
							blnDiff = true;
						}
					}
					else if (strResetType.Equals("BYYEAR"))
					{
						string strDATE1 = DateUtil.Year(strUpdateDate);
						string strDATE2 = DateUtil.Year(strCurrTime);
						Logger.Info("ResetType=" + strResetType + " strDATE1=" + strDATE1 + " strDATE2=" + strDATE2);
						if (!strDATE1.Equals(strDATE2))
						{
							blnDiff = true;
						}
					}
					else if (strResetType.Equals("LOOP"))
					{
						Logger.Info("ResetType=" + strResetType);
						blnDiff = false;
					}
					else
					{
						Logger.Info("SeqNo指定的ResetType錯誤, 請檢查SeqNo Table的內容");
						throw new ResetTypeDefineException();
					}
					strSql = new StringBuilder();
					strSql.Append("Update SeqNo Set ");
					if (blnDiff)
					{
						Logger.Info("要Reset序號");
						strStartValue = GetStartValue(PA_Segment, arySegmentValue);
						strNextValue = StringUtil.CStr(StringUtil.CInt(strStartValue) + 1);
						strResult = strStartValue;
						strSql.Append(" SerialNo = " + strNextValue + ", ");
					}
					else
					{
						Logger.Info("不用Reset序號");
						strResult = StringUtil.CStr(htlSeqNo_Row["SERIALNO"]);
						strSql.Append(" SerialNo = SerialNo + 1, ");
					}
					strSql.Append(" UpdateDate=" + OracleDBUtil.SqlStr(DateUtil.Now()));
					strSql.Append(" Where SheetType=" + OracleDBUtil.SqlStr(PA_Sheet_Type));
					strSql.Append(strCriteria);
					if (!strEndValue.Equals("") && (StringUtil.CInt(strResult) > StringUtil.CInt(strEndValue)))
					{
						Logger.Info("取得的序號已超過最大值");
						throw new SeqNoOutOfBoundException();
					}
					Logger.Info("sql=" + strSql.ToString());
					OracleHelper.ExecuteNonQuery(tx, CommandType.Text, strSql.ToString());
				}
				else
				{
					Logger.Info("原本沒記錄");
					strSql = new StringBuilder();
					strStartValue = GetStartValue(PA_Segment, arySegmentValue);
					strNextValue = StringUtil.CStr(StringUtil.CInt(strStartValue) + 1);
					strSql.Append("Insert Into SeqNo(SheetType,SerialNo,UpdateDate, AddField1, AddField2, AddField3, AddField4, AddField5) Values(");
					strSql.Append(OracleDBUtil.SqlStr(PA_Sheet_Type) + ", ");
					strSql.Append(strNextValue + ", ");
					strSql.Append(OracleDBUtil.SqlStr(DateUtil.Now()) + ", ");
					for (int i = 0; i < intCriteriaCount; i++)
					{
						if (StringUtil.CStr(aryKey[i]).Equals("null"))
						{
							strSql.Append("null");
						}
						else
						{
							strSql.Append(OracleDBUtil.SqlStr(StringUtil.CStr(aryKey[i])));
						}
						if (i < (intCriteriaCount - 1))
						{
							strSql.Append(", ");
						}
						else
						{
							strSql.Append(")");
						}
					}
					Logger.Info("sql=" + strSql.ToString());
					OracleHelper.ExecuteNonQuery(tx, CommandType.Text, strSql.ToString());
					strResult = strStartValue;
				}
				tx.Commit();
			}
			catch (Exception ex)
			{
				try
				{
					tx.Rollback();
				}
				catch (Exception sqlEx)
				{
					Logger.Error(sqlEx);
					throw sqlEx;
				}
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			finally
			{
				tx = null;
			}
			return strResult;
		}

		private static string GetStartValue(Hashtable PA_Segment, ArrayList arySegmentValue)
		{
			string strStartValue = "";
			try
			{
				strStartValue = StringUtil.CStr(PA_Segment["STARTVALUE"]);
				if (strStartValue.Equals(""))
				{
					return "1";
				}
				if (strStartValue.ToUpper().StartsWith("USER"))
				{
					strStartValue = GetUserValue(strStartValue, arySegmentValue);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strStartValue;
		}

		private static string GetUserValue(string strValue, ArrayList arySegmentValue)
		{
			string strUserValue = "";
			string[] aryValue = StringUtil.VBSplit(strValue, ":");
			int intUserIndex = 0;
			try
			{
				Logger.Info("傳入的設定值:" + strValue);
				intUserIndex = StringUtil.CInt(aryValue[1].Trim());
				Logger.Info("intUserIndex=" + intUserIndex.ToString());
				if (intUserIndex < arySegmentValue.Count)
				{
					strUserValue = StringUtil.CStr(arySegmentValue[intUserIndex]);
					Logger.Info("取得使用者自訂文字:" + strUserValue);
					return strUserValue;
				}
				Logger.Info("SegUserIndex欄位所指定的值超過您所提供的ArrayList的個數");
				throw new SegmentTypeDefineException();
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strUserValue;
		}
	}
}

    
