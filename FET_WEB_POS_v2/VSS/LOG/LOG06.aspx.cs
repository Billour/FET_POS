using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPager;
using System.Data;


using Advtek.Utility;
using FET.POS.Model.Common;
using FET.POS.Model.DTO;
using FET.POS.Model.Facade;
using FET.POS.Model.Facade.FacadeImpl;

public partial class VSS_LOG_LOG06 : BasePage
{
    private LOG06_Facade _LOG06_Facade;
    private LOG06_StoreHeaderDiscountPWD _LOG06_SysPara_DTO;
    public string checkdata;
    public string storeNo, EmployeeNo, MachineID;
    private static void btnCommit_ClickExtracted()
    {
    		
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            storeNo = logMsg.STORENO;
            EmployeeNo = logMsg.OPERATOR;
            MachineID = logMsg.MACHINE_ID;
            bindMasterData();

        }
    }
    protected void bindMasterData()
    {
        string StartDate, EndDate, Passwd;
        StartDate = "";
        EndDate = "";
        Passwd = "";
        _LOG06_Facade = new LOG06_Facade();
        DataTable dt = _LOG06_Facade.Query_SYSPara(EmployeeNo, EmployeeNo);
        if (dt.Rows.Count >= 1)
        {
            checkdata = "1";
             
            foreach (DataRow row in dt.Rows)
            {
                StartDate = StringUtil.CStr(row[0]);
                EndDate = StringUtil.CStr(row[1]);
                Passwd = StringUtil.CStr(row[2]);
                // ASPxDateEdit1.Text = StringUtil.CStr(row.ItemArray[0]);
                //ASPxDateEdit2.Text = StringUtil.CStr(row.ItemArray[1]);
                // pw.Text = StringUtil.CStr(row[2]);
                //  cpw.Text = StringUtil.CStr(row[2]);
            
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                ASPxDateEdit1.Text = System.DateTime.Parse(StartDate).ToShortDateString();

            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                if (System.DateTime.Parse(EndDate).ToShortDateString() ==  System.DateTime.Parse(Advtek.Utility.DateUtil.NullDateFormatString).ToShortDateString())
                {


                }
                else 
                {
                    ASPxDateEdit2.Text = System.DateTime.Parse(EndDate).ToShortDateString();
                }
            }
            if (!string.IsNullOrEmpty(Passwd))
            {
                pw.Text = Passwd;
                cpw.Text = Passwd;
               // Label3.Text = "若只修改起訖日，不修改密碼，請修改日期後按下確定即可";
            }
        }
        else
            checkdata = "0";
    
    
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        _LOG06_Facade = new LOG06_Facade();
        string SID = "";
        string Passwd = "";
        EmployeeNo = logMsg.OPERATOR; //Request.QueryString["EmployeeId"];
        DataTable dt = _LOG06_Facade.Query_SYSPara(EmployeeNo, EmployeeNo);
        if (dt.Rows.Count >= 1)
        {
            checkdata = "1";
            foreach (DataRow row in dt.Rows)
            {

                SID = StringUtil.CStr(row[3]);
                Passwd = StringUtil.CStr(row[2]);
            }
        }

        if (checkdata != "1")
        {
            adddata();
            

        }
        else
        {
            updatedata(SID, EmployeeNo, EmployeeNo, Passwd);
        }
    
    }
    protected void adddata()
    {
        if (StringUtil.CStr(ASPxDateEdit1.Text) != "")
        {
            string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
            if (System.DateTime.Parse(ASPxDateEdit1.Text) < System.DateTime.Parse(checkdate))
            {
                Label2.Text = "起始日期須大於或等於系統日期";
                ASPxDateEdit1.Focus();
            }
            else
            {

                if (ASPxDateEdit2.Text != "" && System.DateTime.Parse(ASPxDateEdit2.Text) < System.DateTime.Parse(ASPxDateEdit1.Text))
                {
                    Label2.Text = "若有訖日設定須大於或等於起始日期";
                    ASPxDateEdit2.Focus();
                }
                else
                {
                    if (pw.Text != "")
                    {

                        if (cpw.Text != "")
                        {
                            if (pw.Text == cpw.Text)
                            {
                                Label1.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                Label2.Text = "";

                                btnCommit_ClickExtracted();

                                try
                                {
                                    _LOG06_SysPara_DTO = new LOG06_StoreHeaderDiscountPWD();
                                    LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDDataTable dtSYS;
                                    LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDRow drSYS;


                                    dtSYS = _LOG06_SysPara_DTO.Tables["STORE_HEADER_DISCOUNT_PWD"] as LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDDataTable;

                                    drSYS = dtSYS.NewSTORE_HEADER_DISCOUNT_PWDRow();

                                    string SID = GuidNo.getUUID();
                                    string Enpw = DESCryptography.EncryptDES(pw.Text, "AAAAAAAA", "AAAAAAAA");
                                    DateTime Renddate = new DateTime();
                                    if (ASPxDateEdit2.Text == "")
                                    {

                                        Renddate = Advtek.Utility.DateUtil.NullDateFormat(null);
                                        
                                    }
                                    else
                                    {
                                        Renddate = System.DateTime.Parse(ASPxDateEdit2.Text);
                                    }
                                    //                                                        .Select(dr => dr.Field<string>("SYS_PARA_TYPE_ID")).Single();

                                    //新增之後要抓EMPID QueryString
                                    drSYS.ItemArray = new object[] 
                            {
                                
                               SID,
                               System.DateTime.Parse(ASPxDateEdit1.Text) ,     //Value_Start_Date
                             
                                Renddate,
                                Enpw,    //Password
                                "1",//Status
                                "Kevin", //CREATE_USER
                                Convert.ToDateTime(System.DateTime.Today), //CREATE_DTM                                
                                "Kevin", //MODI_USER
                                Convert.ToDateTime(System.DateTime.Today), //MODI_DTM
                                 EmployeeNo,//EMPNO
                                 EmployeeNo
                            };

                                    dtSYS.Rows.Add(drSYS);
                                    _LOG06_SysPara_DTO.AcceptChanges();
                                    //  _LOG04_SysPara_DTO.AcceptChanges();

                                    //更新資料庫
                                    _LOG06_Facade = new LOG06_Facade();
                                    _LOG06_Facade.AddNewOne_SysPara(_LOG06_SysPara_DTO);


                                    //

                                    ASPxDateEdit1.Text = "";
                                    ASPxDateEdit2.Text = "";
                                    bindMasterData();

                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    _LOG06_SysPara_DTO = null;
                                    _LOG06_Facade = null;
                                    //_LOG04_SysPara_DTO = null;
                                    //_LOG04_Facade = null;

                                }


                            }
                            else
                                Label2.Text = "確認密碼錯誤，請重新輸入";
                        }

                        else
                        {
                            Label2.Text = "請再輸入剛才的密碼";
                            Label1.Text = "";
                        }
                    }
                    else
                        Label2.Text = "密碼不得為空白";
                }
            }
        }

        else
        {
            Label2.Text = "請輸入有效日期";
        }

        
    }
    protected void updatedata(string SID,string EMPNO,string USERID,string Passwd)
    {
        string checkdate = System.DateTime.Now.ToString("yyyy/MM/dd");
        if (ASPxDateEdit1.Text != "")
        {

            if (System.DateTime.Parse(ASPxDateEdit1.Text) < System.DateTime.Parse(checkdate))
            {
                Label2.Text = "起始日期須大於或等於系統日期";
                bindMasterData();
            }
            else
            {
                if (ASPxDateEdit2.Text != "" && System.DateTime.Parse(ASPxDateEdit2.Text) < System.DateTime.Parse(ASPxDateEdit1.Text))
                {
                    Label2.Text = "若有訖日設定須大於或等於起始日期";
                    bindMasterData();
                }
                else
                {
                    if (pw.Text != "")
                    {
                        if (cpw.Text != "")
                        {
                            if (pw.Text == cpw.Text)
                            {
                                Passwd = pw.Text;
                                //updatedata();
                                GoModify(SID, EmployeeNo, EmployeeNo, Passwd);
                                Label1.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                Label2.Text = "";
                                bindMasterData();
                            }
                            else
                            {
                                Label2.Text = "確認密碼錯誤，請重新輸入";
                                bindMasterData();
                            }
                        }
                        else
                        {
                            Label2.Text = "請再輸入剛才的密碼";
                            bindMasterData();

                        }
                    }
                    else
                    {
                        // GoModify(SID, "Test1234", EmployeeNo, Passwd);
                        //Label1.Text = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        //Label2.Text = "";
                        Label2.Text = "請輸入密碼";
                        bindMasterData();
                    }

                }


            }
        }
        else
        {
            Label2.Text = "請輸入起始日期";  


        }
    
    }
    protected void GoModify(string SID, string EMPNO, string USERID, string Passwd)
    {
        try
        {
            _LOG06_SysPara_DTO = new LOG06_StoreHeaderDiscountPWD();
            LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDDataTable dtSYS;
            LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDRow drSYS;


            dtSYS = _LOG06_SysPara_DTO.Tables["STORE_HEADER_DISCOUNT_PWD"] as LOG06_StoreHeaderDiscountPWD.STORE_HEADER_DISCOUNT_PWDDataTable;

            drSYS = dtSYS.NewSTORE_HEADER_DISCOUNT_PWDRow();

            DataTable dt06 = LOG06_PageHelper.GetSHDP(SID);
            // string SID = GuidNo.getUUID();
            DateTime Renddate = new DateTime();
            //string Renddate1 = "";
            if (ASPxDateEdit2.Text == "")
            {
                //Renddate = System.DateTime.Parse("2999/12/31");
                Renddate = Advtek.Utility.DateUtil.NullDateFormat(null);
               // Renddate1 = Advtek.Utility.DateUtil.NullDateFormat(");
            }
            else
                Renddate = System.DateTime.Parse(ASPxDateEdit2.Text);
                //Renddate1 = ASPxDateEdit2.Text;
            foreach (DataRow row in dt06.Rows)
            {
                if (Passwd == StringUtil.CStr(row[3]) || Passwd=="")
                {
                    Passwd = StringUtil.CStr(row[3]);
                }
                else
                { 
                    
                }
                Passwd = DESCryptography.EncryptDES(Passwd, "AAAAAAAA", "AAAAAAAA");
                // var enumSysParaTypeId = LOG04_PageHelper.GetSysParaTypeId(true).Tables[0].AsEnumerable();
                // var SysTypeId = enumSysParaTypeId.Where(dr => dr.Field<string>("SYS_PARA_TYPE_NAME") == e.NewValues["SYS_PARA_TYPE_NAME"])
                //                             .Select(dr => dr.Field<string>("SYS_PARA_TYPE_ID")).Single();
                drSYS.SID = SID;
                drSYS.VALID_START_DATE = System.DateTime.Parse(ASPxDateEdit1.Text);
                drSYS.VALID_END_DATE = Renddate;
                drSYS.ENCRYPT_PASSWORD = Passwd;
                drSYS.Status = StringUtil.CStr(row[4]);
                drSYS.CREATE_USER = StringUtil.CStr(row[5]);
                drSYS.CREATE_DTM = System.DateTime.Parse(StringUtil.CStr(row[6]));
                drSYS.MODI_USER = EmployeeNo;
                drSYS.MODI_DTM = System.DateTime.Parse(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                drSYS.EMPNO = StringUtil.CStr(row[9]);
                drSYS.USERID = StringUtil.CStr(row[10]);
            }


            dtSYS.Rows.Add(drSYS);
            _LOG06_SysPara_DTO.AcceptChanges();
            //  _LOG04_SysPara_DTO.AcceptChanges();

            //更新資料庫
            _LOG06_Facade = new LOG06_Facade();
            _LOG06_Facade.UpdateOne_SysPara(_LOG06_SysPara_DTO);
            //   _LOG06_Facade.AddNewOne_SysPara(_LOG06_SysPara_DTO);
            //  _LOG04_Facade = new LOG04_Facade();
            // _LOG04_Facade.AddNewOne_SysPara(_LOG04_SysPara_DTO);

            //    bindMasterDataAfterWrite();

            //new LOG04_Facade().AddNewOne_SysPara(paraid,StringUtil.CStr(e.NewValues["PARA_KEY"]),
            //    StringUtil.CStr(e.NewValues["PARA_VALUE"]), StringUtil.CStr(e.NewValues["PARA_NAME"]), StringUtil.CStr(e.NewValues["PARA_DESC"]), "Shirley");

            //((ASPxGridView)sender).CancelEdit();
            //e.Cancel = true;

            //

            ASPxDateEdit1.Text = "";
            ASPxDateEdit2.Text = "";

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            _LOG06_SysPara_DTO = null;
            _LOG06_Facade = null;
            //_LOG04_SysPara_DTO = null;
            //_LOG04_Facade = null;

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        _LOG06_Facade = new LOG06_Facade();
        EmployeeNo = logMsg.OPERATOR; //Request.QueryString["EmployeeId"];
        DataTable dt = _LOG06_Facade.Query_SYSPara(EmployeeNo, EmployeeNo);
        if (dt.Rows.Count == 0)
        {
            ASPxDateEdit1.Text = "";
            ASPxDateEdit2.Text = "";
            pw.Text = "";
            cpw.Text = "";
            Label1.Text = ""; 
            Label2.Text = "";
        }
        else
            bindMasterData();
    }
}
