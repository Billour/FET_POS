<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV12.aspx.cs" Inherits="VSS_INV_INV12"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--無訂單進貨資料輸入-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, NoPurchaseOrderDataEntry %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                            AutoPostBack="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s, e){ document.location='INV13.aspx'; }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td class="tdtxt">
                        <!--進貨單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReceivingNoteNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Literal ID="ReceivingNoteNumber" runat="server"></asp:Literal>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Literal ID="ModifiedDate" runat="server" Text="">
                        </asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--進貨日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceivedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxDateEdit ID="ReceivedDate" runat="server" AutoPostBack="true">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Literal ID="ModifiedBy" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--備註-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="200px" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品料號"
            Width="100%" Settings-ShowTitlePanel="true" OnRowInserting="gvMaster_RowInserting">
            <Columns>
            <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                    <EditButton Visible="true">
                    </EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>"
                    VisibleIndex="1">
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup"
                            Text='<%#BIND("[商品料號]") %>' />
                    </EditItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" ReadOnly="True" Caption="<%$ Resources:WebResources, ProductName %>"
                    VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="單位" Caption="<%$Resources:WebResources, Unit %>"
                    VisibleIndex="3" ReadOnly="true">
                    <PropertiesTextEdit>
                        <ReadOnlyStyle>
                            <Border BorderStyle="None" />
                        </ReadOnlyStyle>
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="數量" HeaderStyle-HorizontalAlign="Center" VisibleIndex="4">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Quantity %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                     <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txt1" runat="server" Width="100px" Text='<%#BIND("[數量]")  %>'>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="總金額" HeaderStyle-HorizontalAlign="Center" VisibleIndex="5">
                    <HeaderCaptionTemplate>
                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TotalAmount %>">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                    <EditCellStyle HorizontalAlign="Left" />
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txt2" runat="server" Width="100px" Text='<%#BIND("[總金額]")  %>'>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dx:GridViewDataTextColumn>
            </Columns>
            
            <SettingsEditing Mode="Inline" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>" />
            <Templates>
                <TitlePanel>
                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                    Visible="true" CausesValidation="false" OnClick="btnNew_Click">
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
        </cc:ASPxGridView>
        <div class="seperate">
        </div>
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
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
