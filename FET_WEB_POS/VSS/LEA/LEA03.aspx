<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA03.aspx.cs" Inherits="VSS_LEA03_LEA03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }

        function ft() {
            document.location.href = "www.google.com.tw";
         //   location.href = 'www.kimo.com.tw';

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div class="titlef">
            <!--已租賃設備查詢-->            
           <asp:Literal ID="Literal11" runat="server" Text="已租賃設備查詢"></asp:Literal></div>
           
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--手機地點-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, MobileLocation %>"></asp:Literal>：
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
                        <!--客戶姓名-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--客戶門號-->
                         <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:CheckBox ID="CheckBox1" runat="server" Text="<%$ Resources:WebResources, ListTodaysReservations %>" />
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
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                OnRowDataBound="gvMaster_RowDataBound">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--項次-->
                                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal> 
                                        </th>
                                        <th scope="col">
                                            <!--預約日期-->
                                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ReservationDate %>"></asp:Literal> 
                                        </th>
                                        <th scope="col">
                                            <!--租賃單號-->
                                             <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, LeaseOrderNo %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--客戶姓名-->
                                             <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--客戶門號-->
                                             <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--性別-->
                                             <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Gender %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--手機地點-->
                                             <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, MobileLocation %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--預定領取日-->
                                             <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, CollectionDueDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--預約歸還日-->
                                             <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ReturnDueDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--狀態-->
                                             <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--更新日期-->
                                             <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--更新人員-->
                                             <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="12" class="tdEmptyData">
                                            <!--查無資料，請修改條件，重新查詢-->
                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="<%$ Resources:WebResources, Select %>" CausesValidation="False" Font-Underline="False" BorderStyle="Outset"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" />
                                    <asp:BoundField DataField="預約日期" HeaderText="<%$ Resources:WebResources, ReservationDate %>" />
                                    <asp:BoundField DataField="租賃單號" HeaderText="<%$ Resources:WebResources, LeaseOrderNo %>" />
                                    <asp:BoundField DataField="客戶姓名" HeaderText="<%$ Resources:WebResources, CustomerName %>" />
                                    <asp:BoundField DataField="客戶門號" HeaderText="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                                    <asp:BoundField DataField="性別" HeaderText="<%$ Resources:WebResources, Gender %>" />
                                    <asp:BoundField DataField="手機地點" HeaderText="<%$ Resources:WebResources, MobileLocation %>" />
                                    <asp:BoundField DataField="預定領取日" HeaderText="<%$ Resources:WebResources, CollectionDueDate %>" />
                                    <asp:BoundField DataField="預約歸還日" HeaderText="<%$ Resources:WebResources, ReturnDueDate %>" />
                                    <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
                                    <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
                                    <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
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
    </div>
    </form>
</body>
</html>
