<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="INV27.aspx.cs" Inherits="VSS_INV_INV27" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="hdNo" runat="server" class="hdNo" />
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--總部拆封商品設定-->
                    <asp:Literal ID="Literal1" runat="server" Text="總部拆封商品設定"></asp:Literal>
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
                <td class="tdtxt">
                    <!--拆封日期-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OpenedDate %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  />
                            </td>
                            <%--<td>
                                <dx:ASPxTextBox ID="TextBox3" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false" SkinID="PopupButton" />
                            </td>--%>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                 <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="ProductsPopup"  />
                            </td>
                            <%--<td>
                                <dx:ASPxTextBox ID="TextBox4" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnChooseProduct1" runat="server" AutoPostBack="false" SkinID="PopupButton" />
                            </td>--%>
                        </tr>
                    </table>
                  <%--  <cc:ASPxPopupControl ID="productsPopup1" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                        PopupElementID="btnChooseProduct" TargetElementID="TextBox3" LoadingPanelID="lp">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </cc:ASPxPopupControl>
                    <cc:ASPxPopupControl ID="productsPopup2" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                        PopupElementID="btnChooseProduct1" TargetElementID="TextBox4" LoadingPanelID="lp">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </cc:ASPxPopupControl>
                    <dx:ASPxLoadingPanel ID="lp" runat="server">
                    </dx:ASPxLoadingPanel>--%>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
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
                    <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton">                        
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="商品料號"
        Width="100%" Settings-ShowTitlePanel="true" OnHtmlRowPrepared="grid_HtmlRowPrepared"
        OnPageIndexChanged="grid_PageIndexChanged" OnRowInserting="grid_RowInserting"
        OnRowUpdating="grid_RowUpdating" OnFocusedRowChanged="grid_FocusedRowChanged" OnCustomCallback="grid_CustomCallback"
        EnableCallBacks="false">
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0">
                <HeaderTemplate>
                    <input type="checkbox" onclick="grid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                <EditButton Visible="True">
                </EditButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataDateColumn FieldName="拆封日期" Caption="<%$ Resources:WebResources, OpenedDate %>"
                VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <EditCellStyle HorizontalAlign="Left" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="展示起日" Caption="<%$ Resources:WebResources, ExhibitionStartDate %>"
                VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <EditCellStyle HorizontalAlign="Left" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="展示訖日" Caption="<%$ Resources:WebResources, ExhibitionEndDate %>"
                VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <EditCellStyle HorizontalAlign="Left" />
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                VisibleIndex="5" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <EditItemTemplate>
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  Text='<%#Eval("[商品料號]") %>' />
                
                    <%--<table align="left">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="txtProductCode" runat="server" Width="68px" Text='<%#Eval("[商品料號]") %>'>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxButton ID="ChooseProductCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                    <cc:ASPxPopupControl ID="productsPopup3" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                        PopupElementID="ChooseProductCodeButton" TargetElementID="txtProductCode" LoadingPanelID="lp3">
                    </cc:ASPxPopupControl>
                    <dx:ASPxLoadingPanel ID="lp3" runat="server">
                    </dx:ASPxLoadingPanel>--%>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                ReadOnly="true" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                <EditFormSettings ColumnSpan="2" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="拆封數量" Caption="<%$ Resources:WebResources, OpenedQuantity %>"
                VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right" />
            <dx:GridViewDataComboBoxColumn FieldName="折扣方式" Caption="<%$ Resources:WebResources, DiscountMethod %>"
                VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="金額" Value="金額" />
                        <dx:ListEditItem Text="百分比" Value="百分比" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataColumn FieldName="金額/占比" Caption="<%$ Resources:WebResources, AmountOrPercentage %>"
                VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left" />
            <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                ReadOnly="true" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                ReadOnly="true" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <TitlePanel>
                <table cellpadding="0" cellspacing="0" align="left">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="AddButton_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        </td>
                    </tr>
                </table>
            </TitlePanel>
        </Templates>
        <Styles>
            <EditFormColumnCaption Wrap="False">
            </EditFormColumnCaption>
        </Styles>
        <SettingsPager PageSize="5"></SettingsPager>
        <SettingsEditing EditFormColumnCount="3" />
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
        <ClientSideEvents RowClick="function(s,e){
            //s.CreateLoadingPanel();
            //alert(s.GetRowKey(e.visibleIndex));
            //s.PerformCallback(s.GetRowKey(e.visibleIndex));
        }"  />        
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxPageControl ID="tabPage" ClientInstanceName="tabPage" runat="server" ActiveTabIndex="0"
            EnableHierarchyRecreation="True" Width="100%">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="門市編號"
                                Width="100%" Settings-ShowTitlePanel="true" OnPageIndexChanged="grid_PageIndexChanged"
                                OnRowInserting="detailGrid_RowInserting" OnRowUpdating="detailGrid_RowUpdating">
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                        <HeaderTemplate>
                                            <input type="checkbox" onclick="detailGrid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                        <EditButton Visible="True">
                                        </EditButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>">
                                        <EditItemTemplate>
                                            <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"  />
                                            
                                            <%--<table>
                                                <tr>
                                                    <td width="120px">
                                                        <div style="width: 120px;">
                                                            <dx:ASPxTextBox ID="TextBox14" runat="server" Width="100">
                                                            </dx:ASPxTextBox>
                                                        </div>
                                                    </td>
                                                    <td width="10px">
                                                        <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                            SkinID="PopupButton">
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server" EnableViewState="False"
                                                PopupElementID="Button2" TargetElementID="TextBox14">
                                                <ClientSideEvents Init="function(s, e) {
                                                    var iframe = s.GetContentIFrame();                   
                                                    iframe.popupArguments = {};
                                                    iframe.contentLoaded = false;
                                                    var controlCollection = ASPxClientControl.GetControlCollection();                
                                                    iframe.popupArguments.popupContainer = controlCollection.Get('storesPopup1');                                                                   
                                                    var targetElementId = 'TextBox14';                                                                                        
                                                    iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId) 
                                                        || document.getElementById(targetElementId);
                                                    }"></ClientSideEvents>
                                            </cc:ASPxPopupControl>--%>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="區域別" Caption="<%$ Resources:WebResources, ByDistrict %>">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table cellpadding="0" cellspacing="0" align="left">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                        AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e){ detailGrid.AddNewRow(); }" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="districtComboBox" runat="server" Width="100">
                                                        <Items>
                                                            <dx:ListEditItem Text="區域" Value="區域" Selected="true" />
                                                            <dx:ListEditItem Text="ALL" Value="ALL" />
                                                            <dx:ListEditItem Text="北一區" Value="北一區" />
                                                            <dx:ListEditItem Text="中一區" Value="中一區" />
                                                            <dx:ListEditItem Text="南一區" Value="南一區" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="Button6" runat="server" Text="<%$ Resources:WebResources, DivideDistrict %>" />
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                </Templates>
                                <Styles>
                                    <EditFormColumnCaption Wrap="False">
                                    </EditFormColumnCaption>
                                </Styles>
                                <SettingsEditing Mode="Inline" />
                                <Settings ShowTitlePanel="True"></Settings>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
    <div class="btnPosition">
        <table align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" 
                        OnClick="btnSave_Click" Visible="false" />
                </td>
                <td>
                    <dx:ASPxButton ID="btnDiscard" runat="server" Text="<%$ Resources:WebResources, Discard %>" 
                        Visible="false" />
                </td>
                <td>
                    <dx:ASPxButton ID="Button8" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                        Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <%--<cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
        PopupElementID="btnChooseProduct" TargetElementID="TextBox3">
        <ClientSideEvents Init="function(s, e) {
                    var iframe = s.GetContentIFrame();                   
                    iframe.popupArguments = {};
                    iframe.contentLoaded = false;
                    var controlCollection = ASPxClientControl.GetControlCollection();                
                    iframe.popupArguments.popupContainer = controlCollection.Get('productsPopup');                                                                   
                    var targetElementId = 'TextBox3';                                                                                        
                    iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId) 
                        || document.getElementById(targetElementId);
                    }"></ClientSideEvents>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>--%>
</asp:Content>
