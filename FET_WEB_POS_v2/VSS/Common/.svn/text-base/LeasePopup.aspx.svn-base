<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeasePopup.aspx.cs" Inherits="VSS_Common_LeasePopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>折扣料號查詢</title>       
      <script type="text/javascript">
        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
        function PD_OnCloseUp() {
            alert(window.parent.productsPopup);
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="10" border="0">  
                    <tr>
                        <td >
                            <!--折扣編號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                        </td>
                        <td >
                            <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100"></dx:ASPxTextBox>
                        </td>
                        <td>
                            <!--折扣名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>：
                        </td>
                        <td >
                            <dx:ASPxTextBox ID="TextBox6" runat="server" Width="100"></dx:ASPxTextBox>
                        </td>                       
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton"></dx:ASPxButton>
                            
                        </td>
                    </tr>
                </table>                                                
            </div>
            <div class="seperate">
            </div>
            
            <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="PRODNO" Width="100%"              
                OnPageIndexChanged="grid_PageIndexChanged" OnSelectionChanged="grid_SelectionChanged">                
                <Columns>                       
                    <dx:GridViewDataCheckColumn VisibleIndex="0" Width="10">
                        <DataItemTemplate>
                            <input type="radio" name="radioButton" />
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>               
                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" />
                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, DiscountName %>" />  
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
            <div class="seperate"></div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" onclick="OkButton_Click"/>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" 
                                Text="<%$ Resources:WebResources, Cancel %>" onclick="btnCancel_Click" >                    
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
