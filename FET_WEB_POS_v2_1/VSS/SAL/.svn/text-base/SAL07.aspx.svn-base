<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SAL07.aspx.cs" Inherits="VSS_SAL_SAL07" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            <!--促銷商品價格查詢-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionPriceEnquiry %>" />
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                </table>
                
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--專案類型-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProjectType %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="cbProjectType" runat="server" Width="120">
                                <Items>
                                    <dx:ListEditItem Value="ALL" Text="ALL" Selected="true" />
                                    <dx:ListEditItem Value="現行" Text="現行" />
                                    <dx:ListEditItem Value="過期專案" Text="過期專案" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--商品廠牌-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductBrand %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox7" runat="server" CssClass="tbSpanWidth">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--商品型號-->
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources,ProductModel %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox12" runat="server" CssClass="tbSpanWidth">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--促銷類型-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, PromotionType %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="AutoCompleteComboBox" runat="server" Width="120">
                                <Items>
                                    <dx:ListEditItem Value="ALL" Text="ALL" Selected="true" />
                                    <dx:ListEditItem Value="組合促銷" Text="組合促銷" />
                                    <dx:ListEditItem Value="啟用促銷" Text="啟用促銷" />                                                                        
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--商品料號-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox3" runat="server" CssClass="tbSpanWidth">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--促銷價-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources,PromotionPrice %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" align="left">
                                <tr>
                                    <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                    <td><dx:ASPxTextBox ID="PromotPrice_start" runat="server" Width="75"></dx:ASPxTextBox></td>
                                    <td>&nbsp;</td>
                                    <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                    <td><dx:ASPxTextBox ID="PromotPrice_end" runat="server" Width="75"></dx:ASPxTextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--促銷代號-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox2" runat="server" CssClass="tbSpanWidth">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox5" runat="server" CssClass="tbSpanWidth">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxCheckBox ID="CheckBox3" runat="server" Text="庫存量>0">
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--促銷名稱-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap" colspan="5">
                            <table cellpadding="0" cellspacing="0" align="left">
                                <tr>
                                    <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources,FrontText %>" Width="80"></dx:ASPxLabel></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources,VoiceRates %>" Width="80"></dx:ASPxLabel></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="<%$ Resources:WebResources,VoiceMonthLimit %>" Width="120"></dx:ASPxLabel></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources,BonusRates %>" Width="80"></dx:ASPxLabel></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="<%$ Resources:WebResources,BonusMonthLimit %>" Width="120"></dx:ASPxLabel></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="<%$ Resources:WebResources,PrepaymentAmount %>" Width="80"></dx:ASPxLabel></td>
                                </tr>
                                <tr>
                                    <td><dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Width="80"></dx:ASPxTextBox></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxTextBox ID="ASPxTextBox8" runat="server" Width="80"></dx:ASPxTextBox></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxTextBox ID="ASPxTextBox9" runat="server" Width="120"></dx:ASPxTextBox></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxTextBox ID="ASPxTextBox10" runat="server" Width="80"></dx:ASPxTextBox></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxTextBox ID="ASPxTextBox11" runat="server" Width="120"></dx:ASPxTextBox></td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td><dx:ASPxTextBox ID="ASPxTextBox12" runat="server" Width="80"></dx:ASPxTextBox></td>
                                </tr>
                            </table>                            
                        </td>
                    </tr>                    
                </table>
                
                <div class="seperate"></div>
                
                <div class="btnPosition">
                    <table align="center" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                                    OnClick="btnSearch_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                    AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="seperate"></div>

                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" AutoPostBack="True"
                        OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged" ActiveTabIndex="0">
                        <TabPages>
                            <dx:TabPage Text="促銷商品">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <div>
                                            <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="項次"
                                                AutoGenerateColumns="False" OnPageIndexChanged="gvMaster_PageIndexChanged" 
                                                Width="100%" Settings-ShowTitlePanel="true" EnableCallBacks="False" 
                                                onfocusedrowchanged="gvMaster_FocusedRowChanged">
                                                <SettingsBehavior AllowFocusedRow="True" 
                                                    ProcessFocusedRowChangedOnServer="True" />
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0" />
                                                    <dx:GridViewDataColumn FieldName="促銷類型" Caption="<%$ Resources:WebResources, PromotionType %>" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn FieldName="促銷代號" Caption="<%$ Resources:WebResources, PromotionCode %>" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>" VisibleIndex="3" />
                                                    <dx:GridViewDataColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>" VisibleIndex="4" />
                                                    <dx:GridViewDataColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>" VisibleIndex="5" />
                                                </Columns>
                                                <Templates>
                                                    <TitlePanel>
                                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Export %>"></dx:ASPxButton>
                                                    </TitlePanel>                            
                                                </Templates>
                                                <SettingsPager PageSize="5"></SettingsPager>
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                <Settings ShowTitlePanel="True" />
                                            </cc:ASPxGridView>
                                        </div>
                                        
                                        <div>
                                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" KeyFieldName="項次"
                                                AutoGenerateColumns="False" Width="100%" Settings-ShowTitlePanel="true">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                                        VisibleIndex="0" />
                                                    <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                                        VisibleIndex="1" />
                                                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                                        VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="商品群" Caption="<%$ Resources:WebResources, ProductGroup %>"
                                                        VisibleIndex="3" />
                                                    <dx:GridViewDataColumn FieldName="促銷價" Caption="<%$ Resources:WebResources, PromotionPrice %>"
                                                        VisibleIndex="4" />
                                                    <dx:GridViewDataColumn FieldName="庫存量" Caption="<%$ Resources:WebResources, StockQuantity %>"
                                                        VisibleIndex="5" />
                                                </Columns>
                                                <Templates>
                                                    <TitlePanel>
                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="<%$ Resources:WebResources, PromotionDetail %>"></dx:ASPxLabel>
                                                    </TitlePanel>                            
                                                </Templates>
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                            </cc:ASPxGridView>
                                        </div>
                                        
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            
                            <dx:TabPage Text="折扣查詢">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <div>
                                            <table width="100%">
                                                <tr>
                                                    <td class="tdtxt">促銷代號：</td>
                                                    <td class="tdval" colspan="4">
                                                        <dx:ASPxLabel ID="t" runat="server" Text="AEN00ENQYDN3"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">促銷名稱：</td>
                                                    <td class="tdval" colspan="4">
                                                        <dx:ASPxLabel ID="ASPxTextBox5" runat="server" Text="3G-大雙網765美國運通白金卡單門號優惠(24個月)"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">費率：</td>
                                                    <td class="tdval" colspan="4">
                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem> Voice</asp:ListItem>
                                                            <asp:ListItem>Data</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">GA：</td>
                                                    <td class="tdval" colspan="4">
                                                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem>Postpaid FET-2G</asp:ListItem>
                                                            <asp:ListItem>Postpaid FET-3G</asp:ListItem>
                                                            <asp:ListItem>Prepaid</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">Loyalty：</td>
                                                    <td class="tdval" colspan="4">
                                                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem>FET-2G</asp:ListItem>
                                                            <asp:ListItem>FET-3G</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">2轉3：</td>
                                                    <td class="tdval" colspan="4">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                <td>
                                                                   <asp:CheckBoxList ID="CheckBoxList4" runat="server">
                                                                        <asp:ListItem>FET-2G =&gt; FET-3G (Voice)</asp:ListItem>
                                                                        <asp:ListItem>KGT-2G =&gt; KGT-3G (Voice)</asp:ListItem>
                                                                        <asp:ListItem>FET Prepaid =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                                        <asp:ListItem>New Cash =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                                        <asp:ListItem>KGT Prepaid =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td valign="top">
                                                                    <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                                                                        <asp:ListItem>FET-2G =&gt; FET-3G (Data)</asp:ListItem>
                                                                        <asp:ListItem>KGT-2G =&gt; KGT-3G (Data)</asp:ListItem>
                                                                        <asp:ListItem>FET Prepaid =&gt; FET-3G Postpaid (Data)</asp:ListItem>
                                                                        <asp:ListItem>KGT Prepaid =&gt; FET-3G Postpaid (Data)</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">MNP：</td>
                                                    <td class="tdval" colspan="4">&nbsp;<asp:CheckBox ID="CheckBox2" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">門市編號：</td>
                                                    <td colspan="4">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td><dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                <td>&nbsp;</td>
                                                                <td><dx:ASPxButton ID="ASPxButton3" runat="server" Text="..."></dx:ASPxButton></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">商品料號：</td>
                                                    <td class="tdval" colspan="4">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td><dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                <td>&nbsp;</td>
                                                                <td><dx:ASPxButton ID="ASPxButton2" runat="server" Text="..."></dx:ASPxButton></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">客戶門號：</td>
                                                    <td class="tdval">
                                                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100px"></dx:ASPxTextBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td class="tdtxt">ARPB金額：</td>
                                                    <td class="tdval">
                                                        <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100px"></dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="btnPosition">
                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnSearchD" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                                            OnClick="btnSearchD_Click" />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton5" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                       
                                        <div class="seperate"></div>

                                        <div>
                                           <cc:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="gvMaster" KeyFieldName="項次" Width="100%">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <%#Container.ItemIndex + 1%>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="折扣料號" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn FieldName="折扣名稱" Caption="<%$ Resources:WebResources, DiscountName %>" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="折扣金額" Caption="<%$ Resources:WebResources, DiscountAmount %>" VisibleIndex="3" />
                                                    <dx:GridViewDataColumn FieldName="贈品/加價購" Caption="贈品/加價購" VisibleIndex="4" />
                                                </Columns>
                                                <SettingsPager PageSize="5"></SettingsPager>
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                            </cc:ASPxGridView>
                                        </div>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                     </dx:ASPxPageControl>
                            
                <div class="seperate"></div>

                
            </ContentTemplate>           
        </asp:UpdatePanel>
    </div>
</asp:Content>
