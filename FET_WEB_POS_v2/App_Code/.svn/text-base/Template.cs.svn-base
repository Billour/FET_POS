using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Web.ASPxGridView;

public class CommandColumnHeaderTemplate : ITemplate
{
    ASPxGridView gridView = null;
    public CommandColumnHeaderTemplate(ASPxGridView gridView)
    {
        this.gridView = gridView;
    }

    #region ITemplate Members

    public void InstantiateIn(Control container)
    {
        HtmlInputCheckBox cb = new HtmlInputCheckBox();
        container.Controls.Add(cb);
        cb.Attributes.Add("onclick", "gvMaster.SelectAllRowsOnPage(this.checked);");
        cb.Attributes.Add("title", "Select/Unselect all rows on the page");
    }

    #endregion
} 

