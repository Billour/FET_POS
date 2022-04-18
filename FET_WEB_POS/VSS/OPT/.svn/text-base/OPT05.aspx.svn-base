<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT05.aspx.cs" Inherits="VSS_OPT_OPT05" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        總部發票設定作業
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        門市編號：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server" Width="100px"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" Text="選" OnClientClick="openwindow('../INV/INV18_3.aspx',640,300);return false;" />
                    </td>
                    <td class="tdtxt">
                        門市名稱：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        狀態：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>尚未生效</asp:ListItem>
                            <asp:ListItem>已過期</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        所屬年月：
                    </td>
                    <td class="tdval" colspan="3">
                        起<cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="清空" />
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock" visible="true">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnNew" runat="server" Text="新增" Visible="true" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" Visible="true" />
                        <%--<asp:Button ID="btnDelete0" runat="server" Text="匯入" Visible="true" />--%>
                        <asp:Button ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="Import" PopupButtonID="btnImport"
                                TargetControlID="HiddenField1" Width="630" Height="400" NavigateUrl="~/VSS/OPT/OPT05_Import.aspx" />
                    </div>
                    <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="mGrid" OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                        OnRowUpdating="gvMaster_RowUpdating" OnRowCommand="gvMaster_RowCommand" PageSize="5"
                        PagerStyle-HorizontalAlign="Right" 
                        OnPageIndexChanging="GridView_PageIndexChanging" 
                        onselectedindexchanged="gvMaster_SelectedIndexChanged" 
                        onrowdatabound="gvMaster_RowDataBound">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col" nowrap="nowrap">
                                    &nbsp;
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    &nbsp;
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    門市編號
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    門市名稱
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    用途
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    所屬年月(起)
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    所屬年月(迄)
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    字軌
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    起始編號
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    終止編號
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    目前編號
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    發票張數
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    更新日期
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    更新人員
                                </th>
                            </tr>
                            <tr id="trEmptyData" runat="server">
                                <td colspan="15" class="tdEmptyData">
                                    <!--查無資料，請修改條件，重新查詢-->
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript:if(this.checked){$('#Div1').checkCheckboxes();}else{$('#Div1').unCheckCheckboxes();}" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckItem" runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="存檔" CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="編輯" CommandName="Edit" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                        CommandName="Save" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                        OnClick="btnCancel_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="項次" FooterStyle-Wrap="false" ControlStyle-Width="40px">
                                <EditItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("項次") %>' Width="40px"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("項次") %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="Label913" runat="server" Text='' Width="60px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="門市編號" FooterStyle-Wrap="false" ControlStyle-Width="80px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="Label211" runat="server" Text='<%# Bind("門市編號") %>' Width="80px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="Label2" runat="server" Text='<%# Bind("門市編號") %>' CommandName="select"
                                        Width="80px"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="Label914" runat="server" Text='' Width="80px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="門市名稱" FooterStyle-Wrap="false" ControlStyle-Width="80px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="Label5" runat="server" Text='<%# Bind("門市名稱") %>' Width="80px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("門市名稱") %>' Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="Label915" runat="server" Text='' Width="80px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="用途" FooterStyle-Wrap="false" ControlStyle-Width="40px">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="RadioButtonList1" runat="server" Width="40px">
                                        <asp:ListItem>離線</asp:ListItem>
                                        <asp:ListItem>連線</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("用途") %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="Label95" runat="server" Text='' Width="60px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="所屬年月(起)" HeaderText="所屬年月(起)" HeaderStyle-Width="80px"
                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="80px" />
                            <asp:BoundField DataField="所屬年月(訖)" HeaderText="所屬年月(訖)" HeaderStyle-Width="80px"
                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="80px" />
                            <asp:BoundField DataField="字軌" HeaderText="字軌" HeaderStyle-Width="40px" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false" ItemStyle-Width="40px" />
                            <asp:BoundField DataField="起始編號" HeaderText="起始編號" HeaderStyle-Width="80px" HeaderStyle-Wrap="false"
                               ItemStyle-Width="80px" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="終止編號" HeaderText="終止編號" HeaderStyle-Width="80px" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false" ItemStyle-Width="80px" />
                            <asp:BoundField DataField="目前編號" HeaderText="目前編號" ReadOnly="true" HeaderStyle-Width="80px"
                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="80px" />
                            <asp:TemplateField HeaderText="發票張數" HeaderStyle-Width="80px" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false" ItemStyle-Width="80px">
                                <EditItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("發票張數") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("發票張數") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="Label916" runat="server" Text='' Width="80px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="更新日期" HeaderText="更新日期" ReadOnly="True" HeaderStyle-Width="80px"
                                HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="80px" />
                            <asp:BoundField DataField="更新人員" HeaderText="更新人員" ReadOnly="true" HeaderStyle-Width="80px"
                                HeaderStyle-Wrap="false"  ItemStyle-Width="80px" ItemStyle-Wrap="false" />
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
                    <div class="GridScrollBar" style="height: 216px">
                        <div class="SubEditCommand">
                        </div>
                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            Visible="False" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                            OnRowUpdating="gvDetail_RowUpdating">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        項次
                                    </th>
                                    <th scope="col">
                                        機台號碼
                                    </th>
                                    <th scope="col">
                                        起始編號
                                    </th>
                                    <th scope="col">
                                        終止編號
                                    </th>
                                    <th scope="col">
                                        張數
                                    </th>
                                    <th scope="col">
                                        發票分配日期
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="5" class="tdEmptyData">
                                        此無明細資料
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
                                        <asp:Button ID="btnSave" runat="server" Text="存檔" CommandName="Update" />
                                        <asp:Button ID="btnCancel" runat="server" Text="取消" CommandName="Cancel" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="編輯" CommandName="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="項次">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="機台號碼" HeaderText="機台號碼" ReadOnly="true" />
                                <asp:BoundField DataField="起始編號" HeaderText="起始編號" ReadOnly="true" />
                                <asp:BoundField DataField="終止編號" HeaderText="終止編號" ReadOnly="true" />
                                <asp:TemplateField HeaderText="張數">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("張數") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("張數") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="發票分配日期">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("發票分配日期") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("發票分配日期") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
