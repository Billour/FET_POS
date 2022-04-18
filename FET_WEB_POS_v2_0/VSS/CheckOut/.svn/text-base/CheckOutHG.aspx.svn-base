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
                    <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                        CommandName="MoveNext">
                    </dx:ASPxButton>
                </StartNavigationTemplate>
                <StepNavigationTemplate>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="Button1" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                                    CommandName="MoveNext">
                                </dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxButton ID="Button2" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                                    CommandName="CloseWin" AutoPostBack="false">
                                    <ClientSideEvents Click="function(s,e){window.close();}" />
                                </dx:ASPxButton>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                                    CommandName="ResetValue" OnClick="btnReset_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
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
                            <dx:ASPxGridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                KeyFieldName="項次" Settings-ShowTitlePanel="true">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="項次" Caption="項次" VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="活動名稱" Caption="活動名稱" VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="項目名稱" Caption="項目名稱" CellStyle-HorizontalAlign="Left"
                                        VisibleIndex="2">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="數量" VisibleIndex="3">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="TextBox1" runat="server" Text='<%# Bind("[數量]") %>' Width="25px">
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <asp:Label ID="Label5" runat="server" Text="單商品折抵活動"></asp:Label>
                                    </TitlePanel>
                                </Templates>
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                            </dx:ASPxGridView>
                            <dx:ASPxGridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                                KeyFieldName="項次" Settings-ShowTitlePanel="true">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="項次" Caption="項次" VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="活動名稱" Caption="活動名稱" VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="項目名稱" Caption="項目名稱" CellStyle-HorizontalAlign="Left"
                                        VisibleIndex="2">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="數量" VisibleIndex="3">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="TextBox1" runat="server" Text='<%# Bind("[數量]") %>' Width="25px">
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <asp:Label ID="Label6" runat="server" Text="促銷商品折抵活動"></asp:Label>
                                    </TitlePanel>
                                </Templates>
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                            </dx:ASPxGridView>
                            <dx:ASPxGridView ID="gvMaster" runat="server" AutoGenerateColumns="False" Width="100%"
                                KeyFieldName="項次" Settings-ShowTitlePanel="true">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="項次" Caption="項次" VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="項目名稱" Caption="項目名稱" CellStyle-HorizontalAlign="Left"
                                        VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="數量" VisibleIndex="2">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="TextBox1" runat="server" Text='<%# Bind("[數量]") %>' Width="25px">
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <asp:Label ID="Label7" runat="server" Text="一般兌點通則"></asp:Label>
                                    </TitlePanel>
                                </Templates>
                                <Styles>
                                    <TitlePanel Font-Size="Small" HorizontalAlign="Left">
                                    </TitlePanel>
                                </Styles>
                            </dx:ASPxGridView>
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
