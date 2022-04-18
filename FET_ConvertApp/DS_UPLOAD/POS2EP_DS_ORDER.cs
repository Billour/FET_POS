using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DS_UPLOAD
{
    class POS2EP_DS_ORDER
    {

        public POS2EP_DS_ORDER() { }
        
        public string getByteString(string str, int length) 
        {
            byte[] bytes=System.Text.Encoding.Default.GetBytes(str);
            if (bytes.Length > length)
                return System.Text.Encoding.Default.GetString(bytes, 0, length);
            else
                return str;
        }

        public Dictionary<string, int> getDic() 
        {
            //<欄位名稱,欄位長度>
            Dictionary<string, int> dcProduct = new Dictionary<string, int>();
            dcProduct.Add("POS_ORDER_NUM", 10);
            dcProduct.Add("POS_ORDER_LINENUM", 3);
            dcProduct.Add("RETAIL_NUM", 10);
            dcProduct.Add("RETAIL_NAME", 50);
            dcProduct.Add("RETAIL_ADDRESS", 255);
            dcProduct.Add("RETAIL_TEL", 10);
            dcProduct.Add("RETAIL_CONTACT_NAME", 50);
            dcProduct.Add("CATEGORY_LEVEL1", 255);
            dcProduct.Add("CATEGORY_LEVEL2", 255);
            dcProduct.Add("CATEGORY_LEVEL3", 255);
            dcProduct.Add("CATEGORY_LEVEL4", 255);
            dcProduct.Add("CATEGORY_LEVEL5", 255);
            dcProduct.Add("CATEGORY_LEVEL6", 255);
            dcProduct.Add("ITEM_NUM", 20);
            dcProduct.Add("ITEM_DESC", 255);
            dcProduct.Add("QUANTITY", 10);
            dcProduct.Add("UNIT_PRICE", 10);
            dcProduct.Add("DESTINATION_ORG_ID", 2);
            dcProduct.Add("DESTINATION_SUBINVENTORY", 10);
            dcProduct.Add("DESTINATION_LOCATOR", 10);
            dcProduct.Add("SUGGESTED_VENDOR_SITE_ID", 30);
            dcProduct.Add("NEED_BY_DATE", 20); //22 系統時間
            dcProduct.Add("OUID", 2);    //23  第23欄應為OUID (FET=2, KGT=70, NCIC=?)
            dcProduct.Add("TAX_FLAG", 1);    //24  第24欄TaxFlag，格式：Y/N
            return dcProduct;
        }
        
    }
}
