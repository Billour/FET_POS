using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Resources;

using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;
using Advtek.Utility;
using System.Drawing;

public partial class VSS_OPT_OPT02_OPT02 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //繫結信用卡別資料
            bindCardType(ddlCardType);
            ddlCardType.Items.Insert(0, new ListEditItem("ALL", null));
            ddlCardType.SelectedIndex = 0;

            //繫結狀態資料
            ddlStatus.Items.Add("ALL", null);
            ddlStatus.Items.Add("有效", "有效");
            ddlStatus.Items.Add("尚未生效", "尚未生效");
            ddlStatus.Items.Add("已過期", "已過期");
            ddlStatus.SelectedIndex = 0;

            //取得空的資料表(只有結構描述)
            gvMaster.DataSource = new OPT02_Facade().Query_CreditCardProceRate("-1",
                                                             "",
                                                             "",
                                                             "",
                                                             "",
                                                             "");
            gvMaster.DataBind();
        }
    }

    private bool DataCheck(System.Collections.Specialized.OrderedDictionary NewValues, int index, string status)
    {
        bool UpdateData = false;
        string msgStr = "";

        if (NewValues["S_DATE"] != null && StringUtil.CStr(NewValues["S_DATE"]).Trim() != "")
        {
            if (Convert.ToDateTime(NewValues["S_DATE"]) <= DateTime.Now)
            {
                if (status == "Update")
                {
                    string msgStr2 = StringUtil.CStr(gvMaster.GetRowValues(index, "STATUS"));
                    if (msgStr2 != "有效")
                    {
                        msgStr = "【開始日期】必須>【系統日】";
                    }
                    else
                    {
                        UpdateData = true;
                    }
                }
            }
            else
            {
                if (NewValues["E_DATE"] != null && StringUtil.CStr(NewValues["E_DATE"]).Trim() != "")
                {
                    if (Convert.ToDateTime(NewValues["E_DATE"]) <= Convert.ToDateTime(NewValues["S_DATE"]))
                    {
                        msgStr = "【結束日期】必須>【開始日期】";
                    }
                    else
                    {
                        UpdateData = true;
                    }
                }
                else
                {
                    UpdateData = true;
                }
            }

        }
        if (!UpdateData) { ScriptManager.RegisterClientScriptBlock(this, typeof(string), "RowUpdating", "alert('" + msgStr + "!');", true); }
        return UpdateData;
    }

    private void bindCardType(ASPxComboBox ddlList)
    {
        ddlList.TextField = OPT02_PageHelper.TextField;
        ddlList.ValueField = OPT02_PageHelper.ValueField;
        ddlList.DataSource = OPT02_PageHelper.GetCreditCardTypes();
        ddlList.DataBind();
    }

    private void bindMasterData()
    {
        DataTable dtGvMaster;
        dtGvMaster = new OPT02_Facade().Query_CreditCardProceRate(ddlCardType.Value, 
                                                             ddlStatus.Value,
                                                             txtSDate_S.Text,
                                                             txtSDate_E.Text,
                                                             txtEDate_S.Text,
                                                             txtEDate_E.Text);
        gvMaster.DataSource = dtGvMaster;

        gvMaster.DataBind();
        ViewState["gvMaster"] = dtGvMaster;
    }

    #region Button 觸發事件

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvMaster.PageIndex = 0;
        gvMaster.FocusedRowIndex = -1;
        gvMaster.Selection.UnselectAll();
        bindMasterData();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            List<object> gvPKValues = gvMaster.GetSelectedFieldValues(gvMaster.KeyFieldName);
            string pkFName = gvMaster.KeyFieldName;
            OPT02_CreditCardProceRate_DTO _OPT02_CreditCardProceRate_DTO = new OPT02_CreditCardProceRate_DTO();
            DataTable ProceDataTable = new DataTable();
            ProceDataTable.TableName = _OPT02_CreditCardProceRate_DTO.CREDIT_CARD_PROCE_RATE.TableName;

            ProceDataTable.Columns.Add(pkFName, typeof(string));

            if (ViewState["gvMaster"] == null) { return; }
            DataTable dt = new DataTable();
            if (ViewState["gvMaster"] != null) { dt = (DataTable)ViewState["gvMaster"]; };

            for (int i = 0; i < gvPKValues.Count; i++)
            {
                if (dt.AsEnumerable().Any(dr => dr.Field<string>("CCPR_ID").Equals(StringUtil.CStr(gvPKValues[i]))))
                {
                    DataRow ProceDataRow = ProceDataTable.NewRow();
                    ProceDataRow[pkFName] = StringUtil.CStr(gvPKValues[i]);
                    ProceDataTable.Rows.Add(ProceDataRow);

                    DataRow dr1 = dt.AsEnumerable().Single(dr => dr.Field<string>("CCPR_ID") == StringUtil.CStr(gvPKValues[i]));
                    dt.Rows.Remove(dr1);
                }
            }

            new OPT02_Facade().Delete_CREDIT_CARD_PROCE_RATE(ProceDataTable, pkFName);
            bindMasterData();

        }
        catch (Exception ex)
        {
            if (ex.Message != "DELETE SQL Execute 失敗. ")
            {
                Logger.Log.Error(ex.Message, ex);
            }
        }

    }

    #endregion

    #region gvMaster 觸發事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            e.Row.Attributes["canSelect"] = "false";

            if (StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS")) == "尚未生效")
            {
                e.Row.Attributes["canSelect"] = "true";
            }

        }
        else
        {
            if (e.RowType == GridViewRowType.InlineEdit && !gvMaster.IsNewRowEditing)
            {
                switch (StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS")))
                {
                    case "尚未生效":
                        break;
                    case "有效":
                        e.Row.Cells[3].Enabled = false;
                        e.Row.Cells[4].Enabled = false;
                        e.Row.Cells[5].Enabled = false;
                        e.Row.Cells[6].Enabled = false;
                        break;
                    case "已過期":
                        e.Row.Cells[3].Enabled = false;
                        e.Row.Cells[4].Enabled = false;
                        e.Row.Cells[5].Enabled = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));

                if (status1 == "已過期")
                {
                    e.Enabled = false;
                }
            }
            if (e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
            {
                string status1 = StringUtil.CStr(gvMaster.GetRowValues(e.VisibleIndex, "STATUS"));

                if (status1 == "尚未生效")
                {
                    e.Enabled = true;
                }
                else
                {
                    e.Enabled = false;
                }
            }
        }
    }

    protected void gvMaster_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        gvMaster.Selection.UnselectAll();
        if (e.Column.FieldName == "CREDIT_CARD_TYPE_ID")
        {
            ASPxComboBox card_type = (e.Editor as ASPxComboBox);

            bindCardType(card_type);

            if (card_type.SelectedIndex == -1)
            {
                card_type.SelectedIndex = 0;
            }
        }
    }

    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["S_DATE"] = DateTime.Today.AddDays(1);
    }

    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        OPT02_CreditCardProceRate_DTO ds = new OPT02_CreditCardProceRate_DTO();

        OPT02_CreditCardProceRate_DTO.CREDIT_CARD_PROCE_RATEDataTable dt = ds.CREDIT_CARD_PROCE_RATE;

        if (DataCheck(e.NewValues, ((ASPxGridView)sender).EditingRowVisibleIndex, "Update"))
        {
            dt.Columns["CREATE_USER"].AllowDBNull = true;
            dt.Columns["CREATE_DTM"].AllowDBNull = true;

            OPT02_CreditCardProceRate_DTO.CREDIT_CARD_PROCE_RATERow dr = dt.NewCREDIT_CARD_PROCE_RATERow();

            dr["CCPR_ID"] = e.Keys["CCPR_ID"];
            dr["CREDIT_CARD_TYPE_ID"] = e.NewValues["CREDIT_CARD_TYPE_ID"];
            dr["CHARGE_RATE"] = e.NewValues["CHARGE_RATE"];
            dr["S_DATE"] = e.NewValues["S_DATE"];
            dr["E_DATE"] = e.NewValues["E_DATE"] ?? Advtek.Utility.DateUtil.NullDateFormat("E_DATE");
            dr["MODI_USER"] = this.logMsg.OPERATOR;
            dr["MODI_DTM"] = DateTime.Now;

            dt.AddCREDIT_CARD_PROCE_RATERow(dr);

            ds.AcceptChanges();

            new OPT02_Facade().UpdateOne_CreditCardProceRate(ds);

            ((ASPxGridView)sender).CancelEdit();

        }
        else
        {
            ((ASPxGridView)sender).DataSource = dt;
            ((ASPxGridView)sender).DataBind();
        }
        bindMasterData();
        e.Cancel = true;


    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        if (DataCheck(e.NewValues, ((ASPxGridView)sender).EditingRowVisibleIndex, "Insert"))
        {
            new OPT02_Facade().AddNewOne_CreditCardProceRate(e.NewValues["CREDIT_CARD_TYPE_ID"],
                e.NewValues["CHARGE_RATE"], e.NewValues["S_DATE"], e.NewValues["E_DATE"], this.logMsg.OPERATOR);

            ((ASPxGridView)sender).CancelEdit();
        }

        e.Cancel = true;

        bindMasterData();
    }

    protected void gvMaster_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //檢查手續費必須小於100%
        if (Convert.ToDouble(e.NewValues["CHARGE_RATE"]) > 100)
        {
            e.RowError = "手續費利率不可超過100%。";
            return;
        }

        if (e.IsNewRow)
        {
            if (new OPT02_Facade().CheckDateLimit(string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(e.NewValues["S_DATE"]).Date), string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(e.NewValues["E_DATE"]).Date), StringUtil.CStr(e.NewValues["CREDIT_CARD_TYPE_ID"]), "", "1"))
            {
                e.RowError = "同一信用卡在同一時間區間不可有二筆以上的手續費設定。";
                return;
            }
        }

        float temp = 0;
        //檢查手續費率
        if (!float.TryParse(StringUtil.CStr(e.NewValues["CHARGE_RATE"]), out temp))
        {
            e.RowError = "手續費設定錯誤!!";
            return;
        }

        if (gvMaster.IsEditing && !e.IsNewRow)
        {
            if (new OPT02_Facade().CheckDateLimit(string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(e.NewValues["S_DATE"]).Date), string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(e.NewValues["E_DATE"]).Date), StringUtil.CStr(e.NewValues["CREDIT_CARD_TYPE_ID"]), StringUtil.CStr(e.Keys["CCPR_ID"]), "2"))
            {
                e.RowError = "同一信用卡在同一時間區間不可有二筆以上的手續費設定。";
                return;
            }
        }

        if (gvMaster.IsEditing && e.IsNewRow)
        {
            if (new OPT02_Facade().CheckDateLimit(string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(e.NewValues["S_DATE"]).Date), string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(e.NewValues["E_DATE"]).Date), StringUtil.CStr(e.NewValues["CREDIT_CARD_TYPE_ID"]), "", "1"))
            {
                e.RowError = "同一信用卡在同一時間區間不可有二筆以上的手續費設定。";
                return;
            }
        }

    }

    #endregion
}
