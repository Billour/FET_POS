using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;


public partial class VSS_SA02_SA02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
       {
          bindEmptyData();
       }
    }
    protected void bindEmptyData()
    {
       DataTable dtResult = new DataTable();

       gvMaster.DataSource = dtResult;
       gvMaster.DataBind();
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("交易日期", typeof(string));
        dtResult.Columns.Add("交易序號", typeof(string));
        dtResult.Columns.Add("機台", typeof(string));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("發票號碼", typeof(string));
        dtResult.Columns.Add("金額", typeof(int));
        dtResult.Columns.Add("付款方式", typeof(string));
        //dtResult.Columns.Add("現金", typeof(int));
        //dtResult.Columns.Add("信用卡", typeof(string));
        //dtResult.Columns.Add("禮券", typeof(string));
        //dtResult.Columns.Add("金融卡", typeof(string));
        //dtResult.Columns.Add("HG", typeof(string)); 
        dtResult.Columns.Add("銷售人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/07/08";
        NewRow["交易序號"] = "2101-01-100700018";
        NewRow["機台"] = "機台2";
        NewRow["客戶門號"] = "0942999123";
        NewRow["發票號碼"] = "AB12345678";
        NewRow["金額"] = 4300;
        NewRow["付款方式"] = "現金,信用卡,禮券,金融卡";
        //NewRow["現金"] = 1300;
        //NewRow["信用卡"] = "1000";
        //NewRow["禮券"] = "1000";
        //NewRow["金融卡"] = "1000";
        //NewRow["HG"] = "0";
        NewRow["銷售人員"] = "王大寶";
        NewRow["更新日期"] = "2010/07/08";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/07/16";
        NewRow["交易序號"] = "2101-01-100702345  ";
        NewRow["機台"] = "機台1";
        NewRow["客戶門號"] = "0955822566";
        NewRow["發票號碼"] = "AB12345679";
        NewRow["金額"] = 5000;
        NewRow["付款方式"] = "現金,信用卡,禮券,金融卡";
        //NewRow["現金"] = 1000;
        //NewRow["信用卡"] = "1000";
        //NewRow["禮券"] = "2000";
        //NewRow["金融卡"] = "500";
        //NewRow["HG"] = "500"; 
        NewRow["銷售人員"] = "王大寶";
        NewRow["更新日期"] = "2010/07/16";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "3";
        NewRow["狀態"] = "已退貨";
        NewRow["交易日期"] = "2010/07/18";
        NewRow["交易序號"] = "2101-01-100709801";
        NewRow["機台"] = "機台1";
        NewRow["客戶門號"] = "0986356992";
        NewRow["發票號碼"] = "AB12345680";
        NewRow["金額"] = 6000;
        NewRow["付款方式"] = "現金";
        //NewRow["現金"] = 6000;
        //NewRow["信用卡"] = "0";
        //NewRow["禮券"] = "0";
        //NewRow["金融卡"] = "0";
        //NewRow["HG"] = "0";
        NewRow["銷售人員"] = "王大寶";
        NewRow["更新日期"] = "2010/07/18";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "4";
        NewRow["狀態"] = "已換貨";
        NewRow["交易日期"] = "2010/08/02";
        NewRow["交易序號"] = "2101-01-100800099";
        NewRow["機台"] = "機台2";
        NewRow["客戶門號"] = "0987555888";
        NewRow["發票號碼"] = "AB12345681";
        NewRow["金額"] = 6500;
        NewRow["付款方式"] = "現金,禮券";
        //NewRow["現金"] = 5000;
        //NewRow["信用卡"] = "0";
        //NewRow["禮券"] = "1500";
        //NewRow["金融卡"] = "0";
        //NewRow["HG"] = "0";
        NewRow["銷售人員"] = "李家駿";
        NewRow["更新日期"] = "2010/08/02";
        dtResult.Rows.Add(NewRow);


        NewRow = dtResult.NewRow();
        NewRow["項次"] = "5";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = "2010/08/05";
        NewRow["交易序號"] = "2101-01-100800897";
        NewRow["機台"] = "機台1";
        NewRow["客戶門號"] = "0987658908";
        NewRow["發票號碼"] = "AB12345682";
        NewRow["金額"] = 7500;
        NewRow["付款方式"] = "信用卡";
        //NewRow["現金"] = 0;
       // NewRow["信用卡"] = 7500;
        //NewRow["禮券"] = "0";
        //NewRow["金融卡"] = "0";
        //NewRow["HG"] = "0";
        NewRow["銷售人員"] = "李家駿";
        NewRow["更新日期"] = "2010/08/05";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "6";
        NewRow["狀態"] = "已結帳";
        NewRow["交易日期"] = System.DateTime.Now.ToString("yyyy/MM/dd");
        NewRow["交易序號"] = "2101-01-100801006";
        NewRow["機台"] = "機台1";
        NewRow["客戶門號"] = "0987658908";
        NewRow["發票號碼"] = "AB12345683";
        NewRow["金額"] = 7500;
        NewRow["付款方式"] = "信用卡";
        //NewRow["現金"] = 0;
        //NewRow["信用卡"] = 7500;
        //NewRow["禮券"] = "0";
        //NewRow["金融卡"] = "0";
        //NewRow["HG"] = "0";
        NewRow["銷售人員"] = "李家駿";
        NewRow["更新日期"] = System.DateTime.Now.ToString("yyyy/MM/dd");
        dtResult.Rows.Add(NewRow);


        return dtResult;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        Button1.Visible = true;
        Button21.Visible = true;
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
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //此函式可共用
        GridView gridview = sender as GridView;
        gridview.PageIndex = e.NewPageIndex;

        DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
        gridview.DataSource = dt;
        gridview.DataBind();
    }
    protected void btnGoToIndex_Click(object sender, EventArgs e)
    {
        //此函式可共用
        GridView gridview = (sender as Button).Parent.Parent.Parent.Parent as GridView;
        TextBox textbox = (sender as Button).Parent.FindControl("tbGoToIndex") as TextBox;
        string strIndex = textbox.Text.Trim();
        int index = 0;
        if (int.TryParse(strIndex, out index))
        {
            index = index - 1;
            if (index >= 0 && index <= gridview.PageCount - 1)
            {
                gridview.PageIndex = index;
                DataTable dt = ViewState[gridview.ID] as DataTable ?? null;//GridView的資料要存於ViewState以方便共用此Function
                gridview.DataSource = dt;
                gridview.DataBind();
            }
            else
            {
                textbox.Text = string.Empty;
            }
        }
        else
        {
            textbox.Text = string.Empty;
        }
    }
}
