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

using Advtek.Utility;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using System.Web.UI.HtmlControls;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_CHK_CHK05 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !Page.IsCallback)
        {
            //取得空的資料表
            grid.DataSource = new CHK05_BigMoney_DTO().BIG_MONEY;
            grid.DataBind();

            BinddlMachineIDList();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtTraneDateS.Text.Trim()))
        {
            BindData();
        }
        else
        {
            Response.Write("<script language='javascript'>alert('請輸入有效日期!!');</script>");
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        string HOST_NO = CHK05_PageHelper.GetHostNo(logMsg.STORENO, logMsg.MACHINE_ID);
        if (!string.IsNullOrEmpty(HOST_NO))
        {
            grid.AddNewRow();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "Message", "alert('此門市查無此機台編號!!');", true);
        }
    }

    private void BindData()
    {
        string strMachine = "";
        if (this.ddlMachineID.Value != null)
        {
            strMachine = StringUtil.CStr(this.ddlMachineID.Value);
        }

        grid.DataSource = new CHK05_Facade().Query_BigMoney(this.txtTraneDateS.Text, this.txtTraneDateS.Text, strMachine, logMsg.STORENO);
        grid.DataBind();
    }

    private void BinddlMachineIDList()
    {
        this.ddlMachineID.DataSource = Common_PageHelper.GetStoreTerminatingMachine(logMsg.STORENO);
        this.ddlMachineID.TextField = "HOST_NO";
        this.ddlMachineID.ValueField = "HOST_NO";
        this.ddlMachineID.DataBind();
        this.ddlMachineID.SelectedIndex = 0;
    }

    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        CHK05_BigMoney_DTO CHK05_DTO = new CHK05_BigMoney_DTO();
        CHK05_BigMoney_DTO.BIG_MONEYDataTable dt = CHK05_DTO.BIG_MONEY;
        CHK05_BigMoney_DTO.BIG_MONEYRow dr = dt.NewBIG_MONEYRow();

        CHK05_Facade facade = new CHK05_Facade();

        dr.ID = GuidNo.getUUID();
        dr.TRADE_DATE = Convert.ToDateTime(StringUtil.CStr(e.NewValues["TRADE_DATE"]));
        dr.STORE_NO = logMsg.STORENO;
        dr.MACHINE_ID = logMsg.MACHINE_ID;
        dr.BATCH_NO = Convert.ToInt16(StringUtil.CStr(e.NewValues["BATCH_NO"]));
        dr.AMOUNT = Convert.ToInt64(StringUtil.CStr(e.NewValues["AMOUNT"]));
        dr.CREATE_USER = logMsg.OPERATOR;
        dr.CREATE_DTM = System.DateTime.Now;
        dr.MODI_USER = logMsg.OPERATOR;
        dr.MODI_DTM = System.DateTime.Now;

        dt.Rows.Add(dr);

        CHK05_DTO.AcceptChanges();

        //更新資料庫
        facade.AddNewOne_BigMoney(CHK05_DTO);

        grid.CancelEdit();
        e.Cancel = true;

        this.txtTraneDateS.Text = System.DateTime.Today.ToString("yyyy/MM/dd");
        BindData();
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["TRADE_DATE"] = OracleDBUtil.WorkDay(logMsg.STORENO);
        e.NewValues["HOST_NO"] = CHK05_PageHelper.GetHostNo(logMsg.STORENO, logMsg.MACHINE_ID);
        e.NewValues["BATCH_NO"] = CHK05_PageHelper.GetDataCount(logMsg.STORENO, logMsg.MACHINE_ID, StringUtil.CStr(e.NewValues["TRADE_DATE"])) + 1;
        e.NewValues["EMPNAME"] = new Employee_Facade().GetEmpName(logMsg.OPERATOR);
        e.NewValues["MODI_DTM"] = System.DateTime.Now;
    }
}
