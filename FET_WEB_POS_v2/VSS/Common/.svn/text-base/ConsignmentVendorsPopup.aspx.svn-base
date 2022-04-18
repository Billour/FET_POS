<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsignmentVendorsPopup.aspx.cs" Inherits="VSS_Common_ConsignmentVendorsPopup" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StoreSearch %>"></asp:Literal></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >            
            <tr>
                <td>
                    <!--廠商代號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="TextBox1" runat="server" Width="120px"></dx:ASPxTextBox>
                </td>
                <td>
                    <!--廠商名稱-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="TextBox2" runat="server" Width="120px"></dx:ASPxTextBox>
                </td>
             </tr>
        </table>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="resetButton" SkinID="ResetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s, e){ form1.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="SUPP_NO"
            Width="100%" AutoGenerateColumns="False" OnPageIndexChanged="grid_PageIndexChanged"><%--EnableRowsCache="False"--%>
            <Columns>
               <dx:GridViewDataCheckColumn VisibleIndex="0" Width="30px">
                    <DataItemTemplate>
                        <input type="radio" name="radioButton" />
                    </DataItemTemplate>
                </dx:GridViewDataCheckColumn>                    
                <dx:GridViewDataColumn FieldName="SUPP_NO"  Caption="<%$ Resources:WebResources, SupplierNo %>" />
                <dx:GridViewDataColumn FieldName="SUPP_NAME" Caption="<%$ Resources:WebResources, SupplierName %>" />
            </Columns>
            <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="false" />
            <SettingsPager PageSize="5" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
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
        <div class="seperate"></div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" onclick="OkButton_Click" />
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
    </form>
</body>
</html>
