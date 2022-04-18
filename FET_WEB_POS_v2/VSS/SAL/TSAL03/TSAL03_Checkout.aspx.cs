using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using Newtonsoft.Json.Linq;
using FET.POS.Model.Facade.FacadeImpl;
using FET.POS.Model.Common;

public partial class VSS_SAL_TSAL03_TSAL03_Checkout : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		String json_str = Request.Form["json"];
		JObject o = JObject.Parse(json_str);
		string ORIGINAL_MASTER = (String)o["ORIGINAL_MASTER"];
		string POSUUID_MASTER = (String)o["POSUUID_MASTER"];
		string POSUUID_DETAIL = (String)o["POSUUID_DETAIL"];
		JArray items = (JArray)o["CHECKOUT"];
		string ITEM_TYPE = (String)o["ITEM_TYPE"];
		string CANCEL_TRANS = (String)o["CANCEL_TRANS"];
		string UNI_NO = (String)o["UNI_NO"];
		string UNI_TITLE = (String)o["UNI_TITLE"];
		string REMARK = (String)o["REMARK"];

		if (CANCEL_TRANS.Equals("1"))
		{
			//Delete(POSUUID_MASTER);
		}
		else
		{
			CheckOut(ORIGINAL_MASTER, POSUUID_MASTER, items, ITEM_TYPE, UNI_NO, UNI_TITLE, REMARK);
		}
	}


	private void CheckOut(string ORIGINAL_MASTER, string POSUUID_MASTER, JArray items, string item_type, string uni_no, string uni_title, string remark)
	{
		OracleConnection conn = null;
		OracleTransaction trans = null;
		try
		{
			conn = OracleDBUtil.GetConnection();
			trans = conn.BeginTransaction();
			// 作廢
			Hashtable ht = CancelSale(trans, ORIGINAL_MASTER, item_type);

			// 換貨銷售
			string SALE_NO = string.Format(SerialNo.GenNo("SALE"), logMsg.STORENO, logMsg.MACHINE_ID);
			UpdateSalesHead(trans, POSUUID_MASTER, SALE_NO, uni_no, uni_title, remark);
			InsertPaid_Detail(trans, items, POSUUID_MASTER);
			Imei_log(POSUUID_MASTER, trans);
			CalculationTax(POSUUID_MASTER, trans); //計算稅
			this.Inventory(trans, POSUUID_MASTER, SALE_NO);
			//產生發票
            string INVOICE_NO = SetInvoiceOrReceipt(POSUUID_MASTER, trans);

            //產生
            StoreSpecialDIS(POSUUID_MASTER, trans);
			trans.Commit();

            string url = PrintInventory(POSUUID_MASTER);
			Response.Clear();
			Response.ContentType = "text/plain";
            Response.Write(string.Format("{{ RESULT:'1', SALE_NO:'{0}',INVOICE_URL:'{1}',INVOICE_NO:'{2}' }}", SALE_NO, url, INVOICE_NO));
		}
		catch (Exception ex)
		{
			trans.Rollback();
			Response.Clear();
			Response.ContentType = "text/plain";
			Response.Write(string.Format("{{ RESULT : '0', ERROR:'{0}' }}", ex.Message.Replace("\n", "\\n")));
		}
		finally
		{
			if (conn.State == ConnectionState.Open) conn.Close();
			OracleConnection.ClearAllPools();
		}
	}
    
	private string PrintInventory(string POSUUID_MASTER)
    {
        string url = "";
        Receipt myReceipt = new Receipt();
        SAL01_Facade facade = new SAL01_Facade();
        string filePath = facade.getUploadPath(POSUUID_MASTER);
        if (!string.IsNullOrEmpty(filePath))
        {
            string fileName = myReceipt.generateReceipt(POSUUID_MASTER);
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

				if (!string.IsNullOrEmpty(dr["PRODNO"].ToString()) && dr["ISSTOCK"].ToString() == "1")
				{

					Inventory.PK_INVENTORY_SALE(objTx, "1", dr["PRODNO"].ToString(),
					   logMsg.STORENO, STOCK, SALE_NO,
					   Convert.ToInt32(dr["QUANTITY"].ToString()), logMsg.MODI_USER, dr["ID"].ToString(), ref Code, ref Message);
					if (Code != "000")
					{
						throw new Exception("換貨商品扣庫存失敗，請做銷售扣庫存動作!!");

					}

				}
			}
		}
	}

	public void InsertPaid_Detail(OracleTransaction objTX, JArray checkout, string POSUUID_MASTER)
	{

		try
		{
			foreach (object c in checkout)
			{
				JObject jo = (JObject)c;
				string PAID_AMOUNT = (string)jo["AMOUNT"];
				string PAID_MODE = (string)jo["TYPE"];
				string DESC = (string)jo["DESC"];
				string sqlstr = string.Format("INSERT INTO PAID_DETAIL(ID,PAID_MODE,PAID_AMOUNT,DESCRIPTION,POSUUID_MASTER,CREATE_DTM) values(pos_uuid(),{0},{1},'{2}','{3}',SYSDATE)", PAID_MODE, PAID_AMOUNT, DESC, POSUUID_MASTER);
				OracleCommand cmd = new OracleCommand(sqlstr, objTX.Connection, objTX);
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
		//更新其他
		cmd.ExecuteNonQuery();
	}

	public static void CalculationTax(string POSUUID_MASTER, OracleTransaction TransPOS)
	{
		//讀取商品
		string sqlstr = "select SD.ID,SD.PRODNO,SD.ITEM_TYPE,p.TAXRATE,p.TAXABLE,SD.QUANTITY,SD.UNIT_PRICE from SALE_DETAIL SD join PRODUCT P on SD.PRODNO=P.PRODNO where SD.POSUUID_MASTER=" + OracleDBUtil.SqlStr(POSUUID_MASTER);
		OracleCommand cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
		DataTable prodDt = new DataTable();
		OracleDataAdapter da = new OracleDataAdapter(cmd);
		da.Fill(prodDt);

		//增加欄位
		DataColumn col = new DataColumn("TAX");
		col.DataType = System.Type.GetType("System.Double");
		col.DefaultValue = 0;
		prodDt.Columns.Add(col);

		col = new DataColumn("BEFORE_TAX");
		col.DataType = System.Type.GetType("System.Double");
		col.DefaultValue = 0;
		prodDt.Columns.Add(col);

		col = new DataColumn("TOTAL_AMOUNT");
		col.DataType = System.Type.GetType("System.Double");
		col.DefaultValue = 0;
		prodDt.Columns.Add(col);

		int TAXRATE = 5; //照這算法TAXRATE 就沒作用了
		double SALE_AFTER_TOTAL_AMOUNT = 0;         ///銷售稅後總金額  含稅+不稅之應收總金額
		double SALE_AFTER_TAX_TOTAL_AMOUNT = 0;     ///銷售應課稅產品總金額 只有含稅之應收總金額
		double SALE_TOTAL_TAX = 0;                  ///總稅額   SALE_AFTER_TAX_TOTAL_AMOUNT * (5/105)
		double SALE_BEFORE_TOTAL_AMOUNT = 0;        ///銷售稅前金額 =  SALE_AFTER_TAX_TOTAL_AMOUNT - SALE_TOTAL_TAX         
		//double DISCOUNT_AFTER_TOTAL_AMOUNT = 0;     ///折扣稅後總金額
		//double DISCOUNT_AFTER_TAX_TOTAL_AMOUNT = 0; ///折扣應課稅產品總金額  
		//double DISCOUNT_TOTAL_TAX = 0;              ///總稅額   DISCOUNT_AFTER_TAX_TOTAL_AMOUNT * (5/105)
		//double DISCOUNT_BEFORE_TOTAL_AMOUNT = 0;    ///銷售稅前金額 =  DISCOUNT_AFTER_TAX_TOTAL_AMOUNT - DISCOUNT_TOTAL_TAX

		//商品計算
		foreach (DataRow row in prodDt.Rows)
		{
			Double TOTAL_AMOUNT = Convert.ToDouble(row["UNIT_PRICE"]) * Convert.ToDouble(row["QUANTITY"]);
			row.BeginEdit();
			row["TOTAL_AMOUNT"] = TOTAL_AMOUNT;
			row.EndEdit();
			row.AcceptChanges();
			SALE_AFTER_TOTAL_AMOUNT += Convert.ToDouble(TOTAL_AMOUNT);
			if (row["TAXABLE"].ToString() == "Y") //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅
				SALE_AFTER_TAX_TOTAL_AMOUNT += Convert.ToDouble(TOTAL_AMOUNT);


		}


		//銷售額
		double taxRate = 0;
		if (TAXRATE != -100)
			taxRate = Math.Round(TAXRATE / Convert.ToDouble(100 + TAXRATE), 2);

		SALE_TOTAL_TAX = Math.Round(SALE_AFTER_TAX_TOTAL_AMOUNT * taxRate);//四捨五入
		SALE_BEFORE_TOTAL_AMOUNT = SALE_AFTER_TOTAL_AMOUNT - SALE_TOTAL_TAX;


		//更新SALE_HEAD
		sqlstr = "update SALE_HEAD set SALE_TOTAL_AMOUNT = :SALE_TOTAL_AMOUNT,SALE_TAX = :SALE_TAX, SALE_BEFORE_TAX = :SALE_BEFORE_TAX where POSUUID_MASTER = :POSUUID_MASTER ";
		cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
		cmd.Parameters.Add(":SALE_TOTAL_AMOUNT", OracleType.Number).Value = SALE_AFTER_TAX_TOTAL_AMOUNT;
		cmd.Parameters.Add(":SALE_BEFORE_TAX", OracleType.Number).Value = SALE_BEFORE_TOTAL_AMOUNT;
		cmd.Parameters.Add(":SALE_TAX", OracleType.Number).Value = SALE_TOTAL_TAX;
		cmd.Parameters.Add(":POSUUID_MASTER", OracleType.NVarChar).Value = POSUUID_MASTER;
		cmd.ExecuteNonQuery();


		//處理明細
		double DETAIL_TOTALE_AMOUNT = 0,///稅前金額，商品、銷售折扣、店長折扣、Happy Go折抵由稅額計算規則算出
				   DETAIL_TAX = 0,          ///稅額，商品、銷售折扣、店長折扣、Happy Go折抵由稅額計算規則算出
				   DETAIL_BEFORE_TAX = 0;   ///稅後金額，商品、銷售折扣、店長折扣、Happy Go折抵QUANTITY*UNIT_PRICE
		//取出須算稅額的資料
		DataRow[] Sale_DetailRows = prodDt.Select("ITEM_TYPE IN('1','2','3') AND TAXABLE = 'Y'"); //Y:應稅N:免稅;Y:應稅，taxrate為 0，為零稅


		//銷售稅額
		if (SALE_AFTER_TAX_TOTAL_AMOUNT != 0)
		{
			sqlstr = "update SALE_DETAIL set TOTAL_AMOUNT = :TOTAL_AMOUNT ,TAX = :TAX, BEFORE_TAX= :BEFORE_TAX ,TAXABLE=:TAXABLE,TAXRATE = :TAXRATE where ID = :ID";
			cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
			cmd.Parameters.Add(":TOTAL_AMOUNT", OracleType.Number);
			cmd.Parameters.Add(":TAX", OracleType.Number);
			cmd.Parameters.Add(":BEFORE_TAX", OracleType.Number);
			cmd.Parameters.Add(":ID", OracleType.NVarChar);
			cmd.Parameters.Add(":TAXRATE", OracleType.Number);
			cmd.Parameters.Add(":TAXABLE", OracleType.NVarChar);
			double tempTotal_Tax = SALE_TOTAL_TAX; //x  
			for (int i = 0; i < Sale_DetailRows.Length; i++)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
			{
				DataRow dr = Sale_DetailRows[i];
				if (dr["TOTAL_AMOUNT"] != DBNull.Value && dr["TOTAL_AMOUNT"] != null)
					DETAIL_TOTALE_AMOUNT = Convert.ToDouble(dr["TOTAL_AMOUNT"]);
				else
					DETAIL_TOTALE_AMOUNT = 0;

				DETAIL_TAX = Math.Round(SALE_TOTAL_TAX * (DETAIL_TOTALE_AMOUNT / SALE_AFTER_TAX_TOTAL_AMOUNT));// [sale_detail].sale_tax= [sale_head].sale_tax * ([sale_detail].total_amount/X)    

				if (i + 1 == Sale_DetailRows.Length) //最後一筆     
				{
					DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - tempTotal_Tax;
					DETAIL_TAX = tempTotal_Tax;//最後一筆 稅額放入
				}
				else DETAIL_BEFORE_TAX = DETAIL_TOTALE_AMOUNT - DETAIL_TAX;
				dr["TAX"] = DETAIL_TAX;
				dr["BEFORE_TAX"] = DETAIL_BEFORE_TAX;
				dr["TOTAL_AMOUNT"] = DETAIL_TOTALE_AMOUNT;
				tempTotal_Tax -= DETAIL_TAX;


				//更新SALE_DETAIL

				cmd.Parameters[":TAX"].Value = DETAIL_TAX;
				cmd.Parameters[":BEFORE_TAX"].Value = DETAIL_BEFORE_TAX;
				cmd.Parameters[":TOTAL_AMOUNT"].Value = DETAIL_TOTALE_AMOUNT;
				cmd.Parameters[":ID"].Value = dr["ID"];
				cmd.Parameters[":TAXRATE"].Value = dr["TAXRATE"];
				cmd.Parameters[":TAXABLE"].Value = dr["TAXABLE"];
				cmd.ExecuteNonQuery();
			}
		}
		else
		{


			if (Sale_DetailRows != null && Sale_DetailRows.Length > 0)
			{


				for (int i = 0; i < Sale_DetailRows.Length; i++)//明細資料 含 ITME 1,2,3應收 和 5,6折扣類 不含5未結清單折扣 
				{
					DataRow dr = Sale_DetailRows[i];
					dr["TAX"] = 0;
					dr["BEFORE_TAX"] = dr["TOTAL_AMOUNT"];


					cmd.Parameters[":TAX"].Value = 0;
					cmd.Parameters[":BEFORE_TAX"].Value = dr["TOTAL_AMOUNT"];
					cmd.Parameters[":TOTAL_AMOUNT"].Value = dr["TOTAL_AMOUNT"];
					cmd.Parameters[":ID"].Value = dr["ID"];
					cmd.Parameters[":TAXRATE"].Value = dr["TAXRATE"];
					cmd.Parameters[":TAXABLE"].Value = dr["TAXABLE"];
					cmd.ExecuteNonQuery();
				}
			}
		}



	}

    private string SetInvoiceOrReceipt(string POSUUID_MASTER,OracleTransaction objTx)
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
        sb.Append(" SALE_TOTAL_AMOUNT, ");
        sb.Append(" POSUUID_MASTER, ");
        sb.Append(" '' as INVOICE_TYPE");
        sb.Append(" from  ");
        sb.Append(" sale_head ");
        sb.AppendFormat(" where posuuid_master = {0} ", OracleDBUtil.SqlStr(POSUUID_MASTER));



        cmd = new OracleCommand(sb.ToString(), objTx.Connection, objTx);
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
            sb.Append(" sd.TAXRATE ");
            sb.Append(" from  ");
            sb.Append("  sale_detail sd join product p  ");
            sb.Append(" on SD.PRODNO=p.PRODNO ");
            sb.AppendFormat(" where posuuid_master = {0} ", OracleDBUtil.SqlStr(POSUUID_MASTER));

            cmd = new OracleCommand(sb.ToString(), objTx.Connection, objTx);
            da = new OracleDataAdapter(cmd);
            da.Fill(dtINV);
            #endregion

            #region 判斷是否要開發票或者收據
            System.Text.StringBuilder invProdLis = new System.Text.StringBuilder();
            DataTable dtInvProd = facade.getInvoiceProduct();
            if (dtInvProd != null && dtInvProd.Rows.Count > 0)
            {
                foreach (DataRow dr in dtInvProd.Rows)
                {
                    if (dr[0] != null && dr[0].ToString() != "")
                        invProdLis.Append(dr[0].ToString()).Append(",");
                }
            }

            dtINV.Columns.Add("INV_TYPE");

            //foreach (DataRow row in dtINV.Rows)
            //{
            //    row.BeginEdit();
            //    if (invProdLis.ToString().IndexOf(row["PRODNO"].ToString()) > -1)
            //    {
            //        row["INV_TYPE"] = "1";

            //    }
            //    else
            //    {
            //        //代收交易不開發票貨品,一律開收據,稅算為0

            //        row["INV_TYPE"] = "3";
            //    }
            //    row.EndEdit();
            //    row.AcceptChanges();
            }

            #endregion

            int saleTotalAmt = 0;

            if (head.Rows[0]["SALE_TOTAL_AMOUNT"] != null && head.Rows[0]["SALE_TOTAL_AMOUNT"].ToString() != ""
                && NumberUtil.IsNumeric(head.Rows[0]["SALE_TOTAL_AMOUNT"].ToString()))
                saleTotalAmt = int.Parse(head.Rows[0]["SALE_TOTAL_AMOUNT"].ToString());

            if (saleTotalAmt <= 0)
            {
                RECDT = dtINV.Select(" 1 = 1 ");
                facade.InsertReceipt(head, RECDT, objTx);                                             //收據
            }
            else
            {
                //發票 應稅 TAXABLE = 'Y' and TAXRATE != 0
                INV_TAX_DRA = dtINV.Select("TAXABLE = 'Y' and TAXRATE <> 0");
                //發票 應稅 0 應稅但是稅率0 TAXABLE = 'Y' and TAXRATE = 0
                INV_TAX_DRA_ZERO = dtINV.Select("  TAXABLE = 'Y' and TAXRATE = 0");
                //收據 免稅 TAXABLE = 'N'
                INV_NO_TAX_DRA = dtINV.Select("  TAXABLE = 'N'");

                string HOST_ID = head.Rows[0]["HOST_ID"].ToString();
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
                cmd = new OracleCommand(sb.ToString(), objTx.Connection, objTx);
                da = new OracleDataAdapter(cmd);
                da.Fill(dtInvoice);
                foreach (DataRow row in dtInvoice.Rows)
                {
                    INVOICE_NO += row[0].ToString() + ",";
                }
                if (INVOICE_NO.Length > 0) INVOICE_NO = INVOICE_NO.Substring(0, INVOICE_NO.Length - 1);
            }

            return INVOICE_NO;
    }

	private void Imei_log(string POSUUID_MASTER, OracleTransaction TranObj)
	{
		string strMessage = new SAL01_Facade().IMEISale_Log(TranObj, POSUUID_MASTER, logMsg.MODI_USER);
		string[] strMsg = strMessage.Split('|');
		if (strMsg[0] != "000") //表示失敗
		{
			throw new Exception(strMsg[1]);
		}
	}

    private void StoreSpecialDIS(string POSUUID_MASTER,OracleTransaction trans)
    {
        //讀取特殊折扣
		string sqlstr = string.Format("select count(*) from sale_detail where posuuid_master={0} and item_type='6'", OracleDBUtil.SqlStr(POSUUID_MASTER));
		OracleCommand cmd = new OracleCommand(sqlstr, trans.Connection, trans);
		object o = cmd.ExecuteScalar();
		if (o == null || Convert.ToInt32(o) == 0)
			return;

		sqlstr = "select TOTAL_AMOUNT from SALE_DETAIL where posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER);
		DataTable dt = new DataTable();
        cmd = new OracleCommand(sqlstr, trans.Connection, trans);
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        da.Fill(dt);

        foreach (DataRow row in dt.Rows)
        {
            string USED_AMOUNT = "0";
            USED_AMOUNT = row[0].ToString();//取得特殊抱怨折扣金額
            int AffectRow = 0;
            string strYYMM = OracleDBUtil.WorkDay(logMsg.STORENO); //營業日
            if (logMsg.ROLE_TYPE.ToString() == "1" || logMsg.ROLE_TYPE.ToString() == "2")   //當角色為1:店長或2:店員
            {
                AffectRow = new SAL01_Facade().UpdateStoreSpecialDIS(trans, logMsg.STORENO, strYYMM.Substring(0, 7), USED_AMOUNT, logMsg.ROLE_TYPE);
                if (AffectRow == 0)
                {
					throw new Exception("您已經沒有變更特殊抱怨折扣金額可用 !");
				}
            }
        }
    }

	private void printInvoice(string POSUUID_MASTER)
	{
		DataTable dt = new DataTable();
		OracleConnection conn = null;
		string sqlstr = "select * from SALE_DETAIL where posuuid_master = " + OracleDBUtil.SqlStr(POSUUID_MASTER) + " and ";
		try
		{
			conn = OracleDBUtil.GetConnection();
			OracleCommand cmd = new OracleCommand(sqlstr, conn);
			OracleDataAdapter da = new OracleDataAdapter(cmd);
			da.Fill(dt);

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
	}

	protected Hashtable CancelSale(OracleTransaction objTX, string masterId, string item_type)
	{
		bool bCheckOut = true; //是否可結帳
		string posuuid_master = "";
		string sale_status = "";
		string saleno = "";
		string cancelNo = Advtek.Utility.SerialNo.GenNo("SC");
		DataTable dtHead, dtDetail;
		try
		{
			dtHead = TSAL01_Facade.getSale_Head(masterId);
			dtDetail = TSAL01_Facade.getSale_Detail(masterId, item_type);
			DateTime transDate = DateTime.Now;
			if (dtHead != null && dtHead.Rows.Count > 0 && dtHead.Rows[0]["TRADE_DATE"] != null)
			{
				try
				{
					transDate = DateTime.Parse(dtHead.Rows[0]["TRADE_DATE"].ToString().Trim());
					sale_status = dtHead.Rows[0]["SALE_STATUS"].ToString();
					saleno = dtHead.Rows[0]["SALE_NO"].ToString();
				}
				catch (Exception)
				{

				}
			}
			posuuid_master = masterId;

			INVENTORY_Facade Inventory = new INVENTORY_Facade();
			//作廢退回庫存
			if (bCheckOut && dtDetail.Rows.Count > 0)
			{
				foreach (DataRow dr in dtDetail.Rows)
				{
					string Code = "";
					string Message = "";
					string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();

					if (!string.IsNullOrEmpty(dr["PRODNO"].ToString()) && dr["ISSTOCK"].ToString() == "1")
					{
						try
						{
							Inventory.PK_INVENTORY_RETURN(objTX, "1", dr["PRODNO"].ToString(), logMsg.STORENO, Stock, saleno,
							   Convert.ToInt32(dr["QUANTITY"].ToString()), logMsg.MODI_USER, dr["ID"].ToString(), ref Code, ref Message);
							if (Code != "000")
							{
								bCheckOut = false;
								throw new Exception("作廢貨品退庫存失敗!!");
							}
						}
						catch (Exception ex)
						{
							bCheckOut = false;
							throw new Exception("作廢貨品退庫存失敗!!\n" + ex.Message);
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
					throw new Exception("IMEI_LOG 失敗!!\n" + strMsg[1].Replace("'", "-").Replace("\"", " "));
				}
			}

			//作廢發票
			if (bCheckOut)
			{
				int AffectRow = 0;
                string creditNoteNo = Store_SerialNo.GenNo("CN", logMsg.STORENO, logMsg.MACHINE_ID);
                AffectRow = new SAL03_Facade().invalidOldInvoice(objTX, masterId, transDate, creditNoteNo);
				if (AffectRow < 1)
				{
					bCheckOut = false;
					throw new Exception("作廢發票失敗!!");
				}
			}

			//作廢原交易
			if (bCheckOut)
			{
				int AffectRow = 0;
                AffectRow = new SAL03_Facade().invalidOldTransactionPeriod(objTX, masterId, transDate, logMsg.MODI_USER);
				if (AffectRow < 1)
				{
					bCheckOut = false;
					throw new Exception("作廢原交易失敗 !!");
				}
			}

		}
		catch (Exception ex)
		{
			bCheckOut = false;
			throw new Exception("作廢原交易失敗\n" + ex.Message);
		}
		finally
		{

		}

		try
		{
			if (bCheckOut)
			{
				SAL01_Facade Facade = new SAL01_Facade();
				bool OrderCancel = true;
				int ret = 0;
				if (masterId != null && (!string.IsNullOrEmpty(masterId)))
				{
					if (OrderCancel)
					{
						//將TO_CLOSE_HEAD 更新為 取消中狀態, 並回填關連的POSUUID_MASTER

						Facade.UpdateUnCloseHead(dtHead, "3", logMsg.MODI_USER);
						DataTable dt = Facade.getCancleTO_CLOSE_DATA(masterId);
						foreach (DataRow dr in dt.Rows)
						{
							ret = Facade.CancelOuterSystem(dr["POSUUID_DETAIL"].ToString(), dr["SERVICE_TYPE"].ToString(),
															dr["SERVICE_SYS_ID"].ToString(), dr["BUNDLE_ID"].ToString(),
															dr["STORE_NO"].ToString(), dr["SALE_PERSON"].ToString(),
															dr["BARCODE1"].ToString(), dr["BARCODE2"].ToString(),
															dr["BARCODE3"].ToString(), dr["AMOUNT"].ToString());
							if (ret == 0)
							{
								//取消交易,commit外部系統成功才刪除未結清單中資料
								StringBuilder posuuid_detailList = new StringBuilder("");
								posuuid_detailList.Append(OracleDBUtil.SqlStr(dr["POSUUID_DETAIL"].ToString()));
								Facade.delTO_CLOSE(posuuid_detailList);
							}
							else
							{
								Facade.InsertDataUploadLog(dr["POSUUID_DETAIL"].ToString());
								OrderCancel = false;
							}
						}
					}
				}
				if (OrderCancel)
				{
					if (saleno == "")
						saleno = string.Format(SerialNo.GenNo("SALE"), logMsg.STORENO, logMsg.MACHINE_ID);
				}
				else
				{
					throw new Exception("交易作廢失敗 !");
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception("交易作廢失敗 !\n" + ex.Message);
		}
		finally
		{
		}
		Hashtable ht = new Hashtable();
		ht.Add("CancelNo", cancelNo);
		ht.Add("OrigSaleNo", saleno);
		return ht;
	}

}

