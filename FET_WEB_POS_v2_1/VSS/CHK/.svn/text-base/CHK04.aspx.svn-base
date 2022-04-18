<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CHK04.aspx.cs" Inherits="VSS_CHK_CHK04"
    MasterPageFile="~/MasterPage.master" %>
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
                        <!--找零金-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ChangeAmount %>"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--交易日期-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
                        </td>
                        <td>
                            <!--起-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, start %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                           <!--訖-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                            </dx:ASPxDateEdit>
                        </td>
                        <td class="tdtxt">
                            <!--機台編號-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CashRegisterNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                <Items>
                                    <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                    <dx:ListEditItem Text="01" />
                                    <dx:ListEditItem Text="02" />
                                    <dx:ListEditItem Text="03" />
                                    <dx:ListEditItem Text="04" />
                                    <dx:ListEditItem Text="05" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                    </tr>
                </table>
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
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                AutoPostBack="false" UseSubmitBehavior="false">
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
                    <div id="Div1" runat="server" class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="批次"
                            Width="100%" Settings-ShowTitlePanel="true" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                            OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged"
                            AutoGenerateColumns="False" OnRowInserting="gvMaster_RowInserting" OnRowUpdating="gvMaster_RowUpdating">
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Button" Caption=" ">
                                    <EditButton Visible="false">
                                    </EditButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="交易日期" Caption="<%$ Resources:WebResources, TradeDate %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true" CellStyle-HorizontalAlign="Left">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="機台編號" Caption="<%$ Resources:WebResources, CashRegisterNo %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true" CellStyle-HorizontalAlign="Left">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                </dx:GridViewDataTextColumn>
                               
                                <dx:GridViewDataTextColumn FieldName="批次" Caption="<%$ Resources:WebResources, BatchNo %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true" CellStyle-HorizontalAlign="Left">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="找零金" Caption="<%$ Resources:WebResources, ChangeAmount %>"
                                    HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Right" PropertiesTextEdit-Style-HorizontalAlign="Right" />
                                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true" CellStyle-HorizontalAlign="Left">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                                    HeaderStyle-HorizontalAlign="Center" ReadOnly="true" CellStyle-HorizontalAlign="Left">
                                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                    OnClick="btnAdd_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>                                
                            </Templates>
                            <SettingsEditing Mode="Inline" />
                            <SettingsPager PageSize="10"></SettingsPager>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        </cc:ASPxGridView>
                    </div>
                </ContentTemplate>
                
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
