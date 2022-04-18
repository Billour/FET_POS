<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSAL05_DiscountDetail.aspx.cs"
    Inherits="VSS_SAL_TSAL05_DiscountDetail" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxDataView" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc:ASPxGridView ID="gvMaster" runat="server" ClientInstanceName="gvMaster" KeyFieldName="DISCOUNT_ID"
            AutoGenerateColumns="False" Width="100%" OnHtmlRowCreated="gvMaster_HtmlRowCreated">
            <Columns>
                <%-- 折扣料號--%>
                <dx:GridViewDataDateColumn FieldName="DISCOUNT_ID" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"
                    HeaderStyle-HorizontalAlign="Center" />
                <%-- 折扣名稱--%>
                <dx:GridViewDataDateColumn FieldName="DISCOUNT_NAME" Caption="<%$ Resources:WebResources, DiscountName %>"
                    HeaderStyle-HorizontalAlign="Center" />
                <dx:GridViewDataTextColumn FieldName="DISCOUNT_AMOUNT" HeaderStyle-HorizontalAlign="Center">
                    <HeaderCaptionTemplate>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="折扣金額">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="DISCOUNT_B_DATE" HeaderStyle-HorizontalAlign="Center">
                    <HeaderCaptionTemplate>
                        <dx:ASPxLabel ID="Literal7" runat="server" Text="開始日期">
                        </dx:ASPxLabel>
                    </HeaderCaptionTemplate>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataDateColumn FieldName="DISCOUNT_B_DATE" Caption="結束日期" HeaderStyle-HorizontalAlign="Center"
                    EditCellStyle-HorizontalAlign="Right" />
            </Columns>
            <Templates>
                <DetailRow>
                    <table cellpadding="2" cellspacing="1" style="border-collapse: collapse; width: 96%">
                        <tr>
                            <td style="font-weight: bold;width: 60px">
                                費率:
                            </td>
                            <td>
                                <dx:ASPxLabel ID="lbVALUE" runat="server" Text="">                               
                               </dx:ASPxLabel>
                            </td>
                            <td style="font-weight: bold">
                                商品料號:
                            </td>
                            <td>                                
                               <dx:ASPxLabel ID="lbPRODNO_NAME" runat="server" Text="">                               
                               </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">
                                指定門市:
                            </td>
                            <td>
                               <dx:ASPxLabel ID="lbSTOREID_NAME" runat="server" Text="">
                                </dx:ASPxLabel>
                            </td>
                            
                            <td style="font-weight: bold">
                                客戶對象:
                            </td>
                            <td>
                                <dx:ASPxLabel ID="lbCUST" runat="server" Text="">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">
                                促銷代號:
                            </td>
                            <td colspan="3">
                                <dx:ASPxLabel ID="lbPROMOID_NAME" runat="server" Text="">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                    </table>
                </DetailRow>
            </Templates>
            <SettingsPager PageSize="5" />
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
        </cc:ASPxGridView>
    </div>
    </form>
</body>
</html>
