using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_DIS08_DIS08 : System.Web.UI.Page
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
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("補貼金額", typeof(string));
        dtResult.Columns.Add("生效日(起)", typeof(string));
        dtResult.Columns.Add("生效日(迄)", typeof(string));
       
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("維護人員", typeof(string));



        DataRow NewRow = dtResult.NewRow();
        NewRow["商品編號"] = "商品編號1";
        NewRow["商品名稱"] = "商品名稱1";
      
        NewRow["生效日(起)"] = "2010/05/01";
        NewRow["生效日(迄)"] = "2010/05/11";

        NewRow["更新日期"] = DateTime.Now.ToShortDateString();
        NewRow["維護人員"] = "維護人員1";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品編號"] = "商品編號2";
        NewRow["商品名稱"] = "商品名稱1";
      
        NewRow["生效日(起)"] = "2010/06/22";
        NewRow["生效日(迄)"] = "2010/07/02";
       
        NewRow["更新日期"] = DateTime.Now.ToShortDateString();
        NewRow["維護人員"] = "維護人員2";
        dtResult.Rows.Add(NewRow);





        return dtResult;
    }






    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.div1.Visible = true;
        this.div2.Visible = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

    }
}
