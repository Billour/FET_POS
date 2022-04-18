<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL032.aspx.cs" MasterPageFile="~/MasterPage.master"
	Inherits="VSS_RPT_RPL032" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckDate(s, e) {
            var x = txtSDate.GetValue();
            var y = txtEDate.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if ((x != "" && y != "")) {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("訂單日期起值不可大於訂單日期訖值!!");
                    s.SetValue(null);
                    return false;
                }
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
	<div class="titlef" align="left">
		<!--門市銷售量分析表-->
		<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL032 %>"></asp:Literal>
	</div>
	<div class="seperate">
	</div>
	<div class="criteria">
		<table width="100%" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td class="tdtxt">
					<!--門市編號-->
					<asp:Literal ID="lblSTORE_NO" runat="server" Text="門市編號"></asp:Literal>：
				</td>
				<td class="tdval">
					<table style="width: 400px">
						<tr>
							<td>
								<asp:Literal ID="lblSTORE_NO_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
							</td>
							<td>
								<uc1:PopupControl ID="txtSTORE_NO_S" runat="server" PopupControlName="StoresPopup" />
							</td>
							<td>
								<asp:Literal ID="lblSTORE_NO_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
							</td>
							<td>
								<uc1:PopupControl ID="txtSTORE_NO_E" runat="server" PopupControlName="StoresPopup" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					<!--商品編號-->
					<asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
				</td>
				<td class="tdval">
					<table style="width: 400px">
						<tr>
							<td>
								<asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
							</td>
							<td>
								<uc1:PopupControl ID="txtPRODNO_S" runat="server" PopupControlName="ProductsPopup" />
							</td>
							<td>
								<asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
							</td>
							<td>
								<uc1:PopupControl ID="txtPRODNO_E" runat="server" PopupControlName="ProductsPopup" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					<!--訂單日期-->
					<asp:Literal ID="lblTRADE_DATE" runat="server" Text="<%$ Resources:WebResources, OrderDate %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<table style="width: 250px">
						<tr>
							<td>
								<asp:Literal ID="lblTRADE_DATE_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
							</td>
							<td>
								<dx:ASPxDateEdit ID="txtTRADE_DATE_S" runat="server" ClientInstanceName="txtSDate" EditFormatString="yyyy/MM/dd">
									<ClientSideEvents ValueChanged="function(s, e){ CheckDate(s, e); }" />
								</dx:ASPxDateEdit>
							</td>
							<td>
								<asp:Literal ID="lblTRADE_DATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
							</td>
							<td>
								<dx:ASPxDateEdit ID="txtTRADE_DATE_E" runat="server" ClientInstanceName="txtEDate" EditFormatString="yyyy/MM/dd">
									<ClientSideEvents ValueChanged="function(s, e){ CheckDate(s, e); }" />
								</dx:ASPxDateEdit>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</div>
	<div class="seperate">
	</div>
	<table align="center">
		<tr>
			<td>
				<dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
					OnClick="btnSearch_Clicked">
				</dx:ASPxButton>
			</td>
			<td>
				<dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
					OnClick="btnReset_Clicked">
				</dx:ASPxButton>
			</td>
			<td>
				<dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>"
					OnClick="btnExport_Click" />
			</td>
		</tr>
	</table>
	<div class="seperate">
	</div>
	<cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ITEM_NO"
		Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged">
		<Columns>
			<dx:GridViewDataColumn FieldName="ZONE_NAME" Caption="<%$ Resources:WebResources, Zone %>" />
			<dx:GridViewDataColumn FieldName="STORE_NO" Caption="門市編號" />
			<dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>"/>
			<dx:GridViewDataColumn FieldName="ORDDATE" Caption="<%$ Resources:WebResources, OrderDate %>" >
			
			</dx:GridViewDataColumn>
			<dx:GridViewDataColumn FieldName="ORDER_NO" Caption="<%$ Resources:WebResources, PurchaseNo %>" />
			<dx:GridViewDataColumn FieldName="SEQNO" Caption="<%$ Resources:WebResources, PurchaseItem %>" />
			<dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
			<dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
			<dx:GridViewDataColumn FieldName="VENDER_NAME" Caption="<%$ Resources:WebResources, Suppliername %>" />
			<dx:GridViewDataColumn FieldName="ORDQTY" Caption="<%$ Resources:WebResources, AdjustmentBefore %>" />
			<dx:GridViewDataColumn FieldName="HQ_ADJ_ORDER_QTY" Caption="<%$ Resources:WebResources, AdjustmentAfter %>" />
			<dx:GridViewDataColumn FieldName="ADJ_QTY" Caption="<%$ Resources:WebResources, AdjustmentQTY %>" />
			<dx:GridViewDataColumn FieldName="REGULATION" Caption="<%$ Resources:WebResources, AdjustmentRate %>" />
			<dx:GridViewDataColumn FieldName="REAL_ATR_QTY" Caption="<%$ Resources:WebResources, RealOrderQTY %>" />
			<dx:GridViewDataColumn FieldName="APPROVE_RATE" Caption="<%$ Resources:WebResources, RealSuplieRate %>" />
			<dx:GridViewDataColumn FieldName="INCOUNTQTY" Caption="<%$ Resources:WebResources, GetQTY %>" />
			<dx:GridViewDataColumn FieldName="INWAYQTY"	Caption="<%$ Resources:WebResources, UnGetQTY %>" />
			<dx:GridViewDataColumn FieldName="INCOUNT_RATE"	Caption="<%$ Resources:WebResources, GetRate %>" />
			<dx:GridViewDataColumn FieldName="REMARK"	Caption="<%$ Resources:WebResources, Remark %>" />
		</Columns>
		<SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
		<SettingsPager PageSize="10">
		</SettingsPager>
	</cc:ASPxGridView>
	<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
	</dx:ASPxGridViewExporter>
</asp:Content>
