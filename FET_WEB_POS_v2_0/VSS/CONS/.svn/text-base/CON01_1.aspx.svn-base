<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON01_1.aspx.cs" Inherits="VSS_CON01_1" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef">
        <!--資料匯入作業-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DataImport %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdval">
                        <!--匯入檔案名稱-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ImportFileName %>"></dx:ASPxLabel>：
                    </td>
                    <td colspan="5">
                        <dx1:ASPxUploadControl ID="FileUpload1" runat="server" Width="60%"></dx1:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <dx:ASPxCheckBox ID="CheckBox1" runat="server" Text="廠商資料" Width="115px"></dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server" Width="145px"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>"></dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--寄銷佣金/租金資料-->
                        <dx:ASPxCheckBox ID="CheckBox2" runat="server" Text="寄銷廠商佣金" Width="115px"></dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox5" runat="server" Width="145px"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <dx:ASPxButton ID="Button5" runat="server" Text="<%$ Resources:WebResources, Import %>"></dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--合作店組資料-->
                        <dx:ASPxCheckBox ID="CheckBox3" runat="server" Text="合作店組資料" Width="115px"></dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox6" runat="server" Width="145px"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <dx:ASPxButton ID="Button6" runat="server" Text="<%$ Resources:WebResources, Exit %>"></dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--寄銷商品資料-->
                        <dx:ASPxCheckBox ID="CheckBox4" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInformation %>" Width="115px"></dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox2" runat="server" Width="145px"></dx:ASPxTextBox>
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
                        <dx:ASPxCheckBox ID="CheckBox5" runat="server" Text="<%$ Resources:WebResources, Prorate %>" Width="115px"></dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox3" runat="server" Width="145px"></dx:ASPxTextBox>
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
                        <dx:ASPxCheckBox ID="CheckBox6" runat="server" Text="<%$ Resources:WebResources, Bracket %>" Width="115px"></dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox4" runat="server" Width="145px"></dx:ASPxTextBox>
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
                        <dx:ASPxCheckBox ID="CheckBox7" runat="server" Text="外部廠商商品資料" Width="120px"></dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox7" runat="server" Width="145px"></dx:ASPxTextBox>
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
                        <dx:ASPxCheckBox ID="CheckBox8" runat="server" Text="信用卡手續費" Width="115px"></dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox8" runat="server" Width="145px"></dx:ASPxTextBox>
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
        </div>
        <asp:UpdatePanel ID="upTab" runat="server">
            <ContentTemplate>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" AutoPostBack="true" 
                    ActiveTabIndex="7" onactivetabchanged="ASPxPageControl1_ActiveTabChanged">
                   <TabPages>
                        <dx:TabPage Text="廠商資料">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div>
                                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
                                            KeyFieldName="廠商代號" Width="100%"
                                            AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="廠商類別" 
                                                    Caption="<%$ Resources:WebResources, SupplierCategory %>" VisibleIndex="0" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="廠商代號" 
                                                    Caption="<%$ Resources:WebResources, SupplierNo %>" VisibleIndex="1" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="廠商名稱" 
                                                    Caption="<%$ Resources:WebResources, SupplierName %>" VisibleIndex="2" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="公司地址" 
                                                    Caption="<%$ Resources:WebResources, CompanyAddress %>" VisibleIndex="3" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="聯絡人" 
                                                    Caption="<%$ Resources:WebResources, Contact %>" VisibleIndex="4" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="聯絡電話" 
                                                    Caption="<%$ Resources:WebResources, ContactTelephone %>" VisibleIndex="5" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="合作起日" 
                                                    Caption="<%$ Resources:WebResources, CooperationStartDate %>" VisibleIndex="6" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="合作訖日" 
                                                    Caption="<%$ Resources:WebResources, CooperationEndDate %>" VisibleIndex="7" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="合約號碼" 
                                                    Caption="<%$ Resources:WebResources, ContractNo %>" VisibleIndex="8" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="結算日" Caption="結算日" VisibleIndex="9" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="統一編號" 
                                                    Caption="<%$ Resources:WebResources, UnifiedBusinessNo %>" VisibleIndex="10" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="負責人" 
                                                    Caption="<%$ Resources:WebResources, Owner %>" VisibleIndex="11" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="電話號碼" 
                                                    Caption="<%$ Resources:WebResources, Telephone %>" VisibleIndex="12" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="傳真" 
                                                    Caption="<%$ Resources:WebResources, Fax %>" VisibleIndex="13" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="電子信箱" 
                                                    Caption="<%$ Resources:WebResources, Email %>" VisibleIndex="14" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="總金額底限" 
                                                    Caption="<%$ Resources:WebResources, MinimumTotalAmount %>" VisibleIndex="15" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="總金額底限勾選" Caption="總金額底限勾選" 
                                                    VisibleIndex="16" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="備註" 
                                                    Caption="<%$ Resources:WebResources, Remark %>" VisibleIndex="17" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目1" Caption="科目1" VisibleIndex="18" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目2" Caption="科目2" VisibleIndex="19" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目3" Caption="科目3" VisibleIndex="20" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目4" Caption="科目4" VisibleIndex="21" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目5" Caption="科目5" VisibleIndex="22" >                                            
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目6" Caption="科目6" VisibleIndex="23" >                                            
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <settings showtitlepanel="True" />
                                            <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        </cc:ASPxGridView>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="寄銷廠商佣金">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div>
                                     <cc:ASPxGridView ID="GridView1" ClientInstanceName="GridView1" runat="server" 
                                            KeyFieldName="廠商代號" Width="100%"
                                         AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="廠商代號" 
                                                    Caption="<%$ Resources:WebResources, SupplierNo %>" VisibleIndex="0" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="佣金比率" 
                                                    Caption="<%$ Resources:WebResources, CommissionRate %>" VisibleIndex="1" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="起始月份" 
                                                    Caption="<%$ Resources:WebResources, StartMonth %>" VisibleIndex="2" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="結束月份" 
                                                    Caption="<%$ Resources:WebResources, EndMonth %>" VisibleIndex="3" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <settings showtitlepanel="True" />
                                            <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        </cc:ASPxGridView>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="合作店組資料">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div>
                                       <cc:ASPxGridView ID="GridView2" ClientInstanceName="GridView2" runat="server" 
                                            KeyFieldName="廠商代號" Width="100%"
                                       AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="廠商代號" 
                                                    Caption="<%$ Resources:WebResources, SupplierNo %>" VisibleIndex="0" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="門市代號" Caption="門市代號" VisibleIndex="1" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <settings showtitlepanel="True" />
                                            <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        </cc:ASPxGridView>                                    
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentProductInformation %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div>
                                    <cc:ASPxGridView ID="GridView3" ClientInstanceName="GridView3" runat="server" 
                                            KeyFieldName="廠商代號" Width="100%"
                                    AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="廠商代號" 
                                                    Caption="<%$ Resources:WebResources, SupplierNo %>" VisibleIndex="0" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="商品代號" Caption="商品代號" VisibleIndex="1" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="商品類別" 
                                                    Caption="<%$ Resources:WebResources, ProductCategory %>" VisibleIndex="2" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="商品名稱" 
                                                    Caption="<%$ Resources:WebResources, ProductName %>" VisibleIndex="3" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="上架日" 
                                                    Caption="<%$ Resources:WebResources, SupportStartDate %>" VisibleIndex="4" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="下架日" 
                                                    Caption="<%$ Resources:WebResources, SupportExpiryDate %>" VisibleIndex="5" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="停止訂購日" 
                                                    Caption="<%$ Resources:WebResources, OrderEndDate %>" VisibleIndex="6" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目1" Caption="科目1" VisibleIndex="7" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目2" Caption="科目2" VisibleIndex="8" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目3" Caption="科目3" VisibleIndex="9" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目4" Caption="科目4" VisibleIndex="10" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目5" Caption="科目5" VisibleIndex="11" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="科目6" Caption="科目6" VisibleIndex="12" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="單位" 
                                                    Caption="<%$ Resources:WebResources, Unit %>" VisibleIndex="13" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="佣金比率" 
                                                    Caption="<%$ Resources:WebResources, CommissionRate %>" VisibleIndex="14" >                                          
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="起始月份" 
                                                    Caption="<%$ Resources:WebResources, StartMonth %>" VisibleIndex="15" >                                          
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="結束月份" 
                                                    Caption="<%$ Resources:WebResources, EndMonth %>" VisibleIndex="16" >                                                                                     
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <settings showtitlepanel="True" />
                                            <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        </cc:ASPxGridView>                                    
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="<%$ Resources:WebResources, Prorate %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div>
                                         <cc:ASPxGridView ID="GridView4" ClientInstanceName="GridView4" runat="server" 
                                             KeyFieldName="廠商代號" Width="100%"
                                         AutoGenerateColumns="False">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="廠商代號" 
                                                        Caption="<%$ Resources:WebResources, SupplierNo %>" VisibleIndex="0" >
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="佣金比率" 
                                                        Caption="<%$ Resources:WebResources, CommissionRate %>" VisibleIndex="1" >
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="起始月份" 
                                                        Caption="<%$ Resources:WebResources, StartMonth %>" VisibleIndex="2" >
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="結束月份" 
                                                        Caption="<%$ Resources:WebResources, EndMonth %>" VisibleIndex="3" >
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <settings showtitlepanel="True" />
                                                <Templates>
                                                <EmptyDataRow>
                                                    <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                                </EmptyDataRow>
                                                <TitlePanel>
                                                    <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </TitlePanel>
                                                </Templates>
                                            <SettingsEditing Mode="Inline" />
                                            <SettingsPager PageSize="5"></SettingsPager>
                                            </cc:ASPxGridView>                                    
                                       </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="<%$ Resources:WebResources, Bracket %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div>
                                    <cc:ASPxGridView ID="GridView5" ClientInstanceName="GridView5" runat="server" 
                                            KeyFieldName="廠商代號" Width="100%"
                                    AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="廠商代號" 
                                                    Caption="<%$ Resources:WebResources, SupplierNo %>" VisibleIndex="0" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="級距項次" 
                                                    Caption="<%$ Resources:WebResources, BracketItems %>" VisibleIndex="1" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="起-金額級距" 
                                                    Caption="<%$ Resources:WebResources, BracketStart %>" VisibleIndex="2" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="訖-金額級距" 
                                                    Caption="<%$ Resources:WebResources, BracketEnd %>" VisibleIndex="3" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="佣金比率" 
                                                    Caption="<%$ Resources:WebResources, CommissionRate %>" VisibleIndex="4" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="起始月份" 
                                                    Caption="<%$ Resources:WebResources, StartMonth %>" VisibleIndex="5" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="結束月份" 
                                                    Caption="<%$ Resources:WebResources, EndMonth %>" VisibleIndex="6" >                                            
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <settings showtitlepanel="True" />
                                            <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        </cc:ASPxGridView>         
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="外部廠商商品資料">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div>
                                        <cc:ASPxGridView ID="GridView6" ClientInstanceName="GridView6" runat="server" 
                                            KeyFieldName="廠商代號" Width="100%"
                                        AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="廠商代號" 
                                                    Caption="<%$ Resources:WebResources, SupplierNo %>" VisibleIndex="0" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="商品料號" 
                                                    Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="1" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <settings showtitlepanel="True" />
                                            <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        </cc:ASPxGridView>                                          
                                     </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        
                        <dx:TabPage Text="信用卡手續費">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <div>
                                        <cc:ASPxGridView ID="GridView7" ClientInstanceName="GridView7" runat="server" 
                                            KeyFieldName="項次" Width="100%"
                                        AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="項次" 
                                                    Caption="<%$ Resources:WebResources, Items %>" VisibleIndex="0" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="信用卡別" 
                                                    Caption="<%$ Resources:WebResources, TypeOfCreditCard %>" VisibleIndex="1" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="手續費" Caption="手續費" VisibleIndex="2" >
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <settings showtitlepanel="True" />
                                            <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager PageSize="5"></SettingsPager>
                                        </cc:ASPxGridView>               
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                     </TabPages>
                </dx:ASPxPageControl>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
