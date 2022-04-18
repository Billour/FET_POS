<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OPT15.aspx.cs" Inherits="VSS_OPT_OPT15" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--HG點數兌換-來店禮-->
                        <asp:Literal ID="Literal1" runat="server" Text="HappyGo點數兌換-來店禮"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--開始日期-->
                        <asp:Literal ID="Literal2" runat="server" Text="開始日期"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--活動代號-->
                        <asp:Literal ID="Literal5" runat="server" Text="活動代號"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox3" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox4" runat="server">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--活動名稱-->
                        <asp:Literal ID="Literal8" runat="server" Text="活動名稱"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="活動代號" Settings-ShowTitlePanel="true"
                            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                            OnPageIndexChanged="gvMaster_PageIndexChanged" AutoGenerateColumns="False"
                            OnRowInserting="gvMaster_RowInserting" onrowupdating="gvMaster_RowUpdating">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Button">
                                    <EditButton Visible="true">
                                    </EditButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="活動代號" Caption="<%$ Resources:WebResources, ActivityNo %>" HeaderStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[活動代號]") %>'>
                                        </dx:ASPxLabel>
                                    </EditItemTemplate>
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lbtnActivityNo" runat="server" Text='<%# Bind("活動代號") %>' OnClick="lbtnActivityNo_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="活動名稱" Caption="<%$ Resources:WebResources, ActivityName %>" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="類別" Caption="類別" HeaderStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <dx:ASPxComboBox ID="ddlCategory" runat="server" Width="60">
                                            <Items>
                                                <dx:ListEditItem Text="點數" />
                                                <dx:ListEditItem Text="商品" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTextBox ID="txtProductCode" runat="server" Width="68px" Text='<%#Eval("[商品料號]") %>'>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ChooseProductCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                        ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="點數" Caption="點數" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="名單檢核" Caption="名單檢核" HeaderStyle-HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <dx:ASPxCheckBox ID="CheckItem" runat="server" ReadOnly="true" />
                                    </DataItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn FieldName="折抵次數" Caption="折抵次數" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <DetailRow>
                                </DetailRow>
                                <TitlePanel>
                                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                    OnClick="btnAdd_Click" />
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                </EmptyDataRow>
                            </Templates>
                            <SettingsBehavior AllowFocusedRow="true" />
                            <SettingsEditing Mode="Inline" />
                            <SettingsPager PageSize="3"></SettingsPager>
                        </cc:ASPxGridView>
                    </div>

                    <div class="seperate"></div>

                    <div id="Div_Dt">
                        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%" Visible="false">
                            <TabPages>
                                <dx:TabPage Text="通路設定">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <div  >
                                            <cc:ASPxGridView ID="gvDetail" runat="server" ClientInstanceName="gvDetail" Settings-ShowTitlePanel="true"
                                                Width="100%" OnPageIndexChanged="detailGrid_PageIndexChanged" KeyFieldName="項次" 
                                                AutoGenerateColumns="False" OnRowInserting="gvDetail_RowInserting" onrowupdating="gvDetail_RowUpdating">
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <input type="checkbox" onclick="gvDetail.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Button">
                                                        <EditButton Visible="true"></EditButton>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                                        HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                                        <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="門市編號" Caption="門市編號" HeaderStyle-HorizontalAlign="Center">
                                                        <EditItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtStoreCode" runat="server" Width="68px" Text='<%#Eval("[門市編號]") %>'>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="ChooseStoreCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                            ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_chooseStore.aspx',500,400);return false;}">
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="門市名稱" HeaderStyle-HorizontalAlign="Center"
                                                        ReadOnly="true">
                                                        <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="區域別" Caption="區域別" HeaderStyle-HorizontalAlign="Center"
                                                        ReadOnly="true">
                                                        <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsEditing Mode="Inline" />
                                                <SettingsPager PageSize="5"></SettingsPager>
                                                <Templates>
                                                    <TitlePanel>
                                                        <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxButton ID="btnAdd_detail" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                        OnClick="btnAdd_Click_dt" />
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                                                        <asp:ListItem Text="區域" Value="區域" />
                                                                        <asp:ListItem Text="ALL" Value="ALL" />
                                                                        <asp:ListItem Text="北一區" Value="北一區" />
                                                                        <asp:ListItem Text="中一區" Value="中一區" />
                                                                        <asp:ListItem Text="南一區" Value="南一區" />
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxButton ID="Button5" runat="server" Text="確認">
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </TitlePanel>
                                                    <EmptyDataRow>
                                                        <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                                    </EmptyDataRow>
                                                </Templates>
                                            </cc:ASPxGridView>
                                            </div>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>

                            </TabPages>
                        </dx:ASPxPageControl>
                    </div>



                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="seperate"></div>

    </div>
</asp:Content>
