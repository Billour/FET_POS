using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

public partial class PopupControl : System.Web.UI.UserControl
{
    public string Text  //欄位值
    {
        get { return txtControl.Text; }
        set { txtControl.Text = value; }
    }

    public bool IsValidation   //是否為必填欄位(預設為false)
    {
        get { return txtControl.ValidationSettings.RequiredField.IsRequired; }
        set { txtControl.ValidationSettings.RequiredField.IsRequired = value; }
    }

    public string PopupControlName  //彈跳視窗的連結名稱
    {
        get { return ASPxPopupControl1.SkinID; }
        set { ASPxPopupControl1.SkinID = value; }
    }

    public bool AutoPostBack  //觸動TextBox是否要PostBack
    {
        get { return txtControl.AutoPostBack; }
        set { txtControl.AutoPostBack = value; }
    }


    public bool Enabled   //是否可編輯
    {
        set { txtControl.Enabled = btnControl.Enabled = ASPxPopupControl1.Enabled = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        this.txtControl.TextChanged += new EventHandler(txtControl_TextChanged);
        base.OnInit(e);
    }

    private void txtControl_TextChanged(object sender, EventArgs e)
    {
        OnTextChanged(e);
    }

    public event EventHandler TextChanged;
    protected void OnTextChanged(EventArgs e)
    {
        if (TextChanged != null)
        {
            TextChanged(this, e);
        }
    }

    //[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
    //new public event EventHandler TextChanged
    //{
    //    add { txtControl.TextChanged += value; }
    //    remove { txtControl.TextChanged -= value; }
    //}

    //private void txtControl_TextChanged(object sender, EventArgs e)
    //{
    //   OnTextChanged(e); 
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
