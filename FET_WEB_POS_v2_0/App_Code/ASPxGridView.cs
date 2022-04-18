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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ASPxGridViewEx_CustomCallback);            
            this.Templates.PagerBar = new CustomPagerBarTemplate();            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // TODO:                        
           
        }

        private void ASPxGridViewEx_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {          
            this.SettingsPager.PageSize = int.Parse(e.Parameters);
            this.DataBind();
        }
    }
}