using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;

using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using FET.POS.Model.Common;
using System.Runtime.Serialization;


public partial class VSS_OPT_OPT01_Refactory : System.Web.UI.Page
{
    #region Class Varibles

    private IOPT01_Facade _OPT01_Facade;
    private OPT01_PaymentMethodSet_DTO _PaymentMethodSet_DTO;
    
    // Declare GridView gvPaymentMethodSet EventHandler
    private GridViewEditEventHandler eh_gvPaymentMethodSet_RowEditing;
    private GridViewUpdateEventHandler eh_gvPaymentMethodSet_RowUpdating;
    private GridViewCancelEditEventHandler eh_gvPaymentMethodSet_RowCancelingEdit;
    private GridViewRowEventHandler eh_gvPaymentMethodSet_RowDataBound;
    private GridViewCommandEventHandler eh_gv_RowCommand;
    //private GridViewRowEventHandler eh_gvPaymentMethodSet_RowCreated;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            //Do UseCase for UI
            Do_QueryPaymentMehtodSet();
            Do_AddNewOnePaymentMehtodSet();
            Do_UpdateOnePaymentMethodSet();

            HandlePostbackReBinding();//done
        }
        else
        {
            BindDdlPayMode();
            BindTitleGvPaymentMethodSet();                                   
        }
    }


    #region Do UseCase Methods

    private void Do_QueryPaymentMehtodSet()
    {
        Handon_BtnSearch_Click();
        //Handon_GvPaymentMethodSet_RowCreated();
    }

    private void Do_AddNewOnePaymentMehtodSet()
    {
        // Force gvPaymentMethodSet Add A New BlankRow
        btnNew.Click += delegate(object BtnNew, EventArgs BtnNewEargs)
        {
            gvPaymentMethodSet.ShowFooter = true;
            gvPaymentMethodSet.ShowFooterWhenEmpty = true;
            System.Web.UI.HtmlControls.HtmlTableRow tr =
             gvPaymentMethodSet.FindChildControl<System.Web.UI.HtmlControls.HtmlTableRow>("trEmptyData");
            if (tr != null)
            {
                // 隱藏顯示文字訊息的表格列
                tr.Visible = false;
            }

            //very important!
            BindGvPaymentMehtodSet();
            DropDownList ddlPmn = gvPaymentMethodSet.FooterRow.FindControl("footerDdlPayModeName") as DropDownList;
            ddlPmn.Items.Clear();
            ddlPmn.DataSource = OPT01_PageHelper.GetPaymentModeName(true);
            ddlPmn.DataValueField = "PayModeId";
            ddlPmn.DataTextField = "PayModeName";            
            ddlPmn.DataBind();

            ddlPmn.Items.Remove(ddlPmn.Items.FindByText("ALL"));
            

            // HandOn footerDdlPayModeName SelectedIndexChanged
            ((DropDownList)gvPaymentMethodSet.FooterRow.FindControl("footerDdlPayModeName")).SelectedIndexChanged
            += delegate(object o, EventArgs es)
            {
                ((Label)gvPaymentMethodSet.FooterRow.FindControl("footerPayModeName")).Text = ((DropDownList)o).SelectedValue;
            };

        };        
        
    }

    private void Do_UpdateOnePaymentMethodSet()
    {                
        Handon_GvPaymentMethodSet_RowUpdating();
        Handon_GvPaymentMethodSet_Canceling();        
        Handon_GvPaymentMethodSet_RowEditing();
        Handon_GvPaymentMethodSet_RowDataBound();
    }

    private void Handon_BtnSearch_Click()
    {
        btnSearch.Click += delegate(object BtnSearch, EventArgs BtnSearchEargs)        
          {
              try
              {
                  _OPT01_Facade = new OPT01_Facade();

                  DataTable dt = _OPT01_Facade.Query_PaymentMethodSet(txtAccountName.Text, ddlPayMode.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString());
                  ViewState["gvPaymentMethodSet"] = dt;

                  gvPaymentMethodSet.DataSource = dt;                  
                  gvPaymentMethodSet.DataBind();                  
              }
              catch (Exception ex)
              {                  
                  throw ex;
              }
              

          };
    }

    private void Handon_GvPaymentMethodSet_RowDataBound()
    {   
        //ensure previous event de-Registerd
        if (eh_gvPaymentMethodSet_RowDataBound != null)
        {            
            gvPaymentMethodSet.RowDataBound -= eh_gvPaymentMethodSet_RowDataBound;
        }

        //implement EventHandler Logic
        eh_gvPaymentMethodSet_RowDataBound
            = delegate(object sender, GridViewRowEventArgs e)
              {
                  //very important!
                  if (e.Row.RowType == DataControlRowType.DataRow
                      && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))                                                
                  {
                      //會計科目切六塊顯示
                      ((Label)e.Row.Cells[5].FindControl("itemAccountSub1")).Text
                          = ((Label)e.Row.Cells[4].FindControl("itemAccountCode")).Text.Substring(0, 1);
                      ((Label)e.Row.Cells[6].FindControl("itemAccountSub2")).Text
                          = ((Label)e.Row.Cells[4].FindControl("itemAccountCode")).Text.Substring(1, 1);
                      ((Label)e.Row.Cells[7].FindControl("itemAccountSub3")).Text
                          = ((Label)e.Row.Cells[4].FindControl("itemAccountCode")).Text.Substring(1, 1);
                      ((Label)e.Row.Cells[8].FindControl("itemAccountSub4")).Text
                          = ((Label)e.Row.Cells[4].FindControl("itemAccountCode")).Text.Substring(1, 1);
                      ((Label)e.Row.Cells[9].FindControl("itemAccountSub5")).Text
                          = ((Label)e.Row.Cells[4].FindControl("itemAccountCode")).Text.Substring(0, 1);
                      ((Label)e.Row.Cells[10].FindControl("itemAccountSub6")).Text
                          = ((Label)e.Row.Cells[4].FindControl("itemAccountCode")).Text.Substring(1, 1);

                      //支付狀態顯示

                  }                    
              };

        // handon eh_gvPaymentMethodSet_RowUpdating event
        gvPaymentMethodSet.RowDataBound += eh_gvPaymentMethodSet_RowDataBound;

    }

    private void Handon_GvPaymentMethodSet_RowUpdating()
    {
        try
        {
            //ensure previous event de-Registerd 
            if (eh_gvPaymentMethodSet_RowUpdating != null)
            {
                gvPaymentMethodSet.RowUpdating -= eh_gvPaymentMethodSet_RowUpdating;
            }

            //implement EventHandler Logic
            eh_gvPaymentMethodSet_RowUpdating
                = delegate(object GvPaymentMethodSet, GridViewUpdateEventArgs GvPaymentMethodSetEargs)
                {
                    GridView gridview = GvPaymentMethodSet as GridView;

                    //取得資料
                    _PaymentMethodSet_DTO = new OPT01_PaymentMethodSet_DTO();
                    OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETDataTable dtPaymentMethodSet;
                    OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETRow drPaymentMethodSet;
                    OPT01_PaymentMethodSet_DTO.PAY_MODEDataTable dtPayMode;
                    OPT01_PaymentMethodSet_DTO.PAY_MODERow drPayMode;

                    dtPayMode = _PaymentMethodSet_DTO.Tables["PAY_MODE"] as OPT01_PaymentMethodSet_DTO.PAY_MODEDataTable;
                    drPayMode = dtPayMode.NewPAY_MODERow();
                    drPayMode.PAY_MODE_ID = ((DropDownList)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editDdlPayModeName")).SelectedItem.Value.ToString();
                    drPayMode.PAY_MODE_NAME = ((DropDownList)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editDdlPayModeName")).SelectedItem.Text.ToString();                    
                    drPayMode.MODI_DTM = System.DateTime.Parse(((TextBox)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editModiDtm")).Text);
                    drPayMode.MODI_USER = ((TextBox)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editModiUser")).Text;
                    drPayMode.CREATE_DTM = System.DateTime.Now;
                    drPayMode.CREATE_USER = "Josh";

                    dtPayMode.Rows.Add(drPayMode);                    


                    dtPaymentMethodSet = _PaymentMethodSet_DTO.Tables["PAYMENT_METHOD_SET"] as OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETDataTable;
                    drPaymentMethodSet = dtPaymentMethodSet.NewPAYMENT_METHOD_SETRow();
                    drPaymentMethodSet.PAY_MODE_ID = ((DropDownList)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editDdlPayModeName")).SelectedItem.Value.ToString();
                    drPaymentMethodSet.PAYMENT_METHOD_ID = gridview.DataKeys[GvPaymentMethodSetEargs.RowIndex].Value.ToString();
                    drPaymentMethodSet.ACCOUNT_CODE = ((TextBox)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editAccountCode")).Text;
                    drPaymentMethodSet.S_DATE = System.DateTime.Parse(((TextBox)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editSDate")).Text);
                    drPaymentMethodSet.E_DATE = System.DateTime.Parse(((TextBox)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editEDate")).Text);
                    drPaymentMethodSet.MODI_DTM = System.DateTime.Parse(((TextBox)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editModiDtm")).Text);
                    drPaymentMethodSet.MODI_USER = ((TextBox)gridview.Rows[GvPaymentMethodSetEargs.RowIndex].FindControl("editModiUser")).Text;
                    drPaymentMethodSet.CREATE_DTM = System.DateTime.Now;
                    drPaymentMethodSet.CREATE_USER = "Josh";

                    dtPaymentMethodSet.Rows.Add(drPaymentMethodSet);


                    _PaymentMethodSet_DTO.AcceptChanges();

                    //更新資料庫
                    _OPT01_Facade = new OPT01_Facade();
                    _OPT01_Facade.UpdateOne_PaymentMethodSet(_PaymentMethodSet_DTO);

                    //取消編輯狀態
                    gridview.EditIndex = -1;
                    ViewState["gvPaymentMethodSet"] = _OPT01_Facade.Query_PaymentMethodSet(txtAccountName.Text, ddlPayMode.SelectedValue, ddlStatus.SelectedValue);
                    BindGvPaymentMehtodSet();
                };

            // handon eh_gvPaymentMethodSet_RowUpdating event
            gvPaymentMethodSet.RowUpdating += eh_gvPaymentMethodSet_RowUpdating;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _PaymentMethodSet_DTO = null;            
        }
    }

    private void Handon_GvPaymentMethodSet_RowEditing()
    {
        //ensure previous event deRegisterd
        if (eh_gvPaymentMethodSet_RowEditing != null)
        {
            gvPaymentMethodSet.RowEditing -= eh_gvPaymentMethodSet_RowEditing;
        }

        //implement EventHandler Logic
        eh_gvPaymentMethodSet_RowEditing = delegate(object GvPaymentMethodSet, GridViewEditEventArgs GvPaymentMethodSetEargs)
        {            
            GridView gv = GvPaymentMethodSet as GridView;
            gv.EditIndex = GvPaymentMethodSetEargs.NewEditIndex;           
            //very important!
            BindGvPaymentMehtodSet();
            DropDownList ddl = gv.Rows[GvPaymentMethodSetEargs.NewEditIndex].FindControl("editDdlPayModeName") as DropDownList;
            ddl.Items.Clear();
            ddl.DataSource = OPT01_PageHelper.GetPaymentModeName(true);
            ddl.DataValueField = "PayModeId";
            ddl.DataTextField = "PayModeName";                   
            ddl.DataBind();

            ddl.Items.Remove(ddl.Items.FindByText("ALL"));     
            ddl.Items.FindByValue(((Label)gv.Rows[GvPaymentMethodSetEargs.NewEditIndex].FindControl("editPayModeId")).Text).Selected = true;                                                         

        };

        // handon eh_gvPaymentMethodSet_RowEditing event
        gvPaymentMethodSet.RowEditing += eh_gvPaymentMethodSet_RowEditing;
    }

    private void Handon_GvPaymentMethodSet_Canceling()
    {
        //ensure previous event deRegisterd
        if (eh_gvPaymentMethodSet_RowCancelingEdit != null)
        {
            gvPaymentMethodSet.RowCancelingEdit -= eh_gvPaymentMethodSet_RowCancelingEdit;
        }

        //implement EventHandler Logic
        eh_gvPaymentMethodSet_RowCancelingEdit =
            delegate(object sender, GridViewCancelEditEventArgs e)
            {
                GridView gridview = sender as GridView;
                gridview.EditIndex = -1;
                BindGvPaymentMehtodSet();
            };
        gvPaymentMethodSet.RowCancelingEdit += eh_gvPaymentMethodSet_RowCancelingEdit;
    }

    //private void Handon_GvPaymentMethodSet_RowCreated()
    //{
    //    //ensure previous event deRegisterd
    //    if (eh_gvPaymentMethodSet_RowCreated != null)
    //    {
    //        gvPaymentMethodSet.RowCreated -= eh_gvPaymentMethodSet_RowCreated;
    //    }

    //    //implement EventHandler Logic
    //    eh_gvPaymentMethodSet_RowCreated =
    //        delegate(object sender, GridViewRowEventArgs e)
    //        {
    //            if (e.Row.RowType == DataControlRowType.DataRow)
    //            {
    //                Label lblSeqNo = e.Row.Cells[1].Controls[0] as Label;
    //                lblSeqNo.Text = Convert.ToString(e.Row.RowIndex + 1);

    //                Label lblItemStatus = e.Row.Cells[2].Controls[0] as Label;
    //                Label lblEDate;
    //                Label lblSDate;

    //                //if(System.DateTime.Today >= e.)
    //                //lblItemStatus.Text = "";
    //            }

    //            DataTable dt = ViewState["gvPaymentMethodSet"] as DataTable;

    //            //gvPaymentMethodSet.DataSource = dt;
    //            //gvPaymentMethodSet.DataBind(); 

    //        };
    //    gvPaymentMethodSet.RowCreated += eh_gvPaymentMethodSet_RowCreated;
    //}
    //private void Handon_Gv_RowCommand()
    //{ 
    //    if(eh_gv_RowCommand != null)
    //    {
    //        gvPaymentMethodSet.RowCommand -= eh_gv_RowCommand;
    //    }

    //    eh_gv_RowCommand =
    //        delegate(object sender, GridViewCommandEventArgs e)
    //        {
    //            switch (e.CommandSource)
    //            {
    //                default:
    //                    break;
    //            }
    //        };

    //    gvPaymentMethodSet.RowCommand += eh_gv_RowCommand;
    //}

    #endregion

    // Handle after-Postback reBinding !
    private void HandlePostbackReBinding()
    {
        //this.PreRenderComplete += delegate(object obj, EventArgs evt)
        //{            
        //    BindGvPaymentMehtodSet();
        //};
    }

    #region BindDropDownList
    private void BindDdlPayMode()
    {
        //支付方式
        ddlPayMode.DataSource = OPT01_PageHelper.GetPaymentModeName(true);
        ddlPayMode.DataValueField = "PayModeId";
        ddlPayMode.DataTextField = "PayModeName";
        ddlPayMode.DataBind();
    }
    #endregion

    #region BindGvPaymentMethodSet
    
    private void BindTitleGvPaymentMethodSet()
    {
        gvPaymentMethodSet.DataSource = GetTitleGvPaymentMehtodSet();
        gvPaymentMethodSet.DataBind();        
    }

    private void BindGvPaymentMehtodSet()
    {
        gvPaymentMethodSet.DataSource = ViewState["gvPaymentMethodSet"];
        gvPaymentMethodSet.DataBind();
    }
    
    #endregion

    #region GetGvPaymentMethodSet

    private DataTable GetTitleGvPaymentMehtodSet()
    {
        DataTable dtTitle = new DataTable();

        dtTitle.Columns.Add("PAYMENT_METHOD_ID", typeof(string));
        dtTitle.Columns.Add("PAY_MODE_ID", typeof(string));
        dtTitle.Columns.Add("PAY_MODE_NAME", typeof(string));
        dtTitle.Columns.Add("ACCOUNT_CODE", typeof(string));
        dtTitle.Columns.Add("S_DATE", typeof(string));
        dtTitle.Columns.Add("E_DATE", typeof(string));
        dtTitle.Columns.Add("MODI_DTM", typeof(string));
        dtTitle.Columns.Add("MODI_USER", typeof(string));
        return dtTitle;
    }

    private DataTable GetGvPaymentMehtodSet()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("PAYMENT_METHOD_ID", typeof(string));
        dt.Columns.Add("PAY_MODE_ID", typeof(string));
        dt.Columns.Add("PAY_MODE_NAME", typeof(string));
        dt.Columns.Add("ACCOUNT_CODE", typeof(string));
        dt.Columns.Add("S_DATE", typeof(string));
        dt.Columns.Add("E_DATE", typeof(string));
        dt.Columns.Add("MODI_DTM", typeof(string));
        dt.Columns.Add("MODI_USER", typeof(string));

        dt.Rows.Add(new object[] { "111", "2", "現金", "12", "2010/10/01", "2010/10/10", "2010/01/01", "" });
        dt.Rows.Add(new object[] { "112", "5", "禮券", "65", "2008/08/08", "2008/09/09", "2009/10/11", "" });
        dt.Rows.Add(new object[] { "113", "1", "現金", "33", "2009/01/01", "2009/11/11", "2009/11/30", "" });
        dt.Rows.Add(new object[] { "114", "3", "HappyGo", "35", "2010/10/10", "2010/11/11", "2010/11/12", "" });
        dt.Rows.Add(new object[] { "115", "6", "金融卡", "54", "2007/08/09", "2008/09/07", "2009/01/01", "" });

        return dt;
    }
    
    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
                
    }

    protected void btnSave_click(object sender, EventArgs e)
    {
        try
        {
            _OPT01_Facade = new OPT01_Facade();
            _PaymentMethodSet_DTO = new OPT01_PaymentMethodSet_DTO();


            OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETDataTable dtPaymentMethodSet = _PaymentMethodSet_DTO.PAYMENT_METHOD_SET;
            OPT01_PaymentMethodSet_DTO.PAYMENT_METHOD_SETRow drPaymentMethodSet = dtPaymentMethodSet.NewPAYMENT_METHOD_SETRow();
                        

            drPaymentMethodSet.PAYMENT_METHOD_ID = Advtek.Utility.GuidNo.getUUID();
            drPaymentMethodSet.PAY_MODE_ID = ((DropDownList)gvPaymentMethodSet.FooterRow.FindControl("footerDdlPayModeName")).SelectedValue;
            drPaymentMethodSet.ACCOUNT_CODE = ((TextBox)gvPaymentMethodSet.FooterRow.FindControl("footerAccountCode")).Text;
            drPaymentMethodSet.S_DATE = Convert.ToDateTime(((TextBox)gvPaymentMethodSet.FooterRow.FindControl("footerSDate")).Text);
            drPaymentMethodSet.E_DATE = Convert.ToDateTime(((TextBox)gvPaymentMethodSet.FooterRow.FindControl("footerEDate")).Text);
            drPaymentMethodSet.MODI_DTM = Convert.ToDateTime(((TextBox)gvPaymentMethodSet.FooterRow.FindControl("footerModiDtm")).Text);
            drPaymentMethodSet.MODI_USER = ((TextBox)gvPaymentMethodSet.FooterRow.FindControl("footerModiUser")).Text;
            drPaymentMethodSet.CREATE_USER = "Josh";
            drPaymentMethodSet.CREATE_DTM = Convert.ToDateTime(System.DateTime.Now);

            dtPaymentMethodSet.Rows.Add(drPaymentMethodSet);


            OPT01_PaymentMethodSet_DTO.PAY_MODEDataTable dtPayMode = _PaymentMethodSet_DTO.PAY_MODE;
            OPT01_PaymentMethodSet_DTO.PAY_MODERow drPayMode = dtPayMode.NewPAY_MODERow();

            drPayMode.PAY_MODE_ID = ((DropDownList)gvPaymentMethodSet.FooterRow.FindControl("footerDdlPayModeName")).SelectedValue;
            drPayMode.PAY_MODE_NAME = ((DropDownList)gvPaymentMethodSet.FooterRow.FindControl("footerDdlPayModeName")).SelectedItem.Text;
            drPayMode.MODI_DTM = Convert.ToDateTime(((TextBox)gvPaymentMethodSet.FooterRow.FindControl("footerModiDtm")).Text);
            drPayMode.MODI_USER = ((TextBox)gvPaymentMethodSet.FooterRow.FindControl("footerModiUser")).Text;
            drPayMode.CREATE_DTM = Convert.ToDateTime(System.DateTime.Now);
            drPayMode.CREATE_USER = "Josh";

            dtPayMode.Rows.Add(drPayMode);


            _PaymentMethodSet_DTO.AcceptChanges();


            _OPT01_Facade.AddNewOne_PaymentMethodSet(_PaymentMethodSet_DTO);
            ViewState["gvPaymentMethodSet"] = _OPT01_Facade.Query_PaymentMethodSet(txtAccountName.Text, ddlPayMode.SelectedValue, ddlStatus.SelectedValue);
            BindGvPaymentMehtodSet();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _OPT01_Facade = null;
            _PaymentMethodSet_DTO = null;
        }
        
    }

    protected void btnCancel_click(object sender, EventArgs e)
    {
        gvPaymentMethodSet.ShowFooterWhenEmpty = false;
        gvPaymentMethodSet.ShowFooter = false;

        BindGvPaymentMehtodSet();
    }

}
