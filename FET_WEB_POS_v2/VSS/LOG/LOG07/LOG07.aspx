<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG07.aspx.cs" Inherits="VSS_LOG_LOG07"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--店長折扣密碼設定(測)-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources,DiscountPasswordManager2 %>"></asp:Literal>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="criteria">
                <table>
                    <tr id="trCurrentPassword" runat="server">
                        <td class="tdtxt">
                            <!--輸入舊密碼-->
                            <span style="color: Red">*</span><asp:Literal ID="lblOldPassword" runat="server"
                                Text="<%$ Resources:WebResources, InputOldPassword %>"></asp:Literal>：
                        </td>
                        <td class="tdval" colspan="3" nowrap="nowrap" style="width: 300px">
                            <dx:ASPxTextBox ID="txtOldPassword" runat="server" Password="true" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--輸入新密碼-->
                            <span style="color: Red">*</span><asp:Literal ID="lblNewPassword" runat="server"
                                Text="<%$ Resources:WebResources, InputNewPassword %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtNewPassword" runat="server" Password="true" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--確認新密碼-->
                            <span style="color: Red">*</span><asp:Literal ID="lblConfirmNewPassword" runat="server"
                                Text="<%$ Resources:WebResources, ConfirmNewPassword %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxTextBox ID="txtConfirmNewPassword" runat="server" Password="true" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                </table>
                
                <table width="100px">
                    <tr>
                        <td align="right">
                            <dx:ASPxButton ID="btnCommit" runat="server"  Width="50px" Text="<%$ Resources:WebResources, Ok %>"
                                OnClick="btnCommit_Click">
                            </dx:ASPxButton>
                        </td>
                        <td align="left">
                            <dx:ASPxButton ID="btnCancel" runat="server"  Width="50px" Text="<%$ Resources:WebResources, Cancel %>"
                                SkinID="ResetButton">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
                
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt" visible="true">
                            </td>
                            <td class="tdval" visible="true">
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label><br />
                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            </td>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
