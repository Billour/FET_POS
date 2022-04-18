using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class GridViewPanel : System.Web.UI.UserControl
{
    public DataTable dt { get; set; }
    public string KeyFieldName { get; set; }
    public bool bLeftControl { get; set; }
    public string PanelText { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void BindData()
    { 
        ShowItem();
        this.lg.InnerText = PanelText;

        ASPxGridView1.Columns.Clear();
        ASPxGridView1.DataSource = null;
        ASPxGridView1.AutoGenerateColumns = true;
        ASPxGridView1.DataSource = dt;
        ASPxGridView1.DataBind();
        ASPxGridView1.KeyFieldName = KeyFieldName;
      

        GridViewCommandColumn col = new GridViewCommandColumn();
        col.Caption = " ";
        col.ShowSelectCheckbox = true;
        col.VisibleIndex = 0;
        col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        ASPxGridView1.Columns.Insert(0,col);
        ASPxGridView1.Selection.UnselectAll();
       
      
        if (bLeftControl)
        {
            Session["ASPxGridView1"] = dt;
        }
        else 
        {
            Session["ASPxGridView2"] = dt;
        }
    }

    protected void ASPxGridView1_DataBound(object sender, EventArgs e)
    {
        if (ASPxGridView1.Columns.Count > 0)
        {
            //if (ASPxGridView1.Columns[0].GetType().ToString() != "DevExpress.Web.ASPxGridView.GridViewCommandColumn")
            //{
            //    GridViewCommandColumn col = new GridViewCommandColumn();
            //    col.ShowSelectCheckbox = true;
            //    col.VisibleIndex = 0;

            //    col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            //    //HtmlInputCheckBox ck = new HtmlInputCheckBox();
            //    //ck.Attributes.Add("onclick","ASPxGridView1.SelectAllRowsOnPage(this.checked);");
            //    //ck.Attributes.Add("title", "Select/Unselect all rows on the page");
            //    //col.HeaderTemplate.InstantiateIn(ck);

            //    ASPxGridView1.Columns.Add(col);
            //}

            DataTable dt = (DataTable)ASPxGridView1.DataSource;

            if (bLeftControl)
            {
                Session["ASPxGridView1"] = dt;
            }
            else
            {
                Session["ASPxGridView2"] = dt;
            }
        }

    }

    private void ShowItem()
    {
        switch (PanelText)
        {
            case "Data資費候選項目":
                this.tr1.Visible = true;
                break;
            case "Voice資費候選項目":
                this.tr2.Visible = true;
                break;
            case "申裝類型候選項目":
                this.tr3.Visible = true;
                break;
            case "指定商品候選項目":
                this.tr4.Visible = true;
                break;
            case "促銷代碼候選項目":
                this.tr5.Visible = true;
                break;
            case "門市候選項目":
                this.tr6.Visible = true;
                break;
            case "部門候選項目":
                this.tr7.Visible = true;
                break;
            case "群組成員項目":
                this.tr8.Visible = true;
                break;

            default:

                break;
        }
    }

    protected void ASPxGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "折扣類型")
        {
           // e.Cell.Style[HtmlTextWriterStyle.Color] = "red";
            ASPxComboBox cb = new ASPxComboBox();
            cb.Items.Add("手機折扣");
            e.Cell.Controls.Clear();
            e.Cell.Controls.Add(cb);
            cb.SelectedIndex = 0;
        }

       
    }
}
