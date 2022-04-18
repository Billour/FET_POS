using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_PRE01_PRE01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterEmptyData();
            bindEmptyGvMaster2();
            //this.popupDIV.Style.Add("display","none");        
        }
    }
    protected void bindMasterEmptyData()
    {
        DataTable dtResult = new DataTable();
       
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    protected void bindEmptyGvMaster2()
    {
        DataTable dtResult = new DataTable();
        
        gvMaster2.DataSource = dtResult;
        gvMaster2.DataBind();
        gvMaster2.Visible = true;
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("活動代號", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));
        dtResult.Columns.Add("預購金額", typeof(string));
        dtResult.Columns.Add("總價", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["活動代號"] = "App004";
        NewRow["活動名稱"] = "iPhone4 32G 預購";
        NewRow["數量"] = "1";
        NewRow["預購金額"] = "10000";
        NewRow["總價"] = "10000";
        NewRow["備註"] = "送塑膠保護套一個";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["活動代號"] = "App099";
        NewRow["活動名稱"] = "iPad搶先購";
        NewRow["數量"] = "1";
        NewRow["預購金額"] = "12000";
        NewRow["總價"] = "12000";
        NewRow["備註"] = "送保護貼*1";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void bindMasterData2()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData2();
        gvMaster2.DataSource = dtResult;
        gvMaster2.DataBind();
    }
    private DataTable getMasterData2()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("付款方式", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));
        dtResult.Columns.Add("付款明細", typeof(string));
  

        DataRow NewRow = dtResult.NewRow();
        NewRow["付款方式"] = "現金";
        NewRow["金額"] = "1000";
        NewRow["付款明細"] = "付款明細1";
   
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["付款方式"] = "信用卡";
        NewRow["金額"] = "9000";
        NewRow["付款明細"] = "付款明細2";
        dtResult.Rows.Add(NewRow);
         
        decimal a = 0;
        for (int i = 0; i < dtResult.Rows.Count; i++)
        {
            a += Convert.ToDecimal(dtResult.Rows[i]["金額"].ToString());
        }
        Label2.Text = a.ToString();

        return dtResult;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        gvMaster.Visible = true;
        bindMasterData();
        Label5.Text = "10000";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        gvMaster2.Visible = true;
        bindMasterData2();
    }
    
    //protected void btnSave0_Click(object sender, EventArgs e)
    //{
    //    gvMaster.Visible = true;
    //    bindMasterData();
    //}
    protected void Button6_Click(object sender, EventArgs e)
    {
        Label9.Text = "PR-1234-01-100912345";
        lbInvoceNo.Text = "FX0987654";
    }
    protected void btnCredit_Click(object sender, EventArgs e)
    {
        gvMaster2.Visible = true;
        bindMasterData2();
    }
}
