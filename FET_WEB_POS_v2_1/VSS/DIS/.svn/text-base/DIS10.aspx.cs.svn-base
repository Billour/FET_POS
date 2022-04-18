using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_DIS_DIS10 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData();

        }
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
        dtResult.Columns.Add("ERP Attribute", typeof(string));
        dtResult.Columns.Add("商品分類", typeof(string));
        dtResult.Columns.Add("維護人員", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["ERP Attribute"] = "2G ODM non-imode HS MMS";
        NewRow["商品分類"] = "2G";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["ERP Attribute"] = "3.5G ODM imode HS imode";
        NewRow["商品分類"] = "3.5G";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "3";
        NewRow["ERP Attribute"] = "2G open mkt HS";
        NewRow["商品分類"] = "2G";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "4";
        NewRow["ERP Attribute"] = "3g open mkt HS MMS";
        NewRow["商品分類"] = "3G";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "5";
        NewRow["ERP Attribute"] = "Other Device(Non SIM Base) Computer";
        NewRow["商品分類"] = "OTHER";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
}
