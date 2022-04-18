using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class VSS_INV_INV09 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string dno = Request.QueryString["PO/OE_NO"] ?? "";
        this.ViewState["PO/OE_NO"] = dno;
        string ReNo = Request.QueryString["ReceivingNo"] == null ? "" : Request.QueryString["ReceivingNo"].ToString().Trim();
        this.ViewState["ReceivingNo"] = ReNo;


        if (!IsPostBack)
        {


            if (this.ViewState["PO/OE_NO"].ToString() == "001")
            {
                if (this.ViewState["ReceivingNo"].ToString() == "")
                {
                    //this.Label1.Text = "SC2101-1007002";
                    //gvMaster.Visible = false;
                    Label7.Text = "";
                    Label5.Text = dno.ToString();
                    Label6.Text = "HR1007001";
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    ASPxButton1.Enabled = false;
                    gvDetail.Visible = false;
                    bindMasterData();
                }
                else
                {
                    Label7.Text = "SR2104-1007001-1";
                    Label5.Text = "001-1";
                    Label6.Text = "HR1007002";
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    gvMaster.Visible = false;
                    bindDetailData();
                }


            }
            else
                if (this.ViewState["ReceivingNo"].ToString() == "")
                {
                    //this.Label1.Text = "SC2101-1007002";
                    //gvMaster.Visible = false;
                    Label7.Text = "";
                    Label5.Text = dno.ToString();
                    Label6.Text = "HR1007001";
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    ASPxButton1.Enabled = false;
                    gvDetail.Visible = false;
                    bindMasterData();
                }
                else
                {

                    Label7.Text = "SR2104-1007001-1";
                    Label5.Text = "001-1";
                    Label6.Text = "HR1007002";
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    gvMaster.Visible = false;
                    bindDetailData();
                }





        }

    }

    protected void bindMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getMasterData();
        ViewState["gvMaster"] = dtResult;
        gvMaster.DataSource = dtResult;
        gvMaster.DataBind();
    }

    protected void bindDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult = getDetailData();
        ViewState["gvDetail"] = dtResult;
        gvDetail.DataSource = dtResult;
        gvDetail.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("IMEI檢核", typeof(bool));
        dtResult.Columns.Add("到貨量", typeof(int));
        dtResult.Columns.Add("驗收量", typeof(int));
        dtResult.Columns.Add("CHECK", typeof(bool));
        dtResult.Columns.Add("IMEI", typeof(string));
        dtResult.Columns.Add("在途量", typeof(string));
        dtResult.Columns.Add("供貨商", typeof(string));


        for (int i = 1; i < 6; i++)
        {

            DataRow NewRow = dtResult.NewRow();
            NewRow["商品編號"] = "A021000" + i;
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["IMEI檢核"] = i % 2;
            NewRow["到貨量"] = 5;
            NewRow["驗收量"] = (i == 1) ? 1 : 5;
            NewRow["CHECK"] = (i % 2 == 1) ? true : false;
            NewRow["IMEI"] = (i == 1) ? "1" : "5";
            NewRow["在途量"] = (i == 1) ? "4" : "0";
            NewRow["供貨商"] = "供貨商" + i;
            dtResult.Rows.Add(NewRow);

        }
        return dtResult;
    }

    private DataTable getDetailData()
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("商品編號", typeof(string));
        dtResult.Columns.Add("商品名稱", typeof(string));
        dtResult.Columns.Add("IMEI檢核", typeof(bool));
        dtResult.Columns.Add("到貨量", typeof(int));
        dtResult.Columns.Add("驗收量", typeof(int));
        dtResult.Columns.Add("CHECK", typeof(bool));
        dtResult.Columns.Add("IMEI", typeof(string));
        dtResult.Columns.Add("在途量", typeof(string));
        dtResult.Columns.Add("供貨商", typeof(string));


        for (int i = 1; i < 4; i++)
        {

            DataRow NewRow = dtResult.NewRow();
            NewRow["商品編號"] = "A021000" + i;
            NewRow["商品名稱"] = "商品名稱" + i;
            NewRow["IMEI檢核"] = i % 2;
            NewRow["到貨量"] = 5;
            NewRow["驗收量"] = (i == 2) ? 4 : 5;
            NewRow["CHECK"] = (i % 2 == 1) ? true : false;
            NewRow["IMEI"] = (i == 2) ? "1" : "5";
            // NewRow["在途量"] = (i == 1) ? "4" : "0";
            NewRow["供貨商"] = "供貨商" + i;
            dtResult.Rows.Add(NewRow);

        }
        return dtResult;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Label7.Text = "SR2104-1007001";

    }

    private string SupplyContent(int Count)
    {
        string FORMAT = "<table border=\"1\">";
        for (int i = 1; i <= Count; i++)
        {
            //<tr><td>供貨商名稱：</td><td>{0}</td></tr><tr><td>聯絡窗口：</td><td>窗口</td></tr><tr><td>聯絡電話：</td><td>02-11112222</td></tr><tr><td>傳真號碼：</td><td>02-44441452</td></tr><tr><td>email：</td><td>xxx@aaa.com</td></tr></table>";
            FORMAT += "<tr><td>供貨商名稱：</td><td>" + i.ToString() + "</td></tr><tr><td>聯絡窗口：</td><td>窗口</td></tr><tr><td>聯絡電話：</td><td>02-11112222</td></tr><tr><td>傳真號碼：</td><td>02-44441452</td></tr><tr><td>email：</td><td>xxx@aaa.com</td>";
        }
        FORMAT += "</tr></table>";

        return FORMAT;
    }

    private string IMEIContent(int Count)
    {
        string IMEI_FORMAT = "<table border=\"1\">";
        for (int i = 1; i <= Count; i++)
        {
            IMEI_FORMAT += "<tr><td>778194415641786" + i.ToString() + "</td></tr>";
        }
        IMEI_FORMAT += "</tr></table>";

        return IMEI_FORMAT;
    }

    #region 主GridView 事件

    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gvMaster = sender as ASPxGridView;
        gvMaster.DataSource = getMasterData();
        gvMaster.DataBind();
    }

    protected void gvMaster_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {
        //if (e.VisibleRowIndex > -1)
        //{
        //   DataRowView Rows = (DataRowView)gvMaster.GetRow(e.VisibleRowIndex);
        //   bool check = bool.Parse(Rows["IMEI檢核"].ToString());

        //   if (check == false)
        //   {
        //      if (e.Column.FieldName == "在途量")
        //      {
        //         e.DisplayText = "10";
        //      }
        //   }
        //}
    }

    protected void gvMaster_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        gvMaster.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvMaster.DataBind();
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == GridViewRowType.Data)
        {
            //DataRowView Rows = (DataRowView)gvMaster.GetRow(e.VisibleIndex);
            //bool check = bool.Parse(Rows["IMEI檢核"].ToString());
            bool check = bool.Parse(gvMaster.GetRowValues(e.VisibleIndex, "IMEI檢核").ToString());
            ASPxLabel lbl1 = e.Row.FindChildControl<ASPxLabel>("lbl1");//(ASPxLabel)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[6], "lbl1");
            //ASPxButton btn1 = e.Row.FindChildControl<ASPxButton>("ASPxButton2");//(ASPxButton)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[6], "ASPxButton2");
            ASPxTextBox txt1 = e.Row.FindChildControl<ASPxTextBox>("txt1");//(ASPxTextBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[4], "txt1");
            //ASPxTextBox txt2 = e.Row.FindChildControl<ASPxTextBox>("ASPxTextBox3");//(ASPxTextBox)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[6], "ASPxTextBox3");
            PopupControl pControl = e.Row.FindChildControl<PopupControl>("PopupControl1");
            ASPxLabel lb13 = e.Row.FindChildControl<ASPxLabel>("lb13");//(ASPxLabel)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[8], "lb13");


            int intI_IMEI = int.Parse(e.GetValue("到貨量").ToString()); //gvMaster.GetRowValues(e.VisibleIndex, "到貨量").ToString());
            int intC_IMEI = int.Parse(e.GetValue("驗收量").ToString()); //gvMaster.GetRowValues(e.VisibleIndex, "驗收量").ToString());
            int intS_IMEI = int.Parse(e.GetValue("IMEI").ToString()); //gvMaster.GetRowValues(e.VisibleIndex, "IMEI").ToString());
            ASPxImage imgIMEI = e.Row.FindChildControl<ASPxImage>("imgIMEI");//(ASPxImage)gvMaster.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvMaster.Columns[5], "imgIMEI");

            lbl1.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent(Convert.ToInt16(lbl1.Text)));
            lbl1.Attributes["onmouseout"] = "hide();";

            lb13.Attributes["onmouseover"] = string.Format("show('{0}');", SupplyContent(1));
            lb13.Attributes["onmouseout"] = "hide();";

            //img status
            //intC_IMEI - intS_IMEI = 0
            if ((intI_IMEI - intC_IMEI) > 0)
            {
                // lbl1.Visible = false;
                pControl.Text = "7780944056407861";
                //((ASPxButton)pControl.FindChildControl<ASPxButton>("btnControl")).Enabled = false;
                pControl.Enabled = false;

                //txt2.Text = "7780944056407861";
                //btn1.Enabled = false;
            }

            if ((intC_IMEI - intS_IMEI) == 0)
            {
                imgIMEI.ImageUrl = "~/Icon/check.png";
            }
            //intC_IMEI - intS_IMEI > 1
            if ((intC_IMEI - intS_IMEI) > 0)
            {
                imgIMEI.ImageUrl = "~/Icon/non_complete.png";
            }

            if (check == false)
            {

                lbl1.Visible = false;
                //btn1.Visible = false;
                //txt2.Visible = false;
                pControl.Visible = false;
                imgIMEI.Visible = false;
            }
        }

    }

    #endregion

    #region 明細GridView 事件

    protected void gvDetail_PageIndexChanged(object sender, EventArgs e)
    {
        ASPxGridView gvDetail = sender as ASPxGridView;
        gvDetail.DataSource = getDetailData();
        gvDetail.DataBind();
    }

    protected void gvDetail_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {
        //if (e.VisibleRowIndex > -1)
        //{
        //   DataRowView Rows = (DataRowView)gvMaster.GetRow(e.VisibleRowIndex);
        //   bool check = bool.Parse(Rows["IMEI檢核"].ToString());

        //   if (check == false)
        //   {
        //      if (e.Column.FieldName == "在途量")
        //      {
        //         e.DisplayText = "10";
        //      }
        //   }
        //}
    }

    protected void gvDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        //GridPageSize = int.Parse(e.Parameters);
        gvDetail.SettingsPager.PageSize = int.Parse(e.Parameters);
        gvDetail.DataBind();
    }

    protected void gvDetail_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //DataRowView Rows = (DataRowView)gvMaster.GetRow(e.VisibleIndex);
            //bool check = bool.Parse(Rows["IMEI檢核"].ToString());
            bool check = bool.Parse(gvDetail.GetRowValues(e.VisibleIndex, "IMEI檢核").ToString());
            ASPxLabel lbl2 = e.Row.FindChildControl<ASPxLabel>("lbl2"); // (ASPxLabel)gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns[6], "lbl2");
            ASPxButton btn3 = e.Row.FindChildControl<ASPxButton>("ASPxButton3"); //(ASPxButton)gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns[6], "ASPxButton3");
            ASPxTextBox txt4 = e.Row.FindChildControl<ASPxTextBox>("ASPxTextBox4"); //(ASPxTextBox)gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns[6], "ASPxTextBox4");
            ASPxLabel lb14 = e.Row.FindChildControl<ASPxLabel>("lb14"); //(ASPxLabel)gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns[7], "lb14");


            int intI1_IMEI = int.Parse(e.GetValue("到貨量").ToString()); //gvDetail.GetRowValues(e.VisibleIndex, "到貨量").ToString());
            int intC1_IMEI = int.Parse(e.GetValue("驗收量").ToString()); //gvDetail.GetRowValues(e.VisibleIndex, "驗收量").ToString());
            int intS1_IMEI = int.Parse(e.GetValue("IMEI").ToString()); //gvDetail.GetRowValues(e.VisibleIndex, "IMEI").ToString());
            ASPxImage imgIMEI2 = e.Row.FindChildControl<ASPxImage>("imgIMEI2"); //(ASPxImage)gvDetail.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)gvDetail.Columns[5], "imgIMEI2");

            lbl2.Attributes["onmouseover"] = string.Format("show('{0}');", IMEIContent(Convert.ToInt16(lbl2.Text)));
            lbl2.Attributes["onmouseout"] = "hide();";

            lb14.Attributes["onmouseover"] = string.Format("show('{0}');", SupplyContent(1));
            lb14.Attributes["onmouseout"] = "hide();";

            //img status
            //intC_IMEI - intS_IMEI = 0
            if ((intI1_IMEI - intC1_IMEI) == 0)
            {
                txt4.Visible = false;
                btn3.Enabled = false;
            }
            if ((intC1_IMEI - intS1_IMEI) == 0)
            {
                imgIMEI2.ImageUrl = "~/Icon/check.png";
            }
            //intC_IMEI - intS_IMEI > 1
            if ((intC1_IMEI - intS1_IMEI) > 0)
            {
                imgIMEI2.ImageUrl = "~/Icon/non_complete.png";
            }
            if (check == false)
            {
                imgIMEI2.Visible = false;
                lbl2.Visible = false;
                btn3.Visible = false;
                txt4.Visible = false;

            }
        }
    }

    #endregion
}
