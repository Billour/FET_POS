using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using Advtek.Utility;

public partial class VSS_LEA_LEA07 : BasePage
{
   protected void Page_Load(object sender, EventArgs e)
   {
      if (!IsPostBack)
      {
          bindMasterData(true);
      }
   }

   protected void bindMasterData(bool bInitial)
   {
       DataTable dtResult ;
       if (!bInitial)
       {
           dtResult = new LEA01_Facade().GetSelectData();
       }
       else {
           dtResult = getMasterData();
       }
      ViewState["gvMaster"] = dtResult;
      gvMaster.DataSource = dtResult;
      gvMaster.DataBind();
   }

   private DataTable getMasterData()
   {
      DataTable dtResult = new DataTable();
      dtResult.Columns.Add("LEASE_TYPE", typeof(string));
      dtResult.Columns.Add("PRODTYPE", typeof(string));
      dtResult.Columns.Add("PRODPENAME", typeof(string));
      dtResult.Columns.Add("SUPP_NO", typeof(string));
      dtResult.Columns.Add("SUPP_NAME", typeof(string));
      dtResult.Columns.Add("S_DATE", typeof(string));
      dtResult.Columns.Add("E_DATE", typeof(string));
      dtResult.Columns.Add("EMPNAME", typeof(string));
      dtResult.Columns.Add("MODI_DTM", typeof(string));
   
      return dtResult;
   }

   protected void btnSearch_Click(object sender, EventArgs e)
   {
      bindMasterData(false);
   }

   protected void btnSure_Click(object sender, EventArgs e)
   {
       if (gvMaster.FocusedRowIndex > -1)
       {
           object key = gvMaster.GetRowValues(gvMaster.FocusedRowIndex, gvMaster.KeyFieldName);
           ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('" + StringUtil.CStr(key) + "!');", true);
           Response.Redirect("LEA01.aspx?LEASE=" + StringUtil.CStr(key));
       }

   }

   //ajax 呼當前網頁的方式
   [System.Web.Services.WebMethod()]
   [System.Web.Script.Services.ScriptMethod()]
   static public string getSupplierId(string strSupplierNo)
   {
       //廠商ID
       string SuppId = new Supplier_Facade().GetSuppId(strSupplierNo);
       return SuppId;
   }

   protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
   {
       gvMaster.DataSource =ViewState["gvMaster"];
       gvMaster.DataBind();
   }
}
