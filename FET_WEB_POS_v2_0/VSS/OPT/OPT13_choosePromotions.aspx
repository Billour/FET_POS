<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT13_choosePromotions.aspx.cs" Inherits="VSS_OPT_OPT13_choosePromotions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>促銷代號選擇</title>
</head>
<body>
   <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
       
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            促銷代號：
                        </td>
                        <td nowrap>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">        
                        </td>
                        <td class="tdval">            
                        </td>
                        <td ><asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </div>
           
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="GridScrollBar" style="height:130px">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radioChoose" runat="server" GroupName="Choose" AutoPostBack="true"
                                            OnCheckedChanged="radioChoose_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="項次" HeaderText="項次" />
                                <asp:BoundField DataField="促銷代碼" HeaderText="促銷代碼" />
                                <asp:BoundField DataField="促銷名稱" HeaderText="促銷名稱" />
                                <asp:BoundField DataField="促銷類別" HeaderText="促銷類別" />
                                <asp:BoundField DataField="生效日期" HeaderText="生效日期" />
                                <asp:BoundField DataField="失效日期" HeaderText="失效日期" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="seperate">
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Button ID="btnCommit" runat="server" Text="確定" Visible="false" OnClientClick="window.close();return false;" />
                        <asp:Button ID="btnCalcel" runat="server" Text="取消" Visible="false" OnClientClick="window.close();return false;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
