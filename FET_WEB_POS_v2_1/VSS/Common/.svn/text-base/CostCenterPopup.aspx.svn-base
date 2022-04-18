<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostCenterPopup.aspx.cs" Inherits="VSS_Common_CostCenterPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <div class="criteria">
                <table cellpadding="0" cellspacing="0" border="0">                    
                    <tr>
                        <td >
                            <!--部門代碼-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DepartmentCode %>"></asp:Literal>：
                        </td>
                        <td >
                            <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100"></dx:ASPxTextBox>
                        </td>
                        <td>
                            <!--會計科目-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>"></asp:Literal>：
                        </td>
                        <td >
                            <dx:ASPxTextBox ID="TextBox6" runat="server" Width="100"></dx:ASPxTextBox>
                        </td>                       
                    </tr>
                </table>
            </div>

            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s, e){ form1.reset(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>                                                
            </div>

            <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" 
                KeyFieldName="部門代碼" Width="100%"              
                OnPageIndexChanged="grid_PageIndexChanged">                
                <Columns>                       
                    <dx:GridViewDataCheckColumn VisibleIndex="0" Width="10">
                        <DataItemTemplate>
                            <input type="radio" name="radioButton" />
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>               
                    <dx:GridViewDataColumn FieldName="部門代碼" Caption="<%$ Resources:WebResources, DepartmentCode %>" />
                    <dx:GridViewDataColumn FieldName="會計科目" Caption="<%$ Resources:WebResources, AccountingSubject %>" />                                    
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
            
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" onclick="okButton_Click"/>
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
    </form>
</body>
</html>
