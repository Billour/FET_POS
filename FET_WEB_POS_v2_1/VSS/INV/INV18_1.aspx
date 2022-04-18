<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV18_1.aspx.cs" Inherits="VSS_INV_INV18_1" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StockAdjustment %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                            AutoPostBack="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s, e){ document.location='INV18.aspx'; }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整單號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StockAdjustmentNoteNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="lblOrderNo" runat="server" Text="HR100914001">
                            </dx:ASPxLabel>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整日期-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <div style="width:120px;">
                                <dx:ASPxDateEdit ID="transferOutStartDate" runat="server">
                                    <ValidationSettings CausesValidation="false">                                                
                                        <RequiredField IsRequired="true" />                                                       
                                    </ValidationSettings>                                                
                                </dx:ASPxDateEdit>
                            </div>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            暫存
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--調整門市-->
                            <span style="color: Red">*</span>
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>：
                        </td>
                        <td align="left">
                            <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" Text="門市A" IsValidation="true" />
                            <%--<table>
                                <tr>
                                    <td width="190px">
                                        <div style="width:190px;">
                                            <dx:ASPxTextBox ID="lblOrderNo0" runat="server" Text="門市A" Width=170px>
                                                <ValidationSettings CausesValidation="false">                                                
                                                    <RequiredField IsRequired="true"/>                                                       
                                                </ValidationSettings>                                                
                                            </dx:ASPxTextBox>
                                        </div>
                                    </td>
                                    <td width="10px">
                                        <dx:ASPxButton ID="chooseButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                            SkinID="PopupButton">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>--%>
                        </td>
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label3" runat="server" Text="10/07/12 15:00">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--備註-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap">
                            <dx:ASPxTextBox ID="TextBox3" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt" nowrap="nowrap">
                            <!--更新人員-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval" nowrap="nowrap">
                            <dx:ASPxLabel ID="Label4" runat="server" Text="64591 李家駿">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
                </div>
                </div>

        <div class="seperate"></div>

        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次"
            Width="100%" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated"
            OnPageIndexChanged="gvMaster_PageIndexChanged" OnRowInserting="gvMaster_RowInserting"
            Settings-ShowTitlePanel="true">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                   <EditButton Visible="true"></EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="項次" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, Items %>"
                    VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="商品料號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"
                    VisibleIndex="3">
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Text='<%#BIND("[商品料號]") %>' />
                    
                        <%--<table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtProductCode" runat="server" Width="68px" Text='<%#BIND("[商品料號]") %>'>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="chooseButton2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        SkinID="PopupButton" />
                                </td>
                            </tr>
                        </table>
                        <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" EnableViewState="False"
                            PopupElementID="chooseButton2" TargetElementID="txtProductCode">
                        </cc:ASPxPopupControl>--%>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>"
                    VisibleIndex="4">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="庫存量" runat="server" Caption="<%$ Resources:WebResources, StockQuantity %>"
                    VisibleIndex="5">
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtStockQuantity" runat="server" Width="68px" Text='<%#BIND("[庫存量]")  %>'>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="調整量" runat="server" Caption="<%$ Resources:WebResources, AdjuestmentQuantity %>"
                    VisibleIndex="6">
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtAdjuestmentQuantity" runat="server" Width="68px" Text='<%#BIND("[調整量]")  %>'>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="調整原因" runat="server" Caption="<%$ Resources:WebResources, ReasonForAdjustment %>"
                    VisibleIndex="7">
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtForAdjustment" runat="server" Width="68px" Text='<%#BIND("[調整原因]")  %>'>
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ChoseForAdjustment" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        ClientSideEvents-Click="function(s,e){openwindow('../INV/INV18_2.aspx',500,400);return false;}"
                                        AutoPostBack="false">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="5" />
            <SettingsEditing Mode="Inline" />
            <Templates>
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                    OnClick="btnNew_Click" Visible="true" CausesValidation="false">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                    AutoPostBack="false" Visible="true" UseSubmitBehavior="false" CausesValidation="false">
                                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </TitlePanel>
            </Templates>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
        </cc:ASPxGridView>
        <div class="seperate"></div>        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" 
                            OnClick="btnSave_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" CausesValidation="false"
                            ClientSideEvents-Click="function(s,e){openwindow('../INV/INV18.aspx',500,400);return false;}" />
                    </td>
                </tr>
            </table>
        </div>
<%--        <cc:ASPxPopupControl ID="storesPopup1" SkinID="StoresPopup" runat="server" EnableViewState="False"
            PopupElementID="chooseButton1" TargetElementID="lblOrderNo0">
        </cc:ASPxPopupControl>
--%>
</asp:Content>
