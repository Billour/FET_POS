<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL038.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL038" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--促代新增商品檢核表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL038 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--促銷生效日期-->
                <td class="tdtxt">
                    <asp:Literal ID="lblB_DATE" runat="server" Text="<%$ Resources:WebResources, EffectiveDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblB_DATE_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtB_DATE_S" runat="server" ClientInstanceName="txtSDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="lblB_DATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtB_DATE_E" runat="server" ClientInstanceName="txtEDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--促銷代碼-->
                <td class="tdtxt">
                    <asp:Literal ID="lblPROMO_NO" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 320px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblPROMO_NO_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPROMO_NO_S" runat="server" PopupControlName="PromotionsPopupOnly" />
                            </td>
                            <td>
                                <asp:Literal ID="lblPROMO_NO_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPROMO_NO_E" runat="server" PopupControlName="PromotionsPopupOnly" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!--商品類別-->
                <td class="tdtxt">
                    <asp:Literal ID="lblPRODTYPE" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <div style="width: 125px">
                        <uc1:PopupControl ID="ddlPRODTYPE" runat="server" IsValidation="false" PopupControlName="ProductCategory" />
                    </div>
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
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" OnClick="btnReset_Click"
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                    OnClick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="98%"
        OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn  FieldName="促銷代碼"          Caption="促銷代碼"/>
            <dx:GridViewDataColumn      FieldName="促銷名稱"          Caption="促銷名稱"/>
            <dx:GridViewDataTextColumn  FieldName="商品料號"          Caption="商品料號"/>
            <dx:GridViewDataColumn      FieldName="商品名稱"          Caption="商品名稱"/>
            <dx:GridViewDataColumn      FieldName="商品類別"          Caption="商品類別"/>
            <dx:GridViewDataColumn      FieldName="ERP Attribute1"    Caption="ERP Attribute1" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
    </dx:ASPxGridViewExporter>
</asp:Content>
