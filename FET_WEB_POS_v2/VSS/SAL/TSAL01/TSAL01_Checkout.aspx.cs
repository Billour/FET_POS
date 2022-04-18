using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Net;
using System.Text;
using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.Facade.FacadeImpl;
using Newtonsoft.Json.Linq;

public partial class VSS_SAL_TSAL01_TSAL01_Checkout : BasePage
{
    public string exclude_item_type = "4,5,6,11,12";
    protected void Page_Load(object sender, EventArgs e)
    {
        String json_str = Request.Form["json"];
        JObject o = JObject.Parse(json_str);
        string ORIGINAL_MASTER = (String)o["ORIGINAL_MASTER"];
        string POSUUID_MASTER = (String)o["POSUUID_MASTER"];
        string POSUUID_DETAIL = (String)o["POSUUID_DETAIL"];
        JArray items = (JArray)o["CHECKOUT"];
        string CANCEL_TRANS = (String)o["CANCEL_TRANS"];
        string UNI_NO = (String)o["UNI_NO"];
        string UNI_TITLE = (String)o["UNI_TITLE"];
        string REMARK = (String)o["REMARK"];
        string SALE_TYPE = (String)o["SALE_TYPE"];
        string PRINTNAME = (String)o["PRINTNAME"];
        string RECEIPTNAME = (String)o["RECEIPTNAME"];

        if (CANCEL_TRANS.Equals("1"))
        {
            Delete(POSUUID_MASTER, POSUUID_DETAIL);
        }
        else
        {
            CheckOut(POSUUID_MASTER, ORIGINAL_MASTER, items, UNI_NO, UNI_TITLE, REMARK, PRINTNAME, RECEIPTNAME);
        }
    }


    private void CheckOut(string POSUUID_MASTER, string ORIGINAL_MASTER, JArray items, string uni_no, string uni_title, string remark, string PrintName, string receiptname)
    {
        bool IsCheckOut = false;
        string SALE_NO = "";
        string url = "";
        string INVOICE_NO = "";
        string RECEIPT_URL = "";
        OracleConnection conn = null;
        OracleTransaction trans = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            trans = conn.BeginTransaction();
            if (ORIGINAL_MASTER.Length != 0)
            {
                // 如果 ORIGINAL_MASTER 不為空, 則為換貨程序
                CancelSale(trans, ORIGINAL_MASTER);
            }

            //讀取銷售編號
            SALE_NO = Store_SerialNo.GenNo("SALE", logMsg.STORENO, logMsg.MACHINE_ID);
            //更新SALE_DETAIL
            //UpdateSaleDetail(POSUUID_MASTER, trans);

            UpdateSalesHead(trans, POSUUID_MASTER, SALE_NO, uni_no, uni_title, remark);
            //新增找零
            InsertPaid_Detail(trans, items, POSUUID_MASTER);
            Imei_log(POSUUID_MASTER, trans);
            CalculationTax(POSUUID_MASTER, trans); //計算稅
            //扣庫存
            this.Inventory(trans, POSUUID_MASTER, SALE_NO);
            //產生發票
            INVOICE_NO = SetInvoiceOrReceipt(POSUUID_MASTER, trans);

            //
            StoreSpecialDIS(POSUUID_MASTER, trans);

            UpdateToCloseHead(POSUUID_MASTER, trans);

            if (!Utils.IsDebug())
            {
                CheckOutService(POSUUID_MASTER, trans);
            }


            trans.Commit();



            IsCheckOut = true;
        }
        catch (Exception ex)
        {

            trans.Rollback();
            Logger.Log.Error(string.Format("TSAL01:POSUUID_MASTER:{0},ERROR:{1}", POSUUID_MASTER, ex.Message));
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write(string.Format("{{ RESULT : '{0}'}}", ex.Message.Replace("\n", "\\n")));
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearAllPools();
        }



        if (IsCheckOut)
        {
            try
            {
                OtherService(POSUUID_MASTER);
                collection(POSUUID_MASTER);
                RECEIPT_URL = Collection_Receipt(POSUUID_MASTER, receiptname);
                if (!string.IsNullOrEmpty(INVOICE_NO))
                {
                    url = PrintInventory(POSUUID_MASTER, PrintName);
                }



            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("TSAL01:POSUUID_MASTER:{0},ERROR:{1}", POSUUID_MASTER, ex.Message));
            }
        }

        if (IsCheckOut)
        {
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write(string.Format("{{ RESULT:'1', SALE_NO:'{0}',INVOICE_URL:'{1}',INVOICE_NO:'{2}',HG_POINT:'{3}', RECEIPT_URL:'{4}' }}", SALE_NO, url, INVOICE_NO, "0", RECEIPT_URL));
        }
    }

    private string CheckHP_award_point(string possuuid_master, OracleTransaction trans)
    {
        string result = "";
        HappyGo_Facade facade = new HappyGo_Facade();
        //抓取金額
        List<string> list = new List<string>();
        list = facade.get_hg_award_point(possuuid_master, trans);
        result = list[1];
        //更新金額回去
        string sqlstr = "update SALE_HEAD set hg_award_point = :hg_award_point,hg_prodno=:hg_prodno where posuuid_master = " + OracleDBUtil.SqlStr(possuuid_master);
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        cmd.Parameters.Add(":hg_award_point", OracleType.Number, 9).Value = OracleNumber.Parse(result);
        cmd.Parameters.Add(":hg_prodno", OracleType.VarChar, 20).Value = list[0];
        cmd.ExecuteNonQuery();
        return result;
    }

    private void UpdateToCloseHead(string posuuid_master, OracleTransaction trans)
    {
        //先抓出所有detail
        string sqlstr = "select posuuid_detail from sale_detail where posuuid_master=" + OracleDBUtil.SqlStr(posuuid_master);
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        sqlstr = "update TO_CLOSE_HEAD set posuuid_master = :posuuid_master where posuuid_detail = :posuuid_detail";
        cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        cmd.Parameters.Add(":posuuid_master", OracleType.VarChar).Value = posuuid_master;
        cmd.Parameters.Add(":posuuid_detail", OracleType.VarChar);
        foreach (DataRow row in dt.Rows)
        {
            cmd.Parameters[":posuuid_detail"].Value = StringUtil.CStr(row[0]);
            cmd.ExecuteNonQuery();
        }
    }


    private string PrintInventory(string POSUUID_MASTER, string PrintName)
    {
        string url = "";
        Receipt myReceipt = new Receipt();
        SAL01_Facade facade = new SAL01_Facade();
        string filePath = facade.getUploadPath(POSUUID_MASTER);
        if (!string.IsNullOrEmpty(filePath))
        {
            string fileName = myReceipt.generateReceiptfortest(POSUUID_MASTER, PrintName);
            string fileName2 = myReceipt.generateReceiptforAccount(POSUUID_MASTER, PrintName);
            if (fileName == null || string.IsNullOrEmpty(fileName))
            {
                throw new Exception("列印發票失敗，請重印發票!!");
            }
            else
            {
                url = Request.ApplicationPath + filePath + "/" + fileName;
            }
        }
        else
        {
            throw new Exception("列印發票失敗，請重印發票!!");
        }

        return url;
    }

    private void Inventory(OracleTransaction objTx, string POSUUID_MASTER, string SALE_NO)
    {

        //銷售扣庫存
        INVENTORY_Facade Inventory = new INVENTORY_Facade();
        string sqlstr = "select sd.* ,p.ISSTOCK from SALE_DETAIL sd join PRODUCT p on sd.PRODNO=p.PRODNO  where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
        OracleCommand cmd = new OracleCommand(sqlstr, objTx.Connection, objTx);
        DataTable Detail = new DataTable();
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        da.Fill(Detail);
        DataRow[] Sale_DetailRows5 = Detail.Select("ITEM_TYPE = '1' OR ITEM_TYPE = '2'");
        if (Sale_DetailRows5.Length > 0)
        {
            string STOCK = Common_PageHelper.GetGoodLOCUUID();
            foreach (DataRow dr in Sale_DetailRows5)
            {
                string Code = "";
                string Message = "";

                if (!string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])) && StringUtil.CStr(dr["ISSTOCK"]) == "1")
                {

                    Inventory.PK_INVENTORY_SALE(objTx, "1", StringUtil.CStr(dr["PRODNO"]),
                       logMsg.STORENO, STOCK, SALE_NO,
                       Convert.ToInt32(dr["QUANTITY"]), logMsg.MODI_USER, StringUtil.CStr(dr["ID"]), ref Code, ref Message);
                    if (Code != "000")
                    {
                        throw new Exception(Message);

                    }

                }
            }
        }
    }

    public void InsertPaid_Detail(OracleTransaction objTX, JArray checkout, string POSUUID_MASTER)
    {
        string sqlstr = "";
        OracleCommand cmd = null;
        try
        {
            foreach (object c in checkout)
            {
                JObject jo = (JObject)c;
                string PAID_AMOUNT = (string)jo["AMOUNT"];
                string PAID_MODE = (string)jo["TYPE"];
                string DESC = (string)jo["DESC"];
                sqlstr = string.Format("INSERT INTO PAID_DETAIL(ID,PAID_MODE,PAID_AMOUNT,DESCRIPTION,POSUUID_MASTER,CREATE_DTM) values(pos_uuid(),{0},{1},'{2}','{3}',SYSDATE)", PAID_MODE, PAID_AMOUNT, DESC, POSUUID_MASTER);
                cmd = new OracleCommand(sqlstr, objTX.Connection, objTX);
                cmd.ExecuteNonQuery();
            }

            
            //判斷是否有退保證金
            double guarantee_price = 0;
            sqlstr = "select nvl(sum(total_amount),0) from SALE_DETAIL where total_amount < 0 and prodno in (select GUARANTEE_PRODNO from GUARANTEE_PROD_MAPPING) and posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
            cmd = new OracleCommand(sqlstr, objTX.Connection, objTX);

            guarantee_price = cmd.ExecuteScalar() == null ? 0 : Convert.ToDouble(cmd.ExecuteScalar());
            if (guarantee_price < 0)
            {
                sqlstr = string.Format("INSERT INTO PAID_DETAIL(ID,PAID_MODE,PAID_AMOUNT,DESCRIPTION,POSUUID_MASTER,CREATE_DTM) values(pos_uuid(),{0},{1},'{2}','{3}',SYSDATE)", 1, guarantee_price, "退保證金", POSUUID_MASTER);
                cmd = new OracleCommand(sqlstr, objTX.Connection, objTX);
                cmd.ExecuteNonQuery();

                sqlstr = string.Format("INSERT INTO PAID_DETAIL(ID,PAID_MODE,PAID_AMOUNT,DESCRIPTION,POSUUID_MASTER,CREATE_DTM) values(pos_uuid(),{0},{1},'{2}','{3}',SYSDATE)", 1, guarantee_price * -1, "退保證金", POSUUID_MASTER);
                cmd = new OracleCommand(sqlstr, objTX.Connection, objTX);
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }

    private void UpdateSalesHead(OracleTransaction TransPOS, string POSUUID_MASTER, string SALE_NO, string uni_no, string uni_title, string remark)
    {

        string sqlstr = string.Format("update SALE_HEAD set SALE_STATUS='2', SALE_NO='{0}', UNI_NO={2}, UNI_TITLE={3}, REMARK={4} where POSUUID_MASTER = {1}", SALE_NO, OracleDBUtil.SqlStr(POSUUID_MASTER), OracleDBUtil.SqlStr(uni_no), OracleDBUtil.SqlStr(uni_title), OracleDBUtil.SqlStr(remark));
        OracleCommand cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        cmd.ExecuteNonQuery();
        //更新SALE_TOTAL_AMOUNT
        sqlstr = string.Format("update SALE_HEAD set SALE_TOTAL_AMOUNT = (select sum(TOTAL_AMOUNT) from SALE_DETAIL  where posuuid_master = '{0}') where POSUUID_MASTER = '{0}'", POSUUID_MASTER);
        cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        cmd.ExecuteNonQuery();

        //更新DISCOUNT_TOTAL_AMOUNT
        sqlstr = string.Format("update SALE_HEAD set DISCOUNT_TOTAL_AMOUNT = (select sum(TOTAL_AMOUNT) from SALE_DETAIL  where posuuid_master = '{0}'  and item_type  in ({1}) ) where POSUUID_MASTER = '{0}' ", POSUUID_MASTER, exclude_item_type);
        cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        cmd.ExecuteNonQuery();

    }

    public void UpdateSaleDetail(string POSUUID_MASTER, OracleTransaction trans)
    {
        //確認銷售金額是否為負
        string sqlstr = "select sale_total_amount,discount_total_amount from SALE_HEAD where sale_total_amount < 0 and posuuid_master=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        OracleDataReader dr = cmd.ExecuteReader();
        bool hasRow = false;
        double discount_total_amount = 0;
        double sale_total_amount = 0;
        double sale_amount = 0;
        if (dr.Read())
        {
            hasRow = true;
            sale_total_amount = dr.GetDouble(0);
            discount_total_amount = dr.GetDouble(1);
        }
        dr.Close();

        if (hasRow)
        {
            sale_amount = sale_total_amount - discount_total_amount;
            double temp_amount = sale_amount + discount_total_amount;


            sqlstr = string.Format("select * from sale_detail where total_amount < 0 and posuuid_master = {0} and item_type in ({1}) order by total_amount", OracleDBUtil.SqlStr(POSUUID_MASTER), exclude_item_type);
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            sqlstr = "update SALE_DETAIL set TOTAL_AMOUNT = :TOTAL_AMOUNT,UNIT_PRICE = :TOTAL_AMOUNT where ID=:ID";
            cmd = new OracleCommand(sqlstr, trans.Connection, trans);
            cmd.Parameters.Add(":TOTAL_AMOUNT", OracleType.Number);
            cmd.Parameters.Add(":ID", OracleType.NVarChar);
            cmd.Parameters.Add(":SEQ_NO", OracleType.Number);
            foreach (DataRow row in dt.Rows)
            {
                if (temp_amount != 0)
                {
                    cmd.Parameters[":ID"].Value = row["ID"];
                    double discount_amount = Convert.ToDouble(row["total_amount"]);
                    if (discount_amount > temp_amount)
                    {
                        temp_amount += discount_amount;
                        cmd.Parameters[":TOTAL_AMOUNT"].Value = 0;
                    }
                    else
                    {
                        row["total_amount"] = discount_amount - temp_amount;
                        cmd.Parameters[":TOTAL_AMOUNT"].Value = discount_amount - temp_amount;
                        temp_amount = 0;
                    }
                    cmd.ExecuteNonQuery();
                }
            }


        }
    }


    public void CheckOutService(string POSUUID_MASTER, OracleTransaction trans)
    {
        string sqlStr = @"Delete FROM TO_CLOSE_ITEM WHERE POSUUID_DETAIL IN (Select POSUUID_DETAIL From TO_CLOSE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + ") ";
        OracleCommand cmd = new OracleCommand(sqlStr, trans.Connection, trans);
        cmd.ExecuteNonQuery();

        sqlStr = @"Delete FROM TO_CLOSE_DISCOUNT WHERE POSUUID_DETAIL IN (Select POSUUID_DETAIL From TO_CLOSE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + ") ";
        cmd.CommandText = sqlStr;
        cmd.ExecuteNonQuery();

        sqlStr = @"Delete FROM TO_CLOSE_HEAD where POSUUID_MASTER = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
        cmd.CommandText = sqlStr;
        cmd.ExecuteNonQuery();

        SAL01_Facade SAL01Facade = new SAL01_Facade();
        TSAL01_Facade tSAL01Facade = new TSAL01_Facade();
        DataTable dtDetail = tSAL01Facade.getSale_Detail(POSUUID_MASTER, "2", trans);
        if (dtDetail != null && dtDetail.Rows.Count > 0)
        {
            string prePosuuidDetail = "";
            string posuuitDetail = "";
            foreach (DataRow dr in dtDetail.Rows)
            {
                string sysID = "";
                if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                    posuuitDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);

                if (dr["SOURCE_TYPE"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["SOURCE_TYPE"]))))
                {
                    switch (StringUtil.CStr(dr["SOURCE_TYPE"]))
                    {
                        case "1": sysID = "IA"; break;
                        case "2": sysID = "LOY"; break;
                        case "3": sysID = "PY"; break;
                        case "4": sysID = "SSI"; break;
                        case "5": sysID = "OLR"; break;
                        case "10": sysID = "ES"; break;
                        default: break;
                    }
                }

                if (sysID != "" && posuuitDetail != "" && posuuitDetail != prePosuuidDetail)
                {
                    prePosuuidDetail = posuuitDetail;
                    if (dr["SERVICE_SYS_ID"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["SERVICE_SYS_ID"])))
                        && dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                    {

                        int result = SAL01Facade.CommitOuterSystem(sysID, StringUtil.CStr(dr["SERVICE_SYS_ID"]),
                                                        POSUUID_MASTER, posuuitDetail,
                                                        StringUtil.CStr(dr["BARCODE1"]), logMsg.OPERATOR, logMsg.STORENO, StringUtil.CStr(dr["BUNDLE_ID"]), StringUtil.CStr(dr["BARCODE2"]), StringUtil.CStr(dr["BARCODE3"]));

                        if (result == -1)
                        {
                            throw new Exception("交易系統Commit失敗!");
                        }
                    }
                }
            }
        }
    }


    #region CalulationTax
    /// <summary>
    /// 計算稅
    /// </summary>
    /// <param name="POSUUID_MASTER"></param>
    /// <param name="TransPOS"></param>
    public void CalculationTax(string POSUUID_MASTER, OracleTransaction TransPOS)
    {

        SAL01_Facade Facade = new SAL01_Facade();
        StringBuilder sb = new StringBuilder();
        string sqlstr = "";
        string SALE_TYPE = "1";
        OracleCommand cmd = null;
        OracleDataAdapter da = null;
        DataTable Head = new DataTable();
        DataTable Discount = new DataTable();
        DataTable Detail = new DataTable();
        double STORE_REC_TOTAL_AMOUNT = 0;

        #region SALE_HEAD
        sqlstr = "select * from SALE_HEAD where  posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
        cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        da = new OracleDataAdapter(cmd);
        da.Fill(Head);
        SALE_TYPE = StringUtil.CStr(Head.Rows[0]["SALE_TYPE"]);
        #endregion

        #region SALE_DETAIL
        sqlstr = "select SD.ID,SD.PRODNO,SD.ITEM_TYPE,p.TAXRATE,p.TAXABLE,SD.QUANTITY,SD.UNIT_PRICE,SD.TOTAL_AMOUNT,P.INV_TYPE,0 as TAX,TOTAL_AMOUNT as BEFORE_TAX from SALE_DETAIL SD join PRODUCT P on SD.PRODNO=P.PRODNO where SD.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER) + " and SD.item_type not in (" + exclude_item_type + ")";
        cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        da = new OracleDataAdapter(cmd);
        da.Fill(Detail);

        #endregion

        #region Discount
        sqlstr = "select SD.ID,SD.PRODNO,SD.ITEM_TYPE,p.TAXRATE,p.TAXABLE,SD.QUANTITY,SD.UNIT_PRICE,SD.TOTAL_AMOUNT,P.INV_TYPE,0 as TAX,0 as BEFORE_TAX from SALE_DETAIL SD join PRODUCT P on SD.PRODNO=P.PRODNO where SD.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER) + " and SD.item_type in (" + exclude_item_type + ")";
        cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        da = new OracleDataAdapter(cmd);
        da.Fill(Discount);

        #endregion

        #region 設定稅額預設金額

        if (SALE_TYPE == "2")
        {   //代收交易
            StringBuilder invProdLis = new StringBuilder("");
            DataTable dtInvProd = Facade.getInvoiceProduct();
            if (dtInvProd != null && dtInvProd.Rows.Count > 0)
            {
                foreach (DataRow dr in dtInvProd.Rows)
                {
                    if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                        invProdLis.Append(StringUtil.CStr(dr[0])).Append(",");
                }
            }

            foreach (DataRow dr in Detail.Rows)
            {
                if (StringUtil.CStr(invProdLis).IndexOf(StringUtil.CStr(dr["PRODNO"])) > -1)
                {   //代收交易需要開發票貨品
                    dr.BeginEdit();
                    dr["INV_TYPE"] = "1";
                    dr.EndEdit();
                }
                else
                {   //代收交易不開發票貨品,一律開收據,稅算為0
                    dr.BeginEdit();
                    dr["INV_TYPE"] = "3";
                    dr.EndEdit();
                }
            }
            Detail.AcceptChanges();
        }

        #endregion

        #region 總金額計算
        int TAXRATE = 5; //照這算法TAXRATE 就沒作用了
        double SALE_AFTER_TOTAL_AMOUNT = 0;         ///銷售稅後總金額  含稅+不稅之應收總金額
        double SALE_AFTER_TAX_TOTAL_AMOUNT = 0;     ///銷售應課稅產品總金額 只有含稅之應收總金額
        double SALE_TOTAL_TAX = 0;                  ///總稅額   SALE_AFTER_TAX_TOTAL_AMOUNT * (5/105)
        double SALE_BEFORE_TOTAL_AMOUNT = 0;        ///銷售稅前金額 =  SALE_AFTER_TAX_TOTAL_AMOUNT - SALE_TOTAL_TAX         
        double DISCOUNT_AFTER_TOTAL_AMOUNT = 0;     ///折扣稅後總金額
        double DISCOUNT_AFTER_TAX_TOTAL_AMOUNT = 0; ///折扣應課稅產品總金額  
        double DISCOUNT_TOTAL_TAX = 0;              ///總稅額   DISCOUNT_AFTER_TAX_TOTAL_AMOUNT * (5/105)
        double DISCOUNT_BEFORE_TOTAL_AMOUNT = 0;    ///銷售稅前金額 =  DISCOUNT_AFTER_TAX_TOTAL_AMOUNT - DISCOUNT_TOTAL_TAX

        //總價
        //銷售稅後總價
        string SALE_TOTAL_AMOUNT = StringUtil.CStr(Head.Rows[0]["SALE_TOTAL_AMOUNT"]);
        //折扣稅後總價
        string DISCOUNT_TOTAL_AMOUNT = StringUtil.CStr(Head.Rows[0]["DISCOUNT_TOTAL_AMOUNT"]);




        foreach (DataRow dr in Detail.Rows)//明細資料
        {  //總金額計算
            string TAXABLE = StringUtil.CStr(dr["TAXABLE"]);
            string TOTAL_AMOUNT = StringUtil.CStr(dr["TOTAL_AMOUNT"]);
            string INV_TYPE = StringUtil.CStr(dr["INV_TYPE"]);
            if (TAXABLE == "Y" && INV_TYPE == "1") //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
                if (TOTAL_AMOUNT != "" && NumberUtil.IsNumeric(TOTAL_AMOUNT))
                    SALE_AFTER_TAX_TOTAL_AMOUNT += int.Parse(TOTAL_AMOUNT);
        }

        foreach (DataRow dr in Discount.Rows) //折扣資料
        {
            string TOTAL_AMOUNT = StringUtil.CStr(dr["TOTAL_AMOUNT"]);
            string TAXABLE = StringUtil.CStr(dr["TAXABLE"]);
            string INV_TYPE = StringUtil.CStr(dr["INV_TYPE"]);
            if (TAXABLE == "Y" && INV_TYPE == "1") //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
                if (TOTAL_AMOUNT != "" && NumberUtil.IsNumeric(TOTAL_AMOUNT))
                    DISCOUNT_AFTER_TAX_TOTAL_AMOUNT += int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
        }

        SALE_AFTER_TOTAL_AMOUNT = string.IsNullOrEmpty(SALE_TOTAL_AMOUNT) ? 0 : int.Parse(SALE_TOTAL_AMOUNT);
        DISCOUNT_AFTER_TAX_TOTAL_AMOUNT = string.IsNullOrEmpty(DISCOUNT_TOTAL_AMOUNT) ? 0 : int.Parse(DISCOUNT_TOTAL_AMOUNT);

        if (SALE_AFTER_TOTAL_AMOUNT > 0)
        {
            if (TAXRATE >= 0)
            {
                //稅
                SALE_TOTAL_TAX = (int)Math.Round((double)((SALE_AFTER_TAX_TOTAL_AMOUNT + DISCOUNT_AFTER_TAX_TOTAL_AMOUNT) * TAXRATE / (100 + TAXRATE)), MidpointRounding.AwayFromZero);//四捨五入
                SALE_BEFORE_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT - SALE_TOTAL_TAX;
                //折扣
                DISCOUNT_TOTAL_TAX = (int)Math.Round((double)(DISCOUNT_AFTER_TAX_TOTAL_AMOUNT * (TAXRATE / (100 + TAXRATE))), MidpointRounding.AwayFromZero); //四捨五入
                DISCOUNT_BEFORE_TOTAL_AMOUNT = DISCOUNT_AFTER_TOTAL_AMOUNT - DISCOUNT_TOTAL_TAX;
            }
            else
            {
                SALE_TOTAL_TAX = 0;
                SALE_BEFORE_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT;
                //折扣
                DISCOUNT_TOTAL_TAX = 0;
                DISCOUNT_BEFORE_TOTAL_AMOUNT = DISCOUNT_AFTER_TOTAL_AMOUNT;
            }

        }
        else
        {   //交易總金額小於等於0,不開發票,稅視為0
            SALE_TOTAL_TAX = DISCOUNT_TOTAL_TAX = 0;
            SALE_BEFORE_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT;
            DISCOUNT_BEFORE_TOTAL_AMOUNT = DISCOUNT_AFTER_TOTAL_AMOUNT;
        }




        //更新SALE_HEAD
        sb = new StringBuilder();
        sb.Append(" update SALE_HEAD set  ");
        sb.Append(" SALE_TAX = :SALE_TAX, ");
        sb.Append(" SALE_BEFORE_TAX = :SALE_BEFORE_TAX,");
        sb.Append(" DISCOUNT_TAX = :DISCOUNT_TAX, ");
        sb.Append(" DISCOUNT_BEFORE_TAX = :DISCOUNT_BEFORE_TAX ");
        sb.Append(" where POSUUID_MASTER = :POSUUID_MASTER ");

        cmd = new OracleCommand(StringUtil.CStr(sb), TransPOS.Connection, TransPOS);
        cmd.Parameters.Add(":SALE_BEFORE_TAX", OracleType.Number).Value = SALE_BEFORE_TOTAL_AMOUNT;
        cmd.Parameters.Add(":SALE_TAX", OracleType.Number).Value = SALE_TOTAL_TAX;
        cmd.Parameters.Add(":DISCOUNT_TAX", OracleType.Number).Value = DISCOUNT_TOTAL_TAX;
        cmd.Parameters.Add(":DISCOUNT_BEFORE_TAX", OracleType.Number).Value = DISCOUNT_BEFORE_TOTAL_AMOUNT;
        cmd.Parameters.Add(":POSUUID_MASTER", OracleType.NVarChar).Value = POSUUID_MASTER;
        cmd.ExecuteNonQuery();
        #endregion

        #region 計算明細稅額

        DataTable SALE_DETAIL = new DataTable();

        //處理明細


        Detail.Merge(Discount);
        SALE_DETAIL = CalDetailTax(Detail, POSUUID_MASTER, TransPOS);


        Discount.AcceptChanges();
        Detail.AcceptChanges();

        #region 回寫SALE_DETAIL
        sqlstr = "update SALE_DETAIL set TAX = :TAX, BEFORE_TAX= :BEFORE_TAX ,TAXABLE=:TAXABLE,TAXRATE = :TAXRATE where ID = :ID";
        cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        cmd.Parameters.Add(":TAX", OracleType.Number);
        cmd.Parameters.Add(":BEFORE_TAX", OracleType.Number);
        cmd.Parameters.Add(":ID", OracleType.NVarChar);
        cmd.Parameters.Add(":TAXRATE", OracleType.Number);
        cmd.Parameters.Add(":TAXABLE", OracleType.NVarChar);

        foreach (DataRow row in SALE_DETAIL.Rows)
        {
            cmd.Parameters[":TAX"].Value = row["TAX"];
            cmd.Parameters[":BEFORE_TAX"].Value = row["BEFORE_TAX"];
            cmd.Parameters[":ID"].Value = row["ID"];
            cmd.Parameters[":TAXRATE"].Value = row["TAXRATE"];
            cmd.Parameters[":TAXABLE"].Value = row["TAXABLE"];
            cmd.ExecuteNonQuery();
        }


        #endregion
    }
        #endregion
    #endregion


    #region SetInvoiceOrReceipt
    /// <summary>
    /// 產生發票連結
    /// </summary>
    /// <param name="POSUUID_MASTER"></param>
    /// <param name="objTx"></param>
    /// <returns></returns>
    private string SetInvoiceOrReceipt(string POSUUID_MASTER, OracleTransaction objTx)
    {
        string INVOICE_NO = "";
        DataRow[] INV_TAX_DRA;       //發票 應稅
        DataRow[] INV_TAX_DRA_ZERO;  //發票 應稅 0稅
        DataRow[] INV_NO_TAX_DRA;    //發票 免稅
        DataRow[] RECDT;             //收據
        SAL01_Facade facade = new SAL01_Facade();
        DataTable head = new DataTable();
        DataTable dtINV = new DataTable();
        OracleCommand cmd = null;
        OracleDataAdapter da = null;

        #region Load SALE_HEAD
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(" select ");
        sb.Append(" UNI_NO, ");
        sb.Append(" STORE_NO, ");
        sb.Append(" UNI_TITLE, ");
        sb.Append(" CREATE_DTM, ");
        sb.Append(" CREATE_USER, ");
        sb.Append(" MODI_DTM, ");
        sb.Append(" MODI_USER, ");
        sb.Append(" MACHINE_ID as HOST_ID, ");
        sb.Append(" INVOICE_TOTAL_AMOUNT AS SALE_TOTAL_AMOUNT, ");
        sb.Append(" SALE_BEFORE_TAX, ");
        sb.Append(" POSUUID_MASTER, ");
        sb.Append(" SALE_TYPE, ");
        sb.Append(" SALE_NO, ");
        sb.Append(" '' as INVOICE_TYPE,");
        sb.Append(" trunc(SYSDATE) as INVOICE_DATE ");
        sb.Append(" from  ");
        sb.Append(" sale_head ");
        sb.AppendFormat(" where posuuid_master = {0} ", OracleDBUtil.SqlStr(POSUUID_MASTER));



        cmd = new OracleCommand(StringUtil.CStr(sb), objTx.Connection, objTx);
        da = new OracleDataAdapter(cmd);
        da.Fill(head);
        #endregion

        if (head.Rows.Count > 0)
        {

            #region Load SALE_DETAIL
            sb = new System.Text.StringBuilder();
            sb.Append(" select ");
            sb.Append(" UNIT_PRICE, ");
            sb.Append(" SD.PRODNO, ");
            sb.Append(" P.PRODNAME, ");
            sb.Append(" QUANTITY, ");
            sb.Append(" BEFORE_TAX, ");
            sb.Append(" TOTAL_AMOUNT, ");
            sb.Append(" TAX, ");
            sb.Append(" ID, ");
            sb.Append(" sd.CREATE_DTM, ");
            sb.Append(" sd.CREATE_USER, ");
            sb.Append(" sd.MODI_DTM, ");
            sb.Append(" sd.MODI_USER, ");
            sb.Append(" TOTAL_AMOUNT, ");
            sb.Append(" BEFORE_TAX, ");
            sb.Append(" sd.TAXABLE ,");
            sb.Append(" sd.TAXRATE, ");
            sb.Append(" INV_TYPE ");
            sb.Append(" from  ");
            sb.Append("  sale_detail sd join product p  ");
            sb.Append(" on SD.PRODNO=p.PRODNO ");
            sb.AppendFormat(" where posuuid_master = {0} ", OracleDBUtil.SqlStr(POSUUID_MASTER));

            cmd = new OracleCommand(StringUtil.CStr(sb), objTx.Connection, objTx);
            da = new OracleDataAdapter(cmd);
            da.Fill(dtINV);
            #endregion

            //一般交易
            if (StringUtil.CStr(head.Rows[0]["sale_type"]) == "2")
            {
                //代收
                #region 判斷是否要開發票或者收據
                //System.Text.StringBuilder invProdLis = new System.Text.StringBuilder();
                List<string> invProdLis = new List<string>();
                DataTable dtInvProd = facade.getInvoiceProduct();

                if (dtInvProd != null && dtInvProd.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtInvProd.Rows)
                    {
                        if (dr[0] != null && StringUtil.CStr(dr[0]) != "")
                            invProdLis.Add(StringUtil.CStr(dr[0]));
                    }
                }

                //產生發票
                foreach (DataRow row in dtINV.Rows)
                {
                    if (invProdLis.Contains(StringUtil.CStr(row["PRODNO"])))
                    {
                        row.BeginEdit();
                        row["INV_TYPE"] = "1";
                        row.EndEdit();
                    }
                    else
                    {
                        row.BeginEdit();
                        row["INV_TYPE"] = "3";
                        row.EndEdit();
                    }
                }
                #endregion
            }
        }
        else
        {
            throw new Exception("查無資料");
        }




        int saleTotalAmt = 0;

        if (head.Rows[0]["SALE_TOTAL_AMOUNT"] != null && StringUtil.CStr(head.Rows[0]["SALE_TOTAL_AMOUNT"]) != ""
            && NumberUtil.IsNumeric(StringUtil.CStr(head.Rows[0]["SALE_TOTAL_AMOUNT"])))
            saleTotalAmt = int.Parse(StringUtil.CStr(head.Rows[0]["SALE_TOTAL_AMOUNT"]));

        if (saleTotalAmt <= 0)
        {
            RECDT = dtINV.Select(" 1 = 1 ");
            facade.InsertReceipt(head, RECDT, objTx);                                            //收據
        }
        else
        {
            //發票 應稅 TAXABLE = 'Y' and TAXRATE != 0
            INV_TAX_DRA = dtINV.Select(" INV_TYPE = '1' and TAXABLE = 'Y' and TAXRATE <> 0 ");
            //發票 應稅 0 應稅但是稅率0 TAXABLE = 'Y' and TAXRATE = 0
            INV_TAX_DRA_ZERO = dtINV.Select(" INV_TYPE = '1' and TAXABLE = 'Y' and TAXRATE = 0");
            //收據 免稅 TAXABLE = 'N'
            INV_NO_TAX_DRA = dtINV.Select(" INV_TYPE = '1' and TAXABLE = 'N'");

            string HOST_ID = StringUtil.CStr(head.Rows[0]["HOST_ID"]);
            RECDT = dtINV.Select(" INV_TYPE = '3' ");                                              //收據

            facade.InsertInvoice(head, INV_TAX_DRA, "1", objTx, HOST_ID);
            facade.InsertInvoice(head, INV_TAX_DRA_ZERO, "2", objTx, HOST_ID);
            facade.InsertInvoice(head, INV_NO_TAX_DRA, "3", objTx, HOST_ID);
            facade.InsertReceipt(head, RECDT, objTx);

            sb = new System.Text.StringBuilder();
            sb.Append(" select  ");
            sb.Append(" INVOICE_NO  ");
            sb.Append(" from INVOICE_HEAD  ");
            sb.Append(" where  ");
            sb.Append(" posuuid_master =   " + OracleDBUtil.SqlStr(POSUUID_MASTER));
            DataTable dtInvoice = new DataTable();
            cmd = new OracleCommand(StringUtil.CStr(sb), objTx.Connection, objTx);
            da = new OracleDataAdapter(cmd);
            da.Fill(dtInvoice);
            foreach (DataRow row in dtInvoice.Rows)
            {
                INVOICE_NO += StringUtil.CStr(row[0]) + ",";
            }
            if (INVOICE_NO.Length > 0) INVOICE_NO = INVOICE_NO.Substring(0, INVOICE_NO.Length - 1);
        }

        return INVOICE_NO;
    }
    #endregion

    #region Imei_log
    private void Imei_log(string POSUUID_MASTER, OracleTransaction TranObj)
    {
        string strMessage = new SAL01_Facade().IMEISale_Log(TranObj, POSUUID_MASTER, logMsg.MODI_USER);
        string[] strMsg = strMessage.Split('|');
        if (strMsg[0] != "000") //表示失敗
        {
            throw new Exception(strMsg[1]);
        }
    }
    #endregion

    #region 特殊抱怨折扣處理
    /// <summary>
    /// 特殊抱怨折扣處理
    /// </summary>
    /// <param name="POSUUID_MASTER"></param>
    /// <param name="trans"></param>
    private void StoreSpecialDIS(string POSUUID_MASTER, OracleTransaction trans)
    {
        //讀取特殊折扣

        string sqlstr = string.Format("select count(*) from sale_detail where posuuid_master={0} and item_type='6'", OracleDBUtil.SqlStr(POSUUID_MASTER));
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        object o = cmd.ExecuteScalar();
        if (o == null || Convert.ToInt32(o) == 0)
            return;


        sqlstr = "select TOTAL_AMOUNT, SH_DISCOUNT_DESC from SALE_DETAIL where item_type =6 and  posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
        DataTable dt = new DataTable();
        cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        da.Fill(dt);

        foreach (DataRow row in dt.Rows)
        {
            string USED_AMOUNT = "0";
            decimal amount = Math.Abs(Convert.ToDecimal(row[0]));
            string useRoleType = "2";
            if (logMsg.ROLE_TYPE == "1")
                useRoleType = "1";
            else if (StringUtil.CStr(row[1]).IndexOf("使用店長權限") >= 0)
                useRoleType = "1";
            USED_AMOUNT = StringUtil.CStr(amount);//取得特殊抱怨折扣金額
            int AffectRow = 0;
            string strYYMM = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
            if (logMsg.ROLE_TYPE == "1" || logMsg.ROLE_TYPE == "2")   //當角色為1:店長或2:店員
            {
                AffectRow = new SAL01_Facade().UpdateStoreSpecialDIS(trans, logMsg.STORENO, strYYMM.Substring(0, 7), USED_AMOUNT, useRoleType);
                if (AffectRow == 0)
                {
                    throw new Exception("您已經沒有變更特殊抱怨折扣金額可用 !");
                }
            }
        }
    }
    #endregion

    #region 收據
    private string Collection_Receipt(string posuuid_master, string receiptname)
    {
        string return_url = "";
        List<List<string>> dir = new List<List<string>>();
        OracleConnection conn = null;
        OracleCommand cmd = null;
        OracleDataAdapter da = null;
        OracleDataReader dr = null;
        string trade_date = "";
        string store_no = "";
        string machice_id = "";
        string RECEIPT_NO = "";
        string total_amount = "";
        string barcode1 = "";
        string barcode2 = "";
        string barcode3 = "";
        string CARD_NO = "";
        string msisdn = "";
        string SALE_PERSON = "";
        string hg_card_no = "";
        string pay_mode1 = ""; //現金
        string pay_mode2 = ""; //信用卡
        string pay_mode7 = "";//HappyGo折抵
        string pay_mode8 = "";//找零金
        string source_type = "";
        string prodno = "";
        string sqlstr = "select sh.trade_date,SH.STORE_NO,SH.MACHINE_ID,RH.RECEIPT_NO,SH.SALE_PERSON,sh.hg_card_no from sale_head sh  join RECEIPT_HEAD rh on RH.POSUUID_MASTER = SH.POSUUID_MASTER where  sh.posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
        try
        {
            conn = OracleDBUtil.GetConnection();
            cmd = new OracleCommand(sqlstr, conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                trade_date = dr.IsDBNull(0) ? "" : dr.GetDateTime(0).ToString("yyyy-MM-dd");//StringUtil.CStr(dr[0]);
                store_no = StringUtil.CStr(dr[1]);
                machice_id = StringUtil.CStr(dr[2]);
                RECEIPT_NO = StringUtil.CStr(dr[3]);
                SALE_PERSON = StringUtil.CStr(dr[4]);
                hg_card_no = StringUtil.CStr(dr[5]);
            }
            dr.Read();


            //抓取detal
            sqlstr = "select barcode1,barcode2,barcode3,msisdn,total_amount,id,fun_id,CARD_NO,prodno,source_type from sale_detail where posuuid_master = " + OracleDBUtil.SqlStr(posuuid_master);
            DataTable detail = new DataTable();
            cmd = new OracleCommand(sqlstr, conn);
            da = new OracleDataAdapter(cmd);
            da.Fill(detail);
            foreach (DataRow row in detail.Rows)
            {
                string sale_detail_id = StringUtil.CStr(row["id"]);
                barcode1 = StringUtil.CStr(row["barcode1"]);
                barcode2 = StringUtil.CStr(row["barcode2"]);
                barcode3 = StringUtil.CStr(row["barcode3"]);
                CARD_NO = StringUtil.CStr(row["CARD_NO"]);
                msisdn = StringUtil.CStr(row["msisdn"]);
                total_amount = StringUtil.CStr(row["total_amount"]);
                source_type = StringUtil.CStr(row["source_type"]);
                prodno = StringUtil.CStr(row["prodno"]);
                string fun_id = StringUtil.CStr(row["fun_id"]);

                //抓出拆分金額
                //抓現金
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 1 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode1 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //抓信用卡
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id in(2,3,4) and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode2 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //找零金
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 8 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode8 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());

                //happyGo
                sqlstr = "select sum(amount) from bill_dispatch where pay_mode_id = 7 and sale_detail_id = " + OracleDBUtil.SqlStr(sale_detail_id);
                cmd = new OracleCommand(sqlstr, conn);
                pay_mode7 = cmd.ExecuteScalar() == null ? "0" : StringUtil.CStr(cmd.ExecuteScalar());
                //1: 遠傳帳單
                //2: 和信帳單
                //3: Seednet帳單
                //4: 遠通帳單(有單)
                //5: 遠通帳單(無單)
                //6: 速博帳單
                //判斷類型
                List<string> list = new List<string>();
                if (source_type == "3" && prodno != "700300010")
                {
                    #region 代收
                    switch (fun_id)
                    {
                        case "1":
                            //FET

                            list.Add("4");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("遠傳電信帳單");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(msisdn);
                            list.Add(pay_mode1);
                            list.Add(pay_mode2);
                            list.Add(pay_mode7);
                            list.Add("0");
                            list.Add(hg_card_no);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "2"://KGT

                            list.Add("5");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("和信電信帳單");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(pay_mode1);
                            list.Add(pay_mode2);
                            list.Add(pay_mode7);
                            list.Add(pay_mode8);
                            list.Add(hg_card_no);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "3"://Seednet
                            list.Add("8");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("Seednet帳單");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(barcode2);
                            list.Add(barcode3);
                            list.Add(pay_mode1);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "4"://ETC(有單
                            list.Add("3");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("ETC 代收");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(barcode2);
                            list.Add(barcode3);
                            list.Add(pay_mode1);
                            list.Add(barcode2);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "5"://ETC(無單
                            list.Add("3");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("ETC 代收");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(barcode2);
                            list.Add(barcode3);
                            list.Add(pay_mode1);
                            list.Add(pay_mode2);
                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        case "6"://NCIC
                            list.Add("6");
                            list.Add(trade_date);
                            list.Add(store_no);
                            list.Add(machice_id);
                            list.Add(RECEIPT_NO);
                            list.Add("速博電信帳單");
                            list.Add(total_amount);
                            list.Add(barcode1);
                            list.Add(barcode2);
                            list.Add(barcode3);
                            list.Add(pay_mode1);

                            list.Add(SALE_PERSON);
                            dir.Add(list);
                            break;
                        default:

                            break;
                    }
                    #endregion
                }
                else
                {

                    if (TSAL01_Facade.CheckGUARANTEE(prodno))
                    {
                        list.Add("2");
                        list.Add(trade_date);
                        list.Add(store_no);
                        list.Add(machice_id);
                        list.Add(RECEIPT_NO);
                        list.Add("保證金");
                        list.Add(total_amount);
                        list.Add(msisdn);
                        list.Add(SALE_PERSON);
                        dir.Add(list);
                    }

                }

            }

            string fileName = "";
            if (dir.Count > 0)
            {
                SAL01_Facade facade = new SAL01_Facade();
                string filePath = facade.getUploadPath(posuuid_master);
                IRClass pri = new PriReceipt();
                fileName = pri.Print("M", null, dir, receiptname);

                if (fileName == null || string.IsNullOrEmpty(fileName))
                {
                    throw new Exception("列印收據失敗，請重印收據!!");
                }
                else
                {
                    return_url = Request.ApplicationPath + "/Downloads/Receipt/" + fileName;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
        return return_url;
    }

    #endregion



    /// <summary>
    /// 代收處理
    /// </summary>
    /// <param name="POSUUID_MASTER"></param>
    private void collection(string POSUUID_MASTER)
    {
        OracleConnection conn = null;

        string bill_dispatch_url = "";
        string postString = "";//XML結構                                                                                                                                                                                                                           
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(" select barcode1,fun_id from SALE_DETAIL where  source_type = 3 and posuuid_master =" + OracleDBUtil.SqlStr(POSUUID_MASTER));
        DataTable dt = OracleDBUtil.Query_Data(StringUtil.CStr(sb));
        try
        {
            conn = OracleDBUtil.GetConnection();

            //1: 遠傳帳單                                                                                                                                                                                                                                          
            //2: 和信帳單                                                                                                                                                                                                                                          
            //3: Seednet帳單                                                                                                                                                                                                                                       
            //4: 遠通帳單(有單)                                                                                                                                                                                                                                    
            //5: 遠通帳單(無單)                                                                                                                                                                                                                                    
            //6: 速博帳單                                                                                                                                                                                                                                          
            foreach (DataRow row in dt.Rows)
            {
                string barcode1 = StringUtil.CStr(row["barcode1"]);
                string fun_id = StringUtil.CStr(row["fun_id"]);
                //FET                                                                                                                                                                                                                                              
                #region 判斷代收類型

                switch (fun_id)
                {
                    case "1":

                        FET_BILLING_INSPAYMENTTRX fet = new FET_BILLING_INSPAYMENTTRX();
                        postString = fet.DOMain(POSUUID_MASTER);
                        bill_dispatch_url = get_bill_dispatch_url("FET") + "/posapp/InsPaymentTrx";
                        //bill_dispatch_url = "http://192.168.8.223/HRS_WS/Default2.aspx";                                                                                                                                                                         


                        break;
                    case "3":
                        SEEDNET_BILLING.Instant_SEEDNET_BILLING seed = new SEEDNET_BILLING.Instant_SEEDNET_BILLING();
                        postString = seed.DOMain(POSUUID_MASTER);
                        bill_dispatch_url = get_bill_dispatch_url("SEEDNET");
                        //bill_dispatch_url = "http://localhost:51009/FET_WEB_POS_v2/Default2.aspx";                                                                                                                                                               
                        Logger.Log.Info("FET XML:" + bill_dispatch_url + "?" + postString);

                        break;
                }

                #endregion


                if (fun_id == "3")
                {
                    if (!string.IsNullOrEmpty(postString))
                    {
                        byte[] postData = Encoding.Default.GetBytes(postString);
                        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(bill_dispatch_url);
                        req.Method = "POST";
                        req.ContentType = "application/x-www-form-urlencoded";
                        req.ContentLength = postData.Length;
                        using (System.IO.Stream reqStream = req.GetRequestStream())
                        {
                            reqStream.Write(postData, 0, postData.Length);
                        }

                        string responseString = "";
                        using (WebResponse wr = req.GetResponse())
                        {
                            System.IO.Stream strm = wr.GetResponseStream();
                            System.IO.StreamReader sr = new System.IO.StreamReader(strm);
                            responseString = sr.ReadToEnd();
                            Logger.Log.Info("SEEDNET Response XML:" + responseString);
                        }
                        string status = "";
                        string message = "";
                        string send_flag = "";
                        string sqlstr = "";
                        OracleCommand cmd = new OracleCommand();
                        if (!string.IsNullOrEmpty(responseString))
                        {
                            System.IO.StringReader sr = new System.IO.StringReader(responseString);
                            DataSet ds = new DataSet();
                            ds.ReadXml(sr);

                            DataTable xmldt = ds.Tables["Responce"];
                            foreach (DataRow xmldr in xmldt.Rows)
                            {
                                if (xmldr["StatusCode"].ToString() == "00")
                                {
                                    send_flag = "S";
                                }
                                else
                                {
                                    send_flag = "X";
                                }
                            }

                        }
                        sqlstr = "update SEEDNET_BILLING_M set SEND_STATUS = :SEND_STATUS,SEND_DTM = SYSDATE ";
                        sqlstr += " WHERE SEEDNET_BFM_ID IN (SELECT SEEDNET_BFM_ID FROM SEEDNET_BILLING_D WHERE BILL_DISPATCH_ID ";
                        sqlstr += " IN(SELECT BILL_DISPATCH_ID FROM BILL_DISPATCH WHERE POSUUID_MASTER =:posuuid_master))";
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.Parameters.Add(":SEND_STATUS", OracleType.NVarChar).Value = send_flag;
                        cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (fun_id == "1")
                {
                    if (!string.IsNullOrEmpty(postString))
                    {
                        string status = "";
                        string message = "";
                        string send_flag = "";
                        string sqlstr = "";
                        OracleCommand cmd = new OracleCommand();
                        string[] strpost = postString.Split('|');
                        foreach (string text in strpost)
                        {
                            if (text != "")
                            {
                                Logger.Log.Info("FET XML:" + bill_dispatch_url + "?" + text);
                                string postString1 = "req=" + text;
                                byte[] postData = Encoding.Default.GetBytes(postString1);
                                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(bill_dispatch_url);
                                req.Method = "POST";
                                req.ContentType = "application/x-www-form-urlencoded";
                                req.ContentLength = postData.Length;
                                using (System.IO.Stream reqStream = req.GetRequestStream())
                                {
                                    reqStream.Write(postData, 0, postData.Length);
                                }

                                string responseString = "";
                                using (WebResponse wr = req.GetResponse())
                                {
                                    System.IO.Stream strm = wr.GetResponseStream();
                                    System.IO.StreamReader sr = new System.IO.StreamReader(strm);
                                    responseString = sr.ReadToEnd();
                                    Logger.Log.Info("FET Response XML:" + responseString);
                                }

                                if (!string.IsNullOrEmpty(responseString))
                                {
                                    System.IO.StringReader sr = new System.IO.StringReader(responseString);
                                    DataSet ds = new DataSet();
                                    ds.ReadXml(sr);

                                    DataTable xmldt = ds.Tables["fet-pos-pay-create-res"];
                                    foreach (DataRow xmldr in xmldt.Rows)
                                    {
                                        if (xmldr["Process-Status"].ToString() == "1" && send_flag != "X")
                                        {
                                            send_flag = "S";
                                        }
                                        else
                                        {
                                            message += xmldr["Error-Message"].ToString();
                                            send_flag = "X";
                                        }
                                    }
                                }
                            }
                        }
                        sqlstr = "update FET_BILLING_FILE set send_flag = :send_flag,FET_PROCESS_STATUS = :FET_PROCESS_STATUS,ERROR_MESSAGE=:ERROR_MESSAGE where pos_key in (select posuuid_detail from sale_detail where posuuid_master = :posuuid_master)";
                        cmd = new OracleCommand(sqlstr, conn);
                        cmd.Parameters.Add(":send_flag", OracleType.NVarChar).Value = send_flag;
                        cmd.Parameters.Add(":FET_PROCESS_STATUS", OracleType.NVarChar).Value = status;
                        cmd.Parameters.Add(":ERROR_MESSAGE", OracleType.NVarChar).Value = message;
                        cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;
                        cmd.ExecuteNonQuery();
                    }
                }




                //if (!string.IsNullOrEmpty(postString))                                                                                                                                                                                                           
                //{                                                                                                                                                                                                                                                
                //    byte[] postData = Encoding.Default.GetBytes(postString);                                                                                                                                                                                     
                //    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(bill_dispatch_url);                                                                                                                                                               
                //    req.Method = "POST";                                                                                                                                                                                                                         
                //    req.ContentType = "application/x-www-form-urlencoded";                                                                                                                                                                                       
                //    req.ContentLength = postData.Length;                                                                                                                                                                                                         
                //    using (System.IO.Stream reqStream = req.GetRequestStream())                                                                                                                                                                                  
                //    {                                                                                                                                                                                                                                            
                //        reqStream.Write(postData, 0, postData.Length);                                                                                                                                                                                           
                //    }                                                                                                                                                                                                                                            

                //    string responseString = "";                                                                                                                                                                                                                  
                //    using (WebResponse wr = req.GetResponse())                                                                                                                                                                                                   
                //    {                                                                                                                                                                                                                                            
                //        System.IO.Stream strm = wr.GetResponseStream();                                                                                                                                                                                          
                //        System.IO.StreamReader sr = new System.IO.StreamReader(strm);                                                                                                                                                                            
                //        responseString = sr.ReadToEnd();                                                                                                                                                                                                         
                //        Logger.Log.Info("FET Response XML:" + responseString);                                                                                                                                                                                   
                //    }                                                                                                                                                                                                                                            
                //    string status = "";                                                                                                                                                                                                                          
                //    string message = "";                                                                                                                                                                                                                         
                //    string send_flag = "";                                                                                                                                                                                                                       
                //    string sqlstr = "";                                                                                                                                                                                                                          
                //    OracleCommand cmd = new OracleCommand();                                                                                                                                                                                                     
                //    switch (fun_id)                                                                                                                                                                                                                              
                //    {                                                                                                                                                                                                                                            
                //        case "1":                                                                                                                                                                                                                                


                //            if (!string.IsNullOrEmpty(responseString))                                                                                                                                                                                           
                //            {                                                                                                                                                                                                                                    
                //                using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(responseString))                                                                                                                                                
                //                {                                                                                                                                                                                                                                
                //                    reader.ReadToFollowing("fet-pos-pay-create-res");                                                                                                                                                                            
                //                    reader.ReadToFollowing("Process-Status");                                                                                                                                                                                    
                //                    status = reader.Value;                                                                                                                                                                                                       

                //                    if (status == "0")                                                                                                                                                                                                           
                //                    {                                                                                                                                                                                                                            
                //                        reader.ReadToFollowing("Error-Message");                                                                                                                                                                                 
                //                        message = reader.Value;                                                                                                                                                                                                  
                //                        send_flag = "X";                                                                                                                                                                                                         
                //                    }                                                                                                                                                                                                                            
                //                    else                                                                                                                                                                                                                         
                //                    {                                                                                                                                                                                                                            
                //                        send_flag = "S";                                                                                                                                                                                                         
                //                    }                                                                                                                                                                                                                            
                //                }                                                                                                                                                                                                                                
                //            }                                                                                                                                                                                                                                    

                //            sqlstr = "update FET_BILLING_FILE set send_flag = :send_flag,FET_PROCESS_STATUS = :FET_PROCESS_STATUS,ERROR_MESSAGE=:ERROR_MESSAGE where pos_key in (select posuuid_detail from sale_detail where posuuid_master = :posuuid_master)";
                //            cmd = new OracleCommand(sqlstr, conn);                                                                                                                                                                                               
                //            cmd.Parameters.Add(":send_flag", OracleType.NVarChar).Value = send_flag;                                                                                                                                                             
                //            cmd.Parameters.Add(":FET_PROCESS_STATUS", OracleType.NVarChar).Value = status;                                                                                                                                                       
                //            cmd.Parameters.Add(":ERROR_MESSAGE", OracleType.NVarChar).Value = message;                                                                                                                                                           
                //            cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;                                                                                                                                                   
                //            cmd.ExecuteNonQuery();                                                                                                                                                                                                               
                //            break;                                                                                                                                                                                                                               
                //        case "3":                                                                                                                                                                                                                                
                //            if (!string.IsNullOrEmpty(responseString))                                                                                                                                                                                           
                //            {                                                                                                                                                                                                                                    
                //                using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(responseString))                                                                                                                                                
                //                {                                                                                                                                                                                                                                
                //                    reader.ReadToFollowing("Responce");                                                                                                                                                                                          
                //                    reader.ReadToFollowing("StatusCode");                                                                                                                                                                                        
                //                    status = reader.Value;                                                                                                                                                                                                       
                //                    if (status == "00")                                                                                                                                                                                                          
                //                    {                                                                                                                                                                                                                            
                //                        send_flag = "X";                                                                                                                                                                                                         
                //                    }                                                                                                                                                                                                                            
                //                    else                                                                                                                                                                                                                         
                //                    {                                                                                                                                                                                                                            
                //                        send_flag = "S";                                                                                                                                                                                                         
                //                    }                                                                                                                                                                                                                            
                //                }                                                                                                                                                                                                                                
                //            }                                                                                                                                                                                                                                    
                //            sqlstr = "update SEEDNET_BILLING_M set SEND_STATUS = :SEND_STATUS,SEND_DTM = SYSDATE ";                                                                                                                                              
                //            sqlstr += " where WHERE SEEDNET_BFM_ID IN (SELECT SEEDNET_BFM_ID FROM SEEDNET_BILLING_D WHERE BILL_DISPATCH_ID ";                                                                                                                    
                //            sqlstr += " IN(SELECT BILL_DISPATCH_ID FROM BILL_DISPATCH WHERE POSUUID_MASTER =:posuuid_master))";                                                                                                                                  
                //            cmd = new OracleCommand(sqlstr, conn);                                                                                                                                                                                               
                //            cmd.Parameters.Add(":SEND_STATUS", OracleType.NVarChar).Value = send_flag;                                                                                                                                                           
                //            cmd.Parameters.Add(":posuuid_master", OracleType.NVarChar).Value = POSUUID_MASTER;                                                                                                                                                   
                //            cmd.ExecuteNonQuery();                                                                                                                                                                                                               
                //            break;                                                                                                                                                                                                                               
                //    }                                                                                                                                                                                                                                            
                //}                                                                                                                                                                                                                                                
            }
        }
        catch (Exception ex)
        {
            Logger.Log.Error("TSAL01 Collection Error : " + ex.Message, ex);
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
    }

    /// <summary>
    /// 產生銷帳連結
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string get_bill_dispatch_url(string type)
    {
        string result = "";
        OracleConnection conn = null;
        OracleCommand cmd = null;
        try
        {
            string key = string.Format("{0}_BILL_DISPATCH_URL", type);
            conn = OracleDBUtil.GetConnection();
            string sqlstr = "select PARA_VALUE from sys_para where para_key = " + OracleDBUtil.SqlStr(key);
            cmd = new OracleCommand(sqlstr, conn);
            result = cmd.ExecuteScalar() == null ? "" : StringUtil.CStr(cmd.ExecuteScalar());

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
        return result;
    }


    #region 計算明細稅額
    private void CalDetailTax(double SALE_TOTAL_TAX, double SALE_AFTER_TOTAL_AMOUNT, double DISCOUNT_TOTAL_TAX, double DISCOUNT_AFTER_TOTAL_AMOUNT,
                             ref DataRow[] drsSaleDetail, ref DataRow[] drsSaleDiscount, ref DataRow[] drsDiscount)
    {
        double tempTotal_Tax = SALE_TOTAL_TAX - DISCOUNT_TOTAL_TAX;
        double DETAIL_TOTALE_AMOUNT = 0;
        double DETAIL_TAX = 0;
        double DETAIL_BEFORE_TAX = 0;
        for (int i = 0; i < drsSaleDiscount.Length; i++)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
        {
            DataRow dr = drsSaleDiscount[i];
            if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                DETAIL_TOTALE_AMOUNT = int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
            else
                DETAIL_TOTALE_AMOUNT = 0;
            if (DISCOUNT_AFTER_TOTAL_AMOUNT != 0)
                DETAIL_TAX = (int)Math.Round((double)(DISCOUNT_TOTAL_TAX * (DETAIL_TOTALE_AMOUNT / DISCOUNT_AFTER_TOTAL_AMOUNT)));// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)                    
            else
                DETAIL_TAX = 0;
            DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - DETAIL_TAX; //Y1 = X - Y3 - Y2 = 57 - 5 - 24 = 28
            dr["TAX"] = DETAIL_TAX;
            dr["BEFORE_TAX"] = DETAIL_BEFORE_TAX;
            dr["TOTAL_AMOUNT"] = DETAIL_TOTALE_AMOUNT;
        }

        for (int i = 0; i < drsDiscount.Length; i++)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
        {
            DataRow dr = drsDiscount[i];
            if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                DETAIL_TOTALE_AMOUNT = int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
            else
                DETAIL_TOTALE_AMOUNT = 0;
            if (DISCOUNT_AFTER_TOTAL_AMOUNT != 0)
                DETAIL_TAX = (int)Math.Round((double)(DISCOUNT_TOTAL_TAX * (DETAIL_TOTALE_AMOUNT / DISCOUNT_AFTER_TOTAL_AMOUNT)));// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)                    
            else
                DETAIL_TAX = 0;
            DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - DETAIL_TAX; //Y1 = X - Y3 - Y2 = 57 - 5 - 24 = 28
            dr["TAX"] = DETAIL_TAX;
            dr["BEFORE_TAX"] = DETAIL_BEFORE_TAX;
            dr["TOTAL_AMOUNT"] = DETAIL_TOTALE_AMOUNT;
        }

        for (int i = 0; i < drsSaleDetail.Length; i++) //未結轉入才有的資料 ITEM_TYPE = 5;
        {
            DataRow dr = drsSaleDetail[i];
            if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null
                && StringUtil.CStr(dr["TOTAL_AMOUNT"]) != "" && NumberUtil.IsNumeric(StringUtil.CStr(dr["TOTAL_AMOUNT"])))
                DETAIL_TOTALE_AMOUNT = int.Parse(StringUtil.CStr(dr["TOTAL_AMOUNT"]));
            else
                DETAIL_TOTALE_AMOUNT = 0;
            if (SALE_AFTER_TOTAL_AMOUNT != 0)
                DETAIL_TAX = (int)Math.Round((double)((SALE_TOTAL_TAX - DISCOUNT_TOTAL_TAX) * (DETAIL_TOTALE_AMOUNT / (SALE_AFTER_TOTAL_AMOUNT - DISCOUNT_AFTER_TOTAL_AMOUNT))));// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)                    
            else
                DETAIL_TAX = 0;
            DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - DETAIL_TAX;

            if (i + 1 == drsSaleDetail.Length) //最後一筆 
            {
                DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - tempTotal_Tax; //Y1 = X - Y3 - Y2 = 57 - 5 - 24 = 28
                DETAIL_TAX = tempTotal_Tax;
            }

            dr["TAX"] = DETAIL_TAX;
            dr["BEFORE_TAX"] = DETAIL_BEFORE_TAX;
            dr["TOTAL_AMOUNT"] = DETAIL_TOTALE_AMOUNT;
            tempTotal_Tax -= DETAIL_TAX;
        }
    }
    #endregion

    #region 計算明細稅 CalDetailTax
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Detail"></param>
    /// <returns></returns>
    private DataTable CalDetailTax(DataTable Detail, string posuuid_master, OracleTransaction trans)
    {


        //明細總價
        double DETAIL_TOTALE_AMOUNT = 0;
        //明細總稅
        double DETAIL_TOTAL_TAX = 0;
        //明細稅
        double DETAIL_TAX = 0;

        double DETAIL_AMOUNT = 0;
        //
        double DETAIL_BEFORE_TOTAL_AMOUNT = 0;
        //
        double DETAIL_BEFORE_AMOUNT = 0;


        //單價計算
        foreach (DataRow row in Detail.Rows)
        {
            string INV_TYPE = StringUtil.CStr(row["INV_TYPE"]);
            string TAXABLE = StringUtil.CStr(row["TAXABLE"]);
            string TAXRATE = StringUtil.CStr(row["TAXRATE"]);
            string TOTAL_AMOUNT = StringUtil.CStr(row["TOTAL_AMOUNT"]);
            if (INV_TYPE == "1" && TAXABLE == "Y" && TAXRATE != "0")
            {
                if (TOTAL_AMOUNT != "" && NumberUtil.IsNumeric(TOTAL_AMOUNT))
                    DETAIL_AMOUNT = int.Parse(TOTAL_AMOUNT);
                else
                    DETAIL_AMOUNT = 0;
                //稅前 = 單價 * 100 / 105
                DETAIL_BEFORE_AMOUNT = (int)Math.Round((double)(DETAIL_AMOUNT * 100 / 105), MidpointRounding.AwayFromZero);// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)                    
                DETAIL_TAX = DETAIL_AMOUNT - DETAIL_BEFORE_AMOUNT;

                DETAIL_BEFORE_TOTAL_AMOUNT += DETAIL_BEFORE_AMOUNT;
                DETAIL_TOTAL_TAX += DETAIL_TAX;
                DETAIL_TOTALE_AMOUNT += DETAIL_AMOUNT;
                row.BeginEdit();
                row["TAX"] = DETAIL_TAX;
                row["BEFORE_TAX"] = DETAIL_BEFORE_AMOUNT;
                row.EndEdit();
            }
        }

        double SALE_BEFORE_TOTAL_TAX = (int)Math.Round((double)(DETAIL_TOTALE_AMOUNT * 100 / 105), MidpointRounding.AwayFromZero);
        double SALE_TOTAL_TAX = DETAIL_TOTALE_AMOUNT - SALE_BEFORE_TOTAL_TAX;
        //除稅
        if (Detail.Rows.Count > 0)
        {
            if (DETAIL_BEFORE_TOTAL_AMOUNT != SALE_BEFORE_TOTAL_TAX)
            {
                double before_amount = SALE_BEFORE_TOTAL_TAX - DETAIL_BEFORE_TOTAL_AMOUNT;
                //抓出金額最高的來做除稅

                DataRow row = Detail.Rows[Detail.Rows.Count - 1];
                row.BeginEdit();
                row["BEFORE_TAX"] = Convert.ToInt32(row["BEFORE_TAX"]) + before_amount;
                row.EndEdit();
            }
        }
        Detail.AcceptChanges();

        //回壓SALE_HEAD SALE_TOTAL_TAX ,SALE_BEFORE_TOTAL_TAX
        string sqlstr = "update SALE_HEAD set SALE_BEFORE_TAX = :SALE_BEFORE_TAX ,SALE_TAX = :SALE_TAX,INVOICE_TOTAL_AMOUNT=:INVOICE_TOTAL_AMOUNT where POSUUID_MASTER = :POSUUID_MASTER";
        OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        cmd.Parameters.Add(":SALE_BEFORE_TAX", OracleType.Number).Value = SALE_BEFORE_TOTAL_TAX;
        cmd.Parameters.Add(":SALE_TAX", OracleType.Number).Value = SALE_TOTAL_TAX;
        cmd.Parameters.Add(":INVOICE_TOTAL_AMOUNT", OracleType.Number).Value = DETAIL_TOTALE_AMOUNT;
        cmd.Parameters.Add(":POSUUID_MASTER", OracleType.NVarChar, 32).Value = posuuid_master;
        cmd.ExecuteNonQuery();


        return Detail;
    }
    #endregion


    #region 取消交易
    /// <summary>
    /// 取消交易
    /// </summary>
    /// <param name="POSUUID_MASTER"></param>
    /// <param name="POSUUID_DETAIL"></param>
    private void Delete(string POSUUID_MASTER, string POSUUID_DETAIL)
    {
        bool delOut = false;
        if (!string.IsNullOrEmpty(POSUUID_DETAIL))
        {
            delOut = true;
        }

        POSUUID_DETAIL = POSUUID_DETAIL.TrimEnd(';');
        string[] detailid = POSUUID_DETAIL.TrimEnd(';').Split(';');

        OracleConnection conn = null;
        OracleTransaction trans = null;
        DataTable detail_dt = new DataTable();
        SAL01_Facade Facade = new SAL01_Facade();
        TSAL01_Facade tFacade = new TSAL01_Facade();
        bool OrderCancel = true;
        string processFlag = "";
        try
        {
            conn = OracleDBUtil.GetConnection();
            trans = conn.BeginTransaction();
            if (POSUUID_MASTER != null && (!string.IsNullOrEmpty(POSUUID_MASTER)))
            {   //有交易表頭檔UID,刪除資料庫中交易資料
                Facade.invalidSaleIMEILog(POSUUID_MASTER);
                string sqlstr = "select * from SALE_DETAIL where posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
                OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(detail_dt);

                //判斷是否有線上儲值
                int s5 = detail_dt.Select("source_type = 5").Length;
                if (s5 > 0)
                {
                    trans.Rollback();
                    Response.Clear();
                    Response.ContentType = "text/plain";
                    Response.Write(string.Format("{{ RESULT : '0',ERROR :'{0}'}}", "線上儲值不得交易取消!"));
                    return;
                }

                int ret = tFacade.delSaleData(POSUUID_MASTER, trans);
                if (ret < 0)
                    OrderCancel = false;

                if (OrderCancel && delOut)
                {
                    foreach (string uuid in detailid)
                    {
                        //將TO_CLOSE_HEAD 更新為 取消中狀態, 並回填關連的POSUUID_MASTER
                        tFacade.UpdateUnCloseHead(uuid, POSUUID_MASTER, "3", logMsg.MODI_USER, trans);
                    }

                    string prePosuuidDetail = "";
                    string posuuitDetail = "";
                    DataTable dt = tFacade.getCancleTO_CLOSE_DATA(POSUUID_MASTER, trans);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["POSUUID_DETAIL"] != null && (!string.IsNullOrEmpty(StringUtil.CStr(dr["POSUUID_DETAIL"]))))
                            posuuitDetail = StringUtil.CStr(dr["POSUUID_DETAIL"]);

                        if (posuuitDetail != "" && posuuitDetail != prePosuuidDetail)
                        {
                            prePosuuidDetail = posuuitDetail;
                            ret = Facade.CancelOuterSystem(StringUtil.CStr(dr["POSUUID_DETAIL"]), StringUtil.CStr(dr["SERVICE_TYPE"]),
                                                            StringUtil.CStr(dr["SERVICE_SYS_ID"]), StringUtil.CStr(dr["BUNDLE_ID"]),
                                                            StringUtil.CStr(dr["STORE_NO"]), StringUtil.CStr(dr["SALE_PERSON"]),
                                                            StringUtil.CStr(dr["BARCODE1"]), StringUtil.CStr(dr["BARCODE2"]),
                                                            StringUtil.CStr(dr["BARCODE3"]), StringUtil.CStr(dr["AMOUNT"]));
                            if (ret == 0)
                            {
                                //取消交易,commit外部系統成功才刪除未結清單中資料
                                System.Text.StringBuilder posuuid_detailList = new System.Text.StringBuilder("");
                                posuuid_detailList.Append(OracleDBUtil.SqlStr(StringUtil.CStr(dr["POSUUID_DETAIL"]))).Append(",");
                                processFlag = "Delete To Close Data";
                                tFacade.delTO_CLOSE(posuuid_detailList, trans);
                            }
                            else
                            {
                                processFlag = "Insert Data Upload Log";
                                OrderCancel = true;
                                tFacade.InsertDataUploadLog(StringUtil.CStr(dr["POSUUID_DETAIL"]), trans);
                            }
                        }
                    }
                }

            }



            if (OrderCancel)
            {
                processFlag = "Get Sale No";
                string SALE_NO = "";
                try
                {
                    if (detail_dt.Rows.Count > 0) SALE_NO = detail_dt.Rows[0]["SALE_NO"] == null ? "" : StringUtil.CStr(detail_dt.Rows[0]["SALE_NO"]);

                    if (SALE_NO == "")
                        SALE_NO = string.Format(SerialNo.GenNo("SALE"), logMsg.STORENO, logMsg.MACHINE_ID);
                }
                catch //(Exception ex)
                {

                }
                trans.Commit();
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write(string.Format("{{ RESULT : '1',SALE_NO :'{0}'}}", SALE_NO));

            }
            else
            {
                trans.Rollback();
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write(string.Format("{{ RESULT : '0',ERROR :'{0}'}}", "交易取消失敗!"));
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            if (OrderCancel)
            {
                string message = "交易取消成功!但後續動作[" + processFlag + "]失敗";
                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write(string.Format("{{ RESULT : '0',ERROR :'{0}'}}", message));
            }
            else
            {
                string message = "交易取消失敗，錯誤訊息[" + ex.Message.Replace("'", "-").Replace("\"", " ") + "]!";

                Response.Clear();
                Response.ContentType = "text/plain";
                Response.Write(string.Format("{{ RESULT : '0',ERROR :'{0}'}}", message));
            }
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearAllPools();
        }
    }
    #endregion

    #region 作廢 CancelSale
    /// <summary>
    /// 作廢
    /// </summary>
    /// <param name="objTX"></param>
    /// <param name="masterId"></param>
    protected void CancelSale(OracleTransaction objTX, string masterId)
    {
        ////
        bool bCheckOut = true; //是否可作廢
        bool isCrossMonth = false;
        string posuuid_master = "";
        string sale_status = "";
        string saleno = "";
        OracleDataAdapter da = null;
        OracleCommand cmd = null;
        StringBuilder sb = new StringBuilder();
        //存取銷貨單
        DataTable dtValid = new SAL01_Facade().getSale_Head(masterId);
        DateTime transDate = DateTime.Now;
        if (dtValid != null && dtValid.Rows.Count > 0 && dtValid.Rows[0]["TRADE_DATE"] != null)
        {
            try
            {
                transDate = DateTime.Parse(StringUtil.CStr(dtValid.Rows[0]["TRADE_DATE"]).Trim());
            }
            catch (Exception)
            {

            }
        }

        DataTable Head = new DataTable();
        sb = new StringBuilder();
        sb.Append(" select * from SALE_HEAD where posuuid_master = " + OracleDBUtil.SqlStr(masterId));
        cmd = new OracleCommand(StringUtil.CStr(sb), objTX.Connection, objTX);
        da = new OracleDataAdapter(cmd);
        da.Fill(Head);
        if (Head.Rows[0]["POSUUID_MASTER"] != null)
        {
            posuuid_master = StringUtil.CStr(Head.Rows[0]["POSUUID_MASTER"]);
            sale_status = StringUtil.CStr(Head.Rows[0]["SALE_STATUS"]);
            saleno = StringUtil.CStr(Head.Rows[0]["SALE_NO"]);
        }

        DataTable Detail = new DataTable();
        sb = new StringBuilder();
        sb.Append(" select SD.*,P.ISSTOCK from SALE_DETAIL SD join PRODUCT P on SD.PRODNO=P.PRODNO  where sd.posuuid_master = " + OracleDBUtil.SqlStr(masterId));
        cmd = new OracleCommand(StringUtil.CStr(sb), objTX.Connection, objTX);
        da = new OracleDataAdapter(cmd);
        da.Fill(Detail);

        INVENTORY_Facade Inventory = new INVENTORY_Facade();
        //作廢退回庫存
        DataRow[] Sale_DetailRows4 = Detail.Select(" ITEM_TYPE IN ('1','2','3','7','8','9','10','13','14') ");
        if (bCheckOut && Sale_DetailRows4.Length > 0)
        {
            foreach (DataRow dr in Sale_DetailRows4)
            {
                string Code = "";
                string Message = "";
                string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();

                if (!string.IsNullOrEmpty(StringUtil.CStr(dr["PRODNO"])) && StringUtil.CStr(dr["ISSTOCK"]) == "1")
                {
                    try
                    {
                        Inventory.PK_INVENTORY_RETURN(objTX, "1", StringUtil.CStr(dr["PRODNO"]),
                           logMsg.STORENO, Stock, StringUtil.CStr(Head.Rows[0]["SALE_NO"]),
                           Convert.ToInt32(StringUtil.CStr(dr["QUANTITY"])), logMsg.MODI_USER, StringUtil.CStr(dr["ID"]), ref Code, ref Message);
                        if (Code != "000")
                        {
                            bCheckOut = false;
                            throw new Exception("作廢貨品退庫存失敗!!" + Message + "");
                        }
                    }
                    catch (Exception ex)
                    {
                        bCheckOut = false;

                        throw ex;
                    }
                }
            }
        }

        //IMEI_Log
        if (bCheckOut)
        {
            string strMessage = new SAL041_Facade().IMEIRETURN_Log(objTX, masterId, logMsg.MODI_USER);
            string[] strMsg = strMessage.Split('|');
            if (strMsg[0] != "000") //表示失敗
            {
                bCheckOut = false;

                throw new Exception("IMEI_LOG失敗, " + strMsg[1].Replace("'", "-").Replace("\"", " ") + "");
            }
        }

        if (transDate.Month != DateTime.Now.Month)
        {
            isCrossMonth = true;
        }

        //作廢發票/填入折讓單號
        if (bCheckOut)
        {
            int AffectRow = 0;
            string creditNoteNo = Store_SerialNo.GenNo("CN", logMsg.STORENO, logMsg.MACHINE_ID);
            AffectRow = new SAL03_Facade().invalidOldInvoice(objTX, masterId, transDate, creditNoteNo);
            if (AffectRow < 1)
            {
                bCheckOut = false;
                if (isCrossMonth)
                {

                    throw new Exception("開立折讓單失敗!!");
                }
                else
                {
                    throw new Exception("作廢發票失敗!!");
                }

            }
        }



        //作廢原交易
        if (bCheckOut)
        {
            int AffectRow = 0;
            string cancel_no = Advtek.Utility.SerialNo.GenNo("SC"); ;
            AffectRow = new SAL041_Facade().invalidOldTransactionPeriod(objTX, masterId, transDate, cancel_no, logMsg.MODI_USER); //lblCancelNo.Text
            if (AffectRow < 1)
            {
                bCheckOut = false;

                throw new Exception("作廢原交易失敗!!");

            }
        }



    }
    #endregion

    /// <summary>
    /// 其他服務
    /// </summary>
    /// <param name="posuuid_master"></param>
    private void OtherService(string posuuid_master)
    {
        string code = "";
        string message = "";
        string sale_type = TSAL01_Facade.get_sale_type_by_sale_head(posuuid_master);
        OracleConnection conn = null;
        OracleCommand cmd = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            if (sale_type == "1")
            {
                cmd = new OracleCommand("SP_SALE_DISCOUNT_DISPATCH_ONE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_POSUUID_MASTER", OracleType.VarChar, 32).Value = posuuid_master;
                cmd.Parameters.Add("O_RT_CODE", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("O_RT_MESSAGE", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                code = StringUtil.CStr(cmd.Parameters["O_RT_CODE"].Value);
                message = StringUtil.CStr(cmd.Parameters["O_RT_MESSAGE"].Value);


            }
            else if (sale_type == "2")
            {
                cmd = new OracleCommand("PK_BILL.PayModeSplitOne", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("I_MASTER_ID", OracleType.VarChar, 32).Value = posuuid_master;
                cmd.ExecuteNonQuery();

            }

            cmd = new OracleCommand("SP_SUPPLIER_OUT_DISPATCH", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("I_MASTER_ID", OracleType.VarChar, 32).Value = posuuid_master;
            cmd.Parameters.Add("O_RT_CODE", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("O_RT_MESSAGE", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            code = StringUtil.CStr(cmd.Parameters["O_RT_CODE"].Value);
            message = StringUtil.CStr(cmd.Parameters["O_RT_MESSAGE"].Value);
        }
        catch
        {
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            OracleConnection.ClearPool(conn);
        }
    }

}

