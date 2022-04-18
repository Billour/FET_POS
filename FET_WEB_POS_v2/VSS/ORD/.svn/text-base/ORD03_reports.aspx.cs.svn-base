using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Advtek.Utility;

public partial class VSS_ORD_ORD03_reports : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getGvMaster();

        }
    }   

    protected void getGvMaster()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = getMasterData();
        //gvMaster.DataSource = dtGvMaster;
       // gvMaster.DataBind();
        drMasterDV.DataSource = dtGvMaster;
        drMasterDV.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtMaster = new DataTable();
        dtMaster.Columns.Clear();
        dtMaster.Columns.Add("訂單日期", typeof(string));
        dtMaster.Columns.Add("時間", typeof(string));
        dtMaster.Columns.Add("區域", typeof(string));
        dtMaster.Columns.Add("門市編號", typeof(string));
        dtMaster.Columns.Add("門市名稱", typeof(string));
        dtMaster.Columns.Add("訂單編號", typeof(string));
        dtMaster.Columns.Add("項次", typeof(string));
        dtMaster.Columns.Add("OE_NO", typeof(string));
        dtMaster.Columns.Add("商品編號", typeof(string));
        dtMaster.Columns.Add("商品名稱", typeof(string));
        dtMaster.Columns.Add("門市調整數量", typeof(string));
        dtMaster.Columns.Add("業助調整數量", typeof(string));
        dtMaster.Columns.Add("實際訂購數量", typeof(string));
        dtMaster.Columns.Add("商品型態", typeof(string));
        dtMaster.Columns.Add("配送商", typeof(string));
        dtMaster.Columns.Add("備註", typeof(string));
        dtMaster.Columns.Add("狀態", typeof(string));
        dtMaster.Columns.Add("退回原因", typeof(string));

        string[] array1 = { "北一區", "北二區", "中一區" };
        string[] array2 = { "已存檔" };
        string[] array3 = { "一般", "寄銷" };
        for (int i = 0; i < 10; i++)
        {
            DataRow dtMasterRow = dtMaster.NewRow();
            dtMasterRow["訂單日期"] = "2009/12/" + (i + 1).ToString("00");
            dtMasterRow["時間"] = "11:21";
            dtMasterRow["區域"] = array1[i % 3];
            dtMasterRow["門市編號"] = "2103" + i;
            dtMasterRow["門市名稱"] = "門市名稱" + i;
            dtMasterRow["訂單編號"] = "PO20091201000" + i;
            dtMasterRow["項次"] = i;
            dtMasterRow["OE_NO"] = "101900074" + i;
            dtMasterRow["商品編號"] = "KK101900074" + i;
            dtMasterRow["商品名稱"] = "LG KF" + i + "00 黑簡配";
            dtMasterRow["門市調整數量"] = 10 * (i + 1);
            dtMasterRow["業助調整數量"] = 10 * (i + 1);
            dtMasterRow["實際訂購數量"] = 10 * (i + 1);
            dtMasterRow["商品型態"] = array3[i % 2];
            dtMasterRow["配送商"] = "AC";
            dtMasterRow["備註"] = "一搭一贈品手機套";
            dtMasterRow["狀態"] = array2[0];
            dtMasterRow["退回原因"] = "";
            dtMaster.Rows.Add(dtMasterRow);
        }
        return dtMaster;

    }

}
