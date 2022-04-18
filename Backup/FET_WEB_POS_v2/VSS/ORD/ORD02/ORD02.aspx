<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ORD02.aspx.cs" Inherits="VSS_ORD_ORD02_ORD02" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div>
        <div class="titlef">
            <!--(預)訂單查詢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderSearch %>"></asp:Literal>
        </div>
        
        <div>
            <div align="left">
                <table width="100%">
                    <tr>
                        <td class="tdtxt">
                            <!--訂單日期-->
                            <span style="color: Red">*</span><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" align="left">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                        </asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtSDate" runat="server" ClientInstanceName="txtSDate" EditFormatString="yyyy/MM/dd">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, End %>">
                                        </asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtEDate" runat="server" ClientInstanceName="txtEDate" EditFormatString="yyyy/MM/dd">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td>
                            <uc1:PopupControl ID="txtProductNO" runat="server" PopupControlName="ProductsPopup"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                                OnClick="btnSearch_Click" ClientSideEvents-Click="function(s,e){                                              
                                               
                                                var prodno = document.getElementById('ctl00_MainContentPlaceHolder_txtProductNO_txtControl_I').value;
                                               
                                                 if((prodno.length > 0) && (prodno <0  ||  prodno!=parseInt(prodno)) ){
                                                   alert('商品料號格式錯誤，請重新確認');
                                                    e.processOnServer = false;
                                                    return false;
                                                 }
                                               }">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"/>
                               
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            
            <cc:ASPxGridView ID="gvMasterDV" runat="server" ClientInstanceName="gvMasterDV" AutoGenerateColumns="False"
                Width="100%" 
                onpageindexchanged="gvMasterDV_PageIndexChanged">
                <Columns>
                    <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="ORDDATE" Caption="<%$ Resources:WebResources, OrderDate %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="ORDER_NO" Caption="<%$ Resources:WebResources, OrderNo %>">
                        <DataItemTemplate>
                            <div align="left">
                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text='<%#Eval("ORDER_NO") %>' NavigateUrl='<%# "~/VSS/ORD/ORD02/ORD02_1.aspx?" + TransferURL(Request.QueryString.ToString() + "&ordid=" + Eval("ORDER_ID") + "&order_type" + Eval("ORDER_TYPE")) %>'>
                                </dx:ASPxHyperLink>
                            </div>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="PRE_ORDER_NO" Caption="<%$ Resources:WebResources, BookOrderNumber %>">
                        <DataItemTemplate>
                            <div align="left">
                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text='<%#Eval("PRE_ORDER_NO") %>' NavigateUrl='<%#"~/VSS/ORD/ORD02/ORD02_2.aspx?" + TransferURL(Request.QueryString.ToString() + "&preordid=" + Eval("PRE_ORDER_M_ID") + "&order_type" + Eval("ORDER_TYPE"))  %>'>
                                </dx:ASPxHyperLink>
                            </div>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="STATUS" Caption="<%$ Resources:WebResources, Status %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <TitlePanel>
                         <table align="left" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnXlsExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
                                        UseSubmitBehavior="False" OnClick="btnXlsExport_Click">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </TitlePanel>
                </Templates>
                <SettingsPager PageSize="10"></SettingsPager>
                <Settings ShowTitlePanel="true" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            </cc:ASPxGridView>

        </div>
    </div>

</asp:Content>

