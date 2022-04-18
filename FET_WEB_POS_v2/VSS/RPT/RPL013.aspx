<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL013.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL013" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtSMonth.GetText() != '' && txtEMonth.GetText() != '') {
                if (txtSMonth.GetValue() > txtEMonth.GetValue()) {
                    alert("[月份起值]不允許大於[月份訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--門市申報稅額小計-->
        <asp:Literal ID="Literal1" runat="server" Text="門市報稅彙總表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--月份：-->
                <td class="tdtxt">
                    <asp:Literal ID="lblSTK_DATE" runat="server" Text="月份"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtSDate" runat="server" Width="100px" ClientInstanceName="txtSMonth">
                                    <%--<ClientSideEvents ValueChanged="function(s, e){ chkIsMonth(s, e) ; }" />--%>
                                    <MaskSettings ErrorText="請輸入正確格是" Mask="yyyy/MM" ShowHints="false" />
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtEDate" runat="server" Width="100px" ClientInstanceName="txtEMonth">
                                    <%--<ClientSideEvents ValueChanged="function(s, e){ chkIsMonth(s, e) ; }" />--%>
                                    <MaskSettings ErrorText="請輸入正確格是" Mask="yyyy/MM" ShowHints="false" />
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--課稅別：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal7" runat="server" Text="課稅別"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="0" />
                            <dx:ListEditItem Text="應稅" Value="1" />
                            <dx:ListEditItem Text="零稅" Value="2" />
                            <dx:ListEditItem Text="免稅率" Value="3" />
                            <%--<dx:ListEditItem Text="作廢" Value="Y" />--%>
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <!--門市編號：-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width:115px">
                                <div style="width:106px">
                                <uc1:PopupControl ID="txtStoreNo_S" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                                </div>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                                            <div style="width:115px">
                                <div style="width:106px">
                                <uc1:PopupControl ID="txtStoreNo_E" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                                                                </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td> </tr> </table>
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
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="STORE_NAME" Caption="門市名稱" />
            <dx:GridViewDataTextColumn FieldName="UNI_NO" Caption="門市統編" />
            <dx:GridViewDataTextColumn FieldName="TAXNO" Caption="門市稅編" />
            <dx:GridViewDataColumn FieldName="INVO_AMT" Caption="電子發票申報銷售金額" />
            <dx:GridViewDataColumn FieldName="INVO_TAX" Caption="電子發票申報稅額" />
            <dx:GridViewDataColumn FieldName="MINVO_AMT_3" Caption="補登三聯式手開申報銷售金額" />
            <dx:GridViewDataColumn FieldName="MINVO_TAX_3" Caption="補登三聯式手開申報稅額" />
            <dx:GridViewDataColumn FieldName="MINVO_AMT_2" Caption="補登二聯式手開發票銷售金額" />
            <dx:GridViewDataColumn FieldName="MINVO_TAX_2" Caption="補登二聯式手開發票稅額" />
            <dx:GridViewDataColumn FieldName="INVO_PAYOFF_AMT" Caption="折讓單申報銷售金額" />
            <dx:GridViewDataColumn FieldName="INVO_PAYOFF_TAX" Caption="折讓單申報稅額" />
            <dx:GridViewDataColumn FieldName="TOTAL_AMT" Caption="門市申報銷售金額小計" />
            <dx:GridViewDataTextColumn FieldName="TOTAL_TAX" Caption="門市申報稅額小計" />
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
