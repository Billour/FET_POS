using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_SAL_SAL01_searchProductNo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnCommit.Visible = true;
        btnCancel.Visible = true;
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("庫存", typeof(string));
        dtResult.Columns.Add("價格", typeof(string));

        return dtResult;
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
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("庫存", typeof(string));
        dtResult.Columns.Add("價格", typeof(string)); 

        for (int i = 0; i < 9; i++)
        {
            DataRow NewRow = dtResult.NewRow(); 
            NewRow["商品編號"] = "A0000" + i;
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["庫存"] = i+3;
            NewRow["價格"] = 1000*(i+1); 
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }
}
