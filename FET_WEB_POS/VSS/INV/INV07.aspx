<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV07.aspx.cs" Inherits="VSS_INV07_INV07" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        var mx;
        var my;
        jQuery(document).mousemove(function(event) {
            mx = event.pageX;
            my = event.pageY;
        });

        function show(content) {
            var tip = $("#tooltip");
            tip.html(content);
            tip.css({
                display: "",
                left: mx - 150,
                top: my,
                position: "absolute",
                background: "#FFFFFF"
            });

        }
        function hide() {
            var tip = $("#tooltip");
            tip.css("display", "none");

        }

        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeiclick(con) {
            openwindow("../SAL/SAL01_inputIMEIData.aspx", 720, 300);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="tooltip">
    </div>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--退倉作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousing %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClientClick="document.location='INV06.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="lbOrderNo" runat="server" Text="HR100801001"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdtxt">
                        <!--退倉日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, WarehousedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="2010/08/18"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label2" runat="server" Text="未完成"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--退倉開始日-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text="2010/08/10"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--退倉原因-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReasonForWarehousing %>"></asp:Literal>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label6" runat="server" Text="Product Return "></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--退倉結束日-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label7" runat="server" Text="2010/08/28"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--退倉處理-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingProcess %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label8" runat="server" Text="回收for其他單位出貨"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height: auto">
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                    OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                    OnRowUpdating="gvMaster_RowUpdating" OnRowDataBound="gvMaster_RowDataBound">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--項次-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <asp:Literal runat="server" ID="liProductCode" Text="<%$ Resources:WebResources, ProductCode %>" />
                            </th>
                            <th scope="col">
                                <asp:Literal runat="server" ID="liProductName" Text="<%$ Resources:WebResources, ProductName %>" />
                            </th>
                            <th scope="col">
                                <!--IMEI控管-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--未拆封數量-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SealedQuantity %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--已拆封數量-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, OpenedQuantity %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--退倉數量-->
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ReturnQuantity %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--IMEI-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Imei %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--ERP驗退日期-->
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ErpRejectedDate %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--ERP驗退單號-->
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ErpRejectionNoteNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--驗退數量-->
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, RejectedQuantity %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="11" class="tdEmptyData">
                                <!--此無明細資料-->
                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" />
                        <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                        <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ImeiControl %>" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked='<%#Eval("IMEI控管").ToString() == "1" ? true:false %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="帳上庫存量" HeaderText="帳上庫存量" ReadOnly="true" />
                        <asp:TemplateField HeaderText="未拆封數量" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("未拆封數量") %>' Width="40%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="已拆封數量" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("已拆封數量") %>' Width="40%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="退倉數量" HeaderText="退倉數量" ReadOnly="true" />
                        <asp:TemplateField HeaderText="IMEI" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("IMEI") %>' Font-Underline="True"></asp:Label>
                                <asp:Button ID="Button2" runat="server" Text="選" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="差異量" HeaderText="差異量" ReadOnly="true" />
                        <asp:BoundField DataField="ERP驗退日期" HeaderText="ERP驗退日期" ReadOnly="True" />
                        <asp:BoundField DataField="ERP驗退單號" HeaderText="ERP驗退單號" ReadOnly="true" />
                        <asp:BoundField DataField="驗退數量" HeaderText="驗退數量" ReadOnly="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
            <asp:Button ID="Button2" runat="server" Text="清空" />
            <asp:Button ID="Button6" runat="server" Text="列印" />
        </div>
    </div>
    </form>
</body>
</html>
