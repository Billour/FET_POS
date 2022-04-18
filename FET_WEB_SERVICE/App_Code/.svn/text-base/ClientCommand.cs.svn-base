using System;
using System.Collections;
using System.Web.UI;

namespace Advtek.Utility
{
	public class ClientCommand
	{
		private ArrayList _commandQueue = new ArrayList();

		internal void AddCommand(string strCommand)
		{
			this._commandQueue.Add(strCommand);
		}

		public void Alert(string strMessage)
		{
			this.AddCommand("alert(\"" + this.escape(strMessage) + "\");");
		}

		public void CloseWindow()
		{
			this.AddCommand("top.close();");
		}

		public void DisabledAllActionButton()
		{
			this.AddCommand("top.Toolbar.Btn_AllDisable();");
		}

		public string escape(string strData)
		{
			return strData.Replace("\n", @"\n").Replace("\r", "").Replace("\"", "\\\"");
		}

		public void ExecCommand(string strCommand)
		{
			this.AddCommand(strCommand);
		}

        //private string GetFeature(HelpWindow helpWin)
        //{
        //    string str = "";
        //    if (!helpWin.Width.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "width=" + helpWin.Width + "px";
        //    }
        //    if (!helpWin.Height.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "height=" + helpWin.Height + "px";
        //    }
        //    if (!helpWin.Top.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "top=" + helpWin.Top;
        //    }
        //    if (!helpWin.Left.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "left=" + helpWin.Left;
        //    }
        //    if (!helpWin.ScrollBars.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "scrollbars=" + helpWin.ScrollBars;
        //    }
        //    if (!helpWin.Center.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "center=" + helpWin.Center;
        //    }
        //    if (!helpWin.Border.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "border=" + helpWin.Border;
        //    }
        //    if (!helpWin.Help.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "help=" + helpWin.Help;
        //    }
        //    if (!helpWin.Status.Equals(""))
        //    {
        //        if (!str.Equals(""))
        //        {
        //            str = str + ",";
        //        }
        //        str = str + "status=" + helpWin.Status;
        //    }
        //    if (helpWin.Resizable.Equals(""))
        //    {
        //        return str;
        //    }
        //    if (!str.Equals(""))
        //    {
        //        str = str + ",";
        //    }
        //    return (str + "resizable=" + helpWin.Resizable);
        //}

        //public void OpenHelpWindow(HelpWindow helpWin)
        //{
        //    string feature = this.GetFeature(helpWin);
        //    if (!helpWin.CallBack.Equals(""))
        //    {
        //        this.AddCommand("top.Working.document.all('__CB_Help_Function').value = '" + helpWin.CallBack + "';\n");
        //    }
        //    this.AddCommand("WindowObjectClose();\n");
        //    this.AddCommand("windowobject = window.open(\"" + helpWin.Url + "\", \"" + helpWin.WindowName + "\", \"" + feature + "\");\n");
        //    this.AddCommand("windowobject.focus();\n");
        //}

        //public void OpenHelpWindow(BasePage page, HelpWindow helpWin)
        //{
        //    string feature = this.GetFeature(helpWin);
        //    if (!helpWin.CallBack.Equals(""))
        //    {
        //        this.AddCommand("page.TargetFrame.document.all('__CB_Help_Function').value = '" + helpWin.CallBack + "';\n");
        //    }
        //    this.AddCommand("WindowObjectClose();\n");
        //    this.AddCommand("windowobject = window.open(\"" + helpWin.Url + "\", \"" + helpWin.WindowName + "\", \"" + feature + "\");\n");
        //    this.AddCommand("windowobject.focus();\n");
        //}

		public void OpenWindow(string url)
		{
			this.AddCommand("window.open('" + url + "');");
		}

		public void RedirectPage(string strUrl)
		{
			this.AddCommand("parent.location.href=\"" + strUrl + "\";");
		}

		public void RefreshControl(string targetFrame, string id)
		{
			this.AddCommand("    RefreshControl(" + targetFrame + ", \"" + id + "\");");
		}

		public void SetActionButton(string strAction, bool isShow)
		{
			string str = "true";
			if (!isShow)
			{
				str = "false";
			}
			this.AddCommand("top.Toolbar.Btn_Enable(\"Btn_" + strAction.ToLower() + "\", " + str + ");");
		}

		public void SetControlDisabled(Control webControl)
		{
			this.SetControlEnabled(webControl, false);
		}

		public void SetControlEnabled(Control webControl)
		{
			this.SetControlEnabled(webControl, true);
		}

		private void SetControlEnabled(Control webControl, bool blnEnabled)
		{
			string str = blnEnabled ? "false" : "true";
			this.AddCommand("parent.document.forms[0]." + webControl.ClientID + ".disabled=" + str + ";");
		}

		public void SetFocus(Control webControl)
		{
			this.AddCommand("parent.document.forms[0]." + webControl.ClientID + ".focus();");
		}

		public void SetTitle(string strTitle)
		{
			this.AddCommand("if(top.Toolbar != null){");
			this.AddCommand("    try{");
			this.AddCommand("        top.Toolbar.setTitle(\"" + strTitle + "\");");
			this.AddCommand("    }");
			this.AddCommand("    catch(e){");
			this.AddCommand("    }");
			this.AddCommand("}");
		}

		public void StatueBarMessage(string strMessage)
		{
			this.AddCommand("top.status =\"" + strMessage + "\";");
		}

		public void UpdateForm()
		{
			this.AddCommand("top.Working.document.forms[0].innerHTML = document.forms[0].innerHTML;");
		}

		public ArrayList CommonQueue
		{
			get
			{
				return this._commandQueue;
			}
		}
	}
}

