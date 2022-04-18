using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Advtek.Utility;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Configuration;

public partial class Login : BasePage
{

    //private static LogUtil Logger = new LogUtil(typeof(Login));
    //protected static string PageNO = "Login";
    //private string key = ConfigurationManager.AppSettings["ConnStrKey"].ToString();
    //private string iv = ConfigurationManager.AppSettings["ConnStrIV"].ToString();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //ClientCommand.RedirectPage("MainFrame.aspx");

        string username = Login1.UserName.Trim();
        string password = Login1.Password;


        OracleConnection conn = null;
        OracleDataReader Dr = null;
        StringBuilder strSQL = null;

        try
        {
            string sqlStr = " select user_type_id ,user_type_name from user_type where user_type_id = '" + username + "' and user_type_name = '" + password + "' ";

            strSQL = new StringBuilder();
            strSQL.Append(sqlStr);

            conn = OracleDBUtil.GetConnection();
            Dr = OracleDBUtil.GetDataReader(conn, strSQL.ToString());

            if (Dr.Read())
            {
                if (Dr.HasRows)
                {
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(username, Login1.RememberMeSet, Session.Timeout);
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Dr.Close();

                ASPxPopupControl1.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            throw ex;
            //ASPxPopupControl1.Enabled = true;
        }
        finally
        {
            if (conn != null) conn.Close();
            conn.Dispose();
            Dr.Close();
        }



    }

}
