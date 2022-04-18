using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class VSS_SAL_SAL04 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData();
            bindDetailData();
            bindCheckOutData();

            lbInvoiceNo.Text = "TX98765457";
        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData();
        gvDetail.DataSource = dtResult;
        gvDetail.DataBind();
    }
    protected void bindEmptyCheckOutData()
    {
        //DataTable dtResult = new DataTable();

        //gvCheckOut.DataSource = dtResult;
        //gvCheckOut.DataBind();
    }
    protected void bindCheckOutData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getCheckOutData();
        gvCheckOut.DataSource = dtResult;
        gvCheckOut.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("類別", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        dtResult.Columns.Add("單價", typeof(string));
        dtResult.Columns.Add("總價", typeof(string));
        dtResult.Columns.Add("IMEI", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["類別"] = "促";
        NewRow["商品編號"] = "N33IRWI87";
        NewRow["商品名稱"] = "Nokia-5800";
        NewRow["數量"] = "1";
        NewRow["單價"] = "7500";
        NewRow["總價"] = "7500";
        NewRow["IMEI"] = "IMEI1";
        NewRow["促銷名稱"] = "哈拉900方案";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["類別"] = "促";
        NewRow["商品編號"] = "W213IOP4IOP";
        NewRow["商品名稱"] = "無線基地台";
        NewRow["數量"] = "1";
        NewRow["單價"] = "3500";
        NewRow["總價"] = "3500";
        NewRow["IMEI"] = "IMEI2";
        NewRow["促銷名稱"] = "大雙網寬頻專案";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["類別"] = "單";
        NewRow["商品編號"] = "WI124I3809";
        NewRow["商品名稱"] = "無線網卡";
        NewRow["數量"] = "1";
        NewRow["單價"] = "1290";
        NewRow["總價"] = "1290";
        NewRow["IMEI"] = "";
        NewRow["促銷名稱"] = "";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable getDetailData()
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

    protected void btnVoid_Click(object sender, EventArgs e)
    {
       Label6.Visible = true;
        Label5.Text = "已作廢";
    }

}
