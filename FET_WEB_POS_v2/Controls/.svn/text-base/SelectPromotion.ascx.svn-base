<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectPromotion.ascx.cs" Inherits="SelectPromotion" %>

<table>
    <tr>
        <td>
            <dx:ASPxTextBox ID="txtPromotion" runat="server" Width="170"></dx:ASPxTextBox>
        </td>
        <td>&nbsp;</td>
        <td>
            <dx:ASPxButton ID="btnPromotion" SkinID="PopupButton" runat="server" AutoPostBack="false"></dx:ASPxButton>
        </td>
    </tr>
</table>  

    <cc:ASPxPopupControl ID="PromotionsPopup" SkinID="PromotionsPopupOnly" runat="server" PopupElementID="btnPromotion"
        TargetElementID="txtPromotion" LoadingPanelID="lp">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
