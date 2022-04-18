using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections.Specialized;

public partial class PopupControl : System.Web.UI.UserControl
{
    public string Text  //欄位值
    {
        get { return txtControl.Text; }
        set { txtControl.Text = value; }
    }

    public string TextBoxClientInstanceName
    {
        get { return txtControl.ClientInstanceName;  }
        set { txtControl.ClientInstanceName = value; }
    }

    public string PopupControlClientInstanceName
    {
        get { return ASPxPopupControl1.ClientInstanceName; }
        set { ASPxPopupControl1.ClientInstanceName = value; }
    }

    public string ButtonClientInstanceName
    {
        get { return btnControl.ClientInstanceName; }
        set { btnControl.ClientInstanceName = value; }
    }

    /// <summary>
    /// ClientValidationEvent自定事件
    /// </summary>
    public string SetClientValidationEvent
    {
        set { this.txtControl.ClientSideEvents.Validation += "function(s,e){ " + value + "(s,e);}"; }
    }

    public bool IsValidation   //是否為必填欄位(預設為false)
    {
        get { return txtControl.ValidationSettings.RequiredField.IsRequired; }
        set { txtControl.ValidationSettings.RequiredField.IsRequired = value; }
    }

    public string PopupControlName  //彈跳視窗的連結名稱
    {
        get
        {
            return ASPxPopupControl1.SkinID;
        }
        set
        {
            ASPxPopupControl1.SkinID = value;
            if (value == "InputIMEIData")
            {
                this.txtControl.ReadOnly = true;
                this.txtControl.BackColor = System.Drawing.Color.LightGray;
            }

        }
    }

    public string OnClientTextChanged
    {
        set
        {
            txtControl.ClientSideEvents.TextChanged += value;
        }
    }

    public string OnClientOnValidation
    {
        set
        {
            txtControl.ClientSideEvents.Validation += value;
        }
    }

    /// <summary>
    /// 回傳給指定的元件名稱 LABEL 或 TXTBOX 元件名稱
    /// </summary>
    public string AssignToControlId
    {
        get { return ASPxPopupControl1.AssignToControlId; }
        set { ASPxPopupControl1.AssignToControlId = value; }
    }

    public bool AutoPostBack  //觸動TextBox是否要PostBack
    {
        get { return txtControl.AutoPostBack; }
        set { txtControl.AutoPostBack = value; }
    }

    public DevExpress.Web.ASPxEditors.ASPxTextBox popTextBox
    {
        get { return txtControl; }
        set { txtControl = value; }
    }

    public string Width
    {
        get { return tdControl.Width; }
        set { tdControl.Width = value; }
    }
    /// <summary>
    /// 查詢主KEY1
    /// </summary>
    public string KeyFieldValue1
    {
        get;
        set;
    }
    /// <summary>
    /// 查詢主KEY2
    /// </summary>
    public string KeyFieldValue2
    {
        get;
        set;
    }

    public string ValidationGroup
    {
        get { return txtControl.ValidationSettings.ValidationGroup; }
        set { if (IsValidation) { txtControl.ValidationSettings.ValidationGroup = value; } }
    }

    public bool Enabled   //是否可編輯
    {       
        get
        {
            return txtControl.ClientEnabled;
        }
        set
        {
            txtControl.ClientEnabled = btnControl.ClientEnabled = value;
            this.ASPxPopupControl1.PopupElementID = (value == false ? "" : this.btnControl.ID);
        }
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

    private void txtControl_ValueChanged(object sender, EventArgs e)
    {
        OnValueChanged(e);
    }

    public event EventHandler TextChanged;
    protected void OnTextChanged(EventArgs e)
    {
        if (TextChanged != null)
        {
            TextChanged(this, e);
        }
    }
    public event EventHandler ValueChanged;
    protected void OnValueChanged(EventArgs e)
    {
        if (ValueChanged != null)
        {
            ValueChanged(this, e);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool Key1 = false;
        bool Key2 = false;

        //**2011/04/26 Tina：前頁傳遞參數時，會先以加密處理，在此要解密。
        if (!string.IsNullOrEmpty(Request.QueryString["Param"]))
        {
            NameValueCollection qscoll = Utils.Param_UrlDecode(Request.QueryString["Param"]);
            foreach (string key in qscoll.AllKeys)
            {
                if (key == "KeyFieldValue1")
                {
                    Key1 = true;
                }
                else if (key == "KeyFieldValue2")
                {
                    Key2 = true;
                }
            }
        }

        string url = ASPxPopupControl1.ContentUrl.Split('?')[0];

        string strParam = "";
        //if (ASPxPopupControl1.ContentUrl.IndexOf("SysDate") < 0)
        //{
        //    string Date = System.DateTime.Now.ToString("yyyy/MM/ddHH:mm:ss:ms");
        //    strParam += "SysDate=Date()"; // +Date;
        //}

        if (!Key1 && !string.IsNullOrEmpty(KeyFieldValue1))
        {
            strParam += "&KeyFieldValue1=" + KeyFieldValue1;
        }
        if (!Key2 && !string.IsNullOrEmpty(KeyFieldValue2))
        {
            strParam += "&KeyFieldValue2=" + KeyFieldValue2;
        }

        //**2011/04/27 Tina：傳遞參數時，要先以加密處理。
        string encryptUrl = Utils.Param_Encrypt(strParam);
        ASPxPopupControl1.ContentUrl = url + string.Format("?Param={0}", encryptUrl);
    }
}
