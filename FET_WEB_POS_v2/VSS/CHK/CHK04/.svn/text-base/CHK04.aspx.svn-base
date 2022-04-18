<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master" CodeFile="CHK04.aspx.cs" Inherits="VSS_CHK_CHK04" %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

        <div class="titlef">
            <!--找零金-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ChangeAmount %>"></asp:Literal>
        </div>
        
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--交易日期-->
                        <asp:Literal ID="lblTraneDate" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="txtTraneDateS" runat="server" ClientInstanceName="txtSDate" EditFormat="Date" EditFormatString="yyyy/MM/dd">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt">
                        <!--機台編號-->
                        <asp:Literal ID="lblMachineID" runat="server" Text="<%$ Resources:WebResources, CashRegisterNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlMachineID" runat="server">
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock">
                    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="ID" Width="100%" 
                        OnPageIndexChanged="grid_PageIndexChanged"
                        OnRowInserting="grid_RowInserting" 
                        oninitnewrow="grid_InitNewRow">
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                <EditButton Visible="false"></EditButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="TRADE_DATE" Caption="<%$ Resources:WebResources, TradeDate %>" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="HOST_NO" Caption="<%$ Resources:WebResources, CashRegisterNo %>" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BATCH_NO" Caption="<%$ Resources:WebResources, BatchNo %>" ReadOnly="true">
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="AMOUNT" Caption="<%$ Resources:WebResources, ChangeAmount %>">
                                <EditItemTemplate>
                                    <dx:ASPxTextBox ID="txtAmount" runat="server" HorizontalAlign="Right" Width="100%"
                                        Text = '<%# Bind("AMOUNT") %>'
                                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            <RegularExpression ValidationExpression="\d*" ErrorText="請輸入正整數" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </EditItemTemplate>
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" >
                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                               <EditItemTemplate>
                                    <dx:ASPxLabel ID="lblModiDTM" runat="server" Text='<%# Bind("MODI_DTM") %>'></dx:ASPxLabel>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                OnClick="btnAddNew_Click" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <SettingsPager PageSize="10"></SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Settings ShowTitlePanel="true" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    </cc:ASPxGridView>
                </div>
            </ContentTemplate>
            
        </asp:UpdatePanel>
</asp:Content>
