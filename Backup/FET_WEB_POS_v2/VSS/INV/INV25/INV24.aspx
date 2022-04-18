<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV24.aspx.cs" Inherits="VSS_INV_INV24" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript">


        function getProductInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getProductInfo(s.GetText(), getProductInfo_OnOK);
        }

        function getProductInfo_OnOK(returnData) {
            if (returnData != '') {
                txtProductName.SetText(returnData);
            }
            else {
                txtProductName.SetText(null);
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip"></div>

    <div class="titlef">
        <!--移出查詢作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockTransferOutSearch %>"></asp:Literal>
    </div>
    
    <div class="seperate"></div>
    
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
                        <uc1:PopupControl ID="txtProdNo" runat="server" PopupControlName="ProductsPopup" OnClientTextChanged="function(s,e) { getProductInfo(s,e); }"/>
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
                        <!--移出日期-->
                        <span style="color: Red">*</span>
                        <asp:Literal ID="lblSTDate" runat="server" Text="<%$ Resources:WebResources, TransferOutDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="lblSTDate_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSTDate_S" runat="server" ClientInstanceName="txtSDate">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                        </ValidationSettings>
                                        <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <asp:Literal ID="lblSTDate_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSTDate_E" runat="server" ClientInstanceName="txtEDate">
                                       <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市名稱-->
                        <asp:Literal ID="lblToStoreNO" runat="server" Text="撥入門市名稱"></asp:Literal>：
                    </td>
                    <td class="tdval" align="left">
                        <uc1:PopupControl ID="txtToStoreName" KeyFieldValue1="name" KeyFieldValue2="STORENAME" runat="server" PopupControlName="StoresPopup"  />
                        <%--<dx:ASPxTextBox ID="txtToStoreName" ClientInstanceName="txtStoreName" runat="server" ></dx:ASPxTextBox>--%>
                    </td>
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
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
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
        
        <cc:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
            KeyFieldName="STNO" OnHtmlRowCreated="gvMaster_HtmlRowCreated" ClientInstanceName="gvMaster"
            onhtmlrowprepared="gvMaster_HtmlRowPrepared" 
            onpageindexchanged="gvMaster_PageIndexChanged" >
            <Columns>
                <dx:GridViewDataColumn FieldName="STNO" Caption="<%$ Resources:WebResources, TransferSlipNo %>"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, TransferTo %>"/>
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
                    <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Width="100%"
                        KeyFieldName="STORETRANSFER_D_ID" 
                        OnHtmlRowCreated="gvDetail_HtmlRowCreated"
                        OnPageIndexChanged="gvDetail_PageIndexChanged" >
                        <Columns>
                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"/>
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"/>
                            <dx:GridViewDataColumn FieldName="TRANOUTQTY" Caption="<%$ Resources:WebResources, TransferredOutQuantity %>" />
                            <dx:GridViewDataColumn Caption=" ">
                                <DataItemTemplate>
                                    <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                                    </dx:ASPxImage>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="IMEI" Caption="<%$ Resources:WebResources, Imei %>">
                                <DataItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="lblIMEI_QTY" runat="server" ReadOnly="true" Text='<% #Bind("IMEI_QTY") %>'
                                                                Border-BorderStyle="None" Width="20px" DisabledStyle-Font-Underline="true"></dx:ASPxTextBox>
                                            </td> 
                                            <div id="divIMEI" runat="server">
                                                <td>
                                                    <uc1:PopupControl ID="txtIMEI" runat="server" PopupControlName="InputIMEIData" Text='<% #Bind("IMEI") %>'
                                                         AssignToControlId="lblIMEI_QTY"/>
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
                </DetailRow>
            </Templates>
            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
            <SettingsPager PageSize="10"></SettingsPager>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>       
    </div>

</asp:Content>
