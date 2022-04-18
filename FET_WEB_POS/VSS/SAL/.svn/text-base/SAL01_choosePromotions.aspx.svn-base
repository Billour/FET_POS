<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_choosePromotions.aspx.cs"
    Inherits="VSS_SAL_SAL01_choosePromotions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, SelectPromotionProduct %>" /></title>
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
                            <!--促銷代號-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>" />：
                        </td>
                        <td nowrap>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--促銷名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />：
                        </td>
                        <td >
                            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                        </td>
                        <td ><asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </div>
           
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div class="GridScrollBar" style="height:130px">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="radioChoose" runat="server" GroupName="Choose" AutoPostBack="true"
                                            OnCheckedChanged="radioChoose_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="項次" HeaderText="<%$ Resources:WebResources, Items %>" />
                                <asp:BoundField DataField="促銷代碼" HeaderText="<%$ Resources:WebResources, PromotionCode %>" />
                                <asp:BoundField DataField="促銷名稱" HeaderText="<%$ Resources:WebResources, PromotionName %>" />
                                <asp:BoundField DataField="促銷類別" HeaderText="<%$ Resources:WebResources, PromotionType %>" />
                                <asp:BoundField DataField="生效日期" HeaderText="<%$ Resources:WebResources, EffectiveDate %>" />
                                <asp:BoundField DataField="失效日期" HeaderText="<%$ Resources:WebResources, ExpiryDate %>" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="seperate">
                    </div>
                    <table border="0" id="productDetail" runat="server" style="display: none" width="90%">
                        <tr style="background-color:#780C0C; color:White; text-align:center">
                            <td>
                            </td>
                            <td>
                                <!--商品編號-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />
                            </td>
                            <td>
                                <!--商品名稱-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductName %>" />
                            </td>
                            <td>
                                <!--庫存量-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>" />
                            </td>
                            <td>
                                <!--價格-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Price %>" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品一
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品二
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品三
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品四
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品五
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                商品六
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList6_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <asp:Button ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" Visible="false" OnClientClick="window.close();return false;" />
                        <asp:Button ID="btnCalcel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" Visible="false" OnClientClick="window.close();return false;" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
