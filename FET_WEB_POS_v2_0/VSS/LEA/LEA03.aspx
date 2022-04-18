<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LEA03.aspx.cs" Inherits="VSS_LEA03_LEA03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    
    <div class="titlef" align="left">
            <!--已租賃設備查詢-->
            <asp:Literal ID="Literal11" runat="server" Text="已租賃設備查詢"></asp:Literal>
    </div>

    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--手機地點-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, MobileLocation %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="100">
                        <Items>
                            <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                            <dx:ListEditItem Value="ALL" Text="ALL" />
                            <dx:ListEditItem Value="台北館前店" Text="台北館前店" />
                            <dx:ListEditItem Value="台北遠企店" Text="台北遠企店" />
                            <dx:ListEditItem Value="台北天母店" Text="台北天母店" />
                            <dx:ListEditItem Value="台中忠明店" Text="台中忠明店" />
                            <dx:ListEditItem Value="台中美村店" Text="台中美村店" />
                            <dx:ListEditItem Value="台南台南店" Text="台南台南店" />
                            <dx:ListEditItem Value="高雄林森店" Text="高雄林森店" />
                            <dx:ListEditItem Value="高雄三多店" Text="高雄三多店" />
                            <dx:ListEditItem Value="高雄成功店" Text="高雄成功店" />
                            <dx:ListEditItem Value="機場1" Text="機場1" />
                            <dx:ListEditItem Value="機場2" Text="機場2" />
                            <dx:ListEditItem Value="客服" Text="客服" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--客戶姓名-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--客戶門號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                </td>
                <td class="tdval" colspan="3">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="<%$ Resources:WebResources, ListTodaysReservations %>" />
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="right">
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click" />
                </td>
                <td>&nbsp;</td>
                <td align="left">
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height: auto">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <dx:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                            KeyFieldName="項次">
                            <Columns>
                                <dx:GridViewDataColumn VisibleIndex="0">
                                    <DataItemTemplate>
                                        <dx:ASPxButton ID="btnSelect" runat="server" Text="<%$ Resources:WebResources, Select %>"
                                            CausesValidation="False" Font-Underline="False" OnClick="btnSelect_Click">
                                        </dx:ASPxButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="預約日期" Caption="<%$ Resources:WebResources, ReservationDate %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="租賃單號" Caption="<%$ Resources:WebResources, LeaseOrderNo %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="客戶姓名" Caption="<%$ Resources:WebResources, CustomerName %>"
                                    VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="客戶門號" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="性別" Caption="<%$ Resources:WebResources, Gender %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="手機地點" Caption="<%$ Resources:WebResources, MobileLocation %>"
                                    VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="預定領取日" Caption="<%$ Resources:WebResources, CollectionDueDate %>"
                                    VisibleIndex="7" />
                                <dx:GridViewDataColumn FieldName="預約歸還日" Caption="<%$ Resources:WebResources, ReturnDueDate %>"
                                    VisibleIndex="8" />
                                <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>"
                                    VisibleIndex="9" />
                                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                    VisibleIndex="10" />
                                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                    VisibleIndex="11" />
                            </Columns>                            
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        </dx:ASPxGridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>    
</asp:Content>
