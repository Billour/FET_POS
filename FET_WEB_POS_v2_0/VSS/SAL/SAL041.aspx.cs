using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;


public partial class VSS_SAL041_SAL041 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("交易日期", typeof(string));
        dtResult.Columns.Add("交易序號", typeof(string));
        dtResult.Columns.Add("機台", typeof(string));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("發票號碼", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));
        dtResult.Columns.Add("付款方式", typeof(string));

        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = GetEmptyDataTable();

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/07/08";
        NewRow["交易序號"] = "AK00001";
        NewRow["機台"] = "機台A";
        NewRow["客戶門號"] = "客戶門號A";
        NewRow["發票號碼"] = "AB12345678";
        NewRow["金額"] = "6000";
        NewRow["付款方式"] = "現金,信用卡,禮券,金融卡";

        NewRow["更新人員"] = "王大寶";
        NewRow["更新日期"] = "2010/07/08";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/07/06";
        NewRow["交易序號"] = "2101-01-100806001";
        NewRow["機台"] = "機台B";
        NewRow["客戶門號"] = "客戶門號B";
        NewRow["發票號碼"] = "AB12345679";
        NewRow["金額"] = "5000";
        NewRow["付款方式"] =  "現金,信用卡,禮券,金融卡";

        NewRow["更新人員"] = "王大寶";
        NewRow["更新日期"] = "2010/07/06";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "3";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/07/08";
        NewRow["交易序號"] = "KAD000012";
        NewRow["機台"] = "機台C";
        NewRow["客戶門號"] = "客戶門號C";
        NewRow["發票號碼"] = "AB12345680";
        NewRow["金額"] = "4300";
        NewRow["付款方式"] = "現金";
        
        NewRow["更新人員"] = "王大寶";
        NewRow["更新日期"] = "2010/07/08";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "4";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/08/05";
        NewRow["交易序號"] = "KAD000012";
        NewRow["機台"] = "機台C";
        NewRow["客戶門號"] = "客戶門號C";
        NewRow["發票號碼"] = "AB12345681";
        NewRow["金額"] = "6500";
        NewRow["付款方式"] =   "現金,禮券";

        NewRow["更新人員"] = "王大寶";
        NewRow["更新日期"] = "2010/08/03";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "5";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/08/05";
        NewRow["交易序號"] = "KAD000012";
        NewRow["機台"] = "機台C";
        NewRow["客戶門號"] = "客戶門號C";
        NewRow["發票號碼"] = "AB12345682";
        NewRow["金額"] = "7500";
        NewRow["付款方式"] = "信用卡";

        NewRow["更新人員"] = "王大寶";
        NewRow["更新日期"] = "2010/08/06";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "6";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/08/05";
        NewRow["交易序號"] = "KAD000012";
        NewRow["機台"] = "機台C";
        NewRow["客戶門號"] = "客戶門號C";
        NewRow["發票號碼"] = "AB12345682";
        NewRow["金額"] = "7500";
        NewRow["付款方式"] = "信用卡";

        NewRow["更新人員"] = "王大寶";
        NewRow["更新日期"] = "2010/08/06";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    
        Button31.Visible = true;
    }


    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //RadioButton radio = e.Row.FindControl("radioChoose") as RadioButton;
            //radio.Attributes["name"] = "<STRONG><FONT color=#ff0000>SameRadio</FONT></STRONG>";
        }
    }

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
       ASPxGridView grid = sender as ASPxGridView;
       gvMaster.DataSource = getMasterData();
       gvMaster.DataBind();
    }
}
