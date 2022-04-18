<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LEA06.aspx.cs" Inherits="VSS_LEA06_LEA06" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--租賃收費明細表查詢-->
        <asp:Literal ID="Literal11" runat="server" Text="租賃收費明細表查詢"></asp:Literal></div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--庫存地點-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
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
                    <!--出國時間-->
                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, DurationAbroad %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="1">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                起
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;訖
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查詢" OnClick="ASPxButton1_Click">
                    </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="清空">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="SubEditBlock">
        <div class="GridScrollBar" style="height: auto">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" Settings-ShowTitlePanel="true">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="No" Caption="No" VisibleIndex="0" />
                            <dx:GridViewDataColumn FieldName="手機地點" Caption="<%$ Resources:WebResources, MobileLocation %>"
                                VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="客戶姓名" Caption="<%$ Resources:WebResources, CustomerName %>"
                                VisibleIndex="2" />
                            <dx:GridViewDataColumn FieldName="客戶門號" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                VisibleIndex="3" />
                            <dx:GridViewDataColumn FieldName="預定領取日" Caption="實際領取日" VisibleIndex="4" />
                            <dx:GridViewDataColumn FieldName="預約歸還日" Caption="實際歸還日" VisibleIndex="5" />
                            <dx:GridViewDataColumn FieldName="租金" Caption="<%$ Resources:WebResources, Rent %>"
                                VisibleIndex="6" />
                            <dx:GridViewDataColumn FieldName="賠償金" Caption="<%$ Resources:WebResources, Compensation %>"
                                VisibleIndex="7" />
                            <dx:GridViewDataColumn FieldName="賠償原因" Caption="<%$ Resources:WebResources, ReasonForCompensation %>"
                                VisibleIndex="8" />
                            <dx:GridViewDataColumn FieldName="折扣金額" Caption="<%$ Resources:WebResources, DiscountAmount %>"
                                VisibleIndex="9" />
                            <dx:GridViewDataColumn FieldName="總金額" Caption="<%$ Resources:WebResources, TotalAmount %>"
                                VisibleIndex="10" />
                            <dx:GridViewDataColumn FieldName="備註" Caption="<%$ Resources:WebResources, Remark %>"
                                VisibleIndex="11" />
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, Export %>">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    </dx:ASPxGridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ASPxButton1" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
