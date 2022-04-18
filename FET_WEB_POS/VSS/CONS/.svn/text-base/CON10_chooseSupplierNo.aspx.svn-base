<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON10_chooseSupplierNo.aspx.cs" Inherits="VSS_CON10_chooseSupplierNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StoreSearch %>"></asp:Literal></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td class="tdtxt" height="20"></td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--廠商代號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                 </tr>
            </table>

        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>

        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                  OnRowDataBound="gvMaster_RowDataBound"  PagerStyle-HorizontalAlign="Right" AllowPaging="true"
                OnPageIndexChanging="GridView_PageIndexChanging" PageSize="5">
                <EmptyDataTemplate>
                    <tr>
                        <th>&nbsp;</th>
                        <th scope="col">
                            <!--廠商代號-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--廠商名稱-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td colspan="3" class="tdEmptyData">
                            <!--查無資料，請修改條件，重新查詢-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input id="Radio" type="radio" name="SameRadio" value='<%#Eval("廠商代號") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="廠商代號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" />
                    <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" />                        
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
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClientClick="window.close();return false;" />
            <asp:Button ID="Button21" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;" />
        </div>
    </div>
    </form>
</body>
</html>
