using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CONGridView : System.Web.UI.UserControl
{
    public DataTable dt { get; set; }
    public string KeyFieldName { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BindData()
    {

        gvMaster.Columns.Clear();
        gvMaster.DataSource = null;
        gvMaster.AutoGenerateColumns = true;
        gvMaster.DataSource = dt;
        gvMaster.DataBind();
        gvMaster.KeyFieldName = KeyFieldName;
      
    }
}
