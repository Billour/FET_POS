<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="OSAL041.aspx.cs" Inherits="VSS_SAL_OSAL041" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        //判斷商品料號是否存在
        function getProdInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
            if (s.GetText() != '')
                PageMethods.getProdInfo(_gvSender.GetText(), getProdInfo_OnOK);
        }
        
        function getProdInfo_OnOK(returnData) {
            if (returnData == 0) {
                alert('商品料號不存在!!');
                _gvSender.SetValue(null);
                _gvEventArgs.processOnServer = false;
                _gvSender.Focus();
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>       
        <div class="titlef">
            <!--作廢舊POS交易作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, SalesCancel %>" />
        </div>
         
        <div class="criteria"> 
            <table>
                <tr>
                <td class="tdtxt">
                        類別：
                    </td>
                  <td class="tdval" colspan="3">
                  <asp:RadioButton ID="RadioButton2" Checked="true" Text="一般交易" runat="server" GroupName="rdoTrans"></asp:RadioButton>
                        <asp:RadioButton ID="RadioButton1" Text="代收交易" runat="server" GroupName="rdoTrans"></asp:RadioButton>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        門市編號：
                    </td>
                    <td class="tdval" width="80px">
                        <uc1:PopupControl ID="pcSTORENO" runat="server" ClientInstanceName="pcSTORENO" PopupControlName="StoresPopup" />
                    </td>
                </tr>
                <tr>
                <td class="tdtxt">
                        交易日期：
                    </td>
                     <td class="tdval">
                     <dx:ASPxTextBox ID="txtTRADE_DATE" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="8" >
                          
                        </dx:ASPxTextBox>
                     </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--機台-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, CashRegister %>" />：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtMACHINE_ID" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="2" >
                          
                        </dx:ASPxTextBox>
                    </td>
                   
                </tr>
                <tr>
                 <td class="tdtxt">
                        <!--交易序號-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, TransactionNo %>" />：
                    </td>
                    <td class="tdval" colspan="3">
                        <dx:ASPxTextBox ID="txtSALE_NO" runat="server" CssClass="tbWidthFormat" Width="160px" MaxLength="20">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="btnPosition" id="showFooter" runat="server" visible="true">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnCancelDetail" runat="server" Text="查詢" 
                            AutoPostBack="true" OnClick="btnCancelDetail_Click">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
