<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LEA02.aspx.cs" Inherits="VSS_LEA_LEA02"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script> 
     
 <script type="text/javascript" language="javascript">
     function URLReserve(s, e) {
         var fName = "0_btnReserve";
         var lblDeviceType = getClientInstance('Label', s.name.replace(fName, "2_lblDeviceType"));
         var DeviceType = lblDeviceType.innerText;
         var lblImei = getClientInstance('Label', s.name.replace(fName, "3_lblImei"));
         var Imei = lblImei.innerText;
         var url = "../LEA05/LEA05.aspx?type=" + DeviceType + "&imei=" + Imei + "&meth=預約";
         document.location.href=url ;

     }
     
     function URLAdd(s, e) {
         var fName = "0_btnAdd";
         var lblDeviceType = getClientInstance('Label', s.name.replace(fName, "2_lblDeviceType"));
         var DeviceType = lblDeviceType.innerText;
         var lblImei = getClientInstance('Label', s.name.replace(fName, "3_lblImei"));
         var Imei = lblImei.innerText;
         var url = "../LEA05/LEA05.aspx?type=" + DeviceType + "&imei=" + Imei + "&meth=新增";
         document.location.href = url;

     }
 </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <asp:Literal ID="Literal10" runat="server" Text="可租賃設備查詢"></asp:Literal>
    </div>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--類別-->
                    <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3">
                    <dx:ASPxRadioButtonList ID="rbDeviceType" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow" Border-BorderStyle="None">
                        <Items>
                            <dx:ListEditItem Value="DMS" Text="漫遊租賃" Selected="true"  />
                            <dx:ListEditItem Value="HRS" Text="維修租賃" />
                        </Items>
                    </dx:ASPxRadioButtonList>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--區域-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>：
                </td>
                <td class="tdval">
                     <dx:ASPxComboBox ID="cbbZone" runat="server" ValueType="System.String" 
                         SelectedIndex="1" Width="100" 
                         OnValueChanged="cbbZone_OnValueChanged"
                         AutoPostBack="true">
                     </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--庫存地點-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
                </td>
                <td class="tdval">
                     <dx:ASPxComboBox ID="cbbStoreNo" runat="server" ValueType="System.String" SelectedIndex="1" Width="100">
                     </dx:ASPxComboBox>
                </td>
                <td class="tdtxt"></td>
                <td class="tdval"></td>
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
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <dx:ASPxButton  SkinID="ResetButton" ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="SubEditBlock">
                <div class="GridScrollBar" style="height: auto">
                    <dx:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                        KeyFieldName="STORE_NO" 
                        OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StorageLocation %>" VisibleIndex="0">
                                <DataItemTemplate>
                                   <dx:ASPxLabel ID="lbLStoreNo" runat="server"  Text='<%# Bind("STORE_NO") %>' ClientVisible="true"></dx:ASPxLabel>
                                   <dx:ASPxLabel ID="lblStoreName" runat="server"  Text='<%# Bind("STORENAME") %>' ></dx:ASPxLabel>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="DEVICE_TYPE" Caption="<%$ Resources:WebResources, MobileType %>" VisibleIndex="1" />
                            <dx:GridViewDataColumn FieldName="INVENTORY" Caption="<%$ Resources:WebResources, StockQuantity %>" VisibleIndex="2" />
                        </Columns>
                        <Templates>
                            <DetailRow>
                                <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Width="100%"
                                    AutoGenerateColumns="False" KeyFieldName="IMEI"
                                    OnPageIndexChanged="gvDetail_PageIndexChanged"
                                    OnHtmlRowPrepared="gvDetail_HtmlRowPrepared">
                                    <Columns>
                                        <dx:GridViewDataTextColumn VisibleIndex="0"  Caption=" ">
                                            <DataItemTemplate>
                                                <table cellpadding="0" cellspacing="0" border="0" align="left">
                                                    <tr>
                                                        <td align="right">
                                                            <dx:ASPxButton ID="btnReserve" runat="server" Text="<%$ Resources:WebResources, Reserve %>" >
                                                                <ClientSideEvents Click="function(s,e){URLReserve(s,e);}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>">
                                                                <ClientSideEvents Click="function(s,e){URLAdd(s,e);}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StorageLocation %>"
                                            VisibleIndex="1" >
                                            </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DEVICE_TYPE" Caption="<%$ Resources:WebResources, MobileType %>"
                                            VisibleIndex="2" >
                                              <DataItemTemplate>
                                                <dx:ASPxLabel ID="lblDeviceType" runat="server"  Text='<%# Bind("DEVICE_TYPE") %>' ></dx:ASPxLabel>   
                                             </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, MobileIdentityNumber %>"
                                            VisibleIndex="3" >
                                             <DataItemTemplate>
                                                <dx:ASPxLabel ID="lblImei" runat="server"  Text='<%# Bind("IMEI") %>' ></dx:ASPxLabel>   
                                             </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, Status %>"
                                            VisibleIndex="4" />
                                    </Columns>
                                    <Templates>
                                        <EmptyDataRow>
                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                        </EmptyDataRow>
                                    </Templates>
                                    <Styles>
                                        <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                        </TitlePanel>
                                    </Styles>
                                    <Settings ShowFooter="false" />
                                    <SettingsDetail  IsDetailGrid ="true" />
                                    <SettingsPager PageSize="5"></SettingsPager>
                                </cc:ASPxGridView>
                            </DetailRow>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                            </EmptyDataRow>
                        </Templates>
                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    </dx:ASPxGridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
