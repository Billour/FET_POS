using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class VSS_CHK_CHK03 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.Button3.Enabled = false;
        this.btnSave.Enabled = false;
        //Label32.Visible = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //this.DIV1.Visible = true;
        bindgvMaster();
        bindgvMaster0();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    { }

    protected void bindgvMaster()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("收款日期", typeof(string));
        dtMaster.Columns.Add("保全人員", typeof(string));
        dtMaster.Columns.Add("封條號碼", typeof(string));
        dtMaster.Columns.Add("收款金額", typeof(string));
        dtMaster.Columns.Add("處理人員", typeof(string));
        dtMaster.Columns.Add("起", typeof(string));
        dtMaster.Columns.Add("訖", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
       

        for (int i = 1; i < 12; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["收款日期"] = DateTime.Now.ToShortDateString();
            dtMasterRow["保全人員"] = "A000" + i;
            dtMasterRow["封條號碼"] = "封條號碼" + i;
            dtMasterRow["收款金額"] = 10 * (i + 1) / 2;
            dtMasterRow["處理人員"] = "處理人員" + i ;
            dtMasterRow["起"] = DateTime.Now.ToShortDateString();
            dtMasterRow["訖"] = DateTime.Now.ToShortDateString();
            dtMasterRow["更新日期"] = DateTime.Now.ToShortDateString();
           
            dtMaster.Rows.Add(dtMasterRow);

       
        }
        return dtMaster;

    }


    protected void bindgvMaster0()
    {
        DataTable dtGvMaster0 = new DataTable();
        dtGvMaster0 = getMasterData0();
        ViewState["gvMaster0"] = dtGvMaster0;
        gvMaster0.DataSource = dtGvMaster0;
        gvMaster0.DataBind();
    }

    private DataTable getMasterData0()
    {
        DataTable dtMaster0 = new DataTable();
        dtMaster0.Columns.Clear();
        dtMaster0.Columns.Add("封條號碼", typeof(string));
        dtMaster0.Columns.Add("收款金額", typeof(string));

        dtMaster0.Columns.Add("處理人員", typeof(string));
        dtMaster0.Columns.Add("起", typeof(string));
        dtMaster0.Columns.Add("訖", typeof(string));
      


        for (int i = 1; i < 12; i++)
        {
            DataRow dtMasterRow0 = dtMaster0.NewRow();
            dtMasterRow0["封條號碼"] = "封條號碼" + i;
            dtMasterRow0["收款金額"] = "555" + i;
            dtMasterRow0["處理人員"] = "處理人員" + i;

            dtMasterRow0["起"] = DateTime.Now.ToShortDateString();
            dtMasterRow0["訖"] = DateTime.Now.ToShortDateString();
          

            dtMaster0.Rows.Add(dtMasterRow0);


        }
        return dtMaster0;

    }
    protected void gvMaster_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {


        if (e.NewMode == DetailsViewMode.Edit)
        {
            gvMaster.ChangeMode(DetailsViewMode.Edit);
           
        }
        if (e.NewMode == DetailsViewMode.Insert)
        {
            gvMaster.ChangeMode(DetailsViewMode.Insert);
        }
        if (e.NewMode == DetailsViewMode.ReadOnly)
        {
            gvMaster.ChangeMode(DetailsViewMode.ReadOnly);
        }
       
        bindgvMaster();
    }
    protected void gvMaster_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {

        gvMaster.ChangeMode(DetailsViewMode.ReadOnly);
        bindgvMaster();
         
    }
    protected void gvMaster0_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        gvMaster0.ChangeMode(DetailsViewMode.ReadOnly);
        bindgvMaster0();
    }
    protected void gvMaster0_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        if (e.NewMode == DetailsViewMode.Edit)
        {
            gvMaster0.ChangeMode(DetailsViewMode.Edit);

        }
        if (e.NewMode == DetailsViewMode.Insert)
        {
            gvMaster0.ChangeMode(DetailsViewMode.Insert);
        }
        if (e.NewMode == DetailsViewMode.ReadOnly)
        {
            gvMaster0.ChangeMode(DetailsViewMode.ReadOnly);
        }

        bindgvMaster0();
    }
}
