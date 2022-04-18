<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DIS02.aspx.cs" Inherits="VSS_DIS_DIS02"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--折扣設定查詢-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DiscountSettingsSearch %>"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table style="width: 100%">
            <tr>
                <td class="tdtxt">
                    <!--類別 -->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="Category" runat="server">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--折扣料號 -->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="PartNumberOfDiscount" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--折扣名稱 -->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="DiscountName" runat="server">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--折扣金額 -->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, DiscountAmount %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="DiscountAmount" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--折扣比率 -->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, DiscountRate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="DiscountRate" runat="server">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--有效期間 -->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="2">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
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
    <div class="btnPosition">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnQuery_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="SubEditBlock">
        <cc:ASPxGridView ID="gvMaster" runat="server" Width="100%"
            KeyFieldName="項次" AccessibilityCompliant="True" 
            AutoGenerateColumns="False" OnPageIndexChanged="grid_PageIndexChanged" >
            <Columns>
                <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                    VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="折扣料號" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="折扣名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, DiscountName %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="開始日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, StartDate %>"
                    VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="結束日期" runat="server" Caption="<%$ Resources:WebResources, EndDate %>"
                    VisibleIndex="4">
                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="折扣金額" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, DiscountAmount %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="折扣比率" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, DiscountRate %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="折扣上限次數" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>"
                    VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    VisibleIndex="5">
                </dx:GridViewDataTextColumn>
            </Columns>

             <SettingsEditing Mode="Inline" />
            <SettingsPager PageSize="10" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
    </div>
   
</asp:Content>
