<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT15.aspx.cs" Inherits="VSS_OPT_OPT15" %>

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
                        <!--HG點數兌換-來店禮-->
                        <asp:Literal ID="Literal1" runat="server" Text="HappyGo點數兌換-來店禮"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--開始日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="開始日期"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                            起<cc1:postbackDate_TextBox ID="postbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                            &nbsp;訖<cc1:postbackDate_TextBox ID="postbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg" />
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
                        <!--活動代號-->
                        <asp:Literal ID="Literal5" runat="server" Text="活動代號"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        &nbsp;<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--活動名稱-->
                        <asp:Literal ID="Literal8" runat="server" Text="活動名稱"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">            
                    </td>
                    <td class="tdval">        
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
                    </td>
                    <td class="tdval">                    
                    </td>
                </tr>
            </table>
        </div>        
        <div class="btnPosition">
            <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
            <asp:Button ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
        </div>
        <div class="seperate">
        </div>
        <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="Div1" class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                 OnClick="btnAdd_Click"  />
                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        </div>
  
                            <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                OnRowUpdating="gvMaster_RowUpdating" OnRowCommand="gvMaster_RowCommand"
                                 AllowPaging="true" PageSize="3" PagerStyle-HorizontalAlign="Right" 
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
                                            <!--項次-->
                                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                        </th>     
                                        <th scope="col" nowrap="nowrap">
                                            <!--活動代號-->
                                            <asp:Literal ID="Literal10" runat="server" Text="活動代號"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--活動名稱-->
                                            <asp:Literal ID="Literal11" runat="server" Text="活動名稱"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--開始日期-->
                                            <asp:Literal ID="Literal12" runat="server" Text="開始日期"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--結束日期-->
                                            <asp:Literal ID="Literal13" runat="server" Text="結束日期"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--類別-->
                                            <asp:Literal ID="Literal14" runat="server" Text="類別"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--商品料號-->
                                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--點數-->
                                            <asp:Literal ID="Literal16" runat="server" Text="點數"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--名單檢核-->
                                            <asp:Literal ID="Literal17" runat="server" Text="名單檢核"></asp:Literal>
                                        </th>
                                        <th scope="col" nowrap="nowrap">
                                            <!--兌換次數-->
                                            <asp:Literal ID="Literal19" runat="server" Text="兌換次數"></asp:Literal>
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
                                            請點選新增按鍵增加資料
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>

                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="SHERRY" onclick="javascript:if(this.checked){$('.SHERRY').checkCheckboxes();}else{$('.SHERRY').unCheckCheckboxes();}" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox2" runat="server" CssClass="SHERRY" />
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

                                    <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                                        
                                    <asp:TemplateField HeaderText="活動代號" ControlStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("活動代號") %>' CommandName="select"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="活動名稱" ControlStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPName" runat="server" Text='<%# Bind("活動名稱") %>' Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPName" runat="server" Text='<%# Eval("活動名稱") %>'></asp:Label>
                                        </ItemTemplate>                        
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtPName" runat="server" Width="80px"></asp:TextBox>
                                        </FooterTemplate>
                                   </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="開始日期">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Bind("開始日期") %>' Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("開始日期") %>'></asp:Label>
                                        </ItemTemplate>                        
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtStartDate" runat="server" Width="80px"></asp:TextBox>
                                        </FooterTemplate>
                                   </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="結束日期">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("結束日期") %>' Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("結束日期") %>'></asp:Label>
                                        </ItemTemplate>                        
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtEndDate" runat="server" Width="80px"></asp:TextBox>
                                        </FooterTemplate>
                                   </asp:TemplateField> 


                                    <asp:TemplateField HeaderText="類別" ControlStyle-Width="70px">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlCategory" runat="server" >
                                                <asp:ListItem>點數</asp:ListItem>            
                                                <asp:ListItem>商品</asp:ListItem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("類別") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlCategory" runat="server" >
                                                <asp:ListItem>點數</asp:ListItem>            
                                                <asp:ListItem>商品</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="商品料號" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt2" runat="server" Text='<%# Bind("商品料號") %>' Width="50px"></asp:TextBox>
                                            <asp:Button ID="Button66" runat="server" Text="選" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl2" runat="server" Text='<%# Eval("商品料號") %>'></asp:Label>
                                        </ItemTemplate>                        
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt2" runat="server" Width="50px"></asp:TextBox>
                                            <asp:Button ID="Button66" runat="server" Text="選" OnClientClick="openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);" />
                                        </FooterTemplate>
                                   </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="點數" ControlStyle-Width="40px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt3" runat="server" Text='<%# Bind("點數") %>' Width="30px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl3" runat="server" Text='<%# Eval("點數") %>'></asp:Label>
                                        </ItemTemplate>                        
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt3" runat="server" Width="30px"></asp:TextBox>
                                        </FooterTemplate>
                                   </asp:TemplateField> 

                                    <asp:TemplateField HeaderText="名單檢核" ControlStyle-Width="80px">  
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox3" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="折抵次數" ControlStyle-Width="80px">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt5" runat="server" Text='<%# Bind("兌換次數") %>' Width="30px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl5" runat="server" Text='<%# Eval("兌換次數") %>'></asp:Label>
                                        </ItemTemplate>                        
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt5" runat="server" Width="30px"></asp:TextBox>
                                        </FooterTemplate>
                                   </asp:TemplateField> 

                                    <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedDate %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedBy %>" ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />  
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
                         Width="100%" CssClass="visoft__tab_xpie7" Visible="False" >
                    <asp:TabPanel ID="TabPanel1" runat="server">
                         <HeaderTemplate>
                            <span><!--通路設定-->
                                 <asp:Literal ID="Literal22" runat="server" Text="通路設定"></asp:Literal>
                            </span>
                        </HeaderTemplate>
                    <ContentTemplate>
                     <div class="SubEditBlock"  >
                        <div class="SubEditCommand">
                            <asp:Button ID="Button3" runat="server" 
                                Text="<%$ Resources:WebResources, Add %>" onclick="Button3_Click" />
                            <asp:Button ID="Button4" runat="server" 
                                Text="<%$ Resources:WebResources, Delete %>" />
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Text="區域" Value="區域" />
                                <asp:ListItem Text="ALL" Value="ALL" />
                                <asp:ListItem Text="北一區" Value="北一區" />
                                <asp:ListItem Text="中一區" Value="中一區" />
                                <asp:ListItem Text="南一區" Value="南一區" />
                            </asp:DropDownList>
                            <asp:Button ID="Button5" runat="server" Text="確認" />      
                        </div> 
                        <div id="Div2" class="GridScrollBar" style="height:250px">
                                <cc1:ExGridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing"
                                                OnRowUpdating="gvDetail_RowUpdating">
                                    <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckBox4" runat="server"  onclick="javascript:if(this.checked){$('#Div2').checkCheckboxes();}else{$('#Div2').unCheckCheckboxes();}" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox5" runat="server" />
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
                                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click1" />
                                                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click1" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" />  

                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, StoreNo %>">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("門市編號") %>'></asp:TextBox>
                                                <asp:Button ID="Button6" runat="server" Text="選" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx',500,400);" />
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("門市編號") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="TextBox6" runat="server" Width="150px"></asp:TextBox>
                                                <asp:Button ID="Button6" runat="server" Text="選" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx',500,400);" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>"
                                            ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                        <asp:BoundField DataField="區域別" HeaderText="區域別"
                                            ReadOnly="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                      
                                    </Columns>
                                </cc1:ExGridView> 
                            </div>  
                        </div>
                    </ContentTemplate>
                    </asp:TabPanel>
           
                    </asp:TabContainer>
      </ContentTemplate>
   </asp:UpdatePanel>
        <div class="seperate">
        </div>
    </div>
    </div>
    </form>
</body>
</html>
