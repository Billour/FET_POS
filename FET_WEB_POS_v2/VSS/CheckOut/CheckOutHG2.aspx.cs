using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;
using System.Collections.Specialized;

public partial class VSS_CheckOut_CheckOutHG2 : BasePage
{
    int RequestTotalAmount
    {
        get
        {
            string TOTAL_AMOUNT = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "TOTAL_AMOUNT")
                    {
                        TOTAL_AMOUNT = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            int r = Convert.ToInt32(TOTAL_AMOUNT == "" ? "0" : TOTAL_AMOUNT);
            return r;
        }
    }

    string strTRAN_DATE
    {
        get
        {
            string TRAN_DATE = "";

            //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
            if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
            {
                NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
                foreach (string key in qscoll.AllKeys)
                {
                    if (key == "TRAN_DATE")
                    {
                        TRAN_DATE = string.Join(",", qscoll.GetValues(key));
                        break;
                    }
                }
            }

            return TRAN_DATE;

            //string r = Request["TRAN_DATE"] == null ? "" : StringUtil.CStr(Request["TRAN_DATE"]);
            //if (r == null)
            //{
            //    r = "";
            //}
            //return r;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //ClientScript.RegisterHiddenField("hiddenSTORENO", logMsg.STORENO);
        this.hidStoreNo.Value = logMsg.STORENO;

        if (!IsPostBack)
        {
            ClientScript.RegisterHiddenField("TOTAL_AMOUNT", StringUtil.CStr(RequestTotalAmount));
        }

    }

    private void BindGridViewData()
    {
        if (ViewState["dtConvert"] == null)
        {
            DataTable dt = GetGridViewEmptyData();

            #region 取得一般兌點通則
            DataTable dtConvert = new SAL01_Facade().getConvertAction(strTRAN_DATE, "2");
            foreach (DataRow drConvert in dtConvert.Rows)
            {
                DataRow NewRow = dt.NewRow();
                NewRow["折扣料號"] = StringUtil.CStr(drConvert["DISCOUNT_CODE"]);
                NewRow["折扣料號"] = StringUtil.CStr(drConvert["DISCOUNT_NAME"]);
                NewRow["活動代碼"] = StringUtil.CStr(drConvert["CONVERT_NO"]);
                NewRow["項目名稱"] = StringUtil.CStr(drConvert["CONVERT_NAME"]);
                NewRow["兌換點數"] = StringUtil.CStr(drConvert["DIVIDABLE_POINT"]);
                NewRow["兌換金額"] = StringUtil.CStr(drConvert["CONVERT_CURRENCY"]);
                NewRow["數量"] = "0";
                dt.Rows.Add(NewRow);
            }
            #endregion  取得一般兌點通則

            CalPoint(ref dt); //剩餘點數兌換

            ViewState["dtConvert"] = dt;
        }

        bindGridViewData();
    }

    private void CalPoint(ref DataTable dt)
    {
        int SumPoint = 0;   //總兌換點數
        int SumAmount = 0;  //總兌換金額
        int happyGoPoint = Convert.ToInt32(string.IsNullOrEmpty(this.lblHG_LEFT_POINT.Text) ? "0" : this.lblHG_LEFT_POINT.Text);
        happyGoPoint = ChangeQuantity(happyGoPoint, ref dt, ref SumPoint, ref SumAmount);

        this.hdSumPoint.Text = StringUtil.CStr(SumPoint);
        this.hdSumAmount.Text = StringUtil.CStr(SumAmount);
        this.lblHG_REDEEM_POINT.Text = StringUtil.CStr(SumAmount) + "元(" + StringUtil.CStr(SumPoint) + "點)";
        this.lblHG_LEFT_POINT.Text = StringUtil.CStr((Convert.ToInt32(lblHG_LEFT_POINT.Text) - SumPoint));
    }

    private int ChangeQuantity(int happyGoPoint, ref DataTable dt, ref int SumPoint, ref int SumAmount)
    {
        if (happyGoPoint > 0 && happyGoPoint > SumPoint && RequestTotalAmount > SumAmount && dt.Rows.Count > 0)
        {
            //先找出最高級兌換條件，取金額/點數最大值的條件
            double maxCp = 0.0;
            double valueCp = 0.0;
            int chgPoint = 0;
            int chgAmt = 0;
            int selIndex = 0;
            int ind = 0;

            foreach (DataRow dr in dt.Rows)
            {
                int iPoint = Convert.ToInt32(string.IsNullOrEmpty(StringUtil.CStr(dr["兌換點數"])) ? "0" : StringUtil.CStr(dr["兌換點數"]));
                int iAmt = Convert.ToInt32(string.IsNullOrEmpty(StringUtil.CStr(dr["兌換金額"])) ? "0" : StringUtil.CStr(dr["兌換金額"]));
                //當兌換點數條件不大於目前剩餘HappyGo點數且兌換金額加上以兌換金額不大於應收總金額時，將此條件列入考慮
                if (iPoint != 0 && iPoint <= happyGoPoint - SumPoint && iAmt + SumAmount <= RequestTotalAmount)
                    valueCp = (double)iAmt / iPoint;
                else
                    valueCp = 0.0;

                if (valueCp > maxCp)
                {
                    maxCp = valueCp;
                    chgPoint = iPoint;
                    chgAmt = iAmt;
                    selIndex = ind;
                }
                ind++;
            }

            if (chgPoint > 0)
            {
                int usedCnt = (int)((happyGoPoint - SumPoint) / chgPoint);
                int changedAmt = usedCnt * chgAmt;
                if (changedAmt + SumAmount > RequestTotalAmount)
                {
                    if (chgAmt > 0)
                        usedCnt = (int)((RequestTotalAmount - SumAmount) / chgAmt);
                    else
                        usedCnt = 0;
                    changedAmt = usedCnt * chgAmt;
                }

                SumAmount += changedAmt;
                SumPoint += usedCnt * chgPoint;
                dt.Rows[selIndex]["數量"] = usedCnt;
            }
        }
        return happyGoPoint;
    }

    protected void bindGridViewData()
    {
        DataTable dtResult = new DataTable();
        dtResult = ViewState["dtConvert"] as DataTable;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetGridViewEmptyData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("活動代碼", typeof(string));
        dtResult.Columns.Add("項目名稱", typeof(string));
        dtResult.Columns.Add("兌換點數", typeof(string));
        dtResult.Columns.Add("兌換金額", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        return dtResult;
    }

    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.NextStepIndex)
        {
            case 1:  //第一畫面刷卡成功後，載入可折抵的商品項目
                this.lblHG_CAR_NO.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_CAR_NO").Text;
                this.lblHG_LEFT_POINT.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text;
                this.lblOriginalHG_LEFT_POINT.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text;
                BindGridViewData();
                break;
            case 2:  //填寫完兌換數量後，轉資料到最後頁面
                int Point = 0, leftPoint = 0;
                if (this.Wizard1.FindChildControl<ASPxTextBox>("hdSumPoint").Text != "")
                    Point = Convert.ToInt32(this.Wizard1.FindChildControl<ASPxTextBox>("hdSumPoint").Text);
                leftPoint = Convert.ToInt32(this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text);
                this.lblFinHG_LEFT_POINT.Text = StringUtil.CStr((leftPoint - Point));     //剩餘點數

                this.lblFinHG_CAR_NO.Text = this.lblHG_CAR_NO.Text;             //HG卡號
                //this.lblFinHG_LEFT_POINT.Text = this.lblHG_LEFT_POINT.Text;     //剩餘點數
                this.lblFinHG_REDEEM_POINT.Text = this.lblHG_REDEEM_POINT.Text; //兌換明細
                this.hdFinSumPoint.Text = this.hdSumPoint.Text;                 //總兌換點數
                this.hdFinSumAmount.Text = this.hdSumAmount.Text;               //總兌換金額
                break;
            default:
                break;
        }
    }

    protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.NextStepIndex)
        {
            case 1:
                this.lblOriginalHG_LEFT_POINT.Text = this.Wizard1.FindChildControl<ASPxTextBox>("hdHG_LEFT_POINT").Text;
                this.lblHG_LEFT_POINT.Text = this.lblFinHG_LEFT_POINT.Text;
                break;
            default:
                break;
        }
        
    }

}
