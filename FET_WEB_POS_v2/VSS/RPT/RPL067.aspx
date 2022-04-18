<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL067.aspx.cs" Inherits="VSS_RPT_RPL067"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (ASPxDateEdit1.GetText() != '' && ASPxDateEdit2.GetText() != '') {
                if (ASPxDateEdit1.GetValue() > ASPxDateEdit2.GetValue()) {
                    alert("[設備租借日期起值]不允許大於[設備租借日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }

            if (ASPxDateEdit3.GetText() != '' && ASPxDateEdit4.GetText() != '') {
                if (ASPxDateEdit3.GetValue() > ASPxDateEdit4.GetValue()) {
                    alert("[發票日期起值]不允許大於[發票日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--設備賠償明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="設備賠償明細表"></asp:Literal>
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
                    <table style="width: 254px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox1" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxTextBox2" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                <!--設備租借日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal6" runat="server" Text="設備租借日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ClientInstanceName="ASPxDateEdit1">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ClientInstanceName="ASPxDateEdit2">
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
                <!--發票日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal11" runat="server" Text="發票日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" ClientInstanceName="ASPxDateEdit3">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit4" runat="server" ClientInstanceName="ASPxDateEdit4">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="false" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--租借類別-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal9" runat="server" Text="租借類別"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="大寬頻" Value="3"/>
                            <dx:ListEditItem Text="維修備機" Value="2" />
                            <dx:ListEditItem Text="漫遊機" Value="1" />
                            <dx:ListEditItem Text="網卡" Value="4" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <!--處理人員-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="處理人員"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="cbSALE_PERSON" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                        SelectedIndex="0" Width="120px">
                    </dx:ASPxComboBox>
                </td>
                <!--員工編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal14" runat="server" Text="員工編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width:126px">
                    <uc1:PopupControl ID="popEmployee" runat="server" IsValidation="false" PopupControlName="EmployeesPopup" /></div>
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
            <dx:GridViewDataTextColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="門市名稱" />
            <dx:GridViewDataTextColumn FieldName="租借類別" Caption="租借類別" />
            <dx:GridViewDataTextColumn FieldName="商品型號" Caption="商品型號" />
            <dx:GridViewDataTextColumn FieldName="IMEI" Caption="IMEI" />
            <dx:GridViewDataTextColumn FieldName="設備租借日期" Caption="設備租借日期" />
            <dx:GridViewDataTextColumn FieldName="設備歸還日期" Caption="設備歸還日期" />
            <dx:GridViewDataTextColumn FieldName="發票日期" Caption="發票日期" />
            <dx:GridViewDataTextColumn FieldName="發票號碼" Caption="發票號碼" />
            <dx:GridViewDataTextColumn FieldName="賠償項目" Caption="賠償項目" />
            <dx:GridViewDataColumn FieldName="賠償金" Caption="賠償金" />
            <dx:GridViewDataTextColumn FieldName="處理人員" Caption="處理人員" />
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
