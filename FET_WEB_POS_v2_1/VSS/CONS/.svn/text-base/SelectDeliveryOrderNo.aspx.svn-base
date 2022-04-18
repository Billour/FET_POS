<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectDeliveryOrderNo.aspx.cs" Inherits="VSS_CONS_SelectDeliveryOrderNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>選擇出貨編號</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="func">
        <div>
            <div class="GridScrollBar" style="height: 214px">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="mGrid">
                    <EmptyDataTemplate>
                        <tr>
                            <th scope="col">
                                <!--出貨編號-->
                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, DeliveryOrderNo %>"></asp:Literal>
                            </th>                                
                            
                        </tr>
                        <tr>
                            <td class="tdEmptyData">
                                <asp:Literal ID="litEmptyText" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                            </td>
                        </tr>
                    </EmptyDataTemplate>   
                    <Columns>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center">                           
                            <ItemTemplate>
                                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="RadioButton"  />
                            </ItemTemplate>                                
                        </asp:TemplateField>
                        <asp:BoundField DataField="出貨編號" HeaderText="<%$ Resources:WebResources, DeliveryOrderNo %>" />                            
                    </Columns>                                     
                </asp:GridView>             
            </div>
            <div class="seperate">
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnOk" runat="server" Text="<%$ Resources:WebResources, Ok %>"  OnClick="btnOk_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" OnClientClick="window.close();return false;" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
