<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV05.aspx.cs" Inherits="VSS_INV05_INV05" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="func">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--退倉設定作業-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingSettings %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e){ document.location='INV04.aspx'; }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉單號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbOrderNo" runat="server" Text="HR100914001">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--開單日期-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label2" runat="server" Text="00 未存檔">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉開始日-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉結束日-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label3" runat="server" Text="10/07/12 15:00">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉原因代號-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReasonCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="100px">
                                <Items>
                                    <dx:ListEditItem Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Text="退倉原因代號" />
                                    <dx:ListEditItem Text="退倉原因代號" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--後續處理代號-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PostProcessCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="100px">
                                <Items>
                                    <dx:ListEditItem Text="請選擇" Selected="true" />
                                    <dx:ListEditItem Text="後續處理代號1" />
                                    <dx:ListEditItem Text="後續處理代號2" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label4" runat="server" Text="64591 李家駿">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="seperate">
        </div>
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
            <TabPages>
                <dx:TabPage Text="商品">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
                                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
                                    Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                                    OnPageIndexChanged="gvMaster_PageIndexChanged" AutoGenerateColumns="False" OnRowUpdating="gvMaster_RowUpdating"
                                    OnRowInserting="gvMaster_RowInserting" Settings-ShowTitlePanel="true">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                            <EditButton Visible="true" Text="">
                                            </EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                                            VisibleIndex="2">
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="ProductCOde" runat="server" Width="68px" Text='<%# BIND("[商品料號]") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="chooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                SkinID="PopupButton">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                                    PopupElementID="chooseButton1" TargetElementID="ProductCOde">
                                                </cc:ASPxPopupControl>
                                            </EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>"
                                            VisibleIndex="4">
                                            <PropertiesTextEdit>
                                                <ReadOnlyStyle>
                                                    <Border BorderStyle="None" />
                                                </ReadOnlyStyle>
                                            </PropertiesTextEdit>
                                            <CellStyle HorizontalAlign="Left">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                            OnClick="btnAddNew_Click" />
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                        <EmptyDataRow>
                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                        </EmptyDataRow>
                                    </Templates>
                                    <SettingsEditing Mode="Inline" />
                                    <SettingsPager PageSize="5">
                                    </SettingsPager>
                                </cc:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="門市">
                    <ContentCollection>
                        <dx:ContentControl>
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td class="tdval">
                                                <table>
                                                    <tr>
                                                        <td class="tdcen">
                                                            <!--未選擇-->
                                                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></asp:Literal>
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                        <td class="tdcen">
                                                            <!--已選擇-->
                                                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Selected %>"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdcen">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlSubZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                        <td class="tdcen">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdListBox" rowspan="5">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ListBox ID="ListBox1" runat="server" Height="327px" SelectionMode="Multiple"
                                                                        Width="259px"></asp:ListBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td class="tdBtn">
                                                        </td>
                                                        <td rowspan="5" class="tdListBox">
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ListBox ID="ListBox2" runat="server" Height="327px" SelectionMode="Multiple"
                                                                        Width="259px"></asp:ListBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/next.png" OnClick="btnAdd_Click" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Images/previous.png" OnClick="btnBack_Click" /></ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdBtn">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
        <br />
        <br />
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Save %>"
                            OnClick="btnSave_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                            <ClientSideEvents Click="function(s, e){ document.location='INV05_Import.aspx'; }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            <%-- <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
            OnClick="btnSave_Click" />
        <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        <asp:Button ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" />
        <asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />
        <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="Import" PopupButtonID="btnImport"
            TargetControlID="HiddenField1" Width="400" Height="400" OnOkScript="onOk" NavigateUrl="~/VSS/INV/INV05_Import.aspx" />

        <script type="text/javascript">
            function onOk() {
                __doPostBack('<%= HiddenField1.UniqueID %>', '');
            }
        </script>--%>
        </div>
</asp:Content>
