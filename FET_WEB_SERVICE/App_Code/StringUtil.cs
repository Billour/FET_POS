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
				Logger.Info("character琌null");
				throw new ApplicationException("Character is not valid.");
			}
			if (character.Equals(""))
			{
				Logger.Info("character琌﹃");
				throw new ApplicationException("Character is not valid.");
			}
			if (character.Length == 1)
			{
				ASCIIEncoding asciiEncoding = new ASCIIEncoding();
				return asciiEncoding.GetBytes(character)[0];
			}
			Logger.Info("characterぃ单1 : " + character);
			throw new ApplicationException("Character is not valid.");
        }

        #region 璸衡BYTE
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

        #region ﹃锣DOUBLE
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
				Logger.Info("祇ネ岿粇ず甧:" + CStr(obj));
				Logger.Error(ex.Message, ex);
				throw ex;
			}
			return dblResult;
        }

        #endregion

        #region 计翴ぇ计
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
				Logger.Info("璶锣传计=" + strValue);
				intPointPos = strValue.IndexOf(".");
				if (intPointPos == -1)
				{
					Logger.Info("⊿Τ计翴, ボ肚琌俱计");
					strDecimal = Pad("", "0", intDecimals, "R");
					if (!strDecimal.Equals(""))
					{
						strValue = strValue + "." + strDecimal;
					}
					return strValue;
				}
				Logger.Info("Τ计翴, ボ肚琌疊翴计");
				strInteger = strValue.Substring(0, intPointPos);
				Logger.Info("俱计场:" + strInteger);
				strDecimal = Pad(strValue.Substring(intPointPos + 1), "0", intDecimals + 1, "R");
				strDigit = strDecimal.Substring(intDecimals, 1);
				Logger.Info("璶耞计:" + strDigit);
				if (CInt(strDigit) > 4)
				{
					Logger.Info("璶秈");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("计翴计ヘ:" + strDecimal);
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
					Logger.Info("ぃノ秈");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("计翴计ヘ:" + strDecimal);
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

        #region ascii 锣char
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

        #region ﹃锣俱计
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

        #region ン锣﹃
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
				Logger.Info("璶锣传计=" + strValue);
				intPointPos = strValue.IndexOf(".");
				if (intPointPos == -1)
				{
					Logger.Info("⊿Τ计翴, ボ肚琌俱计");
					strDecimal = Pad("", "0", intDecimals, "R");
					if (!strDecimal.Equals(""))
					{
						strValue = strValue + "." + strDecimal;
					}
					return strValue;
				}
				Logger.Info("Τ计翴, ボ肚琌疊翴计");
				strInteger = strValue.Substring(0, intPointPos);
				Logger.Info("俱计场:" + strInteger);
				strDecimal = Pad(strValue.Substring(intPointPos + 1), "0", intDecimals + 1, "R");
				strDigit = strDecimal.Substring(intDecimals, 1);
				Logger.Info("璶耞计:" + strDigit);
				if (CInt(strDigit) > 4)
				{
					Logger.Info("璶秈");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("计翴计ヘ:" + strDecimal);
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
					Logger.Info("ぃノ秈");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("计翴计ヘ:" + strDecimal);
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

        #region ﹃锣Θ计彼き计翴计
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
				Logger.Info("璶锣传计=" + strValue);
				intPointPos = strValue.IndexOf(".");
				if (intPointPos == -1)
				{
					Logger.Info("⊿Τ计翴, ボ肚琌俱计");
					strDecimal = Pad("", "0", intDecimals, "R");
					if (!strDecimal.Equals(""))
					{
						strValue = strValue + "." + strDecimal;
					}
					return strValue;
				}
				Logger.Info("Τ计翴, ボ肚琌疊翴计");
				strInteger = strValue.Substring(0, intPointPos);
				Logger.Info("俱计场:" + strInteger);
				strDecimal = strValue.Substring(intPointPos + 1);
				Logger.Info("计场:" + strDecimal);
				strDecimal = Pad(strDecimal, "0", intDecimals + 1, "R");
				Logger.Info("计场干箂ぇ:" + strDecimal);
				strDigit = strDecimal.Substring(intDecimals, 1);
				Logger.Info("璶耞计:" + strDigit);
				if (CInt(strDigit) > 4)
				{
					Logger.Info("璶秈");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("计翴计ヘ:" + strDecimal);
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
					Logger.Info("ぃノ秈");
					strDecimal = strDecimal.Substring(0, intDecimals);
					Logger.Info("计翴计ヘ:" + strDecimal);
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
                Logger.Info("祇ネ岿粇戈:" + PA_Str);
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

        #region IsInteger() 浪琩琌俱计
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

        #region Left() 耝オ凹じ
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

        #region Right() 耝凹じ
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

        #region FillRight() ﹚じ恶骸よぃì竚
        public string FillRight(string mstr, int ilen)
        {
            return FillRight(mstr, ilen, "0");
        }

        public string FillRight(string mstr, int ilen, string fstr)
        {
            string nstr = "";

            // 材じ恶骸じ
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

        #region FillLeft() ﹚じ恶骸オよぃì竚
        public string FillLeft(string mstr, int ilen)
        {
            return FillLeft(mstr, ilen, "0");
        }

        public string FillLeft(string mstr, int ilen, string fstr)
        {
            string nstr = "";

            // 材じ恶骸じ
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

        #region Dupliacte() 玻ネ滦﹃
        public string Duplicate(string mstr, int ncnt)
        {
            // ㄏノ StringBuilder 硉﹃滦玻ネ硉
            StringBuilder dstr = new StringBuilder();
            int icnt = 0;

            for (icnt = 0; icnt < ncnt; icnt++)
            {
                dstr.Append(mstr);
            }

            return dstr.ToString();
        }
        #endregion

        #region ChNumber() 计锣いゅ计 (箂....)
        public string ChNumber(int NInt)
        {
            return ChNumber(NInt.ToString());
        }

        public string ChNumber(string NStr)
        {
            // よじ
            NStr = Left(NStr, 1);

            switch (NStr)
            {
                case "0":
                    NStr = "箂";
                    break;

                case "1":
                    NStr = "";
                    break;

                case "2":
                    NStr = "";
                    break;

                case "3":
                    NStr = "";
                    break;

                case "4":
                    NStr = "";
                    break;

                case "5":
                    NStr = "き";
                    break;

                case "6":
                    NStr = "せ";
                    break;

                case "7":
                    NStr = "";
                    break;

                case "8":
                    NStr = "";
                    break;

                case "9":
                    NStr = "";
                    break;

                default:
                    NStr = "";
                    break;
            }
            return NStr;
        }
        #endregion

        #region ChCapitalNumber() 计锣いゅ糶计 (箂滁禠...)
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
                    NStr = "箂";
                    break;

                case "1":
                    NStr = "滁";
                    break;

                case "2":
                    NStr = "禠";
                    break;

                case "3":
                    NStr = "把";
                    break;

                case "4":
                    NStr = "竩";
                    break;

                case "5":
                    NStr = "ヮ";
                    break;

                case "6":
                    NStr = "嘲";
                    break;

                case "7":
                    NStr = "琺";
                    break;

                case "8":
                    NStr = "";
                    break;

                case "9":
                    NStr = "╤";
                    break;

                default:
                    NStr = "";
                    break;
            }
            return NStr;
        }
        #endregion

        #region GetFourChNumber() 眔–计いゅ计 (窾货...)
        public string GetFourChNumber(int iDigit)
        {
            string sDigit = "";

            // 俱计篈ㄊぇいゅ计琌ノぃ
            switch (iDigit)
            {
                case 0:
                    sDigit = "";
                    break;
                case 1:
                    sDigit = "窾";
                    break;
                case 2:
                    sDigit = "货";
                    break;
                case 3:
                    sDigit = "";
                    break;
                case 4:
                    sDigit = "ㄊ";
                    break;
                case 5:
                    sDigit = "";
                    break;
                case 6:
                    sDigit = "荫";
                    break;
                case 7:
                    sDigit = "鲽";
                    break;
                case 8:
                    sDigit = "肪";
                    break;
                case 9:
                    sDigit = "碱";
                    break;
                case 10:
                    sDigit = "タ";
                    break;
                case 11:
                    sDigit = "更";
                    break;
                case 12:
                    sDigit = "伐";
                    break;
                default:
                    sDigit = "≯";
                    break;
            }

            return sDigit;
        }
        #endregion

        #region GetChNumber() 俱计锣いゅ计 10050 => 窾箂き
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

            // いゅ计计矪瞶虫(窾货ㄊ....)
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

                    if (tmpStr == "箂")
                    {
                        if (Left(ChSubStr, 1) != "箂" && Left(ChSubStr, 1) != "")
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
                                ChSubStr = tmpStr + "" + ChSubStr;
                                break;
                            case 2:
                                ChSubStr = tmpStr + "κ" + ChSubStr;
                                break;
                            case 3:
                                ChSubStr = tmpStr + "" + ChSubStr;
                                break;
                            default:
                                ChSubStr = tmpStr + "≯" + ChSubStr;
                                break;
                        }
                    }
                }

                if (ChSubStr == "箂" && Left(ChStr, 1) != "" && Left(ChStr, 1) != "箂")
                    ChStr = "箂" + ChStr;
                else
                {
                    if (ChSubStr != "")
                    {
                        // 眔10000Ωよいゅ计
                        // 俱计篈ㄊぇいゅ计琌ノぃ
                        ChStr = ChSubStr + GetFourChNumber(lCnt) + ChStr;
                    }
                }
            }

            if (ChStr == "")
                ChStr = "箂";
            else if (Left(ChStr, 1) == "箂")
                ChStr = ChStr.Substring(1, ChStr.Length - 1);

            return ChStr;
        }
        #endregion

        #region GetChNumberFill() 俱计锣いゅ计 10050 => 窾箂箂κき箂
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

            // いゅ计计矪瞶虫(窾货ㄊ....)
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
                            ChSubStr = tmpStr + "" + ChSubStr;
                            break;
                        case 2:
                            ChSubStr = tmpStr + "κ" + ChSubStr;
                            break;
                        case 3:
                            ChSubStr = tmpStr + "" + ChSubStr;
                            break;
                        default:
                            ChSubStr = tmpStr + ChSubStr;
                            break;
                    }
                }

                if (ChSubStr != "")
                {
                    // 眔10000Ωよいゅ计
                    // 俱计篈ㄊぇいゅ计琌ノぃ
                    ChStr = ChSubStr + GetFourChNumber(lCnt) + ChStr;
                }
            }

            while (Left(ChStr, 1) == "箂" && ChStr.Length > 1)
            {
                if (Left(ChStr, 1) == "箂")
                    ChStr = ChStr.Substring(2, ChStr.Length - 2);
            }

            return ChStr;
        }
        #endregion

        #region GetChNumberShort() 俱计锣虏菠いゅ计 (10050 => 箂箂き箂)
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

        #region GetChCapitalNumber() 俱计锣いゅ糶计10050 => 滁窾箂ヮ珺
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

            // いゅ计计矪瞶虫(窾货ㄊ....)
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

                    if (tmpStr == "箂")
                    {
                        if (Left(ChSubStr, 1) != "箂" && Left(ChSubStr, 1) != "")
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
                                ChSubStr = tmpStr + "珺" + ChSubStr;
                                break;
                            case 2:
                                ChSubStr = tmpStr + "ㄕ" + ChSubStr;
                                break;
                            case 3:
                                ChSubStr = tmpStr + "" + ChSubStr;
                                break;
                            default:
                                ChSubStr = tmpStr + ChSubStr;
                                break;
                        }
                    }
                }

                if (ChSubStr == "箂" && Left(ChStr, 1) != "" && Left(ChStr, 1) != "箂")
                    ChStr = "箂" + ChStr;
                else
                {
                    if (ChSubStr != "")
                    {
                        // 眔10000Ωよいゅ计
                        // 俱计篈ㄊぇいゅ计琌ノぃ
                        ChStr = ChSubStr + GetFourChNumber(lCnt) + ChStr;
                    }
                }
            }

            if (ChStr == "")
                ChStr = "箂";
            else if (Left(ChStr, 1) == "箂")
                ChStr = ChStr.Substring(1, ChStr.Length - 1);

            return ChStr;
        }
        #endregion

        #region GetChCapitalNumberFill() 俱计锣いゅ糶计 10050 => 滁窾箂箂ㄕヮ珺箂
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

            // いゅ计计矪瞶虫(窾货ㄊ....)
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
                            ChSubStr = tmpStr + "珺" + ChSubStr;
                            break;
                        case 2:
                            ChSubStr = tmpStr + "ㄕ" + ChSubStr;
                            break;
                        case 3:
                            ChSubStr = tmpStr + "" + ChSubStr;
                            break;
                        default:
                            ChSubStr = tmpStr + ChSubStr;
                            break;
                    }
                }

                if (ChSubStr != "")
                {
                    // 眔10000Ωよいゅ计
                    // 俱计篈ㄊぇいゅ计琌ノぃ
                    ChStr = ChSubStr + GetFourChNumber(lCnt) + ChStr;
                }
            }

            while (Left(ChStr, 1) == "箂" && ChStr.Length > 1)
            {
                if (Left(ChStr, 1) == "箂")
                    ChStr = ChStr.Substring(2, ChStr.Length - 2);
            }

            return ChStr;
        }
        #endregion

        #region GetChCapitalNumberShort() 俱计锣虏菠いゅ糶计 (10050 => 滁箂箂ヮ箂)
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

