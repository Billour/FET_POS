<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SAL01_choosePromotions.aspx.cs"
    Inherits="VSS_SAL_SAL01_choosePromotions"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Pragma" content="no-cache"> 
    <title><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, MixPromotionAndProductInput %>" /></title>
    <script type="text/javascript">
        function ReturnValue(s, e) {
            if (window.document.getElementById("hidSelectProd1").value != "") {
                returnValue = "MixPromotion;" + window.document.getElementById("hidSelPromotion_Code").value + "^" + window.document.getElementById("hidSelPromotion_Name").value 
                                + ";" + window.document.getElementById("hidSelectProd1").value;
                if (window.document.getElementById("hidSelectProd2").value != "")
                    returnValue += ";" + window.document.getElementById("hidSelectProd2").value;
                if (window.document.getElementById("hidSelectProd3").value != "")
                    returnValue += ";" + window.document.getElementById("hidSelectProd3").value;
                if (window.document.getElementById("hidSelectProd4").value != "")
                    returnValue += ";" + window.document.getElementById("hidSelectProd4").value;
                if (window.document.getElementById("hidSelectProd5").value != "")
                    returnValue += ";" + window.document.getElementById("hidSelectProd5").value;
                if (window.document.getElementById("hidSelectProd6").value != "")
                    returnValue += ";" + window.document.getElementById("hidSelectProd6").value;
            } 
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                <asp:HiddenField ID="hidPromotion_Code" runat="server" />
                <asp:HiddenField ID="hidSelPromotion_Code" runat="server" />
                <asp:HiddenField ID="hidSelPromotion_Name" runat="server" />
                <asp:HiddenField ID="hidSelectProd1" runat="server" />
                <asp:HiddenField ID="hidSelectProd2" runat="server" />
                <asp:HiddenField ID="hidSelectProd3" runat="server" />
                <asp:HiddenField ID="hidSelectProd4" runat="server" />
                <asp:HiddenField ID="hidSelectProd5" runat="server" />
                <asp:HiddenField ID="hidSelectProd6" runat="server" />
                <asp:HiddenField ID="hidOldProdList" runat="server" />
                
                <div id="queryDiv" runat="server">
                    <table id="queryTable" runat="server">
                        <tr>
                            <td class="tdtxt">
                                <!--促銷代碼-->
                                <asp:Literal ID="lblPromotion_Code" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtQueryPromotion_Code" runat="server" CssClass="tbWidthFormat" ClientInstanceName="txtQueryPromotion_Code"
                                    AutoPostBack="false" Width="120">
                                </dx:ASPxTextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td class="tdtxt">
                                <!--促銷名稱-->
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="txtQueryPromotion_Name" runat="server" CssClass="tbWidthFormat" ClientInstanceName="txtQueryPromotion_Name"
                                    AutoPostBack="false" Width="120">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">
                                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                    UseSubmitBehavior="false" OnClick="btnSearch_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
        
                <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" 
                        KeyFieldName="UUID" AutoGenerateColumns="False" EnableCallBacks="false" 
                    Width="100%" OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" 
                        OnPageIndexChanged="gvMaster_PageIndexChanged" OnFocusedRowChanged="gvMaster_FocusedRowChanged">
                    <Columns>
                        <dx:GridViewDataDateColumn VisibleIndex="0" Caption="">
                            <DataItemTemplate>
                                <dx:ASPxRadioButton ID="rdoButton" GroupName="rdoGroup" ClientInstanceName="rdoButton" runat="server" />
                            </DataItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataColumn FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="PROMO_NO" Caption="<%$ Resources:WebResources, PromotionCode %>" VisibleIndex="2">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtPROMO_NO" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[PROMO_NO]") %>'
                                    runat="server">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="PROMO_NAME" Caption="<%$ Resources:WebResources, PromotionName %>" VisibleIndex="3">
                            <DataItemTemplate>
                                <dx:ASPxTextBox ID="txtPROMO_NAME" ReadOnly="true" Border-BorderStyle="None" Text='<%# Bind("[PROMO_NAME]") %>'
                                    runat="server">
                                </dx:ASPxTextBox>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="CATEGORY" Caption="<%$ Resources:WebResources, PromotionalCategory %>"
                            VisibleIndex="4" />
                        <dx:GridViewDataColumn FieldName="B_DATE" Caption="<%$ Resources:WebResources, EffectiveDate %>"
                            VisibleIndex="5" />
                        <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, ExpiryDate %>"
                            VisibleIndex="6" />                            
                    </Columns>
                    <SettingsPager PageSize="4">
                    </SettingsPager>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <Settings ShowVerticalScrollBar="false" VerticalScrollableHeight="100" VerticalScrollBarStyle="Virtual"/>
                    <SettingsBehavior AllowFocusedRow="True" />
                    <ClientSideEvents
                    FocusedRowChanged="function(s, e) {
                       if(s.GetFocusedRowIndex() == -1) {
                            e.processOnServer=false;
                            return false;
                       }
                       var row = s.GetRow(s.GetFocusedRowIndex());
                       
                        if(__aspxIE) {
                            row.cells[0].childNodes[0].checked = true;
                        } else {
                            row.cells[0].childNodes[1].checked = true;
                        }
                        e.processOnServer=true;
                    }" />
                </cc:ASPxGridView>
                    <div class="seperate">
                    </div>
                    <table border="0" id="productDetail" runat="server" width="90%" align="center">
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
                                <asp:DropDownList ID="ddlProdList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProdList1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblProdName1" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInvQty1" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPrice1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品二
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProdList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProdList2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblProdName2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInvQty2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPrice2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品三
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProdList3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProdList3_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblProdName3" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInvQty3" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPrice3" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品四
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProdList4" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProdList4_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblProdName4" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInvQty4" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPrice4" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                商品五
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProdList5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProdList5_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblProdName5" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInvQty5" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPrice5" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                商品六
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProdList6" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProdList6_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblProdName6" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInvQty6" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPrice6" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                    <ContentTemplate >
                        <table border="0" cellpadding="0" cellspacing="0" align="center">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, Ok %>" >
                                            <ClientSideEvents Click="function(s,e){ReturnValue(s, e);}" />
                                </dx:ASPxButton>                            
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxButton ID="btnCalcel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" >
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
