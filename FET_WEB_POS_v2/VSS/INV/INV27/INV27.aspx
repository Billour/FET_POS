<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="INV27.aspx.cs" Inherits="VSS_INV_INV27" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type= "text/javascript">
        _gvSender = null;
        _gvEventArgs = null;
        function getPRODNAME(s, e) {
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getPRODNAME(_gvSender.GetText(), getPRODNAME_OnOK);
        }
        function getPRODNAME_OnOK(returnData) {
            if (returnData == '') {
                alert("商品料號不存在!");
                _gvSender.Focus();
                _gvSender.SetText("");
            }
            else {
                if (returnData == "fail") {
                    alert("商品料號不允許設定!");
                    _gvSender.Focus();
                    _gvSender.SetText("");
                }
                else {
                    PRODNAME.SetValue(returnData);
                }
            }
        }

        //檢查拆機門市是否存在
        function getTakeOffStore(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getTakeOffStore(grid.keys[0].toString(), s.GetText(), getTakeOffStore_OnOK);
        }
        function getTakeOffStore_OnOK(returnData) {
            if (returnData != '') {
                alert("此門市代號己輸入，請重新輸入!");
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus()
                window.event.returnValue = null;
            }
        }

        //檢查門市是否存在
        function getStore(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getStore(s.GetText(), getStore_OnOK);
        }
        function getStore_OnOK(returnData) {
            if (returnData == '') {
                alert("此門市代號不存在，請重新輸入!");
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();

                window.event.returnValue = null;
            }
        }

        //不選取DISENABLED的CHECKBOX
        function CheckAll_onclick() { 
            if (typeof(grid) != 'undefined') {       
                for (var i = 0; i < grid.pageRowCount; i++) {
                    if (grid.GetRow(i + grid.visibleStartIndex) != null && grid.GetRow(i + grid.visibleStartIndex).attributes["canSelect"].value == "true") {
                        var chk = document.getElementById("checkbox1");
                        if (chk.checked) {
                            grid.SelectRowOnPage(i + grid.visibleStartIndex, true);
                        } else {
                            grid.SelectRowOnPage(i + grid.visibleStartIndex, false);
                        }
                    }
                }
            }
        }

        function getStoreInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getStoreInfo(s.GetText(), getStoreInfo_OnOK);
        }

        function getStoreInfo_OnOK(returnData) {
            if (returnData != '') {
                var values = returnData.split(';');
                STORENAME.SetValue(values[0]);
                ZONE.SetValue(values[1]);
            }
            else {
                STORENAME.SetValue(null);
                ZONE.SetValue(null);
            }
        }

        function checkDate(s, e) {
            var x = txtSDate.GetValue();
            var y = txtEDate.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("拆封日訖不允許小於拆封日起，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }

        function checkPRODEvent(s, e, fName) {
          
            var txtS_PRODNO = document.getElementById('ctl00_MainContentPlaceHolder_txtS_PRODNO_txtControl_I');
            var txtE_PRODNO = document.getElementById('ctl00_MainContentPlaceHolder_txtE_PRODNO_txtControl_I');

            var x = txtS_PRODNO.value;
            var y = txtE_PRODNO.value;

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {
                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("商品料號訖不允許小於商品料號起，請重新輸入!!");
                    s.SetValue(null);
                    e.processOnServer = false;
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <!--總部拆封商品設定-->
        <asp:Literal ID="Literal1" runat="server" Text="總部拆封商品設定"></asp:Literal>
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
                                <dx:ASPxDateEdit ID="txtS_Date" ClientInstanceName="txtSDate" runat="server" >
                                    <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtE_Date" ClientInstanceName="txtEDate"  runat="server" >
                                    <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
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
                                <uc1:PopupControl ID="txtS_PRODNO"  runat="server"  PopupControlName="ProductsPopup"  OnClientTextChanged="function(s,e){ checkPRODEvent(s,e, 'txtS_PRODNO'); }" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtE_PRODNO" runat="server" PopupControlName="ProductsPopup" OnClientTextChanged="function(s,e){ checkPRODEvent(s,e, 'txtE_PRODNO'); } " />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"                       
                        OnClick="btnSearch_Click" >
                        </dx:ASPxButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton"  Text="<%$ Resources:WebResources, Reset %>">                       
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc:ASPxGridView ID="grid" ClientInstanceName="grid" EnableCallBacks = "false" 
                runat="server" KeyFieldName="SEAL_OFF_PROD_ID"
                Width="100%"
                OnHtmlRowPrepared="grid_HtmlRowPrepared"
                OnPageIndexChanged="grid_PageIndexChanged" 
                OnRowInserting="grid_RowInserting"
                OnRowUpdating="grid_RowUpdating" 
                OnFocusedRowChanged="grid_FocusedRowChanged" 
                OnRowValidating = "grid_RowValidating"
                OnCommandButtonInitialize="grid_CommandButtonInitialize"
                OnStartRowEditing="grid_StartRowEditing" 
                oninitnewrow="grid_InitNewRow" 
                oncelleditorinitialize="grid_CellEditorInitialize" 
                onhtmlrowcreated="grid_HtmlRowCreated">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0">
                        <HeaderTemplate>
                            <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();"  title="Select/Unselect all rows on the page" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                        <EditButton Visible="True">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataDateColumn FieldName="SEAL_OFF_DATE"   Caption="<%$ Resources:WebResources, OpenedDate %>"
                        VisibleIndex="2" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                        <EditCellStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, ExhibitionStartDate %>"
                        VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                        <EditCellStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, ExhibitionEndDate %>"
                        VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                        <EditCellStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                        VisibleIndex="5" >
                        <EditItemTemplate>
                            <uc1:PopupControl ID="pcPRODNO" runat="server" PopupControlName="ProductsPopup" AutoPostBack="false" KeyFieldValue1="extrasale" 
                                  IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                 OnClientTextChanged="function(s,e){ getPRODNAME(s,e);}" Text='<%# Bind("PRODNO") %>'  />                           
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                        ReadOnly="true" VisibleIndex="6" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                        <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                            <ReadOnlyStyle>
                                <Border BorderStyle="None"></Border>
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <EditFormSettings ColumnSpan="2" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtProdName" runat="server" ClientInstanceName="PRODNAME" Border-BorderStyle="None" ReadOnly="true" Text='<% #Bind("PRODNAME") %>'></dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SEAL_OFF_QTY" Caption="<%$ Resources:WebResources, OpenedQuantity %>"
                        VisibleIndex="7" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right">
                        <PropertiesTextEdit MaxLength="7" Style-HorizontalAlign="Left">
                            <ValidationSettings>
                                <RegularExpression ValidationExpression="^\d*"  ErrorText="金額/佔比不允許非整數或小於0，請重新輸入!"/>                            
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="DISCOUNT_TYPE" Caption="<%$ Resources:WebResources, DiscountMethod %>"
                        VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Text="金額" Value="1" />
                                <dx:ListEditItem Text="百分比" Value="2" />
                            </Items>
                        </PropertiesComboBox>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </dx:GridViewDataComboBoxColumn>
                    <%--金額/占比--%>
                    <dx:GridViewDataTextColumn FieldName="DISCOUNT_PRICE" Caption="<%$ Resources:WebResources, AmountOrPercentage %>"
                        VisibleIndex="9" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                        <PropertiesTextEdit MaxLength="7" Style-HorizontalAlign="Left">
                            <ValidationSettings>
                                <RegularExpression ValidationExpression="^\d*"  ErrorText="金額/佔比不允許非整數或小於0，請重新輸入!"/>                        
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                            <Style HorizontalAlign="Left"></Style>
                        </PropertiesTextEdit>
                         <HeaderStyle HorizontalAlign="Center" />
                         <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                        ReadOnly="true" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                        <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                            <ReadOnlyStyle>
                                <Border BorderStyle="None"></Border>
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="False" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                        ReadOnly="true" VisibleIndex="11" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                        <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                            <ReadOnlyStyle>
                                <Border BorderStyle="None"></Border>
                            </ReadOnlyStyle>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="False" />
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
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
                                    <dx:ASPxButton  ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                        OnClick="deleteButton_Click" >                                        
                                           <ClientSideEvents Click = "function(s,e){if (!confirm('系統將刪除勾選之資料，確認刪除？')){e.processOnServer=false;}}"/>
                                        </dx:ASPxButton>
                                   
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <Styles>
                    <EditFormColumnCaption Wrap="False">
                    </EditFormColumnCaption>
                </Styles>
                <SettingsPager PageSize="5">
                </SettingsPager>
                <SettingsEditing EditFormColumnCount="3" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
                <Settings ShowTitlePanel="True"></Settings>
            </cc:ASPxGridView>
            
            <div class="seperate">
                <dx:ASPxPageControl ID="tabPage" ClientInstanceName="tabPage" runat="server"  ActiveTabIndex="0"
                    EnableHierarchyRecreation="True" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <cc:ASPxGridView ID="detailGrid" ClientInstanceName="detailGrid" runat="server" KeyFieldName="SEAL_OFF_STORE_ID"
                                        Width="100%" EnableCallBacks="false" Enabled="false"
                                        OnPageIndexChanged="detailGrid_PageIndexChanged1"
                                        OnRowInserting="detailGrid_RowInserting" 
                                        OnRowUpdating="detailGrid_RowUpdating" 
                                        OnRowValidating="detailGrid_RowValidating" 
                                        OnPreRender="detailGrid_PreRender" 
                                        OnStartRowEditing="detailGrid_StartRowEditing" 
                                        OnInitNewRow="detailGrid_InitNewRow">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="if (typeof(detailGrid) != 'undefined') {detailGrid.SelectAllRowsOnPage(this.checked);}" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="txtSTORE_NO" runat="server" Text = '<%#Bind("STORE_NO") %>' PopupControlName="StoresPopup" 
                                                        SetClientValidationEvent="getStoreInfo" IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'/>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                                <EditItemTemplate>
                                                    <dx:ASPxLabel ID="STORENAME" runat="server" Text='<%#Bind("STORENAME") %>' ClientInstanceName="STORENAME" >
                                                    </dx:ASPxLabel>
                                                </EditItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ZONE_NAME" Caption="<%$ Resources:WebResources, ByDistrict %>">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                                <EditItemTemplate>
                                                    <dx:ASPxLabel ID="lbZONE_NAME" runat="server" Text='<%#Bind("ZONE_NAME") %>' ClientInstanceName="ZONE" >
                                                    </dx:ASPxLabel>
                                                </EditItemTemplate>
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
                                                            <dx:ASPxButton ID="btnDetailDelete" runat="server" 
                                                                Text="<%$ Resources:WebResources, Delete %>" onclick="btnDetailDelete_Click" >
                                                                    <ClientSideEvents Click = "function(s,e){ e.processOnServer=confirm('系統將刪除勾選之資料，確認刪除？') ;}"/>
                                                                </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxComboBox ID="districtComboBox"  runat="server" Width="100" 
                                                                EnableCallbackMode="True">
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="Button6" runat="server" 
                                                                Text="<%$ Resources:WebResources, DivideDistrict %>" onclick="Button6_Click" >                                                                
                                                                <ClientSideEvents Click = "function(s,e){ e.processOnServer=confirm('確定帶入分區?'); }"/>
                                                                </dx:ASPxButton>
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
            
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content> 