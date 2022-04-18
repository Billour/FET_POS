<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PRE01.aspx.cs" Inherits="VSS_PRE01_PRE01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--預購作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderOperation %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--預購單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PreOrderSheetNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label9" runat="server"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <span style="color: Red">*</span><!--客戶身份證號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CustomerIdentifyNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="00-未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票號碼-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="lbInvoceNo" runat="server" />
                    </td>
                    <td class="tdtxt">
                        <span style="color: Red">*</span><!--客戶姓名-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--發票抬頭-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceTitle %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--維護人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <span style="color: Red">*</span><!--聯絡電話-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--啟用類型-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ActivationType %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>續約</asp:ListItem>
                            <asp:ListItem>MND</asp:ListItem>
                            <asp:ListItem>其他</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--e-Mail-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Email %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:TextBox ID="TextBox6" runat="server" Width="98%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="SubEditBlock">
            <div class="SubEditCommand">
                <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <div class="GridScrollBar" style="height: auto">
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--活動代號-->
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ActivityNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--活動名稱-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ActivityName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--預購金額-->
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, PreOrderAmount %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--數量-->
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Quantity %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--總價-->
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, TotalPrice %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--備註-->
                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6" class="tdEmptyData">
                                <!--請點選新增按鍵增加資料-->
                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="CheckAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckItem" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ActivityNo %>">
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("活動代號") %>' />
                                <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('PRE01_SelectActivity.aspx',400,200);return false;" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="活動名稱" HeaderText="<%$ Resources:WebResources, ActivityName %>" />
                        <asp:BoundField DataField="預購金額" HeaderText="<%$ Resources:WebResources, PreOrderAmount %>" />
                        <asp:BoundField DataField="數量" HeaderText="<%$ Resources:WebResources, Quantity %>" />
                        <asp:BoundField DataField="總價" HeaderText="<%$ Resources:WebResources, TotalPrice %>" />
                        <asp:BoundField DataField="備註" HeaderText="<%$ Resources:WebResources, RemarksLimitedTo50Chars %>" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="txt">
            <!--應收總金額-->
            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>"></asp:Literal>：<asp:Label ID="Label5" runat="server" Text=""></asp:Label></div>
        <div class="seperate">
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="btnCash" runat="server" Text="<%$ Resources:WebResources, Cash %>" OnClientClick="openwindow('../CheckOut/CheckOutCash.aspx',100,100);"
                                OnClick="Button3_Click" />
                            <asp:Button ID="btnCredit" runat="server" Text="<%$ Resources:WebResources, CreditCard %>" OnClientClick="openwindow('../CheckOut/CheckOutCredit.aspx',300,300);"
                                OnClick="btnCredit_Click" />
                            <asp:Button ID="btnOffLineCredit" runat="server" Text="<%$ Resources:WebResources, OffLineCreditCard %>" OnClientClick="openwindow('../CheckOut/CheckOutCreditUnline.aspx',300,200);" />
                            <asp:Button ID="btnVisaDebit" runat="server" Text="<%$ Resources:WebResources, BankCard %>" OnClientClick="openwindow('../CheckOut/CheckOutDebitCard.aspx',300,300);return false;" />
                            <asp:Button ID="btnPayDEL" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        </div>
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster2" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--付款方式-->
                                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--金額-->
                                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Amount %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--付款明細-->
                                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, PaymentStatement %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="tdEmptyData">
                                            <!--請點選按鍵增加資料-->
                                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckAll" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckItem" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="付款方式" HeaderText="<%$ Resources:WebResources, PaymentMethod %>" />
                                    <asp:BoundField DataField="金額" HeaderText="<%$ Resources:WebResources, Amount %>" />
                                    <asp:BoundField DataField="付款明細" HeaderText="<%$ Resources:WebResources, PaymentStatement %>" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="seperate">
                    </div>
                    <!--訂金金額-->
                    <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, DepositPaid %>"></asp:Literal>：<asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button6" runat="server" Text="<%$ Resources:WebResources, CheckOut %>" OnClick="Button6_Click" />
            <asp:Button ID="Button7" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
            <asp:Button ID="Button8" runat="server" Text="<%$ Resources:WebResources, CancelPreOrder %>" />
            <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, PrintPreOrderVoucher %>" />
        </div>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
