<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON01a.aspx.cs" Inherits="VSS_CONS_CON01a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="titlef" align="left">
        <!--外部廠商查詢作業面(門市)-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OutsideFirmSearchSC %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
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
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox6" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                        <td class="tdtxt">
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
       
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server"
        Width="100%" AutoGenerateColumns="False"
        EnableRowsCache="False">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="廠商編號" Caption="<%$ Resources:WebResources, SupplierNo %>" CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataTextColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>" CellStyle-HorizontalAlign="Right" />
                <dx:GridViewDataTextColumn FieldName="廠商類別" Caption="<%$ Resources:WebResources, SupplierCategory %>" CellStyle-HorizontalAlign="Right" />
                <dx:GridViewDataTextColumn FieldName="統一編號" Caption="<%$ Resources:WebResources, UnifiedBusinessNo %>" CellStyle-HorizontalAlign="Left"/>
                <dx:GridViewDataTextColumn FieldName="合作起日" Caption="<%$ Resources:WebResources, CooperationStartDate %>" CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataTextColumn FieldName="合作訖日" Caption="<%$ Resources:WebResources, CooperationEndDate %>"  CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataTextColumn FieldName="聯絡人員" Caption="<%$ Resources:WebResources, Contact1 %>" CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataTextColumn FieldName="聯絡電話" Caption="<%$ Resources:WebResources, ContactTelephone %>" CellStyle-HorizontalAlign="Right" />
                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" CellStyle-HorizontalAlign="Left" />
                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"  CellStyle-HorizontalAlign="Right" />
            </Columns>
            <SettingsPager PageSize="10" />
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
            
    </div>
</asp:Content>