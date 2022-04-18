<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV18_2.aspx.cs" Inherits="VSS_INV_INV18_2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>庫存調整原因輸入</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height: 192px">
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                      OnRowDataBound="gvMaster_RowDataBound"  PagerStyle-HorizontalAlign="Right"
                    OnPageIndexChanging="GridView_PageIndexChanging">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--原因-->
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Reason %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="1" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input id="Radio" type="radio" name="SameRadio" value='<%#Eval("原因") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="原因" HeaderText="<%$ Resources:WebResources, Reason %>" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClientClick="window.close();return false;" />
            <asp:Button ID="Button21" runat="server" Text="<%$ Resources:WebResources, Cancel %>"   OnClientClick="window.close();return false;" />
        </div>
    </div>
    </div>
    </form>
</body>
</html>
