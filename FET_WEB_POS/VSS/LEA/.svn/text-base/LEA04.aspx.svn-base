<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA04.aspx.cs" Inherits="VSS_LEA04_LEA04" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="func">
        <div class="titlef">
            <!--可租賃設備預約/新增/領取/歸還-->
            <%= string.Format("{0}{1}/{2}/{3}/{4}", GetGlobalResourceObject("WebResources", "LeasableEquipment"),
                                GetGlobalResourceObject("WebResources", "Add"),
                                GetGlobalResourceObject("WebResources", "Reserve"),
                                GetGlobalResourceObject("WebResources", "Collect"),
                                GetGlobalResourceObject("WebResources", "Return"))%>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--租賃單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, LeaseOrderNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblLEANo" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdtxt">
                        <!--預約日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReservationDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lblResDate" runat="server" Text="(系統自帶)"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="00-未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--手機類型-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, MobileType %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label2" runat="server" Text="(系統自帶)"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--手機序號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, MobileIdentityNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lblMobileNo" runat="server">(系統自帶)</asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--庫存地點-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text="(系統自帶)"></asp:Label>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--維護人員-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="tbCustPhNumber" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--客戶姓名-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="tbCusName" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--客戶等級-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CustomerGrade %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--性別-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Gender %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:RadioButtonList ID="ddlSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True">男</asp:ListItem>
                            <asp:ListItem>女</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--預定領取日-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, CollectionDueDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <cc1:postbackDate_TextBox ID="tbxResTakeDate" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--預定歸還日-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ReturnDueDate %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <cc1:postbackDate_TextBox ID="tbxResReturnDate" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--領取方式-->
                         <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, CollectionMethod %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Selected="True">親至門市</asp:ListItem>
                            <asp:ListItem>快遞送貨</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--地址-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Address %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:TextBox ID="TextBox4" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--出國時間-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, DurationAbroad %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="tbxResTakeDate0" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    &nbsp;<asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="tbxResTakeDate1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--實際領取日-->
                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ActualCollectionDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <cc1:postbackDate_TextBox ID="tbxResTakeDate2" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--實際歸還日-->
                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, ActualReturnDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <cc1:postbackDate_TextBox ID="tbxResTakeDate3" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--是否有賠償-->
                         <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, CompensationRequired %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Selected="True">是</asp:ListItem>
                            <asp:ListItem>否</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, SaveReservation %>" OnClick="btnSave_Click" />
            <asp:Button ID="btnReserCancel" runat="server" Text="<%$ Resources:WebResources, CancelReservation %>" />
            <asp:Button ID="btnCheck" runat="server" Text="<%$ Resources:WebResources, CheckOut %>" />
            <asp:Button ID="btnReturn" runat="server" Text="<%$ Resources:WebResources, Return %>" />
            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
        </div>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
