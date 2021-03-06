using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_CON15_CON15 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindMasterData();
        }
        else
        {
            DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            if (dtGvMaster != null)
            {
                gvMaster.DataSource = dtGvMaster;
                gvMaster.DataBind();
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
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
        dtResult.Columns.Add("盤點單號", typeof(string));
        dtResult.Columns.Add("盤點日期", typeof(string));
        
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("盤點狀態", typeof(string));
        dtResult.Columns.Add("人員", typeof(string));
        dtResult.Columns.Add("日期", typeof(string));

        //string[] array1 = { "盤點狀態1", "盤點狀態2" };
       
            DataRow NewRow = dtResult.NewRow();
            NewRow["盤點單號"] = "STC2010072801";
            NewRow["盤點日期"] = "2010/07/28";
           
            NewRow["門市編號"] = "AC00001";
            NewRow["門市名稱"] = "中和門市";
            NewRow["盤點狀態"] = "未盤點";
            NewRow["人員"] = "王小明";
            NewRow["日期"] = "2010/07/28";
            dtResult.Rows.Add(NewRow);
       
        return dtResult;
    }

    //protected void bindDetailData()
    //{
    //    DataTable dtResult = new DataTable();
    //    //dtResult = getDetailData();
    //    //gvDetail.Visible = true;
    //    //DIVdetail.Visible = true;
    //    //gvDetail.DataSource = dtResult;
    //    //gvDetail.DataBind();
    //}

    //private DataTable getDetailData()
    //{
    //    DataTable dtDetail = new DataTable();
    //    dtDetail.Columns.Clear();
    //    dtDetail.Columns.Add("廠商編號", typeof(string));
    //    dtDetail.Columns.Add("廠商名稱", typeof(string));
    //    dtDetail.Columns.Add("商品編號", typeof(string));
    //    dtDetail.Columns.Add("商品名稱", typeof(string));
    //    dtDetail.Columns.Add("庫存量", typeof(string));
    //    dtDetail.Columns.Add("門市盤點量", typeof(string));
    //    dtDetail.Columns.Add("盤差量", typeof(string));

    //    for (int i = 0; i < 5; i++)
    //    {
    //        DataRow dtMasterRow = dtDetail.NewRow();
    //        dtMasterRow["廠商編號"] = "AC001";
    //        dtMasterRow["廠商名稱"] = "全虹";
    //        dtMasterRow["商品編號"] = "AC12654933"+i;
    //        dtMasterRow["商品名稱"] = "商品名稱" + i;
    //        dtMasterRow["庫存量"] = 5 + 2 * i;
    //        dtMasterRow["門市盤點量"] = 5 + 2 * i;
    //        dtMasterRow["盤差量"] = "0";
    //        dtDetail.Rows.Add(dtMasterRow);
    //    }
    //    return dtDetail;
    //}
}
