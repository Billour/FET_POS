<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="CON06.aspx.cs" Inherits="VSS_CONS_CON06" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function getPRODINFO(s, e)
        {

            this.s = s;
            this.Sender = s;

            if (s.GetText() != '')
                PageMethods.getProductsInfo(Sender.GetText() + ';' + lbStoreNo.GetText() + ';' + lbSuppId.GetText(), getPRODINFO_OnOK);
        }

        function getPRODINFO_OnOK(returnData)
        {
            if (returnData == '')
            {
                alert("商品料號不存在!");
                Sender.Focus();
                Sender.SetValue(null);
                txtPRODNAME.SetText(null);
                txtPRODTYPENO.SetText(null);
                txtPRODTYPENAME.SetText(null);
                txtADVISEQTY.SetText(null);
                txtPRICE.SetText(null);
                txtORDQTY.SetText(null);
                txtAMOUNT.SetText(null);
            }
            else
            {

                //商品名稱∩商品類別編號_商品類別名稱∩建議訂購量∩單價
                var DataArray = returnData.split("∩");
                txtPRODNAME.SetText(DataArray[0]);

                if (DataArray[1] != '')
                {
                    var PRODTYPEArray = DataArray[1].split("_")
                    txtPRODTYPENO.SetText(PRODTYPEArray[0]);
                    txtPRODTYPENAME.SetText(PRODTYPEArray[1]);
                }
                else
                {

                    txtPRODTYPENO.SetText(null);
                    txtPRODTYPENAME.SetText(null);
                }
                txtADVISEQTY.SetText(DataArray[2]);
                txtPRICE.SetText(DataArray[3]);
                txtORDQTY.SetText(null);
                txtAMOUNT.SetText(null);
            }

        }
        function getSUPPINFO(s, e)
        {
            this.s = s;
            this.Sender = s;

            if (s.GetText() != '')
                PageMethods.getSuppInfo(Sender.GetText(), getSUPPINFO_OnOK);
        }

        function getSUPPINFO_OnOK(returnData)
        {

            if (returnData == '')
            {
                alert("廠商編號不存在!");
                Sender.Focus();
                Sender.SetValue(null);
                lbSuppId.SetText(null);
                txtSuppName.SetText(null);
                lblAMOUNT_MAX_tmp.SetText(0);
            }
            else
            {
                //廠商名稱∩總金額底限
                var DataArray = returnData.split("∩");
                lbSuppId.SetText(DataArray[0]);
                txtSuppName.SetText(DataArray[1]);
                lblAMOUNT_MAX_tmp.SetText(DataArray[2]);
            }
            SetAMOUNT();
            ac1.PerformCallback();
        }
        function SetAMOUNT()
        {
            lblAMOUNT_MAX.SetText(lblAMOUNT_MAX_tmp.GetText());
            //lblToAMOUNT.SetText(txtToAMOUNT_tmp.GetText());            
        }
        //實際訂購量輸入檢查
        function checkORDQTY(s, e)
        {
            if (s.GetText() != "")
            {
                var tmpORDQTY = s.GetText();
                if (isNaN(Number(tmpORDQTY)))
                {
                    alert("輸入字串不為數字格式，請重新輸入!");
                    s.SetText(null);
                    txtAMOUNT.SetText(null);
                } else if (Number(tmpORDQTY) <= 0)
                {
                    alert("不允許小於等於0，請重新輸入");
                    s.SetText(null);
                    txtAMOUNT.SetText(null);

                } else if (txtPRODNAME.GetText() == "" || txtPRODNAME.GetText() == null)
                {
                    //txtPRICE.GetText
                    alert("請先選擇商品料號!");
                    s.SetText(null);
                    txtAMOUNT.SetText(null);

                } else
                {
                    if (!isNaN(Number(txtPRICE.GetText())))
                    {
                        var AMOUNT;
                        AMOUNT = Number(tmpORDQTY) * Number(txtPRICE.GetText());
                        txtAMOUNT.SetText(AMOUNT);
                    }
                }


            }



        }
    </script>

    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--寄銷商品訂貨作業-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentOrderPlacement %>"></asp:Literal>        
                    <dx:ASPxTextBox ID="lbSuppId" runat="server" ClientInstanceName="lbSuppId" ClientVisible="false" />
                    <dx:ASPxTextBox ID="lbStoreNo" ClientInstanceName="lbStoreNo" runat="server" ClientVisible="false" />
                    <dx:ASPxTextBox ID="lblAMOUNT_MAX_tmp" ClientInstanceName="lblAMOUNT_MAX_tmp" runat="server"
                        ClientVisible="false" />
                    <dx:ASPxTextBox ID="blAMOUNT_tmp" ClientInstanceName="blAMOUNT_tmp" runat="server"
                        ClientVisible="false" />
                </td>
                <td align="right">
                    <dx:ASPxButton ID="btnQueryEdit" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="False" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e) { document.location='../CON06/CON05.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單編號-->
                                <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderNo %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="lblOrderNo" runat="server">
                                </dx:ASPxLabel>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單日期-->
                                <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderDate %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="lblOrderDate" runat="server">
                                </dx:ASPxLabel>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--狀態-->
                                <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="lblStatus" runat="server">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--廠商編號-->
                                <dx:ASPxLabel ID="Literal16" runat="server" Text="<%$ Resources:WebResources,SupplierNo %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <uc1:PopupControl ID="txtSuppNo" runat="server" PopupControlName="ConsignmentVendorsPopup"
                                    Text="" IsValidation="true" SetClientValidationEvent="getSUPPINFO" KeyFieldValue2="SUPP_NO" />
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--廠商名稱-->
                                <dx:ASPxLabel ID="Literal18" runat="server" Text="<%$ Resources:WebResources,SupplierName %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtSuppName" ClientInstanceName="txtSuppName" runat="server"
                                    Width="250px" Text="" ReadOnly="true">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--更新日期-->
                                <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources,ModifiedDate %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="lblUpdDateTime" runat="server">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--備註-->
                                <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources,Remark %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td colspan="3" class="tdval" nowrap="nowrap">
                                <dx:ASPxTextBox ID="txtMemo" runat="server" Width="99%">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--更新人員-->
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="lblUpdUser" runat="server">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="seperate">
                </div>
                <div>
                    <div id="Div1" class="SubEditBlock">
                        <dx:ASPxCallbackPanel ID="ac1" runat="server" ClientInstanceName="ac1" OnCallback="ac1_Callback">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PRODNO"
                                        Width="100%" OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating"
                                        OnRowValidating="gvMaster_RowValidating" OnStartRowEditing="gvMaster_StartRowEditing"
                                        OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnPageIndexChanged="gvMaster_PageIndexChanged"
                                        OnCancelRowEditing="gvMaster_CancelRowEditing" OnInitNewRow="gvMaster_InitNewRow"
                                        OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" EnableCallBacks="false"
                                        AutoGenerateColumns="False" OnDataBound="gvMaster_DataBound">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderTemplate>
                                                    <div style="text-align: center">
                                                        <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                                    </div>
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button">
                                                <EditButton Visible="True">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ITEMS" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                                                VisibleIndex="2">
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
                                            <dx:GridViewDataTextColumn FieldName="PRODNO" Width="170px" VisibleIndex="3" runat="server">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <EditItemTemplate>
                                                    <uc1:PopupControl ID="txtPRODNO" Width="150px" runat="server" PopupControlName="ProductsPopup3"
                                                        Text='<%#Bind("PRODNO") %>' SetClientValidationEvent="getPRODINFO" IsValidation="true"
                                                        KeyFieldValue1="consignmentsale_suppid" ValidationGroup='<%# Container.ValidationGroup %>' />
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRODNAME" VisibleIndex="4" runat="server" Caption="<%$ Resources:WebResources, ProductName %>">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtPRODNAME" runat="server" Text='<%# Bind("PRODNAME") %>' ClientInstanceName="txtPRODNAME"
                                                        Border-BorderStyle="None" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRODTYPENAME" runat="server" ReadOnly="True"
                                                Caption="<%$ Resources:WebResources, ProductCategory %>" VisibleIndex="5">                 
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtPRODTYPENO" runat="server" Text='<%# Bind("PRODTYPENO") %>'
                                                        ClientVisible="false" ClientInstanceName="txtPRODTYPENO" Border-BorderStyle="None"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                    <dx:ASPxTextBox ID="txtPRODTYPENAME" runat="server" Text='<%# Bind("PRODTYPENAME") %>'
                                                        ClientInstanceName="txtPRODTYPENAME" Border-BorderStyle="None" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ADVISEQTY" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, RecommendedOrderQuantity %>"
                                                VisibleIndex="7">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtADVISEQTY" runat="server" Text='<%# Bind("ADVISEQTY") %>'
                                                        ClientInstanceName="txtADVISEQTY" Border-BorderStyle="None" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ORDQTY" runat="server" VisibleIndex="8">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ActualOrderQuantity %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtORDQTY" runat="server" Text='<%# Bind("ORDQTY") %>' Width="60px"
                                                        ClientInstanceName="txtORDQTY" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                        </ValidationSettings>
                                                        <ClientSideEvents TextChanged="function(s,e){checkORDQTY(s,e);}" />
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRICE" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, UnitPrice %>"
                                                VisibleIndex="9">
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtPRICE" runat="server" Text='<%# Bind("PRICE") %>' ClientInstanceName="txtPRICE"
                                                        Border-BorderStyle="None" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="AMOUNT" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, TotalPrice %>"
                                                VisibleIndex="10">
                                                
                                                <EditItemTemplate>
                                                    <dx:ASPxTextBox ID="txtAMOUNT" runat="server" Text='<%# Bind("AMOUNT") %>' ClientInstanceName="txtAMOUNT"
                                                        Border-BorderStyle="None" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnSaleToOrder" runat="server" Text="<%$ Resources:WebResources, SalesToOrder %>"
                                                                OnClick="btnSaleToOrder_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                OnClick="btnAddNew_Click" />
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                                                OnClick="btnDelete_Click" AutoPostBack="False" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            <FooterRow>
                                                <table align="right" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <!--最低訂單金額-->
                                                            <dx:ASPxLabel ID="Literal20" runat="server" Text="<%$ Resources:WebResources, MinimumOrderAmount %>">
                                                            </dx:ASPxLabel>
                                                            ：<dx:ASPxLabel ID="lblAMOUNT_MAX" ClientInstanceName="lblAMOUNT_MAX" runat="server"
                                                                Text="0">
                                                            </dx:ASPxLabel>
                                                            元
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <!--訂單總價-->
                                                            <dx:ASPxLabel ID="Literal18" runat="server" Text="<%$ Resources:WebResources, OrderAmount %>">
                                                            </dx:ASPxLabel>
                                                            ：<dx:ASPxLabel ID="lblToAMOUNT" ClientInstanceName="lblToAMOUNT" runat="server" Text="0">
                                                            </dx:ASPxLabel>
                                                            元
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterRow>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabe11" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                        </Templates>
                                        <Settings ShowFooter="True" ShowTitlePanel="True" />
                                        <SettingsPager PageSize="10" />
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsBehavior AllowFocusedRow="false" ProcessFocusedRowChangedOnServer="false" />
                                    
                                    </cc:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="btnPosition">
                        <table align="center">
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                        OnClick="btnSave_Click" />
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Discard %>"
                                        OnClick="btnDrop_Click" />
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                        OnClick="btnClear_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
