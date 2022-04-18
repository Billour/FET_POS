<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PRE02.aspx.cs" Inherits="VSS_PRE_PRE02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--預購查詢作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, PreOrderSearch %>"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval" colspan="2">
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged"
                        Text="跨門市查詢(限單筆)" />
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--門市代號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxtextbox id="TextBox9" runat="server">
                    </dx:aspxtextbox>
                </td>
                <td class="tdtxt">
                    &nbsp;<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxtextbox id="TextBox7" runat="server">
                    </dx:aspxtextbox>
                </td>
                <td class="tdtxt">
                    <!--狀態-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Status %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxcombobox id="DropDownList1" runat="server" width="100">
                        <Items>
                            <dx:ListEditItem Selected="true" Value="預收款" Text="預收款" />
                            <dx:ListEditItem Value="預收款作廢" Text="預收款作廢" />
                            <dx:ListEditItem Value="已結帳" Text="已結帳" />
                        </Items>
                    </dx:aspxcombobox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, PreOrderSheetNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxtextbox id="TextBox1" runat="server">
                    </dx:aspxtextbox>
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, CustomerIdentifyNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxtextbox id="TextBox11" runat="server">
                    </dx:aspxtextbox>
                </td>
                <td class="tdtxt">
                    &nbsp;<asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, SalesClerk %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxcombobox id="DropDownList2" runat="server" width="100">
                        <Items>
                            <dx:ListEditItem Value="12345-王大寶" Text="12345-王大寶" Selected="true" />
                            <dx:ListEditItem Value="22345-王小寶" Text="22345-王小寶" />
                        </Items>
                    </dx:aspxcombobox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--預購日期-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, PreOrderDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:aspxdateedit id="postbackDate_TextBox1" runat="server">
                                </dx:aspxdateedit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:aspxdateedit id="postbackDate_TextBox2" runat="server">
                                </dx:aspxdateedit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tdtxt">
                    <!--客戶門號-->
                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxtextbox id="TextBox8" runat="server">
                    </dx:aspxtextbox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--統一編號-->
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxtextbox id="TextBox3" runat="server">
                    </dx:aspxtextbox>
                </td>
                
                <td class="tdtxt">
                    <!--聯絡電話-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxtextbox id="TextBox5" runat="server">
                    </dx:aspxtextbox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--發票號碼-->
                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:aspxtextbox id="TextBox2" runat="server">
                    </dx:aspxtextbox>
               </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <div class="btnPosition">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <tr>
                <td align="right">
                    <dx:aspxbutton id="btnQuery" runat="server" text="<%$ Resources:WebResources, Search %>"
                        onclick="btnQuery_Click">
                    </dx:aspxbutton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <dx:aspxbutton id="btnClear" runat="server" text="<%$ Resources:WebResources, Reset %>">
                    </dx:aspxbutton>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate"></div>
    <div>
        <div class="SubEditBlock">
            <div class="GridScrollBar" style="height: auto">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <dx:aspxgridview id="ASPxGridView1" runat="server" autogeneratecolumns="False" width="100%">
                            <Columns>
                                <dx:GridViewDataCheckColumn VisibleIndex="0">
                                    <DataItemTemplate>
                                        <input id="Radio" type="radio" name="SameRadio" value='<%#Eval("項次") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>"
                                    VisibleIndex="1" />
                                <dx:GridViewDataColumn FieldName="狀態" Caption="<%$ Resources:WebResources, Status %>"
                                    VisibleIndex="2" />
                                <dx:GridViewDataColumn FieldName="預購日期" Caption="<%$ Resources:WebResources, PreOrderDate %>"
                                    VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="預購單號" Caption="<%$ Resources:WebResources, PreOrderSheetNo %>"
                                    VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="客戶姓名" Caption="<%$ Resources:WebResources, CustomerName %>"
                                    VisibleIndex="5" />
                                <dx:GridViewDataColumn FieldName="客戶門號" Caption="<%$ Resources:WebResources, CustomerMobileNumber %>"
                                    VisibleIndex="6" />
                                <dx:GridViewDataColumn FieldName="聯絡電話" Caption="<%$ Resources:WebResources, ContactTelephone %>"
                                    VisibleIndex="7" />
                                <dx:GridViewDataColumn FieldName="預購金額" Caption="<%$ Resources:WebResources, PreOrderAmount %>"
                                    VisibleIndex="8" />
                                <dx:GridViewDataColumn FieldName="門市代號" Caption="<%$ Resources:WebResources, StoreNo %>"
                                    VisibleIndex="9" />
                                <dx:GridViewDataDateColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" VisibleIndex="10">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataColumn FieldName="銷售人員" Caption="<%$ Resources:WebResources, SalesClerk %>"
                                    VisibleIndex="10" />
                            </Columns>
                            <Templates>
                                <EmptyDataRow>
                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                </EmptyDataRow>
                                <TitlePanel>
                                    <table cellpadding="0" cellspacing="0" border="0" align="left">
                                        <tr>
                                            <td align="right">
                                                <dx:ASPxButton ID="btnAdd1" runat="server" Text="<%$ Resources:WebResources, Export %>" AutoPostBack="false">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </TitlePanel>
                            </Templates>
                            <Settings ShowTitlePanel="true" />
                        </dx:aspxgridview>
                        <div class="seperate"></div>
                        <div class="btnPosition">
                            <table cellpadding="0" cellspacing="0" border="0" align="center">
                                <tr>
                                    <td align="right">
                                        <dx:aspxbutton id="Button1" runat="server" visible="false" text="<%$ Resources:WebResources, View %>">
                                        </dx:aspxbutton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>    
</asp:Content>
