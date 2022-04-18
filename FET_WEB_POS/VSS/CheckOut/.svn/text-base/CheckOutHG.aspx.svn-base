<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckOutHG.aspx.cs" Inherits="VSS_CheckOut_CheckOutHG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HappyGo資料輸入</title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(function() {
            $("#btnStep3_OK").click(function() {
                window.close();
            });
        });
        $(function() {
            $("#DropDownList1").change(function() {
                if ($(this).val() != "") {
                    var rate = $(this).val();
                    var point = $("#Label2").text()
                    $("#Label3").text(rate * point + "元(" + point + "點)");
                    $("#HiddenField1").val(rate * point + "元(" + point + "點)");
                }
                else {
                    $("#Label3").text("");
                    $("#HiddenField1").val("");
                }
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="seperate">
    </div>
    <div class="checkOutDiv">
        <div>
            <asp:Wizard ID="Wizard1" runat="server" OnNextButtonClick="Wizard1_NextButtonClick"
                DisplaySideBar="false" Width="100%" FinishCompleteButtonText="<%$ Resources:WebResources, Ok %>"
                OnFinishButtonClick="Wizard1_FinishButtonClick">
                <StartNavigationTemplate>
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                        CommandName="MoveNext" />
                </StartNavigationTemplate>
                <StepNavigationTemplate>
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                       CommandName="CloseWin" OnClientClick="javascript:window.close();" />
                    <asp:Button ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                        CommandName="CloseWin" OnClientClick="javascript:window.close();" />
                   
                    <asp:Button ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                        CommandName="ResetValue" OnClick="btnReset_Click"/>
                </StepNavigationTemplate>
                <NavigationStyle HorizontalAlign="Center" />
                <WizardSteps> 
                    <asp:WizardStep ID="Step1" runat="server" StepType="Start">
                        <table align="center">
                            <tr>
                                <td style="width: 200px; height: 200px; background-color: Silver" align="center"
                                    valign="middle">
                                    請過卡
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep ID="Step2" runat="server" StepType="Step">
                        <table>
                            <tr>
                                <td class="tdtxt">
                                    HG卡號：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label7" runat="server" Text="1111-2222-3333-4444"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">
                                    剩餘點數：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label8" runat="server" Text="2000"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">
                                    欲兌金額/點數：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label9" runat="server" Text="300元(540點)" ForeColor="Red"></asp:Label>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div class="seperate">
                        </div>
                        <div>
                            <asp:GridView ID="GridView1" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                                Caption="單商品折抵活動" CaptionAlign="Left" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="項次" HeaderText="項次" />
                                    <asp:BoundField DataField="活動名稱" HeaderText="活動名稱" />
                                    <asp:BoundField DataField="項目名稱" HeaderText="項目名稱" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="數量">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Width="25px" Text='<%# Bind("數量") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="GridView2" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                                Caption="促銷商品折抵活動" CaptionAlign="Left" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="項次" HeaderText="項次" />
                                    <asp:BoundField DataField="活動名稱" HeaderText="活動名稱" />
                                    <asp:BoundField DataField="項目名稱" HeaderText="項目名稱" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="數量">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Width="25px" Text='<%# Bind("數量") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="gvMaster" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                                Caption="一般兌點通則" CaptionAlign="Left" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="項次" HeaderText="項次" />
                                    <asp:BoundField DataField="項目名稱" HeaderText="項目名稱" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="數量">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Width="25px" Text='<%# Bind("數量") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep1" runat="server" StepType="Finish">
                        <table>
                            <tr>
                                <td class="tdtxt">
                                    HG卡號：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label4" runat="server" Text="1111-2222-3333-4444"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">
                                    剩餘點數：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label5" runat="server" Text="700"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdtxt">
                                    欲兌金額/點數：
                                </td>
                                <td class="tdval">
                                    <asp:Label ID="Label6" runat="server" Text="300元(540點)"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 160px;" align="center" valign="middle">
                                    <div style="width: 150px; height: 150px; background-color: Silver">
                                        請過卡兌點</div>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                </WizardSteps>
            </asp:Wizard>
        </div>
    </div>
    </form>
</body>
</html>
