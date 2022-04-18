using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_INV_INV23 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //gvMaster_RowEditing();
            bindMasterData();
            //lblReturnDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("倉別名稱", typeof(string));
        dtResult.Columns.Add("可銷售", typeof(string));
        dtResult.Columns.Add("檢核IMEI", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        return dtResult;
    }
    protected void bindMasterData()
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
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("倉別名稱", typeof(string));
        dtMaster.Columns.Add("可銷售", typeof(string));
        dtMaster.Columns.Add("檢核IMEI", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));


        //string[] array1 = { "有效", "失效" };
        //string[] array2 = { "不控管", "銷售時記錄", "銷售時確認", "庫存異動控管" };
        string[] array3 = { "銷售倉", "展示倉", "租賃倉", "維修倉" };

        for (int i = 1; i < 5; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            // dtMasterRow["狀態"] = array1[i % 2];
            dtMasterRow["倉別名稱"] = array3[i % 4];
            //dtMasterRow["檢核IMEI"] = array2[i % 4];
            dtMasterRow["更新人員"] = "王小明";
            dtMasterRow["更新日期"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        gvMaster.CancelEdit();
        e.Cancel = true;
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
        //e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.
    }

}
