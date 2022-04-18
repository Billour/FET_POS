<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT06_1.aspx.cs" Inherits="VSS_OPT_OPT06_1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>門市選擇</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
       <div class="func">
       
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            門市代碼：
                        </td>
                        <td nowrap>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            門市名稱：
                        </td>
                        <td >
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        </td>
                        <td ><asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </div>
           
            <div class="seperate">
            </div>
           
                    <div class="GridScrollBar" style="height:180px">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radioChoose" runat="server" GroupName="Choose" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="門市代碼" HeaderText="門市代碼" />
                                <asp:BoundField DataField="門市名稱" HeaderText="門市名稱" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="seperate">
                    </div>
                    
            
            <div class="seperate">
            </div>
            <div class="btnPosition">
                        <asp:Button ID="btnCommit" runat="server" Text="確定" Visible="false" 
                            OnClientClick="window.close();" />
                        <asp:Button ID="btnCalcel" runat="server" Text="取消" Visible="false" OnClientClick="window.close();return false;" />
                   
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
