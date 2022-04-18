<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT16.aspx.cs" Inherits="VSS_OPT16_OPT16" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 179px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server"> 

    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--HappyGo兌點名單上傳-->
                        <asp:Literal ID="Literal1" runat="server" Text="HappyGo兌點名單上傳"></asp:Literal>
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
                    <td style="width:200px" align="right">HG活動代號：</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width:200px" align="right">
                        <!--檔案路徑：-->
                        <asp:Literal ID="Literal6" runat="server" Text="檔案路徑"></asp:Literal>：
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>                
            </table>
        </div>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="HG活動代號" Width="100%" 
        AutoGenerateColumns="False" EnableRowsCache="False" >
            <Columns>
                <dx:GridViewDataDateColumn FieldName="HG活動代號" Caption="HG活動代號" HeaderStyle-HorizontalAlign="Center" />
                <dx:GridViewDataDateColumn FieldName="HappyGo卡號" Caption="<%$ Resources:WebResources, HappyGoCardNo %>" HeaderStyle-HorizontalAlign="Center" />
            </Columns>
            <Templates>
                 <EmptyDataRow>
                    <asp:Label ID="emptyDataLabel" runat="server" Text="目前無匯入資料"></asp:Label>
                 </EmptyDataRow>
            </Templates>
        </cc:ASPxGridView>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><dx:ASPxButton ID="Button3" runat="server" Text="上傳確認" ></dx:ASPxButton> </td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, Cancel %>" ></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>       

    </div>
</asp:Content>
