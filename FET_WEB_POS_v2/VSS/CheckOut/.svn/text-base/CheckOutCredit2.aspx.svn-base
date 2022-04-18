<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCredit2.aspx.cs" Inherits="VSS_CheckOut_CheckOutCredit2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信用卡金額輸入</title>
    <object id="oECR" classid="CLSID:9126B51B-F1B9-4AED-AA4C-131FC05482B3" codebase="ECRAPI.CAB#version=1,0,0,0">
    </object>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
       
        //刷退
        function Call_Credit_Card_Refund(s,e) {
            returnValue = '0000';
            window.close();
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table align="center">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        店號：
                    </td>
                    <td class="tdval">
                         <dx:ASPxTextBox ID="lblSTORE_NO" ClientInstanceName="lblSTORE_NO" runat="server" ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        信用卡卡號：
                    </td>
                    <td class="tdval">
                         <dx:ASPxLabel ID="lblCREDIT_CARD_NO" runat="server"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        應退回金額：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="lblPAID_AMOUNT" ClientInstanceName="lblPAID_AMOUNT" runat="server" ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>

        <div align="center">
            <table>
                <tr>
                    <td style="width: 300px; height: 200px; background-color: Silver" align="center" valign="middle">
                        請記得手動刷退原交易信用卡金額
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){ Call_Credit_Card_Refund(s,e); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
