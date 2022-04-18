<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SAL12.aspx.cs" Inherits="VSS_SAL_SAL_SAL12" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/DISItemChargesAndApply.ascx" TagName="DISItemChargesAndApply" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtSDate_S.GetText() != '' && txtSDate_E.GetText() != '') {
                if (txtSDate_S.GetValue() > txtSDate_E.GetValue()) {
                    alert("[生效起日訖值]不允許小於[生效起日起值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
            if (txtEDate_S.GetText() != '' && txtEDate_E.GetText() != '') {
                if (txtEDate_S.GetValue() > txtEDate_E.GetValue()) {
                    alert("[生效訖日起值]不允許大於[生效訖日訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
        
        function passToDetail(s, e) {
            var o = ASPxClientGridView.Cast(s);
            //alert(o.GetFocusedRowIndex());
            if (e.visibleIndex > -1) {
                o.SetFocusedRowIndex(e.visibleIndex);
                ASPxPageControl1.GetTab(1).SetVisible(true);
                ASPxPageControl1.GetTab(0).SetVisible(false);

            } else {
                alert('請選擇產品料號');
            }
        }
        
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <!--商品單價查詢作業-->
        <asp:Literal ID="Literal8" runat="server" Text="商品單價查詢"></asp:Literal>
    </div>
    
    <div class="criteria">
        <table style="width: 100%">
            <tr>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    <asp:CheckBoxList ID="chkISCONSIGNMENT_ISEXPIRED" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Value="0">寄銷商品</asp:ListItem>
                        <asp:ListItem Value="1">含過期料號</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="商品分類"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cboProductCategory" runat="server" Width="170px">
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, EffectiveStartDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="txtSDate_S" ClientInstanceName="txtSDate_S" runat="server" EditFormatString="yyyy/MM/dd">
                                </dx:ASPxDateEdit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="txtSDate_E" ClientInstanceName="txtSDate_E" runat="server" EditFormatString="yyyy/MM/dd">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Litera20" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <uc1:PopupControl ID="txtProductTypeNo" runat="server" PopupControlName="ProductType"  />
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, EffectiveEndDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="txtEDate_S" ClientInstanceName="txtEDate_S" runat="server" EditFormatString="yyyy/MM/dd">
                                </dx:ASPxDateEdit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="txtEDate_E" ClientInstanceName="txtEDate_E" runat="server" EditFormatString="yyyy/MM/dd">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtPRODNO" ClientInstanceName="txtPRODNO" runat="server" Width="100px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--廠商名稱-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtSUPPNAME" runat="server" Width="100px">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品名稱-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtPRODNAME" runat="server" Width="100px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Price %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="txtPRICE1" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="txtPRICE2" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div class="btnPosition">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                        OnClick="btnSearch_Click">
                        <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                        </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div class="SubEditBlock">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" AutoPostBack="True"
                    OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged" ActiveTabIndex="0"
                    ClientInstanceName="ASPxPageControl1" 
                    EnableClientSideAPI="True">
                    <TabPages>
                        <dx:TabPage Text="商品料號">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <div>
                                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" 
                                            runat="server" KeyFieldName="PRODNO" Width="100%"
                                            OnPageIndexChanged="gvMaster_PageIndexChanged" 
                                            onfocusedrowchanged="gvMaster_FocusedRowChanged">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <%#Container.ItemIndex + 1%>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PRODTYPENAME" Caption="<%$ Resources:WebResources, ProductCategory %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="UNIT" Caption="<%$ Resources:WebResources, Unit %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataComboBoxColumn FieldName="IMEI_FLAG" Caption="<%$ Resources:WebResources, VerifyImei %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataCheckColumn FieldName="IS_POS_DEF_PRICE" Caption="<%$ Resources:WebResources, CustomPrice %>" HeaderStyle-HorizontalAlign="Center" />
                                                <dx:GridViewDataCheckColumn FieldName="ISSTOCK" Caption="<%$ Resources:WebResources, ReduceInventory %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataCheckColumn>
                                                <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, EffectiveStartDate %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EffectiveEndDate %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PRICE" Caption="<%$ Resources:WebResources, Price %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="SUPPNAME" Caption="<%$ Resources:WebResources, SupplierName %>" HeaderStyle-HorizontalAlign="Center">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsPager PageSize="10" AlwaysShowPager="true"></SettingsPager>
                                            <SettingsBehavior AllowFocusedRow="true"  ProcessFocusedRowChangedOnServer="True" />
                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                            <ClientSideEvents RowClick="function(s,e){passToDetail(s,e);}" />                                        
                                        </cc:ASPxGridView>
                                    </div>
                                    
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="折扣查詢">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server">
                                    <div>
                                        <table width="100%" align="left">
                                            <tr>
                                                <td class="tdtxt">商品料號：</td>
                                                <td class="tdval" colspan="6">
                                                    <dx:ASPxLabel ID="lblProdNo" runat="server" Text=""></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tdtxt">商品名稱：</td>
                                                <td class="tdval" colspan="6">
                                                    <dx:ASPxLabel ID="lblProdName" runat="server" Text=""></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td class="tdval" colspan="7" align="center">
                                                    <uc2:DISItemChargesAndApply ID="DISItemChargesAndApply1" runat="server" />   
                                                 </td>
                                             </tr>
                                            <tr>
                                                <td class="tdtxt">
                                                    <!--門市編號-->
                                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, IMEIIvrcode %>"></asp:Literal>：
                                                </td>
                                                <td class="tdval" colspan="1">
                                                    <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" 
                                                     />
                                                </td>
                                                <td class="tdtxt">客戶門號：</td>
                                                <td class="tdval">
                                                    <dx:ASPxTextBox ID="txtMSISDN" runat="server" Width="100px" MaxLength="10">
                                                        <ValidationSettings>
                                                            <RegularExpression ErrorText="格式不正確" ValidationExpression="^[0-9]{10}" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="tdtxt">ARPB金額：</td>
                                                <td class="tdval">
                                                    <dx:ASPxTextBox ID="txtARPB" Text="0" runat="server" Width="100px" MaxLength="9">
                                                        <ValidationSettings>
                                                            <RegularExpression ErrorText="請輸入數字" ValidationExpression="^-?\d+$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <!-- Search Detail Button -->
                                            <tr>
                                                <td class="tdval" colspan="7" align="center">
                                                    <div class="seperate"></div>
                                                    <div class="btnPosition">
                                                        <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="btnSearchD" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                                                    OnClick="btnSearchD_Click" >
                                                                    </dx:ASPxButton>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnReset2" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                                                    OnClick="btnReset2_Click">
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <!-- Detail GridView -->
                                            <tr>
                                                <td class="tdval" colspan="7" align="center">
                                                    <div class="seperate"></div>
                                                    <div>
                                                       <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" KeyFieldName="項次" Width="100%"
                                                        OnPageIndexChanged="gvDetail_PageIndexChanged"
                                                       >
                                                            <Columns>
                                                                <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0">
                                                                    <DataItemTemplate>
                                                                        <%#Container.ItemIndex + 1%>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="DISCOUNT_CODE" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" VisibleIndex="1" />
                                                                <dx:GridViewDataColumn FieldName="DISCOUNT_NAME" Caption="<%$ Resources:WebResources, DiscountName %>" VisibleIndex="2" />
                                                                <dx:GridViewDataColumn FieldName="DISCOUNT_MONEY" Caption="<%$ Resources:WebResources, DiscountAmount %>" VisibleIndex="3" />
                                                                <dx:GridViewDataColumn FieldName="DISCOUNT_GIFT_ADD" Caption="贈品/加價購" VisibleIndex="4" />
                                                            </Columns>
                                                            <SettingsPager PageSize="5"></SettingsPager>
                                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                        </cc:ASPxGridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>      
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                 </dx:ASPxPageControl>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
