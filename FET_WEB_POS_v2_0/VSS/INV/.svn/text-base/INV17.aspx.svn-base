<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV17.aspx.cs" Inherits="VSS_INV_INV17" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "關帳日查詢", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=no,scrollbars=no,location=no,toolbar=no,status=no');

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="func">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            <!--關帳日設定-->
                            <asp:Literal ID="Literal1" runat="server" Text="關帳日設定"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div>
                    <table>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--關帳年月-->
                                <asp:Literal ID="Literal2" runat="server" Text="關帳年月"></asp:Literal>：
                            </td>
                            <td nowrap="nowrap">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <dx:ASPxDateEdit ID="transferOutStartEndDate" runat="server">
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="seperate">
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
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="關帳年月"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlCommandCellPrepared="gvMaster_HtmlCommandCellPrepared"
            OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged"
            OnRowUpdating="gvMaster_RowUpdating" OnRowInserting="gvMaster_RowInserting" OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
            Settings-ShowTitlePanel="true" AutoGenerateColumns="true">
            <Columns>
                <dx:GridViewDataTextColumn VisibleIndex="0" Caption=" ">
                    <HeaderTemplate>
                        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                        </dx:ASPxCheckBox>
                    </HeaderTemplate>
                    <EditItemTemplate>
                        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                        </dx:ASPxCheckBox>
                    </EditItemTemplate>
                    <DataItemTemplate>
                        <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                        </dx:ASPxCheckBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                    <EditButton Visible="true" Text="編輯">
                    </EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="關帳年月" runat="server" Caption="關帳年月" VisibleIndex="2">
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txt1" runat="server" Width="100px" Text='<%#BIND("[關帳年月]")  %>'>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="關帳日" runat="server" Caption="關帳日" VisibleIndex="3">
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txt2" runat="server" Width="100px" Text='<%#BIND("[關帳日]")  %>'>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                    VisibleIndex="4">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                    VisibleIndex="5">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsEditing Mode="Inline" />
            <SettingsPager PageSize="5" />
            <Templates>
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                    OnClick="btnNew_Click" Visible="true">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                    AutoPostBack="false" Visible="true" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
    </div>
    </div>
</asp:Content>
