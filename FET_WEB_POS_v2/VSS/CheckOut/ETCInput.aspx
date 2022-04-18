<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ETCInput.aspx.cs" Inherits="VSS_CheckOut_ETCInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
<title>ETC加值</title>

<script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

<script src="../../ClientUtility/Common.js" type="text/javascript"></script>

<script type="text/javascript">

function ReturnValue(s, e) {
    returnValue = 'ETCAdd^' + Number(TOTAL_AMOUNT.GetText()).toFixed(0);
    window.close();
}

function DisabledDISType(s, e) {
    btnOK.SetEnabled(false);
    if (s.GetText() != '') {
        var IsNumber = Number(s.GetText());
        if (isNaN(IsNumber) || Number(s.GetText()) <= 0) {
            s.SetText(null);
            alert('輸入字串非數字格式且不允許小於等於0，請重新輸入!');
        } else {
            if (!isInteger(s.GetText())) {
                s.SetText(null);
                alert('折抵金額不為整數，請重新輸入!');
            } else {
                if (Number(s.GetText()) < Number(window.document.getElementById("hidLowLimitAmt").value)) {
                    s.SetText(null);
                    alert('加值金額低於最低加值金額，請重新輸入!');
                } else if (!isInteger("" + (Number(s.GetText()) / 100))) {
                    s.SetText(null);
                    alert('加值金額需以百元為單位，請重新輸入!');
                } else {
                    btnOK.SetEnabled(true);
                }
            }
        }
    } 
}
</script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidLowLimitAmt" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ScriptManager>
    <div style="text-align: center">
        <table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <tr>
                        <td class="tdtxt">
                            最低加值金額：
                        </td>
                        <td class="tdval">
                            <dx:ASPxLabel ID="lblLowLimitAmt" ClientInstanceName="lblLowLimitAmt" runat="server"
                                Text="0">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            加值金額：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtTOTAL_AMOUNT" ClientInstanceName="TOTAL_AMOUNT" runat="server"
                                Width="100px" MaxLength="8" HorizontalAlign="Right">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e){ DisabledDISType(s, e); }" />
                            </dx:ASPxTextBox>
                            (每次加值以百元為單位)
                        </td>
                    </tr>
                </ContentTemplate>
            </asp:UpdatePanel>
        </table>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table border="0" cellpadding="0" cellspacing="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnOK" runat="server" ClientInstanceName="btnOK" ClientEnabled="false"
                            Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){ ReturnValue(s, e); }" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                            AutoPostBack="false" CausesValidation="false">
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
