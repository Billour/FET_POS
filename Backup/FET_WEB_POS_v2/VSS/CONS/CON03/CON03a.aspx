<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CON03a.aspx.cs" Inherits="VSS_CONS_CON03a" %>
    
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
<script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
<script type="text/javascript">
      //停止訂購日開始日驗證
        function chkSDate(s, e) {
            e.isValid = true;
            var x = dateFrom.GetValue();
            var y = dateTo.GetValue();

            var dvalue = s.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[開始起日訖值]不允許小於[開始起日起值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {

                var Sx = dateFrom.GetValue();
                var Sy = dateTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue > Sx) || (Sy != "" && dvalue > Sy)) {
                    e.isValid = false;
                    alert("[開始日期]不允許大於[結束日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }

        }
        //停止訂購日結束日驗證
        function chkEDate(s, e) {
            e.isValid = true;
            var x = dateFrom.GetValue();
            var y = dateTo.GetValue();

            var dvalue = s.GetValue(); 
            
            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[結束訖日起值]不允許大於[結束訖日訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {
                var Sx = dateFrom.GetValue();
                var Sy = dateTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue < Sx) || (Sy != "" && dvalue < Sy)) {
                    e.isValid = false;
                    alert("[結束日期]不允許小於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }
        
         //上架開始日驗證
        function chkSupportStartDate(s, e) {
            e.isValid = true;
            var x = SupportStartDateFrom.GetValue();
            var y = SupportStartDateTo.GetValue();

            var dvalue = s.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[開始起日訖值]不允許小於[開始起日起值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {

                var Sx = SupportStartDateFrom.GetValue();
                var Sy = SupportStartDateTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue > Sx) || (Sy != "" && dvalue > Sy)) {
                    e.isValid = false;
                    alert("[開始日期]不允許大於[結束日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }

        }
        //上架結束日驗證
        function chkSupportEndDate(s, e) {
            e.isValid = true;
             var x = SupportStartDateFrom.GetValue();
            var y = SupportStartDateTo.GetValue();

            var dvalue = s.GetValue(); 
            
            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[結束訖日起值]不允許大於[結束訖日訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {
                var Sx = SupportStartDateFrom.GetValue();
                var Sy = SupportStartDateTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue < Sx) || (Sy != "" && dvalue < Sy)) {
                    e.isValid = false;
                    alert("[結束日期]不允許小於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }
           //下架開始日驗證
        function chkSupportExpiryDateFrom(s, e) {
            e.isValid = true;
            var x = SupportExpiryDateFrom.GetValue();
            var y = SupportExpiryDateTo.GetValue();

            var dvalue = s.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[開始起日訖值]不允許小於[開始起日起值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {

                var Sx = SupportExpiryDateFrom.GetValue();
                var Sy = SupportExpiryDateTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue > Sx) || (Sy != "" && dvalue > Sy)) {
                    e.isValid = false;
                    alert("[開始日期]不允許大於[結束日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }

        }
        //上架結束日驗證
        function chkSupportExpiryDateTo(s, e) {
            e.isValid = true;
            var x = SupportExpiryDateFrom.GetValue();
            var y = SupportExpiryDateTo.GetValue();

            var dvalue = s.GetValue(); 
            
            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[結束訖日起值]不允許大於[結束訖日訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {
                var Sx = SupportExpiryDateFrom.GetValue();
                var Sy = SupportExpiryDateTo.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue < Sx) || (Sy != "" && dvalue < Sy)) {
                    e.isValid = false;
                    alert("[結束日期]不允許小於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<asp:Panel ID="PanelPage" runat="server">
    <div class="titlef" align="left">
        <!--寄銷商品查詢作業(門市)-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ConsignmentProductSearchSC %>"></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--廠商編號-->
                        <dx:ASPxLabel ID="Literal2" runat="server" Text="<%$ Resources:WebResources, SupplierNo %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        
                         <uc1:PopupControl ID="txtSupplierNo" runat="server" PopupControlName="ConsignmentVendorsPopup"
                                KeyFieldValue2="SUPP_NO"  />
                    </td>
                    <td class="tdtxt">
                        <!--廠商名稱-->
                        <dx:ASPxLabel ID="Literal24" runat="server" Text="<%$ Resources:WebResources, SupplierName %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                       <uc1:PopupControl ID="ConsignmentVendorsPopup1" runat="server" PopupControlName="ConsignmentVendorsPopup"  
                        IsValidation="false" KeyFieldValue2="SUPP_NAME"/>
                       <%-- <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox1" runat="server" Width="100">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        AutoPostBack="false" SkinID="PopupButton" />
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                    <td class="tdtxt">
                        <!--商品類別-->
                        <dx:ASPxLabel ID="Literal11" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        
                         <dx:ASPxComboBox ID="ddlProductCategory" runat="server">
                            </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--商品編號-->
                        <dx:ASPxLabel ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
<uc1:PopupControl ID="ProductsPopup1" KeyFieldValue1="consignmentsale" KeyFieldValue2="PRODNO" runat="server" PopupControlName="ProductsPopup3"  />                       <%-- <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="TextBox4" runat="server" Width="100">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        AutoPostBack="false" SkinID="PopupButton" />
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                    <td class="tdtxt">
                        <!--商品名稱-->
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductName %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                       <uc1:PopupControl ID="ProductsPopup2" KeyFieldValue1="consignmentsale" KeyFieldValue2="PRODNAME" runat="server" PopupControlName="ProductsPopup3"  />
<%--                        <table cellpadding="0" cellspacing="0" border="0" style="width: 120px">
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                        AutoPostBack="false" SkinID="PopupButton" />
                                </td>
                            </tr>
                        </table>
--%>                    </td>
                    <td class="tdtxt">
                        <!--停止訂購日-->
                        <dx:ASPxLabel ID="Literal12" runat="server" Text="<%$ Resources:WebResources, OrderEndDay %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval" colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal9" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    
                                     <dx:ASPxDateEdit ID="dateFrom" runat="server" ClientInstanceName="dateFrom" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkSDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal10" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dateTo" runat="server"  ClientInstanceName="dateTo" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkEDate(s, e); }"  />                                    
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt">
                        <!--上架日期-->
                        <dx:ASPxLabel ID="Literal4" runat="server" Text="<%$ Resources:WebResources, SupportStartDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    
                                    <dx:ASPxDateEdit ID="SupportStartDateFrom" runat="server"  ClientInstanceName="SupportStartDateFrom" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkSupportStartDate(s, e); }"  />                                    
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="SupportStartDateTo" runat="server"  ClientInstanceName="SupportStartDateTo" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkSupportEndDate(s, e); }"  />                                    
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                        <!--下架日期-->
                        <dx:ASPxLabel ID="Literal8" runat="server" Text="<%$ Resources:WebResources, SupportExpiryDate %>">
                        </dx:ASPxLabel>
                        ：
                    </td>
                    <td class="tdval">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="Literal25" runat="server" Text="<%$ Resources:WebResources, Start %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                   
                                    <dx:ASPxDateEdit ID="SupportExpiryDateFrom" runat="server"  ClientInstanceName="SupportExpiryDateFrom" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkSupportExpiryDateFrom(s, e); }"  />                                    
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="Literal26" runat="server" Text="<%$ Resources:WebResources, End %>">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                     <dx:ASPxDateEdit ID="SupportExpiryDateTo" runat="server"  ClientInstanceName="SupportExpiryDateTo" EditFormatString="yyyy/MM/dd">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkSupportExpiryDateTo(s, e); }"  />                                    
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
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
        <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0" align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" 
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>"　SkinID="ResetButton"/>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%"
                    AutoGenerateColumns="False" EnableRowsCache="False">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="SUPP_NO" Caption="<%$ Resources:WebResources, SupplierNo %>" />
                        <dx:GridViewDataTextColumn FieldName="SUPP_NAME" Caption="<%$ Resources:WebResources, SupplierName %>" />
                        <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                        <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
                        <dx:GridViewDataTextColumn FieldName="PRODTYPENAME" Caption="<%$ Resources:WebResources, ProductCategory %>" />
                        <dx:GridViewDataTextColumn FieldName="S_DATE"  Caption="<%$ Resources:WebResources, SupportStartDate %>" />
                        <dx:GridViewDataTextColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, SupportExpiryDate %>" />
                        <dx:GridViewDataTextColumn FieldName="CEASEDATE" Caption="<%$ Resources:WebResources, OrderEndDay %>" />
                        <dx:GridViewDataTextColumn FieldName="EMPNAME" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <SettingsPager PageSize="20" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                </cc:ASPxGridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    
 <%--   <cc:ASPxPopupControl ID="ASPxPopupControl1" SkinID="ConsignmentVendorsPopup" runat="server"
        PopupElementID="ASPxButton1" TargetElementID="TextBox1" LoadingPanelID="lp">
    </cc:ASPxPopupControl>
     <cc:ASPxPopupControl ID="ASPxPopupControl2" SkinID="ProductsPopup" runat="server"
        PopupElementID="ASPxButton2" TargetElementID="aspxTextBox1" LoadingPanelID="lp">
    </cc:ASPxPopupControl>
    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server" PopupElementID="btnChooseProduct"
        TargetElementID="TextBox4" LoadingPanelID="lp">
    </cc:ASPxPopupControl>
    <dx:ASPxLoadingPanel ID="lp" ru]nat="server" ClientInstanceName="lp">
    </dx:ASPxLoadingPanel>--%>
    </asp:Panel>
</asp:Content>
