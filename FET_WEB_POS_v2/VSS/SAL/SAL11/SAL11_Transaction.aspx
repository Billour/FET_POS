<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL11_Transaction.aspx.cs" Inherits="VSS_SAL_SAL11_Transaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>交易補登選項</title>
</head>
<body>
    <form id="form1" runat="server">    
    <div class="func">
        <div>
            <table cellpadding="0" cellspacing="0" border="0" align="center"> 
                <tr>
                    <td align="left">
                        <dx:ASPxRadioButton ID="Radio1" ClientInstanceName="radio1" runat="server" GroupName="radioChoose" Text="由交易未結清單選取資料" Checked="true">
                        </dx:ASPxRadioButton>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <dx:ASPxRadioButton ID="Radio2" ClientInstanceName="radio2" runat="server" GroupName="radioChoose" Text="手動輸入交易資料">                            
                        </dx:ASPxRadioButton>
                    </td>
                </tr>
                <tr><td>&nbsp;</td></tr>
                <tr>
                    <td align="center">
                        <dx:ASPxButton ID="btnCommit" runat="server" 
                            Text="<%$ Resources:WebResources, Ok %>" AutoPostBack="false" >
                      <%--  <ClientSideEvents Click="function(s, e) {
                            if (radio1.GetChecked()) {
                                if (location.search == '?s=2') {
                                    parent.location='../TSAL05/TSAL05.aspx?s=2';
                                }
                                else {
                                parent.location='../SAL05/SAL05.aspx?s=1';
                                // parent.redirect('../SAL05/SAL05.aspx?s=1');
                                }
                            }
                            hidePopupWindow();}" /> --%>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>