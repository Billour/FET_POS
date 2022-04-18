using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSS_Common_WeightRatingExportPopup : Popup
{
    protected void btnImport_Click(object sender, EventArgs e)
    {
        bindMasterData();
        btnUpdate.Visible = true;
        btnCalcel.Visible = true;
    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }
    private DataTable getMasterData()
    {
        //門市編號	門市名稱	比率	異常原因

        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("門市編號", typeof(string));
        dtResult.Columns.Add("門市名稱", typeof(string));
        dtResult.Columns.Add("商品料號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("主動配貨", typeof(string));
        dtResult.Columns.Add("異常原因", typeof(string));
        /*
         2101			店組不存在	
2102	遠企		比率不可為空值	
2103	西門	0	比率應大於0	
2105	華納	90		
2106	站前	8		
		98	加總比率非100%	

         
         */

        
        DataRow NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2101";
        NewRow["門市名稱"] = "";
        NewRow["商品料號"] = "100100250";
        NewRow["商品名稱"] = "Nokia6230";
        NewRow["主動配貨"] = "3";
        NewRow["異常原因"] = "店組不存在";        
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2102";
        NewRow["門市名稱"] = "遠企";
        NewRow["商品料號"] = "100100251";
        NewRow["商品名稱"] = "";
        NewRow["主動配貨"] = "3";
        NewRow["異常原因"] = "商品料號不存在";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2103";
        NewRow["門市名稱"] = "西門";
        NewRow["商品料號"] = "100100252";
        NewRow["商品名稱"] = "Nokia6235";
        NewRow["主動配貨"] = "";
        NewRow["異常原因"] = "主配數量不可為空值";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2105";
        NewRow["門市名稱"] = "華納";
        NewRow["商品料號"] = "100100253";
        NewRow["商品名稱"] = "Nokia3300";
        NewRow["主動配貨"] = "0";
        NewRow["異常原因"] = "主配數量應大於0";
        dtResult.Rows.Add(NewRow);

        NewRow = dtResult.NewRow();
        NewRow["門市編號"] = "2106";
        NewRow["門市名稱"] = "站前";
        NewRow["商品料號"] = "100100255";
        NewRow["商品名稱"] = "Moto";
        NewRow["主動配貨"] = "6";
        NewRow["異常原因"] = "";
        dtResult.Rows.Add(NewRow);
        
        return dtResult;
    }   



    protected void gvMasterDV_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
    {        
        if (e.DataColumn.FieldName == "異常原因")
        {
            if (!string.IsNullOrEmpty(e.CellValue.ToString()))
            {
                e.Cell.Style[HtmlTextWriterStyle.Color] = "red";                
            }
        }
    }
}
