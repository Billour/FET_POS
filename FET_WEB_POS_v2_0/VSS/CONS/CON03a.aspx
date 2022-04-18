<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON03a.aspx.cs" Inherits="VSS_CON03a_CON03a" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
   </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="titlef" align="left">
        <!--寄銷商品查詢作業(門市)-->
        <asp:Literal ID="Literal1" runat="server" Text="寄銷商品查詢作業(門市)"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="AC1" />
                                <dx:ListEditItem Text="AC2" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal24" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:120px">
                            <tr>
                                <td><dx:ASPxTextBox ID="TextBox4" runat="server" Width="100"></dx:ASPxTextBox></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>" AutoPostBack="false" SkinID="PopupButton" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--上架日期-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupportStartDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td><dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="SupportStartDateFrom" runat="server"></dx:ASPxDateEdit></td>
                                <td><dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="SupportStartDateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--下架日期-->
                        <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SupportExpiryDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td><dx:ASPxLabel ID="Literal25" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="SupportExpiryDateFrom" runat="server"></dx:ASPxDateEdit></td>
                                <td><dx:ASPxLabel ID="Literal26" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="SupportExpiryDateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--商品類別-->
                        <dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlProductCategory" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Text="3G Handset" />
                                <dx:ListEditItem Text="SIM Card" />
                                <dx:ListEditItem Text="3G Accessory" />
                                <dx:ListEditItem Text="On Line Recharge" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--停止訂購日-->
                        <dx:ASPxLabel ID="Literal12" runat="server" Text="停止訂購日"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td><dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="dateFrom" runat="server"></dx:ASPxDateEdit></td>
                                <td><dx:ASPxLabel ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td><dx:ASPxDateEdit ID="dateTo" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
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
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
                Width="100%" AutoGenerateColumns="False"
                EnableRowsCache="False">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="廠商編號" Caption="<%$ Resources:WebResources, SupplierNo %>" />
                        <dx:GridViewDataTextColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>" />
                        <dx:GridViewDataTextColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>" />
                        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
                        <dx:GridViewDataTextColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>" />
                        <dx:GridViewDataTextColumn FieldName="上架日期" Caption="<%$ Resources:WebResources, SupportStartDate %>" />
                        <dx:GridViewDataTextColumn FieldName="下架日期" Caption="<%$ Resources:WebResources, SupportExpiryDate %>" />
                        <dx:GridViewDataTextColumn FieldName="停止訂購日" Caption="停止訂購日" />
                        <dx:GridViewDataTextColumn FieldName="人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                        <dx:GridViewDataTextColumn FieldName="日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <SettingsPager PageSize="10" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                </cc:ASPxGridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    
    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server"  
         PopupElementID="btnChooseProduct" TargetElementID="TextBox4" LoadingPanelID="lp">                
     </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp"></dx:ASPxLoadingPanel>
</asp:Content>