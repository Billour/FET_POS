<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON01.aspx.cs" Inherits="VSS_CON01_CON01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="titlef">
        <!--外部廠商查詢作業(總部)-->
        <asp:Literal ID="Literal1" runat="server" Text="外部廠商查詢作業(總部)"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                     <td class="tdtxt">
                        <!--廠商類別-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList3" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="寄售廠商" />
                                <dx:ListEditItem Text="外部廠商" />
                                <dx:ListEditItem Text="全部" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" >
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="AC1" />
                                <dx:ListEditItem Text="AC2" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox6" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">&nbsp;</td>
                    <td class="tdval">&nbsp;</td>
                    <td class="tdtxt">&nbsp;</td>
                    <td class="tdval">&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table>
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server">
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="廠商編號"
                    Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                    EnableRowsCache="False">
                        <Columns>
                            <dx:GridViewDataHyperLinkColumn FieldName="廠商編號" Caption="<%$ Resources:WebResources, SupplierNo %>">
                                <PropertiesHyperLinkEdit NavigateUrlFormatString="~/VSS/CONS/CON02.aspx?No={0}">
                                </PropertiesHyperLinkEdit>
                            </dx:GridViewDataHyperLinkColumn>
                            <dx:GridViewDataTextColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>" />
                            <dx:GridViewDataTextColumn FieldName="廠商類別" Caption="<%$ Resources:WebResources, SupplierCategory %>" />
                            <dx:GridViewDataTextColumn FieldName="統一編號" Caption="<%$ Resources:WebResources, UnifiedBusinessNo %>" />
                            <dx:GridViewDataTextColumn FieldName="合作起日" Caption="<%$ Resources:WebResources, CooperationStartDate %>" />
                            <dx:GridViewDataTextColumn FieldName="合作訖日" Caption="<%$ Resources:WebResources, CooperationEndDate %>" />
                            <dx:GridViewDataTextColumn FieldName="負責人" Caption="<%$ Resources:WebResources, Owner %>" />
                            <dx:GridViewDataTextColumn FieldName="電話號碼" Caption="<%$ Resources:WebResources, Telephone %>" />
                            <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                            <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table align="left"><tr><td>
                                    <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                                </td></tr></table>
                            </TitlePanel>
                        </Templates>
                        <SettingsPager PageSize="14" />
                        <SettingsEditing Mode="Inline" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    </cc:ASPxGridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>