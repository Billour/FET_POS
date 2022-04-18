<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV05_Import.aspx.cs" Inherits="VSS_INV_INV05_Import" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">               
            <div class="criteria">
                <table>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload" runat="server" />
                        </td>                                                
                    </tr>                    
                </table>
            </div>
             <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click" />
                <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
            </div>
            <div class="seperate">
            </div>            
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="100%" CssClass="visoft__tab_xpie7">
                <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <span><!--商品-->
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Product %>"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>             
                        <div id="Div1" class="SubEditBlock">                        
                            <div class="GridScrollBar" style="height: auto">
                                <cc1:ExGridView ID="gvProduct" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowDataBound="GridView_RowDataBound">
                                    <EmptyDataTemplate>
                                        <tr>                                        
                                            <th scope="col">
                                                <!--商品編號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--商品名稱-->
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--異常原因-->
                                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ErrorDescription %>"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="tdEmptyData">                                                    
                                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoRecordsImported %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>                                                                                                                                   
                                        <asp:BoundField DataField="商品料號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true"/>
                                        <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true"/>
                                        <%--<asp:BoundField DataField="異常原因" HeaderText="<%$ Resources:WebResources, ErrorDescription %>" ReadOnly="true"/>--%>
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ErrorDescription %>" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("異常原因") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>                                        
                                </cc1:ExGridView>
                            </div>
                        </div>                        
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server">
                    <HeaderTemplate>
                        <span><!--門市-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Store %>"></asp:Literal></span>
                    </HeaderTemplate>
                    <ContentTemplate>             
                        <div id="Div2" class="SubEditBlock">                        
                            <div class="GridScrollBar" style="height: auto">
                                <cc1:ExGridView ID="gvStore" runat="server" AutoGenerateColumns="False" CssClass="mGrid" OnRowDataBound="GridView_RowDataBound">
                                    <EmptyDataTemplate>
                                        <tr>                                        
                                            <th scope="col">
                                                <!--門市編號-->
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--門市名稱-->
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>
                                            </th>
                                            <th scope="col">
                                                <!--異常原因-->
                                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ErrorDescription %>"></asp:Literal>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="tdEmptyData">                                                    
                                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoRecordsImported %>"></asp:Literal>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <Columns>                                                                                                                                   
                                        <asp:BoundField DataField="門市編號" HeaderText="<%$ Resources:WebResources, StoreNo %>" ReadOnly="true"/>
                                        <asp:BoundField DataField="門市名稱" HeaderText="<%$ Resources:WebResources, StoreName %>" ReadOnly="true"/>
                                        <%--<asp:BoundField DataField="異常原因" HeaderText="<%$ Resources:WebResources, ErrorDescription %>" ReadOnly="true"/>--%>
                                        <asp:TemplateField HeaderText="<%$ Resources:WebResources, ErrorDescription %>" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("異常原因") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>                                        
                                </cc1:ExGridView>
                            </div>
                        </div>                        
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <div class="seperate"></div>
            <div class="btnPosition">                
                <asp:Button ID="btnCommit" runat="server" 
                    Text="<%$ Resources:WebResources, CommitUpload %>" onclick="btnCommit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"  OnClientClick="window.close();return false;" />                    
            </div>
        </div>
    </form>
</body>
</html>
