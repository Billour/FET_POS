using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;

public partial class DISGridViewPanel : System.Web.UI.UserControl
{
    public DataTable dt { get; set; }
    public string KeyFieldName { get; set; }
    public string ItemName { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindData()
    {
        gvMaster.DataSource = null;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        gvMaster.KeyFieldName = KeyFieldName;

        AddCheckBoxColumns();
        ShowItem();

    }

    protected void gvMaster_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.gvMaster.AddNewRow();
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.InlineEdit)
        {
            PopupControl sPopup = Page.LoadControl("../../Controls/PopupControl.ascx") as PopupControl;

            switch (hdItemName.Value)
            {
                case "指定商品":
                    sPopup.PopupControlName = "ProductsPopup";
                    sPopup.ID = "ucProduct";
                    sPopup.IsValidation = true;
                    e.Row.Cells[1].Controls.Clear();
                    e.Row.Cells[1].Controls.Add(sPopup);
                    break;
                case "指定門市":
                    sPopup.PopupControlName = "StoresPopup";
                    sPopup.ID = "ucStore";
                    e.Row.Cells[1].Controls.Clear();
                    e.Row.Cells[1].Controls.Add(sPopup);
                    break;
                case "指定促銷":
                    sPopup.PopupControlName = "PromotionsPopupOnly";
                    sPopup.ID = "ucPromotion";
                    e.Row.Cells[1].Controls.Clear();
                    e.Row.Cells[1].Controls.Add(sPopup);
                    break;
                case "成本中心":
                    sPopup.PopupControlName = "CostCenterPopup";
                    sPopup.ID = "ucCostCenter";
                    e.Row.Cells[1].Controls.Clear();
                    e.Row.Cells[1].Controls.Add(sPopup);

                    ASPxComboBox cb = new ASPxComboBox();
                    cb.Items.Add("手機類");
                    cb.Items.Add("配件類");
                    cb.Items.Add("3C");
                    cb.Items.Add("新啟用");
                    cb.Items.Add("續約");
                    cb.Items.Add("通路");
                    cb.ID = "cbProductType";
                    e.Row.Cells[2].Controls.Clear();
                    e.Row.Cells[2].Controls.Add(cb);
                    break;
                default:
                    break;
            }
        }


    }

    protected void gvMaster_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            if (e.Row.BackColor != System.Drawing.Color.White)
            {
                e.Row.BackColor = System.Drawing.Color.White;
            }
        }
    }

    protected void gvMaster_CancelRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
    {
        AddCheckBoxColumns();
    }

    private void AddCheckBoxColumns()
    {
        if (gvMaster.Columns.Count > 0)
        {
            if (gvMaster.Columns[0].GetType().Name != "GridViewCommandColumn")
            {
                GridViewCommandColumn col = new GridViewCommandColumn();
                col.Caption = " ";
                col.ShowSelectCheckbox = true;
                col.VisibleIndex = 0;
                col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                col.ButtonType = ButtonType.Button;
                gvMaster.Columns.Insert(0, col);
                gvMaster.Selection.UnselectAll();
            }
            gvMaster.Columns[0].HeaderTemplate = new CommandColumnHeaderTemplate(gvMaster);
        }

    }

    private void ShowItem()
    {
        hdItemName.Value = ItemName;

        

        switch (ItemName)
        {
            case "指定門市":
                ASPxButton btnTimes = this.gvMaster.FindChildControl<ASPxButton>("btnTimes");
                ASPxLabel lblRemainingTimes = this.gvMaster.FindChildControl<ASPxLabel>("lblRemainingTimes");
                btnTimes.Visible = true;  //均平次數
                lblRemainingTimes.Visible = true;  //顯示剩餘數量

                

                break;
            case "客戶對象":
                if (gvMaster.Columns["客戶對象"] != null)
                {
                    gvMaster.Columns["客戶對象"].Visible = false;
                }
                break;

            case "指定商品":
                ASPxButton btnImport = this.gvMaster.FindChildControl<ASPxButton>("btnImport");
                btnImport.PostBackUrl = "../VSS/DIS/DIS01_Import.ASPX";
            

              
                break;
            default:
                break;

        }
    }
}
