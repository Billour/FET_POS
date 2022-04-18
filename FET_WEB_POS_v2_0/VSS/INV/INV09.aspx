<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="INV09.aspx.cs" Inherits="VSS_INV09_INV09" %>

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
                            <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Search %>"
                                AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){ document.location='INV08.aspx'; }" />
                            </dx:ASPxButton>
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
                                <dx:ASPxLabel ID="Label1" runat="server" Text="2104 天母">
                                </dx:ASPxLabel>
                            </td>
                            <td class="tdtxt">
                            </td>
                            <td class="tdval">
                            </td>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單狀態-->
                                <asp:Literal ID="Literal3" runat="server" Text="訂單狀態"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="Label2" runat="server" Text="00 未存檔">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--驗收單編號-->
                                <asp:Literal ID="Literal4" runat="server" Text="驗收單編號"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="Label7" runat="server" Text="SR2104-1007001 ">
                                </dx:ASPxLabel>
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
                                <dx:ASPxLabel ID="Label3" runat="server" Text="10/07/12 15:00">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--PO/OE_NO-->
                                <asp:Literal ID="Literal6" runat="server" Text="PO/OE_NO"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="Label5" runat="server" Text="001-1">
                                </dx:ASPxLabel>
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
                                <dx:ASPxLabel ID="Label4" runat="server" Text="64591 李家駿">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt" nowrap="nowrap">
                                <!--訂單/主配編號-->
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, OrderNoOrDistributionNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval" nowrap="nowrap">
                                <dx:ASPxLabel ID="Label6" runat="server" Text="HR1007002 ">
                                </dx:ASPxLabel>
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
            <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="商品編號"
                Width="100%" OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged"
                OnCustomColumnDisplayText="gvMaster_CustomColumnDisplayText">
                <Columns>
                    <dx:GridViewDataColumn FieldName="商品編號" Caption="<%$ Resources:WebResources, ProductCode %>"
                        VisibleIndex="0" />
                    <dx:GridViewDataColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>"
                        VisibleIndex="1" />
                    <dx:GridViewDataCheckColumn HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center"
                        FieldName="IMEI檢核" Caption="IMEI檢核" VisibleIndex="2">
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataColumn FieldName="到貨量" Caption="<%$ Resources:WebResources, ArrivalQuantity %>"
                        VisibleIndex="3" />
                    <dx:GridViewDataColumn FieldName="驗收量" runat="server" Caption="驗收量" VisibleIndex="4">
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txt1" runat="server" Text='<%# Bind("[驗收量]") %>' Width="60px"
                                Enabled="false">
                            </dx:ASPxTextBox>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="IMEI" runat="server" Caption="IMEI" VisibleIndex="5">
                        <DataItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lbl1" runat="server" Text='<%# Bind("[IMEI]") %>' Width="10px"
                                            Enabled="false" DisabledStyle-Font-Underline="true">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="Button2" runat="server" Text="選" Visible="true" AutoPostBack="false"
                                            ClientSideEvents-Click="function(s,e){openwindow('../SAL/SAL01_inputIMEIData.aspx',500,400);return false;}">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="在途量" Caption="在途量" VisibleIndex="6" />
                    <dx:GridViewDataColumn FieldName="供貨商" Caption="供貨商" CellStyle-Font-Underline="true"
                        VisibleIndex="7" />
                </Columns>
                <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                <SettingsPager PageSize="5">
                </SettingsPager>
            </cc:ASPxGridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div> </div>
    <br />
    <br />
    <div class="btnPosition">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Save %>"
                        OnClick="btnSave_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="條碼列印" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
