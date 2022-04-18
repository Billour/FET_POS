<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ORD11.aspx.cs" Inherits="VSS_ORD_ORD11" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
<script type="text/javascript">
    var _prodName = '';
    function getProductInfo(s, e) {
        _prodName = s.name;
        _gvEventArgs = e;
        _gvSender = s;
        if (s.GetText() != '') {
            PageMethods.getProductInfo(_gvSender.GetText(), getProductInfo_OnOK);
        }
    }
    
    function getProductInfo_OnOK(returnData) {
        if (returnData != '') {
            if (_prodName != '' && _prodName.indexOf('txtPRODNO') > -1) {
                txtPRODNAME.SetValue(returnData);
            } else if (_prodName != '' && _prodName.indexOf('txtPROD_NO') > -1) {
            lblPRODNAME.SetValue(returnData);
            } 
            _gvEventArgs.processOnServer = false;
            _gvSender.Focus();
        }
        else {
            if (_prodName != '' && _prodName.indexOf('txtPRODNO') > -1) {
                txtPRODNAME.SetValue(null);
            } else if (_prodName != '' && _prodName.indexOf('txtPROD_NO') > -1) {
                lblPRODNAME.SetValue(null);
            } 
        }
    }
    
    function EnableDate(s, e) {
            var fName = "edit0_3_cbSALE_REGION2";            
            sDate = s.name.replace(fName, "DXEditor4");
            eDate = s.name.replace(fName, "DXEditor5");
            if (s.GetValue() == "2") {
                document.getElementById(sDate).disabled = false;
                document.getElementById(eDate).disabled = false; 
            } else {
                document.getElementById(sDate).disabled = true;
                document.getElementById(eDate).disabled = true; 
            }
            
            
        }
        
    //檢查移出數量
        function CheckSafetyQty2(s, e) {
            var fName = "5_txtSAFETY_VALUE2";

            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty == null || Qty == "") {
                e.isValid = false;
                e.errorText = '安全係數不允許空白，請重新輸入!!';
                return false;
            }
            else {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串不符合數字格式，請重新輸入!!';
                    return false;
                }
                else if (iQty <= 0) {
                    e.isValid = false;
                    e.errorText = '安全係數不允許小於等於0，請重新輸入!!';
                    return false;
                }
            }
        }
        function CheckSafetyQty(s, e) {
            var fName = "7_txtSAFETY_VALUE";

            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty == null || Qty == "") {
                e.isValid = false;
                e.errorText = '安全係數不允許空白，請重新輸入!!';
                return false;
            }
            else {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串不符合數字格式，請重新輸入!!';
                    return false;
                }
                else if (iQty <= 0) {
                    e.isValid = false;
                    e.errorText = '安全係數不允許小於等於0，請重新輸入!!';
                    return false;
                }
            }
        }

</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--商品建議訂購量設定-->
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductSuggestOrderAmountSetting %>"></asp:Literal>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td>
                            <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup" SetClientValidationEvent="getProductInfo" />
                        </td>
                        <td class="tdtxt">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtPRODNAME"  ClientInstanceName="txtPRODNAME" runat="server" Text=""></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                OnClick="btnSearch_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="Div2" runat="server" class="SubEditBlock">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                <ContentTemplate>
                <cc:ASPxGridView ID="gvHead" ClientInstanceName="gvHead" runat="server" KeyFieldName="SID"
                         Width="100%" EnableCallBacks = "false"
                        OnRowInserting="gvHead_RowInserting" 
                        OnRowUpdating="gvHead_RowUpdating"
                        oninitnewrow="gvHead_InitNewRow" 
                        onrowvalidating="gvHead_RowValidating"  
                        onhtmlrowcreated="gvHead_HtmlRowCreated" 
                        oncancelrowediting="gvHead_CancelRowEditing" 
                        onstartrowediting="gvHead_StartRowEditing">
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Button" Caption=" ">
                                <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                </EditButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="PRODNAME" ReadOnly="true" Caption=" ">
                                <EditItemTemplate>
                                    <dx:ASPxLabel ID="txtPRODNAME" Text='通則' runat="server">
                                    </dx:ASPxLabel>
                                </EditItemTemplate>                        
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn FieldName="SALE_REGION" Caption="<%$ Resources:WebResources, SalesBase %>">
                                <EditItemTemplate>
                                    <dx:ASPxComboBox ID="cbSALE_REGION2" runat="server" Value='<%# BIND("SALE_REGION") %>' OnSelectedIndexChanged="cbSALE_REGION2_SelectedIndexChanged" AutoPostBack="true">
                                        <ClientSideEvents />
                                        <Items>
                                            <dx:ListEditItem Text="14天" Value="0" />
                                            <dx:ListEditItem Text="30天" Value="1" />
                                            <dx:ListEditItem Text="指定期間" Value="2" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </EditItemTemplate>
                                <DataItemTemplate>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%#Bind("SALE_REGION_NAME") %>'>
                                    </dx:ASPxLabel>
                                </DataItemTemplate>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartDate %>">
                                <EditItemTemplate>
                                    <dx:ASPxDateEdit ID="S_DATE" runat="server" Text='<%# Bind("S_DATE") %>'>
                                    </dx:ASPxDateEdit>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>">
                                <EditItemTemplate>
                                    <dx:ASPxDateEdit ID="E_DATE" runat="server" Text='<%# Bind("E_DATE") %>'>
                                    </dx:ASPxDateEdit>
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>                             
                            <dx:GridViewDataColumn FieldName="SAFETY_VALUE" Caption="<%$ Resources:WebResources, SafeOrderCoefficient %>">
                                <EditItemTemplate>
                                   <dx:ASPxTextBox ID="txtSAFETY_VALUE2"  runat="server" Text = '<%#Bind("SAFETY_VALUE") %>' Width="170px"
                                   ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="9">
                                       <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="安全係數不允許空白，請重新輸入!!" />
                                       </ValidationSettings>
                                       <ClientSideEvents Validation="function(s,e){ CheckSafetyQty2(s, e); }" />
                                   </dx:ASPxTextBox>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="lblError" runat="server" ForeColor="Red" ClientInstanceName="lblError"></dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>                    
                        <SettingsEditing Mode="Inline" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <Settings ShowTitlePanel="False" />
                        <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="True" />
                    </cc:ASPxGridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
            <div class="seperate"></div>
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" ActiveTabIndex="0">
	            <TabPages>
		            <dx:TabPage Text="例外">
			            <ContentCollection>
				            <dx:ContentControl>
					            <div>
            						<asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                                    <ContentTemplate>
                                        <div id="Div1" runat="server" class="SubEditBlock">
                                            <cc:ASPxGridView ID="gvMaster" KeyFieldName="SID;PRODNO" ClientInstanceName="gvMaster"
                                                runat="server" Width="100%" EnableCallBacks="False" 
                                                OnPageIndexChanged="gvMaster_PageIndexChanged"
                                                OnRowInserting="gvMaster_RowInserting" 
                                                OnRowUpdating="gvMaster_RowUpdating"
                                                 oninitnewrow="gvMaster_InitNewRow" 
                                                onrowvalidating="gvMaster_RowValidating"  
                                                onhtmlrowprepared="gvMaster_HtmlRowPrepared"
                                                onhtmlrowcreated="gvMaster_HtmlRowCreated" 
                                                oncancelrowediting="gvMaster_CancelRowEditing" 
                                                onstartrowediting="gvMaster_StartRowEditing">
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True">
                                                        <HeaderTemplate>
                                                            <input type="checkbox" id="checkbox1" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Button" Caption=" ">
                                                        <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                                        </EditButton>
                                                        <UpdateButton Text="<%$ Resources:WebResources, Save %>">
                                                        </UpdateButton>
                                                        <CancelButton Text="<%$ Resources:WebResources, Cancel %>">
                                                        </CancelButton>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                                                        <EditItemTemplate>
                                                            <uc1:PopupControl ID="txtPROD_NO" runat="server" PopupControlName="ProductsPopup"
                                                                SetClientValidationEvent="getProductInfo" IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                                                Text='<%# BIND("PRODNO") %>' />
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataTextColumn ReadOnly="true"  FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                                                        <EditItemTemplate>
                                                            <dx:ASPxLabel ID="lblPRODNAME" ClientInstanceName="lblPRODNAME" runat="server" Text='<%# Bind("PRODNAME") %>'></dx:ASPxLabel>
                                                        </EditItemTemplate>
                                                        <PropertiesTextEdit>
                                                            <ReadOnlyStyle>
                                                                <Border BorderStyle="None" />
                                                            </ReadOnlyStyle>
                                                        </PropertiesTextEdit>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="SALE_REGION" Caption="<%$ Resources:WebResources, SalesBase %>">
                                                        <EditItemTemplate>
                                                            <dx:ASPxComboBox ID="cbSALE_REGION" runat="server" Value='<%# BIND("SALE_REGION") %>' OnSelectedIndexChanged="cbSALE_REGION_SelectedIndexChanged" AutoPostBack="true">
                                                                <ClientSideEvents />
                                                                <Items>
                                                                    <dx:ListEditItem Text="14天" Value="0" Selected />
                                                                    <dx:ListEditItem Text="30天" Value="1" />
                                                                    <dx:ListEditItem Text="指定期間" Value="2" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </EditItemTemplate>
                                                        <DataItemTemplate>
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Bind("SALE_REGION_NAME") %>'>
                                                            </dx:ASPxLabel>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartDate %>">
                                                        <EditItemTemplate>
                                                            <dx:ASPxDateEdit ID="S_DATE" runat="server" Text='<%# Bind("S_DATE") %>'>
                                                            </dx:ASPxDateEdit>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>">
                                                        <EditItemTemplate>
                                                            <dx:ASPxDateEdit ID="E_DATE" runat="server" Text='<%# Bind("E_DATE") %>'>
                                                            </dx:ASPxDateEdit>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataTextColumn>                              
                                                    <dx:GridViewDataColumn FieldName="SAFETY_VALUE" Caption="<%$ Resources:WebResources, SafeOrderCoefficient %>">
                                                        <EditItemTemplate>
                                                           <dx:ASPxTextBox ID="txtSAFETY_VALUE"  runat="server" Text = '<%#Bind("SAFETY_VALUE") %>' Width="170px"
                                                           ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="9">
                                                               <ValidationSettings>
                                                                    <RequiredField IsRequired="true" ErrorText="安全係數不允許空白，請重新輸入!!" />
                                                               </ValidationSettings>
                                                               <ClientSideEvents Validation="function(s,e){ CheckSafetyQty(s, e); }" />
                                                           </dx:ASPxTextBox>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                </Columns>                                               
                                                <Templates>
                                                    <TitlePanel>
                                                        <table align="left">
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="<%$ Resources:WebResources, Add %>" ClientEnabled="true">
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxButton ID="btnDelete" runat="server" SkinID="DeleteBtn"
                                                                        Text="<%$ Resources:WebResources, Delete %>" onclick="btnDelete_Click">
                                                                        <ClientSideEvents Click = "function(s,e){if (!confirm('系統將刪除勾選之資料，確認刪除？')){e.processOnServer=false;}}"/>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>
                                                                    <dx:ASPxLabel ID="lblErrorM" runat="server" ForeColor="Red" ></dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </TitlePanel>
                                                </Templates>
                                                <SettingsPager PageSize="5"></SettingsPager>
                                                <SettingsEditing Mode="Inline" />
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                <Settings ShowTitlePanel="True" />
                                                <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="True" />
                                            </cc:ASPxGridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
					            </div>
				            </dx:ContentControl>
			            </ContentCollection>
		            </dx:TabPage>
	            </TabPages>
            </dx:ASPxPageControl>
        </div>
    </div>
</asp:Content>
