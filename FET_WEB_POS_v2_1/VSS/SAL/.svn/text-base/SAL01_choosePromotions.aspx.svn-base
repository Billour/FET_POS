<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_choosePromotions.aspx.cs"
    Inherits="VSS_SAL_SAL01_choosePromotions"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, MixPromotionAndProductInput %>" /></title>
        <script type="text/javascript">
        function OnInit(s, e) {
            s.GetInputElement().name = "radioChoose";
        }
    </script>
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
                            <dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox>
                        </td>
                        <td class="tdtxt">
                            <!--促銷名稱-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PromotionName %>" />：
                        </td>
                        <td >
                            <dx:ASPxTextBox ID="TextBox6" runat="server"></dx:ASPxTextBox>
                        </td>
                        <td >
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click">
                            </dx:ASPxButton>
                    </tr>
                </table>
            </div>
            <div class="seperate">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" AutoGenerateColumns="False"
                    Width="100%">
                    <Columns>
                        <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                            <DataItemTemplate>
                                <dx:ASPxRadioButton ID="radioChoose" runat="server" ClientInstanceName="rc1" GroupName="radioChoose" AutoPostBack="true" OnCheckedChanged="radioChoose_CheckedChanged">
                                    <ClientSideEvents Init="OnInit" />
                                </dx:ASPxRadioButton>
                            </DataItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="促銷代碼" Caption="<%$ Resources:WebResources, PromotionCode %>"
                            VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="促銷名稱" Caption="<%$ Resources:WebResources, PromotionName %>"
                            VisibleIndex="3" />
                        <dx:GridViewDataColumn FieldName="促銷類別" Caption="<%$ Resources:WebResources, PromotionalCategory %>"
                            VisibleIndex="4" />
                        <dx:GridViewDataColumn FieldName="生效日期" Caption="<%$ Resources:WebResources, EffectiveDate %>"
                            VisibleIndex="5" />
                        <dx:GridViewDataColumn FieldName="失效日期" Caption="<%$ Resources:WebResources, ExpiryDate %>"
                            VisibleIndex="6" />                            
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <Settings ShowVerticalScrollBar="false" VerticalScrollableHeight="100" VerticalScrollBarStyle="Virtual"/>
                </cc:ASPxGridView>
                    <div class="seperate">
                    </div>
                    <table border="0" id="productDetail" runat="server" style="display: none" width="90%" align="center">
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
                    <ContentTemplate >
                        <table border="0" cellpadding="0" cellspacing="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" Visible="false" >
                                            <ClientSideEvents Click="function(s,e){window.close();}" />
                                </dx:ASPxButton>                            
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxButton ID="btnCalcel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" Visible="false" >
                                            <ClientSideEvents Click="function(s,e){window.close();}" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
