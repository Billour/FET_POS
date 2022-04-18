<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV17.aspx.cs" Inherits="VSS_INV17_INV17" %>

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
        function openwindow(url, width, height) {
            window.open(url, "關帳日查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">關帳日設定</td>
                    <td align="right">&nbsp;</td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">關帳年月：</td>
                        <td>
                            起<cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                            訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                <asp:Button ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"  />
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="divContent" runat="server" class="SubEditBlock" >
                       <div class="SubEditCommand">
                            <asp:Button ID="btnNew" runat="server" Text="新增" OnClick="btnNew_Click" />
                            <asp:Button ID="btnDel" runat="server" Text="刪除" />
                       </div>
                       <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" OnRowDataBound="gvMaster_RowDataBound" AllowPaging="True"
                            PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col" nowrap="nowrap">&nbsp;</th>
                                    <th scope="col" nowrap="nowrap">關帳年月</th>
                                    <th scope="col" nowrap="nowrap">關帳日</th>
                                    <th scope="col" nowrap="nowrap">更新人員</th>
                                    <th scope="col" nowrap="nowrap">更新日期</th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="5" class="tdEmptyData">
                                        查無資料，請修改條件，重新查詢
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
                                <asp:TemplateField ShowHeader="False" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="<%$ Resources:WebResources, Save %>" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="<%$ Resources:WebResources, Cancel %>" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="編輯" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="關帳年月" ItemStyle-HorizontalAlign="Left">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("關帳年月") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Format="yyyy/MM" TargetControlID="TextBox1"></asp:CalendarExtender>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("關帳年月") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("關帳年月") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Format="yyyy/MM" TargetControlID="TextBox1"></asp:CalendarExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="關帳日" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="label" runat="server" Text='<%# Bind("關帳日") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("關帳日") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="TextBox4_CalendarExtender" runat="server" TargetControlID="TextBox4" Format="yyyy/MM/dd"></asp:CalendarExtender>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("關帳日") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="TextBox4_CalendarExtender" runat="server" TargetControlID="TextBox4" Format="yyyy/MM/dd"></asp:CalendarExtender>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="更新人員" HeaderText="更新人員" ReadOnly="true" />
                                <asp:BoundField DataField="更新日期" HeaderText="更新日期" ReadOnly="true" />
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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
