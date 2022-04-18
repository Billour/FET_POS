using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;

public partial class VSS_SAL_SAL05_DiscountDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("折扣料號", typeof(string));
            dtResult.Columns.Add("折扣名稱", typeof(string));
            dtResult.Columns.Add("折扣金額", typeof(int));
            dtResult.Columns.Add("開始日期", typeof(string));
            dtResult.Columns.Add("結束日期", typeof(string));

            DataRow NewRow = dtResult.NewRow();
            NewRow["折扣料號"] = "00001";
            NewRow["折扣名稱"] = "賓賓有理";
            NewRow["折扣金額"] = 199;
            NewRow["開始日期"] = "2010/07/01";
            NewRow["結束日期"] = "2010/07/01";
            dtResult.Rows.Add(NewRow);

            NewRow = dtResult.NewRow();
            NewRow["折扣料號"] = "00002";
            NewRow["折扣名稱"] = "iPhone折抵";
            NewRow["折扣金額"] = 1990;
            NewRow["開始日期"] = "2010/07/02";
            NewRow["結束日期"] = "2010/07/03";
            dtResult.Rows.Add(NewRow);

            gvMaster.DataSource = dtResult;
            gvMaster.DataBind();
        }
    }
}
