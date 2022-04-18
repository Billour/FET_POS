<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON20.aspx.cs" Inherits="VSS_CON20_CON20" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=200,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   

        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品移出作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferOut %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferOutSearch %>" ClientSideEvents-Click="function(){document.location='CON19.aspx'}" AutoPostBack="False" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
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
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="lbOrderNo" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdtxt">
                        <!--移出門市-->
                        <dx:ASPxLabel ID="Literal16" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text="2103 永和"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                         <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Label ID="Label2" runat="server" Text="未存檔"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">  
                        <!--撥入門市-->
                        <dx:ASPxLabel ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0" align="center" style="width:120px">
                            <tr>
                                <td><dx:ASPxTextBox ID="TextBox2" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxButton ID="Button3" runat="server" SkinID="PopupButton" AutoPostBack="false" /></td>
                            </tr>
                        </table>
                        <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server"  
                             EnableViewState="False" PopupElementID="Button3" TargetElementID="TextBox2" LoadingPanelID="lp1">         
                         </cc:ASPxPopupControl>
                         <dx:ASPxLoadingPanel ID="lp1" runat="server"></dx:ASPxLoadingPanel>
                    </td>
                    <td class="tdtxt">          
                    </td>
                    <td class="tdval">      
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="10/07/01 22:00"></asp:Label>
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
                        <!--更新人員-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>        
        <div  id="Div1" runat="server" class="SubEditBlock" visible="true" >           
        <dx:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號" 
            Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
            EnableRowsCache="False" onrowinserting="gvMaster_RowInserting" 
                onrowupdating="gvMaster_RowUpdating">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                        <HeaderTemplate>
                            <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                        <EditButton Visible="True">
                        </EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="商品類別" runat="server" Caption="<%$ Resources:WebResources, ProductCategory %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="商品料號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="移出數量" runat="server" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>"></dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                        <table align="left">
                            <tr>
                                <td><dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" /></td>
                                <td><dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" /></td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <SettingsPager PageSize="5" />
                <SettingsEditing Mode="Inline" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            </dx:ASPxGridView>           
        </div>         
        <div class="seperate"></div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td><dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="Button62" runat="server" Text="<%$ Resources:WebResources, Cancel %>" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="Button7" runat="server" Text="<%$ Resources:WebResources, PrintTransferSlip %>" /></td>
                </tr>
            </table>
        </div>
</asp:Content>