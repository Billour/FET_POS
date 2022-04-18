using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Web.Services;
using Advtek.Utility;
using System.Collections;
using FET.POS.WS.PROXY;
using FET.POS.Model.Facade.FacadeImpl;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    #region 全域變數
    string paramsString = "";
    OracleConnection conn_new_pos;
    string _BeginToLog = "";
    string simQueryUrl = System.Configuration.ConfigurationManager.AppSettings["SimQueryURL"].ToString();
    #endregion

    public Service()
    {
        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent();         
    }

    [WebMethod]
    public string WS_Interface_eForm(string Image_Number, string Employee_Id, string Sys_Id, string Store_Id)
    {
        // Sys_Id mapping SERVICE_TYPE 
        NameValueCollection Service_Type = new NameValueCollection();
        Service_Type.Add("IA", "1");
        Service_Type.Add("LOY", "2");
        Service_Type.Add("SSI", "4");
        Service_Type.Add("eStore", "10");

        paramsString = string.Format("Image_Number={0},Employee_Id={1},Sys_Id={2},Store_Id={3}", Image_Number, Employee_Id, Sys_Id, Store_Id);

        //POSUUID_DETAIL or errMessage
        string ReturnValue = "";

        //先將傳入參數寫入 LOG
        this.WriteLog("OnLoad", OracleDBUtil.SqlStr("begin_to_select_POSUUID_DETAIL," + paramsString), "WEB SERVICE", Employee_Id, 0);



        try
        {
            conn_new_pos = OracleDBUtil.GetConnection();

            //check SERVICE_SYS_ID
            DataTable dt_POSUUID_DETAIL = OracleDBUtil.GetDataSet(
                    conn_new_pos,
                    string.Format("SELECT POSUUID_DETAIL FROM TO_CLOSE_HEAD M WHERE M.SERVICE_SYS_ID={0} AND M.SERVICE_TYPE={1}",
                            OracleDBUtil.SqlStr(Image_Number), OracleDBUtil.SqlStr(Service_Type[Sys_Id]))
                    ).Tables[0];

            DataTable dt_POSUUID_DETAIL_LOG = OracleDBUtil.GetDataSet(
                    conn_new_pos,
                   string.Format("SELECT POSUUID_DETAIL FROM TO_CLOSE_HEAD_LOG M WHERE M.SERVICE_SYS_ID={0} AND M.SERVICE_TYPE={1}",
                            OracleDBUtil.SqlStr(Image_Number), OracleDBUtil.SqlStr(Service_Type[Sys_Id]))
                    ).Tables[0];

            if (dt_POSUUID_DETAIL.Rows.Count > 0 || dt_POSUUID_DETAIL_LOG.Rows.Count > 0)
            {
                //Image_Number重複，回傳原來的POSUUID_DETAIL
                if (dt_POSUUID_DETAIL.Rows.Count > 0) ReturnValue = dt_POSUUID_DETAIL.Rows[0][0].ToString();
                if (dt_POSUUID_DETAIL_LOG.Rows.Count > 0) ReturnValue = dt_POSUUID_DETAIL_LOG.Rows[0][0].ToString();
                this.WriteLog("Finish", OracleDBUtil.SqlStr("Image_Number重複，回傳原來的POSUUID_DETAIL!"), "WEB SERVICE", Employee_Id, 0);

            }
            else if (Sys_Id == Service_Type.GetKey(0))
            {
                #region IA service_type='1'
                bool IsSale = false;
                bool IsSIM = false;
                string SIM = "";
                string SIM_PRODNO = "SIM"; //依照SIM抓取的PRODNO
                string SALE_NO = "";
                string SIM_UNIT_PRICE = "0";
                string IMAGE_NUMBER = Image_Number;
                OracleConnection objConn = OracleDBUtil.GetIAConnection();
                OracleTransaction objTX = conn_new_pos.BeginTransaction();
                try
                {
                    //取得連線
                    string iaSql = string.Format("SELECT * FROM VIEW_ACTI_MASTER M WHERE M.IMAGE_NUMBER={0}", OracleDBUtil.SqlStr(Image_Number));
                    DataTable dt = OracleDBUtil.GetDataSet(objConn, iaSql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr_head = dt.Rows[0];

                        //取TWO_SIM_IMAGE_NUMBER 2011 3/8 by siwen
                        string TWO_SIM_IMAGE_NUMBER = dr_head["TWO_SIM_IMAGE_NUMBER"] == DBNull.Value ? Image_Number : dr_head["TWO_SIM_IMAGE_NUMBER"].ToString();

                        //買一送一 判斷  2011 3/17 by siwen
                        string NC_MSISDN = dr_head["NC_MSISDN"] == DBNull.Value ? "" : dr_head["NC_MSISDN"].ToString();
                        string NC_SIM = dr_head["NC_SIM"] == DBNull.Value ? "" : dr_head["NC_SIM"].ToString();



                        if (TWO_SIM_IMAGE_NUMBER != Image_Number)
                        {
                            iaSql = string.Format("SELECT * FROM VIEW_ACTI_MASTER M WHERE M.IMAGE_NUMBER={0}", OracleDBUtil.SqlStr(TWO_SIM_IMAGE_NUMBER));
                            dt = OracleDBUtil.GetDataSet(objConn, iaSql).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                dr_head = dt.Rows[0];
                                IMAGE_NUMBER = TWO_SIM_IMAGE_NUMBER;
                                NC_MSISDN = dr_head["NC_MSISDN"] == DBNull.Value ? "" : dr_head["NC_MSISDN"].ToString();
                                NC_SIM = dr_head["NC_SIM"] == DBNull.Value ? "" : dr_head["NC_SIM"].ToString();
                            }
                            else
                            {
                                return ReturnValue;
                            }

                        }
                        //取POS_UUID
                        string POS_UUID = GuidNo.getUUID();
                        //取POSUUID_MASTER
                        string POSUUID_MASTER = GuidNo.getUUID();

                        IsSale = dr_head["FUN_ID"].ToString().ToUpper() == "IA_NONCLOSING";
                        //IsSale = true;
                        //如果買一送一 就一定進入to_close
                        //if (!string.IsNullOrEmpty(NC_MSISDN) && !string.IsNullOrEmpty(NC_SIM))
                        //{
                        //    IsSale = false;
                        //}

                        #region 判斷是否扣NewCash
                        if (!string.IsNullOrEmpty(NC_MSISDN) && !string.IsNullOrEmpty(NC_SIM))
                        {
                            //抓取料號
                            Hashtable table = SimQueryProxy.PostPaidSimQuery(NC_SIM.Trim(), simQueryUrl);
                            //string temp_prodno = "319900302";//debug
                            if (table["Result"].ToString() != "Err" && !string.IsNullOrEmpty(table["Result"].ToString()))
                            {


                                OracleCommand cmd = new OracleCommand("SP_Direct_Trans_IA");
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = conn_new_pos;
                                cmd.Transaction = objTX;
                                cmd.Parameters.Add("Image_number", OracleType.VarChar).Value = Image_Number;
                                cmd.Parameters.Add("Sim_Card_number", OracleType.VarChar).Value = NC_SIM.Trim();
                                cmd.Parameters.Add("Item_Code", OracleType.VarChar).Value = table["Result"].ToString();
                                cmd.Parameters.Add("Fun_Id", OracleType.VarChar).Value = dr_head["FUN_ID"].ToString();
                                cmd.Parameters.Add("Employee_Id", OracleType.VarChar).Value = Employee_Id;
                                cmd.Parameters.Add("Sys_Id", OracleType.VarChar).Value = Sys_Id;
                                cmd.Parameters.Add("Store_Id", OracleType.VarChar).Value = Store_Id;
                                cmd.Parameters.Add("O_POSuuid_Details", OracleType.VarChar, 500).Direction = ParameterDirection.Output;
                                cmd.Parameters.Add("O_POSuuid_Master", OracleType.VarChar, 500).Direction = ParameterDirection.Output;
                                cmd.Parameters.Add("O_Image_Numer", OracleType.VarChar, 500).Direction = ParameterDirection.Output;
                                cmd.Parameters.Add("O_Reasoncode", OracleType.VarChar, 500).Direction = ParameterDirection.Output;
                                cmd.Parameters.Add("O_Errordec", OracleType.VarChar, 500).Direction = ParameterDirection.Output;
                                cmd.ExecuteNonQuery();
                                string o_posuuid_detail = cmd.Parameters["O_POSuuid_Details"].Value.ToString();
                                string o_posuuid_master = cmd.Parameters["O_POSuuid_Master"].Value.ToString();
                                string o_resoncode = cmd.Parameters["O_Reasoncode"].Value.ToString();
                                string o_errordec = cmd.Parameters["O_Errordec"].Value.ToString();
                                //寫LOG
                                _BeginToLog = "with_NewCash_call_SP_Direct_Trans:" + "Resoncode:" + o_resoncode + ",errordec:" + o_errordec + ",Image_Number=" + IMAGE_NUMBER + ",Posuuid_details=" + o_posuuid_detail + ",Posuuid_master=" + o_posuuid_master + ",Source_detail=" + POS_UUID + ",SIM=" + NC_SIM.Trim();

                                this.WriteLog("NewCash", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);
                            }
                            else
                            {
                                _BeginToLog = "NewCash:" + "Resoncode:999,errordec:查無SIM卡料號,Image_Number=" + IMAGE_NUMBER + ",posuuid_detail=" + POS_UUID;
                                this.WriteLog("NewCash", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);
                            }


                            //BuckleInventory(objTX, Store_Id, SALE_NO, Employee_Id, SIM_PRODNO, detailid);


                        }
                        #endregion

                        #region SIM Check
                        IsSIM = dr_head["SIM"] != null && !string.IsNullOrEmpty(dr_head["SIM"].ToString()) && dr_head["TRANS_TYPE"].ToString().ToUpper() != "WIMAX" && dr_head["SIM"].ToString().ToUpper() != "NSID";
                        if (IsSIM)
                        {
                            try
                            {
                                SIM = dr_head["SIM"].ToString();
                                Hashtable table = new Hashtable();
                                bool IsPrepaid = CheckPrepaid(dr_head["TRANS_TYPE"].ToString());
                                if (IsPrepaid)
                                {
                                    table = SimQueryProxy.PrePaidSimQuery(SIM, simQueryUrl);
                                }
                                else
                                {
                                    table = SimQueryProxy.PostPaidSimQuery(SIM, simQueryUrl);
                                }

                                if (table["Result"].ToString() == "Err" || string.IsNullOrEmpty(table["Result"].ToString()))
                                {

                                    IsSale = false;
                                }
                                else
                                {
                                    SIM_PRODNO = table["Result"].ToString();
                                    if (IsPrepaid)
                                    {
                                        SIM_UNIT_PRICE = table["Price"].ToString();
                                    }

                                }

                            }
                            catch
                            {
                                IsSale = false;
                            }
                        }
                        #endregion

                        if (IsSale)
                        {
                            SALE_NO = string.Format(SerialNo.GenNo("SALE"), Store_Id, "01");
                        }
                        string PROMOTION_CODE = dr_head["PROMOTION_CODE"].ToString();
                        //write head 
                        StringBuilder sb_head = new StringBuilder();
                        if (!IsSale)
                        {
                            #region TO_CLOSE_HEAD
                            sb_head.AppendLine("INSERT INTO TO_CLOSE_HEAD(");
                            sb_head.AppendLine("STATUS, ");
                            sb_head.AppendLine("APPLY_DATE, ");
                            sb_head.AppendLine("STORE_NO, ");
                            sb_head.AppendLine("SERVICE_TYPE, ");
                            sb_head.AppendLine("SALE_PERSON, ");
                            sb_head.AppendLine("CREATE_USER, ");
                            sb_head.AppendLine("CREATE_DTM, ");
                            sb_head.AppendLine("MODI_USER, ");
                            sb_head.AppendLine("MODI_DTM, ");
                            sb_head.AppendLine("POSUUID_DETAIL, ");
                            sb_head.AppendLine("FUN_ID, ");
                            sb_head.AppendLine("R_RATE, ");
                            sb_head.AppendLine("DATA, ");
                            sb_head.AppendLine("VOICE, ");
                            sb_head.AppendLine("TRANS_TYPE, ");
                            sb_head.AppendLine("APPROVE_STATUS_POS, ");
                            sb_head.AppendLine("CONN_FLAG, ");
                            sb_head.AppendLine("QUERY_TABLE_NAME, ");
                            sb_head.AppendLine("BUNDLE_TYPE, ");
                            sb_head.AppendLine("BUNDLE_ID, ");
                            sb_head.AppendLine("TOTAL_AMOUNT, ");
                            sb_head.AppendLine("SERVICE_SYS_ID, ");
                            sb_head.AppendLine("MSISDN ");
                            sb_head.AppendLine(") VALUES(");
                            sb_head.AppendLine("'1', ");
                            sb_head.AppendLine("TRUNC(SYSDATE), ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(Store_Id) + ", ");
                            sb_head.AppendLine("'1', ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                            sb_head.AppendLine("SYSDATE, ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                            sb_head.AppendLine("SYSDATE, ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(POS_UUID) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["FUN_ID"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["R_RATE"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["DATA"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["VOICE"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["TRANS_TYPE"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["APPROVE_STATUS"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["CONN_FLAG"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["QUERY_TABLE_NAME"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["BUNDLE_TYPE"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["BUNDLE_ID"].ToString()) + ", ");
                            sb_head.AppendLine("0, ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(IMAGE_NUMBER) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["MSISDN"].ToString()));
                            sb_head.AppendLine(")");

                            OracleDBUtil.ExecuteSql(objTX, sb_head.ToString());
                            #endregion
                        }
                        else
                        {
                            #region TO_CLOSE_HEAD_LOG
                            sb_head.AppendLine("INSERT INTO TO_CLOSE_HEAD_LOG(");
                            sb_head.AppendLine("ID,");
                            sb_head.AppendLine("STATUS, ");
                            sb_head.AppendLine("APPLY_DATE, ");
                            sb_head.AppendLine("STORE_NO, ");
                            sb_head.AppendLine("SERVICE_TYPE, ");
                            sb_head.AppendLine("SALE_PERSON, ");
                            sb_head.AppendLine("CREATE_USER, ");
                            sb_head.AppendLine("CREATE_DTM, ");
                            sb_head.AppendLine("MODI_USER, ");
                            sb_head.AppendLine("MODI_DTM, ");
                            sb_head.AppendLine("POSUUID_DETAIL, ");
                            sb_head.AppendLine("FUN_ID, ");
                            sb_head.AppendLine("R_RATE, ");
                            sb_head.AppendLine("DATA, ");
                            sb_head.AppendLine("VOICE, ");
                            sb_head.AppendLine("TRANS_TYPE, ");
                            sb_head.AppendLine("APPROVE_STATUS_POS, ");
                            sb_head.AppendLine("CONN_FLAG, ");
                            sb_head.AppendLine("QUERY_TABLE_NAME, ");
                            sb_head.AppendLine("BOUNDLE_TYPE, ");
                            sb_head.AppendLine("BOUNDLE_ID, ");
                            sb_head.AppendLine("TOTAL_AMOUNT, ");
                            sb_head.AppendLine("SERVICE_SYS_ID ");
                            sb_head.AppendLine(") VALUES(");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(GuidNo.getUUID()) + ",");
                            sb_head.AppendLine("'1', ");
                            sb_head.AppendLine("TRUNC(SYSDATE), ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(Store_Id) + ", ");
                            sb_head.AppendLine("'1', ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                            sb_head.AppendLine("SYSDATE, ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                            sb_head.AppendLine("SYSDATE, ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(POS_UUID) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["FUN_ID"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["R_RATE"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["DATA"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["VOICE"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["TRANS_TYPE"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["APPROVE_STATUS"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["CONN_FLAG"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["QUERY_TABLE_NAME"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["BUNDLE_TYPE"].ToString()) + ", ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["BUNDLE_ID"].ToString()) + ", ");
                            sb_head.AppendLine("0, ");
                            sb_head.AppendLine(OracleDBUtil.SqlStr(IMAGE_NUMBER));
                            sb_head.AppendLine(")");

                            OracleDBUtil.ExecuteSql(objTX, sb_head.ToString());
                            #endregion

                            #region SALE_HEAD

                            //STORE_NAME
                            string strStoreName = "";
                            string strSql = string.Format("SELECT S.STORENAME FROM STORE S WHERE S.STORE_NO={0}", OracleDBUtil.SqlStr(Store_Id));
                            DataTable sdt = OracleDBUtil.GetDataSet(objTX, strSql).Tables[0];
                            if (sdt != null && sdt.Rows.Count > 0)
                                strStoreName = OracleDBUtil.SqlStr(sdt.Rows[0][0].ToString());

                            this.InsertSalesHead(Store_Id, Employee_Id, strStoreName, POSUUID_MASTER, SALE_NO, "2", objTX);
                            #endregion
                        }
                        int i = 1;

                        #region SIM

                        if (IsSIM)
                        {

                            if (!IsSale)
                            {
                                #region TO_CLOSE_ITEM
                                //write item                    
                                StringBuilder sb_item = new StringBuilder();
                                sb_item.AppendLine("INSERT INTO TO_CLOSE_ITEM(");
                                sb_item.AppendLine("ID, ");
                                sb_item.AppendLine("SEQNO, ");
                                sb_item.AppendLine("PRODNO, ");
                                sb_item.AppendLine("PROMOTION_CODE, ");
                                sb_item.AppendLine("CREATE_USER, ");
                                sb_item.AppendLine("CREATE_DTM, ");
                                sb_item.AppendLine("MODI_USER, ");
                                sb_item.AppendLine("MODI_DTM, ");
                                sb_item.AppendLine("SIM_CARD_NO, ");
                                sb_item.AppendLine("MSISDN, ");
                                sb_item.AppendLine("QUANTITY, ");
                                sb_item.AppendLine("AMOUNT, ");
                                sb_item.AppendLine("UNIT_PRICE, ");
                                sb_item.AppendLine("POSUUID_DETAIL ");
                                sb_item.AppendLine(") VALUES(");
                                sb_item.AppendLine("'" + GuidNo.getUUID() + "', ");
                                sb_item.AppendLine("'" + i++ + "', ");
                                sb_item.AppendLine("'" + SIM_PRODNO + "', ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(dr_head["PROMOTION_CODE"].ToString()) + ", ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                                sb_item.AppendLine("SYSDATE, ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                                sb_item.AppendLine("SYSDATE, ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(dr_head["SIM"].ToString()) + ", ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(dr_head["MSISDN"].ToString()) + ", ");
                                sb_item.AppendLine("1, ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(SIM_UNIT_PRICE) + ", ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(SIM_UNIT_PRICE) + ", ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(POS_UUID));
                                sb_item.AppendLine(")");

                                OracleDBUtil.ExecuteSql(objTX, sb_item.ToString());
                                i++;
                                #endregion
                            }
                            else
                            {
                                #region TO_CLOSE_ITEM_LOG
                                //write item                    
                                StringBuilder sb_item = new StringBuilder();
                                sb_item.AppendLine("INSERT INTO TO_CLOSE_ITEM_LOG(");
                                sb_item.AppendLine("ID, ");
                                sb_item.AppendLine("SEQNO, ");
                                sb_item.AppendLine("PRODNO, ");
                                sb_item.AppendLine("PROMOTION_CODE, ");
                                sb_item.AppendLine("CREATE_USER, ");
                                sb_item.AppendLine("CREATE_DTM, ");
                                sb_item.AppendLine("MODI_USER, ");
                                sb_item.AppendLine("MODI_DTM, ");
                                sb_item.AppendLine("SIM_CARD_NO, ");
                                sb_item.AppendLine("MSISDN, ");
                                sb_item.AppendLine("QUANTITY, ");
                                sb_item.AppendLine("AMOUNT, ");
                                sb_item.AppendLine("UNIT_PRICE, ");
                                sb_item.AppendLine("POSUUID_DETAIL ");
                                sb_item.AppendLine(") VALUES(");
                                sb_item.AppendLine("'" + GuidNo.getUUID() + "', ");
                                sb_item.AppendLine("'1', ");
                                sb_item.AppendLine("'" + SIM_PRODNO + "', ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(dr_head["PROMOTION_CODE"].ToString()) + ", ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                                sb_item.AppendLine("SYSDATE, ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                                sb_item.AppendLine("SYSDATE, ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(dr_head["SIM"].ToString()) + ", ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(dr_head["MSISDN"].ToString()) + ", ");
                                sb_item.AppendLine("1, ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(SIM_UNIT_PRICE) + ", ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(SIM_UNIT_PRICE) + ", ");
                                sb_item.AppendLine(OracleDBUtil.SqlStr(POS_UUID));
                                sb_item.AppendLine(")");

                                OracleDBUtil.ExecuteSql(objTX, sb_item.ToString());
                                i++;
                                #endregion

                                #region SALE_DETAIL
                                Hashtable source = new Hashtable();
                                source.Add("BUNDLE_ID", dr_head["BUNDLE_ID"].ToString());
                                source.Add("BUNDLE_TYPE", dr_head["BUNDLE_TYPE"].ToString());
                                source.Add("FUN_ID", dr_head["FUN_ID"].ToString());
                                source.Add("DATA", dr_head["DATA"].ToString());
                                source.Add("VOICE", dr_head["VOICE"].ToString());
                                source.Add("APPROVE_STATUS_POS", dr_head["APPROVE_STATUS"].ToString());
                                source.Add("CONN_FLAG", dr_head["CONN_FLAG"].ToString());
                                source.Add("SIM_NO", dr_head["SIM"].ToString());
                                source.Add("MSISDN", dr_head["MSISDN"].ToString());
                                source.Add("ACCOUNT_ID", dr_head["ACCOUNTNO"].ToString());
                                source.Add("SUBSCRIBER_ID", dr_head["SUBRID"].ToString());
                                source.Add("UNIT_PRICE", SIM_UNIT_PRICE);
                                string detailid = this.InsertSalesDetail(POS_UUID, source, i, Employee_Id, Image_Number, "1", SIM_UNIT_PRICE, POSUUID_MASTER, SIM_PRODNO, objTX);
                                #endregion

                                #region 扣庫存

                                BuckleInventory(objTX, Store_Id, SALE_NO, Employee_Id, SIM_PRODNO, detailid);

                                #endregion
                            }


                        }

                        #endregion




                        DataTable dt_detail = OracleDBUtil.GetDataSet(
                            objConn,
                            string.Format("SELECT * FROM VIEW_ACTI_ITEM D WHERE D.IMAGE_NUMBER={0}", OracleDBUtil.SqlStr(IMAGE_NUMBER))
                            ).Tables[0];
                        decimal total_amount = 0;
                        int j = 1;
                        foreach (DataRow dr_detail in dt_detail.Rows)
                        {
                            //抓出價格
                            string PRODNO = dr_detail["ITEM_CODES"].ToString();
                            string ITEM_LEVEL = dr_detail["ITEM_LEVEL"].ToString();
                            string sqlstr = string.Format("select FN_QUERY_PROMO_AMOUNT('{0}','{1}','{2}') from dual", PROMOTION_CODE, PRODNO, ITEM_LEVEL);
                            j++;
                            DataTable dt_amount = OracleDBUtil.GetDataSet(objTX, sqlstr).Tables[0];
                            decimal amount = 0;
                            if (dt_amount.Rows.Count > 0) amount = Convert.ToDecimal(dt_amount.Rows[0][0]);

                            string POSUUID_DETAIL = GuidNo.getUUID();

                            if (!IsSale)
                            {
                                #region TO_CLOSE_ITEM
                                StringBuilder sb_detail = new StringBuilder();

                                sb_detail.AppendLine("INSERT INTO TO_CLOSE_ITEM(");
                                sb_detail.AppendLine("ID, "); //1
                                sb_detail.AppendLine("SEQNO, ");//2
                                sb_detail.AppendLine("PRODNO, ");//3
                                sb_detail.AppendLine("CREATE_USER, ");//4
                                sb_detail.AppendLine("CREATE_DTM, ");//5
                                sb_detail.AppendLine("MODI_USER, ");//6
                                sb_detail.AppendLine("MODI_DTM, ");//7
                                sb_detail.AppendLine("QUANTITY, ");//8
                                sb_detail.AppendLine("AMOUNT, ");//9
                                sb_detail.AppendLine("UNIT_PRICE, ");//9
                                sb_detail.AppendLine("PROMOTION_CODE, ");//10
                                sb_detail.AppendLine("POSUUID_DETAIL ");//11
                                sb_detail.AppendLine(") VALUES(");
                                sb_detail.AppendLine("'" + POSUUID_DETAIL + "', ");//1
                                sb_detail.AppendLine("'" + i++ + "', ");//2
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_detail["ITEM_CODES"].ToString()) + ", ");//3
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");//4
                                sb_detail.AppendLine("SYSDATE, ");//5
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");//6
                                sb_detail.AppendLine("SYSDATE, ");//7
                                sb_detail.AppendLine("1, ");//8
                                sb_detail.AppendFormat(amount.ToString() + ", ");//9
                                sb_detail.AppendFormat(amount.ToString() + ", ");//9
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_head["PROMOTION_CODE"].ToString()) + ", ");//10
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(POS_UUID));//11
                                sb_detail.AppendLine(")");
                                OracleDBUtil.ExecuteSql(objTX, sb_detail.ToString());
                                total_amount += amount;

                                //新增折扣
                                //InsertToCloseDiscount(dr_head["MSISDN"].ToString(), dr_head["R_RATE"].ToString(), dr_head["PROMOTION_CODE"].ToString(), dr_detail["ITEM_CODES"].ToString(), dr_head["DATA"].ToString(), dr_head["VOICE"].ToString(), dr_head["TRANS_TYPE"].ToString(), "", dr_head["BUNDLE_TYPE"].ToString(), Employee_Id, "1", Store_Id, amount.ToString(), Employee_Id, POSUUID_DETAIL, objTX);
                                #endregion
                            }
                            else
                            {
                                #region TO_CLOSE_ITEM_LOG
                                StringBuilder sb_detail = new StringBuilder();

                                sb_detail.AppendLine("INSERT INTO TO_CLOSE_ITEM_LOG(");
                                sb_detail.AppendLine("ID, "); //1
                                sb_detail.AppendLine("SEQNO, ");//2
                                sb_detail.AppendLine("PRODNO, ");//3
                                sb_detail.AppendLine("CREATE_USER, ");//4
                                sb_detail.AppendLine("CREATE_DTM, ");//5
                                sb_detail.AppendLine("MODI_USER, ");//6
                                sb_detail.AppendLine("MODI_DTM, ");//7
                                sb_detail.AppendLine("QUANTITY, ");//8
                                sb_detail.AppendLine("AMOUNT, ");//9
                                sb_detail.AppendLine("UNIT_PRICE, ");//9
                                sb_detail.AppendLine("PROMOTION_CODE, ");//10
                                sb_detail.AppendLine("POSUUID_DETAIL ");//11
                                sb_detail.AppendLine(") VALUES(");
                                sb_detail.AppendLine("'" + POSUUID_DETAIL + "', ");//1
                                sb_detail.AppendLine("'" + i++ + "', ");//2
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_detail["ITEM_CODES"].ToString()) + ", ");//3
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");//4
                                sb_detail.AppendLine("SYSDATE, ");//5
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");//6
                                sb_detail.AppendLine("SYSDATE, ");//7
                                sb_detail.AppendLine("1, ");//8
                                sb_detail.AppendFormat(amount.ToString() + ", ");//9
                                sb_detail.AppendFormat(amount.ToString() + ", ");//9
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_head["PROMOTION_CODE"].ToString()) + ", ");//10
                                sb_detail.AppendLine(OracleDBUtil.SqlStr(POS_UUID));//11
                                sb_detail.AppendLine(")");
                                OracleDBUtil.ExecuteSql(objTX, sb_detail.ToString());
                                #endregion

                                #region SALE_DETAIL
                                Hashtable source = new Hashtable();
                                source.Add("BUNDLE_ID", dr_head["BUNDLE_ID"].ToString());
                                source.Add("BUNDLE_TYPE", dr_head["BUNDLE_TYPE"].ToString());
                                source.Add("FUN_ID", dr_head["FUN_ID"].ToString());
                                source.Add("DATA", dr_head["DATA"].ToString());
                                source.Add("VOICE", dr_head["VOICE"].ToString());
                                source.Add("APPROVE_STATUS_POS", dr_head["APPROVE_STATUS"].ToString());
                                source.Add("CONN_FLAG", dr_head["CONN_FLAG"].ToString());
                                source.Add("SIM_NO", dr_head["SIM"].ToString());
                                source.Add("MSISDN", dr_head["MSISDN"].ToString());
                                source.Add("ACCOUNT_ID", dr_head["ACCOUNTNO"].ToString());
                                source.Add("SUBSCRIBER_ID", dr_head["SUBRID"].ToString());
                                source.Add("UNIT_PRICE", amount.ToString());




                                string detailid = this.InsertSalesDetail(POS_UUID, source, i, Employee_Id, Image_Number, "1", amount.ToString(), POSUUID_MASTER, dr_detail["ITEM_CODES"].ToString(), objTX);
                                total_amount += amount;
                                #endregion

                                #region 扣庫存

                                BuckleInventory(objTX, Store_Id, SALE_NO, Employee_Id, dr_detail["ITEM_CODES"].ToString(), detailid);
                                #endregion
                            }

                        }

                     

                        #region 更新TOTAL_AMOUNT
                        StringBuilder sb_update = new StringBuilder();
                        if (!IsSale)
                        {

                            sb_update.AppendFormat("update TO_CLOSE_HEAD h set total_amount  = {0} where POSUUID_DETAIL ='{1}' ", total_amount.ToString(), POS_UUID);
                        }
                        else
                        {
                            sb_update.AppendFormat("update SALE_HEAD h set SALE_TOTAL_AMOUNT  = {0} where POSUUID_MASTER ='{1}' ", total_amount.ToString(), POSUUID_MASTER);
                            OracleDBUtil.ExecuteSql(objTX, sb_update.ToString());
                            sb_update = new StringBuilder();
                            sb_update.AppendFormat("update TO_CLOSE_HEAD_LOG h set total_amount  = {0} where POSUUID_DETAIL ='{1}' ", total_amount.ToString(), POS_UUID);
                        }
                        OracleDBUtil.ExecuteSql(objTX, sb_update.ToString());

                        #endregion

                        #region 計算折扣
                        if (!IsSale)
                        {
                            CreateDiscount(POS_UUID, 1, objTX);
                        }
                        #endregion

                        objTX.Commit();
                        OracleParameter parSTATUS = new OracleParameter();
                        parSTATUS.OracleType = OracleType.VarChar;
                        parSTATUS.Size = 200;
                        parSTATUS.ParameterName = "STATUS";
                        parSTATUS.Direction = ParameterDirection.Output;

                        OracleParameter parERR_CODE = new OracleParameter();
                        parERR_CODE.OracleType = OracleType.Number;
                        parERR_CODE.ParameterName = "ERR_CODE";
                        parERR_CODE.Direction = ParameterDirection.Output;

                        OracleParameter parERR_MESG = new OracleParameter();
                        parERR_MESG.OracleType = OracleType.VarChar;
                        parERR_MESG.Size = 200;
                        parERR_MESG.ParameterName = "ERR_MESG";
                        parERR_MESG.Direction = ParameterDirection.Output;
                        //寫LOG
                        _BeginToLog = "begin_to_call_SP_IA_Update_ACTI_POSUUID:" + "ACTI_NO=" + IMAGE_NUMBER + ",QUERY_TABLE_NAME=" + dr_head["QUERY_TABLE_NAME"] + ",UUID_DETAILS=" + POS_UUID;
                        //先將傳入參數寫入
                        this.WriteLog("OnLoad", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);



                        OracleTransaction objIATX = objConn.BeginTransaction();
                        OracleDBUtil.ExecuteSql_SP(
                            objIATX,
                            "SP_IA_Update_ACTI_POSUUID",
                            new OracleParameter("ACTI_NO", IMAGE_NUMBER),
                            new OracleParameter("QUERY_TABLE_NAME", dr_head["QUERY_TABLE_NAME"]),
                            new OracleParameter("UUID_DETAILS", POS_UUID),
                            parSTATUS,
                            parERR_CODE,
                            parERR_MESG
                            );
                        objIATX.Commit();

                        ReturnValue = POS_UUID;
                    }


                }
                catch (Exception ex)
                {
                    objTX.Rollback();
                    //ReturnValue = ex.Message;
                    throw ex;
                }
                finally
                {
                    objTX.Dispose();

                }


                #endregion
            }
            else if (Sys_Id == Service_Type.GetKey(1))
            {

                #region LOY service_type='2'
                //取得連線
                OracleConnection objConn = OracleDBUtil.GetLYConnection();
                OracleTransaction objTX = conn_new_pos.BeginTransaction();
                try
                {
                    #region Load VW_LOYALTY_POS_MASTER
                    DataTable dt = OracleDBUtil.GetDataSet(
                        objConn,
                        string.Format("SELECT * FROM LOYALTY.VW_LOYALTY_POS_MASTER M WHERE M.IMAGE_NO={0}", OracleDBUtil.SqlStr(Image_Number))
                        ).Tables[0];
                    #endregion

                    if (dt.Rows.Count > 0)
                    {
                        #region 資料處理
                        DataRow dr_head = dt.Rows[0];

                        string BUNDLE_ID = dr_head["BUNDLE_ID"] == null ? "" : dr_head["BUNDLE_ID"].ToString();
                        string BUNDLE_TYPE = dr_head["BUNDLE_TYPE"] == null ? "" : dr_head["BUNDLE_TYPE"].ToString();
                        string SUBSCRIBE_NO = dr_head["SUBR_NO"] == null ? "" : dr_head["SUBR_NO"].ToString();
                        string BILLING_ACCOUNT_ID = dr_head["ACCOUNT_NO"] == null ? "" : dr_head["ACCOUNT_NO"].ToString();
                        string TRANS_TYPE = dr_head["TRANS_TYPE"] == null ? "" : dr_head["TRANS_TYPE"].ToString(); ;
                        //取POS_UUID
                        string POS_UUID = GuidNo.getUUID();

                        #region Insert Head
                        //write head 
                        StringBuilder sb_head = new StringBuilder();

                        sb_head.AppendLine("INSERT INTO TO_CLOSE_HEAD(");
                        sb_head.AppendLine("STATUS, ");
                        sb_head.AppendLine("APPLY_DATE, ");
                        sb_head.AppendLine("STORE_NO, ");
                        sb_head.AppendLine("SERVICE_TYPE, ");
                        sb_head.AppendLine("SALE_PERSON, ");
                        sb_head.AppendLine("CREATE_USER, ");
                        sb_head.AppendLine("CREATE_DTM, ");
                        sb_head.AppendLine("MODI_USER, ");
                        sb_head.AppendLine("MODI_DTM, ");
                        sb_head.AppendLine("POSUUID_DETAIL, ");
                        sb_head.AppendLine("R_RATE, ");
                        sb_head.AppendLine("APPROVE_STATUS_POS, ");
                        sb_head.AppendLine("SERVICE_SYS_ID, ");
                        sb_head.AppendLine("BUNDLE_ID, ");
                        sb_head.AppendLine("BUNDLE_TYPE, ");
                        sb_head.AppendLine("SUBSCRIBE_NO, ");
                        sb_head.AppendLine("BILLING_ACCOUNT_ID, ");
                        sb_head.AppendLine("TRANS_TYPE, ");
                        sb_head.AppendLine("MSISDN ");
                        sb_head.AppendLine(") VALUES(");
                        sb_head.AppendLine("'1', ");
                        sb_head.AppendLine(OracleDBUtil.DateStr(Convert.ToDateTime(dr_head["APPLY_DATE"]).ToString("yyyy/MM/dd")) + ", ");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(Store_Id) + ", ");
                        sb_head.AppendLine("'2', ");//SERVICE_TYPE
                        sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                        sb_head.AppendLine("SYSDATE, ");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");
                        sb_head.AppendLine("SYSDATE, ");
                        sb_head.AppendLine("'" + POS_UUID + "', ");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["NEWRATEPLANCODE"].ToString()) + ", ");
                        sb_head.AppendLine("'1', ");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(Image_Number) + ",");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(BUNDLE_ID) + ",");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(BUNDLE_TYPE) + ",");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(SUBSCRIBE_NO) + ",");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(BILLING_ACCOUNT_ID) + ",");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(TRANS_TYPE) + ",");
                        sb_head.AppendLine(OracleDBUtil.SqlStr(dr_head["MSISDN"].ToString()));
                        sb_head.AppendLine(")");

                        OracleDBUtil.ExecuteSql(objTX, sb_head.ToString());

                        DataTable dt_detail = OracleDBUtil.GetDataSet(
                            objConn,
                            string.Format(@"
SELECT master.PROMOTION_CODE, detail.* 
FROM LOYALTY.VW_LOYALTY_POS_MASTER master, LOYALTY.VW_LOYALTY_POS_DETAIL detail 
WHERE master.IMAGE_NO = detail.IMAGE_NO AND detail.IMAGE_NO={0}", OracleDBUtil.SqlStr(Image_Number))
                            ).Tables[0];
                        #endregion

                        #region Insert Item
                        int i = 1;
                        decimal total_amount = 0;
                        foreach (DataRow dr_detail in dt_detail.Rows)
                        {
                            string POSUUID_DETAIL = GuidNo.getUUID();
                            StringBuilder sb_detail = new StringBuilder();

                            sb_detail.AppendLine("INSERT INTO TO_CLOSE_ITEM(");
                            sb_detail.AppendLine("ID, ");  //1
                            sb_detail.AppendLine("SEQNO, ");//2
                            sb_detail.AppendLine("PRODNO, ");//3
                            sb_detail.AppendLine("PROMOTION_CODE, ");//4
                            sb_detail.AppendLine("CREATE_USER, ");//5
                            sb_detail.AppendLine("CREATE_DTM, ");//6
                            sb_detail.AppendLine("MODI_USER, ");//7
                            sb_detail.AppendLine("MODI_DTM, ");//8
                            sb_detail.AppendLine("QUANTITY, ");//9
                            sb_detail.AppendLine("AMOUNT, ");//10
                            sb_detail.AppendLine("UNIT_PRICE, ");//10
                            sb_detail.AppendLine("POSUUID_DETAIL, ");//11
                            sb_detail.AppendLine("MSISDN ");//12
                            sb_detail.AppendLine(") VALUES(");
                            sb_detail.AppendLine("'" + POSUUID_DETAIL + "', ");//1
                            sb_detail.AppendLine("'" + i++ + "', ");//2
                            sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_detail["DEVICE_NO"].ToString()) + ", ");//3
                            //sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_head["PROMOTION_CODE"].ToString()) + ", ");
                            sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_detail["PROMOTION_CODE"].ToString()) + ", ");//4
                            sb_detail.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");//5
                            sb_detail.AppendLine("SYSDATE, ");//6
                            sb_detail.AppendLine(OracleDBUtil.SqlStr(Employee_Id) + ", ");//7
                            sb_detail.AppendLine("SYSDATE, ");//8
                            sb_detail.AppendLine("1, ");//9
                            sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_detail["PRICE"].ToString()) + ", ");//10
                            sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_detail["PRICE"].ToString()) + ", ");//10
                            sb_detail.AppendLine(OracleDBUtil.SqlStr(POS_UUID) + ",");//11
                            sb_detail.AppendLine(OracleDBUtil.SqlStr(dr_head["MSISDN"].ToString()));
                            sb_detail.AppendLine(")");

                            OracleDBUtil.ExecuteSql(objTX, sb_detail.ToString());
                            total_amount += Convert.ToDecimal(dr_detail["PRICE"].ToString());


                            //新增折扣
                            //InsertToCloseDiscount(dr_head["MSIDN"].ToString(), dr_head["NEWRATEPLANCODE"].ToString(), dr_detail["PROMOTION_CODE"].ToString(), dr_detail["DEVICE_NO"].ToString(), "", "", "", "", "", Employee_Id, "2", Store_Id, dr_detail["PRICE"].ToString(), Employee_Id, POSUUID_DETAIL, objTX);


                        }

                 
                        //更新總金額
                        StringBuilder sb_update = new StringBuilder();
                        sb_update.AppendFormat("UPDATE TO_CLOSE_HEAD set TOTAL_AMOUNT = {0} where POSUUID_DETAIL = '{1}'", total_amount.ToString(), POS_UUID);
                        OracleDBUtil.ExecuteSql(objTX, sb_update.ToString());
                        #region 計算折扣

                        CreateDiscount(POS_UUID, 1, objTX);

                        #endregion

                        objTX.Commit();
                        #endregion

                        #region SP_LOY_Update_POSUUID
                        OracleParameter errMsg = new OracleParameter();
                        errMsg.OracleType = OracleType.VarChar;
                        errMsg.Size = 200;
                        //errMsg.DbType = DbType.String;
                        //resultCursor.ParameterName = "outCursor";
                        errMsg.ParameterName = "msg";
                        errMsg.Direction = ParameterDirection.Output;

                        OracleTransaction objLYTX = objConn.BeginTransaction();


                        //寫LOG
                        _BeginToLog = string.Format("begin_to_call_SP_LOY_Update_POSUUID:image_number={0},vPOSuuid_Details={1}", Image_Number, POS_UUID);
                        //先將傳入參數寫入
                        this.WriteLog("OnLoad", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);

                        OracleDBUtil.ExecuteSql_SP(
                            objLYTX,
                            "LOYALTY.SP_LOY_Update_POSUUID",
                            new OracleParameter("image_number", Image_Number),
                            new OracleParameter("vPOSuuid_Details", POS_UUID),
                            errMsg
                            );

                        objLYTX.Commit();
                        #endregion

                        ReturnValue = POS_UUID;
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    objTX.Rollback();
                    throw ex;
                }
                finally
                {
                    objTX.Dispose();

                }
                #endregion
            }
            else if (Sys_Id == Service_Type.GetKey(2))
            {
                ReturnValue = this.ProcessSSI(Image_Number, Employee_Id, Store_Id, ReturnValue);
            }
            else if (Sys_Id == Service_Type.GetKey(3))
            {
                #region eStore service_type='10'
                OracleParameter o_POSUUID_DETAIL = new OracleParameter();
                o_POSUUID_DETAIL.OracleType = OracleType.VarChar;
                o_POSUUID_DETAIL.Size = 200;
                o_POSUUID_DETAIL.ParameterName = "o_POSUUID_DETAIL";
                o_POSUUID_DETAIL.Direction = ParameterDirection.Output;

                OracleParameter O_Result = new OracleParameter();
                O_Result.OracleType = OracleType.VarChar;
                O_Result.Size = 200;
                O_Result.ParameterName = "O_Result";
                O_Result.Direction = ParameterDirection.Output;

                OracleParameter O_Description = new OracleParameter();
                O_Description.OracleType = OracleType.VarChar;
                O_Description.Size = 200;
                O_Description.ParameterName = "O_Description";
                O_Description.Direction = ParameterDirection.Output;

                OracleConnection objConn = OracleDBUtil.GetConnection();
                OracleTransaction objTX = objTX = objConn.BeginTransaction();

                //寫LOG
                _BeginToLog = string.Format("begin_to_call_SP_ES_SOM2CLOSE:v_PACKAGE_NO={0},v_USER_ID={1}", Image_Number, Employee_Id);

                this.WriteLog("OnLoad", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);

                OracleDBUtil.ExecuteSql_SP(
                    objTX,
                    "SP_ES_SOM2CLOSE",
                    new OracleParameter("v_PACKAGE_NO", Image_Number),
                    new OracleParameter("v_USER_ID", Employee_Id),
                    o_POSUUID_DETAIL,
                    O_Result,
                    O_Description
                    );

                if (O_Result.Value.ToString() == "000")
                {
                    objTX.Commit();
                    ReturnValue = o_POSUUID_DETAIL.Value.ToString();
                }
                else
                {
                    objTX.Rollback();
                    ReturnValue = GetErrMsg(O_Description.Value.ToString(), Employee_Id);
                }
                #endregion
            }
            else
            {
                ReturnValue = GetErrMsg("Sys_Id error!", Employee_Id);
            }
        }
        catch (Exception ex)
        {
            //throw ex;
            return GetErrMsg(ex.Message, Employee_Id);
        }
        finally
        {
            if (conn_new_pos.State == ConnectionState.Open) conn_new_pos.Close();
            conn_new_pos.Dispose();
            OracleConnection.ClearAllPools();
        }

        this.WriteLog("LOG", OracleDBUtil.SqlStr("ReturnValue:" + ReturnValue), "WEB SERVICE", Employee_Id, 0);
        return ReturnValue;
    }

    //============================
    #region 處理 SSI 交易 service_type='4'
    private string ProcessSSI(string Image_Number, string Employee_Id, string Store_Id, string ReturnValue)
    {
        //取POS_UUID
        string POS_UUID = GuidNo.getUUID();

        //取得SSI連線
        OracleConnection ConnSource = null;
        OracleTransaction TransSource = null;
        OracleDataReader drSource = null;
        try
        {
            ConnSource = OracleDBUtil.GetSSIConnection();
            TransSource = ConnSource.BeginTransaction();
            //取得SSI QUERY MASTER 的資料
            drSource = GetSSIQueryMaster(Image_Number, Employee_Id, POS_UUID, TransSource);

            ReturnValue = this.ProcessSSIDetail(drSource, TransSource, POS_UUID, Image_Number, Employee_Id, Store_Id, ReturnValue, "");
        }
        catch (Exception ex)
        {
            ReturnValue = this.GetErrMsg(ex.Message, Employee_Id);
        }
        finally
        {
            drSource.Dispose();
            TransSource.Dispose();
            ConnSource.Dispose();
            OracleConnection.ClearAllPools();
        }

        return ReturnValue;
    }

    private string ProcessSSIDetail(OracleDataReader drSource, OracleTransaction TransSource, string POS_UUID
        , string Image_Number, string Employee_Id, string Store_Id, string ReturnValue, string FuncIdTest)
    {
        if (!drSource.HasRows)
        {
            TransSource.Rollback();
            throw new Exception("SP_SSI_QUERY_MASTER no data!");
        }

        //取得POS連線
        OracleConnection ConnPOS = null;
        OracleTransaction TransPOS = null;

        try
        {
            ConnPOS = OracleDBUtil.GetConnection();
            TransPOS = ConnPOS.BeginTransaction();

            while (drSource.Read())
            {
                #region 區域變數
                string strFuncId = drSource["FUND_ID"].ToString().Trim();

                //測試用
                if (FuncIdTest != string.Empty) strFuncId = FuncIdTest;

                int i = 1;
                string strPromotionCode = OracleDBUtil.SqlStr(drSource["PROMOTION_CODE"].ToString());
                string strEmployeeId = OracleDBUtil.SqlStr(Employee_Id);
                string strPosuuid = OracleDBUtil.SqlStr(POS_UUID);
                string strSourceType = "4";

                //計算總量
                string strTotoalAmount = string.Empty;
                int iPAmount = 0, iDAmount = 0, iRDAmount = 0;
                int.TryParse(drSource["PENALTY_AMOUNT"].ToString(), out iPAmount);
                int.TryParse(drSource["DEPOSIT_AMOUNT"].ToString(), out iDAmount);
                int.TryParse(drSource["RETURN_DEPOSIT_AMOUNT"].ToString(), out iRDAmount);
                strTotoalAmount = (iPAmount + iDAmount - Math.Abs(iRDAmount)).ToString();

                string strStoreName = "''";

                //For sales
                string strPosUUIDMaster = GuidNo.getUUID();

                //STORE_NAME
                string strSql = string.Format("SELECT S.STORENAME FROM STORE S WHERE S.STORE_NO={0}", OracleDBUtil.SqlStr(Store_Id));
                DataTable dt = OracleDBUtil.GetDataSet(TransPOS, strSql).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                    strStoreName = OracleDBUtil.SqlStr(dt.Rows[0][0].ToString());

                #region To_Close_Head Data
                ToCloseHeadDataObject objChd = new ToCloseHeadDataObject
                {
                    StoreId = Store_Id,
                    EmployeeId = strEmployeeId,
                    Posuuid = POS_UUID,
                    BundleId = drSource["BUNDLE_ID"].ToString(),
                    FundId = drSource["FUND_ID"].ToString(),
                    Data = drSource["DATA"].ToString(),
                    Voice = drSource["VOICE"].ToString(),
                    ApproveStatus = drSource["APPROVE_STATUS_POS"].ToString(),
                    ConnFlag = drSource["CONN_FLAG"].ToString(),
                    BundleType = drSource["BUNDLE_TYPE"].ToString(),
                    ImageNumber = Image_Number,
                    SubscriberId = drSource["SUBSCRIBER_ID"].ToString(),
                    AccountId = drSource["ACCOUNT_ID"].ToString(),
                    TotalAmount = strTotoalAmount,
                    StoreName = strStoreName,
                    ProcessId = strFuncId,
                    MSISDN = drSource["MSISDN"].ToString()
                };
                #endregion

                #region To_Close_Item Data
                ToCloseItemDataObject objCid = new ToCloseItemDataObject
                {
                    ItemId = string.Empty,
                    SeqNo = i,
                    ProdNo = string.Empty,
                    Amount = string.Empty,
                    PromoCode = strPromotionCode,
                    EmployeeId = strEmployeeId,
                    Quantity = "1",
                    PosuuidDetail = strPosuuid,
                    MSISDN = drSource["MSISDN"].ToString(),
                    SimCardNo = string.Empty,
                    ProcessId = strFuncId,
                    PosuuidHead = POS_UUID,
                    SourceType = strSourceType,
                    TotalAmount = strTotoalAmount,
                    PosuuidMaster = strPosUUIDMaster
                };
                #endregion

                bool doUpdateSSIImageData = true;
                #endregion

                #region 判斷 FuncID
                switch (strFuncId)
                {
                    //未結
                    case "150"://一退一租 (需審核)
                        //違約金, 收保證金
                        this.InsertToCloseItemAnalysis(objChd, objCid, TransPOS, drSource, false, true, true, true, false, false);
                        break;
                    case "11": //暫時停機
                        //違約金, 收保證金, 退保證金
                        this.InsertToCloseItemAnalysis(objCid, TransPOS, drSource, false, true, true, true, false, false);
                        break;
                    case "180"://保證金退現及查詢
                        //退保證金
                        this.InsertToCloseItemAnalysis(objCid, TransPOS, drSource, false, false, false, true, false, false);
                        break;
                    case "121"://合約資料-變更促代 (需審核)
                    case "123":
                        //因為要抓金額所以先Insert Head 方便 Update Total Amount
                        string ORG_POSUUID_MASTER = drSource["ORI_UUID"].ToString();
                        //讀取舊交易posuuid_detail

                        //如果沒有ORG_POSUUID_MASTER
                        if (!string.IsNullOrEmpty(ORG_POSUUID_MASTER))
                        {
                            this.get_old_posuuid_detail(ref POS_UUID, ORG_POSUUID_MASTER, objCid.MSISDN, TransPOS);


                            if (string.IsNullOrEmpty(POS_UUID))
                            {
                                throw new Exception(string.Format("無此舊交易,image_number:{0},ORI_UUID:{1},MSISDN:{2}", Image_Number, ORG_POSUUID_MASTER, objCid.MSISDN));
                            }
                            else
                            {
                                //判斷是否有其他的交易
                                string sqlstr = "select * from to_close_head where posuuid_master = " + OracleDBUtil.SqlStr(ORG_POSUUID_MASTER);
                                OracleCommand cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
                                bool result = false;
                                OracleDataReader dr = cmd.ExecuteReader();
                                result = dr.HasRows;
                                dr.Close();
                                if (result)
                                {
                                    throw new Exception(string.Format("上一筆交易未完成,image_number:{0},ORI_UUID:{1},MSISDN:{2}", Image_Number, ORG_POSUUID_MASTER, objCid.MSISDN));
                                }
                            }
                        }

                        objChd.Data = "Y";
                        objChd.Voice = "Y";
                        objChd.ApproveStatus = "nonapprove";
                        objChd.Posuuid = POS_UUID;
                        objCid.PosuuidHead = POS_UUID;
                        objCid.PosuuidDetail = OracleDBUtil.SqlStr(POS_UUID); ;
                        this.InsertToCloseHead(objChd, TransPOS);
                        this.UpdatePOSTUUID_MASTER(POS_UUID, ORG_POSUUID_MASTER, TransPOS);

                        //違約金, 物流ITEMS
                        this.InsertToCloseItemAnalysis(objChd, objCid, TransPOS, drSource, false, true, false, false, true, false);
                        break;
                    case "1": //提前解約
                    case "8": //加值服務高改低
                        //違約金
                        this.InsertToCloseItemAnalysis(objChd, objCid, TransPOS, drSource, false, true, false, false, false, false);
                        break;
                    #region 換卡相關, 如有取到 Sim 卡料號, 則不需進 TO_CLOSE_xxxx 只需進 SALE_xxxx; 但如無取到, 則進 TO_CLOSE_xxxx_LOG, 但不進 SALE_xxxx
                    case "12":	// 換卡
                    case "2":	// NonStop-換卡
                    case "505":	// PrePaid-換卡
                        {
                            string prodNo = string.Empty;
                            if (drSource["SIM_NO"].ToString() == string.Empty)
                                throw new Exception("來源資料無 SIM_NO 欄位資料");
                            else
                            {
                                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                                ht.Add("Result", drSource["MATERIAL_NO"].ToString());

                                this.WriteLog("SimQuery", string.Format("實際呼叫 WebService : {0}", ht["URL"]), "WEB SERVICE", Employee_Id, 0);
                                string paidType = (strFuncId == "505") ? "PrePaid" : "PostPaid";
                                if (ht["Result"].ToString() == "")
                                {
                                    //this.WriteLog("SimQuery", string.Format("查無 SIM 卡料號({0}):{1}", paidType, drSource["SIM_NO"]), "WEB SERVICE", Employee_Id, 0);
                                    prodNo = paidType + "SIM";
                                }
                                else
                                    prodNo = ht["Result"].ToString();

                                if (prodNo.Substring(prodNo.Length - 3) != "SIM")
                                {
                                    // 有 SIM 卡料號, 不進 TO_CLOSE_xxxx, 直接進 SALE 結帳
                                    string sale_no = string.Format(SerialNo.GenNo("SALE"), Store_Id, "01");
                                    //Insert CLOSE LOG
                                    objCid.ItemId = GuidNo.getUUID();
                                    objCid.ProdNo = prodNo;
                                    objCid.Amount = strTotoalAmount;

                                    objCid.SimCardNo = drSource["SIM_NO"].ToString();


                                    this.InsertToCloseLog(objChd, objCid, TransPOS);
                                    // Insert SalesHead
                                    InsertSalesHead(Store_Id, strEmployeeId, strStoreName, strPosUUIDMaster, sale_no, (ht["Result"].ToString().Length == 0) ? "1" : "2", TransPOS);


                                    // Insert SalesDetail
                                    string detailId = InsertSalesDetail(POS_UUID, drSource,
                                        objCid.SeqNo, Employee_Id, Image_Number, strSourceType, strTotoalAmount,
                                        strPosUUIDMaster, OracleDBUtil.SqlStr(prodNo), TransPOS);
                                    // 扣庫存
                                    BuckleInventory(TransPOS, Store_Id, sale_no, Employee_Id, prodNo, detailId);

                                    UpdateSSIImageData(Image_Number, POS_UUID, Employee_Id, TransSource);
                                    doUpdateSSIImageData = false;

                                    CommitOuterSystem(Image_Number, strPosUUIDMaster, Image_Number, Employee_Id);
                                }
                                else
                                {
                                    // 沒有 SIM 卡料號, 不進 SALE, 但需進 TO_CLOSE_xxxx_LOG
                                    objCid.ItemId = GuidNo.getUUID();
                                    objCid.ProdNo = prodNo;
                                    objCid.Amount = strTotoalAmount;
                                    objCid.MSISDN = drSource["MSISDN"].ToString();
                                    objCid.SimCardNo = drSource["SIM_NO"].ToString();
                                    objCid.ProcessId = prodNo + "-MissProdNo";

                                    InsertToCloseLog(objChd, objCid, TransPOS);
                                }

                                ht.Clear();
                                ht = null;
                            }
                        }
                        break;
                    #endregion

                    //換貨 --  
                    case "122"://合約資料-取消促代 (需審核) 
                    case "124":

                        //讀取舊交易posuuid_detail
                        if (!string.IsNullOrEmpty(drSource["ORI_UUID"].ToString()))
                        {
                            this.get_old_posuuid_detail(ref POS_UUID, drSource["ORI_UUID"].ToString(), objCid.MSISDN, TransPOS);


                            if (string.IsNullOrEmpty(POS_UUID))
                            {
                                throw new Exception(string.Format("無此舊交易,image_number:{0},ORI_UUID:{1},MSISDN:{2}", Image_Number, drSource["ORI_UUID"].ToString(), objCid.MSISDN));
                            }
                            else
                            {
                                //判斷是否有其他的交易
                                string sqlstr = "select * from to_close_head where posuuid_master = " + OracleDBUtil.SqlStr(drSource["ORI_UUID"].ToString());
                                OracleCommand cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
                                bool result = false;
                                OracleDataReader dr = cmd.ExecuteReader();
                                result = dr.HasRows;
                                dr.Close();
                                if (result)
                                {
                                    throw new Exception(string.Format("上一筆交易未完成,image_number:{0},ORI_UUID:{1},MSISDN:{2}", Image_Number, drSource["ORI_UUID"].ToString(), objCid.MSISDN));
                                }
                            }
                            objChd.Posuuid = POS_UUID;
                            this.InsertToCloseHead(objChd, TransPOS);
                            this.UpdatePOSTUUID_MASTER(POS_UUID, drSource["ORI_UUID"].ToString(), TransPOS);
                        }
                        else
                        {
                            throw new Exception(string.Format("無ORI_UUID,image_number:{0},ORI_UUID:{1},MSISDN:{2}", Image_Number, drSource["ORI_UUID"].ToString(), objCid.MSISDN));
                        }


                        break;
                }

                #region INSERT INTO TO_CLOSE_HEAD
                switch (objChd.ProcessId)
                {
                    case "150":

                        objChd.Data = "Y";
                        objChd.Voice = "Y";
                        objChd.ApproveStatus = "nonapprove";
                        this.InsertToCloseHead(objChd, TransPOS);
                        break;
                    case "11":
                    case "180":
                    case "1":
                    case "8":
                        this.InsertToCloseHead(objChd, TransPOS);
                        break;
                    // 換卡不需進 TO_CLOSE_xxxx 只需進 Sales_xxxx
                    case "12":
                    case "2":
                    case "505":
                    case "121":
                    case "123":
                        break;
                }
                #endregion

                #endregion

                #region Update SSI image data
                if (doUpdateSSIImageData)
                    UpdateSSIImageData(Image_Number, POS_UUID, Employee_Id, TransSource);
                #endregion
            }

            TransSource.Commit();
            TransPOS.Commit();
            drSource.Close();
        }
        catch (Exception ex)
        {
            TransSource.Rollback();
            TransPOS.Rollback();
            throw ex;
        }
        finally
        {
            drSource.Dispose();
            TransPOS.Dispose();
            ConnPOS.Dispose();
            OracleConnection.ClearAllPools();
        }

        ReturnValue = POS_UUID;

        return ReturnValue;
    }

    #region Get SSI Query Master
    private OracleDataReader GetSSIQueryMaster(string Image_Number, string Employee_Id, string POS_UUID, OracleTransaction TransSource)
    {
        OracleParameter resultCursor = new OracleParameter()
        {
            OracleType = OracleType.Cursor,
            ParameterName = "resultCur",
            Direction = ParameterDirection.Output
        };

        try
        {
            //寫LOG，先將傳入參數寫入
            _BeginToLog = string.Format("begin_to_call_SP_SSI_QUERY_MASTER:actiOno={0},POSuuid_Details={1}", Image_Number, POS_UUID);
            this.WriteLog("OnLoad", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);

            //呼叫 SP_SSI_QUERY_MASTER
            OracleDBUtil.ExecuteSql_SP(TransSource, "SP_SSI_QUERY_MASTER", new OracleParameter("ActiONO", Image_Number), new OracleParameter("POSuuid_details", POS_UUID), resultCursor);

            _BeginToLog = "end_to_call_SP_SSI_QUERY_MASTER";
            this.WriteLog("Finish", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);

        }
        catch (Exception ex)
        {
            TransSource.Rollback();
            throw ex;
        }

        OracleDataReader drSource = (OracleDataReader)resultCursor.Value;
        return drSource;
    }

    #endregion

    #endregion

    #region Insert Into TO_CLOSE_xxxx_LOG
    private void InsertToCloseLog(ToCloseHeadDataObject chd, ToCloseItemDataObject cid, OracleTransaction TransPOS)
    {

        #region Insert TO_CLOSE_HEAD_LOG Data
        StringBuilder sb_head = new StringBuilder();
        List<string> lstColumnsCH = new List<string>();
        List<string> lstValueCH = new List<string>();
        lstColumnsCH.AddRange(new string[]
        {   
            "STATUS",		"APPLY_DATE",	"STORE_NO",			"SERVICE_TYPE",		"SALE_PERSON",
			"CREATE_USER",	"CREATE_DTM",	"MODI_USER",		"MODI_DTM",			"POSUUID_DETAIL",
			"BOUNDLE_ID",	"FUN_ID",		"DATA",				"VOICE",			"APPROVE_STATUS_POS",	
			"CONN_FLAG",	"BOUNDLE_TYPE",	"SERVICE_SYS_ID",	"SUBSCRIBE_NO",		"BILLING_ACCOUNT_ID",
			"TOTAL_AMOUNT", "STORE_NAME",	"ID"
        });
        lstValueCH.AddRange(new string[]
        {
            "'1'", 
			OracleDBUtil.DateStr(DateTime.Today.ToString("yyyy/MM/dd")),
            OracleDBUtil.SqlStr(chd.StoreId), 
			"'4'", 
			chd.EmployeeId, 
			chd.EmployeeId,
            "SYSDATE", 
			chd.EmployeeId, 
			"SYSDATE", 
			OracleDBUtil.SqlStr(chd.Posuuid),
            OracleDBUtil.SqlStr(chd.BundleId), 
			OracleDBUtil.SqlStr(chd.FundId),
            OracleDBUtil.SqlStr(chd.Data), 
			OracleDBUtil.SqlStr(chd.Voice),
            OracleDBUtil.SqlStr(chd.ApproveStatus), 
			OracleDBUtil.SqlStr(chd.ConnFlag),
            OracleDBUtil.SqlStr(chd.BundleType), 
			OracleDBUtil.SqlStr(chd.ImageNumber),
            OracleDBUtil.SqlStr(chd.SubscriberId), 
			OracleDBUtil.SqlStr(chd.AccountId),
            OracleDBUtil.SqlStr(chd.TotalAmount), 
			chd.StoreName,
            OracleDBUtil.SqlStr(GuidNo.getUUID())
        });
        this.GetSqlInsertItem(sb_head, "TO_CLOSE_HEAD_LOG", lstColumnsCH, lstValueCH);
        this.ExecuteSql(TransPOS, sb_head, sb_head.ToString(), chd.EmployeeId, chd.ProcessId);
        #endregion

        #region Insert TO_CLOSE_ITEM_LOG Data
        StringBuilder sb_item = new StringBuilder();
        List<string> lstColumns = new List<string>
        {   
            "ID",				"SEQNO",		"PRODNO",		"AMOUNT",	"PROMOTION_CODE",
			"CREATE_USER",		"CREATE_DTM",	"MODI_USER",	"MODI_DTM",	"QUANTITY",
			"POSUUID_DETAIL",	"MSISDN",		"SIM_CARD_NO", "UNIT_PRICE"
        };

        List<string> lstValue = new List<string>
        {
            OracleDBUtil.SqlStr(cid.ItemId), 
			string.Format("'{0}'", cid.SeqNo++),
            OracleDBUtil.SqlStr(cid.ProdNo), 
			OracleDBUtil.SqlStr(cid.Amount),
            cid.PromoCode, 
			cid.EmployeeId, 
			"SYSDATE", 
			cid.EmployeeId, 
			"SYSDATE", 
            OracleDBUtil.SqlStr(cid.Quantity), 
			cid.PosuuidDetail,
            OracleDBUtil.SqlStr(cid.MSISDN), 
			OracleDBUtil.SqlStr(cid.SimCardNo),
            OracleDBUtil.SqlStr(cid.Amount)
        };
        this.GetSqlInsertItem(sb_item, "TO_CLOSE_ITEM_LOG", lstColumns, lstValue);
        this.ExecuteSql(TransPOS, sb_item, sb_item.ToString(), cid.EmployeeId, cid.ProcessId);
        #endregion

    }
    #endregion

    #region Analysis And Insert TO_CLOSE_ITEM
    private void InsertToCloseItemAnalysis(ToCloseItemDataObject CID, OracleTransaction TransPOS, OracleDataReader drSource,
       bool IsNewSimNo, bool IsPenalty, bool IsDeposit, bool IsReturnDeposit, bool IsItem, bool IsSales)
    {
        string strProcId = CID.ProcessId;

        //違約金700200008
        if (IsPenalty && drSource["PENALTY_AMOUNT"].ToString() != "" && drSource["PENALTY_AMOUNT"].ToString() != "0")
        {
            string penaltyNo = System.Configuration.ConfigurationManager.AppSettings["PenaltyNo"].ToString();

            CID.ItemId = GuidNo.getUUID();
            CID.ProdNo = penaltyNo;
            CID.Amount = drSource["PENALTY_AMOUNT"].ToString();
            CID.MSISDN = drSource["MSISDN"].ToString();
            CID.SimCardNo = string.Empty;
            CID.ProcessId = strProcId + "-" + penaltyNo;
            this.InsertToCloseItem(CID, TransPOS);

            if (IsSales)
            {
                this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                    CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                    OracleDBUtil.SqlStr(penaltyNo), TransPOS);

            }

        }

        //收保證金700210001
        if (IsDeposit && drSource["DEPOSIT_AMOUNT"].ToString() != "" && drSource["DEPOSIT_AMOUNT"].ToString() != "0")
        {
            string depositNo = System.Configuration.ConfigurationManager.AppSettings["DepositNo"].ToString();

            CID.ItemId = GuidNo.getUUID();
            CID.ProdNo = depositNo;
            CID.Amount = drSource["DEPOSIT_AMOUNT"].ToString();
            CID.MSISDN = drSource["MSISDN"].ToString();
            CID.SimCardNo = string.Empty;
            CID.ProcessId = strProcId + "-" + depositNo;
            this.InsertToCloseItem(CID, TransPOS);

            if (IsSales)
            {
                this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                    CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                    OracleDBUtil.SqlStr(depositNo), TransPOS);
            }
        }

        //退保證金700210001
        if (IsReturnDeposit && drSource["RETURN_DEPOSIT_AMOUNT"].ToString() != "" && drSource["RETURN_DEPOSIT_AMOUNT"].ToString() != "0")
        {
            string returnDepositNo = System.Configuration.ConfigurationManager.AppSettings["ReturnDepositNo"].ToString();

            CID.ItemId = GuidNo.getUUID();
            CID.ProdNo = returnDepositNo;
            CID.Amount = "-" + drSource["RETURN_DEPOSIT_AMOUNT"].ToString();
            CID.MSISDN = drSource["MSISDN"].ToString();
            CID.SimCardNo = string.Empty;
            CID.ProcessId = strProcId + "-" + returnDepositNo + "-2";
            this.InsertToCloseItem(CID, TransPOS);

            if (IsSales)
            {
                this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                    CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                    OracleDBUtil.SqlStr(returnDepositNo), TransPOS);
            }
        }

        //物流ITEMS
        if (IsItem)
        {
            int j = 1;
            foreach (string prodNo in drSource["ITEMS"].ToString().Split('|'))
            {
                string item_uuid = GuidNo.getUUID();
                #region 抓取金額
                string sqlstr = string.Format("select FN_QUERY_PROMO_AMOUNT({0},'{1}','{2}') from dual", CID.PromoCode, prodNo, j.ToString());
                j++;
                DataTable dt_amount = OracleDBUtil.GetDataSet(TransPOS, sqlstr).Tables[0];
                decimal amount = 0;
                if (dt_amount.Rows.Count > 0) amount = Convert.ToDecimal(dt_amount.Rows[0][0]);
                #endregion
                CID.ItemId = item_uuid;
                CID.ProdNo = prodNo;
                CID.Amount = amount.ToString();
                CID.MSISDN = drSource["MSISDN"].ToString();
                CID.SimCardNo = string.Empty;
                CID.ProcessId = strProcId + "-items";
                this.InsertToCloseItem(CID, TransPOS);
                //因為這邊才抓金額所以還要update回去
                this.UpdateToCloseHeadTotalAmount(CID, TransPOS);

                if (IsSales)
                {
                    this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                        CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                        OracleDBUtil.SqlStr(prodNo), TransPOS);
                }
            }
        }
    }

    private void InsertToCloseItemAnalysis(ToCloseHeadDataObject CHD, ToCloseItemDataObject CID, OracleTransaction TransPOS, OracleDataReader drSource,
     bool IsNewSimNo, bool IsPenalty, bool IsDeposit, bool IsReturnDeposit, bool IsItem, bool IsSales)
    {
        string strProcId = CID.ProcessId;

        //違約金700200008
        if (IsPenalty && drSource["PENALTY_AMOUNT"].ToString() != "" && drSource["PENALTY_AMOUNT"].ToString() != "0")
        {
            string penaltyNo = System.Configuration.ConfigurationManager.AppSettings["PenaltyNo"].ToString();

            CID.ItemId = GuidNo.getUUID();
            CID.ProdNo = penaltyNo;
            CID.Amount = drSource["PENALTY_AMOUNT"].ToString();
            CID.MSISDN = drSource["MSISDN"].ToString();
            CID.SimCardNo = string.Empty;
            CID.ProcessId = strProcId + "-" + penaltyNo;
            this.InsertToCloseItem(CID, TransPOS);
            //this.InsertToCloseDiscount(CHD, CID, TransPOS);
            if (IsSales)
            {
                this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                    CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                    OracleDBUtil.SqlStr(penaltyNo), TransPOS);

            }

        }

        //收保證金700210001
        if (IsDeposit && drSource["DEPOSIT_AMOUNT"].ToString() != "" && drSource["DEPOSIT_AMOUNT"].ToString() != "0")
        {
            string depositNo = System.Configuration.ConfigurationManager.AppSettings["DepositNo"].ToString();

            CID.ItemId = GuidNo.getUUID();
            CID.ProdNo = depositNo;
            CID.Amount = drSource["DEPOSIT_AMOUNT"].ToString();
            CID.MSISDN = drSource["MSISDN"].ToString();
            CID.SimCardNo = string.Empty;
            CID.ProcessId = strProcId + "-" + depositNo;
            this.InsertToCloseItem(CID, TransPOS);
            //this.InsertToCloseDiscount(CHD, CID, TransPOS);
            if (IsSales)
            {
                this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                    CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                    OracleDBUtil.SqlStr(depositNo), TransPOS);
            }
        }

        //退保證金700210001
        if (IsReturnDeposit && drSource["RETURN_DEPOSIT_AMOUNT"].ToString() != "" && drSource["RETURN_DEPOSIT_AMOUNT"].ToString() != "0")
        {
            string returnDepositNo = System.Configuration.ConfigurationManager.AppSettings["ReturnDepositNo"].ToString();

            CID.ItemId = GuidNo.getUUID();
            CID.ProdNo = returnDepositNo;
            CID.Amount = "-" + drSource["RETURN_DEPOSIT_AMOUNT"].ToString();
            CID.MSISDN = drSource["MSISDN"].ToString();
            CID.SimCardNo = string.Empty;
            CID.ProcessId = strProcId + "-" + returnDepositNo + "-2";
            this.InsertToCloseItem(CID, TransPOS);
            //this.InsertToCloseDiscount(CHD, CID, TransPOS);
            if (IsSales)
            {
                this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                    CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                    OracleDBUtil.SqlStr(returnDepositNo), TransPOS);
            }
        }

        //預繳金PREPAID_AMOUNT
        if (CHD.FundId == "150" && drSource["PREPAID_AMOUNT"].ToString() != "" && drSource["PREPAID_AMOUNT"].ToString() != "0")
        {
            string returnPrepaidNo = "700200029";//System.Configuration.ConfigurationManager.AppSettings["ReturnDepositNo"].ToString();

            CID.ItemId = GuidNo.getUUID();
            CID.ProdNo = returnPrepaidNo;
            CID.Amount = "-" + drSource["RETURN_DEPOSIT_AMOUNT"].ToString();
            CID.MSISDN = drSource["MSISDN"].ToString();
            CID.SimCardNo = string.Empty;
            CID.ProcessId = strProcId + "-" + returnPrepaidNo + "-2";
            this.InsertToCloseItem(CID, TransPOS);
            //this.InsertToCloseDiscount(CHD, CID, TransPOS);
            if (IsSales)
            {
                this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                    CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                    OracleDBUtil.SqlStr(returnPrepaidNo), TransPOS);
            }
        }

        //物流ITEMS
        if (IsItem)
        {
            int j = 1;
            foreach (string prodNo in drSource["ITEMS"].ToString().Split('|'))
            {
                string item_uuid = GuidNo.getUUID();
                string p_no = prodNo;
                decimal amount = 0;
                if (!string.IsNullOrEmpty(prodNo.Trim()))
                {

                    #region 抓取金額
                    string sqlstr = string.Format("select FN_QUERY_PROMO_AMOUNT({0},'{1}','{2}') from dual", CID.PromoCode, prodNo, j.ToString());
                    j++;
                    DataTable dt_amount = OracleDBUtil.GetDataSet(TransPOS, sqlstr).Tables[0];

                    if (dt_amount.Rows.Count > 0)
                    {
                        if (dt_amount.Rows[0][0] != DBNull.Value)
                        {
                            amount = Convert.ToDecimal(dt_amount.Rows[0][0]);
                        }
                        else
                        {
                            amount = -1;
                        }
                    }
                    #endregion
                }
                else
                {
                    p_no = "80020001";
                }
                CID.ItemId = item_uuid;
                CID.ProdNo = p_no;
                CID.Amount = amount.ToString();
                CID.MSISDN = drSource["MSISDN"].ToString();
                CID.SimCardNo = string.Empty;
                CID.ProcessId = strProcId + "-items";
                this.InsertToCloseItem(CID, TransPOS);
                //this.InsertToCloseDiscount(CHD, CID, TransPOS);
                //因為這邊才抓金額所以還要update回去
                this.UpdateToCloseHeadTotalAmount(CID, TransPOS);

                if (IsSales)
                {
                    this.InsertSalesDetail(CID.PosuuidHead, drSource, CID.SeqNo, CID.EmployeeId,
                        CID.PosuuidDetail, CID.SourceType, CID.TotalAmount, CID.PosuuidMaster,
                        OracleDBUtil.SqlStr(prodNo), TransPOS);
                }
            }
        }
    }
    #endregion

    #region Update SSI image data
    private void UpdateSSIImageData(string Image_Number, string POS_UUID, string Employee_Id, OracleTransaction TransSource)
    {
        _BeginToLog = string.Format("begin_to_call_SP_SSI_UPDATE_POSUUID:Image_Number={0},POSuuid_Details={1}", Image_Number, POS_UUID);
        this.WriteLog("OnLoad", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);

        OracleParameter errMsg = new OracleParameter()
        {
            OracleType = OracleType.VarChar,
            Size = 200,
            ParameterName = "result",
            Direction = ParameterDirection.Output
        };
        OracleDBUtil.ExecuteSql_SP(TransSource, "SP_SSI_UPDATE_POSUUID", new OracleParameter("Image_Number", Image_Number), new OracleParameter("POSuuid_Details", POS_UUID), errMsg);

        _BeginToLog = string.Format("end_to_call_SP_SSI_UPDATE_POSUUID:Image_Number={0},POSuuid_Details={1}", Image_Number, POS_UUID);
        this.WriteLog("FINISH", OracleDBUtil.SqlStr(_BeginToLog), "WEB SERVICE", Employee_Id, 0);

    }
    #endregion

    //============================
    #region 寫入 TO_CLOSE 相關資料表
    private void InsertToCloseItem(ToCloseItemDataObject CID, OracleTransaction TransPOS)
    {
        StringBuilder sb_item = new StringBuilder();
        List<string> lstColumns = new List<string>
        {   
            "ID",				"SEQNO",		"PRODNO",		"AMOUNT",	"PROMOTION_CODE",
			"CREATE_USER",		"CREATE_DTM",	"MODI_USER",	"MODI_DTM",	"QUANTITY",
			"POSUUID_DETAIL",	"MSISDN",		"SIM_CARD_NO", "UNIT_PRICE"
        };

        List<string> lstValue = new List<string>
        {
            OracleDBUtil.SqlStr(CID.ItemId), 
			string.Format("'{0}'", CID.SeqNo++),
            OracleDBUtil.SqlStr(CID.ProdNo), 
			OracleDBUtil.SqlStr(CID.Amount),
            CID.PromoCode, 
			CID.EmployeeId, 
			"SYSDATE", 
			CID.EmployeeId, 
			"SYSDATE", 
            OracleDBUtil.SqlStr(CID.Quantity), 
			CID.PosuuidDetail,
            OracleDBUtil.SqlStr(CID.MSISDN), 
			OracleDBUtil.SqlStr(CID.SimCardNo),
            OracleDBUtil.SqlStr(CID.Amount)
        };
        this.GetSqlInsertItem(sb_item, "TO_CLOSE_ITEM", lstColumns, lstValue);
        this.ExecuteSql(TransPOS, sb_item, sb_item.ToString(), CID.EmployeeId, CID.ProcessId);
    }

    private void InsertToCloseHead(ToCloseHeadDataObject CHD, OracleTransaction TransPOS)
    {
        StringBuilder sb_head = new StringBuilder();
        List<string> lstColumnsCH = new List<string>();
        List<string> lstValueCH = new List<string>();
        lstColumnsCH.AddRange(new string[]
        {   
            "STATUS",		"APPLY_DATE",	"STORE_NO",			"SERVICE_TYPE",		"SALE_PERSON",
			"CREATE_USER",	"CREATE_DTM",	"MODI_USER",		"MODI_DTM",			"POSUUID_DETAIL",
			"BUNDLE_ID",	"FUN_ID",		"DATA",				"VOICE",			"APPROVE_STATUS_POS",	
			"CONN_FLAG",	"BUNDLE_TYPE",	"SERVICE_SYS_ID",	"SUBSCRIBE_NO",		"BILLING_ACCOUNT_ID",
			"TOTAL_AMOUNT", "STORE_NAME","MSISDN"
        });
        lstValueCH.AddRange(new string[]
        {
            "'1'", 
			OracleDBUtil.DateStr(DateTime.Today.ToString("yyyy/MM/dd")),
            OracleDBUtil.SqlStr(CHD.StoreId), 
			"'4'", 
			CHD.EmployeeId, 
			CHD.EmployeeId,
            "SYSDATE", 
			CHD.EmployeeId, 
			"SYSDATE", 
			OracleDBUtil.SqlStr(CHD.Posuuid),
            OracleDBUtil.SqlStr(CHD.BundleId), 
			OracleDBUtil.SqlStr(CHD.FundId),
            OracleDBUtil.SqlStr(CHD.Data), 
			OracleDBUtil.SqlStr(CHD.Voice),
            OracleDBUtil.SqlStr(CHD.ApproveStatus), 
			OracleDBUtil.SqlStr(CHD.ConnFlag),
            OracleDBUtil.SqlStr(CHD.BundleType), 
			OracleDBUtil.SqlStr(CHD.ImageNumber),
            OracleDBUtil.SqlStr(CHD.SubscriberId), 
			OracleDBUtil.SqlStr(CHD.AccountId),
            OracleDBUtil.SqlStr(CHD.TotalAmount), 
			CHD.StoreName,
            OracleDBUtil.SqlStr(CHD.MSISDN)
        });
        this.GetSqlInsertItem(sb_head, "TO_CLOSE_HEAD", lstColumnsCH, lstValueCH);
        this.ExecuteSql(TransPOS, sb_head, sb_head.ToString(), CHD.EmployeeId, CHD.ProcessId);
    }

    private void UpdateToCloseHeadTotalAmount(ToCloseItemDataObject CID, OracleTransaction TransPOS)
    {
        string sqlstr = string.Format("update TO_CLOSE_HEAD h set total_amount  = total_amount + {0} where POSUUID_DETAIL ='{1}' ", CID.Amount, CID.PosuuidHead);
        OracleCommand cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        cmd.ExecuteNonQuery();
    }

    private void get_old_posuuid_detail(ref string posuuid, string POSUUID_MASTER, string MSISDN, OracleTransaction TransPOS)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select posuuid_detail ");
        sb.Append(" from SALE_DETAIL ");
        sb.Append(" where ");
        sb.AppendFormat(" MSISDN = {0} ", OracleDBUtil.SqlStr(MSISDN));
        sb.AppendFormat(" AND POSUUID_MASTER = {0} ", OracleDBUtil.SqlStr(POSUUID_MASTER));
        OracleCommand cmd = new OracleCommand(sb.ToString(), TransPOS.Connection, TransPOS);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            posuuid = dr.GetString(0);
        }
        else
        {
            posuuid = "";
        }
        dr.Close();
    }

    private void UpdatePOSTUUID_MASTER(string posuuid, string POSUUID_MASTER, OracleTransaction TransPOS)
    {

        string sqlstr = string.Format("update TO_CLOSE_HEAD h set POSUUID_MASTER  = '{0}' where POSUUID_DETAIL ='{1}' ", POSUUID_MASTER, posuuid);
        OracleCommand cmd = new OracleCommand(sqlstr, TransPOS.Connection, TransPOS);
        cmd.ExecuteNonQuery();

    }

    /// <summary>
    /// 寫入未結折扣
    /// </summary>
    /// <param name="CID"></param>
    /// <param name="TransPOS"></param>
    private void InsertToCloseDiscount(string msisdn, string r_rate, string promotion_code, string item_code, string data, string voice, string trans_type,
        string mnp, string bundle_type, string employee_id, string sys_id, string store_id, string amount, string MODI_USER, string POSUUID_DETAIL, OracleTransaction TransPOS)
    {

        #region 讀取Discount Master
        OracleCommand discountCmd = new OracleCommand("sp_query_discount_ws", TransPOS.Connection, TransPOS);
        discountCmd.CommandType = CommandType.StoredProcedure;
        discountCmd.Parameters.Add("v_msisdn", OracleType.NVarChar).Value = msisdn;
        discountCmd.Parameters.Add("v_r_rate", OracleType.NVarChar).Value = r_rate;
        discountCmd.Parameters.Add("v_promotion_code", OracleType.NVarChar).Value = promotion_code;
        discountCmd.Parameters.Add("v_item_code", OracleType.NVarChar).Value = item_code;
        discountCmd.Parameters.Add("v_data", OracleType.NVarChar).Value = data;
        discountCmd.Parameters.Add("v_voice", OracleType.NVarChar).Value = voice;
        discountCmd.Parameters.Add("v_trans_type", OracleType.NVarChar).Value = trans_type;
        discountCmd.Parameters.Add("v_mnp", OracleType.NVarChar).Value = mnp;
        discountCmd.Parameters.Add("v_bundle_type", OracleType.NVarChar).Value = bundle_type;
        discountCmd.Parameters.Add("v_employee_id", OracleType.NVarChar).Value = employee_id;
        discountCmd.Parameters.Add("v_sys_id", OracleType.NVarChar).Value = sys_id;
        discountCmd.Parameters.Add("v_store_id", OracleType.NVarChar).Value = store_id;
        discountCmd.Parameters.Add("o_data", OracleType.Cursor).Direction = ParameterDirection.Output;
        discountCmd.Parameters.Add("v_msgcode", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
        discountCmd.Parameters.Add("v_message", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
        discountCmd.ExecuteNonQuery();
        string v_msgcode = discountCmd.Parameters["v_msgcode"].Value.ToString();
        string v_message = discountCmd.Parameters["v_message"].Value.ToString();
        OracleDataReader dr = (OracleDataReader)discountCmd.Parameters["o_data"].Value;
        DataTable discountDt = new DataTable();
        discountDt.Columns.Add("discount_code");
        discountDt.Columns.Add("discount_name");
        discountDt.Columns.Add("DISCOUNT_MONEY");
        discountDt.Columns.Add("DISCOUNT_RATE");

        discountDt.Columns.Add("S_DATE");
        discountDt.Columns.Add("E_DATE");
        if (v_msgcode == "000")
        {
            while (dr.Read())
            {
                DataRow row = discountDt.NewRow();
                row["discount_code"] = dr[0].ToString();
                row["discount_name"] = dr[1].ToString();
                row["DISCOUNT_MONEY"] = dr[2].ToString();
                row["DISCOUNT_RATE"] = dr[3].ToString();
                row["S_DATE"] = dr[4].ToString();
                row["E_DATE"] = dr[5].ToString();
                discountDt.Rows.Add(row);
            }
            dr.Close();
        }
        else
        {
            throw new Exception(v_message);
        }
        #endregion

        #region Insert To_Close_Discount
        StringBuilder sb_item = new StringBuilder();
        sb_item.Append(" insert into TO_CLOSE_DISCOUNT (");
        sb_item.Append(" ID,");
        sb_item.Append(" SEQNO,");
        sb_item.Append(" DISCOUNT_ID,");
        sb_item.Append(" DISCOUNT_PRICE,");
        sb_item.Append(" DISCOUNT_AMOUNT,");
        sb_item.Append(" DISCOUNT_B_DATE,");
        sb_item.Append(" CREATE_USER,");
        sb_item.Append(" CREATE_DTM,");
        sb_item.Append(" MODI_USER,");
        sb_item.Append(" MODI_DTM,");
        sb_item.Append(" POSUUID_DETAIL,");
        sb_item.Append(" DISCOUNT_E_DATE ");
        sb_item.Append(" )VALUES(");
        sb_item.Append(" POS_UUID(),");
        sb_item.Append(" :SEQNO,");
        sb_item.Append(" :DISCOUNT_ID,");
        sb_item.Append(" :DISCOUNT_PRICE,");
        sb_item.Append(" :DISCOUNT_AMOUNT,");
        sb_item.Append(" :DISCOUNT_B_DATE,");
        sb_item.Append(" :CREATE_USER,");
        sb_item.Append(" SYSDATE,");
        sb_item.Append(" :MODI_USER,");
        sb_item.Append(" SYSDATE,");
        sb_item.Append(" :POSUUID_DETAIL,");
        sb_item.Append(" :DISCOUNT_E_DATE");
        sb_item.Append(")");

        OracleCommand cmd = new OracleCommand(sb_item.ToString(), TransPOS.Connection, TransPOS);
        cmd.Parameters.Add(":SEQNO", OracleType.VarChar, 32);
        cmd.Parameters.Add(":DISCOUNT_ID", OracleType.VarChar, 50);
        cmd.Parameters.Add(":DISCOUNT_PRICE", OracleType.Number);
        cmd.Parameters.Add(":DISCOUNT_AMOUNT", OracleType.Number);
        cmd.Parameters.Add(":DISCOUNT_B_DATE", OracleType.DateTime);
        cmd.Parameters.Add(":CREATE_USER", OracleType.VarChar, 50).Value = MODI_USER;
        cmd.Parameters.Add(":DISCOUNT_E_DATE", OracleType.DateTime);
        cmd.Parameters.Add(":MODI_USER", OracleType.VarChar, 50).Value = MODI_USER;
        cmd.Parameters.Add(":POSUUID_DETAIL", OracleType.VarChar, 32).Value = POSUUID_DETAIL;


        int i = 1;
        foreach (DataRow row in discountDt.Rows)
        {

            cmd.Parameters[":SEQNO"].Value = i++;
            cmd.Parameters[":DISCOUNT_ID"].Value = row["discount_code"].ToString();
            if (row["DISCOUNT_MONEY"].ToString() == "0")
            {
                double price = Convert.ToDouble(row["DISCOUNT_MONEY"]);
                cmd.Parameters[":DISCOUNT_PRICE"].Value = price;
                cmd.Parameters[":DISCOUNT_AMOUNT"].Value = price;
            }
            else
            {
                double rate = Convert.ToDouble(row["DISCOUNT_RATE"]) * 0.01;
                double t_amount = Convert.ToDouble(amount);
                cmd.Parameters[":DISCOUNT_PRICE"].Value = t_amount * rate * -1;
                cmd.Parameters[":DISCOUNT_AMOUNT"].Value = t_amount * rate * -1;
            }

            cmd.Parameters[":DISCOUNT_B_DATE"].Value = row["S_DATE"];
            cmd.Parameters[":DISCOUNT_E_DATE"].Value = row["E_DATE"];
            cmd.ExecuteNonQuery();
        }
        #endregion

    }

    /// <summary>
    /// 寫入未結折扣
    /// </summary>
    /// <param name="CID"></param>
    /// <param name="TransPOS"></param>
    private void InsertToCloseDiscount(ToCloseHeadDataObject CHD, ToCloseItemDataObject CID, OracleTransaction TransPOS)
    {

        #region 讀取Discount Master
        OracleCommand discountCmd = new OracleCommand("sp_query_discount_ws", TransPOS.Connection, TransPOS);
        discountCmd.CommandType = CommandType.StoredProcedure;
        discountCmd.Parameters.Add("v_msisdn", OracleType.NVarChar).Value = CID.MSISDN;
        discountCmd.Parameters.Add("v_r_rate", OracleType.NVarChar).Value = CHD.R_Rate;
        discountCmd.Parameters.Add("v_promotion_code", OracleType.NVarChar).Value = CID.PromoCode;
        discountCmd.Parameters.Add("v_item_code", OracleType.NVarChar).Value = CID.ProdNo;
        discountCmd.Parameters.Add("v_data", OracleType.NVarChar).Value = CHD.Data;
        discountCmd.Parameters.Add("v_voice", OracleType.NVarChar).Value = CHD.Voice;
        discountCmd.Parameters.Add("v_trans_type", OracleType.NVarChar).Value = CHD.Trans_type;
        discountCmd.Parameters.Add("v_mnp", OracleType.NVarChar).Value = CHD.MNP;
        discountCmd.Parameters.Add("v_bundle_type", OracleType.NVarChar).Value = CHD.BundleId;
        discountCmd.Parameters.Add("v_employee_id", OracleType.NVarChar).Value = CHD.EmployeeId;
        discountCmd.Parameters.Add("v_sys_id", OracleType.NVarChar).Value = CHD.SysId;
        discountCmd.Parameters.Add("v_store_id", OracleType.NVarChar).Value = CHD.StoreId;
        discountCmd.Parameters.Add("o_data", OracleType.Cursor).Direction = ParameterDirection.Output;
        discountCmd.Parameters.Add("v_msgcode", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
        discountCmd.Parameters.Add("v_message", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
        discountCmd.ExecuteNonQuery();
        string v_msgcode = discountCmd.Parameters["v_msgcode"].Value.ToString();
        OracleDataReader dr = (OracleDataReader)discountCmd.Parameters["o_data"].Value;
        DataTable discountDt = new DataTable();
        discountDt.Columns.Add("discount_code");
        discountDt.Columns.Add("discount_name");
        discountDt.Columns.Add("DISCOUNT_MONEY");
        discountDt.Columns.Add("DISCOUNT_RATE");

        discountDt.Columns.Add("S_DATE");
        discountDt.Columns.Add("E_DATE");
        while (dr.Read())
        {
            DataRow row = discountDt.NewRow();
            row["discount_code"] = dr[0].ToString();
            row["discount_name"] = dr[1].ToString();
            row["DISCOUNT_MONEY"] = dr[2].ToString();
            row["DISCOUNT_RATE"] = dr[3].ToString();
            row["S_DATE"] = dr[4].ToString();
            row["E_DATE"] = dr[5].ToString();
            discountDt.Rows.Add(row);
        }
        dr.Close();
        #endregion

        #region Insert To_Close_Discount
        StringBuilder sb_item = new StringBuilder();
        sb_item.Append(" insert into TO_CLOSE_DISCOUNT (");
        sb_item.Append(" ID,");
        sb_item.Append(" SEQNO,");
        sb_item.Append(" DISCOUNT_ID,");
        sb_item.Append(" DISCOUNT_PRICE,");
        sb_item.Append(" DISCOUNT_AMOUNT,");
        sb_item.Append(" DISCOUNT_B_DATE,");
        sb_item.Append(" CREATE_USER,");
        sb_item.Append(" CREATE_DTM,");
        sb_item.Append(" MODI_USER,");
        sb_item.Append(" MODI_DTM,");
        sb_item.Append(" POSUUID_DETAIL,");
        sb_item.Append(" DISCOUNT_E_DATE ");
        sb_item.Append(" VALUES(");
        sb_item.Append(" POS_UUID(),");
        sb_item.Append(" :SEQNO,");
        sb_item.Append(" :DISCOUNT_ID,");
        sb_item.Append(" :DISCOUNT_PRICE,");
        sb_item.Append(" :DISCOUNT_AMOUNT,");
        sb_item.Append(" :DISCOUNT_B_DATE,");
        sb_item.Append(" :CREATE_USER,");
        sb_item.Append(" SYSDATE,");
        sb_item.Append(" :MODI_USER,");
        sb_item.Append(" SYSDATE,");
        sb_item.Append(" :POSUUID_DETAIL,");
        sb_item.Append(" :DISCOUNT_E_DATE");
        sb_item.Append(")");

        OracleCommand cmd = new OracleCommand(sb_item.ToString(), TransPOS.Connection, TransPOS);
        cmd.Parameters.Add(":SEQNO", OracleType.VarChar, 32);
        cmd.Parameters.Add(":DISCOUNT_ID", OracleType.VarChar, 50);
        cmd.Parameters.Add(":DISCOUNT_PRICE", OracleType.Number);
        cmd.Parameters.Add(":DISCOUNT_AMOUNT", OracleType.Number);
        cmd.Parameters.Add(":DISCOUNT_B_DATE", OracleType.DateTime);
        cmd.Parameters.Add(":CREATE_USER", OracleType.VarChar, 50).Value = CHD.EmployeeId;
        cmd.Parameters.Add(":DISCOUNT_E_DATE", OracleType.DateTime);
        cmd.Parameters.Add(":MODI_USER", OracleType.VarChar, 50).Value = CHD.EmployeeId;
        cmd.Parameters.Add(":POSUUID_DETAIL", OracleType.VarChar, 32).Value = CID.PosuuidDetail;


        int i = 1;
        foreach (DataRow row in discountDt.Rows)
        {

            cmd.Parameters[":SEQNO"].Value = i++;
            cmd.Parameters[":DISCOUNT_ID"].Value = row["DISCOUNT_ID"].ToString();
            if (row["DISCOUNT_MONEY"].ToString() == "0")
            {
                double price = Convert.ToDouble(row["DISCOUNT_MONEY"]);
                cmd.Parameters[":DISCOUNT_PRICE"].Value = price;
                cmd.Parameters[":DISCOUNT_AMOUNT"].Value = price;
            }
            else
            {
                double rate = Convert.ToDouble(row["DISCOUNT_RATE"]) * 0.01;
                double t_amount = Convert.ToDouble(CID.Amount);
                cmd.Parameters[":DISCOUNT_PRICE"].Value = t_amount * rate * -1;
                cmd.Parameters[":DISCOUNT_AMOUNT"].Value = t_amount * rate * -1;
            }

            cmd.Parameters[":DISCOUNT_B_DATE"].Value = row["S_DATE"];
            cmd.Parameters[":DISCOUNT_E_DATE"].Value = row["E_DATE"];
            cmd.ExecuteNonQuery();
        }
        #endregion

    }
    #endregion

    //============================
    #region 寫入 Sale 相關資料表
    private void InsertSalesHead(string Store_Id, string strEmployeeId, string strStoreName, string strPosUUIDMaster, string sale_no, string sale_status, OracleTransaction TransPOS)
    {
        StringBuilder sbSalesHead = new StringBuilder();
        List<string> lstColumnsSalesHead = new List<string>
        {   
            "POSUUID_MASTER",	"STORE_NO",		"STORE_NAME",	"MACHINE_ID",	"TRADE_DATE",
			"SALE_PERSON",		"CREATE_USER",	"MODI_USER",	"CREATE_DTM",	"MODI_DTM",
			"SALE_NO",			"SALE_STATUS"
        };
        List<string> lstValuesSalesHead = new List<string>
        {
            OracleDBUtil.SqlStr(strPosUUIDMaster),
            OracleDBUtil.SqlStr(Store_Id),
            strStoreName,
			"'01'",
            "SYSDATE",
            strEmployeeId, 
			strEmployeeId, 
			strEmployeeId, 
			"SYSDATE", 
			"SYSDATE",
            OracleDBUtil.SqlStr(sale_no),
            OracleDBUtil.SqlStr(sale_status)
        };
        this.GetSqlInsertItem(sbSalesHead, "SALE_HEAD", lstColumnsSalesHead, lstValuesSalesHead);
        this.ExecuteSql(TransPOS, sbSalesHead, sbSalesHead.ToString(), strEmployeeId, "SalesDetail");
    }

    private string InsertSalesDetail(string POS_UUID, OracleDataReader drSource, int i, string strEmployeeId, string strPosuuid, string strSourceType, string strTotoalAmount, string strPosUUIDMaster, string strProdNo, OracleTransaction TransPOS)
    {
        StringBuilder sbSalesDetail = new StringBuilder();
        List<string> lstColumnsSalesDetail = new List<string>
        {   
            "ID",				"POSUUID_MASTER",	"POSUUID_DETAIL",	"SEQNO",		"PRODNO",
			"QUANTITY",			"TOTAL_AMOUNT",		"PROMOTION_CODE",	"APPLY_DATE",	"SERVICE_SYS_ID",
			"BUNDLE_ID",		"BUNDLE_TYPE",		"FUN_ID",			"DATA",			"VOICE",
			"APPROVE_STATUS",	"CONN_FLAG",		"SIM_CARD_NO",		"MSISDN",		"BILLING_ACCOUNT_ID",
			"SUBSCRIBE_NO",		"CREATE_USER",		"MODI_USER",		"CREATE_DTM",	"MODI_DTM",
			"SOURCE_TYPE",		"ITEM_TYPE", "UNIT_PRICE"
        };
        string DetailID = GuidNo.getUUID();

        List<string> lstValuesSalesDetail = new List<string>
        {
            OracleDBUtil.SqlStr(DetailID),
            OracleDBUtil.SqlStr(strPosUUIDMaster),
            OracleDBUtil.SqlStr(POS_UUID),
            string.Format("'{0}'", (i - 1)),
			strProdNo, 
			"'1'", 
            OracleDBUtil.SqlStr(strTotoalAmount),
            "''", 
			"SYSDATE", 
            OracleDBUtil.SqlStr(strPosuuid),
            OracleDBUtil.SqlStr(drSource["BUNDLE_ID"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["BUNDLE_TYPE"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["FUND_ID"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["DATA"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["VOICE"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["APPROVE_STATUS_POS"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["CONN_FLAG"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["SIM_NO"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["MSISDN"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["ACCOUNT_ID"].ToString()),
            OracleDBUtil.SqlStr(drSource["SUBSCRIBER_ID"].ToString()),
            strEmployeeId, 
			strEmployeeId, 
			"SYSDATE", 
			"SYSDATE",
            OracleDBUtil.SqlStr(strSourceType),
			"'2'",
               OracleDBUtil.SqlStr(strTotoalAmount)
        };

        this.GetSqlInsertItem(sbSalesDetail, "SALE_DETAIL", lstColumnsSalesDetail, lstValuesSalesDetail);
        this.ExecuteSql(TransPOS, sbSalesDetail, sbSalesDetail.ToString(), strEmployeeId, "SalesDetail");
        return DetailID;
    }


    private string InsertSalesDetail(string POS_UUID, Hashtable drSource, int i, string strEmployeeId, string strPosuuid, string strSourceType, string strTotoalAmount, string strPosUUIDMaster, string strProdNo, OracleTransaction TransPOS)
    {
        StringBuilder sbSalesDetail = new StringBuilder();
        List<string> lstColumnsSalesDetail = new List<string>
        {   
            "ID",				"POSUUID_MASTER",	"POSUUID_DETAIL",	"SEQNO",		"PRODNO",
			"QUANTITY",			"TOTAL_AMOUNT",		"PROMOTION_CODE",	"APPLY_DATE",	"SERVICE_SYS_ID",
			"BUNDLE_ID",		"BUNDLE_TYPE",		"FUN_ID",			"DATA",			"VOICE",
			"APPROVE_STATUS",	"CONN_FLAG",		"SIM_CARD_NO",		"MSISDN",		"BILLING_ACCOUNT_ID",
			"SUBSCRIBE_NO",		"CREATE_USER",		"MODI_USER",		"CREATE_DTM",	"MODI_DTM",
			"SOURCE_TYPE",		"ITEM_TYPE", "UNIT_PRICE"
        };
        string DetailID = GuidNo.getUUID();
        List<string> lstValuesSalesDetail = new List<string>
        {
            OracleDBUtil.SqlStr(DetailID),
            OracleDBUtil.SqlStr(strPosUUIDMaster),
            OracleDBUtil.SqlStr(POS_UUID),
            string.Format("'{0}'", (i - 1)),
			strProdNo, 
			"'1'", 
            OracleDBUtil.SqlStr(strTotoalAmount),
            "''", 
			"SYSDATE", 
            OracleDBUtil.SqlStr(strPosuuid),
            OracleDBUtil.SqlStr(drSource["BUNDLE_ID"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["BUNDLE_TYPE"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["FUN_ID"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["DATA"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["VOICE"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["APPROVE_STATUS_POS"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["CONN_FLAG"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["SIM_NO"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["MSISDN"].ToString()) ,
            OracleDBUtil.SqlStr(drSource["ACCOUNT_ID"].ToString()),
            OracleDBUtil.SqlStr(drSource["SUBSCRIBER_ID"].ToString()),
            strEmployeeId, 
			strEmployeeId, 
			"SYSDATE", 
			"SYSDATE",
            OracleDBUtil.SqlStr(strSourceType),
			"'2'",
            OracleDBUtil.SqlStr(drSource["UNIT_PRICE"].ToString())
        };

        this.GetSqlInsertItem(sbSalesDetail, "SALE_DETAIL", lstColumnsSalesDetail, lstValuesSalesDetail);
        this.ExecuteSql(TransPOS, sbSalesDetail, sbSalesDetail.ToString(), strEmployeeId, "SalesDetail");
        return DetailID;
    }
    #endregion


    //============================
    #region 扣庫存
    private void BuckleInventory(OracleTransaction TransPOS, string Store_Id, string sale_no, string strEmployeeId, string prodNo, string detailId)
    {
        // 扣庫存
        INVENTORY_Facade Inventory = new INVENTORY_Facade();
        try
        {
            string Code = "";
            string Message = "";
            string Stock = GetGoodLOCUUID(TransPOS);
            Inventory.PK_INVENTORY_SALE(TransPOS, "1", prodNo, Store_Id, Stock, sale_no, 1, strEmployeeId, detailId, ref Code, ref Message);
            if (Code != "000")
                throw new Exception("扣庫存失敗");
        }
        catch (Exception ex)
        {
            string paramsString = string.Format("ProdNo={0},Store_Id={1},Errmessage:{2}", prodNo, Store_Id, ex.Message);
            //先將傳入參數寫入 LOG
            this.WriteLog("Exception", OracleDBUtil.SqlStr(paramsString), "WEB SERVICE", strEmployeeId, 0);
            throw ex;
        }
    }
    #endregion

    //============================
    #region 取得倉別
    private string GetGoodLOCUUID(OracleTransaction TransPOS)
    {
        string sql = "SELECT INV_GoodLOCUUID() AS UUID FROM DUAL";

        DataTable dt = OracleDBUtil.GetDataSet(TransPOS, sql).Tables[0];

        string UUID = "";
        if (dt.Rows.Count > 0)
            UUID = dt.Rows[0]["UUID"].ToString();
        return UUID;
    }
    #endregion

    //============================
    #region Commit外部系統交易狀態
    /// <summary>
    /// Commit外部系統交易狀態
    /// </summary>
    /// <param name="SERVICE_SYS_ID">外部系統主鍵值</param>
    /// <param name="POSUUID_MASTER">銷售主鍵值</param>
    /// <param name="POSUUID_DETAIL">未結清單主鍵值</param>
    /// <returns>結果:0 成功, -1 失敗</returns>
    private int CommitOuterSystem(string SERVICE_SYS_ID, string POSUUID_MASTER, string POSUUID_DETAIL, string Employee_Id)
    {
        string outerCmd = "";
        string strMCode = "";
        OracleConnection objConn = null;
        OracleCommand oraCmd = null;
        try
        {
            outerCmd = "SP_SSI_commit_POS";
            objConn = OracleDBUtil.GetSSIConnection();
            oraCmd = new OracleCommand(outerCmd);
            oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
            oraCmd.Parameters.Add(new OracleParameter("image_number", OracleType.VarChar, 2000)).Value = SERVICE_SYS_ID;
            oraCmd.Parameters.Add(new OracleParameter("POSuuid_Master", OracleType.VarChar, 2000)).Value = POSUUID_MASTER;
            oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            oraCmd.Connection = objConn;
            oraCmd.ExecuteNonQuery();
            oraCmd.Parameters.Add(new OracleParameter("result", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
            strMCode = ",result=" + oraCmd.Parameters["result"].Value.ToString();
            string msg = string.Format("POS通知服務系統結帳完成: SP={0},image_number={1},POSuuid_Master={2}", outerCmd, SERVICE_SYS_ID, POSUUID_MASTER);
            this.WriteLog("CommitOuterSystem", OracleDBUtil.SqlStr(msg), "WEB SERVICE", Employee_Id, 0);
            return 0;
        }
        catch (Exception ex)
        {
            string msg = string.Format("POS通知服務系統結帳失敗: SP={0},image_number={1},POSuuid_Master={2},ErrMsg={3}", outerCmd, SERVICE_SYS_ID, POSUUID_MASTER, ex.Message);
            this.WriteLog("CommitOuterSystem", OracleDBUtil.SqlStr(msg), "WEB SERVICE", Employee_Id, 0);
            return -1;
        }
        finally
        {
            oraCmd.Dispose();
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
            objConn.Dispose();
            OracleConnection.ClearAllPools();
        }
    }
    #endregion

    //============================
    #region 寫 Log
    private void WriteLog(string ActionType, string LogMsg, string FunctionNo)
    {
        try
        {
            conn_new_pos = OracleDBUtil.GetConnection();
            OracleDBUtil.ExecuteSql(conn_new_pos, string.Format(@"
INSERT INTO SYS_PROCESS_LOG (FUNC_GROUP, ACTION_TYPE, CREATE_DTM, PARAMETER, FUNCTION_NO) 
VALUES('WEB SERVICE', '{0}', SYSDATE, {1}, '{2}')", ActionType, OracleDBUtil.SqlStr(LogMsg), FunctionNo));
        }
        catch { }
        finally
        {
            conn_new_pos.Dispose();
            OracleConnection.ClearAllPools();
        }
    }

    private void WriteLog(string ActionType, string LogMsg, string FunctionNo,
                          string EmployeeId, int ImpactRowCount)
    {
        try
        {
            conn_new_pos = OracleDBUtil.GetConnection();
            OracleDBUtil.ExecuteSql(conn_new_pos, string.Format(@"
INSERT INTO SYS_PROCESS_LOG (
	FUNC_GROUP, 
	ACTION_TYPE, 
	PARAMETER, 
	FUNCTION_NO, 
	OPERATOR, 
	CREATE_USER, 
	MODI_USER, 
	CREATE_DTM, 
	MODI_DTM, 
	ENTERY_DTM, 
	ROLE_TYPE, 
	IMPACT_REC_COUNT, 
	HOST_IP
) VALUES(
'WEB SERVICE', {0}, {1}, {2}, {3}, {3}, {3}, SYSDATE, SYSDATE, SYSDATE, 'WS', {4}, {5})",
    OracleDBUtil.SqlStr(ActionType),
    OracleDBUtil.SqlStr(LogMsg),
    OracleDBUtil.SqlStr(FunctionNo),
    EmployeeId,
    ImpactRowCount,
    OracleDBUtil.SqlStr(Context.Request.ServerVariables.Get("LOCAL_ADDR"))
    ));
        }
        catch { }
        finally
        {
            conn_new_pos.Dispose();
            OracleConnection.ClearAllPools();
        }
    }
    #endregion

    //============================
    #region 處理 Sql Statement
    private void GetSqlInsertItem(StringBuilder sbitem, string TableName, List<string> Columns, List<string> Values)
    {
        sbitem.AppendFormat("INSERT INTO {0} (", TableName);

        for (int i = 0, iCount = Columns.Count; i < iCount; i++)
        {
            if (i != 0) sbitem.Append(",");
            sbitem.AppendLine(Columns[i]);
        }
        sbitem.AppendLine(") VALUES(");
        for (int i = 0, iCount = Values.Count; i < iCount; i++)
        {
            if (i != 0) sbitem.Append(",");
            sbitem.AppendLine(Values[i]);
        }
        sbitem.AppendLine(")");
    }
    private void ExecuteSql(OracleTransaction TransPOS, StringBuilder SQLStatement, string LogMsg, string EmployeeId, string ProcessId)
    {
        this.WriteLog("ProcessSSI", string.Format("Process: {0},{1}", ProcessId, LogMsg), "WEB SERVICE", EmployeeId, 0);
        int iImpactRowCount = OracleDBUtil.ExecuteSql(TransPOS, SQLStatement.ToString());
        this.WriteLog("ProcessSSI", string.Format("ProcessEnd: {0},", ProcessId), "WEB SERVICE", EmployeeId, iImpactRowCount);
    }
    #endregion

    //============================
    #region 錯誤處理
    private string GetErrMsg(string ErrMsg, string EmployeeId)
    {
        ErrMsg = "ERROR:" + ErrMsg;
        this.WriteLog("ERROR", OracleDBUtil.SqlStr(paramsString + "," + ErrMsg), "WEB SERVICE", EmployeeId, 0);

        return ErrMsg;
    }
    #endregion

    //============================
    #region 確認是否為Prepaid
    private bool CheckPrepaid(string trans_type)
    {
        List<string> list = new List<string>();
        list.Add("NCASH");
        list.Add("3NCASH");
        list.Add("PREKA");
        list.Add("PREGA");

        return list.Contains(trans_type);
    }
    #endregion

    #region 折扣處理

    public struct Discount_Conditions
    {
        public string discount_type;
        public string prodno;
        public string data;
        public string voice;
        public string r_rate;
        public string msisdn;
        public string promotion_code;
        public string trans_type;
        public string mnp;
        public string bundle_type;
        public string employee_id;
        public string sys_id;
        public string store_id;
        public string uni_price;
        public string modi_user;
        public string posuuid_detail;
        public string total_amount;
    }

    public void CreateDiscount(string posuuid_detail, int quantity, OracleTransaction trans)
    {
        //判斷是否TO_CLOSE_DISCOUNT


        Discount_Conditions conditions = new Discount_Conditions();
        OracleConnection conn = trans.Connection;
        string fun_id = "";

        bool hasRow = false;

        string sqlstr = "select * from TO_CLOSE_HEAD where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
        OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            conditions.posuuid_detail = posuuid_detail;
            conditions.r_rate = dr["r_rate"].ToString();
            conditions.data = dr["data"].ToString();
            conditions.voice = dr["voice"].ToString();
            conditions.trans_type = dr["trans_type"].ToString();
            conditions.mnp = dr["mnp"].ToString();
            conditions.bundle_type = dr["BUNDLE_TYPE"].ToString();
            conditions.employee_id = dr["MODI_USER"].ToString();
            conditions.sys_id = dr["SERVICE_TYPE"].ToString();
            conditions.store_id = dr["store_no"].ToString();
            conditions.total_amount = dr["total_amount"].ToString();
            fun_id = dr["fun_id"].ToString();
            hasRow = true;
        }
        dr.Close();

        if (!hasRow)
        {
            return;
        }

        //排除
        if (conditions.sys_id == "4" && (fun_id == "180" || fun_id == "150" || fun_id == "11"))
        {
            return;
        }

        sqlstr = "select * from TO_CLOSE_ITEM where posuuid_detail = " + OracleDBUtil.SqlStr(posuuid_detail);
        cmd = new OracleCommand(sqlstr, conn, trans);
        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        int i = 1;
        conditions.posuuid_detail = posuuid_detail;
        conditions.prodno = "";
        conditions.promotion_code = "";
        conditions.msisdn = "";
        foreach (DataRow row in dt.Rows)
        {
            if (string.IsNullOrEmpty(conditions.promotion_code))
            {
                conditions.promotion_code = row["PROMOTION_CODE"].ToString();
            }

            if (string.IsNullOrEmpty(conditions.prodno))
            {
                conditions.msisdn = StringUtil.CStr(row["MSISDN"]);
            }
            conditions.prodno += row["prodno"].ToString() + "|";


        }
        conditions.prodno = conditions.prodno.TrimEnd('|');
        //新增折扣
        InsertToCloseDiscount(conditions, trans, ref i, 1);

    }

    public static DataTable get_Discount(Discount_Conditions conditions, int quantity)
    {
        DataTable discountDt = new DataTable();
        OracleConnection conn = null;
        try
        {
            conn = OracleDBUtil.GetConnection();
            #region 讀取Discount Master
            OracleCommand discountCmd = new OracleCommand("sp_query_discount_ws");
            discountCmd.CommandType = CommandType.StoredProcedure;
            discountCmd.Connection = conn;
            if (conditions.msisdn != null)
                discountCmd.Parameters.Add("v_msisdn", OracleType.NVarChar).Value = conditions.msisdn;
            else
                discountCmd.Parameters.Add("v_msisdn", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.r_rate != null)
                discountCmd.Parameters.Add("v_r_rate", OracleType.NVarChar).Value = conditions.r_rate;
            else
                discountCmd.Parameters.Add("v_r_rate", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.promotion_code != null)
                discountCmd.Parameters.Add("v_promotion_code", OracleType.NVarChar).Value = conditions.promotion_code;
            else
                discountCmd.Parameters.Add("v_promotion_code", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.prodno != null)
                discountCmd.Parameters.Add("v_item_code", OracleType.NVarChar).Value = conditions.prodno;
            else
                discountCmd.Parameters.Add("v_item_code", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.data != null)
                discountCmd.Parameters.Add("v_data", OracleType.NVarChar).Value = conditions.data;
            else
                discountCmd.Parameters.Add("v_data", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.voice != null)
                discountCmd.Parameters.Add("v_voice", OracleType.NVarChar).Value = conditions.voice;
            else
                discountCmd.Parameters.Add("v_voice", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.trans_type != null)
                discountCmd.Parameters.Add("v_trans_type", OracleType.NVarChar).Value = conditions.trans_type;
            else
                discountCmd.Parameters.Add("v_trans_type", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.mnp != null)
                discountCmd.Parameters.Add("v_mnp", OracleType.NVarChar).Value = conditions.mnp;
            else
                discountCmd.Parameters.Add("v_mnp", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.bundle_type != null)
                discountCmd.Parameters.Add("v_bundle_type", OracleType.NVarChar).Value = conditions.bundle_type;
            else
                discountCmd.Parameters.Add("v_bundle_type", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.employee_id != null)
                discountCmd.Parameters.Add("v_employee_id", OracleType.NVarChar).Value = conditions.employee_id;
            else
                discountCmd.Parameters.Add("v_employee_id", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.sys_id != null)
                discountCmd.Parameters.Add("v_sys_id", OracleType.NVarChar).Value = conditions.sys_id;
            else
                discountCmd.Parameters.Add("v_sys_id", OracleType.NVarChar).Value = DBNull.Value;

            if (conditions.store_id != null)
                discountCmd.Parameters.Add("v_store_id", OracleType.NVarChar).Value = conditions.store_id;
            else
                discountCmd.Parameters.Add("v_store_id", OracleType.NVarChar).Value = DBNull.Value;


            discountCmd.Parameters.Add("o_data", OracleType.Cursor).Direction = ParameterDirection.Output;
            discountCmd.Parameters.Add("v_msgcode", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
            discountCmd.Parameters.Add("v_message", OracleType.VarChar, 4000).Direction = ParameterDirection.Output;
            discountCmd.ExecuteNonQuery();
            string v_msgcode = discountCmd.Parameters["v_msgcode"].Value.ToString();
            string v_message = discountCmd.Parameters["v_message"].Value.ToString();
            OracleDataReader dr = (OracleDataReader)discountCmd.Parameters["o_data"].Value;

            discountDt.Columns.Add("discount_master_id");
            discountDt.Columns.Add("discount_code");
            discountDt.Columns.Add("discount_id");
            discountDt.Columns.Add("discount_name");
            discountDt.Columns.Add("DISCOUNT_MONEY");
            discountDt.Columns.Add("DISCOUNT_RATE");
            discountDt.Columns.Add("DISCOUNT_PRICE");
            discountDt.Columns.Add("DISCOUNT_AMOUNT");
            discountDt.Columns.Add("DISCOUNT_B_DATE");
            discountDt.Columns.Add("quantity");
            discountDt.Columns.Add("S_DATE");

            discountDt.Columns.Add("DISCOUNT_E_DATE");
            discountDt.Columns.Add("E_DATE");

            double discount_total_amount = 0;
            if (v_msgcode == "000")
            {
                while (dr.Read())
                {
                    string discount_master_id = dr.IsDBNull(0) ? "" : dr[0].ToString();

                    DataRow row = discountDt.NewRow();
                    row["discount_master_id"] = discount_master_id;
                    row["discount_code"] = dr.IsDBNull(1) ? "" : dr[1].ToString();
                    row["discount_id"] = dr.IsDBNull(1) ? "" : dr[1].ToString();
                    row["discount_name"] = dr.IsDBNull(2) ? "" : dr[2].ToString();
                    row["DISCOUNT_MONEY"] = dr.IsDBNull(3) ? "" : dr[3].ToString();
                    row["DISCOUNT_RATE"] = dr.IsDBNull(4) ? "" : dr[4].ToString();
                    row["quantity"] = quantity;
                    if (!string.IsNullOrEmpty(StringUtil.CStr(row["DISCOUNT_MONEY"])))
                    {
                        double price = Convert.ToDouble(row["DISCOUNT_MONEY"]);
                        row["DISCOUNT_PRICE"] = price;
                        row["DISCOUNT_AMOUNT"] = price * quantity;
                        discount_total_amount += Convert.ToDouble(row["DISCOUNT_AMOUNT"]);
                    }
                    else
                    {
                        double rate = Convert.ToDouble(row["DISCOUNT_RATE"]) * 0.01;
                        double t_amount = Convert.ToDouble(conditions.uni_price);
                        row["DISCOUNT_PRICE"] = t_amount * rate;
                        row["DISCOUNT_AMOUNT"] = (t_amount * rate) * quantity;
                        discount_total_amount += Convert.ToDouble(row["DISCOUNT_AMOUNT"]);
                    }


                    row["DISCOUNT_B_DATE"] = dr.IsDBNull(5) ? "" : dr[5].ToString();
                    row["S_DATE"] = dr.IsDBNull(5) ? "" : dr[5].ToString();
                    row["E_DATE"] = dr.IsDBNull(6) ? "" : dr[6].ToString();
                    row["DISCOUNT_E_DATE"] = dr.IsDBNull(6) ? "" : dr[6].ToString();
                    discountDt.Rows.Add(row);
                }

                dr.Close();

                //平衡價格
                double total_amount = String.IsNullOrEmpty(conditions.total_amount) ? 0 : Convert.ToDouble(conditions.total_amount);
                if (discount_total_amount < 0 && Math.Abs(discount_total_amount) > total_amount)
                {

                    double temp_amount = total_amount + discount_total_amount;
                    foreach (DataRow row in discountDt.Rows)
                    {
                        if (temp_amount != 0)
                        {
                            double discount_amount = Convert.ToDouble(row["DISCOUNT_PRICE"]);
                            if (discount_amount > temp_amount)
                            {
                                temp_amount -= discount_amount;
                                row.BeginEdit();
                                row["DISCOUNT_PRICE"] = 0;
                                row["DISCOUNT_AMOUNT"] = 0;
                                row.EndEdit();

                            }
                            else
                            {
                                row.BeginEdit();
                                row["DISCOUNT_PRICE"] = (temp_amount - discount_amount) * -1;
                                row["DISCOUNT_AMOUNT"] = (temp_amount - discount_amount) * -1;
                                temp_amount = 0;
                                row.EndEdit();
                            }
                            row.AcceptChanges();
                        }
                    }
                    discountDt.AcceptChanges();
                }

            }
            else
            {
                throw new Exception(v_message);
            }
            #endregion
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
        return discountDt;
    }

    public void InsertToCloseDiscount(Discount_Conditions conditions, OracleTransaction TransPOS, ref int i, int quantity)
    {

        DataTable discountDt = get_Discount(conditions, quantity);

        #region Insert To_Close_Discount
        StringBuilder sb_item = new StringBuilder();
        sb_item.Append(" insert into TO_CLOSE_DISCOUNT (");
        sb_item.Append(" ID,");
        sb_item.Append(" SEQNO,");
        sb_item.Append(" DISCOUNT_ID,");
        sb_item.Append(" DISCOUNT_PRICE,");
        sb_item.Append(" DISCOUNT_AMOUNT,");
        sb_item.Append(" DISCOUNT_B_DATE,");
        sb_item.Append(" CREATE_USER,");
        sb_item.Append(" CREATE_DTM,");
        sb_item.Append(" MODI_USER,");
        sb_item.Append(" MODI_DTM,");
        sb_item.Append(" POSUUID_DETAIL,");
        sb_item.Append(" DISCOUNT_E_DATE ");
        sb_item.Append(" )VALUES(");
        sb_item.Append(" POS_UUID(),");
        sb_item.Append(" :SEQNO,");
        sb_item.Append(" :DISCOUNT_ID,");
        sb_item.Append(" :DISCOUNT_PRICE,");
        sb_item.Append(" :DISCOUNT_AMOUNT,");
        sb_item.Append(" :DISCOUNT_B_DATE,");
        sb_item.Append(" :CREATE_USER,");
        sb_item.Append(" SYSDATE,");
        sb_item.Append(" :MODI_USER,");
        sb_item.Append(" SYSDATE,");
        sb_item.Append(" :POSUUID_DETAIL,");
        sb_item.Append(" :DISCOUNT_E_DATE");
        sb_item.Append(")");

        OracleCommand cmd = new OracleCommand(sb_item.ToString(), TransPOS.Connection, TransPOS);
        cmd.Parameters.Add(":SEQNO", OracleType.VarChar, 32);
        cmd.Parameters.Add(":DISCOUNT_ID", OracleType.VarChar, 50);
        cmd.Parameters.Add(":DISCOUNT_PRICE", OracleType.Number);
        cmd.Parameters.Add(":DISCOUNT_AMOUNT", OracleType.Number);
        cmd.Parameters.Add(":DISCOUNT_B_DATE", OracleType.DateTime);
        cmd.Parameters.Add(":CREATE_USER", OracleType.VarChar, 50).Value = conditions.employee_id;
        cmd.Parameters.Add(":DISCOUNT_E_DATE", OracleType.DateTime);
        cmd.Parameters.Add(":MODI_USER", OracleType.VarChar, 50).Value = conditions.employee_id;
        cmd.Parameters.Add(":POSUUID_DETAIL", OracleType.VarChar, 32).Value = conditions.posuuid_detail;



        foreach (DataRow row in discountDt.Rows)
        {

            cmd.Parameters[":SEQNO"].Value = i++;
            cmd.Parameters[":DISCOUNT_ID"].Value = row["discount_code"].ToString();
         
            cmd.Parameters[":DISCOUNT_PRICE"].Value = row["DISCOUNT_PRICE"].ToString();
            cmd.Parameters[":DISCOUNT_AMOUNT"].Value = row["DISCOUNT_AMOUNT"].ToString(); 
          


            if (row.IsNull("S_DATE") || string.IsNullOrEmpty(row["S_DATE"].ToString()))
            {
                cmd.Parameters[":DISCOUNT_B_DATE"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters[":DISCOUNT_B_DATE"].Value = row["S_DATE"];
            }
            if (row.IsNull("E_DATE") || string.IsNullOrEmpty(row["E_DATE"].ToString()))
            {
                cmd.Parameters[":DISCOUNT_E_DATE"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters[":DISCOUNT_E_DATE"].Value = row["E_DATE"];
            }
            cmd.ExecuteNonQuery();
        }
        #endregion

    }
    #endregion

}

#region To Close Tables Data Objects
class ToCloseHeadDataObject
{
    public string StoreId { get; set; }
    public string EmployeeId { get; set; }
    public string Posuuid { get; set; }
    public string BundleId { get; set; }
    public string FundId { get; set; }
    public string Data { get; set; }
    public string Voice { get; set; }
    public string ApproveStatus { get; set; }
    public string ConnFlag { get; set; }
    public string BundleType { get; set; }
    public string ImageNumber { get; set; }
    public string SubscriberId { get; set; }
    public string AccountId { get; set; }
    public string TotalAmount { get; set; }
    public string StoreName { get; set; }
    public string ProcessId { get; set; }
    public string SysId { get; set; }
    public string Trans_type { get; set; }
    public string R_Rate { get; set; }
    public string MNP { get; set; }
    public string MSISDN { get; set; }
}
class ToCloseItemDataObject
{
    public string ItemId { get; set; }
    public string EmployeeId { get; set; }
    public int SeqNo { get; set; }
    public string ProdNo { get; set; }
    public string Amount { get; set; }
    public string PromoCode { get; set; }
    public string Quantity { get; set; }
    public string PosuuidDetail { get; set; }
    public string MSISDN { get; set; }
    public string SimCardNo { get; set; }
    public string ProcessId { get; set; }
    public string PosuuidHead { get; set; }
    public string SourceType { get; set; }
    public string TotalAmount { get; set; }
    public string PosuuidMaster { get; set; }
}
#endregion