<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="OPT15.aspx.cs" Inherits="VSS_OPT_OPT15" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function getStoreInfo(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getStoreInfo(s.GetText(), onSuccess);
        }

        function getProductInfo(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getProductInfo(s.GetText(), onSuccess);
        }

        function getPRODINFOExtraSale(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;

            if (s.GetText() != '')
                PageMethods.getPRODINFOExtraSale(s.GetText(), onSuccess);
        }

        function onSuccess(returnData, userContext, methodName)
        {
            if (methodName == "getStoreInfo")
            {
                if (returnData != '')
                {
                    var values = returnData.split(';');
                    STORENAME.SetValue(values[0]);
                    ZONE.SetValue(values[1]);
                }
                else
                {
                   alert("門市編號不存在!");
                    STORENAME.SetValue(null);
                    ZONE.SetValue(null);
                    _gvSender.SetValue(null);
                    _gvSender.Focus();
                 
               }
            }
            else if (methodName == "getProductInfo")
            {
                if (returnData != '')
                {
                    txtDiscountName.SetValue(returnData);
                }
                else
                {
                    alert("折扣料號不存在!");
                    _gvSender.SetValue(null);
                    _gvSender.Focus();
                    txtDiscountName.SetValue(null);
                }
            }
            else if (methodName == "getPRODINFOExtraSale")
            {
                if (returnData == '')
                {
                    alert("商品料號不存在!");

                    _gvSender.Focus();
                    _gvSender.SetValue(null);

                }
                else
                {
                    if (returnData == "fail")
                    {
                        alert("商品料號不允許設定!");
                        _gvSender.Focus();
                        _gvSender.SetValue(null);

                    }
                }
            }
        }

        function CheckRequiredField(s, e)
        {
            var Value = s.GetValue();
            if (Value == null || Value == "")
            {
                e.isValid = false;
                e.errorText = '【欄位名稱】不允許空白，請重新輸入';
                return false;
            }
        }

        function CheckQty(s, e)
        {

            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty != null)
            {
                iQty = Number(Qty);
                if (isNaN(iQty))
                {
                    e.isValid = false;
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iQty <= 0)
                {
                    e.isValid = false;
                    if (s.name.indexOf("txtDIVIDABLE_POINT") > 0)
                    {
                        e.errorText = '點數不允許小於0，請重新輸入';
                    }
                    else
                    {
                        e.errorText = '兌換次數不允許小於0，請重新輸入';
                    }
                    return false;
                }
                else if (Qty.indexOf(".") > 0)
                {
                    e.isValid = false;
                    if (s.name.indexOf("txtDIVIDABLE_POINT") > 0)
                    {
                        e.errorText = '點數不允許輸入小數點，請重新輸入';
                    }
                    else
                    {
                        e.errorText = '兌換次數不允許輸入小數點，請重新輸入';
                    }
                    return false;
                }
            }
        }

        function onOK()
        {
            __doPostBack('<%= txtBatchNO.UniqueID %>', 'AAA');
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
                <!--HG點數兌換-來店禮-->
                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoPointsExchangeStoreGift %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--開始日期-->
                        <asp:Literal ID="lblSDate" runat="server" Text="<%$ Resources:WebResources, StartdATE %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="lblSDate_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate_S" runat="server" ClientInstanceName="txtSDate">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="lblSDate_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate_E" runat="server" ClientInstanceName="txtEDate">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
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
                        <!--活動代號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPartNumberOfDiscountS" ClientInstanceName="txtPartNumberOfDiscountS" Size="15" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPartNumberOfDiscountE" ClientInstanceName="txtPartNumberOfDiscountE" Size="15" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--活動名稱-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtDiscountName" ClientInstanceName="txtDiscountName" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click">
                        </dx:ASPxButton>
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
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ACTIVITY_ID"
                            Width="100%" EnableCallBacks="false" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                            OnPageIndexChanged="gvMaster_PageIndexChanged" OnRowInserting="gvMaster_RowInserting"
                            OnRowUpdating="gvMaster_RowUpdating" OnFocusedRowChanged="gvMaster_FocusedRowChanged"
                            OnHtmlEditFormCreated="gvMaster_HtmlEditFormCreated" OnRowValidating="gvMaster_RowValidating"
                            OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                            OnInitNewRow="gvMaster_InitNewRow" OnStartRowEditing="gvMaster_StartRowEditing"
                            OnCancelRowEditing="gvMaster_CancelRowEditing">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                        </div>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Button" Caption=" ">
                                    <EditButton Visible="true">
                                    </EditButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="ITEMNO" runat="server" Caption="<%$ Resources:WebResources, Items %>" ReadOnly="true">
                                    <DataItemTemplate>
                                        <%#Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ACTIVITY_NO" HeaderStyle-HorizontalAlign="Center"
                                    Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" 
                                    PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"
                                    PropertiesTextEdit-ValidationSettings-ErrorText="必填欄位" >
                                    <HeaderCaptionTemplate>
                                        <span style="color: Red">*</span>
                                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>">
                                        </dx:ASPxLabel>
                                    </HeaderCaptionTemplate>
                                    <EditItemTemplate>
                                        <uc1:PopupControl ID="ACTIVITY_NO" runat="server" PopupControlName="DiscountPopup"
                                            AutoPostBack="false" IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                            OnClientTextChanged="function(s,e){ getProductInfo(s,e);}" Text='<%# Bind("ACTIVITY_NO") %>'
                                            Width="150" />
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ACTIVITY_NAME" Caption="<%$ Resources:WebResources, DiscountName %>">
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtDiscountName" runat="server" Text='<%# Bind("ACTIVITY_NAME") %>'
                                            ReadOnly="true" Width="200" Border-BorderStyle="None" ClientInstanceName="txtDiscountName">
                                        </dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartDate %>">
                                    <EditItemTemplate>
                                        <dx:ASPxDateEdit ID="txtS_DATE" runat="server" Value='<%# Bind("S_DATE") %>' ValidationSettings-RequiredField-IsRequired="true"
                                             ValidationSettings-ErrorText="必填欄位"
                                             ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MinDate='<%# DateTime.Today.AddDays(0) %>'>
                                            <ClientSideEvents Validation="function(s,e){ CheckRequiredField(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </EditItemTemplate>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>">
                                    <EditItemTemplate>
                                        <dx:ASPxDateEdit ID="txtE_DATE" runat="server" Value='<%# Bind("E_DATE") %>' MinDate='<%# DateTime.Today.AddDays(1) %>'>
                                        </dx:ASPxDateEdit>
                                    </EditItemTemplate>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="TYPE" Caption="<%$ Resources:WebResources, Category %>">
                                    <PropertiesComboBox ValueType="System.String">
                                    </PropertiesComboBox>
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lblTYPE" Text='<%# Bind("TYPE") %>' runat="server">
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxComboBox ID="ddlType" runat="server" ValueType="System.String" Value='<%# Bind("TYPE") %>'
                                            ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ErrorText="必填欄位" 
                                            Width="60px" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                             OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true">
                                            <Items>
                                                <dx:ListEditItem Text="點數" Value="1" />
                                                <dx:ListEditItem Text="商品" Value="2" />
                                            </Items>
                                            <ClientSideEvents Validation="function(s,e){ CheckRequiredField(s, e); }" />
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                                    <EditItemTemplate>
                                        <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup" Text='<%# Bind("PRODNO") %>'
                                            Width="130" KeyFieldValue1="extrasale" OnClientTextChanged="function(s,e){ getPRODINFOExtraSale(s,e);}" />
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DIVIDABLE_POINT" Caption="<%$ Resources:WebResources, Points %>">
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtDIVIDABLE_POINT" runat="server" MaxLength="3" Width="40" Text='<%# Bind("DIVIDABLE_POINT") %>'
                                            HorizontalAlign="Right" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                            <ValidationSettings>
                                                <RegularExpression ValidationExpression="\d*" />
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s,e){ CheckQty(s, e); }" />
                                        </dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="MEMBER_CHECK_FLAG" Caption="<%$ Resources:WebResources, NameListVerification %>">
                                    <DataItemTemplate>
                                        <dx:ASPxCheckBox ID="cbMEMBER_CHECK_FLAG" runat="server" ReadOnly="true">
                                        </dx:ASPxCheckBox>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxCheckBox ID="cbMEMBER_CHECK_FLAG" runat="server" AutoPostBack="true" OnCheckedChanged="cbMEMBER_CHECK_FLAG_CheckedChanged">
                                        </dx:ASPxCheckBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn FieldName="USE_COUNT" Caption="兌換次數">
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtUSE_COUNT" runat="server" MaxLength="3" Width="40" Text='<%# Bind("USE_COUNT") %>'
                                            HorizontalAlign="Right" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                            <ValidationSettings>
                                                <RegularExpression ValidationExpression="\d*" />
                                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents Validation="function(s,e){ CheckRequiredField(s, e); CheckQty(s, e); }" />
                                        </dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnAddM" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                    OnClick="btnAddM_Click" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxButton SkinID="DeleteBtn" ID="btnDeleteM" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                    OnClick="btnDeleteM_Click" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblError" runat="server" ForeColor="Red">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                            </Templates>
                            <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <Settings ShowTitlePanel="True" />
                            <SettingsEditing Mode='Inline' />
                        </cc:ASPxGridView>
                    </div>
                    <div class="seperate">
                    </div>
                    <div id="Div_Dt">
                        <dx:ASPxPageControl ID="ASPxPageControl1" ClientInstanceName="ASPxPageControl1" runat="server"
                            Visible="false" ActiveTabIndex="0" Width="100%" AutoPostBack="true" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged" onprerender="ASPxPageControl1_PreRender">
                            <TabPages>
                                <dx:TabPage Text="<%$ Resources:WebResources, SpecifyStore %>">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <div>
                                                <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Width="100%"  EnableCallBacks="false"
                                                    KeyFieldName="SID" OnPageIndexChanged="gvDetail_PageIndexChanged" OnRowInserting="gvDetail_RowInserting"   OnStartRowEditing="gvDetail_OnStartRowEditing" 
                                                    OnRowUpdating="gvDetail_RowUpdating" OnPreRender="gvDetail_PreRender" OnRowValidating="gvDetail_RowValidating">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <HeaderTemplate>
                                                                <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                            </HeaderTemplate>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Button" Caption=" ">
                                                            <EditButton Visible="true">
                                                            </EditButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                                                            <DataItemTemplate>
                                                                <%#Container.ItemIndex + 1%>
                                                            </DataItemTemplate>
                                                            <EditItemTemplate>
                                                                &nbsp;</EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                                                            <EditItemTemplate>
                                                                <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" Text='<%#Bind("STORE_NO") %>'
                                                                    SetClientValidationEvent="getStoreInfo" IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>' />
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                                                            <EditItemTemplate>
                                                                <dx:ASPxLabel ID="txtStoreName" runat="server" Text='<%# Bind("STORENAME") %>' ClientInstanceName="STORENAME">
                                                                </dx:ASPxLabel>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="ZONE_NAME" Caption="<%$ Resources:WebResources, ByDistrict %>">
                                                            <EditItemTemplate>
                                                                <dx:ASPxLabel ID="txtByDistrict" runat="server" Text='<%# Bind("ZONE_NAME") %>' ClientInstanceName="ZONE">
                                                                </dx:ASPxLabel>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <TitlePanel>
                                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnAddD" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                            OnClick="btnAddD_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton SkinID="DeleteBtn" ID="btnDeleteD" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                                            OnClick="btnDeleteD_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="lblZone" runat="server" Text="區域：">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="ddlZone" runat="server" Width="80px">
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnConfirm" runat="server" Text="<%$ Resources:WebResources, Confirm %>"
                                                                            OnClick="btnConfirm_Click">
                                                                            <ClientSideEvents />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </TitlePanel>
                                                        <EditForm>
                                                            <table width="80%" align="center">
                                                                <tr>
                                                                    <%--門市編號--%>
                                                                    <td class="tdtxt" nowrap="nowrap">
                                                                        <asp:Literal ID="lblStoreNo" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                                                                    </td>
                                                                    <td class="tdval" nowrap="nowrap">
                                                                        <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" Text='<%#Eval("[STORE_NO]") %>'
                                                                            SetClientValidationEvent="getStoreInfo" IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>' />
                                                                    </td>
                                                                    <%--門市名稱--%>
                                                                    <td class="tdtxt" nowrap="nowrap">
                                                                        <asp:Literal ID="lblStoreName" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                                                                    </td>
                                                                    <td class="tdval" nowrap="nowrap">
                                                                        <dx:ASPxLabel ID="txtStoreName" runat="server" Text='<%# Eval("STORENAME") %>' ClientInstanceName="STORENAME">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <%--區域別--%>
                                                                    <td class="tdtxt" nowrap="nowrap">
                                                                        <asp:Literal ID="lblByDistrict" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                                                                    </td>
                                                                    <td class="tdval" nowrap="nowrap">
                                                                        <dx:ASPxLabel ID="txtByDistrict" runat="server" Text='<%# Eval("ZONE_NAME") %>' ClientInstanceName="ZONE">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                    runat="server">
                                                                </dx:ASPxGridViewTemplateReplacement>
                                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                    runat="server">
                                                                </dx:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="5">
                                                    </SettingsPager>
                                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                                    <Settings ShowTitlePanel="True" />
                                                    <SettingsEditing Mode="Inline" />
                                                </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="<%$ Resources:WebResources, List %>">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="titlef">
                                                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" />
                                                        <dx:ASPxTextBox ID="txtBatchNO" runat="server" Width="170px" ClientVisible="false">
                                                        </dx:ASPxTextBox>
                                                        <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                                            CloseAction="CloseButton"   PopupHorizontalAlign="Center"
                                                            PopupVerticalAlign="WindowCenter" ShowFooter="false" onOKScript="onOK" Width="500px"
                                                            Height="500px" TargetElementID="txtBatchNO" Modal="true" HeaderText="<%$ Resources:WebResources, HappyGoPointListUpload %>"
                                                            ClientInstanceName="dataImportPopup" PopupElementID="btnImport" LoadingPanelID="lp">
                                                          
                                                               <ClientSideEvents Init="function(s, e) {
                    var iframe = s.GetContentIFrame();                   
                    iframe.popupArguments = {};
                    iframe.contentLoaded = false;
                    var controlCollection = ASPxClientControl.GetControlCollection();                
                    iframe.popupArguments.popupContainer = controlCollection.Get('ASPxPageControl1_ASPxPopupControl1');
                    ASPxClientUtils.AttachEventToElement(iframe, 'load', function(e) 
                    {
                        if (!controlCollection.Get('ASPxPageControl1_ASPxPopupControl1').GetClientVisible()) 
                            return; 
                        controlCollection.Get('ASPxPageControl1_lp').Hide(); 
                        iframe.contentLoaded = true;
                    });
                                                                                   
                    var targetElementId = 'ASPxPageControl1_txtBatchNO';                                                                                        
                    iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId) 
                        || document.getElementById(targetElementId);
                                                                                       
                    var onOKScript = onOK;                                                                                        
                    iframe.popupArguments.okscript = onOKScript;
                    }" Shown="function(s, e) {  
                    if (!s.GetContentIFrame().contentLoaded)   
                    ASPxClientControl.GetControlCollection().Get('ASPxPageControl1_lp')
                        .ShowInElement(s.GetContentIFrame());}" />
                                                            <ContentCollection>
                                                                <dx:PopupControlContentControl runat="server">
                                                                </dx:PopupControlContentControl>
                                                            </ContentCollection>
                                                        </cc:ASPxPopupControl>
                                                        <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                        </dx:ASPxLoadingPanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblImportStatus" runat="server">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="seperate"></div>
    </div>
</asp:Content>
