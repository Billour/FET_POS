<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT11.aspx.cs" Inherits="VSS_OPT11_OPT11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

    <style type="text/css">
        .mGrid
        {
            margin-top: 0px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--HappyGo點數兌換設定-->
                        <asp:Literal ID="Literal1" runat="server" Text="HappyGo點數兌換設定"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table width="100%">
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--類別-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Category %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                            <Items>
                                <dx:ListEditItem Text="ALL" Selected="true" />
                                <dx:ListEditItem Text="銷售" />
                                <dx:ListEditItem Text="代收" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開始日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="開始日期"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">

                         <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                         </table>

                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--兌點名稱-->
                        <asp:Literal ID="Literal16" runat="server" Text="兌點名稱"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox4" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--兌點點數-->
                        <asp:Literal ID="Literal17" runat="server" Text="兌點點數"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">

                         <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="txtMemo1" runat="server"></dx:ASPxTextBox></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="txtMemo2" runat="server"></dx:ASPxTextBox></td>
                            </tr>
                         </table>

                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--兌換金額-->
                        <asp:Literal ID="Literal5" runat="server" Text="兌換金額"></asp:Literal>：
                    </td>
                    <td colspan="5" class="tdval" nowrap="nowrap">

                         <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="txtMemo" runat="server"></dx:ASPxTextBox></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="txtMemo0" runat="server"></dx:ASPxTextBox></td>
                            </tr>
                         </table>

                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                    </td>
                    <td class="tdval" nowrap="nowrap"></td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次" Width="100%" 
        OnHtmlRowPrepared="grid_HtmlRowPrepared" OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged"
        AutoGenerateColumns="False" OnRowInserting="grid_RowInserting" onrowupdating="grid_RowUpdating" Settings-ShowTitlePanel="true">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn ButtonType="Button" >
                    <EditButton Visible="true"></EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>" HeaderStyle-HorizontalAlign="Center">
                    <EditItemTemplate>
                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="60">
                            <Items>
                                <dx:ListEditItem Text="銷售" />
                                <dx:ListEditItem Text="代收" />
                            </Items>
                        </dx:ASPxComboBox>
                    </EditItemTemplate>
                </dx:GridViewDataComboBoxColumn>
                <%--<dx:GridViewDataTextColumn FieldName="類別" Caption="<%$ Resources:WebResources, Category %>" HeaderStyle-HorizontalAlign="Center" />--%>
                <dx:GridViewDataTextColumn FieldName="兑點代號" Caption="兑點代號" HeaderStyle-HorizontalAlign="Center" />
                <dx:GridViewDataTextColumn FieldName="兑點名稱" Caption="兑點名稱" HeaderStyle-HorizontalAlign="Center" />
                <dx:GridViewDataDateColumn FieldName="開始日期" Caption="開始日期" HeaderStyle-HorizontalAlign="Center" />
                <dx:GridViewDataDateColumn FieldName="結束日期" Caption="結束日期" HeaderStyle-HorizontalAlign="Center" />
                <dx:GridViewDataTextColumn FieldName="點數" Caption="點數" HeaderStyle-HorizontalAlign="Center" />
                <dx:GridViewDataTextColumn FieldName="兑換金額" Caption="兑換金額" HeaderStyle-HorizontalAlign="Center" />
                <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                    <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td><dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddNew_Click" /></td>
                            <td><dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"/></td>
                        </tr>
                    </table>
                </TitlePanel>
                <EmptyDataRow>
                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                </EmptyDataRow>
            </Templates>
            <SettingsEditing Mode="Inline" />
            <SettingsPager PageSize="5"></SettingsPager>
        </cc:ASPxGridView>

    </div>
</asp:Content>
