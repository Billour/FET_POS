<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT13_1.aspx.cs" Inherits="VSS_OPT_OPT13_1" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HappyGo活動兌點限制－促銷活動</title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--HG活動兌點限制－促銷活動-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoRedeemPointsForEvent %>"></asp:Literal>
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
                    <td class="tdtxt" nowrap="nowrap">
                        <!--活動代號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ActivityNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--活動名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ActivityName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開始日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        &nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>                    
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷代號-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="txtPromoCode1" runat="server"></asp:TextBox><asp:Button ID="btnChoosePromoCode1" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                         <uc1:PopupWindow ID="PopupWindow1" runat="server"
                                    Name="SelectPromoCode" 
                                    PopupButtonID="btnChoosePromoCode1" 
                                    TargetControlID="txtPromoCode1"
                                    Width="550" Height="420"                       
                                    NavigateUrl="~/VSS/SAL/SAL01_choosePromotions.aspx" />
                        &nbsp;<asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="txtPromoCode2" runat="server"></asp:TextBox><asp:Button ID="btnChoosePromoCode2" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                         <uc1:PopupWindow ID="PopupWindow2" runat="server"
                                    Name="SelectPromoCode" 
                                    PopupButtonID="btnChoosePromoCode2" 
                                    TargetControlID="txtPromoCode2"
                                    Width="550" Height="420"                       
                                    NavigateUrl="~/VSS/SAL/SAL01_choosePromotions.aspx" />
                    </td>                   
                    <td class="tdtxt" nowrap="nowrap">
                        <!--促銷名稱-->
                        <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>
                </tr>                                
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate"></div>
        <div>            
            <div id="Div1" class="SubEditBlock">
                <div class="SubEditCommand">
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                        OnClick="btnAdd_Click" />
                    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                </div>                
                    <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                        OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                        OnRowUpdating="gvMaster_RowUpdating" OnRowCommand="gvMaster_RowCommand" OnRowDataBound="gvMaster_RowDataBound"
                        OnRowCreated="gvMaster_RowCreated" AllowPaging="true" PageSize="2" PagerStyle-HorizontalAlign="Right"
                        OnPageIndexChanging="GridView_PageIndexChanging">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col" nowrap="nowrap">
                                    &nbsp;
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    &nbsp;
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--活動代號-->
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ActivityNo %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--活動名稱-->
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ActivityName %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--促銷代號-->
                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--促銷名稱-->
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--開始日期-->
                                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--結束日期-->
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--名單檢核-->
                                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, NameListVerification %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--折抵方式-->
                                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, RedemptionMethod %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--折抵上限-->
                                    <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, RedemptionLimit %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--折抵次數-->
                                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, RedeemingFrequency %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--更新日期-->
                                    <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--更新人員-->
                                    <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                                </th>
                            </tr>
                            <tr id="trEmptyData" runat="server">
                                <td colspan="14" class="tdEmptyData">
                                    <!--請點選新增按鍵增加資料-->
                                    <asp:Literal ID="Literal30" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="chk" onclick="javascript:if(this.checked){$('.chk').checkCheckboxes();}else{$('.chk').unCheckCheckboxes();}"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" CssClass="chk" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                     <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                      <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ActivityNo %>" ControlStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("活動代號") %>' CommandName="select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="<%$ Resources:WebResources, ActivityName %>" HeaderText="活動名稱" ReadOnly="true" 
                                ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" >
                            </asp:BoundField>
                            
                           <asp:TemplateField HeaderText="<%$ Resources:WebResources, PromotionCode %>" ItemStyle-Wrap="false" FooterStyle-Wrap="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPromoCode" runat="server" Text='<%# Bind("促銷代號") %>' Width="80"></asp:TextBox><asp:Button ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                                    <uc1:PopupWindow ID="PopupWindow3" runat="server"
                                    Name="DeliveryOrderNoSearch" 
                                    PopupButtonID="ChooseButton1" 
                                    TargetControlID="txtPromoCode"
                                    Width="300" Height="300"                       
                                    NavigateUrl="~/VSS/SAL/SAL01_choosePromotions.aspx" />                                    
                                </EditItemTemplate>
                                <ItemTemplate >
                                    <asp:Label ID="lblPromoCode" runat="server" Text='<%# Bind("促銷代號") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPromoCode" runat="server" Width="80"></asp:TextBox><asp:Button ID="ChooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                                    <uc1:PopupWindow ID="PopupWindow3" runat="server"
                                    Name="DeliveryOrderNoSearch" 
                                    PopupButtonID="ChooseButton1" 
                                    TargetControlID="txtPromoCode"
                                    Width="300" Height="300"                       
                                    NavigateUrl="~/VSS/SAL/SAL01_choosePromotions.aspx" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="促銷名稱" HeaderText="<%$ Resources:WebResources, PromotionName %>" ReadOnly="true" 
                                ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />                                                        
                            <asp:BoundField DataField="開始日期" HeaderText="<%$ Resources:WebResources, StartDate %>" ReadOnly="true" 
                                ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                            <asp:BoundField DataField="結束日期" HeaderText="<%$ Resources:WebResources, EndDate %>" ReadOnly="true" 
                                ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />                           
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, NameListVerification %>" ControlStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox3" runat="server" Enabled="false" />
                                    <asp:HiddenField ID="名單檢核" runat="server" Value='<%# Bind("名單檢核") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, RedemptionMethod %>" ControlStyle-Width="80px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("折抵方式") %>' Width="40px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate >
                                    <asp:Label ID="Label30" runat="server" Text='<%# Bind("折抵方式") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="40"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, RedemptionLimit %>" ControlStyle-Width="80px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("折抵上限") %>' Width="50px" ></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label31" runat="server" Text='<%# Bind("折抵上限") %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Width="50px" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, RedeemingFrequency %>" ControlStyle-Width="80px">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("折抵次數") %>' Width="50px" ></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label33" runat="server" Text='<%# Bind("折抵次數") %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Width="50px" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedDate %>"
                                ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" 
                                HeaderStyle-Wrap="false" >
                            </asp:BoundField>
                            <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedBy %>"
                                ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" 
                                HeaderStyle-Wrap="false" >
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
            <div class="seperate">
            </div>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="100%" CssClass="visoft__tab_xpie7" Visible="false">
                <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--兌點設定-->
                            <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, RedemptionSetting %>"></asp:Literal>
                        </span>                        
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="SubEditBlock">
                            <div class="SubEditCommand">
                                <asp:Button ID="btnAddDetail1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                    Enabled="True" OnClick="btnAddDetail1_Click" />
                                <asp:Button ID="Button8" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                    Enabled="True" />                                
                            </div>
                            <div id="Div5" class="GridScrollBar" style="height:300px">
                                <cc1:ExGridView ID="gvDetail1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    OnRowCancelingEdit="gvDetail1_RowCancelingEdit" OnRowEditing="gvDetail1_RowEditing"
                                    OnRowUpdating="gvDetail1_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" CssClass="chk1" onclick="javascript:if(this.checked){$('.chk1').checkCheckboxes();}else{$('.chk1').unCheckCheckboxes();}"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" CssClass="chk1" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                 <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                                  <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancelAddDetail1_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancelAddDetail1_Click" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" />                                                                                                                               
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, RedemptionName %>">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRedemptionName" runat="server" Text='<%# Bind("兌點名稱") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRedemptionName" runat="server" Text='<%# Eval("兌點名稱") %>'></asp:Label>
                                            </ItemTemplate>                        
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtRedemptionName" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField> 


                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, Points %>" ControlStyle-Width="80">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPoints" runat="server" Text='<%# Bind("點數") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPoints" runat="server" Text='<%# Eval("點數") %>'></asp:Label>
                                            </ItemTemplate>                        
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPoints" runat="server" Width="60"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField> 
                                        
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, RedemptionAmount %>" ControlStyle-Width="80">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt13" runat="server" Text='<%# Bind("兌換金額") %>' Width="60"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl13" runat="server" Text='<%# Eval("兌換金額") %>'></asp:Label>
                                            </ItemTemplate>                        
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt13" runat="server" Width="60"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField> 

                                    </Columns>
                                </cc1:ExGridView> 
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--指定門市-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, SpecifyStore %>"></asp:Literal>
                        </span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="SubEditBlock">
                            <div class="SubEditCommand">
                                <asp:Button ID="btnAddDetail2" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                    Enabled="True" OnClick="btnAddDetail2_Click" />
                                <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                    Enabled="True" />
                                <asp:DropDownList ID="DropDownList2" runat="server" Enabled="True">
                                    <asp:ListItem Text="區域" Value="區域" />
                                    <asp:ListItem Text="ALL" Value="ALL" />
                                    <asp:ListItem Text="北一區" Value="北一區" />
                                    <asp:ListItem Text="中一區" Value="中一區" />
                                    <asp:ListItem Text="南一區" Value="南一區" />
                                </asp:DropDownList>
                                <asp:Button ID="Button5" runat="server" Text="<%$ Resources:WebResources, SubmitDistrict %>" Enabled="True" />
                            </div>
                            <div id="Div3" class="GridScrollBar" style="height:300px">
                                <cc1:ExGridView ID="gvDetail2" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    OnRowCancelingEdit="gvDetail2_RowCancelingEdit" OnRowEditing="gvDetail2_RowEditing"
                                    OnRowUpdating="gvDetail2_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" CssClass="chk2" onclick="javascript:if(this.checked){$('.chk2').checkCheckboxes();}else{$('.chk2').unCheckCheckboxes();}"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" CssClass="chk2" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                 <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                                  <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancelAddDetail2_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancelAddDetail2_Click" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("門市編號") %>'></asp:TextBox>
                                                <asp:Button ID="Button12" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx',500,400);" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("門市編號") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreName %>" ControlStyle-Width="80">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt12" runat="server" Text='<%# Bind("門市名稱") %>' Width="60"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl12" runat="server" Text='<%# Eval("門市名稱") %>'></asp:Label>
                                            </ItemTemplate>                        
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt12" runat="server" Width="60"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField> 


                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ByDistrict %>" ControlStyle-Width="80">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt13" runat="server" Text='<%# Bind("區域別") %>' Width="60"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl13" runat="server" Text='<%# Eval("區域別") %>'></asp:Label>
                                            </ItemTemplate>                        
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt13" runat="server" Width="60"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField> 

                                    </Columns>
                                </cc1:ExGridView> 
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server">
                    <HeaderTemplate>
                        <span>
                            <!--加購價-->
                            <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, AdditionalCharges %>"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="SubEditBlock">
                            <div class="SubEditCommand">
                                <asp:Button ID="Button10" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                    OnClick="btnAddDetail3_Click" />
                                <asp:Button ID="Button11" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                            </div>
                            <div id="Div2" class="GridScrollBar" style="height:300px">
                                <cc1:ExGridView ID="gvDetail3" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                    OnRowCancelingEdit="gvDetail3_RowCancelingEdit" OnRowEditing="gvDetail3_RowEditing"
                                    OnRowUpdating="gvDetail3_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" CssClass="chk3" onclick="javascript:if(this.checked){$('.chk3').checkCheckboxes();}else{$('.chk3').unCheckCheckboxes();}"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" CssClass="chk3" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                 <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                                                  <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancelAddDetail3_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancelAddDetail3_Click" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProductCode" runat="server" Text='<%# Bind("商品料號") %>'></asp:TextBox><asp:Button ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                                                <uc1:PopupWindow ID="PopupWindow2" runat="server"
                                                    Name="SelectPromoCode" 
                                                    PopupButtonID="btnChooseProduct" 
                                                    TargetControlID="txtProductCode"
                                                    Width="500" Height="400"                       
                                                    NavigateUrl="~/VSS/SAL/SAL01_searchProductNo.aspx" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("商品料號") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtProductCode" runat="server" Text='<%# Bind("商品料號") %>'></asp:TextBox><asp:Button ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>" />
                                                <uc1:PopupWindow ID="PopupWindow2" runat="server"
                                                    Name="SelectPromoCode" 
                                                    PopupButtonID="btnChooseProduct" 
                                                    TargetControlID="txtProductCode"
                                                    Width="500" Height="400"                       
                                                    NavigateUrl="~/VSS/SAL/SAL01_searchProductNo.aspx" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductName %>">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt1" runat="server" Text='<%# Bind("商品名稱") %>' Width="80"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl1" runat="server" Text='<%# Eval("商品名稱") %>'></asp:Label>
                                            </ItemTemplate>                        
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt1" runat="server" Width="80"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, RedemptionPoints %>" ControlStyle-Width="80">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("兌換點數") %>' Width="60"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl2" runat="server" Text='<%# Eval("兌換點數") %>'></asp:Label>
                                            </ItemTemplate>                        
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt2" runat="server" Width="60"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, AdditionalCharges %>">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt3" runat="server" Text='<%# Bind("加購價") %>' Width="60"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl3" runat="server" Text='<%# Eval("加購價") %>'></asp:Label>
                                            </ItemTemplate>                        
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt3" runat="server" Width="60"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField> 

                                    </Columns>
                                </cc1:ExGridView> 
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>                                                                
            </asp:TabContainer>
            <div class="seperate">
            </div>

        </div>
    </div>
    </form>
</body>
</html>
