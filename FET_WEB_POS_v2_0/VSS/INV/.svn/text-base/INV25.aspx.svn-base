<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV25.aspx.cs" Inherits="VSS_INV25_INV25" %>

<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--移出作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferOut %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="LinkButton1" runat="server" Text="移出查詢作業" PostBackUrl="INV24.aspx">
                    </dx:ASPxButton>
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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td align="right">
                                    <dx:ASPxTextBox ID="TextBox14" runat="server" Width="100">
                                    </dx:ASPxTextBox>
                                </td>
                                <td align="left">
                                    <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label2" runat="server" Text="00 未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources,ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                    </td>
                    <td colspan="3" class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
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
                    <cc:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" KeyFieldName="商品料號"
                        OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating1"
                        Width="100%">
                        <SettingsEditing Mode="Inline" />
                        <Columns>
                            <dx:GridViewDataTextColumn VisibleIndex="0">
                                <DataItemTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" CssClass="DiscountItems" />
                                </DataItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckALL" runat="server" CssClass="DiscountItems" onclick="javascript:if(this.checked){$('.DiscountItems').checkCheckboxes();}else{$('.DiscountItems').unCheckCheckboxes();}" />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    &nbsp;
                                </EditItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                <HeaderTemplate>
                                    &nbsp;
                                </HeaderTemplate>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                                VisibleIndex="2">
                                <EditItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right">
                                                <dx:ASPxTextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>' Width="100px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td align="left">
                                                <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>">
                                                    <ClientSideEvents Click="function(s, e) {
	openwindow('../ORD/ORD01_searchProductNo.aspx',640,300);return false;
}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                                VisibleIndex="3">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI控管" Caption="<%$ Resources:WebResources, ImeiControl %>"
                                VisibleIndex="4">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" Enabled="false" ItemStyle-HorizontalAlign="Center" />
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="移出數量" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>"
                                VisibleIndex="5">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>"
                                VisibleIndex="6">
                                <EditItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" Text='0'></asp:Label>
                                            </td>
                                            <td align="left">
                                                <dx:ASPxButton ID="Button10" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                    Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Templates>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Label>
                            </EmptyDataRow>
                            <TitlePanel>
                                <table cellpadding="0" cellspacing="0" border="0" align="left">
                                    <tr>
                                        <td align="right">
                                            <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                OnClick="btnAddNew_Click" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                        </td>
                                    </tr>
                                </table>
                            </TitlePanel>
                        </Templates>
                        <Settings ShowTitlePanel="true" />
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
                            <dx:ASPxButton ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, PrintTransferOutSlip %>" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server" EnableViewState="False"
        PopupElementID="Button2" TargetElementID="TextBox14">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
</asp:Content>
