<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT04.aspx.cs" Inherits="VSS_OPT_OPT04" %>

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
    <div class="titlef">
        禮券設定作業
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">公司別：</td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:DropDownList ID="ddlCompany" runat="server"></asp:DropDownList>
                    </td>
                    <td class="tdtxt"></td>
                    <td class="tdval"></td>
                    <td class="tdtxt" nowrap="nowrap">狀態：</td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>尚未生效</asp:ListItem>
                            <asp:ListItem>已過期</asp:ListItem>
                        </asp:DropDownList>
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock" visible="true">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>"
                            OnClick="btnAdd_Click" Visible="true" />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" Visible="true" />
                    </div>
                    <div class="GridScrollBar" style="height: 500px">
                        <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="mGrid" OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" PageSize="5" PagerStyle-HorizontalAlign="Right"
                            OnPageIndexChanging="GridView_PageIndexChanging" 
                            ShowFooterWhenEmpty="False" ShowHeaderWhenEmpty="False">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--項次-->
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--狀態-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--公司別-->
                                        <asp:Literal ID="Literal8" runat="server" Text="公司別"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--禮券名稱-->
                                        <asp:Literal ID="Literal9" runat="server" Text="禮券名稱"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap" width="60">
                                        <!--手續費-->
                                        <asp:Literal ID="Literal22" runat="server" Text="手續費"></asp:Literal>
                                    </th>
                                    <th scope="col" width="90">
                                        <!--是否輸入禮券序號-->
                                        <asp:Literal ID="Literal23" runat="server" Text="是否輸入禮券序號"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--開始日期-->
                                        <asp:Literal ID="Literal3" runat="server" Text="開始日期"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--結束日期-->
                                        <asp:Literal ID="Literal6" runat="server" Text="終止編號"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--更新日期-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--更新人員-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="12" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" CssClass="checkItem" onclick="javascript:if(this.checked){$('.checkItem').checkCheckboxes();}else{$('.checkItem').unCheckCheckboxes();}" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server"  CssClass="checkItem" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
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
                                            CommandName="Save" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            OnClick="btnCancel_Click" />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="序號" HeaderText="項次" ReadOnly="true" FooterStyle-Wrap="false"
                                    ControlStyle-Width="40px" >
                                </asp:BoundField>
                                <asp:BoundField DataField="狀態" HeaderText="狀態" ReadOnly="true" FooterStyle-Wrap="false"
                                    ControlStyle-Width="40px" >
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="公司別" FooterStyle-Wrap="false" ControlStyle-Width="60px">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCompany" runat="server">
                                            <asp:ListItem Text="遠東百貨" Value="遠東百貨"></asp:ListItem>
                                            <asp:ListItem Text="愛買" Value="愛買"></asp:ListItem>
                                            <asp:ListItem Text="Sogo" Value="Sogo"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                       <asp:DropDownList ID="ddlCompany" runat="server">
                                            <asp:ListItem Text="遠東百貨" Value="遠東百貨"></asp:ListItem>
                                            <asp:ListItem Text="愛買" Value="愛買"></asp:ListItem>
                                            <asp:ListItem Text="Sogo" Value="Sogo"></asp:ListItem>
                                        </asp:DropDownList>  
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="bl31" runat="server" Text='<%# Bind("公司別") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="禮券名稱" FooterStyle-Wrap="false" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlCuponName">
                                            <asp:ListItem Text="酬賓禮券" Value="0" />
                                            <asp:ListItem Text="高級禮券" Value="1" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                       <asp:DropDownList runat="server" ID="ddlCuponName">
                                            <asp:ListItem Text="酬賓禮券" Value="0" />
                                            <asp:ListItem Text="高級禮券" Value="1" />
                                        </asp:DropDownList>  
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("禮券名稱") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="手續費" FooterStyle-Wrap="false" ControlStyle-Width="25px" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="60">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("手續費") %>' Width="25px"></asp:TextBox>%
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("手續費") %>' Width="25px"></asp:Label>%
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt2" runat="server" Width="25px"></asp:TextBox>%
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="是否輸入禮券序號" HeaderStyle-Width="90" ControlStyle-Width="40px" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="開始日期" FooterStyle-Wrap="false" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("開始日期") %>' Width="80px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("開始日期") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtStartDate" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="結束日期" FooterStyle-Wrap="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("結束日期") %>'  Width="80"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("結束日期") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtEndDate" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="更新日期" HeaderText="更新日期" ReadOnly="True" 
                                    HeaderStyle-Width="80px" DataFormatString="{0:yyyy/MM/dd hh:mm:ss}">
                                </asp:BoundField>
                                <asp:BoundField DataField="更新人員" HeaderText="更新人員" ReadOnly="true" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                                </asp:BoundField>
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
