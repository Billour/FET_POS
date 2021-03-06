namespace Advtek.Utility
{
	using log4net;
	using Microsoft.VisualBasic;
	using System;
	using System.Collections;
	using System.Globalization;
	using System.Text;

	public sealed class StringUtil
	{
		private static readonly ILog Logger = LogManager.GetLogger(typeof(StringUtil));

		public static bool AreEqual(string Left, string Right)
		{
			return (string.Compare(Left, Right, true, CultureInfo.InvariantCulture) == 0);
		}

		public static int Asc(string character)
		{
			if (character == null)
			{
				Logger.Info("character是null");
				throw new ApplicationException("Character is not valid.");
			}
			if (character.Equals(""))
			{
				Logger.Info("character是空字串");
				throw new ApplicationException("Character is not valid.");
			}
			if (character.Length == 1)
			{
				ASCIIEncoding asciiEncoding = new ASCIIEncoding();
				return asciiEncoding.GetBytes(character)[0];
			}
			Logger.Info("character長度不等於1 : " + character);
			throw new ApplicationException("Character is not valid.");
        }

        #region 計算BYTE長度
        private int ByteLen(string str)
		{
			byte[] b = new ASCIIEncoding().GetBytes(str);
			int l = 0;
			for (int i = 0; i <= (b.Length - 1); i++)
			{
				if (b[i] == 0x3f)
				{
					l++;
				}
				l++;
			}
			return l;
        }

        #endregion

        #region 字串轉DOUBLE
        public static double CDbl(object obj)
		{
			double dblResult = 0.0;
			try
			{
				if (obj == null)
				{
					return 0.0;
				}
				if (obj.ToString().Equals(""))
				{
					return 0.0;
				}
				dblResult = Convert.ToDouble(obj);
			}
			catch (Exception ex)
			{
				Logger.Info("發生錯誤的內容為:" + CStr(obj));
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return dblResult;
        }

        #endregion

        #region 取小數點之後的數值
        public static string Ceiling(object obj, int intDecimals)
		{
			int intPointPos = 0;
			string strInteger = "";
			string strDecimal = "";
			string strDigit = "";
			double dblIncrese = 1.0;
			double dblValue = 0.0;
			string strValue = "";
			try
			{
				for (int i = 0; i < intDecimals; i++)
				{
					dblIncrese *= 0.1;
				}
				strValue = CStr(CDbl(obj));
				Logger.Info("要轉換的數字=" + strValue);
				intPointPos = strValue.IndexOf(".");
				if (intPointPos == -1)
				{
					Logger.Info("沒有小數點, 表示傳入的是整數");
					strDecimal = Pad("", "0", intDecimals, "R");
					if (!strDecimal.Equals(""))
					{
						strValue = strValue + "." + strDecimal;
					}
					return strValue;
				}
				Logger.Info("有小數點, 表示傳入的是浮點數");
				strInteger = strValue.Substring(0, intPointPos);
				Logger.Info("整數部份:" + strInteger);
				strDecimal = Pad(strValue.Substring(intPointPos + 1), "0", intDecimals + 1, "R");
				strDigit = strDecimal.Substring(intDecimals, 1);
				Logger.Info("要判斷的數字:" + strDigit);
				if (CInt(strDigit) > 4)
				{
					Logger.Info("要進位");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("小數點後的數目:" + strDecimal);
					if (strDecimal.Equals(""))
					{
						dblValue = CDbl(strInteger) + dblIncrese;
					}
					else
					{
						dblValue = (CDbl(strInteger) + CDbl("." + strDecimal)) + dblIncrese;
					}
				}
				else
				{
					Logger.Info("不用進位");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("小數點後的數目:" + strDecimal);
					if (strDecimal.Equals(""))
					{
						dblValue = CDbl(strInteger);
					}
					else
					{
						dblValue = CDbl(strInteger + "." + strDecimal);
					}
				}
				strValue = CStr(dblValue);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strValue;
        }

        #endregion

        #region ascii 轉char
        public static string Chr(int asciiCode)
		{
			string CS0;
			try
			{
				if ((asciiCode >= 0) && (asciiCode <= 0xff))
				{
					ASCIIEncoding asciiEncoding = new ASCIIEncoding();
					byte[] byteArray = new byte[] { (byte) asciiCode };
					CS0 = asciiEncoding.GetString(byteArray);
				}
				else
				{
					Logger.Info("ASCII Code is not valid : " + asciiCode.ToString());
					throw new ApplicationException("ASCII Code is not valid.");
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return CS0;
        }
        #endregion

        #region 字串轉整數
        public static int CInt(object obj)
		{
			int intResult = 0;
			try
			{
				if (obj == null)
				{
					return 0;
				}
				if (obj.ToString().Equals(""))
				{
					return 0;
				}
				intResult = Convert.ToInt32(obj);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return intResult;
        }
        #endregion

        #region 物件型別轉字串
        public static string CStr(object obj)
		{
			string strResult = "";
			try
			{
				if (obj == null)
				{
					return "";
				}
				strResult = obj.ToString();
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strResult;
        }
        #endregion

		public static string DupStr(string strOriStr, int intDupTimes)
		{
			StringBuilder strResult = null;
			try
			{
				strResult = new StringBuilder();
				for (int i = 0; i < intDupTimes; i++)
				{
					strResult.Append(strOriStr);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strResult.ToString();
		}

		public static string Floor(object obj, int intDecimals)
		{
			int intPointPos = 0;
			string strInteger = "";
			string strDecimal = "";
			string strDigit = "";
			double dblIncrese = 1.0;
			double dblValue = 0.0;
			string strValue = "";
			try
			{
				for (int i = 0; i < intDecimals; i++)
				{
					dblIncrese *= 0.1;
				}
				strValue = CStr(CDbl(obj));
				Logger.Info("要轉換的數字=" + strValue);
				intPointPos = strValue.IndexOf(".");
				if (intPointPos == -1)
				{
					Logger.Info("沒有小數點, 表示傳入的是整數");
					strDecimal = Pad("", "0", intDecimals, "R");
					if (!strDecimal.Equals(""))
					{
						strValue = strValue + "." + strDecimal;
					}
					return strValue;
				}
				Logger.Info("有小數點, 表示傳入的是浮點數");
				strInteger = strValue.Substring(0, intPointPos);
				Logger.Info("整數部份:" + strInteger);
				strDecimal = Pad(strValue.Substring(intPointPos + 1), "0", intDecimals + 1, "R");
				strDigit = strDecimal.Substring(intDecimals, 1);
				Logger.Info("要判斷的數字:" + strDigit);
				if (CInt(strDigit) > 4)
				{
					Logger.Info("要進位");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("小數點後的數目:" + strDecimal);
					if (strDecimal.Equals(""))
					{
						dblValue = CDbl(strInteger) + dblIncrese;
					}
					else
					{
						dblValue = (CDbl(strInteger) + CDbl("." + strDecimal)) + dblIncrese;
					}
				}
				else
				{
					Logger.Info("不用進位");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("小數點後的數目:" + strDecimal);
					if (strDecimal.Equals(""))
					{
						dblValue = CDbl(strInteger);
					}
					else
					{
						dblValue = CDbl(strInteger + "." + strDecimal);
					}
				}
				strValue = CStr(dblValue);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strValue;
		}

		public static string GetLastElement(string PA_Value, string PA_Splitter)
		{
			string[] aryTemp = null;
			string strValue = "";
			string strSplitter = "";
			string strResult = "";
			try
			{
				if (PA_Value == null)
				{
					strValue = "";
				}
				else if (PA_Value.Equals(""))
				{
					strValue = "";
				}
				else
				{
					strValue = PA_Value;
				}
				if (PA_Splitter == null)
				{
					strSplitter = ",";
				}
				else if (PA_Splitter.Equals(""))
				{
					strSplitter = ",";
				}
				else
				{
					strSplitter = PA_Splitter;
				}
				aryTemp = VBSplit(strValue, strSplitter);
				strResult = aryTemp[aryTemp.Length - 1];
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strResult;
		}

		public static string Merge(ArrayList PA_Array)
		{
			string strResult = "";
			try
			{
				strResult = Merge(PA_Array, ",");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strResult;
		}

		public static string Merge(string[] PA_Array)
		{
			string strResult = "";
			try
			{
				strResult = Merge(PA_Array, ",");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strResult;
		}

        //public static string Merge(Hashtable PA_Data, string PA_Key)
        //{
        //    string strResult = "";
        //    try
        //    {
        //        strResult = Merge(PA_Data, PA_Key, ",");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //    return strResult;
        //}

		public static string Merge(string[] PA_Array, string PA_Splitter)
		{
			StringBuilder strResult = null;
			try
			{
				strResult = new StringBuilder();
				for (int i = 0; i < PA_Array.Length; i++)
				{
					strResult.Append(PA_Array[i]);
					if (i < (PA_Array.Length - 1))
					{
						strResult.Append(PA_Splitter);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strResult.ToString();
		}

		public static string Merge(ArrayList PA_Array, string PA_Splitter)
		{
			StringBuilder strResult = null;
			try
			{
				strResult = new StringBuilder();
				for (int i = 0; i < PA_Array.Count; i++)
				{
					strResult.Append(CStr(PA_Array[i]));
					if (i < (PA_Array.Count - 1))
					{
						strResult.Append(PA_Splitter);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strResult.ToString();
		}

        public static string Merge(string[,] PA_Array, int ColIndex)
        {
            string strResult = "";
            try
            {
                strResult = Merge(PA_Array, ColIndex, ",");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
            return strResult;
        }

        public static string Merge(string[,] PA_Array, int ColIndex, string PA_Splitter)
        {
            StringBuilder strResult = null;
            try
            {
                strResult = new StringBuilder();
                for (int i = 0; i < PA_Array.GetLength(0); i++)
                {
                    strResult.Append(PA_Array[i, ColIndex]);
                    if (i < (PA_Array.GetLength(0) - 1))
                    {
                        strResult.Append(PA_Splitter);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strResult.ToString();
        }

		public static string Merge(Hashtable PA_Data, string PA_Key, string PA_Splitter)
		{
			StringBuilder strResult = null;
			Hashtable htlData_Row = null;
			try
			{
				strResult = new StringBuilder();
				for (int i = 0; i < PA_Data.Count; i++)
				{
					htlData_Row = (Hashtable) PA_Data[i];
					strResult.Append(CStr(htlData_Row[PA_Key]));
					if (i < (PA_Data.Count - 1))
					{
						strResult.Append(PA_Splitter);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			finally
			{
				htlData_Row = null;
			}
			return strResult.ToString();
		}

		public static string Mid(string param, int startIndex)
		{
			return param.Substring(startIndex);
		}

		public static string Mid(string param, int startIndex, int length)
		{
			string result = "";
			try
			{
				if (length <= (param.Length - startIndex))
				{
					return param.Substring(startIndex, length);
				}
				result = param.Substring(startIndex);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return result;
		}

		public static string NumberFormat(double PA_Number)
		{
			return NumberFormat(PA_Number, 2, false);
		}

		public static string NumberFormat(string PA_Number)
		{
			return NumberFormat(CDbl(PA_Number), 2, false);
		}

		public static string NumberFormat(double PA_Number, int PA_Digit)
		{
			NumberFormatInfo nfi = new NumberFormatInfo();
			nfi.NumberDecimalDigits = PA_Digit;
			return PA_Number.ToString("N", nfi);
		}

		public static string NumberFormat(string PA_Number, int PA_Digit)
		{
			return NumberFormat(CDbl(PA_Number), PA_Digit, false);
		}

		public static string NumberFormat(double PA_Number, int PA_Digit, bool PA_Comma)
		{
			NumberFormatInfo nfi = new NumberFormatInfo();
			nfi.NumberDecimalDigits = PA_Digit;
			if (PA_Comma)
			{
				nfi.NumberGroupSeparator = ",";
			}
			return PA_Number.ToString("N", nfi);
		}

		public static string Pad(string PA_Ori_Value, string PA_Pad_Value, int PA_Len, string PA_Dir)
		{
			//string strResult = "";
			string strPadString = "";
			if (PA_Ori_Value == null)
			{
				//strResult = "";
			}
			else if (PA_Ori_Value.Equals(""))
			{
				//strResult = "";
			}
			if (PA_Pad_Value == null)
			{
				return PA_Ori_Value;
			}
			if (PA_Pad_Value.Equals(""))
			{
				return PA_Ori_Value;
			}
			if (PA_Ori_Value.Length >= PA_Len)
			{
				return PA_Ori_Value;
			}
			for (int i = PA_Ori_Value.Length; i < PA_Len; i++)
			{
				strPadString = strPadString + PA_Pad_Value;
			}
			if (CStr(PA_Dir).ToUpper().Equals("R"))
			{
				return (PA_Ori_Value + strPadString);
			}
			return (strPadString + PA_Ori_Value);
		}

        //public static string Right(string param, int length)
        //{
        //    return param.Substring(param.Length - length, length);
        //}

        #region 字串轉成數值並四捨五入到指的小數點位數
        public static string Round(object obj, int intDecimals)
		{
			int intPointPos = 0;
			string strInteger = "";
			string strDecimal = "";
			string strDigit = "";
			double dblIncrese = 1.0;
			double dblValue = 0.0;
			string strValue = "";
			try
			{
				for (int i = 0; i < intDecimals; i++)
				{
					dblIncrese *= 0.1;
				}
				strValue = CStr(CDbl(obj));
				Logger.Info("要轉換的數字=" + strValue);
				intPointPos = strValue.IndexOf(".");
				if (intPointPos == -1)
				{
					Logger.Info("沒有小數點, 表示傳入的是整數");
					strDecimal = Pad("", "0", intDecimals, "R");
					if (!strDecimal.Equals(""))
					{
						strValue = strValue + "." + strDecimal;
					}
					return strValue;
				}
				Logger.Info("有小數點, 表示傳入的是浮點數");
				strInteger = strValue.Substring(0, intPointPos);
				Logger.Info("整數部份:" + strInteger);
				strDecimal = strValue.Substring(intPointPos + 1);
				Logger.Info("小數部份:" + strDecimal);
				strDecimal = Pad(strDecimal, "0", intDecimals + 1, "R");
				Logger.Info("小數部份補零之後:" + strDecimal);
				strDigit = strDecimal.Substring(intDecimals, 1);
				Logger.Info("要判斷的數字:" + strDigit);
				if (CInt(strDigit) > 4)
				{
					Logger.Info("要進位");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("小數點後的數目:" + strDecimal);
					if (strDecimal.Equals(""))
					{
						dblValue = CDbl(strInteger) + dblIncrese;
					}
					else
					{
						dblValue = (CDbl(strInteger) + CDbl("." + strDecimal)) + dblIncrese;
					}
				}
				else
				{
					Logger.Info("不用進位");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("小數點後的數目:" + strDecimal);
					if (strDecimal.Equals(""))
					{
						dblValue = CDbl(strInteger);
					}
					else
					{
						dblValue = CDbl(strInteger + "." + strDecimal);
					}
				}
				strValue = CStr(dblValue);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return strValue;
        }
        #endregion

        #region Help Function
        public static string VBFormatNumber(string PA_Str, int PA_Digit, bool PA_ShowZero, bool PA_NegParens, bool PA_GroupDigits)
        {
            string strResult = "";
            TriState blnShowZero = TriState.True;
            TriState blnNegParens = TriState.False;
            TriState blnGroupDigits = TriState.False;
            try
            {
                if (PA_Str == null)
                {
                    return "";
                }
                if (PA_Str.Equals(""))
                {
                    return "";
                }
                if (PA_ShowZero)
                {
                    blnShowZero = TriState.True;
                }
                else
                {
                    blnShowZero = TriState.False;
                }
                if (PA_NegParens)
                {
                    blnNegParens = TriState.True;
                }
                else
                {
                    blnNegParens = TriState.False;
                }
                if (PA_GroupDigits)
                {
                    blnGroupDigits = TriState.True;
                }
                else
                {
                    blnGroupDigits = TriState.False;
                }
                strResult = Strings.FormatNumber(PA_Str, PA_Digit, blnShowZero, blnNegParens, blnGroupDigits);
            }
            catch (Exception ex)
            {
                Logger.Info("發生錯誤的資料:" + PA_Str);
                Logger.Error(ex.Message, ex);
                throw ex;
            }
            return strResult;
        }

        public static string[] VBSplit(string PA_Value, string PA_Splitter)
        {
            string[] strResult = null;
            string strValue = "";
            string strSplitter = "";
            try
            {
                if (PA_Value == null)
                {
                    strValue = "";
                }
                else if (PA_Value.Equals(""))
                {
                    strValue = "";
                }
                else
                {
                    strValue = PA_Value;
                }
                if (PA_Splitter == null)
                {
                    strSplitter = ",";
                }
                else if (PA_Splitter.Equals(""))
                {
                    strSplitter = ",";
                }
                else
                {
                    strSplitter = PA_Splitter;
                }
                strResult = Strings.Split(strValue, strSplitter, -1, CompareMethod.Text);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
            return strResult;
        }
        #endregion

        #region IsInteger() 檢查是否為整數值
        public bool IsInteger(string mdata)
        {
            bool mfg = true;

            for (int iCnt = 0; iCnt < mdata.Length; iCnt++)
            {
                if (!char.IsDigit(mdata[iCnt]))
                {
                    mfg = false;
                    iCnt = mdata.Length;
                }
            }

            return mfg;
        }
        #endregion

        #region Left() 擷取左側字元
        public string Left(string mdata)
        {
            return Left(mdata, 1);
        }
        public string Left(string mdata, int leng)
        {
            mdata = mdata.Trim();
            if (mdata.Length < leng)
                leng = mdata.Length;

            return mdata.Trim().Substring(0, leng);
        }
        #endregion

        #region Right() 擷取右側字元
        public string Right(string mdata)
        {
            return Right(mdata, 1);
        }

        public string Right(string mdata, int leng)
        {
            mdata = mdata.Trim();

            if (mdata.Length < leng)
                leng = mdata.Length;

            return mdata.Substring(mdata.Length - leng, leng);
        }
        #endregion

        #region FillRight() 以指定字元填滿右方不足長度的位置
        public string FillRight(string mstr, int ilen)
        {
            return FillRight(mstr, ilen, "0");
        }

        public string FillRight(string mstr, int ilen, string fstr)
        {
            string nstr = "";

            // 只取第一個字元作為填滿字元
            if (fstr.Length > 1)
                fstr = fstr.Substring(0, 1);

            if (mstr.Length >= ilen)
            {
                nstr = mstr;
            }
            else
            {
                nstr = mstr + Duplicate(fstr, ilen - mstr.Length);
            }

            return nstr;
        }
        #endregion

        #region FillLeft() 以指定字元填滿左方不足長度的位置
        public string FillLeft(string mstr, int ilen)
        {
            return FillLeft(mstr, ilen, "0");
        }

        public string FillLeft(string mstr, int ilen, string fstr)
        {
            string nstr = "";

            // 只取第一個字元作為填滿字元
            if (fstr.Length > 1)
                fstr = fstr.Substring(0, 1);

            if (mstr.Length >= ilen)
            {
                nstr = mstr;
            }
            else
            {
                nstr = Duplicate(fstr, ilen - mstr.Length) + mstr;
            }

            return nstr;
        }
        #endregion

        #region Dupliacte() 產生重覆字串
        public string Duplicate(string mstr, int ncnt)
        {
            // 使用 StringBuilder 加速字串重覆產生的速度
            StringBuilder dstr = new StringBuilder();
            int icnt = 0;

            for (icnt = 0; icnt < ncnt; icnt++)
            {
                dstr.Append(mstr);
            }

            return dstr.ToString();
        }
        #endregion

        #region ChNumber() 個位數字轉中文數字 (零、一、二....)
        public string ChNumber(int NInt)
        {
            return ChNumber(NInt.ToString());
        }

        public string ChNumber(string NStr)
        {
            // 只取右方一個字元
            NStr = Left(NStr, 1);

            switch (NStr)
            {
                case "0":
                    NStr = "零";
                    break;

                case "1":
                    NStr = "一";
                    break;

                case "2":
                    NStr = "二";
                    break;

                case "3":
                    NStr = "三";
                    break;

                case "4":
                    NStr = "四";
                    break;

                case "5":
                    NStr = "五";
                    break;

                case "6":
                    NStr = "六";
                    break;

                case "7":
                    NStr = "七";
                    break;

                case "8":
                    NStr = "八";
                    break;

                case "9":
                    NStr = "九";
                    break;

                default:
                    NStr = "？";
                    break;
            }
            return NStr;
        }
        #endregion

        #region ChCapitalNumber() 個位數字轉中文大寫數字 (零、壹、貳...)
        public string ChCapitalNumber(int NInt)
        {
            return ChCapitalNumber(NInt.ToString());
        }

        public string ChCapitalNumber(string NStr)
        {
            NStr = Left(NStr, 1);

            switch (NStr)
            {
                case "0":
                    NStr = "零";
                    break;

                case "1":
                    NStr = "壹";
                    break;

                case "2":
                    NStr = "貳";
                    break;

                case "3":
                    NStr = "參";
                    break;

                case "4":
                    NStr = "肆";
                    break;

                case "5":
                    NStr = "伍";
                    break;

                case "6":
                    NStr = "陸";
                    break;

                case "7":
                    NStr = "柒";
                    break;

                case "8":
                    NStr = "捌";
                    break;

                case "9":
                    NStr = "玖";
                    break;

                default:
                    NStr = "？";
                    break;
            }
            return NStr;
        }
        #endregion

        #region GetFourChNumber() 取得每四位數的中文位數字 (萬、億、兆...)
        public string GetFourChNumber(int iDigit)
        {
            string sDigit = "";

            // 限於整數態的上限，「京」之後的中文數字是用不到
            switch (iDigit)
            {
                case 0:
                    sDigit = "";
                    break;
                case 1:
                    sDigit = "萬";
                    break;
                case 2:
                    sDigit = "億";
                    break;
                case 3:
                    sDigit = "兆";
                    break;
                case 4:
                    sDigit = "京";
                    break;
                case 5:
                    sDigit = "垓";
                    break;
                case 6:
                    sDigit = "秭";
                    break;
                case 7:
                    sDigit = "穰";
                    break;
                case 8:
                    sDigit = "溝";
                    break;
                case 9:
                    sDigit = "澗";
                    break;
                case 10:
                    sDigit = "正";
                    break;
                case 11:
                    sDigit = "載";
                    break;
                case 12:
                    sDigit = "極";
                    break;
                default:
                    sDigit = "∞";
                    break;
            }

            return sDigit;
        }
        #endregion

        #region GetChNumber() 整數轉中文數字 （10050 => 一萬零五十）
        public string GetChNumber(int mInt)
        {
            return GetChNumber(mInt.ToString());
        }

        public string GetChNumber(Int64 mInt)
        {
            return GetChNumber(mInt.ToString());
        }

        public string GetChNumber(UInt64 mInt)
        {
            return GetChNumber(mInt.ToString());
        }

        public string GetChNumber(String NumStr)
        {
            string ChStr = "", ChSubStr = "", tmpStr = "";
            int iCnt = 0, jCnt = 0, kCnt = 0, lCnt = -1, nCnt = 0;

            if (!IsInteger(NumStr))
                NumStr = "0";

            nCnt = NumStr.Length;

            // 中文數字以四位數為一個處理單位(萬、億、兆、京....)
            iCnt = nCnt % 4;
            NumStr = Duplicate("0", 4 - iCnt) + NumStr;
            nCnt = NumStr.Length;

            for (iCnt = 0; iCnt < nCnt; iCnt += 4)
            {
                lCnt++;
                ChSubStr = "";

                for (jCnt = 0; jCnt < 4; jCnt++)
                {
                    kCnt = nCnt - iCnt - jCnt - 1;
                    tmpStr = ChNumber(NumStr.Substring(kCnt, 1));

                    if (tmpStr == "零")
                    {
                        if (Left(ChSubStr, 1) != "零" && Left(ChSubStr, 1) != "")
                            ChSubStr = tmpStr + ChSubStr;
                    }
                    else
                    {
                        switch (jCnt)
                        {
                            case 0:
                                ChSubStr = tmpStr;
                                break;
                            case 1:
                                ChSubStr = tmpStr + "十" + ChSubStr;
                                break;
                            case 2:
                                ChSubStr = tmpStr + "百" + ChSubStr;
                                break;
                            case 3:
                                ChSubStr = tmpStr + "千" + ChSubStr;
                                break;
                            default:
                                ChSubStr = tmpStr + "∞" + ChSubStr;
                                break;
                        }
                    }
                }

                if (ChSubStr == "零" && Left(ChStr, 1) != "" && Left(ChStr, 1) != "零")
                    ChStr = "零" + ChStr;
                else
                {
                    if (ChSubStr != "")
                    {
                        // 取得10000的次方中文數字
                        // 限於整數態的上限，「京」之後的中文數字是用不到
                        ChStr = ChSubStr + GetFourChNumber(lCnt) + ChStr;
                    }
                }
            }

            if (ChStr == "")
                ChStr = "零";
            else if (Left(ChStr, 1) == "零")
                ChStr = ChStr.Substring(1, ChStr.Length - 1);

            return ChStr;
        }
        #endregion

        #region GetChNumberFill() 整數轉中文數字 （10050 => 一萬零千零百五十零）
        public string GetChNumberFill(int mInt)
        {
            return GetChNumberFill(mInt.ToString());
        }

        public string GetChNumberFill(Int64 mInt)
        {
            return GetChNumberFill(mInt.ToString());
        }

        public string GetChNumberFill(UInt64 mInt)
        {
            return GetChNumberFill(mInt.ToString());
        }

        public string GetChNumberFill(string NumStr)
        {
            string ChStr = "", ChSubStr = "", tmpStr = "";
            int iCnt = 0, jCnt = 0, kCnt = 0, lCnt = -1, nCnt = 0;

            if (!IsInteger(NumStr))
                NumStr = "0";
            nCnt = NumStr.Length;

            // 中文數字以四位數為一個處理單位(萬、億、兆、京....)
            iCnt = nCnt % 4;
            NumStr = Duplicate("0", 4 - iCnt) + NumStr;
            nCnt = NumStr.Length;

            for (iCnt = 0; iCnt < nCnt; iCnt += 4)
            {
                lCnt++;
                ChSubStr = "";

                for (jCnt = 0; jCnt < 4; jCnt++)
                {
                    kCnt = nCnt - iCnt - jCnt - 1;
                    tmpStr = ChNumber(NumStr.Substring(kCnt, 1));

                    switch (jCnt)
                    {
                        case 0:
                            ChSubStr = tmpStr;
                            break;
                        case 1:
                            ChSubStr = tmpStr + "十" + ChSubStr;
                            break;
                        case 2:
                            ChSubStr = tmpStr + "百" + ChSubStr;
                            break;
                        case 3:
                            ChSubStr = tmpStr + "千" + ChSubStr;
                            break;
                        default:
                            ChSubStr = tmpStr + ChSubStr;
                            break;
                    }
                }

                if (ChSubStr != "")
                {
                    // 取得10000的次方中文數字
                    // 限於整數態的上限，「京」之後的中文數字是用不到
                    ChStr = ChSubStr + GetFourChNumber(lCnt) + ChStr;
                }
            }

            while (Left(ChStr, 1) == "零" && ChStr.Length > 1)
            {
                if (Left(ChStr, 1) == "零")
                    ChStr = ChStr.Substring(2, ChStr.Length - 2);
            }

            return ChStr;
        }
        #endregion

        #region GetChNumberShort() 整數轉簡略中文數字 (10050 => 一零零五零)
        public string GetChNumberShort(int mInt)
        {
            return GetChNumberShort(mInt.ToString());
        }

        public string GetChNumberShort(Int64 mInt)
        {
            return GetChNumberShort(mInt.ToString());
        }

        public string GetChNumberShort(UInt64 mInt)
        {
            return GetChNumberShort(mInt.ToString());
        }

        public string GetChNumberShort(string NumStr)
        {
            string ChStr = "";
            int iCnt = 0, nCnt = 0;

            if (!IsInteger(NumStr))
                NumStr = "0";
            nCnt = NumStr.Length;

            for (iCnt = 0; iCnt < nCnt; iCnt++)
            {
                ChStr += ChNumber(NumStr[iCnt].ToString());
            }

            return ChStr;
        }
        #endregion

        #region GetChCapitalNumber() 整數轉中文大寫數字（10050 => 壹萬零伍拾）
        public string GetChCapitalNumber(int mInt)
        {
            return GetChCapitalNumber(mInt.ToString());
        }

        public string GetChCapitalNumber(Int64 mInt)
        {
            return GetChCapitalNumber(mInt.ToString());
        }

        public string GetChCapitalNumber(UInt64 mInt)
        {
            return GetChCapitalNumber(mInt.ToString());
        }

        public string GetChCapitalNumber(string NumStr)
        {
            string ChStr = "", ChSubStr = "", tmpStr = "";
            int iCnt = 0, jCnt = 0, kCnt = 0, lCnt = -1, nCnt = 0;

            if (!IsInteger(NumStr))
                NumStr = "0";
            nCnt = NumStr.Length;

            // 中文數字以四位數為一個處理單位(萬、億、兆、京....)
            iCnt = nCnt % 4;
            NumStr = Duplicate("0", 4 - iCnt) + NumStr;
            nCnt = NumStr.Length;

            for (iCnt = 0; iCnt < nCnt; iCnt += 4)
            {
                lCnt++;
                ChSubStr = "";

                for (jCnt = 0; jCnt < 4; jCnt++)
                {
                    kCnt = nCnt - iCnt - jCnt - 1;
                    tmpStr = ChCapitalNumber(NumStr.Substring(kCnt, 1));

                    if (tmpStr == "零")
                    {
                        if (Left(ChSubStr, 1) != "零" && Left(ChSubStr, 1) != "")
                            ChSubStr = tmpStr + ChSubStr;
                    }
                    else
                    {
                        switch (jCnt)
                        {
                            case 0:
                                ChSubStr = tmpStr;
                                break;
                            case 1:
                                ChSubStr = tmpStr + "拾" + ChSubStr;
                                break;
                            case 2:
                                ChSubStr = tmpStr + "佰" + ChSubStr;
                                break;
                            case 3:
                                ChSubStr = tmpStr + "仟" + ChSubStr;
                                break;
                            default:
                                ChSubStr = tmpStr + ChSubStr;
                                break;
                        }
                    }
                }

                if (ChSubStr == "零" && Left(ChStr, 1) != "" && Left(ChStr, 1) != "零")
                    ChStr = "零" + ChStr;
                else
                {
                    if (ChSubStr != "")
                    {
                        // 取得10000的次方中文數字
                        // 限於整數態的上限，「京」之後的中文數字是用不到
                        ChStr = ChSubStr + GetFourChNumber(lCnt) + ChStr;
                    }
                }
            }

            if (ChStr == "")
                ChStr = "零";
            else if (Left(ChStr, 1) == "零")
                ChStr = ChStr.Substring(1, ChStr.Length - 1);

            return ChStr;
        }
        #endregion

        #region GetChCapitalNumberFill() 整數轉中文大寫數字 （10050 => 壹萬零仟零佰伍拾零）
        public string GetChCapitalNumberFill(int mInt)
        {
            return GetChCapitalNumberFill(mInt.ToString());
        }

        public string GetChCapitalNumberFill(Int64 mInt)
        {
            return GetChCapitalNumberFill(mInt.ToString());
        }

        public string GetChCapitalNumberFill(UInt64 mInt)
        {
            return GetChCapitalNumberFill(mInt.ToString());
        }

        public string GetChCapitalNumberFill(string NumStr)
        {
            string ChStr = "", ChSubStr = "", tmpStr = "";
            int iCnt = 0, jCnt = 0, kCnt = 0, lCnt = -1, nCnt = 0;

            if (!IsInteger(NumStr))
                NumStr = "0";
            nCnt = NumStr.Length;

            // 中文數字以四位數為一個處理單位(萬、億、兆、京....)
            iCnt = nCnt % 4;
            NumStr = Duplicate("0", 4 - iCnt) + NumStr;
            nCnt = NumStr.Length;

            for (iCnt = 0; iCnt < nCnt; iCnt += 4)
            {
                lCnt++;
                ChSubStr = "";

                for (jCnt = 0; jCnt < 4; jCnt++)
                {
                    kCnt = nCnt - iCnt - jCnt - 1;
                    tmpStr = ChCapitalNumber(NumStr.Substring(kCnt, 1));

                    switch (jCnt)
                    {
                        case 0:
                            ChSubStr = tmpStr;
                            break;
                        case 1:
                            ChSubStr = tmpStr + "拾" + ChSubStr;
                            break;
                        case 2:
                            ChSubStr = tmpStr + "佰" + ChSubStr;
                            break;
                        case 3:
                            ChSubStr = tmpStr + "仟" + ChSubStr;
                            break;
                        default:
                            ChSubStr = tmpStr + ChSubStr;
                            break;
                    }
                }

                if (ChSubStr != "")
                {
                    // 取得10000的次方中文數字
                    // 限於整數態的上限，「京」之後的中文數字是用不到
                    ChStr = ChSubStr + GetFourChNumber(lCnt) + ChStr;
                }
            }

            while (Left(ChStr, 1) == "零" && ChStr.Length > 1)
            {
                if (Left(ChStr, 1) == "零")
                    ChStr = ChStr.Substring(2, ChStr.Length - 2);
            }

            return ChStr;
        }
        #endregion

        #region GetChCapitalNumberShort() 整數轉簡略中文大寫數字 (10050 => 壹零零伍零)
        public string GetChCapitalNumberShort(int mInt)
        {
            return GetChCapitalNumberShort(mInt.ToString());
        }

        public string GetChCapitalNumberShort(Int64 mInt)
        {
            return GetChCapitalNumberShort(mInt.ToString());
        }

        public string GetChCapitalNumberShort(UInt64 mInt)
        {
            return GetChCapitalNumberShort(mInt.ToString());
        }

        public string GetChCapitalNumberShort(string NumStr)
        {
            string ChStr = "";
            int iCnt = 0, nCnt = 0;

            if (!IsInteger(NumStr))
                NumStr = "0";
            nCnt = NumStr.Length;

            for (iCnt = 0; iCnt < nCnt; iCnt++)
            {
                ChStr += ChCapitalNumber(NumStr[iCnt].ToString());
            }

            return ChStr;
        }
        #endregion

	}
}

