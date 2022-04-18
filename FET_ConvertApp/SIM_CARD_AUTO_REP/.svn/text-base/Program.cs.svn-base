using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data;
using System.Data.OracleClient;
using System.IO;

namespace SIM_CARD_AUTO_REP
{
    class Program
    {
        static void Main(string[] args)
        {
            OracleConnection conn = OracleDBUtil.GetConnection();
            OracleTransaction trans = conn.BeginTransaction();
            ConvertLog log = new ConvertLog("SIM_CARD_AUTO_REP");
            try
            {
                SIM_CARD_AUTO_REP SCAR = new SIM_CARD_AUTO_REP(conn, trans, log);
                if (SCAR.Check_DD())
                {
                    SCAR.Produce_Order();
                }
                else
                {
                    SCAR.UpDate_SIM_STORE_MEND();
                }

                trans.Commit();

                //須修改成功訊息
                log.Success("success!");
            }
            catch (Exception ex)
            {

                trans.Rollback();
                Console.WriteLine(ex.Message);
                log.Fail(ex.Message);

            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Dispose();
                OracleConnection.ClearAllPools();
                conn = null;
            }
        }
    }

    public class SIM_CARD_AUTO_REP
    {
        OracleConnection conn;
        OracleTransaction trans;
        ConvertLog log;
        public List<POS_SIM_ATR> ATRList;
        public List<SIM_STORE_MEND> STOREList;
        public List<STORE_ORDER> ORDERList;

        public Boolean Check_DD()
        {

            string[] DDSplit = null;
            string sqlstr = "SELECT  PARA_VALUE FROM SYS_PARA  WHERE PARA_KEY='AUTO_SAVE_DATE'";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                DDSplit = dr["PARA_VALUE"].ToString().Split(',');
            }
            dr.Close();

            for (int i = 0; i < DDSplit.GetUpperBound(0) + 1; i++)
            {
                if (DDSplit[i].PadRight(2, '0') == DateTime.Now.ToString("dd"))
                {
                    return true;
                }
            }
            return false;
        }

        public string GetOrderNO()
        {
            string ORDER_NO = "";
            string sqlstr = "SELECT  PARA_VALUE FROM SYS_PARA  WHERE PARA_KEY='PO_ORDER_NO'";
            OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ORDER_NO = dr["PARA_VALUE"].ToString();
            }

            dr.Close();
            return ORDER_NO;
        }

        public SIM_CARD_AUTO_REP(OracleConnection conn, OracleTransaction trans, ConvertLog log)
        {
            this.conn = conn;
            this.trans = trans;
            this.log = log;
            //取得預設POS_SIM_ATR的資料
            ATRList = POS_SIM_ATR.GetData(this.conn, this.trans);
            //取得預設SIM_STORE_MEND的資料
            STOREList = SIM_STORE_MEND.GetData(this.conn, this.trans);
            //產生訂單資料
            ORDERList = Comparison();
        }

        //產生訂單資料
        public List<STORE_ORDER> Comparison()
        {
            List<STORE_ORDER> dataList = new List<STORE_ORDER>();
            //取得毎一個GROUP的ID、安全存量及庫存量
            var STORE_GROUP = from p in STOREList
                              group p by new { p.SIM_GROUP_ID } into g
                              select new { GROUP_ID = g.Key.SIM_GROUP_ID, ON_HAND_QTY = g.Sum(p => p.ON_HAND_QTY) };
            foreach (var drSTORE_GROUP in STORE_GROUP)
            {
                //int SAFE_QTY = GetSafeSum(drSTORE_GROUP.GROUP_ID);

                if (GetSum(drSTORE_GROUP.GROUP_ID) >= GetSafeSum(drSTORE_GROUP.GROUP_ID) - drSTORE_GROUP.ON_HAND_QTY)
                {
                    var STORE_GROUP1 = from p in STOREList
                                       where p.SIM_GROUP_ID == drSTORE_GROUP.GROUP_ID
                                       orderby p.STORE_NO
                                       group p by new { p.STORE_NO } into g
                                       select new { GROUP_ID = g.Key.STORE_NO, ON_HAND_QTY = g.Sum(p => p.ON_HAND_QTY) };
                    foreach (var drSTORE_GROUP1 in STORE_GROUP1)
                    {
                        int QTY = GetSafeSum(drSTORE_GROUP.GROUP_ID, drSTORE_GROUP1.GROUP_ID) - drSTORE_GROUP1.ON_HAND_QTY;
                        var STORE = from a1 in STOREList
                                    where a1.SIM_GROUP_ID == drSTORE_GROUP.GROUP_ID && a1.STORE_NO == drSTORE_GROUP1.GROUP_ID
                                    orderby a1.PRODNO
                                    select new { SIM_GROUP_NAME = a1.SIM_GROUP_NAME, MEND = a1.SIM_MEND_ID, PRODNO = a1.PRODNO, SAFE_QTY = a1.SAFE_QTY, ON_HAND_QTY = a1.ON_HAND_QTY, STORE_NO = a1.STORE_NO, SCQC_D_ID = a1.SCQC_D_ID };
                        foreach (var drSTORE in STORE)
                        {
                            if (QTY > 0)
                            {
                                var query = from a1 in ATRList
                                            where a1.PRODNO == drSTORE.PRODNO
                                            select a1;
                                foreach (var ord in query)
                                {
                                    if (ord.ATRQTY > 0)
                                    {
                                        if (ord.ATRQTY >= QTY)
                                        {
                                            ord.ATRQTY = ord.ATRQTY - QTY;
                                            dataList.Add(new STORE_ORDER
                                            {
                                                STORE_NO = drSTORE.STORE_NO,
                                                PRODNO = drSTORE.PRODNO,
                                                ORDQTY = QTY,
                                                MEND = drSTORE.MEND,
                                                SCQC_D_ID = drSTORE.SCQC_D_ID
                                            });
                                            QTY = 0;
                                        }
                                        else
                                        {
                                            int JQTY = ord.ATRQTY;
                                            ord.ATRQTY = 0;
                                            dataList.Add(new STORE_ORDER
                                            {
                                                STORE_NO = drSTORE.STORE_NO,
                                                PRODNO = drSTORE.PRODNO,
                                                ORDQTY = JQTY,
                                                MEND = drSTORE.MEND,
                                                SCQC_D_ID = drSTORE.SCQC_D_ID
                                            });
                                            QTY = QTY - JQTY;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    var STORE = from a1 in STOREList
                                where a1.SIM_GROUP_ID == drSTORE_GROUP.GROUP_ID
                                select new { MEND = a1.SIM_MEND_ID };
                    foreach (var drSTORE in STORE)
                    {
                        UPDATEErr(drSTORE.MEND, "庫存量不足");
                    }
                }
            }
            return dataList;
        }

        public void UPDATEErr(string ID, string ErrMSG)
        {
            string Sqlstr = "UPDATE SIM_STORE_MEND SET MARK =:ErrMSG  WHERE SIM_MEND_ID=:MENO";
            OracleCommand cmd = new OracleCommand(Sqlstr, conn, trans);
            cmd.Parameters.Add(":ErrMSG", OracleType.VarChar, 32).Value = ErrMSG;
            cmd.Parameters.Add(":MENO", OracleType.VarChar, 32).Value = ID;
            cmd.ExecuteNonQuery();
        }

        public int GetSum(string GROUP_ID)
        {
            int sum = 0;
            var STORE = from a1 in STOREList
                        where a1.SIM_GROUP_ID == GROUP_ID
                        group a1 by new { a1.PRODNO } into g
                        select new { PRODNO = g.Key.PRODNO };
            foreach (var drSTORE in STORE)
            {
                var QTY = from a2 in ATRList
                          where a2.PRODNO == drSTORE.PRODNO
                          select new { ATRQTY = a2.ATRQTY };
                if (QTY.Count() > 0) sum += QTY.First().ATRQTY;
            }
            return sum;
        }

        public int GetSafeSum(string GROUP_ID, string STORE_NO)
        {
            int sum = 0;
            var STORE_GROUP = from p in STOREList
                              where p.SIM_GROUP_ID == GROUP_ID && p.STORE_NO == STORE_NO
                              group p by new { p.SIM_GROUP_ID, p.STORE_NO, p.SAFE_QTY } into g
                              select new { SAFE_QTY = g.Key.SAFE_QTY };
            foreach (var drSTORE in STORE_GROUP)
            {
                sum += drSTORE.SAFE_QTY;
            }
            return sum;
        }

        public int GetSafeSum(string GROUP_ID)
        {
            int sum = 0;
            var STORE_GROUP = from p in STOREList
                              where p.SIM_GROUP_ID == GROUP_ID
                              group p by new { p.SIM_GROUP_ID, p.STORE_NO, p.SAFE_QTY } into g
                              select new { SAFE_QTY = g.Key.SAFE_QTY };
            foreach (var drSTORE in STORE_GROUP)
            {
                sum += drSTORE.SAFE_QTY;
            }
            return sum;
        }

        public void Produce_Order()
        {

            string Sqlstr = "";
            string ORDER_NO = SerialNo.GenNo("PO_ORDER_SIM"); //GetOrderNO();
            var STORE = from a in ORDERList
                        group a by new { a.STORE_NO } into g
                        select new { STORE_NO = g.Key.STORE_NO };
            foreach (var drSTORE in STORE)
            {
                string Work_Day = "";

                OracleCommand cmd = new OracleCommand();
                int COUNT = 0;


                string POS_UUID = GuidNo.getUUID();
                Sqlstr = " select TO_CHAR(WorkingDay(:STORE_NO), 'YYYYMMDD') Work_D from dual ";
                cmd = new OracleCommand(Sqlstr, conn, trans);
                cmd.Parameters.Add(":STORE_NO", OracleType.VarChar, 32).Value = drSTORE.STORE_NO;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Work_Day = dr["Work_D"].ToString();
                }
                dr.Close();


                Sqlstr = "INSERT INTO ORDER_M (ISOK,ORDER_ID,AMOUNT,DIFFREASON, ";
                Sqlstr += "REMARK,ORDDATE,ERP_ORDER_HEADER_ID,ULSNO,CREATE_USER,CREATE_DTM, ";
                Sqlstr += "MODI_USER,MODI_DTM,ORDER_NO,ORDER_TYPE,PRE_ORDER_M_ID,STORE_NO,HQ_ORDER_M_ID,STATUS) ";
                Sqlstr += "VALUES ('',:UUID, 0,'', ";
                Sqlstr += "'',:WORK,'','','CONVERT',SYSDATE, ";
                Sqlstr += "'','',:ORDER_NO,'5','',:STORE_NO,'','50')";

                cmd = new OracleCommand(Sqlstr, conn, trans);
                cmd.Parameters.Add(":UUID", OracleType.VarChar , 32).Value = POS_UUID;
                cmd.Parameters.Add(":WORK", OracleType.VarChar, 32).Value = Work_Day;
                cmd.Parameters.Add(":ORDER_NO", OracleType.VarChar, 32).Value = ORDER_NO;
                cmd.Parameters.Add(":STORE_NO", OracleType.VarChar, 32).Value = drSTORE.STORE_NO;
                cmd.ExecuteNonQuery();

                var PRODNO = from a in ORDERList
                             where a.STORE_NO == drSTORE.STORE_NO
                             select new { PRODNO = a.PRODNO, ORDQTY = a.ORDQTY, MENO = a.MEND, SCQC_D_ID = a.SCQC_D_ID };
                foreach (var drPRODNO in PRODNO)
                {
                    COUNT += 1;
                    Sqlstr = "INSERT INTO ORDER_D(APPROVEQTY,OENO,INCOUNTQTY,INWAYQTY,DIFFQTY ";
                    Sqlstr += ",REASON,ADVISEQTY,ORDQTY,REMARK,ULSNO ";
                    Sqlstr += ",PO_NO,PRODNO_M,QTY_BDATE,QTY_EDATE,CREATE_USER ";
                    Sqlstr += ",CREATE_DTM,MODI_USER,SEQNO,PRODNO,GIFT_FALG ";
                    Sqlstr += ",MODI_DTM,DS_FLAG,CHECK_IN_QTY,STOCK_QTY,TODAY_ORDER_QTY ";
                    Sqlstr += ",ES_QTY,SHIPCONFIRM_DTM,ORDER_ID,ORDER_ITEMS_ID,PRE_ORDER_D_ID ";
                    Sqlstr += ",HQ_ORDER_STORE,LOC_ID,HQ_ADJ_ORDER_QTY) ";
                    Sqlstr += "VALUES ";
                    Sqlstr += "(0,'',0,0,0 ";
                    Sqlstr += ",'',TO_NUMBER(REMAINED_ATR(:PRODNO,:STORE_NO)),:ORDQTY,'','' ";
                    Sqlstr += ",'','','','','CONVERT' ";
                    Sqlstr += ",SYSDATE,'',:COUNT,:PRODNO,'0' ";
                    Sqlstr += ",'','N',0,TO_NUMBER(INV_ONHANDQTY(:PRODNO,:STORE_NO)),TO_NUMBER(INORDER_QTY(:STORE_NO,:PRODNO)) ";
                    Sqlstr += ",TO_NUMBER(ESTORE_ORDERQTY(:PRODNO,:STORE_NO)),SYSDATE,:UUID,POS_UUID,'' ";
                    Sqlstr += ",'',INV_GOODLOCUUID,0) ";
                    cmd = new OracleCommand(Sqlstr, conn, trans);
                    cmd.Parameters.Add(":UUID", OracleType.VarChar, 32).Value = POS_UUID;
                    cmd.Parameters.Add(":PRODNO", OracleType.VarChar, 32).Value = drPRODNO.PRODNO;
                    cmd.Parameters.Add(":STORE_NO", OracleType.VarChar, 32).Value = drSTORE.STORE_NO;
                    cmd.Parameters.Add(":ORDQTY", OracleType.Number).Value = Convert.ToDecimal(drPRODNO.ORDQTY);
                    cmd.Parameters.Add(":COUNT", OracleType.Number).Value = Convert.ToDecimal(COUNT);
                    cmd.ExecuteNonQuery();

                    Sqlstr = "UPDATE SIM_STORE_MEND SET MEND_QTY =:HAVE_QTY ,ORDER_NO=:ORDER_NO WHERE SIM_MEND_ID=:MENO";
                    cmd = new OracleCommand(Sqlstr, conn, trans);
                    cmd.Parameters.Add(":HAVE_QTY", OracleType.Number).Value = Convert.ToDecimal(drPRODNO.ORDQTY);
                    cmd.Parameters.Add(":ORDER_NO", OracleType.VarChar, 32).Value = ORDER_NO;
                    cmd.Parameters.Add(":MENO", OracleType.VarChar, 32).Value = drPRODNO.MENO;
                    cmd.ExecuteNonQuery();



                    Sqlstr = "UPDATE SCQC_D SET ORDER_QTY =:HAVE_QTY WHERE SCQC_D_ID=:SCQC_D_ID";
                    cmd = new OracleCommand(Sqlstr, conn, trans);
                    cmd.Parameters.Add(":HAVE_QTY", OracleType.Number).Value = Convert.ToDecimal(drPRODNO.ORDQTY);
                    cmd.Parameters.Add(":SCQC_D_ID", OracleType.VarChar, 32).Value = drPRODNO.SCQC_D_ID;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpDate_SIM_STORE_MEND()
        {
            string Sqlstr = "";


            OracleCommand cmd = new OracleCommand();

            var PRODNO = from a in ORDERList
                         select new { PRODNO = a.PRODNO, ORDQTY = a.ORDQTY, MENO = a.MEND, SCQC_D_ID = a.SCQC_D_ID };
            foreach (var drPRODNO in PRODNO)
            {
                Sqlstr = "UPDATE SIM_STORE_MEND SET MEND_QTY =:HAVE_QTY  WHERE SIM_MEND_ID=:MENO";
                cmd = new OracleCommand(Sqlstr, conn, trans);
                cmd.Parameters.Add(":HAVE_QTY", OracleType.Number).Value = Convert.ToDecimal(drPRODNO.ORDQTY);
                cmd.Parameters.Add(":MENO", OracleType.VarChar, 32).Value = drPRODNO.MENO;
                cmd.ExecuteNonQuery();
            }


        }

        public void Commit()
        {
            trans.Commit();
        }

        public void Rollback()
        {
            trans.Rollback();
        }

        public void Close()
        {
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
            OracleConnection.ClearAllPools();
        }

        #region Data POS_SIM_ATR
        public class POS_SIM_ATR
        {
            public string STORE_ATR_ID;
            public int ATRQTY;
            public string PRODNO;

            #region 讀取Data
            public static List<POS_SIM_ATR> GetData(OracleConnection conn, OracleTransaction trans)
            {
                List<POS_SIM_ATR> dataList = new List<POS_SIM_ATR>();
                string sqlstr = "select  STORE_ATR_ID,ATRQTY,PRODNO FROM POS_SIM_ATR ";
                sqlstr += " WHERE  TRUNC(TRANS_DATE) = TRUNC(SYSDATE)  ";
                OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
                try
                {

                    OracleDataReader dr = cmd.ExecuteReader();
                    //int i = 1;
                    while (dr.Read())
                    {
                        dataList.Add(new POS_SIM_ATR { STORE_ATR_ID = dr["STORE_ATR_ID"].ToString(), ATRQTY = int.Parse(dr["ATRQTY"].ToString()), PRODNO = dr["PRODNO"].ToString() });
                    }
                    dr.Close();
                    return dataList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #endregion
        }
        #endregion

        #region Data SIM_STORE_MEND
        public class SIM_STORE_MEND
        {
            public string SIM_MEND_ID;
            public string STORE_NO;
            public string SIM_GROUP_ID;
            public string PRODNO;
            public int ON_HAND_QTY;
            public int SAFE_QTY;
            public string SCQC_D_ID;
            public string SIM_GROUP_NAME;
            #region 讀取Data
            public static List<SIM_STORE_MEND> GetData(OracleConnection conn, OracleTransaction trans)
            {
                List<SIM_STORE_MEND> dataList = new List<SIM_STORE_MEND>();
                string sqlstr = " select   SIM_MEND_ID,SCQC_D_ID, STORE_NO, SIM_GROUP_ID,SIM_GROUP_NAME, ON_HAND_QTY, SAFE_QTY,PRODNO FROM SIM_STORE_MEND ";
                sqlstr += "WHERE TRUNC(TRANS_DATE) = TRUNC(SYSDATE) ";
                OracleCommand cmd = new OracleCommand(sqlstr, conn, trans);
                try
                {

                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dataList.Add(new SIM_STORE_MEND
                        {
                            SIM_MEND_ID = dr["SIM_MEND_ID"].ToString(),
                            STORE_NO = dr["STORE_NO"].ToString(),
                            PRODNO = dr["PRODNO"].ToString(),
                            SIM_GROUP_ID = dr["SIM_GROUP_ID"].ToString(),
                            SCQC_D_ID = dr["SCQC_D_ID"].ToString(),
                            SIM_GROUP_NAME = dr["SIM_GROUP_NAME"].ToString(),
                            ON_HAND_QTY = int.Parse(dr["ON_HAND_QTY"].ToString()),
                            SAFE_QTY = int.Parse(dr["SAFE_QTY"].ToString())
                        });
                    }
                    dr.Close();
                    return dataList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #endregion
        }
        #endregion

        #region Data STORE_ORDER
        public class STORE_ORDER
        {
            public string STORE_NO;
            public string PRODNO;
            public int ORDQTY;
            public string MEND;
            public string SCQC_D_ID;
        }
        #endregion

    }
}
