using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advtek.Utility;
using System.Data.OracleClient;
using System.Data;
using FET.POS.Model.Facade.FacadeImpl;

namespace FET.POS.Model.Common
{
    public class CON06_PageHelper : BaseClass
    {
        public static DataTable GetProdDataByKey(string PRODNO,string SuppId)
        {
          
            DataTable dt = new DataTable();
            dt = new Product_Facade().Query_ProductConsignmentSale(PRODNO, "", "", SuppId);
            return dt;
        }

        public static DataTable GetSuppDataByKey(string SUPPNO)
        {

            DataTable dt = new DataTable();
            dt = new Supplier_Facade().Query_SuppData(SUPPNO);
            return dt;
        }
    }
}
