<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON05.aspx.cs" Inherits="VSS_CON05_CON05" %>

<%@ Register assembly="AdvTekUserCtrl" namespace="AdvTekUserCtrl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=230,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

//        function imeicheckbox(con) {
//            if (con.checked) {
//                openwindow("SAL01_inputIMEIData.aspx");
//            }
//        }

        function checkID() {
            var vID = document.getElementById("tbInvoiceNo").value;
            if (vID.length != 8 && vID > 0) {
                openwindow("SAL01_checkIDNumber.aspx", 300, 200);
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品訂單查詢-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentOrderSearch %>"></asp:Literal>
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
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單日期-->
                         <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" 
                            ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--訂單編號-->
                         <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                     <td class="tdtxt" nowrap="nowrap">
                        <!--狀態-->
                         <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>已存檔</asp:ListItem>
                            <asp:ListItem>轉單中</asp:ListItem>
                            <asp:ListItem>已成單</asp:ListItem>
                            <asp:ListItem>待進貨</asp:ListItem>
                            <asp:ListItem>已驗收</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                   
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品料號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',550,400);return false;" />
                    </td>
                   
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="2" nowrap="nowrap">
                        <asp:Literal ID="Literal24" runat="server" Text="21031 門市名稱1"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <asp:GridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                    OnRowCommand="gvMaster_RowCommand" onrowdatabound="gvMaster_RowDataBound">
                    <PagerStyle HorizontalAlign="Right" />
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col" nowrap="nowrap">
                                &nbsp;
                            </th>
                            <th scope="col" nowrap="nowrap">
                                <!--訂單日期-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>                                
                            </th>
                            <th scope="col" nowrap="nowrap">
                                <!--訂單編號-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>
                            </th>
                            <th scope="col" nowrap="nowrap">
                                <!--狀態-->
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                            </th>
                            <th scope="col" nowrap="nowrap">
                                <!--更新人員-->
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                            </th>
                            <th scope="col" nowrap="nowrap">
                                <!--更新日期-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                            </th>
                        </tr>
                        <tr>
                            <td colspan="6" class="tdEmptyData">
                                <!--查無資料，請修改條件，重新查詢-->
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField ShowHeader="false" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                           <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" CausesValidation="False" CommandName="select"
                                    Text="<%$ Resources:WebResources, Select %>" />
                                <%--<asp:Button ID="btnfix" runat="server" CausesValidation="False" CommandName="fix"
                                    Text="修改" Visible="false" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="訂單日期" HeaderText="<%$ Resources:WebResources, OrderDate %>" />
                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, OrderNo %>">
                            <EditItemTemplate>
                                <asp:TextBox ID="Label211" runat="server" Text='<%# Bind("訂單編號") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("訂單編號") %>' CommandName="View"></asp:LinkButton>
                                <%--<asp:HyperLink ID="HyperLink1" runat="server" Text="<%$ Resources:WebResources, Reserve %>"></asp:HyperLink>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="狀態" HeaderText="<%$ Resources:WebResources, Status %>" />
                        <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" />
                        <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" />
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
                <div class="GridScrollBar" style="height: 216px" runat="server" id="DIVdetail" visible="false">
                    <div class="SubEditCommand">
                        <asp:Label ID="Label5" runat="server" Text="訂單編號:101900074" ForeColor="White"></asp:Label>
                    </div>
                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        Visible="False" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                        OnRowUpdating="gvDetail_RowUpdating" AllowPaging="True" PageSize="2" PagerStyle-HorizontalAlign="Right" 
                        OnPageIndexChanging="GridView_PageIndexChanging">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col" nowrap="nowrap">
                                    <!--項次-->
                                     <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--廠商名稱-->
                                     <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--商品料號->
                                     <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--商品名稱-->
                                     <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--商品類別-->
                                     <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--建議訂購量-->
                                     <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, RecommendedOrderQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--實際訂購量-->
                                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ActualOrderQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--核准數量-->
                                    <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ApprovedQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--驗收量-->
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InspectionQuantity %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="9" class="tdEmptyData">
                                    <!--此無明細資料-->
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckAll" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckItem" runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" Visible="False">
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, Items %>">
                                <EditItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" ReadOnly="true" />
                            <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                            <asp:TemplateField HeaderText="商品名稱">
                                <EditItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCategory %>">
                                <EditItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("商品類別") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("商品類別") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="建議訂購量" HeaderText="<%$ Resources:WebResources, RecommendedOrderQuantity %>" />
                            <asp:BoundField DataField="實際訂購量" HeaderText="<%$ Resources:WebResources, ActualOrderQuantity %>" />
                            <asp:BoundField DataField="核准數量" HeaderText="<%$ Resources:WebResources, ApprovedQuantity %>" />
                            <asp:BoundField DataField="驗收量" HeaderText="<%$ Resources:WebResources, InspectionQuantity %>" />
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
            </ContentTemplate>
           
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
