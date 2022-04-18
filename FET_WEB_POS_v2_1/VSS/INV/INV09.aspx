<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="INV09.aspx.cs" Inherits="VSS_INV_INV09" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        var mx;
        var my;
        jQuery(document).mousemove(function(event) {
            mx = event.pageX;
            my = event.pageY;
        });

        function show(content) {
            var tip = $("#tooltip");
            tip.html(content);
            tip.css({
                display: "",
                left: mx - 150,
                top: my,
                position: "absolute",
                background: "#FFFFFF"
            });

        }
        function hide() {
            var tip = $("#tooltip");
            tip.css("display", "none");

        }

        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }

        function imeiclick(con) {
            openwindow("../SAL/SAL01_inputIMEIData.aspx", 720, 300);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="tooltip">
    </div>
    <div>
        <div class="func">
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                    <tr>
                        <td align="left">
                            <!--進貨驗收作業-->
                            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ReceivingInspection %>"></asp:Literal>
                        </td>
                        <td align="right">
                            <dx:aspxbutton id="Button4" runat="server" text="<%$ Resources:WebResources, QueryEdit %>"
                                autopostback="false">
                                <ClientSideEvents Click="function(s, e){ document.location='INV08.aspx'; }" />
                            </dx:aspxbutton>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div class="criteria">
                    <table>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--門市名稱-->
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:aspxlabel id="Label1" runat="server" text="2104 天母">
                                </dx:aspxlabel>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單狀態-->
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, OrderStatus %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:aspxlabel id="Label2" runat="server" text="00 未存檔">
                                </dx:aspxlabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--驗收單編號-->
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ReceivingNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:aspxlabel id="Label7" runat="server">
                                </dx:aspxlabel>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--更新日期-->
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ModifiedDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:aspxlabel id="Label3" runat="server" text="10/07/12 15:00">
                                </dx:aspxlabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--PO/OE_NO-->
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, pooeno %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:aspxlabel id="Label5" runat="server">
                                </dx:aspxlabel>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--更新人員-->
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ModifiedBy %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:aspxlabel id="Label4" runat="server" text="64591 李家駿">
                                </dx:aspxlabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單/主配編號-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:aspxlabel id="Label6" runat="server">
                                </dx:aspxlabel>
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
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div class="seperate">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc:aspxgridview id="gvMaster" clientinstancename="gvMaster" runat="server" keyfieldname="商品編號"
                width="100%" onhtmlrowcreated="gvMaster_HtmlRowCreated" onpageindexchanged="gvMaster_PageIndexChanged"
                oncustomcolumndisplaytext="gvMaster_CustomColumnDisplayText">
                <Columns>
                    <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                        VisibleIndex="0" />
                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                        VisibleIndex="1" />
                    <dx:GridViewDataCheckColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center"
                        FieldName="IMEI檢核" Caption="IMEI檢核" VisibleIndex="2" Visible="false" >
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataColumn FieldName="到貨量" Caption="<%$ Resources:WebResources, ArrivalQuantity %>"
                        VisibleIndex="3" />
                    <dx:GridViewDataColumn FieldName="驗收量" runat="server" Caption="<%$ Resources:WebResources, InspectionQuantity %>" VisibleIndex="4">
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txt1" runat="server" Text='<%# Bind("[驗收量]") %>' Width="60px">
                            </dx:ASPxTextBox>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="" VisibleIndex="5">
                        <DataItemTemplate>
                            <dx:ASPxImage ID="imgIMEI" runat="server" ImageUrl="">
                            </dx:ASPxImage>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="IMEI" runat="server" Caption="<%$ Resources:WebResources, imei %>" VisibleIndex="6">
                        <DataItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbl1" runat="server" Text='<%# Bind("[IMEI]") %>' Width="20px"
                                            Enabled="false" DisabledStyle-Font-Underline="true">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                       <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="InputIMEIData" />
                                    </td>
                                 <%--   <td>
                                        <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="120px" Text="">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="<%$ Resources:WebResources, choose %>" Visible="true" AutoPostBack="false"
                                            SkinID="PopupButton" ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_inputIMEIData.aspx',500,400);return false;}">
                                        </dx:ASPxButton>
                                    </td>--%>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="在途量" Caption="<%$ Resources:WebResources, OnOrderQuantity %>" VisibleIndex="7" />
                    <dx:GridViewDataColumn FieldName="供貨商" Caption="<%$ Resources:WebResources, Supplier %>" CellStyle-Font-Underline="true"
                        VisibleIndex="8">
                          <DataItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lb13" runat="server" Text='<%# Bind("[供貨商]") %>' 
                                            Enabled="false" DisabledStyle-Font-Underline="true">
                                        </dx:ASPxLabel>
                                    </td>
                                     </tr>
                            </table>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsPager PageSize="5">
                </SettingsPager>
            </cc:aspxgridview>
            <cc:aspxgridview id="gvDetail" clientinstancename="gvDetail" runat="server" keyfieldname="商品編號"
                width="100%" onhtmlrowcreated="gvDetail_HtmlRowCreated" onpageindexchanged="gvDetail_PageIndexChanged"
                oncustomcolumndisplaytext="gvDetail_CustomColumnDisplayText">
                <Columns>
                    <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                        VisibleIndex="0" />
                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                        VisibleIndex="1" />
                    <dx:GridViewDataCheckColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center"
                        FieldName="IMEI檢核" Caption="<%$ Resources:WebResources, VerifyImei %>" VisibleIndex="2" Visible="false">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataColumn FieldName="到貨量" Caption="<%$ Resources:WebResources, ArrivalQuantity %>"
                        VisibleIndex="3" />
                    <dx:GridViewDataColumn FieldName="驗收量" runat="server" Caption="<%$ Resources:WebResources, InspectionQuantity %>" VisibleIndex="4" />
                    <dx:GridViewDataColumn Caption="" VisibleIndex="5">
                        <DataItemTemplate>
                            <dx:ASPxImage ID="imgIMEI2" runat="server" ImageUrl="">
                            </dx:ASPxImage>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="IMEI" runat="server"  Caption="<%$ Resources:WebResources, imei %>" VisibleIndex="6">
                        <DataItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbl2" runat="server" Text='<%# Bind("[IMEI]") %>' Width="10px"
                                            Enabled="false" DisabledStyle-Font-Underline="true">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="InputIMEIData" />
                                    </td>
                                        <td>
                                            <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="120px" Text="">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton3" runat="server" Text="<%$ Resources:WebResources, choose %>" Visible="true" AutoPostBack="false"
                                                SkinID="PopupButton" ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_inputIMEIData.aspx',500,400);return false;}">
                                            </dx:ASPxButton>
                                        </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                    
                    <dx:GridViewDataColumn FieldName="供貨商" Caption="<%$ Resources:WebResources, Supplier %>" CellStyle-Font-Underline="true"
                        VisibleIndex="7" >
                           <DataItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lb14" runat="server" Text='<%# Bind("[供貨商]") %>' 
                                            Enabled="false" DisabledStyle-Font-Underline="true">
                                        </dx:ASPxLabel>
                                    </td>
                                     </tr>
                            </table>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsPager PageSize="5">
                </SettingsPager>
            </cc:aspxgridview>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <div class="btnPosition">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:aspxbutton id="btnSave" runat="server" text="<%$ Resources:WebResources, Save %>" 
                        onclick="btnSave_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:aspxbutton id="btnCancel" runat="server" text="<%$ Resources:WebResources, Cancel %>" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:aspxbutton id="ASPxButton1" runat="server" text="<%$ Resources:WebResources, PrintBarCode %>"  />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
