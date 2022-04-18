<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG03.aspx.cs" Inherits="VSS_LOG_LOG03"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>

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
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--角色功能對應作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RolePermissionMapping %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            Visible="false" />
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--角色代碼-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, RoleID %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList1" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="AC" />
                                <dx:ListEditItem Value="2" Text="CB" />
                                <dx:ListEditItem Value="3" Text="DC" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--角色名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, RoleName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="DropDownList2" runat="server">
                            <Items>
                                <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                                <dx:ListEditItem Value="1" Text="店長" />
                                <dx:ListEditItem Value="2" Text="店員" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                        <!--角色狀態-->
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="功能代碼"
            Settings-ShowTitlePanel="true" Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared"
            OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="順序" runat="server" Caption="<%$ Resources:WebResources, SortNo %>"
                    VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="角色代碼" runat="server" Caption="<%$ Resources:WebResources, RoleID %>"
                    VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="角色名稱" runat="server" Caption="<%$ Resources:WebResources, RoleName %>"
                    VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="系統別" runat="server" Caption="<%$ Resources:WebResources, System %>"
                    VisibleIndex="6">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="模組名稱" runat="server" Caption="<%$ Resources:WebResources, ModuleName %>"
                    VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="功能代碼" runat="server" Caption="<%$ Resources:WebResources, Choose %>"
                    VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="功能名稱" runat="server" Caption="<%$ Resources:WebResources, FunctionName %>"
                    VisibleIndex="7">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="URL" runat="server" Caption="<%$ Resources:WebResources, Url %>"
                    VisibleIndex="7">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="5" />
            <SettingsEditing Mode="Inline" />
            <Templates>                
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>" 
                                ClientSideEvents-Click="function(s,e){openwindow('../LOG/GrantPermissions.aspx',500,400);}" AutoPostBack="false">
                                </dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                            </td>
                        </tr>
                    </table>
                </TitlePanel>                
            </Templates>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" >
                        
                        </dx:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </td>
                </tr>
            </table>
        </div> 
</asp:Content>
