using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Data;
using System.Runtime.Serialization;
using DevExpress.Web.ASPxGridView;

#region 宗佑
/*Author：宗佑
  Date：100.02.18
  Description：RPL028代收資費彙總表 後端程式
*/
public partial class VSS_RPT_RPL028 : BasePage
{
    private bool isSearch = false;
    private int totalCount;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnReset_Click(null, null);
        }
        
    }
    
    private void BindMasterData()
    {
        DataTable dt = new RPL_Facade().RPL028(StringUtil.CStr(this.ASPxComboBox2.SelectedItem.Value), StringUtil.CStr(this.ASPxComboBox1.SelectedItem.Value ), this.StoreNoPop_S.Text,
                                                           this.StoreNoPop_E.Text, this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
                                                           this.ASPxTextBox1.Text, this.ASPxTextBox2.Text, this.ASPxTextBox3.Text, this.ASPxTextBox6.Text);

        if (dt.Columns.Count > 0)
        {
            gvMaster.Columns.Clear();
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                GridViewDataTextColumn col = new GridViewDataTextColumn();
                col.Caption = dt.Columns[i].ColumnName;
                col.FieldName = dt.Columns[i].ColumnName;
                

                gvMaster.Columns.Add(col);
            }
        }


        this.gvMaster.DataSource = dt;
        this.gvMaster.DataBind();

        DataTable dt_total = new RPL_Facade().RPL028_TOTAL(StringUtil.CStr(this.ASPxComboBox2.SelectedItem.Value), StringUtil.CStr(this.ASPxComboBox1.SelectedItem.Value), this.StoreNoPop_S.Text,
                                                   this.StoreNoPop_E.Text, this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
                                                   this.ASPxTextBox1.Text, this.ASPxTextBox2.Text, this.ASPxTextBox3.Text, this.ASPxTextBox6.Text);

        if (dt.Columns.Count > 0)
        {
            gvExport.Columns.Clear();
            for (int i = 1; i < dt_total.Columns.Count; i++)
            {
                GridViewDataTextColumn col = new GridViewDataTextColumn();
                col.Caption = dt_total.Columns[i].ColumnName;
                col.FieldName = dt_total.Columns[i].ColumnName;
                gvExport.Columns.Add(col);
            }
        }

        //totalCount = dt.Rows.Count;
        gvExport.Templates.PagerBar = new CustomPagerBarTemplate(this.gvMaster.VisibleRowCount);

        this.gvExport.DataSource = dt_total;
        this.gvExport.DataBind();

        //this.gvMaster.DataSource = new RPL_Facade().RPL028(this.ASPxComboBox2.SelectedItem.Value.ToString(), this.ASPxComboBox1.SelectedItem.Value.ToString(), this.StoreNoPop_S.Text,
        //                                                   this.StoreNoPop_E.Text, this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
        //                                                   this.ASPxTextBox1.Text, this.ASPxTextBox2.Text, this.ASPxTextBox3.Text, this.ASPxTextBox6.Text);
        /*this.gvSum.DataSource = new RPL_Facade().RPL028_SUM(this.ASPxComboBox2.SelectedItem.Text, this.ASPxComboBox1.SelectedItem.Text, this.StoreNoPop_S.Text,
                                                           this.StoreNoPop_E.Text, this.txtOrdDateStart.Text, this.txtOrdDateEnd.Text,
                                                           this.ASPxTextBox1.Text, this.ASPxTextBox2.Text, this.ASPxTextBox3.Text, this.ASPxTextBox6.Text);*/
        //this.gvMaster.DataBind();
        //this.gvSum.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            StoreNoPop_S.Text = StringUtil.CStr(logMsg.STORENO); StoreNoPop_S.Enabled = false;
            StoreNoPop_E.Text = StringUtil.CStr(logMsg.STORENO); StoreNoPop_E.Enabled = false;
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        isSearch = true;
        BindMasterData();
        gvMaster.PageIndex = 0;
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BindMasterData();
        new Output().ExportXLS(800, "", Resources.WebResources.RPL028, dtHeader(), this.ASPxGridViewExporter1, null, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("RPL028.xls"));
    }

    private DataTable dtHeader()
    {
        DataTable dtHeader = new DataTable();
        dtHeader.Columns.Add("Text", typeof(string));
        dtHeader.Columns.Add("Align", typeof(string));
        dtHeader.Columns.Add("FontSize", typeof(string));
        dtHeader.Columns.Add("BackColor", typeof(string));

        DataRow NewRow = dtHeader.NewRow();
        NewRow["Text"] = "公司別：" + this.ASPxComboBox2.SelectedItem.Text
            + "|交易型態：" + this.ASPxComboBox1.SelectedItem.Text
            + "|門市編號：" + this.StoreNoPop_S.Text + "～" + this.StoreNoPop_E.Text
            + "|交易日期：" + this.txtOrdDateStart.Text + "～" + this.txtOrdDateEnd.Text
            + "|帳單金額：" + this.ASPxTextBox1.Text + "～" + this.ASPxTextBox2.Text
            + "|帳單號碼：" + this.ASPxTextBox3.Text
            + "|門號：" + this.ASPxTextBox6.Text
            + "";
        NewRow["Align"] = "LEFT";
        NewRow["FontSize"] = "11";
        NewRow["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow);

        DataRow NewRow2 = dtHeader.NewRow();
        NewRow2["Text"] = "列印日期：" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            + "|列印人員：" + logMsg.MODI_USER + " " + new Employee_Facade().GetEmpName(logMsg.MODI_USER)
            + "|頁　　次：" + "1"
            + "|總筆數：" + this.gvMaster.VisibleRowCount;
        NewRow2["Align"] = "LEFT";
        NewRow2["FontSize"] = "11";
        NewRow2["BackColor"] = "WHITE";
        dtHeader.Rows.Add(NewRow2);

        return dtHeader;

    }

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        if (!isSearch)
        {
            BindMasterData();
        }
    }

    protected void gvExport_PageIndexChanged(object sender, EventArgs e)
    {
        if (!isSearch)
        {
            BindMasterData();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        //if (logMsg.ROLE_TYPE != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非總部人員無法使用此功能!!');", true);

        //    ASPxComboBox2.Enabled = false;
        //    ASPxComboBox1.Enabled = false;
        //    StoreNoPop_S.Enabled = false;
        //    StoreNoPop_E.Enabled = false;
        //    txtOrdDateStart.Enabled = false;
        //    txtOrdDateEnd.Enabled = false;
        //    ASPxTextBox1.Enabled = false;
        //    ASPxTextBox2.Enabled = false;


        //    btnSearch.Enabled = false;
        //    btnReset.Enabled = false;
        //    btnExport.Enabled = false;
        //    return;
        //}

        //查詢條件清空，下拉選單回復預設值

        this.ASPxComboBox2.SelectedIndex = 0;
        this.ASPxComboBox1.SelectedIndex = 0;
        this.StoreNoPop_S.Text = null;
        this.StoreNoPop_E.Text = null;
        this.txtOrdDateStart.Text = null;
        this.txtOrdDateEnd.Text = null;
        this.ASPxTextBox1.Text = null;
        this.ASPxTextBox2.Text = null;
        this.ASPxTextBox3.Text = null;
        this.ASPxTextBox6.Text = null;

        gvMaster.DataSource = null;
        gvMaster.DataBind();

        gvExport.DataSource = null;
        gvExport.DataBind();

        if (StringUtil.CStr(logMsg.STORENO) != System.Web.Configuration.WebConfigurationManager.AppSettings["DefaultRoleHQ"])
        {
            StoreNoPop_S.Text = StringUtil.CStr(logMsg.STORENO); StoreNoPop_S.Enabled = false;
            StoreNoPop_E.Text = StringUtil.CStr(logMsg.STORENO); StoreNoPop_E.Enabled = false;
        }

    }
    
}
#endregion