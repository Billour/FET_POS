using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;
using FET.POS.Model.DTO;

namespace FET.POS.Model.Facade.FacadeImpl
{
    public class INVENTORY_Facade : BaseClass
    {

        public void PK_INVENTORY_SHIPCONFIRM(OracleTransaction objTX, string I_SERVICE_TYPE, string I_PRODNO
                                            , string I_STORE_NO, string I_STOCK_ID, string I_SHEET_NO
                                            , int I_INV_QTY, string I_USER, string I_SOURCE_REFERENCE
                                            , ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.SHIPCONFIRM");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;              //服務系統
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                          //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                      //門市代號
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                      //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                      //交易序號
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                               //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                              //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;      //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;    //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳訊息

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_SHIPACCEPT(OracleTransaction objTX, string I_SERVICE_TYPE, string I_PRODNO
                                            , string I_STORE_NO, string I_STOCK_ID, string I_SHEET_NO
                                            , int I_INV_QTY, string I_USER, string I_SOURCE_REFERENCE
                                            , ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.SHIPACCEPT");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;                  //服務系統
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                              //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                          //門市代號
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                          //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                          //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                                   //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                                  //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;          //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;        //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;     //回傳訊息

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_REJECTINVENTORY(OracleTransaction objTX, string I_SERVICE_TYPE, string I_PRODNO
                                            , string I_STORE_NO, string I_STOCK_ID, string I_SHEET_NO
                                            , int I_INV_QTY, string I_USER, string I_SOURCE_REFERENCE
                                            , ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {

                oraCmd = new OracleCommand("PK_INVENTORY.REJECTINVENTORY");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;              //服務系統
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                          //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                      //門市代碼
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                      //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                      //交易序號
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                               //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                              //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;      //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;    //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳訊息

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_SALE(OracleTransaction objTX, string I_SERVICE_TYPE, string I_PRODNO, string I_STORE_NO, 
                                        string I_STOCK_ID, string I_SHEET_NO, int I_INV_QTY, string I_USER, string I_SOURCE_REFERENCE, 
                                        ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.SALE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                O_RT_CODE = "999";
                O_RT_MESSAGE = "扣庫存失敗!";
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_CHANGE(OracleTransaction objTX, string I_SERVICE_TYPE, string I_PRODNO, string I_STORE_NO, string I_STOCK_ID,
                                        string I_SHEET_NO, int I_INV_QTY, string I_USER, string I_SOURCE_REFERENCE, ref string O_RT_CODE,
                                        ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.CHANGE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                O_RT_CODE = "999";
                O_RT_MESSAGE = "退庫存失敗!";
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_RETURN(OracleTransaction objTX, string I_SERVICE_TYPE, string I_PRODNO, string I_STORE_NO, string I_STOCK_ID, string I_SHEET_NO,
                                        int I_INV_QTY, string I_USER, string I_SOURCE_REFERENCE, ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.RETURN");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();
                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString();
            }
            catch (Exception ex)
            {
                O_RT_CODE = "999";
                O_RT_MESSAGE = "退庫存失敗!";
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_SHIFTSTOCK(OracleTransaction objTX, string I_SERVICE_TYPE, string I_INV_TRAN_TYPE
                                            , string I_PRODNO, string I_STORE_NO, string I_STOCK_ID
                                            , string I_SHEET_NO, int I_INV_QTY, string I_USER
                                            , string I_SOURCE_REFERENCE, ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.SHIFTSTOCK");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;              //服務系統
                oraCmd.Parameters.Add(new OracleParameter("I_INV_TRAN_TYPE", OracleType.VarChar, 2000)).Value = I_INV_TRAN_TYPE;            //庫存異動交易類型
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                          //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                      //門市代號
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                      //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                      //交易序號
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                               //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                              //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;      //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;    //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳訊息

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_ADJUST(OracleTransaction objTX, string I_SERVICE_TYPE, string I_INV_TRAN_TYPE
                                        , string I_PRODNO, string I_STORE_NO, string I_STOCK_ID
                                        , string I_SHEET_NO, int I_INV_QTY, string I_USER
                                        , string I_SOURCE_REFERENCE, ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.ADJUST");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;                  //服務系統
                oraCmd.Parameters.Add(new OracleParameter("I_INV_TRAN_TYPE", OracleType.VarChar, 2000)).Value = I_INV_TRAN_TYPE;                //庫存異動交易類型
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                              //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                          //門市代號
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                          //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                          //交易序號
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                                   //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                                  //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;          //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;        //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;     //回傳訊息

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_MAKEANINVENTORY(OracleTransaction objTX, string I_SERVICE_TYPE, string I_INV_TRAN_TYPE
                                        , string I_PRODNO, string I_STORE_NO, string I_STOCK_ID
                                        , string I_SHEET_NO, int I_INV_QTY, string I_USER
                                        , string I_SOURCE_REFERENCE, ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.MAKEANINVENTORY");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;              //服務系統
                oraCmd.Parameters.Add(new OracleParameter("I_INV_TRAN_TYPE", OracleType.VarChar, 2000)).Value = I_INV_TRAN_TYPE;            //庫存異動交易類型
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                          //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                      //門市代號
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                      //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                      //交易序號
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                               //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                              //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;          //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;    //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳訊息  

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

        public void PK_INVENTORY_TRANSFER(OracleTransaction objTX, string I_SERVICE_TYPE, string I_INV_TRAN_TYPE
                                        , string I_PRODNO, string I_STORE_NO, string I_STOCK_ID
                                        , string I_SHEET_NO, int I_INV_QTY, string I_USER
                                        , string I_SOURCE_REFERENCE, ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.TRANSFER");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;              //服務系統
                oraCmd.Parameters.Add(new OracleParameter("I_INV_TRAN_TYPE", OracleType.VarChar, 2000)).Value = I_INV_TRAN_TYPE;            //庫存異動交易類型
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                          //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                      //門市代號
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                      //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                      //交易序號
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                               //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                              //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;      //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;    //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳訊息 

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

         public void PK_INVENTORY_COLLARUSE(OracleTransaction objTX, string I_SERVICE_TYPE, string I_INV_TRAN_TYPE
                                        , string I_PRODNO, string I_STORE_NO, string I_STOCK_ID
                                        , string I_SHEET_NO, int I_INV_QTY, string I_USER
                                        , string I_SOURCE_REFERENCE, ref string O_RT_CODE, ref string O_RT_MESSAGE)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.COLLARUSE");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_SERVICE_TYPE", OracleType.VarChar, 2000)).Value = I_SERVICE_TYPE;              //服務系統           
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;                          //商品料號
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;                      //門市代號
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;                      //倉庫代碼
                oraCmd.Parameters.Add(new OracleParameter("I_SHEET_NO", OracleType.VarChar, 2000)).Value = I_SHEET_NO;                      //交易序號
                oraCmd.Parameters.Add(new OracleParameter("I_INV_QTY", OracleType.Number)).Value = I_INV_QTY;                               //庫存異動量
                oraCmd.Parameters.Add(new OracleParameter("I_USER", OracleType.VarChar, 2000)).Value = I_USER;                              //人員
                oraCmd.Parameters.Add(new OracleParameter("I_SOURCE_REFERENCE", OracleType.VarChar, 2000)).Value = I_SOURCE_REFERENCE;      //呼叫來源_UUID
                oraCmd.Parameters.Add(new OracleParameter("O_RT_CODE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output;    //回傳碼
                oraCmd.Parameters.Add(new OracleParameter("O_RT_MESSAGE", OracleType.VarChar, 2000)).Direction = ParameterDirection.Output; //回傳訊息

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

                O_RT_CODE = oraCmd.Parameters["O_RT_CODE"].Value.ToString();
                O_RT_MESSAGE = oraCmd.Parameters["O_RT_MESSAGE"].Value.ToString(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

         public void PK_INVENTORY_CHECKINVENTORY(OracleTransaction objTX, string I_PRODNO, string I_STORE_NO, string I_STOCK_ID, ref int O_ON_HAND_QTY)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY.CHECKINVENTORY");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("I_PRODNO", OracleType.VarChar, 2000)).Value = I_PRODNO;
                oraCmd.Parameters.Add(new OracleParameter("I_STORE_NO", OracleType.VarChar, 2000)).Value = I_STORE_NO;
                oraCmd.Parameters.Add(new OracleParameter("I_STOCK_ID", OracleType.VarChar, 2000)).Value = I_STOCK_ID;
                oraCmd.Parameters.Add(new OracleParameter("O_ON_HAND_QTY", OracleType.Number)).Direction = ParameterDirection.Output;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

         public void INV_ONHANDQTY(OracleTransaction objTX, string V_PRODNO, string V_STORE_NO)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("INV_ONHANDQTY");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                oraCmd.Parameters.Add(new OracleParameter("V_PRODNO", OracleType.VarChar, 2000)).Value = V_PRODNO;
                oraCmd.Parameters.Add(new OracleParameter("V_STORE_NO", OracleType.VarChar, 2000)).Value = V_STORE_NO;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

         public void INV_GOODLOCUUID(OracleTransaction objTX)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("INV_GOODLOCUUID");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

         public void PK_INVENTORY(OracleTransaction objTX)
        {
            OracleCommand oraCmd = null;

            try
            {
                oraCmd = new OracleCommand("PK_INVENTORY");
                oraCmd.CommandType = System.Data.CommandType.StoredProcedure;

                oraCmd.Connection = objTX.Connection;
                oraCmd.Transaction = objTX;
                oraCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oraCmd.Dispose();
            }
        }

    }
}