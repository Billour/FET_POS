<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON08.aspx.cs" Inherits="VSS_CON08_Default" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" language="javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=123,left=280,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   

        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品主配作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductDistribution %>"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table id="Tab1" >
                <tr>
                    <td class="tdval">
                        <!--匯入檔案名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="匯入檔案路徑"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Import %>"
                        OnClick="Button1_Click" />
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                </tr>
            </table>        
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <dx:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="廠商代號" 
                Width="100%" AutoGenerateColumns="False" 
                EnableRowsCache="False" onhtmlrowprepared="gvMaster_HtmlRowPrepared" >
                    <SettingsText EmptyDataRow="目前無匯入資料" />
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="廠商代號" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>">
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="TextBox1" runat="server" Text='<%# Bind("廠商代號") %>' Width="50px"></dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Button10" runat="server" Text="選" SkinID="PopupButton" AutoPostBack="false" />
                                        </td>
                                    </tr>
                                </table>
                                <cc:ASPxPopupControl ID="consignmentVendorsPopup1" SkinID="ConsignmentVendorsPopup" runat="server"  
                                     EnableViewState="False" PopupElementID="Button10" TargetElementID="TextBox1" LoadingPanelID="lp1">                                     
                                 </cc:ASPxPopupControl>
                                 <dx:ASPxLoadingPanel ID="lp1" runat="server"></dx:ASPxLoadingPanel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="<%$ Resources:WebResources, StoreNo %>">
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="TextBox2" runat="server" Text='<%# Bind("門市編號") %>' Width="50px"></dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Button9" runat="server" Text="選" SkinID="PopupButton" AutoPostBack="false" />
                                        </td>
                                    </tr>
                                </table>
                                <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server"  
                                     EnableViewState="False" PopupElementID="Button9" TargetElementID="TextBox2" LoadingPanelID="lp2">                                     
                                 </cc:ASPxPopupControl>
                                 <dx:ASPxLoadingPanel ID="lp2" runat="server"></dx:ASPxLoadingPanel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="門市名稱" runat="server" Caption="<%$ Resources:WebResources, StoreName %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="TextBox3" runat="server" Text='<%# Bind("商品料號") %>' Width="80px"></dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="Button8" runat="server" Text="選" SkinID="PopupButton" AutoPostBack="false" />
                                        </td>
                                    </tr>
                                </table>
                                <cc:ASPxPopupControl ID="productsPopup1" SkinID="ProductsPopup" runat="server"  
                                     EnableViewState="False" PopupElementID="Button8" TargetElementID="TextBox3" LoadingPanelID="lp3">                                     
                                 </cc:ASPxPopupControl>
                                 <dx:ASPxLoadingPanel ID="lp3" runat="server"></dx:ASPxLoadingPanel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="實際訂購量" runat="server" Caption="<%$ Resources:WebResources, ActualOrderQuantity %>">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="TextBox4" runat="server" Text='<%# Bind("實際訂購量") %>' Width="80px"></dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="異常原因" runat="server" Caption="異常原因">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table align="center">
            <tr>
            <td><dx:ASPxButton ID="Button3" runat="server" Text="上傳確認" /></td>
            <td><dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>" /></td>
            </tr>
            </table>
        </div>   
</asp:Content>
