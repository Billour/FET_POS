using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Advtek.Utility;

public partial class VSS_CONS_CON14 : BasePage
{
    //驗收單編號
    private string _INNO = "";
    private string _UUID = "";
    private string _Err = "";       //錯誤訊息 M 代表數量不一致 H代表資料有問題
    //是否可以編輯
    private string EDIT_STATUS = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        doChangeMaster();
        if (!IsPostBack)
        {
            comOENO.TextField = "OENO";
            comOENO.ValueField = "OENO";
            DataTable DT = new CON14_Facade().get_OENO(true);
            DT.Rows[0][0] = "-請選擇-";
            DT.AcceptChanges();
            comOENO.SelectedIndex = 0;
            comOENO.DataSource = DT;// Supplier_Facade.GetSupplierNo(true);
            comOENO.DataBind();

            lblUSER.Text = logMsg.OPERATOR + " " + new Employee_Facade().GetEmpName(logMsg.OPERATOR);
            lblModifiedDate.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");  //更新日期
            ReceivedDate1.Text = DateTime.Now.ToString("yyyy/MM/dd");
            hidSubmit.Text = "";
        }
    }

    protected void bindMasterData()
    {
        DataTable dtGvMaster = new DataTable();
        dtGvMaster = new CON14_Facade().getDetail_VW_CON14(comOENO.Text);
        if (dtGvMaster.Rows.Count > 0)
        {
            lblSUPPNAME.Text = dtGvMaster.Rows[0]["SUPP_NAME"].ToString();
            ReceivedDate1.Text = dtGvMaster.Rows[0]["ORDER_DATE"].ToString();
            lblOrderNo.Text = dtGvMaster.Rows[0]["ORDNO"].ToString();
            laSUPP_ID.Text = dtGvMaster.Rows[0]["SUPP_ID"].ToString();
            labStoreName.Text = dtGvMaster.Rows[0]["STORE_NO"].ToString();
            divButtons.Visible = true;
        }
        else
        {
            divButtons.Visible = false;
            lblSUPPNAME.Text = "";
            lblOrderNo.Text = "";
            laSUPP_ID.Text = "";
            labStoreName.Text = "";
        }
        btnSave.Visible = true;
        btnClear.Visible = true;
        btnPrint.Visible = false;
        lblStatus.Text = "未驗收";
        Session["gvMaster"] = dtGvMaster;
        gvMaster.DataSource = dtGvMaster;
        gvMaster.DataBind();
    }




    protected void btnSave_Click(object sender, EventArgs e)
    {
        _Err = "";
        DataTable v_CSM_INM = CSM_INM();
        DataTable v_CSM_IND = CSM_IND();
        if (_Err == "H")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "驗收量", "alert('驗收量不可為0，請重新輸入!!!');", true);
            return;
        }
        if (_Err == "M")
        {
            hidSubmit.Text = "Y";
            Session["v_CSM_INM"] = v_CSM_INM;
            Session["v_CSM_IND"] = v_CSM_IND;
        }
        else
        {
            doSaveData(v_CSM_INM, v_CSM_IND);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void TbtnSave_Click(object sender, EventArgs e)
    {
        doSaveData((DataTable)Session["v_CSM_INM"], (DataTable)Session["v_CSM_IND"]);
    }
    protected void comOENO_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
        divButtons.Visible = true;
    }
    protected void GoToCon13_Click(object sender, EventArgs e)
    {
        Response.Redirect("CON13.aspx");
    }


    protected void gvMaster_PageIndexChanged(object sender, EventArgs e)
    {
        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();
        gvMaster.DetailRows.CollapseAllRows();

    }
    private void doSaveData(DataTable v_CSM_INM, DataTable v_CSM_IND)
    {
        int intResult = new CON14_Facade().SaveOrderData(v_CSM_INM, v_CSM_IND);
        string msgStr = "";
        if (_Err == "M")
        {
            msgStr = "存檔完成，部分驗收完成!";
            lblStatus.Text = "部分驗收";
        }
        else
        {
            msgStr = "存檔完成，已完成驗收!";
            lblStatus.Text = "已驗收";
        }
        EDIT_STATUS = "N";
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "儲存", "alert('" + msgStr + "!');", true);
        btnPrint.Visible = true;
        btnSave.Visible = false;
        btnClear.Visible = false;
        gvMaster.DataSource = Session["gvMaster"];
        gvMaster.DataBind();
    }

    protected void gvMaster_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            ASPxTextBox txtIN_QTY = e.Row.FindChildControl<ASPxTextBox>("txtIN_QTY");
            ASPxTextBox txtREMARK = e.Row.FindChildControl<ASPxTextBox>("txtREMARK");
            //已結案
            if (this.lblStatus.Text.ToString().Trim() == "已驗收" || EDIT_STATUS == "N")
            {
                txtIN_QTY.ReadOnly = true;
                txtREMARK.ReadOnly = true;
            }
        }
    }


    private void doChangeMaster()
    {
        if (gvMaster.VisibleRowCount > 0)
        {
            DataTable dtINVD = (DataTable)Session["gvMaster"];
            DataRow drINVD;
            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                ASPxLabel PRODNO = ((ASPxLabel)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["PRODNO"], "txtPRODNO"));
                if (PRODNO != null)
                {
                    if (PRODNO.Text != "")
                    {
                        drINVD = dtINVD.Select("PRODNO='" + PRODNO.Text + "'")[0];
                        string txtIN_QTY = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["IN_QTY"], "txtIN_QTY")).Text;
                        drINVD["IN_QTY"] = txtIN_QTY.ToString();
                        string txtREMARK = ((ASPxTextBox)gvMaster.FindRowCellTemplateControl(i, (GridViewDataColumn)gvMaster.Columns["REMARK"], "txtREMARK")).Text;
                        drINVD["REMARK"] = txtREMARK.ToString();
                    }
                }
            }
            dtINVD.AcceptChanges();
            Session["gvMaster"] = dtINVD;
        }
    }


    private DataTable CSM_INM()
    {
        DataTable CSM_INM = new DataTable();
        //驗收單編號
        _INNO = "SR" + this.labStoreName.Text.ToString().Substring(0, 4) + "-" + SerialNo.GenNo("SC");
        _UUID = GuidNo.getUUID();
        //寄銷商品進貨主檔
        CSM_INM.TableName = "CSM_INM";
        CSM_INM.Columns.Add("CSM_INM_ID", typeof(string)); //CSM_INM_ID 
        CSM_INM.Columns.Add("OENO", typeof(string)); //OENO       
        CSM_INM.Columns.Add("INNO", typeof(string)); //INNO       
        CSM_INM.Columns.Add("ORDNO", typeof(string)); //ORDNO      
        CSM_INM.Columns.Add("INDATE", typeof(string)); //INDATE     
        CSM_INM.Columns.Add("ISOK", typeof(string)); //ISOK       
        CSM_INM.Columns.Add("CREATE_USER", typeof(string)); //CREATE_USER
        CSM_INM.Columns.Add("CREATE_DTM", typeof(DateTime)); //CREATE_DTM 
        CSM_INM.Columns.Add("MODI_USER", typeof(string)); //MODI_USER  
        CSM_INM.Columns.Add("MODI_DTM", typeof(DateTime)); //MODI_DTM   
        CSM_INM.Columns.Add("SUPP_ID", typeof(string)); //SUPP_ID    
        CSM_INM.Columns.Add("STORE_NO", typeof(string)); //STORE_NO   
        DataRow CSM_INM_NewRow = CSM_INM.NewRow();
        CSM_INM_NewRow["CSM_INM_ID"] = _UUID;
        CSM_INM_NewRow["OENO"] = comOENO.Text;
        CSM_INM_NewRow["INNO"] = _INNO;
        CSM_INM_NewRow["ORDNO"] = lblOrderNo.Text;
        CSM_INM_NewRow["INDATE"] = Convert.ToDateTime(ReceivedDate1.Text).ToString("yyyyMMdd");
        CSM_INM_NewRow["ISOK"] = "7";
        CSM_INM_NewRow["CREATE_USER"] = this.logMsg.CREATE_USER.ToString().Trim();
        CSM_INM_NewRow["CREATE_DTM"] = this.logMsg.CREATE_DTM.ToString().Trim();
        CSM_INM_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
        CSM_INM_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
        CSM_INM_NewRow["SUPP_ID"] = laSUPP_ID.Text.ToString();
        CSM_INM_NewRow["STORE_NO"] = labStoreName.Text.ToString();
        CSM_INM.Rows.Add(CSM_INM_NewRow);
        return CSM_INM;
    }

    private DataTable CSM_IND()
    {
        DataTable CSM_IND = new DataTable();
        int SUM_QTY = 0;
        CSM_IND.TableName = "CSM_IND";
        CSM_IND.Columns.Add("PRODNO", typeof(string)); //PRODNO      
        CSM_IND.Columns.Add("INQTY", typeof(int)); //INQTY       
        CSM_IND.Columns.Add("REMARK", typeof(string)); //REMARK      
        CSM_IND.Columns.Add("CHECK_QTY", typeof(int)); //CHECK_QTY   
        CSM_IND.Columns.Add("CREATE_USER", typeof(string)); //CREATE_USER 
        CSM_IND.Columns.Add("CHECK_USER", typeof(string)); //CHECK_USER  
        CSM_IND.Columns.Add("CREATE_DTM", typeof(DateTime)); //CREATE_DTM  
        CSM_IND.Columns.Add("MODI_USER", typeof(string)); //MODI_USER   
        CSM_IND.Columns.Add("MODI_DTM", typeof(DateTime)); //MODI_DTM    
        CSM_IND.Columns.Add("CSM_INM_ID", typeof(string)); //CSM_INM_ID  
        CSM_IND.Columns.Add("CSM_IND_ID", typeof(string)); //CSM_IND_ID  
        DataTable DtMaster = Session["gvMaster"] as DataTable;
        if (gvMaster.VisibleRowCount > 0)
        {
            for (int i = 0; i < gvMaster.VisibleRowCount; i++)
            {
                if (int.Parse(DtMaster.Rows[i]["QTY"].ToString()) != int.Parse(DtMaster.Rows[i]["IN_QTY"].ToString()))
                {
                    _Err = "M";
                }
                SUM_QTY += int.Parse(DtMaster.Rows[i]["IN_QTY"].ToString());
                DataRow CSM_IND_NewRow = CSM_IND.NewRow();
                CSM_IND_NewRow["PRODNO"] = DtMaster.Rows[i]["PRODNO"].ToString();
                CSM_IND_NewRow["INQTY"] = DtMaster.Rows[i]["QTY"].ToString();
                CSM_IND_NewRow["REMARK"] = DtMaster.Rows[i]["REMARK"].ToString();
                CSM_IND_NewRow["CHECK_QTY"] = DtMaster.Rows[i]["IN_QTY"].ToString();
                CSM_IND_NewRow["CREATE_USER"] = this.logMsg.CREATE_USER.ToString().Trim();
                CSM_IND_NewRow["CREATE_USER"] = this.logMsg.CREATE_USER.ToString().Trim();
                CSM_IND_NewRow["CREATE_DTM"] = this.logMsg.CREATE_DTM.ToString().Trim();
                CSM_IND_NewRow["MODI_USER"] = this.logMsg.MODI_USER.ToString().Trim();
                CSM_IND_NewRow["MODI_DTM"] = this.logMsg.MODI_DTM.ToString().Trim();
                CSM_IND_NewRow["CSM_INM_ID"] = _UUID;
                CSM_IND_NewRow["CSM_IND_ID"] = GuidNo.getUUID();
                CSM_IND.Rows.Add(CSM_IND_NewRow);
            }
        }
        if (SUM_QTY <= 0) _Err = "H";
        return CSM_IND;
    }
}
