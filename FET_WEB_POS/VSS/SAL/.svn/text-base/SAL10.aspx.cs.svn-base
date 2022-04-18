using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class VSS_SAL_SAL10 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindMasterData();
        divContent.Visible = true;
        gvMaster.Visible = true;
        gvDetail.Visible = false;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.SelectedIndex = -1;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("狀態", typeof(string));
        dtResult.Columns.Add("服務序號", typeof(string));
        dtResult.Columns.Add("銷售日期", typeof(string));
        dtResult.Columns.Add("服務屬性", typeof(string));
        dtResult.Columns.Add("服務類別", typeof(string));
        dtResult.Columns.Add("門號", typeof(string));
        dtResult.Columns.Add("機台", typeof(string));
        dtResult.Columns.Add("銷售人員", typeof(string));

        string[] array0 = { "待結帳", "已審核", "拒絕" };
        string[] array1 = { "IA", "Loyalty", "SSI", "HRS", "Payment", "e-Store" };
        string[] array2 = { "全球卡", "換補卡", "2轉3", "新啟用", "續約", "代收", "維修", "網購", "預購" };
        for (int i = 0; i < 10; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["狀態"] = array0[i % 3];
            NewRow["服務序號"] = "A0000" + i;
            NewRow["銷售日期"] = "2010/07/01";
            NewRow["服務屬性"] = array1[i % 6];
            NewRow["服務類別"] = array2[i % 9];
            NewRow["門號"] = "091536214" + i;
            NewRow["機台"] = "01";
            NewRow["銷售人員"] = "王小明";
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }

    protected void bindDetailData(string KeyNo)
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData(KeyNo);
        ViewState["gvDetail"] = dtResult;
        gvDetail.DataSource = dtResult;
        gvDetail.DataBind();
    }

    private DataTable getDetailData(string KeyNo)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("促銷編號", typeof(string));
        dtResult.Columns.Add("促銷名稱", typeof(string));
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("卡片序號(SIM)", typeof(string));
        dtResult.Columns.Add("金額", typeof(string));

        for (int i = 0; i < 5; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["促銷編號"] = KeyNo + "_A0" + i;
            NewRow["促銷名稱"] = "大雙網促銷";
            NewRow["商品編號"] = "20102852" + i;
            NewRow["商品名稱"] = string.Format("BenQ S{0}20i(銀,簡)", i);
            NewRow["卡片序號(SIM)"] = "234313353453" + i;
            NewRow["金額"] = 1000 * (i + 1);
            dtResult.Rows.Add(NewRow);
        }
        return dtResult;
    }



    protected void gvMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView mygridview = sender as GridView;
        if (mygridview.SelectedIndex >= 0)
        {
            string KeyNo = mygridview.Rows[mygridview.SelectedIndex].Cells[3].Text;
            bindDetailData(KeyNo);
            gvDetail.Visible = true;
        }
    }
}
