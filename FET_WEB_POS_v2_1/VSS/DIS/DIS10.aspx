<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DIS10.aspx.cs" Inherits="VSS_DIS_DIS10" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    
    <div class="titlef" align="left">
        <!--ERP與類別屬性對應查詢-->
        <asp:Literal ID="lblTitle" runat="server" Text="<%$ Resources:WebResources, ObjectPropertiesMapping %>"></asp:Literal>
    </div>
    
    <div class="criteria">
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <!--商品分類-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCategory1 %>"></asp:Literal> ：
                </td>
                <td>
                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" ValueType="System.String" Width="170px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="2G" Value="2G" />
                            <dx:ListEditItem Text="3G" Value="3G" />
                            <dx:ListEditItem Text="3.5G" Value="3.5G" />
                            <dx:ListEditItem Text="Datacard" Value="Datacard" />
                            <dx:ListEditItem Text="Netbook" Value="Netbook" />
                            <dx:ListEditItem Text="Other" Value="Other" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td>&nbsp;</td>
                <td align="right">
                    <!--ERP Attribute1-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ErpAttribute1 %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="center">
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnQuery_Click"></dx:ASPxButton>
                </td>
                <td>&nbsp;</td>
                <td align="center">
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" runat="server" Width="100%" KeyFieldName="項次">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" />
                        <dx:GridViewDataColumn FieldName="ERP Attribute" Caption="<%$ Resources:WebResources, ErpAttribute1 %>" />
                        <dx:GridViewDataColumn FieldName="商品分類" Caption="<%$ Resources:WebResources, ProductCategory1 %>" />
                        <dx:GridViewDataColumn FieldName="維護人員" Caption="<%$ Resources:WebResources, MaintainedBy %>"/>                           
                        <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"/>
                    </Columns>
                    <SettingsPager PageSize="10"></SettingsPager>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
</asp:Content>
