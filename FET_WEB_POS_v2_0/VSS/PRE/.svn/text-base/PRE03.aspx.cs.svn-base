using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_PRE03_PRE03 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            bindEmptyMasterData();

        }
    }
    protected void bindEmptyMasterData()
    {
        DataTable dtResult = new DataTable();
    
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
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
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        


        DataRow NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "商品料號1";
        NewRow["商品名稱"] = "商品名稱1";
        
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "商品料號2";
        NewRow["商品名稱"] = "商品名稱2";
        
        dtResult.Rows.Add(NewRow);




        return dtResult;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        gvMaster.Visible = true;
        bindMasterData();
    }
}
