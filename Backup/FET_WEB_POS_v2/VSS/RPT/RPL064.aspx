<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL064.aspx.cs" Inherits="VSS_RPT_RPL064"
    MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (txtOrdDateStart.GetText() != '' && txtOrdDateEnd.GetText() != '')
            {
                if (txtOrdDateStart.GetValue() > txtOrdDateEnd.GetValue())
                {
                    alert("[退倉日期起值]不允許大於[退倉日期訖值]，請重新輸入!");
                    _gvEventArgs.processOnServer = false;
                    return;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--退倉明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="退倉明細表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <%--  <asp:Literal ID="Literal12" runat="server" Text=""></asp:Literal>：--%>
                </td>
                <td class="tdval">
                    <asp:RadioButton ID="rType1" runat="server" GroupName="choice" Text="一般商品" Checked="true" />
                    <asp:RadioButton ID="rType2" runat="server" GroupName="choice" Text="寄銷商品" />
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!---->
                    <asp:Literal ID="Literal2" runat="server" Text="退倉單號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="txtRtnNo" runat="server" Text=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="退倉日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <td>
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                        </td>
                        <td>
                            <div style="width: 120px;">
                                <dx:ASPxDateEdit ID="txtOrdDateStart" ClientInstanceName="txtOrdDateStart" runat="server" EditFormatString="yyyy/MM/dd">
                                  <%--  <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>--%>
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                        <td>
                            <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtOrdDateEnd" ClientInstanceName="txtOrdDateEnd" runat="server" EditFormatString="yyyy/MM/dd">
                              <%--  <ValidationSettings CausesValidation="false">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>--%>
                            </dx:ASPxDateEdit>
                        </td>
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
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtSProdNo" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtEProdNo" runat="server" Width="100px">
                                </dx:ASPxTextBox>
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
                    OnClick="btnReset_Click">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                    OnClick="btnExport_Click">
                     <ClientSideEvents Click="function(s,e){ CheckDate(s, e); }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"  OnPageIndexChanged="gvMaster_PageIndexChanged"
        Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="RTNDATE" Caption="<%$ Resources:WebResources, WarehousedDate%>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="RTNNO" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo%>" />
            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="UNIT" Caption="<%$ Resources:WebResources, Unit%>" />
            <dx:GridViewDataColumn FieldName="UNOPENQTY" Caption="<%$ Resources:WebResources, OpenedQty%>" />
            <dx:GridViewDataColumn FieldName="OPENQTY" Caption="<%$ Resources:WebResources, SealedQuantity%>" />
            <dx:GridViewDataColumn FieldName="RTNQTY" Caption="<%$ Resources:WebResources, ReturnQuantity %>" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
    </dx:ASPxGridViewExporter>
</asp:Content>
