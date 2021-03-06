<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LEA07.aspx.cs" Inherits="VSS_LEA_LEA07" %>
    
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
<script type="text/javascript">
    function getSupplierId(s, e) {
        _gvEventArgs = e;
        _gvSender = s;
        if (s.GetText() != '')
            PageMethods.getSupplierId(_gvSender.GetText(), getSupplierId_OnOK);
    }
    
    function getSupplierId_OnOK(returnData) {
        //debugger;
        if (returnData == '') {
            alert("廠商編號有誤!");
            _gvEventArgs.processOnServer = false;
            _gvSender.Focus();
            _gvSender.SetText(null);

        }
        else {
            //txtSupplierName.SetValue(returnData);
        }
    }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--設備租賃設定查詢作業-->
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, EquipmentLeasingSearch %>"></asp:Literal>
        </div>
        
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--類別-->
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">漫遊租賃</asp:ListItem>
                                <asp:ListItem Value="2">維修租賃</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <!--產品類別-->
                            <asp:Literal ID="Literal3" runat="server" Text="產品類別"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">產品類別1</asp:ListItem>
                                <asp:ListItem Value="2">產品類別2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <!--產品名稱-->
                            <asp:Literal ID="Literal5" runat="server" Text="產品名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList4" runat="server">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">產品名稱1</asp:ListItem>
                                <asp:ListItem Value="2">產品名稱2</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--外部廠商代碼-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, OutsideFirmNo %>"></asp:Literal>：
                        </td>
                        <td>
                            <uc1:PopupControl ID="txtSupplierNo" runat="server" PopupControlName="ConsignmentVendorsPopup" 
                         KeyFieldValue2="SUPP_NO" Width="280px"  SetClientValidationEvent="getSupplierId"  />
                        </td>
                        <td class="tdtxt">
                            <!--外部廠商名稱-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, OutsideFirmName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtSupplierName" ClientInstanceName="txtSupplierName" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td align="right">
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                                OnClick="btnSearch_Click" />
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>" />
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="seperate"></div>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Settings-ShowTitlePanel="true"  
                         EnableCallBacks="False" AutoGenerateColumns="False" KeyFieldName="LEASE_ID" 
                            Width="100%" onpageindexchanged="gvMaster_PageIndexChanged">
                            <Columns>
                                <dx:GridViewDataCheckColumn VisibleIndex="0">
                                    <DataItemTemplate>
                                        <input type="radio" name="radioButton" />
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataColumn FieldName="LEASE_TYPE" Caption="<%$ Resources:WebResources, Category %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="PRODTYPE" Caption="<%$ Resources:WebResources, ProductType2  %>" VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="PRODPENAME" Caption="<%$ Resources:WebResources, ProductName2 %>" VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="SUPP_NO" Caption="<%$ Resources:WebResources, OutsideFirmNo %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="SUPP_NAME" Caption="<%$ Resources:WebResources, OutsideFirmName %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartDate %>"
                                    VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>"
                                    VisibleIndex="7" />
                                <dx:GridViewDataColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    VisibleIndex="8" />
                                <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                    VisibleIndex="9" /> 
                                 <dx:GridViewDataColumn FieldName="LEASE_ID"  Visible="false" VisibleIndex="10" />
                            </Columns>
                            <Templates>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                            <SettingsBehavior AllowFocusedRow="True" />
                            <SettingsPager PageSize="10"></SettingsPager>
                            <Settings ShowTitlePanel="True" />
                            <SettingsEditing EditFormColumnCount="10" Mode="EditFormAndDisplayRow" />
                        </cc:ASPxGridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            
            <div class="seperate"></div>
            
            <div class="btnPosition">
                <dx:ASPxButton ID="btnSure" runat="server" Text="<%$ Resources:WebResources, Ok %>" 
                    OnClick="btnSure_Click" />
            </div>
        </div>
    </div>
</asp:Content>
