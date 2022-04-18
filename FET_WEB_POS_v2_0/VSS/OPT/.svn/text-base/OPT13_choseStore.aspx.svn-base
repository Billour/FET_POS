<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT13_choseStore.aspx.cs" Inherits="VSS_OPT_OPT13_choseStore" %>

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

    <script type="text/javascript">
          function OnInit(s, e) {
              s.GetInputElement().name = "radioChoose";
          }
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
                            區域別：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox2" runat="server" ></dx:ASPxTextBox>
                        </td>                          
                    </tr>                   
                    <tr>
                        <td class="tdtxt">
                            門市編號：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox1" runat="server" ></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            門市名稱：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="TextBox6" runat="server" ></dx:ASPxTextBox>
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
            <div class="seperate">
            </div>

            <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="門市編號" Width="100%"              
                OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged">                
                <Columns>   
                    <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                        <DataItemTemplate>
                            <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose">
                                <ClientSideEvents Init="OnInit" />
                            </dx:ASPxRadioButton>
                        </DataItemTemplate>
                    </dx:GridViewDataDateColumn>             
                    <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
                    <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />                                    
                    <dx:GridViewDataColumn FieldName="區域別" Caption="<%$ Resources:WebResources, ByDistrict %>" />                                    
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
        </div>
    </div>
    </form>
</body>
</html>