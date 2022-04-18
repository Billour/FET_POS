<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV26.aspx.cs" Inherits="VSS_INV_INV26" %>

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
                    <!--撥入作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferInSearch %>"></asp:Literal>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
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
                        <!--撥入日期-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="1">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td align="left">
                                    <div style="width:120px;">
                                        <dx:ASPxDateEdit ID="postbackDate_TextBox1" runat="server">
                                            <ValidationSettings CausesValidation="false">                                                
                                                <RequiredField IsRequired="true" />                                                       
                                            </ValidationSettings>                                                
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td align="left">
                                    <div style="width:120px;">
                                        <dx:ASPxDateEdit ID="postbackDate_TextBox2" runat="server">
                                            <ValidationSettings CausesValidation="false">                                                
                                                <RequiredField IsRequired="true" />                                                       
                                            </ValidationSettings>                                                
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市名稱-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"  />
                        <%--<table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td align="right">
                                    <dx:ASPxTextBox ID="TextBox2" runat="server" Width="100">
                                    </dx:ASPxTextBox>
                                </td>
                                <td align="left">
                                    <dx:ASPxButton ID="ChooseButton1" runat="server" SkinID="PopupButton">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td align="right">
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                    KeyFieldName="移撥單號" OnHtmlRowCreated="gvMaster_HtmlRowCreated1" 
                    onhtmlrowprepared="gvMaster_HtmlRowPrepared">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="移撥單號" Caption="<%$ Resources:WebResources, TransferSlipNo %>"
                            VisibleIndex="0">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="移出門市" Caption="<%$ Resources:WebResources, TransferFrom %>"
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
                                    <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                        VisibleIndex="0" />
                                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                        VisibleIndex="1" />
                                    <dx:GridViewDataColumn FieldName="移出數量" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>"
                                        VisibleIndex="2" />
                                    <%--<dx:GridViewDataColumn FieldName="IMEI控管" Caption="<%$ Resources:WebResources, ImeiControl %>"
                                        VisibleIndex="3">
                                        <DataItemTemplate>
                                            <asp:HiddenField ID="hidIMEI" runat="server" Value='<%# Bind("IMEI控管") %>' />
                                            <asp:CheckBox ID="CheckBox3" runat="server" Enabled="false" /></DataItemTemplate>
                                    </dx:GridViewDataColumn>--%>
                                    <dx:GridViewDataColumn FieldName="撥入數量" Caption="<%$ Resources:WebResources, TransferredInQuantity %>"
                                        VisibleIndex="3">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="TextBox2" runat="server" Text='<%# Eval("撥入數量") %>' Width="100px">
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="" VisibleIndex="4">
                                        <DataItemTemplate>
                                            <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                            </dx:ASPxImage>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>"
                                        VisibleIndex="5">
                                        <DataItemTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Bind("IMEI") %>' Font-Underline="True"></asp:Label>
                                                    </td>
                                                    
                                                   <td>
                                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px" Text="1234566">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                         
                                                        <dx:ASPxButton ID="Button10" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                            Enabled="false" AutoPostBack="false">
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
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                            </cc:ASPxGridView>
                            <div class="seperate">
                            </div>
                            <div class="btnPosition">
                                <table cellpadding="0" cellspacing="0" border="0" align="center">
                                    <tr>
                                        <td align="right">
                                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfirmTransferIn %>" CausesValidation="false" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Button6" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
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
                <div runat="server" id="DIVdetail" visible="false">
                    <div class="SubEditCommand">
                        <asp:Label ID="Label5" runat="server" Text="移撥單號:ST2101-100815001" ForeColor="White"></asp:Label>
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="btnPosition">
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfirmTransferIn %>" />
                        <dx:ASPxButton ID="Button6" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--<cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server" EnableViewState="False"
        PopupElementID="ChooseButton1" TargetElementID="TextBox2">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>--%>
</asp:Content>
