<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DISItemChargesAndApply.ascx.cs"
    Inherits="DISItemChargesAndApply" %>

<table width="100%" align="left">
    <tr align="left">
        <td>
            <asp:Literal ID="ltlRate" runat="server" Text="費率"></asp:Literal>
        </td>
        <td>
            <asp:CheckBoxList ID="cbRate" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="VOICE"> Voice</asp:ListItem>
                <asp:ListItem Value="DATA">Data</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Literal ID="ltlGA" runat="server" Text="GA"></asp:Literal>
        </td>
        <td>
            &nbsp;<dx:ASPxCheckBox ID="cbGAAll" ClientInstanceName="cbGAAll" Text="所有GA" runat="server">
                <ClientSideEvents CheckedChanged=
                "function(s,e){
                     $('#gaChackList input:checkbox').each(function(){
                                        $(this).attr('checked',s.GetChecked());
                                    });
                              
                           }
                " />
            </dx:ASPxCheckBox>
        </td>
    </tr>
    <tr align="left">
        <td>
            &nbsp;
        </td>
        <td>
        <div id="gaChackList">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbGAG1" runat="server">
                            <asp:ListItem Value="POS3G">3G Postpaid啟用</asp:ListItem>
                            <asp:ListItem Value="POSTC">2G Postpaid啟用</asp:ListItem>
                            <asp:ListItem Value="POSWA">WALA Postpaid啟用</asp:ListItem>
                            <asp:ListItem Value="WIMAX">WiMAX啟用</asp:ListItem>
                            <asp:ListItem Value="2INNC">買一送一的3G NewCash啟用</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cbGAG2" runat="server">
                            <asp:ListItem Value="2KT3N">KGT Postpaid =&gt; 3G NewCash</asp:ListItem>
                            <asp:ListItem Value="3GT3N">3G Postpaid =&gt; 3G NewCash</asp:ListItem>
                            <asp:ListItem Value="3NCASH">3G NewCash啟用</asp:ListItem>
                            <asp:ListItem Value="PREGA">2G Prepaid啟用</asp:ListItem>
                            <asp:ListItem Value="PREKA">KGT NewCash啟用</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table></div>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Literal ID="ltlLoyalty" runat="server" Text="Loyalty"></asp:Literal>
        </td>
        <td>
            <asp:CheckBoxList ID="cbLoyalty" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="FET2G">FET-2G</asp:ListItem>
                <asp:ListItem Value="FET3G"> FET-3G</asp:ListItem>
                <asp:ListItem Value="KGT">KGT</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Literal ID="ltl223" runat="server" Text="2轉3"></asp:Literal>
        </td>
        <td>
            &nbsp;<dx:ASPxCheckBox ID="cb223All" ClientInstanceName="cb223All" Text="所有2轉3" runat="server">
                <ClientSideEvents CheckedChanged="function(s,e){ $('#dv223 input:checkbox').each(function(){
                                        $(this).attr('checked',s.GetChecked());
                                    });}" />
            </dx:ASPxCheckBox>
        </td>
    </tr>
    <tr align="left">
        <td>
            &nbsp;
        </td>
        <td>
        <div id="dv223">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:CheckBoxList ID="cb223G1" runat="server">
                            <asp:ListItem Value="2GT3G">2G Postpaid =&gt; 3G Postpaid</asp:ListItem>
                            <asp:ListItem Value="2KT3G">KGT Prepaid =&gt; 3G Postpaid</asp:ListItem>
                            <asp:ListItem Value="3GTPO">3G Postpaid =&gt; 2G Postpaid</asp:ListItem>
                            <asp:ListItem Value="3NT3G">3G New Cash =&gt; 3G Postpaid</asp:ListItem>
                            <asp:ListItem Value="IFT3G">KGT Prepaid =&gt; 3G Postpaid</asp:ListItem>
                            <asp:ListItem Value="NCT3G">2G New Cash =&gt; 3G Postpaid</asp:ListItem>
                            <asp:ListItem Value="NPF3G">MNP =&gt; 3G Postpaid</asp:ListItem>
                            <asp:ListItem Value="NPFPP">MNP =&gt; 2G Postpaid</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td valign="top">
                        <asp:CheckBoxList ID="cb223G2" runat="server">
                            <asp:ListItem Value="PRT3G">2G Prepaid =&gt; 3G Postpaid</asp:ListItem>
                            <asp:ListItem Value="FPP3N">2G Prepaid =&gt; 3G New Cash</asp:ListItem>
                            <asp:ListItem Value="NCT3N">2G New Cash =&gt; 3G New Cash</asp:ListItem>
                            <asp:ListItem Value="NPF3N">MNP =&gt; 3G New Cash</asp:ListItem>
                            <asp:ListItem Value="POT3N">2G Postpaid =&gt; 3G New Cash</asp:ListItem>
                            <asp:ListItem Value="PPT3N">KGT Prepaid =&gt; 3G New Cash</asp:ListItem>
                            <asp:ListItem Value="KGTPR">KGT Prepaid =&gt; 2G Prepaid</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
            </div>
        </td>
    </tr>
    <tr align="left">
        <td>
            <asp:Literal ID="ltlMNP" runat="server" Text="MNP"></asp:Literal>
        </td>
        <td>
            <asp:CheckBoxList ID="cbMNP" runat="server">
                <asp:ListItem Value="NPF3X">FET MNP到其它業者再MNP回FET</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr align="left">
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>