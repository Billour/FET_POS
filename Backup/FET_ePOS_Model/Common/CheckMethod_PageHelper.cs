using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

namespace FET.POS.Model.Common
{
    /// <summary>
    /// true 表示沒有任何錯誤
    /// false 表示經驗證後有錯誤
    /// ErrorMessage 失敗訊息
    /// 程式進入點 CheckMethod_PageHelper
    /// </summary>
    public class CheckMethod_PageHelper : ValidatBase
    {        
        /// <summary>
        /// 初始化程式
        /// </summary>
        public CheckMethod_PageHelper()
        {
            State.IsSuccess = false;
            State.ErrorMessage  = "";
        }

        /// <summary>
        /// 是否為數字(除去特殊字元^\d+(\.)?\d*$)
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^\d+(\.)?\d*$");
            State.IsSuccess = r.IsMatch(strNumber);
            return State.IsSuccess;
        }

        /// <summary>
        /// 時間起迄是否重覆
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public bool CheckSdateEdate<T1, T2>(T1 sDate, T2 eDate)
        {
            DateValidat<T1, T2> ipDate = new DateValidat<T1 , T2 >( sDate , eDate);            
            if (ipDate.CheckDate())
            {
                return ipDate.State.IsSuccess;
            }else {
                State.ErrorMessage = ipDate.State.ErrorMessage;
                return ipDate.State.IsSuccess;
            }
        }

        /// <summary>
        /// 共用ckeck INV統一編號 Function
        /// </summary>
        /// <param name="strINV"></param>
        /// <returns></returns>
        public string CheckINVFunction(string strINV)
        {
            string strInfo = "";
            Advtek.Utility.Check_ID subCheck = new Check_ID();
            //如果不足8碼
            if (subCheck.Check_TW_INV(strINV) == 3)
            {
                strInfo = "false";
            }
            try
            {
                Int32.Parse(strINV);
            }
            catch //(Exception ex)
            {
                strInfo = "notInteger";
            }
            return strInfo;
        }

        /// <summary>
        /// 廠商代碼是否已存在DB內
        /// </summary>
        /// <returns></returns>
        public bool IsSuppNOInDB(string NO)
        {
            bool strInfo = false;
            DataTable dt = new CON02_PageHelper().QuerySuppNO(NO, "");
            if (dt.Rows.Count > 0)
            {
                strInfo = true;
            }
            return strInfo;
        }
    }

    /// <summary>
    /// 驗證的狀態
    /// </summary>
    public class ValidatState
    {
        /// <summary>
        /// 驗證是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 驗證失敗的失誤訊息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// 時間驗證
    /// </summary>
    public class DateValidat<T1, T2>
    {
        private string strDateNow = System.DateTime.Now.ToString("yyyy/MM/dd");
        private ValidatState StateSub = new ValidatState();

        public DateTime DateNow;
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        
        /// <summary>
        ///  驗證的狀態
        /// </summary>
        public ValidatState State { get { return StateSub; } set { StateSub = value; } }

        /// <summary>
        /// 建構子
        /// </summary>
        public DateValidat()
        {
            initial();
        }
        public DateValidat(T1 SDateInp, T2 EDateInp)
        {
            try
            {
                if (!string.IsNullOrEmpty(SDateInp.ToString()))
                    this.SDate = System.DateTime.Parse(SDateInp.ToString());
                else
                    this.SDate = System.DateTime.MinValue;

                if (!string.IsNullOrEmpty(EDateInp.ToString()))
                    this.EDate = System.DateTime.Parse(EDateInp.ToString());
                else
                    this.EDate = System.DateTime.MaxValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            initial();
        }
        private void initial()
        {
            DateNow = System.DateTime.Parse(strDateNow);
        }
        
        /// <summary>
        /// 驗證SDate、EDate
        /// </summary>
        /// <returns></returns>
        public bool CheckDate()
        {//以下Check 算法禁止亂改，修改前先查明其影響
            State.IsSuccess = true;
            if (SDate <= DateNow)
            {
                State.ErrorMessage += " 起始日期須大於系統日期!!";
                State.IsSuccess = false;
                return State.IsSuccess;
            }

            if (!string.IsNullOrEmpty(EDate.ToString()))
            {
                //if (EDate < DateNow)
                //{
                //    State.ErrorMessage += "結束日期須大於或等於系統日期!!";
                //    State.IsSuccess = false;
                //    return State.IsSuccess;
                //}
                if (SDate > EDate)
                {
                    State.ErrorMessage += "起始日期須小於結束日期!!";
                    State.IsSuccess = false;
                    return State.IsSuccess;
                }
            }
            return State.IsSuccess;
        }

        //例子
        public T testG<T>(T inputData)
        {
            return inputData;
        }
    }

    /// <summary>
    /// 驗證的基底 Base Class
    /// </summary>
    public class ValidatBase
    {
        private ValidatState StateSub = new ValidatState();

        /// <summary>
        /// 執行驗證的名稱
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 執行驗證的型態
        /// </summary>
        public Type ProcessType { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 驗證次數
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 驗證失敗次數
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        ///  驗證的狀態
        /// </summary>
        public ValidatState State { get { return StateSub; } set { StateSub = value; } }
    }

    /// <summary>
    /// 傳入驗證的參數
    /// </summary>
    public class CheckData : ValidatBase
    {
        private string sDateSub;
        private string eDateSub;

       /// <summary>
       /// 起日期
       /// </summary>
        public string SDate
        {
            get { return sDateSub.Trim(); }

            set { sDateSub = value; }
        }
        
        /// <summary>
        /// 迄日期
        /// </summary>
        public string EDate
        {
            get { return eDateSub.Trim(); }

            set { eDateSub = value; }
        }

        /// <summary>
        /// 商品料號
        /// </summary>
        public string ProductNO { get; set; }

        /// <summary>
        /// 信用卡別
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 要取得Session 的名稱
        /// </summary>
        public string SessionName { get; set; }
       
        /// <summary>
        /// 判斷是否是編輯資料
        /// </summary>
        public bool IsNewRowEditing { get; set; }

        /// <summary>
        /// PK 數值
        /// </summary>
        public string PkData { get; set; }

        /// <summary>
        /// Pk 名字
        /// </summary>
        public string PkName { get; set; }

        /// <summary>
        /// 賠償項目
        /// </summary>
        public string IndItemName { get; set; }

    }
}
