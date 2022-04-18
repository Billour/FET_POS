<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON14.aspx.cs" Inherits="VSS_CON14_CON14" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
    div.window {    
        position: absolute;            
        width: 400px; 
        height: 300px;           
        border-style:ridge;        
        border-color:White;         
    }

    div.titlebar {  /* Specifies position, size, and style of the titlebar */
        position: absolute;        /* It's a positioned element */
        top: 0px; height: 18px;    /* titlebar is 18px + padding and borders */
        width: 390px;              /* 290 + 5px padding on left and right = 300 */
        background-color: ActiveCaption;  /* Use system titlebar color */
        border-bottom: groove black 2px;  /* Titlebar has border on bottom only */
        padding: 3px 5px 2px 5px;  /* Values clockwise: top, right, bottom, left */
        color:CaptionText;             /* Use system font for titlebar */
        font-weight:bold;
    }

    div.content {   /* Specifies size, position and scrolling for window content */
        position: absolute;        /* It's a positioned element */
        top: 25px;                 /* 18px title+2px border+3px+2px padding */
        height: 265px;             /* 200px total - 25px titlebar - 10px padding */
        width: 390px;              /* 300px width - 10px of padding */
        padding: 5px;              /* allow space on all four sides */
        overflow: auto;            /* give us scrollbars if we need them */
        background-color: #ffffff; /* White background by default */
    }
  
    </style>   
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品進貨驗收作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventoryExamination %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='CON13.aspx';return false;" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--出貨編號-->
                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, DeliveryOrderNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Value="">-請選擇-</asp:ListItem>
                            <asp:ListItem>123456789</asp:ListItem>
                            <asp:ListItem>123456790</asp:ListItem>
                            <asp:ListItem>123456791</asp:ListItem>
                            <asp:ListItem>123456792</asp:ListItem>
                            <asp:ListItem>123456793</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                         <!--廠商名稱-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                        </td>
                        <td class="tdval"> <asp:Label ID="Label3" runat="server">V01001 AAA	
                            </asp:Label>
                        </td>
                                            
                        <td class="tdtxt">
                          
                            <!--狀態-->
                            <asp:Literal ID="Literal4" runat="server" 
                                Text="<%$ Resources:WebResources, Status %>"></asp:Literal>
                            ：</td>
                       
                        
                        <td class="tdval">
                            <asp:Label ID="lblStatus" runat="server" Text="未存檔"></asp:Label>
                        </td>
                        
                                                                                                                        
                    </tr>
                    <tr>
                        <td class="tdtxt">
                         <!--進貨日期-->
                            <asp:Literal ID="Literal6" runat="server" 
                                Text="<%$ Resources:WebResources, ReceivedDate %>"></asp:Literal>
                            ：</td>
                         
                        
                        <td class="tdval">
                        <asp:Label ID="Label2" runat="server" Text="2010/09/27"></asp:Label>
                        </td>
                        
                         <td class="tdtxt">
                           <!--訂單/主配編號-->
                            <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></asp:Literal>：
                        </td>
                        <!--td class="tdval">
                        </td-->
                        <td>  <asp:Label ID="lblOrderNo" runat="server"></asp:Label></td>
                        <td class="tdtxt">
                         <!--更新日期-->
                            <asp:Literal ID="Literal5" runat="server" 
                                Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                            ：                           
                        </td>
                                                                     
                        <td class="tdval">
                           <asp:Label ID="lblModifiedDate" runat="server" Text=""></asp:Label>
                        </td>
                      
                        
                    </tr>                    
                    <tr>
                           <td class="tdtxt"></td>
                        <td class="tdval"></td>
                       
                         <td class="tdtxt"></td>
                        <td class="tdval"></td>
                         <td class="tdtxt">
                            <!--驗收人員-->
                            <asp:Literal ID="Literal17" runat="server" 
                                Text="<%$ Resources:WebResources, ReceivedBy %>"></asp:Literal>
                            ：
                        </td>
                       
                        <td class="tdval">
                          <asp:Label ID="Label1" runat="server" Text="64591 李家駿"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                          <!--未驗收-->
                            <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, PendingApproval %>" Visible="false"></asp:Literal>
                        </td>
                        <!--td class="tdval">
                        </td-->
                        <td class="tdtxt" rowspan="4">
                         <asp:ListBox ID="ListBox1" runat="server" Visible="false">
                                <asp:ListItem Text="123456789"></asp:ListItem>
                                <asp:ListItem Text="123456790"></asp:ListItem>
                                <asp:ListItem Text="123456791"></asp:ListItem>
                                <asp:ListItem Text="123456792"></asp:ListItem>
                                <asp:ListItem Text="123456793"></asp:ListItem>
                            </asp:ListBox>
                        </td>
                        <td class="tdval">
                        </td>
                        <td class="tdtxt">
                           
                        </td>
                         <td class="tdtxt"></td>
                        <td class="tdval"></td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                        </td>
                        <!--td class="tdval">
                        </td-->
                        <td class="tdtxt">
                        </td>
                        <td class="tdval">
                        </td>
                         <td class="tdtxt"></td>
                        <td class="tdval"></td>
                    </tr>
                </table>
            </div>
            <div>
               
                        <%--<!--出貨單號-->
                        <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, DeliveryOrderNo %>"></asp:Literal>：
                    
                        <asp:Label ID="lblDeliveryOrderNo" runat="server" Text=""></asp:Label>--%>
                    
            </div>
            <div class="seperate"></div>
                <div class="GridScrollBar" style="height: 350px">
                        <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                            OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                            OnRowUpdating="gvMaster_RowUpdating"> 
                            <EmptyDataTemplate>
                                <tr>                                                                                                                
                                    <th scope="col">
                                        <!--商品編號-->
                                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--商品名稱-->
                                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                    </th>
                                     <th scope="col">
                                        <!--廠商編號-->
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--廠商名稱-->
                                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>
                                    </th>
                                     <th scope="col">
                                        <!--到貨量-->
                                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ArrivalQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--驗收量-->
                                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, InspectionQuantity %>"></asp:Literal>
                                    </th>
                                    <th scope="col">
                                        <!--備註-->
                                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, Remark %>"></asp:Literal>
                                    </th>
                                    <tr>
                                        <td colspan="7" class="tdEmptyData">
                                            <!--此無明細資料-->
                                            <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </tr>
                            </EmptyDataTemplate>
                            <Columns>    
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, DeliveryOrderNo %>" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("進貨編號") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("進貨編號") %>' Width="90"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                      
                                <asp:BoundField DataField="商品編號" HeaderText="<%$ Resources:WebResources, ProductCode %>" ReadOnly="true" />
                                <asp:BoundField DataField="商品名稱" HeaderText="<%$ Resources:WebResources, ProductName %>" ReadOnly="true" />
                                <asp:BoundField DataField="廠商編號" HeaderText="<%$ Resources:WebResources, SupplierNo %>" ReadOnly="true" />
                                <asp:BoundField DataField="廠商名稱" HeaderText="<%$ Resources:WebResources, SupplierName %>" ReadOnly="true" />  
                                <asp:BoundField DataField="到貨量" HeaderText="<%$ Resources:WebResources, ArrivalQuantity %>" ReadOnly="true" />                              
                               
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, InspectionQuantity %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("驗收量") %>' Width="90"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("驗收量") %>' Width="90"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:WebResources, Remark %>">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("備註") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("備註") %>'></asp:TextBox>                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
            <div class="seperate"></div>
            </div>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" onclick="btnSave_Click" />
                <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, ConfirmReceive %>" Visible="false" />
                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
                <asp:Button ID="Button4" runat="server" Text="<%$ Resources:WebResources, PrintBarCode %>" />
            </div>
        
    </div>
    
    
    
 
        <asp:Panel ID="ModalPopupPanel" runat="server" Visible="false">                      
        </asp:Panel>
    
    </form>
</body>
</html>
