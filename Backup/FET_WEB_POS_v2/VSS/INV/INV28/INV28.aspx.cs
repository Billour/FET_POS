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
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.Data;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;

public partial class VSS_INV_INV28 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (logMsg.ROLE_TYPE == StringUtil.CStr(System.Configuration.ConfigurationManager.AppSettings["DefaultRoleHQ"]))
        {
            //彈跳視窗
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "AlertMessage", "alert('非門市人員無法使用此功能!!');", true);
            grid.Enabled = false;
            ASPxDateEdit1.Enabled = false;
            ASPxDateEdit1.ControlStyle.BackColor = System.Drawing.Color.LightGray;
            ASPxDateEdit2.Enabled = false;
            ASPxDateEdit2.ControlStyle.BackColor = System.Drawing.Color.LightGray;
            txtS_PRODNO.Enabled = false;
            txtE_PRODNO.Enabled = false;
            searchButton.Enabled = false;
            resetButton.Enabled = false;
            return;
        }
    }

    private void BindMasterData()
    {
        grid.DataSource = GetMasterData();
        grid.DataBind();
    }

    private DataTable GetMasterData()
    {
        INV28_Facade cINV28 = new INV28_Facade();
        return cINV28.Query_SEAL_OFF_PROD_M(ASPxDateEdit1.Text.Trim(), ASPxDateEdit2.Text.Trim(), txtS_PRODNO.Text.Trim(), txtE_PRODNO.Text.Trim(), logMsg.STORENO);
    }

    private void BindDetailData()
    {
        if (this.grid.FocusedRowIndex > -1)
        {
            object[] arrObject = (object[])grid.GetRowValues(this.grid.FocusedRowIndex, new string[] { "SEAL_OFF_PROD_ID", "SEAL_OFF_STORE_ID", "STORE_NO" });
            if (arrObject.Length == 3)
            {
                string parm01 = StringUtil.CStr(arrObject[0]);
                string parm02 = StringUtil.CStr(arrObject[1]);
                string parm03 = StringUtil.CStr(arrObject[2]);
                detailGrid.DataSource = GetDetailData(parm01, parm02);
                detailGrid.DataBind();
                ASPxLabel lblStoreNo = (ASPxLabel)detailGrid.FindTitleTemplateControl("Label5");
                if (lblStoreNo != null)
                    lblStoreNo.Text = "門市編號:" + parm03;
            }

        }

    }

    private DataTable GetDetailData(string sSEAL_OFF_PROD_ID, string sSEAL_OFF_STORE_ID)
    {
        return new INV28_Facade().Query_SEAL_OFF_IMEI(sSEAL_OFF_PROD_ID, sSEAL_OFF_STORE_ID);
    }

    private void SetInitState_detailGrid()
    {
        detailGrid.PageIndex = 0;
        detailGrid.FocusedRowIndex = -1;
        detailGrid.Selection.UnselectAll();
        detailGrid.CancelEdit();
    }

    #region Button 觸發事件

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        BindMasterData();
        grid.FocusedRowIndex = -1;
        grid.PageIndex = 0;
        detailGrid.Visible = false;
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
        detailGrid.Selection.UnselectAll();
        detailGrid.AddNewRow();
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        INV28_SEAL_OFF_IMEI_DTO INV28_DTO = new INV28_SEAL_OFF_IMEI_DTO();
        try
        {
            DataSet ds = new DataSet();

            DataTable dtSOI = new DataTable();
            dtSOI.TableName = INV28_DTO.SEAL_OFF_IMEI.TableName;
            dtSOI.Columns.Add("SEAL_OFF_IMEI_ID");
            dtSOI.Columns.Add("IMEI");
            dtSOI.Columns.Add("STORE_NO");
            dtSOI.Columns.Add("PRODNO");
            dtSOI.Columns.Add("PROD_NO");
            dtSOI.Columns.Add("MODI_USER");

            List<object> lstObj = this.detailGrid.GetSelectedFieldValues("SEAL_OFF_IMEI_ID");
            foreach (object li in lstObj)
            {
                DataRow dr = dtSOI.NewRow();
                dr["SEAL_OFF_IMEI_ID"] = StringUtil.CStr(li);
                dr["IMEI"] = INV28_Facade.GetIMEI(StringUtil.CStr(dr["SEAL_OFF_IMEI_ID"]));
                dr["STORE_NO"] = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "STORE_NO"));
                dr["PRODNO"] = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID"));
                dr["PROD_NO"] = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "PRODNO"));
                dr["MODI_USER"] = logMsg.MODI_USER;
                dtSOI.Rows.Add(dr);

                //**2011/03/10 Tina：註解，將IMEI異動的Method搬移至DeleteOne_SealOffIMEI()一起執行。
                //string Code = "";
                //string Message = "";
                //string sStoreNo = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "STORE_NO"));
                //string sProdNo = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID"));
                //string sIMEI = INV28_Facade.GetIMEI(StringUtil.CStr(dr["SEAL_OFF_IMEI_ID"]));
                //if (sIMEI != "")
                //{
                //    new INV28_Facade().SaveCHANGELOC(StringUtil.CStr(li), sIMEI, sStoreNo, sProdNo, logMsg.MACHINE_ID, logMsg.MODI_USER, ref Code, ref Message, "delete");
                //}
                //if (Code != "000") throw new Exception(Message);
            }

            INV28_Facade Facade = new INV28_Facade();
            Facade.DeleteOne_SealOffIMEI(dtSOI, logMsg.MACHINE_ID);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "") +"');", true);
        }
        BindMasterData();
        BindDetailData();
        //// DataTable dt =  grid.DataSource as DataTable;
        //grid_FocusedRowChanged(grid, new EventArgs());
    }

    #endregion

    #region gvMaster 觸發事件

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        BindMasterData();
        detailGrid.Visible = false;
    }

    protected void grid_FocusedRowChanged(object sender, EventArgs e)
    {
        if (this.grid.FocusedRowIndex >= 0)
        {
            detailGrid.Visible = true;
            SetInitState_detailGrid();

            object[] arrObject = (object[])grid.GetRowValues(this.grid.FocusedRowIndex, new string[] { "SEAL_OFF_PROD_ID", "SEAL_OFF_STORE_ID" });
            int FocusIndex = this.grid.FocusedRowIndex;
            if (arrObject.Length == 2)
            {
                string parm01 = StringUtil.CStr(arrObject[0]);
                string parm02 = StringUtil.CStr(arrObject[1]);
                BindMasterData();
                grid.FocusedRowIndex = FocusIndex;
                detailGrid.DataSource = GetDetailData(parm01, parm02);
                detailGrid.DataBind();
                detailGrid.Visible = true;
                if (grid.FocusedRowIndex > -1)
                {
                    if (Convert.ToDateTime(StringUtil.CStr(grid.GetRowValues(FocusIndex, "E_DATE"))) < DateTime.Now)
                    {
                        detailGrid.Enabled = false;
                        detailGrid.PagerBarEnabled = true;
                    }
                    else
                    {
                        detailGrid.Enabled = true;
                        detailGrid.PagerBarEnabled = true;
                        string sIMEIQty = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "IMEI_QTY"));
                        string sSEAL_OFF_QTY = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_QTY"));
                        if (Convert.ToInt32(sIMEIQty) >= Convert.ToInt32(sSEAL_OFF_QTY))
                        {
                            ASPxButton btnAdd = (ASPxButton)detailGrid.FindTitleTemplateControl("addButton");
                            ASPxButton btnDel = (ASPxButton)detailGrid.FindTitleTemplateControl("deleteButton");
                            if (btnAdd != null)
                                btnAdd.Enabled = false;
                        }
                    }
                }

            }
        }
    }

    #endregion

    #region gvDetail 觸發事件

    protected void detailGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView _grid = sender as ASPxGridView;
        int FocusIndex = this.grid.FocusedRowIndex;
        //寫入SEAL_OFF_IMEI
        if (this.grid.FocusedRowIndex > -1)
        {
            object[] arrObject = (object[])grid.GetRowValues(this.grid.FocusedRowIndex, new string[] { "SEAL_OFF_PROD_ID", "SEAL_OFF_STORE_ID", "PRODNO", "STORE_NO" });
            if (arrObject.Length == 4)
            {
                try
                {
                    string parm01 = StringUtil.CStr(arrObject[0]);
                    string parm02 = StringUtil.CStr(arrObject[1]);
                    string parm03 = StringUtil.CStr(arrObject[2]);
                    string parm04 = StringUtil.CStr(arrObject[3]);
                    DataTable dtTmp = new INV28_Facade().Query_IS_SEAL_OFF_IMEI_OVER(parm01, parm02);
                    if (dtTmp.Rows.Count > 0 && (Convert.ToInt32(StringUtil.CStr(dtTmp.Rows[0]["SEAL_OFF_QTY"])) <= Convert.ToInt32(StringUtil.CStr(dtTmp.Rows[0]["IMEI_QTY"]))))
                    {
                        throw new Exception("IMEI數量>=拆封數量");
                    }

                    INV28_SEAL_OFF_IMEI_DTO dtoINV28 = new INV28_SEAL_OFF_IMEI_DTO();
                    INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIDataTable _dt = dtoINV28.SEAL_OFF_IMEI;
                    INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIRow _dr = _dt.NewSEAL_OFF_IMEIRow();
                    _dr.SEAL_OFF_IMEI_ID = GuidNo.getUUID();
                    _dr.SEAL_OFF_PROD_ID = parm01;
                    _dr.PRODNO = parm03;
                    _dr.STORE_NO = parm04;
                    _dr.IMEI = ((ASPxTextBox)detailGrid.FindEditRowCellTemplateControl((GridViewDataColumn)_grid.Columns["IMEI"], "imeiTextBox")).Text.Trim();
                    _dr.CREATE_USER = logMsg.MODI_USER;//sQueryUserID;
                    _dr.CREATE_DTM = System.DateTime.Now;
                    _dr.MODI_USER = _dr.CREATE_USER;//sQueryUserID;
                    _dr.MODI_DTM = System.DateTime.Now;
                    _dr.SEAL_OFF_STORE_ID = parm02;
                    _dt.Rows.Add(_dr);

                    //**2011/03/10 Tina：註解，將IMEI異動的Method搬移至AddNewOne_SealOffImei()一起執行。
                    //string Code = "";
                    //string Message = "";
                    //new INV28_Facade().SaveCHANGELOC(_dr.IMEI, _dr.STORE_NO, _dr.PRODNO, logMsg.MACHINE_ID, _dr.MODI_USER, ref Code, ref Message, "add");
                    //if (Code != "000") throw new Exception(Message);

                    dtoINV28.AcceptChanges();
                    new INV28_Facade().AddNewOne_SealOffImei(dtoINV28, logMsg.MACHINE_ID);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r","").Replace("\n","") + "');", true);
                }
            }
        }
        _grid.CancelEdit();
        e.Cancel = true;

        BindDetailData();
    }

    protected void detailGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxGridView _grid = sender as ASPxGridView;
        try
        {
            INV28_SEAL_OFF_IMEI_DTO dtoINV28 = new INV28_SEAL_OFF_IMEI_DTO();
            INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIDataTable _dt = dtoINV28.SEAL_OFF_IMEI;
            INV28_SEAL_OFF_IMEI_DTO.SEAL_OFF_IMEIRow _dr = _dt.NewSEAL_OFF_IMEIRow();
            int FocusIndex = grid.FocusedRowIndex;
            _dr.IMEI = ((ASPxTextBox)detailGrid.FindEditRowCellTemplateControl((GridViewDataColumn)_grid.Columns["IMEI"], "imeiTextBox")).Text.Trim();

            string sSEAL_OFF_IMEI_ID = StringUtil.CStr(e.Keys[detailGrid.KeyFieldName]);
            INV28_Facade cINV28 = new INV28_Facade();
            DataTable dtTmp = cINV28.Query_SEAL_OFF_IMEI(sSEAL_OFF_IMEI_ID);
            if (dtTmp.Rows.Count > 0)
            {
                _dr.SEAL_OFF_PROD_ID = StringUtil.CStr(dtTmp.Rows[0]["SEAL_OFF_PROD_ID"]);
                _dr.SEAL_OFF_STORE_ID = StringUtil.CStr(dtTmp.Rows[0]["SEAL_OFF_STORE_ID"]);
            }

            _dr.SEAL_OFF_IMEI_ID = sSEAL_OFF_IMEI_ID;
            _dr.STORE_NO = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "STORE_NO"));
            _dr.PRODNO = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "PRODNO"));
            _dr.MODI_USER = logMsg.MODI_USER;//sQueryUserID;
            _dr.MODI_DTM = System.DateTime.Now;
            _dt.Rows.Add(_dr);

            //**2011/03/10 Tina：註解，將IMEI異動的Method搬移至UpdateOne_SealOffImei()一起執行。
            //string Code = "";
            //string Message = "";
            //string sStoreNo = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "STORE_NO"));
            //string sProdNo = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID"));
            //if (ViewState["OldIMEI"] != null && !string.IsNullOrEmpty(StringUtil.CStr(ViewState["OldIMEI"])))
            //{
            //    string sOldIMEI = StringUtil.CStr(ViewState["OldIMEI"]);
            //    if (_dr.IMEI != sOldIMEI)
            //    {
            //        new INV28_Facade().SaveCHANGELOC(sOldIMEI, sStoreNo, sProdNo, logMsg.MACHINE_ID, _dr.MODI_USER, ref Code, ref Message, "delete");
            //        if (Code != "000") throw new Exception(Message);
            //        new INV28_Facade().SaveCHANGELOC(_dr.IMEI, sStoreNo, sProdNo, logMsg.MACHINE_ID, _dr.MODI_USER, ref Code, ref Message, "add");
            //        if (Code != "000") throw new Exception(Message);
            //    }
            //}

            bool IsDiff = false;
            string sOldIMEI = "";
            if (ViewState["OldIMEI"] != null && !string.IsNullOrEmpty(StringUtil.CStr(ViewState["OldIMEI"])))
            {
                sOldIMEI = StringUtil.CStr(ViewState["OldIMEI"]);
                if (_dr.IMEI != sOldIMEI)
                {
                    IsDiff = true;
                }
            }

            dtoINV28.AcceptChanges();
            cINV28.UpdateOne_SealOffImei(dtoINV28, logMsg.MACHINE_ID, IsDiff, sOldIMEI);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Common", @"alert('" + ex.Message.Replace("'", "-").Replace("\"", " ").Replace("\r", "").Replace("\n", "") + "');", true);
        }
        e.Cancel = true;
        _grid.CancelEdit();
        BindDetailData();
    }

    protected void gvDetail_PreRender(object sender, EventArgs e)
    {
        if (grid.FocusedRowIndex > -1)
        {
            string sStoreNo = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "STORE_NO"));
            ASPxLabel lblStoreNo = (ASPxLabel)detailGrid.FindTitleTemplateControl("Label5");
            if (lblStoreNo != null)
                lblStoreNo.Text = "門市編號:" + sStoreNo;
        }
    }

    protected void detailGrid_PageIndexChanged(object sender, EventArgs e)
    {
        BindDetailData();
    }

    protected void detailGrid_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        string sProdNo = "", sSealOffProdId = "", sOldIMEI = "";
        if (grid.FocusedRowIndex > -1)
        {
            sProdNo = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "PRODNO"));
            sSealOffProdId = StringUtil.CStr(grid.GetRowValues(grid.FocusedRowIndex, "SEAL_OFF_PROD_ID"));

            if (e.NewValues["IMEI"] == null || StringUtil.CStr(e.NewValues["IMEI"]).Trim() == "")
                e.RowError = "請輸入IMEI!";
            else
            {
                if (ViewState["OldIMEI"] != null)
                    sOldIMEI = StringUtil.CStr(ViewState["OldIMEI"]);
                if (detailGrid.IsEditing)
                {
                    if (INV28_Facade.GetIMEIByProdnoIsExist(sProdNo, StringUtil.CStr(e.NewValues["IMEI"]), logMsg.STORENO, sOldIMEI) == 0)
                        e.RowError = "【" + StringUtil.CStr(e.NewValues["IMEI"]) + "】不存在，請重新輸入!";
                }

                DataTable dt = null;
                if (detailGrid.IsNewRowEditing)
                    dt = INV28_Facade.getImeiByProdno(sProdNo, StringUtil.CStr(e.NewValues["IMEI"]), "");
                else
                    dt = INV28_Facade.getImeiByProdno(sProdNo, StringUtil.CStr(e.NewValues["IMEI"]), StringUtil.CStr(e.Keys[0]));

                if (dt.Rows.Count > 0)
                    e.RowError = "此IMEI已新增，請重新輸入!";
            }
        }
    }

    protected void detailGrid_StartRowEditing(object sender, ASPxStartRowEditingEventArgs e)
    {
        detailGrid.Selection.UnselectAll();
        if (detailGrid.IsEditing)
        {
            string sIMEI = StringUtil.CStr(detailGrid.GetRowValuesByKeyValue(e.EditingKeyValue, "IMEI"));
            ViewState["OldIMEI"] = sIMEI;
        }
    }

    #endregion
}
