<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA01.aspx.cs" Inherits="VSS_LEA01_LEA01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

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
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--設備租賃設定-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, EquipmentLeasing %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='LEA07.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--類別-->
                        <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="1" Text="漫遊租賃"></asp:ListItem>
                            <asp:ListItem Value="2" Text="維修租賃"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--產品類別-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem>魔拖羅拉</asp:ListItem>
                            <asp:ListItem>紅打電</asp:ListItem>
                            <asp:ListItem>HOKIA</asp:ListItem>
                            <asp:ListItem>其他</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--產品名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem Text="產品1"></asp:ListItem>
                            <asp:ListItem Text="產品2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="00-未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--外部廠商代碼-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, OutsideFirmNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList3" runat="server">
                            <asp:ListItem>-請選擇-</asp:ListItem>
                            <asp:ListItem Value="廠商1"></asp:ListItem>
                            <asp:ListItem Value="產品2"></asp:ListItem>
                            <asp:ListItem Value="產品3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdtxt">
                        <!--外部廠商名稱-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OutsideFirmName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label6" runat="server" Text="名稱1"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="2010/07/01 22:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--租金料號-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfRent %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--日租金額-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, DailyRent %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--維護人員-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--保證金料號-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, PartNumberOfRentDeposit %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--保證金-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, RentDeposit %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--賠償金料號-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, PartNumberOfCompensation %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
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
                        <!--有效期間-->
                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox
                            ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox
                            ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--備註-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Width="98%"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="100%" CssClass="visoft__tab_xpie7" AutoPostBack="True">
                <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--賠償項目-->
                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, CompensationItems %>"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div id="Div1" runat="server" class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="btnAdd1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnAdd1_Click" />
                                        <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </div>
                                    <div class="GridScrollBar" style="height: auto">
                                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                            OnRowUpdating="gvMaster_RowUpdating">
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <th scope="col">
                                                        &nbsp;
                                                    </th>
                                                    <th scope="col">
                                                        <asp:Literal ID="Literal29" runat="server" Text=""></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--賠償項目-->
                                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, CompensationItems %>"></asp:Literal>
                                                    </th>
                                                    <th scope="col">
                                                        <!--金額-->
                                                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, Amount %>"></asp:Literal>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" class="tdEmptyData">
                                                        <!--請點選新增按鍵增加資料-->
                                                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckALL" runat="server" CssClass="CompenItems" onclick="javascript:if(this.checked){$('.CompenItems').checkCheckboxes();}else{$('.CompenItems').unCheckCheckboxes();}" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="CompenItems" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>"
                                                            CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                                            CommandName="Update" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                                            CommandName="Cancel" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                                            CommandName="Save" OnClick="btnCancel1_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                                            OnClick="btnCancel1_Click" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, CompensationItems %>"
                                                    ControlStyle-Width="40px">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="Label98" runat="server" Text='<%# Bind("賠償項目") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label99" runat="server" Text='<%# Bind("賠償項目") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtCompenItem" runat="server" Width="50px"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, Amount %>" ControlStyle-Width="40px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt99" runat="server" Text='<%# Bind("金額") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label97" runat="server" Text='<%# Bind("金額") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" Width="50px"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="賠償項目" HeaderText="<%$ Resources:WebResources, CompensationItems %>" />
                                                <asp:BoundField DataField="金額" HeaderText="<%$ Resources:WebResources, Amount %>" />--%>
                                            </Columns>
                                        </cc1:ExGridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--折扣項目-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, DiscountItems %>"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="btnAdd2" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnAdd2_Click" />
                                        <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                    </div>
                                    <div class="GridScrollBar" style="height: auto">
                                        <cc1:ExGridView ID="gvDiscountItem" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                            OnRowCancelingEdit="gvDiscountItem_RowCancelingEdit" OnRowEditing="gvDiscountItem_RowEditing"
                                            OnRowUpdating="gvDiscountItem_RowUpdating">
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <th scope="col">
                                                        &nbsp;
                                                    </th>
                                                    <th scope="col">
                                                        <th scope="col">
                                                            <!--折扣料號-->
                                                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>
                                                        </th>
                                                        <th scope="col">
                                                            <!--折扣名稱-->
                                                            <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>
                                                        </th>
                                                        <th scope="col">
                                                            <!--折扣金額-->
                                                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, DiscountAmount %>"></asp:Literal>
                                                        </th>
                                                        <th scope="col">
                                                            <!--折扣比率-->
                                                            <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, DiscountRate %>"></asp:Literal>
                                                        </th>
                                                        <th scope="col">
                                                            <!--成本中心-->
                                                            <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, CostCenter %>"></asp:Literal>
                                                        </th>
                                                        <th scope="col">
                                                            <!--會計科目-->
                                                            <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>"></asp:Literal>
                                                        </th>
                                                </tr>
                                                <tr>
                                                    <td colspan="8" class="tdEmptyData">
                                                        <!--請點選新增按鍵增加資料-->
                                                        <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckALL" runat="server" CssClass="DiscountItems" onclick="javascript:if(this.checked){$('.DiscountItems').checkCheckboxes();}else{$('.DiscountItems').unCheckCheckboxes();}" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="DiscountItems" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEdit1" runat="server" Text="<%$ Resources:WebResources, Edit %>"
                                                            CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Button ID="btnUpdate1" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                                            CommandName="Update" />
                                                        <asp:Button ID="btnCancel1" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                                            CommandName="Cancel" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnSave1" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                                            CommandName="Save" OnClick="btnCancel2_Click" />
                                                        <asp:Button ID="btnCancel1" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                                            OnClick="btnCancel2_Click" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                                                    ControlStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbl87" runat="server" Text='<%# Bind("折扣料號") %>' Width="50px"></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl90" runat="server" Text='<%# Eval("折扣料號") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt90" runat="server" Width="50px"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DiscountName %>" ControlStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lbl83" runat="server" Text='<%# Bind("折扣名稱") %>'></asp:Label>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl89" runat="server" Text='<%# Eval("折扣名稱") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt89" runat="server" Width="50px"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DiscountAmount %>" ControlStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt82" runat="server" Text='<%# Bind("折扣金額") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl82" runat="server" Text='<%# Eval("折扣金額") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt81" runat="server" Width="50px"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DiscountRate %>" ControlStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt87" runat="server" Text='<%# Bind("折扣比率") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl88" runat="server" Text='<%# Eval("折扣比率") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt88" runat="server" Width="50px"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, CostCenter %>" ControlStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt86" runat="server" Text='<%# Bind("成本中心") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl85" runat="server" Text='<%# Eval("成本中心") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt85" runat="server" Width="50px"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, AccountingSubject %>"
                                                    ControlStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt84" runat="server" Text='<%# Bind("會計科目") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl84" runat="server" Text='<%# Eval("會計科目") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt83" runat="server" Width="50px"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </cc1:ExGridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                    <HeaderTemplate>
                        <span>租賃手機庫存</span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <div class="GridScrollBar" style="height: auto">
                                            <asp:GridView ID="gvMobileStock" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                                <EmptyDataTemplate>
                                                    <tr>
                                                        <th scope="col">
                                                            門市代號
                                                        </th>
                                                        <th scope="col">
                                                            門市名稱
                                                        </th>
                                                        <th scope="col">
                                                            手機序號
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" class="tdEmptyData">
                                                            請點選新增按鍵增加資料
                                                        </td>
                                                    </tr>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField DataField="門市代號" HeaderText="門市代號" />
                                                    <asp:BoundField DataField="門市名稱" HeaderText="門市名稱" />
                                                    <asp:BoundField DataField="手機序號" HeaderText="手機序號" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="seperate">
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
        <div class="seperate">
        </div>
        <div class="GridScrollBar" style="height: auto">
            <asp:Panel ID="Panel1" runat="server" Visible="false">
            </asp:Panel>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="存檔" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="取消" />
            <asp:Button ID="btnDelete" runat="server" Text="刪除" />
            <asp:Button ID="btnImport" runat="server" Text="匯入" />
        </div>
            <div class="GridScrollBar" style="height: auto">
        <asp:Panel ID="Panel2" runat="server" Visible="false">
            <asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" Width="60%">
                <ItemTemplate>
                    <table class="mGrid" width="60%">
                        <tr>
                        <td  align="center" >
                                <!--修改記錄-->
                                <asp:Literal ID="Literal40" runat="server" Text="修改記錄"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <!--更新日期-->
                                <asp:Literal ID="Literal33" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                            </td>
                            <td align="center">
                                <!--工號-->
                                <asp:Literal ID="Literal30" runat="server" Text="工號"></asp:Literal>
                            </td>
                            <td align="center">
                                <!--姓名-->
                                <asp:Literal ID="Literal31" runat="server" Text="姓名"></asp:Literal>
                            </td>
                            <td align="center">
                                <!--說明-->
                                <asp:Literal ID="Literal34" runat="server" Text="說明"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Literal ID="Literal32" runat="server" Text="2010/09/30"></asp:Literal>
                            </td>
                          
                            <td align="center">
                                <asp:Literal ID="Literal7" runat="server" Text="60736"></asp:Literal>
                            </td>
                           
                            <td align="center">
                                <asp:Literal ID="Literal19" runat="server" Text="王小明"></asp:Literal>
                            </td>
                            
                            <td align="center">
                                <asp:Literal ID="Literal35" runat="server" Text="保證金料號修改"></asp:Literal>
                            </td>
                           
                        </tr>
                     
                    </table>
                </ItemTemplate>
            </asp:FormView>
        
        </asp:Panel>
        </div>
               
    </div>
    </form>
</body>
</html>
