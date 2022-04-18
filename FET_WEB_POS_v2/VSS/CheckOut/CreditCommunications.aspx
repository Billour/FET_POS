<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreditCommunications.aspx.cs" Inherits="VSS_CheckOut_CreditCommunications" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>授信通聯</title>
    <script type="text/javascript" src="../../ClientUtility/CheckDate.js"></script>
    <script type= "text/javascript">
         function checkCellphoneNum(s, e) {
            if (s.GetText() != '') {
                var IsNumber = Number(s.GetText());
                if (isNaN(IsNumber) || Number(s.GetText()) <= 0) {
                    s.SetText(null);
                    alert('門號僅可輸入數字，請重新輸入!');
                }
                else if (s.GetText().length < 10) {
                    alert('門號輸入不正確，請重新輸入!');
                }
            }
         }
         function checkDate(s, e) {
             var x = txtSDate.GetValue();
             var y = txtEDate.GetValue();

             if (x == null) { x = ""; }
             if (y == null) { y = ""; }

             if (x != "" && y != "") {
                
                 e.isValid = (x <= y);
                 if (!e.isValid) {
                     alert("授信通聯日訖不允許小於授信通聯日起，請重新輸入!");
                     s.SetValue(null);
                 }
                 else {
                    var lblDaily = hdDaily.GetText();
                    var lblBasic = hdBasic.GetText();
                    var Days = DateDiff(x, y);
                    if (Number(Days) > 183) {
                         alert("授信通聯日不可超過183天，請重新輸入!");
                         s.SetValue(null);
                    }
                    else {
                        //lblTotalAmount.SetText(Number(lblDaily * Number(Days)) + Number(lblBasic) + " 元");
                        lblDays.SetText(Days + " 天");
                        lblTotalAmount.SetText(lblBasic + " + " + (lblDaily * Number(Days)) + " 元");
                    }
                 }
            }
         }
         function  DateDiff(beginDate,  endDate) {    
             iDays = parseInt(Math.abs(beginDate - endDate) / 1000 / 60 / 60 / 24) + 1;    //轉換為天數 
             return iDays;  
         }    
         function ReturnValue(s, e) {
            if (txtMobileNumber.GetText() == "") {
                alert("請輸入客戶門號!");
            }
            else if (txtMobileNumber.GetText().length < 10) {
                alert('門號輸入不正確，請重新輸入!');
            }
            else if (txtSDate.GetText() == "") {
                alert("請輸入授信通聯起日!");
            }
            else if (txtEDate.GetText() == "") {
                alert("請輸入授信通聯訖日!");
            }
            else {
                var plusIndex = lblTotalAmount.GetText().indexOf('+');
                var totalAmount = lblTotalAmount.GetText().substring(plusIndex+2, lblTotalAmount.GetText().length-2);
                returnValue = 'CommAdd^' + hdBasicProdno.GetText() + '^' + txtMobileNumber.GetText() + '^' + hdBasic.GetText() 
                             + '|CommAdd^' + hdProdno.GetText() + '^' + txtMobileNumber.GetText() + '^' + totalAmount;
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
                    <!--客戶門號-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtMobileNumber" ClientInstanceName="txtMobileNumber" runat="server" Width="100px" MaxLength="10">
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="門號僅可輸入數字，請重新輸入。"/>
                        </ValidationSettings>
                        <ClientSideEvents TextChanged="function(s,e){ checkCellphoneNum(s, e); }" />
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt" nowrap="nowrap">
                    <!--授信通聯日期-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CreditCommunicationDate %>"></asp:Literal>：
                </td>
                <td class="tdval" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="dateEditSDate" runat="server" ClientInstanceName="txtSDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="dateEditEDate" runat="server" ClientInstanceName="txtEDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--天數-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Days %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="lblDays" ClientInstanceName="lblDays" runat="server" ForeColor="Red" Border-BorderStyle="None" ReadOnly="true"></dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblInfo" ClientInstanceName="lblInfo" runat="server" Text=""></dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--總金額-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TotalAmount %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="hdProdno" ClientInstanceName="hdProdno" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hdBasicProdno" ClientInstanceName="hdBasicProdno" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hdDaily" ClientInstanceName="hdDaily" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="hdBasic" ClientInstanceName="hdBasic" runat="server" ClientVisible="false"></dx:ASPxTextBox>
                    <dx:ASPxTextBox ID="lblTotalAmount" ClientInstanceName="lblTotalAmount" runat="server" ForeColor="Red" Border-BorderStyle="None" ReadOnly="true"></dx:ASPxTextBox>
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
