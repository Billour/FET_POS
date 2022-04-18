<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV18_1.aspx.cs" Inherits="VSS_INV_INV18_1" %>

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
        function setPopupContentUrl_INV18(imeiPop, UUID, PRODNO, iQty)
        {

            var v_iQty = iQty;                  //調整量
            if (v_iQty < 0) v_iQty = -v_iQty;   //若調整量 小於 0(為負數)，進行變型 => 變成正數，以便比較IMEI數量是否符合。
            var url = imeiPop.GetContentUrl();
            var sValue = url.split('?');
            if (sValue.length > 1)
            {
                var t = new Date().getTime();
                url = sValue[0] + "?SysDate=" + t + "&KeyFieldValue1=STOCKADJ_D_IMEI;" + UUID + ";" + PRODNO + ";" + v_iQty + ";" + iQty + ";" + txtSTORE_NO.GetText();
            }

            ModifyPopupURLByEncrypt(url, imeiPop);
        }
    </script>

    <script type="text/javascript">
        _gvSender = null;
        _gvEventArgs = null;

        //檢查門市是否存在
        function getStore(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getStore(_gvSender.GetText(), getStore_OnOK);
        }

        function getStore_OnOK(returnData)
        {
            if (returnData == '')
            {
                window.event.returnValue = null;
                _gvSender.SetValue(null);
                lblStoreName.SetValue(null);
                txtADJDATE.SetValue(new Date());
            }
            else
            {
                var DataArray = returnData.split(";");
                lblStoreName.SetValue(DataArray[0]);
                txtADJDATE.SetValue(new Date(DataArray[1]));

                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
        }

        //庫存調整原因
        function getAdjNAME(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getAdjNAME(_gvSender.GetText(), getAdjNAME_OnOK);
        }

        function getAdjNAME_OnOK(returnData)
        {

            if (returnData == '')
            {
                _gvSender.SetValue(null);
                STOCKADJ_REASON_CODE.SetText(null);
            }
            else
            {
                STOCKADJ_REASON_CODE.SetText(returnData);
            }
        }

        //檢查商品料號是否存在
        function getProductInfo(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getProductInfo(s.GetText(), txtSTORE_NO.GetText(), getProductInfo_OnOK);
            else
            {
                PRODNAME.SetText(null);
            }
        }

        function getProductInfo_OnOK(returnData)
        {

            if (returnData == '')
            {
                alert("商品料號不存在!");
                _gvSender.SetValue(null);
                PRODNAME.SetText(null);
                INV_OnHandQty.SetValue(null);
                txtIMEIFlag.SetText(null);
                lblIMEI_QTY.SetText(null);
                txtInputIMEIData.SetText(null);
            }
            else
            {
                var values = returnData.split(';');

                var OldProdName = PRODNAME.GetText();
                if (OldProdName != '' && OldProdName != values[1])
                {
                    //變更商品，就把原本選取的IMEI個數清空
                    lblIMEI_QTY.SetText(null);
                    txtInputIMEIData.SetText(null);
                    PageMethods.delIMEI(UUID.GetText());
                }

                PRODNAME.SetText(values[1]);        //商品名稱
                INV_OnHandQty.SetValue(values[2]);  //庫存數
                txtIMEIFlag.SetText(values[3]);     //IMEI_Flag

                EnableIMEI();   //是否可編輯IMEI

                //imeiPop.SetContentUrl(setContentUrl_IMEI_PRODNO(imeiPop.GetContentUrl(), values[0]));
                //**2011/04/27 Tina：將URL傳遞的參數加密。
                setPopupContentUrl_INV18(imeiPop, UUID.GetText(), txtProdNo.GetText(), ADJQTY.GetText());

            }
        }

        //是否可編輯IMEI
        function EnableIMEI()
        {

            var strProductName = PRODNAME.GetText();  //商品料號

            var iQty = Number(ADJQTY.GetText());
            //商品料號 && 調整數量 皆有值，才可編輯IMEI (IMEI_FLAG =3 或 4 才需設定IMEI)
            if ((txtIMEIFlag.GetText() == '3' || txtIMEIFlag.GetText() == '4') && strProductName != '' && !isNaN(iQty) && ADJQTY.GetText() != '')
            {
                if (!txtInputIMEIData.GetVisible()) txtInputIMEIData.SetVisible(true);
                if (!btnInputIMEIData.GetVisible()) btnInputIMEIData.SetVisible(true);
                btnInputIMEIData.SetEnabled(true);
                imeiPop.SetPopupElementID(btnInputIMEIData.name);
                if (!imgIMEI.GetVisible()) imgIMEI.SetVisible(true);

                var v_ADJQTY = Number(ADJQTY.GetText()); //調整量
                if (v_ADJQTY < 0) v_ADJQTY = -v_ADJQTY;  //若調整量 小於 0(為負數)，進行變型 => 變成正數，以便比較IMEI數量是否符合。

                if (v_ADJQTY == Number(lblIMEI_QTY.GetText()))
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
                imeiPop.SetPopupElementID(null);
                if (imgIMEI.GetVisible()) imgIMEI.SetVisible(false);
            }

        }

        //變更IMEI圖示
        function ChangeImageIMEI(s, e)
        {

            var IMEI_Qty = s;
            var v_ADJQTY = Number(ADJQTY.GetText());  //調整量
            OLDADJQTY.SetText(ADJQTY.GetText()); //給修改過後 調整量 保存起來
            if (v_ADJQTY < 0) v_ADJQTY = -v_ADJQTY;   //若調整量 小於 0(為負數)，進行變型 => 變成正數，以便比較IMEI數量是否符合。

            if (v_ADJQTY == Number(IMEI_Qty.GetText()))
            {
                imgIMEI.SetImageUrl("../../../Icon/check.png");
                imgIMEI.SetSize(16, 16);
            }
            else
            {
                imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                imgIMEI.SetSize(27, 16);
            }

            this.s = s;
            //var pValues = getIMEI(s, e);
            //0:TableName 1:OE_NO 2:PRODNO
            //PageMethods.IMEIContent(pValues[0], pValues[1], pValues[2], IMEIContent);

            PageMethods.IMEIContent("STOCKADJ_D_IMEI", UUID.GetText(), txtProdNo.GetText(), IMEIContent);
        }

        function IMEIContent(values)
        {
            var lblIMEI = window.document.all[lblIMEI_QTY.name];
            lblIMEI.attributes["onmouseover"].value = "show('" + values + "');";
            lblIMEI.attributes["onmouseout"].value = "hide();";
        }

        //取得IMEI上傳參數
        function getIMEI(s, e)
        {
            var fName = this.objName;
            var pValues = null;
            var u = imeiPop.GetContentUrl().split('KeyFieldValue1=');
            if (u.length > 1)
            {
                var oldKeyFieldValue1 = u[1].split('&')[0];
                pValues = oldKeyFieldValue1.split(';');
            }
            return pValues;
        }

        //檢查調整量
        function CheckTranOutQty(s, e)
        {

          

            var Qty = s.GetValue();
            var iQty = 0;
            if (Qty == null || Qty == "")
            {
                e.isValid = false;
                e.errorText = '調整量不允許空白，請重新輸入';
                return false;
            }
            else
            {
                iQty = Number(Qty);
                if (isNaN(iQty))
                {
                    e.isValid = false;
                    e.errorText = '輸入字串不為數字格式，請重新輸入';
                    return false;
                }
                else if (iQty == 0)
                {
                    e.isValid = false;
                    e.errorText = '調整量不允許為0，請重新輸入';
                    return false;
                }
                else
                {
                    //**2011/02/17 Tina：如果庫存量 + 調整量(可輸入負數) < 0，則要顯示錯誤訊息
                    if (Number(INV_OnHandQty.GetValue()) + iQty < 0)
                    {
                        e.isValid = false;
                        e.errorText = '庫存量不足，請重新輸入';
                        return false;
                    }
                    
                    //判斷如果 輸入的新值 與 舊值 如果正負號不同 清空IMEI輸入資料
                    var v_OLDADJQTY;
                    var v_OLDADJQTY_tmp;
                    v_OLDADJQTY_tmp = OLDADJQTY.GetText();
                
                    if (v_OLDADJQTY_tmp != null || v_OLDADJQTY_tmp != "")
                    {
                        v_OLDADJQTY = Number(v_OLDADJQTY_tmp);
                        if (!isNaN(v_OLDADJQTY))
                        {
                            if ((iQty > 0 && v_OLDADJQTY < 0) || (iQty < 0 && v_OLDADJQTY > 0))
                            {
                                //就把原本選取的IMEI個數清空
                                lblIMEI_QTY.SetText(null);
                                txtInputIMEIData.SetText(null);
                                PageMethods.delIMEI(UUID.GetText());
                            }
                        }

                    }
                    EnableIMEI();
                    //if (v_iQty < 0) v_iQty = -v_iQty;  

                    //imeiPop.SetContentUrl(setContentUrl_IMEI_QTY(imeiPop.GetContentUrl(), v_iQty));

                    //**2011/04/27 Tina：將URL傳遞的參數加密。
                    setPopupContentUrl_INV18(imeiPop, UUID.GetText(), txtProdNo.GetText(), iQty);

                }
            }
        }
        function onOK()
        {
            __doPostBack('<%= txtBatchNO.UniqueID %>', 'AAA');
        }

        function InitDisUseCount()
        {
            if (gvMaster.IsEditing())
            {
                btnSave.SetEnabled(false);
                btnCancel.SetEnabled(false);
            }
            else
            {
                btnSave.SetEnabled(true);
                btnCancel.SetEnabled(true);
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip">
    </div>
    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockAdjustment %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='INV18.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整單號lblOrderNo-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lblADJNO" runat="server">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整日期transferOutStartDate-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <div style="width: 120px;">
                                <dx:ASPxDateEdit ID="txtADJDATE" ClientInstanceName="txtADJDATE" runat="server" ClientEnabled="false">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lblSTATUS" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整門市編號-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
                        </td>
                        <td align="left">
                            <uc1:PopupControl ID="txtSTORE_NO" runat="server" PopupControlName="StoresPopup"
                                TextBoxClientInstanceName="txtSTORE_NO" SetClientValidationEvent="getStore" />
                        </td>
                        <td align="left" nowrap="nowrap">
                            <dx:ASPxTextBox ID="lblStoreName" ClientInstanceName="lblStoreName" runat="server"
                                Text="" Border-BorderStyle="None" ReadOnly="true">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期Label3-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lblMODTM" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--備註TextBox3-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap">
                            <dx:ASPxTextBox ID="txtRemark" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員Label4-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <%--<dx:ASPxLabel ID="lblMOUSER" runat="server"></dx:ASPxLabel>--%>
                            <dx:ASPxLabel ID="lblMOUSER" runat="server">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
                    KeyFieldName="STOCKADJD_ID" EnableViewState="true" OnRowInserting="gvMaster_RowInserting"
                    OnRowUpdating="gvMaster_RowUpdating" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                    OnInitNewRow="gvMaster_InitNewRow" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                    OnRowValidating="gvMaster_RowValidating" OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnStartRowEditing="gvMaster_StartRowEditing" OnCancelRowEditing="gvMaster_CancelRowEditing">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <div style="text-align: center">
                                    <input type="checkbox" onclick="if (typeof(gvMaster) != 'undefined') {gvMaster.SelectAllRowsOnPage(this.checked);}"
                                        title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                            <HeaderCaptionTemplate>
                            </HeaderCaptionTemplate>
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditCellStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup"
                                    TextBoxClientInstanceName="txtProdNo" Text='<%#Bind("PRODNO") %>' SetClientValidationEvent="getProductInfo"
                                    IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>' />
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" ReadOnly="True" Width="200"
                            Caption="<%$ Resources:WebResources, ProductName %>">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtProdName" runat="server" Text='<%# Bind("PRODNAME") %>' ClientInstanceName="PRODNAME"
                                    Border-BorderStyle="None" Width="200" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="INV_OnHandQty" runat="server" Caption="<%$ Resources:WebResources, StockQuantity %>">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtStockQuantity" runat="server" Width="68px" HorizontalAlign="Right"
                                                ClientInstanceName="INV_OnHandQty" Text='<%#BIND("INV_OnHandQty")  %>' Border-BorderStyle="None"
                                                ReadOnly="true">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ADJQTY" runat="server" Caption="<%$ Resources:WebResources, AdjuestmentQuantity %>">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtAdjuestmentQuantity" runat="server" Width="68px" HorizontalAlign="Right"
                                                MaxLength="8" Text='<%#BIND("ADJQTY")  %>' ClientInstanceName="ADJQTY" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                                <ClientSideEvents Validation="function(s,e){ CheckTranOutQty(s, e); }" />
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="txtOLDADJQTY" runat="server" Width="68px" HorizontalAlign="Right"
                                                MaxLength="8" ClientInstanceName="OLDADJQTY" ClientVisible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ADJREASON" runat="server" Caption="<%$ Resources:WebResources, ReasonForAdjustment %>">
                            <PropertiesTextEdit>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditCellStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <uc1:PopupControl ID="ChoseForAdjustment" runat="server" PopupControlName="ForAdjustment"
                                                Text='<%#BIND("ADJREASON")  %>' OnClientTextChanged="function(s,e) { getAdjNAME(s,e); }"
                                                IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>' Width="300" />
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtForAdjustment" runat="server" Text='<%#BIND("STOCKADJ_REASON_CODE")  %>'
                                                ClientInstanceName="STOCKADJ_REASON_CODE" ClientVisible="false">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtForAdjustment1" runat="server" Text='<%#BIND("ADJREASON")  %>'
                                                Border-BorderStyle="None" ReadOnly="true">
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="txtForAdjustment" runat="server" Text='<%#BIND("STOCKADJ_REASON_CODE")  %>'
                                                Border-BorderStyle="None" ClientVisible="false">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="imgIMEI" Caption=" " Width="20px">
                            <DataItemTemplate>
                                <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                </dx:ASPxImage>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="" ClientInstanceName="imgIMEI">
                                </dx:ASPxImage>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>">
                            <DataItemTemplate>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td align="right">
                                            <div id="divIMEI_QTY" runat="server">
                                                <dx:ASPxLabel ID="lblIMEI" runat="server" Text='<% #Bind("IMEI_QTY") %>'>
                                                </dx:ASPxLabel>
                                            </div>
                                        </td>
                                        <div id="divIMEI" runat="server">
                                            <td>
                                                <dx:ASPxTextBox ID="lblIMEIFlag" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_FLAG") %>'
                                                    Border-BorderStyle="None" ClientVisible="false">
                                                </dx:ASPxTextBox>
                                                <uc1:PopupControl ID="lblShowIMEI" runat="server" PopupControlName="InputIMEIData"
                                                    Text='<%# Bind("IMEI") %>' Enabled="false" />
                                            </td>
                                        </div>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                            <EditItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="lblIMEI_QTY" runat="server" ClientInstanceName="lblIMEI_QTY"
                                                ReadOnly="true" Text='<% #Bind("IMEI_QTY") %>' Border-BorderStyle="None" Width="20px"
                                                DisabledStyle-Font-Underline="true">
                                                <ClientSideEvents TextChanged="function(s, e) { ChangeImageIMEI(s, e); }" />
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtIMEIFlag" runat="server" ReadOnly="false" Text='<% #Bind("IMEI_FLAG") %>'
                                                ClientInstanceName="txtIMEIFlag" ClientVisible="false">
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="STOCKADJD_ID" ClientInstanceName="UUID" runat="server" Width="68px"
                                                Text='<%#BIND("STOCKADJD_ID")  %>' ClientVisible="false">
                                            </dx:ASPxTextBox>
                                            <uc1:PopupControl ID="txtIMEI" runat="server" PopupControlName="InputIMEIData" AssignToControlId="lblIMEI_QTY"
                                                Text='<%# Bind("IMEI") %>' TextBoxClientInstanceName="txtInputIMEIData"  PopupControlClientInstanceName="imeiPop"
                                                ButtonClientInstanceName="btnInputIMEIData" />
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnNew_Click" Visible="true" CausesValidation="false">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                            OnClick="btnDelete_Click" ClientInstanceName="btnDelete" CausesValidation="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                        </dx:ASPxButton>
                                        <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" AllowResize="True"
                                            CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl='~/VSS/INV/INV18_1/INV18_1_Import.aspx'
                                            Width="640" Height="400" LoadingPanelID="lp" HeaderText="庫存調整上傳" onOKScript="onOK">
                                            <ContentStyle>
                                                <Paddings Padding="4px"></Paddings>
                                            </ContentStyle>
                                        </cc:ASPxPopupControl>
                                        <dx:ASPxLoadingPanel ID="lp" runat="server">
                                        </dx:ASPxLoadingPanel>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <Settings ShowTitlePanel="True"></Settings>
                    <SettingsPager PageSize="5" />
                    <SettingsEditing Mode="Inline" />
                    <ClientSideEvents EndCallback="InitDisUseCount" />
                </cc:ASPxGridView>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <table align="center" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                OnClick="btnSave_Click" ClientInstanceName="btnSave" >
                                <ClientSideEvents Click="function(s,e){ if(ASPxClientEdit.ValidateEditorsInContainer(null)) { Loading('存檔中...'); }}" />    
                            </dx:ASPxButton>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                OnClick="btnClear_Click" ClientInstanceName="btnCancel" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input type="hidden" id="txtBatchNO" runat="server" class="txtBatchNO" />
    <input type="hidden" id="hdSTNO" runat="server" />
    <input type="hidden" id="hdSEQNO" runat="server" />
</asp:Content>
