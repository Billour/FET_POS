<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV06.aspx.cs" Inherits="VSS_INV_INV06" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title><</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
 
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--退倉作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousing %>"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox1" runat="server" Width="80px">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉日-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, WarehousedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width:120px;">
                                        <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                            <ValidationSettings CausesValidation="false">                                                
                                                <RequiredField IsRequired="true"/>
                                                                                                  
                                            </ValidationSettings>                                                
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width:120px;">
                                        <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                            <ValidationSettings CausesValidation="false">                                                
                                                <RequiredField IsRequired="true"/>                                                       
                                            </ValidationSettings>                                                
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品編號-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox4" runat="server" Width="80px">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉狀態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                            <Items>
                                <dx:ListEditItem Text="ALL" Selected="true" />
                                <dx:ListEditItem Text="未完成" Value="00" />
                                <dx:ListEditItem Text="已完成" Value="01" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>       
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="退倉單號"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
            OnPageIndexChanged="gvMaster_PageIndexChanged">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="退倉單號" runat="server" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>">
                    <DataItemTemplate>
                        <asp:LinkButton ID="Label1" runat="server" Text='<%# Bind("退倉單號") %>' CommandName="Select"
                            OnCommand="CommandButton_Click" CommandArgument='<%# Eval("退倉單號") %>'>
                        </asp:LinkButton>
                    </DataItemTemplate>
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="退倉開始日" Caption="<%$ Resources:WebResources, ReturnWarehousingStartDate %>" />
                <dx:GridViewDataColumn FieldName="退倉結束日" Caption="<%$ Resources:WebResources, ReturnWarehousingEndDate %>" />
                <dx:GridViewDataColumn FieldName="退倉日" Caption="<%$ Resources:WebResources, WarehousedDate %>" />
                <dx:GridViewDataColumn FieldName="退倉狀態" Caption="<%$ Resources:WebResources, ReturnWarehousingStatus %>" />
                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="5">
            </SettingsPager>
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" KeyFieldName="項次"
            Width="100%" AutoGenerateColumns="true" EnableRowsCache="true" OnHtmlRowCreated="gvDetail_HtmlRowCreated"
            OnPageIndexChanged="gvDetail_PageIndexChanged" Visible="false">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                    VisibleIndex="0">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductCode %>"
                    VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>"
                    VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center"
                    FieldName="IMEI控管" Caption="<%$ Resources:WebResources, ImeiControl %>" VisibleIndex="3">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="帳上庫存量" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, AccountOnTheStock %>"
                    VisibleIndex="4">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="未拆封數量" runat="server" ReadOnly="false" Caption="<%$ Resources:WebResources, SealedQuantity %>"
                    VisibleIndex="5">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="已拆封數量" runat="server" ReadOnly="false" Caption="<%$ Resources:WebResources, OpenedQty %>"
                    VisibleIndex="6">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="退倉數量" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ReturnQuantity %>"
                    VisibleIndex="7">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IMEI" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, imei %>"
                    VisibleIndex="8">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <DataItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbl1" runat="server" Text='<%# Bind("[IMEI]") %>' Width="10px"
                                        Enabled="false" DisabledStyle-Font-Underline="true">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, choose %>"
                                        Visible="true" ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_inputIMEIData.aspx',500,400);return false;}"
                                        AutoPostBack="false">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="差異量" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, OfDifference %>"
                    VisibleIndex="9">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ERP驗退日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ErpRejectedDate %>"
                    VisibleIndex="10">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ERP驗退單號" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ErpRejectionNoteNo %>"
                    VisibleIndex="11">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="驗退數量" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, RejectedQuantity %>"
                    VisibleIndex="12">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="5">
            </SettingsPager>
        </cc:ASPxGridView>
    </div>
</asp:Content>
