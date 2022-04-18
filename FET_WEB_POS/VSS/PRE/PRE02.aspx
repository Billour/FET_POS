<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PRE02.aspx.cs" Inherits="VSS_PRE02_PRE02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url) {
            window.open(url, "window");
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div class="titlef">
            <!--預購查詢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderSearch %>"></asp:Literal>
         </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval" colspan="2">
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged"
                            Text="跨門市查詢(限單筆)" />
                    </td>
                   
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--門市代號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                    </td>
                     <td class="tdtxt">
                        <!--預購單號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, PreOrderSheetNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                   <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem Selected="True">預購</asp:ListItem>
                            <asp:ListItem>預購作廢</asp:ListItem>
                            <asp:ListItem>已結帳</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                
                    <td class="tdtxt">
                        <!--活動代號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ActivityNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--活動名稱-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ActivityName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>可輸入自動尋找</asp:ListItem>
                            <asp:ListItem>HTC Desire預購</asp:ListItem>
                            <asp:ListItem>iPhone4預購</asp:ListItem>
                            <asp:ListItem>iPad預購</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                   
                    <td class="tdtxt">
                        <!--客戶姓名-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--預購日期-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PreOrderDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                  
                    
                </tr>
                <tr>
                      <td class="tdtxt">
                        <!--統一編號-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                     <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                     <td class="tdtxt">
                        <!--客戶身份證號-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CustomerIdentifyNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>             
                </tr>
                <tr>
                   <td class="tdtxt">
                        <!--發票號碼-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                      <td class="tdtxt">
                        <!--聯絡電話-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                  
                   <td class="tdtxt">
                        <!--銷售人員-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>12345-王大寶</asp:ListItem>
                            <asp:ListItem>22345-王小寶</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                   
                </tr>
            </table>
            <div class="btnPosition">
                <asp:Button ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnQuery_Click" />
                <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <div class="GridScrollBar" style="height: auto">
                            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                Visible="False">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--項次-->
                                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--狀態-->
                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--預購日期-->
                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, PreOrderDate %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--預購單號-->
                                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, PreOrderSheetNo %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--客戶姓名-->
                                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--客戶門號-->
                                            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--聯絡電話-->
                                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>
                                        </th>                                        
                                        <th scope="col">
                                            <!--活動名稱-->
                                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, ActivityName %>"></asp:Literal>
                                        </th>
                                         <th scope="col">
                                            <!--預購金額-->
                                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, PreOrderAmount %>"></asp:Literal>
                                        </th>                                       
                                        <th scope="col">
                                            <!--門市代號-->
                                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--銷售人員-->
                                            <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="12" class="tdEmptyData">
                                            <!--查無資料，請修改條件，重新查詢-->
                                            <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <input id="Radio" type="radio" name="SameRadio" value='<%#Eval("項次") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" />
                                    <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
                                    <asp:BoundField DataField="預購日期" HeaderText="<%$ Resources:WebResources, PreOrderDate %>" />
                                    <asp:BoundField DataField="預購單號" HeaderText="<%$ Resources:WebResources, PreOrderSheetNo %>" />
                                    <asp:BoundField DataField="客戶姓名" HeaderText="<%$ Resources:WebResources, CustomerName %>" />
                                    <asp:BoundField DataField="客戶門號" HeaderText="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                                    <asp:BoundField DataField="聯絡電話" HeaderText="<%$ Resources:WebResources, ContactTelephone %>" />
                                    <asp:BoundField DataField="活動名稱" HeaderText="<%$ Resources:WebResources, ActivityName %>" />
                                    <asp:BoundField DataField="預購金額" HeaderText="<%$ Resources:WebResources, PreOrderAmount %>" />                                    
                                    <asp:BoundField DataField="門市代號" HeaderText="<%$ Resources:WebResources, StoreNo %>" />
                                    <asp:BoundField DataField="銷售人員" HeaderText="<%$ Resources:WebResources, SalesClerk %>" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="btnPosition">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, View %>" Visible="false" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
