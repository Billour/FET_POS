<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ORD06.aspx.cs" Inherits="VSS_ORD_ORD06" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="hdNo" runat="server" class="hdNo" />
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <%--一搭一設定作業--%>
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TwoForOneOfferSetting %>"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <%--主商品編號--%>
                        <td class="tdtxt">
                            <span style="color: Red">*</span><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, MainProductNumber %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Text="10004" IsValidation="true" />
                            <%--<table style="width: 100px">
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ID="ASPxTextBox1"
                                            runat="server" Width="60px" Text="10004">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" SkinID="PopupButton">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <cc:ASPxPopupControl ID="ASPxPopupControl2" SkinID="ProductsPopup" runat="server"
                                EnableViewState="False" PopupElementID="ASPxButton2" TargetElementID="ASPxTextBox1">
                            </cc:ASPxPopupControl>--%>
                        </td>
                        <%--主商品名稱--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, MainProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100px" Text="主商品名稱">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <%--搭配商品編號--%>
                        <td class="tdtxt">
                            <span style="color: Red">*</span><asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, CollocationProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <uc1:PopupControl ID="PopupControl2" runat="server" PopupControlName="ProductsPopup" Text="10004" IsValidation="true" />
                            <%--<table style="width: 100px">
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ValidationSettings-RequiredField-IsRequired="true" ID="ASPxTextBox3"
                                            runat="server" Width="60px" Text="10004">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton4" runat="server" AutoPostBack="false" SkinID="PopupButton">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                PopupElementID="ASPxButton4" TargetElementID="ASPxTextBox3">
                            </cc:ASPxPopupControl>--%>
                        </td>
                        <%--搭配商品名稱--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, WithTheProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="100px" Text="搭配商品名稱">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <%--開始日期--%>
                        <td class="tdtxt">
                            <span style="color: Red">*</span><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxDateEdit ValidationSettings-RequiredField-IsRequired="true" ID="ASPxDateEdit1"
                                runat="server">
                            </dx:ASPxDateEdit>
                        </td>
                        <%--結束日期--%>
                        <td class="tdtxt">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server">
                            </dx:ASPxDateEdit>
                        </td>
                        <%--狀態--%>
                        <td class="tdtxt">
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
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            CausesValidation="false" OnClick="btnSearch_Click"
                                >
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnReset" CausesValidation="false" AutoPostBack="false" UseSubmitBehavior="false"
                                runat="server" Text="<%$ Resources:WebResources, Reset %>">
                                <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
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
                            OnRowInserting="gvMasterDV_RowInserting" EnableCallBacks="False" OnFocusedRowChanged="gvMasterDV_FocusedRowChanged"
                            OnHtmlRowPrepared="gvMasterDV_HtmlRowPrepared" OnHtmlRowCreated="gvMasterDV_HtmlRowCreated"
                            OnInitNewRow="gvMasterDV_InitNewRow" OnStartRowEditing="gvMasterDV_StartRowEditing">
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
                                    <HeaderCaptionTemplate>
                                    </HeaderCaptionTemplate>
                                    <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                    </EditButton>
                                    <UpdateButton Text="<%$ Resources:WebResources, Save %>">
                                    </UpdateButton>
                                    <CancelButton Text="<%$ Resources:WebResources, Cancel %>">
                                    </CancelButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="2">
                                    <EditItemTemplate>
                                        &nbsp;</EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[狀態]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="4" FieldName="主商品編號" Caption="<%$ Resources:WebResources, MainProductNumber %>">
                                    <EditItemTemplate>
                                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Text='<%#Eval("[主商品編號]") %>' />
                                        <%--<table style="width: 100px">
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="60px" Text='<%#Eval("[主商品編號]") %>'
                                                        OnDataBound="ASPxTextBox1_DataBound">
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" SkinID="PopupButton">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <cc:ASPxPopupControl ID="ASPxPopupControl2" SkinID="ProductsPopup" runat="server"
                                            EnableViewState="False" PopupElementID="ASPxButton2" TargetElementID="ASPxTextBox1">
                                        </cc:ASPxPopupControl>--%>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="商品名稱" Caption="<%$ Resources:WebResources, MainProductName %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Bind("[商品名稱]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="搭配商品編號" Caption="<%$ Resources:WebResources, CollocationProductCode %>">
                                    <EditItemTemplate>
                                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Text='<%#Eval("[搭配商品編號]") %>' />
                                        <%--<table style="width: 100px">
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="60px" Text='<%#Eval("[搭配商品編號]") %>'>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ASPxButton4" runat="server" AutoPostBack="false" SkinID="PopupButton">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                                            PopupElementID="ASPxButton4" TargetElementID="ASPxTextBox3">
                                        </cc:ASPxPopupControl>--%>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="7" FieldName="搭配商品名稱" Caption="<%$ Resources:WebResources, WithTheProductName %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[搭配商品名稱]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn VisibleIndex="8" FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn VisibleIndex="9" FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="10" FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text='<%#Eval("[更新人員]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="11" FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[更新日期]") %>'>
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
                            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                            <Settings ShowTitlePanel="True" />
                        </cc:ASPxGridView>
                        <div class="seperate">
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
