<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChooseConsignVendor.aspx.cs" Inherits="VSS_Common_ChooseConsignVendor" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>寄銷廠商查詢</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
      <div class="criteria">
                <table>                    
                    <tr>
                        <td class="tdtxt">
                            廠商編號：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            廠商名稱：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        </td>                       
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="清空" />
            </div>
            <div class="seperate">
            </div>
            <div class="GridScrollBar" style="height: 214px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radioChoose" runat="server" GroupName="rbSameGroup" 
                                            AutoPostBack="True" oncheckedchanged="radioChoose_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="廠商編號" HeaderText="廠商編號" />
                                <asp:BoundField DataField="廠商名稱" HeaderText="廠商名稱" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="btnPosition"> <asp:Button ID="btnCommit" runat="server" Text="確定" Visible="false" OnClientClick="window.close();return false;" /></div>
    </div>
    </form>
</body>
</html>
