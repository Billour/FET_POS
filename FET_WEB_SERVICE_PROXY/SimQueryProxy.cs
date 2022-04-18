using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Xml;

namespace FET.POS.WS.PROXY
{
	public class SimQueryProxy
	{
		enum QueryType
		{
			PostPaidSimQuery,
			PrePaidSimQuery
        }

        #region Public Static Method : Hashtable PostPaidSimQuery(string simNo, string simQueryUrl)
        /// <summary>
		/// 取得 SIM Card 料號
		/// </summary>
		/// <param name="simNo">Sim No</param>
		/// <returns>Result : 料號或空白; ErrMsg : 當 Result 為空白時, 此處放置錯誤訊息</returns>
		public static Hashtable PostPaidSimQuery(string simNo, string simQueryUrl)
		{
			return getSimQueryData(simNo, QueryType.PostPaidSimQuery, simQueryUrl);
		}
		#endregion

        #region Public Static Method : Hashtable PrePaidSimQuery(string simNo, string simQueryUrl)
        /// <summary>
		/// 取得 SIM Card 料號
		/// </summary>
		/// <param name="simNo">Sim No</param>
		/// <returns>Result : 料號或空白; ErrMsg : 當 Result 為空白時, 此處放置錯誤訊息</returns>
        public static Hashtable PrePaidSimQuery(string simNo, string simQueryUrl)
		{
            return getSimQueryData(simNo, QueryType.PrePaidSimQuery, simQueryUrl);
		}
		#endregion

        #region Private Static Method : Hashtable getSimQueryData(string simNo, QueryType type, string simQueryUrl)
        /// <summary>
		/// 取得 SIM Card 料號主程式
		/// </summary>
		/// <param name="simNo">SimNo</param>
		/// <param name="type">取得方式</param>
		/// <returns>Result : 料號或空白, ErrMsg : 當 Result 為空白時, 此處放置錯誤訊息</returns>
        private static Hashtable getSimQueryData(string simNo, QueryType type, string simQueryUrl)
		{
			Hashtable htResult = new Hashtable();
			htResult.Add("Result", "");
            htResult.Add("Price", "");
			if (simNo.Length == 0)
			{
				htResult.Add("ErrMsg", "未傳入欲查詢的 SimNo");
				return htResult;
			}
			try
			{
				wsSimQuery.SimQuery query = new FET.POS.WS.PROXY.wsSimQuery.SimQuery();
                query.Url = simQueryUrl;
				htResult["URL"] = query.Url;
				XmlDocument xml = new XmlDocument();
				if (type == QueryType.PostPaidSimQuery)
					htResult.Add("Xml", query.PostPaidSimQuery(simNo));
				else if (type == QueryType.PrePaidSimQuery)
					htResult.Add("Xml", query.PrePaidSimQuery(simNo));
				else
				{
					query.Dispose();
					htResult["Result"] = "Err";
					htResult.Add("ErrMsg", string.Format("錯誤的呼叫函式:{0}", type.ToString()));
					return htResult;
				}
				try
				{
					xml.LoadXml(htResult["Xml"].ToString());
					try
					{
						XmlElement root = xml.DocumentElement;
                        if (root.GetElementsByTagName("ReturnCode")[0].InnerText != "000")
                            htResult.Add("ErrMsg", root.GetElementsByTagName("Description")[0].InnerText);
                        else
                        {
                            htResult["Result"] = root.GetElementsByTagName("Prodno")[0].InnerText;
                            htResult["Price"] = root.GetElementsByTagName("Price")[0].InnerText;
                        }
					}
					catch (XmlException ex)
					{
						htResult["Result"] = "Err";
						htResult.Add("ErrMsg", string.Format("XML 節點錯誤 : {0}", ex.Message));
					}
					catch (Exception ex)
					{
						htResult["Result"] = "Err";
						htResult.Add("ErrMsg", string.Format("XML 節點讀取錯誤 : {0}", ex.Message));
					}
				}
				catch (XmlException ex)
				{
					htResult["Result"] = "Err";
					htResult.Add("ErrMsg", "無法解析 XML");
				}
				catch (Exception ex)
				{
					htResult["Result"] = "Err";
					htResult.Add("ErrMsg", string.Format("未知的錯誤 : {0}", ex.Message));
				}
			}
			catch (Exception ex)
			{
				htResult["Result"] = "Err";
				htResult.Add("ErrMsg", string.Format("SimQuery 讀取錯誤 : {0}", ex.Message));
			}
			return htResult;
		}
		#endregion

	}
}
