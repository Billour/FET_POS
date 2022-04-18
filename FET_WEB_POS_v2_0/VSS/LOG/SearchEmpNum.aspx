<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchEmpNum.aspx.cs" Inherits="VSS_LOG_SearchEmpNum" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>選擇員工編號</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--部門代碼-->
                            <asp:Literal ID="Literal1" runat="server" Text="部門代碼"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100"></dx:ASPxTextBox>                          
                        </td>
                        <td class="tdtxt">
                            <!--部門名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="部門名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox6" runat="server" Width="100"></dx:ASPxTextBox>
                        </td>                       
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--員工編號-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>"></asp:Literal>：</td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox7" runat="server" Width="100"></dx:ASPxTextBox>                            
                        </td>
                        <td class="tdtxt">
                            <!--員工姓名-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, EmployeeName %>"></asp:Literal>：</td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox8" runat="server" Width="100"></dx:ASPxTextBox>
                        </td>                        
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click"></dx:ASPxButton>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s, e){ form1.reset(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>        
            </div>
            <div class="seperate">
            </div>
            <div class="GridScrollBar" style="height: 214px">
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="員工編號" Width="100%"
                 OnSelectionChanged="gvMaster_SelectedIndexChanged" onpageindexchanged="gvMaster_PageIndexChanged">
                 <Columns>                       
                    <dx:GridViewDataCheckColumn VisibleIndex="0" Width="10">
                        <DataItemTemplate>
                            <input type="radio" name="RadioButton1" />
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>               
                    <dx:GridViewDataColumn FieldName="員工編號" Caption="<%$ Resources:WebResources, EmployeeNo %>" />
                    <dx:GridViewDataColumn FieldName="員工姓名" Caption="<%$ Resources:WebResources, EmployeeName %>" />  
                    <dx:GridViewDataColumn FieldName="部門" Caption="部門"/>                                     
                </Columns>
                <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="false" />
                <SettingsPager PageSize="5"></SettingsPager>               
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" /> 
                <ClientSideEvents
                            FocusedRowChanged="function(s, e) {
                               if(s.GetFocusedRowIndex() == -1)
                                    return;
                               var row = s.GetRow(s.GetFocusedRowIndex());
                               
                                if(__aspxIE)
                                    row.cells[0].childNodes[0].checked = true;
                                else
                                    row.cells[0].childNodes[1].checked = true;
                            }" />                                
                </cc:ASPxGridView>
            
               <%-- <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" 
                    CssClass="mGrid" AllowPaging="True" 
                    onselectedindexchanged="gvMaster_SelectedIndexChanged" PageSize="5">
                    <PagerStyle HorizontalAlign="Right" />
                 <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--員工編號-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>"></asp:Literal>
                            </th>
                            <th scope="col">
                                <!--員工姓名-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, EmployeeName %>"></asp:Literal>
                            </th>  
                            <th scope="col">
                                <!--部門-->
                                <asp:Literal ID="Literal6" runat="server" Text="部門"></asp:Literal>
                            </th>                            
                        </tr>
                        <tr>
                            <td colspan="3" class="tdEmptyData">                                                                            
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:RadioButton ID="RadioButton1" runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate></EditItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="員工編號" HeaderText="<%$ Resources:WebResources, EmployeeNo %>" />
                        <asp:BoundField DataField="員工姓名" HeaderText="<%$ Resources:WebResources, EmployeeName %>" />  
                        <asp:BoundField DataField="部門" HeaderText="部門" />                  
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
                        <asp:Label ID="lblCurrPage" runat="server" 
                            Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1%>'></asp:Label>頁/共
                        <asp:Label ID="lblPageCount" runat="server" 
                            Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>頁
                        <asp:LinkButton ID="lbtnNext" runat="server" CommandName="Page" CommandArgument="Next"
                            
                            
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/next.png" /></asp:LinkButton>
                        <asp:LinkButton ID="lbtnLast" runat="server" CommandArgument="Last" CommandName="Page"
                            
                            
                            Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/last.png" /></asp:LinkButton>
                        到第
                        <asp:TextBox ID="tbGoToIndex" runat="server" Width="40" AutoCompleteType="None"></asp:TextBox>
                        頁
                        <asp:Button ID="btnGoToIndex" runat="server" Text="GO" 
                            OnClick="btnGoToIndex_Click" />
                    </PagerTemplate>
                </asp:GridView>--%>
                   
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">                
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnOk" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnOk_Click"></dx:ASPxButton>   
                        </td>
                        <td>&nbsp;</td>
                        <td>  
                             <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                               <ClientSideEvents Click="function(s, e) {
                                    hidePopupWindow();              
                                }" /> 
                            </dx:ASPxButton>
                       </td>
                    </tr>
                </table>                                 
            </div>
        </div>
    </div>
    </form>
</body>
</html>
