<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL035.aspx.cs"  MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL035" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            
            if (txtTranDateStart.GetText() != '' && txtTranDateEnd.GetText() != '') {
                if (txtTranDateStart.GetValue() > txtTranDateEnd.GetValue()) {
                    alert("[交易日期起值]不允許大於[交易日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }

            }

            var bdate = txtTranDateStart.GetText();
            var sdate = txtTranDateEnd.GetText()

            if (DateDiff(bdate, sdate) > 31) {
                alert("交易日期區間不允許超過一個月，請重新輸入!");
                _gvEventArgs.processOnServer = false;
                return;
            }

            function DateDiff(beginDate, endDate) {
                var arrbeginDate, Date1, Date2, arrendDate, iDays
                arrbeginDate = beginDate.split("/")
                Date1 = new Date(arrbeginDate[1] + '/' + arrbeginDate[2] + '/' + arrbeginDate[0])
                arrendDate = endDate.split("/")
                Date2 = new Date(arrendDate[1] + '/' + arrendDate[2] + '/' + arrendDate[0])
                iDays = parseInt(Math.abs(Date1 - Date2) / 1000 / 60 / 60 / 24)    //轉換為天數 
                return iDays
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
   
    <div class="titlef" align="left">
        <!--業績分析報表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL035 %>"></asp:Literal>
    </div>

    <div class="seperate"></div>

    <div align="left">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <uc1:PopupControl ID="cbPromotionCode" runat="server" IsValidation="false" PopupControlName="PromotionsPopupOnly" />
                </td>
            </tr>     
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="商品料號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <uc1:PopupControl ID="cbProductNo" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtTranDateStart" runat="server" ClientInstanceName="txtTranDateStart">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtTranDateEnd" runat="server" ClientInstanceName="txtTranDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate"></div>
        
    <table align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" onclick="btnSearch_Click">
                <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" 
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" OnClick="btnReset_Click">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" onclick="btnExport_Click"></dx:ASPxButton>
            </td>
        </tr>
    </table>

    <div class="seperate"></div>

    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
        OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Item %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="交易日期" runat="server" Caption="<%$ Resources:WebResources, TradeDate %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
            <dx:GridViewDataTextColumn FieldName="門市代碼" Caption="<%$ Resources:WebResources, IMEIIvrcode %>" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
            <dx:GridViewDataColumn FieldName="銷售銷退" Caption="<%$ Resources:WebResources, SalesandBack %>" />
            <dx:GridViewDataColumn FieldName="銷售數量" Caption="<%$ Resources:WebResources, SalesQTY %>" />
            <dx:GridViewDataColumn FieldName="門號" Caption="<%$ Resources:WebResources, MobileNumber %>" />
            <dx:GridViewDataColumn FieldName="促銷代碼" Caption="<%$ Resources:WebResources, PromotionCode %>" />
            <dx:GridViewDataColumn FieldName="銷售金額" Caption="銷售金額" />
            <dx:GridViewDataColumn FieldName="員工編號" Caption="<%$ Resources:WebResources, EmployeeNo %>" />
            
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" ></dx:ASPxGridViewExporter>   

    <div class="seperate"></div>

</asp:Content>