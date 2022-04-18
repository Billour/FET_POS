<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="SAL15_2.aspx.cs" Inherits="VSS_SAL_SAL015_2" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <base target="_self"></base>
    
    <title>HappyGo���I���ө�§</title>
    
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckHappyGo() {

        }
        function checkQty(s, e) {
            var fName = "7_txtQuantity";
            var lblUserCount = getClientInstance('Label', s.name.replace(fName, "6_lblUserCount"));


            var chkQty = s.GetValue();
            var iStkchkQty = 0;
            if (chkQty == null || chkQty == "") {
                iStkchkQty = 0;
                e.isValid = false;
                e.errorText = '�п�J�ƶq';
                return false;
            }
            ichkQty = Number(chkQty);
            if (isNaN(ichkQty)) {
                e.isValid = false;
                e.errorText = '��J�r�ꤣ�ŦX�Ʀr�榡�A�Э��s��J';
                //alert('��J�r�ꤣ�ŦX�Ʀr�榡�A�Э��s��J');
                return false;
            }
            else if (ichkQty < 0) {
                e.isValid = false;
                e.errorText = '�ƶq�����\�p��0�A�Э��s��J';
                //alert('�����L�I�q�����\�p��0�A�Э��s��J');
                return false;
            }

            var ilblUserCount = lblUserCount.innerText;
            var Diff = Number(ilblUserCount) - Number(chkQty);
            if (Diff < 0) {
                e.isValid = false;
                e.errorText = '��J�ƶq���o�j��I������';
                return false;
            }
            
            e.isValid = true;
            return true;
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
     <div>
        <div class="titlef">
            <!--HG�I�ƧI��-�ө�§-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoPointsExchangeStoreGift2 %>"></asp:Literal>
        </div>
        <div class="seperate">
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="criteria">
                        <table id="tableIndex">
                            <tr>
                                <!--����Ǹ�-->
                                <td align="tdtxt">
                                    <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="lblTradeNo" runat="server" />
                                </td>
                                <!--���A-->
                                <td align="tdtxt">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Status %>" />
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="lblStatus" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <!--������-->
                                <td align="tdtxt">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>" />
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="lblTradeDate" runat="server" />
                                </td>
                                <!--��s���-->
                                <td align="tdtxt">
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>" />
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="lblModiDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <!--HG�d��-->
                                <td align="tdtxt">
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, HappyGoCardNo %>" />
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="lblHGNo" runat="server" />
                                </td>
                                <!--��s�H��-->
                                <td align="tdtxt">
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>" />
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="lblModiUser" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                
                                <!--�����G-->
                                <td align="tdtxt">
                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="<%$ Resources:WebResources, MobileNumber %>" />
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtmsisdn" runat="server" Width="100px" MaxLength="10">
                                        <ValidationSettings SetFocusOnError="true">
                                            <RegularExpression ValidationExpression="^\d{10}$" ErrorText="�榡���~" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <!--�Ƶ�-->
                                <td align="tdtxt">
                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="<%$ Resources:WebResources, Remark %>" />
                                </td>
                                <td colspan="3">
                                    <dx:ASPxTextBox ID="txtRemark" runat="server" Width="200px" MaxLength="25" />
                                </td>
                               
                            </tr>
                        </table>
                    </div>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ACTIVITY_ID"
                            Width="500px"  EnableCallBacks="false" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                            OnPageIndexChanged="gvMaster_PageIndexChanged" OnRowValidating="gvMaster_RowValidating"
                            OnRowUpdating="gvMaster_RowUpdating" AutoGenerateColumns="False">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                        </div>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn runat="server" Caption="<%$ Resources:WebResources, Items %>"
                                    ReadOnly="true" VisibleIndex="2" CellStyle-HorizontalAlign="Center" Width="30">
                                    <DataItemTemplate>
                                        <%#Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ACTIVITY_NAME" Caption="<%$ Resources:WebResources, ActivityName %>"
                                    VisibleIndex="3">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="txtActivityNo" Text='<%# Bind("ACTIVITY_NO") %>' runat="server"
                                            ClientVisible="false">
                                        </dx:ASPxTextBox>
                                        <dx:ASPxTextBox ID="txtTYPE" Text='<%# Bind("TYPE") %>' runat="server" ClientVisible="false">
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel ID="lblDiscountName" Text='<%# Bind("ACTIVITY_NAME") %>' runat="server">
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataComboBoxColumn FieldName="TYPE" Caption="<%$ Resources:WebResources, Category %>">
                                    <PropertiesComboBox ValueType="System.String">
                                    </PropertiesComboBox>
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lblTYPE" Text='<%# Bind("TYPE") %>' runat="server">
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                   
                                </dx:GridViewDataComboBoxColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                    VisibleIndex="4">
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lblProdName" Text='<%# Bind("PRODNAME") %>' runat="server">
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DIVIDABLE_POINT" Caption="<%$ Resources:WebResources, Points %>"
                                    VisibleIndex="5">
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lblDividablePoint" Text='<%# Bind("DIVIDABLE_POINT") %>' runat="server">
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                    
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="MEMBER_CHECK_FLAG" Caption="<%$ Resources:WebResources, NameListVerification %>"
                                    VisibleIndex="6">
                                    <DataItemTemplate>
                                        <dx:ASPxCheckBox ID="cbMEMBER_CHECK_FLAG" runat="server" ReadOnly="true">
                                        </dx:ASPxCheckBox>
                                    </DataItemTemplate>
                                    
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn FieldName="USE_COUNT" Caption="�I������" VisibleIndex="7">
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lblUserCount" Text='<%# Bind("USE_COUNT") %>' runat="server">
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn Caption="<%$ Resources:WebResources, Quantity %>" VisibleIndex="8">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="txtQuantity" Text='<%# Bind("QTY") %>' runat="server" HorizontalAlign="Right" Width="70px">
                                            <ClientSideEvents Validation="function(s,e){ checkQty(s, e);  }" />
                                        </dx:ASPxTextBox>
                                    </DataItemTemplate>
                                    
                                </dx:GridViewDataCheckColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <dx:ASPxButton SkinID="DeleteBtn" ID="btnDeleteRow" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                        OnClick="btnDeleteRow_Click" CausesValidation="false" ClientInstanceName="btnDeleteRow" />
                                </TitlePanel>
                            </Templates>
                            <SettingsBehavior AllowFocusedRow="false" />
                            <SettingsPager PageSize="5" />
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <Settings ShowTitlePanel="True" />
                            <SettingsEditing Mode='Inline' />
                        </cc:ASPxGridView>
                    </div>
                    <div class="seperate">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                            OnClick="btnSave_Click" CausesValidation="true" Width="25px">
                            <ClientSideEvents Click="function(s,e){ CheckHappyGo();}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                          ClientSideEvents-Click="function(s,e){ window.close();}">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <dx:ASPxTextBox ID="txtHGCardCount" runat="server" Width="170px" ClientInstanceName="txtHGCardCount"
        ClientVisible="false" />
</asp:Content>
