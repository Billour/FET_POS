<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL004.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL004" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[調整日期起值]不允許大於[調整日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--門市對帳明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL04 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--調整日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="調整日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="txtOrdDateStart">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--門市編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Store %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 270px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                <div style="width: 106px;">
                                    <uc1:PopupControl ID="ASPxTextBox2" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                                </div>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox3" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--付款方式-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                       <Items>
                            <dx:ListEditItem Text="ALL" Value="0" />
                            <dx:ListEditItem Text="現金" Value="1" />
                            <dx:ListEditItem Text="信用卡" Value="2" />
                            <dx:ListEditItem Text="離線信用卡" Value="3" />
                            <dx:ListEditItem Text="分期付款" Value="4" />
                            <dx:ListEditItem Text="禮券" Value="5" />
                            <dx:ListEditItem Text="金融卡" Value="6" />
                            <dx:ListEditItem Text="Happy Go" Value="7" />
                        </Items>
                    </dx:ASPxComboBox>
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
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" OnClick="btnReset_Click">
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
        Width="98%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="調整日期" Caption="調整日期" />
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="門市名稱" />
            <dx:GridViewDataTextColumn FieldName="付款方式" Caption="付款方式" />
            <dx:GridViewDataTextColumn FieldName="調整原因" Caption="調整原因" />
            <dx:GridViewDataColumn FieldName="調整金額" Caption="調整金額" />
            <dx:GridViewDataTextColumn FieldName="會計科目" Caption="會計科目" />
            <dx:GridViewDataTextColumn FieldName="備註說明" Caption="備註說明" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster"></dx:ASPxGridViewExporter>
    </div>
</asp:Content>
