<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupControl.ascx.cs" Inherits="PopupControl" %>

 <table cellpadding="0" cellspacing="0" border="0" align="left">
    <tr>
        <td width="180">
            <div style="width:180px;">  
                <dx:ASPxTextBox ID="txtControl" runat="server" Width="170">
                  <ValidationSettings CausesValidation="false">
                      <RequiredField IsRequired="false" />
                  </ValidationSettings>
                </dx:ASPxTextBox>
            </div>
        </td>
        <td width="10">&nbsp;</td>
        <td>
            <dx:ASPxButton ID="btnControl" SkinID="PopupButton" runat="server" CausesValidation="false" AutoPostBack="false" Width="20px"></dx:ASPxButton>
        </td>
        <td>&nbsp;</td>
    </tr>
</table>   

    <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" PopupElementID="btnControl"
        TargetElementID="txtControl" LoadingPanelID="lp">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Width="150px">
    </dx:ASPxLoadingPanel>

