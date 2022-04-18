using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_ORD_ORD13_Import : Popup
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();

        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));    
        dtResult.Columns.Add("卡片群組", typeof(string));
        dtResult.Columns.Add("安全庫存量", typeof(string));
        dtResult.Columns.Add("最低庫存量", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        List<string[]> dataRows = new List<string[]> { 
            new string[] {"2000", "", "2G", "1000", "500", "店組不存在"},
            new string[] {"2101","遠企", "group", "1000", "500", "卡片群組不存在"},
            new string[] {"2101","遠企", "2G", "", "100", "安全庫存量不可為空值"},
            new string[] {"2101","遠企", "3G", "100", "", "最低庫存量不可為空值"},
            new string[] {"2101","遠企", "2G", "", "0", "安全庫存量應大於0"},
            new string[] {"2101","遠企", "3G", "0", "", "最低庫存量應大於0"},
            new string[] {"2101","遠企", "3G", "1000", "100", "設定已存在"},
            new string[] {"2102","華納", "3G", "1000", "100", ""}
        };

        dataRows.ForEach(dr =>
        {
            DataRow NewRow = dtResult.NewRow();
            NewRow["門市編號"] = dr[0];
            NewRow["門市名稱"] = dr[1];
            NewRow["卡片群組"] = dr[2];
            NewRow["安全庫存量"] = dr[3];
            NewRow["最低庫存量"] = dr[4];
            NewRow["異常原因"] = dr[5];
            dtResult.Rows.Add(NewRow);
        });

        return dtResult;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
        Div1.Visible = true;
    }
}
