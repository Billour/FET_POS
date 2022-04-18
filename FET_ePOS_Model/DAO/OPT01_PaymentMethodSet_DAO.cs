using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OracleClient;
using Advtek.Utility;

namespace FET.POS.Model.DAO
{
    public class OPT01_PaymentMethodSet_DAO : BaseClass
    {
        public int Insert(ref OracleTransaction oracleTX, DataTable dt)
        {
            int result = 0;

            try
            {
                OracleDBUtil.Insert(oracleTX, dt);
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }

            return result;
        }

        public int Update(ref OracleTransaction oracleTX, DataTable dt, String Id)
        {
            int result = 0;

            try
            {
                OracleDBUtil.UPDDATEByUUID(oracleTX, dt, Id);
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                throw ex;
            }

            return result;
        }        
        
    }
}
