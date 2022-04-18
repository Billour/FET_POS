<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD10.aspx.cs" Inherits="VSS_ORD_ORD10" Title="權重佔比分配" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "_blank", 'width=' + width + ',height=' + height + ',top=250,left=380,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');
        }                
    </script>
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
                        <!--權重佔比分配-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, WeightRatingAssignment %>"></asp:Literal>
                    </td>
                    <td align="right">
                        
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--區域-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem Value="1">北一區</asp:ListItem>
                                <asp:ListItem Value="2">中一區</asp:ListItem>
                                <asp:ListItem Value="3">南一區</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="txtStoreNo" runat="server"></asp:TextBox><asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx',550,350);return false;" />
                        </td>
                        
                    </tr>                    
                </table>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate"></div>
           <asp:Panel ID="Panel1" runat="server" >
            <div id="divContent" runat="server" class="SubEditBlock">
                <div class="SubEditCommand">
                    <asp:Button ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" />                           
                    <asp:Button ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <uc1:PopupWindow ID="PopupWindow1" runat="server"
                                    Name="Import" 
                                    PopupButtonID="btnImport" 
                                    TargetControlID="HiddenField1"                                    
                                    Width="400" Height="400"                       
                                    NavigateUrl="~/VSS/ORD/ORD10_Import.aspx" />
                </div>
                <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"                            
                    PagerStyle-HorizontalAlign="Right" 
                     OnRowDataBound="gvMaster_RowDataBound" ShowFooter="true">
                    <PagerStyle HorizontalAlign="Right" />
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--項次-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--區域-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--門市代號-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--門市名稱-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--比率-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Ratio %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="5" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>   
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, Items %>">
                            <ItemTemplate><%# Eval("項次") %></ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, District %>">
                            <ItemTemplate><%# Eval("區域") %></ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>">
                            <ItemTemplate><%# Eval("門市代號")%></ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreName %>">
                            <ItemTemplate><%# Eval("門市名稱")%></ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, Ratio %>" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate><%# Eval("比率")%></ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </asp:TemplateField>                                                       
                    </Columns>
                                       
                </asp:GridView>                        
            </div>
            <div class="seperate"></div>
            <div id="Div03" style="width:90%;  text-align:right;" visible="false" >
                <!--比率統計-->
                <asp:Literal ID="Literal01" runat="server" Text="比率統計"></asp:Literal>
                <asp:Literal ID="Literal02" runat="server" Text="："></asp:Literal>
                <asp:Literal ID="Literal03" runat="server" Text="100%"></asp:Literal>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <asp:Button ID="OkButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" />
                <asp:Button ID="CancelButton" runat="server" Text="<%$ Resources:WebResources, Cancel %>"/>
            </div>
            </asp:Panel>        
           
        </div>
    </div>
    </form>
</body>
</html>
