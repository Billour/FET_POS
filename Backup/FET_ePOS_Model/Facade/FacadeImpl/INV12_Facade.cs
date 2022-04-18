using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using FET.POS.Model.DTO;
using Advtek.Utility;
using System.Data.OracleClient;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class INV12_Facade
    {
        /// <summary>
        /// 新增進貨單
        /// </summary>
        /// <param name="ds"></param>
        public void InsertNPOrder(INV12_NPOrder_DTO ds)
        {
            OracleConnection objConn = null;
            OracleTransaction objTX = null;

            try
            {
                objConn = OracleDBUtil.GetConnection();
                objTX = objConn.BeginTransaction();

                INVENTORY_Facade Inventory = new INVENTORY_Facade();
                OracleDBUtil.Insert(objTX, ds.NP_ORDER_M);
                OracleDBUtil.Insert(objTX, ds.NP_ORDER_D);

                //進貨單號
                string NP_ORDER_ON = ds.NP_ORDER_M.Rows[0]["NP_ORDER_ON"].ToString();
                //門市編號
                string STORE_NO = ds.NP_ORDER_M.Rows[0]["STORE_NO"].ToString();
                //銷售倉編號
                string Stock = FET.POS.Model.Common.Common_PageHelper.GetGoodLOCUUID();

                foreach (INV12_NPOrder_DTO.NP_ORDER_DRow dr in ds.NP_ORDER_D.Rows)
                {
                    //異動庫存 PK_INVENTORY_SHIPACCEPT(); 
                    string Code = "";
                    string Message = "";

                    Inventory.PK_INVENTORY_SHIPACCEPT(objTX, "1", dr.PRODNO, STORE_NO,
                       Stock, NP_ORDER_ON, Convert.ToInt32(dr.ORDER_QTY),
                       dr.MODI_USER, dr.NP_ORDER_D_ID, ref Code, ref Message);

                    if (Code != "000") throw new Exception("進貨單號：" + NP_ORDER_ON + ", 商品料號：" + dr.PRODNO + "異動庫存檔失敗. ERROR_MSG:" + Message);
                }

                objTX.Commit();
            }
            catch (Exception ex)
            {
                objTX.Rollback();
                throw ex;
            }
            finally
            {
                objTX = null;

                if (objConn.State == ConnectionState.Open) objConn.Close();
                objConn.Dispose();
                OracleConnection.ClearAllPools();
            }
        }
    }
}
