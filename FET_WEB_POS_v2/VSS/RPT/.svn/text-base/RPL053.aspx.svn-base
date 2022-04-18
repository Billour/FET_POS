<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL053.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL053" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--庫存日報表-->
        <asp:Literal ID="Literal1" runat="server" Text="庫存日報表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CheckOption %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:RadioButton ID="RadioAll" runat="server" GroupName="choice" Text="全部" />
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="choice" Text="一般商品"
                        Checked="true" />
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="choice" Text="寄銷商品" />
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal8" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTOREStart" runat="server" PopupControlName="StoresPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtSTOREEnd" runat="server" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--交易日期-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server">
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
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server">
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
                <td class="tdtxt">
                    <!--商品類別-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtProductTypeNoS" runat="server" PopupControlName="ProductType" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtProductTypeNoE" runat="server" PopupControlName="ProductType" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品代號-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtProdNo_S" runat="server" PopupControlName="ProductsPopup" />
                            </td>
                            <td>
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtProdNo_E" runat="server" PopupControlName="ProductsPopup" />
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
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    OnClick="btnReset_Click"   AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" OnClick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <%--<div>
        <td>
            <div>
                <table border="1" cellpadding="0" cellspacing="0" style="color: #000000; background-color: #DCDCDC; border : solid 1px #A0A0A0 "
                    width="1400px">
                    <tr>
                        <td width="390px">　</td>
                        <td width="800px" align="center">
                            銷售倉
                        </td>
                        <td width="300px">　</td>
                    </tr>
                </table>
            </div>
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        OnPageIndexChanged="gvMaster_PageIndexChanged" Width="1400px" AutoGenerateColumns="False"
        IsClearStatus="True">
        <Columns>
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductCode %>" Caption="<%$ Resources:WebResources, ProductCode %>"
                Width="100px" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductName %>" Caption="<%$ Resources:WebResources, ProductName%>"
                Width="250px" />
            <dx:GridViewDataColumn FieldName="展示倉" Caption="展示倉" Width="100px" />
            <dx:GridViewDataColumn FieldName="期初" Caption="期初" Width="100px" />
            <dx:GridViewDataColumn FieldName="進貨" Caption="進貨" Width="100px" />
            <dx:GridViewDataColumn FieldName="銷貨" Caption="銷貨" Width="100px" />
            <dx:GridViewDataColumn FieldName="銷退" Caption="銷退" Width="100px" />
            <dx:GridViewDataColumn FieldName="退倉" Caption="退倉" Width="100px" />
            <dx:GridViewDataColumn FieldName="移出" Caption="移出" Width="100px" />
            <dx:GridViewDataColumn FieldName="撥入" Caption="撥入" Width="100px" />
            <dx:GridViewDataColumn FieldName="調整" Caption="調整" Width="100px" />
            <dx:GridViewDataColumn FieldName="驗退" Caption="驗退" Width="100px" />
            <dx:GridViewDataColumn FieldName="維修倉" runat="server" Caption="維修倉" Width="100px" />
            <dx:GridViewDataColumn FieldName="銷售倉期末" runat="server" Caption="銷售倉期末" Width="100px" />
            <dx:GridViewDataColumn FieldName="租賃倉期末" Caption="租賃倉期末" Width="100px" />
        </Columns>
        <SettingsText EmptyDataRow=" " />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>--%>
    <div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
            OnPageIndexChanged="gvMaster_PageIndexChanged">
            <Columns>
                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductCode %>" Caption="<%$ Resources:WebResources, ProductCode %>"
                    Width="100px" />
                <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductName %>" Caption="<%$ Resources:WebResources, ProductName%>"
                    Width="250px" />
                <dx:GridViewDataColumn FieldName="展示倉" Caption="展示倉" />
                <dx:GridViewDataTextColumn>
                    <HeaderTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" align="center" width="100%">
                            <tr>
                                <td colspan="9" align ="center"  style="border-bottom: 1px #8E8E8E  solid">
                                    銷售倉
                                </td>
                            </tr>
                            <tr>
                                <td width="11.11%">
                                    期初
                                </td>
                                <td width="11.11%">
                                    進貨
                                </td>
                                <td width="11.11%">
                                    銷貨
                                </td>
                                <td width="11.11%">
                                    銷退
                                </td>
                                <td width="11.11%">
                                    退倉
                                </td>
                                <td width="11.11%">
                                    移出
                                </td>
                                <td width="11.11%">
                                    撥入
                                </td>
                               <td width="11.11%">
                                    調整
                                </td>
                                <td width="11.11%">
                                    驗退
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <DataItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" align="right" width="100%">
                            <tr>
                                <td align="center" width="11.11%" >
                                    <dx:ASPxLabel ID="lblNetbook" runat="server" Text='<%#BIND("期初")  %>' />
                                </td>
                                <td align="center" width="11.11%">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#BIND("進貨")  %>' />
                                </td>
                                <td align="center" width="11.11%">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text='<%#BIND("銷貨")  %>' />
                                </td>
                                <td align="center" width="11.11%">
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text='<%#BIND("銷退")  %>' />
                                </td>
                                <td align="center" width="11.11%">
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text='<%#BIND("退倉")  %>' />
                                </td>
                                <td align="center" width="11.11%">
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text='<%#BIND("移出")  %>' />
                                </td>
                                <td align="center" width="11.11%">
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text='<%#BIND("撥入")  %>' />
                                </td>
                                <td align="center" width="11.11%">
                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text='<%#BIND("調整")  %>' />
                                </td>
                                <td align="center" width="11.11%">
                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text='<%#BIND("驗退")  %>' />
                                </td>
                            </tr>
                        </table>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn FieldName="維修倉" runat="server" Caption="維修倉" />
                <dx:GridViewDataColumn FieldName="銷售倉期末" runat="server" Caption="銷售倉期末" />
                <dx:GridViewDataColumn FieldName="租賃倉期末" Caption="租賃倉期末"  />
            </Columns>
            <SettingsPager PageSize="10">
            </SettingsPager>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <Settings ShowTitlePanel="false" />
        </cc:ASPxGridView>
    </div>
    <div class="seperate">
    </div>
</asp:Content>
