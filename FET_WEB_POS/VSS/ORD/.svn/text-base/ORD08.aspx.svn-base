<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD08.aspx.cs" Inherits="VSS_ORD08_ORD08" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--Non-DropShipment主配作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="Non-DropShipment主配作業"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='ORD07.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--主配單號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DistributionNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label1" runat="server" Text="HO100817002"></asp:Label>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label3" runat="server" Text="00 未存檔"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            &nbsp;
                        </td>
                        <td class="tdval" colspan="3">
                            &nbsp;
                        </td>
                        <td class="tdtxt">
                            <!--更新日期-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label4" runat="server" Text="10/07/12 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                            <!--更新人員-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label5" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="Div1" runat="server" class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="Button1_Click" />
                            <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                            <asp:Button ID="Button6" runat="server" Text="ATR比對" />
                            <asp:Button ID="Button7" runat="server" Text="<%$ Resources:WebResources, Import %>" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="Import" PopupButtonID="Button7"
                                TargetControlID="HiddenField1" Width="500" Height="500" NavigateUrl="~/VSS/ORD/ORD10_Import.aspx" />
                        </div>
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            AllowPaging="True" OnSelectedIndexChanged="gvMaster_SelectedIndexChanged" PageSize="5"
                            PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating1" OnRowCommand="gvMaster_RowCommand">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--ATR-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, AtrQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <asp:Literal ID="Literal20" runat="server" Text="自動分配" />
                                    </th>
                                    <th scope="col">
                                        <!--主配量-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, DistributionQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--備註-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="8" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" CssClass="chk" onclick="javascript:if(this.checked){$('.chk').checkCheckboxes();}else{$('.chk').unCheckCheckboxes();}" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server" CssClass="chk"  />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="30px" ItemStyle-Width="30px" FooterStyle-Width="30px">
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                        CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                        CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>"
                                        CommandName="Edit" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                        CommandName="Save" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                        OnClick="btnCancel_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>                                                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="Label1" runat="server" Text='<%# Eval("商品編號") %>' />
                                        <asp:Button ID="Button8" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNo.aspx',640,300);return false;" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnProductNo" CommandName="View" runat="server" Text='<%# Bind("商品編號") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Eval("商品編號") %>' />
                                        <asp:Button ID="Button8" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNo.aspx',640,300);return false;" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductName %>">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品名稱") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="TextBox1" runat="server" Text='<%# Bind("商品名稱") %>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, AtrQuantity %>">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("ATR量") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox13" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="自動分配" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAutoSend" Checked="true" runat="server" Enabled="false" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="checkbox1" runat="server" Width="80"></asp:CheckBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DistributionQuantity %>">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("主配量") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox24" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, Remark %>">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Text='<%# Bind("備註") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("備註") %>' Width="80"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox14" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
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
                        </cc1:ExGridView>
                        <div class="seperate">
                        </div>
                        <div id="showDetail" class="SubEditCommand" runat="server" visible="false">
                            <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="Button9_Click" Enabled="true" />
                            <asp:DropDownList ID="DropDownList3" runat="server" Enabled="true">
                                <asp:ListItem Text="ALL" Value="ALL" />
                                <asp:ListItem Text="北區" Value="北區" />
                                <asp:ListItem Text="中區" Value="中區" />
                                <asp:ListItem Text="南區" Value="南區" />
                            </asp:DropDownList>
                            <asp:Button ID="Button19" runat="server" Text="<%$ Resources:WebResources, Confirm %>"
                                Enabled="true" />
                            <asp:Button ID="Button17" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                Enabled="true" />
                        </div>
                    </div>
                    <cc1:ExGridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        Visible="False" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                        OnRowUpdating="gvDetail_RowUpdating">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    <!--出貨倉別-->
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ShipmentWarehouse %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--門市編號-->
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--門市名稱-->
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--主動配貨量-->
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, DistributionQuantity %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr id="showEmpty" runat="server">
                                <td colspan="4" class="tdEmptyData">
                                    此無明細資料
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckAll" runat="server" CssClass="chk111" onclick="javascript:if(this.checked){$('.chk111').checkCheckboxes();}else{$('.chk111').unCheckCheckboxes();}" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckItem" runat="server" CssClass="chk111" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="30px" ItemStyle-Width="30px" FooterStyle-Width="30px">
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                        CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                        CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>"
                                        CommandName="Edit" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                        CommandName="Save" OnClick="btnCancel1_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                        OnClick="btnCancel1_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="出貨倉別" HeaderText="<%$ Resources:WebResources, ShipmentWarehouse %>"
                                ReadOnly="true" />
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>">
                                <EditItemTemplate>
                                    <asp:TextBox ID="Label1" runat="server" Text='<%# Eval("門市編號") %>' />
                                    <asp:Button ID="Button2" runat="server" Text="選" OnClientClick="openwindow('../INV/INV18_3.aspx',640,300);return false;" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("門市編號") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="Label111" runat="server" Text='<%# Eval("門市編號") %>' Width="80px" />
                                    <asp:Button ID="Button222" runat="server" Text="選" OnClientClick="openwindow('../INV/INV18_3.aspx',640,300);return false;" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>"
                                ReadOnly="true" />
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>">
                                <EditItemTemplate>
                                    <asp:TextBox ID="Label31" runat="server" Text='<%# Eval("主動配貨量") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label311" runat="server" Text='<%# Bind("主動配貨量") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="Label321" runat="server" Text='<%# Eval("主動配貨量") %>' Width="80px" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc1:ExGridView>
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="btnPosition" id="divShow" runat="server">
                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Save %>" />
                        <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                        <asp:Button ID="Button13" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                        <asp:Button ID="Button14" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>" />
                        <asp:Button ID="Button15" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                    </div>
                    <div class="seperate">
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
