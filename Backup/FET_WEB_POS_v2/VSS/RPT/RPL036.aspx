<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL036.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL036" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[生效日期起值]不允許大於[生效日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--Handset Subsidy Report-->
        <asp:Literal ID="Literal1" runat="server" Text="Handset Subsidy Report"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--生效日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SalesDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="txtOrdDateStart" EditFormatString="yyyy/MM/dd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd" EditFormatString="yyyy/MM/dd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--商品料號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="商品料號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="pupPRODTYPENO_S" runat="server" IsValidation="false" PopupControlName="ProductsPopup"
                                    Width="150px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="pupPRODTYPENO_E" runat="server" IsValidation="false" PopupControlName="ProductsPopup"
                                    Width="150px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--促銷代碼-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="pupNO_S" runat="server" IsValidation="false" PopupControlName="PromotionsPopupOnly"
                                    Width="150px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="pupNO_E" runat="server" IsValidation="false" PopupControlName="PromotionsPopupOnly"
                                    Width="150px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--商品名稱-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="pupPRODNAME" runat="server" Width="200px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--促銷名稱-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="pupPROMONAME" runat="server" Width="200px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <table align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                    OnClick="btnSearch_Click">
                    <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    OnClick="btnReset_Click" AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click">
                    <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        OnPageIndexChanged="gvMaster_PageIndexChanged" Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="銷售年月日" Caption="銷售年月日" />
            <dx:GridViewDataTextColumn FieldName="商品料號" Caption="商品料號" />
            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="商品名稱" />
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataTextColumn FieldName="促銷名稱" Caption="促銷名稱" />
            <dx:GridViewDataColumn FieldName="SUBSIDY" Caption="SUBSIDY" />
            <dx:GridViewDataColumn FieldName="QTY" Caption="QTY" />
            <dx:GridViewDataColumn FieldName="QTS" Caption="QTS" />
            <dx:GridViewDataColumn FieldName="單機價" Caption="單機價" />
            <dx:GridViewDataColumn FieldName="轉換值" Caption="轉換值" />
            <dx:GridViewDataColumn FieldName="還原單機價" Caption="還原單機價" />
            <dx:GridViewDataColumn FieldName="SUBSIDY 差異金額" Caption="Subsidy 差異金額" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
        </dx:ASPxGridViewExporter>
    </div>
</asp:Content>
