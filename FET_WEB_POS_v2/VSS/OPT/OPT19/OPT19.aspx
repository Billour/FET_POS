<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="OPT19.aspx.cs" Inherits="VSS_OPT_OPT19_OPT19" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <head>
        <title>分錄作業</title>

        <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

        <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

        <style>
            div1
            {
                overflow: scroll;
                height: 250px;
                scrollbar-face-color: #0000FF;
                scrollbar-highlight-color: #cccccc;
                scrollbar-darkshadow-color: #cccccc;
            }
            .table1
            {
                border-style: double;
                border-width: medium;
                padding-bottom:10px;
                padding-left:10px;
                padding-top:10px;
                padding-right:10px;
                width: 800px;
                height: 250px;
                border-color: Gray;
                margin-top: 30px;
                margin-left: 30px;
                margin-right: 10px;
                margin-bottom: 50px;
                text-align: left;
                vertical-align: top;
            }
            .tr1
            {
                text-align: left;
                vertical-align: top;
            }
        </style>
    </head>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    
    <div class="div1">
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="table1">
                    <tr class="tr1">
                        <td colspan="7">
                            <div class="titlef">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ClearRecord %>"></asp:Literal>
                            </div>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>"></asp:Literal>：
                        </td>
                        <td width="200px">
                            <dx:ASPxTextBox ID="txtSaleNo" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReconciliationDateInterval %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtSdate" runat="server" EditFormatString="yyyy/MM/dd" AllowUserInput="false"
                                ClientInstanceName="txtSdate">
                            </dx:ASPxDateEdit>
                        </td>
                        <td width="20px">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtEdate" runat="server" AllowUserInput="false" EditFormatString="yyyy/MM/dd"
                                ClientInstanceName="txtEdate">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <dx:ASPxButton ID="btnOk" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                OnClick="btnOk_Click" CommandName="Clear">
                            </dx:ASPxButton>
                        </td>
                        <td colspan="3" align="left">
                            <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                OnClick="btnClear_Click" CommandName="Clear">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td colspan="7" align="center">
                            
                            <textarea id="txtClearMessage" cols="80" rows="3" runat="server" readonly></textarea>
                        </td>
                    </tr>
                </table>
                
                 <table class="table1">
                    <tr class="tr1">
                        <td colspan="7">
                            <div class="titlef">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProcessRecord %>"></asp:Literal>
                            </div>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>"></asp:Literal>：
                        </td>
                        <td width="200px">
                            <dx:ASPxTextBox ID="txtSaleNo2" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ReconciliationDateInterval %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtSdate2" runat="server" EditFormatString="yyyy/MM/dd" AllowUserInput="false"
                                ClientInstanceName="txtSdate2">
                            </dx:ASPxDateEdit>
                        </td>
                        <td width="20px">
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtEdate2" runat="server" AllowUserInput="false" EditFormatString="yyyy/MM/dd"
                                ClientInstanceName="txtEdate2">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <dx:ASPxButton ID="btnOk2" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                OnClick="btnOk_Click" CommandName="Process">
                            </dx:ASPxButton>
                        </td>
                        <td colspan="3" align="left">
                            <dx:ASPxButton ID="btnClear2" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                OnClick="btnClear_Click" CommandName="Process">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td colspan="7" align="center">
                            
                            <textarea id="txtClearMessage2" cols="80" rows="3" runat="server" readonly></textarea>
                        </td>
                    </tr>
                </table>
                
                 <table class="table1">
                    <tr class="tr1">
                        <td colspan="7">
                            <div class="titlef">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, UploadRecord %>"></asp:Literal>
                            </div>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td>
                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>"></asp:Literal>：
                        </td>
                        <td width="200px">
                            <dx:ASPxTextBox ID="txtSaleNo3" runat="server">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ReconciliationDateInterval %>"></asp:Literal>：
                        </td>
                        <td>
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtSdate3" runat="server" EditFormatString="yyyy/MM/dd" AllowUserInput="false"
                                ClientInstanceName="txtSdate3">
                            </dx:ASPxDateEdit>
                        </td>
                        <td width="20px">
                            <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtEdate3" runat="server" AllowUserInput="false" EditFormatString="yyyy/MM/dd"
                                ClientInstanceName="txtEdate3">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <dx:ASPxButton ID="btnOk3" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                OnClick="btnOk_Click" CommandName="Upload">
                            </dx:ASPxButton>
                        </td>
                        <td colspan="3" align="left">
                            <dx:ASPxButton ID="btnClear3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                OnClick="btnClear_Click" CommandName="Upload">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr class="tr1">
                        <td colspan="7" align="center">
                            
                            <textarea id="txtClearMessage3" cols="80" rows="3" runat="server" readonly></textarea>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
