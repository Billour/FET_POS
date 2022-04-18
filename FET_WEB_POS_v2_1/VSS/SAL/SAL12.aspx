<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SAL12.aspx.cs" Inherits="VSS_SAL_SAL12" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=300,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
            <tr>
                <td align="left">
                    <!--商品單價查詢作業-->
                    <asp:Literal ID="Literal8" runat="server" Text="商品單價查詢"></asp:Literal>
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table style="width: 100%">
            <tr>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Value="0">寄銷商品</asp:ListItem>
                        <asp:ListItem Value="1">含過期料號</asp:ListItem>
                    </asp:CheckBoxList>
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
                    <asp:Literal ID="Literal6" runat="server" Text="商品分類"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" SelectedIndex="1">
                        <Items>
                            <dx:ListEditItem Text="2G" Selected="True" Value="0" />
                            <dx:ListEditItem Text="3G" Value="1" />
                            <dx:ListEditItem Text="3.5G" Value="2" />
                            <dx:ListEditItem Text="DatacardNetbook" Value="3" />
                            <dx:ListEditItem Text="Other" Value="4" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, EffectiveStartDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="postbackDate_TextBox1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="postbackDate_TextBox2" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <asp:Literal ID="Litera20" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductCategory"  />
                    <%--<table align="left" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnChooseProCategory" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                    AutoPostBack="false" SkinID="PopupButton" />
                            </td>
                        </tr>
                    </table>--%>

                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, EffectiveEndDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="Aspxdateedit1" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="Aspxdateedit2" runat="server">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品編號-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--廠商名稱-->
                    <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, SupplierName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100px">
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品名稱-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server" Width="100px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Price %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100px">
                                </dx:ASPxTextBox>
                            </td>
                            <td align="left">
                                &nbsp;
                            </td>
                            <td align="left">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100px">
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
    <div class="btnPosition">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                        OnClick="btnSearch_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                        AutoPostBack="False" />
                </td>
            </tr>
        </table>
    </div>
    <div class="seperate">
    </div>
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, TiredPointSetting %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <div>
    <cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="商品編號"
        Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>">
                <DataItemTemplate>
                    <%#Container.ItemIndex + 1%>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>">
            </dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="商品類別" Caption="<%$ Resources:WebResources, ProductCategory %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="單位" Caption="<%$ Resources:WebResources, Unit %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="檢核IMEI" Caption="<%$ Resources:WebResources, VerifyImei %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataCheckColumn FieldName="自訂價格" Caption="<%$ Resources:WebResources, CustomPrice %>" />
            <dx:GridViewDataCheckColumn FieldName="扣庫存" Caption="<%$ Resources:WebResources, ReduceInventory %>">
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataTextColumn FieldName="生效日起" Caption="<%$ Resources:WebResources, EffectiveStartDate %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="生效日訖" Caption="<%$ Resources:WebResources, EffectiveEndDate %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="價格" Caption="<%$ Resources:WebResources, Price %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="廠商名稱" Caption="<%$ Resources:WebResources, SupplierName %>">
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsPager PageSize="10" AlwaysShowPager="true">
        </SettingsPager>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
    </cc:ASPxGridView>
    </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="折扣查詢">
                    <ContentCollection>
                                    <dx:ContentControl>
                                        <div>
                                            <table width="100%">
                                                <tr>
                                                    <td class="tdtxt">
                                                    <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" colspan="4">
                                                        <dx:ASPxLabel ID="t" runat="server" Text="100010026"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt"><asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>：
                                                    </td>
                                                    <td class="tdval" colspan="4">
                                                        <dx:ASPxLabel ID="ASPxTextBox6" runat="server" Text="Motorola-T2688-NAD"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">費率：</td>
                                                    <td class="tdval" colspan="4">
                                                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem> Voice</asp:ListItem>
                                                            <asp:ListItem>Data</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">GA：</td>
                                                    <td class="tdval" colspan="4">
                                                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem>Postpaid FET-2G</asp:ListItem>
                                                            <asp:ListItem>Postpaid FET-3G</asp:ListItem>
                                                            <asp:ListItem>Prepaid</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">Loyalty：</td>
                                                    <td class="tdval" colspan="4">
                                                        <asp:CheckBoxList ID="CheckBoxList4" runat="server" 
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem>FET-2G</asp:ListItem>
                                                            <asp:ListItem>FET-3G</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">2轉3：</td>
                                                    <td class="tdval" colspan="4">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                <td>
                                                                   <asp:CheckBoxList ID="CheckBoxList5" runat="server">
                                                                        <asp:ListItem>FET-2G =&gt; FET-3G (Voice)</asp:ListItem>
                                                                        <asp:ListItem>KGT-2G =&gt; KGT-3G (Voice)</asp:ListItem>
                                                                        <asp:ListItem>FET Prepaid =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                                        <asp:ListItem>New Cash =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                                        <asp:ListItem>KGT Prepaid =&gt; FET-3G Postpaid (Voice)</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td valign="top">
                                                                    <asp:CheckBoxList ID="CheckBoxList6" runat="server">
                                                                        <asp:ListItem>FET-2G =&gt; FET-3G (Data)</asp:ListItem>
                                                                        <asp:ListItem>KGT-2G =&gt; KGT-3G (Data)</asp:ListItem>
                                                                        <asp:ListItem>FET Prepaid =&gt; FET-3G Postpaid (Data)</asp:ListItem>
                                                                        <asp:ListItem>KGT Prepaid =&gt; FET-3G Postpaid (Data)</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">MNP：</td>
                                                    <td class="tdval" colspan="4">&nbsp;<asp:CheckBox ID="CheckBox2" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">門市編號：</td>
                                                    <td colspan="4">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td><dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                <td>&nbsp;</td>
                                                                <td><dx:ASPxButton ID="ASPxButton3" runat="server" Text="..."></dx:ASPxButton></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">商品料號：</td>
                                                    <td class="tdval" colspan="4">
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td><dx:ASPxTextBox ID="ASPxTextBox31" runat="server" Width="100px"></dx:ASPxTextBox></td>
                                                                <td>&nbsp;</td>
                                                                <td><dx:ASPxButton ID="ASPxButton2" runat="server" Text="..."></dx:ASPxButton></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdtxt">客戶門號：</td>
                                                    <td class="tdval">
                                                        <dx:ASPxTextBox ID="ASPxTextBox51" runat="server" Width="100px"></dx:ASPxTextBox>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td class="tdtxt">ARPB金額：</td>
                                                    <td class="tdval">
                                                        <dx:ASPxTextBox ID="ASPxTextBox61" runat="server" Width="100px"></dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="btnPosition">
                                            <table align="center" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnSearchD" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                                            OnClick="btnSearchD_Click" />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton5" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                       
                                        <div class="seperate"></div>

                                        <div>
                                           <cc:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="gvMaster" KeyFieldName="項次" Width="100%">
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <%#Container.ItemIndex + 1%>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn FieldName="折扣料號" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn FieldName="折扣名稱" Caption="<%$ Resources:WebResources, DiscountName %>" VisibleIndex="2" />
                                                    <dx:GridViewDataColumn FieldName="折扣金額" Caption="<%$ Resources:WebResources, DiscountAmount %>" VisibleIndex="3" />
                                                    <dx:GridViewDataColumn FieldName="贈品/加價購" Caption="贈品/加價購" VisibleIndex="4" />
                                                </Columns>
                                                <SettingsPager PageSize="5"></SettingsPager>
                                                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                            </cc:ASPxGridView>
                                        </div>
                                    </dx:ContentControl>
                                </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>

</asp:Content>
