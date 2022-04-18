<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG03b.aspx.cs" Inherits="VSS_LOG_LOG03b"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupWindow.ascx" TagName="PopupWindow" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
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
                    <!--使用者功能對應作業-->
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, UserPermissionMapping %>" />
                </td>
                <td align="right">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, UserPermissionMapping %>"
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
                    <!--區域別-->
                    <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList2" runat="server">
                        <Items>
                            <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                            <dx:ListEditItem Value="1" Text="北一區" />
                            <dx:ListEditItem Value="2" Text="中一區" />
                            <dx:ListEditItem Value="3" Text="南一區" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--門市編號-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StoreNo %>" />：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="DropDownList3" runat="server">
                        <Items>
                            <dx:ListEditItem Value="0" Text="請選擇" Selected="true" />
                            <dx:ListEditItem Value="1" Text="2101" />
                            <dx:ListEditItem Value="2" Text="2102" />
                            <dx:ListEditItem Value="3" Text="2103" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <!--使用者代碼-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, UserID %>" />：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--員工編號-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>" />：
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
                    <!--使用者名稱-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, UserName %>" />：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate"></div>
    <div class="btnPosition">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Search %>"
                        OnClick="btnSearch_Click" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate"></div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
        Settings-ShowTitlePanel="true" Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared"
        OnHtmlRowCreated="grid_HtmlRowCreated" OnPageIndexChanged="grid_PageIndexChanged">
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                <HeaderTemplate>
                    <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="項次" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="員工編號" runat="server" Caption="<%$ Resources:WebResources, EmployeeNo %>"
                VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="員工姓名" runat="server" Caption="<%$ Resources:WebResources, EmployeeName %>"
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="<%$ Resources:WebResources, StoreNo %>"
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="門市名稱" runat="server" Caption="<%$ Resources:WebResources, StoreName %>"
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="區域別" runat="server" Caption="<%$ Resources:WebResources, ByDistrict %>"
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="模組名稱" runat="server" Caption="<%$ Resources:WebResources, ModuleName %>"
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="功能名稱" runat="server" Caption="<%$ Resources:WebResources, FunctionName %>"
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="更新人員" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                VisibleIndex="8">
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
                                ClientSideEvents-Click="function(s,e){openwindow('../LOG/GrantEmployeePermissions.aspx',500,400);return false;}">
                            </dx:ASPxButton>                                                    
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                            </dx:ASPxButton>
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
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
