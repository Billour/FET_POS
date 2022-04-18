<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_inputIMEIData.aspx.cs"
    Inherits="VSS_SAL_SAL01_inputIMEIData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IMEI輸入</title>
    <script type="text/javascript">
        function CheckIMEI(s, e) {
            if (txtIMEI.GetText() == '') {
                alert('請輸入IMEI值!');
                e.processOnServer = false;
            }

            var chkStr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890';
            for (i = 0; i < txtIMEI.GetText().length; i++) {
                if (chkStr.indexOf(txtIMEI.GetText().substr(i, 1).toUpperCase()) < 0) {
                    alert('IMEI只允許輸入英數字!!');
                    e.processOnServer = false;
                    return false;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidForcedInput" runat="server" value="N" />
    <div class="func">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <div class="criteria">
                        <table>
                            <tr>
                                <td>
                                    <!--商品編號-->
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                                </td>
                                <td>
                                    <asp:Label ID="lbPRODNO" runat="server" Text="125458700"></asp:Label>
                                </td>
                                <td>
                                    <!--商品名稱-->
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                                </td>
                                <td>
                                    <asp:Label ID="lbPRODNAME" runat="server" Text="哈拉900方案 (1/2) - 5800手機"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <!--IMEI-->
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Imei %>"></asp:Literal>：
                                </td>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="txtIMEI" ClientInstanceName="txtIMEI" runat="server" Width="200px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnInsert" ClientInstanceName="btnInsert" runat="server" Text="<%$ Resources:WebResources, Enter %>"
                                                    OnClick="btnInsert_Click">
                                                    <ClientSideEvents Click="function(s,e) {  CheckIMEI(s,e); }" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="seperate">
                    </div>
                    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="SID"
                        Width="100%" 
                        OnPageIndexChanged="grid_PageIndexChanged"
                        OnRowValidating="grid_RowValidating">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                <HeaderTemplate>
                                    <input type="checkbox" onclick="grid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, IMEI %>"
                                ReadOnly="true" />
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                    OnClick="deleteButton_Click" >
                                       <ClientSideEvents Click = "function(s,e){if (!confirm('系統將刪除勾選之資料，確認刪除？')){e.processOnServer=false;}}"/>
                                        </dx:ASPxButton>
                            </TitlePanel>
                        </Templates>
                        <Styles>
                            <EditFormColumnCaption Wrap="False">
                            </EditFormColumnCaption>
                        </Styles>
                        <SettingsEditing EditFormColumnCount="4" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <Settings ShowTitlePanel="True"></Settings>
                    </cc:ASPxGridView>
                    <div class="seperate">
                    </div>
                    <div class="btnPosition">
                        <table cellpadding="0" cellspacing="0" border="0" align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                        OnClick="OkButton_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                                        <ClientSideEvents Click="function(s, e) { hidePopupWindow();  }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <script type="text/javascript">
        function confirmIMEIInput() {
            if (confirm("IMEI資料已存在!是否強制輸入?")) {
                window.document.getElementById("hidForcedInput").value = "Y";
                btnInsert.SendPostBack('Click');
            } 
        }
    </script>
    
    </form>
</body>
</html>
