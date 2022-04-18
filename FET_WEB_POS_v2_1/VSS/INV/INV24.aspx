<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV24.aspx.cs" Inherits="VSS_INV_INV24" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip">
    </div>
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--移出查詢作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferOutSearch %>"></asp:Literal>
                </td>
                <td align="right">
                
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server" Width="100">
                            <Items>
                                <dx:ListEditItem Value="暫存" Text="暫存" />
                                <dx:ListEditItem Value="在途" Text="在途" Selected="true" />
                                <dx:ListEditItem Value="巳撥入" Text="巳撥入" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移出日期-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="1">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="postbackDate_TextBox1" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="postbackDate_TextBox2" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市名稱-->
                        <asp:Literal ID="Literal17" runat="server" Text="撥入門市名稱"></asp:Literal>：
                    </td>
                    <td class="tdval" align="left">
                       <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" IsValidation="true"  />
                       
                       <%-- <table cellpadding="0" cellspacing="0" border="0" align="left">
                            <tr>
                                <td align="right">
                                    <dx:ASPxTextBox ID="TextBox2" runat="server" Width="100"></dx:ASPxTextBox>
                                </td>
                                <td align="left">
                                    <dx:ASPxButton ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>--%>
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
                    <td align="left">
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
            KeyFieldName="移撥單號" OnHtmlRowCreated="gvMaster_HtmlRowCreated1" 
            onhtmlrowprepared="gvMaster_HtmlRowPrepared">
            <Columns>
                <dx:GridViewDataColumn FieldName="移撥單號" Caption="<%$ Resources:WebResources, TransferSlipNo %>"
                    VisibleIndex="0">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="撥入門市" Caption="<%$ Resources:WebResources, TransferTo %>"
                    VisibleIndex="1" />
                <dx:GridViewDataColumn FieldName="移出日期" Caption="<%$ Resources:WebResources, TransferOutDate %>"
                    VisibleIndex="2" />
                <dx:GridViewDataColumn FieldName="撥入日期" Caption="<%$ Resources:WebResources, TransferInDate %>"
                    VisibleIndex="3" />
                <dx:GridViewDataColumn FieldName="移撥狀態" Caption="<%$ Resources:WebResources, TransferStatus %>"
                    VisibleIndex="4" />
                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="5" />
                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    VisibleIndex="6" />
            </Columns>
            <SettingsPager PageSize="5">
            </SettingsPager>
            <Templates>
                <DetailRow>
                    <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="detailGrid" Width="100%"
                        KeyFieldName="商品料號" OnHtmlRowCreated="gvDetail_HtmlRowCreated">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="0"/>
                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="1"/>
                            <%--<dx:GridViewDataColumn FieldName="IMEI控管" Caption="<%$ Resources:WebResources, ImeiControl %>" VisibleIndex="2">
                                <DataItemTemplate>
                                    <asp:HiddenField ID="hidIMEI" runat="server" Value='<%# Bind("IMEI控管") %>' />
                                    <asp:CheckBox ID="CheckBox3" runat="server" Enabled="false" />
                                 </DataItemTemplate>
                            </dx:GridViewDataColumn>--%>
                            <dx:GridViewDataColumn FieldName="移出數量" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>" VisibleIndex="3" />
                            <dx:GridViewDataColumn Caption="" VisibleIndex="4">
                                <DataItemTemplate>
                                    <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                    </dx:ASPxImage>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>" VisibleIndex="5">
                                <DataItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                        <td>
                                                <dx:ASPxLabel ID="lbl1" runat="server" Text='<%# Bind("[IMEI]") %>' Width="20px"
                                            Enabled="false" DisabledStyle-Font-Underline="true">
                                        </dx:ASPxLabel>
                                           </td> 
                                             <td>
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px" Text="1234566" Enabled="false">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            
                                                <dx:ASPxButton ID="Button10" runat="server" Text="<%$ Resources:WebResources, Choose %>" Enabled="false" AutoPostBack="false">
                                                 <ClientSideEvents Click="function(s, e) {
	                                                            openwindow('../SAL/SAL01_inputIMEIData.aspx','500','400');return false;
                                                            }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Settings ShowFooter="false" />
                        <SettingsDetail IsDetailGrid="true" />
                        <SettingsPager PageSize="5">
                        </SettingsPager>
                        <Templates>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                            </EmptyDataRow>
                        </Templates>
                    </cc:ASPxGridView>
                    <div class="seperate">
                    </div>
                </DetailRow>
                <EmptyDataRow>
                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                </EmptyDataRow>
            </Templates>
            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
        <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">
            <div class="SubEditCommand">
                <asp:Label ID="Label5" runat="server" Text="移撥單號:ST2101-100815001" ForeColor="White"></asp:Label>
            </div>
        </div>
    </div>
<%--    <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server" EnableViewState="False"
        PopupElementID="ChooseButton1" TargetElementID="TextBox2">
<ClientSideEvents Init="function(s, e) {
                    var iframe = s.GetContentIFrame();                   
                    iframe.popupArguments = {};
                    iframe.contentLoaded = false;
                    var controlCollection = ASPxClientControl.GetControlCollection();                
                    iframe.popupArguments.popupContainer = controlCollection.Get('storesPopup1');                                                                   
                    var targetElementId = 'TextBox2';                                                                                        
                    iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId) 
                        || document.getElementById(targetElementId);
                    }"></ClientSideEvents>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
--%></asp:Content>
