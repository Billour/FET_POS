using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using Advtek.Utility;

public partial class VSS_INV_INV26_inputIMEIData : Popup
{
    public static class param
    {
        public static string TableName = ""; //以此來判別資料IMEI的資料來源
        public static string PO_OE_NO = "";
        public static string PRODNO = "";
        public static int QTY = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] s = KeyFieldValue1.Split(new char[] { ';' });
            if (s.Length >= 4)
            {
                param.TableName = s[0];
                param.PO_OE_NO = s[1];
                param.PRODNO = s[2];
                param.QTY = 0;
                if (!string.IsNullOrEmpty(s[3]))
                {
                    param.QTY = Convert.ToInt16(s[3]);
                }
            }

            lbPRODNO.Text = param.PRODNO;
            DataTable dtProduct = new Product_Facade().Query_ProductInfo(param.PRODNO);
            if (dtProduct != null && dtProduct.Rows.Count > 0 && dtProduct.Rows[0]["PRODNAME"] != null)
                lbPRODNAME.Text = StringUtil.CStr(dtProduct.Rows[0]["PRODNAME"]);
            bindMasterData();
        }
    }

    protected void bindMasterData()
    {
        grid.DataSource = getMasterData();
        grid.DataBind();
    }

    private DataTable getMasterData()
    {
        return new IMEI_Facade().getINV_IMEI(param.TableName, param.PO_OE_NO, param.PRODNO);
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

}
