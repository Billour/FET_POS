using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.DTO;
using Advtek.Utility;

public partial class VSS_SAL_SAL01_inputIMEIData : Popup
{
    public static class param
    {
        public static string TableName = ""; //以此來判別資料IMEI的資料來源
        public static string REFID = "";   //相關連table的ID, 如銷售系統為 SALE_DETAIL 的 ID
        public static string PRODNO = "";
        public static string IMEIFLAG = "";
        public static string POOENO = "";
        public static int QTY = 0;
        public static int INV18_QTY = 0;
        public static string INV18_STORENO = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["dtIMEI"] = null;

            string[] s = KeyFieldValue1.Split(new char[] { ';' });
            if (s.Length >= 4)
            {
                param.TableName = s[0];
                param.REFID = s[1];
                param.PRODNO = s[2];
                param.QTY = 0;
                if (!string.IsNullOrEmpty(s[3]))
                {
                    param.QTY = Convert.ToInt32(s[3]);
                }
            }
            if (s.Length >= 5)
            {
                switch (param.TableName)
                {
                    case "INV_APPRO_IMEI":
                        param.POOENO = StringUtil.CStr(s[4]);
                        break;
                    case "STOCKADJ_D_IMEI":
                        param.INV18_QTY = Convert.ToInt32(s[4]);
                        param.INV18_STORENO = StringUtil.CStr(s[5]);
                        break;
                    default:
                        if (!string.IsNullOrEmpty(s[4]))
                        {
                            param.IMEIFLAG = StringUtil.CStr(s[4]);
                        }
                        break;

                }

            }

            lbPRODNO.Text = param.PRODNO;
            DataTable dtProduct = new Product_Facade().Query_ProductInfo(param.PRODNO);
            if (dtProduct != null && dtProduct.Rows.Count > 0 && dtProduct.Rows[0]["PRODNAME"] != null)
                lbPRODNAME.Text = StringUtil.CStr(dtProduct.Rows[0]["PRODNAME"]);
            else
                lbPRODNAME.Text = "";

            bindMasterData();  //載入資料
            txtIMEI.Focus();
            //EnabledControl();

        }
    }

    protected void bindMasterData()
    {
        grid.DataSource = getMasterData();
        grid.DataBind();
    }

    private DataTable getMasterData()
    {
        DataTable dt = null;
        if (ViewState["dtIMEI"] == null)
        {
            dt = new IMEI_Facade().getINV_IMEI(param.TableName, param.REFID, param.PRODNO);
            ViewState["dtIMEI"] = dt;
        }
        else
        {
            dt = ViewState["dtIMEI"] as DataTable;
        }
        return dt;
    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        bindMasterData();
    }

    protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (ViewState["dtIMEI"] != null)
        {
            DataTable dt = ViewState["dtIMEI"] as DataTable;
            string strSID = StringUtil.CStr(e.Keys["SID"]);

            string expression;
            expression = "IMEI = '" + StringUtil.CStr(e.NewValues["IMEI"]) + "'";
            if (!string.IsNullOrEmpty(strSID))
            {
                expression += " AND SID <> '" + strSID + "'";
            }

            DataRow[] data = dt.Select(expression);

            if (data.Length > 0)
            {
                e.RowError = "IMEI資料已存在!";
                return;
            }
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        IMEI_Facade imei = new IMEI_Facade();
        if (param.TableName == "SALE_IMEI_LOG")
        {
            bool haveGarbageIMEIRec = new SAL01_Facade().haveGarbageIMEIRec(txtIMEI.Text);
            if (haveGarbageIMEIRec)
            {
                int ret = imei.DeleteINV_IMEI("SALE_IMEI_LOG", txtIMEI.Text, false);
                if (ret < 0)
                {
                    txtIMEI.Enabled = false;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "DBERROR", "alert('資料庫出問題!');", true);
                    return;
                }
            }
        }

        //銷售要另外判斷，該IMEI是否被其它筆資料Cache住。
        if (param.TableName == "SALE_IMEI_LOG")
        {
            DataTable dt = imei.CheckINV_IMEI(
                             param.TableName,
                             param.REFID,
                             param.PRODNO,
                             txtIMEI.Text);
            if (dt.Rows.Count > 0)
            {
                if (hidForcedInput.Value == "Y")
                {
                    //該IMEI是否被其它筆資料Cache住了，但User同意強制輸入，則先刪除被Cache住的IMEI資料
                    int ret = imei.DeleteINV_IMEI("SALE_IMEI_LOG", txtIMEI.Text, true);
                    if (ret < 0)
                    {
                        txtIMEI.Enabled = false;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "DBERROR", "alert('資料庫出問題!');", true);
                        return;
                    }

                    dt = imei.CheckINV_IMEI(
                             param.TableName,
                             param.REFID,
                             param.PRODNO,
                             txtIMEI.Text);

                    if (dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "IMEI_DATA_EXIST", "alert('IMEI資料已存在且不允許被刪除!');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "IMEI_DATA_EXIST", "confirmIMEIInput();", true);
                    return;
                }
            }
        }

        DataTable dtEMIE = null;
        string locId = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();

        dtEMIE = imei.getApprove_AllowIMEI(param.TableName, logMsg.STORENO, locId, param.PRODNO, txtIMEI.Text);

        DataTable dtView = ViewState["dtIMEI"] as DataTable;

        if (dtEMIE.Rows.Count > 0)
        {
            if (param.QTY <= dtView.Rows.Count)
            {
                txtIMEI.Enabled = false;
                btnInsert.Enabled = false;
                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "IMEI_OVER_QTY", "alert('只能輸入" + StringUtil.CStr(param.QTY) + "個IMEI數量');", true);
                return;
            }
            else
            {
                string expression = "IMEI = '" + txtIMEI.Text + "'";
                DataRow[] data = dtView.Select(expression);

                if (data.Length > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "IMEI_OVER_QTY", "alert('IMEI資料已存在');", true);
                    return;
                }
                else
                {
                    if (param.TableName == "STOCKADJ_D_IMEI")
                    {
                        DataRow[] dataTmp;
                        dataTmp = dtEMIE.Select("STATUS = '4' ");
                        if (dataTmp.Length > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "IMEI_OVER_QTY", "alert('此IMEI已銷售');", true);
                            return;
                        }
                        if (param.INV18_QTY > 0) // >0為 加庫存量
                        {

                            dataTmp = dtEMIE.Select("IVRCODE <> '' or IVRCODE is Not Null ");
                            if (dataTmp.Length > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "IMEI_OVER_QTY", "alert('此IMEI已使用');", true);
                                return;
                            }

                        }
                        if (param.INV18_QTY < 0) // <0為 減庫存量
                        {

                            dataTmp = dtEMIE.Select("IVRCODE <> '" + param.INV18_STORENO + "' or IVRCODE = '' or IVRCODE is Null");
                            if (dataTmp.Length > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "IMEI_OVER_QTY", "alert('此IMEI非調整門市的資料');", true);
                                return;
                            }

                        }

                    }
                    //**2011/04/06 Tina：按下[確定]才把ASPxGridView中的資料存入至DB中，此時先把資料放入ViewState中。
                    //imei.InsertINV_IMEI(param.TableName, param.REFID, param.PRODNO, txtIMEI.Text, logMsg.MODI_USER, param.POOENO);
                    InsertViewState();
                    bindMasterData();
                    txtIMEI.Text = "";
                    //EnabledControl();
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "IMEI_NOT_ALLOW", "alert('IMEI不存在');", true);
        }
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        //**2011/04/06 Tina：按下[確定]才把ASPxGridView中的資料存入至DB中，此時是把ViewState的資料刪除。
        //new IMEI_Facade().DeleteINV_IMEI(param.TableName, this.grid.GetSelectedFieldValues(grid.KeyFieldName));
        DeleteViewState();
        bindMasterData();
        //EnabledControl();
    }

    protected void OkButton_Click(object sender, EventArgs e)
    {
        ModifyDataToDB();  //將ASPxGridView中的資料變更至DB Table中。
        ReturnValue();
    }

    private void ReturnValue()
    {
        string IMEI = "";
        if (grid.VisibleRowCount > 0)
        {
            IMEI = StringUtil.CStr(grid.GetRowValues(0, "IMEI"));
        }

        SetReturnValue(IMEI);
        SetReturnControlValue(StringUtil.CStr(getMasterData().Rows.Count));

    }

    private void ModifyDataToDB()
    {
        if (ViewState["dtIMEI"] != null)
        {
            DataTable dt = ViewState["dtIMEI"] as DataTable;
            //Delete + Insert = Update
            IMEI_Facade imei = new IMEI_Facade();
            imei.Modify_IMEI(dt, param.TableName, param.REFID, param.PRODNO, logMsg.OPERATOR, param.POOENO);
        }
    }

    private void InsertViewState()
    {
        if (ViewState["dtIMEI"] != null)
        {
            DataTable dt = ViewState["dtIMEI"] as DataTable;
            DataRow dr = dt.NewRow();
            dr["SID"] = GuidNo.getUUID();
            dr["IMEI"] = txtIMEI.Text;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }

    }

    private void DeleteViewState()
    {
        if (ViewState["dtIMEI"] != null)
        {
            DataTable dt = ViewState["dtIMEI"] as DataTable;
            string where = "";
            List<object> keyValues = this.grid.GetSelectedFieldValues(grid.KeyFieldName);
            foreach (string skey in keyValues)
            {
                where += "'" + skey + "',";
            }
            if (where.Length > 0)
                where = where.Substring(0, where.Length - 1);
            else where = "''";

            DataRow[] dra = dt.Select("SID in(" + where + ")");

            foreach (DataRow dr in dra)
            {
                dt.Rows.Remove(dr);
                dt.AcceptChanges();
            }
        }
    }

    private void EnabledControl()
    {
        if (param.TableName == "SALE_IMEI_LOG" && param.IMEIFLAG != "4")
        {
            DataTable dtEMIE = (DataTable)grid.DataSource;
            if (dtEMIE.Rows.Count == param.QTY)
            {
                txtIMEI.Enabled = false;
                btnInsert.Enabled = false;
                okButton.Enabled = false;
                btnCancel.Focus();
            }
            else if (dtEMIE.Rows.Count < param.QTY)
            {
                txtIMEI.Enabled = true;
                btnInsert.Enabled = true;
                okButton.Enabled = true;
            }
        }
    }
}
