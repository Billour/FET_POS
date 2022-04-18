<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridViewPanel.ascx.cs" Inherits="GridViewPanel" %>


<%@ Register src="ucItem1.ascx" tagname="ucItem1" tagprefix="uc1" %>
<%@ Register src="ucItem2.ascx" tagname="ucItem2" tagprefix="uc2" %>
<%@ Register src="ucItem3.ascx" tagname="ucItem3" tagprefix="uc3" %>
<%@ Register src="ucItem4.ascx" tagname="ucItem4" tagprefix="uc4" %>
<%@ Register src="ucItem5.ascx" tagname="ucItem5" tagprefix="uc5" %>
<%@ Register src="ucItem6.ascx" tagname="ucItem6" tagprefix="uc6" %>
<%@ Register src="ucItem7.ascx" tagname="ucItem7" tagprefix="uc7" %>

<style type="text/css">
    fieldset {
        font-family: Arial;
        -moz-border-radius: 15px;
        -webkit-border-radius: 15px;
        padding: 1px 1px;
        border: 1px solid #dedede;
    /*    width:50%;
     height:100px; */
    }
</style>


<fieldset>
<legend runat="server" ID="lg"></legend>
<br />
<table cellpadding="0" cellspacing="0" border="0">
    <tr id="tr1" runat="server" visible="false">
        <td>&nbsp;</td>
        <td>
             <uc1:ucItem1 ID="ucItem1" runat="server"></uc1:ucItem1>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="tr2" runat="server" visible="false">
        <td>&nbsp;</td>
        <td>
             <uc2:ucItem2 ID="ucItem2" runat="server"></uc2:ucItem2>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="tr3" runat="server" visible="false">
        <td>&nbsp;</td>
        <td>
             <uc3:ucItem3 ID="ucItem3" runat="server"></uc3:ucItem3>
        </td>
        <td>&nbsp;</td>
    </tr>    
    <tr id="tr4" runat="server" visible="false">
        <td>&nbsp;</td>
        <td>
             <uc4:ucItem4 ID="ucItem4" runat="server"></uc4:ucItem4>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="tr5" runat="server" visible="false">
        <td>&nbsp;</td>
        <td>
             <uc5:ucItem5 ID="ucItem5" runat="server"></uc5:ucItem5>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="tr6" runat="server" visible="false">
        <td>&nbsp;</td>
        <td>
             <uc6:ucItem6 ID="ucItem6" runat="server"></uc6:ucItem6>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="tr7" runat="server" visible="false">
        <td>&nbsp;</td>
        <td>
             <uc7:ucItem7 ID="ucItem7" runat="server"></uc7:ucItem7>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr id="tr8" runat="server" visible="false">
        <td>&nbsp;</td>
        <td>
            <dx:ASPxButton ID="ASPxButton7" runat="server" Text="加入"></dx:ASPxButton>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr><td colspan="3">&nbsp;</td></tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <div style="overflow:auto;">
                <cc:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1"
                    runat="server" Width="100%" ondatabound="ASPxGridView1_DataBound"
                    onhtmldatacellprepared="ASPxGridView1_HtmlDataCellPrepared">
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />     
                    
                 </cc:ASPxGridView>
            </div>
        </td>
        <td>&nbsp;</td>
    </tr>
</table>
<br />
</fieldset>