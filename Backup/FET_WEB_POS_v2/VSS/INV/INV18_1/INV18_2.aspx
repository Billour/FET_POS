<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV18_2.aspx.cs" Inherits="VSS_INV_INV18_2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>庫存調整原因輸入</title>
      <script type="text/javascript">
        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <div class="seperate"></div>

        <div>
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="STOCKADJ_DESCRIPTION"
                Width="100%" EnableCallBacks="false" AutoGenerateColumns="False" 
                OnPageIndexChanged="gvMaster_PageIndexChanged"
                OnSelectionChanged="grid_SelectionChanged">
            <Columns>
                <dx:GridViewDataCheckColumn VisibleIndex="0" Width="10">
                    <DataItemTemplate>
                        <input type="radio" name="radioButton" />
                    </DataItemTemplate>
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataColumn FieldName="STOCKADJ_DESCRIPTION" Caption="<%$ Resources:WebResources, Reason %>" />
            </Columns>
            <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="false" />
            <SettingsPager PageSize="10"></SettingsPager> 
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
        </div>

        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table>
                <tr>
                    <td>
                        <dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" onclick="OkButton_Click"/>
                    </td>
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
