<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL063.aspx.cs" Inherits="VSS_RPT_RPL063"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--空白盤點單-->
        <asp:Literal ID="Literal1" runat="server" Text="空白盤點單"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    
                    <asp:Literal ID="Literal12" runat="server" Text="盤點類型"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="choice" Text="重盤" />
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="choice" Text="全盤" />
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="choice" Text="關帳盤點" />
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    
                    <asp:Literal ID="Literal3" runat="server" Text="盤點日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
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
    <table align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" /></dx:ASPxButton></td><td> <dx:ASPxButton ID="btnExport" runat="server" Text="匯出">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductNo %>" Caption="<%$ Resources:WebResources, ProductNo %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductName %>" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductCategory %>" Caption="<%$ Resources:WebResources, ProductCategory %>" />
            <dx:GridViewDataColumn FieldName="銷售倉" Caption="銷售倉" />
            <dx:GridViewDataColumn FieldName="盤點數量" Caption="盤點數量" />
            <dx:GridViewDataColumn FieldName="租賃倉" Caption="租賃倉" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
</asp:Content>
