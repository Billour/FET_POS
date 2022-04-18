<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV18_1.aspx.cs" Inherits="VSS_INV_INV18_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
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
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockAdjustment %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='INV18.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="lblOrderNo" runat="server" Text="HR100914001"></asp:Label>
                            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <cc1:postbackdate_textbox id="postbackDate_TextBox1" runat="server" imageurl="~/Icon/calendar.jpg"
                                text="2010/07/01" />
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            暫存
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整門市-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:TextBox ID="lblOrderNo0" runat="server" Text="門市A" Width="110"></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                OnClientClick="openwindow('INV18_3.aspx',640,300);return false;" />
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--備註-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap">
                            <asp:TextBox ID="TextBox3" runat="server" Width="100%"></asp:TextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="SubEditCommand">
                <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                    OnClick="btnNew_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="divContent" runat="server" class="SubEditBlock">
                        <cc1:exgridview id="gvMaster" runat="server" autogeneratecolumns="False" cssclass="mGrid"
                            onrowcancelingedit="gvMaster_RowCancelingEdit" onrowediting="gvMaster_RowEditing"
                            onrowupdating="gvMaster_RowUpdating" allowpaging="True" pagesize="8" pagerstyle-horizontalalign="Right"
                            onpageindexchanging="GridView_PageIndexChanging"  >
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">&nbsp;</th>
                                        <th scope="col">&nbsp;</th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--項次-->
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap" >
                                            <!--商品編號-->
                                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--商品名稱-->
                                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--庫存量-->
                                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--調整量-->
                                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AdjuestmentQuantity %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--調整原因-->
                                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ReasonForAdjustment %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr id="trEmptyData" runat="server">
                                        <td colspan="8" class="tdEmptyData">
                                            <!--choose add button-->
                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckAll" runat="server" CssClass="SHERRY" onclick="javascript:if(this.checked){$('.SHERRY').checkCheckboxes();}else{$('.SHERRY').unCheckCheckboxes();}" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckItem" runat="server" CssClass="SHERRY"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:Button ID="Button3" runat="server" CausesValidation="True" CommandName="Update"
                                                Text="<%$ Resources:WebResources, Save %>" />
                                            &nbsp;<asp:Button ID="Button4" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="<%$ Resources:WebResources, Cancel %>" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                Text="<%$ Resources:WebResources, Edit %>" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" ItemStyle-HorizontalAlign="Left" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:Label ID="label" runat="server" Text='<%# Bind("商品編號") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("商品編號") %>' Width="70"></asp:TextBox>
                                            <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;" />
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <FooterTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("商品編號") %>' Width="70"></asp:TextBox>
                                            <asp:Button ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;" />
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, StockQuantity %>">
                                        <ItemTemplate>
                                            <asp:Label ID="laQuantity" runat="server" Text='<%# Bind("庫存量") %>' Width="100"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txQuantity" runat="server" Text='<%# Bind("庫存量") %>' Width="100"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txQuantity1" runat="server" Text='<%# Bind("庫存量") %>' Width="100"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, AdjuestmentQuantity %>">
                                        <ItemTemplate>
                                            <asp:Label ID="laAdjqty" runat="server" Text='<%# Bind("調整量") %>' Width="100"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txAdjqty" runat="server" Text='<%# Bind("調整量") %>' Width="100"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txAdjqty" runat="server" Text='<%# Bind("調整量") %>' Width="100"/>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:WebResources, ReasonForAdjustment %>" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="tbReason" Text='<%# Bind("調整原因") %>' Width="160"/>
                                            <asp:Button ID="Label2" runat="server" Text="選" OnClientClick="openwindow('INV18_2.aspx',300,300);return false;" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="TextBox1" runat="server" Text='<%# Bind("調整原因") %>' Width="80"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="tbReason" Text='<%# Bind("調整原因") %>' Width="80"/>
                                            <asp:Button ID="Label2" runat="server" Text="選" OnClientClick="openwindow('INV18_2.aspx',300,300);return false;" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    
                                </columns>
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
                            </cc1:exgridview>
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="btnPosition">
                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                            OnClick="btnSave_Click" Visible="false" />
                        <asp:Button ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                            Visible="false" OnClientClick="document.location='INV18.aspx';return false;" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
