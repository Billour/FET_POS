using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CheckOut_CheckOutHG2 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }

    /*
    protected void btnStep1_OK_Click(object sender, EventArgs e)
    {
        divStep1.Style["display"] = "none";
        divStep2.Style["display"] = "";
        bindMasterData();
        divStep3.Style["display"] = "none";
    }
    protected void btnStep2_OK_Click(object sender, EventArgs e)
    {
        divStep1.Style["display"] = "none";
        divStep2.Style["display"] = "none";
        divStep3.Style["display"] = "";

        Label6.Text = HiddenField1.Value;
    }
    protected void btnStep3_Pre_Click(object sender, EventArgs e)
    {
        divStep1.Style["display"] = "none";
        divStep2.Style["display"] = "";
        divStep3.Style["display"] = "none";
        Label3.Text = Label6.Text; 
    }
    */


    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    protected void bindGridView1Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = GetGridView1Data();
        //ViewState["gv1"] = dtResult;
        //GridView1.DataSource = dtResult;
        //GridView1.DataBind();
    }

    protected void bindGridView2Data()
    {
        DataTable dtResult = new DataTable();
        dtResult = GetGridView2Data();
        //ViewState["gv2"] = dtResult;
        //GridView2.DataSource = dtResult;
        //GridView2.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("項目名稱", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["項目名稱"] = "180點兌換50元";
        NewRow["數量"] = "0";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["項目名稱"] = "350點兌換100元";
        NewRow["數量"] = "0";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "3";
        NewRow["項目名稱"] = "1000點兌換300元";
        NewRow["數量"] = "0";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "4";
        NewRow["項目名稱"] = "1700點兌換500元";
        NewRow["數量"] = "0";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    private DataTable GetGridView1Data()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("項目名稱", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["活動名稱"] = "iPhone 3G優惠折抵";
        NewRow["項目名稱"] = "150點兌換80元";
        NewRow["數量"] = "0";
        dtResult.Rows.Add(NewRow);
        
        return dtResult;
    }

    private DataTable GetGridView2Data()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("項目名稱", typeof(string));
        dtResult.Columns.Add("數量", typeof(string));

        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["活動名稱"] = "iPhone 4G促銷活動";
        NewRow["項目名稱"] = "150點兌換70元";
        NewRow["數量"] = "0";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["活動名稱"] = "iPhone 4G促銷活動";
        NewRow["項目名稱"] = "180點兌換100元";
        NewRow["數量"] = "3";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }


    protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        switch (e.NextStepIndex)
        {
            case 1:
                bindMasterData();
                bindGridView1Data();
                bindGridView2Data();
                break;
            case 2:
                break;
        }
    }

    protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
    {
        // When the Previous button is clicked, decrease the 
        // Wizard1.BorderWidth by 1.
        
    }

    protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        // When the Finish button is clicked, write a confirmation
        // that the wizard was completed to Label1, and make it visible.
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindows", @"
            window.close();
        ", true);
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
       bindMasterData();
       bindGridView1Data();
       bindGridView2Data();
    }
}
