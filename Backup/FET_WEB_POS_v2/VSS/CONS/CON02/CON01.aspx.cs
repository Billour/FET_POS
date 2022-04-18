using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using System.Web.Configuration;
using System.Text;
using System.Data.OracleClient;


public partial class VSS_CONS_CON01 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindMasterData(false);
        }
        else
        {
            //DataTable dtGvMaster = ViewState["gvMaster"] as DataTable;
            //if (dtGvMaster != null)
            //{
            //    gvMaster.DataSource = dtGvMaster;
            //    gvMaster.DataBind();
            //}
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData(false);         
    }

    protected void bindMasterData(bool bEmptyData)
    {
        DataTable dtResult = new DataTable();
        string strType = DropType.Value.ToString();
        if (strType == "select" || strType == "ALL")
        {
            strType = "";
        }
        if (!bEmptyData)
            dtResult = new CON02_Facade().GetSelectData(strType, popSupplierNo.Text, popSupplierName.Text, txtCompanyId.Text);
        else
            dtResult = new CON02_Facade().GetSelectData("dfsdfs", "", "", "");

        //ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        DropType.SelectedIndex = 0;
        popSupplierNo.Text = "";
        popSupplierName.Text = "";
        txtCompanyId.Text = "";

        bindMasterData(true);
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dtExport;
        //總部人員可查詢各門市資料, 門市人員只能查詢自己的門市
        dtExport = Export_StoreOrders(
            logMsg.STORENO,
            logMsg.ROLE_TYPE == WebConfigurationManager.AppSettings["DefaultRoleHQ"]
            );
        string filename = new Output().Print("XLS", "外部廠商維護作業<td></td><td>匯出時間：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "</td>", null, dtExport, null);
        Response.Redirect(Utils.CreateTamperProofDownloadXlsURL("SUPPLIER.xls"));
    }

    //ajax 呼當前網頁的方式
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    static public string checkINVcode(string strINV)
    {
        string strInfo = "";
        Advtek.Utility.Check_ID subCheck = new Check_ID();
        if (!string.IsNullOrEmpty(strINV))
        {
            //如果不足8碼
            if (subCheck.Check_TW_INV(strINV) == 3)
            {
                strInfo = "false";
            }
        }
        
        return strInfo;
    }
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {        
        gvMaster.FocusedRowIndex = -1;
        gvMaster.PageIndex = 0;
        bindMasterData(false);

    }
    /// <summary>
    /// 匯出查詢訂單
    /// </summary>
    /// <param name="sDate"></param>
    /// <param name="eDate"></param>
    /// <param name="prodNo"></param>
    /// <returns></returns>
    public DataTable Export_StoreOrders(string STORENO, bool isHQ)
    {
        //OracleConnection objConn = null;
        DataTable dt;

        string strType = DropType.Value.ToString();
        if (strType == "select" || strType == "ALL")
        {
            strType = "";
        }
        try
        {
            dt = new CON02_Facade().GetSelectData(strType, popSupplierNo.Text, popSupplierName.Text, txtCompanyId.Text);

            DataTable dtt = new DataTable();
            dtt.Columns.Add("廠商編號");
            dtt.Columns.Add("廠商名稱");
            dtt.Columns.Add("廠商類別");
            dtt.Columns.Add("統一編號");
            dtt.Columns.Add("合作起日");
            dtt.Columns.Add("合作訖日");
            dtt.Columns.Add("聯絡人員");
            dtt.Columns.Add("電話號碼");
            dtt.Columns.Add("更新人員");
            dtt.Columns.Add("更新日期");

            foreach (DataRow dr in dt.Rows)
            {
                DataRow drr = dtt.NewRow();

                drr[0] = dr["SUPP_NO"].ToString();
                drr[1] = dr["SUPP_NAME"].ToString();
                drr[2] = dr["CSM_TYPE"].ToString();
                drr[3] = dr["COMPANY_ID"].ToString();
                drr[4] = dr["S_DATE"].ToString();
                drr[5] = dr["E_DATE"].ToString();
                drr[6] = dr["FET_CONTACE_USER"].ToString();
                drr[7] = dr["BOSS_TEL_NO"].ToString();
                drr[8] = dr["MODI_USER"].ToString();
                drr[9] = dr["MODI_DTM"].ToString();

                dtt.Rows.Add(drr);
            }
            return dtt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt = null;
        }
    }

}
