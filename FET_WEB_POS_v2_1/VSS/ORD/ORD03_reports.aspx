<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD03_reports.aspx.cs" Inherits="VSS_ORD_ORD03_reports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div >
        <div class="titlef">
            <!--訂單報表-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, OrderReport %>"></asp:Literal>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" >
            </dx:ASPxButton>
        </div>
        <div class="seperate">
        </div>
        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:WebResources, OrderDetailsSearch %>"></asp:Label>
        <!--報表產出時間-->
        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReportCreatedDate %>"></asp:Literal>：<asp:Label
            ID="Label2" runat="server" Text="2010/08/01 12:29:41"></asp:Label>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height:300px">
                <cc:ASPxGridView ID="drMasterDV" Width="100%" runat="server" ClientInstanceName="drMasterDV">
                </cc:ASPxGridView>
            </div>
        </div>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
