<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV18.aspx.cs" Inherits="VSS_INV_INV18" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--庫存調整查詢作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentSerch %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div style="text-align: left;">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox9" runat="server" Width="80px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>：
                        </td>
                        <td class="" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整門市-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox16" runat="server" Width="80px">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--商品料號-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"  />
                                    </td>
                                  <%--  <td>
                                        <dx:ASPxTextBox ID="TextBox3" runat="server" Width="80px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            AutoPostBack="false" SkinID="PopupButton" />
                                    </td>--%>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                         <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="ProductsPopup"  />
                                    </td>
                                   <%-- <td>
                                        <dx:ASPxTextBox ID="TextBox4" runat="server" Width="80px">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnChooseProduct1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            AutoPostBack="false" SkinID="PopupButton" />
                                    </td>--%>
                                </tr>
                            </table>
                          <%--  <cc:ASPxPopupControl ID="productsPopup1" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="btnChooseProduct" TargetElementID="TextBox3" LoadingPanelID="lp">
                                <ContentCollection>
                                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </cc:ASPxPopupControl>
                            <cc:ASPxPopupControl ID="productsPopup2" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="btnChooseProduct1" TargetElementID="TextBox4" LoadingPanelID="lp">
                                <ContentCollection>
                                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </cc:ASPxPopupControl>
                            <dx:ASPxLoadingPanel ID="lp" runat="server">
                            </dx:ASPxLoadingPanel>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="調整單號"
                Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                OnPageIndexChanged="gvMaster_PageIndexChanged">
                <Columns>
                    <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/INV/INV18_1.aspx?dno={0}"
                        FieldName="調整單號" Caption="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"
                        PropertiesHyperLinkEdit-Style-Font-Underline="true">
                    </dx:GridViewDataHyperLinkColumn>
                    <dx:GridViewDataColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" />
                    <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" />
                    <dx:GridViewDataColumn FieldName="調整日期" Caption="<%$ Resources:WebResources, AdjustmentDate %>" />
                    <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>" />
                    <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                    <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsPager PageSize="5">
                </SettingsPager>
            </cc:ASPxGridView>
        </div>
    </div>
    <%--<cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
        PopupElementID="btnChooseProduct" TargetElementID="TextBox3">
        <ClientSideEvents Init="function(s, e) {
                    var iframe = s.GetContentIFrame();                   
                    iframe.popupArguments = {};
                    iframe.contentLoaded = false;
                    var controlCollection = ASPxClientControl.GetControlCollection();                
                    iframe.popupArguments.popupContainer = controlCollection.Get('productsPopup');                                                                   
                    var targetElementId = 'TextBox3';                                                                                        
                    iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId) 
                        || document.getElementById(targetElementId);
                    }"></ClientSideEvents>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>--%>
</asp:Content>
