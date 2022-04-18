<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DIS08.aspx.cs" Inherits="VSS_DIS_DIS08" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <div class="titlef" align="left">
        <asp:Literal ID="lblTitle" runat="server" Text="組合促銷轉換值查詢"></asp:Literal>
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
                    <!--分類-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Type %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" 
                        Width="170px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="商品分類" Value="商品分類" />
                            <dx:ListEditItem Text="商品料號" Value="商品料號" />
                        </Items>
                    </dx:ASPxComboBox>
                    
                </td>
                <td>&nbsp;</td>
                <td align="right">
                    <!--生效日-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, EffectiveStartDate %>"></asp:Literal>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                            <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ></dx:ASPxDateEdit></td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <!--商品分類-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCategory1 %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" ValueType="System.String" 
                        Width="170px">
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
                    <!--失效日期-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, EffectiveEndDate %>"></asp:Literal>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                            <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" ></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit4" runat="server" ></dx:ASPxDateEdit></td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <!--商品料號-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>

    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="center"><dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnQuery_Click"></dx:ASPxButton></td>
                <td>&nbsp;</td>
                <td align="center"><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false"></dx:ASPxButton></td>
            </tr>
        </table>
    </div>
    <br />
    <div class="SubEditBlock">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <cc:ASPxGridView ID="gvMaster" runat="server" Width="100%" KeyFieldName="商品編號" 
                      onpageindexchanged="gvMaster_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" />
                            <dx:GridViewDataColumn FieldName="商品分類" Caption="<%$ Resources:WebResources, ProductCategory1 %>" />
                            <dx:GridViewDataColumn FieldName="商品料號(起)" Caption="<%$ Resources:WebResources, ProductCodeStart %>" />
                            <dx:GridViewDataColumn FieldName="商品名稱(起)" Caption="<%$ Resources:WebResources, ProductNameStart %>"/>
                            <dx:GridViewDataColumn FieldName="商品料號(迄)" Caption="<%$ Resources:WebResources, ProductCodeEnd %>" />
                            <dx:GridViewDataColumn FieldName="商品名稱(迄)" Caption="<%$ Resources:WebResources, ProductNameEnd %>"/>
                            <dx:GridViewDataColumn FieldName="生效日(起)" Caption="<%$ Resources:WebResources, EffectiveStartDate %>"/>
                            <dx:GridViewDataColumn FieldName="生效日(迄)" Caption="<%$ Resources:WebResources, EffectiveEndDate %>"/>
                            <dx:GridViewDataColumn FieldName="轉換值" Caption="<%$ Resources:WebResources, TransformedValue %>"/>
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
