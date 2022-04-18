<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DIS10.aspx.cs" Inherits="VSS_DIS_DIS10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <div class="titlef">
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
                    <dx:ASPxComboBox ID="ddlProductCategory" runat="server" Width="170px">
                    </dx:ASPxComboBox>
                </td>
                <td>&nbsp;</td>
                <td align="right">
                    <!--ERP Attribute1-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ErpAttribute1 %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtErpAttribute1" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>

    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnQuery_Click"></dx:ASPxButton>
                </td>
                <td>&nbsp;</td>
                <td>
                     <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                     </dx:ASPxButton>            
                </td>
            </tr>
        </table>
    </div>

    <div class="seperate"></div>

    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%" KeyFieldName="UUID" 
                    onpageindexchanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                            <DataItemTemplate>
                                 <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>                        
                        <dx:GridViewDataColumn FieldName="ERP_ATTRIBUTE1" Caption="<%$ Resources:WebResources, ErpAttribute1 %>" />
                        <dx:GridViewDataColumn FieldName="CATE_NAME" Caption="<%$ Resources:WebResources, ProductCategory1 %>" />
                        <dx:GridViewDataColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, MaintainedBy %>"/>                           
                        <dx:GridViewDataDateColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                                <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd HH:mm:ss"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                    </Columns>
                    <SettingsPager PageSize="10"></SettingsPager>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsBehavior AllowFocusedRow="true" />
                </cc:ASPxGridView>            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
</asp:Content>
