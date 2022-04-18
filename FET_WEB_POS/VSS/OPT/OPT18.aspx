<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT18.aspx.cs" Inherits="VSS_OPT_OPT18" enableEventValidation="false" %>
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
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--門市店長折扣設定-->
        <asp:Literal ID="Literal1" runat="server" Text="門市店長折扣設定"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server" Width="120"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                            OnClientClick="openwindow('../INV/INV18_3.aspx',640,300);return false;" />
                    </td>
                    <td class="tdtxt">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--折扣月份-->
                        <asp:Literal ID="Literal4" runat="server" Text="折扣月份"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="5">
                        <!--起-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                        <!--訖-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        <cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
   
        <div id="Div1" runat="server" class="SubEditBlock">
            <div class="SubEditCommand">
                <asp:Button ID="btnAdd1" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd1_Click" />
                <asp:Button ID="btnDelete1" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                OnRowUpdating="gvMaster_RowUpdating">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col">&nbsp;</th>
                        <th scope="col">&nbsp;</th>
                        <th scope="col">
                            <!--項次-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--門市編號-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--門市名稱-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--折扣月份-->
                            <asp:Literal ID="Literal9" runat="server" Text="折扣月份"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--折扣總額-->
                            <asp:Literal ID="Literal22" runat="server" Text="折扣總額"></asp:Literal>
                        </th>                                                           
                        <th scope="col">
                            <!--更新人員-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--更新日期-->
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                        </th>
                    </tr>
                    <tr id="trEmptyData" runat="server">
                        <td colspan="9" class="tdEmptyData">
                            <!--查無資料，請修改條件，重新查詢-->
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript:if(this.checked){$('#Div1').checkCheckboxes();}else{$('#Div1').unCheckCheckboxes();}" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckItem" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                              <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnSave1" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel1_Click" />
                            <asp:Button ID="btnCancel1" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel1_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>"
                        ReadOnly="true" />
                    <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>"
                        ReadOnly="true" />
                    <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>"
                        ReadOnly="true" />
                    
                     <asp:TemplateField  HeaderText="折扣月份">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDiscountMonth" runat="server" Text='<%# Bind("折扣月份") %>' Width="80"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDiscountMonth" runat="server" Text='<%# Eval("折扣月份") %>'></asp:Label>
                        </ItemTemplate>                        
                        <FooterTemplate>
                           <asp:TextBox ID="txtDiscountMonth" runat="server" Width="80"></asp:TextBox>
                        </FooterTemplate>
                   </asp:TemplateField>  
                     <asp:TemplateField  HeaderText="折扣總額">
                        <EditItemTemplate>
                           <asp:TextBox ID="txtTotalDiscount" runat="server" Text='<%# Bind("折扣總額") %>' Width="80"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTotalDiscount" runat="server" Text='<%# Eval("折扣總額") %>'></asp:Label>
                        </ItemTemplate>                        
                        <FooterTemplate>
                           <asp:TextBox ID="txtTotalDiscount" runat="server" Width="80"></asp:TextBox>
                        </FooterTemplate>
                   </asp:TemplateField>                                                             
                    <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>"  ReadOnly="true"/>
                    <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>"  ReadOnly="true"/>
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

        <div id="Div2" runat="server" class="SubEditBlock">
            <div class="SubEditCommand">
                <asp:Button ID="btnAdd2" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd2_Click" />
                <asp:Button ID="btnDelete2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
            </div>
            <cc1:ExGridView ID="gvDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                OnRowCancelingEdit="gvDetails_RowCancelingEdit" OnRowEditing="gvDetails_RowEditing" 
                OnRowUpdating="gvDetails_RowUpdating">
                <EmptyDataTemplate>
                    <tr>
                        <th scope="col">&nbsp;</th>
                        <th scope="col">&nbsp;</th>
                        <th scope="col">
                            <!--項次-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--角色-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Role %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--金額-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Amount %>"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--比率-->
                            <asp:Literal ID="Literal9" runat="server" Text="折扣月份"></asp:Literal>
                        </th>
                        <th scope="col">
                            <!--折扣上限金額-->
                            <asp:Literal ID="Literal22" runat="server" Text="折扣總額"></asp:Literal>
                        </th>                                                                                  
                    </tr>
                    <tr id="trEmptyData" runat="server">
                        <td colspan="7" class="tdEmptyData">
                            <!--查無資料，請修改條件，重新查詢-->
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                        </td>
                    </tr>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript:if(this.checked){$('#Div2').checkCheckboxes();}else{$('#Div2').unCheckCheckboxes();}" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckItem" runat="server" />
                        </ItemTemplate> 
                        <FooterTemplate>&nbsp;</FooterTemplate>                       
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>" CommandName="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                             <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Update" />
                              <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CommandName="Cancel" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnSave2" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel2_Click" />
                            <asp:Button ID="btnCancel2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel2_Click" />
                        </FooterTemplate>
                    </asp:TemplateField>
                                                                 
                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>"
                        ReadOnly="true" />                        
                   <asp:TemplateField  HeaderText="<%$ Resources:WebResources, Role %>">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Text="店員" Value="店員"></asp:ListItem>
                                <asp:ListItem Text="店長" Value="店長"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server" Text='<%# Eval("角色") %>'></asp:Label>
                        </ItemTemplate>                        
                        <FooterTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Text="店員" Value="店員"></asp:ListItem>
                                <asp:ListItem Text="店長" Value="店長"></asp:ListItem>
                            </asp:DropDownList>      
                        </FooterTemplate>
                   </asp:TemplateField>      
                   
                   <asp:TemplateField  HeaderText="<%$ Resources:WebResources, Amount %>">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("金額") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("金額") %>'></asp:Label>
                        </ItemTemplate>                        
                        <FooterTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("金額") %>'></asp:TextBox>
                        </FooterTemplate>
                   </asp:TemplateField>                                                                           
                    
                    <asp:TemplateField  HeaderText="<%$ Resources:WebResources, Ratio %>">
                        <EditItemTemplate>
                            <asp:TextBox ID="textBox1" runat="server" Text='<%# Bind("比率") %>'></asp:TextBox>%
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRatio" runat="server" Text='<%# Eval("比率", "{0:D}%") %>'></asp:Label>
                        </ItemTemplate>                        
                        <FooterTemplate>
                            <asp:TextBox ID="textBox1" runat="server" Text='<%# Bind("比率") %>'></asp:TextBox>%
                        </FooterTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField  HeaderText="折扣上限金額">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDiscount" runat="server" Text='<%# Bind("折扣上限金額") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("折扣上限金額") %>'></asp:Label>
                        </ItemTemplate>                        
                        <FooterTemplate>
                            <asp:TextBox ID="txtDiscount" runat="server" Text='<%# Bind("折扣上限金額") %>'></asp:TextBox>
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
        </div>
           
    </div>
    </form>
</body>
</html>

