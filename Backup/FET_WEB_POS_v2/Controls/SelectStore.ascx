<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectStore.ascx.cs" Inherits="SelectStore" %>

 <table>
    <tr>
        <td>
            <dx:ASPxTextBox ID="txtStore" runat="server" Width="170"></dx:ASPxTextBox>
        </td>
        <td>&nbsp;</td>
        <td>
            <dx:ASPxButton ID="btnStore" runat="server" SkinID="PopupButton" AutoPostBack="false"></dx:ASPxButton>
        </td>
    </tr>
</table>  

    <cc:ASPxPopupControl ID="StoresPopup" SkinID="StoresPopup" runat="server" PopupElementID="btnStore"
        TargetElementID="txtStore" LoadingPanelID="lp">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>
