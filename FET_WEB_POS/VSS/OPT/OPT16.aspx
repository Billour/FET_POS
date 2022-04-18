<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT16.aspx.cs" Inherits="VSS_OPT16_OPT16" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 179px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--HappyGo兌點名單上傳-->
        <asp:Literal ID="Literal1" runat="server" Text="HappyGo兌點名單上傳"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td style="width:200px" align="right">
                        <!--*名單對應代碼：-->
                        <%--<asp:Literal ID="Literal2" runat="server" Text=""></asp:Literal>：--%>
                        HG活動代號：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px" align="right">
                        <!--檔案路徑：-->
                        <asp:Literal ID="Literal6" runat="server" Text="檔案路徑"></asp:Literal>：
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>                
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Import %>"
                OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
            <EmptyDataTemplate>           
                <tr>
                    <th scope="col">
                        <!--HG活動代號-->
                        <asp:Literal ID="Literal8" runat="server" Text="HG活動代號"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--HappyGo卡號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, HappyGoCardNo %>"></asp:Literal>
                    </th>
                </tr>
                <tr>
                    <td colspan="2" class="tdEmptyData">
                        目前無匯入資料
                    </td>
                </tr>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="HG活動代號" HeaderText="HG活動代號" />
                <asp:BoundField DataField="HappyGo卡號" HeaderText="<%$ Resources:WebResources, HappyGoCardNo %>" />
            </Columns>
        </asp:GridView>
        <div class="seperate">
        </div>
         <div class="btnPosition">
            <asp:Button ID="Button3" runat="server" Text="上傳確認" OnClientClick="window.close();return false;"/>
            <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;"/>
        </div>       
    </div>
    </form>
</body>
</html>
