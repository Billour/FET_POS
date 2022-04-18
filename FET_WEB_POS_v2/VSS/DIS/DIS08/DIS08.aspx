<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DIS08.aspx.cs" Inherits="VSS_DIS_DIS08" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtSDate_S.GetText() != '' && txtSDate_E.GetText() != '') {
                if (txtSDate_S.GetValue() > txtSDate_E.GetValue()) {
                    alert("[生效起日訖值]不允許小於[生效起日起值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    _gvSender.Focus();
                }
            }
            if (txtEDate_S.GetText() != '' && txtEDate_E.GetText() != '') {
                if (txtEDate_S.GetValue() > txtEDate_E.GetValue()) {
                    alert("[生效訖日起值]不允許大於[生效訖日訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    _gvSender.Focus();
                }
            }
    //        if ((txtSDate_S.GetText() != '' && txtSDate_E.GetText() == '') || (txtSDate_S.GetText() == '' && txtSDate_E.GetText() != '') ||
    //            (txtEDate_S.GetText() != '' && txtEDate_E.GetText() == '') || (txtEDate_S.GetText() == '' && txtEDate_E.GetText() != '')) {
    //            alert("生效起日或生效訖日的 起/訖 兩欄位只可是都空值或都有值兩種情況!");
    //            _gvEventArgs.processOnServer = false;
    //            _gvSender.Focus();
    //        }

        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <div class="titlef" align="left">
        <asp:Literal ID="lblTitle" runat="server" Text="組合促銷轉換值查詢"></asp:Literal>
    </div>

    <div class="criteria">
        <table>
            <tr>
                <td align="right">
                    <!--分類-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Type %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxComboBox ID="cbbSelectClassType" runat="server" ValueType="System.String" 
                        Width="170px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" Selected="true" />
                            <dx:ListEditItem Text="商品分類" Value="0" />
                            <dx:ListEditItem Text="商品料號" Value="1" />
                        </Items>
                    </dx:ASPxComboBox>
                    
                </td>
                <td>&nbsp;</td>
                <td align="right">
                    <!--生效起日-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, EffectiveStartDate %>"></asp:Literal>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                            <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="txtSDate_S" runat="server" ClientInstanceName="txtSDate_S"></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="txtSDate_E" runat="server" ClientInstanceName="txtSDate_E" ></dx:ASPxDateEdit></td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <!--商品分類-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCategory1 %>"></asp:Literal>：
                </td>
                <td>
                    <dx:ASPxComboBox ID="cbbPs_Type" runat="server" ValueType="System.String" 
                        Width="170px"></dx:ASPxComboBox>
                </td>
                <td>&nbsp;</td>
                <td align="right">
                    <!--生效訖日-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, EffectiveEndDate %>"></asp:Literal>：
                </td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                            <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="txtEDate_S" runat="server" ClientInstanceName="txtEDate_S" ></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="txtEDate_E" runat="server" ClientInstanceName="txtEDate_E" ></dx:ASPxDateEdit></td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    <!--商品料號-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td style="width:231px">
                    <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>

    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="center">
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnQuery_Click">
                        <ClientSideEvents Click="function(s, e) {  CheckDate(s, e); }" />
                   </dx:ASPxButton>
                </td>
                <td>&nbsp;</td>
                <td align="center">
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                     </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>

    <div class="seperate"></div>

    <div class="SubEditBlock">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                  <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%" KeyFieldName="ITEM_NO" 
                    OnPageIndexChanged="gvMaster_PageIndexChanged" >
                        <Columns>
                            <dx:GridViewDataColumn FieldName="ITEM_NO" Caption="<%$ Resources:WebResources, Items %>" />
                            <dx:GridViewDataColumn FieldName="CATEGORY" Caption="<%$ Resources:WebResources, ProductCategory1 %>" />
                            <dx:GridViewDataColumn FieldName="B_PROD_NO" Caption="<%$ Resources:WebResources, ProductCodeStart %>" />
                            <dx:GridViewDataColumn FieldName="B_PRODNAME" Caption="<%$ Resources:WebResources, ProductNameStart %>"/>
                            <dx:GridViewDataColumn FieldName="E_PROD_NO" Caption="<%$ Resources:WebResources, ProductCodeEnd %>" />
                            <dx:GridViewDataColumn FieldName="E_PRODNAME" Caption="<%$ Resources:WebResources, ProductNameEnd %>"/>
                            <dx:GridViewDataColumn FieldName="B_DATE" Caption="<%$ Resources:WebResources, EffectiveStartDate %>"/>
                            <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EffectiveEndDate %>"/>
                            <dx:GridViewDataColumn FieldName="TRANS_VALUE" Caption="<%$ Resources:WebResources, TransformedValue %>"/>
                            <dx:GridViewDataColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, MaintainedBy %>"/>                           
                            <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"/>
                        </Columns>
                        <SettingsPager PageSize="10"></SettingsPager>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                   </cc:ASPxGridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnQuery" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    
</asp:Content>
