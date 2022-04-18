<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LEA04.aspx.cs" Inherits="VSS_LEA_LEA04" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="func">
        <div class="titlef" align="left">
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
                        <dx:ASPxTextBox ID="tbCustPhNumber" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--客戶姓名-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="tbCusName" runat="server">
                        </dx:ASPxTextBox>
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
                        <dx:ASPxTextBox ID="TextBox3" runat="server">
                        </dx:ASPxTextBox>
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
                        <dx:ASPxDateEdit ID="tbxResTakeDate" runat="server">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt">
                        <!--預定歸還日-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ReturnDueDate %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxDateEdit ID="tbxResReturnDate" runat="server">
                        </dx:ASPxDateEdit>
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
                        <dx:ASPxTextBox ID="TextBox4" runat="server" Width="98%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--出國時間-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, DurationAbroad %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="1">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td align="left">
                                    <dx:ASPxDateEdit ID="tbxResTakeDate0" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td align="left">
                                    <dx:ASPxDateEdit ID="tbxResTakeDate1" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
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
                        <dx:ASPxDateEdit ID="tbxResTakeDate2" runat="server">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt">
                        <!--實際歸還日-->
                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, ActualReturnDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxDateEdit ID="tbxResTakeDate3" runat="server">
                        </dx:ASPxDateEdit>
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
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td align="right">
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, SaveReservation %>" 
                            OnClick="btnSave_Click" />
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="btnReserCancel" runat="server" Text="<%$ Resources:WebResources, CancelReservation %>" />
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="btnCheck" runat="server" Text="<%$ Resources:WebResources, CheckOut %>" />
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="btnReturn" runat="server" Text="<%$ Resources:WebResources, Return %>" />
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
    </div>
</asp:Content>
