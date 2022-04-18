<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV06_1.aspx.cs" Inherits="VSS_INV_INV06_INV06_1" MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
        function setPopupContentUrl_INV06(imeiPop, UUID, PRODNO, QTY) {

            var url = imeiPop.GetContentUrl();
            var sValue = url.split('?');
            if (sValue.length > 1) {
                var t = new Date().getTime();
                url = sValue[0] + "?SysDate=" + t + "&KeyFieldValue1=RTND_IMEI;" + UUID + ";" + PRODNO + ";" + QTY;
            }

            ModifyPopupURLByEncrypt(url, imeiPop);
        }
    </script>
    
    <script type="text/javascript">
        function CalcuDiffStkQty(s, e)
        {
            var fName = "4_txtUNOPENQTY";
            var lblProdNo = getClientInstance('TxtBox', s.name.replace(fName, "1_lblProdNo"));
            var lbltotal = getClientInstance('TxtBox', s.name.replace(fName, "3_txtONQTY"));
            var lblopen = getClientInstance('TxtBox', s.name.replace(fName, "5_txtOPENQTY"));
            var txtRTNQTY = getClientInstance('TxtBox', s.name.replace(fName, "6_txtRTNQTY"));
            var UUID = getClientInstance('TxtBox', s.name.replace(fName, "8_txtRTND_UP_ID"));
            var imeiPop = getClientInstance('Popup', s.name.replace(fName, "8_txtIMEI_ASPxPopupControl1"));
            var lbldiff = getClientInstance('TxtBox', s.name.replace(fName, "9_lbldiff"));

            lbldiff.SetValue("");
            txtRTNQTY.SetValue("");
            if (lblopen.GetText() == null || lblopen.GetText() == "")
            {
                lblopen.SetValue(0);
            }

            var StkchkQty = s.GetValue();
            var iStkchkQty = 0;
            var diffQty = 0;
            if (StkchkQty == null || StkchkQty == "")
            {
                iStkchkQty = 0;
            }
            else
            {
                iStkchkQty = Number(StkchkQty);
                if (isNaN(iStkchkQty))
                {
                    alert('輸入字串不符合數字格式，請重新輸入');
                    s.SetValue("");
                    EnableIMEI(fName, s.name);
                    return false;
                }
                else if (iStkchkQty < 0)
                {
                    alert('退貨量不允許小於0，請重新輸入');
                    s.SetValue("");
                    EnableIMEI(fName, s.name);
                    return false;
                }
            }

            var iStkQty = lbltotal.GetValue();
            var iStkQty2 = lblopen.GetValue();

            var Diff = Number(iStkQty) - Number(iStkQty2) - Number(iStkchkQty);
            var tt = Number(iStkQty2) + Number(iStkchkQty);
            if (Diff < 0)
            {
                alert('退貨量不允許超過庫存量，請重新輸入');
                s.SetValue("");
                EnableIMEI(fName, s.name);
                return false;
            }

            if (Diff != 0)
                alert('數量有差異，請確認');
                
            lbldiff.SetValue(Diff);
            txtRTNQTY.SetValue(tt);
            EnableIMEI(fName, s.name);

            //imeiPop.SetContentUrl(setContentUrl_IMEI_PRODNO(imeiPop.GetContentUrl(), tt));
            //**2011/04/27 Tina：將URL傳遞的參數加密。
            setPopupContentUrl_INV06(imeiPop, UUID.GetText(), lblProdNo.GetText(), txtRTNQTY.GetText());

        }

        function CalcuDiffStkQty2(s, e)
        {
            var fName = "5_txtOPENQTY";
            var lblProdNo = getClientInstance('TxtBox', s.name.replace(fName, "1_lblProdNo"));
            var lbltotal = getClientInstance('TxtBox', s.name.replace(fName, "3_txtONQTY"));
            var lblopen = getClientInstance('TxtBox', s.name.replace(fName, "4_txtUNOPENQTY"));
            var txtRTNQTY = getClientInstance('TxtBox', s.name.replace(fName, "6_txtRTNQTY"));
            var imeiPop = getClientInstance('Popup', s.name.replace(fName, "8_txtIMEI_ASPxPopupControl1"));
            var UUID = getClientInstance('TxtBox', s.name.replace(fName, "8_txtRTND_UP_ID"));
            var lbldiff = getClientInstance('TxtBox', s.name.replace(fName, "9_lbldiff"));
            
            lbldiff.SetValue("");
            txtRTNQTY.SetValue("");
            if (lblopen.GetText() == null || lblopen.GetText() == "")
            {
                lblopen.SetValue(0);
            }

            var StkchkQty = s.GetValue();
            var iStkchkQty = 0;
            var diffQty = 0;
            if (StkchkQty == null || StkchkQty == "")
            {
                iStkchkQty = 0;
            }
            else
            {
                iStkchkQty = Number(StkchkQty);
                if (isNaN(iStkchkQty))
                {
                    alert('輸入字串不符合數字格式，請重新輸入');
                    s.SetValue("");
                    EnableIMEI(fName, s.name);
                    return false;
                }
                else if (iStkchkQty < 0)
                {
                    alert('退貨量不允許小於0，請重新輸入');
                    s.SetValue("");
                    EnableIMEI(fName, s.name);
                    return false;
                }
            }

            var iStkQty = lbltotal.GetValue();
            var iStkQty2 = lblopen.GetValue();

            var Diff = Number(iStkQty) - Number(iStkQty2) - Number(iStkchkQty);
            var tt = Number(iStkQty2) + Number(iStkchkQty);
            if (Diff < 0)
            {
                alert('退貨量不允許超過庫存量，請重新輸入');
                s.SetValue("");
                EnableIMEI(fName, s.name);
                return false;
            }

            if (Diff != 0)
                alert('數量有差異，請確認');
            lbldiff.SetValue(Diff);
            txtRTNQTY.SetValue(tt);

            EnableIMEI(fName, s.name);

            //imeiPop.SetContentUrl(setContentUrl_IMEI_PRODNO(imeiPop.GetContentUrl(), tt));
            //**2011/04/27 Tina：將URL傳遞的參數加密。
            setPopupContentUrl_INV06(imeiPop, UUID.GetText(), lblProdNo.GetText(), txtRTNQTY.GetText());

        }

        //設定IMEI的商品料號
        function setContentUrl_IMEI_PRODNO(url,strRTNQTY)
        {
            var s = url.split('SysDate=');
            if (s.length > 1)
            {
                var ordSysDate = s[1].split('&')[0];
                var newSysDate = Date();
                url = url.replace(ordSysDate, newSysDate);
            }

            var u = url.split('KeyFieldValue1=');
            if (u.length > 1)
            {
                var oldKeyFieldValue1 = u[1].split('&')[0];
                pValues = oldKeyFieldValue1.split(';');
                if (pValues.length >= 4)
                {
                    pValues[3] = strRTNQTY;
                    var newKeyFieldValue1 = pValues[0] + ';' + pValues[1] + ';' + pValues[2] + ';' + pValues[3];                
                    url = url.replace(oldKeyFieldValue1, newKeyFieldValue1);
                }
            }
            return url;
        }

        //是否可編輯IMEI
        function EnableIMEI(object, ControlName)
        {
            var fName = object;
            var strProductName = PRODNAME.GetText();
            var txtRTNQTY = getClientInstance('TxtBox', ControlName.replace(fName, "6_txtRTNQTY"));
            var imgIMEI = getClientInstance('Image', ControlName.replace(fName, "7_imgIMEI"));
            var txtInputIMEIData = getClientInstance('TxtBox', ControlName.replace(fName, "8_txtIMEI_txtControl"));
            var txtIMEIFlagData = getClientInstance('TxtBox', ControlName.replace(fName, "8_txtIMEIFlag"));
            var btnInputIMEIData = getClientInstance('Button', ControlName.replace(fName, "8_txtIMEI_btnControl"));
            var lblIMEI_QTY = getClientInstance('TxtBox', ControlName.replace(fName, "8_lblIMEI_QTY"));
            var imeiPop = getClientInstance('Popup', ControlName.replace(fName, "8_txtIMEI_ASPxPopupControl1"));
            var iQty = Number(txtRTNQTY.GetText());
            
            //商品料號 && 移出數量 皆有值，才可編輯IMEI (IMEI_FLAG = 3, 4 要設定IMEI)
            if ((txtIMEIFlagData.GetText() == "3") && strProductName != '' && !isNaN(iQty) && txtRTNQTY.GetText() != '' && txtRTNQTY.GetText() != null)
            {
                txtInputIMEIData.SetVisible(true);
                btnInputIMEIData.SetVisible(true);
                txtInputIMEIData.SetEnabled(true);
                btnInputIMEIData.SetEnabled(true);

                imeiPop.SetPopupElementID(btnInputIMEIData.name);
                imgIMEI.SetVisible(true);
                if (txtRTNQTY.GetText() == lblIMEI_QTY.GetText())
                {
                    imgIMEI.SetImageUrl("../../../Icon/check.png");
                    imgIMEI.SetSize(16, 16);
                }
                else
                {
                    imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                    imgIMEI.SetSize(27, 16);
                }
            }
            else
            {
                if (txtInputIMEIData.GetVisible()) txtInputIMEIData.SetVisible(false);
                if (btnInputIMEIData.GetVisible()) btnInputIMEIData.SetVisible(false);
                imeiPop.SetPopupElementID(null);

                imgIMEI.SetVisible(false);
            }

        }

        //變更IMEI圖示
        function ChangeImageIMEI(s, e)
        {
            var fName = "8_lblIMEI_QTY";
            var IMEI_Qty = s;
            var lblProdNo = getClientInstance('TxtBox', s.name.replace(fName, "1_lblProdNo"));
            var txtRTNQTY = getClientInstance('TxtBox', s.name.replace(fName, "6_txtRTNQTY"));
            var imgIMEI = getClientInstance('Image', s.name.replace(fName, "7_imgIMEI"));
            var UUID = getClientInstance('TxtBox', s.name.replace(fName, "8_txtRTND_UP_ID"));

            if (txtRTNQTY.GetText() == IMEI_Qty.GetText())
            {
                imgIMEI.SetImageUrl("../../../Icon/check.png");
                imgIMEI.SetSize(16, 16);
            }
            else
            {
                imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                imgIMEI.SetSize(27, 16);
            }
            this.objName = fName;
            this.s = s;
            //var pValues = getIMEI(s, e);
            //0:TableName 1:OE_NO 2:PRODNO
            //PageMethods.IMEIContent(pValues[0], pValues[1], pValues[2], IMEIContent);

            PageMethods.IMEIContent("RTND_IMEI", UUID.GetText(), lblProdNo.GetText(), IMEIContent);
        }

        function IMEIContent(values)
        {
            var lblIMEI_QTY = window.document.all[s.name.replace(objName, "8_lblIMEI_QTY")];
            lblIMEI_QTY.attributes["onmouseover"].value = "show('" + values + "');";
            lblIMEI_QTY.attributes["onmouseout"].value = "hide();";
        }

        //取得IMEI上傳參數;
        function getIMEI(s, e)
        {
            var fName = this.objName;
            var pValues = null;
            var imeiPop = getClientInstance('TxtBox', s.name.replace(fName, "8_txtIMEI_ASPxPopupControl1"));
            var u = imeiPop.GetContentUrl().split('KeyFieldValue1=');
            if (u.length > 1)
            {
                var oldKeyFieldValue1 = u[1].split('&')[0];
                pValues = oldKeyFieldValue1.split(';');
            }
            return pValues;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:HiddenField ID="tempStatus" runat="server" Value="0" />
    <asp:HiddenField ID="tempUnopen" runat="server" Value="0" />
      <dx:ASPxTextBox ID="txtWorkDate" runat="server" ClientVisible="false" />
    <div id="tooltip"></div>

        <div class="func">
            <div class="titlef">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <!--退倉作業-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousing %>"></asp:Literal>
                        </td>
                        <td align="right">
                            <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){ document.location='INV06.aspx'; }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉單號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbOrderNo" runat="server" Text=""></dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, WarehousedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbRtndate" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--狀態-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbDataStatus" runat="server" Text="" Visible="false"></dx:ASPxLabel>
                            <dx:ASPxLabel ID="lbStatus" runat="server" Text=""></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉開始日-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStartDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbBdate" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉原因-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReasonForWarehousing %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbReDesc" runat="server" Text=" ">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbModidtm" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉結束日-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingEndDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbEdate" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--退倉處理-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingProcess %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbDesc" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbModiuser" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--備註-->
                            <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lbStatus2" runat="server" Text="">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="seperate"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTND_UP_ID" EnableCallBacks="true" Width="100%"                   
                    OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnPageIndexChanged="gvMaster_PageIndexChanged" IsClearStatus="false">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="" VisibleIndex="0" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRODNO" VisibleIndex="1" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductCode %>">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="lblProdNo" runat="server" Text='<%# Bind("PRODNO") %>' Border-BorderStyle="None" ReadOnly="true"></dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRODNAME" VisibleIndex="2" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtProdName" runat="server" Text='<%# Bind("PRODNAME") %>' ClientInstanceName="PRODNAME"
                                    Border-BorderStyle="None" ReadOnly="true" Width="250" >
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ON_HAND_QTY" VisibleIndex="3" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, AccountOnTheStock %>">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtONQTY" Border-BorderStyle="None" HorizontalAlign="Right" runat="server" Enabled="true" Text='<%# Bind("ON_HAND_QTY") %>' ReadOnly="true">
                                </dx:ASPxTextBox>
                                <asp:HiddenField ID="PROD_ID" runat="server" />
                                <asp:HiddenField ID="Store_ID" runat="server" />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="UNOPENQTY" VisibleIndex="4" Name="UNOPENQTY" runat="server" ReadOnly="false" Caption="<%$ Resources:WebResources, SealedQuantity %>">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtUNOPENQTY" Text='<%# Bind("UNOPENQTY") %>' HorizontalAlign="Right"
                                    runat="server" Width="100px">
                                    <ClientSideEvents TextChanged="function(s,e){ CalcuDiffStkQty(s, e); }" />
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="OPENQTY" Name="OpenQty" VisibleIndex="5" runat="server" ReadOnly="false" Caption="<%$ Resources:WebResources, OpenedQty %>">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtOPENQTY" Text='<%# Bind("OPENQTY") %>' HorizontalAlign="Right"
                                    runat="server" Width="100px">
                                    <ClientSideEvents TextChanged="function(s,e){ CalcuDiffStkQty2(s, e); }" />
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="right"></CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="RTNQTY" VisibleIndex="6" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ReturnQuantity %>">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtRTNQTY" Text='<%# Bind("RTNQTY") %>' Border-BorderStyle="None"
                                    HorizontalAlign="Right" runat="server" ReadOnly="true">
                                    <ClientSideEvents TextChanged="function(s,e){}" />
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left"></CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="imgIMEI" VisibleIndex="7" Caption=" " Width="20px">
                            <DataItemTemplate>
                                <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                    <ClientSideEvents Click="function(s,e){}" />
                                </dx:ASPxImage>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="IMEI" VisibleIndex="8" Caption="<%$ Resources:WebResources, Imei %>">
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
                                            <dx:ASPxTextBox ID="txtIMEIFlag" runat="server" ReadOnly="false" Text='<% #Bind("IMEI_FLAG") %>' ClientVisible="false"></dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="txtRTND_UP_ID" runat="server" Width="68px" Text='<%#BIND("RTND_UP_ID")  %>' ClientVisible="false"></dx:ASPxTextBox>
                                            <uc1:PopupControl ID="txtIMEI" runat="server" PopupControlName="InputIMEIData" AssignToControlId="lblIMEI_QTY" Text='<%# Bind("IMEI") %>'    />
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn FieldName="DIFF" VisibleIndex="9" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, OfDifference %>">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="lbldiff" Border-BorderStyle="None" HorizontalAlign="Right" runat="server" Text='<%#BIND("DIFF")  %>' ReadOnly="true">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="5"></SettingsPager>
                </cc:ASPxGridView>
                <div class="seperate"></div>
                <div class="btnPosition">
                    <table align="center" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnSave" ClientInstanceName="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                    OnClick="btnSave_Click" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function (s, e) {e.processOnServer = confirm('退倉單儲存後不可再修改，請確認');}" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    AutoPostBack="false">
                                    <ClientSideEvents Click="function(s, e){ document.location='INV06.aspx'; }" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnPrint" runat="server" Text="<%$ Resources:WebResources, print %>"
                                    OnClick="btnPrint_Click" >
                                    <ClientSideEvents Click="function (s, e) {e.processOnServer = confirm('是否列印退倉單?');}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <iframe id="fDownload" width="100%" src="" style="display: none" runat="server"></iframe>
        
</asp:Content>
