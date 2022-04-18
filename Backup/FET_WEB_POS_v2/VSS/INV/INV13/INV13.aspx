<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV13.aspx.cs" Inherits="VSS_INV_INV13_INV13" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function Call_BarcodePrintFile(vorderno, vbarcodestr) {
            var oBarcodePrint = new ActiveXObject("ProjBarcodePrint.BarcodePrint");
            var result = oBarcodePrint.BarcodePrintFile(vorderno, vbarcodestr);

            alert(result);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<OBJECT ID="BarcodePrint"
CLASSID="CLSID:5BF61033-4F10-4492-84F4-2052AB55CFFF"
CODEBASE="BarcodePrint.CAB#version=1,0,0,0">
</OBJECT>
    <asp:HiddenField ID="tempMID" runat="server" />
    <div>
        <div class="titlef">
            <!--無訂單進貨資料查詢作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, NoPurchaseOrderDataEntrySearch %>"></asp:Literal>
        </div>
        <div>
            <table style="width: 100%">
                <tr>
                    <!--進貨日期-->
                    <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources,ReceivedDate %>">：</asp:Literal>
                    </td>
                    <td nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 120px;">
                                        <dx:ASPxDateEdit ID="txtSDate" runat="server" ClientInstanceName="txtSDate" EditFormatString="yyyy/MM/dd">
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 120px;">
                                        <dx:ASPxDateEdit ID="txtEDate" runat="server" ClientInstanceName="txtEDate" EditFormatString="yyyy/MM/dd">
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <div id="divSTORE" runat="server" visible="false">
                        <td class="tdtxt">
                            <!--門市編號-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <uc1:PopupControl ID="txtStoreNO" runat="server" PopupControlName="StoresPopup" IsValidation="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </div>
                </tr>
            </table>
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
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                SkinID="ResetButton">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
        </div>
        <asp:UpdatePanel ID="test" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="NP_ORDER_M_ID" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                    OnPageIndexChanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataTextColumn runat="server" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="1">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ORDER_NO" runat="server" Caption="<%$ Resources:WebResources, ReceivingNoteNumber %>"
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STORENAME" runat="server" Caption="進貨門市" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ORDDATE" runat="server" Caption="<%$ Resources:WebResources, ReceivedDate %>"
                            VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="REMARK" runat="server" Caption="<%$ Resources:WebResources, Remark %>"
                            VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                            VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                            VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetail" ClientInstanceName="gvDetail" runat="server" Width="100%"
                                KeyFieldName="NP_ORDER_D_ID" OnPageIndexChanged="gvDetail_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewDataColumn Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0">
                                        <DataItemTemplate>
                                            <%#Container.ItemIndex + 1%>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                                        VisibleIndex="1" />
                                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                        VisibleIndex="2" />
                                    <dx:GridViewDataColumn FieldName="UNIT" Caption="<%$ Resources:WebResources, Unit %>"
                                        VisibleIndex="3" />
                                    <dx:GridViewDataColumn FieldName="ORDER_QTY" Caption="<%$ Resources:WebResources, Quantity %>"
                                        VisibleIndex="4" />
                                    <dx:GridViewDataColumn FieldName="ORDER_AMT" Caption="<%$ Resources:WebResources, TotalAmount %>"
                                        VisibleIndex="5" />
                                </Columns>
                                <SettingsPager PageSize="5" />
                            </cc:ASPxGridView>
                            <div class="seperate">
                            </div>
                            <div id="divButton" runat="server" class="btnPosition">
                                <table cellpadding="0" cellspacing="0" border="0" align="center">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnPrint" runat="server" Text="<%$ Resources:WebResources, PrintBarCode %>"
                                                UseSubmitBehavior="false" CausesValidation="false" AutoPostBack="false" OnClick="btnPrint_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <SettingsPager PageSize="5" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                </cc:ASPxGridView>


            </ContentTemplate>
            </asp:UpdatePanel>
           
           
    </div>
     <iframe id="fDownload" style="display: none" src="" runat="server"></iframe>
            <iframe id="fDownload1" style="display: none" src="" runat="server"></iframe>
            <iframe id="Iframe1" style="display: none" src="" runat="server"></iframe>
</asp:Content>
