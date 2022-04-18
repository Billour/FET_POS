<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="DIS05.aspx.cs" Inherits="VSS_DIS_DIS05" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">  
    <div class="func">
        <div class="titlef">
            <!--促銷商品設定查詢-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionMixSearch %>"></asp:Literal>
        </div>
        
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--促銷代碼-->
                        <asp:Literal ID="lblPromotionCode" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtPromotionCode" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td class="tdtxt">
                        <!--促銷名稱-->
                        <asp:Literal ID="lblPromotionName" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtPromotionName" runat="server"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--開始日期-->
                        <asp:Literal ID="lblStartDate" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
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
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品型態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductType %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlProductType" runat="server" ValueType="System.String">
                            <Items>
                                <dx:ListEditItem Text="All" Value="All" Selected="true" />
                                <dx:ListEditItem Text="指定商品" Value="指定商品" />
                                <dx:ListEditItem Text="一般商品" Value="一般商品" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td>&nbsp;</td>
                    <td class="tdtxt">
                        <!--變價規則-->
                        <asp:Literal ID="Literal10" runat="server" Text="變價規則"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String">
                            <Items>
                                <dx:ListEditItem Text="All" Value="All" Selected="true" />
                                <dx:ListEditItem Text="變價" Value="變價" />
                                <dx:ListEditItem Text="不變價" Value="不變價" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="lblProductCode" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                       <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  /></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="ProductsPopup"  /></td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="lblProductName" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtProductName" runat="server"></dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                    OnClick="btnSearch_Click" /></td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                    </dx:ASPxButton>
                </td>
                </tr>
            </table>
        </div>        
        
        <div class="seperate"></div>
        
        <div class="SubEditBlock">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="GridScrollBar">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PromotionCode" 
                            Width="100%">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                                    <DataItemTemplate>
                                         <%#Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="促銷代號" Caption="<%$ Resources:WebResources, PromotionCode %>" />
                                <dx:GridViewDataTextColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>" />
                                <dx:GridViewDataColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>" />
                                <dx:GridViewDataColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>" />
                                <dx:GridViewDataTextColumn FieldName="預計上架日" Caption="預計上架日" />
                                <dx:GridViewDataTextColumn FieldName="商品型態" Caption="<%$ Resources:WebResources, ProductType %>" />
                                <dx:GridViewDataTextColumn FieldName="PromotionSubsidy" Caption="Promotion Subsidy" />
                                <dx:GridViewDataTextColumn FieldName="變價規則" Caption="變價規則" />
                                <dx:GridViewDataTextColumn FieldName="備註" Caption="<%$ Resources:WebResources, Remark %>" />
                                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                            </Columns>
                            <SettingsPager PageSize="5"></SettingsPager>
                            <Settings ShowTitlePanel="false" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView> 
                    </div>
                    
                    <div id="Div_Dt">
                        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" AutoPostBack="true" 
                            Width="100%" onactivetabchanged="ASPxPageControl1_ActiveTabChanged">
                            <TabPages>
                            
                                <dx:TabPage Text="<%$ Resources:WebResources, ProductCategory %>">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail1" ClientInstanceName="gvDetail" runat="server" 
                                                    Width="100%" KeyFieldName="項次">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" Visible="false">
                                                             <DataItemTemplate>
                                                                <%#Container.ItemIndex + 1%>
                                                             </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn>
                                                            <HeaderTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                                                                    <tr>
                                                                        <td colspan="3" style="border-bottom:1px #8E8E8E  solid">Handset</td>
                                                                    </tr>
                                                                     <tr>
                                                                        <td width="30%">2G</td>
                                                                        <td width="30%">3G</td>
                                                                        <td width="40%">3.5G</td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            
                                                            <DataItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                                                                    <tr>
                                                                        <td align="center" width="30%">
                                                                            <dx:ASPxLabel ID="lbl2G" runat="server" Text='<%#BIND("2G")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" width="30%">
                                                                            <dx:ASPxLabel ID="lbl3G" runat="server" Text='<%#BIND("3G")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" width="40%">
                                                                            <dx:ASPxLabel ID="lbl35G" runat="server" Text='<%#BIND("35G")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        
                                                        <dx:GridViewDataTextColumn>
                                                            <HeaderTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                                                                    <tr>
                                                                        <td colspan="2" style="border-bottom:1px #8E8E8E  solid">Broadband</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="50%">Netbook</td>
                                                                        <td width="50%">Datacard</td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            
                                                            <DataItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                                                                    <tr>
                                                                        <td align="center" width="50%">
                                                                            <dx:ASPxLabel ID="lblNetbook" runat="server" Text='<%#BIND("Netbook")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                        <td align="center" width="50%">
                                                                            <dx:ASPxLabel ID="lblDatacard" runat="server" Text='<%#BIND("Datacard")  %>'></dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Other" CellStyle-HorizontalAlign="Center">
                                                            <DataItemTemplate>
                                                                <dx:ASPxLabel ID="lblOther" runat="server" Text='<%#BIND("Other")  %>'></dx:ASPxLabel>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="補貼金額" Caption="補貼金額" />
                                                    </Columns>
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                    <Settings ShowTitlePanel="false" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                
                                <dx:TabPage Text="促銷商品">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail2" ClientInstanceName="gvDetail2" runat="server" 
                                                    Width="100%" KeyFieldName="ProductCode">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, Items %>">
                                                             <DataItemTemplate>
                                                                <%#Container.ItemIndex + 1%>
                                                             </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                                        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
                                                        <dx:GridViewDataTextColumn FieldName="商品群組" Caption="商品群組" />
                                                        <dx:GridViewDataTextColumn FieldName="促銷價" Caption="<%$ Resources:WebResources, PromotionPrice %>" />
                                                    </Columns>
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                    <Settings ShowTitlePanel="false" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>

                                <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail3" ClientInstanceName="gvDetail" runat="server" 
                                                    Width="100%" KeyFieldName="門市編號">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
                                                        <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                                                    </Columns>
                                                    <SettingsPager PageSize="5"></SettingsPager>
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                    <Settings ShowTitlePanel="false" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>

                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>

</asp:Content>
