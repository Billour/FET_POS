using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_DIS07_DIS07 : System.Web.UI.Page
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
        dtResult.Columns.Add("群組代碼", typeof(string));
        dtResult.Columns.Add("群組名稱", typeof(string));
        dtResult.Columns.Add("補貼金額", typeof(string));
        dtResult.Columns.Add("生效日(起)", typeof(string));
        dtResult.Columns.Add("生效日(迄)", typeof(string));
        dtResult.Columns.Add("基準補貼群組", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("維護人員", typeof(string));



        DataRow NewRow = dtResult.NewRow();
        NewRow["群組代碼"] = "群組代碼1";
        NewRow["群組名稱"] = "群組名稱1";
        NewRow["補貼金額"] = "101";
        NewRow["生效日(起)"] = "2010/05/01";
        NewRow["生效日(迄)"] = "2010/05/11";
        NewRow["基準補貼群組"] = "基準補貼群組1";
        NewRow["備註"] = "備註1";
        NewRow["更新日期"] = DateTime.Now.ToShortDateString();
        NewRow["維護人員"] = "維護人員1";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["群組代碼"] = "群組代碼2";
        NewRow["群組名稱"] = "群組名稱2";
        NewRow["補貼金額"] = "772";
        NewRow["生效日(起)"] = "2010/06/22";
        NewRow["生效日(迄)"] = "2010/07/02";
        NewRow["基準補貼群組"] = "基準補貼群組2";
        NewRow["備註"] = "備註2";
        NewRow["更新日期"] = DateTime.Now.ToShortDateString();
        NewRow["維護人員"] = "維護人員2";
        dtResult.Rows.Add(NewRow);





        return dtResult;
    }






    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.div1.Visible = true;
        this.div2.Visible = true;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

    }
}
