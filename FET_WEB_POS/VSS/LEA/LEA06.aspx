<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA06.aspx.cs" Inherits="VSS_LEA06_LEA06" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="func">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="titlef">
            <!--租賃收費明細表查詢-->
            <asp:Literal ID="Literal11" runat="server" Text="租賃收費明細表查詢"></asp:Literal></div>
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
                        <!--庫存地點-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem> ALL</asp:ListItem>
                            <asp:ListItem>台北館前店</asp:ListItem>
                            <asp:ListItem>台北遠企店</asp:ListItem>
                            <asp:ListItem>台北天母店</asp:ListItem>
                            <asp:ListItem>台中忠明店</asp:ListItem>
                            <asp:ListItem>台中美村店</asp:ListItem>
                            <asp:ListItem>台南台南店</asp:ListItem>
                            <asp:ListItem>高雄林森店</asp:ListItem>
                            <asp:ListItem>高雄三多店</asp:ListItem>
                            <asp:ListItem>高雄成功店</asp:ListItem>
                            <asp:ListItem>機場1</asp:ListItem>
                            <asp:ListItem>機場2</asp:ListItem>
                            <asp:ListItem>客服</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--出國時間-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, DurationAbroad %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                         <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                            &nbsp; <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
           
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="SubEditBlock">
                 <div class="SubEditCommand">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                    </div>
                    <div class="GridScrollBar" style="height: auto">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowDataBound="gvMaster_RowDataBound">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--手機地點-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, MobileLocation %>" />
                                    </th>
                                    <th scope="col">
                                        <!--客戶姓名-->
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CustomerName %>" />
                                    </th>
                                    <th scope="col">
                                        <!--客戶門號-->
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                                    </th>
                                    <th scope="col">
                                        <!--預定領取日-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CollectionDueDate %>" />
                                    </th>
                                    <th scope="col">
                                        <!--預約歸還日-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnDueDate %>" />
                                    </th>
                                    <th scope="col">
                                        <!--租金-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Rent %>" />
                                    </th>
                                    <th scope="col">
                                        <!--賠償金-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Compensation %>" />
                                    </th>
                                    <th scope="col">
                                        <!--賠償原因-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ReasonForCompensation %>" />
                                    </th>
                                    <th scope="col">
                                        <!--折扣金額-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, DiscountAmount %>" />
                                    </th>
                                    <th scope="col">
                                        <!--總金額-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, TotalAmount %>" />
                                    </th>
                                    <th scope="col">
                                        <!--備註-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Remark %>" />
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="11" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                         <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>" />
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="No" />
                                <asp:BoundField DataField="手機地點" HeaderText="<%$ Resources:WebResources, MobileLocation %>" />
                                <asp:BoundField DataField="客戶姓名" HeaderText="<%$ Resources:WebResources, CustomerName %>" />
                                <asp:BoundField DataField="客戶門號" HeaderText="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                                <asp:BoundField DataField="預定領取日" HeaderText="<%$ Resources:WebResources, CollectionDueDate %>" />
                                <asp:BoundField DataField="預約歸還日" HeaderText="<%$ Resources:WebResources, ReturnDueDate %>" />
                                <asp:BoundField DataField="租金" HeaderText="<%$ Resources:WebResources, Rent %>" />
                                <asp:BoundField DataField="賠償金" HeaderText="<%$ Resources:WebResources, Compensation %>" />
                                <asp:BoundField DataField="賠償原因" HeaderText="<%$ Resources:WebResources, ReasonForCompensation %>" />
                                <asp:BoundField DataField="折扣金額" HeaderText="<%$ Resources:WebResources, DiscountAmount %>" />
                                <asp:BoundField DataField="總金額" HeaderText="<%$ Resources:WebResources, TotalAmount %>" />
                                <asp:BoundField DataField="備註" HeaderText="<%$ Resources:WebResources, Remark %>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="seperate">
    </div>
    </form>
</body>
</html>
