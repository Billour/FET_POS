<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON02_Import.aspx.cs" Inherits="VSS_CONS_CON02_Import" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx1" %>
<%@ Register Src="~/Controls/CONGridView.ascx" TagName="CONGridView" TagPrefix="uc1" %>
<%@ Register Src="../../../Controls/PopupControl.ascx" TagName="popupcontrol" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function chName(s, e) {
            //debugger;
            var Qty = s.GetValue();
            var iQty = 0;
            if (s.GetText() == '') {
                e.errorText = '工作表不允許空值，請重新輸入';
                return false;
            }
            if (Qty != null) {
                iQty = Number(Qty);
                if (isNaN(iQty)) {
                    e.errorText = '輸入字串非數字格式，請重新輸入';
                    return false;
                }
                else if (iQty <= 0) {
                    e.errorText = '工作表不允許小於0，請重新輸入';
                    return false;
                }
                else if (Qty.indexOf(".") > 0) {
                    e.errorText = '工作表不允許輸入小數點，請重新輸入';
                    return false;
                }
            }
        }

        function DisabledAndRunButton(s, e) {
            var file = document.form1.FileUpload.value;
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file))) {
                alert("匯入的檔案不正確,須為Excel檔!!");
                var t = document.getElementById('FileUpload');
                t.outerHTML = t.outerHTML;
                e.processOnServer = false;
            }
            else {
                if (s.GetEnabled()) {
                    s.SendPostBack('Click');
                    s.SetEnabled(false);
                }
            }
        }
        function Import(s, e) {
            var rtn = confirm('匯入資料後，會將原本畫面上的資料清除，是否要執行匯入動作?');
            if (!rtn) {
                e.processOnServer = false;
            }
            else {
                hidePopupWindow();
            }

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    <div class="titlef" style="display: none">
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
                    <asp:FileUpload ID="FileUpload" runat="server" Width="500px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100px" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td><!-- 廠商類別 -->
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, SupplierCategory %>">
                                </dx:ASPxLabel>
                                ：
                            </td>
                            <td>
                                <dx:ASPxRadioButton ID="RadioConsignment" runat="server" Text="<%$ Resources:WebResources, Consignment %>"
                                    AutoPostBack="true" GroupName="TYPE" OnCheckedChanged="RadioType_CheckedChanged"
                                    Checked="True" Width="100px">
                                </dx:ASPxRadioButton>
                            </td>
                            <td>
                                <dx:ASPxRadioButton ID="RadioSupplierVendors" runat="server" AutoPostBack="true"
                                    Text="<%$ Resources:WebResources, SupplierVendors%>" GroupName="TYPE" OnCheckedChanged="RadioType_CheckedChanged"
                                    Width="100px">
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
                    <dx:ASPxTextBox ID="TextBox1" ClientInstanceName="TextBox1" MaxLength="3" runat="server"
                        Width="145px">
                        <ClientSideEvents Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    <dx:ASPxButton ID="btnOK" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                        OnClick="btnOK_Click">
                        <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
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
                    <dx:ASPxTextBox ID="TextBox2" runat="server" MaxLength="3"  Width="145px">
                        <ClientSideEvents Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    <dx:ASPxButton ID="btnImport" runat="server"  Visible="false" Text="<%$ Resources:WebResources, Import %>"
                        OnClick="btnImport_Click">
                        <ClientSideEvents Click="function(s, e) { Import(s, e); }" />
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
                    <dx:ASPxTextBox ID="TextBox3" runat="server" MaxLength="3"  Width="145px">
                        <ClientSideEvents Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    &nbsp;
                </td>
                <td class="tdval">
                    <dx:ASPxButton ID="btnCalcel" runat="server" Text="<%$ Resources:WebResources, Exit %>"
                        OnClick="btnCalcel_Click">
                        <ClientSideEvents Click="function(s, e) { hidePopupWindow();}" />
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
                    <dx:ASPxTextBox ID="TextBox4" runat="server" MaxLength="3"  Width="145px">
                        <ClientSideEvents Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
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
                    <dx:ASPxTextBox ID="TextBox5" runat="server" MaxLength="3"  Width="145px">
                        <ClientSideEvents Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
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
                    <dx:ASPxTextBox ID="TextBox6" runat="server" MaxLength="3"  Width="145px">
                        <ClientSideEvents Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
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
                    <dx:ASPxCheckBox ID="CheckBox7" runat="server" Text="<%$ Resources:WebResources, SupplierVendorsProductData %>"
                        Width="120px">
                    </dx:ASPxCheckBox>
                </td>
                <td class="tdtxt">
                    <!--工作表-->
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox7" runat="server" MaxLength="3"  Width="145px">
                        <ClientSideEvents Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
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
                    <dx:ASPxTextBox ID="TextBox8" runat="server" MaxLength="3"  Width="145px">
                        <ClientSideEvents Validation="function(s, e){ chName(s, e); }" />
                        <ValidationSettings>
                            <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。" />
                        </ValidationSettings>
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
    <div  style="width:100%;height:250px;overflow:scroll;" >
        <asp:UpdatePanel ID="upTab" runat="server">
            <ContentTemplate>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" AutoPostBack="true"
                    ActiveTabIndex="0" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged">
                    <TabPages>
                        <dx:TabPage Text="<%$ Resources:WebResources, VendorInformation %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <cc:ASPxGridView ID="gvCSM_SUPPLIER" ClientInstanceName="gvCSM_SUPPLIER" runat="server"
                                        KeyFieldName="SUPP_NO" Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                                        EnableCallBacks="False" EnableTheming="True">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="CSM_TYPE" Caption="廠商類別" />
                                            <dx:GridViewDataTextColumn FieldName="SUPP_NO" Caption="廠商代號" />
                                            <dx:GridViewDataTextColumn FieldName="SUPP_NAME" Caption="廠商名稱" />
                                            <dx:GridViewDataTextColumn FieldName="SUPP_ADDRESS" Caption="公司地址" />
                                            <dx:GridViewDataTextColumn FieldName="CONTACE" Caption="聯絡人" />
                                            <dx:GridViewDataTextColumn FieldName="TELNO" Caption="聯絡電話" />
                                            <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="合作起日" />
                                            <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="合作迄日" />
                                            <dx:GridViewDataTextColumn FieldName="CONTRACTNO" Caption="合約號碼" />
                                            <dx:GridViewDataTextColumn FieldName="CLOSEDAY" Caption="結算日" />
                                            <dx:GridViewDataTextColumn FieldName="COMPANY_ID" Caption="統一編號" />
                                            <dx:GridViewDataTextColumn FieldName="BOSS_NAME" Caption="負責人" />
                                            <dx:GridViewDataTextColumn FieldName="BOSS_TEL_NO" Caption="電話號碼" />
                                            <dx:GridViewDataTextColumn FieldName="FAX" Caption="傳真" />
                                            <dx:GridViewDataTextColumn FieldName="EMAIL" Caption="電子信箱" />
                                            <dx:GridViewDataTextColumn FieldName="AMOUNT_MAX" Caption="總金額底限" />
                                            <dx:GridViewDataTextColumn FieldName="F20" Caption="總金額底限勾選" />
                                            <dx:GridViewDataTextColumn FieldName="MEMO" Caption="備註" />
                                            <dx:GridViewDataTextColumn FieldName="ACCOUNTCODE1" Caption="會計科目1" />
                                            <dx:GridViewDataTextColumn FieldName="ACCOUNTCODE2" Caption="會計科目2" />
                                            <dx:GridViewDataTextColumn FieldName="ACCOUNTCODE3" Caption="會計科目3" />
                                            <dx:GridViewDataTextColumn FieldName="ACCOUNTCODE4" Caption="會計科目4" />
                                            <dx:GridViewDataTextColumn FieldName="ACCOUNTCODE5" Caption="會計科目5" />
                                            <dx:GridViewDataTextColumn FieldName="ACCOUNTCODE6" Caption="會計科目6" />
                                            <dx:GridViewDataTextColumn FieldName="FET_CONTACE_USER" Caption="遠傳聯絡窗口" />
                                            <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因"  
                                                CellStyle-ForeColor="Red" >
                                                <CellStyle ForeColor="Red">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
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
                                        <SettingsPager PageSize="10" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                                        <Settings ShowTitlePanel="True" />
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentCommission %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <cc:ASPxGridView ID="gvCSM_SUPP_COMMISSION" ClientInstanceName="gvCSM_SUPP_COMMISSION1"
                                        runat="server" Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                                        EnableCallBacks="False" EnableTheming="True">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="F4" Caption="廠商代號" />
                                            <dx:GridViewDataTextColumn FieldName="COMMISSION" Caption="佣金比率" />
                                            <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="起始月份" />
                                            <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="結束月份" />
                                            <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因" 
                                                CellStyle-ForeColor="Red" >
                                                <CellStyle ForeColor="Red">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="10" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                                        <Settings ShowTitlePanel="True" />
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, CoSetOfDataStores %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <cc:ASPxGridView ID="gvCSM_SUPPSTORE" ClientInstanceName="gvCSM_SUPPSTORE" runat="server"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableCallBacks="False"
                                        EnableTheming="True">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="F4" Caption="廠商代號" />
                                            <dx:GridViewDataTextColumn FieldName="STORE_NO" Caption="門市代號" />
                                            <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因" 
                                                CellStyle-ForeColor="Red" >
                                                <CellStyle ForeColor="Red">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="10" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                                        <Settings ShowTitlePanel="True" />
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentProductInformation %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <cc:ASPxGridView ID="ASPxGridView1" ClientInstanceName="gvCSM_SUPPSTORE" runat="server"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableCallBacks="False"
                                        EnableTheming="True">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="廠商代號" Caption="廠商代號" />
                                            <dx:GridViewDataTextColumn FieldName="商品代號" Caption="商品代號" />
                                            <dx:GridViewDataTextColumn FieldName="商品類別" Caption="商品類別" />
                                            <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="商品名稱" />
                                            <dx:GridViewDataTextColumn FieldName="上架日" Caption="上架日" />
                                            <dx:GridViewDataTextColumn FieldName="下架日" Caption="下架日" />
                                            <dx:GridViewDataTextColumn FieldName="停止訂購日" Caption="停止訂購日" />
                                            <dx:GridViewDataTextColumn FieldName="會計科目1" Caption="會計科目1" />
                                            <dx:GridViewDataTextColumn FieldName="會計科目2" Caption="會計科目2" />
                                            <dx:GridViewDataTextColumn FieldName="會計科目3" Caption="會計科目3" />
                                            <dx:GridViewDataTextColumn FieldName="會計科目4" Caption="會計科目4" />
                                            <dx:GridViewDataTextColumn FieldName="會計科目5" Caption="會計科目5" />
                                            <dx:GridViewDataTextColumn FieldName="會計科目6" Caption="會計科目6" />
                                            <dx:GridViewDataTextColumn FieldName="單位" Caption="單位" />
                                            <dx:GridViewDataTextColumn FieldName="佣金比率" Caption="佣金比率" />
                                            <dx:GridViewDataTextColumn FieldName="起始月份" Caption="起始月份" />
                                            <dx:GridViewDataTextColumn FieldName="結束月份" Caption="結束月份" />
                                            <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因" 
                                                CellStyle-ForeColor="Red" >
                                                <CellStyle ForeColor="Red">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="10" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                                        <Settings ShowTitlePanel="True" />
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, Prorate %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <cc:ASPxGridView ID="gvCSM_SUPP_COMMISSION2" ClientInstanceName="gvCSM_SUPP_COMMISSION2"
                                        runat="server" Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                                        EnableCallBacks="False" EnableTheming="True">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="F4" Caption="廠商代號" />
                                            <dx:GridViewDataTextColumn FieldName="COMMISSION" Caption="佣金比率" />
                                            <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="起始日" />
                                            <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="結束日" />
                                            <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因"  
                                                CellStyle-ForeColor="Red">
                                                <CellStyle ForeColor="Red">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="10" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                                        <Settings ShowTitlePanel="True" />
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, Bracket %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <cc:ASPxGridView ID="gvCSM_SUP_AMT_LEVEL" ClientInstanceName="gvCSM_SUP_AMT_LEVEL"
                                        runat="server" Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                                        EnableCallBacks="False" EnableTheming="True">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="F4" Caption="廠商代號" />
                                            <dx:GridViewDataTextColumn FieldName="SEQNO" Caption="級距項次" />
                                            <dx:GridViewDataTextColumn FieldName="S_AMT" Caption="起-金額級距" />
                                            <dx:GridViewDataTextColumn FieldName="E_AMT" Caption="訖-金額級距" />
                                            <dx:GridViewDataTextColumn FieldName="COMMISION_RATE" Caption="佣金比率" />
                                            <dx:GridViewDataTextColumn FieldName="S_DATE" Caption="開始日期" />
                                            <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="結束日期" />
                                            <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因" 
                                                CellStyle-ForeColor="Red" >
                                                <CellStyle ForeColor="Red">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="10" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                                        <Settings ShowTitlePanel="True" />
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, SupplierVendorsProductData %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <cc:ASPxGridView ID="gvCSM_SUP_PROD" ClientInstanceName="gvCSM_SUP_PROD" runat="server"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableCallBacks="False"
                                        EnableTheming="True">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="F4" Caption="廠商代號" />
                                            <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="商品料號" />
                                            <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因" 
                                                CellStyle-ForeColor="Red" >
                                                <CellStyle ForeColor="Red">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="10" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                                        <Settings ShowTitlePanel="True" />
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                    </cc:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, CreditCardFees %>">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <cc:ASPxGridView ID="gvCSM_CREDIT_CARD_PROCE_RATE" ClientInstanceName="gvCSM_CREDIT_CARD_PROCE_RATE" runat="server"
                                        Width="100%" AutoGenerateColumns="False" Settings-ShowTitlePanel="true" EnableCallBacks="False"
                                        EnableTheming="True">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="F4" Caption="項次" />
                                            <dx:GridViewDataTextColumn FieldName="CREDIT_CARD_TYPE_ID" Caption="信用卡別" />
                                            <dx:GridViewDataTextColumn FieldName="CHARGE_RATE" Caption="手續費" />
                                            <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因"  
                                                CellStyle-ForeColor="Red">
                                                <CellStyle ForeColor="Red">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="10" />
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                                        <Settings ShowTitlePanel="True" />
                                        <Templates>
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="資料筆數：0 筆"></dx:ASPxLabel>&nbsp;&nbsp;
                                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="錯誤筆數：0 筆" ForeColor="Red"></dx:ASPxLabel>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            </Templates>
                                    </cc:ASPxGridView>
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
