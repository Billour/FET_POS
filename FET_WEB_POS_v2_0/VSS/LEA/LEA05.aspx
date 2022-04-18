<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LEA05.aspx.cs" Inherits="VSS_LEA_LEA05" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--設備租賃作業-->
                        <asp:Literal ID="Literal11" runat="server" Text="設備租賃作業"></asp:Literal>
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
                        <!--租賃單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, LeaseOrderNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <span id="lblResDate">L2102-2010090701</span>
                    </td>
                    <td class="tdtxt">
                        <!--預約日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ReservationDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <span id="Span3">2010/09/07</span>
                    </td>
                    <td class="tdtxt">
                        <!--設定狀態-->
                        <asp:Literal ID="Literal19" runat="server" Text="設定狀態"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <span id="Span5">00-未存檔</span>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--手機類型-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, MobileType %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <span id="Span1">NOKIA</span>
                    </td>
                    <td class="tdtxt">
                        <!--手機序號-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, MobileIdentityNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <span id="Span4">011933000674639</span>
                    </td>
                    <td class="tdtxt">
                        <!--更新日期-->
                        <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <span id="Span6">2010/09/07 22:00</span>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--庫存地點-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StorageLocation %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <span id="Span2">遠企門市</span>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--維護人員-->
                        <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <span id="Span7">12345 王大寶</span>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--客戶門號-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, CustomerMobileNumber %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--客戶姓名-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox2" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--客戶等級-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, CustomerGrade %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox3" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        <!--性別-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Gender %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxRadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <Items>
                                <dx:ListEditItem Text="男" Selected="true" />
                                <dx:ListEditItem Text="女" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
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
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--預定領取日-->
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, CollectionDueDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxDateEdit ID="tbxResTakeDate" runat="server">
                        </dx:ASPxDateEdit>
                    </td>
                    <td class="tdtxt">
                        <!--預定歸還日-->
                        <asp:Literal ID="Literal16" runat="server" Text="預定歸還日"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxDateEdit ID="PostbackDate_TextBox1" runat="server">
                        </dx:ASPxDateEdit>
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
                        <!--領取方式-->
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, CollectionMethod %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxRadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <Items>
                                <dx:ListEditItem Text="親至門市" Selected="True" />
                                <dx:ListEditItem Text="快遞送貨" />
                            </Items>
                        </dx:ASPxRadioButtonList>
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
                        <!--地址-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Address %>"></asp:Literal>
                        ：
                    </td>
                    <td class="tdval" colspan="3">
                        <dx:ASPxTextBox ID="TextBox5" runat="server" Width="98%">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--出國時間-->
                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, DurationAbroad %>"></asp:Literal>：
                    </td>
                    <td class="tdval" colspan="1">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal27" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td align="left">
                                    <dx:ASPxDateEdit ID="tbxResTakeDate0" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <asp:Literal ID="Literal28" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td align="left">
                                    <dx:ASPxDateEdit ID="tbxResTakeDate1" runat="server">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
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
                                <!--實際領取日-->
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ActualCollectionDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="TextBox6" runat="server" AutoPostBack="true" OnTextChanged="TextBox6_TextChanged">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                <!--應收保證金：-->
                                <asp:Literal ID="Literal25" runat="server" Text="應收保證金"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Label ID="Label1" runat="server" Text="2000" Visible="false"></asp:Label>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--實際歸還日-->
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, ActualReturnDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxTextBox ID="TextBox4" runat="server" AutoPostBack="true" OnTextChanged="TextBox4_TextChanged">
                                </dx:ASPxTextBox>
                            </td>
                            <td class="tdtxt">
                                <!--應付租金-->
                                <asp:Literal ID="Literal22" runat="server" Text="應付租金"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Label ID="Label2" runat="server" Text="4000" Visible="false"></asp:Label>
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
                                <!--是否有賠償-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, CompensationRequired %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxRadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged">
                                    <Items>
                                        <dx:ListEditItem Selected="true" Text="否" Value="0"></dx:ListEditItem>
                                        <dx:ListEditItem Text="是" Value="1"></dx:ListEditItem>
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                            <td class="tdtxt">
                                <!--賠償金額-->
                                <asp:Literal ID="Literal26" runat="server" Text="賠償金額"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Label ID="Label3" runat="server" Text="100" Visible="false"></asp:Label>
                            </td>
                            <td class="tdtxt">
                                &nbsp;
                            </td>
                            <td class="tdval">
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="showList" runat="server" >
                            <td colspan="6">
                                <table width="35%">
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="gvList" runat="server" Width="40%" AutoGenerateColumns="False"
                                                KeyFieldName="賠償項目" ClientInstanceName="gvList">
                                                <Columns>
                                                    <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" VisibleIndex="0">
                                                    <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvList.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate></dx:GridViewCommandColumn>
                                                    <dx:GridViewDataColumn FieldName="賠償項目" Caption="賠償項目" VisibleIndex="1" />
                                                    <dx:GridViewDataColumn FieldName="金額" Caption="金額" VisibleIndex="2" />
                                                </Columns>
                                                <Templates>
                                                    <EmptyDataRow>
                                                        <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Label>
                                                    </EmptyDataRow>
                                                </Templates>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <dx:ASPxButton ID="Button1" Width="50px" runat="server" Text="<%$ Resources:WebResources, OK %>"
                                                OnClick="Button1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
            </table>
            <br />
        </div>
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, SaveReservation %>"
                            OnClick="btnSave_Click" Width="100px" Wrap="False" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReserCancel" Width="100px" runat="server" Text="<%$ Resources:WebResources, CancelReservation %>"
                            Wrap="False" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCheck" Width="100px" runat="server" Text="設備領取" PostBackUrl="~/VSS/SAL/SAL01.aspx"
                            Wrap="False" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReturn" Width="100px" runat="server" Text="設備歸還" PostBackUrl="~/VSS/SAL/SAL01.aspx"
                            Wrap="False" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" Width="100px" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                            Wrap="False" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <div class="GridScrollBar" style="height: auto">
            <asp:Panel ID="Panel2" runat="server" Visible="false">
                <asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" Width="60%">
                    <ItemTemplate>
                        <table class="mGrid" width="60%">
                            <tr>
                                <td align="center">
                                    <!--修改記錄-->
                                    <asp:Literal ID="Literal40" runat="server" Text="修改記錄"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <!--更新日期-->
                                    <asp:Literal ID="Literal33" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>
                                </td>
                                <td align="center">
                                    <!--工號-->
                                    <asp:Literal ID="Literal30" runat="server" Text="工號"></asp:Literal>
                                </td>
                                <td align="center">
                                    <!--姓名-->
                                    <asp:Literal ID="Literal31" runat="server" Text="姓名"></asp:Literal>
                                </td>
                                <td align="center">
                                    <!--說明-->
                                    <asp:Literal ID="Literal34" runat="server" Text="說明"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Literal ID="Literal32" runat="server" Text="2010/09/30"></asp:Literal>
                                </td>
                                <td align="center">
                                    <asp:Literal ID="Literal7" runat="server" Text="60736"></asp:Literal>
                                </td>
                                <td align="center">
                                    <asp:Literal ID="Literal19" runat="server" Text="王小明"></asp:Literal>
                                </td>
                                <td align="center">
                                    <asp:Literal ID="Literal35" runat="server" Text="保證金料號修改"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
            </asp:Panel>
        </div>
   
</asp:Content>
