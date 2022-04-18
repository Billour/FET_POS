<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV18_5.aspx.cs" Inherits="VSS_INV_INV18_5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                        <!--庫存調整作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockAdjustment %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClientClick="document.location='INV18.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--調整單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">                         
                            <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>                                
                        </td>
                        <td class="tdtxt">
                            <!--調整日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label2" runat="server" Text="2010/07/01"></asp:Label>                            
                        </td>
                        <td class="tdtxt">
                            <!--更新日期-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--調整門市-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="lblOrderNo0" runat="server" Text="門市A" Width="110"></asp:Label>                            
                        </td>
                        <td class="tdtxt">
                            <!--調整人員-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, AdjustedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label1" runat="server" Text="64591 李家駿" Width="110"></asp:Label>                           
                        </td>
                        <td class="tdtxt">
                            <!--更新人員-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--備註-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <asp:Label ID="TextBox3" runat="server" Width="100%"></asp:Label>
                        </td>
                        <td class="tdtxt">
                            &nbsp;
                        </td>
                        <td class="tdval">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>                    
                <div id="divContent" runat="server" class="SubEditBlock">
                    <div class="GridScrollBar" style="height: 262px">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid" AllowPaging="True"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating" PagerStyle-HorizontalAlign="Right" OnPageIndexChanging="GridView_PageIndexChanging">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        <!--項次-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--來源倉-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SourceWarehouse %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--目的倉-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, DestinationWarehouse %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--調整量-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, AdjuestmentQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--調整原因-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ReasonForAdjustment %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="59" class="tdEmptyData">                                        
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>                                                           
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" ReadOnly="true" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="label" runat="server" Text='<%# Bind("商品編號") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>                                        
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, SourceWarehouse %>">
                                    <ItemTemplate>
                                        <asp:Label ID="label11" runat="server" Text='<%# Eval("來源倉2") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>                                        
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DestinationWarehouse %>">
                                    <ItemTemplate>
                                        <asp:Label ID="label22" runat="server" Text='<%# Eval("目的倉2") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>                                       
                                </asp:TemplateField>
                                <asp:BoundField DataField="調整量" HeaderText="<%$ Resources:WebResources, AdjuestmentQuantity %>" />
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ReasonForAdjustment %>">                                       
                                    <ItemTemplate>
                                        <asp:Label ID="TextBox1" runat="server" Text='<%# Bind("調整原因") %>' />
                                    </ItemTemplate>
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
                            <PagerStyle HorizontalAlign="Right" />
                        </asp:GridView>
                    </div>
                </div>                
        </div>
    </div>
    </form>
</body>
</html>
