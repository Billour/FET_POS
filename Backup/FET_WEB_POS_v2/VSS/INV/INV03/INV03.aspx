<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV03.aspx.cs" Inherits="VSS_INV_INV03" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            if (_prodName != '' && _prodName.indexOf('txtProdNo') > -1) {
                txtProdName.SetValue(returnData);
            }
            _gvEventArgs.processOnServer = false;
            _gvSender.Focus();
        }
        else {
            if (_prodName != '' && _prodName.indexOf('txtProdNo') > -1) {
                txtProdName.SetValue(null);
            } 
        }
    }
    //檢查門市
        function CheckStoreNO(s, e) {        
            var StoreNO = s.GetValue();
            PageMethods.getStoreInfo(StoreNO, getStoreInfo_OnOK);
        }

        function getStoreInfo_OnOK(returnData) {
            if (returnData != '') {
                txtStoreName.SetText(returnData);
            }
        }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--庫存查詢作業-->
            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, StockSearch %>"></asp:Literal>
        </div>
        
        <div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                    </td>
                    <td>
                        <div style="width: 220px;">
                            <dx:ASPxComboBox ID="cbbZone" runat="server" ValueType="System.String" SelectedIndex="1">
                            </dx:ASPxComboBox>
                        </div>
                    </td>
                    <td>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td>
                        <uc1:PopupControl ID="txtStoreNo" runat="server" PopupControlName="StoresPopup" SetClientValidationEvent="CheckStoreNO" />
                    </td>
                    <td>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtStoreName" runat="server" Width="100px" ClientInstanceName="txtStoreName">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Litera20" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cboCategory" runat="server">
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td>
                        <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup" SetClientValidationEvent="getProductInfo" />
                    </td>
                    <td>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtProdName"  ClientInstanceName="txtProdName" runat="server" Text="" Width="180px"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Warehouse %>"></asp:Literal>：
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cbbLoc" runat="server">
                        </dx:ASPxComboBox>
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
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"/>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PRODNO"
                    Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="ZONE_NAME" Caption="<%$ Resources:WebResources, ByDistrict %>" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>"  CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="PRODTYPENAME" Caption="<%$ Resources:WebResources, ProductCategory %>" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" CellStyle-HorizontalAlign="Right" />
                        <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="STOCK_NAME" Caption="<%$ Resources:WebResources, Warehouse %>" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="ON_HAND_QTY" Caption="<%$ Resources:WebResources, Quantity %>" CellStyle-HorizontalAlign="Right" />
                    </Columns>
                    <Styles Header-HorizontalAlign="Center"></Styles>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="10"></SettingsPager>
                </cc:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
