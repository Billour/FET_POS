<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT17.aspx.cs" Inherits="VSS_OPT17_OPT17" %>
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
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--門市手開發票號碼設定-->
        <asp:Literal ID="Literal1" runat="server" Text="門市手開發票號碼設定"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox2" runat="server" Width="120"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                            OnClientClick="openwindow('../INV/INV18_3.aspx',640,300);return false;" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--所屬年月-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, YearMonth %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="5" nowrap="nowrap">
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server" class="SubEditBlock">
                    <div class="SubEditCommand">
                        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" />
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                    </div>
                    <cc1:ExGridView ID="gvMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="mGrid" PageSize="5" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging"
                        OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                        OnRowUpdating="gvMaster_RowUpdating">
                        <EmptyDataTemplate>
                            <tr>
                                <th scope="col">&nbsp;</th>
                                <th scope="col">&nbsp;</th>
                                <th scope="col" nowrap="nowrap">
                                    <!--項次-->
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--門市編號-->
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--門市名稱-->
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--發票格式-->
                                    <asp:Literal ID="Literal11" runat="server" Text="發票格式"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--所屬年月(起)-->
                                    <asp:Literal ID="Literal9" runat="server" Text="所屬年月(起)"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--所屬年月(訖)-->
                                    <asp:Literal ID="Literal22" runat="server" Text="所屬年月(訖)"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--字軌-->
                                    <asp:Literal ID="Literal23" runat="server" Text="字軌"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--起始編號-->
                                    <asp:Literal ID="Literal3" runat="server" Text="起始編號"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--終止編號-->
                                    <asp:Literal ID="Literal6" runat="server" Text="終止編號"></asp:Literal>
                                </th>
                                <th scope="col" nowrap="nowrap">
                                    <!--發票張數-->
                                    <asp:Literal ID="Literal7" runat="server" Text="發票張數"></asp:Literal>
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
                                <td colspan="14" class="tdEmptyData">
                                    <!--查無資料，請修改條件，重新查詢-->
                                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                </td>
                            </tr>
                        </EmptyDataTemplate>
                        
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckAll" runat="server" CssClass="Class1" onclick="javascript:if(this.checked){$('.Class1').checkCheckboxes();}else{$('.Class1').unCheckCheckboxes();}"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckItem" runat="server" CssClass="Class1" />
                                </ItemTemplate>                                
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
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" CommandName="Save" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClick="btnCancel_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true"  FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>
                            <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" ReadOnly="true"  FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>
                            <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" ReadOnly="true"  FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>

                            <asp:TemplateField HeaderText="發票格式" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlInvoice" runat="server">
                                        <asp:ListItem Text="手開二聯式" Value="手開二聯式"></asp:ListItem>
                                        <asp:ListItem Text="手開三聯式" Value="手開三聯式"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbInvoice1" runat="server" Text='<%# Bind("發票格式") %>' Visible="false"
                                        Width="40px" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbInvoice2" runat="server" Text='<%# Bind("發票格式") %>' Width="40px" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlInvoice" runat="server">
                                        <asp:ListItem Text="手開二聯式" Value="手開二聯式"></asp:ListItem>
                                        <asp:ListItem Text="手開三聯式" Value="手開三聯式"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbInvoice1" runat="server" Text='<%# Bind("發票格式") %>' Visible="false"
                                        Width="40px" />
                                </FooterTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="所屬年月(起)" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Bind("所屬年月起") %>' Width="80"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("所屬年月起") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:TextBox ID="txtStartDate" runat="server" Width="80"></asp:TextBox>
                                </FooterTemplate>
                           </asp:TemplateField>     
                           <asp:TemplateField HeaderText="所屬年月(訖)" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("所屬年月訖") %>' Width="80"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("所屬年月訖") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEndDate" runat="server" Width="80"></asp:TextBox>
                                </FooterTemplate>
                           </asp:TemplateField>                            
                           <asp:TemplateField HeaderText="字軌" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtInitial" runat="server" Text='<%# Bind("字軌") %>' Width="40"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblInitial" runat="server" Text='<%# Eval("字軌") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInitial" runat="server" Width="40"></asp:TextBox>
                                </FooterTemplate>
                           </asp:TemplateField> 
                           <asp:TemplateField HeaderText="起始編號" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStartNumber" runat="server" Text='<%# Bind("起始編號") %>' Width="80"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStartNumber" runat="server" Text='<%# Eval("起始編號") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:TextBox ID="txtStartNumber" runat="server" Width="80"></asp:TextBox>
                                </FooterTemplate>
                           </asp:TemplateField>      
                           
                           <asp:TemplateField HeaderText="終止編號" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEndNumber" runat="server" Text='<%# Bind("終止編號") %>' Width="80"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEndNumber" runat="server" Text='<%# Eval("終止編號") %>'></asp:Label>
                                </ItemTemplate>                        
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEndNumber" runat="server" Width="80"></asp:TextBox>
                                </FooterTemplate>
                           </asp:TemplateField>   
                           
                           <asp:TemplateField HeaderText="發票張數" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                <EditItemTemplate>
                                    <asp:Label ID="lblQty_1" runat="server" Text='<%# Eval("發票張數") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("發票張數") %>'></asp:Label>
                                </ItemTemplate>                        

                           </asp:TemplateField>

                           <asp:BoundField DataField="更新日期" HeaderText="<%$ Resources:WebResources, ModifiedDate %>"  ReadOnly="true" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>
                           <asp:BoundField DataField="更新人員" HeaderText="<%$ Resources:WebResources, ModifiedBy %>"  ReadOnly="true" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"/>
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
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
