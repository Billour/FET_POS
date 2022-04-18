<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON01.aspx.cs" Inherits="VSS_CONS_CON01" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type= "text/javascript">
     function checkINVcode(s, e) {
         _gvEventArgs = e;
         _gvSender = s;
         if (s.GetText() != '')
             PageMethods.checkINVcode(_gvSender.GetText(), wmCheck_OnOK);
     }

     function wmCheck_OnOK(returnData) {
         if (returnData == '') {
             _gvEventArgs.processOnServer = false;
             _gvSender.Focus();
         }
         else {
             alert("統一編號請輸入8碼，請重新輸入");
         }
     }
 </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="titlef">
        <!--外部廠商查詢作業(總部)-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OutsideFirmSearchHQ %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                     <td class="tdtxt">
                        <!--廠商類別-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropType" runat="server">
                            <Items>
                                <dx:ListEditItem Text="-請選擇-" Value="select" Selected="true" />
                                <dx:ListEditItem Text="寄銷廠商" Value="1" />
                                <dx:ListEditItem Text="外部廠商" Value="2" />
                                <dx:ListEditItem Text="全部" Value="ALL" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval" >
                        <uc1:PopupControl ID="popSupplierNo" runat="server" PopupControlName="ConsignmentVendorsPopup"
                                IsValidation="false" KeyFieldValue2="SUPP_NO" /> 
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="popSupplierName" runat="server" PopupControlName="ConsignmentVendorsPopup"  
                         IsValidation="false" KeyFieldValue2="SUPP_NAME"/>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--統一編號-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtCompanyId" MaxLength="8" Width="210px" runat="server">
                            <ClientSideEvents  Validation="function(s, e){checkINVcode(s, e); }" />
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">&nbsp;</td>
                    <td class="tdval">&nbsp;</td>
                    <td class="tdtxt">&nbsp;</td>
                    <td class="tdval">&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" 
                            Text="<%$ Resources:WebResources, Reset %>" onclick="btnClear_Click" /></td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="Div1" runat="server">
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="SUPP_NO"
                    Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" 
                        onpageindexchanged="gvMaster_PageIndexChanged" EnableCallBacks="False" 
                        EnableTheming="True">
                        <Columns>
                            <dx:GridViewDataHyperLinkColumn FieldName="SUPP_NO" Caption="<%$ Resources:WebResources, SupplierNo %>">
                                <PropertiesHyperLinkEdit NavigateUrlFormatString="~/VSS/CONS/CON02/CON02.aspx?No={0}">
                                </PropertiesHyperLinkEdit>
                            </dx:GridViewDataHyperLinkColumn>
                            <dx:GridViewDataTextColumn FieldName="SUPP_NAME" Caption="<%$ Resources:WebResources, SupplierName %>" />
                            <dx:GridViewDataTextColumn FieldName="CSM_TYPE" Caption="<%$ Resources:WebResources, SupplierCategory %>" />
                            <dx:GridViewDataTextColumn FieldName="COMPANY_ID" Caption="<%$ Resources:WebResources, UnifiedBusinessNo %>" />
                            <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, CooperationStartDate %>" />
                            <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, CooperationEndDate %>" />
                            <dx:GridViewDataTextColumn FieldName="FET_CONTACE_USER" Caption="<%$ Resources:WebResources, Contact1 %>" />
                            <dx:GridViewDataTextColumn FieldName="BOSS_TEL_NO" Caption="<%$ Resources:WebResources, Telephone %>" />
                            <dx:GridViewDataTextColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                            <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <table align="left"><tr><td>
                                    <dx:ASPxButton ID="btnExport" onclick="btnExport_Click" runat="server" Text="<%$ Resources:WebResources, Export %>" />
                                </td></tr></table>
                            </TitlePanel>
                        </Templates>
                        <SettingsPager PageSize="10" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        <Settings ShowTitlePanel="True" />
                    </cc:ASPxGridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>