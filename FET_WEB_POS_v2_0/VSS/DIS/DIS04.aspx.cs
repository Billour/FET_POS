using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_DIS04_DIS04 : System.Web.UI.Page
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
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("群組關連代號", typeof(string));
        dtResult.Columns.Add("群組關連名稱", typeof(string));
        dtResult.Columns.Add("有效日期(起)", typeof(string));
        dtResult.Columns.Add("有效日期(迄)", typeof(string));
        dtResult.Columns.Add("類別", typeof(string));
        dtResult.Columns.Add("人員", typeof(string));
        dtResult.Columns.Add("日期", typeof(string));



        DataRow NewRow = dtResult.NewRow();
        NewRow["狀態"] = "狀態1";
        NewRow["群組關連代號"] = "群組關連代號1";
        NewRow["群組關連名稱"] = "群組關連名稱1";
        NewRow["有效日期(起)"] = "2010/05/01";
        NewRow["有效日期(迄)"] = "2010/05/11";
        NewRow["類別"] = "類別1";
        NewRow["人員"] = "人員1";
        NewRow["日期"] = DateTime.Now.ToShortDateString();
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["狀態"] = "狀態2";
        NewRow["群組關連代號"] = "群組關連代號2";
        NewRow["群組關連名稱"] = "群組關連名稱2";
        NewRow["有效日期(起)"] = "2010/06/22";
        NewRow["有效日期(迄)"] = "2010/07/02";
        NewRow["類別"] = "類別2";
        NewRow["人員"] = "人員2";
        NewRow["日期"] = DateTime.Now.ToShortDateString();
        dtResult.Rows.Add(NewRow);




        return dtResult;
    }






    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.gvMaster.Visible = true;
    }
}
