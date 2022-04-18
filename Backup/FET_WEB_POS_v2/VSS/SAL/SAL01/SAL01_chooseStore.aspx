<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_chooseStore.aspx.cs"
    Inherits="VSS_SAL_SAL01_chooseStore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>選擇門市</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript" />

    <script type="text/javascript" language="javascript">
        $(function() {
            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
            $("input:radio").attr("name", "SameRadio");
        });
    </script>

    <script type="text/javascript" language="javascript">
        $(function() {
            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
            $("input:radio").attr("name", "SameRadio");
        });
    </script>

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
                        <td class="tdtxt">                            
                            <!--門市編號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="storeNoTextBox" runat="server" Width="100"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--門市名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:TextBox ID="storeNameTextBox" runat="server" Width="100"></asp:TextBox>
                        </td>                       
                    </tr>
                </table>
            </div>
            <div class="seperate">
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
            <div class="seperate"></div>           
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>                
                     <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="門市編號" Width="100%"              
                        OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged">                
                        <Columns>                
                            <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
                            <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />                                    
                        </Columns>
                        <SettingsPager PageSize="10"></SettingsPager>  
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />                   
                    </cc:ASPxGridView>                                                                                      
                </ContentTemplate>
            </asp:UpdatePanel>            
            <div class="seperate"></div>
            <div class="btnPosition">
               <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" 
                            onclick="btnCommit_Click" />
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
