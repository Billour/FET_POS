<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV05_1.aspx.cs" Inherits="VSS_INV_INV05_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReasonCode %>"></asp:Literal></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td align="tdtxt">
                        <!--退倉原因代號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReasonCode %>"></asp:Literal>：
                    </td>
                    <td align="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td align="tdtxt">
                        <!--退倉原因-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ReasonForWarehousing %>"></asp:Literal>：
                    </td>
                    <td align="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td align="tdtxt">
                    </td>                    
                    <td align="tdval">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Search %>" />
                    </td>
                </tr>
            </table>
    
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
                                <!--退倉原因代號-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReasonCode %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--退倉原因-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ReasonForWarehousing %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="2" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <input id="Radio" type="radio" name="SameRadio" value='<%#Eval("退倉代號") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="退倉代號" HeaderText="<%$ Resources:WebResources, ReturnWarehousingReasonCode %>" />
                        <asp:BoundField DataField="退倉原因" HeaderText="<%$ Resources:WebResources, ReasonForWarehousing %>" />                        
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClientClick="window.close();return false;" />
            <asp:Button ID="Button21" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;" />
        </div>
    </div>
    </div>
    </form>
</body>
</html>
