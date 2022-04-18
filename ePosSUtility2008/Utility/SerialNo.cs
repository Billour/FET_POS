namespace Advtek.Utility
{
    using System.Data.OracleClient;
    using Advtek.Utility.UserException;
    using log4net;
    using System;
    using System.Collections;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Configuration;

    //原取號程式

    //public sealed class SerialNo
    //{
    //    private static readonly string CONNECT_STRING = OracleDBUtil.GetDecryptConnectionString("Connection_String");

    //    private static SerialNo singleton;

    //    //Singleton instance
    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    public static SerialNo GetInstance()
    //    {
    //        if (singleton == null)
    //        {
    //            singleton = new SerialNo();
    //        }
    //        return singleton;
    //    }


    //    //取號程式
    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    public static string GenNo(string PA_Sheet_Type)
    //    {
    //        OracleConnection conn;
    //        StringBuilder strSql = null;
    //        string strResult = "";
    //        string strSegment = "";
    //        Hashtable htlSegment = null;
    //        string strSegType = "";
    //        string strFixString = "";
    //        string strSegLength = "";
    //        string strPadChar = "";
    //        string strSerialNo = "";
    //        string strResetType = "";
    //        try
    //        {
    //            conn = OracleDBUtil.GetConnectionByConnString(CONNECT_STRING);

    //            strSql = new StringBuilder();
    //            strSql.Append("Select * From SeqSegment ");
    //            strSql.Append(" Where Sheet_Type=" + OracleDBUtil.SqlStr(PA_Sheet_Type));
    //            strSql.Append(" Order By Seq_No ");

    //            htlSegment = OracleDBUtil.GetData(conn, strSql.ToString());

    //            if (htlSegment.Count > 0)
    //            {
    //                for (int i = 0; i < htlSegment.Count; i++)
    //                {
    //                    Hashtable htlSegment_Row = (Hashtable)htlSegment[i];
    //                    strSegType = StringUtil.CStr(htlSegment_Row["SEG_TYPE"]).ToUpper();
    //                    strSegLength = StringUtil.CStr(htlSegment_Row["SEG_LENGTH"]);
    //                    strResetType = StringUtil.CStr(htlSegment_Row["RESET_TYPE"]).ToUpper();
    //                    strPadChar = StringUtil.CStr(htlSegment_Row["PAD_CHAR"]);

    //                    if (strSegType.Equals("FIX"))  //固定文字串
    //                    {
    //                        strFixString = StringUtil.CStr(htlSegment_Row["SEG_FIXSTRING"]);
    //                        //Logger.Info("取得固定文字:" + strFixString);
    //                        strSegment = strFixString;
    //                    }
    //                    else if (strSegType.Equals("YYYY"))  //4碼西元年
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Year(DateUtil.Now()), "0", 4, "L");
    //                    }
    //                    else if (strSegType.Equals("YY") || strSegType.Equals("[Y]")) //西元年未 2碼
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Year(DateUtil.Now()).Substring(2, 2), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("CYY")) //中華民國年
    //                    {
    //                        int intYear = StringUtil.CInt(DateUtil.Year(DateUtil.Now())) - 0x777;
    //                        strSegment = StringUtil.Pad(StringUtil.CStr(intYear), "0", 3, "L");
    //                    }
    //                    else if (strSegType.Equals("MM") || strSegType.Equals("[M]")) //月份(會補左0)
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Month(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("DD")) //日期
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Day(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("HH")) //HOURSE
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Hour(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("NN")) //分
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Minute(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("SS")) //秒
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Second(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("NUMBER"))
    //                    {
    //                        strSerialNo = GetSerialNo(conn, PA_Sheet_Type, htlSegment_Row);
    //                        //Logger.Info("取得的序號=" + strSerialNo);
    //                        strSegment = StringUtil.Pad(strSerialNo, strPadChar, StringUtil.CInt(strSegLength), "L");
    //                    }
    //                    else
    //                    {
    //                        //Logger.Info("指定的區段類型錯誤, 請檢查SeqSegment Table的內容");
    //                        throw new SegmentTypeDefineException();
    //                    }

    //                    strResult = strResult + strSegment;
    //                }
    //                return strResult;
    //            }
    //            //Logger.Info("指定的單據類型在SeqSegment Table裡找不到任何資料");
    //            throw new SheetTypeNotFoundException();
    //        }
    //        catch (Exception ex)
    //        {
    //            //Logger.Error(ex.Message, ex);
    //            throw ex;
    //        }
    //    }

    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    private static string GetSerialNo(OracleConnection conn, string PA_Sheet_Type, Hashtable PA_Segment)
    //    {
    //        string strResult = "";
    //        OracleTransaction tx = null;
    //        StringBuilder strSql = null;
    //        Hashtable htlSeqNo = null;
    //        string strUpdateDate = "";
    //        string strCurrTime = "";
    //        bool blnDiff = false;
    //        string strNextValue = "";
    //        string strResetType = StringUtil.CStr(PA_Segment["RESET_TYPE"]).ToUpper();
    //        string strStartValue = StringUtil.CStr(PA_Segment["START_VALUE"]);
    //        string strEndValue = StringUtil.CStr(PA_Segment["END_VALUE"]);
    //        string strSegLength = StringUtil.CStr(PA_Segment["SEG_LENGTH"]);
    //        string strPadChar = StringUtil.CStr(PA_Segment["PAD_CHAR"]);

    //        try
    //        {
    //            tx = conn.BeginTransaction(IsolationLevel.Serializable);

    //            strSql = new StringBuilder();
    //            strSql.Append("Select * From SeqNo ");
    //            strSql.Append(" Where Sheet_Type=" + OracleDBUtil.SqlStr(PA_Sheet_Type));


    //            //Logger.Info("判斷是否已有記錄=" + strSql.ToString());

    //            htlSeqNo = OracleDBUtil.GetData(tx, strSql.ToString());
    //            if (htlSeqNo.Count > 0)
    //            {
    //                //Logger.Info("原本有記錄");
    //                Hashtable htlSeqNo_Row = (Hashtable)htlSeqNo[0];
    //                strUpdateDate = DateUtil.DtStr(StringUtil.CStr(htlSeqNo_Row["UPDATE_DATE"]), "YMDHNS");
    //                strCurrTime = DateUtil.Now();

    //                if (strResetType.Equals("BYDAY"))
    //                {
    //                    string strDATE1 = DateUtil.Year(strUpdateDate) + "/" + DateUtil.Month(strUpdateDate) + "/" + DateUtil.Day(strUpdateDate);
    //                    string strDATE2 = DateUtil.Year(strCurrTime) + "/" + DateUtil.Month(strCurrTime) + "/" + DateUtil.Day(strCurrTime);

    //                    if (!strDATE1.Equals(strDATE2))
    //                    {
    //                        blnDiff = true;
    //                    }
    //                }
    //                else if (strResetType.Equals("BYMONTH"))
    //                {
    //                    string strDATE1 = DateUtil.Year(strUpdateDate) + "/" + DateUtil.Month(strUpdateDate);
    //                    string strDATE2 = DateUtil.Year(strCurrTime) + "/" + DateUtil.Month(strCurrTime);
    //                    if (!strDATE1.Equals(strDATE2))
    //                    {
    //                        blnDiff = true;
    //                    }
    //                }
    //                else if (strResetType.Equals("BYYEAR"))
    //                {
    //                    string strDATE1 = DateUtil.Year(strUpdateDate);
    //                    string strDATE2 = DateUtil.Year(strCurrTime);
    //                    if (!strDATE1.Equals(strDATE2))
    //                    {
    //                        blnDiff = true;
    //                    }
    //                }
    //                else if (strResetType.Equals("LOOP"))
    //                {
    //                    //必須再檢查是否有 超出範圍
    //                    blnDiff = false;
    //                }
    //                else
    //                {
    //                    //Logger.Info("SeqNo指定的ResetType錯誤, 請檢查SeqNo Table的內容");
    //                    throw new ResetTypeDefineException();
    //                }

    //                strSql = new StringBuilder();
    //                strSql.Append("Update SeqNo Set ");
    //                if (blnDiff)
    //                {
    //                    //Logger.Info("要Reset序號");
    //                    strStartValue = GetStartValue(PA_Segment);
    //                    strNextValue = StringUtil.CStr(StringUtil.CInt(strStartValue) + 1);
    //                    strResult = strStartValue;
    //                    strSql.Append(" SERIAL_NO  = " + strNextValue + ", ");
    //                }
    //                else
    //                {
    //                    //Logger.Info("不用Reset序號");
    //                    strResult = StringUtil.CStr(htlSeqNo_Row["SERIAL_NO"]);
    //                    strSql.Append(" SERIAL_NO  = SERIAL_NO + 1, ");
    //                }

    //                strSql.Append(" Update_Date= sysdate ");
    //                strSql.Append(" Where Sheet_Type=" + OracleDBUtil.SqlStr(PA_Sheet_Type));

    //                OracleHelper.ExecuteNonQuery(tx, CommandType.Text, strSql.ToString());
    //            }
    //            else
    //            {
    //                //系統中沒有序號的 log

    //                strSql = new StringBuilder();
    //                strStartValue = GetStartValue(PA_Segment);
    //                strNextValue = StringUtil.CStr(StringUtil.CInt(strStartValue) + 1);
    //                strSql.Append("Insert Into SeqNo(Sheet_Type,Serial_No,Update_Date) Values(");
    //                strSql.Append(OracleDBUtil.SqlStr(PA_Sheet_Type) + ", ");
    //                strSql.Append(strNextValue + ", ");
    //                strSql.Append("sysdate ) ");

    //                OracleHelper.ExecuteNonQuery(tx, CommandType.Text, strSql.ToString());
    //                strResult = strStartValue;
    //            }
    //            tx.Commit();
    //        }
    //        catch (Exception ex)
    //        {
    //            try
    //            {
    //                tx.Rollback();
    //            }
    //            catch (Exception sqlEx)
    //            {
    //                //Logger.Error(sqlEx);
    //                throw sqlEx;
    //            }
    //            //Logger.Error(ex.Message, ex);
    //            throw ex;
    //        }
    //        finally
    //        {
    //            tx = null;
    //        }

    //        return strResult;

    //    }

    //    public static String getSerNoPatent(String PA_SNPatent)
    //    {
    //        try
    //        {
    //            //1. [ to [[
    //            PA_SNPatent = PA_SNPatent.Replace("[", "[[");
    //            //2. ][[ to ],[
    //            PA_SNPatent = PA_SNPatent.Replace("][[", "],[");
    //            //3. 如果第1及第2字元為[[則改成[
    //            String strTemp = PA_SNPatent.Substring(0, 2);
    //            if (strTemp == "[[") PA_SNPatent = PA_SNPatent.Substring(1);

    //            //4. [[ to ,[
    //            PA_SNPatent = PA_SNPatent.Replace("[[", ",[");

    //            //5. ] to ]]
    //            PA_SNPatent = PA_SNPatent.Replace("]", "]]");


    //            //6. ]],[ to ],[
    //            PA_SNPatent = PA_SNPatent.Replace("]],[", "],[");


    //            //7. 如果最後第1及第2字元為]]則改成]
    //            String strTemp1 = PA_SNPatent.Substring(PA_SNPatent.Length - 2);
    //            if (strTemp1 == "]]") PA_SNPatent = PA_SNPatent.Substring(0, (PA_SNPatent.Length - 1));

    //            //8. ]] to ]
    //            PA_SNPatent = PA_SNPatent.Replace("]]", "]");
    //        }
    //        catch (Exception ex)
    //        { throw ex; }

    //        return PA_SNPatent;
    //    }

    //    private static string GetEndValue(Hashtable PA_Segment, ArrayList arySegmentValue)
    //    {
    //        string strEndValue = "";
    //        try
    //        {
    //            strEndValue = StringUtil.CStr(PA_Segment["END_VALUE"]);

    //        }
    //        catch (Exception ex)
    //        {
    //            //Logger.Error(ex.Message, ex);
    //            throw ex;
    //        }
    //        return strEndValue;
    //    }

    //    private static string GetStartValue(Hashtable PA_Segment)
    //    {
    //        string strStartValue = "";
    //        try
    //        {
    //            strStartValue = StringUtil.CStr(PA_Segment["START_VALUE"]);
    //            if (strStartValue.Equals(""))
    //            {
    //                return "1";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Logger.Error(ex.Message, ex);
    //            throw ex;
    //        }
    //        return strStartValue;
    //    }



    //}


    //public sealed class Store_SerialNo
    //{
    //    private static readonly string CONNECT_STRING = OracleDBUtil.GetDecryptConnectionString("Connection_String");

    //    private static Store_SerialNo singleton;

    //    //Singleton instance
    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    public static Store_SerialNo GetInstance()
    //    {
    //        if (singleton == null)
    //        {
    //            singleton = new Store_SerialNo();
    //        }
    //        return singleton;
    //    }


    //    //取號程式
    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    public static string GenNo(string PA_Sheet_Type, string STORE_NO, string MACHINE_ID)
    //    {
    //        OracleConnection conn;
    //        StringBuilder strSql = null;
    //        string strResult = "";
    //        string strSegment = "";
    //        Hashtable htlSegment = null;
    //        string strSegType = "";
    //        string strFixString = "";
    //        string strSegLength = "";
    //        string strPadChar = "";
    //        string strSerialNo = "";
    //        string strResetType = "";
    //        string strSTORE_NO = "";
    //        try
    //        {
    //            conn = OracleDBUtil.GetConnectionByConnString(CONNECT_STRING);

    //            strSql = new StringBuilder();
    //            strSql.Append("Select * From STORE_SeqSegment ");
    //            strSql.Append(" Where Sheet_Type=" + OracleDBUtil.SqlStr(PA_Sheet_Type));
    //            strSql.Append(" AND STORE_NO=" + OracleDBUtil.SqlStr(STORE_NO));
    //            strSql.Append(" Order By Seq_No ");

    //            htlSegment = OracleDBUtil.GetData(conn, strSql.ToString());

    //            if (htlSegment.Count > 0)
    //            {
    //                for (int i = 0; i < htlSegment.Count; i++)
    //                {
    //                    Hashtable htlSegment_Row = (Hashtable)htlSegment[i];
    //                    strSegType = StringUtil.CStr(htlSegment_Row["SEG_TYPE"]).ToUpper();
    //                    strSegLength = StringUtil.CStr(htlSegment_Row["SEG_LENGTH"]);
    //                    strResetType = StringUtil.CStr(htlSegment_Row["RESET_TYPE"]).ToUpper();
    //                    strPadChar = StringUtil.CStr(htlSegment_Row["PAD_CHAR"]);
    //                    strSTORE_NO = StringUtil.CStr(htlSegment_Row["STORE_NO"]);

    //                    if (strSegType.Equals("FIX"))  //固定文字串
    //                    {
    //                        strFixString = StringUtil.CStr(htlSegment_Row["SEG_FIXSTRING"]);
    //                        //Logger.Info("取得固定文字:" + strFixString);
    //                        strSegment = strFixString;
    //                    }
    //                    else if (strSegType.Equals("YYYY"))  //4碼西元年
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Year(DateUtil.Now()), "0", 4, "L");
    //                    }
    //                    else if (strSegType.Equals("YY") || strSegType.Equals("[Y]")) //西元年未 2碼
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Year(DateUtil.Now()).Substring(2, 2), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("CYY")) //中華民國年
    //                    {
    //                        int intYear = StringUtil.CInt(DateUtil.Year(DateUtil.Now())) - 0x777;
    //                        strSegment = StringUtil.Pad(StringUtil.CStr(intYear), "0", 3, "L");
    //                    }
    //                    else if (strSegType.Equals("MM") || strSegType.Equals("[M]")) //月份(會補左0)
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Month(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("DD")) //日期
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Day(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("HH")) //HOURSE
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Hour(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("NN")) //分
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Minute(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("SS")) //秒
    //                    {
    //                        strSegment = StringUtil.Pad(DateUtil.Second(DateUtil.Now()), "0", 2, "L");
    //                    }
    //                    else if (strSegType.Equals("NUMBER"))
    //                    {
    //                        strSerialNo = GetSerialNo(conn, PA_Sheet_Type, htlSegment_Row, STORE_NO);
    //                        //Logger.Info("取得的序號=" + strSerialNo);
    //                        strSegment = StringUtil.Pad(strSerialNo, strPadChar, StringUtil.CInt(strSegLength), "L");
    //                    }
    //                    else
    //                    {
    //                        //Logger.Info("指定的區段類型錯誤, 請檢查SeqSegment Table的內容");
    //                        throw new SegmentTypeDefineException();
    //                    }

    //                    strResult = strResult + strSegment;
    //                }
    //                return STORE_NO + "-" + MACHINE_ID + "-" + strResult;
    //            }
    //            //Logger.Info("指定的單據類型在SeqSegment Table裡找不到任何資料");
    //            //throw new SheetTypeNotFoundException();
    //            return "指定的單據類型在SeqSegment Table裡找不到任何資料";
    //        }
    //        catch (Exception ex)
    //        {
    //            //Logger.Error(ex.Message, ex);
    //            throw ex;
    //        }
    //    }

    //    [MethodImpl(MethodImplOptions.Synchronized)]
    //    private static string GetSerialNo(OracleConnection conn, string PA_Sheet_Type, Hashtable PA_Segment, string STORE_NO)
    //    {
    //        string strResult = "";
    //        OracleTransaction tx = null;
    //        StringBuilder strSql = null;
    //        Hashtable htlSeqNo = null;
    //        string strUpdateDate = "";
    //        string strCurrTime = "";
    //        bool blnDiff = false;
    //        string strNextValue = "";
    //        string strResetType = StringUtil.CStr(PA_Segment["RESET_TYPE"]).ToUpper();
    //        string strStartValue = StringUtil.CStr(PA_Segment["START_VALUE"]);
    //        string strEndValue = StringUtil.CStr(PA_Segment["END_VALUE"]);
    //        string strSegLength = StringUtil.CStr(PA_Segment["SEG_LENGTH"]);
    //        string strPadChar = StringUtil.CStr(PA_Segment["PAD_CHAR"]);

    //        try
    //        {
    //            tx = conn.BeginTransaction(IsolationLevel.Serializable);

    //            strSql = new StringBuilder();
    //            strSql.Append("Select * From STORE_SeqNo ");
    //            strSql.Append(" Where Sheet_Type=" + OracleDBUtil.SqlStr(PA_Sheet_Type));
    //            strSql.Append(" AND STORE_NO=" + OracleDBUtil.SqlStr(STORE_NO));

    //            //Logger.Info("判斷是否已有記錄=" + strSql.ToString());

    //            htlSeqNo = OracleDBUtil.GetData(tx, strSql.ToString());
    //            if (htlSeqNo.Count > 0)
    //            {
    //                //Logger.Info("原本有記錄");
    //                Hashtable htlSeqNo_Row = (Hashtable)htlSeqNo[0];
    //                strUpdateDate = DateUtil.DtStr(StringUtil.CStr(htlSeqNo_Row["UPDATE_DATE"]), "YMDHNS");
    //                strCurrTime = DateUtil.Now();

    //                if (strResetType.Equals("BYDAY"))
    //                {
    //                    string strDATE1 = DateUtil.Year(strUpdateDate) + "/" + DateUtil.Month(strUpdateDate) + "/" + DateUtil.Day(strUpdateDate);
    //                    string strDATE2 = DateUtil.Year(strCurrTime) + "/" + DateUtil.Month(strCurrTime) + "/" + DateUtil.Day(strCurrTime);

    //                    if (!strDATE1.Equals(strDATE2))
    //                    {
    //                        blnDiff = true;
    //                    }
    //                }
    //                else if (strResetType.Equals("BYMONTH"))
    //                {
    //                    string strDATE1 = DateUtil.Year(strUpdateDate) + "/" + DateUtil.Month(strUpdateDate);
    //                    string strDATE2 = DateUtil.Year(strCurrTime) + "/" + DateUtil.Month(strCurrTime);
    //                    if (!strDATE1.Equals(strDATE2))
    //                    {
    //                        blnDiff = true;
    //                    }
    //                }
    //                else if (strResetType.Equals("BYYEAR"))
    //                {
    //                    string strDATE1 = DateUtil.Year(strUpdateDate);
    //                    string strDATE2 = DateUtil.Year(strCurrTime);
    //                    if (!strDATE1.Equals(strDATE2))
    //                    {
    //                        blnDiff = true;
    //                    }
    //                }
    //                else if (strResetType.Equals("LOOP"))
    //                {
    //                    //必須再檢查是否有 超出範圍
    //                    blnDiff = false;
    //                }
    //                else
    //                {
    //                    //Logger.Info("SeqNo指定的ResetType錯誤, 請檢查SeqNo Table的內容");
    //                    throw new ResetTypeDefineException();
    //                }

    //                strSql = new StringBuilder();
    //                strSql.Append("Update STORE_SeqNo Set ");
    //                if (blnDiff)
    //                {
    //                    //Logger.Info("要Reset序號");
    //                    strStartValue = GetStartValue(PA_Segment);
    //                    strNextValue = StringUtil.CStr(StringUtil.CInt(strStartValue) + 1);
    //                    strResult = strStartValue;
    //                    strSql.Append(" SERIAL_NO  = " + strNextValue + ", ");
    //                }
    //                else
    //                {
    //                    //Logger.Info("不用Reset序號");
    //                    strResult = StringUtil.CStr(htlSeqNo_Row["SERIAL_NO"]);
    //                    strSql.Append(" SERIAL_NO  = SERIAL_NO + 1, ");
    //                }

    //                strSql.Append(" Update_Date= sysdate ");
    //                strSql.Append(" Where Sheet_Type=" + OracleDBUtil.SqlStr(PA_Sheet_Type));
    //                strSql.Append(" AND STORE_NO=" + OracleDBUtil.SqlStr(STORE_NO));
    //                OracleHelper.ExecuteNonQuery(tx, CommandType.Text, strSql.ToString());
    //            }
    //            else
    //            {
    //                //系統中沒有序號的 log

    //                strSql = new StringBuilder();
    //                strStartValue = GetStartValue(PA_Segment);
    //                strNextValue = StringUtil.CStr(StringUtil.CInt(strStartValue) + 1);
    //                strSql.Append("Insert Into STORE_SeqNo(Sheet_Type,Serial_No,STORE_NO,Update_Date) Values(");
    //                strSql.Append(OracleDBUtil.SqlStr(PA_Sheet_Type) + ", ");
    //                strSql.Append(strNextValue + ", ");
    //                strSql.Append(STORE_NO + ", ");
    //                strSql.Append("sysdate ) ");

    //                OracleHelper.ExecuteNonQuery(tx, CommandType.Text, strSql.ToString());
    //                strResult = strStartValue;
    //            }
    //            tx.Commit();
    //        }
    //        catch (Exception ex)
    //        {
    //            try
    //            {
    //                tx.Rollback();
    //            }
    //            catch (Exception sqlEx)
    //            {
    //                //Logger.Error(sqlEx);
    //                throw sqlEx;
    //            }
    //            //Logger.Error(ex.Message, ex);
    //            throw ex;
    //        }
    //        finally
    //        {
    //            tx = null;
    //        }

    //        return strResult;

    //    }

    //    public static String getSerNoPatent(String PA_SNPatent)
    //    {
    //        try
    //        {
    //            //1. [ to [[
    //            PA_SNPatent = PA_SNPatent.Replace("[", "[[");
    //            //2. ][[ to ],[
    //            PA_SNPatent = PA_SNPatent.Replace("][[", "],[");
    //            //3. 如果第1及第2字元為[[則改成[
    //            String strTemp = PA_SNPatent.Substring(0, 2);
    //            if (strTemp == "[[") PA_SNPatent = PA_SNPatent.Substring(1);

    //            //4. [[ to ,[
    //            PA_SNPatent = PA_SNPatent.Replace("[[", ",[");

    //            //5. ] to ]]
    //            PA_SNPatent = PA_SNPatent.Replace("]", "]]");


    //            //6. ]],[ to ],[
    //            PA_SNPatent = PA_SNPatent.Replace("]],[", "],[");


    //            //7. 如果最後第1及第2字元為]]則改成]
    //            String strTemp1 = PA_SNPatent.Substring(PA_SNPatent.Length - 2);
    //            if (strTemp1 == "]]") PA_SNPatent = PA_SNPatent.Substring(0, (PA_SNPatent.Length - 1));

    //            //8. ]] to ]
    //            PA_SNPatent = PA_SNPatent.Replace("]]", "]");
    //        }
    //        catch (Exception ex)
    //        { throw ex; }

    //        return PA_SNPatent;
    //    }

    //    private static string GetEndValue(Hashtable PA_Segment, ArrayList arySegmentValue)
    //    {
    //        string strEndValue = "";
    //        try
    //        {
    //            strEndValue = StringUtil.CStr(PA_Segment["END_VALUE"]);

    //        }
    //        catch (Exception ex)
    //        {
    //            //Logger.Error(ex.Message, ex);
    //            throw ex;
    //        }
    //        return strEndValue;
    //    }

    //    private static string GetStartValue(Hashtable PA_Segment)
    //    {
    //        string strStartValue = "";
    //        try
    //        {
    //            strStartValue = StringUtil.CStr(PA_Segment["START_VALUE"]);
    //            if (strStartValue.Equals(""))
    //            {
    //                return "1";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //Logger.Error(ex.Message, ex);
    //            throw ex;
    //        }
    //        return strStartValue;
    //    }



    //}

    /// <summary>
    /// SALE取號用針對不同的門市來取號
    /// 用StoredProcedure
    /// </summary>
    public sealed class Store_SerialNo
    {
        private static readonly string CONNECT_STRING = OracleDBUtil.GetDecryptConnectionString("Connection_String");

        private static Store_SerialNo singleton;

        //Singleton instance
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Store_SerialNo GetInstance()
        {
            if (singleton == null)
            {
                singleton = new Store_SerialNo();
            }
            return singleton;
        }


        //取號程式
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GenNo(string PA_Sheet_Type, string STORE_NO, string MACHINE_ID)
        {
            OracleConnection conn;
            string strResult = "";
            OracleCommand oraCmd = null;
            OracleTransaction objTX = null;
            try
            {
                conn = OracleDBUtil.GetConnectionByConnString(CONNECT_STRING);
                objTX = conn.BeginTransaction(IsolationLevel.Serializable);
                oraCmd = new OracleCommand("PK_GET_GEN_NO.SP_GET_STORE_GEN_NO");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("PA_Sheet_Type", OracleType.VarChar, 2000)).Value = PA_Sheet_Type;
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = STORE_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_MACHINE_ID", OracleType.VarChar, 2000)).Value = MACHINE_ID;
                oraCmd.Parameters.Add(new OracleParameter("strResult", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                strResult = oraCmd.Parameters["strResult"].Value.ToString();
                objTX.Commit();
                return strResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    /// <summary>
    /// 一般的取號
    /// /// 用StoredProcedure
    /// </summary>
    public sealed class SerialNo
    {
        private static readonly string CONNECT_STRING = OracleDBUtil.GetDecryptConnectionString("Connection_String");

        private static SerialNo singleton;

        //Singleton instance
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SerialNo GetInstance()
        {
            if (singleton == null)
            {
                singleton = new SerialNo();
            }
            return singleton;
        }


        //取號程式
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string GenNo(string PA_Sheet_Type)
        {
            OracleConnection conn;
            string strResult = "";
            OracleCommand oraCmd = null;
            OracleTransaction objTX = null;
            try
            {
                conn = OracleDBUtil.GetConnectionByConnString(CONNECT_STRING);
                objTX = conn.BeginTransaction(IsolationLevel.Serializable);
                oraCmd = new OracleCommand("PK_GET_GEN_NO.SP_GET_GEN_NO");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("PA_Sheet_Type", OracleType.VarChar, 2000)).Value = PA_Sheet_Type;
                oraCmd.Parameters.Add(new OracleParameter("strResult", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                strResult = oraCmd.Parameters["strResult"].Value.ToString();
                objTX.Commit();
                return strResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


