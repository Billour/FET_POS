<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL02_searchDiscountNo.aspx.cs"
    Inherits="VSS_SAL_SAL02_searchDiscountNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>促銷代碼查詢</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(function() {
            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
            $("input:radio").attr("name", "SameRadio");
        });
    </script>

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
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            促銷代碼：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            促銷名稱：
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
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
                                        <asp:RadioButton ID="radioChoose" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="促銷代碼" HeaderText="促銷代碼" />
                                <asp:BoundField DataField="促銷名稱" HeaderText="促銷名稱" />
                                <asp:BoundField DataField="開始日期" HeaderText="開始日期" DataFormatString="{0:yyyy/MM/dd}" />
                                <asp:BoundField DataField="結束日期" HeaderText="結束日期" DataFormatString="{0:yyyy/MM/dd}" />
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
                        <asp:Button ID="btnCommit" runat="server" Text="確定" Visible="false" OnClientClick="window.close();return false;" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消" Visible="false" OnClientClick="window.close();return false;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
