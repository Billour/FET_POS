<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD01.aspx.cs" Inherits="VSS_ORD_ORD01_ORD01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        //檢查當日訂購量
        function CheckOrdQtyOnTextChanged(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;
            _qtyName = s.name;
            var fName = "8_txtOrdqty";
            lblPRODNO = getClientInstance('Label', s.name.replace(fName, "2_lblPRODNO"));
            lblADVISEQTY = getClientInstance('Label', s.name.replace(fName, "4_lblADVISEQTY"));
            lblCHECK_IN_QTY = getClientInstance('Label', s.name.replace(fName, "9_lblCHECK_IN_QTY"));
            lblES_QTY = getClientInstance('Label', s.name.replace(fName, "5_lblES_QTY"));
            lblSTOCK_QTY = getClientInstance('Label', s.name.replace(fName, "6_lblSTOCK_QTY"));
            //alert(lblES_QTY.outerText + "   " + lblSTOCK_QTY.outerText);
            var iPRODNO = lblPRODNO.outerText;
            var iADVISEQTY = lblADVISEQTY.outerText;
            var iCHECK_IN_QTY = lblCHECK_IN_QTY.outerText;
            // var iITEMNO = lblITEMNO.outerText;
             Qty = s.GetValue();
            var iQty = 0;
            var iES_QTY = lblES_QTY.outerText;
            var iSTOCK_QTY = lblSTOCK_QTY.outerText;
             iTmpQty = 0;
            if (Qty == null || Qty == "")
            {
                s.SetValue('0');
            }
            else
            {
                if (Number(iES_QTY) > 0 && (Number(iES_QTY) - Number(iSTOCK_QTY)) > 0)
                    iTmpQty = Number(iES_QTY) - Number(iSTOCK_QTY);

                iQty = Number(Qty);
                if (isNaN(iQty))
                {
                    e.isValid = false;
                    alert('輸入字串不為數字格式，請重新輸入');
                    s.SetValue(iTmpQty);
                    return false;
                }
                else if (iQty < 0)
                {
                    e.isValid = false;
                    alert('當日訂購量需不允許小於0，請重新輸入');
                    s.SetValue(iTmpQty);
                    return false;
                }
                else if (Number(Qty) + Number(iCHECK_IN_QTY) > Math.ceil(Number(iADVISEQTY) * 1.5))
                {
                    alert('當日訂購量不允許大於[建議訂購量1.5倍(無條件進位)-當日總訂購量]，請重新輸入');                  
                    
                    s.SetValue(iTmpQty);
                    return false;
                }
               //**2011/02/15 Tina：註解。
               //**Issue 538 => 訂貨量加上門市庫量低於今日網購商品需求量，系統mail通報店長、副店及業務(列出未備貨商品/訂單資料/門市訂貨人員)。
               //**             並非強制性一定要輸入網購的差異量，請將Alert Message中網購部份的檢查移除。
               //else if (Number(iES_QTY) > 0 && (Number(iES_QTY) - Number(iSTOCK_QTY)) > 0 && (Number(iES_QTY) - Number(iSTOCK_QTY)) > Number(iQty))
               // {
               //     e.isValid = false;
               //     alert('當日訂購量需大於等於網購需求量-庫存量，請重新輸入');
               //     s.SetValue(iTmpQty);
               //    return false;
               //}


            }

            //***20110417拿掉檢查StroeAtr的量
            //var strData = lblPRODNO.outerText + ';' + lbStoreNo.GetText() + ';' + Qty + ';' + lblOrderDate.GetValue();     
            //商品料號,門市編號,輸入數量 # 搭贈商品ATR量檢查  
            //PageMethods.getStorAtr(strData, onSuccess);
            //****

        }

        //***20110417拿掉檢查StroeAtr的量
//        function onSuccess(returnData, userContext, methodName)
//        {    
//              
//                var fName = "8_txtOrdqty";
//                lblPRODNO = getClientInstance('Label', _gvSender.name.replace(fName, "2_lblPRODNO"));
//              
//                if (returnData != "")
//                {
//                    _gvSender.SetValue(iTmpQty);
//                    alert("【" + lblPRODNO.outerText + "】" + returnData);
//                }
//                else
//                {
//                    if (typeof (this.gvDetailDV) != "undefined" && this.gvDetailDV != null)
//                    {
//                        this.gvDetailDV.PerformCallback(lblPRODNO.outerText + ';' + Qty + ';' + _gvSender.name.split('_cell')[1].split('_')[0]);
//                    }
//                }

//        }
        //****
//        function CheckOrdQty(s, e)
//        {
//            for (var i = 0; i < drMasterDV.pageRowCount; i++)
//            {
//                var itemNo = i + 1;
//                var fName = "btnSave";
//                lblADVISEQTY = getClientInstance('Label', s.name.replace(fName, "drMasterDV_cell" + i + "_4_lblADVISEQTY"));
//                lblCHECK_IN_QTY = getClientInstance('Label', s.name.replace(fName, "drMasterDV_cell" + i + "_9_lblCHECK_IN_QTY"));

//                var iADVISEQTY = lblADVISEQTY.innerText;
//                var iCHECK_IN_QTY = lblCHECK_IN_QTY.innerText;

//                txtOrdqty = getClientInstance('TxtBox', s.name.replace(fName, "drMasterDV_cell" + i + "_8_txtOrdqty"));
//                var Qty = txtOrdqty.GetValue();
//                var iQty = 0;
//                if (Qty == null || Qty == "")
//                {
//                    e.isValid = false;
//                    alert('項次' + itemNo + ',當日訂購量不允許空白，請重新輸入');
//                    e.processOnServer = false;
//                }
//                else
//                {
//                    iQty = Number(Qty);
//                    if (isNaN(iQty))
//                    {
//                        e.isValid = false;
//                        alert('項次' + itemNo + ',輸入字串不為數字格式，請重新輸入');
//                        e.processOnServer = false;
//                    }
//                    else if (iQty < 0)
//                    {
//                        e.isValid = false;
//                        alert('項次' + itemNo + ',當日訂購量需不允許小於0，請重新輸入');
//                        e.processOnServer = false;
//                    }
//                    else if (Number(Qty) + Number(iCHECK_IN_QTY) > Math.ceil(Number(iADVISEQTY) * 1.5))
//                    {

//                        if (Qty != 0)
//                        {
//                            alert('項次' + itemNo + ',當日訂購量不允許大於[建議訂購量1.5倍(無條件進位)-當日總訂購量]，請重新輸入');
//                            e.processOnServer = false;
//                        }
//                    }
//                }
//            }

//        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--訂貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderPlacement %>"></asp:Literal>
                    <dx:ASPxLabel ID="lbStoreNo" ClientInstanceName="lbStoreNo" runat="server" ClientVisible="false" />
                    <dx:ASPxLabel ID="lbCheck" ClientInstanceName="lbCheck" runat="server" ClientVisible="false" />
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="False" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e) { document.location='../ORD02/ORD02.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--訂單編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdtxt">
                    <!--訂單日期-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxLabel ID="lblOrderDate" runat="server" ClientInstanceName="lblOrderDate"></dx:ASPxLabel>
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="訊息"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblStatus" runat="server" Text="當日總訂購量尚未出貨"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--備註-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                </td>
                <td colspan="3" class="tdval" rowspan="2">
                    <asp:TextBox ID="txtMemo" runat="server" Width="98%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblUpdDateTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
                <td class="tdtxt">
                    <!--更新人員-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblUpdUser" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <div id="divContent" runat="server" class="SubEditBlock">
                <cc:ASPxGridView ID="drMasterDV" Width="100%" runat="server" ClientInstanceName="drMasterDV"
                    KeyFieldName="ORDER_D_TEMP_ID" AutoGenerateColumns="False" OnPageIndexChanged="drMasterDV_PageIndexChanged"
                    OnHtmlCommandCellPrepared="drMasterDV_HtmlCommandCellPrepared" OnRowCommand="drMasterDV_RowCommand"
                    OnHtmlRowCreated="drMasterDV_HtmlRowCreated">
                    <Columns>
                        <dx:GridViewDataButtonEditColumn VisibleIndex="0">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btnOnetoone" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>">
                                </dx:ASPxButton>
                            </DataItemTemplate>
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblITEMNO" runat="server" Text='<%# Bind("ITEMNO") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblPRODNO" runat="server" Text='<%# Bind("PRODNO") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="ADVISEQTY" Caption="<%$ Resources:WebResources, RecommendedOrderQuantity %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblADVISEQTY" runat="server" Text='<%# Bind("ADVISEQTY") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="ES_QTY" Caption="<%$ Resources:WebResources, EconomicOrderQuantity %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblES_QTY" runat="server" Text='<%# Bind("ES_QTY") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="STOCK_QTY" Caption="<%$ Resources:WebResources, StockQuantity %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblSTOCK_QTY" runat="server" Text='<%# Bind("STOCK_QTY") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="INWAYQTY" Caption="<%$ Resources:WebResources, OnOrderQuantity %>">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="ORDQTY" Caption="<%$ Resources:WebResources, IntraDayOrderQuantity %>">
                            <DataItemTemplate>
                                <div align="right">
                                    <dx:ASPxTextBox ID="txtOrdqty" runat="server" Width="50px" MaxLength="5" Text='<%#BIND("ORDQTY") %>'
                                        CellStyle-HorizontalAlign="Right">
                                        <ClientSideEvents TextChanged="function(s,e){ CheckOrdQtyOnTextChanged(s, e); }" />
                                    </dx:ASPxTextBox>
                                </div>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="9" FieldName="CHECK_IN_QTY" Caption="<%$ Resources:WebResources, IntraDayTotalOrderQuantity %>">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="lblCHECK_IN_QTY" runat="server" Text='<%# Bind("CHECK_IN_QTY") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetailDV" Width="80%" runat="server" ClientInstanceName="gvDetailDV"
                                OnCustomCallback="gvDetailDV_CustomCallback" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                                EnableRowsCache="true">
                                <Columns>
                                    <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="BASE_QTY" Caption="<%$ Resources:WebResources, OrderQuantity %>">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <div align="left">
                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CollocationProduct %>"></asp:Literal>
                                        </div>
                                    </TitlePanel>
                                    <EmptyDataRow>
                                        <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                    </EmptyDataRow>
                                </Templates>
                                <SettingsDetail IsDetailGrid="true" />
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                        </DetailRow>
                    </Templates>
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                    <SettingsDetail ShowDetailRow="true" ShowDetailButtons="False" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="lblError" runat="server" Text="" ForeColor="Red" ClientInstanceName="lblError">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                CommandName="save" OnClick="btnSave_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>" SkinID="ResetButton"
                                 CausesValidation="false">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnTransfer" runat="server" Text="<%$ Resources:WebResources, Transfer %>"
                                CommandName="Commit" OnClick="btnSave_Click">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
