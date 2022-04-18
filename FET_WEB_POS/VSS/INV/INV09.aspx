<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV09.aspx.cs" Inherits="VSS_INV09_INV09" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <!--進貨驗收作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReceivingInspection %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClientClick="document.location='INV08.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--門市名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label1" runat="server" Text="2104 天母"></asp:Label>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label2" runat="server" Text="00 未存檔"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--驗收單編號-->
                             <asp:Literal ID="Literal4" runat="server" Text="驗收單編號"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label7" runat="server" Text="SR2104-1007001 "></asp:Label>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
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
                            <!--PO/OE_NO-->
                            <asp:Literal ID="Literal6" runat="server" Text="PO/OE_NO"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label5" runat="server" Text="001-1"></asp:Label>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                            <!--更新人員-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--訂單/主配編號-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label6" runat="server" Text="HR1007002 "></asp:Label>
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
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                CssClass="mGrid" OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                OnRowUpdating="gvMaster_RowUpdating" OnRowDataBound="gvMaster_RowDataBound">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--商品編號-->
                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--商品名稱-->
                                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--IMEI檢核-->
                                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, VerifyImei %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--到貨量-->
                                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ArrivalQuantity %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--驗收量-->
                                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, InspectionQuantity %>"></asp:Literal>                                            
                                        </th>
                                        <th scope="col">
                                            <!--IMEI-->
                                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Imei %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--在途量-->
                                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, OnOrderQuantity %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--供貨商-->
                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Supplier %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="11" class="tdEmptyData">
                                            <!--此無明細資料-->
                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                                    <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="IMEI檢核">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked='<%#Eval("IMEI檢核").ToString() == "1" ? true:false %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="到貨量" HeaderText="<%$ Resources:WebResources, ArrivalQuantity %>" ReadOnly="true" />                                    
                                    <asp:TemplateField HeaderText="驗收量" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("驗收量") %>' Width="40%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IMEI">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("IMEI") %>' Font-Underline="True" ></asp:Label>
                                            <asp:Button ID="Button2" runat="server" Text="選" Visible="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="在途量" HeaderText="在途量" ReadOnly="True" />
                                    <asp:BoundField DataField="供貨商" HeaderText="供貨商" ReadOnly="true" ItemStyle-Font-Underline="true">
                                        <ItemStyle Font-Underline="True" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="儲存" OnClick="btnSave_Click" />
                <asp:Button ID="btnClear" runat="server" Text="清空" />
                <asp:Button ID="Button1" runat="server" Text="條碼列印" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
