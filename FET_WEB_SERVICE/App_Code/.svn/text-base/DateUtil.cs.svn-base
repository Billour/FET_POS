using System; 
using System.IO; 
using System.Configuration; 
using System.Data; 
using System.Web.UI; 
using System.Data.OracleClient; 
using Microsoft.VisualBasic; 
using System.Text;

namespace Advtek.Utility 
{ 
	public class DateUtil 
	{ 
		private static LogUtil Logger = new LogUtil(typeof(DateUtil)); 
        
		#region "Now()" 
		public static string Now()
		{ 
			string strYear = DateTime.Now.Year.ToString(); 
			string strMonth = DateTime.Now.Month.ToString(); 
			string strDay = DateTime.Now.Day.ToString(); 
			string strHour = DateTime.Now.Hour.ToString(); 
			string strMinute = DateTime.Now.Minute.ToString(); 
			string strSecond = DateTime.Now.Second.ToString(); 
			string txtNow = strYear + "/" + strMonth + "/" + strDay + " " + strHour + ":" + strMinute + ":" + strSecond; 
            
			return txtNow; 
            
		} 
		#endregion 
        
		#region "DtStr" 
		// <summary> 
		// 用來把日期格式的字串, 從sourceFormatStr指定的格式轉換成targetFormatStr指定的格式 
		// </summary> 
		// <remarks> 
		// 目前提供幾種轉換<br/> 
		// 1.YMD->CMD<br/> 
		// 2.CMD->YMD<br/> 
		// 3.YMDHNS->YMD<br/> 
		// 4.YMDHNS->CMD<br/> 
		// 5.YMDHNS->CMDHNS<br/> 
		// 6.YMDHNS->HNS<br/> 
		// 7.CMDHNS->YMD<br/> 
		// 8.CMDHNS->CMD<br/> 
		// 9.CMDHNS->HNS<br/> 
		// 10.CMDHNS->YMDHNS<br/> 
		// </remarks> 
		// <param name="strDate"></param> 
		// <param name="sourceFormatStr"> 
		// 來源日期格式<br/> 
		// YMD-YYYY(西元年)/MM/DD的格式<br/> 
		// CMD-CYY(民國年)/MM/DD的格式<br/> 
		// YMDHNS-YYYY(西元年)/MM/DD HH:NN:SS的格式<br/> 
		// CMDHNS-CYY(民國年)/MM/DD HH:NN:SS的格式<br/> 
		// </param> 
		// <param name="targetFormatStr"> 
		// 要轉換的目的格式<br/> 
		// YMD-以YYYY(西元年)/MM/DD的格式 格式化傳入的字串<br/> 
		// CMD-以CYY(民國年)/MM/DD的格式 格式化傳入的字串<br/> 
		// HNS-以HH:NN:SS的格式 格式化傳入的字串<br/> 
		// YMDHNS-以YYYY(西元年)/MM/DD HH:NN:SS的格式 格式化傳入的字串<br/> 
		// CMDHNS-以CYY(民國年)/MM/DD HH:NN:SS的格式 格式化傳入的字串<br/> 
		// </param> 
		// <returns>格式化後的日期字串</returns> 
		// <remarks> 
		// 如果傳入的參數是null或空字串或來源格式與目的格式不合法時, 會回傳空字串 
		// </remarks> 
		public static string DtStr(string strDate, string sourceFormatStr, string targetFormatStr) 
		{ 
			string strSource = sourceFormatStr.ToUpper(); 
			string strTarget = targetFormatStr.ToUpper(); 
			string result = ""; 
            
			try 
			{ 
				if (((strDate == null) | (sourceFormatStr == null) | (targetFormatStr == null))) 
				{ 
					return ""; 
				} 
				else if (((strDate.Equals("")) | (sourceFormatStr.Equals("")) | (targetFormatStr.Equals("")))) 
				{ 
					return ""; 
				} 
				else if (((!strSource.Equals("YMD")) & (!strSource.Equals("CMD")) & (!strSource.Equals("YMDHNS")) & (!strSource.Equals("CMDHNS")))) 
				{ 
					return ""; 
				} 
				else if (((!strTarget.Equals("YMD")) & (!strTarget.Equals("CMD")) & (!strTarget.Equals("YMDHNS")) & (!strTarget.Equals("CMDHNS")) & (!strTarget.Equals("HNS")))) 
				{ 
					return ""; 
				} 
				else if (((strSource.Equals("YMD") & strTarget.Equals("CMD")) | (strSource.Equals("CMD") & strTarget.Equals("YMD")) | (strSource.Equals("YMDHNS") & strTarget.Equals("YMD")) | (strSource.Equals("YMDHNS") & strTarget.Equals("CMD")) | (strSource.Equals("YMDHNS") & strTarget.Equals("CMDHNS")) | (strSource.Equals("YMDHNS") & strTarget.Equals("HNS")) | (strSource.Equals("CMDHNS") & strTarget.Equals("YMD")) | (strSource.Equals("CMDHNS") & strTarget.Equals("CMD")) | (strSource.Equals("CMDHNS") & strTarget.Equals("HNS")) | (strSource.Equals("CMDHNS") & strTarget.Equals("YMDHNS")))) 
				{ 
                    
					//檢查是否為合法的轉換 
					string strCYear = ""; 
                    
					if ((strSource.Equals("CMD") | strSource.Equals("CMDHNS"))) 
					{ 
						strCYear = strDate.Substring(0, strDate.IndexOf("/")); 
						strDate = (StringUtil.CStr((StringUtil.CInt(strCYear) + 1911)) + strDate.Substring(strDate.IndexOf("/"))); 
					} 
                    
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					string strYear = dtTemp.Year.ToString(); 
					strCYear = (dtTemp.Year - 1911).ToString(); 
					string strMonth = dtTemp.Month.ToString(); 
					string strDay = dtTemp.Day.ToString(); 
					string strHour = dtTemp.Hour.ToString(); 
					string strMinute = dtTemp.Minute.ToString(); 
					string strSecond = dtTemp.Second.ToString(); 
                    
					if (targetFormatStr.ToUpper().Equals("YMD")) 
					{ 
						result = strYear + "/" + strMonth + "/" + strDay; 
					} 
					else if (targetFormatStr.ToUpper().Equals("CMD")) 
					{ 
						result = strCYear + "/" + strMonth + "/" + strDay; 
					} 
					else if (targetFormatStr.ToUpper().Equals("HNS")) 
					{ 
						result = strHour + ":" + strMinute + ":" + strSecond; 
					} 
					else if (targetFormatStr.ToUpper().Equals("YMDHNS")) 
					{ 
						result = strYear + "/" + strMonth + "/" + strDay + " " + strHour + ":" + strMinute + ":" + strSecond; 
					} 
					else if (targetFormatStr.ToUpper().Equals("CMDHNS")) 
					{ 
						result = strCYear + "/" + strMonth + "/" + strDay + " " + strHour + ":" + strMinute + ":" + strSecond; 
					} 
				} 
                
				else 
				{ 
					result = ""; 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "DtStr" 
		// <summary> 
		// 將日期字串以指定的的targetFormatStr傳回 
		// </summary> 
		// <param name="strDate">要處理的日期字串</param> 
		// <param name="targetFormatStr"> 
		// 要轉換的目的格式<br/> 
		// YMD-以YYYY(西元年)/MM/DD的格式 格式化傳入的字串<br/> 
		// CMD-以CYY(民國年)/MM/DD的格式 格式化傳入的字串<br/> 
		// HNS-以HH:NN:SS的格式 格式化傳入的字串<br/> 
		// YMDHNS-以YYYY(西元年)/MM/DD HH:NN:SS的格式 格式化傳入的字串<br/> 
		// CMDHNS-以CYY(民國年)/MM/DD HH:NN:SS的格式 格式化傳入的字串<br/> 
		// </param> 
		// <returns>格式化後的日期字串</returns> 
		// <remarks> 
		// 如果傳入的參數是null或空字串時, 會回傳空字串 
		// </remarks> 
		public static string DtStr(string strDate, string targetFormatStr) 
		{ 
			string result = ""; 
			DateTime dtTemp; 
			string strYear = ""; 
			string strCYear = ""; 
			string strMonth = ""; 
			string strDay = ""; 
			string strHour = ""; 
			string strMinute = ""; 
			string strSecond = ""; 
            
			try 
			{ 
				if (((strDate == null) | (targetFormatStr == null))) 
				{ 
					return ""; 
				} 
				else if ((strDate.Equals("") | targetFormatStr.Equals(""))) 
				{ 
					return ""; 
				} 
				else 
				{ 
                    
					dtTemp = Convert.ToDateTime(strDate); 
                    
					strYear = dtTemp.Year.ToString(); 
					strCYear = (dtTemp.Year - 1911).ToString(); 
					strMonth = dtTemp.Month.ToString(); 
					strDay = dtTemp.Day.ToString(); 
					strHour = dtTemp.Hour.ToString(); 
					strMinute = dtTemp.Minute.ToString(); 
					strSecond = dtTemp.Second.ToString(); 
                    
					if (targetFormatStr.ToUpper().Equals("YMD")) 
					{ 
						return strYear + "/" + strMonth + "/" + strDay; 
					} 
					else if (targetFormatStr.ToUpper().Equals("CMD")) 
					{ 
						return strCYear + "/" + strMonth + "/" + strDay; 
					} 
					else if (targetFormatStr.ToUpper().Equals("HNS")) 
					{ 
						return strHour + ":" + strMinute + ":" + strSecond; 
					} 
					else if (targetFormatStr.ToUpper().Equals("YMDHNS")) 
					{ 
						return strYear + "/" + strMonth + "/" + strDay + " " + strHour + ":" + strMinute + ":" + strSecond; 
					} 
					else if (targetFormatStr.ToUpper().Equals("CMDHNS")) 
					{ 
						return strCYear + "/" + strMonth + "/" + strDay + " " + strHour + ":" + strMinute + ":" + strSecond; 
					} 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "DtStr" 
		// <summary> 
		// 將日期字串以YYYY/MM/DD的格式傳回 
		// </summary> 
		// <param name="strDate">YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>YYYY/MM/DD格式的日期字串</returns> 
		// <remarks> 
		// 如果傳入的參數是null或空字串時, 會回傳空字串 
		// </remarks> 
		public static  string  DtStr(string strDate) 
		{ 
			string result = ""; 
            
			try 
			{ 
				if ((strDate == null)) 
				{ 
					return ""; 
				} 
				else if (strDate.Equals("")) 
				{ 
					return ""; 
				} 
				else 
				{ 
					result = DateUtil.DtStr(strDate, "YMD"); 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "IsDate" 
		// <summary> 
		// 判斷傳入的日期字串是否為合法的日期 
		// </summary> 
		// <param name="PA_Date">日期格式字串</param> 
		// <returns> 
		// True:此字串符合日期格式, <br/> 
		// False:此字串不符合日期格式<br/> 
		// </returns> 
		public static bool IsDate(string PA_Date) 
		{ 
			bool blnResult = true; 
            
			try 
			{ 
				if ((PA_Date == null)) 
				{ 
					return false; 
				} 
                
				if (PA_Date.Equals("")) 
				{ 
					return false; 
				} 
                
				blnResult = Information.IsDate(PA_Date); 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return blnResult; 
		} 
		#endregion 
        
		#region "ManDateAdd" 
		// <summary> 
		// 用來做工作日加減的運算 
		// </summary> 
		// <param name="PA_Number">要調整的日期或時間的數量</param> 
		// <param name="PA_Date">要進行計算的日期, YYYY/MM/DD格式的日期字串</param> 
		// <returns>計算後的日期字串, 格式為YYYY/MM/DD</returns> 
		// <remarks> 
		// 
		// </remarks> 
		public static string ManDateAdd(int PA_Number, string PA_Date) 
		{ 
			string sreResult = ""; 
			DateTime dteValue; 
			int intWeekDay; 
			int intCount = PA_Number; 
			string strCurrDate = PA_Date; 
            
			try 
			{ 
				if ((DateUtil.IsDate(PA_Date))) 
				{ 
                    
					while ((intCount > 0)) 
					{ 
                        
						//判斷傳入那一天是禮拜幾, 1代表禮拜一, 7代表禮拜天 

						DateTime objDateTime =(DateTime)  ConvertUtil.ToDateTime( strCurrDate);
						intWeekDay = DateAndTime.Weekday(objDateTime, FirstDayOfWeek.Monday); 
                        
						if ((intWeekDay < 6)) 
						{ 
							//如果是禮拜一到禮拜五 
							intCount -= 1; 
						} 
                        
						//如果Counter還有值, 就取下一天 
						if ((intCount > 0)) 
						{ 
							dteValue = DateAndTime.DateAdd("d", 1, strCurrDate); 
							strCurrDate = StringUtil.CStr(dteValue); 
						} 
					} 
                    
					sreResult = DateUtil.DtStr(strCurrDate, "YMD"); 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return sreResult; 
		} 
		#endregion 
        
		#region "DateAdd" 
		// <summary> 
		// 用來做日期時間加減的運算 
		// </summary> 
		// <param name="PA_Interval"> 
		// 日期比對的單位<br/> 
		// yyyy-年<br/> 
		// m-月<br/> 
		// d-天<br/> 
		// h-時<br/> 
		// n-分<br/> 
		// s-秒<br/> 
		// w-週<br/> 
		// </param> 
		// <param name="PA_Number">要調整的日期或時間的數量</param> 
		// <param name="PA_Date">要進行計算的日期, YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>計算後的日期字串, 格式為YYYY/MM/DD HH:NN:SS</returns> 
		// <remarks> 
		// 如果傳入的比對單位不合法或是調整數量為null或日期不為合法的日期的話, 會回傳0 
		// </remarks> 
		public static string DateAdd(string PA_Interval, double PA_Number, string PA_Date) 
		{ 
			string sreResult = ""; 
            
			try 
			{ 
				if ((PA_Interval.ToLower().Equals("yyyy") | PA_Interval.ToLower().Equals("m") | PA_Interval.ToLower().Equals("d") | PA_Interval.ToLower().Equals("h") | PA_Interval.ToLower().Equals("n") | PA_Interval.ToLower().Equals("s") | PA_Interval.ToLower().Equals("w")) & DateUtil.IsDate(PA_Date)) 
				{ 
                    
					DateTime dteValue = DateAndTime.DateAdd(PA_Interval, PA_Number, PA_Date); 
					sreResult = StringUtil.CStr(dteValue); 
					sreResult = DateUtil.DtStr(sreResult, "YMDHNS"); 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return sreResult; 
		} 
		#endregion 
        
		#region "DateDiff" 
		// <summary> 
		// 依據指定的單位來比對兩個日期之間的時間差異, 是以Date2-Date1來進行計算 
		// </summary> 
		// <param name="PA_Interval"> 
		// 日期比對的單位<br/> 
		// yyyy-年<br/> 
		// m-月<br/> 
		// d-天<br/> 
		// h-時<br/> 
		// n-分<br/> 
		// s-秒<br/> 
		// w-週<br/> 
		// </param> 
		// <param name="PA_Date1">YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <param name="PA_Date2">YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>兩個日期之間的時間差異, 若為正數表示Date2比Date1晚</returns> 
		// <remarks> 
		// 如果傳入的比對單位不合法或是日期不為合法的日期的話, 會回傳0 
		// </remarks> 
		public static long DateDiff(string PA_Interval, string PA_Date1, string PA_Date2) 
		{ 
			long lngValue = 0; 
            
			try 
			{ 
				if ((PA_Interval.ToLower().Equals("yyyy") | PA_Interval.ToLower().Equals("m") | PA_Interval.ToLower().Equals("d") | PA_Interval.ToLower().Equals("h") | PA_Interval.ToLower().Equals("n") | PA_Interval.ToLower().Equals("s") | PA_Interval.ToLower().Equals("w")) & DateUtil.IsDate(PA_Date1) & DateUtil.IsDate(PA_Date2)) 
				{ 
                    
					lngValue = DateAndTime.DateDiff(PA_Interval, PA_Date1, PA_Date2, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1); 
                    
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return lngValue; 
		} 
		#endregion 
        
		#region "Today" 
		// <summary> 
		// 以YYYY/MM/DD的格式回傳目前的時間字串 
		// </summary> 
		// <returns>回傳值為目前的日期, 以YYYY/MM/DD的格式組成的字串</returns> 
		public static string Today() 
		{ 
            
			//StringUtil.Pad(DateUtil.Hour, "0", 2, "L") 
			string strYear = DateTime.Now.Year.ToString(); 
			string strMonth = StringUtil.Pad(DateTime.Now.Month.ToString(), "0", 2, "L"); 
			string strDay = StringUtil.Pad(DateTime.Now.Day.ToString(), "0", 2, "L"); 
			return strYear + "/" + strMonth + "/" + strDay; 
		} 
		#endregion 
        
		#region "Year" 
		// <summary> 
		// 取得日期中的年度 
		// </summary> 
		// <param name="strDate">YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>回傳值為西元年</returns> 
		// <remarks>若傳入的字串不為合法的日期, 會回傳空字串</remarks> 
		public static string Year(string strDate) 
		{ 
			string result = ""; 
            
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					string strYear = dtTemp.Year.ToString(); 
					result = strYear; 
				} 
			} 
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Year" 
		// <summary> 
		// 取得今天的年度 
		// </summary> 
		// <returns>回傳值為西元年</returns> 
		public static string Year() 
		{ 
			string result = ""; 
            
			try 
			{ 
				DateTime dtTemp = Convert.ToDateTime(DateUtil.Now()); 
				string strYear = dtTemp.Year.ToString(); 
				result = strYear; 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Month" 
		// <summary> 
		// 取得日期中的月份 
		// </summary> 
		// <param name="strDate">YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>回傳值為月份</returns> 
		public static string Month(string strDate) 
		{ 
			string result = ""; 
            
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					string strMonth = dtTemp.Month.ToString(); 
					result = strMonth; 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Month" 
		// <summary> 
		// 取得日期中的月份 
		// </summary> 
		// <returns>回傳值為月份</returns> 
		public static string Month() 
		{ 
			string result = ""; 
            
			try 
			{ 
				DateTime dtTemp = Convert.ToDateTime(DateUtil.Now()); 
				string strMonth = dtTemp.Month.ToString(); 
				result = strMonth; 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Day" 
		// <summary> 
		// 取得日期中的日 
		// </summary> 
		// <param name="strDate">YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>回傳值為日期字串中的日</returns> 
		public static string Day(string strDate) 
		{ 
			string result = ""; 
            
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					string strDay = dtTemp.Day.ToString(); 
					result = strDay; 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Day" 
		// <summary> 
		// 取得日期中的日 
		// </summary> 
		// <returns>回傳值為日期字串中的日</returns> 
		public static string Day() 
		{ 
			string result = ""; 
            
			try 
			{ 
				DateTime dtTemp = Convert.ToDateTime(DateUtil.Now()); 
				string strDay = dtTemp.Day.ToString(); 
				result = strDay; 
			} 
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Hour" 
		// <summary> 
		// 取得日期中的時 
		// </summary> 
		// <param name="strDate">HH:NN:SS或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>回傳值為日期字串中的時</returns> 
		public static string Hour(string strDate) 
		{ 
			string result = ""; 
            
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					result = dtTemp.Hour.ToString(); 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Hour" 
		// <summary> 
		// 取得日期中的時 
		// </summary> 
		// <returns>回傳值為日期字串中的時</returns> 
		public static string Hour() 
		{ 
			string result = ""; 
            
			try 
			{ 
				DateTime dtTemp = Convert.ToDateTime(DateUtil.Now()); 
				result = dtTemp.Hour.ToString(); 
			} 
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Minute" 
		// <summary> 
		// 取得日期中的分鐘 
		// </summary> 
		// <param name="strDate">HH:NN:SS或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>回傳值為日期字串中的分鐘</returns> 
		public static string Minute(string strDate) 
		{ 
			string result = ""; 
            
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					string strDay = dtTemp.Minute.ToString(); 
					result = strDay; 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Minute" 
		// <summary> 
		// 取得日期中的分鐘 
		// </summary> 
		// <returns>回傳值為日期字串中的分鐘</returns> 
		public static string Minute() 
		{ 
			string result = ""; 
            
			try 
			{ 
				DateTime dtTemp = Convert.ToDateTime(DateUtil.Now()); 
				string strDay = dtTemp.Minute.ToString(); 
				result = strDay; 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Second" 
		// <summary> 
		// 取得日期中的秒 
		// </summary> 
		// <returns>回傳值為日期字串中的秒</returns> 
		public static string Second(string strDate) 
		{ 
			string result = ""; 
            
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					result = dtTemp.Second.ToString(); 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Second" 
		// <summary> 
		// 取得日期中的秒 
		// </summary> 
		// <param name="strDate">HH:NN:SS或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>回傳值為日期字串中的秒</returns> 
		public static string Second() 
		{ 
			string result = ""; 
            
			try 
			{ 
				DateTime dtTemp = Convert.ToDateTime(DateUtil.Now()); 
				result = dtTemp.Second.ToString(); 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "Season" 
		// <summary> 
		// 取得季節 
		// </summary> 
		// <param name="strDate">HH:NN:SS或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>回傳值為季節(1-春,2-夏,3-秋,4-冬)</returns> 
		public static string Season(string strDate) 
		{ 
			string result = ""; 
			string strMonth = ""; 
            
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
                    
					strMonth = DateUtil.Month(strDate); 
                    
					if (((strMonth.Equals("1") | strMonth.Equals("2")) | strMonth.Equals("3"))) 
					{ 
						result = "1"; 
					} 
					else if (((strMonth.Equals("4") | strMonth.Equals("5")) | strMonth.Equals("6"))) 
					{ 
						result = "2"; 
					} 
					else if (((strMonth.Equals("7") | strMonth.Equals("8")) | strMonth.Equals("9"))) 
					{ 
						result = "3"; 
					} 
					else if (((strMonth.Equals("10") | strMonth.Equals("11")) | strMonth.Equals("12"))) 
					{ 
						result = "4"; 
					} 
                    
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "DayOfYear" 
		// <summary> 
		// Gets the day of the year represented by this instance. 
		// </summary> 
		// <param name="strDate">YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>The day of the year, between 1 and 366.</returns> 
		public static int DayOfYear(string strDate) 
		{ 
			int result = 0; 
            
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					result = dtTemp.DayOfYear; 
				} 
			} 
            
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
            
			return result; 
		} 
		#endregion 
        
		#region "DayOfWeek" 
		// <summary> 
		// 取得傳入日期是是該星期的第幾天 
		// </summary> 
		// <param name="strDate">YYYY/MM/DD或YYYY/MM/DD HH:NN:SS格式的日期字串</param> 
		// <returns>0-星期天, 6-星期六</returns> 
		public static int DayOfWeek(string strDate) 
		{ 
			int result = 0; 
			try 
			{ 
				if (DateUtil.IsDate(strDate)) 
				{ 
					DateTime dtTemp = Convert.ToDateTime(strDate); 
					result = (int)dtTemp.DayOfWeek; 
				} 
			} 
			catch (Exception ex) 
			{ 
				Logger.Log.Error(ex.Message, ex); 
				throw ex; 
			} 
			return result; 
		} 
        
		#endregion        
        #region NullDateFormat
        public const string NullDateFormatString = "9000/01/01";
        public static DateTime NullDateFormat(object dt)
        {
              DateTime r = Convert.ToDateTime(NullDateFormatString);
              try
              {
                  if (dt != null)
                  {
                      r = Convert.ToDateTime(dt);
                  }
              }
              catch
              { r = Convert.ToDateTime(NullDateFormatString); }
              return r;


        }
        #endregion

    } 
} 