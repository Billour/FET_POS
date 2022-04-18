<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LEA01_Import.aspx.cs" Inherits="VSS_LEA_LEA01_Import" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx1" %>
<%@ Register Src="~/Controls/CONGridView.ascx" TagName="CONGridView" TagPrefix="uc1" %>
<%@ Register Src="../../../Controls/PopupControl.ascx" TagName="popupcontrol" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

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
        
        function Import(s, e) {
            var rtn = confirm('匯入資料後，會將原本畫面上的資料清除，是否要執行匯入動作?');
            if (!rtn) {
                e.processOnServer = false;
            }
            else {
                hidePopupWindow();
            }

        }
        function Exit(s, e) {
            btnImport.SetEnabled = false ;
            btnOK.SetEnabled = true ;
            hidePopupWindow();

        }
        
    </script>
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
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ImportFileName %>"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <asp:FileUpload ID="FileUpload" runat="server" Width="60%" />
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--基本資料-->
                        <dx:ASPxCheckBox  ID="CheckBox1" runat="server" Text="基本資料" >
                        </dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox1" runat="server">
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
                        <dx:ASPxButton ID="btnOK" runat="server"  ClientInstanceName="btnOK"
                        Text="<%$ Resources:WebResources, Ok %>" onclick="btnOK_Click">
                    <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
                    </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--賠償項目-->
                        <dx:ASPxCheckBox  ID="CheckBox2" runat="server" Text="賠償項目" >
                        </dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox2" runat="server">
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
                    <dx:ASPxButton ID="btnImport" runat="server"  Visible="false" ClientInstanceName="btnImport"
                        Text="<%$ Resources:WebResources, Import %>" onclick="btnImport_Click" >                    
                    <ClientSideEvents Click="function(s, e) { Import(s, e); }" />
                    </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                        <!--折扣項目-->
                        <dx:ASPxCheckBox  ID="CheckBox3" runat="server" Text="折扣項目" >
                        </dx:ASPxCheckBox>
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="TextBox3" runat="server">
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
                        <dx:ASPxButton ID="btnCalcel"  runat="server" Text="<%$ Resources:WebResources, Exit %>" >
                       <ClientSideEvents Click="function(s, e) { hidePopupWindow();}" />
                    </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <dx:ASPxPageControl ID="ASPxPageControl1"  ActiveTabIndex="0" runat="server" Width="100%">
            <TabPages>
                <dx:TabPage Text="基本資料">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ClientInstanceName="gvLEASE" ID="gvLEASE" runat="server" Width="100%"
                                KeyFieldName="LEASE_ID"
                                 OnHtmlDataCellPrepared="gvLEASE_HtmlDataCellPrepared"
                                 OnHtmlRowCreated ="gvLEASE_HtmlRowCreated"
                                 OnPageIndexChanged="gvLEASE_PageIndexChanged" EnableCallBacks="False">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="LEASE_TYPE" Caption="產品類別"
                                        VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="產品名稱"
                                        VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="SUPP_NO" Caption="外部廠商代碼"
                                        VisibleIndex="2">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="SUPP_NAME" Caption="外部廠商名稱"
                                        VisibleIndex="3">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="RENT_PRODNO" Caption="租金料號"
                                        VisibleIndex="4">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="DAILY_RENT_PRICE" Caption="日租金額"
                                        VisibleIndex="5">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="EARNEST_PRODNO" Caption="保證金料號">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="EARNEST_MONEY" Caption="保證金">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="INDEMNITY_PRODNO" Caption="賠償金料號">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="S_DATE" Caption="有效期間(起)">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="E_DATE" Caption="有效期間(訖)">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因"  
                                        CellStyle-ForeColor="Red" >
                                        <CellStyle ForeColor="Red">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn FieldName="LEASE_ID"  Caption="廠商id" Visible ="false">
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                                            <tr>
                                                <td align="right">
                                                    <dx:ASPxLabel ID="lblOkCount" runat="server" ></dx:ASPxLabel>
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                <dx:ASPxLabel ID="lblErrorCount" runat="server" ForeColor="Red" ></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                    <EmptyDataRow>
                                        <!--choose add button-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </EmptyDataRow>
                                </Templates>
                                <Settings ShowTitlePanel="True" ShowFooter="True" />
                            </cc:ASPxGridView>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="賠償項目">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ClientInstanceName="gvIndemnify" ID="gvIndemnify" runat="server" Width="100%"
                                KeyFieldName="RENT_INDEMNIFY_ITEMS" EnableCallBacks="False"
                                OnHtmlDataCellPrepared="gvIndemnify_HtmlDataCellPrepared"
                                 OnHtmlRowCreated ="gvIndemnify_HtmlRowCreated"
                                 OnPageIndexChanged="gvIndemnify_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="產品名稱"
                                        VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="IND_ITEM_NAME" Caption="賠償項目"
                                        VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="IND_UNIT_PRICE" Caption="金額"
                                        VisibleIndex="2">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因"  
                                        CellStyle-ForeColor="Red" >
                                        <CellStyle ForeColor="Red">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn FieldName="RENT_INDEMNIFY_ITEMS"  Caption="廠商id" Visible ="false">
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                                            <tr>
                                                <td align="right">
                                                <dx:ASPxLabel ID="lblOkCount2" runat="server" ></dx:ASPxLabel>                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                <dx:ASPxLabel ID="lblErrorCount2" runat="server" ForeColor="Red" ></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                    <EmptyDataRow>
                                        <!--choose add button-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </EmptyDataRow>
                                </Templates>
                                <Settings ShowTitlePanel="True" ShowFooter="True" />
                            </cc:ASPxGridView>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="折扣項目">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ClientInstanceName="gvDiscount" ID="gvDiscount" runat="server" Width="100%"
                                KeyFieldName="RENT_DISCOUNT_ID" AutoGenerateColumns="False" Settings-ShowTitlePanel="true"
                                 EnableCallBacks="False" EnableTheming="True" 
                                OnPageIndexChanged="gvDiscount_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="SUPPNO" Caption="產品名稱" VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="折扣料號" VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="COMMISSION" Caption="折扣名稱" VisibleIndex="2">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="DISCOUNT_AMT" Caption="折扣金額"  VisibleIndex="3">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="DISCOUNT_RATE" Caption="折扣比率"  VisibleIndex="4">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="COST_CENTER_NO" Caption="成本中心"  VisibleIndex="5">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ACCOUNT_CODE" Caption="會計科目"  VisibleIndex="6">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataTextColumn FieldName="ERR_DESC" Caption="失敗原因"  
                                        CellStyle-ForeColor="Red" >
                                        <CellStyle ForeColor="Red">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn FieldName="RENT_DISCOUNT_ID"  Caption="廠商id" Visible ="false">
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Templates>
                                    <TitlePanel>
                                        <table cellpadding="0" cellspacing="0" border="0" align="left">
                                            <tr>
                                                <td align="right">
                                                <dx:ASPxLabel ID="lblOkCount2" runat="server" ></dx:ASPxLabel>                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                <dx:ASPxLabel ID="lblErrorCount2" runat="server" ForeColor="Red" ></dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </TitlePanel>
                                    <EmptyDataRow>
                                        <!--choose add button-->
                                        <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, NoMatchesFound %>"></asp:Literal>
                                    </EmptyDataRow>
                                </Templates>
                                <Settings ShowTitlePanel="True" ShowFooter="True" />
                            </cc:ASPxGridView>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
        <div class="seperate">
        </div>
    </div>
    </form>
</body>
</html>
