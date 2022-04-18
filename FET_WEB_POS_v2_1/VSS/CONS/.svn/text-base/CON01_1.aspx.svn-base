<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON01_1.aspx.cs" Inherits="VSS_CONS_CON01_1" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx1" %>
<%@ Register Src="../../Controls/CONGridView.ascx" TagName="CONGridView" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef" style="display:none">
        <!--資料匯入作業-->
        <asp:Literal ID="Literal01" runat="server" Text="<%$ Resources:WebResources, DataImport %>"></asp:Literal>
    </div>
    <div class="criteria">
        <table>
            <tr>
                <td class="tdval">
                    <!--匯入檔案名稱-->
                    <dx:ASPxLabel ID="Literal02" runat="server" Text="<%$ Resources:WebResources, ImportFileName %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td colspan="5">
                    <dx1:ASPxUploadControl ID="FileUpload1" runat="server" Width="60%">
                    </dx1:ASPxUploadControl>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td>
                                <dx:ASPxRadioButton ID="RadioButton1" runat="server" Text="<%$ Resources:WebResources, Consignment %>"
                                    AutoPostBack="true" GroupName="TYPE" OnCheckedChanged="RadioButton1_CheckedChanged">
                                </dx:ASPxRadioButton>
                            </td>
                            <td>
                                <dx:ASPxRadioButton ID="RadioButton2" runat="server" AutoPostBack="true" Text="<%$ Resources:WebResources, SupplierVendors%>"
                                    GroupName="TYPE" OnCheckedChanged="RadioButton1_CheckedChanged">
                                </dx:ASPxRadioButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdval" colspan="2">
                    <dx:ASPxCheckBox ID="CheckBox1" runat="server" Text="<%$ Resources:WebResources, VendorInformation %>"
                        Width="115px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server" Width="145px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>">
                    </dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td class="tdval" colspan="2">
                    <!--寄銷佣金/租金資料-->
                    <dx:ASPxCheckBox ID="CheckBox2" runat="server" Text="<%$ Resources:WebResources, ConsignmentCommission %>"
                        Width="115px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server" Width="145px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    <dx:ASPxButton ID="Button5" runat="server" Text="<%$ Resources:WebResources, Import %>" >
                    </dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td class="tdval" colspan="2">
                    <!--合作店組資料-->
                    <dx:ASPxCheckBox ID="CheckBox3" runat="server" Text="<%$ Resources:WebResources, CoSetOfDataStores %>"
                        Width="115px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox3" runat="server" Width="145px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    <dx:ASPxButton ID="Button6" runat="server" Text="<%$ Resources:WebResources, Exit %>">
                    </dx:ASPxButton>
                </td>
            </tr>
            <tr>
                <td class="tdval" colspan="2">
                    <!--寄銷商品資料-->
                    <dx:ASPxCheckBox ID="CheckBox4" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInformation %>"
                        Width="115px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox4" runat="server" Width="145px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdval" colspan="2">
                    <!--總額抽成-->
                    <dx:ASPxCheckBox ID="CheckBox5" runat="server" Text="<%$ Resources:WebResources, Prorate %>"
                        Width="115px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox5" runat="server" Width="145px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdval" colspan="2">
                    <!--金額級距-->
                    <dx:ASPxCheckBox ID="CheckBox6" runat="server" Text="<%$ Resources:WebResources, Bracket %>"
                        Width="115px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox6" runat="server" Width="145px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdval" colspan="2">
                    <!--外部廠商商品資料-->
                    <dx:ASPxCheckBox ID="CheckBox7" runat="server" Text="<%$ Resources:WebResources, SupplierVendorsProductData %>" Width="120px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox7" runat="server" Width="145px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdval" colspan="2">
                    <!--信用卡手續費-->
                    <dx:ASPxCheckBox ID="CheckBox8" runat="server" Text="<%$ Resources:WebResources, CreditCardFees %>"
                        Width="115px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox8" runat="server" Width="145px">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    &nbsp;
                </td>
            </tr>
            
        </table>
    </div>
    <div class="seperate">
        <asp:UpdatePanel ID="upTab" runat="server">
            <ContentTemplate>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" AutoPostBack="true"
                    ActiveTabIndex="0" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged">
                    <TabPages>
                         
                        <dx:TabPage Text="<%$ Resources:WebResources, VendorInformation %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <uc1:CONGridView ID="ucGridView1" runat="server" />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentCommission %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <uc1:CONGridView ID="ucGridView2" runat="server" />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="<%$ Resources:WebResources, CoSetOfDataStores %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <uc1:CONGridView ID="ucGridView3" runat="server" />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentProductInformation %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <uc1:CONGridView ID="ucGridView4" runat="server" />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, Prorate %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <uc1:CONGridView ID="ucGridView5" runat="server" />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, Bracket %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <uc1:CONGridView ID="ucGridView6" runat="server" />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="<%$ Resources:WebResources, SupplierVendorsProductData %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <uc1:CONGridView ID="ucGridView7" runat="server" />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="<%$ Resources:WebResources, CreditCardFees %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <uc1:CONGridView ID="ucGridView8" runat="server" />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
