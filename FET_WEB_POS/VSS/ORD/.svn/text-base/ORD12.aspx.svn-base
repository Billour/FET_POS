<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD12.aspx.cs" Inherits="VSS_ORD_ORD12" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        div.window
        {
            position: absolute;
            width: 400px;
            height: 300px;
            border-style: ridge;
            border-color: White;
        }
        div.titlebar
        {
            /* Specifies position, size, and style of the titlebar */
            position: absolute; /* It's a positioned element */
            top: 0px;
            height: 18px; /* titlebar is 18px + padding and borders */
            width: 390px; /* 290 + 5px padding on left and right = 300 */
            background-color: ActiveCaption; /* Use system titlebar color */
            border-bottom: groove black 2px; /* Titlebar has border on bottom only */
            padding: 3px 5px 2px 5px; /* Values clockwise: top, right, bottom, left */
            color: CaptionText; /* Use system font for titlebar */
            font-weight: bold;
        }
        div.content
        {
            /* Specifies size, position and scrolling for window content */
            position: absolute; /* It's a positioned element */
            top: 25px; /* 18px title+2px border+3px+2px padding */
            height: 265px; /* 200px total - 25px titlebar - 10px padding */
            width: 390px; /* 300px width - 10px of padding */
            padding: 5px; /* allow space on all four sides */
            overflow: auto; /* give us scrollbars if we need them */
            background-color: #ffffff; /* White background by default */
        }
        .messagemodalbackground
        {
            background-color: Gray;
            filter: alpha(opacity=0);
            opacity: 1;
            z-index: 998;
        }
    </style>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>
    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=150,left=250,resizable=yes,scrollbars=yes,location=no,toolbar=no,status=no');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <div class="func">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--預訂貨作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PrePurchaseOrder %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, OrderSearch %>"
                            OnClientClick="document.location='ORD02.aspx';return false;" />
                    </td>
                </tr>
            </table>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--訂單編號-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OrderNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Label ID="lblOrderNo" runat="server" Text="PR2101-10070101"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="tdtxt">
                            <!--訂單日期-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="lblOrderDate" runat="server">2010/07/01 22:00</asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--狀態-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label2" runat="server" Text="預訂"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--備註-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>：
                        </td>
                        <td colspan="3" class="tdval" rowspan="2">
                            <asp:TextBox ID="txtMemo" runat="server" Width="98%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="tdtxt">
                            <!--更新日期-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label3" runat="server" Text="10/07/12 15:00"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                        <td class="tdtxt">
                            <!--更新人員-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label4" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="SubEditCommand" id="showEditCommand" runat="server">
            <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>"
                OnClick="btnNew_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <div id="divContent" runat="server" class="SubEditBlock">
                    <div class="GridScrollBar" style="height: 192px">
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowDataBound="gvMaster_OnRowDataBound" OnRowUpdating="gvMaster_RowUpdating"
                            OnSelectedIndexChanged="gvMaster_SelectedIndexChanged" 
                            OnRowCommand="gvMaster_RowCommand" ShowFooterWhenEmpty="False" 
                            ShowHeaderWhenEmpty="False">
                            <EmptyDataTemplate>
                                <tr>
                                <th></th>
                                    <th></th>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        <!--項次-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Items %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--網購需求量-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, EconomicOrderQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市庫存量-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, StockQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--在途量-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, OnOrderQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--預訂量-->
                                        <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, PreOrderQuantity %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr id="trEmptyData" runat="server">
                                    <td colspan="10" class="tdEmptyData">
                                        <asp:Literal ID="litEmptyText" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            
                            <Columns>
                            
                            <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <FooterTemplate>                                    
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px" ControlStyle-Width="40px"
                                    FooterStyle-Width="40px" FooterStyle-Wrap="true">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>"
                                            CommandName="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                            CommandName="Update" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            CommandName="Cancel" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                            CommandName="Save" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                            OnClick="btnCancel_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelect" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>"
                                            CommandName="Select" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <FooterTemplate>
                                        <asp:Button ID="btnSelect1" runat="server" Text="<%$ Resources:WebResources, ThrowIn %>"
                                            CommandName="frSelect" Enabled="false"/>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("項次") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label51" runat="server" Text="1"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="label" runat="server" Text='<%# Bind("商品編號") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                        <asp:Button ID="Button8" runat="server" Text="選" OnClientClick="openwindow('ORD01_searchProductNo.aspx',640,300);return false;" />
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductName %>" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="label1" runat="server" Text='<%# Bind("商品名稱") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, EconomicOrderQuantity %>"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="label2" runat="server" Text='<%# Bind("網購需求量") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="label21" runat="server" Text='9' Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, StockQuantity %>" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="label3" runat="server" Text='<%# Bind("門市庫存量") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="label31" runat="server" Text='6' Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, OnOrderQuantity %>" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="label4" runat="server" Text='<%# Bind("在途量") %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="label41" runat="server" Text='6' Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, PreOrderQuantity %>" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPreOrderQty" runat="server" Text='<%# Eval("預訂量") %>' Width="40"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text="" Width="40px"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                        </cc1:ExGridView>
                    </div>
                    <br />
                    <!--搭配商品-->
                    <div id="showDetailGv" runat="server">
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, CollocationProduct %>"></asp:Literal>
                        <div class="GridScrollBar">
                            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="False" CssClass="mGrid">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--商品編號-->
                                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--商品名稱-->
                                            <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                        </th>
                                       <%-- <th scope="col">
                                            <!--搭配量-->
                                            <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, CollocationQuantity %>"></asp:Literal>
                                        </th>--%>
                                        <th scope="col">
                                            <!--訂購量-->
                                            <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, OrderQuantity %>"></asp:Literal>
                                        </th>
                                         <tr id="trEmptyData" runat="server">
                                        <td colspan="4" class="tdEmptyData">
                                            <asp:Literal ID="litEmptyText" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                        </td>
                                    </tr>
                                    
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                                    <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                                   <%-- <asp:BoundField DataField="搭配量" HeaderText="<%$ Resources:WebResources, CollocationQuantity %>" />--%>
                                    <asp:BoundField DataField="訂購量" HeaderText="<%$ Resources:WebResources, OrderQuantity %>" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="seperate">
                </div>
                <div class="btnPosition" id="showBtnFooter" runat="server">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnDrop" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Transfer %>" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button runat="server" ID="HiddenTargetControlForModalPopup" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopup" runat="server" TargetControlID="HiddenTargetControlForModalPopup"
            PopupControlID="ModalPopupPanel" BackgroundCssClass="messagemodalbackground"
            DropShadow="true" OkControlID="OkButton" CancelControlID="CancelButton" />
        <asp:Panel ID="ModalPopupPanel" runat="server">
            <div class="window" style="left: 10px; top: 10px; z-index: 10; background-color: White">
                <div class="titlebar">
                    <!--網購商品訂貨-->
                    <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, EconomicOrder %>"></asp:Literal>
                </div>
                <div class="content" style="background-color: #ffffff;">
                    <div class="GridScrollBar" style="height: 192px">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="mGrid">
                            <EmptyDataTemplate>
                                <tr>
                                
                                
                                    <th scope="col">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--需求量-->
                                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, QuantityDemanded %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--門市庫存量-->
                                        <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, StoreStockQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--訂購量-->
                                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, OrderQuantity %>"></asp:Literal>
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="5" class="tdEmptyData">
                                        <asp:Literal ID="litEmptyText" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server" Checked="true" onclick="javascript:if(this.checked){$('#ModalPopupPanel').checkCheckboxes();}else{$('#ModalPopupPanel').unCheckCheckboxes();}" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckItem" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" />
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" />
                                <asp:BoundField DataField="需求量" HeaderText="<%$ Resources:WebResources, QuantityDemanded %>" />
                                <asp:BoundField DataField="門市庫存量" HeaderText="<%$ Resources:WebResources, StoreStockQuantity %>" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <!--訂購量-->
                                        <asp:Literal ID="litEmptyText" runat="server" Text="<%$ Resources:WebResources, OrderQuantity %>"></asp:Literal>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("訂購量") %>' Width="40"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="btnPosition">
                        <asp:Button ID="OkButton" runat="server" Text="<%$ Resources:WebResources, Ok %>" />
                        <asp:Button ID="CancelButton" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
