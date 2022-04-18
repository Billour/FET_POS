using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxGridView;

public partial class VSS_INV_INV12 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReceivedDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ModifiedBy.Text = "12345 王大寶";
        ModifiedDate.Text = DateTime.Now.ToString();
        ReceivingNoteNumber.Text = "NPO" + DateTime.Today.ToString("yyyyMMdd") + "001";
    }

    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();

    }

    protected void bindgvMaster()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();

        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        
        dtMaster.Columns.Add("商品料號", typeof(string));
        dtMaster.Columns.Add("單位", typeof(string));
        dtMaster.Columns.Add("數量", typeof(string));
        dtMaster.Columns.Add("總金額", typeof(int));
        


        for (int i = 1; i < 12; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            
            dtMasterRow["商品料號"] = "10010010" + i;
            dtMasterRow["單位"] = "商品名稱" + i;
            dtMasterRow["數量"] = 2 * (i + 1);
            dtMasterRow["總金額"] = 1000 * (i + 1);
            

            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }

    protected void btnNew_Click(object sender, EventArgs e)
    { 
        gvMaster.AddNewRow();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        gvMaster.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();

    }


}
