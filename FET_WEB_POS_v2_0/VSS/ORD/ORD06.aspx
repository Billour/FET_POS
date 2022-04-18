<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="ORD06.aspx.cs" Inherits="VSS_ORD06_ORD06" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--一搭一設定作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TwoForOneOfferSetting %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--主商品編號-->
                            <asp:Literal ID="Literal1" runat="server" Text="主商品編號"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <table style="width: 200px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="60px" Text="10004">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="選" AutoPostBack="false" SkinID="PopupButton">
                                  <%--          <ClientSideEvents Click="function(s,e){openwindow('ORD01_searchProductNo.aspx',640,300);return false;}" />
                                 --%>       </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="60px" Text="10004">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton3" runat="server" Text="選" AutoPostBack="false" SkinID="PopupButton">
                                 <%--           <ClientSideEvents Click="function(s,e){openwindow('ORD01_searchProductNo.aspx',640,300);return false;}" />
                               --%>         </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <cc:ASPxPopupControl ID="ASPxPopupControl2" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="ASPxButton2" TargetElementID="ASPxTextBox1" LoadingPanelID="lp1">
                            </cc:ASPxPopupControl>
                            <cc:ASPxPopupControl ID="ASPxPopupControl3" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="ASPxButton3" TargetElementID="ASPxTextBox2" LoadingPanelID="lp1">
                            </cc:ASPxPopupControl>
                        </td>
                        <td class="tdtxt">
                            <!--主商品名稱-->
                            <asp:Literal ID="Literal3" runat="server" Text="主商品名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100px" Text="主商品名稱">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--搭配商品編號-->
                            <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, CollocationProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <table style="width: 200px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="60px" Text="10004">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton4" runat="server" Text="選" AutoPostBack="false" SkinID="PopupButton">
                                  <%--          <ClientSideEvents Click="function(s,e){openwindow('ORD01_searchProductNo.aspx',640,300);return false;}" />
                                 --%>       </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="60px" Text="10004">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton5" runat="server" Text="選" AutoPostBack="false" SkinID="PopupButton">
                                       <%--     <ClientSideEvents Click="function(s,e){openwindow('ORD01_searchProductNo.aspx',640,300);return false;}" />
                                     --%>   </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="ASPxButton4" TargetElementID="ASPxTextBox3" LoadingPanelID="lp1">
                            </cc:ASPxPopupControl>
                            <cc:ASPxPopupControl ID="ASPxPopupControl1" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="ASPxButton5" TargetElementID="ASPxTextBox4" LoadingPanelID="lp1">
                            </cc:ASPxPopupControl>
                            <dx:ASPxLoadingPanel ID="lp1" runat="server">
                            </dx:ASPxLoadingPanel>
                        </td>
                        <td class="tdtxt">
                            <!--搭配商品名稱-->
                            <asp:Literal ID="Literal23" runat="server" Text="搭配商品名稱"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="100px" Text="搭配商品名稱">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--搭配日期-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CollocationDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3">
                            <table style="width: 200px">
                                <tr>
                                    <td>
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                        </asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td>
                                        &nbsp;<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>">
                                        </asp:Literal>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </td>
                    <td class="tdtxt">
                        <!--狀 態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="120px">
                            <Items>
                                <dx:ListEditItem Value="0" Text="生效" />
                                <dx:ListEditItem Value="1" Text="過期" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    </tr>
                </table>
            </div>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="<%$ Resources:WebResources, Search %>">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMasterDV" Settings-ShowTitlePanel="true" runat="server" KeyFieldName="項次"
                            ClientInstanceName="gvMasterDV" AutoGenerateColumns="False" Width="100%" SettingsEditing-Mode="Inline"
                            OnPageIndexChanged="gvMasterDV_PageIndexChanged" OnRowUpdating="gvMasterDV_RowUpdating"
                            OnRowInserting="gvMasterDV_RowInserting">
                            <ClientSideEvents RowDblClick="function(s, e) {
gvMasterDV.StartEditRow(e.visibleIndex);
}" />
                            <SettingsEditing Mode="Inline" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvMasterDV.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                                    <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                    </EditButton>
                                    <UpdateButton Text="<%$ Resources:WebResources, Save %>">
                                    </UpdateButton>
                                    <CancelButton Text="<%$ Resources:WebResources, Cancel %>">
                                    </CancelButton>
                                </dx:GridViewCommandColumn>
                                <%--  <dx:GridViewDataTextColumn VisibleIndex="1">
                                <EditItemTemplate>
                                </EditItemTemplate>
                      
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="2">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[項次]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[狀態]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="主商品編號" Caption="<%$ Resources:WebResources, PrimaryProductCode %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[主商品編號]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lbtnProductNo" runat="server" Text='<%# Bind("主商品編號") %>' OnClick="lbtnProductNo_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[商品名稱]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn VisibleIndex="6" FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn VisibleIndex="7" FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[更新日期]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="9" FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text='<%#Eval("[更新人員]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnNew" runat="server" OnClick="btnNew_Click" Text="<%$ Resources:WebResources, Add %>">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabe11" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                        </cc:ASPxGridView>
                        <div class="seperate">
                        </div>
                        <cc:ASPxGridView ID="gvDetailDV" runat="server" Visible="false" KeyFieldName="搭配商品編號"
                            ClientInstanceName="gvDetailDV" AutoGenerateColumns="False" Width="100%" SettingsEditing-Mode="Inline"
                            OnRowUpdating="gvDetailDV_RowUpdating" Settings-ShowTitlePanel="true" OnRowInserting="gvDetailDV_RowInserting">
                            <SettingsEditing Mode="Inline" />
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="0">
                                    <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                    </EditButton>
                                    <UpdateButton Text="<%$ Resources:WebResources, Save %>">
                                    </UpdateButton>
                                    <CancelButton Text="<%$ Resources:WebResources, Cancel %>">
                                    </CancelButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="0" FieldName="項次" Caption="項次">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="搭配商品編號" Caption="<%$ Resources:WebResources, CollocationProductCode %>">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                    OnClick="btnNew3_Click">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel12" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                        </cc:ASPxGridView>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="btnPosition">
                <table align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                OnClick="btnSave_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
