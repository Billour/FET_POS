<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectCostCenter.ascx.cs" Inherits="SelectCostCenter" %>

 <table>
    <tr>
        <td>
            <dx:ASPxTextBox ID="txtCostCenter" runat="server" Width="170"></dx:ASPxTextBox>
        </td>
        <td>&nbsp;</td>
        <td>
            <dx:ASPxButton ID="btnCostCenter" SkinID="PopupButton" runat="server" AutoPostBack="false">
            </dx:ASPxButton>
        </td>
    </tr>
</table>   


    <cc:ASPxPopupControl ID="CostCenterPopup" SkinID="CostCenterPopup" runat="server" PopupElementID="btnCostCenter"
        TargetElementID="txtCostCenter" LoadingPanelID="lp">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
