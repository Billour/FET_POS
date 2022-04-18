using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VSS_CON08_1 : Advtek.Utility.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           bindMasterData(0);
        }
    }
    protected void bindMasterData(int tempCount)
    {
       DataTable dtResult = new DataTable();
       dtResult = getMasterData(tempCount);
       ViewState["gvMaster"] = dtResult;
       gvMaster.DataSource = dtResult;
       gvMaster.DataBind();
    }

    private DataTable getMasterData(int tempCount)
    {
       DataTable dtResult = new DataTable();
       dtResult.Columns.Add("廠商編號", typeof(string));
       dtResult.Columns.Add("廠商名稱", typeof(string));
       dtResult.Columns.Add("門市編號", typeof(string));
       dtResult.Columns.Add("門市名稱", typeof(string));
       dtResult.Columns.Add("商品編號", typeof(string));
       dtResult.Columns.Add("商品名稱", typeof(string));
       dtResult.Columns.Add("實際訂購量", typeof(string));
       dtResult.Columns.Add("異常原因", typeof(string));

       for (int i = 0; i < tempCount; i++)
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["廠商編號"] = "AC"+ (i+1).ToString("000");
            NewRow["廠商名稱"] = "廠商名稱" + (i + 1).ToString("000");
            NewRow["門市編號"] = (i + 1).ToString("000");
            NewRow["門市名稱"] = "門市名稱" + (i + 1).ToString("000");
            NewRow["商品編號"] = "1001002" + (i + 1).ToString("00");
            NewRow["商品名稱"] = "商品名稱" + (i + 1).ToString("000");
            NewRow["實際訂購量"] = "100";
            NewRow["異常原因"] = "異常"+ (i + 1).ToString("000");;
            dtResult.Rows.Add(NewRow);
        }
       return dtResult;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       bindMasterData(2);
    }
}
