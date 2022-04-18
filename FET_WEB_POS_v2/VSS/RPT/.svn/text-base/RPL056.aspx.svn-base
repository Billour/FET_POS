<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL056.aspx.cs" Inherits="VSS_RPT_RPL056"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '') {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue()) {
                    alert("[交易日期起值]不允許大於[交易日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--商品銷售排行表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL056 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <!--排序方式-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="排序方式"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="choice" Text="金額" Width="60px" Checked="true" />
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="choice" Text="數量" Width="60px" />
                </td>
                
                <!--區域-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                            <dx:ListEditItem Text="北一區" Value="1" />
                            <dx:ListEditItem Text="北二區" Value="1" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>                
            </tr>
            <tr>
                <!--門市編號-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal8" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 320px">
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
                
                <!--商品類別-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox1" runat="server" IsValidation="false" PopupControlName="ProductType" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox2" runat="server" IsValidation="false" PopupControlName="ProductType" />
                            </td>
                        </tr>
                    </table>
                </td>                
            </tr>
            <tr>
                <!--商品料號-->
                <td class="tdtxt">                    
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 320px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox4" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="ASPxComboBox5" runat="server" IsValidation="false" PopupControlName="ProductsPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
                
                <!--交易日期-->
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
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
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataColumn FieldName="門市編號" Caption="門市編號" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, StoreName %>" runat="server"
                Caption="<%$ Resources:WebResources, StoreName %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCategory %>"
                Caption="<%$ Resources:WebResources, ProductCategory %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCode %>" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductName %>" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="名次" Caption="名次" />
            <dx:GridViewDataTextColumn FieldName="數量" Caption="<%$ Resources:WebResources, Quantity %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="金額" Caption="金額" />
            <dx:GridViewDataColumn FieldName="數量銷貨比率" Caption="數量銷貨比率" />
            <dx:GridViewDataColumn FieldName="金額銷貨比率" Caption="金額銷貨比率" />
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
