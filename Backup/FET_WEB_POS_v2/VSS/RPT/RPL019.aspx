<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL019.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL019" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--促銷及折扣設定項目明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL019 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <!--折扣類別-->
                    <asp:Literal ID="lbltype" runat="server" Text="<%$ Resources:WebResources, DiscountClass %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddltype" runat="server" Width="120px" ValueType="System.String"
                        SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="一般" Value="1" />
                            <dx:ListEditItem Text="舊機回收" Value="2" />
                            <dx:ListEditItem Text="租賃" Value="3" />
                            <dx:ListEditItem Text="特殊折扣" Value="4" />
                            <dx:ListEditItem Text="HappyGO折扣" Value="5" />
                            <dx:ListEditItem Text="贈品設定" Value="6" />
                            <dx:ListEditItem Text="加價購" Value="7" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--折扣料號-->
                    <asp:Literal ID="lblno" runat="server" Text="折扣料號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtno" runat="server" Width="100px">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--是否失效-->
                    <asp:Literal ID="lblerror" runat="server" Text="是否失效"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlerror" runat="server" Width="120px" ValueType="System.String"
                        SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="N" Value="N" />
                            <dx:ListEditItem Text="Y" Value="Y" />
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
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    OnClick="btnReset_Click">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" OnPageIndexChanged="gvMaster_PageIndexChanged"
        Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="折扣類別" runat="server" Caption="<%$ Resources:WebResources, DiscountClass %>"/>
            <dx:GridViewDataTextColumn FieldName="折扣料號" runat="server" Caption="折扣料號"/>
            <dx:GridViewDataTextColumn FieldName="折扣名稱" Caption="<%$ Resources:WebResources, DiscountName %>" />
            <dx:GridViewDataColumn FieldName="折扣金額" Caption="<%$ Resources:WebResources, DiscountAmount %>"/>
            <dx:GridViewDataTextColumn FieldName="生效日" Caption="<%$ Resources:WebResources, EffectiveDate %>" />
            <dx:GridViewDataTextColumn FieldName="失效日" Caption="<%$ Resources:WebResources, ExpiryDate %>" />
            <dx:GridViewDataColumn FieldName="成本中心" Caption="<%$ Resources:WebResources, CostCenter %>" />
            <dx:GridViewDataColumn FieldName="成本中心負擔金額" Caption="<%$ Resources:WebResources, Costcenteramount %>" />
            <dx:GridViewDataTextColumn FieldName="科目1" Caption="<%$ Resources:WebResources, Segnment_1 %>" />
            <dx:GridViewDataTextColumn FieldName="科目2" Caption="<%$ Resources:WebResources, Segnment_2 %>" />
            <dx:GridViewDataTextColumn FieldName="科目3" Caption="<%$ Resources:WebResources, Segnment_3 %>" />
            <dx:GridViewDataTextColumn FieldName="科目4" Caption="<%$ Resources:WebResources, Segnment_4 %>" />
            <dx:GridViewDataTextColumn FieldName="科目5" Caption="<%$ Resources:WebResources, Segnment_5 %>" />
            <dx:GridViewDataTextColumn FieldName="科目6" Caption="<%$ Resources:WebResources, Segnment_6 %>" />
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
