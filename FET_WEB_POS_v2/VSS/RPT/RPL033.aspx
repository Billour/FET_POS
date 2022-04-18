<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL033.aspx.cs"  MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL033" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
    <div class="titlef" align="left">
        <!--門市庫存量分析表-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL033 %>"></asp:Literal>
    </div>
    
    <div class="seperate"></div>
    
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td class="tdtxt">
                    <!--月份-->
                    <asp:Literal ID="lblSTK_DATE" runat="server" Text="月份"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtSTK_DATE" runat="server" Width="120px">
                        <ClientSideEvents ValueChanged="function(s, e){ chkIsMonth(s, e); }"  />
                        <MaskSettings ErrorText="請輸入正確格是" Mask="yyyy/MM" ShowHints="True" />
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品類別-->
                    <asp:Literal ID="lblPRODTYPENO" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <uc1:PopupControl ID="txtProductTypeNoS" runat="server" PopupControlName="ProductType" />
                            </td>
                            <td>
                                ~
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
                    <!--商品料號-->
                    <asp:Literal ID="lblPRODNO" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <uc1:PopupControl ID="txtPRODNO_S" runat="server" PopupControlName="ProductsPopup"  />
                            </td>
                            <td>
                                ~
                            </td>
                            <td>
                                <uc1:PopupControl ID="txtPRODNO_E" runat="server" PopupControlName="ProductsPopup"  />
                            </td>
                        </tr>
                    </table>
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
                <dx:ASPxButton ID="btnReset" runat="server"  SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>" />
            </td>
            <td> 
                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" 
                    onclick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    
    <div class="seperate"></div>
               
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server"
         onpageindexchanged="gvMaster_PageIndexChanged" AutoGenerateColumns ="true" Width="100%" >
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10"></SettingsPager>
        <Columns>
         <dx:GridViewDataColumn FieldName="ItemCode" Caption="ItemCode" />
          <dx:GridViewDataColumn FieldName="品名" Caption="品名" />
          </Columns>
    </cc:ASPxGridView>
   
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" ></dx:ASPxGridViewExporter>   

</asp:Content>