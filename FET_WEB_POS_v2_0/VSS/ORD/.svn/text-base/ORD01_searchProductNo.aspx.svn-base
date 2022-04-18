<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD01_searchProductNo.aspx.cs" Inherits="VSS_ORD_ORD01_searchProductNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品編號查詢</title>    
      <script type="text/javascript">
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
                        <td >
                            <!--商品編號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td >
                            <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100"></dx:ASPxTextBox>
                        </td>
                        <td>
                            <!--商品名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
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
            <div class="seperate">
            </div>
            
            <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="商品編號" Width="100%"              
                OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged">                
                <Columns>   
                    <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                        <DataItemTemplate>
                            <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose">
                                <ClientSideEvents Init="OnInit" />
                            </dx:ASPxRadioButton>
                        </DataItemTemplate>
                    </dx:GridViewDataDateColumn>             
                    <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>" />
                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />                                    
                </Columns>
                <SettingsPager PageSize="5"></SettingsPager>               
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />                   
            </cc:ASPxGridView>            
            <div class="seperate"></div>
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                            onclick="btnCommit_Click" >  <ClientSideEvents Click="function(s, e) {
                              window.close();      
                            }" /> </dx:ASPxButton>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">                    
                            <ClientSideEvents Click="function(s, e) {
                              window.close();e.returnValue =false;          
                            }" /> 
                        </dx:ASPxButton>   
                        </td>
                    </tr>                
                </table>                                                                              
            </div>
        
    </form>
</body>
</html>
