<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV28.aspx.cs" Inherits="VSS_INV_INV28" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeicheckbox(con) {
            if (con.checked) {
                openwindow("SAL01_inputIMEIData.aspx");
            }
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
                        <!--門市拆封IMEI設定-->
                        <asp:Literal ID="Literal1" runat="server" Text="門市拆封IMEI設定"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    
                    <tr>
                        <td class="tdtxt">
                            <!--拆封日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OpenedDate %>"></asp:Literal>：
                        </td>
                        <td class="" >
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg"
                                Text="2010/07/01" />
                            &nbsp;<asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg"
                                Text="2010/07/01" />
                        </td>                                           
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--商品料號-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="" colspan="3">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                        <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_searchProductNo.aspx',640,300);return false;" />
                            &nbsp;<asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                        <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Choose %>" 
                                OnClientClick="openwindow('../SAL/SAL01_searchProductNo.aspx',640,300);return false;" />
                        </td>                        
                        
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>                    
                    <div class="SubEditBlock">
                        <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="mGrid" PageSize="8" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                            OnRowCommand="gvMaster_RowCommand" OnRowDataBound="gvMaster_RowDataBound">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--門市編號-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市名稱-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品料號-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--展示起日-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ExhibitionStartDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--展示訖日-->
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ExhibitionEndDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--拆封數量-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, OpenedQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--折扣方式-->
                                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, DiscountMethod %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--金額/佔比-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AmountOrPercentage %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="7" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("門市編號") %>' CommandName="select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />
                                <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                                <asp:BoundField DataField="展示起日" HeaderText="<%$ Resources:WebResources, ExhibitionStartDate %>" />
                                <asp:BoundField DataField="展示訖日" HeaderText="<%$ Resources:WebResources, ExhibitionEndDate %>" />
                                <asp:BoundField DataField="拆封數量" HeaderText="<%$ Resources:WebResources, OpenedQuantity %>" />
                                <asp:BoundField DataField="折扣方式" HeaderText="<%$ Resources:WebResources, DiscountMethod %>" />
                                <asp:BoundField DataField="金額/佔比" HeaderText="<%$ Resources:WebResources, AmountOrPercentage %>" />
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
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="GridScrollBar" style="height: 213px" runat="server" id="DIVdetail" visible="false">
                        <div class="SubEditCommand">
                        </div>
                        <div class="SubEditCommand">
                            <asp:Button ID="Button2" runat="server" 
                                Text="<%$ Resources:WebResources, Add %>"/>
                            <asp:Button ID="Button3" runat="server" 
                                Text="<%$ Resources:WebResources, Delete %>" />
                            <asp:Label ID="Label5" runat="server" Text="門市編號:GA00001" ForeColor="White"></asp:Label>
                        </div>
                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            Visible="False" OnRowDataBound="gvDetail_RowDataBound" OnRowUpdating="gvDetail_RowUpdating"
                            OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing" >
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                    </th>
                                    <th scope="col">
                                    </th>
                                    <th scope="col">
                                        <!--IMEI-->
                                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Imei %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="6" class="tdEmptyData">
                                        <!--此無明細資料-->
                                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>

                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckBox3" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox4" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ButtonType="Button"
                                    UpdateText="<%$ Resources:WebResources, Save %>" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("IMEI") %>'></asp:TextBox>
                                        <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            OnClientClick="openwindow('../SAL/SAL01_inputIMEIData.aspx','500','400');return false;" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("IMEI") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true"  />
                                <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true"  />
                            </Columns>
                        </asp:GridView>
                        <div class="seperate">
                        </div>
                        <div class="btnPosition">
                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                OnClick="btnSave_Click" Visible="false" />
                            <asp:Button ID="btnDiscard" runat="server" Text="<%$ Resources:WebResources, Discard %>"
                                Visible="false" />
                            <asp:Button ID="Button8" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                Visible="false" />
                        </div>
                    </div>
                </ContentTemplate>
                <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>--%>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>

        </div>
    </div>
    </form>
</body>
</html>
