using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Drawing;

/// <summary>
/// CustomPagerBarTemplate 的摘要描述
/// </summary>
public class CustomPagerBarTemplate : ITemplate
{
    ASPxGridView grid;
    enum PageBarButtonType { First, Prev, Next, Last }

    protected ASPxGridView Grid { get { return grid; } }

    private int rowCount = 0;

    private string ClientInstanceName
    {
        get
        {
            return grid.ClientID;
            /*
            return this.grid.SettingsDetail.IsDetailGrid ? 
                grid.ClientID : grid.ClientInstanceName;
            */
        }
    }

    public CustomPagerBarTemplate() { }
    public CustomPagerBarTemplate(int count) 
    {
        this.rowCount = count;
    }

    public void InstantiateIn(Control container)
    {        
        this.grid = (ASPxGridView)((GridViewPagerBarTemplateContainer)container).Grid;
        grid.ControlStyle.BackColor = Color.FromName("#FFCC99");
        Table table = new Table();
        table.HorizontalAlign = HorizontalAlign.Right;
        table.BackColor =  Color.FromName("#FFCC99");
        container.Controls.Add(table);
        TableRow row = new TableRow();
        
        table.Rows.Add(row);
        if (this.rowCount > 0)
        {
            AddLiteralCell(row.Cells, string.Format("總筆數：{0}", this.rowCount));
        }
        else
        {
            AddLiteralCell(row.Cells, string.Format("總筆數：{0}", this.grid.VisibleRowCount));
        }
        
        AddLinkCell(row.Cells, IsButtonEnabled(PageBarButtonType.First), "~/Images/first.png", "function(s, e) { aspxGVPagerOnClick('" + ClientInstanceName + "','PBF'); }");
        AddLinkCell(row.Cells, IsButtonEnabled(PageBarButtonType.Prev), "~/Images/previous.png", "function(s, e) { aspxGVPagerOnClick('" + ClientInstanceName + "','PBP'); }");        
        AddLiteralCell(row.Cells, "第");
        AddTextBoxCell(row.Cells);
        AddLiteralCell(row.Cells, string.Format("/ {0} 頁", Grid.PageCount));
        AddLinkCell(row.Cells, IsButtonEnabled(PageBarButtonType.Next), "~/Images/next.png", "function(s, e) { aspxGVPagerOnClick('" + ClientInstanceName + "','PBN'); }");
        AddLinkCell(row.Cells, IsButtonEnabled(PageBarButtonType.Last), "~/Images/last.png", "function(s, e) { aspxGVPagerOnClick('" + ClientInstanceName + "','PN" + (this.grid.PageCount - 1).ToString() + "'); }");                
    }
    void AddButtonCell(TableCellCollection cells, bool enabled, string text, string clickHandlerName)
    {
        TableCell cell = new TableCell();
        cells.Add(cell);
        ASPxButton button = new ASPxButton();
        cell.Controls.Add(button);
        button.Text = text;
        button.AutoPostBack = false;
        button.UseSubmitBehavior = false;
        button.Enabled = enabled;        
        if (enabled)
            button.ClientSideEvents.Click = clickHandlerName;
    }
    void AddLinkCell(TableCellCollection cells, bool enabled, string imageUrl, string clickHandlerName)
    {
        TableCell cell = new TableCell();
        cells.Add(cell);
        ASPxHyperLink link = new ASPxHyperLink();
        cell.Controls.Add(link);
        link.ImageUrl = imageUrl;
        link.Enabled = enabled;
        if (enabled)
            link.ClientSideEvents.Click = clickHandlerName;
    }
    void AddTextBoxCell(TableCellCollection cells)
    {
        TableCell cell = new TableCell();
        cells.Add(cell);
        ASPxTextBox textBox = new ASPxTextBox();
        cell.Controls.Add(textBox);
        textBox.Width = 30;
        int pageNumber = Grid.PageIndex + 1;
        textBox.JSProperties["cpText"] = pageNumber;
        textBox.ClientSideEvents.Init = "function(s, e) { s.SetText(s.cpText); }";
        textBox.ClientSideEvents.ValueChanged = @"function(s, e) { 
            if (s.GetText() == '' || isNaN(Number(s.GetText())))
            {
                s.SetText(" + Grid.ClientInstanceName + @".pageIndex + 1);
            }
            else
            {
                var pageIndex = (parseInt(s.GetText()) <= 0) ? 0 : parseInt(s.GetText()) - 1;
                aspxGVPagerOnClick('" + ClientInstanceName + @"','PN' + pageIndex);
            }
        }";
        textBox.ClientSideEvents.KeyPress = @"function (s, e) {
            if (e.htmlEvent.keyCode != 13)
                return;
            
            if (isNaN(Number(s.GetText())))
            {
                s.SetText(" + Grid.ClientInstanceName + @".pageIndex + 1);
            }
            else
            {
                e.htmlEvent.cancelBubble = true;
                e.htmlEvent.returnValue = false;
                var pageIndex = (parseInt(s.GetText()) <= 0) ? 0 : parseInt(s.GetText()) - 1;
                aspxGVPagerOnClick('" + ClientInstanceName + @"','PN' + pageIndex);
            }
        }";
    }
    void AddComboBoxCell(TableCellCollection cells)
    {
        TableCell cell = new TableCell();
        cells.Add(cell);
        ASPxComboBox comboBox = new ASPxComboBox();
        cell.Controls.Add(comboBox);
        comboBox.Width = 50;
        comboBox.DropDownWidth = 50;
        comboBox.Items.Add(new ListEditItem("10"));
        comboBox.Items.Add(new ListEditItem("20"));
        comboBox.Items.Add(new ListEditItem("30"));
        comboBox.ValueType = Type.GetType("Int32");
        comboBox.Value = Grid.SettingsPager.PageSize;
        comboBox.ClientSideEvents.SelectedIndexChanged = @"function (s, e) {
            " + ClientInstanceName + @".PerformCallback(s.GetSelectedItem().text);
        }";
    }
    void AddLiteralCell(TableCellCollection cells, string text)
    {
        TableCell cell = new TableCell();

        cells.Add(cell);
        cell.Text = text;
        cell.Wrap = false;
    }
    void AddSpacerCell(TableCellCollection cells)
    {
        TableCell cell = new TableCell();
        cells.Add(cell);
        cell.Width = Unit.Percentage(100);
    }
    bool IsButtonEnabled(PageBarButtonType type)
    {
        if (Grid.PageIndex == 0)
        {
            if (type == PageBarButtonType.First || type == PageBarButtonType.Prev)
                return false;
        }
        if (Grid.PageIndex == Grid.PageCount - 1)
        {
            if (type == PageBarButtonType.Next || type == PageBarButtonType.Last)
                return false;
        }
        return true;
    }
}