using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_DIS11_DIS11 : System.Web.UI.Page
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
        dtResult.Columns.Add("商品類別", typeof(string));
        
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("維護人員", typeof(string));



        DataRow NewRow = dtResult.NewRow();
        NewRow["商品類別"] = "商品類別1";
       
        NewRow["更新日期"] = DateTime.Now.ToShortDateString();
        NewRow["維護人員"] = "維護人員1";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品類別"] = "商品類別2";
     
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
