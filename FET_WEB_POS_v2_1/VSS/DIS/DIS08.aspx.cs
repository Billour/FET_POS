using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
using DevExpress.Web.ASPxGridView;
public partial class VSS_DIS_DIS08 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData();

        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("商品分類", typeof(string));
        dtResult.Columns.Add("商品料號(起)", typeof(string));
        dtResult.Columns.Add("商品名稱(起)", typeof(string));
        dtResult.Columns.Add("商品料號(迄)", typeof(string));
        dtResult.Columns.Add("商品名稱(迄)", typeof(string));
        dtResult.Columns.Add("生效日(起)", typeof(string));
        dtResult.Columns.Add("生效日(迄)", typeof(string));
        dtResult.Columns.Add("轉換值", typeof(int));
        dtResult.Columns.Add("維護人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));


        for (int i = 1; i < 16; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] =  i.ToString();
            NewRow["商品分類"] = "2G";
            NewRow["商品料號(起)"] = "123";
            NewRow["商品名稱(起)"] = "ABC";
            NewRow["商品料號(迄)"] = "456";
            NewRow["商品名稱(迄)"] = "DEF";
            NewRow["生效日(起)"] = "2010/07/13";
            NewRow["生效日(迄)"] = "2010/07/13";
            NewRow["轉換值"] = i%2==0? 800:500;
            NewRow["維護人員"] = "Max";
            NewRow["更新日期"] = "2010/1/1 12:30:26 PM";

            dtResult.Rows.Add(NewRow);
        }

        return dtResult;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }
}
