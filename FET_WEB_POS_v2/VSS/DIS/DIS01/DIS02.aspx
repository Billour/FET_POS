<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DIS02.aspx.cs" Inherits="VSS_DIS_DIS02"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div class="titlef">
       <%-- <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">--%>
                    <!--折扣設定查詢-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DiscountSettingsSearch %>"></asp:Literal>
               <%-- </td>
                <td align="right">
                    <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='DIS01.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>--%>
    </div>
    
    <div class="criteria" style="width: 98%">
        <table>
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
                    <ValidationSettings SetFocusOnError="true">
                            <RegularExpression ValidationExpression="-\d.*" ErrorText="格式不正確,請以負值輸入" />
                    </ValidationSettings>
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
    
    <div class="seperate" style="width: 98%"></div>
    
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
                        <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate" style="width: 98%"></div>
    
    <div class="SubEditBlock">
        <cc:ASPxGridView ID="gvMaster" runat="server" Width="98%" ClientInstanceName="gvMaster"
            KeyFieldName="項次" AccessibilityCompliant="True" AutoGenerateColumns="False"  
            OnPageIndexChanged="grid_PageIndexChanged" 
            onhtmlrowprepared="gvMaster_HtmlRowPrepared" >
            <Columns>
                <dx:GridViewDataColumn FieldName="項次" Caption="項次" VisibleIndex="0">
                    <DataItemTemplate>
                        <%#Container.ItemIndex + 1%>
                    </DataItemTemplate>                                    
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="DISCOUNT_CODE" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                    VisibleIndex="1">
                     <DataItemTemplate>
                        <div align="left">
                            <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text='<%#Bind("DISCOUNT_CODE") %>' ForeColor="Black">
                            </dx:ASPxHyperLink>
                        </div>
                     </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DISCOUNT_NAME" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, DiscountName %>"
                    VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="S_DATE" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, StartDate %>"
                    VisibleIndex="3">
                    <EditItemTemplate>
                        <dx:ASPxDateEdit ID="deS_DATE" EditFormatString="yyyy/MM/dd" runat="server" Value='<%# Bind("S_DATE") %>'  ></dx:ASPxDateEdit>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="E_DATE"  runat="server" Caption="<%$ Resources:WebResources, EndDate %>"
                    VisibleIndex="4">
                    <EditItemTemplate>
                        <dx:ASPxDateEdit ID="deE_DATE" EditFormatString="yyyy/MM/dd" runat="server" Value='<%# Bind("E_DATE") %>'  ></dx:ASPxDateEdit>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DISCOUNT_MONEY" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, DiscountAmount %>"
                    VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DISCOUNT_RATE" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, DiscountRate %>"
                    VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DIS_USE_TYPE" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>"
                    VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="MODI_USER" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="8">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    VisibleIndex="9">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="Inline" />
            <SettingsPager PageSize="10" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
    </div>
   
</asp:Content>
