<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON10_chooseSupplierNo.aspx.cs" Inherits="VSS_CON10_chooseSupplierNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StoreSearch %>"></asp:Literal></title>
      <script type="text/javascript">
        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td class="tdtxt" height="20"></td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--廠商代號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </td>
                 </tr>
            </table>

        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table>
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td><dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>

        <div class="seperate">
        </div>
        <div class="SubEditBlock">
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="廠商代號"
            Width="100%" AutoGenerateColumns="False"
            EnableRowsCache="False">
                <Columns>
                    <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                        <DataItemTemplate>
                            <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose">
                                <ClientSideEvents Init="OnInit" />
                            </dx:ASPxRadioButton>
                        </DataItemTemplate>
                    </dx:GridViewDataDateColumn> 
                    <dx:GridViewDataColumn FieldName="廠商代號" Caption="<%$ Resources:WebResources, SupplierNo %>" />
                    <dx:GridViewDataColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>" />
                </Columns>
                <SettingsPager PageSize="5" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            </cc:ASPxGridView>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table>
                <tr>
                    <td><dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>" ClientSideEvents-Click="function(s,e){window.close();return false;}" /></td>
                    <td><dx:ASPxButton ID="Button21" runat="server" Text="<%$ Resources:WebResources, Cancel %>" ClientSideEvents-Click="function(s,e){window.close();return false;}" /></td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
