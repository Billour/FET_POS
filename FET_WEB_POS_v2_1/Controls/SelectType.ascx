<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectType.ascx.cs" Inherits="SelectPromotion" %>

<table>
    <tr>
        <td>
            <dx:ASPxTextBox ID="txType" runat="server" Width="170"></dx:ASPxTextBox>
        </td>
        <td>&nbsp;</td>
        <td>
            <dx:ASPxButton ID="btnType" SkinID="PopupButton" runat="server" AutoPostBack="false"></dx:ASPxButton>
        </td>
    </tr>
</table>  

    <cc:ASPxPopupControl ID="ProductType" SkinID="TypePopupOnly" runat="server" PopupElementID="btnType"
        TargetElementID="txtType" LoadingPanelID="lp">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
