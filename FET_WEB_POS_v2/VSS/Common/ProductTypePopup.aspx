<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductTypePopup.aspx.cs" Inherits="VSS_Common_ProductTypePopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>選擇商品類別</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript" />

    <script type="text/javascript" language="javascript">
//        $(function() {
//            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
//            $("input:radio").attr("name", "SameRadio");
//        });
    </script>
    <script type="text/javascript">
        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="func">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="criteria">
                        <table>                    
                            <tr>
                                <td class="tdtxt">                            
                                    <!--類別編號-->
                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, CategoryNo %>"></asp:Literal>：
                                </td>
                                <td class="tdval">
                                    <asp:TextBox ID="ProductTypeNoTextBox" runat="server" Width="100"></asp:TextBox>
                                </td>
                                <td class="tdtxt">
                                    <!--類別名稱-->
                                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, CategoryName %>"></asp:Literal>：
                                </td>
                                <td class="tdval">
                                    <asp:TextBox ID="ProductTypeNameTextBox" runat="server" Width="100"></asp:TextBox>
                                </td>                       
                            </tr>
                        </table>
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="btnPosition">
                        <table cellpadding="0" cellspacing="0" border="0" align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" OnClick="btnReset_Click" />
                                </td>
                            </tr>
                        </table>                                                
                    </div>
                    <div class="seperate"></div>                             
                    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="PRODTYPENO" Width="100%"              
                        OnPageIndexChanged="grid_PageIndexChanged">                
                        <Columns>   
                            <dx:GridViewDataCheckColumn VisibleIndex="0" Width="10">
                                <DataItemTemplate>
                                    <input type="radio" name="radioButton" />
                                </DataItemTemplate>
                            </dx:GridViewDataCheckColumn>                                                                                                                                                     
                            <dx:GridViewDataColumn FieldName="PRODTYPENO" Caption="<%$ Resources:WebResources, CategoryNo %>" />
                            <dx:GridViewDataColumn FieldName="PRODTYPENAME" Caption="<%$ Resources:WebResources, CategoryName %>" />                                    
                        </Columns>
                        <SettingsPager PageSize="5"></SettingsPager>  
                        <SettingsBehavior AllowFocusedRow="True" />
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="okButton" />
                </Triggers>
            </asp:UpdatePanel>
                                                                                                              
            <div class="seperate"></div>
            <div class="btnPosition">
               <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" onclick="OkButton_Click"/>
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
