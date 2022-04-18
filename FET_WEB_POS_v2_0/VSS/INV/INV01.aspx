<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV01.aspx.cs" Inherits="VSS_INV01_INV01" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeicheckbox(con) {
            if (con.checked) {
                openwindow("SAL01_inputIMEIData.aspx");
            }
        }

        function checkID() {
            var vID = document.getElementById("tbInvoiceNo").value;
            if (vID.length != 8 && vID > 0) {
                openwindow("SAL01_checkIDNumber.aspx", 300, 200);
                return false;
            }
        }
    </script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    <div>        
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--總部移撥查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="總部移撥查詢作業"></asp:Literal>
                    </td>
                    <td align="right">                        
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width:100%">
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                         <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100px" />
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td><dx:ASPxTextBox ID="TextBox8" runat="server" Width="100px" /></td>
                                <td>&nbsp;</td>
                                <td>
                                <dx:ASPxButton ID="chooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>" SkinID="PopupButton"></dx:ASPxButton>
                                </td>
                            </tr>
                        </table>                                                
                    </td>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                         <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">   
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="100">
                            <Items>
                                <dx:ListEditItem Value="-請選擇-" Text="-請選擇-" Selected="true" />
                                <dx:ListEditItem Value="在途" Text="在途" />
                                <dx:ListEditItem Value="在途" Text="在途" />
                            </Items>
                        </dx:ASPxComboBox>                       
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移出日期-->
                         <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0" align="left"> 
                            <tr>
                                <td><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="transferOutStartDate" runat="server"></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>                        
                    </td>
                    <td class="tdtxt">
                        <!--移出門市-->
                         <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td><dx:ASPxTextBox ID="TextBox4" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxButton ID="chooseButton2" runat="server" SkinID="PopupButton" Text="<%$ Resources:WebResources, Choose %>">                                                                      
                                </dx:ASPxButton></td>
                            </tr>
                        </table>                                                                            
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--撥入日期-->
                         <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                         <table cellpadding="0" cellspacing="0" border="0" align="left"> 
                            <tr>
                                <td><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                        </table>      
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市-->
                         <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">  
                        <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td>                          
                                    <dx:ASPxTextBox ID="TextBox2" runat="server" Width="100px"></dx:ASPxTextBox>
                                </td>      
                                <td>&nbsp;</td>                          
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" 
                                        Text="<%$ Resources:WebResources, Choose %>" SkinID="PopupButton">                                                         
                                   </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>   
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="resetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>                                
        </div>
        <div class="seperate"></div>
        <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="移撥單號" Width="100%"              
        OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged">                
            <Columns>                
                <dx:GridViewDataColumn FieldName="移撥單號" Caption="<%$ Resources:WebResources, TransferSlipNo %>" />
                <dx:GridViewDataColumn FieldName="移撥狀態" Caption="移撥狀態" />            
                <dx:GridViewDataColumn FieldName="移出門市" Caption="<%$ Resources:WebResources, TransferFrom %>" EditCellStyle-Wrap="True" />            
                <dx:GridViewDataColumn FieldName="移出日期" Caption="<%$ Resources:WebResources, TransferOutDate %>" />                                                                        
                <dx:GridViewDataColumn FieldName="撥入門市" Caption="<%$ Resources:WebResources, TransferTo %>" />                                  
                <dx:GridViewDataColumn FieldName="撥入日期" Caption="<%$ Resources:WebResources, TransferInDate %>" />
                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />            
            </Columns>
            <Templates>                    
                 <DetailRow>                                             
                         <cc:ASPxGridView ID="detailGrid" runat="server" ClientInstanceName="detailGrid" Settings-ShowTitlePanel="true"
                             Width="100%" OnBeforePerformDataSelect="detailGrid_DataSelect" OnPageIndexChanged="detailGrid_PageIndexChanged" EnableRowsCache="true">                                                  
                            <Columns>                                    
                                <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />                            
                                <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />                            
                                <dx:GridViewDataCheckColumn FieldName="IMEI控管" Caption="<%$ Resources:WebResources, ImeiControl %>" />                            
                                <dx:GridViewDataColumn FieldName="移出數量" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>" />
                                <dx:GridViewDataColumn FieldName="移出IMEI" Caption="移出IMEI" />
                                <dx:GridViewDataColumn FieldName="撥入數量" Caption="<%$ Resources:WebResources, TransferredInQuantity %>" />
                                <dx:GridViewDataColumn FieldName="移入IMEI" Caption="移入IMEI" />                                                                
                            </Columns>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <Settings ShowFooter="false" />                                 
                            <SettingsDetail IsDetailGrid="true" />
                            <SettingsPager PageSize="5"></SettingsPager>                                
                            <Templates>
                                <TitlePanel>
                                    移撥單號：<asp:Label ID="Label5" runat="server" Text="ST2013-100712001" ></asp:Label>
                                </TitlePanel>                                                          
                            </Templates>                            
                            <Styles>
                                <TitlePanel Font-Size="Small" HorizontalAlign="Left"></TitlePanel>
                            </Styles>
                        </cc:ASPxGridView>
                 </DetailRow>                                                      
             </Templates>
             <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
             <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" /> 
             
             <SettingsPager PageSize="15"></SettingsPager>   
             <Images >
                <DetailCollapsedButton Url="~/Icon/toggle_expand.png"></DetailCollapsedButton>
                <DetailExpandedButton Url="~/Icon/toggle_collapse.png"></DetailExpandedButton>               
             </Images>                                                      
        </cc:ASPxGridView>           
    </div>     
        
    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server"  
         EnableViewState="False" PopupElementID="chooseButton1" TargetElementID="TextBox8" LoadingPanelID="lp1">                
     </cc:ASPxPopupControl>
     
     <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server"  
         EnableViewState="False" PopupElementID="chooseButton2" TargetElementID="TextBox4" LoadingPanelID="lp1">         
     </cc:ASPxPopupControl>
     
     <cc:ASPxPopupControl ID="storesPopup2" ClientInstanceName="StoresPopup" SkinID="StoresPopup" runat="server"  
         EnableViewState="False" PopupElementID="ASPxButton1" TargetElementID="TextBox2" LoadingPanelID="lp1">         
     </cc:ASPxPopupControl>
     
     
     <dx:ASPxLoadingPanel ID="lp1" runat="server"></dx:ASPxLoadingPanel>
              
</asp:Content>