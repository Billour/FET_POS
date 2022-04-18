<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostCenterPopup.aspx.cs"
    Inherits="VSS_Common_CostCenterPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript" />

    <script type="text/javascript" language="javascript">
        $(function() {
            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
            $("input:radio").attr("name", "SameRadio");
        });

        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="criteria">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <!--部門代碼-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DepartmentCode %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtCUSTNO" runat="server" Width="100">
                    </dx:ASPxTextBox>
                </td>
                <td>
                    <!--部門名稱-->
                    <asp:Literal ID="Literal2" runat="server" Text="部門名稱"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtCUSTNAME" runat="server" Width="100">
                    </dx:ASPxTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s, e){ form1.reset(); }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="COST_CENTER_NO"
        Width="100%" OnPageIndexChanged="grid_PageIndexChanged">
        <Columns>
            <dx:GridViewDataCheckColumn VisibleIndex="0" Width="10">
                <DataItemTemplate>
                    <input type="radio" name="radioButton" />
                </DataItemTemplate>
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataColumn FieldName="COST_CENTER_NO" Caption="部門代碼" />
            <dx:GridViewDataColumn FieldName="COST_CENTER_NAME" Caption="部門名稱" />
        </Columns>
        <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="false" />
        <SettingsPager PageSize="5">
        </SettingsPager>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <ClientSideEvents FocusedRowChanged="function(s, e) {
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
                <td>
                    <dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                        OnClick="okButton_Click" />
                </td>
                <td>
                    &nbsp;
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
    </form>
</body>
</html>
