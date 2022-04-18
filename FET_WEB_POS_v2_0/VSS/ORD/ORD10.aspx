<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD10.aspx.cs" Inherits="VSS_ORD_ORD10" Title="權重佔比分配" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--權重佔比分配-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, WeightRatingAssignment %>"></asp:Literal>
                    </td>
                    <td align="right">
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div>
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--區域-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, District %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:aspxcombobox id="ASPxComboBox1" runat="server" width="100px">
                                <Items>
                                    <dx:ListEditItem Value="1" Text="北一區" />
                                    <dx:ListEditItem Value="2" Text="中一區" />
                                    <dx:ListEditItem Value="3" Text="南一區" />
                                </Items>
                            </dx:aspxcombobox>
                        </td>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <table width="100px">
                                <tr>
                                    <td>
                                        <dx:aspxtextbox id="txtStoreNo" runat="server" width="170px">
                                        </dx:aspxtextbox>
                                    </td>
                                    <td>
                                        <dx:aspxbutton id="ASPxButton1" runat="server" width="15px" skinid="PopupButton"
                                            text="<%$ Resources:WebResources, Choose %>">
                                            <ClientSideEvents Click="function(s,e){openwindow('../SAL/SAL01_chooseStore.aspx',550,350);return false;}" />
                                        </dx:aspxbutton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:aspxbutton id="btnSearch" onclick="btnSearch_Click" runat="server" text="<%$ Resources:WebResources, Search %>">
                            </dx:aspxbutton>
                        </td>
                        <td>
                            <dx:aspxbutton id="btnReset" runat="server" text="<%$ Resources:WebResources, Reset %>">
                            </dx:aspxbutton>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <div id="divContent" runat="server">
                    <dx:aspxgridview id="gvMaster" keyfieldname="項次" clientinstancename="gvMaster" runat="server"
                        width="100%" settings-showtitlepanel="true">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="區域" Caption="<%$ Resources:WebResources, District %>">
                            </dx:GridViewDataColumn><%--"<%$ Resources:WebResources, StoreNo %>"--%>
                            <dx:GridViewDataColumn FieldName="門市代號" Caption="門市代號">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="比率" Caption="<%$ Resources:WebResources, Ratio %>">
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Templates>
                            <EmptyDataRow>
                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                            </EmptyDataRow>
                            <TitlePanel>
                                <table align="left">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnImport" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="Import"  PopupButtonID="btnImport"
                                    TargetControlID="HiddenField1" Width="400" Height="400" NavigateUrl="~/VSS/ORD/ORD10_Import.aspx" />
                            </TitlePanel>
                        </Templates>
                    </dx:aspxgridview>
                </div>
                <div class="seperate">
                </div>
                <div id="Div03" style="width: 90%; text-align: right;" visible="false">
                    <!--比率統計-->
                    <asp:Literal ID="Literal01" runat="server" Text="比率統計"></asp:Literal>
                    <asp:Literal ID="Literal02" runat="server" Text="："></asp:Literal>
                    <asp:Literal ID="Literal03" runat="server" Text="100%"></asp:Literal>
                </div>
                <div class="seperate">
                </div>
                <div class="btnPosition">
                    <table align="center">
                        <tr>
                            <td>
                                <dx:aspxbutton id="OkButton" runat="server" text="<%$ Resources:WebResources, Ok %>">
                                </dx:aspxbutton>
                            </td>
                            <td>
                                <dx:aspxbutton id="CancelButton" runat="server" text="<%$ Resources:WebResources, Cancel %>">
                                </dx:aspxbutton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
