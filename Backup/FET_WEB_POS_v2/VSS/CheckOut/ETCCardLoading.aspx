<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ETCCardLoading.aspx.cs" Inherits="VSS_CheckOut_ETCCardLoading" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ETC Card 加值</title>
    <object id="oECR" classid="CLSID:9126B51B-F1B9-4AED-AA4C-131FC05482B3" codebase="ECRAPI.CAB#version=1,0,0,0">
    </object>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
       
        //ETC Card 加值
        function Call_FETC_Loading(s,e) {
            var oECR = new ActiveXObject("ProjECR.ECRAPI");
            var result = oECR.FETC_Loading(lblPAID_AMOUNT.GetText(), lblSTORE_NO.GetText());
            
            returnValue = result;
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
                        加值金額：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="lblPAID_AMOUNT" ClientInstanceName="lblPAID_AMOUNT" runat="server" ReadOnly="true" Border-BorderStyle="None" MaxLength="8"></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>

        <div align="center">
            <table>
                <tr>
                    <td style="width: 300px; height: 200px; background-color: Silver" align="center" valign="middle">
                        請於刷卡機插入ETC 卡，加值金額
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){ Call_FETC_Loading(s,e); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
