<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ORD12.aspx.cs" Inherits="VSS_ORD_ORD12_ORD12" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        _gvSender = null;
        _gvEventArgs = null;
        function getPRODNAME(s, e)
        {
            _gvEventArgs = e;
            _gvSender = s;

            if (s.GetText() != '')
            {
                intPRODNO = Number(s.GetText());
                if (isNaN(intPRODNO) || intPRODNO < 0)
                {
                    _gvSender.Focus();
                    _gvSender.SetText("");
                    ProductName.SetValue(null);
                    EStoreBookQTY.SetValue(null);
                    CurrentInvQTY.SetValue(null);
                    PurchQTY.SetValue(null);
                    alert("不可為非正整數或非數字，請重新輸入");
                    return false;
                }

            }


            var _STORE_NO = getClientInstance('Label', s.name.split('_gvMasterDV')[0] + "_txtSTORE_NO");
            if (s.GetText() != '')
                PageMethods.getPRODNAME(_gvSender.GetText(), _STORE_NO.outerText, getPRODNAME_OnOK);
        }
        function getPRODNAME_OnOK(returnData)
        {
            if (returnData == '')
            {
                alert("商品料號不存在!");
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
                _gvSender.SetText("");
                ProductName.SetValue(null);
                EStoreBookQTY.SetValue(null);
                CurrentInvQTY.SetValue(null);
                PurchQTY.SetValue(null);
            }
            else
            {
                var chk = returnData.split('|');
                if (chk[0] == "fail")
                {
                    switch (chk[1])
                    {
                        case "0":
                            alert("商品料號[" + chk[2] + "]，已設定在卡片群組裡，不可新增!!");
                            break;
                        case "1":
                            alert("搭贈商品[" + chk[2] + "]，已設定在卡片群組裡，不可新增!!");
                            break;
                        case "2":
                            alert("此商品已設定在[" + chk[2] + "]的搭贈商品裡，不可新增!!");
                            break;
                      
                    }

                

                    _gvSender.Focus();
                    _gvSender.SetText("");
                    ProductName.SetValue(null);
                    EStoreBookQTY.SetValue(null);
                    CurrentInvQTY.SetValue(null);
                    PurchQTY.SetValue(null);
                }
                else
                {
                    //EnableIMEI(fName, ProductName.name);
                    var values = chk[1].split(';');
                    ProductName.SetValue(values[0]);
                    EStoreBookQTY.SetValue(values[1]);
                    CurrentInvQTY.SetValue(values[2]);
                    PurchQTY.SetValue(values[3]);
                    if (values[4] != "" && !gvMasterDV.IsNewRowEditing())
                    {
                        ASPxButton3.SetEnabled(true);
                    }
                    else
                    {
                        ASPxButton3.SetEnabled(false);
                    }
                    //var _Qty = getClientInstance('TxtBox', _gvSender.name.replace("_4_txtProductNo_txtControl", "_9_txtPreOrderQty"));
                    var _Qty = document.getElementById(_gvSender.name.replace("_4_txtProductNo_txtControl", "_9_txtPreOrderQty_I"));
                    var tt = document.getElementById(_gvSender.name.replace("_edit", "_dxdt").replace("_4_txtProductNo_txtControl", "_gvDetailDV_DXMainTable"))

                    if (tt == null)
                    {
                        //gvMasterDV.PerformCallback(_gvSender.GetText() + ";" + _Qty.value + ";" + _gvSender.name.replace("ctl00_ASPxSplitter1_MainContentPlaceHolder_gvMasterDV_edit", "").replace("_4_txtProductNo_txtControl", ""));
                    } else
                    {
                        gvDetailDV.PerformCallback(_gvSender.GetText() + ";" + _Qty.value + ";" + _gvSender.name.split('_edit')[1].replace("_4_txtProductNo_txtControl", ""));
                    }
                    _gvSender.Focus();
                }
            }
        }

        function CheckOrdQty(s, e)
        {
            var StkchkQty = s.GetValue();
            var iStkchkQty = 0;
            var diffQty = 0;

            if (StkchkQty == null || StkchkQty == "")
            {
                e.isValid = false;
                e.errorText = '預訂量不允許為空白，請重新輸入';
                return false;
            }
            else
            {
                iStkchkQty = Number(StkchkQty);
                if (isNaN(iStkchkQty))
                {
                    e.isValid = false;
                    e.errorText = '輸入字串不符合數字格式，請重新輸入';
                    return false;
                }
                else if (iStkchkQty == 0)
                {
                    e.isValid = false;
                    e.errorText = '預訂量不允許為0，請重新輸入';
                    return false;
                }
                else if (iStkchkQty < 0)
                {
                    e.isValid = false;
                    e.errorText = '預訂量不允許小於0，請重新輸入';
                    return false;
                }
            }

            var _Pro = getClientInstance('Popup', s.name.replace("_9_txtPreOrderQty", "_4_txtProductNo_txtControl"));
            var tt = document.getElementById(s.name.replace("_edit", "_dxdt").replace("_9_txtPreOrderQty", "_gvDetailDV_DXMainTable"))
            if (tt == null)
            {
                //gvMasterDV.PerformCallback(_Pro.lastChangedValue + ";" + s.GetText() + ";" + s.name.replace("ctl00_ASPxSplitter1_MainContentPlaceHolder_gvMasterDV_edit", "").replace("_9_txtPreOrderQty", ""));
            } else
            {
                gvDetailDV.PerformCallback(_Pro.lastChangedValue + ";" + s.GetText() + ";" + s.name.split('_edit')[1].replace("_9_txtPreOrderQty", ""));

            }
        }

        function InitDisUseCount()
        {
            if (gvMasterDV.IsEditing())
            {
                btnSave.SetEnabled(false);
                btnDrop.SetEnabled(false);
            }
            else
            {
                btnSave.SetEnabled(true);
                btnDrop.SetEnabled(true);
            }

        }

        //不選取DISENABLED的CHECKBOX
        function CheckAll_onclick()
        {
            for (var i = 0; i < gvMasterDV.pageRowCount; i++)
            {
                if (gvMasterDV.GetRow(i + gvMasterDV.visibleStartIndex) != null && gvMasterDV.GetRow(i + gvMasterDV.visibleStartIndex).attributes["canSelect"].value == "true")
                {
                    var chk = document.getElementById("checkbox1");
                    if (chk.checked)
                    {
                        gvMasterDV.SelectRowOnPage(i + gvMasterDV.visibleStartIndex, true);
                    } else
                    {
                        gvMasterDV.SelectRowOnPage(i + gvMasterDV.visibleStartIndex, false);
                    }
                }
            }
        }      

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--預訂貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PrePurchaseOrder %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) { document.location='../ORD02/ORD02.aspx';return false; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--訂單編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, BookOrderNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="tdtxt">
                    <!--訂單日期-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, BookOrderDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
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
                    <asp:Label ID="lblUpdDateTime" runat="server" Text=""></asp:Label>
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
                    <asp:Label ID="lblUpdateUser" runat="server" Text=""></asp:Label>
                    <asp:Label ID="txtSTORE_NO" runat="server" Text="" ForeColor="White"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
        <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="ASPxCallback1"
            OnCallback="ASPxCallback1_Callback">
        </dx:ASPxCallback>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <div id="divContent" runat="server" class="SubEditBlock">
                <cc:ASPxGridView ID="gvMasterDV" runat="server" KeyFieldName="PRE_ORDER_SEQNO" ClientInstanceName="gvMasterDV"
                    AutoGenerateColumns="False" Width="98%" EnableCallBacks="false" OnHtmlRowCreated="gvMasterDV_HtmlRowCreated"
                    OnPageIndexChanged="grid_PageIndexChanged" OnRowUpdating="gvMasterDV_RowUpdating"
                    OnInitNewRow="gvMasterDV_InitNewRow" OnRowInserting="gvMasterDV_RowInserting"
                    OnRowCommand="gvMasterDV_RowCommand" OnRowValidating="gvMasterDV_RowValidating"
                    OnStartRowEditing="gvMasterDV_StartRowEditing" OnCancelRowEditing="gvMasterDV_CancelRowEditing"
                    OnCustomCallback="gvMasterDV_CustomCallback" OnCommandButtonInitialize="gvMasterDV_CommandButtonInitialize"
                    OnHtmlRowPrepared="gvMasterDV_HtmlRowPrepared">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <div style="text-align: center">
                                    <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                            <HeaderCaptionTemplate>
                            </HeaderCaptionTemplate>
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataButtonEditColumn VisibleIndex="2">
                            <EditItemTemplate>
                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>"
                                    CausesValidation="false" ClientInstanceName="ASPxButton3">
                                </dx:ASPxButton>
                                <asp:HiddenField ID="hidOneToOneSID" runat="server" Value='<%# Bind("OneToOneSID") %>' />
                            </EditItemTemplate>
                            <DataItemTemplate>
                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>"
                                    CausesValidation="false" ClientInstanceName="ASPxButton3">
                                </dx:ASPxButton>
                                <asp:HiddenField ID="hidOneToOneSID" runat="server" Value='<%# Bind("OneToOneSID") %>' />
                            </DataItemTemplate>
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="3">
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
                            <EditItemTemplate>
                                &nbsp;
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ProductNo" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                            VisibleIndex="4">
                            <PropertiesTextEdit MaxLength="20">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditCellStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <uc1:PopupControl ID="txtProductNo" runat="server" PopupControlName="ProductsPopup"
                                    KeyFieldValue1="ProductNotInSIMGroup" Text='<%#Bind("ProductNo") %>' IsValidation="true"
                                    ValidationGroup='<%# Container.ValidationGroup %>' OnClientTextChanged="getPRODNAME" />
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ProductName" runat="server" ReadOnly="True"
                            Width="200" Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="5">
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditItemTemplate>
                                <dx:ASPxTextBox ID="txtProductName" runat="server" Text='<%# Bind("ProductName") %>'
                                    ClientInstanceName="ProductName" Border-BorderStyle="None" Width="200" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EStoreBookQTY" runat="server" Caption="<%$ Resources:WebResources, EconomicOrderQuantity %>"
                            VisibleIndex="6">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditCellStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtEStoreBookQTY" runat="server" Width="68px" HorizontalAlign="Right"
                                                ClientInstanceName="EStoreBookQTY" Text='<%#BIND("EStoreBookQTY")  %>' Border-BorderStyle="None"
                                                ReadOnly="true">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CurrentInvQTY" runat="server" Caption="<%$ Resources:WebResources, StockQuantity %>"
                            VisibleIndex="7">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditCellStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtCurrentInvQTY" runat="server" Width="68px" HorizontalAlign="Right"
                                                ClientInstanceName="CurrentInvQTY" Text='<%#BIND("[CurrentInvQTY]")  %>' Border-BorderStyle="None"
                                                ReadOnly="true">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PurchQTY" runat="server" Caption="<%$ Resources:WebResources, OnOrderQuantity %>"
                            VisibleIndex="8">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditCellStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtPurchQTY" runat="server" Width="68px" HorizontalAlign="Right"
                                                ClientInstanceName="PurchQTY" Text='<%#BIND("PurchQTY")  %>' Border-BorderStyle="None"
                                                ReadOnly="true">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PreOrderQty" HeaderStyle-HorizontalAlign="Right"
                            VisibleIndex="9" Width="100" Caption="<%$ Resources:WebResources, PreOrderQuantity %>"
                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-ErrorText="必填欄位">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                            <EditCellStyle HorizontalAlign="Left" />
                            <EditItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="txtPreOrderQty" ClientInstanceName="txtPreOrderQty" runat="server"
                                                Width="90px" MaxLength="9" Text='<%#BIND("PreOrderQty") %>' CellStyle-HorizontalAlign="Right"
                                                ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                    <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且必須為正整數，請重新輸入。" />
                                                </ValidationSettings>
                                                <ClientSideEvents Validation="function(s,e){ CheckOrdQty(s, e); }" />
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetailDV" Width="80%" runat="server" ClientInstanceName="gvDetailDV"
                                EnableRowsCache="true" AutoGenerateColumns="False" OnCustomCallback="gvDetailDV_CustomCallback">
                                <Columns>
                                    <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="REAL_QTY" Caption="<%$ Resources:WebResources, OrderQuantity %>">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <div align="left">
                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CollocationProduct %>"></asp:Literal>
                                        </div>
                                    </TitlePanel>
                                </Templates>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                <SettingsDetail IsDetailGrid="true" />
                                <Settings ShowTitlePanel="true" />
                            </cc:ASPxGridView>
                            <asp:Literal ID="lblOneToOneSID" runat="server" Text='<%# Eval("OneToOneSID") %>'
                                Visible="false"></asp:Literal>
                        </DetailRow>
                        <TitlePanel>
                            <table align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            ClientInstanceName="btnNew" OnClick="btnNew_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn" AutoPostBack="false" runat="server"
                                            ClientInstanceName="btnDelete" Text="<%$ Resources:WebResources, Delete %>" OnClick="btnDelete_Click">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <Settings ShowTitlePanel="True" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                    <SettingsDetail ShowDetailRow="true" ShowDetailButtons="False" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
                    <ClientSideEvents EndCallback="InitDisUseCount" />
                </cc:ASPxGridView>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition" id="showBtnFooter" runat="server">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                OnClick="btnSave_Click" ClientInstanceName="btnSave" />
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                OnClick="btnDrop_Click" ClientInstanceName="btnDrop">
                                <ClientSideEvents Click="function(s,e){ e.processOnServer=confirm('您確定要取消嗎？'); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
