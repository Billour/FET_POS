<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="INV09.aspx.cs" Inherits="VSS_INV_INV09" ValidateRequest="false" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        /// <summary>
        /// 變更原本IMEI URL路徑中的商品料號,數量及IMEI_FLAG
        /// **2011/04/27 Tina：將URL傳遞的參數加密。
        /// </summary>
        /// <param name="imeiPop">IMEI的Popup Control</param>
        /// <param name="UUID">此筆資料的UUID</param>
        /// <param name="PRODNO">商品料號</param>
        /// <param name="QTY">數量</param>
        /// <returns>新的URL路徑</returns>
        function setPopupContentUrl_INV09(imeiPop, UUID, PRODNO, QTY) {

            var url = imeiPop.GetContentUrl();
            var sValue = url.split('?');
            if (sValue.length > 1) {
                var t = new Date().getTime();
                url = sValue[0] + "?SysDate=" + t + "&KeyFieldValue1=STOCKADJ_D_IMEI;" + UUID + ";" + PRODNO + ";" + QTY;
            }

            ModifyPopupURLByEncrypt(url, imeiPop);
        }
    </script>
    
    <script type="text/javascript" language="javascript">

        function Call_BarcodePrintFile(vorderno, vbarcodestr) {
            var oBarcodePrint = new ActiveXObject("ProjBarcodePrint.BarcodePrint");
            var result = oBarcodePrint.BarcodePrintFile(vorderno, vbarcodestr);
            
            alert(result);
        }

        //是否可編輯IMEI
        function EnableIMEI(object, ControlName) {
            var fName = object;
            var txtPRODNAME = getClientInstance('TxtBox', ControlName.replace(fName, "1_txtProdName"));
            var TranOutQty = getClientInstance('TxtBox', ControlName.replace(fName, "3_txtAdjuestmentQuantity"));
            var imgIMEI = getClientInstance('Image', ControlName.replace(fName, "4_imgIMEI"));
            var txtInputIMEIData = getClientInstance('TxtBox', ControlName.replace(fName, "5_txtIMEI_txtControl"));
            var txtIMEIFlag = getClientInstance('TxtBox', ControlName.replace(fName, "5_txtIMEIFlag"));
            var btnInputIMEIData = getClientInstance('Button', ControlName.replace(fName, "5_txtIMEI_btnControl"));
            var lblIMEI_QTY = getClientInstance('TxtBox', ControlName.replace(fName, "5_lblIMEI_QTY"));
            var imeiPop = getClientInstance('Popup', ControlName.replace(fName, "5_txtIMEI_ASPxPopupControl1"));

            var strProductName = txtPRODNAME.GetText();

            var iQty = Number(TranOutQty.GetText());
            //商品料號 && 移出數量 皆有值，才可編輯IMEI (IMEI_FLAG =3 才設IMEI)
            if ((txtIMEIFlag.GetText() == '3') && strProductName != '' && !isNaN(iQty) && TranOutQty.GetText() != '') {
                txtInputIMEIData.SetVisible(true);
                btnInputIMEIData.SetVisible(true);
                imeiPop.SetPopupElementID(btnInputIMEIData.name);
                imgIMEI.SetVisible(true);
                var v_TranOutQty = Number(TranOutQty.GetText());
                if ((v_TranOutQty) < 0) v_TranOutQty = -v_TranOutQty
                if (v_TranOutQty == Number(lblIMEI_QTY.GetText())) {
                    imgIMEI.SetImageUrl("../../../Icon/check.png");
                    imgIMEI.SetSize(16, 16);
                }
                else {
                    imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                    imgIMEI.SetSize(27, 16);
                }
            }
            else {
                imeiPop.SetPopupElementID(null);
                imgIMEI.SetVisible(false);
            }
        }
        
        //變更IMEI圖示
        function ChangeImageIMEI(s, e) {
            var fName = "5_lblIMEI_QTY";
            var IMEI_Qty = s;
            var lblProdNo = getClientInstance('TxtBox', s.name.replace(fName, "0_lblProdNo"));
            var TranOutQty = getClientInstance('TxtBox', s.name.replace(fName, "3_txtAdjuestmentQuantity"));
            var imgIMEI = getClientInstance('Image', s.name.replace(fName, "4_imgIMEI"));
            var UUID = getClientInstance('TxtBox', s.name.replace(fName, "5_PO_OE_NO"));


            var v_TranOutQty = Number(TranOutQty.GetText());
            if ((v_TranOutQty) < 0) v_TranOutQty = -v_TranOutQty

            if (v_TranOutQty == Number(IMEI_Qty.GetText())) {
                imgIMEI.SetImageUrl("../../../Icon/check.png");
                imgIMEI.SetSize(16, 16);
            }
            else {
                imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                imgIMEI.SetSize(27, 16);
            }

            this.objName = fName;
            this.s = s;
            //var pValues = getIMEI(s, e);
            //PageMethods.IMEIContent(pValues[0], pValues[1], pValues[2], IMEIContent);
            PageMethods.IMEIContent("STOCKADJ_D_IMEI", UUID.GetText(), lblProdNo.GetText(), IMEIContent);
        }
        
        function IMEIContent(values) {
            var lblIMEI_QTY = window.document.all[s.name.replace(objName, "5_lblIMEI_QTY")];
            lblIMEI_QTY.attributes["onmouseover"].value = "show('" + values + "');";
            lblIMEI_QTY.attributes["onmouseout"].value = "hide();";
        }

        //取得IMEI上傳參數;
        function getIMEI(s, e) {
            var fName = this.objName;
            var pValues = null;
            var imeiPop = getClientInstance('TxtBox', s.name.replace(fName, "5_txtIMEI_ASPxPopupControl1"));
            var u = imeiPop.GetContentUrl().split('KeyFieldValue1=');
            if (u.length > 1) {
                var oldKeyFieldValue1 = u[1].split('&')[0];
                pValues = oldKeyFieldValue1.split(';');
            }
            return pValues;
        }

        function CheckTranOutQty(s, e) {
            var fName = "3_txtAdjuestmentQuantity";
            var lblProdNo = getClientInstance('TxtBox', s.name.replace(fName, "0_lblProdNo"));
            var txtINQTY = getClientInstance('Label', s.name.replace(fName, "2_txtINQTY"));
            var CHECK_IN_QTY = getClientInstance('TxtBox', s.name.replace(fName, "5_txtCHECK_IN_QTY"));
            var imeiPop = getClientInstance('Popup', s.name.replace(fName, "5_txtIMEI_ASPxPopupControl1"));
            var UUID = getClientInstance('TxtBox', s.name.replace(fName, "5_PO_OE_NO"));
            var OnHandQty = getClientInstance('TxtBox', s.name.replace(fName, "6_txtOnHandQty"));

            EnableIMEI(fName, s.name);
            var Qty = s.GetValue();
            var iQty = 0;
            var inQty = 0;
            var ohQty = 0;
            if (Qty == null || Qty == "") {
                e.isValid = false;
                e.errorText = '驗收量不允許空白，請重新輸入';
                return false;
            }
            else {
                iQty = Number(Qty) + Number(CHECK_IN_QTY.GetText()); //本次驗收量加之前驗收量;
                inQty = Number(txtINQTY.innerText)
                ohQty = Number(OnHandQty.GetText());

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
                } else {
                    //imeiPop.SetContentUrl(setContentUrl_IMEI_QTY1(imeiPop.GetContentUrl(), s.GetText()));
                    //**2011/04/27 Tina：將URL傳遞的參數加密。
                    setPopupContentUrl_INV09(imeiPop, UUID.GetText(), lblProdNo.GetText(), s.GetText());

                    e.processOnServer = false;
                }
                if (Number(inQty) && Number(iQty) && Number(ohQty)) {
                    ohQty = inQty - iQty;
                    OnHandQty.SetText(ohQty);
                }

                OnHandQty.SetText(txtINQTY.innerText - Qty - Number(CHECK_IN_QTY.GetText()));
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip"></div>
    <input type="hidden" id="MUUID" runat="server" />
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div>

                <div class="titlef">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <!--進貨驗收作業-->
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReceivingInspection %>"></asp:Literal>
                            </td>
                            <td align="right">
                                <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e){ document.location='INV08.aspx'; }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--門市名稱Label1-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="labStoreName" runat="server">
                                </dx:ASPxLabel>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單狀態Label2-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="labOrderStatus" runat="server">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--驗收單編號Label7-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceivingNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="labReceivingNo" runat="server">
                                </dx:ASPxLabel>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--更新日期Label3-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="labModiDate" runat="server">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--PO/OE_NO-Label5-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, pooeno %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="labPooeNo" runat="server">
                                </dx:ASPxLabel>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--更新人員Label4-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="labModBy" runat="server">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單/主配編號Label6-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="labOrderNo" runat="server">
                                </dx:ASPxLabel>
                            </td>
                            <td class="tdtxt" colspan="2">
                                <dx:ASPxTextBox ID="txtOSH_ID" runat="server" Visible="false">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="seperate"></div>
                
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="DUUID"
                    EnableCallBacks="true"  Width="100%"
                    OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="0" >
                             <DataItemTemplate>
                                <dx:ASPxTextBox ID="lblProdNo" runat="server" Text='<%# Bind("PRODNO") %>' Border-BorderStyle="None" ReadOnly="true"></dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="1">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtProdName" runat="server" Text='<%# Bind("PRODNAME") %>' Border-BorderStyle="None" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="IN_QTY" Caption="<%$ Resources:WebResources, ArrivalQuantity %>" VisibleIndex="2">
                            <DataItemTemplate>
                                <dx:ASPxLabel ID="txtINQTY" runat="server" Text='<%# Bind("IN_QTY") %>'>
                                </dx:ASPxLabel>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="right"></CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="ACCEPT_QTY" runat="server" Caption="<%$ Resources:WebResources, InspectionQuantity %>" VisibleIndex="3">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtAdjuestmentQuantity" runat="server" Text='<%# Bind("ACCEPT_QTY") %>' Width="60px">
                                    <ValidationSettings SetFocusOnError="true">
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s,e){ CheckTranOutQty(s, e); }" />
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="right">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="imgIMEI" runat="server" Caption=" " VisibleIndex="4" Width="20px">
                            <DataItemTemplate>
                                <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                    <ClientSideEvents Click="function(s,e){}" />
                                </dx:ASPxImage>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="IMEI" runat="server" Caption="<%$ Resources:WebResources, Imei %>" VisibleIndex="5">
                            <DataItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="lblIMEI_QTY" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_QTY") %>'
                                                Border-BorderStyle="None" Width="20px" DisabledStyle-Font-Underline="true">
                                                <ClientSideEvents TextChanged="function(s, e) { ChangeImageIMEI(s, e); }" />
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtIMEIFlag" runat="server" Text='<% #Bind("IMEI_FLAG") %>' ClientVisible="false"></dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="txtCHECK_IN_QTY" runat="server" Text='<% #Bind("CHECK_IN_QTY") %>' ClientVisible="false"></dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="PO_OE_NO" runat="server" Width="68px" Text='<%#BIND("PO_OE_NO")  %>' ClientVisible="false"></dx:ASPxTextBox>
                                            <uc1:PopupControl ID="txtIMEI" runat="server" PopupControlName="InputIMEIData" AssignToControlId="lblIMEI_QTY" Text='<%# Bind("IMEI") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="ON_HAND_QTY" Caption="<%$ Resources:WebResources, OnOrderQuantity %>" VisibleIndex="6">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtOnHandQty" runat="server" ReadOnly="true" Text='<% #Bind("ON_HAND_QTY") %>'
                                    Border-BorderStyle="None" Width="20px" DisabledStyle-Font-Underline="true">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="right">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="SUPPNAME" Caption="<%$ Resources:WebResources, Supplier %>" CellStyle-Font-Underline="true" VisibleIndex="7">
                            <DataItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="lb13" runat="server" Text='<%# Bind("SUPPNAME") %>' Enabled="false"
                                                DisabledStyle-Font-Underline="true">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="lblsuppno" runat="server" Text='<%# Bind("SUPPNO") %>' Enabled="false"
                                                DisabledStyle-Font-Underline="true" Visible="false">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="txtDUUID" runat="server" Text='<%# Bind("DUUID") %>' Enabled="false"
                                                DisabledStyle-Font-Underline="true" Visible="false">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="10"></SettingsPager>
                </cc:ASPxGridView>

                <div class="seperate"></div>

                <div class="btnPosition">
                    <table align="center" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                    OnClick="btnSave_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    OnClick="btnCancel_Click">
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="btnSave1" ClientInstanceName ="btnSave1" ClientVisible ="false"  runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    OnClick="btnSave1_Click">
                                </dx:ASPxButton>
                                <dx:ASPxTextBox ID="SaveItem" ClientInstanceName ="SaveItem" runat="server" ReadOnly="false" Text='<% #Bind("IMEI_FLAG") %>'
                                    Border-BorderStyle="None" ClientVisible="false">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnPrBarCo" runat="server" 
                                    Text="<%$ Resources:WebResources, PrintBarCode %>" onclick="btnPrBarCo_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
       <iframe id="fDownload" style="display: none" src="" runat="server"></iframe>
</asp:Content>
