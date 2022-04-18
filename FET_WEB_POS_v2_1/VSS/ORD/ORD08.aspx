<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ORD08.aspx.cs" Inherits="VSS_ORD_ORD08" %>

<%@ Register Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
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
                        <!--Non-DropShipment主配作業-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, NonDropShipmentProductDistribution %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton AutoPostBack="false" ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>">
                            <ClientSideEvents Click="function(s, e) {
                                document.location='ORD07.aspx';
                            }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--主配單號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DistributionNo %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="HO100817002">
                            </asp:Label>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label3" runat="server" Text="00 未存檔"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            &nbsp;
                        </td>
                        <td class="tdval" colspan="3">
                            &nbsp;
                        </td>
                        <td class="tdtxt">
                            <!--更新日期-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label4" runat="server" Text="10/07/12 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                            <!--更新人員-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label5" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="Div1" runat="server" class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" Settings-ShowTitlePanel="true" runat="server" KeyFieldName="商品編號"
                            ClientInstanceName="gvMaster" AutoGenerateColumns="False" Width="100%" SettingsEditing-Mode="Inline"
                            OnPageIndexChanged="gvMaster_PageIndexChanged" OnRowUpdating="gvMaster_RowUpdating"
                            OnRowInserting="gvMaster_RowInserting" EnableCallBacks="False" 
                            onfocusedrowchanged="gvMaster_FocusedRowChanged" 
                            onhtmlrowprepared="gvMaster_HtmlRowPrepared">
                            <SettingsEditing Mode="Inline" />
                            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1" Caption="&nbsp;">
                                    <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                    </EditButton>
                                    <UpdateButton Text="<%$ Resources:WebResources, Save %>">
                                    </UpdateButton>
                                    <CancelButton Text="<%$ Resources:WebResources, Cancel %>">
                                    </CancelButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                                    VisibleIndex="2">
<%--                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lbtnProductNo" runat="server" Text='<%# Eval("[商品編號]") %>' OnClick="lbtnProductNo_Click" />
                                    </DataItemTemplate>
--%>                                    <EditItemTemplate>
                                            <uc2:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Text='<%# Eval("[商品編號]") %>' />
                                       <%-- <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px" Text='<%# Eval("[商品編號]") %>'>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, choose %>" SkinID="PopupButton">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <cc:ASPxPopupControl ID="ASPxPopupControl2" SkinID="ProductsPopup" runat="server"
                                            EnableViewState="False" PopupElementID="ASPxButton2" TargetElementID="ASPxTextBox1"
                                            LoadingPanelID="lp1">
                                        </cc:ASPxPopupControl>
                                        <dx:ASPxLoadingPanel ID="lp1" runat="server">
                                        </dx:ASPxLoadingPanel>--%>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" ReadOnly="true" FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="5" ReadOnly="true" FieldName="ATR量" Caption="<%$ Resources:WebResources, AtrQuantity %>">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="6" Caption="<%$ Resources:WebResources, AutomaticallyAssigned %>">
                                    <DataItemTemplate>
                                        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Checked="true" Enabled="false">
                                        </dx:ASPxCheckBox>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Checked="true">
                                        </dx:ASPxCheckBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="7" ReadOnly="true" FieldName="主配量" Caption="<%$ Resources:WebResources, DistributionQuantity %>">
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="8" FieldName="備註" Caption="<%$ Resources:WebResources, Remark %>">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>                                
                                <TitlePanel>
                                    <table align="left">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="Button1" ClientInstanceName="Button1" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                    OnClick="Button1_Click">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton5" runat="server" Text="<%$ Resources:WebResources, Import %>">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <uc1:PopupWindow ID="PopupWindow1" runat="server" Name="Import" PopupButtonID="ASPxButton5"
                                        TargetControlID="HiddenField1" Width="500" Height="500" NavigateUrl="~/VSS/ORD/ORD10_Import.aspx" />
                                </TitlePanel>
                            </Templates>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />                            
                            <Settings ShowTitlePanel="True" />
                        </cc:ASPxGridView>
                        <div class="seperate">
                        </div>
                        <div id="showDetail" runat="server">
                            <cc:ASPxGridView ID="gvDetail" KeyFieldName="出貨倉別" Width="100%" runat="server" ClientInstanceName="gvDetail"
                                SettingsEditing-Mode="Inline" Settings-ShowTitlePanel="true" OnRowInserting="gvDetail_RowInserting"
                                OnRowUpdating="gvDetail_RowUpdating1">
                                <SettingsPager PageSize="5">
                                </SettingsPager>
                                <SettingsEditing Mode="Inline" />
                                <Columns>
                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                        <HeaderTemplate>
                                            <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1" Caption="&nbsp;">
                                        <EditButton Visible="true" Text="<%$ Resources:WebResources, Edit %>">
                                        </EditButton>
                                        <UpdateButton Text="<%$ Resources:WebResources, Save %>">
                                        </UpdateButton>
                                        <CancelButton Text="<%$ Resources:WebResources, Cancel %>">
                                        </CancelButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="2" ReadOnly="true" FieldName="出貨倉別" Caption="<%$ Resources:WebResources, ShipmentWarehouse %>">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>">
                                        <EditItemTemplate>
                                            <uc2:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup"  Text='<%# BIND("[門市編號]") %>' />
                                            <%--<table align="center">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxTextBox ID="ASPxTextBox11" runat="server" Width="170px" Text='<%# BIND("[門市編號]") %>'>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton22" runat="server" Text="<%$ Resources:WebResources, choose %>" SkinID="PopupButton">
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <cc:ASPxPopupControl ID="ASPxPopupControl2" SkinID="StoresPopup" runat="server" EnableViewState="False"
                                                PopupElementID="ASPxButton22" TargetElementID="ASPxTextBox11" LoadingPanelID="lp1">
                                            </cc:ASPxPopupControl>
                                            <dx:ASPxLoadingPanel ID="lp1" runat="server">
                                            </dx:ASPxLoadingPanel>--%>
                                        </EditItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="4" ReadOnly="true" FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>">
                                        <PropertiesTextEdit>
                                            <ReadOnlyStyle>
                                                <Border BorderStyle="None" />
                                            </ReadOnlyStyle>
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="主動配貨量" Caption="<%$ Resources:WebResources, DistributionQuantity %>">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>                                    
                                    <TitlePanel>
                                        <table align="left">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="Button9" Enabled="true" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                        OnClick="Button9_Click">
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="DropDownList3" Width="80px" runat="server" SelectedIndex="0"
                                                        ValueType="System.String">
                                                        <Items>
                                                            <dx:ListEditItem Selected="True" Text="ALL" />
                                                            <dx:ListEditItem Selected="True" Text="北區" />
                                                            <dx:ListEditItem Selected="True" Text="中區" />
                                                            <dx:ListEditItem Selected="True" Text="南區" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="Button19" runat="server" Text="<%$ Resources:WebResources, Confirm %>">
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="Button17" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                </Templates>
                                <Settings ShowTitlePanel="True" />
                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            </cc:ASPxGridView>
                        </div>
                    </div>
                    <div class="seperate">
                    </div>
                    <div class="btnPosition" id="divShow" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Save %>" >
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button13" runat="server" Text="<%$ Resources:WebResources, Export %>" >
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button14" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>" >
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="Button15" runat="server" Text="<%$ Resources:WebResources, Delete %>" >
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="seperate">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
