<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LEA03.aspx.cs" Inherits="VSS_LEA_LEA03" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script> 
    
 <script type="text/javascript" language="javascript">
     function URLSelect(s, e) {
         var fName = "0_btnSelect";
         var txtDeviceType = getClientInstance('TxtBox', s.name.replace(fName, "1_txtDeviceType"));
         var DeviceType = txtDeviceType.GetText();
         var txtImei = getClientInstance('TxtBox', s.name.replace(fName, "1_txtImei"));
         var Imei = txtImei.GetText();
         var txtShno = getClientInstance('TxtBox', s.name.replace(fName, "1_txtRentSheetNo"));
         var Shno = txtShno.GetText();
         var url = "../LEA05/LEA05.aspx?type=" + DeviceType + "&imei=" + Imei + "&meth=已租賃&SHNO=" + Shno;
         
         document.location.href=url ;
         
     }
   
 </script>
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    
    <div class="titlef">
        <!--已租賃設備查詢-->
        <asp:Literal ID="Literal11" runat="server" Text="已租賃設備查詢"></asp:Literal>
    </div>

    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--手機地點-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, MobileLocation %>"></asp:Literal>：
                </td>
                <td class="tdval">
                     <dx:ASPxComboBox ID="cbbStoreNo" runat="server" ValueType="System.String"  Width="100">
                     </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--客戶姓名-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtCustName" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--客戶門號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtMsisdn" runat="server">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                </td>
                <td class="tdval" colspan="3">
                    <asp:CheckBox ID="cbBookingToday" runat="server" Text="<%$ Resources:WebResources, ListTodaysReservations %>" />
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="right">
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                        OnClick="btnSearch_Click" />
                </td>
                <td>&nbsp;</td>
                <td align="left">
                    <dx:ASPxButton ID="btnClear"  SkinID="ResetButton" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <div>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height: auto">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <dx:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" AutoGenerateColumns="False" 
                            Width="100%" KeyFieldName="RENT_SHEET_NO"
                            OnHtmlRowPrepared ="gvMaster_HtmlRowPrepared">
                            <Columns>
                                <dx:GridViewDataColumn VisibleIndex="0" Caption=" ">
                                    <DataItemTemplate>
                                        <dx:ASPxButton ID="btnSelect" runat="server" Text="<%$ Resources:WebResources, Select %>"
                                           >
                                            <ClientSideEvents Click="function(s,e){URLSelect(s,e);}" />
                                        </dx:ASPxButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn >
                                <dx:GridViewDataColumn FieldName="ITEM"  Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="1"  >
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lblItem" runat="server"  Text='<%# Bind("ITEM") %>' ></dx:ASPxLabel>
                                        <dx:AspxTextBox ID="txtRentSheetNo" runat="server"  Text='<%# Bind("RENT_SHEET_NO") %>' ClientVisible="false"></dx:AspxTextBox>
                                        <dx:AspxTextBox ID="txtImei" runat="server"  Text='<%# Bind("IMEI") %>' ClientVisible="false"></dx:AspxTextBox>
                                        <dx:AspxTextBox ID="txtDeviceType" runat="server"  Text='<%# Bind("DEVICE_TYPE") %>' ClientVisible="false"></dx:AspxTextBox>
                                        <dx:AspxTextBox ID="txtRentStatus" runat="server"  Text='<%# Bind("RENT_STATUS") %>' ClientVisible="false"></dx:AspxTextBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="BOOKING_DATE" Caption="<%$ Resources:WebResources, ReservationDate %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="RENT_SHEET_NO" Caption="<%$ Resources:WebResources, LeaseOrderNo %>"
                                    VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="CUST_NAME" Caption="<%$ Resources:WebResources, CustomerName %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="MSISDN" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="SEX" Caption="<%$ Resources:WebResources, Gender %>"
                                    VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, MobileLocation %>"
                                    VisibleIndex="7" />
                                <dx:GridViewDataColumn FieldName="PRE_S_DATE" Caption="<%$ Resources:WebResources, CollectionDueDate %>"
                                    VisibleIndex="8" />
                                <dx:GridViewDataColumn FieldName="PRE_E_DATE" Caption="<%$ Resources:WebResources, ReturnDueDate %>"
                                    VisibleIndex="9" />
                                <dx:GridViewDataColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, Status %>"
                                    VisibleIndex="10" >
                                    <DataItemTemplate>
                                     <dx:ASPxLabel ID="lblStatus" runat="server"  Text='<%# Bind("STATUS") %>' ClientVisible="true"></dx:ASPxLabel>
                                    </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                    VisibleIndex="10" />
                                <dx:GridViewDataColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    VisibleIndex="11" />
                            </Columns>                            
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                        </dx:ASPxGridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>    
</asp:Content>
