//---------------------------------------------------------------------------- 
//專案名稱	公用函數
//程式功能	證件相關檢查
//---------------------------------------------------------------------------- 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Advtek.Utility
{

    public class Check_ID
    {
        #region Check_TW_ID() 驗證台灣身分證字號(10碼)
        public int Check_TW_ID(string strID)
        {
            string nFirst = "";
            int nTol = 0, nCount = 0, chk_value = 9, ckint = -1;

            strID = strID.Trim();

            // 確定台灣身分證號碼有10碼，最後一碼為檢查碼
            if (strID.Length == 10)
            {
                nFirst = strID.Substring(2, 1);
                if (nFirst == "1" || nFirst == "2")
                {
                    if (int.TryParse(strID.Substring(1, 9), out ckint))
                    {
                        nFirst = strID.Substring(0, 1).ToUpper();
                        switch (nFirst)
                        {
                            case "B":
                            case "N":
                            case "Z":
                                nTol = 0;
                                break;
                            case "A":
                            case "M":
                            case "W":
                                nTol = 1;
                                break;
                            case "K":
                            case "L":
                            case "Y":
                                nTol = 2;
                                break;
                            case "J":
                            case "V":
                            case "X":
                                nTol = 3;
                                break;
                            case "H":
                            case "U":
                                nTol = 4;
                                break;
                            case "G":
                            case "T":
                                nTol = 5;
                                break;
                            case "F":
                            case "S":
                                nTol = 6;
                                break;
                            case "E":
                            case "R":
                                nTol = 7;
                                break;
                            case "D":
                            case "O":
                            case "Q":
                                nTol = 8;
                                break;
                            case "C":
                            case "I":
                            case "P":
                                nTol = 9;
                                break;
                            default:
                                nTol = 99;
                                break;
                        }

                        if (nTol != 99)
                        {
                            for (nCount = 1; nCount < 10; nCount++)
                            {
                                nTol += int.Parse(strID.Substring(nCount, 1)) * (9 - nCount);
                            }

                            nTol += int.Parse(strID.Substring(9, 1));

                            if ((nTol % 10) == 0)
                                chk_value = 0;
                            else
                                chk_value = 1;
                        }
                        else
                            chk_value = 2;
                    }
                    else
                        chk_value = 4;
                }
                else
                    chk_value = 5;
            }
            else
                chk_value = 3;

            return chk_value;
        }
        #endregion

        #region Check_TW_INV() 驗證台灣營利事業統一編號 (8碼)
        public int Check_TW_INV(string strID)
        {
            int chk_value = 0, ckint = 0;
            int[] intX = new int[8];
            int[] intY = new int[8];
            int intMod = 0;				// 餘數變數 
            int intSum = 0;				// 合計數變數 

            chk_value = 9;

            // 營利事業統一編號
            if (strID.Length == 8)
            {
                if (int.TryParse(strID, out ckint))
                {
                    intX[0] = int.Parse(strID.Substring(0, 1)) * 1;		// 第 1位數 * 1 
                    intX[1] = int.Parse(strID.Substring(1, 1)) * 2;		// 第 2位數 * 2 
                    intX[2] = int.Parse(strID.Substring(2, 1)) * 1;		// 第 3位數 * 1 
                    intX[3] = int.Parse(strID.Substring(3, 1)) * 2;		// 第 4位數 * 2 
                    intX[4] = int.Parse(strID.Substring(4, 1)) * 1;		// 第 5位數 * 1 
                    intX[5] = int.Parse(strID.Substring(5, 1)) * 2;		// 第 6位數 * 2 
                    intX[6] = int.Parse(strID.Substring(6, 1)) * 4;		// 第 7位數 * 4 
                    intX[7] = int.Parse(strID.Substring(7, 1)) * 1;		// 第 8位數 * 1 

                    intY[0] = intX[1] / 10;								// 第 2位數的乘積可能大於10, 除以10, 取其整數 
                    intY[1] = intX[1] % 10;								// 第 2位數的乘積可能大於10, 除以10, 取其餘數 
                    intY[2] = intX[3] / 10;								// 第 4位數的乘積可能大於10, 除以10, 取其整數 
                    intY[3] = intX[3] % 10;								// 第 4位數的乘積可能大於10, 除以10, 取其餘數 
                    intY[4] = intX[5] / 10;								// 第 6位數的乘積可能大於10, 除以10, 取其整數 
                    intY[5] = intX[5] % 10;								// 第 6位數的乘積可能大於10, 除以10, 取其餘數 
                    intY[6] = intX[6] / 10;								// 第 7位數的乘積可能大於10, 除以10, 取其整數 
                    intY[7] = intX[6] % 10;								// 第 7位數的乘積可能大於10, 除以10, 取其餘數 

                    intSum = intX[0] + intX[2] + intX[4] + intX[7] + intY[0] + intY[1] + intY[2] + intY[3] + intY[4] + intY[5] + intY[6] + intY[7];
                    intMod = intSum % 10;

                    // 判斷 1: 第 7 位數是否為 7 時
                    if (strID.Substring(6, 1) == "7")
                    {
                        // 判斷 2: 餘數是否為 0 
                        if (intMod == 0)
                            chk_value = 0;
                        else
                        {
                            intSum = intSum + 1;

                            // 再行計算 1999/11/19 修正 
                            intMod = intSum % 10;
                            if (intMod == 0)
                                chk_value = 0;
                            else
                                chk_value = 1;
                        }
                    }
                    else
                    {
                        if (intMod == 0)
                            chk_value = 0;
                        else
                            chk_value = 1;
                    }
                }
                else
                    chk_value = 2;
            }
            else
                chk_value = 3;

            return chk_value;
        }

        #endregion

    }
}