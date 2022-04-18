using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;  


namespace Advtek.Utility 
{ 
   public class DESCryptography
    {
       private static LogUtil Logger = new LogUtil(typeof(DESCryptography));

       public DESCryptography() { }

          /// <summary> 
       /// DES 加密字串 
       /// </summary> 
       /// <param name="original">原始字串</param> 
       /// <param name="key">Key，長度必須為 8 個 ASCII 字元</param> 
       /// <param name="iv">IV，長度必須為 8 個 ASCII 字元</param> 
       /// <returns></returns> 
       public static string EncryptDES(string original, string key, string iv)
       {
           try
           {
               DESCryptoServiceProvider des = new DESCryptoServiceProvider();
               des.Key = Encoding.ASCII.GetBytes(key);
               des.IV = Encoding.ASCII.GetBytes(iv);
               byte[] s = Encoding.ASCII.GetBytes(original);
               ICryptoTransform desencrypt = des.CreateEncryptor();
               return BitConverter.ToString(desencrypt.TransformFinalBlock(s, 0, s.Length)).Replace("-", string.Empty);
           }
           catch { return original; }
       }

       /// <summary> 
       /// DES 解密字串 
       /// </summary> 
       /// <param name="hexString">加密後 Hex String</param> 
       /// <param name="key">Key，長度必須為 8 個 ASCII 字元</param> 
       /// <param name="iv">IV，長度必須為 8 個 ASCII 字元</param> 
       /// <returns></returns> 
       public static string DecryptDES(string hexString, string key, string iv)
       {
           try
           {
               DESCryptoServiceProvider des = new DESCryptoServiceProvider();
               des.Key = Encoding.ASCII.GetBytes(key);
               des.IV = Encoding.ASCII.GetBytes(iv);

               byte[] s = new byte[hexString.Length / 2];
               int j = 0;
               for (int i = 0; i < hexString.Length / 2; i++)
               {
                   s[i] = Byte.Parse(hexString[j].ToString() + hexString[j + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                   j += 2;
               }
               ICryptoTransform desencrypt = des.CreateDecryptor();
               return Encoding.ASCII.GetString(desencrypt.TransformFinalBlock(s, 0, s.Length));
           }
           catch { return hexString; }
       } 
 
    }
}
