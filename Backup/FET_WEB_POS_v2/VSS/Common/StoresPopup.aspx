<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoresPopup.aspx.cs" Inherits="VSS_Common_StoresPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>選擇門市</title>
    <script src="../../ClientUtility/jquery.js" type="text/javascript" />

    <script type="text/javascript" language="javascript">
//        $(function() {
//            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
//            $("input:radio").attr("name", "SameRadio");
//        });

        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">

    <div class="func">
        <div>
            <div class="criteria">
                <table>                    
                    <tr>
                        <td class="tdtxt">                            
                            <!--區域別-->
                            <asp:Literal ID="lblByDistrict" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                             <dx:ASPxComboBox ID="ddlDistrict" runat="server" Width="80px"></dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            &nbsp;</td>
                        <td class="tdval">
                            &nbsp;</td>                       
                    </tr>
                    <tr>
                        <td class="tdtxt">                            
                            <!--門市編號-->
                            <asp:Literal ID="lblStoreNo" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtStoreNo" runat="server" Width="100px"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--門市名稱-->
                            <asp:Literal ID="lblStoreName" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtStoreName" runat="server" Width="170px"></dx:ASPxTextBox>
                        </td>                       
                    </tr>
                </table>
            </div>
            
            <div class="seperate"></div>
            
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton"
                                Text="<%$ Resources:WebResources, Reset %>" >
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>                                                
            </div>
            
            <div class="seperate"></div> 
                                        
             <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="STORE_NO" Width="100%"              
                OnPageIndexChanged="grid_PageIndexChanged">                
                <Columns>   
                    <dx:GridViewDataCheckColumn VisibleIndex="0" Width="10">
                        <DataItemTemplate>
                            <input type="radio" name="radioButton" />
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>                                                                                                                                                     
                    <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" />
                    <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" />                                    
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
                                                                                                          
            <div class="seperate"></div>
            
            <div class="btnPosition">
               <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td><dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>"  
                            onclick="btnOK_Click"/>
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
