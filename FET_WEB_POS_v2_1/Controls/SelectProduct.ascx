<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectProduct.ascx.cs" Inherits="SelectProduct" %>

 <table>
    <tr>
        <td>
            <dx:ASPxTextBox ID="txtProduct" runat="server" Width="170"></dx:ASPxTextBox>
        </td>
        <td>&nbsp;</td>
        <td>
            <dx:ASPxButton ID="btnProduct" SkinID="PopupButton" runat="server" AutoPostBack="false"></dx:ASPxButton>
        </td>
    </tr>
</table>   

    <cc:ASPxPopupControl ID="ProductsPopup" SkinID="ProductsPopup" runat="server" PopupElementID="btnProduct"
        TargetElementID="txtProduct" LoadingPanelID="lp">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
