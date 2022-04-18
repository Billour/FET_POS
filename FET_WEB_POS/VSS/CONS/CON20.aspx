<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON20.aspx.cs" Inherits="VSS_CON20_CON20" %>

<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">  
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=200,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品移出作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentStockTransferOut %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='CON19.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--移撥單號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:Label ID="lbOrderNo" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="tdtxt">
                        <!--撥入門市-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, TransfertO %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label5" runat="server" Text="2103 永和"></asp:Label>
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                         <asp:UpdatePanel ID="UpdatePanel4" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Label ID="Label2" runat="server" Text="未存檔"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">  
                       <%-- <!--撥入門市-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, TransferTo %>"></asp:Literal>：--%>
                    </td>
                    <td class="tdval">
                       <%-- <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Choose %>" OnClientClick="openwindow('../SAL/SAL01_chooseStore.aspx','400','350');return false;" />--%>
                    </td>
                    <td class="tdtxt">          
                    </td>
                    <td class="tdval">      
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label3" runat="server" Text="10/07/01 22:00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--更新人員-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:Label ID="Label4" runat="server" Text="12345 王大寶"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="seperate">
        </div>
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>--%>
                <div  id="Div1" runat="server" class="SubEditBlock" visible="true" >
                    <div class="SubEditCommand">
                        <asp:Button ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" />
                        <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                    </div>
                    <div class="GridScrollBar" style="height: auto">
                        <cc1:ExGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating">
                            <EmptyDataTemplate>
                                <tr>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        &nbsp;
                                    </th>
                                    <th scope="col">
                                        <!--商品類別-->
                                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品料號-->
                                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--移出數量-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, TransferredOutQuantity %>"></asp:Literal>
                                    </th>
                                </tr>
                               <tr id="trEmptyData" runat="server">
                                <td colspan="7" class="tdEmptyData">
                                   <%-- <!--查無資料，請修改條件，重新查詢-->
                                    <asp:Literal ID="Literal99" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>--%>
                                </td>
                            </tr>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="CheckAll" runat="server"  onclick="javascript:if(this.checked){$('#Div1').checkCheckboxes();}else{$('#Div1').unCheckCheckboxes();}" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>   
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderStyle-Width="30px" ItemStyle-Width="30px" FooterStyle-Width="30px">
                                <EditItemTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                        CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                        CommandName="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="<%$ Resources:WebResources, Edit %>"
                                        CommandName="Edit" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                                        CommandName="Save" OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                        OnClick="btnCancel_Click" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCategory %>" HeaderStyle-Width="60px" ItemStyle-Width="60px" FooterStyle-Width="60px">
                                <EditItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("商品類別") %>' Width="80px"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品類別")%>' Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtProductCatrgory" runat="server" Width="70px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-Width="60px" ItemStyle-Width="60px" FooterStyle-Width="60px">
                                <EditItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("商品料號") %>' Width="80px"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品料號")%>' Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtProductCode" runat="server" Width="70px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, ProductName %>" HeaderStyle-Width="60px" ItemStyle-Width="60px" FooterStyle-Width="60px">
                                <EditItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("商品名稱") %>' Width="80px"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("商品名稱")%>' Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtProductName" runat="server" Width="70px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:WebResources, TransferredOutQuantity %>" HeaderStyle-Width="60px" ItemStyle-Width="60px" FooterStyle-Width="60px">
                                <EditItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("移出數量") %>' Width="80px"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("移出數量")%>' Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTransferredOutQuantity" runat="server" Width="70px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            </Columns>
                        </cc1:ExGridView>
                    </div>
                </div>
          <%--  </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, ConfrimTransferOut %>" OnClick="btnSave_Click" />
            <%--<asp:Button ID="Button61" runat="server" Text="<%$ Resources:WebResources, ConfrimTransferOut %>" />--%>
            <asp:Button ID="Button62" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
            <asp:Button ID="Button7" runat="server" Text="<%$ Resources:WebResources, PrintTransferOutSlip %>" />
        </div>
    </div>
    </form>
</body>
</html>
