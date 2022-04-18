<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD04.aspx.cs" Inherits="VSS_ORD_ORD04_ORD04" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    <script>
        function chkDate1(s, e) {
            var x = txtSDate.GetValue();
            var y = txtEDate.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if ((x != "" && y != "")) {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("訂單日期 訖 不可早於 訂單日期 起!!");
                    s.SetValue(null);
                    return false;
                }
            }
           
            else {
                return true;
            }
        }


        function chkORDER1(s, e) {
            var x = ddlPRODTYPENO_S.GetText();
            var y = ddlPRODTYPENO_E.GetText();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if ((x != "" && y != "")) {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("訖 訂單編號不可早於 起 訂單編號!!");
                    s.SetValue(null);
                    return false;
                }
            }
            else {
                return true;
            }
        }
    
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <!--調整訂單作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderAdjustment %>"></asp:Literal>
    </div> 
       
    <div>
        <div class="criteria">
            <table>
                   <tr>
                        <td class="tdtxt">
                            <!--區域別-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ddlArea" runat="server" Width="120px">
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            <!--訂單編號-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="2">
                            <table style="width: 250px">
                                <td>
                                    <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtOrdNoStart" runat="server" Width="100px" ClientInstanceName="ddlPRODTYPENO_S">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkORDER1(s, e); }" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtOrdNoEnd" runat="server" Width="100px" ClientInstanceName="ddlPRODTYPENO_E">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkORDER1(s, e); }" />
                                    </dx:ASPxTextBox>
                                </td>
                            </table>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtbox1" runat="server" Text="" >
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="輸入值需為正整數，請重新輸入" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--訂單日期-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <table style="width: 250px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <div style="width: 120px;">
                                            <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server" EditFormatString="yyyy/MM/dd"
                                                ClientInstanceName="txtSDate">
                                                <ValidationSettings>                                                
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                                <ClientSideEvents ValueChanged="function(s, e){ chkDate1(s, e); }" />
                                            </dx:ASPxDateEdit>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server" EditFormatString="yyyy/MM/dd"
                                            ClientInstanceName="txtEDate">
                                            <ValidationSettings>                                                
                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                            </ValidationSettings>
                                            <ClientSideEvents ValueChanged="function(s, e){ chkDate1(s, e); }" />
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdtxt">
                            <!-- 更新日期 -->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Literal ID="Literal6" runat="server" Text=""></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Text="" >
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--訂單狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                                <Items>
                                    <dx:ListEditItem Text="ALL" Value="" />
                                    <dx:ListEditItem Text="正式" Value="10" />
                                    <dx:ListEditItem Text="已傳輸" Value="50" Selected="true" />
                                    <dx:ListEditItem Text="已成單" Value="51" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                            <!-- 更新人員 -->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Literal ID="Literal8" runat="server"></asp:Literal>
                        </td>
                    </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <table align="center">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnReBindData" runat="server" Text="test" OnClick="btnReBindData_Click" Visible="false"
                        ClientInstanceName="btnReBindData">
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="<%$ Resources:WebResources, Search %>">
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"/>
                </td>
            </tr>
        </table>
        <div class="seperate"></div>
        
        <cc:ASPxGridView ID="gvDetailDV" ClientInstanceName="gvDetailDV" runat="server" Visible="false" KeyFieldName="PRODNO_M"
            AutoGenerateColumns="False" Width="100%" EnableCallBacks="false"
            OnFocusedRowChanged="gvDetailDV_FocusedRowChanged" 
            onpageindexchanged="gvDetailDV_PageIndexChanged">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="PRODNO_M" Caption="<%$ Resources:WebResources, ProductCode %>"
                    CellStyle-HorizontalAlign="Right">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PRODNAME_M" Caption="<%$ Resources:WebResources, ProductName %>"
                    CellStyle-HorizontalAlign="Left">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, WithTheProductCode %>"
                    CellStyle-HorizontalAlign="Right">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, WithTheProductName %>"
                    CellStyle-HorizontalAlign="Left">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="STOCK_QTY" Caption="<%$ Resources:WebResources, AllocatableQuantity %>"
                    CellStyle-HorizontalAlign="Right">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ORDQTY" Caption="<%$ Resources:WebResources, OrderTotal %>"
                    CellStyle-HorizontalAlign="Right">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="HQ_QTY" Caption="<%$ Resources:WebResources, TotalAdjustments %>"
                    CellStyle-HorizontalAlign="Right">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
            <SettingsEditing Mode="Inline" />
        </cc:ASPxGridView>
        
        <div class="seperate"></div>
        
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvMasterDV" runat="server" ClientInstanceName="gvMasterDV" AutoGenerateColumns="False"
                Width="100%" KeyFieldName="ORDER_ITEMS_ID" EnableCallBacks="false"
                OnRowUpdating="gvMasterDV_RowUpdating"
                OnCommandButtonInitialize="gvMaster_CommandButtonInitialize" 
                OnPageIndexChanged="gvMasterDV_PageIndexChanged"
                OnHtmlRowCreated="gvMasterDV_HtmlRowCreated"
                OnHtmlRowPrepared="gvMasterDV_HtmlRowPrepared" 
                OnRowValidating="gvMasterDV_RowValidating"
                oncancelrowediting="gvMasterDV_CancelRowEditing"
                OnFocusedRowChanged="gvMasterDV_FocusedRowChanged" >
                <Columns>
                    <dx:GridViewCommandColumn ButtonType="Button" Caption=" ">
                        <EditButton Visible="true"></EditButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="ORDDATE" Caption="<%$ Resources:WebResources, OrderDate %>">
                        <EditItemTemplate>
                            <dx:ASPxLabel ID="ORDDATE" runat="server" Text='<%#BIND("ORDDATE") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ORDER_NO" Caption="<%$ Resources:WebResources, OrderNo %>">
                        <EditItemTemplate>
                            <dx:ASPxLabel ID="ORDER_NO" runat="server" Text='<%#BIND("ORDER_NO") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, OrderStatus %>">
                        <DataItemTemplate>
                            <dx:ASPxLabel ID="STATUS" runat="server" Text='<%#BIND("STATUS") %>'></dx:ASPxLabel>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dx:ASPxLabel ID="STATUS" runat="server" Text='<%#BIND("STATUS") %>'></dx:ASPxLabel>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Left"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>">
                        <EditItemTemplate>
                            <dx:ASPxLabel ID="STORE_NO" runat="server" Text='<%#BIND("STORE_NO") %>'>
                            </dx:ASPxLabel>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                        <EditItemTemplate>
                            <dx:ASPxLabel ID="STORENAME" runat="server" Text='<%#BIND("STORENAME") %>'>
                            </dx:ASPxLabel>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Left"> </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ORDQTY" Caption="<%$ Resources:WebResources, OrderQuantity %>">
                        <EditItemTemplate>
                            <dx:ASPxLabel ID="ORDQTY" runat="server" Text='<%#BIND("ORDQTY") %>'>
                            </dx:ASPxLabel>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="STOCK_QTY" Caption="<%$ Resources:WebResources, StockQuantity %>">
                        <EditItemTemplate>
                            <dx:ASPxLabel ID="STOCK_QTY" runat="server" Text='<%#BIND("STOCK_QTY") %>'>
                            </dx:ASPxLabel>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="HQ_ADJ_ORDER_QTY" Caption="<%$ Resources:WebResources, AssistantAdjustment %>">
                        <CellStyle HorizontalAlign="Right"></CellStyle>
                        <EditItemTemplate>
                            <dx:ASPxTextBox ID="txtDiscountQuota" runat="server" Width="100%" Text='<%# Bind("HQ_ADJ_ORDER_QTY") %>'
                                HorizontalAlign="Right" MaxLength="7" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="\d*" ErrorText="輸入值需為正整數，請重新輸入" />
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="REMARK" Caption="<%$ Resources:WebResources, Remark %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ORDER_ID" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PRODNO" Visible="false"></dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager PageSize="5"></SettingsPager>
                <SettingsEditing Mode="Inline"></SettingsEditing>
                <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <%--<ClientSideEvents EndCallback="function(s, e) { btnReBindData.DoClick(); }" />--%>
           </cc:ASPxGridView>
        </div>            

    </div>
</asp:Content>
