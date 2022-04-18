<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutCash.aspx.cs" Inherits="VSS_CheckOut_CheckOutCash" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>現金金額輸入</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
//        $(function() {
//            $("#btnOK").click(function() {
//                var r = parseInt(txtCash.GetText());
//                if (r.toString() == 'NaN') {
//                    alert('請輸入金額!');
//                }
//                else {
//                    returnValue = 'CASH,' + r;
//                    window.close();
//                }
//            });
//        });


        function keyDown(s, e) {
            if (event.keyCode == "13") {
                $("#btnOK").click();
            }
        }

        function ReturnValue(s, e) {
            var r = parseInt(Number(txtCash.GetText()));
            if (r.toString() == 'NaN') {
                alert('請輸入金額!');
            }
            else {
                returnValue = 'CASH,' + r;
                window.close();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            現金金額：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtCash" ClientInstanceName="txtCash" Width="100" runat="server" MaxLength="8">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="^-?\d*" ErrorText="請輸入數字" />
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                                <ClientSideEvents KeyPress="function(s,e) {keyDown(s, e); }" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <table cellpadding="0" cellspacing="0" border="0" align="center">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnOK" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, OK %>"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) { ReturnValue(s,e); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            UseSubmitBehavior="false" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e){returnValue='';window.close();}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
