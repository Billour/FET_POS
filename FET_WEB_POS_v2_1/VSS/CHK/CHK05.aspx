<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="CHK05.aspx.cs" Inherits="VSS_CHK_CHK05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--繳大鈔-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DollarBills %>"></asp:Literal>
    </div>
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
                    <dx:ASPxDateEdit ID="tradeDate" runat="server">
                    </dx:ASPxDateEdit>
                </td>
                <td>
                    <!--訖-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                </td>
                <td>
                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                    </dx:ASPxDateEdit>
                </td>
                <td class="tdtxt">
                    <!--機台編號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, CashRegisterNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList1" runat="server">
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
        <table cellpadding="0" cellspacing="0" border="0" align="center">
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
                        AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e) { resetForm(aspnetForm); }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="批次"
        Width="100%" Settings-ShowTitlePanel="true" OnPageIndexChanged="grid_PageIndexChanged"
        OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                <EditButton Visible="false">
                </EditButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="交易日期" Caption="<%$ Resources:WebResources, TradeDate %>"
                ReadOnly="true" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="機台編號" Caption="<%$ Resources:WebResources, CashRegisterNo %>"
                HeaderStyle-HorizontalAlign="Center" ReadOnly="true" CellStyle-HorizontalAlign="Left">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn FieldName="批次" Caption="<%$ Resources:WebResources, BatchNo %>"
                ReadOnly="true" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="繳大鈔" Caption="繳大鈔" HeaderStyle-HorizontalAlign="Center"
                CellStyle-HorizontalAlign="Right" PropertiesTextEdit-Style-HorizontalAlign="Right" />
            <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                ReadOnly="true" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                ReadOnly="true" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Left">
                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <TitlePanel>
                <table cellpadding="0" cellspacing="0" align="left">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                OnClick="AddButton_Click" />
                        </td>
                    </tr>
                </table>
            </TitlePanel>
        </Templates>
        <Styles>
            <EditFormColumnCaption Wrap="False">
            </EditFormColumnCaption>
        </Styles>
        <SettingsPager PageSize="10">
        </SettingsPager>
        <SettingsEditing Mode="Inline" />
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
</asp:Content>
