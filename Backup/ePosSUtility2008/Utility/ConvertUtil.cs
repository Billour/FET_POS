
    using log4net;
    using System;
    using System.IO;
    using System.Configuration;
    using System.Data;
    using System.Web.UI;
namespace Advtek.Utility
{
    public sealed class ConvertUtil : BaseClass
    {
       // private static LogUtil Logger = new LogUtil(typeof(ConvertUtil)); 
	

        public static object ToDateTime(string PA_Value)
        {
            object objResult = null;
            try
            {
                if ((PA_Value != null) && !PA_Value.Equals(""))
                {
                    objResult = Convert.ToDateTime(PA_Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            return objResult;
        }

        public static object ToDecimal(string PA_Value)
        {
            object objResult = null;
            try
            {
                if ((PA_Value != null) && !PA_Value.Equals(""))
                {
                    objResult = Convert.ToDecimal(PA_Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            return objResult;
        }

        public static object ToDouble(string PA_Value)
        {
            object objResult = null;
            try
            {
                if ((PA_Value != null) && !PA_Value.Equals(""))
                {
                    objResult = Convert.ToDouble(PA_Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            return objResult;
        }

        public static object ToInt32(string PA_Value)
        {
            object objResult = null;
            try
            {
                if ((PA_Value != null) && !PA_Value.Equals(""))
                {
                    objResult = Convert.ToInt32(PA_Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            return objResult;
        }

        public static object ToString(string PA_Value)
        {
            try
            {
                if (PA_Value != null)
                {
                    return Convert.ToString(PA_Value);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }
            return "";
        }
    }
}

