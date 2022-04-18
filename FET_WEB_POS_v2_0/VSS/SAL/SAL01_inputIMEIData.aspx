<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_inputIMEIData.aspx.cs"
    Inherits="VSS_SAL_SAL01_inputIMEIData" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IMEI輸入</title>     
</head>
<body>
    <form id="form1" runat="server">
    <div class="func">
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td>
                            <!--商品編號-->
                             <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="125458700"></asp:Label>
                        </td>
                        <td>
                            <!--商品名稱-->
                             <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="哈拉900方案 (1/2) - 5800手機"></asp:Label>
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
                                    <td><dx:ASPxTextBox ID="ASPxTextBox1" runat="server"></dx:ASPxTextBox></td>
                                    <td><dx:ASPxButton ID="btnInsert" runat="server" Text="<%$ Resources:WebResources, Enter %>"  /></td>
                                </tr>
                           </table>                                                                                                                                    
                        </td>                       
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
                                       
            <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="IMEI" Width="100%"  Settings-ShowTitlePanel="true"            
                OnPageIndexChanged="grid_PageIndexChanged">                
                <Columns>                
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                        <HeaderTemplate>
                            <input type="checkbox" onclick="grid.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                        </HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewCommandColumn>                                                                   
                    <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, IMEI %>" ReadOnly="true" />                                    
                </Columns>
                <Templates>
                    <TitlePanel>
                        <dx:ASPxButton ID="deleteButton" runat="server" Text="<%$ Resources:WebResources, Delete %>" />                                                                
                    </TitlePanel>
                </Templates>
                <Styles>
                    <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>                
                </Styles>                
                <SettingsEditing EditFormColumnCount="4" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            </cc:ASPxGridView>                                                                                                                                                        
            
            <div class="seperate"></div>
            <div class="btnPosition">
              <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                            onclick="OkButton_Click" />
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
