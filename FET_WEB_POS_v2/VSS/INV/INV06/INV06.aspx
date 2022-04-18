<%@ Page Language="C#" AutoEventWireup="true" CodeFile="INV06.aspx.cs" Inherits="VSS_INV_INV06_INV06" ValidateRequest="false" MasterPageFile="~/MasterPage.master" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkDate(s, e)
        {
            var sd = transferOutStartDate.GetText(); //退倉起日
            var ed = transferOutStartEndDate.GetText(); //退倉訖日
            if (sd == '' || ed == '')
            {
                return false;
            } else
            {
                if ((new Date(sd)) > (new Date(ed)))
                {
                    alert('【退倉訖日】不可小於【退倉起日】');
                    e.processOnServer = false;
                    return false;
                }
            }
            e.processOnServer = true;
        }

        //變更IMEI圖示
        function ChangeImageIMEI(s, e)
        {
            var fName = "8_lblIMEI_QTY";
            var IMEI_Qty = s; 
            var txtRTNQTY = getClientInstance('TxtBox', s.name.replace(fName, "6_txtRTNQTY"));
            var imgIMEI = getClientInstance('Image', s.name.replace(fName, "7_imgIMEI"));

            if (txtRTNQTY.GetText() == IMEI_Qty.GetText())
            {
                imgIMEI.SetImageUrl("../../../Icon/check.png");
                imgIMEI.SetSize(16, 16);
            }
            else
            {
                imgIMEI.SetImageUrl("../../../Icon/non_complete.png");
                imgIMEI.SetSize(27, 16);
            }
            this.objName = fName;
            this.s = s;
            var pValues = getIMEI(s, e);
            //0:TableName 1:OE_NO 2:PRODNO,4 IMEI_QTY
            PageMethods.IMEIContent(pValues[0], pValues[1], pValues[2], IMEIContent);
        }

        function IMEIContent(values)
        {
            var lblIMEI_QTY = window.document.all[s.name.replace(objName, "8_lblIMEI_QTY")];
            lblIMEI_QTY.attributes["onmouseover"].value = "show('" + values + "');";
            lblIMEI_QTY.attributes["onmouseout"].value = "hide();";
        }

        //取得IMEI上傳參數;
        function getIMEI(s, e)
        {

            var fName = this.objName;
            var pValues = null;
            imeiPop = getClientInstance('TxtBox', s.name.replace(fName, "8_txtIMEI_ASPxPopupControl1"));
            var u = imeiPop.GetContentUrl().split('KeyFieldValue1=');
            if (u.length > 1)
            {
                var oldKeyFieldValue1 = u[1].split('&')[0];
                pValues = oldKeyFieldValue1.split(';');
            }
            return pValues;
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
 <div id="tooltip"></div>
    <div>
        <div class="titlef">
            <!--退倉作業-->
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousing %>"></asp:Literal>
        </div>
        
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉單號-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="RTNNOBox" runat="server" Width="150px">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉日-->
                        <%--<span style="color: Red">*</span>--%>
                           <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, WarehousedDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 120px;">
                                        <dx:ASPxDateEdit ID="transferOutStartDate" ClientInstanceName="transferOutStartDate" runat="server">
                                         <%-- <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" ErrorText="退倉日期不允許空白，請重新輸入" />
                                        </ValidationSettings>--%>
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 120px;">
                                        <dx:ASPxDateEdit ID="transferOutStartEndDate" ClientInstanceName="transferOutStartEndDate" runat="server">
                                        <%--   <ValidationSettings CausesValidation="false">
                                            <RequiredField IsRequired="true" ErrorText="退倉日期不允許空白，請重新輸入" />
                                           </ValidationSettings>--%>
                                        </dx:ASPxDateEdit>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <!--商品料號-->
                        <dx:ASPxTextBox ID="Prodno" runat="server" Width="150px">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--退倉狀態-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ReturnWarehousingStatus %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxComboBox ID="CbType" runat="server" Width="120px">
                            <Items>
                                <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                                <dx:ListEditItem Text="未完成" Value="10" />
                                <dx:ListEditItem Text="已完成" Value="60" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click">
                            <ClientSideEvents Click="function(s,e){checkDate(s,e);}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton"/>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNN_ID"
                    Width="100%"
                    OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" 
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                    OnPageIndexChanged="gvMaster_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="RTNNO" runat="server" Caption="<%$ Resources:WebResources, ReturnWarehousingReceiptNo %>">
                            <DataItemTemplate>
                                <asp:LinkButton ID="Label1" runat="server" Text='<%# Bind("RTNNO") %>'  CommandName="Select"
                                    OnCommand="CommandButton_Click" CommandArgument='<%# Eval("RTNN_ID") %>'>
                                </asp:LinkButton>
                            </DataItemTemplate>
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="B_DATE" Caption="<%$ Resources:WebResources, ReturnWarehousingStartDate %>" />
                        <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, ReturnWarehousingEndDate %>" />
                        <dx:GridViewDataColumn FieldName="RTNDATE" Caption="<%$ Resources:WebResources, WarehousedDate %>" />
                        <dx:GridViewDataColumn FieldName="STATUS" Caption="<%$ Resources:WebResources, ReturnWarehousingStatus %>" />
                        <dx:GridViewDataColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>" />
                        <dx:GridViewDataColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" />
                    </Columns>
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <SettingsPager PageSize="5"></SettingsPager>
                </cc:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
