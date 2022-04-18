<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON14.aspx.cs" Inherits="VSS_CON14_CON14" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    <div class="func">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品進貨驗收作業-->
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, InventoryExamination %>"></dx:ASPxLabel>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="Button1" runat="server" 
                            Text="<%$ Resources:WebResources, QueryEdit %>" onclick="Button1_Click" 
                            style="height: 23px" Width="70px"/>
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
                            <dx:ASPxLabel ID="Literal16" runat="server" Text="<%$ Resources:WebResources, DeliveryOrderNo %>"></dx:ASPxLabel>：
                        </td>
                        <td class="tdval">
                            <dx:ASPxComboBox ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                <Items>
                                    <dx:ListEditItem Text="-請選擇-" Selected="true" />
                                    <dx:ListEditItem Text="123456789" />
                                    <dx:ListEditItem Text="123456790" />
                                    <dx:ListEditItem Text="123456791" />
                                    <dx:ListEditItem Text="123456792" />
                                    <dx:ListEditItem Text="123456793" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                                            
                        <td class="tdtxt">
                          
                            <!--狀態-->
                            <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></dx:ASPxLabel>：</td>

                        <td class="tdval">
                            <dx:ASPxLabel ID="lblStatus" runat="server" Text="未存檔"></dx:ASPxLabel>
                        </td>
                                                                                                                        
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--廠商名稱-->
                            <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></dx:ASPxLabel>：
                        </td>
                        <td>
                         <dx:ASPxLabel ID="Label3" runat="server" Text="V01001 AAA"></dx:ASPxLabel>
                        </td>
                        <td class="tdtxt">
                         <!--日期-->
                            <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Date %>"></dx:ASPxLabel>：
                        </td>
                                                                     
                        <td class="tdval">
                           <dx:ASPxLabel ID="lblModifiedDate" runat="server" Text="10/07/12 15:00"></dx:ASPxLabel>
                        </td>
                        
                    </tr>                    
                    <tr>
                         <td class="tdtxt">
                           <!--訂單/主配編號-->
                            <dx:ASPxLabel ID="Literal14" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></dx:ASPxLabel>：
                        </td>
                        <!--td class="tdval">
                        </td-->
                        <td>  <dx:ASPxLabel ID="lblOrderNo" runat="server"></dx:ASPxLabel></td>
                        <td class="tdtxt">
                            <!--驗收人員-->
                            <dx:ASPxLabel ID="Literal17" runat="server" 
                                Text="<%$ Resources:WebResources, ReceivedBy %>"></dx:ASPxLabel>
                            ：
                        </td>
                       
                        <td class="tdval">
                          <dx:ASPxLabel ID="Label1" runat="server" Text="64591 李家駿"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                          <!--未驗收-->
                            <dx:ASPxLabel ID="Literal21" runat="server" Text="<%$ Resources:WebResources, PendingApproval %>" Visible="false"></dx:ASPxLabel>
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
                    </tr>
                </table>
            </div>
            <div>
               
                        <%--<!--出貨單號-->
                        <dx:ASPxLabel ID="Literal19" runat="server" Text="<%$ Resources:WebResources, DeliveryOrderNo %>"></dx:ASPxLabel>：
                    
                        <asp:Label ID="lblDeliveryOrderNo" runat="server" Text=""></asp:Label>--%>
                    
            </div>
            <div class="seperate"></div>
                <div class="GridScrollBar" style="height: 350px">
                    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server"  
                    Width="100%" AutoGenerateColumns="False" 
                    EnableRowsCache="False">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="廠商編號" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="實際到貨數量" runat="server" Caption="<%$ Resources:WebResources, ActualArrivalQuantity %>">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="TextBox6" runat="server" Text='<%# Bind("實際到貨數量") %>' Width="90"></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="備註" runat="server" Caption="<%$ Resources:WebResources, Remark %>">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="TextBox9" runat="server" Text='<%# Bind("備註") %>'></dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager PageSize="10" />
                        <SettingsEditing Mode="Inline" />
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    </cc:ASPxGridView>
                </div>
            <div class="seperate"></div>
            </div>
            <div class="btnPosition">
                <table>
                    <tr>
                        <td><dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" onclick="btnSave_Click" /></td>
                        <td><dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, ConfirmReceive %>" Visible="false" /></td>
                        <td><dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" /></td>
                        <td><dx:ASPxButton ID="Button4" runat="server" Text="<%$ Resources:WebResources, PrintBarCode %>" /></td>
                    </tr>
                </table> 
            </div>
        
    </div>
    
    
    
 
        <asp:Panel ID="ModalPopupPanel" runat="server" Visible="false">                      
        </asp:Panel>
</asp:Content>