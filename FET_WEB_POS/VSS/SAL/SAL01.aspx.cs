using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data ;
using Advtek.Utility;

public partial class VSS_SA01_SA01 :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbStatus.Text = "00 未結帳";
            Label3.Text = DateTime.Now.ToString("yyyy/MM/dd");
            lbUpdate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            //bindMasterData(1);
            //bindCheckOutData();
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("類別", typeof(string));
            dtResult.Columns.Add("品名規格", typeof(string));
            dtResult.Columns.Add("數量", typeof(string));
            dtResult.Columns.Add("單價", typeof(string));
            dtResult.Columns.Add("總價", typeof(string));
            dtResult.Columns.Add("促銷名稱", typeof(string));
            gvMaster.DataSource = dtResult;
            gvMaster.DataBind();

            DataTable dtPay = new DataTable();
            dtPay.Columns.Add("付款方式", typeof(string));
            dtPay.Columns.Add("金額", typeof(string));
            dtPay.Columns.Add("付款明細", typeof(string));

            gvCheckOut.DataSource = dtPay;
            gvCheckOut.DataBind();
        }
    }

    //protected void bindMasterData(int TempCount)
    //{
    //    DataTable dtResult = new DataTable();
    //    dtResult = getMasterData(TempCount);
    //    ViewState["gvMaster"] = dtResult;
    //    gvMaster.DataSource = dtResult;
    //    gvMaster.DataBind();
    //}

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindGvDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGvDetailData();
        gvDetail.DataSource = dtResult;
        gvDetail.DataBind();
    }
    protected void bindCheckOutData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getCheckOutData();
        gvCheckOut.DataSource = dtResult;
        gvCheckOut.DataBind();
    }

    //private DataTable getMasterData(int TempCount)
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("類別", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        dtResult.Columns.Add("單價", typeof(string));
        dtResult.Columns.Add("總價", typeof(string));
        dtResult.Columns.Add("CHECK", typeof(string));
        dtResult.Columns.Add("IMEI", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));

        //for (int i = 0; i < TempCount; i++)
        //{
        //    DataRow NewRow = dtResult.NewRow();
        //    NewRow["類別"] = "促";
        //    NewRow["商品編號"] = "125458700";
        //    NewRow["商品名稱"] = "哈拉900方案 (1/2) - 5800手機";
        //    NewRow["數量"] = "1";
        //    NewRow["單價"] = "6500";
        //    NewRow["總價"] = "6500";
        //    NewRow["IMEI"] = "65005465118642773";
        //    NewRow["備註"] = "";
        //    dtResult.Rows.Add(NewRow);
        //}

        DataRow NewRow = dtResult.NewRow();
        NewRow["類別"] = "促";
        NewRow["商品編號"] = "KJ489593";
        NewRow["商品名稱"] = "哈拉900方案 -NOKIA 5800手機";
        NewRow["數量"] = "1";
        NewRow["單價"] = "6500";
        NewRow["總價"] = "6500";
        NewRow["CHECK"] = 1;
        NewRow["IMEI"] = "YU1234567";
        NewRow["促銷名稱"] = "";
        dtResult.Rows.Add(NewRow);


        NewRow = dtResult.NewRow();
        NewRow["類別"] = "單";
        NewRow["商品編號"] = "UT3827494";
        NewRow["商品名稱"] = "無線網卡";
        NewRow["數量"] = "1";
        NewRow["單價"] = "1290";
        NewRow["總價"] = "1290";
        NewRow["CHECK"] = 0;
        NewRow["IMEI"] = "";
        NewRow["促銷名稱"] = "";
        dtResult.Rows.Add(NewRow);
        return dtResult;
    }


    private DataTable getCheckOutData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("付款方式", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));
        dtResult.Columns.Add("付款明細", typeof(string));  

        DataRow NewRow = dtResult.NewRow();
        NewRow["付款方式"] = "信用卡";
        NewRow["金額"] = "7290";
        NewRow["付款明細"] = "卡號: 1234567890123456, 分期: 無"; 
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["付款方式"] = "現金";
        NewRow["金額"] = "7000";
        NewRow["付款明細"] = "";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable getGvDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        dtResult.Columns.Add("單價", typeof(string));
        dtResult.Columns.Add("總價", typeof(string));
        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["折扣料號"] = "折扣料號1";
        NewRow["折扣名稱"] = "折扣名稱1";
        NewRow["數量"] = "1";
        NewRow["單價"] = "1000";
        NewRow["總價"] = "1000";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["折扣料號"] = "折扣料號2";
        NewRow["折扣名稱"] = "折扣名稱2";
        NewRow["數量"] = "2";
        NewRow["單價"] = "200";
        NewRow["總價"] = "200";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox cbIMEI = e.Row.FindControl("cbIMEI") as CheckBox;
            cbIMEI.Attributes["onclick"] = "imeicheckbox(this);";
        }
    }
    protected void btnItemAdd_Click(object sender, EventArgs e)
    {
        bindMasterData();
        Label2.Text = "14290元";
    }
    protected void btnCash_Click(object sender, EventArgs e)
    {
        bindCheckOutData();
        bindGvDetailData();
        Label8.Visible = true;
        lbStatus.Text = "40 進行結帳";
        lbPayAmount.Text = "14290元";
        lbChange.Text = "0元";
        //btnCash.Enabled = false;
    }
    protected void btnOrderCheckOut_Click(object sender, EventArgs e)
    {

        btnOrderCheckOut.Enabled = false;

        lbTSNo.Text = "2101-01-100912345";
        lbStatus.Text = "50 已結帳";
       // lbInvoiceNo.Text = "FX001222445";
        if (tbInvoiceNo.Text != "")
        {
            if (tbInvoiceNo.Text.Length != 8)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "body", "<script language='javascript'>window.open('SAL01_checkIDNumber.aspx','window','width=400',height='400');</script>", false);
            }
        }
        else
        {
            //Page.ClientScript.RegisterClientScriptBlock
            
        }
    }
}
