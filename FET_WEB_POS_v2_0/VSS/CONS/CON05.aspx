<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON05.aspx.cs" Inherits="VSS_CON05_CON05" %>

<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=230,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

//        function imeicheckbox(con) {
//            if (con.checked) {
//                openwindow("SAL01_inputIMEIData.aspx");
//            }
//        }

        function checkID() {
            var vID = document.getElementById("tbInvoiceNo").value;
            if (vID.length != 8 && vID > 0) {
                openwindow("SAL01_checkIDNumber.aspx", 300, 200);
                return false;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品訂單查詢-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentOrderSearch %>"></asp:Literal>
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
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單日期-->
                         <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxDateEdit ID="dxOrderDate" runat="server"></dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單編號-->
                         <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox5" runat="server"></dx:ASPxTextBox>
                    </td>
                     <td class="tdtxt" nowrap="nowrap">
                        <!--訂單狀態-->
                         <dx:ASPxLabel ID="Literal4" runat="server" Text="訂單狀態"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="已存檔" />
                                <dx:ListEditItem Text="轉單中" />
                                <dx:ListEditItem Text="已成單" />
                                <dx:ListEditItem Text="待進貨" />
                                <dx:ListEditItem Text="已驗收" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                   
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品料號-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table>
                            <tr>
                                <td><dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="Button5" runat="server" Text="<%$ Resources:WebResources, Choose %>" SkinID="PopupButton" AutoPostBack="false" /></td>
                            </tr>
                        </table>
                    </td>
                   
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <dx:ASPxLabel ID="Literal23" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="2" nowrap="nowrap">
                        <dx:ASPxLabel ID="Literal24" runat="server" Text="21031 門市名稱1"></dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>
        
        <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server"  
             EnableViewState="False" PopupElementID="Button5" TargetElementID="TextBox1" LoadingPanelID="lp1">                
         </cc:ASPxPopupControl>
         <dx:ASPxLoadingPanel ID="lp1" runat="server"></dx:ASPxLoadingPanel>

        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="訂單編號"
                Width="100%" AutoGenerateColumns="False"
                EnableRowsCache="False" 
                    ondetailrowexpandedchanged="gvMaster_DetailRowExpandedChanged" 
                    onhtmldatacellprepared="gvMaster_HtmlDataCellPrepared">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="訂單日期" Caption="<%$ Resources:WebResources, OrderDate %>" />
                        <dx:GridViewDataHyperLinkColumn FieldName="訂單編號" Caption="<%$ Resources:WebResources, OrderNo %>" />
                        <dx:GridViewDataTextColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>" />
                        <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                        <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <Templates>
                        <DetailRow>
                                 <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                     Width="100%" EnableRowsCache="true">                                                  
                                    <Columns>                                    
                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" />
                                        <dx:GridViewDataTextColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>" />
                                        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="商品名稱" />
                                        <dx:GridViewDataTextColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>" />
                                        <dx:GridViewDataTextColumn FieldName="建議訂購量" Caption="<%$ Resources:WebResources, RecommendedOrderQuantity %>" />
                                        <dx:GridViewDataTextColumn FieldName="實際訂購量" Caption="<%$ Resources:WebResources, ActualOrderQuantity %>" />
                                        <dx:GridViewDataTextColumn FieldName="核准數量" Caption="<%$ Resources:WebResources, ApprovedQuantity %>" />
                                        <dx:GridViewDataTextColumn FieldName="驗收量" Caption="<%$ Resources:WebResources, InspectionQuantity %>" />                     
                                    </Columns>
                                    <Settings ShowFooter="false" />                                 
                                    <SettingsDetail IsDetailGrid="true" />
                                    <SettingsPager PageSize="5"></SettingsPager> 
                                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />                               
                                    <Templates>
                                        <TitlePanel>
                                            訂單編號：<asp:Label ID="Label5" runat="server" Text="101900074" ></asp:Label>
                                        </TitlePanel>                                
                                    </Templates>
                                    <Styles>
                                        <TitlePanel Font-Size="Small" HorizontalAlign="Left"></TitlePanel>
                                    </Styles>
                                </cc:ASPxGridView>
                        </DetailRow>                 
                    </Templates>
                    <SettingsPager PageSize="5" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                </cc:ASPxGridView>
                <div class="seperate">
                </div>
            </ContentTemplate>
           
        </asp:UpdatePanel>
    </div>
</asp:Content>