<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CHK06.aspx.cs" Inherits="VSS_CHK_CHK06" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--總部對帳作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="總部對帳作業"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table id="Tab1" >
                <tr>
                    <td align="right">
                        <!--對帳日期區間-->
                        <asp:Literal ID="Literal11" runat="server" Text="對帳日期區間"></asp:Literal>：
                    </td>
                    <td colspan="3">
                         <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" Width="100" />
                            &nbsp;<asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" Width="100" />                            
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <!--合庫入帳檔案路徑-->
                        <asp:Literal ID="Literal4" runat="server" Text="合庫入帳檔案路徑"></asp:Literal>：
                    </td>
                    <td colspan="4">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--NCCC信用卡入帳檔案路徑-->
                        <asp:Literal ID="Literal5" runat="server" Text="NCCC信用卡入帳檔案路徑"></asp:Literal>：
                    </td>
                    <td colspan="4">
                        <asp:FileUpload ID="FileUpload2" runat="server" Width="60%" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Import %>"
                OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditCommand">
                <asp:Label ID="Label1" runat="server" Text="總額比對:" ForeColor="White"></asp:Label>
        </div>
        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid" onrowdatabound="gvMaster_RowDataBound">
            <EmptyDataTemplate>
                <tr>
                    <th scope="col">
                        <!--對帳日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="對帳日期"></asp:Literal>
                    </th>
                    <th scope="col" >
                        <!--合庫入帳-->
                        <asp:Literal ID="Literal7" runat="server" Text="合庫入帳"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--POS現金帳-->
                        <asp:Literal ID="Litera8" runat="server" Text="POS現金帳"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--NCCC信用卡入帳-->
                        <asp:Literal ID="Litera9" runat="server" Text="NCCC信用卡入帳"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--POS信用卡帳-->
                        <asp:Literal ID="Literal0" runat="server" Text="POS信用卡帳"></asp:Literal>
                    </th>
                    <th scope="col">
                        <!--異常原因-->
                        <asp:Literal ID="Literal1" runat="server" Text="異常原因"></asp:Literal>
                    </th>                 
                </tr>
                <tr>
                    <td colspan="6" class="tdEmptyData">
                        目前無匯入資料
                    </td>
                </tr>
            </EmptyDataTemplate>
            <Columns>
                    <asp:BoundField DataField="對帳日期" HeaderText="對帳日期"  />
                    <asp:BoundField DataField="合庫入帳" HeaderText="合庫入帳" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="POS現金帳" HeaderText="POS現金帳" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="NCCC信用卡入帳" HeaderText="NCCC信用卡入帳" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="POS信用卡帳" HeaderText="POS信用卡帳" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="異常原因" HeaderText="異常原因" />
            </Columns>
        </asp:GridView>
         <div class="seperate">
        </div>
        <%--<div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">--%>

            <div class="SubEditCommand" >
                <asp:Label ID="Label2" runat="server" Text="異常明細清單:" ForeColor="White" Visible="false"></asp:Label>
            </div>
            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid" onrowdatabound="gvDetail_RowDataBound" >
                <EmptyDataTemplate>
                    <tr>
                        <td colspan="8" class="tdEmptyData">
                            <!--此無明細資料-->
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="對帳日期" HeaderText="對帳日期" />
                    <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                    <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />     
                    <asp:BoundField DataField="合庫入帳" HeaderText="合庫入帳" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="POS現金帳" HeaderText="POS現金帳" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="NCCC信用卡入帳" HeaderText="NCCC信用卡入帳" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField DataField="POS信用卡帳" HeaderText="POS信用卡帳" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="異常原因" HeaderText="異常原因" />
                </Columns>
             </asp:GridView>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="Button3" runat="server" Text="資料儲存" OnClientClick="window.close();return false;" />
            <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                OnClientClick="window.close();return false;" />
        </div>

    </div>
    </form>
</body>
</html>

