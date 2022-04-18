using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridView.Rendering;
using DevExpress.Web.ASPxPager;

public partial class VSS_INV_INV17 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !IsCallback)
        {
            bindEmptyData();
        }
    }
    protected void bindEmptyData()
    {

        DataTable dtResult = new DataTable();

        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();


    }
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
        dtMaster.Columns.Add("關帳年月", typeof(string));
        dtMaster.Columns.Add("關帳日", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
      

        int year = DateTime.Now.Year;
        int month = 0;
        for (int i = 12; i > 0 ; i--)
        {   
            month = i;
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["關帳年月"] = year.ToString() + "/" + month.ToString().PadLeft(2, '0');
            dtMasterRow["關帳日"] = year.ToString() + "/" + month.ToString().PadLeft(2, '0') + "/" + DateTime.DaysInMonth(year, month);// DateTime("2010/08/01").AddDays(i).ToShortDateString();
            dtMasterRow["更新人員"] = "更新人員" + i;
            dtMasterRow["更新日期"] = year.ToString() + "/" + month.ToString().PadLeft(2, '0') + "/" + (DateTime.DaysInMonth(year, month) - 1) + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
       
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }

  



    protected void btnNew_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();
    }
 
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindgvMaster();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        gvMaster.CancelEdit();
        e.Cancel = true;
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
        //e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.
    }

    protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        //gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        //gvMaster.DataBind();
    }



    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        //if (e.RowType == GridViewRowType.Detail)
        //{
        //    // 繫結明細資料表           
        //    ASPxGridView detailGrid = e.Row.FindChildControl<ASPxGridView>("detailGrid");
        //    detailGrid.DataSource = GetDetailData();
        //    detailGrid.DataBind();
        //}
    }


    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gvMaster = sender as ASPxGridView;
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {     
                string date = gvMaster.GetRowValues(e.VisibleIndex, "關帳日").ToString();          
                if (Convert.ToDateTime(date.ToString()) < DateTime.Now)
                {
                    e.Enabled = false;
                }                
            }
        }
    }

    protected void gvMaster_HtmlCommandCellPrepared(object sender, ASPxGridViewTableCommandCellEventArgs e) 
	{ 
        //if (e.CommandCellType == DevExpress.Web.ASPxGridView.GridViewTableCommandCellType.Data) 
        //{            
        //} 
	} 

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {                                    
            //e.Row.FindChildControl<Button>("")
            //GridViewTableCommandCell cmdCell = (GridViewTableCommandCell)e.Row.Cells[1]; // if 0 is the index of your Command Cell
            //cmdCell.Column.EditButton.Visible = false;

            //TO DO:

        }        
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        //// DataRow[] DRSelf = dt.Select("項次='" + e.Keys[0].ToString().Trim() + "'");
        // if (DRSelf.Length > 0)
        // {

        //     DRSelf[0]["類別"] = e.NewValues["類別"];
        //     DRSelf[0]["兑點代號"] = e.NewValues["兑點代號"];
        //     DRSelf[0]["兑點名稱"] = e.NewValues["兑點名稱"];
        //     DRSelf[0]["開始日期"] = e.NewValues["開始日期"];
        //     DRSelf[0]["結束日期"] = e.NewValues["結束日期"];
        //     DRSelf[0]["點數"] = e.NewValues["點數"];
        //     DRSelf[0]["兑換金額"] = e.NewValues["兑換金額"];
        // }
        gvMaster.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }


    //protected void gvMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    DataRowView view = e.Row.DataItem as DataRowView;
    //    if (view!=null)
    //    {
         
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            if (e.Row.RowIndex != -1)
    //            {

    //                if (Convert.ToDateTime(view.Row["關帳日"].ToString()) < DateTime.Now)
    //               {
    //                   if (e.Row.FindControl("btnEdit") != null)
    //                   {
    //                       Button btnEdit = (Button)(e.Row.FindControl("btnEdit"));
    //                       btnEdit.Enabled = false;
    //                       //LinkButton LkEdit = (LinkButton)(e.Row.FindControl("LkEdit"));
    //                       //LkEdit.Visible = false;
    //                   }
    //                   else
    //                   {
    //                       Button btnEdit = (Button)(e.Row.FindControl("btnEdit"));
    //                       btnEdit.Enabled = true ;
    //                   }
    //                   //if (e.Row.FindControl("btnChoose") != null)
    //                   //{
    //                   //    Button btnChoose = (Button)(e.Row.FindControl("btnChoose"));
    //                   //    btnChoose.Visible = false;
    //                   //}
    //               }
    //            }
    //        }
          
    //    }
    //}


  
    
}
