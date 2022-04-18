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
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.ConvertApp;
using System.Web.Configuration;
using Advtek.Utility;

public partial class VSS_Common_OddNumberPopup : Popup
{

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

        //switch (KeyFieldValue1.Trim().ToLower())
        //{

        //    case "consignmentsale": //寄銷商品
        //    case "consignmentsale_suppid": //寄銷商品+SUPPID(廠商編號)條件
        //        dt = PRODUCT_PageHelper.GetProDTypeForConsignmentSale(true, "Select");
        //        break;
        //    default:
        //        dt = PRODUCT_PageHelper.GetProDTypeNo(true);
        //        break;
        //}

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
        string sOddNo = this.OddNo.Text.Trim();

        DataTable dt = new DataTable();
        switch (KeyFieldValue1.Trim().ToLower())
        {

            case "prepay_head": //預收款單號
                dt = new OddNumber_Facade().Query_Prepay_Head(KeyFieldValue1.Trim().ToLower(), KeyFieldValue2.Trim(), sOddNo);
                break;
            default:
                dt = new OddNumber_Facade().Query_OddNumber(KeyFieldValue1.Trim().ToLower(), KeyFieldValue2.Trim().ToLower(), sOddNo);
                break;
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

                case "prepay_head": //預收款單號                 
                default:
                    key = grid.GetRowValues(grid.FocusedRowIndex, grid.KeyFieldName);
                    break;
            }


            SetReturnValue(StringUtil.CStr(key));

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
