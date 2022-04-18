<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV25.aspx.cs" Inherits="VSS_INV25_INV25" %>

<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--移出作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferOut %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="LinkButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                            PostBackUrl="INV24.aspx"></asp:Button>
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
                            <asp:TextBox ID="TextBox14" runat="server" Width="100"></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                OnClientClick="openwindow('../INV/INV18_3.aspx',640,300);return false;" />
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
            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>--%>
            <div class="SubEditBlock">
                <div class="SubEditCommand">
                    <asp:Button ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                        OnClick="btnAddNew_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                </div>
                <div id="Div1" runat="server" class="SubEditBlock" visible="true">
                    <div class="GridScrollBar">
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False">
                            <EmptyDataTemplate>
                                <tr>
                                    <th>
                                    </th>
                                    <th>
                                    </th>
                                    <th scope="col">
                                        <!--商品料號-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--移出數量-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TransferredOutQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--IMEI控管-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ImeiControl %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--IMEI-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Imei %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="7" class="tdEmptyData">
                                        <!--請點選新增按鍵增加資料-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" CssClass="class1" onclick="javascript:if(this.checked){$('.class1').checkCheckboxes();}else{$('.class1').unCheckCheckboxes();}" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server" CssClass="class1" Enabled="true" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ControlStyle-Width="40px"
                                    FooterStyle-Width="40px" FooterStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>"
                                            CommandName="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                            CommandName="Update" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            CommandName="Cancel" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                            CommandName="Save" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            OnClick="btnCancel_Click" />
                                    </FooterTemplate>
                                    <ControlStyle Width="40px"></ControlStyle>
                                    <FooterStyle Wrap="True" Width="40px"></FooterStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>'></asp:TextBox>
                                        <asp:Button ID="Button2" runat="server" Text="選" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,300);return false;" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("商品料號") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品料號") %>'></asp:TextBox>
                                        <asp:Button ID="Button2" runat="server" Text="選" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',640,300);return false;" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>"
                                    ReadOnly="true" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferredOutQuantity %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("移出數量") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("移出數量") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("移出數量") %>'></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ImeiControl %>">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" ItemStyle-HorizontalAlign="Center" />
                                        <asp:HiddenField ID="hidIMEI1" runat="server" Value='<%# Bind("IMEI控管") %>' />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" Enabled="true" ItemStyle-HorizontalAlign="Center" />
                                        <asp:HiddenField ID="hidIMEI2" runat="server" Value='<%# Bind("IMEI控管") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" Enabled="false" ItemStyle-HorizontalAlign="Center" />
                                        <asp:HiddenField ID="hidIMEI2" runat="server" Value='<%# Bind("IMEI控管") %>' />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, Imei %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("IMEI") %>'></asp:TextBox>
                                        <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            OnClientClick="openwindow('../SAL/SAL01_inputIMEIData.aspx','500','400');return false;" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("IMEI") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%--<asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("IMEI") %>'></asp:TextBox>--%>
                                        <asp:Label ID="Label12" runat="server" Text='0'></asp:Label>
                                        <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            OnClientClick="openwindow('../SAL/SAL01_inputIMEIData.aspx','500','400');return false;" Enabled="false" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </cc1:ExGridView>
                    </div>
                </div>
                <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAddNew" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>--%>
                <div class="seperate">
                </div>
                <div class="btnPosition">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfirmTransferOut %>"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, PrintTransferOutSlip %>" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
