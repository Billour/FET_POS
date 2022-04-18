using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;

public partial class VSS_OPT_OPT18_Import : Popup
{
    protected void btnImport_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnCommit.Visible = true;
        btnCalcel.Visible = true;
        Literal6.Text = "0";
        Literal7.Text = "3";
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("角色", typeof(string));
        dtResult.Columns.Add("折扣月份", typeof(string));
        dtResult.Columns.Add("折扣總額", typeof(string));
        dtResult.Columns.Add("折扣金額", typeof(string));
        dtResult.Columns.Add("折扣比例", typeof(string));
        dtResult.Columns.Add("折扣上限", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));


        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2010";
        NewRow["門市名稱"] = "";
        NewRow["角色"] = "店長";
        NewRow["折扣月份"] = "201010";
        NewRow["折扣總額"] = "9999";
        NewRow["折扣金額"] = "9999";
        NewRow["折扣比例"] = "";
        NewRow["折扣上限"] = "";
        NewRow["異常原因"] = "門市編號不存在";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2011";
        NewRow["門市名稱"] = "華納門市";
        NewRow["角色"] = "店長";
        NewRow["折扣月份"] = "201002";
        NewRow["折扣金額"] = "9999";
        NewRow["折扣比例"] = "";
        NewRow["折扣上限"] = "";
        NewRow["異常原因"] = "月份不可小於系統日";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2012";
        NewRow["門市名稱"] = "忠敦門市";
        NewRow["角色"] = "店長";
        NewRow["折扣月份"] = "201002";
        NewRow["折扣金額"] = "ABC";
        NewRow["折扣比例"] = "";
        NewRow["折扣上限"] = "";
        NewRow["異常原因"] = "折扣總額非數值";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "異常原因")
        {
            e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
        }
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {

    }
}
