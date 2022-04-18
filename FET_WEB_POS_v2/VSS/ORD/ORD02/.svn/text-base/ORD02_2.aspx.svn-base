<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD02_2.aspx.cs" Inherits="VSS_ORD_ORD02_ORD02_2" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--預訂貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PrePurchaseOrder %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) { document.location='../ORD02/ORD02.aspx';return false; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--訂單編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, BookOrderNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="tdtxt">
                    <!--訂單日期-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                </td>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblUpdDateTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--備註-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                </td>
                <td colspan="3" class="tdval" rowspan="2">
                    <%--   <asp:TextBox ID="txtMemo" runat="server" Width="98%" Rows="3" TextMode="MultiLine" Border-BorderStyle="None"></asp:TextBox>--%>
                    <dx:ASPxTextBox ID="txtMemo" runat="server" Width="98%" Border-BorderStyle="None"
                        ReadOnly="true">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--更新人員-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblUpdateUser" runat="server" Text=""></asp:Label>
                    <asp:Label ID="txtSTORE_NO" runat="server" Text="" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <%--    <tr>
                <td colspan="2">
                    &nbsp;
                </td>
              
            </tr>--%>
        </table>
    </div>
    <div class="seperate">
        <%--        <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="ASPxCallback1"
            OnCallback="ASPxCallback1_Callback">
        </dx:ASPxCallback>--%>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <div id="divContent" runat="server" class="SubEditBlock">
                <cc:ASPxGridView ID="gvMasterDV" runat="server" KeyFieldName="PRE_ORDER_SEQNO" ClientInstanceName="gvMasterDV"
                    AutoGenerateColumns="False" Width="98%" EnableCallBacks="false" OnHtmlRowCreated="gvMasterDV_HtmlRowCreated"
                    OnPageIndexChanged="grid_PageIndexChanged" OnRowCommand="gvMasterDV_RowCommand"
                   >
                    <Columns>
                        <dx:GridViewDataButtonEditColumn VisibleIndex="2">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btnOnetoone" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>"
                                    CausesValidation="false">
                                </dx:ASPxButton>
                                <asp:HiddenField ID="hidOneToOneSID" runat="server" Value='<%# Bind("OneToOneSID") %>' />
                            </DataItemTemplate>
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataTextColumn FieldName="PRE_ORDER_SEQNO" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="3">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ProductNo" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                            VisibleIndex="4">
                            <PropertiesTextEdit MaxLength="20">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ProductName" runat="server" ReadOnly="True"
                            Width="200" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="5">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PreOrderQty" HeaderStyle-HorizontalAlign="Right"
                            VisibleIndex="6" Caption="<%$ Resources:WebResources, PreOrderQuantity %>" 
                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" 
                            PropertiesTextEdit-ValidationSettings-ErrorText="必填欄位" >
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="REAL_ORDER_QTY" runat="server" HeaderStyle-HorizontalAlign="Right"
                            Caption="<%$ Resources:WebResources, IntoSingleRealNumber %>" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FAIL_REASON" runat="server" HeaderStyle-HorizontalAlign="Right"
                            Caption="<%$ Resources:WebResources, Failure %>" VisibleIndex="8">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetailDV" Width="80%" runat="server" ClientInstanceName="gvDetailDV"
                                EnableRowsCache="true" AutoGenerateColumns="False"  >
                                <Columns>
                                    <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="ORDQTY" HeaderStyle-HorizontalAlign="Right"
                                        VisibleIndex="2" Caption="<%$ Resources:WebResources, PreOrderQuantity %>" 
                                        PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-ErrorText="必填欄位">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="REAL_ORDER_QTY" runat="server" HeaderStyle-HorizontalAlign="Right"
                                        Caption="<%$ Resources:WebResources, IntoSingleRealNumber %>" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="FAIL_REASON" runat="server" HeaderStyle-HorizontalAlign="Right"
                                        Caption="<%$ Resources:WebResources, Failure %>" VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <div align="left">
                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CollocationProduct %>"></asp:Literal>
                                        </div>
                                    </TitlePanel>
                                </Templates>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                <SettingsDetail IsDetailGrid="true" />
                                <Settings ShowTitlePanel="true" />
                            </cc:ASPxGridView>
                            <asp:Literal ID="lblOneToOneSID" runat="server" Text='<%# Eval("OneToOneSID") %>'
                                Visible="false"></asp:Literal>
                        </DetailRow>
                    </Templates>
                    <Settings ShowTitlePanel="True" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                    <SettingsDetail ShowDetailRow="true" ShowDetailButtons="False" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                </cc:ASPxGridView>
            </div>
            <div class="seperate">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
