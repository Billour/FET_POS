<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON21.aspx.cs" Inherits="VSS_CONS_CON21" %>

<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function getStoreInfo(s, e) {
            PageMethods.getStoreInfo(s.GetText(), getStoreInfo_OnOK);
        }
        function getStoreInfo_OnOK(returnData) {
            if (returnData != '') {
                txtStoreName.SetValue(returnData);
            }
            else {
                txtStoreName.SetValue(null);
            }
        }

        function getProductInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getProductInfo(s.GetText(), getProductInfo_OnOK);
        }

        function getProductInfo_OnOK(returnData) {
            if (returnData != '') {
                txtProductName.SetValue(returnData);
            }
            else {
                txtProductName.SetValue(null);
            }
        }

        //檢查撥入數量
        function CheckTraninQty(s, e) {
            fName = "4_txtTraninQty";
            //var OutQty = Number(lblTranOutQty.GetValue());
            //var OutQty = Number(
            lblDiffStkQty = getClientInstance('TxtBox', s.name.replace(fName, "4_lblTranOutQty")); //移出數量
            OutQty = lblDiffStkQty.GetValue();
            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty == null || Qty == "") {
                e.isValid = false;
                e.errorText = '撥入數量不允許空白，請重新輸入';
                return false;
            }
            else {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串不為數字格式，請重新輸入';
                    return false;
                }
                else if (iQty <= 0) {
                    e.isValid = false;
                    e.errorText = '撥入數量需不允許小於等於0，請重新輸入';
                    return false;
                }
                else if (iQty != OutQty) {
                    e.isValid = false;
                    e.errorText = '撥入數量需與移出數量一致，請重新輸入';
                    return false;
                }
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品撥入作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferIn %>"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <div>
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt">
                                <!--移撥單號-->
                                <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval" colspan="1">
                                <dx:ASPxTextBox ID="txtStno" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                <dx:ASPxLabel ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductName %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtProductName" ClientInstanceName="txtProductName" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                <dx:ASPxLabel ID="Literal18" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <dx:ASPxComboBox ID="ddlTStatus" runat="server">
                                    <Items>
                                        <dx:ListEditItem Value="20" Text="在途" Selected="true" />
                                        <dx:ListEditItem Value="30" Text="已撥入" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <!--移出日期-->
                            <td class="tdtxt">
                                <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval" colspan="3">
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="TransferOutDateFrom" runat="server">
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="TransferOutDateTo" runat="server">
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdtxt">
                                <!--移出門市-->
                                <dx:ASPxLabel ID="Literal19" runat="server" Text="<%$ Resources:WebResources, Transferfrom %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <uc1:PopupControl ID="txtFormStoreNo" KeyFieldValue1="name" KeyFieldValue2="STORENAME"
                                    runat="server" PopupControlName="StoresPopup" />
                            </td>
                        </tr>
                        <tr>
                            <!--撥入日期-->
                            <td class="tdtxt">
                                <dx:ASPxLabel ID="Literal17" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval" colspan="3">
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 240px">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtTSTDate_S" runat="server" ClientInstanceName="txtSDate">
                                                <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="txtTSTDate_E" runat="server" ClientInstanceName="txtEDate">
                                                <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdtxt">
                                <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Transferto %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <dx:ASPxLabel ID="lblToStoreName" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="btnPosition">
                    <table cellpadding="0" cellspacing="0" border="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                    OnClick="btnSearch_Click" UseSubmitBehavior="false" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                    SkinID="ResetButton" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="seperate">
                </div>
                <cc:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                    KeyFieldName="CSM_STORETRANSFERM_ID" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                    ClientInstanceName="gvMaster" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnPageIndexChanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="STNO" Caption="<%$ Resources:WebResources, TransferSlipNo %>">
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="FROM_STORE_NAME" Caption="<%$ Resources:WebResources, TransferFrom %>" />
                        <dx:GridViewDataColumn FieldName="STDATE" Caption="<%$ Resources:WebResources, TransferOutDate %>" />
                        <dx:GridViewDataColumn FieldName="TO_STORE_NAME" Caption="<%$ Resources:WebResources, TransferTo %>" />
                        <dx:GridViewDataColumn FieldName="TSTDATE" Caption="<%$ Resources:WebResources, TransferInDate %>" />
                        <dx:GridViewDataColumn FieldName="TSTATUS" Caption="<%$ Resources:WebResources, TransferStatus %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# Bind("TSTATUS") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                        <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Width="100%"
                                KeyFieldName="CSM_STORETRANSFER_D_ID" OnHtmlRowCreated="gvDetail_HtmlRowCreated"
                                OnPageIndexChanged="gvDetail_PageIndexChanged" Settings-ShowTitlePanel="true">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="PRODTYPENAME" Caption="<%$ Resources:WebResources, ProductCategory %>" />
                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
                                    <dx:GridViewDataColumn FieldName="TRANOUTQTY" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>">
                                       
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="TRANINQTY" Caption="<%$ Resources:WebResources, TransferredInQuantity %>">
                                        <DataItemTemplate>
                                        <dx:ASPxLabel ID="lblTranOutQty" runat="server"  Text='<%# Bind("TRANOUTQTY") %>' ClientVisible="false"></dx:ASPxLabel>
                                            <dx:ASPxTextBox ID="txtTraninQty" runat="server" Text='<%# Bind("TRANOUTQTY") %>' MaxLength="9"
                                                HorizontalAlign="Right" Width="100px" >
                                                <ValidationSettings>
                                                    <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串不為數字格式，請重新輸入" />
                                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s,e){ CheckTraninQty(s, e); }" />
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Settings ShowFooter="false" />
                                <SettingsDetail IsDetailGrid="true" />
                                <SettingsPager PageSize="5">
                                </SettingsPager>
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                            <div class="seperate">
                            </div>
                            <div class="btnPosition">
                                <table cellpadding="0" cellspacing="0" border="0" align="center">
                                    <tr>
                                        <td align="right">
                                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfirmTransferIn %>"
                                                UseSubmitBehavior="false" OnClick="btnSave_Click" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnCancel" SkinID="ResetButton" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <SettingsPager PageSize="5">
                    </SettingsPager>
                    
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <SettingsBehavior AllowFocusedRow="true" />
                </cc:ASPxGridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
