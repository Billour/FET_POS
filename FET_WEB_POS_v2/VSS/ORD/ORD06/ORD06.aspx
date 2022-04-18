<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD06.aspx.cs" Inherits="VSS_ORD_ORD06" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        var _prodName = '';
        var _TmpGvSender = '';
        function getProductInfo(s, e)
        {
            _prodName = s.name;
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
            {
                _TmpGvSender = _gvSender.GetText();
                PageMethods.getProductInfo(_TmpGvSender.trim(), getProductInfo_OnOK);
                _gvSender.SetValue(_TmpGvSender.trim());
            } else    
                txtProdName.SetValue(null);              
          
          
        }
        function getProductInfo_OnOK(returnData)
        {
            if (returnData != '')
            {
              txtProdName.SetValue(returnData);
             
                //_gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
            else
            {          
               txtProdName.SetValue(null);
               _gvSender.SetText(null);
               alert("【主商品編號】不存在");
             
            }
          
        }


        function getWithTheProductInfo(s, e)
        {
            _prodName = s.name;
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
            {
                _TmpGvSender = _gvSender.GetText();
                PageMethods.getProductInfo(_TmpGvSender.trim(), getWithTheProductInfo_OnOK);
                _gvSender.SetValue(_TmpGvSender.trim());
            } else                       
                 txtWithTheProductName.SetValue(null);
                
        
        }
        function getWithTheProductInfo_OnOK(returnData)
        {
            if (returnData != '')
            {
               txtWithTheProductName.SetValue(returnData);               
                _gvSender.Focus();
            }
            else
            {       
                    txtWithTheProductName.SetValue(null);
                    _gvSender.SetText(null);
                    //_gvSender.SetText(null);
                    alert("【搭配商品編號】不存在");
             
            }

        }

        function getAddPMProductInfo(s, e)
        {
            _prodName = s.name;
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
            {
                _TmpGvSender = _gvSender.GetText();
                PageMethods.getProductInfo(_TmpGvSender.trim(), getAddPMProductInfo_OnOK);
                _gvSender.SetValue(_TmpGvSender.trim());
            } 
          
        }
        function getAddPMProductInfo_OnOK(returnData)
        {
            if (returnData != '')
            {
                lblPM_PRODNAME.SetValue(returnData);          
                _gvSender.Focus();
            }
            else          
               lblPM_PRODNAME.SetValue(null);         

        }

        function getAddPDProductInfo(s, e)
        {
            _prodName = s.name;
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
            {
                _TmpGvSender = _gvSender.GetText();
                PageMethods.getProductInfo(_TmpGvSender.trim(), getAddPDProductInfo_OnOK);
                _gvSender.SetValue(_TmpGvSender.trim());
            }

        }
        function getAddPDProductInfo_OnOK(returnData)
        {
            if (returnData != '')
            {
                lblPD_PRODNAME.SetValue(returnData);           
                //_gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
            else      
              lblPD_PRODNAME.SetValue(null);
             

        }
        
        function checkDate(s, e)
        {
            var dS = new Date(txtStartDate.GetText());
            var dE = new Date(txtEndDate.GetText());
            if (dS > dE)
            {
                alert('「結束日期不允許小於開始日期，請重新輸入」');
                s.SetText('');
            }

        }

        function onOK()
        {

            ac1.PerformCallback();
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="hdNo" runat="server" class="hdNo" />
    <div>
        <div class="titlef">
            <%--一搭一設定作業--%>
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TwoForOneOfferSetting %>"></asp:Literal>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <%--主商品編號--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, MainProductNumber %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup"
                                Text="" IsValidation="false" SetClientValidationEvent="getProductInfo" />
                        </td>
                        <%--主商品名稱--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, MainProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtProdName" ClientInstanceName="txtProdName" runat="server"
                                Width="300px" Text="" ReadOnly="true">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <%--搭配商品編號--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, CollocationProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <uc1:PopupControl ID="txtWithTheProductNo" runat="server" PopupControlName="ProductsPopup"
                                Text="" IsValidation="false" SetClientValidationEvent="getWithTheProductInfo" />
                        </td>
                        <%--搭配商品名稱--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, WithTheProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtWithTheProductName" ClientInstanceName="txtWithTheProductName"
                                runat="server" Width="300px" Text="" ReadOnly="true">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <%--開始日期--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxDateEdit ID="txtStartDate" runat="server" ClientInstanceName="txtStartDate"    EditFormatString="yyyy/MM/dd"
                                >
                            </dx:ASPxDateEdit>
                        </td>
                        <%--結束日期--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxDateEdit ID="txtEndDate" runat="server" ClientInstanceName="txtEndDate"     EditFormatString="yyyy/MM/dd">
                                <ClientSideEvents ValueChanged="function(s,e){checkDate(s,e);}" />
                            </dx:ASPxDateEdit>
                        </td>
                        <%--狀態--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="cbStatus" runat="server" Width="120px">
                                <Items>
                                    <dx:ListEditItem Value="" Text="ALL" Selected="true" />
                                    <dx:ListEditItem Value="1" Text="有效" />
                                    <dx:ListEditItem Value="2" Text="過期" />
                                    <dx:ListEditItem Value="3" Text="尚未生效" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                CausesValidation="false" OnClick="btnSearch_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <dx:ASPxCallbackPanel ID="ac1" runat="server" ClientInstanceName="ac1" OnCallback="ac1_Callback">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <cc:ASPxGridView ID="gvMaster" runat="server" KeyFieldName="SID" ClientInstanceName="gvMaster"
                                        AutoGenerateColumns="False" Width="100%" EnableCallBacks="true" EnableViewState="true"
                                        OnPageIndexChanged="gvMaster_PageIndexChanged" OnRowUpdating="gvMaster_RowUpdating"
                                        OnRowInserting="gvMaster_RowInserting" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                                        OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnInitNewRow="gvMaster_InitNewRow"
                                        OnRowValidating="gvMaster_RowValidating" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                                        OnStartRowEditing="gvMaster_StartRowEditing">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <HeaderTemplate>
                                                    <div style="text-align: center">
                                                        <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                                    </div>
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                                <HeaderCaptionTemplate>
                                                </HeaderCaptionTemplate>
                                                <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                                </EditButton>
                                                <UpdateButton Text="<%$ Resources:WebResources, Save %>">
                                                </UpdateButton>
                                                <CancelButton Text="<%$ Resources:WebResources, Cancel %>">
                                                </CancelButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ITEMS" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                                                VisibleIndex="2">
                                                <EditItemTemplate>
                                                    <dx:ASPxLabel ID="lblITEMS" runat="server" Text='<%#Bind("ITEMS") %>'>
                                                    </dx:ASPxLabel>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="SID" runat="server" Visible="false" VisibleIndex="2">
                                                <EditItemTemplate>
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Bind("SID") %>'>
                                                    </dx:ASPxLabel>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="STATUS" HeaderStyle-HorizontalAlign="Center"
                                                Caption="<%$ Resources:WebResources, Status %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxLabel ID="lblSTATUS" runat="server" Text='<%#Bind("STATUS") %>'>
                                                    </dx:ASPxLabel>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="PM_PRODNO" HeaderStyle-HorizontalAlign="Center"
                                                Caption="<%$ Resources:WebResources, MainProductNumber %>">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span>
                                                    <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, MainProductNumber %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="txtPM_PRODNO" runat="server" PopupControlName="ProductsPopup"
                                                        Text='<%#Bind("PM_PRODNO") %>' SetClientValidationEvent="getAddPMProductInfo" />
                                                </EditItemTemplate>
                                                <DataItemTemplate>
                                                    <dx:ASPxLabel ID="lblPM_PRODNO" runat="server" Text='<%#Bind("PM_PRODNO") %>'>
                                                    </dx:ASPxLabel>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="PM_PRODNAME" HeaderStyle-HorizontalAlign="Center"
                                                Caption="<%$ Resources:WebResources, MainProductName %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxLabel ID="lblPM_PRODNAME" runat="server" Text='<%#Bind("PM_PRODNAME") %>'
                                                        ClientInstanceName="lblPM_PRODNAME">
                                                    </dx:ASPxLabel>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="PD_PRODNO" HeaderStyle-HorizontalAlign="Center"
                                                Caption="<%$ Resources:WebResources, CollocationProductCode %>">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span>
                                                    <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CollocationProductCode %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="txtPD_PRODNO" runat="server" PopupControlName="ProductsPopup"
                                                        Text='<%#Bind("PD_PRODNO") %>' SetClientValidationEvent="getAddPDProductInfo" />
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="PD_PRODNAME" HeaderStyle-HorizontalAlign="Center"
                                                Caption="<%$ Resources:WebResources, WithTheProductName %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxLabel ID="lblPD_PRODNAME" runat="server" Text='<%#Bind("PD_PRODNAME") %>'
                                                        ClientInstanceName="lblPD_PRODNAME">
                                                    </dx:ASPxLabel>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn FieldName="S_DATE" runat="server" Caption="<%$ Resources:WebResources, StartDate %>"
                                                VisibleIndex="7">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span>
                                                    <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StartDate %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="txtSDATE" ClientInstanceName="txtSDATE" runat="server" Value='<%# Bind("S_DATE") %>' MinDate='<%# DateTime.Today.AddDays(1) %>'>
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn VisibleIndex="9" FieldName="E_DATE" HeaderStyle-HorizontalAlign="Center"
                                                Caption="<%$ Resources:WebResources, EndDate %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxDateEdit ID="txtEDATE" runat="server" Value='<%#Bind("E_DATE")%>' MinDate='<%# DateTime.Today.AddDays(1) %>'>
                                                    </dx:ASPxDateEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataDateColumn>
                                            
                                            <dx:GridViewDataTextColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                                <DataItemTemplate>
                                                    <dx:ASPxLabel ID="lbMODI_DTM" runat="server" Text='<%# Eval("MODI_DTM") %>'>
                                                    </dx:ASPxLabel>
                                                </DataItemTemplate>
                                                <EditItemTemplate>
                                                    <dx:ASPxLabel ID="lbMODI_DTM" runat="server" Text='<%# Eval("MODI_DTM") %>'>
                                                    </dx:ASPxLabel>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnNew" runat="server" OnClick="btnNew_Click" Text="<%$ Resources:WebResources, Add %>"
                                                                CausesValidation="true">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelete" runat="server" CausesValidation="false" Text="<%$ Resources:WebResources, Delete %>"
                                                                OnClick="btnDelete_Click" SkinID="DeleteBtn">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                                            </dx:ASPxButton>
                                                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                                                CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl='~/VSS/ORD/ORD06/ORD06_Import.aspx'
                                                                Width="640" Height="400" LoadingPanelID="lp" HeaderText="一搭一商品上傳" onOKScript="onOK">
                                                                <ContentStyle>
                                                                    <Paddings Padding="4px"></Paddings>
                                                                </ContentStyle>
                                                            </cc:ASPxPopupControl>
                                                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                            </dx:ASPxLoadingPanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabe11" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label></EmptyDataRow>
                                        </Templates>
                                        <SettingsBehavior AllowFocusedRow="false" ProcessFocusedRowChangedOnServer="false" />
                                        <SettingsPager PageSize="10">
                                        </SettingsPager>
                                        <Settings ShowTitlePanel="True" />
                                        <SettingsEditing Mode="Inline" />
                                    </cc:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                       </dx:ASPxCallbackPanel>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
