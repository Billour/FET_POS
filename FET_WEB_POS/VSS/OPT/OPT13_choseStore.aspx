<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT13_choseStore.aspx.cs" Inherits="VSS_OPT_OPT13_choseStore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>選擇門市</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript" />

    <script type="text/javascript" language="javascript">
        $(function() {
            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
            $("input:radio").attr("name", "SameRadio");
        });
    </script>

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
                        <td class="tdtxt">
                            區域別：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox2" runat="server" Width="100"></asp:TextBox>
                        </td>                          
                    </tr>                   
                    <tr>
                        <td class="tdtxt">
                            門市編號：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox1" runat="server" Width="100"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            門市名稱：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="TextBox6" runat="server" Width="100"></asp:TextBox>
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
                        <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--門市編號-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市名稱-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--區域別-->
                                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>
                                    </th>      
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="4" class="tdEmptyData">
                                       <%-- <!--請點選新增按鍵增加資料-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>--%>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radioChoose" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />
                                <asp:BoundField DataField="區域別" HeaderText="<%$ Resources:WebResources, ByDistrict %>" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>