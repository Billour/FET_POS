<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV02.aspx.cs" Inherits="VSS_INV02_INV02" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeicheckbox(con) {
            if (con.checked) {
                openwindow("SAL01_inputIMEIData.aspx");
            }
        }

        function checkID() {
            var vID = document.getElementById("tbInvoiceNo").value;
            if (vID.length != 8 && vID > 0) {
                openwindow("SAL01_checkIDNumber.aspx", 300, 200);
                return false;
            }
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
                        <!--移撥作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Transferring %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClientClick="document.location='INV01.aspx';return false;" />
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
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
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
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label2" runat="server" Text="暫存"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移出門市-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lbMOutStore" runat="server" Text="2103 永和"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--指定撥入門市-->
                         <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx');return false;" />
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移出日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="label222" runat="server" Text="2010/07/01 22:00"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--撥入日期-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="label223" runat="server" Text="2010/07/01 22:00"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div class="SubEditBlock">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnNew_Click" />
                        <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                    </div>
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--移出數量-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, TransferredOutQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--撥入數量-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, TransferredInQuantity %>"></asp:Literal>
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
                                <tr>
                                    <td colspan="6" class="tdEmptyData">
                                        <!--請點選新增按鍵增加資料-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("商品編號") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("商品編號") %>'></asp:Label>
                                        <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_searchProductNo.aspx',300,300);return false;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferredOutQuantity %>">
                                    <ItemTemplate>
                                       <asp:TextBox ID="txtMoveOut" Text='<%#Bind("移出數量") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="撥入數量" HeaderText="<%$ Resources:WebResources, TransferredInQuantity %>" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ImeiControl %>">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox3" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, Imei %>">
                                    <ItemTemplate>
                                        <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_inputIMEIData.aspx');return false;" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" />
            <asp:Button ID="Button6" runat="server" Text="<%$ Resources:WebResources, CancelTransfer %>" />
            <asp:Button ID="Button61" runat="server" Text="<%$ Resources:WebResources, ConfrimTransferOut %>" />
            <asp:Button ID="Button62" runat="server" Text="<%$ Resources:WebResources, ConfrimTransferIn %>" />
            <asp:Button ID="Button7" runat="server" Text="<%$ Resources:WebResources, PrintTransferSlip %>" />
        </div>
    </div>
    </form>
</body>
</html>
