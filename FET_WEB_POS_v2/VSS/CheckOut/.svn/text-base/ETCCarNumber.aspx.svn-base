<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ETCCarNumber.aspx.cs" Inherits="VSS_CheckOut_ETCCarNumber" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ETC加值卡</title>
    <script type= "text/javascript">
        function ReturnValue(s, e) {
            if (txtCarNumber.GetText() == "") {
                alert("請輸入車號!");
            }
            else {
                returnValue = hdETC_CARDProdno.GetText() + '^' + txtCarNumber.GetText() + '^' + hdETC_CARDPrice.GetText() 
                             + '|' + hdETC_CARD_BAILProdno.GetText() + '^' + txtCarNumber.GetText() + '^' + hdETC_CARD_BAILPrice.GetText();
                window.close();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td class="tdtxt">
                    <!--車號-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, CarNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="hdETC_CARDProdno" ClientInstanceName="hdETC_CARDProdno" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hdETC_CARD_BAILProdno" ClientInstanceName="hdETC_CARD_BAILProdno" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hdETC_CARDPrice" ClientInstanceName="hdETC_CARDPrice" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hdETC_CARD_BAILPrice" ClientInstanceName="hdETC_CARD_BAILPrice" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="txtCarNumber" ClientInstanceName="txtCarNumber" runat="server" Width="100px" MaxLength="10"></dx:ASPxTextBox>
                </td>
            </tr>
        </table>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnOK" runat="server" ClientInstanceName="btnOK" Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){ ReturnValue(s, e); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" AutoPostBack="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s,e){returnValue = ''; window.close();}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
