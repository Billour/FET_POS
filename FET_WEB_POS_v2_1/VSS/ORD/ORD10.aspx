<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD10.aspx.cs" Inherits="VSS_ORD_ORD10" Title="權重佔比分配" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>
    
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
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
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
                                <uc2:PopupControl ID="SelectStore1" runat="server" PopupControlName="StoresPopup"  />

<%--                             <table width="100px">
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
--%>                          </td>
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
                    <cc:aspxgridview id="gvMaster" keyfieldname="項次" clientinstancename="gvMaster" runat="server"
                        width="100%" settings-showtitlepanel="true">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="區域" Caption="<%$ Resources:WebResources, District %>">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="門市代號" Caption="<%$ Resources:WebResources, StoreNo %>">
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
                                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                                                AutoPostBack="false" CausesValidation="false">                            
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                             AutoPostBack="false" CausesValidation="false">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server"
                                AllowDragging="True" AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/Common/WeightRatingImportPopup.aspx"
                                PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
                                Width="400px" Height="350px"
                                HeaderText="資料匯入"  PopupElementID="btnImport" LoadingPanelID="lp">            
                                </cc:ASPxPopupControl>
                                
                                <cc:ASPxPopupControl ID="ASPxPopupControl2" runat="server"
                                AllowDragging="True" AllowResize="True" CloseAction="CloseButton" ContentUrl="~/VSS/Common/WeightRatingExportPopup.aspx"
                                PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false"
                                Width="400px" Height="350px"
                                HeaderText="資料匯出" PopupElementID="btnExport" LoadingPanelID="lp">            
                                </cc:ASPxPopupControl>

                                <dx:ASPxLoadingPanel ID="lp" runat="server"></dx:ASPxLoadingPanel>
                                
<%--                                  <asp:HiddenField ID="HiddenField1" runat="server" />
                              <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="Import"  PopupButtonID="btnImport"
                                    TargetControlID="HiddenField1" Width="400" Height="400" NavigateUrl="~/VSS/ORD/ORD10_Import.aspx" />
--%>                            </TitlePanel>
                        </Templates>
                    </cc:aspxgridview>
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
             </asp:Panel>
        </div>
    </div>

</asp:Content>
