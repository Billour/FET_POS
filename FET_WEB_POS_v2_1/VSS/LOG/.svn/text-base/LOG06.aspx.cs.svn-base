using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_LOG_LOG06 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        //string test = postbackDate_TextBox3.Text;
        //this.postbackDate_TextBox3.Text.ToString();

        if (ASPxDateEdit1.Text.ToString() != "")

        {
            if (pw.Text != "")
            {

                if (cpw.Text != "")
                {
                    if (pw.Text == cpw.Text)
                    {
                        Label1.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        Label2.Text = "";
                        ASPxDateEdit1.Text = "";
                        ASPxDateEdit2.Text = "";
                    }
                    else
                        Label2.Text = "密碼輸入不正確";
                }

                else
                {
                    Label2.Text = "請再輸入剛才的密碼";
                    Label1.Text = "";
                }
            }
            else
                Label2.Text = "密碼不得為空白";
                
            
        }

        else

            Label2.Text = "請輸入有效日期"; 
        
        
        //if (pw.Text == cpw.Text)

        //{ Label1.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        //Label2.Text = "";
        //}

        //else

        //{ 
        //Label2.Text = "密碼輸入不正確";
        //Label1.Text = "";
        //}

      
    
    }
}
