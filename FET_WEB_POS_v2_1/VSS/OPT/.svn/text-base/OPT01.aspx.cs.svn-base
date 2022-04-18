using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using DevExpress.Web.ASPxEditors;

public partial class VSS_OPT_OPT01 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {

        }
    }

    protected void bindMasterData()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        ViewState["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();

    }

    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("支付方式", typeof(string));
        dtResult.Columns.Add("科目1", typeof(string));
        dtResult.Columns.Add("科目2", typeof(string));
        dtResult.Columns.Add("科目3", typeof(string));
        dtResult.Columns.Add("科目4", typeof(string));
        dtResult.Columns.Add("科目5", typeof(string));
        dtResult.Columns.Add("科目6", typeof(string));
        dtResult.Columns.Add("起始日期", typeof(string));
        dtResult.Columns.Add("結束日期", typeof(string));
        dtResult.Columns.Add("更新日期", typeof(string));
        dtResult.Columns.Add("更新人員", typeof(string));
        return dtResult;
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("支付方式", typeof(string));
        dtMaster.Columns.Add("科目1", typeof(string));
        dtMaster.Columns.Add("科目2", typeof(string));
        dtMaster.Columns.Add("科目3", typeof(string));
        dtMaster.Columns.Add("科目4", typeof(string));
        dtMaster.Columns.Add("科目5", typeof(string));
        dtMaster.Columns.Add("科目6", typeof(string));
        dtMaster.Columns.Add("起始日期", typeof(string));
        dtMaster.Columns.Add("結束日期", typeof(string));
        dtMaster.Columns.Add("更新日期", typeof(string));
        dtMaster.Columns.Add("更新人員", typeof(string));

        string[] array1 = { "有效", "尚未生效", "巳過期" };
        string[] array2 = { "現金", "信用卡", "禮券", "金融卡", "HappyGo" };
        for (int i = 0; i < 12; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["項次"] = i;
            dtMasterRow["狀態"] = array1[i % 3];
            dtMasterRow["支付方式"] = array2[i % 5];
            dtMasterRow["科目1"] = (i + 1).ToString("00");
            dtMasterRow["科目2"] = (i + 1).ToString("000");
            dtMasterRow["科目3"] = (i + 1).ToString("0000");
            dtMasterRow["科目4"] = (i + 1).ToString("000000");
            dtMasterRow["科目5"] = (i + 1).ToString("0000");
            dtMasterRow["科目6"] = (i + 1).ToString("0000");
            dtMasterRow["起始日期"] = DateTime.Now.AddDays(-i).ToString("yyyy/MM/dd");
            if (i == 3)
            {
                dtMasterRow["結束日期"] = "";
            }
            else
            {
                dtMasterRow["結束日期"] = DateTime.Now.AddMonths(i).ToString("yyyy/MM/dd");

            }
            //dtMasterRow["開始日期"] = Convert.ToDateTime("2010/07/01" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");
            //dtMasterRow["結束日期"] = Convert.ToDateTime("2010/07/30" + DateTime.Now.ToLongTimeString()).ToString("yyyy/MM/dd HH:mm:ss");
            dtMasterRow["更新日期"] = DateTime.Now.AddDays(-i).AddMinutes(i * 32).ToString("yyyy/MM/dd HH:mm:ss");
            dtMasterRow["更新人員"] = "王小明";
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Div1.Visible = true;
        //btnADD.Visible = true;
        //btnDelete.Visible = false;
        bindMasterData();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gvMaster.AddNewRow();

    }

    /// <summary>
    /// 主GridView的分頁變更事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        grid.DataSource = getMasterData();
        grid.DataBind();
    }


    protected void gvMaster_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        //DataRow[] DRSelf = dt.Select("項次='" +  e.Keys[0].ToString().Trim() + "'");
        //if (DRSelf.Length > 0)
        //{

        //    DRSelf[0]["類別"] = e.NewValues["類別"];
        //    DRSelf[0]["兑點代號"] = e.NewValues["兑點代號"];
        //    DRSelf[0]["兑點名稱"] = e.NewValues["兑點名稱"];
        //    DRSelf[0]["開始日期"] = e.NewValues["開始日期"];
        //    DRSelf[0]["結束日期"] = e.NewValues["結束日期"];
        //    DRSelf[0]["點數"] = e.NewValues["點數"];
        //    DRSelf[0]["兑換金額"] = e.NewValues["兑換金額"];
        //}
        grid.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
    }

    protected void gvMaster_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //若不使用DataSourceID時UPDATE INSERT 和DELPETE 都要自己控制 以下為UPDATE時需自行控制
        ASPxGridView grid = (ASPxGridView)sender;
        DataTable dt = ViewState["gvMaster"] as DataTable ?? null;
        gvMaster.CancelEdit();
        e.Cancel = true;
        grid.DataSource = dt;
        grid.DataBind();
        ////e.NewValues["TemplateID"] = Guid.NewGuid(); // I set this PK myself.

    }

    protected void gvMaster_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.VisibleIndex > -1)
        {
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                string status = gvMaster.GetRowValues(e.VisibleIndex, "狀態").ToString();

                if (status == "巳過期")
                {
                    e.Enabled = false;
                }

            }
        }

    }

    protected void gvMaster_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        if (gvMaster.IsEditing)
        {
            foreach (GridViewColumn col in gvMaster.Columns)
            {
                if (col is GridViewDataColumn)
                {
                    GridViewDataColumn dataCol = (GridViewDataColumn)col;
                    Type typeCol = col.GetType();
                    if (dataCol.FieldName != "結束日期")
                    {

                        string status1 = gvMaster.GetRowValuesByKeyValue(e.EditingKeyValue, "狀態").ToString();

                        if (status1.ToString() == "有效")
                        {
                            dataCol.ReadOnly = true;
                            dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                            switch (typeCol.Name)
                            {
                                case "GridViewDataComboBoxColumn":
                                    GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                                    cbCol.PropertiesComboBox.DropDownButton.Enabled = false;
                                    cbCol.PropertiesComboBox.DropDownButton.Visible = false;
                                    break;
                                case "GridViewDataDateColumn":
                                    GridViewDataDateColumn ddCol = (GridViewDataDateColumn)dataCol;
                                    ddCol.PropertiesDateEdit.DropDownButton.Visible = false;
                                    ddCol.PropertiesDateEdit.DropDownButton.Enabled = false;
                                    break;
                            }
                        }
                        else if (status1.ToString() == "尚未生效" && dataCol.FieldName != "起始日期")
                        {
                            dataCol.ReadOnly = true;
                            dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                            switch (typeCol.Name)
                            {
                                case "GridViewDataComboBoxColumn":
                                    GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                                    cbCol.PropertiesComboBox.DropDownButton.Enabled = false;
                                    cbCol.PropertiesComboBox.DropDownButton.Visible = false;
                                    break;
                                case "GridViewDataDateColumn":
                                    GridViewDataDateColumn ddCol = (GridViewDataDateColumn)dataCol;
                                    ddCol.PropertiesDateEdit.DropDownButton.Visible = false;
                                    ddCol.PropertiesDateEdit.DropDownButton.Enabled = false;
                                    break;
                            }
                        }
                        else if (status1.ToString() == "巳過期")
                        {
                            dataCol.ReadOnly = true;
                            dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                            switch (typeCol.Name)
                            {
                                case "GridViewDataComboBoxColumn":
                                    GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                                    cbCol.PropertiesComboBox.DropDownButton.Enabled = false;
                                    cbCol.PropertiesComboBox.DropDownButton.Visible = false;
                                    break;
                                case "GridViewDataDateColumn":
                                    GridViewDataDateColumn ddCol = (GridViewDataDateColumn)dataCol;
                                    ddCol.PropertiesDateEdit.DropDownButton.Visible = false;
                                    ddCol.PropertiesDateEdit.DropDownButton.Enabled = false;
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// add New Row Events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMaster_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        if (gvMaster.IsNewRowEditing)
        {
            foreach (GridViewColumn col in gvMaster.Columns)
            {
                if (col is GridViewDataColumn)
                {
                    GridViewDataColumn dataCol = (GridViewDataColumn)col;
                    Type typeCol = col.GetType();
                    if (dataCol.ReadOnly == false && dataCol.FieldName == "結束日期" && dataCol.FieldName == "起始日期")
                    {
                        dataCol.ReadOnly = true;
                        dataCol.PropertiesEdit.Style.Border.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
                        switch (typeCol.Name)
                        {
                            case "GridViewDataComboBoxColumn":
                                GridViewDataComboBoxColumn cbCol = (GridViewDataComboBoxColumn)dataCol;
                                cbCol.PropertiesComboBox.DropDownButton.Enabled = false;
                                cbCol.PropertiesComboBox.DropDownButton.Visible = false;
                                break;
                            case "GridViewDataDateColumn":
                                GridViewDataDateColumn ddCol = (GridViewDataDateColumn)dataCol;
                                ddCol.PropertiesDateEdit.DropDownButton.Visible = false;
                                ddCol.PropertiesDateEdit.DropDownButton.Enabled = false;
                                break;
                        }
                    }
                }
            }
        }
    }
}
