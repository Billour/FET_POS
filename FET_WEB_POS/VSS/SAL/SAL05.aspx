<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL05.aspx.cs" Inherits="VSS_SAL_SAL05" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div class="titlef">
            <!--交易未結清單-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, UnclearedTradeListing %>" />
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--申請日期-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ApplicationDate %>" />：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>" /><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                                ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>" /><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀 態-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>未結帳</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--服務屬性-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ServiceNature %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:DropDownList ID="DropDownList3" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>-請選擇-</asp:ListItem>
                                <asp:ListItem>IA</asp:ListItem>
                                <asp:ListItem>Loyalty</asp:ListItem>
                                <asp:ListItem>SSI</asp:ListItem>
                                <asp:ListItem>HRS</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--服務類別-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ServiceClass %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:DropDownList ID="DropDownList4" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>-請選擇-</asp:ListItem>
                                <asp:ListItem>全球卡</asp:ListItem>
                                <asp:ListItem>換補卡</asp:ListItem>
                                <asp:ListItem>2轉3</asp:ListItem>
                                <asp:ListItem>新啟用</asp:ListItem>
                                <asp:ListItem>續約</asp:ListItem>
                                <asp:ListItem>代收</asp:ListItem>
                                <asp:ListItem>維修</asp:ListItem>
                                <asp:ListItem>網購</asp:ListItem>
                                <asp:ListItem>預購</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--銷售人員-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ServiceClass %>" />：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="ddlWidthFormat">
                                <asp:ListItem>王大明</asp:ListItem>
                            </asp:DropDownList>                        
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="divContent" runat="server"  class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, ConsolidatedCheckout %>" OnClientClick="document.location='SAL01.aspx';return false;" />
                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, CancelTransaction %>" />
                        </div>
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnSelectedIndexChanged="gvMaster_SelectedIndexChanged" PageSize="5" AllowPaging="True"
                            PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">&nbsp;</th>
                                    <th scope="col" nowrap="nowrap">&nbsp;</th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--項次-->
                                        <asp:Literal ID="Literal71" runat="server" Text="<%$ Resources:WebResources, Items %>" />
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Status %>" />
                                    </th>
                                     <th scope="col" nowrap="nowrap">
                                        <!--申請日期-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ApplicationDate %>" />
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--服務屬性-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ServiceNature %>" />
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--服務類別-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ServiceClass %>" />
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--應收總金額-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AmountReceivable %>" />
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--客戶門號-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--銷售人員-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />                                        
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="10" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>" />                                        
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" CssClass="Class1" onclick="javascript:if(this.checked){$('.Class1').checkCheckboxes();}else{$('.Class1').unCheckCheckboxes();}"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server" CssClass="Class1" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="false" ShowEditButton="false" ShowSelectButton="true" ButtonType="Button" />
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="申請日期" HeaderText="<%$ Resources:WebResources, ApplicationDate %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="服務屬性" HeaderText="<%$ Resources:WebResources, ServiceNature %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="服務類別" HeaderText="<%$ Resources:WebResources, ServiceClass %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="應收總金額" HeaderText="<%$ Resources:WebResources, AmountReceivable %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="客戶門號" HeaderText="<%$ Resources:WebResources, CustomerMobileNumber %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="銷售人員" HeaderText="<%$ Resources:WebResources, SalesClerk %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                            </Columns>
                            <PagerTemplate>
                                <asp:LinkButton ID="lbtnFirst" runat="server" CommandName="Page" CommandArgument="First"
                                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/first.png" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="lbtnPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/previous.png" /></asp:LinkButton>
                                第
                                <asp:Label ID="lblCurrPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                                <asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                                <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                                <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                                到第
                                <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                                頁
                                <asp:Button ID="btnGoToIndex" runat="server" Text="GO" OnClick="btnGoToIndex_Click" />
                            </PagerTemplate>
                        </asp:GridView>
                        <div class="seperate">
                        </div>
                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            Visible="false">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col" nowrap="nowrap">
                                        <!--項次-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Items %>" /> 
                                    </th>                                    
                                    <th scope="col" nowrap="nowrap">
                                        <!--促銷代號-->
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" /> 
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--促銷名稱-->
                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" /> 
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--商品料號-->
                                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" /> 
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ProductName %>" /> 
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--卡片序號(SIM)-->
                                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, SimCardSerialNumber %>" /> 
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--金額-->
                                        <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, Amount %>" /> 
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="6" class="tdEmptyData">
                                        <!--此無明細資料-->
                                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>" /> 
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="促銷代號" HeaderText="<%$ Resources:WebResources, PromotionCode %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="促銷名稱" HeaderText="<%$ Resources:WebResources, PromotionName %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="卡片序號(SIM)" HeaderText="<%$ Resources:WebResources, SimCardSerialNumber %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                <asp:BoundField DataField="金額" HeaderText="<%$ Resources:WebResources, Amount %>" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
