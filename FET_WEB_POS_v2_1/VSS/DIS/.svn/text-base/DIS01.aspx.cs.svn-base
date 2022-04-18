using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.HtmlControls;

public partial class VSS_DIS_DIS01 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindDataProduct();
        BindDataStore();
        BindDataPromotion();
        BindDataCostCenter();
        BindViewData1();
        BindViewData2();

        
    }

    protected void btnAdd1_Click(object sender, EventArgs e)
    {
        gvMaster1.AddNewRow();
    }
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        ASPxGridView1.AddNewRow();
    }
    protected void btnAdd3_Click(object sender, EventArgs e)
    {
        ASPxGridView10.AddNewRow();
    }
    protected void btnAdd4_Click(object sender, EventArgs e)
    {
        ASPxGridView3.AddNewRow();
    }
    protected void btnAdd5_Click(object sender, EventArgs e)
    {
        ASPxGridView4.AddNewRow();
    }
    protected void btnAdd6_Click(object sender, EventArgs e)
    {
        ASPxGridView5.AddNewRow();
    }

    protected void BindDataProduct()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridViewDataProduct();
        gvMaster1.DataSource = dtResult;
        gvMaster1.DataBind();

    }
    private DataTable getGridViewDataProduct()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "100100100";
        NewRow["商品名稱"] = "iPhone4";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["商品料號"] = "100100210";
        NewRow["商品名稱"] = "HTC Desire HD";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }
    protected void BindDataStore()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridViewDataStore();
        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();

    }

    private DataTable getGridViewDataStore()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("區域別", typeof(string));
        dtResult.Columns.Add("折扣上限次數", typeof(int));

        for (int i = 1; i < 4; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["門市編號"] = "SA100000" + (i + 1).ToString();
            dtMasterRow["門市名稱"] = "門市名稱_" + (i + 1).ToString();
            dtMasterRow["折扣上限次數"] = 3;

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void BindDataPromotion()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridViewDataPromotion();
        ASPxGridView10.DataSource = dtResult;
        ASPxGridView10.DataBind();

    }

    private DataTable getGridViewDataPromotion()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        
        for (int i = 1; i < 5; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["促銷代號"] = "210" + (i + 1).ToString();
            dtMasterRow["促銷名稱"] = "促銷名稱_" + (i + 1).ToString();

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void BindDataCostCenter()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridViewDataCostCenter();
        ASPxGridView3.DataSource = dtResult;
        ASPxGridView3.DataBind();

    }

    private DataTable getGridViewDataCostCenter()
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("成本中心", typeof(string));
        dtResult.Columns.Add("商品分類", typeof(string));
        dtResult.Columns.Add("會計科目", typeof(string));
        dtResult.Columns.Add("金額", typeof(int));
        dtResult.Columns.Add("備註", typeof(string));

        
        string[] ary1 = { "手機類", "配件類", "3C", "新啟用", "續約", "通路" };

        for (int i = 1; i < 6; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["成本中心"] = "6100" + (i + 1).ToString();
            dtMasterRow["商品分類"] = ary1[i % 5];
            dtMasterRow["會計科目"] = "5400" + (i + 1).ToString();
            dtMasterRow["金額"] = 500;

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewDataCustomer1(int count)
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("客戶對象", typeof(string));
        dtResult.Columns.Add("ARPB金額(起)", typeof(int));
        dtResult.Columns.Add("ARPB金額(訖)", typeof(int));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["客戶對象"] };

        for (int i = 0; i < count; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["客戶對象"] = i.ToString();
            dtMasterRow["ARPB金額(起)"] = 500 + i;
            dtMasterRow["ARPB金額(訖)"] = 500 + i + 10;

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewDataCustomer2(int count)
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("客戶門號", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["客戶門號"] };

        for (int i = 0; i < count; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["客戶門號"] = "090000000" + i;

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }


    protected void BindViewData1()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridViewData1();
        ASPxGridView4.DataSource = dtResult;
        ASPxGridView4.DataBind();

    }

    private DataTable getGridViewData1()
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["商品名稱"] };

        DataRow dtMasterRow1 = dtResult.NewRow();
        dtMasterRow1["商品料號"] = "";
        dtMasterRow1["商品名稱"] = "公仔(紅)";
        dtResult.Rows.Add(dtMasterRow1);

        DataRow dtMasterRow2 = dtResult.NewRow();
        dtMasterRow2["商品料號"] = "";
        dtMasterRow2["商品名稱"] = "公仔(藍)";
        dtResult.Rows.Add(dtMasterRow2);

        return dtResult;
    }
    protected void BindViewData2()
    {
        DataTable dtResult = new DataTable();
        dtResult = getGridViewData2();
        ASPxGridView5.DataSource = dtResult;
        ASPxGridView5.DataBind();

    }
    private DataTable getGridViewData2()
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("折扣金額", typeof(int));
        dtResult.Columns.Add("單機價", typeof(int));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["商品名稱"] };

        DataRow dtMasterRow1 = dtResult.NewRow();
        dtMasterRow1["商品料號"] = "";
        dtMasterRow1["商品名稱"] = "公仔(紅)";
        dtMasterRow1["折扣金額"] = 200;
        dtMasterRow1["單機價"] = 400;
        dtResult.Rows.Add(dtMasterRow1);

        DataRow dtMasterRow2 = dtResult.NewRow();
        dtMasterRow2["商品料號"] = "";
        dtMasterRow2["商品名稱"] = "公仔(藍)";
        dtMasterRow2["折扣金額"] = 100;
        dtMasterRow2["單機價"] = 250;
        dtResult.Rows.Add(dtMasterRow2);

        return dtResult;
    }

 
    protected void bindMasterDataCustomer1()
    {
        DataTable dtResult = new DataTable();

        dtResult = getGridViewDataCustomer1(2);
        DISGridViewPanelCustomer1.dt = dtResult;
        DISGridViewPanelCustomer1.KeyFieldName = "客戶對象";
        DISGridViewPanelCustomer1.ItemName = "客戶對象";
        DISGridViewPanelCustomer1.BindData();
    }
    protected void bindMasterDataCustomer2()
    {
        DataTable dtResult = new DataTable();

        dtResult = getGridViewDataCustomer2(2);
        DISGridViewPanelCustomer2.dt = dtResult;
        DISGridViewPanelCustomer2.KeyFieldName = "客戶門號";
        DISGridViewPanelCustomer2.ItemName = "客戶對象";
        DISGridViewPanelCustomer2.BindData();
    }
   
   
   

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        DevExpress.Web.ASPxEditors.ASPxButton btnImport =
            (DevExpress.Web.ASPxEditors.ASPxButton)DISGridViewPanelCustomer1.FindControl("btnImport");
        int a = this.ASPxPageControl2.ActiveTabIndex;

        switch (a + 1)
        {
            case 1:
                break;

           
            case 5:
                bindMasterDataCustomer1();
                break;

            

          
          

            default:
                break;
        }
    }

    protected void rbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbCustomer.SelectedValue == "客戶等級")
        {
            this.DISGridViewPanelCustomer1.Visible = true;
            this.DISGridViewPanelCustomer2.Visible = false;
            bindMasterDataCustomer1();
        }
        else
        {
            this.DISGridViewPanelCustomer1.Visible = false;
            this.DISGridViewPanelCustomer2.Visible = true;
            bindMasterDataCustomer2();
        }
    }
}
