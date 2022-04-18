using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;
using Resources;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using AdvTek.CustomControls;


public partial class VSS_INV_INV06_INV06 : BasePage
{
    private INV06_Facade _INV06_Facade;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable dt = new DataTable();
            dt = bindMasterData();
            Session["gvMaster"] = dt;
            gvMaster.DataSource = (DataTable)Session["gvMaster"];
            gvMaster.DataBind();

            CbType.SelectedIndex = 1;
        }
    }

    protected DataTable bindMasterData()
    {
        DataTable dt = new DataTable();
        dt = INV06_PageHelper.GetRTNDMethodData(logMsg.STORENO);
        return dt;
    }
 
    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string RTNNO = RTNNOBox.Text;
        string PRODNO = Prodno.Text;
        string S_DATE = transferOutStartDate.Text;
        string E_DATE = transferOutStartEndDate.Text;
        string STATUS = StringUtil.CStr(CbType.SelectedItem.Value);

        string storeNo = logMsg.STORENO;

        _INV06_Facade = new INV06_Facade();
        DataTable dt = _INV06_Facade.QueryRTNDData(S_DATE, E_DATE, "1", true, storeNo, RTNNO, PRODNO, STATUS,"");
        Session["gvMaster"] = dt;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
    }

    protected void CommandButton_Click(Object sender, CommandEventArgs e)
    {
        string s = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(StringUtil.CStr(e.CommandArgument), "RTNNO"));
        string sRTNDATE = StringUtil.CStr(gvMaster.GetRowValuesByKeyValue(StringUtil.CStr(e.CommandArgument), "RTNDATE"));

        string storeNo = logMsg.STORENO;
        string EmployeeNo = logMsg.OPERATOR;
        string MachineID = logMsg.MACHINE_ID;
        string role = logMsg.ROLE_TYPE;
        string encryptUrl = Utils.Param_Encrypt("RTNN_ID=" + StringUtil.CStr(e.CommandArgument) + "&&RTNNO=" + s + "&&storeId=" + storeNo + "&&EmployeeId=" + EmployeeNo + "&&Machine_ID=" + MachineID + "&&role=" + role);
        string Url = string.Format("INV06_1.aspx?Param={0}", encryptUrl);
        Response.Redirect(Url);
    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string s = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
            LinkButton lblItemIndex = gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns["RTNNO"], "Label1") as LinkButton;
            switch (s)
            {
                case "10":
                    s = "未完成";
                    lblItemIndex.ForeColor = System.Drawing.Color.Red;

                    break;
                case "60":
                    s = "已完成";
                    break;

            }
            if (s == "未完成")
            {
                e.Row.Style.Add(HtmlTextWriterStyle.Color, "red");
            }
        }

    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            string s = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));
            switch (s)
            {
                case "10":
                    e.Row.Cells[4].Text = "未完成";
                    break;
                case "60":
                    e.Row.Cells[4].Text = "已完成";
                    break;
            }
        }
    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = (DataTable)Session["gvMaster"];
        gvMaster.DataBind();
    }

    #endregion

    //ajax IMEI清單
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string IMEIContent(string TABLENAME, string ID, string PRODNO)
    {
        DataTable dt = new IMEI_Facade().getINV_IMEI(TABLENAME, ID, PRODNO);

        string IMEI_FORMAT = "<table border=\"1\">";
        foreach (DataRow dr in dt.Rows)
        {
            IMEI_FORMAT += "<tr><td>" + StringUtil.CStr(dr["IMEI"]) + "</td></tr>";
        }
        IMEI_FORMAT += "</table>";

        return IMEI_FORMAT;
    }
}
