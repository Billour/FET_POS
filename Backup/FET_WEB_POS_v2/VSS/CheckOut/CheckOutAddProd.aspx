<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutAddProd.aspx.cs" Inherits="VSS_CheckOut_CheckOutAddProd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target ="_self"/>

    <title>加價購資料輸入</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">

        function getADDPRODINFO(s, e) {
            this.Sender = s;
            var fName = "cboPRODNO";
            window.document.getElementById("hidGetProdInfoComplete").value = "false";
            var store_no = window.document.getElementById("hidStore_No").value;
            var empid = window.document.getElementById("hidEmployId").value;
            var hidMappingProdNo = getClientInstance('TxtBox', s.name.replace(fName, "hidMappingProdNo"));
            var hidPOSUUID_DETAIL = getClientInstance('TxtBox', s.name.replace(fName, "hidPOSUUID_DETAIL"));
            
            if (hidMappingProdNo.GetText() != '' && s.GetText() != '')
                PageMethods.getADDPRODINFO(hidMappingProdNo.GetText(), hidPOSUUID_DETAIL.GetText(), s.GetText(), store_no, empid, getADDPRODINFO_OnOK);
        }

        function getADDPRODINFO_OnOK(returnData) {

            var fName = "1_cboPRODNO";
            var txtPRODNAME = getClientInstance('TxtBox', Sender.name.replace(fName, "2_txtPRODNAME"));
            var txtPRODNO = getClientInstance('TxtBox', Sender.name);                  
            var p = returnData.split(';');
            //單價
            var hidItem_Type = getClientInstance('TxtBox', Sender.name.replace(fName, "1_hidItem_Type"));
            var txtUNIT_PRICE = getClientInstance('TxtBox', Sender.name.replace(fName, "3_txtUNIT_PRICE"));
            var txtDISCOUNT_PRICE = getClientInstance('TxtBox', Sender.name.replace(fName, "4_txtDISCOUNT_PRICE"));
            var txtQuantity = getClientInstance('TxtBox', Sender.name.replace(fName, "5_txtQuantity"));
            var txtTotal_Amt = getClientInstance('TxtBox', Sender.name.replace(fName, "6_txtTotal_Amt"));
            var qty = parseInt(Number(txtQuantity.GetText()));
            var Price = 0;
            
            txtPRODNAME.SetText(p[0]);
            txtUNIT_PRICE.SetText(p[1]);
            txtDISCOUNT_PRICE.SetText(p[2]);
            if (p[2] != null && p[2] != "" && !isNaN(p[2])) 
                Price = parseInt(Number(p[2]));
            txtTotal_Amt.SetText((qty * Price).toString());
            hidItem_Type.SetText(p[3]);
            
            window.document.getElementById("hidGetProdInfoComplete").value = "true";
        }
        
        function CheckQuantity(s,e) {
            this.Sender = s;
            var fName = "5_txtQuantity";
            var hidItemQuantity = getClientInstance('TxtBox', s.name.replace(fName, "5_hidItemQuantity"));
            var maxQty = 0;
            var qty = 0;
            
            if (hidItemQuantity != null && hidItemQuantity.GetText() != '' && !isNaN(hidItemQuantity.GetText()))
                maxQty = parseInt(hidItemQuantity.GetText());
            if (s.GetText() != '' && !isNaN(s.GetText()))
                qty = parseInt(Number(s.GetText()));
                
            if (qty > maxQty) {
                alert('加購/領取數量不得超過銷售商品數量!');
                s.SetText("0");
            }
            
            var txtDISCOUNT_PRICE = getClientInstance('TxtBox', s.name.replace(fName, "4_txtDISCOUNT_PRICE"));
            var txtTotal_Amt = getClientInstance('TxtBox', s.name.replace(fName, "6_txtTotal_Amt"));
            if (txtDISCOUNT_PRICE != null && txtDISCOUNT_PRICE.GetText() != '' && !isNaN(txtDISCOUNT_PRICE.GetText()))
                txtTotal_Amt.SetText("" + (parseInt(txtDISCOUNT_PRICE.GetText()) * qty));
        }
        
        function chkGetInfoComplete() {
            if (window.document.getElementById("hidGetProdInfoComplete").value == "false") {
                alert('尚未取得商品完整資訊，請稍候!');
                return false;
            } else 
                return true;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"> </asp:ScriptManager>
    <div class="seperate">
    </div>
    <div class="checkOutDiv">
        <div>
            <asp:Panel ID="scrollPanel" ScrollBars="Auto" runat="server" Enabled="true" Height="550">
                <div class="seperate">
                    <asp:HiddenField ID="hidGetProdInfoComplete" runat="server" />
                    <asp:HiddenField ID="hidStore_No" runat="server" />
                    <asp:HiddenField ID="hidEmployId" runat="server" />
                </div>
                <div>
                    <dx:ASPxGridView ID="gvMaster" runat="server" Width="100%" KeyFieldName="項次" 
                        onhtmlrowcreated="gvMaster_HtmlRowCreated" 
                        onhtmlrowprepared="gvMaster_HtmlRowPrepared">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="項次" Caption="項次" VisibleIndex="0">
                                <DataItemTemplate>
                                    <%#Container.ItemIndex + 1%>
                                </DataItemTemplate>                                    
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="商品料號" CellStyle-HorizontalAlign="Left" VisibleIndex="1">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hidMappingProdNo" runat="server" Text='<%# Eval("MappingProdNo") %>' ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hidPOSUUID_DETAIL" runat="server" Text='<%# Eval("POSUUID_DETAIL") %>' ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hidItem_Type" runat="server" Text='<%# Eval("ITEM_TYPE") %>' ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="hidDiscount_Code" runat="server" Text='<%# Eval("DISCOUNT_CODE") %>' ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxComboBox ID="cboPRODNO" runat="server" ValueType="System.String">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e){ getADDPRODINFO(s, e); }" />
                                    </dx:ASPxComboBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="商品名稱" CellStyle-HorizontalAlign="Left" VisibleIndex="2">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtPRODNAME" runat="server" Text='<%# Bind("PRODNAME") %>' ></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="UNIT_PRICE" Caption="原價" VisibleIndex="3">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtUNIT_PRICE" runat="server" Text='<%# Bind("UNIT_PRICE") %>' ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="DISCOUNT_PRICE" Caption="加購價" VisibleIndex="4">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtDISCOUNT_PRICE" runat="server" Text='<%# Bind("DISCOUNT_PRICE") %>' ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Qty" Caption="數量" VisibleIndex="5">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="hidItemQuantity" runat="server" Text='<%# Eval("ItemQty") %>' ClientVisible="false"></dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtQuantity" runat="server" Text='<%# Bind("Qty") %>' Width="50px" MaxLength="8">
                                        <ValidationSettings>
                                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                                        </ValidationSettings>
                                        <ClientSideEvents TextChanged="function(s,e) { CheckQuantity(s,e); } " />
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Total_Amt" Caption="小計" VisibleIndex="6">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtTotal_Amt" runat="server" Text='<%# Bind("Total_Amt") %>' ReadOnly="true" Border-BorderStyle="None"></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>                                    
                        </Columns>
                        <Templates>
                            <TitlePanel>
                                <asp:Label ID="Label5" runat="server" Text="加價購/贈品"></asp:Label>
                            </TitlePanel>
                        </Templates>
                        <Styles>
                            <TitlePanel Font-Size="Small" HorizontalAlign="Left"></TitlePanel>
                        </Styles>
                        <Settings ShowTitlePanel="true" />
                        <SettingsBehavior AllowSort="false" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    </dx:ASPxGridView>
                </div>
                <div class="btnPosition">
                    <table align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnConfirm" runat="server" Text="<%$ Resources:WebResources, Confirm %>"
                                    UseSubmitBehavior="false" OnClick="btnConfirm_Click">
                                    <ClientSideEvents Click="function(s,e){ConfirmTran(s,e);}" />
                                </dx:ASPxButton>

                                <script type="text/javascript">

                                    function ConfirmTran(s, e) { //交易確認
                                      if (!chkGetInfoComplete()) {
                                        e.processOnServer=false;
                                        return false;
                                      }
                                    }
                                </script>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    UseSubmitBehavior="false" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){window.close();}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>
