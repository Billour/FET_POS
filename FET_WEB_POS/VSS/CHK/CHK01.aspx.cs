﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_CHK01_CHK01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
           this.postbackDate_TextBox1.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");

           Label7.Text = DateTime.Now.ToString("yyyy/MM/dd");
            Label6.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       this.Button4.Enabled = false;
       this.Label3.Text = "結算完成";
       this.TextBox4.Text = "1000";
       this.TextBox5.Text = "1000";
       this.TextBox6.Text = "1000";
       this.TextBox7.Text = "1000";
       this.TextBox8.Text = "1000";

    }
}
