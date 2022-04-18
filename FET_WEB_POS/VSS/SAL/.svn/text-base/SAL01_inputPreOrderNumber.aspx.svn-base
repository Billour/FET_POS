<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_inputPreOrderNumber.aspx.cs"
    Inherits="VSS_SAL_SAL01_inputPreOrderNumber" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>預購單號查詢</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div class="criteria">
         <div class="seperate">
        </div>
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--預購單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderNo %>" />：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt"><!--客戶身分證號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, CustomersIdentityNumber %>" />：</td>
                    <td class="tdval"><asp:TextBox ID="tbID" runat="server" /></td>
                    <td class="tdtxt"><!--客戶門號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />：</td>
                    <td class="tdval"><asp:TextBox ID="tbMSISDN" runat="server" /></td>                
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div> <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <div class="GridScrollBar" style="height :114px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radioChoose" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="預購單號" HeaderText="<%$ Resources:WebResources, PreOrderNo %>" />
                                <asp:BoundField DataField="客戶身分證號" HeaderText="<%$ Resources:WebResources, CustomersIdentityNumber %>" />
                                <asp:BoundField DataField="客戶姓名" HeaderText="<%$ Resources:WebResources, CustomerName %>" />
                                <asp:BoundField DataField="客戶門號" HeaderText="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                                <asp:BoundField DataField="預購金額" HeaderText="<%$ Resources:WebResources, PreOrderAmount %>" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="seperate">
            </div>
        <div class="btnPosition">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Button ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClientClick="window.close();return false;" />
                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
