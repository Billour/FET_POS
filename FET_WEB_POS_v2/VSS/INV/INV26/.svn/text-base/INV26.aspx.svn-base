<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV26.aspx.cs" Inherits="VSS_INV_INV26" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
     <script type="text/javascript">
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
        
            fName = "3_txtTraninQty";
            var lblDiffStkQty = getClientInstance('TxtBox', s.name.replace(fName, "3_lblTranOutQty")); //移出數量
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
<div id="tooltip"></div>
    <div class="titlef">
        <!--撥入作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferInSearch %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <asp:Literal ID="lblStno" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtStno" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--商品料號-->
                        <asp:Literal ID="lblProductCode" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup" OnClientTextChanged="function(s,e) { getProductInfo(s,e); }"  />
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <asp:Literal ID="lblProductName" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtProductName" ClientInstanceName="txtProductName" runat="server"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--撥入日期-->
                        <asp:Literal ID="lblTSTDate" runat="server" Text="<%$ Resources:WebResources, TransferInDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="lblTSTDate_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td align="left">
                                    <div style="width:120px;">
                                        <dx:ASPxDateEdit ID="txtTSTDate_S" runat="server" ClientInstanceName="txtSDate">
                                            <ClientSideEvents  ValueChanged="function(s, e){ chkDate(s, e); }"  />                                               
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Literal ID="lblTSTDate_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td align="left">
                                    <div style="width:120px;">
                                        <dx:ASPxDateEdit ID="txtTSTDate_E" runat="server" ClientInstanceName="txtEDate">
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />                                               
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--移出門市名稱-->
                        <asp:Literal ID="lblFromStoreNo" runat="server" Text="<%$ Resources:WebResources, TransferFrom %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <div class="criteria">
                           <uc1:PopupControl ID="txtFromStoreNo" runat="server" PopupControlName="StoresPopup" OnClientTextChanged="function(s,e) { getStoreInfo(s,e); }" />
                        </div>
                           <dx:ASPxTextBox ID="txtFromStoreName" ClientInstanceName="txtStoreName" runat="server" ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                        
                    </td>
                    <%--<td class="tdtxt">
                        <!--撥入門市名稱-->
                        <asp:Literal ID="lblToStoreNO" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                           <table cellpadding="0" cellspacing="0" border="0" align="left">
                            <tr>
                                <td><uc1:PopupControl ID="txtToStoreNO" runat="server" PopupControlName="StoresPopup" Width="70px" OnClientTextChanged="function(s,e) { getStoreInfo(s,e); }" /></td>
                                <td><dx:ASPxTextBox ID="txtToStoreName" ClientInstanceName="txtStoreName" runat="server" Width="70px"  ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox></td>
                            </tr>
                        </table>
                    </td>--%>
                    <td class="tdtxt">
                        <!--移撥狀態-->
                        <asp:Literal ID="lblTStatus" runat="server" Text="<%$ Resources:WebResources, TransferStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlTStatus" runat="server" Width="100">
                            <Items>
                                <dx:ListEditItem Value="20" Text="在途" Selected="true" />
                                <dx:ListEditItem Value="30" Text="已撥入" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td align="right">
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" UseSubmitBehavior="false" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
                    Width="100%" KeyFieldName="STNO" 
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated" 
                    onhtmlrowprepared="gvMaster_HtmlRowPrepared" 
                    onpageindexchanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataColumn FieldName="STNO" Caption="<%$ Resources:WebResources, TransferSlipNo %>"></dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, TransferFrom %>"/>
                        <dx:GridViewDataColumn FieldName="STDATE" Caption="<%$ Resources:WebResources, TransferOutDate %>" />
                        <dx:GridViewDataColumn FieldName="TSTDATE" Caption="<%$ Resources:WebResources, TransferInDate %>"/>
                        <dx:GridViewDataColumn FieldName="TSTATUS" Caption="<%$ Resources:WebResources, TransferStatus %>">
                           <DataItemTemplate>
                               <dx:ASPxLabel ID="lblStatus" runat="server" Text='<%# Bind("TSTATUS") %>'></dx:ASPxLabel>
                           </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                        <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="detailGrid" Width="100%"
                                KeyFieldName="STORETRANSFER_D_ID" 
                                OnHtmlRowCreated="gvDetail_HtmlRowCreated"
                                OnPageIndexChanged="gvDetail_PageIndexChanged" >
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                                        <EditItemTemplate>
                                            <dx:ASPxTextBox ID="STNO1" runat="server" Width="68px" HorizontalAlign="Right" Text='<%#BIND("STNO1")  %>' 
                                                    Border-BorderStyle="None" ReadOnly="true" Visible ="false"  >
                                            </dx:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"/>
                                    <dx:GridViewDataColumn FieldName="TRANOUTQTY" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="txtTRANOUQTY" runat="server" Text = '<%# Eval("TRANOUTQTY") %>' ReadOnly="true" Border-BorderStyle="None" HorizontalAlign="Right"></dx:ASPxTextBox> 
                                        </DataItemTemplate>
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
                                    <dx:GridViewDataColumn Caption="">
                                        <DataItemTemplate>
                                            <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl=""></dx:ASPxImage>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>">
                                        <DataItemTemplate>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="right">
                                                       <dx:ASPxTextBox ID="lblIMEI_QTY" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_QTY") %>'
                                                                Border-BorderStyle="None" Width="20px" DisabledStyle-Font-Underline="true"></dx:ASPxTextBox>
                                                    </td>
                                                    <td align="right"></td>
                                                    <div id="divIMEI" runat="server">
                                                        <td>
                                                            <uc1:PopupControl ID="txtIMEI" runat="server" PopupControlName="InputIMEIData"  Text='<% #Bind("IMEI") %>'
                                                                AssignToControlId="lblIMEI_QTY" />
                                                        </td>
                                                    </div>
                                                </tr>
                                            </table>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Settings ShowFooter="false" />
                                <SettingsDetail IsDetailGrid="true" />
                                <SettingsPager PageSize="5"></SettingsPager>
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                            <div class="seperate"></div>
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
                                            <dx:ASPxButton ID="btnCancel" SkinID="ResetButton" runat="server" Text="<%$ Resources:WebResources, Cancel %>"  />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </DetailRow>
                    </Templates>
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <SettingsPager PageSize="10"></SettingsPager>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <SettingsBehavior AllowFocusedRow="true" />
                </cc:ASPxGridView>
           </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
