<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_inputIMEIData.aspx.cs"
    Inherits="VSS_SAL_SAL01_inputIMEIData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IMEI輸入</title>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--商品編號-->
                             <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label1" runat="server" Text="125458700"></asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--商品名稱-->
                             <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label2" runat="server" Text="哈拉900方案 (1/2) - 5800手機"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--IMEI-->
                             <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Imei %>"></asp:Literal>：
                        </td>
                        <td class="tdval" >
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            <asp:Button runat="server" Text="<%$ Resources:WebResources, Enter %>" ID="btnInsert" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <div class="SubEditBlock">
                <div class="SubEditCommand">
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                </div>
                <div class="GridScrollBar" style="height: auto">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckAll" runat="server" CssClass="Class1" onclick="javascript:if(this.checked){$('.Class1').checkCheckboxes();}else{$('.Class1').unCheckCheckboxes();}" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckItem" runat="server" CssClass="Class1"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IMEI" HeaderText="<%$ Resources:WebResources, Imei %>" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Button ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" Visible="false" OnClientClick="window.close();return false;" />
                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" Visible="false" OnClientClick="window.close();return false;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
