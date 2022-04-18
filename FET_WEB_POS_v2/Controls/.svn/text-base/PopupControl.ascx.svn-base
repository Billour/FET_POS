<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupControl.ascx.cs" Inherits="PopupControl" %>

 <table id="tdControl" runat="server" width="220px" cellpadding="0" cellspacing="0" border="0" align="left">
    <tr>
        <td width="80%">
            <div style="width:100%;">  
                <dx:ASPxTextBox ID="txtControl" runat="server" Width="95%" EnableViewState="false" AutoCompleteType="Disabled">
                  <ValidationSettings CausesValidation="false">
                      <RequiredField IsRequired="false" ErrorText="必填欄位" />
                  </ValidationSettings>
                </dx:ASPxTextBox>
            </div>
        </td>
        <td width="3px">&nbsp;</td>
        <td width="5%" align="left">
            <dx:ASPxButton ID="btnControl" SkinID="PopupButton" runat="server" CausesValidation="false" AutoPostBack="false" Width="20px">
            </dx:ASPxButton>
        </td>
        <td>&nbsp;</td>
    </tr>
</table>   
<script type="text/javascript">
    function setValue(s, e) { 
    
    }
</script>

    <script type= "text/javascript">
        function ASPxPopupControl_Init(s, e) {
            var iframe = s.GetContentIFrame();
            iframe.popupArguments = {};
            iframe.contentLoaded = false;
            var controlCollection = ASPxClientControl.GetControlCollection();
            iframe.popupArguments.popupContainer = controlCollection.Get('ctl00_ASPxPopupControl1');
            
            ASPxClientUtils.AttachEventToElement(iframe, 'load', function(e) {
                if (!controlCollection.Get('ctl00_ASPxPopupControl1').GetClientVisible())
                    return;
                controlCollection.Get('ctl00_lp').Hide();
                iframe.contentLoaded = true;
            });

            var targetElementId = 'ctl00_txtControl';

            iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId)
                        || document.getElementById(targetElementId);


        }

        function Shown(s, e) {
            if (!s.GetContentIFrame().contentLoaded)
                ASPxClientControl.GetControlCollection().Get('ctl00_lp').ShowInElement(s.GetContentIFrame());
        }
    </script>
    <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" PopupElementID="btnControl"
        TargetElementID="txtControl" LoadingPanelID="lp" Modal="true">
        <ClientSideEvents Init="function(s, e) {ASPxPopupControl_Init(s,e);}" 
                    Shown="function(s, e) {Shown(s, e); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </cc:ASPxPopupControl>
     <dx:ASPxLoadingPanel ID="lp" runat="server" ClientInstanceName="lp" Width="150px">
    </dx:ASPxLoadingPanel>

