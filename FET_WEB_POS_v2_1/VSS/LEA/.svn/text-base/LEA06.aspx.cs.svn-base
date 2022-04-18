using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_LEA_LEA06 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindEmptyData();
        }
    }

    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();


    }

    protected void bindData1()
    {
        DataTable dtResult = new DataTable();
        dtResult = getData1();
        ASPxGridView1.DataSource = dtResult;
        ASPxGridView1.DataBind();
    }



    private DataTable getData1()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("No", typeof(string));
        dtResult.Columns.Add("手機地點", typeof(string));
        dtResult.Columns.Add("客戶姓名", typeof(string));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("預定領取日", typeof(string));
        dtResult.Columns.Add("預約歸還日", typeof(string));
        dtResult.Columns.Add("租金", typeof(string));
        dtResult.Columns.Add("賠償金", typeof(string));
        dtResult.Columns.Add("賠償原因", typeof(string));
        dtResult.Columns.Add("折扣金額", typeof(string));
        dtResult.Columns.Add("總金額", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["No"] = "1";
        NewRow["手機地點"] = "台北";
        NewRow["客戶姓名"] = "蕭大騰";
        NewRow["客戶門號"] = "3333939889";
        NewRow["預定領取日"] = "2010/09/09";
        NewRow["預約歸還日"] = "2010/09/10";
        NewRow["租金"] = "1000";
        NewRow["賠償金"] = "2000";
        NewRow["賠償原因"] = "手機損毀";
        NewRow["折扣金額"] = "500";
        NewRow["總金額"] = "3000";
        NewRow["備註"] = "test";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["No"] = "2";
        NewRow["手機地點"] = "台中";
        NewRow["客戶姓名"] = "蕭大騰0";
        NewRow["客戶門號"] = "33339398890";
        NewRow["預定領取日"] = "2010/09/11";
        NewRow["預約歸還日"] = "2010/09/30";
        NewRow["租金"] = "10000";
        NewRow["賠償金"] = "20000";
        NewRow["賠償原因"] = "泡水";
        NewRow["折扣金額"] = "5000";
        NewRow["總金額"] = "30000";
        NewRow["備註"] = "test";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        bindData1();
    }
}
