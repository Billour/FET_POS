namespace Advtek.Utility
{
    using log4net;
    using Microsoft.VisualBasic;
    using System;

    public sealed class NumberUtil
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(NumberUtil));

        public static bool IsNumeric(string PA_Str)
        {
            bool blnResult = true;
            try
            {
                if (PA_Str == null)
                {
                    return false;
                }
                if (PA_Str.Equals(""))
                {
                    return false;
                }
                blnResult = Information.IsNumeric(PA_Str);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
            return blnResult;
        }

        public static double Round(double PA_Num, int PA_Precision)
        {
            return Math.Round(PA_Num, PA_Precision);
        }

		public static string CNumber(string NumStr) 
		{ 
			string functionReturnValue = null; 
			string dZero; 
			int iLen; 
			string TempStr; 
			string sValues; 
			string TempA; 
            
			if (Information.IsNumeric(NumStr)) 
			{ 
				sValues = ""; 
				NumStr = StringUtil.CStr(Math.Round(StringUtil.CDbl(NumStr) , 2)); 

				
                
				if (NumStr.IndexOf(".") > 0) 
				{ 
					dZero = Strings.Right(NumStr, Strings.Len(NumStr) - NumStr.IndexOf(".")); 
					NumStr = Strings.Left(NumStr, NumStr.IndexOf(".") - 1); 
				} 
				else 
				{ 
					//NumStr = (string)Math.Round((double)NumStr, 0); 
					NumStr = StringUtil.CStr(Math.Round(StringUtil.CDbl(NumStr) , 0)); 
					//NumStr = NumStr; 
					dZero = ""; 
				} 
                
				iLen = Strings.Len(Strings.Trim(NumStr)); 
                
				for (int i = 1; i <= iLen; i += 1) 
				{ 
					TempStr = Strings.Right(NumStr, i); 
					TempStr = Strings.Left(TempStr, 1); 
					TempStr = StringUtil.CStr(GF_Converts((object) TempStr)); 
                    
					switch (i) 
					{ 
						case 1: 
							if (TempStr == "零") 
							{ 
								if (iLen == 1) 
								{ 
									TempStr = "零元"; 
								} 
								else 
								{ 
									TempStr = "元"; 
								} 
							} 
							else 
							{ 
								TempStr = TempStr + "元"; 
							} 

							break; 
						case 2: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else if (TempStr == "一") 
							{ 
								TempStr = "十"; 
							} 
							else 
							{ 
								TempStr = TempStr + "十"; 
							} 

							break; 
						case 3: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "佰"; 
							} 

							break; 
						case 4: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "仟"; 
							} 

							break; 
						case 5: 
							if (TempStr == "零") 
							{ 
								TempStr = "萬"; 
							} 
							else 
							{ 
								TempStr = TempStr + "萬"; 
							} 

							break; 
						case 6: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "十"; 
							} 

							break; 
						case 7: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "佰"; 
							} 

							break; 
						case 8: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "仟"; 
							} 

							break; 
						case 9: 
							if (TempStr == "零") 
							{ 
								TempStr = "億"; 
							} 
							else 
							{ 
								TempStr = TempStr + "億"; 
							} 

							break; 
						case 10: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "拾"; 
							} 

							break; 
						case 11: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "佰"; 
							} 

							break; 
						case 12: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "仟"; 
							} 

							break; 
						case 13: 
							if (TempStr == "零") 
							{ 
								TempStr = "零"; 
							} 
							else 
							{ 
								TempStr = TempStr + "萬"; 
							} 

							break; 
					} 
                    
					TempA = Strings.Left(Strings.Trim(sValues), 1); 
                    
					if (TempStr == "零") 
					{ 
						switch (TempA) 
						{ 
							case "零": 
							case "元": 
							case "萬": 
							case "億": 
								//sValues = sValues; 
								break; 
							default: 
								sValues = TempStr + sValues; 
								break; 
						} 
					} 
					else 
					{ 
						sValues = TempStr + sValues; 
					} 
				} 
                
				TempA = ""; 
                
				switch (Strings.Len(dZero)) 
				{ 
					case 0: 
						break; 
					case 1: 
						if (StringUtil.CDbl(dZero)  > 0) 
						{ 
							TempStr =StringUtil.CStr( GF_Converts(dZero)); 
							if (TempStr != "零") 
							{ 
								TempA = TempStr + "角"; 
							} 
						} 

						break; 
					case 2: 
						if (StringUtil.CDbl(dZero) > 0) 
						{ 
							TempStr = StringUtil.CStr( GF_Converts(Strings.Right(dZero, 1))); 
							if (TempStr != "零") 
							{ 
								TempA = TempStr + "分"; 
							} 
							TempStr = StringUtil.CStr( GF_Converts(Strings.Left(dZero, 1))); 
							if (TempStr == "零") 
							{ 
								if (Strings.Len(TempA) > 0) 
								{ 
									TempA = TempStr + TempA; 
								} 
							} 
							else 
							{ 
								TempA = TempStr + "角" + TempA; 
							} 
						} 

						break; 
					default: 
						break; 
                    
				} 
                
				functionReturnValue = sValues + TempA; 
			} 
            
			else 
			{ 
				functionReturnValue = ""; 
			} 
			return functionReturnValue; 
            
		} 
        
		//*** 轉換一位數 
		private static object GF_Converts(object NumStr) 
		{ 
			object functionReturnValue = null; 
            
			switch (StringUtil.CInt(NumStr)) 
			{ 
				case 0: 
					functionReturnValue = "零"; 
					break; 
				case 1: 
					functionReturnValue = "一"; 
					break; 
				case 2: 
					functionReturnValue = "二"; 
					break; 
				case 3: 
					functionReturnValue = "三"; 
					break; 
				case 4: 
					functionReturnValue = "四"; 
					break; 
				case 5: 
					functionReturnValue = "五"; 
					break; 
				case 6: 
					functionReturnValue = "六"; 
					break; 
				case 7: 
					functionReturnValue = "七"; 
					break; 
				case 8: 
					functionReturnValue = "八"; 
					break; 
				case 9: 
					functionReturnValue = "九"; 
					break; 
			} 
			return functionReturnValue; 
		} 
    }
}

