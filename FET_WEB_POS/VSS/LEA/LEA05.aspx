<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA05.aspx.cs" Inherits="VSS_LEA_LEA05" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>

    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="func">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--設備租賃作業-->
                        <asp:Literal ID="Literal11" runat="server" Text="設備租賃作業"></asp:Literal>
                    </td>
                    <%--<td align="right">
                        <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>" OnClientClick="document.location='LEA04.aspx';return false;" />
                    </td>--%>
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
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--客戶姓名-->
                        <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, CustomerName %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                        <!--性別-->
                        <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Gender %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Selected="True">男</asp:ListItem>
                            <asp:ListItem>女</asp:ListItem>
                        </asp:RadioButtonList>
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
                        <cc1:postbackDate_TextBox ID="tbxResTakeDate" runat="server" ImageUrl="~/Icon/calendar.jpg" />
                    </td>
                    <td class="tdtxt">
                        <!--預定歸還日-->
                        <asp:Literal ID="Literal16" runat="server" Text="預定歸還日"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox1" runat="server" ImageUrl="~/Icon/calendar.jpg" />
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
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Selected="True">親至門市</asp:ListItem>
                            <asp:ListItem>快遞送貨</asp:ListItem>
                        </asp:RadioButtonList>
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
                        <asp:TextBox ID="TextBox5" runat="server" Width="98%"></asp:TextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval" colspan="3">
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
                    <td class="tdval" colspan="3">
                        <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                        <asp:TextBox ID="TextBox7" runat="server" Width="100" Text="2010/09/08"></asp:TextBox><img id="img1" src="~/Icon/calendar.jpg" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox7" PopupButtonID="img1" Format="yyyy/MM/dd">
                        </asp:CalendarExtender>                                                                       
                        &nbsp;<asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                        <asp:TextBox ID="TextBox8" runat="server" Width="100" Text="2010/09/08"></asp:TextBox><img id="img2" src="~/Icon/calendar.jpg" runat="server" />
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox8" PopupButtonID="img2" Format="yyyy/MM/dd">
                        </asp:CalendarExtender>
                                                
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
                <asp:UpdatePanel ID="upACD" runat="server">
                    <ContentTemplate>
                        <tr>
                            <td class="tdtxt">
                                <!--實際領取日-->
                                <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, ActualCollectionDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:TextBox ID="TextBox6" runat="server" AutoPostBack="true" OnTextChanged="TextBox6_TextChanged"></asp:TextBox>
                                <%--                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox2" runat="server" ImageUrl="~/Icon/calendar.jpg"
                            isAutoPostBackCheck="true" OnOnBlur="PostbackDate_TextBox2_OnBlur" />
--%>
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
                                <asp:TextBox ID="TextBox4" runat="server" AutoPostBack="true" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
                                <%--                        <cc1:postbackDate_TextBox ID="PostbackDate_TextBox3" runat="server" ImageUrl="~/Icon/calendar.jpg"
                            isAutoPostBackCheck="true" OnOnBlur="PostbackDate_TextBox3_OnBlur" />
--%>
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
                                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList3_SelectedIndexChanged">
                                    <asp:ListItem>是</asp:ListItem>
                                    <asp:ListItem>否</asp:ListItem>
                                </asp:RadioButtonList>
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
                        <tr id="showList" runat="server" visible="false">
                            <td colspan="6">
                                <table width="35%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvList" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                                                Width="30%">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="CheckALL" runat="server" CssClass="CompenItems" onclick="javascript:if(this.checked){$('.CompenItems').checkCheckboxes();}else{$('.CompenItems').unCheckCheckboxes();}" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox2" runat="server" CssClass="CompenItems" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="賠償項目" DataField="賠償項目" />
                                                    <asp:BoundField HeaderText="金額" DataField="金額" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, OK %>"
                                                OnClick="Button1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </table>
            <br />
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:WebResources, SaveReservation %>" OnClick="btnSave_Click"/>
                <asp:Button ID="btnReserCancel" runat="server" Text="<%$ Resources:WebResources, CancelReservation %>" />
                <asp:Button ID="btnCheck" runat="server" Text="設備領取" PostBackUrl="~/VSS/SAL/SAL01.aspx"/>
                <asp:Button ID="btnReturn" runat="server" Text="設備歸還" PostBackUrl="~/VSS/SAL/SAL01.aspx"/>
                <asp:Button ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" />
            </div>
        </div>
        <div class="seperate">
        </div>
            <div class="GridScrollBar" style="height: auto">
        <asp:Panel ID="Panel2" runat="server" Visible="false">
            <asp:FormView ID="FormView1" runat="server" DefaultMode="ReadOnly" Width="60%">
                <ItemTemplate>
                    <table class="mGrid" width="60%">
                        <tr>
                        <td  align="center" >
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
        
    </form>
</body>
</html>
