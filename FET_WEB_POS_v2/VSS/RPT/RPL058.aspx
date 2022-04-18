<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL058.aspx.cs" MasterPageFile="~/MasterPage.master"
    Inherits="VSS_RPT_RPL058" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--商品迴轉率表-->
        <asp:Literal ID="Literal1" runat="server" Text="商品迴轉率表"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal7" runat="server" Text="門市編號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    
                    <asp:Literal ID="Literal3" runat="server" Text="發票日期"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <div style="width: 120px;">
                                    <dx:ASPxDateEdit ID="txtOrdDateStart" runat="server">
                                        <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </div>
                            </td>
                            <td>
                                <asp:Literal ID="Literal32" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtOrdDateEnd" runat="server">
                                    <ValidationSettings CausesValidation="false">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, InvoiceAmount %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, EmployeeNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox8" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <table align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" /></dx:ASPxButton></td><td> <dx:ASPxButton ID="btnExport" runat="server" Text="匯出">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" align="left">
            <tr>
                <td>
                    <asp:Literal ID="Literal4" runat="server" Text="門市編號"></asp:Literal>：
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                 <td>
                    <asp:Literal ID="Literal2" runat="server" Text=" 發票日期"></asp:Literal>：
                    <asp:Label ID="Label1" runat="server" Text="	2010/11/1	～	2010/11/17	"></asp:Label>
                </td>
              
                <td>
                    <asp:Literal ID="Literal16" runat="server" Text=" <%$ Resources:WebResources, PrintDate %>"></asp:Literal>：
                    <asp:Label ID="Labelx" runat="server" Text="	2010/11/10 12:00	～	2010/11/11 12:00	"></asp:Label>
                </td>
            </tr>
            <tr>
                  <td>
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                     <asp:Literal ID="Literal19" runat="server" Text="稅籍編號"></asp:Literal>：
                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                </td>
                               
                <td>
                    <asp:Literal ID="Literal18" runat="server" Text=" <%$ Resources:WebResources, PrintPerson %>"></asp:Literal>：
                    <asp:Label ID="Label4" runat="server" Text="Kevin"></asp:Label>
                </td>
            </tr>
            <tr>
                 <td>
                    <asp:Literal ID="Literal23" runat="server" Text="營業人姓名"></asp:Literal>：
                    <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Literal ID="Literal20" runat="server" Text=" <%$ Resources:WebResources, Page %>"></asp:Literal>：
                    <asp:Label ID="Label6" runat="server" Text="	1 / 10	"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%">
        <Columns>


            <dx:GridViewDataColumn FieldName="發票日期" Caption="發票日期" />
            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, InvoiceNo %>" runat="server" Caption="<%$ Resources:WebResources, InvoiceNo %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="格式(電子三聯、收銀機二聯、手開二三聯)" Caption="格式(電子三聯、收銀機二聯、手開二三聯)">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, CashRegisterNo %>" Caption="<%$ Resources:WebResources, CashRegisterNo %>" />
            <dx:GridViewDataColumn FieldName="課稅別" Caption="<%$ Resources:WebResources, GTaxType %>" />
            <dx:GridViewDataColumn FieldName="客戶代碼" Caption="客戶代碼" />
            <dx:GridViewDataTextColumn FieldName="發票<%$ Resources:WebResources, TotalAmount %>" Caption="<%$ Resources:WebResources, InvoiceAmount %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="銷售額" Caption="<%$ Resources:WebResources, SellAmount %>" />
            <dx:GridViewDataColumn FieldName="稅額" Caption="<%$ Resources:WebResources, Tax %>" />
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, EmployeeNo %>" Caption="<%$ Resources:WebResources, EmployeeNo %>" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
</asp:Content>
