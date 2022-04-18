using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;

public partial class VSS_OPT_OPT10 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grid.DataSource = GetEmptyDataTable();
            grid.DataBind();
        }
    }

    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("商品類別", typeof(string));
        dtMaster.Columns.Add("單位", typeof(string));
        dtMaster.Columns.Add("單機價格", typeof(string));
        dtMaster.Columns.Add("有效日期1", typeof(string));
        dtMaster.Columns.Add("有效日期2", typeof(string));
        dtMaster.Columns.Add("扣庫存", typeof(bool));
        dtMaster.Columns.Add("檢核IMEI", typeof(string));
        dtMaster.Columns.Add("自訂價格", typeof(bool));
        dtMaster.Columns.Add("科目1", typeof(string));
        dtMaster.Columns.Add("科目2", typeof(string));
        dtMaster.Columns.Add("科目3", typeof(string));
        dtMaster.Columns.Add("科目4", typeof(string));
        dtMaster.Columns.Add("科目5", typeof(string));
        dtMaster.Columns.Add("科目6", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        return dtMaster;
    }

    private DataTable GetMasterData( )
    {
        DataTable dtMaster = GetEmptyDataTable();
       
        string[] array1 = { "3G Handset", "SIM Card", "3G Accessory" };
        string[] array2 = { "失效", "已過期", "未生效" };
        string[] array3 = { "不控管", "銷售時記錄", "銷售時確認", "庫存異動控管" };
        for (int i = 0; i <= 10; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["狀態"] = array2[i % 3];
            dtMasterRow["商品編號"] = "A00100" + i;
            dtMasterRow["商品名稱"] = "商品名稱" + i;
            dtMasterRow["商品類別"] = array1[i % 3];
            dtMasterRow["單位"] = i + "個";
            dtMasterRow["單機價格"] = i+"1234"+i;
            dtMasterRow["有效日期1"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd");
            dtMasterRow["有效日期2"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd");
            dtMasterRow["扣庫存"] = i % 2 == 0;
            dtMasterRow["檢核IMEI"] = array3[i % 4];
            dtMasterRow["自訂價格"] = i % 2 == 0;
            dtMasterRow["科目1"] = i+"1";
            dtMasterRow["科目2"] = i + "12";
            dtMasterRow["科目3"] = i + "123";
            dtMasterRow["科目4"] = i + "12345";
            dtMasterRow["科目5"] = i + "123";
            dtMasterRow["科目6"] = i + "123";


            dtMasterRow["更新人員"] = "王小明";

            dtMasterRow["更新日期"] = Convert.ToDateTime("2010/07/20" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");

            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();                
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.CancelEdit();
        e.Cancel = true;
        BindData();
    }

    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        e.Cancel = true;
        grid.CancelEdit();
        BindData();
    }
}
