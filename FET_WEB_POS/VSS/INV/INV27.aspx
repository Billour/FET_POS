<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV27.aspx.cs" Inherits="VSS_INV_INV27" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--總部拆封商品設定-->
                        <asp:Literal ID="Literal1" runat="server" Text="總部拆封商品設定"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--拆封日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OpenedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox
                            ID="TextBox1" runat="server"></asp:TextBox>
                        &nbsp;<asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox
                            ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        <asp:Button ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                            OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400); return false;" />
                        &nbsp;<asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        <asp:Button ID="btnChooseProduct1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                            OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400); return false;" />
                    </td>
                    <td class="tdtxt">                
                    </td>
                    <td class="tdval">                  
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>             
                <tr> <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                     <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                     <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <div class="SubEditBlock">
                <div class="SubEditCommand">
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                        OnClick="Button1_Click" visible="false"/>
                    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" visible="false"/>
                </div>
                <div class="GridScrollBar" style="height: 200px">
                    <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                        OnRowUpdating="gvMaster_RowUpdating" OnRowCommand="gvMaster_RowCommand">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">
                                    &nbsp;
                                </th>
                                <th scope="col">
                                    &nbsp;
                                </th>
                                <th scope="col">
                                    <!--拆封日期-->
                                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, OpenedDate %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--展示起日-->
                                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ExhibitionStartDate %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--展示訖日-->
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ExhibitionEndDate %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品料號-->
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--商品名稱-->
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--拆封數量-->
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, OpenedQuantity %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--折扣方式-->
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, DiscountMethod %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--金額/占比-->
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AmountOrPercentage %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--更新日期-->
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                </th>
                                <th scope="col">
                                    <!--更新人員-->
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr>
                                <td colspan="12" class="tdEmptyData">
                                    <!--請點選新增按鍵增加資料-->
                                     <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="false" ShowEditButton="True" ButtonType="Button"
                                UpdateText="<%$ Resources:WebResources, Save %>" />
                            <asp:BoundField DataField="拆封日期" HeaderText="<%$ Resources:WebResources, OpenedDate %>"  DataFormatString="{0:yyyy/MM/dd}"/>
                            <asp:BoundField DataField="展示起日" HeaderText="<%$ Resources:WebResources, ExhibitionStartDate %>"  DataFormatString="{0:yyyy/MM/dd}"/>
                            <asp:BoundField DataField="展示訖日" HeaderText="<%$ Resources:WebResources, ExhibitionEndDate %>"  DataFormatString="{0:yyyy/MM/dd}"/>
                             <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("商品料號") %>'></asp:TextBox>
                                                <asp:Button ID="Button6" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                               <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("商品料號") %>' CommandName="select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                            
                            <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                            <asp:BoundField DataField="拆封數量" HeaderText="<%$ Resources:WebResources, OpenedQuantity %>" />
                           
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, DiscountMethod %>">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlDiscount" runat="server">
                                        <asp:ListItem>金額</asp:ListItem>
                                        <asp:ListItem>百分比</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("折扣方式") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="90px" />
                                </asp:TemplateField>
                            <asp:BoundField DataField="金額/占比" HeaderText="<%$ Resources:WebResources, AmountOrPercentage %>" />
                            <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedDate %>"
                                ReadOnly="true" />
                            <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedBy %>"
                                ReadOnly="true" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="seperate">
            </div>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="100%" CssClass="visoft__tab_xpie7" Visible="false">
                <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--指定門市-->
                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, SpecifyStore %>"></asp:Literal>
                        </span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="SubEditBlock">
                            <div class="SubEditCommand">
                                <asp:Button ID="Button4" runat="server" 
                                    Text="<%$ Resources:WebResources, Add %>" OnClick="Button4_Click" />
                                <asp:Button ID="Button5" runat="server" 
                                    Text="<%$ Resources:WebResources, Delete %>" />
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem Text="區域" Value="區域" />
                                    <asp:ListItem Text="ALL" Value="ALL" />
                                    <asp:ListItem Text="北一區" Value="北一區" />
                                    <asp:ListItem Text="中一區" Value="中一區" />
                                    <asp:ListItem Text="南一區" Value="南一區" />
                                </asp:DropDownList>
                                <!--區域確認-->
                                <asp:Button ID="Button6" runat="server" Text="<%$ Resources:WebResources, DivideDistrict %>" />
                            </div>
                            <div class="GridScrollBar">
                                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                                    OnRowUpdating="gvDetail_RowUpdating">
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
                                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("門市編號") %>'></asp:TextBox>
                                                <asp:Button ID="Button7" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx',500,400); return false;" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("門市編號") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" />
                                        <asp:BoundField DataField="區域別" HeaderText="<%$ Resources:WebResources, ByDistrict %>" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>                                
            </asp:TabContainer>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                    OnClick="btnSave_Click" Visible="false" />
                <asp:Button ID="btnDiscard" runat="server" Text="<%$ Resources:WebResources, Discard %>"
                    Visible="false" />
                <asp:Button ID="Button8" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    Visible="false" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
