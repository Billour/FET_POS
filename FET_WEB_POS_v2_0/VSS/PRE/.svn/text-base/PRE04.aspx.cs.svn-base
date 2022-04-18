using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_PRE_PRE04 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            ASPxGridView1.DataSource = GetEmptyDataTable();
            ASPxGridView1.DataBind();
        }
    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("活動代號", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("有效期間起始", typeof(string));
        dtResult.Columns.Add("有效期間結束", typeof(string));

        return dtResult;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("活動代號", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("有效期間起始", typeof(string));
        dtResult.Columns.Add("有效期間結束", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["狀態"] = "已生效";
        NewRow["活動代號"] = "i493090";
        NewRow["活動名稱"] = "iPhone4You";
        NewRow["有效期間起始"] = "2010/09/22";
        NewRow["有效期間結束"] = "2010/09/30";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["狀態"] = "已生效";
        NewRow["活動代號"] = "i928203";
        NewRow["活動名稱"] = "iPad is Lift";
        NewRow["有效期間起始"] = "2010/09/12";
        NewRow["有效期間結束"] = "2010/09/30";

        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }
}
