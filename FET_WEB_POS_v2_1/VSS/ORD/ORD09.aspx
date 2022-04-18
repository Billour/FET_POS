<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ORD09.aspx.cs" Inherits="VSS_ORD_ORD09" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--DropShipment主配上傳-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DropShipmentPrimaryUpload %>"></asp:Literal></div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdval">
                        <!--匯入檔案名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                            OnClick="Button1_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <dx:ASPxGridView ID="gvMaster" KeyFieldName="商品料號" ClientInstanceName="gvMaster"
            runat="server" Width="100%" OnHtmlDataCellPrepared="gvMasterDV_HtmlDataCellPrepared">
            <Columns>
               
                <dx:GridViewDataColumn FieldName="門市代碼" Caption="<%$ Resources:WebResources, StoreNo %>">
                    <DataItemTemplate>
                       <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" Text='<%# Bind("門市代碼") %>' />

                        <%--<table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox1" Text='<%# Bind("門市代碼") %>' runat="server" Width="80px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton3" runat="server" Text="<%$ Resources:WebResources, choose %>" AutoPostBack="false" SkinID="PopupButton">
                                        <ClientSideEvents Click="function(s,e){openwindow('../SAL/SAL01_chooseStore.aspx',550,350);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>--%>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
                    <DataItemTemplate>
                       <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Text='<%# Bind("商品料號") %>' />
                       <%-- <table>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="lbProductCode" Text='<%# Bind("商品料號") %>' runat="server" Width="80px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnProductCode" runat="server" Text="<%$ Resources:WebResources, choose %>" AutoPostBack="false" SkinID="PopupButton">
                                        <ClientSideEvents Click="function(s,e){openwindow('ORD01_searchProductNo.aspx',640,300);return false;}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>--%>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="主配量" Caption="<%$ Resources:WebResources, DistributionQuantity %>">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="ASPxTextBox1" Text='<%# Bind("主配量") %>' runat="server" Width="80px">
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="異常原因" Caption="<%$ Resources:WebResources, ErrorDescription %>">
                </dx:GridViewDataColumn>
            </Columns>
            <Templates>
                <EmptyDataRow>
                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                </EmptyDataRow>
            </Templates>
        </dx:ASPxGridView>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center"">
                <tr>
                    <td>
                    <!--上傳確認-->
                    
                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>" >
                            <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="ASPxButton2" AutoPostBack="false" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                            <ClientSideEvents Click="function(s,e){window.close();return false;}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
