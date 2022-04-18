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

public partial class VSS_DIS_DIS04 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    BindMasterData();
        //}
    }

    protected void BindDetailData(ASPxGridView gv)
    {
        string flag = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, "flag").ToString();
        string col1 = "";
        string col2 = "";
        if (flag == "類型")
        {
            col1 = "商品類型";
            col2 = "商品類型名稱";
        }
        else
        {
            col1 = "商品編號";
            col2 = "商品名稱";
        }

        DataTable dtResult = new DataTable();
        dtResult = getDetailData(5, col1, col2);
        gv.Columns.Clear();
        gv.AutoGenerateColumns = true;
        gv.DataSource = dtResult;
        gv.DataBind();
        gv.KeyFieldName = col1;

    }

    protected void BindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("flag", typeof(string));
        dtResult.Columns.Add("促銷代號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));


        DataRow NewRow = dtResult.NewRow();
        NewRow["flag"] = "編號";
        NewRow["促銷代號"] = "促銷代號1";
        NewRow["促銷名稱"] = "促銷名稱1";
       
        NewRow["更新人員"] = "人員1";
        NewRow["更新日期"] = DateTime.Now.ToShortDateString();
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["flag"] = "類型";
        NewRow["促銷代號"] = "促銷代號2";
        NewRow["促銷名稱"] = "促銷名稱2";
        NewRow["更新人員"] = "人員1";
        NewRow["更新日期"] = DateTime.Now.ToShortDateString();
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable getDetailData(int count, string Col1, string Col2)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add(Col1, typeof(string));
        dtResult.Columns.Add(Col2, typeof(string));

        for (int i = 1; i <= count; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i.ToString();
            NewRow[Col1] = Col1 + i.ToString();
            NewRow[Col2] = Col2 + i.ToString();
            dtResult.Rows.Add(NewRow);
        }

        return dtResult;

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindMasterData();
    }

    protected void gvMaster_FocusedRowChanged(object sender, EventArgs e)
    {
        if (gvMaster.FocusedRowIndex >= 0)
        {
            this.ASPxPageControl1.Visible = true;
            this.ASPxPageControl1.ActiveTabIndex = 0;
            BindDetailData(this.gvDetail1);
        }
    }

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int a = this.ASPxPageControl1.ActiveTabIndex;

        switch (a + 1)
        {
            case 1:
                BindDetailData(this.gvDetail1);
                break;

            case 2:
                BindDetailData(this.gvDetail2);
                break;

            case 3:
                BindDetailData(this.gvDetail3);
                break;

            case 4:
                BindDetailData(this.gvDetail4);
                break;

            case 5:
                BindDetailData(this.gvDetail5);
                break;

            case 6:
                BindDetailData(this.gvDetail6);
                break;

            default:
                break;
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }

}
