using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web.ASPxEditors;

public partial class VSS_LEA_LEA03 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();
        }
    }

    protected void bindEmptyData()
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
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("預約日期", typeof(string));
        dtResult.Columns.Add("租賃單號", typeof(string));
        dtResult.Columns.Add("客戶姓名", typeof(string));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("性別", typeof(string));
        dtResult.Columns.Add("手機地點", typeof(string));
        dtResult.Columns.Add("預定領取日", typeof(string));
        dtResult.Columns.Add("預約歸還日", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));

        string[] array1 = {  "已預約", "已借出"};
        string[] array2 = { "台北", "台中", "高雄" };
        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["項次"] = i+1;
            NewRow["預約日期"] = "2010/08/03";
            NewRow["租賃單號"] = "租賃單號" + i;
            NewRow["客戶姓名"] = "蕭大同";
            NewRow["客戶門號"] = string.Format("091{0}654{0}1{0}", i);
            NewRow["性別"] = "男";
            NewRow["手機地點"] = array2[i % 3];
            NewRow["預定領取日"] = "2010/09/02";
            NewRow["預約歸還日"] = "2010/09/30";
            NewRow["狀態"] = array1[i % 2];
            NewRow["更新日期"] = "2010/09/01";
            NewRow["更新人員"] = "方敬騰";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView view = row.DataItem as DataRowView;
            ASPxButton b = e.Row.FindControl("btnSelect") as ASPxButton;
            if (view["手機地點"].ToString() == "台北")
            {
                //b.CommandName = "GoToEdit";
                //b.Text = GetGlobalResourceObject("WebResources", "Edit").ToString();
                b.Enabled = true;
            }
            else
            {
                b.Enabled = false;
                //b.CommandName = "Select";
                //b.Text = GetGlobalResourceObject("WebResources", "View").ToString();
                //b.Enabled = false;
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strResDate = e.Row.Cells[2].Text;
            string strLEANo = e.Row.Cells[3].Text;
            string strCusName = e.Row.Cells[4].Text;

            string strCusPhNumber = e.Row.Cells[5].Text;
            string strSex = e.Row.Cells[6].Text;
            string strMobileLocal = e.Row.Cells[7].Text;

            string strResTakeDate = e.Row.Cells[8].Text;
            string strResReturnDate = e.Row.Cells[8].Text;
            string strStatus = e.Row.Cells[10].Text;

            string strUpdDate = e.Row.Cells[11].Text;
            string strUpdMan = e.Row.Cells[12].Text;

            ASPxButton btnSelect = (ASPxButton)(e.Row.FindControl("btnSelect"));
            btnSelect.PostBackUrl = @"../LEA/LEA05.aspx?ResDate=" + strResDate + "&LEANo=" + strLEANo + "&CusName=" + strCusName + "&CusPhNumber=" + strCusPhNumber
                                     + "&Sex=" + strSex + "&MobileLocal=" + strMobileLocal + "&ResTakeDate=" + strResTakeDate
                                     + "&ResReturnDate=" + strResReturnDate;
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Response.Redirect("../LEA/LEA05.aspx");
    }
}
