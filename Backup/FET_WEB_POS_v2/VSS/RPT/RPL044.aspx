<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL044.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL044" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
    <div class="titlef" align="left">
       <!--各項交易明細表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL044 %>"></asp:Literal>
    </div>
    
    <div class="seperate"></div>

    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
            <tr>
                 <td class="tdtxt">
                    <!--促銷生效日期-->
                    <asp:Literal ID="lblB_DATE" runat="server" Text="<%$ Resources:WebResources, PromotionEFDate %>"></asp:Literal>：
                </td>
                 <td class="tdval">
                    <table style="width: 340px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblB_DATE_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtB_DATE_S" runat="server" ClientInstanceName="txtSDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="lblB_DATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtB_DATE_E" runat="server" ClientInstanceName="txtEDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            
                        </tr>
                    </table>
                </td>
             </tr>
            <tr>
                <td class="tdtxt">
                    <!--促銷代號-->
                    <asp:Literal ID="lblPROMO_NO" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                </td>

                <td class="tdval">
                    <table style="width: 340px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblPROMO_NO_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:popupcontrol ID="txtPROMO_NO_S" runat="server" PopupControlName="PromotionsPopupOnly"  />
                            </td>
                            <td>
                                <asp:Literal ID="lblPROMO_NO_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <uc1:popupcontrol ID="txtPROMO_NO_E" runat="server" PopupControlName="PromotionsPopupOnly"  />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品名稱-->
                    <asp:Literal ID="lblProdName" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtProdName" runat="server" Width="170px"></dx:ASPxTextBox>
                </td>
            </tr> 
        </table>
    </div>
    
    <div class="seperate"></div>
        
    <table align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnSearch" runat="server" 
                    Text="<%$ Resources:WebResources, Search %>" onclick="btnSearch_Click" >
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>" 
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" OnClick ="btnReset_Click">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                 </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" 
                    onclick="btnExport_Click"></dx:ASPxButton>
            </td>
        </tr>
    </table>
    
    <div class="seperate"></div>

    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server"
        Width="98%" onpageindexchanged="gvMaster_PageIndexChanged" AutoGenerateColumns ="true" >
        <Columns>
            <dx:GridViewDataColumn FieldName="促銷代碼" Caption="<%$ Resources:WebResources, PromotionCode %>" />
            <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>" />
            <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" />
            <dx:GridViewDataColumn FieldName="全區" Caption="<%$ Resources:WebResources, Allarea %>" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster"></dx:ASPxGridViewExporter>   

</asp:Content>
