<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CON14.aspx.cs" Inherits="VSS_CONS_CON14" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        div.window
        {
            position: absolute;
            width: 400px;
            height: 300px;
            border-style: ridge;
            border-color: White;
        }
        div.titlebar
        {
            /* Specifies position, size, and style of the titlebar */
            position: absolute; /* It's a positioned element */
            top: 0px;
            height: 18px; /* titlebar is 18px + padding and borders */
            width: 390px; /* 290 + 5px padding on left and right = 300 */
            background-color: ActiveCaption; /* Use system titlebar color */
            border-bottom: groove black 2px; /* Titlebar has border on bottom only */
            padding: 3px 5px 2px 5px; /* Values clockwise: top, right, bottom, left */
            color: CaptionText; /* Use system font for titlebar */
            font-weight: bold;
        }
        div.content
        {
            /* Specifies size, position and scrolling for window content */
            position: absolute; /* It's a positioned element */
            top: 25px; /* 18px title+2px border+3px+2px padding */
            height: 265px; /* 200px total - 25px titlebar - 10px padding */
            width: 390px; /* 300px width - 10px of padding */
            padding: 5px; /* allow space on all four sides */
            overflow: auto; /* give us scrollbars if we need them */
            background-color: #ffffff; /* White background by default */
        }
    </style>

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function CheckTranOutQty(s, e) {
            var fName = "5_txtIN_QTY";
            var Qty = s.GetValue();
            var iQty = 0;
            var inQty = 0;
            var txtINQTY = getClientInstance('Label', s.name.replace(fName, "4_txtQTY"));
            if (Qty == null || Qty == "") {
                e.isValid = false;
                e.errorText = '驗收量不允許空白，請重新輸入';
                return false;
            }
            else {
                iQty = Number(Qty);  //+ Number(CHECK_IN_QTY.GetText()); //本次驗收量加之前驗收量;
                //inQty = Number(txtINQTY.innerText)
                inQty = Number(txtINQTY.innerHTML);

                if (isNaN(iQty)) {
                    e.isValid = false;
                    e.errorText = '輸入字串不為數字格式，請重新輸入';
                    return false;
                }
                else if (!isInteger(iQty)) {
                    e.isValid = false;
                    e.errorText = '驗收量不為整數，請重新輸入';
                    return false;
                } else if (Qty.indexOf('.') != -1) {
                    e.isValid = false;
                    e.errorText = '驗收量不允許輸入小數點，請重新輸入';
                    return false;

                } else if (iQty < 0) {
                    e.isValid = false;
                    e.errorText = '驗收量不允許小於0，請重新輸入';
                    return false;
                } else if (iQty > inQty) {
                    e.isValid = false;
                    e.errorText = '驗收量不允許大於到貨量，請重新輸入';
                    return false;
                }
                //                else {
                //                    imeiPop = getClientInstance('Popup', s.name.replace(fName, "5_txtIMEI_ASPxPopupControl1"));
                //                    imeiPop.SetContentUrl(setContentUrl_IMEI_QTY1(imeiPop.GetContentUrl(), s.GetText()));
                //                    e.processOnServer = false;
                //                }
                //                if (Number(inQty) && Number(iQty) && Number(ohQty)) {
                //                    ohQty = inQty - iQty;
                //                    OnHandQty.SetText(ohQty);
                //                    // OnHandQty.innerHtml = ohQty;
                //                }
                //                OnHandQty.SetText(txtINQTY.innerText - Qty - Number(CHECK_IN_QTY.GetText()));
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品進貨驗收作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventoryExamination %>">
                    </asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="False" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e) { document.location='../CON14/CON13.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--出貨編號-->
                        <dx:ASPxLabel ID="Literal16" runat="server" Text="<%$ Resources:WebResources, DeliveryOrderNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="comOENO" runat="server" OnSelectedIndexChanged="comOENO_SelectedIndexChanged"
                            AutoPostBack="true" EnableClientSideAPI="false">
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierName %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblSUPPNAME" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblStatus" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--進貨日期-->
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, ReceivedDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxDateEdit ID="ReceivedDate1" runat="server">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt">
                        <!--訂單/主配編號-->
                        <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td>
                        <dx:ASPxLabel ID="lblOrderNo" runat="server">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblModifiedDate" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <dx:ASPxLabel ID="labStoreName" runat="server" Text="" ClientVisible="false">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="laSUPP_ID" runat="server" Text="" ClientVisible="false">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <dx:ASPxTextBox ID="hidSubmit" ClientInstanceName="hidSubmit" ClientVisible="false"
                            runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--驗收人員-->
                        <dx:ASPxLabel ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ReceivedBy %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="lblUSER" runat="server" Text="">
                        </dx:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="GridScrollBar" style="height: 350px">
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
                OnPageIndexChanged="gvMaster_PageIndexChanged" OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                <Columns>
                    <dx:GridViewDataColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                        <DataItemTemplate>
                            <dx:ASPxLabel ID="txtPRODNO" runat="server" Text='<%# Bind("PRODNO") %>' />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="SUPP_NO" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="SUPP_NAME" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>">
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="QTY" runat="server" Caption="<%$ Resources:WebResources, ArrivalQuantity %>">
                        <DataItemTemplate>
                            <dx:ASPxLabel ID="txtQTY" runat="server" Text='<%# Bind("QTY") %>'>
                            </dx:ASPxLabel>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="IN_QTY" runat="server" Caption="<%$ Resources:WebResources, InspectionQuantity %>">
                        <HeaderCaptionTemplate>
                            <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, InspectionQuantity %>">
                            </dx:ASPxLabel>
                        </HeaderCaptionTemplate>
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txtIN_QTY" runat="server" Text='<%# Bind("IN_QTY") %>' Width="60">
                                <ValidationSettings SetFocusOnError="true">
                                </ValidationSettings>
                                <ClientSideEvents Validation="function(s,e){ CheckTranOutQty(s, e); }" />
                            </dx:ASPxTextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="REMARK" runat="server" Caption="<%$ Resources:WebResources, Remark %>">
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txtREMARK" runat="server" Text='<%# Bind("REMARK") %>' Width="120">
                            </dx:ASPxTextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                </Columns>
                <SettingsPager PageSize="2" />
                <SettingsEditing Mode="Inline" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            </cc:ASPxGridView>
        </div>
    </div>
    <div class="seperate">
    </div>
    <div id="divButtons" class="btnPosition" runat="server" visible="false">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                        OnClick="btnSave_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                        OnClick="btnClear_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnPrint" runat="server" Text="<%$ Resources:WebResources, PrintBarCode %>" />
                </td>
                <dx:ASPxButton ID="TbtnSave" ClientInstanceName="TbtnSave" ClientVisible="false"
                    runat="server" OnClick="TbtnSave_Click">
                </dx:ASPxButton>
                <dx:ASPxTextBox ID="FbtnSave" ClientInstanceName="FbtnSave" ClientVisible="false"
                    runat="server">
                </dx:ASPxTextBox>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        if (hidSubmit.GetText() == "Y") {
            hidSubmit.SetText("");
            if (confirm('驗收量與到貨量有差異，是否確認儲存?')) { FbtnSave.SetText('Y'); TbtnSave.SendPostBack('Click'); }
        }
    </script>

</asp:Content>
