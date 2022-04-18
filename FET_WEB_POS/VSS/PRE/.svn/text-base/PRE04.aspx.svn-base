<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PRE04.aspx.cs" Inherits="VSS_PRE_PRE04" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <div class="func">
        <div class="titlef">
            預購活動查詢作業
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        活動代號：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        活動名稱：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                      狀態：
                    </td>
                    <td class="tdval">
                       <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem>已生效</asp:ListItem>
                            <asp:ListItem>尚未生效</asp:ListItem>
                            <asp:ListItem>已過期</asp:ListItem>
                       </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        有效期間：
                    </td>
                    <td class="tdval" colspan="3">
                      起<cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
&nbsp;訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        
                    </td>
                    <td class="tdval">
                       
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        
                        訂金：</td>
                    <td class="tdval">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>不需要</asp:ListItem>
                            <asp:ListItem>需要</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="tdtxt">
                        最低預購訂金：</td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        
                    </td>
                    <td class="tdval">
                       
                    </td>
                </tr>
                </table>
        </div>
        <div class="btnPosition"> <asp:Button ID="Button1" runat="server" Text="查詢" OnClick="Button1_Click" />
                            <asp:Button ID="Button2" runat="server" Text="清空" /></div>
        <div class="seperate">
        </div>
        <div>
            <div class="SubEditBlock">
                <div class="GridScrollBar" style="height: auto">
               
                    <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                   狀態
                                </th>
                                <th scope="col">
                                    活動代號
                                </th>
                                <th scope="col">
                                    活動名稱
                                </th>
                                <th scope="col">
                                    有效期間(起)
                                </th>
                                <th scope="col">
                                    有效期間(訖)
                                </th>
                            </tr>
                            <tr>
                                <td colspan="6" class="tdEmptyData">
                                    <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="狀態" HeaderText="狀態" />
                            <asp:BoundField DataField="活動代號" HeaderText="活動代號" />
                            <asp:BoundField DataField="活動名稱" HeaderText="活動名稱" />
                            <asp:BoundField DataField="有效期間起始" HeaderText="有效期間(起)" />
                            <asp:BoundField DataField="有效期間結束" HeaderText="有效期間(訖)" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        
    </div>
    </form>
</body>
</html>
