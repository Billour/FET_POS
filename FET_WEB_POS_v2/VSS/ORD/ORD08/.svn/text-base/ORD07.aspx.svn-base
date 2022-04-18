<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ORD07.aspx.cs" Inherits="VSS_ORD_ORD07" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function checkDate(s, e) {
            var x = txtSDate.GetValue();
            var y = txtEDate.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("主配日期訖不允許小於主配日期起，請重新輸入!");
                    s.SetValue(null);
                }
            }

        }
        
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--Non-DropShipment主配查詢作業-->
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, NonDropShipmentProductDistributionSearch %>"></asp:Literal>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--主配單號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DistributionNo %>"></asp:Literal>:
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtHQ_NDS_ORDER_NO" runat="server" Width="120px"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--出貨倉別-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ShipmentWarehouse %>"></asp:Literal>:
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ddlLOC_ID" runat="server" Width="120px">
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            <!--狀 態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>:
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ddlSTATUS" runat="server" Width="120px">
                                <Items>
                                    <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                                    <dx:ListEditItem Text="已存檔" Value="1" />
                                    <dx:ListEditItem Text="已上傳" Value="2" />
                                    <dx:ListEditItem Text="已轉門市訂單" Value="3" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--主配日期-->
                            <span style="color: Red">*</span><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, DistributionDate %>"></asp:Literal>:
                        </td>
                        <td class="tdval">
                            <table style="width: 100px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtCREATE_DATE_S" runat="server" ClientInstanceName="txtSDate">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtCREATE_DATE_E" runat="server" ClientInstanceName="txtEDate">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt">商品料號：</td>
                        <td class="tdval">
                            <uc1:PopupControl ID="txtPRODNO" runat="server" PopupControlName="ProductsPopup"  />
                        </td>
                        <td class="tdtxt">更新人員：</td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtMODI_USER" runat="server"></dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate"></div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                                OnClick="btnSearch_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"  SkinID="ResetButton">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div>
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%" KeyFieldName="HQ_NDS_ORDER_NO"
                            OnPageIndexChanged="gvMaster_PageIndexChanged" 
                            OnHtmlRowPrepared="gvMaster_HtmlRowPrepared">
                            <Columns>
                                <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="HQ_NDS_ORDER_NO" Caption="<%$ Resources:WebResources, DistributionNo %>">
                                    <DataItemTemplate>
                                        <asp:HyperLink ID="hlkdno1" runat="server" Text='<%# Bind("[HQ_NDS_ORDER_NO]") %>'></asp:HyperLink>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="CREATE_DATE" Caption="<%$ Resources:WebResources, DistributionDate %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="LOC_ID" Caption="<%$ Resources:WebResources, ShipmentWarehouse %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="STATUS" Caption="<%$ Resources:WebResources, Status %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsPager PageSize="10"></SettingsPager>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
