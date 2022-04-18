<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON10.aspx.cs" Inherits="VSS_CON10_Default" %>

<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=250,left=330,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }       
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   

    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品退倉設定作業(總部)-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentReturnWarehousingSettings %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button6" runat="server" 
                            Text="<%$ Resources:WebResources, QueryEdit %>" 
                            Width="70px" onclick="Button6_Click" >
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="lblOrderNo" runat="server" Text=""></dx:ASPxLabel>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開單日期-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxDateEdit ID="ReceiptDate" runat="server"></dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label1" runat="server" Text="未存檔"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉開始日-->
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxDateEdit ID="ReturnWarehousingStartDate" runat="server"></dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉結束日-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxDateEdit ID="ReturnWarehousingEndDate" runat="server"></dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--更新日期-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label3" runat="server" Text="2010/07/01 22:00"></dx:ASPxLabel>
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
                        <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxLabel ID="Label4" runat="server" Text="12345 王大寶"></dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="seperate"></div>

    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
        <TabPages>
            <dx:TabPage Text="<%$ Resources:WebResources, Product %>">
                <ContentCollection>
                    <dx:ContentControl>
                        <div >
                            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="廠商代號" 
                            Width="100%" AutoGenerateColumns="False" 
                            EnableRowsCache="False" onrowinserting="gvMaster_RowInserting" 
                                onrowupdating="gvMaster_RowUpdating" >
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
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
                                    <dx:GridViewDataTextColumn FieldName="廠商代號" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>">
                                        <EditItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txtSupplierNo" runat="server" Text='<%# Eval("[廠商代號]") %>'></dx:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnChooseSupplierNo" runat="server" Text="<%$ Resources:WebResources, Choose %>" ClientSideEvents-Click="function(s,e){openwindow('../CONS/CON10_chooseSupplierNo.aspx',500,400);return false;}" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>" ReadOnly="True">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>    
                                        <CellStyle HorizontalAlign="Left">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                                        <EditItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txtProductCode" runat="server" Text='<%# Eval("[商品料號]") %>'></dx:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnChooseProductCode" runat="server" Text="<%$ Resources:WebResources, Choose %>" ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>" ReadOnly="True">
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
                                                <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnNew_Click" />
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
                                                    <dx:ASPxLabel ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Nonselect %>"></dx:ASPxLabel>
                                                </td>
                                                <td class="tdcen">
                                                </td>
                                                <td class="tdcen">
                                                    <!--已選擇-->
                                                    <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Selected %>"></dx:ASPxLabel>
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
    </dx:ASPxPageControl>

    <div class="seperate"></div>
    <div class="btnPosition">
        <table align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="Button10" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
                </td>
                <td>
                    <dx:ASPxButton ID="Button11" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                </td>
                <td>
                    <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" />
                    <asp:HiddenField ID="HiddenField1" runat="server"  OnValueChanged="HiddenField1_ValueChanged" />
                    <uc1:PopupWindow ID="PopupWindow1" runat="server"
                                    Name="Import" 
                                    PopupButtonID="btnImport" 
                                    TargetControlID="HiddenField1"                                    
                                    Width="400" Height="400"  
                                    OnOkScript="onOk"                     
                                    NavigateUrl="~/VSS/INV/INV05_Import.aspx" />
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
