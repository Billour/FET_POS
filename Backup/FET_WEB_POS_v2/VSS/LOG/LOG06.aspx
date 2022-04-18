<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LOG06.aspx.cs" Inherits="VSS_LOG_LOG06" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">


    <div class="titlef">
        <!--門市特殊客訴處理折扣密碼設定-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources,DiscountStoreManagerPwdSettings %>"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--有效日期-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ValidDate %>"></asp:Literal>：
                </td>
                <td class="tdval" colspan="3" nowrap="nowrap">
                    <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                        <tr>
                            <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ></dx:ASPxDateEdit></td>
                            <td>&nbsp;</td>
                            <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                            <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ></dx:ASPxDateEdit></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--輸入密碼-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, InputPassword %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <asp:TextBox ID="pw" runat="server" TextMode="Password" />
                </td>
            </tr>
            <td class="tdtxt">
                <!--確認密碼-->
                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ConfirmPassword %>"></asp:Literal>：
            </td>
            <td class="tdval">
                <asp:TextBox ID="cpw" runat="server" TextMode="Password" />
            </td>
            <tr>
                <td class="tdtxt">
                    <!--更新日期-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                </td>
                <td class="tdval" visible="true">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div class="btnPosition">
            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                OnClick="btnCommit_Click" Style="height: 21px" />
            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                onclick="Button2_Click" />
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" visible="true">
                      
                    </td>
                    <td class="tdval" visible="true">
                      <asp:Label ID="Label2" runat="server" Text=""></asp:Label><br />
                       <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </td>
            </table>
        </div>
    </div>
</asp:Content>