<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV04.aspx.cs" Inherits="VSS_INV_INV04" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=250,left=380,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--退倉設定查詢作業-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingSearch %>"></asp:Literal>
                    </td>
                    <td align="right">
                   
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox1" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--門市編號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                    </td>
                    <td nowrap="nowrap">
                       <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"  />
                    
                       <%-- <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox2" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="chooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        SkinID="PopupButton">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品編號-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td nowrap="nowrap">
                       <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="ProductsPopup"  />
                    
                        <%--<table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox4" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="chooseButton2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        SkinID="PopupButton" />
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開單日-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceiptDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 120px;">
                                        <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 120px;">
                                        <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                            <ValidationSettings CausesValidation="false">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </div>
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉狀態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="請選擇" Selected="true" />
                                <dx:ListEditItem Text="已存檔" />
                                <dx:ListEditItem Text="已傳輸" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
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
                            AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="退倉單號"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
            OnPageIndexChanged="gvMaster_PageIndexChanged" Settings-ShowTitlePanel="true">
            <Columns>
                <dx:GridViewDataHyperLinkColumn PropertiesHyperLinkEdit-NavigateUrlFormatString="~/VSS/INV/INV05.aspx?dno={0}"
                    FieldName="退倉單號" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"
                    PropertiesHyperLinkEdit-Style-Font-Underline="true">
                </dx:GridViewDataHyperLinkColumn>
                <dx:GridViewDataColumn FieldName="退倉狀態" Caption="<%$ Resources:WebResources, ReturnWarehousingStatus %>" />
                <dx:GridViewDataColumn FieldName="退倉開始日" Caption="<%$ Resources:WebResources, ReturnWarehousingStartDate %>" />
                <dx:GridViewDataColumn FieldName="退倉結束日" Caption="<%$ Resources:WebResources, ReturnWarehousingEndDate %>" />
                <dx:GridViewDataColumn FieldName="退倉原因" Caption="<%$ Resources:WebResources, ReasonForWarehousing %>" />
                <dx:GridViewDataColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            <SettingsPager PageSize="5">
            </SettingsPager>
            <Templates>
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnExprot" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                    Visible="true">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
                <EmptyDataRow>
                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                </EmptyDataRow>
            </Templates>
        </cc:ASPxGridView>
    </div>
<%--    <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server" EnableViewState="False"
        PopupElementID="chooseButton1" TargetElementID="TextBox2">
    </cc:ASPxPopupControl>
    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
        PopupElementID="chooseButton2" TargetElementID="TextBox4">
    </cc:ASPxPopupControl>
--%>    
</asp:Content>
