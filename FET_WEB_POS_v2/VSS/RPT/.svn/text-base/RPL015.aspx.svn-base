<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL015.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL015" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[跨月作廢日期起值]不允許大於[跨月作廢日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--統一發票明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL015 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--門市編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal8" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 345px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox2" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox3" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--跨月作廢日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="跨月作廢日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" ClientInstanceName="txtOrdDateStart">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" ClientInstanceName="txtOrdDateEnd">
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
                <!--是否開立折讓單：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CollectionClass %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="Y" Value="Y" />
                            <dx:ListEditItem Text="N" Value="N" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <!--是否收回：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal2" runat="server" Text="是否收回"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="Y" Value="Y" />
                            <dx:ListEditItem Text="N" Value="N" />
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
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="門市編號及名稱" Caption="門市編號及名稱" />
            <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="發票號碼" />
            <dx:GridViewDataTextColumn FieldName="聯式" Caption="聯式" />
            <dx:GridViewDataTextColumn FieldName="跨月作廢日期" Caption="跨月作廢日期" />
            <dx:GridViewDataTextColumn FieldName="原交易日期" Caption="原交易日期" />
            <dx:GridViewDataTextColumn FieldName="機台 / 原序號" Caption="機台 / 原序號" />
            <dx:GridViewDataTextColumn FieldName="統一編號" Caption="統一編號" />
            <dx:GridViewDataColumn FieldName="現金" Caption="現金" />
            <dx:GridViewDataColumn FieldName="信用卡" Caption="信用卡" />
            <dx:GridViewDataColumn FieldName="分期付款" Caption="分期付款" />
            <dx:GridViewDataColumn FieldName="支票" Caption="支票" />
            <dx:GridViewDataColumn FieldName="未稅金額" Caption="未稅金額" />
            <dx:GridViewDataColumn FieldName="稅額" Caption="稅額" />
            <dx:GridViewDataColumn FieldName="發票金額" Caption="發票金額" />
            <dx:GridViewDataTextColumn FieldName="是否開立折讓單" Caption="是否開立折讓單" />
            <dx:GridViewDataTextColumn FieldName="是否收回" Caption="是否收回" />
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
