<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL072.aspx.cs" Inherits="VSS_RPT_RPL072" MasterPageFile="~/MasterPage.master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--寄銷商品退倉簽收單-->
        <asp:Literal ID="Literal1" runat="server" Text="寄銷商品退倉簽收單"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
           
         
            <tr>
                <td class="tdtxt">
                    <!--門市-->
                    <asp:Literal ID="Literal14" runat="server" Text="退倉單號"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="120px">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="0" Selected="true" />
                        </Items>
                    </dx:ASPxComboBox>
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
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" /></dx:ASPxButton>
                    </td><td> <dx:ASPxButton ID="btnExport" runat="server" Text="匯出">
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
                    <asp:Literal ID="Literal10" runat="server" Text=" 退倉單號"></asp:Literal>：
                    <asp:Label ID="Label2" runat="server" Text="		"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal3" runat="server" Text=" 退倉日期"></asp:Literal>：
                    <asp:Label ID="Label1" runat="server" Text="	2010/11/1	～	2010/11/17	"></asp:Label>
                </td>
                <td>
                    <asp:Literal ID="Literal2" runat="server" Text=" <%$ Resources:WebResources, PrintDate %>"></asp:Literal>：
                    <asp:Label ID="Labelx" runat="server" Text="	2010/11/10 12:00	～	2010/11/11 12:00	"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal4" runat="server" Text=""></asp:Literal>
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Literal ID="Literal8" runat="server" Text=" <%$ Resources:WebResources, PrintPerson %>"></asp:Literal>
                    <asp:Label ID="Label4" runat="server" Text="Kevin"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal23" runat="server" Text=""></asp:Literal>：
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


            <dx:GridViewDataTextColumn FieldName="<%$ Resources:WebResources, ProductCategory %>" Caption="<%$ Resources:WebResources, ProductCategory %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, ProductNo %>" Caption="<%$ Resources:WebResources, ProductNo %>" />
            <dx:GridViewDataColumn FieldName="單位" Caption="單位" />
            <dx:GridViewDataColumn FieldName="退倉數量" Caption="<%$ Resources:WebResources, ReturnQuantity %>" />
            <dx:GridViewDataColumn FieldName="實收數量" Caption="實收數量" />
                  
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
</asp:Content>
