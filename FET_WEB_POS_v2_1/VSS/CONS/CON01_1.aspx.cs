using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxEditors;

public partial class VSS_CONS_CON01_1 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindMasterData(1);
        }
    }

    protected void bindMasterData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData(TempCount);
        ucGridView1.dt = dtResult;
        ucGridView1.KeyFieldName = "廠商類別";
        ucGridView1.BindData();
    }
    private DataTable getMasterData(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商類別", typeof(string));
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("廠商名稱", typeof(string));
        dtResult.Columns.Add("公司地址", typeof(string));
        dtResult.Columns.Add("聯絡人", typeof(string));
        dtResult.Columns.Add("聯絡電話", typeof(string));
        dtResult.Columns.Add("合作起日", typeof(string));
        dtResult.Columns.Add("合作訖日", typeof(string));
        dtResult.Columns.Add("合約號碼", typeof(string));
        dtResult.Columns.Add("結算日", typeof(string));
        dtResult.Columns.Add("統一編號", typeof(string));
        dtResult.Columns.Add("負責人", typeof(string));
        dtResult.Columns.Add("電話號碼", typeof(string));
        dtResult.Columns.Add("傳真", typeof(string));
        dtResult.Columns.Add("電子信箱", typeof(string));
        dtResult.Columns.Add("總金額底限", typeof(string));
        dtResult.Columns.Add("總金額底限勾選", typeof(string));
        dtResult.Columns.Add("備註", typeof(string));
        dtResult.Columns.Add("科目1", typeof(string));
        dtResult.Columns.Add("科目2", typeof(string));
        dtResult.Columns.Add("科目3", typeof(string));
        dtResult.Columns.Add("科目4", typeof(string));
        dtResult.Columns.Add("科目5", typeof(string));
        dtResult.Columns.Add("科目6", typeof(string));
        dtResult.Columns.Add("失敗原因", typeof(string));


        for (int i = 0; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["廠商類別"] = 1;
            dtMasterRow["廠商代號"] = "";
            dtMasterRow["廠商名稱"] = 1;
            dtMasterRow["公司地址"] = 1;
            dtMasterRow["聯絡人"] = 1;
            dtMasterRow["聯絡電話"] = 1;
            dtMasterRow["合作起日"] = 1;
            dtMasterRow["合作訖日"] = 1;
            dtMasterRow["合約號碼"] = 1;
            dtMasterRow["結算日"] = 1;
            dtMasterRow["統一編號"] = 1;
            dtMasterRow["負責人"] = 1;
            dtMasterRow["電話號碼"] = 1;
            dtMasterRow["傳真"] = 1;
            dtMasterRow["電子信箱"] = 1;
            dtMasterRow["總金額底限"] = 1;
            dtMasterRow["總金額底限勾選"] = 1;
            dtMasterRow["備註"] = 1;
            dtMasterRow["科目1"] = 1;
            dtMasterRow["科目2"] = 1;
            dtMasterRow["科目3"] = 1;
            dtMasterRow["科目4"] = 1;
            dtMasterRow["科目5"] = 1;
            dtMasterRow["科目6"] = 1;
            dtMasterRow["失敗原因"] = "廠商代號不得為空白";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void bindMasterData1(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData1(TempCount);
        ucGridView2.dt = dtResult;
        ucGridView2.KeyFieldName = "廠商代號";
        ucGridView2.BindData();
    }
    private DataTable getMasterData1(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("起始月份", typeof(string));
        dtResult.Columns.Add("結束月份", typeof(string));
        dtResult.Columns.Add("失敗原因", typeof(string));

        for (int i = 0; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["廠商代號"] = 1;
            dtMasterRow["佣金比率"] = "";
            dtMasterRow["起始月份"] = 1;
            dtMasterRow["結束月份"] = 1;
            dtMasterRow["失敗原因"] = "佣金比率有誤";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void bindMasterData2(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData2(TempCount);
        ucGridView3.dt = dtResult;
        ucGridView3.KeyFieldName = "廠商代號";
        ucGridView3.BindData();
    }
    private DataTable getMasterData2(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("門市代號", typeof(string));
        dtResult.Columns.Add("失敗原因", typeof(string));

        for (int i = 0; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["廠商代號"] = 1;
            dtMasterRow["門市代號"] = "";
            dtMasterRow["失敗原因"] = "門市代號不得為空白";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void bindMasterData3(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData3(TempCount);
        ucGridView4.dt = dtResult;
        ucGridView4.KeyFieldName = "廠商代號";
        ucGridView4.BindData();
    }
    private DataTable getMasterData3(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("商品代號", typeof(string));
        dtResult.Columns.Add("商品類別", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("上架日", typeof(string));
        dtResult.Columns.Add("下架日", typeof(string));
        dtResult.Columns.Add("停止訂購日", typeof(string));
        dtResult.Columns.Add("科目1", typeof(string));
        dtResult.Columns.Add("科目2", typeof(string));
        dtResult.Columns.Add("科目3", typeof(string));
        dtResult.Columns.Add("科目4", typeof(string));
        dtResult.Columns.Add("科目5", typeof(string));
        dtResult.Columns.Add("科目6", typeof(string));
        dtResult.Columns.Add("單位", typeof(string));
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("起始月份", typeof(string));
        dtResult.Columns.Add("結束月份", typeof(string));
        dtResult.Columns.Add("失敗原因", typeof(string));

        for (int i = 0; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["廠商代號"] = 1;
            dtMasterRow["商品代號"] = 1;
            dtMasterRow["商品類別"] = 1;
            dtMasterRow["商品名稱"] = 1;
            dtMasterRow["上架日"] = 1;
            dtMasterRow["下架日"] = 1;
            dtMasterRow["停止訂購日"] = 1;
            dtMasterRow["科目1"] = 1;
            dtMasterRow["科目2"] = 1;
            dtMasterRow["科目3"] = 1;
            dtMasterRow["科目4"] = 1;
            dtMasterRow["科目5"] = 1;
            dtMasterRow["科目6"] = 1;
            dtMasterRow["單位"] = 1;
            dtMasterRow["佣金比率"] = 1;
            dtMasterRow["起始月份"] = "";
            dtMasterRow["結束月份"] = 1;
            dtMasterRow["失敗原因"] = "起始月份不得為空白";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void bindMasterData4(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData4(TempCount);
        ucGridView5.dt = dtResult;
        ucGridView5.KeyFieldName = "廠商代號";
        ucGridView5.BindData();
    }
    private DataTable getMasterData4(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("起始月份", typeof(string));
        dtResult.Columns.Add("結束月份", typeof(string));
        dtResult.Columns.Add("失敗原因", typeof(string));

        for (int i = 0; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["廠商代號"] = 1;
            dtMasterRow["佣金比率"] = 1;
            dtMasterRow["起始月份"] = "";
            dtMasterRow["結束月份"] = 1;
            dtMasterRow["失敗原因"] = "起始月份不得為空白";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void bindMasterData5(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData5(TempCount);
        ucGridView6.dt = dtResult;
        ucGridView6.KeyFieldName = "廠商代號";
        ucGridView6.BindData();
    }
    private DataTable getMasterData5(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("級距項次", typeof(string));
        dtResult.Columns.Add("起-金額級距", typeof(string));
        dtResult.Columns.Add("訖-金額級距", typeof(string));
        dtResult.Columns.Add("佣金比率", typeof(string));
        dtResult.Columns.Add("起始月份", typeof(string));
        dtResult.Columns.Add("結束月份", typeof(string));
        dtResult.Columns.Add("失敗原因", typeof(string));


        for (int i = 0; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["廠商代號"] = 1;
            dtMasterRow["級距項次"] = 1;
            dtMasterRow["起-金額級距"] = 1;
            dtMasterRow["訖-金額級距"] = 1;
            dtMasterRow["佣金比率"] = 1;
            dtMasterRow["起始月份"] = "";
            dtMasterRow["結束月份"] = 1;
            dtMasterRow["失敗原因"] = "起始月份不得為空白";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void bindMasterData6(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData6(TempCount);
        ucGridView7.dt = dtResult;
        ucGridView7.KeyFieldName = "廠商代號";
        ucGridView7.BindData();
    }
    private DataTable getMasterData6(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("廠商代號", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("失敗原因", typeof(string));

        for (int i = 0; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["廠商代號"] = "";
            dtMasterRow["商品料號"] = 1;
            dtMasterRow["失敗原因"] = "廠商代號不得為空白";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void bindMasterData7(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData7(TempCount);
        ucGridView8.dt = dtResult;
        ucGridView8.KeyFieldName = "項次";
        ucGridView8.BindData();
    }
    private DataTable getMasterData7(int TempCount)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("項次", typeof(string));
        dtResult.Columns.Add("信用卡別", typeof(string));
        dtResult.Columns.Add("手續費", typeof(string));
        dtResult.Columns.Add("失敗原因", typeof(string));

        for (int i = 0; i < TempCount; i++)
        {
            DataRow dtMasterRow = dtResult.NewRow();
            dtMasterRow["項次"] = 1;
            dtMasterRow["信用卡別"] = "";
            dtMasterRow["手續費"] = 1;
            dtMasterRow["失敗原因"] = "信用卡別不得為空白";

            dtResult.Rows.Add(dtMasterRow);
        }

        return dtResult;
    }

    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        int a = this.ASPxPageControl1.ActiveTabIndex;

        switch (a)
        {
            case 1:
                bindMasterData1(1);
                break;

            case 2:
                bindMasterData2(2);
                break;

            case 3:
                bindMasterData3(3);
                break;

            case 4:
                bindMasterData4(4);
                break;

            case 5:
                bindMasterData5(5);
                break;

            case 6:
                bindMasterData6(4);
                break;

            case 7:
                bindMasterData7(3);
                break;

            default:
                bindMasterData(2);
                break;
        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {


        //for (int a = 1; a <= 10; a++)
        //{
        //    ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + a.ToString());
        //    ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + a.ToString());
        //    chk.Checked = false;
        //    chk.Enabled = true;
        //}



        if (RadioButton1.Checked)
        {

            for (int i = 1; i <= 3; i++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + i.ToString());
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + i.ToString());
                chk.Checked = true;
                chk.Enabled = true;
                txb.Enabled = true;




            }
            for (int j = 4; j <= 8; j++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + j.ToString());
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + j.ToString());
                chk.Enabled = false;
                chk.Checked = false;
                txb.Enabled = false;
            }


        }
        else
        {
            CheckBox1.Checked = true;
            TextBox1.Enabled = true;
            for (int i = 5; i <= 8; i++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + i.ToString());
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + i.ToString());
                chk.Checked = true;
                chk.Enabled = true;
                txb.Enabled = true;

            }
            for (int j = 2; j <= 4; j++)
            {
                ASPxCheckBox chk = (ASPxCheckBox)FindControl("CheckBox" + j.ToString());
                ASPxTextBox txb = (ASPxTextBox)FindControl("TextBox" + j.ToString());
                chk.Enabled = false;
                chk.Checked = false;
                txb.Enabled = false;
            }
           
            

        }

    }
   
}
