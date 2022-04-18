using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using System.Web.UI.HtmlControls;

public partial class VSS_DIS_DIS01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData1();
        }
    }

    private DataTable getGridViewData1(int StartIndex, int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("資費代碼", typeof(string));
        dtResult.Columns.Add("資費名稱", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["資費代碼"] };  

        for (int i = StartIndex; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["資費代碼"] = "A0000" + (i+1).ToString();
            dtMasterRow["資費名稱"] = "資費名稱_" + (i + 1).ToString();

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewData2(int StartIndex, int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("資費代碼", typeof(string));
        dtResult.Columns.Add("資費名稱", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["資費代碼"] };

        for (int i = StartIndex; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["資費代碼"] = "A0000" + (i + 1).ToString();
            dtMasterRow["資費名稱"] = "Voice 資費名稱_" + (i + 1).ToString();

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewData3(int StartIndex, int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("申裝系統", typeof(string));
        dtResult.Columns.Add("申裝名稱", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["申裝系統"] };

        for (int i = StartIndex; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["申裝系統"] = "G" + (i + 1).ToString();
            dtMasterRow["申裝名稱"] = "申裝名稱_" + (i + 1).ToString();

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewData4(int StartIndex, int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["商品編號"] };

        for (int i = StartIndex; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["商品編號"] = "100000" + (i + 1).ToString();
            dtMasterRow["商品名稱"] = "商品名稱_" + (i + 1).ToString();

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewData5(int StartIndex, int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("促銷代碼", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["促銷代碼"] };

        for (int i = StartIndex; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["促銷代碼"] = "SA100000" + (i + 1).ToString();
            dtMasterRow["促銷名稱"] = "促銷名稱_" + (i + 1).ToString();

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewData7(int StartIndex, int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("區域別", typeof(string));
        dtResult.Columns.Add("門市代碼", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["門市代碼"] };

        for (int i = StartIndex; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["區域別"] = "北區";
            dtMasterRow["門市代碼"] = "210" + (i + 1).ToString();
            dtMasterRow["門市名稱"] = "門市_" + (i + 1).ToString();

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewData8(int StartIndex, int TempCount)
    {

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("部門代碼", typeof(string));
        dtResult.Columns.Add("部門名稱", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["部門代碼"] };

        for (int i = StartIndex; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["部門代碼"] = "61000" + (i + 1).ToString();
            dtMasterRow["部門名稱"] = "部門" + (i + 1).ToString();

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }
    private DataTable getGridViewData8_1(int StartIndex, int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("部門代碼", typeof(string));
        dtResult.Columns.Add("部門名稱", typeof(string));
        dtResult.Columns.Add("折扣類型", typeof(string));
        dtResult.Columns.Add("會計科目代碼", typeof(string));
        dtResult.Columns.Add("分攤率(%)", typeof(string));

        dtResult.PrimaryKey = new DataColumn[] { dtResult.Columns["部門代碼"] };

        for (int i = StartIndex; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["部門代碼"] = "61000" + (i + 1).ToString();
            dtMasterRow["部門名稱"] = "部門" + (i + 1).ToString();
            dtMasterRow["折扣類型"] = "手機折扣";
            dtMasterRow["會計科目代碼"] = "A" + (i + 1).ToString();
            dtMasterRow["分攤率(%)"] = "50%";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void bindMasterData1()
    {
        GridViewPanel GridViewPanel1 = (GridViewPanel)ItemPage1.FindControl("GridViewPanel1");
        GridViewPanel GridViewPanel2 = (GridViewPanel)ItemPage1.FindControl("GridViewPanel2");

        DataTable dtResult = new DataTable();

        dtResult = getGridViewData1(0, 2);
        GridViewPanel1.dt = dtResult;
        GridViewPanel1.KeyFieldName = "資費代碼";
        GridViewPanel1.bLeftControl = true;
        GridViewPanel1.PanelText = "Data資費候選項目";
        GridViewPanel1.BindData();

        dtResult = getGridViewData1(3, 4);
        GridViewPanel2.dt = dtResult;
        GridViewPanel2.KeyFieldName = "資費代碼";
        GridViewPanel2.bLeftControl = false;
        GridViewPanel2.PanelText = "群組成員項目";
        GridViewPanel2.BindData();

    }
    protected void bindMasterData2()
    {
        GridViewPanel GridViewPanel1 = (GridViewPanel)ItemPage2.FindControl("GridViewPanel1");
        GridViewPanel GridViewPanel2 = (GridViewPanel)ItemPage2.FindControl("GridViewPanel2");

        DataTable dtResult = new DataTable();

        dtResult = getGridViewData2(0, 2);
        GridViewPanel1.dt = dtResult;
        GridViewPanel1.KeyFieldName = "資費代碼";
        GridViewPanel1.bLeftControl = true;
        GridViewPanel1.PanelText = "Voice資費候選項目";
        GridViewPanel1.BindData();

        dtResult = getGridViewData2(3, 4);
        GridViewPanel2.dt = dtResult;
        GridViewPanel2.KeyFieldName = "資費代碼";
        GridViewPanel2.bLeftControl = false;
        GridViewPanel2.PanelText = "群組成員項目";
        GridViewPanel2.BindData();

    }
    protected void bindMasterData3()
    {
        GridViewPanel GridViewPanel1 = (GridViewPanel)ItemPage3.FindControl("GridViewPanel1");
        GridViewPanel GridViewPanel2 = (GridViewPanel)ItemPage3.FindControl("GridViewPanel2");

        DataTable dtResult = new DataTable();

        dtResult = getGridViewData3(0, 2);
        GridViewPanel1.dt = dtResult;
        GridViewPanel1.KeyFieldName = "申裝系統";
        GridViewPanel1.bLeftControl = true;
        GridViewPanel1.PanelText = "申裝類型候選項目";
        GridViewPanel1.BindData();

        dtResult = getGridViewData3(3, 4);
        GridViewPanel2.dt = dtResult;
        GridViewPanel2.KeyFieldName = "申裝系統";
        GridViewPanel2.bLeftControl = false;
        GridViewPanel2.PanelText = "群組成員項目";
        GridViewPanel2.BindData();

    }
    protected void bindMasterData4()
    {
        GridViewPanel GridViewPanel1 = (GridViewPanel)ItemPage4.FindControl("GridViewPanel1");
        GridViewPanel GridViewPanel2 = (GridViewPanel)ItemPage4.FindControl("GridViewPanel2");

        DataTable dtResult = new DataTable();

        dtResult = getGridViewData4(0, 2);
        GridViewPanel1.dt = dtResult;
        GridViewPanel1.KeyFieldName = "商品編號";
        GridViewPanel1.bLeftControl = true;
        GridViewPanel1.PanelText = "指定商品候選項目";
        GridViewPanel1.BindData();

        dtResult = getGridViewData4(3, 4);
        GridViewPanel2.dt = dtResult;
        GridViewPanel2.KeyFieldName = "商品編號";
        GridViewPanel2.bLeftControl = false;
        GridViewPanel2.PanelText = "群組成員項目";
        GridViewPanel2.BindData();

    }
    protected void bindMasterData5()
    {
        GridViewPanel GridViewPanel1 = (GridViewPanel)ItemPage5.FindControl("GridViewPanel1");
        GridViewPanel GridViewPanel2 = (GridViewPanel)ItemPage5.FindControl("GridViewPanel2");

        DataTable dtResult = new DataTable();

        dtResult = getGridViewData5(0, 2);
        GridViewPanel1.dt = dtResult;
        GridViewPanel1.KeyFieldName = "促銷代碼";
        GridViewPanel1.bLeftControl = true;
        GridViewPanel1.PanelText = "促銷代碼候選項目";
        GridViewPanel1.BindData();

        dtResult = getGridViewData5(3, 4);
        GridViewPanel2.dt = dtResult;
        GridViewPanel2.KeyFieldName = "促銷代碼";
        GridViewPanel2.bLeftControl = false;
        GridViewPanel2.PanelText = "群組成員項目";
        GridViewPanel2.BindData();

    }
    protected void bindMasterData7()
    {
        GridViewPanel GridViewPanel1 = (GridViewPanel)ItemPage7.FindControl("GridViewPanel1");
        GridViewPanel GridViewPanel2 = (GridViewPanel)ItemPage7.FindControl("GridViewPanel2");

        DataTable dtResult = new DataTable();

        dtResult = getGridViewData7(0, 2);
        GridViewPanel1.dt = dtResult;
        GridViewPanel1.KeyFieldName = "門市代碼";
        GridViewPanel1.bLeftControl = true;
        GridViewPanel1.PanelText = "門市候選項目";
        GridViewPanel1.BindData();

        dtResult = getGridViewData7(3, 4);
        GridViewPanel2.dt = dtResult;
        GridViewPanel2.KeyFieldName = "門市代碼";
        GridViewPanel2.bLeftControl = false;
        GridViewPanel2.PanelText = "群組成員項目";
        GridViewPanel2.BindData();

    }
    protected void bindMasterData8()
    {
        GridViewPanel GridViewPanel1 = (GridViewPanel)ItemPage8.FindControl("GridViewPanel1");
        GridViewPanel GridViewPanel2 = (GridViewPanel)ItemPage8.FindControl("GridViewPanel2");

        DataTable dtResult = new DataTable();

        dtResult = getGridViewData8(0, 2);
        GridViewPanel1.dt = dtResult;
        GridViewPanel1.KeyFieldName = "部門代碼";
        GridViewPanel1.bLeftControl = true;
        GridViewPanel1.PanelText = "部門候選項目";
        GridViewPanel1.BindData();

        dtResult = getGridViewData8_1(3, 4);
        GridViewPanel2.dt = dtResult;
        GridViewPanel2.KeyFieldName = "部門代碼";
        GridViewPanel2.bLeftControl = false;
        GridViewPanel2.PanelText = "群組成員項目";
        GridViewPanel2.BindData();

    }

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int a = this.ASPxPageControl1.ActiveTabIndex;

        switch (a+1)
        {
            case 1:
                bindMasterData1();
                break;

            case 2:
                bindMasterData2();
                break;

            case 3:
                bindMasterData3();
                break;

            case 4:
                bindMasterData4();
                break;

            case 5:
                bindMasterData5();
                break;

            case 6:
                break;

            case 7:
                bindMasterData7();
                break;

            case 8:
                bindMasterData8();
                break;

            default:
                break;
        }
    }
  

}
