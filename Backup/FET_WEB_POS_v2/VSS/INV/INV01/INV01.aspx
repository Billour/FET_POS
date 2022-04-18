<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV01.aspx.cs" Inherits="VSS_INV_INV01" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register src="../../../Controls/ExportExcelData.ascx" tagname="ExportExcelData" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">
        _gvSender = null;
        _gvEventArgs = null;
        function getProductInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '') {
                PageMethods.getProductInfo(_gvSender.GetText(), getProductInfo_OnOK);
            }
            else {
                lblProductName.SetValue(null);
            }
        }
        
        function getProductInfo_OnOK(returnData) {
            if (returnData != '') {
                lblProductName.SetValue(returnData);
                _gvSender.Focus();
            }
            else {
                lblProductName.SetValue(null);
            }
        }

        function getStoreInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;

            if (s.GetText() != '') {
                PageMethods.getStoreInfo(_gvSender.GetText(), getStoreInfo_OnOK);
            }
            else {
                lblStoreFromName.SetValue(null);
            }
        }
        function getStoreInfo_OnOK(returnData) {
            if (returnData != '') {
                lblStoreFromName.SetValue(returnData);
                _gvSender.Focus();
            }
            else {
                lblStoreFromName.SetValue(null);
            }
        }
        function getStoreInfo2(s, e) {
            _gvEventArgs = e;
            _gvSender = s;

            if (s.GetText() != '') {
                PageMethods.getStoreInfo(_gvSender.GetText(), getStoreInfo_OnOK2);
            }
            else {
                lblStoreFromName.SetValue(null);
            }
        }
        function getStoreInfo_OnOK2(returnData) {
            if (returnData != '') {
                lblStoreToName.SetValue(returnData);
                _gvSender.Focus();
            }
            else {
                lblStoreFromName.SetValue(null);
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip"></div>
    <div>
        <div class="titlef">
            <!--總部移撥查詢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TransferSearchHQ %>"></asp:Literal>
        </div>
        
        <div>
            <asp:HiddenField ID="tempstoreNO" runat="server" />
            <table style="width: 100%">
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtTransferNo" runat="server" Width="200px" />
                    </td>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="ProductsPopup" runat="server" PopupControlName="ProductsPopup"  OnClientTextChanged="function(s,e){getProductInfo(s,e);}"  />
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="lblProductName" runat="server" ReadOnly="true" ClientInstanceName="lblProductName" Text="" Border-BorderStyle="None" Width="300"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移出日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                    </td>
                    <td colspan="1">
                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="transferOutStartDate" runat="server" EditFormatString="yyyy/MM/dd">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server" EditFormatString="yyyy/MM/dd">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--撥出門市名稱-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, StoreFromID %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="1">
                        <uc1:PopupControl ID="transferOutStorePopup" runat="server" PopupControlName="StoresPopup" OnClientTextChanged="function(s,e){getStoreInfo(s,e);}" />
                    </td>
                    <td class="tdtxt">
                        <!--撥出門市名稱-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="lblStoareFromName" runat="server" ClientInstanceName="lblStoreFromName" Text="" Width="200px"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--撥入日期-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：
                    </td>
                    <td colspan="1">
                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="transferInStartDate" runat="server" EditFormatString="yyyy/MM/dd">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="transferInEndDate" runat="server" EditFormatString="yyyy/MM/dd">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市名稱-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, lblStoreToID %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="1">
                        <uc1:PopupControl ID="transferInStorePopup" runat="server" PopupControlName="StoresPopup" OnClientTextChanged="function(s,e){getStoreInfo2(s,e);}" />
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市名稱-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="lblStoreToName" runat="server" ClientInstanceName="lblStoreToName" Text="" Width="200px"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="cobStatus" runat="server" Width="100">
                            <Items>
                                <dx:ListEditItem Value="" Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Value="20" Text="在途" />
                                <dx:ListEditItem Value="30" Text="已撥入" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click"/>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="resetButton" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="STNO"
            Width="100%" 
            OnHtmlRowPrepared="grid_HtmlRowPrepared" 
            OnHtmlRowCreated="grid_HtmlRowCreated"
            OnPageIndexChanged="grid_PageIndexChanged">
            <Columns>
                <dx:GridViewDataCheckColumn VisibleIndex="0"  Width="10">
                    <DataItemTemplate>
                        <input type="radio" name="RadioButton1"  />
                    </DataItemTemplate>
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataColumn FieldName="STNO" Caption="<%$ Resources:WebResources, TransferSlipNo %>" />
                <dx:GridViewDataColumn FieldName="TSTATUS" Caption="<%$ Resources:WebResources, TransferStatus %>" />
                <dx:GridViewDataColumn FieldName="FROM_STORE_NO" Caption="<%$ Resources:WebResources, TransferFrom %>" />
                <dx:GridViewDataColumn FieldName="STDATE" Caption="<%$ Resources:WebResources, TransferOutDate %>" />
                <dx:GridViewDataColumn FieldName="TO_STORE_NO" Caption="<%$ Resources:WebResources, TransferTo %>">
                    <EditCellStyle Wrap="True"></EditCellStyle>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="TSTDATE" Caption="<%$ Resources:WebResources, TransferInDate %>" />
                <dx:GridViewDataColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
            </Columns>
            <Templates>
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                    UseSubmitBehavior="False" OnClick="btnXlsExport_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
                <DetailRow>
                    <cc:ASPxGridView ID="detailGrid" runat="server" ClientInstanceName="detailGrid" KeyFieldName="UUID"
                        Width="100%" EnableRowsCache="true"  
                        OnBeforePerformDataSelect="detailGrid_DataSelect"
                        OnPageIndexChanged="detailGrid_PageIndexChanged"
                        onhtmlrowcreated="detailGrid_HtmlRowCreated">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" >
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="Txt_PRODNO" ReadOnly="true" runat="server" Text='<%# Bind("PRODNO") %>'  Border-BorderStyle="None"></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" >
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="Txt_PRODNAME" ReadOnly="true" runat="server" Text='<%# Bind("PRODNAME") %>' Width="300"  Border-BorderStyle="None"></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn> 
                            <dx:GridViewDataColumn FieldName="OUTQTY" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>" >
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="Txt_OUTQTY" ReadOnly="true" runat="server" Text='<%# Bind("OUTQTY") %>' Width="50"  Border-BorderStyle="None"></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                             
                            <dx:GridViewDataTextColumn FieldName="imgIMEI" Caption=" " Width="20px">
                                <DataItemTemplate>
                                    <dx:ASPxImage ID="imgOUTIMEI" runat="server" ImageUrl="">
                                    </dx:ASPxImage>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>                          
                            <dx:GridViewDataColumn FieldName="OUTIMEI" Caption="<%$ Resources:WebResources, TransferOUTImei %>">
                                <DataItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right">
                                                <div id="divOUTIMEI_QTY" runat="server" >
                                                <dx:ASPxLabel ID="lblOUTIMEI" runat="server" Text='<% #Bind("OUTQTY") %>'>
                                                </dx:ASPxLabel>
                                                </div>
                                            </td>
                                            <div id="divOUTIMEI" runat="server">
                                                <td>
                                                    <dx:ASPxTextBox ID="lblOUTIMEIFlag" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_FLAG") %>'
                                                        Border-BorderStyle="None" ClientVisible="false">
                                                    </dx:ASPxTextBox>
                                                    <uc1:PopupControl ID="lblOUTShowIMEI" runat="server" PopupControlName="InputIMEIData" Text='<%# Bind("OUTIMEI") %>'
                                                        Enabled="false" />
                                                </td>
                                            </div>
                                        </tr>
                                    </table>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                       
                            <dx:GridViewDataColumn FieldName="INQTY" Caption="<%$ Resources:WebResources, TransferredInQuantity %>" >
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="Txt_INQTY" runat="server" Border-BorderStyle="None" Text='<%# Bind("INQTY") %>'></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                            
                            <dx:GridViewDataTextColumn FieldName="imgIMEI" Caption=" " Width="20px">
                                <DataItemTemplate>
                                    <dx:ASPxImage ID="imgINIMEI" runat="server" ImageUrl="">
                                    </dx:ASPxImage>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>                                                    
                            <dx:GridViewDataColumn FieldName="INIMEI" Caption="<%$ Resources:WebResources, TransferInImei %>">
                                <DataItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right">
                                                <div id="divINIMEI_QTY" runat="server" >
                                                <dx:ASPxLabel ID="lblINIMEI" runat="server" Text='<% #Bind("INQTY") %>'>
                                                </dx:ASPxLabel>
                                                </div>
                                            </td>
                                            <div id="divINIMEI" runat="server">
                                                <td>
                                                    <dx:ASPxTextBox ID="lblINIMEIFlag" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_FLAG") %>'
                                                        Border-BorderStyle="None" ClientVisible="false">
                                                    </dx:ASPxTextBox>
                                                    <uc1:PopupControl ID="lblShowINIMEI" runat="server" PopupControlName="InputIMEIData" Text='<%# Bind("INIMEI") %>'
                                                        Enabled="false" />
                                                </td>
                                            </div>
                                        </tr>
                                    </table>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="STNO" Caption="<%$ Resources:WebResources, TransferSlipNo %>" Visible="false" />
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <!--移撥單號-->
                                <asp:Literal ID="lit" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>
                                ：<asp:Label ID="Label5" runat="server" Text="123"></asp:Label>
                            </TitlePanel>
                        </Templates>  
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <Settings ShowFooter="false" ShowTitlePanel="true" />
                        <SettingsDetail IsDetailGrid="true" />
                        <SettingsPager PageSize="5">
                        </SettingsPager>
                    </cc:ASPxGridView>
                </DetailRow>
            </Templates>           
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
            <Settings ShowTitlePanel="True" />
            <SettingsPager PageSize="15"></SettingsPager>    
            <SettingsBehavior AllowFocusedRow="true" />  
        </cc:ASPxGridView>
       
    </div>
 
    <iframe id="fDownload" style="display:none" src="" runat="server"></iframe>
 
</asp:Content>
