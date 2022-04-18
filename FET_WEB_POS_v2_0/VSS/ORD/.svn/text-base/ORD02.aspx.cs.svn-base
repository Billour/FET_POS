using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_ORD02_ORD02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       getGvMaster();
      // div1.Visible = true;
    }

    protected void getGvMaster()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
      //  gvMaster.DataSource = dtGvMaster;
      //  gvMaster.DataBind();

        gvMasterDV.DataSource = dtGvMaster;
        gvMasterDV.DataBind();
    }


    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("訂單日期", typeof(string));
        dtMaster.Columns.Add("區域", typeof(string));
        dtMaster.Columns.Add("門市編號", typeof(string));
        dtMaster.Columns.Add("門市名稱", typeof(string));
        dtMaster.Columns.Add("訂單編號", typeof(string));
        dtMaster.Columns.Add("預訂單號", typeof(string));


        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("人員", typeof(string));
        dtMaster.Columns.Add("日期", typeof(string));

        string[] array1 = { "北一區", "北二區", "中一區" };
        string[] array2 = { "完成", "作廢" };
        //for (int i = 0; i < 10; i++)
        //{
        //    DataRow dtMasterRow = dtMaster.NewRow();
        //    dtMasterRow["訂單日期"] = "2010/07/12";
        //    dtMasterRow["區域"] = array1[i % 3];
        //    dtMasterRow["門市編號"] = "GA0000" + i;
        //    dtMasterRow["門市名稱"] = "門市" + i;
        //    dtMasterRow["訂單編號"] = "A0000" + i;
        //    dtMasterRow["狀態"] = array2[i % 2];
        //    dtMasterRow["人員"] = "Jackey";
        //    dtMasterRow["日期"] = "2010/07/12";
        //    dtMaster.Rows.Add(dtMasterRow);
        //}

        DataRow dtMasterRow = dtMaster.NewRow();
        dtMasterRow["訂單日期"] = "2010/09/16";
        dtMasterRow["區域"] = "北一區"; 
        dtMasterRow["門市編號"] = "2101";
        dtMasterRow["門市名稱"] = "永和";
        dtMasterRow["訂單編號"] = "SO2101-1007010";
        dtMasterRow["預訂單號"] = "PR2101-1007011";

        dtMasterRow["狀態"] = "已成單";
        dtMasterRow["人員"] = "Jackey";
        dtMasterRow["日期"] = "2010/09/16" + " " + DateTime.Now.ToString("HH:mm:ss");
        dtMaster.Rows.Add(dtMasterRow);

        //2
        dtMasterRow = dtMaster.NewRow();
        dtMasterRow["訂單日期"] = "2010/09/16";
        dtMasterRow["區域"] = "北一區";
        dtMasterRow["門市編號"] = "2110";
        dtMasterRow["門市名稱"] = "萬和";
        dtMasterRow["訂單編號"] = "SO2110-1007011";
        dtMasterRow["預訂單號"] = "";

        dtMasterRow["狀態"] = "已成單";
        dtMasterRow["人員"] = "Jackey";
        dtMasterRow["日期"] = "2010/09/16" + " " + DateTime.Now.ToString("HH:mm:ss");
        dtMaster.Rows.Add(dtMasterRow);

        return dtMaster;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getGvMaster();
      //  div1.Visible = true;

    }

    #region 分頁相關 (共用)
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
    #endregion


}
