<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="SAL031.aspx.cs" Inherits="VSS_SAL_SAL031" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "商品編號查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');

        }
        function openwindow2(url, width, height) {
            window.open(url, "促銷代碼查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');

        }
        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    換貨作業
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="tdtxt">
                    門市編號：
                </td>
                <td class="tdval" width="80px">
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"  />
                    <%--<table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="110">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <%--<dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    SkinID="PopupButton" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){openwindow('../SAL/SAL01_chooseStore.aspx',450,350);return false;}" />
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnChooseStore" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false" SkinID="PopupButton" />
                            </td>
                        </tr>
                    </table>--%>
                </td>
                <td class="tdtxt">
                    <!--交易日期-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>" />：
                </td>
                <td class="tdval" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
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
                <td class="tdtxt">
                    <!--機台-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CashRegister %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server" Width="110">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--客戶門號-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox8" runat="server" Width="110">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--狀態-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Status %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server" ValueType="System.String" AutoResizeWithContainer="true">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="ALL" />
                            <dx:ListEditItem Text="已結帳" Value="已結帳" Selected="true" />
                            <dx:ListEditItem Text="已作廢" Value="已作廢" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--交易序號-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server" Width="110">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--促銷代碼-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                </td>
                <td class="tdval">
                     <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="PromotionsPopup"  />
                    <%--<table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" CssClass="tbWidthFormat" Width="110">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <%--<dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){openwindow2('SAL02_searchDiscountNo.aspx',450,350);return false;}" />
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnChoosePromotionCode" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false" SkinID="PopupButton" />
                            </td>
                        </tr>
                    </table>--%>
                </td>
                <td class="tdtxt">
                    <!--銷售人員-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList3" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                        SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="-請選擇-" Value="-請選擇-" />
                            <dx:ListEditItem Text="劉光俊" Value="劉光俊" />
                            <dx:ListEditItem Text="林雅玲" Value="林雅玲" />
                            <dx:ListEditItem Text="游惠貞" Value="游惠貞" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票號碼-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox5" runat="server" Width="110">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--商品編號-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
                </td>
                <td class="tdval">
                    <uc1:PopupControl ID="PopupControl3" runat="server" PopupControlName="ProductsPopup"  />
                    <%--<table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" CssClass="tbWidthFormat" Width="110">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <%--<dx:ASPxButton ID="Button9" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){openwindow2('SAL01_searchProductNo.aspx',450,350);return false;}" />
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnChooseProducts" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false" SkinID="PopupButton" />
                            </td>
                        </tr>
                    </table>--%>
                </td>
                <td class="tdtxt">
                    <!--付款方式-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PaymentMethod %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" AutoResizeWithContainer="true"
                        SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Text="-請選擇-" Value="-請選擇-" />
                            <dx:ListEditItem Text="現金" Value="現金" />
                            <dx:ListEditItem Text="信用卡" Value="信用卡" />
                            <dx:ListEditItem Text="禮券" Value="禮券" />
                            <dx:ListEditItem Text="金融卡" Value="金融卡" />
                            <dx:ListEditItem Text="HappyGo" Value="HappyGo" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
        </table>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){resetForm(aspnetForm);}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="項次"
                AutoGenerateColumns="False" Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
                <SettingsPager PageSize="5">
                </SettingsPager>
                <Columns>
                    <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                        <DataItemTemplate>
                            <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose">
                                <ClientSideEvents Init="OnInit" />
                            </dx:ASPxRadioButton>
                        </DataItemTemplate>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                        VisibleIndex="1" />
                    <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>"
                        VisibleIndex="2" />
                    <dx:GridViewDataColumn FieldName="交易日期" Caption="<%$ Resources:WebResources, TradeDate %>"
                        VisibleIndex="3" />
                    <dx:GridViewDataColumn FieldName="交易序號" Caption="<%$ Resources:WebResources, TransactionNo %>"
                        VisibleIndex="4" />
                    <dx:GridViewDataColumn FieldName="機台" Caption="<%$ Resources:WebResources, CashRegister %>"
                        VisibleIndex="5" />
                    <dx:GridViewDataColumn FieldName="客戶門號" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                        VisibleIndex="6" />
                    <dx:GridViewDataColumn FieldName="發票號碼" Caption="<%$ Resources:WebResources, InvoiceNo %>"
                        VisibleIndex="7" />                        
                    <dx:GridViewDataColumn FieldName="金額" Caption="<%$ Resources:WebResources, Amount %>"
                        VisibleIndex="8" />
                    <dx:GridViewDataColumn FieldName="付款方式" Caption="<%$ Resources:WebResources, PaymentMethod %>"
                        VisibleIndex="9" />
                    <dx:GridViewDataColumn FieldName="銷售人員" Caption="<%$ Resources:WebResources, SalesClerk %>"
                        VisibleIndex="10" />
                    <dx:GridViewDataColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                        VisibleIndex="11" />
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            </cc:ASPxGridView>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="Button21" runat="server" Text="換貨明細查詢"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e){document.location='SAL03.aspx';return false;}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>

<%--    <cc:ASPxPopupControl ID="StoresPopup" ClientInstanceName="StoresPopup" SkinID="StoresPopup"
        runat="server" EnableViewState="False" PopupElementID="btnChooseStore" TargetElementID="ASPxTextBox1">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>

    <cc:ASPxPopupControl ID="PromotionsPopup" ClientInstanceName="PromotionsPopup" SkinID="PromotionsPopup"
        runat="server" EnableViewState="False" PopupElementID="btnChoosePromotionCode" TargetElementID="ASPxTextBox3">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>

    <cc:ASPxPopupControl ID="ProductsPopup" ClientInstanceName="ProductsPopup" SkinID="ProductsPopup"
        runat="server" EnableViewState="False" PopupElementID="btnChooseProducts" TargetElementID="ASPxTextBox4">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
--%>    
</asp:Content>
