using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;
using System.Collections;
public partial class VSS_PRE02_PRE02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 繫結空的資料表，以顯示表頭欄位
            gvMaster.DataSource = GetEmptyDataTable();
            gvMaster.DataBind();
            this.gvMaster.Visible = true;
        }
    }
    private DataTable GetEmptyDataTable()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("預購單號", typeof(string));
        dtResult.Columns.Add("預購名稱", typeof(string));
        dtResult.Columns.Add("客戶姓名", typeof(string));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("聯絡電話", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("預購金額", typeof(string));
        dtResult.Columns.Add("門市代號", typeof(string));
        dtResult.Columns.Add("預購日期", typeof(string));
        dtResult.Columns.Add("銷售人員", typeof(string));

        return dtResult;
    }
    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("預購單號", typeof(string));
        dtResult.Columns.Add("預購名稱", typeof(string));
        dtResult.Columns.Add("客戶姓名", typeof(string));
        dtResult.Columns.Add("客戶門號", typeof(string));
        dtResult.Columns.Add("聯絡電話", typeof(string));
        dtResult.Columns.Add("活動名稱", typeof(string));
        dtResult.Columns.Add("預購金額", typeof(string));
        dtResult.Columns.Add("門市代號", typeof(string));
        dtResult.Columns.Add("預購日期", typeof(string));
        dtResult.Columns.Add("銷售人員", typeof(string));



        DataRow NewRow = dtResult.NewRow();
        NewRow["項次"] = "1";
        NewRow["狀態"] = "狀態1";
        NewRow["預購單號"] = "預購單號1";
        NewRow["預購名稱"] = "預購名稱1";
        NewRow["客戶姓名"] = "客戶姓名1";
        NewRow["客戶門號"] = "客戶門號1";
        NewRow["聯絡電話"] = "33223421";
        NewRow["活動名稱"] = "活動名稱1";
        NewRow["預購金額"] = "3331";
        NewRow["門市代號"] = "門市代號1";
        NewRow["預購日期"] = "2010/07/07";
        NewRow["銷售人員"] = "銷售人員1";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["項次"] = "2";
        NewRow["狀態"] = "狀態2";
        NewRow["預購單號"] = "預購單號2";
        NewRow["預購名稱"] = "預購名稱2";
        NewRow["客戶姓名"] = "客戶姓名2";
        NewRow["客戶門號"] = "客戶門號2";
        NewRow["聯絡電話"] = "3939889";
        NewRow["活動名稱"] = "活動名稱2";
        NewRow["預購金額"] = "2322";
        NewRow["門市代號"] = "門市代號2";
        NewRow["預購日期"] = "2010/07/02";
        NewRow["銷售人員"] = "銷售人員2";
        dtResult.Rows.Add(NewRow);

        return dtResult;
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bindMasterData();
        this.gvMaster.Visible = true;
        this.Button1.Visible = true;
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            TextBox10.Enabled = false;
            TextBox10.BackColor = System.Drawing.Color.Gray;
            postbackDate_TextBox1.Enabled = false;
            postbackDate_TextBox1.BackColor = System.Drawing.Color.Gray; ;
            postbackDate_TextBox2.Enabled = false;
            postbackDate_TextBox2.BackColor = System.Drawing.Color.Gray;
            TextBox9.Enabled = false;
            TextBox9.BackColor = System.Drawing.Color.Gray;
            TextBox2.Enabled = false;
            TextBox2.BackColor = System.Drawing.Color.Gray;
            DropDownList3.Enabled = false;
            DropDownList3.BackColor = System.Drawing.Color.Gray;
            TextBox7.Enabled = false;
            TextBox7.BackColor = System.Drawing.Color.Gray;
            DropDownList1.Enabled = false;
            DropDownList1.BackColor = System.Drawing.Color.Gray;
            DropDownList2.Enabled = false;
            DropDownList2.BackColor = System.Drawing.Color.Gray;
        }
        else
        {
            TextBox10.Enabled = true;
            TextBox10.BackColor = System.Drawing.Color.White;
            postbackDate_TextBox1.Enabled = true;
            postbackDate_TextBox1.BackColor = System.Drawing.Color.White;
            postbackDate_TextBox2.Enabled = true;
            postbackDate_TextBox2.BackColor = System.Drawing.Color.White;
            TextBox9.Enabled = true;
            TextBox9.BackColor = System.Drawing.Color.White;
            TextBox2.Enabled = true;
            TextBox2.BackColor = System.Drawing.Color.White;
            DropDownList3.Enabled = true;
            DropDownList3.BackColor = System.Drawing.Color.White;
            TextBox7.Enabled = true;
            TextBox7.BackColor = System.Drawing.Color.White;
            DropDownList1.Enabled = true;
            DropDownList1.BackColor = System.Drawing.Color.White;
            DropDownList2.Enabled = true;
            DropDownList2.BackColor = System.Drawing.Color.White;
        }
    }
}
