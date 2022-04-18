﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.ConvertApp;
using System.Web.Configuration;
using Advtek.Utility;

public partial class VSS_Common_ProductsPopup3 : Popup
{
    string _STOCKID
    {
        get
        {
            string s = "";
            if (KeyFieldValue1.Trim().ToLower() == "salehouse")
            {
                s = Common_PageHelper.GetGoodLOCUUID();
            }
            return s;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
         
            grid.DataSource = new DataTable();
            grid.DataBind();
            bindCategory();
        }
    }

    private void bindCategory()
    {
        DataTable dt = new DataTable();
      
        switch (KeyFieldValue1.Trim().ToLower())
        {
         
            case "consignmentsale": //寄銷商品
            case "consignmentsale_suppid": //寄銷商品+SUPPID(廠商編號)條件
                dt = PRODUCT_PageHelper.GetProDTypeForConsignmentSale(true, "Select");
                break;
            default:
                dt = PRODUCT_PageHelper.GetProDTypeNo(true);
                break;
        }


        cboCategory.DataSource = dt;
        cboCategory.TextField = "PRODTYPE_NAME";
        cboCategory.ValueField = "PRODTYPE_NO";
        cboCategory.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
        grid.FocusedRowIndex = -1;
        grid.PageIndex = 0;
    }

    protected void BindData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    private DataTable GetMasterData()
    {
        string sProdNo = this.TextBox1.Text.Trim();
        string sProName = this.TextBox6.Text.Trim();
        string sProdType = StringUtil.CStr(this.cboCategory.SelectedItem.Value);
        DataTable dt = new DataTable();

        if (!string.IsNullOrEmpty(_STOCKID))  //要篩選出有庫存的料號
        {
            dt = new Product_Facade().Query_Product(sProdNo, sProName, sProdType, logMsg.STORENO, _STOCKID, WebConfigurationManager.AppSettings["DefaultRoleHQ"]);
        }
        else  //篩選出有效日期區間內的料號
        {
            switch (KeyFieldValue1.Trim().ToLower())
            {
              
                case "consignmentsale": //寄銷商品
                    dt = new Product_Facade().Query_ProductConsignmentSale(sProdNo, sProName, sProdType);
                    break;
                case "consignmentsale_suppid": //寄銷商品+SUPPID(廠商編號)條件
                    dt = new Product_Facade().Query_ProductConsignmentSale(sProdNo, sProName, sProdType, KeyFieldValue2.Trim());
                    break;
                default:
                    dt = new Product_Facade().Query_Product(sProdNo, sProName, sProdType);
                    break;
            }
                     

        }
        DataTable dtResult = dt;
        return dtResult;
    }


    protected void OkButton_Click(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {
            object key;
            switch (KeyFieldValue1.Trim().ToLower())
            {

                case "consignmentsale": //寄銷商品
                    key = grid.GetRowValues(grid.FocusedRowIndex, KeyFieldValue2.Trim());
                    break;
              
                default:
                    key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
                    break;
            }

           
            SetReturnValue(StringUtil.CStr(key));
            //object key2 = grid.GetRowValues(grid.FocusedRowIndex, "PRODNAME");
            //if (key2 != null) { SetReturnValue2(StringUtil.CStr(key)); };

            SetReturnOKScript();
        }
        else
        {
            SetReturnValue(string.Empty);
            SetReturnValue2(string.Empty);
        }
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

}
