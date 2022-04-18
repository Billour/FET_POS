<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON10.aspx.cs" Inherits="VSS_CONS_CON10" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=250,left=330,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品退倉設定作業(總部)-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSettings %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:aspxbutton id="Button6" runat="server" text="<%$ Resources:WebResources, QueryEdit %>"
                            width="70px" onclick="Button6_Click">
                        </dx:aspxbutton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <dx:aspxlabel id="Literal2" runat="server" text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxlabel id="lblOrderNo" runat="server" text="">
                        </dx:aspxlabel>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開單日期-->
                        <dx:aspxlabel id="Literal4" runat="server" text="<%$ Resources:WebResources, ReceiptDate %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxdateedit id="ReceiptDate" runat="server">
                        </dx:aspxdateedit>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <dx:aspxlabel id="Literal3" runat="server" text="<%$ Resources:WebResources, Status %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxlabel id="Label1" runat="server" text="未存檔">
                        </dx:aspxlabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉開始日-->
                        <dx:aspxlabel id="Literal6" runat="server" text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxdateedit id="ReturnWarehousingStartDate" runat="server">
                        </dx:aspxdateedit>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉結束日-->
                        <dx:aspxlabel id="Literal7" runat="server" text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxdateedit id="ReturnWarehousingEndDate" runat="server">
                        </dx:aspxdateedit>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新日期-->
                        <dx:aspxlabel id="Literal5" runat="server" text="<%$ Resources:WebResources, ModifiedDate %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxlabel id="Label3" runat="server" text="2010/07/01 22:00">
                        </dx:aspxlabel>
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
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新人員-->
                        <dx:aspxlabel id="Literal8" runat="server" text="<%$ Resources:WebResources, ModifiedBy %>">
                        </dx:aspxlabel>
                        ：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:aspxlabel id="Label4" runat="server" text="12345 王大寶">
                        </dx:aspxlabel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="seperate">
    </div>
    <dx:aspxpagecontrol id="ASPxPageControl1" runat="server" width="100%">
        <TabPages>
            <dx:TabPage Text="<%$ Resources:WebResources, Product %>">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="廠商代號"
                                Width="100%" AutoGenerateColumns="False" EnableRowsCache="False" OnRowInserting="gvMaster_RowInserting"
                                OnRowUpdating="gvMaster_RowUpdating" Settings-ShowTitlePanel="true">
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                        <HeaderTemplate>
                                            <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                                        <EditButton Visible="True">
                                        </EditButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" VisibleIndex="2" Caption="<%$ Resources:WebResources, ProductCode %>" >
                                        <HeaderCaptionTemplate>
                                            <span style="color: Red">*</span><dx:ASPxLabel ID="Literal71" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" >
                                            </dx:ASPxLabel>
                                        </HeaderCaptionTemplate>
                                        <EditItemTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txtProductCode" runat="server" Text='<%# Eval("[商品料號]") %>' Width="100px">
                                                            <ValidationSettings CausesValidation="false">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnChooseProductCode" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                            ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);}"
                                                            AutoPostBack="false" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="廠商代號" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>"
                                        VisibleIndex="4">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>"
                                        ReadOnly="True" VisibleIndex="5">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"
                                        ReadOnly="True" VisibleIndex="3">
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
                                        <table align="left">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                        OnClick="btnNew_Click" />
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnDel" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                </Templates>
                                <SettingsPager PageSize="5" />
                                <SettingsEditing Mode="Inline" />
                            </cc:ASPxGridView>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="<%$ Resources:WebResources, Store %>">
                <ContentCollection>
                    <dx:ContentControl>
                        <div>
                            <table>
                                <tr>
                                    <td class="tdval">
                                        <table>
                                            <tr>
                                                <td class="tdcen">
                                                    <!--未選擇-->
                                                    <dx:ASPxLabel ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Nonselect %>">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="tdcen">
                                                </td>
                                                <td class="tdcen">
                                                    <!--已選擇-->
                                                    <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Selected %>">
                                                    </dx:ASPxLabel>
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
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:aspxpagecontrol>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table align="center">
            <tr>
                <td>
                    <dx:aspxbutton id="Button10" runat="server" text="<%$ Resources:WebResources, Save %>" 
                        onclick="btnSave_Click" />
                </td>
                <td>
                    <dx:aspxbutton id="Button11" runat="server" text="<%$ Resources:WebResources, Cancel %>" />
                </td>
                <td>
                    <dx:aspxbutton id="btnImport" runat="server" text="<%$ Resources:WebResources, Import %>"  />
                    <asp:HiddenField ID="HiddenField1" runat="server" OnValueChanged="HiddenField1_ValueChanged" />
                    <uc1:popupwindow id="PopupWindow1" runat="server" name="Import" popupbuttonid="btnImport"
                        targetcontrolid="HiddenField1" width="400" height="400" onokscript="onOk" navigateurl="~/VSS/INV/INV05_Import.aspx" />

                    <script type="text/javascript">
                        function onOk() {
                            __doPostBack('<%= HiddenField1.UniqueID %>', '');
                        }
                    </script>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
