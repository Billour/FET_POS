using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

namespace AdvTek.CustomControls
{
    /// <summary>
    /// ASPxGridViewEx 的摘要描述
    /// </summary>
    public class ASPxGridView : DevExpress.Web.ASPxGridView.ASPxGridView
    {
        public ASPxGridView()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
            
        }

        public bool PagerBarEnabled
        {
            set
            {
                if (!this.Enabled && value)
                {
                    this.Enabled = true;
                    if (this.Settings.ShowTitlePanel)
                    {
                        this.Settings.ShowTitlePanel = false;
                    }

                    foreach (GridViewColumn col in this.Columns)
                    {
                        if (col is GridViewCommandColumn)
                        {
                            col.Visible = false;
                        }
                    }
                }
                else
                {
                    if (!this.Settings.ShowTitlePanel)
                    {
                        this.Settings.ShowTitlePanel = true;
                    }

                    foreach (GridViewColumn col in this.Columns)
                    {
                        if (col is GridViewCommandColumn)
                        {
                            col.Visible = true;
                        }
                    }

                }
            }
        }

        private bool IsClearStatusWhenPageIndexChanged = true;
        public bool IsClearStatus
        {
            set { IsClearStatusWhenPageIndexChanged = value; }
            get { return IsClearStatusWhenPageIndexChanged; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ASPxGridViewEx_CustomCallback);            
            this.Templates.PagerBar = new CustomPagerBarTemplate();    
            //this.HtmlRowCreated += new ASPxGridViewTableRowEventHandler(ASPxGridView_HtmlRowCreated);
            this.DataBound += new EventHandler(ASPxGridView_DataBound);
            this.PageIndexChanged += new EventHandler(ASPxGridView_PageIndexChanged);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // TODO:                        
           
        }

        protected void ASPxGridView_DataBound(object sender, EventArgs e)
        {
            DataTable dt = this.DataSource as DataTable;
            
            for (int i=0; i < this.Columns.Count; i++)
            {
                GridViewDataColumn col = this.Columns[i] as GridViewDataColumn;
                if (col == null)
                {
                    if (this.Columns[i] is GridViewCommandColumn)
                    {
                        this.Columns[i].Caption = " ";
                    }

                    continue;
                }

                col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                if (dt != null && !string.IsNullOrEmpty(col.FieldName))
                {
                    DataColumn dc = dt.Columns[col.FieldName];
                    if (dc != null)
                    {
                        if (dc.DataType == typeof(string) || dc.DataType == typeof(DateTime))
                        {
                            col.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        }
                        else if (dc.DataType == typeof(bool))
                        {
                            col.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                        }
                        else
                        {
                            col.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                }                
            }
        }

        protected void ASPxGridView_PageIndexChanged(object sender, EventArgs e)
        {
            if (IsClearStatusWhenPageIndexChanged)
            {
                if (this.IsEditing) this.CancelEdit();
                this.Selection.UnselectAll();
                this.FocusedRowIndex = -1;
            }
        }

        //protected void ASPxGridView_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        //{
        //    //if (e.RowType == GridViewRowType.Header)
        //    //{
        //    //    foreach (TableCell c in e.Row.Cells)
        //    //    {
        //    //       // e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        //    //    }
        //    //}
        //}

        private void ASPxGridViewEx_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int Num;
            bool isNum = int.TryParse(e.Parameters, out Num);

            if (isNum)
            {
                this.SettingsPager.PageSize = int.Parse(e.Parameters);
                this.DataBind();
            }
        }
    }
}