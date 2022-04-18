<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL037.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL037" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[促銷生效日期起值]不允許大於[促銷生效日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--促銷及補貼檢核表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL037 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <!--促銷生效日期-->
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="促銷生效日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 340px">
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
            <!--促銷代碼-->
            <td class="tdtxt">
                <asp:Literal ID="lblPRODTYPENO" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
            </td>
            <td class="tdval">
                <table style="width: 340px">
                    <tr>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                        </td>
                        <td>
                            <uc1:PopupControl ID="pupPRODTYPENO_S" runat="server" PopupControlName="PromotionsPopupOnly"
                                Width="200px" />
                        </td>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        </td>
                        <td>
                            <uc1:PopupControl ID="pupPRODTYPENO_E" runat="server" PopupControlName="PromotionsPopupOnly"
                                Width="200px" />
                        </td>
                    </tr>
                </table>
            </td>
            <tr>
            </tr>
            <!--商品類別-->
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 340px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductCategory" 
                                    Width="150px" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="ProductCategory"
                                    Width="150px" />
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
        Width="100%" OnPageIndexChanged = "gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="促銷代碼" Caption="促銷代碼" />
            <dx:GridViewDataTextColumn FieldName="促銷名稱" runat="server" Caption="促銷名稱" />
            <dx:GridViewDataTextColumn FieldName="促銷生效日" Caption="促銷生效日" />
            <dx:GridViewDataColumn FieldName="促銷失效日" Caption="促銷失效日" />
            <dx:GridViewDataColumn FieldName="促銷補貼金額" Caption="促銷補貼金額" />
            <dx:GridViewDataColumn FieldName="基準補貼金額" Caption="基準補貼金額" />
            <dx:GridViewDataColumn FieldName="商品群組" Caption="商品群組" />
            <dx:GridViewDataColumn FieldName="商品類別" Caption="商品類別" />
            <dx:GridViewDataColumn FieldName="補貼金額" Caption="補貼金額" />
            <dx:GridViewDataColumn FieldName="ERP Attribute1" Caption="ERP Attribute1" />
            <dx:GridViewDataTextColumn FieldName="轉換值" runat="server" Caption="轉換值" />
            <dx:GridViewDataTextColumn FieldName="生效日" runat="server" Caption="生效日" />
            <dx:GridViewDataTextColumn FieldName="失效日" runat="server" Caption="失效日" />
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
