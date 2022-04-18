<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV01.aspx.cs" Inherits="VSS_INV01_INV01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="func">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            <!--總部移撥查詢作業-->
                            <asp:Literal ID="Literal1" runat="server" Text="總部移撥查詢作業"></asp:Literal>
                        </td>
                        <td align="right">
                            <%--<asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClientClick="document.location='INV02.aspx';return false;" />--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--移撥單號-->
                             <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--商品名稱-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox8" runat="server" Width="100px"></asp:TextBox>
                            <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx','500','400');return false;" />
                        </td>
                        <td class="tdtxt">
                            <!--移撥狀態-->
                             <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Text="-請選擇-"></asp:ListItem>
                                <asp:ListItem Text="在途"></asp:ListItem>
                                <asp:ListItem Text="已撥入"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--移出日期-->
                             <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                             <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="PostbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp; <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="PostbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        </td>
                        <td class="tdtxt">
                            <!--移出門市-->
                             <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx','500','400');return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--撥入日期-->
                             <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                             <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="PostbackDate_TextBox3" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp; <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="PostbackDate_TextBox4" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        </td>
                        <td class="tdtxt">
                            <!--撥入門市-->
                             <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                            <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx','500','400');return false;" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                AllowPaging="True" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging" OnRowCommand="gvMaster_RowCommand">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--移撥單號-->
                                             <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--狀態-->
                                             <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--移出門市-->
                                             <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--移出日期-->
                                             <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--撥入門市-->
                                             <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--撥入日期->
                                             <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--更新人員-->
                                             <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--更新日期-->
                                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="8" class="tdEmptyData">
                                            <!--查無資料，請修改條件，重新查詢-->
                                             <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferSlipNo %>">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="Label211" runat="server" Text='<%# Bind("移撥單號") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("移撥單號") %>' 
                                                CommandName="select"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
                                    <asp:BoundField DataField="移出門市" HeaderText="<%$ Resources:WebResources, TransferFrom %>" />
                                    <asp:BoundField DataField="移出日期" HeaderText="<%$ Resources:WebResources, TransferOutDate %>" />
                                    <asp:BoundField DataField="撥入門市" HeaderText="<%$ Resources:WebResources, TransferTo %>" />
                                    <asp:BoundField DataField="撥入日期" HeaderText="<%$ Resources:WebResources, TransferInDate %>" />
                                    <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" />
                                    <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                                </Columns>
                                <PagerTemplate>
                                    <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Page" CommandArgument="First"
                                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>"> <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/first.png" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>"> <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/previous.png" /></asp:LinkButton>
                                    第
                                    <asp:Label ID="lblCurrPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                                    <asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                                    <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>"> <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                                        Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>"> <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                                    到第
                                    <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                                    頁
                                    <asp:Button ID="btnGoToIndex" runat="server" Text="GO" OnClick="btnGoToIndex_Click" />
                                </PagerTemplate>
                            </asp:GridView>
                            

                <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false" >
                    <div class="SubEditCommand">
                        <asp:Label ID="Label5" runat="server" Text="移撥單號:ST2013-100712001" ForeColor="White"></asp:Label>
                    </div>
                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False"  visible="False" 
                        CssClass="mGrid" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                        OnRowUpdating="gvDetail_RowUpdating" AllowPaging="True" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging" OnRowCommand="gvMaster_RowCommand">
                        <EmptyDataTemplate>
                            <tr>
                                <td colspan="9" class="tdEmptyData">
                                    <!--此無明細資料-->
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                            <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ImeiControl %>">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox3" runat="server" Checked="True" Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="移出數量" HeaderText="<%$ Resources:WebResources, TransferredOutQuantity %>" ReadOnly="true"  />
                            <asp:BoundField DataField="移出IMEI" HeaderText="移出IMEI" ReadOnly="true"  />
                            <asp:BoundField DataField="撥入數量" HeaderText="<%$ Resources:WebResources, TransferredInQuantity %>" ReadOnly="true"  />
                            <asp:BoundField DataField="移入IMEI" HeaderText="移入IMEI" ReadOnly="true"  />
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
                        <asp:Label ID="lblCurrPage" runat="server" 
                            Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                        <asp:Label ID="lblPageCount" runat="server" 
                            Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                        <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                            
                            
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                            
                            
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                        到第
                        <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                        頁
                        <asp:Button ID="btnGoToIndex" runat="server" Text="GO" 
                            OnClick="btnGoToIndex_Click" />
                    </PagerTemplate>
                    </asp:GridView>
                </div>

                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
