<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV25.aspx.cs" Inherits="VSS_INV_INV25" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>
    
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip">
    </div>

    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--移出作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferOut %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="LinkButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" PostBackUrl="INV24.aspx" CausesValidation="false">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
            <table>
                <tr>
                    <td align="right">
                        <!--移撥單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td align="left">
                        <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                        <!--撥入門市-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal3" runat="server" Text="撥入門市編號"></asp:Literal>：
                    </td>
                    <td align="left">
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" IsValidation="true"  />
                        <%--<table>
                            <tr>
                                <td width="120px">
                                    <div style="width:120px;">
                                        <dx:ASPxTextBox ID="TextBox14" runat="server" Width="100">
                                            <ValidationSettings CausesValidation="false">                                                
                                                <RequiredField IsRequired="true" />                                                       
                                            </ValidationSettings>                                                
                                        </dx:ASPxTextBox>
                                    </div>
                                </td>
                                <td width="10px">
                                    <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>" SkinID="PopupButton">
                                    </dx:ASPxButton>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>--%>
                    </td>
                    <td align="right">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td align="left">
                        <asp:Label ID="Label2" runat="server" Text="00 未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td align="right">
                        <!--更新日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources,ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td align="left">
                        <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3"></td>
                    <td align="right">
                        <!--更新人員-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td align="left">
                        <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                    </td>
                </tr>
            </table>
    </div>
        
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <div id="Div1" runat="server" class="SubEditBlock" visible="true">
                <div class="GridScrollBar" style="height: auto">
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
                        AutoGenerateColumns="False" KeyFieldName="商品料號"
                        OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating"
                        Width="100%" oncommandbuttoninitialize="gvMaster_CommandButtonInitialize" 
                        OnHtmlRowCreated="gvMaster_HtmlRowCreated" 
                        oninitnewrow="gvMaster_InitNewRow">
                        <SettingsEditing Mode="Inline" />
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                                <EditItemTemplate>
                                     <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" />

                                    <%--<table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right">
                                                <dx:ASPxTextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>' Width="100px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td align="left">
                                                <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>" SkinID="PopupButton">
                                                    <ClientSideEvents Click="function(s, e) {
	                                                    openwindow('../ORD/ORD01_searchProductNo.aspx',640,300);return false;
                                                    }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>--%>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                <EditItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%# Bind("商品名稱") %>'></dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>                            
                            <dx:GridViewDataColumn FieldName="移出數量" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn Caption=" ">
                                <DataItemTemplate>
                                    <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl=""></dx:ASPxImage>
                                </DataItemTemplate>
                                <EditItemTemplate>&nbsp;</EditItemTemplate>                                                                                      
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>">
                                <EditItemTemplate>
                                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="InputIMEIData" />

                                    <%--<table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right">
                                                <dx:ASPxLabel ID="countLabel" runat="server" Text="1" Width="20"></dx:ASPxLabel>
                                            </td>
                                            
                                            <td>
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px" Text="1234566">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td align="left">
                                                <dx:ASPxButton ID="popupButton" runat="server" Text="<%$ Resources:WebResources, Choose %>" Enabled="false" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e) {
                                                              openwindow('../SAL/SAL01_inputIMEIData.aspx','500','400');return false;
                                                            }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>--%>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Templates>                            
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" border="0" align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddNew_Click" CausesValidation="false" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <Settings ShowTitlePanel="true" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                    </cc:ASPxGridView>
                </div>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td align="right">
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfirmTransferOut %>" 
                                OnClick="btnSave_Click" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CausesValidation="false"/>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, PrintTransferOutSlip %>" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

   <%-- <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server" EnableViewState="False"
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
</asp:Content>
