<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CON04_1.aspx.cs" Inherits="VSS_CONS_CON04_1" %>
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
                        <!--寄銷商品資料-->
                        <asp:CheckBox ID="ckProductSheetNo" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductInformation %>" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtProductSheetNo" runat="server">
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
                        <!--寄銷佣金/租金資料-->
                        <asp:CheckBox ID="ckCommissionSheetNo" runat="server" Text="<%$ Resources:WebResources, ConsignmentCommission %>" />
                    </td>
                    <td class="tdtxt">
                        <!--工作表-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Worksheet %>"></asp:Literal>
                    </td>
                    <td class="tdval">
                        <dx:ASPxTextBox ID="txtCommissionSheetNo" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                    <dx:ASPxButton ID="btnImport" runat="server"  Enabled="false" ClientInstanceName="btnImport"
                        Text="<%$ Resources:WebResources, Import %>" onclick="btnImport_Click" >                    
                    <ClientSideEvents Click="function(s, e) { Import(s, e); }" />
                    </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td class="tdval" colspan="2">
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        <%--<dx:ASPxButton Width="50px" ID="btnCalcel" runat="server" Text="<%$ Resources:WebResources, Exit %>" >
                             <ClientSideEvents Click="function(s, e) { hidePopupWindow();}" />
                        </dx:ASPxButton>--%>
                        <dx:ASPxButton ID="btnCalcel"  runat="server" Text="<%$ Resources:WebResources, Exit %>"
                       >
                       <ClientSideEvents Click="function(s, e) { Exit();}" />
                    </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
            <TabPages>
                <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentProductInformation %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ClientInstanceName="gvMaster" ID="gvMaster" runat="server" Width="100%"
                                KeyFieldName="PRODNO"
                                 OnHtmlDataCellPrepared="gvMaster_HtmlDataCellPrepared"
                                 OnHtmlRowCreated ="gvMaster_HtmlRowCreated"
                                 OnPageIndexChanged="gvMaster_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="SUPPNO" Caption="<%$ Resources:WebResources, SupplierNo %>"
                                        VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                                        VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PRODTYPENO" Caption="<%$ Resources:WebResources, ProductCategory %>"
                                        VisibleIndex="2">
                                        <DataItemTemplate>
                                         <dx:ASPxTextBox ID="lblProdTypeName" runat="server" ClientInstanceName="ProdTypeName"
                                            Text='<% #Bind("PRODTYPENAME") %>' ReadOnly="true" Border-BorderStyle="None" ClientVisible="true">
                                        </dx:ASPxTextBox>
                                         <dx:ASPxLabel ID="lblProdTypeNo" runat="server" ClientInstanceName="ProdTypeNo"
                                            Text='<% #Bind("PRODTYPENO") %>' ReadOnly="true" Border-BorderStyle="None" ClientVisible="false">
                                        </dx:ASPxLabel>                                
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                        VisibleIndex="3">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, SupportStartDate %>"
                                        VisibleIndex="4">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, SupportExpiryDate %>"
                                        VisibleIndex="5">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="CEASEDATE" Caption="<%$ Resources:WebResources, OrderEndDate %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ACCT1" Caption="<%$ Resources:WebResources, Subject1 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ACCT2" Caption="<%$ Resources:WebResources, Subject2 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ACCT3" Caption="<%$ Resources:WebResources, Subject3 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ACCT4" Caption="<%$ Resources:WebResources, Subject4 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ACCT5" Caption="<%$ Resources:WebResources, Subject5 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ACCT6" Caption="<%$ Resources:WebResources, Subject6 %>">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ERR_DESC" Caption="<%$ Resources:WebResources, ErrorDescription %>">
                                    </dx:GridViewDataColumn>
                                      <dx:GridViewDataColumn FieldName="SUPP_ID" Caption="廠商id" Visible ="false" >
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
                <dx:TabPage Text="<%$ Resources:WebResources, ConsignmentCommission %>">
                    <ContentCollection>
                        <dx:ContentControl>
                            <cc:ASPxGridView ClientInstanceName="gvDetail" ID="gvDetail" runat="server" Width="100%"
                                KeyFieldName="SUPPNO"
                                OnHtmlDataCellPrepared="gvDetail_HtmlDataCellPrepared"
                                 OnHtmlRowCreated ="gvDetail_HtmlRowCreated"
                                 OnPageIndexChanged="gvDetail_PageIndexChanged">
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="SUPPNO" Caption="<%$ Resources:WebResources, SupplierNo %>"
                                        VisibleIndex="0">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>"
                                        VisibleIndex="1">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="COMMISSION" Caption="<%$ Resources:WebResources, CommissionRate %>"
                                        VisibleIndex="2">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartMonth %>"  VisibleIndex="3">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndMonth %>"  VisibleIndex="4">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="ERR_DESC" Caption="<%$ Resources:WebResources, ErrorDescription %>"  VisibleIndex="5">
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn FieldName="SUPP_ID"  Caption="廠商id" Visible ="false">
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
