﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV26.aspx.cs" Inherits="VSS_INV26_INV26" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
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
    </script>

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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <div id="tooltip">
    </div>
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--撥入作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferInSearch %>"></asp:Literal>
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
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem Selected="True">在途</asp:ListItem>
                            <asp:ListItem>巳撥入</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--撥入日期-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>：
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>：
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--移出門市-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server" Width="100"></asp:TextBox>
                        <asp:Button ID="ChooseButton1" runat="server" Text='<%$ Resources:WebResources, Choose %>' />
                        <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="DeliveryOrderNoSearch" PopupButtonID="ChooseButton1"
                            TargetControlID="TextBox2" Width="450" Height="400" NavigateUrl="~/VSS/SAL/SAL01_chooseStore.aspx" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="mGrid" PageSize="8" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                    OnRowCommand="gvMaster_RowCommand" OnRowDataBound="gvMaster_RowDataBound">
                    <PagerStyle HorizontalAlign="Right" />
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--移撥單號-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--移出門市-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--移出日期-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--撥入日期-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--移撥狀態-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--更新人員-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--更新日期-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="7" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferSlipNo %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("移撥單號") %>' CommandName="select"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="移出門市" HeaderText="<%$ Resources:WebResources, TransferFrom %>" />
                        <asp:BoundField DataField="移出日期" HeaderText="<%$ Resources:WebResources, TransferOutDate %>" />
                        <asp:BoundField DataField="撥入日期" HeaderText="<%$ Resources:WebResources, TransferInDate %>" />
                        <asp:BoundField DataField="移撥狀態" HeaderText="<%$ Resources:WebResources, TransferStatus %>" />
                        <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" />
                        <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <PagerTemplate>
                        <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Page" CommandArgument="First"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/first.png" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/previous.png" /></asp:LinkButton>
                        第
                        <asp:Label ID="lblCurrPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                        <asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                        <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                        到第
                        <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                        頁
                        <asp:Button ID="btnGoToIndex" runat="server" Text="GO" OnClick="btnGoToIndex_Click" />
                    </PagerTemplate>
                </asp:GridView>
                <div class="seperate">
                </div>
                <div runat="server" id="DIVdetail" visible="false">
                    <div class="SubEditCommand">
                        <asp:Label ID="Label5" runat="server" Text="移撥單號:ST2101-100815001" ForeColor="White"></asp:Label>
                    </div>
                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        Visible="False" OnRowDataBound="gvDetail_RowDataBound" AllowPaging="true" PageSize="5"
                        PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    <!--商品料號-->
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品名稱-->
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--移出數量-->
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, TransferredOutQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--撥入數量-->
                                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, TransferredInQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--IMEI控管->
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ImeiControl %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--IMEI-->
                                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Imei %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="6" class="tdEmptyData">
                                    <!--此無明細資料-->
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                            <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                            <asp:BoundField DataField="移出數量" HeaderText="<%$ Resources:WebResources, TransferredOutQuantity %>" />
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferredInQuantity %>">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("撥入數量") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ImeiControl %>">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox3" runat="server" Enabled="false" />
                                    <asp:HiddenField ID="hidIMEI" runat="server" Value='<%# Bind("IMEI控管") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, Imei %>" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="Label34" runat="server" Text='<%# Bind("IMEI") %>' Font-Underline="True"></asp:Label>
                                    <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        OnClientClick="openwindow('../SAL/SAL01_inputIMEIData.aspx','500','400');return false;"
                                        Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                            <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Page" CommandArgument="First"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/first.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="lbtnPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/previous.png" /></asp:LinkButton>
                            第
                            <asp:Label ID="lblCurrPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                            <asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                            <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                            <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                                Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                            到第
                            <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                            頁
                            <asp:Button ID="btnGoToIndex" runat="server" Text="GO" OnClick="btnGoToIndex_Click" />
                        </PagerTemplate>
                    </asp:GridView>
                    <div class="seperate">
                    </div>
                    <div class="btnPosition">
                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfirmTransferIn %>" />
                        <asp:Button ID="Button6" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
