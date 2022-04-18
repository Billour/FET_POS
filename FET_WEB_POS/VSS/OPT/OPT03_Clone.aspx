<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT03_Clone.aspx.cs" Inherits="VSS_OPT_OPT03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <style type="text/css">
        /*AutoComplete flyout */
        .autocomplete_completionListElement
        {
            margin: 0px !important;
            background-color: #ffffff;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            cursor: 'default';
            overflow: auto;
            height: 200px;
            text-align: left;
            list-style-type: none;
        }
        /* AutoComplete highlighted item */
        .autocomplete_highlightedListItem
        {
            background-color: Highlight;
            color: HighlightText;
            padding: 1px;
            margin-left: 0px;
        }
        /* AutoComplete item */
        .autocomplete_listItem
        {
            background-color: window;
            color: windowtext;
            padding: 1px;
            margin-left: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        信用卡分期設定作業
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        發卡銀行：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlCardBank" runat="server" Visible="false" />
                        <asp:TextBox ID="txtIssuingBank" runat="server"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="autoComplete1" runat="server" TargetControlID="txtIssuingBank"
                            ServiceMethod="GetBankNameList" ServicePath="~/WebServices/BankService.asmx"
                            MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                            CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="false">
                        </asp:AutoCompleteExtender>
                    </td>
                    <td class="tdtxt">
                        成本中心：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlCostCenter" runat="server" />
                    </td>
                    <td class="tdtxt">
                        狀態：
                    </td>
                    <td class="tdval">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem>請選擇</asp:ListItem>
                            <asp:ListItem>有效</asp:ListItem>
                            <asp:ListItem>尚未生效</asp:ListItem>
                            <asp:ListItem>已過期</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">分期期數：</td>
                    <td class="tdval" colspan="5" nowrap="nowrap">
                        <asp:TextBox ID="txtPaySeqment" runat="server"></asp:TextBox>
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
                <div id="Div1" runat="server" class="SubEditBlock">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnAdd1" runat="server" Text="新增" OnClick="btnAdd1_Click" />
                    </div>
                    <div class="GridScrollBar" style="height: 300px">
                        <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="mGrid" OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" OnRowCommand="gvMaster_RowCommand" PageSize="5"
                            PagerStyle-HorizontalAlign="Right" 
                            OnPageIndexChanging="GridView_PageIndexChanging" ShowFooterWhenEmpty="False" 
                            ShowHeaderWhenEmpty="False"
                            datakeynames="INSTELLMENT_ID">
                            <PagerStyle HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <tr>
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
                                        <!--發卡銀行-->
                                        <asp:Literal ID="Literal8" runat="server" Text="發卡銀行"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--分期期數-->
                                        <asp:Literal ID="Literal9" runat="server" Text="分期期數"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--分期利率-->
                                        <asp:Literal ID="Literal22" runat="server" Text="分期利率"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--開始日期-->
                                        <asp:Literal ID="Literal23" runat="server" Text="開始日期"></asp:Literal>
                                    </th>
                                    <th scope="col" nowrap="nowrap">
                                        <!--結束日期-->
                                        <asp:Literal ID="Literal3" runat="server" Text="結束日期"></asp:Literal>
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
                                    <td colspan="11" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" CommandName="Edit" 
                                            Text="<%$ Resources:WebResources, Edit %>" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" 
                                            Text="<%$ Resources:WebResources, Save %>" />
                                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" 
                                            Text="<%$ Resources:WebResources, Cancel %>" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnSave1" runat="server" CommandName="Save" 
                                            OnClick="btnSave1_Click" Text="<%$ Resources:WebResources, Save %>" />
                                        <asp:Button ID="btnCancel1" runat="server" OnClick="btnCancel1_Click" 
                                            Text="<%$ Resources:WebResources, Cancel %>" />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="項次">
                                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                    <EditItemTemplate><%# Container.DataItemIndex + 1 %></EditItemTemplate>
                                    <ControlStyle Width="40px" />
                                    <FooterStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Status" HeaderText="狀態" ReadOnly="true" FooterStyle-Wrap="true"
                                    ControlStyle-Width="40px" >
                                    <ControlStyle Width="40px" />
                                    <FooterStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="發卡銀行" FooterStyle-Wrap="true" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <%--<asp:DropDownList ID="ddlBank" runat="server">
                                            <asp:ListItem Text="台灣銀行" Value="台灣銀行"></asp:ListItem>
                                            <asp:ListItem Text="遠東銀行" Value="遠東銀行"></asp:ListItem>
                                            <asp:ListItem Text="中國信託" Value="中國信託"></asp:ListItem>
                                            <asp:ListItem Text="台新銀行" Value="台新銀行"></asp:ListItem>
                                            <asp:ListItem Text="花旗銀行" Value="花旗銀行"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbBank" runat="server" Text='<%# Bind("發卡銀行") %>' Visible="false"
                                            Width="40px" />--%>
                                            <%--<asp:LinkButton ID="lbtn1" runat="server" Text='<%# Bind("發卡銀行") %>' CommandName="select"
                                            Width="40px"></asp:LinkButton>--%>
                                            <asp:DropDownList ID="editDdlBankId" runat="server" />                                        
                                            <asp:Label ID="editBankId" runat="server" Text='<%# Bind("BANK_NAME") %>' 
                                            Width="40px" Visible="False" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="itemBankId" runat="server" Text='<%# Bind("BANK_NAME") %>' CommandName="select"
                                            Width="40px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="footerDdlBankId" runat="server">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                    <ControlStyle Width="80px" />
                                    <FooterStyle Wrap="True" />
                                </asp:TemplateField>
                                                             
                                                               
                                <asp:TemplateField HeaderText="分期期數" FooterStyle-Wrap="false" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("分期期數") %>' Width="25px"></asp:TextBox>--%>
                                        <asp:TextBox ID="editPaySeqment" runat="server" 
                                            Text='<%# Bind("PAY_SEQMENT") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="itemPaySeqment" runat="server" Text='<%# Bind("PAY_SEQMENT") %>' 
                                            Width="25px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="footerPaySeqment" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                    <ControlStyle Width="80px" />
                                    <FooterStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="分期利率" FooterStyle-Wrap="true" ControlStyle-Width="25px" FooterStyle-HorizontalAlign="Right">
                                    <EditItemTemplate>
                                        <%--<asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("分期利率") %>' Width="25px"></asp:TextBox>%--%>
                                        <asp:TextBox ID="editSeqmentRate" runat="server" 
                                            Text='<%# Bind("SEQMENT_RATE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="itemSeqmentRate" runat="server" 
                                            Text='<%# Bind("SEQMENT_RATE") %>'></asp:Label>%
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="footerSeqmentRate" runat="server" Width="60"></asp:TextBox>%
                                    </FooterTemplate>
                                    <ControlStyle Width="25px" />
                                    <FooterStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="開始日期" FooterStyle-Wrap="true" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editSDate" runat="server" Text='<%# Eval("S_DATE") %>'
                                            Width="40px" />                                        
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="itemSDate" runat="server" Text='<%# Bind("S_DATE") %>' 
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="footerSDate" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                    <ControlStyle Width="80px" />
                                    <FooterStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="結束日期" FooterStyle-Wrap="true" ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editEDate" runat="server" Text='<%# Bind("E_DATE") %>' 
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="itemEDate" runat="server" Text='<%# Bind("E_DATE") %>' 
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="footerEDate" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                    <ControlStyle Width="80px" />
                                    <FooterStyle Wrap="True" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="MODI_DTM" HeaderText="更新日期" ReadOnly="True" FooterStyle-Wrap="true"
                                    ControlStyle-Width="80px" >
                                    <ControlStyle Width="80px" />
                                    <FooterStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MODI_USER" HeaderText="更新人員" ReadOnly="true" FooterStyle-Wrap="true"
                                    ControlStyle-Width="80px" >
                                    <ControlStyle Width="80px" />
                                    <FooterStyle Wrap="True" />
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
                <div class="seperate">
                </div>
                <div id="dvDetail" runat="server" style="display: none">
                    <div class="GridScrollBar" style="height: 216px">
                        <div class="SubEditCommand">
                            <asp:Button ID="btnAdd2" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="btnAdd2_Click" />
                            <asp:Button ID="btnDelete2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        </div>
                        <cc1:ExGridView ID="gvDetail" runat="server"
                            DataKeyNames = "SETTLEMENT_ID"  
                            AutoGenerateColumns="False" CssClass="mGrid"                            
                            OnRowCancelingEdit="gvDetail_RowCancelingEdit" 
                            OnRowEditing="gvDetail_RowEditing"
                            OnRowUpdating="gvDetail_RowUpdating">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        <!--項次-->
                                        <asp:Literal ID="Literal5" runat="server" 
                                            Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--成本中心-->
                                        <asp:Literal ID="Literal10" runat="server" Text="成本中心"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--成本中心拆帳比率-->
                                        <asp:Literal ID="Literal8" runat="server" Text="成本中心拆帳比率"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="5" class="tdEmptyData">
                                        <!--查無資料，請修改條件，重新查詢-->
                                        <asp:Literal ID="Literal15" runat="server" 
                                            Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" 
                                            
                                            onclick="javascript:if(this.checked){$('#dvDetail').checkCheckboxes();}else{$('#dvDetail').unCheckCheckboxes();}" />
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
                                        <asp:Button ID="btnSave2" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                            CommandName="Save" 
                                            OnClick="btnSave2_Click" />
                                        <asp:Button ID="btnCancel2" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            OnClick="btnCancel2_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="項次" FooterStyle-Wrap="true" 
                                    ControlStyle-Width="40px">
                                    <EditItemTemplate>                                        
                                        <%# Container.DataItemIndex + 1 %>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%--<asp:TextBox ID="txtItem" runat="server" Width="60"></asp:TextBox>--%>                                        
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="成本中心" FooterStyle-Wrap="true" 
                                    ControlStyle-Width="80px">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="editDdlCostCenter" runat="server" />
                                        <asp:Label ID="editCostCenter" runat="server" Text='<%# Bind("COST_CENTER_NAME") %>' Visible="false" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="itemCostCenterName" runat="server" Text='<%# Bind("COST_CENTER_NAME") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="footerDdlCostCenter" runat="server" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="成本中心拆帳比率" FooterStyle-Wrap="true" 
                                    ControlStyle-Width="25px">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="editSettlementRate" runat="server" Text='<%# Bind("SETTLEMENT_RATE") %>' 
                                            Width="25px" />%
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("SETTLEMENT_RATE") %>' 
                                            Width="25px"></asp:Label>%
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="footerSettlementRate" runat="server" Width="80"></asp:TextBox>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
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
