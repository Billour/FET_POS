using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_DIS_DIS02 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            

            string[] ttl = { "ALL", "一般", "舊機回收", " 收賃", "店長折扣", "HappyGo折扣", "贈品", "加價購" };
            Category.DataSource = ttl;
            Category.DataBind();
            Category.SelectedIndex = 0;
        }
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
        ViewState["gvMaster"] = dtResult;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("折扣料號", typeof(string));
        dtResult.Columns.Add("折扣名稱", typeof(string));
        dtResult.Columns.Add("開始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));
        dtResult.Columns.Add("折扣金額", typeof(string));
        dtResult.Columns.Add("折扣比率", typeof(string));
        dtResult.Columns.Add("折扣上限次數", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));



        for (int i = 0; i < 12; i++)
        {
            //for (int j = 500; j < 12; j++)
            //{
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i + 1;
            NewRow["折扣料號"] = "20020020" + i;
            //NewRow["折扣名稱"] = "賓賓有禮折扣" + j + "元";
            NewRow["折扣名稱"] = "賓賓有禮折扣500元";
            NewRow["開始日期"] = "2010/05/01";
            NewRow["結束日期"] = "2010/11/30";
            NewRow["折扣金額"] = "500";
            NewRow["折扣比率"] = i + "%";
            NewRow["折扣上限次數"] = i + 5;
            NewRow["更新人員"] = "王小明";
            NewRow["更新日期"] = DateTime.Now.ToShortDateString();
            dtResult.Rows.Add(NewRow);
            //}
        }





        return dtResult;
    }


    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }



    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }



}
