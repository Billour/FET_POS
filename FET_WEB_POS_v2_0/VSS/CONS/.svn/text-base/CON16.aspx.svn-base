<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON16.aspx.cs" Inherits="VSS_CON16_CON16" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
   
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--寄銷商品盤點作業-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductSockTaking %>"></asp:Literal>
                    </td>
                    <td align="right">
                        <dx:ASPxButton ID="btnQueryEdit" runat="server" 
                            Text="<%$ Resources:WebResources, QueryEdit %>" 
                            style="height: 23px" Width="70px" onclick="btnQueryEdit_Click"/>
                    </td>
                </tr>
            </table>
        </div>

        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">作業類型：</td>
                    <td class="tdval" colspan="2">
                        <asp:RadioButton ID="RadioButton4" runat="server" GroupName="TYPE" 
                        Text="列印(空白盤點單)" />
                        <asp:RadioButton ID="RadioButton5" runat="server" GroupName="TYPE" Text="盤點輸入" />
                    </td>
                    <td class="tdval"> 
                        </td>
                    <td class="tdtxt">
                    <!--狀態-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Status %>"></dx:ASPxLabel>：</td>
                    <td class="tdval">
                        <asp:Label ID="Label1" runat="server" Text="00 未存檔"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">盤點型態：</td>
                    <td class="tdval">
                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="InventoryType" 
                            Text="<%$ Resources:WebResources, Recount %>" AutoPostBack="false" 
                            oncheckedchanged="RadioButton1_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="InventoryType" 
                            Text="<%$ Resources:WebResources, CountAll %>" AutoPostBack="false" 
                            oncheckedchanged="RadioButton1_CheckedChanged" />
                    </td>
                    <td class="tdtxt">
                        <!--盤點單號-->
                    </td>
                    <td class="tdval">
                    
                    </td>
                    <td class="tdtxt">
                       
                        <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></dx:ASPxLabel>：</td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="Label8" runat="server" Text="2010/07/12 15:00"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--盤點日期-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, InventoryNo %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                                <dx:ASPxLabel ID="lblOrderNo" runat="server" />
                    </td>
                    <td class="tdtxt">
                        <!--盤點人員-->
                        <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CountedBy %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="Label6" runat="server" Text="王小明"></dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--日期-->
                        &nbsp;<dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></dx:ASPxLabel>：</td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="Label4" runat="server" Text="64591 李家駿"></dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--門市名稱-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, InventoryDate %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="Label5" runat="server" ></dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></dx:ASPxLabel>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxLabel ID="Label7" runat="server" Text="門市1"></dx:ASPxLabel>
                    </td>
                    <td class="tdtxt">
                        <!--人員-->
                        &nbsp;</td>
                    <td class="tdval">
                        &nbsp;</td>
                </tr>
            </table>
        </div>   
        <div class="seperate"></div>       
          <div class="btnPosition">
            <dx:ASPxButton ID="btnOk" runat="server" Text="<%$ Resources:WebResources, Ok %>" OnClick="btnOk_Click" />        
        </div>
        <div class="seperate"></div> 
        <div class="GridScrollBar" style="height: auto">
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="項次" 
            Width="100%" AutoGenerateColumns="False" 
            EnableRowsCache="False">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="廠商編號" runat="server" Caption="<%$ Resources:WebResources, SupplierNo %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="廠商名稱" runat="server" Caption="<%$ Resources:WebResources, SupplierName %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="商品編號" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="商品名稱" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="庫存量" runat="server" Caption="<%$ Resources:WebResources, StockQuantity %>"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="門市盤點量" runat="server" Caption="<%$ Resources:WebResources, PhysicalInventory %>">
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="TextBox3" runat="server" Text='<%# Bind("門市盤點量") %>' Width="100px"></dx:ASPxTextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="盤差量" runat="server" Caption="<%$ Resources:WebResources, DifferenceQuantity %>"></dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager PageSize="5" />
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
            </cc:ASPxGridView>
        </div>
         <div class="seperate"></div>
        <div class="btnPosition">
            <table>
                <tr>
                    <td><dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>" OnClick="btnSave_Click" CausesValidation="False" /></td>
                    <td><dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>" /></td>
                </tr>
            </table>
        </div>                          

</asp:Content>