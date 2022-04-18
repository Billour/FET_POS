<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL026.aspx.cs" MasterPageFile="~/MasterPage.master"
	Inherits="VSS_RPT_RPL026" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
	<div class="titlef" align="left">
		<!--在途明細表-->
		<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL026 %>"></asp:Literal>
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
					<table style="width: 250px">
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
				<td class="tdtxt">
					<!--交易日期-->
					<asp:Literal ID="lblTRADE_DATE" runat="server" Text="<%$ Resources:WebResources, TradeDate %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<table style="width: 250px">
						<tr>
							<td>
								<asp:Literal ID="lblTRADE_DATE_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
							</td>
							<td>
								<dx:ASPxDateEdit ID="txtTRADE_DATE_S" runat="server" ClientInstanceName="txtSDate">
									<ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
								</dx:ASPxDateEdit>
							</td>
							<td>
								<asp:Literal ID="lblTRADE_DATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
							</td>
							<td>
								<dx:ASPxDateEdit ID="txtTRADE_DATE_E" runat="server" ClientInstanceName="txtEDate">
									<ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }" />
								</dx:ASPxDateEdit>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					<!--調出門市-->
					<asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, OutputStore %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<table style="width: 250px">
						<tr>
							<td>
								<asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
							</td>
							<td>
								<uc1:PopupControl ID="txtOUT_STORE_NO_S" runat="server" PopupControlName="StoresPopup" />
							</td>
							<td>
								<asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
							</td>
							<td>
								<uc1:PopupControl ID="txtOUT_STORE_NO_E" runat="server" PopupControlName="StoresPopup" />
							</td>
						</tr>
					</table>
				</td>
				<td class="tdtxt">
					<!--交易類別-->
					<asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, TradeType %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<dx:ASPxComboBox ID="cbTradeType" runat="server" Width="120px" AutoPostBack="true"
						OnSelectedIndexChanged="cbTradeType_SelectedIndexChanged">
						<Items>
							<dx:ListEditItem Text="ALL" Value="" Selected="true" />
							<dx:ListEditItem Text="進貨" Value="進貨" />
							<dx:ListEditItem Text="調撥" Value="調撥" />
						</Items>
					</dx:ASPxComboBox>
				</td>
			</tr>
			<tr>
				<td class="tdtxt">
					<!--調入門市-->
					<asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, InputStore %>"></asp:Literal>：
				</td>
				<td class="tdval">
					<table style="width: 250px">
						<tr>
							<td>
								<asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
							</td>
							<td>
								<uc1:PopupControl ID="txtIN_STORE_NO_S" runat="server" PopupControlName="StoresPopup" />
							</td>
							<td>
								<asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
							</td>
							<td>
								<uc1:PopupControl ID="txtIN_STORE_NO_E" runat="server" PopupControlName="StoresPopup" />
							</td>
						</tr>
					</table>
				</td>
				<td class="tdtxt">
					<!--商品編號-->
					<asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, ProductCode %>" />：
				</td>
				<td class="tdval">
					<table style="width: 300px">
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
	<cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
		Width="100%" OnPageIndexChanged="gvMaster_PageIndexChanged"
        onhtmlrowcreated="gvMaster_HtmlRowCreated" 
        onhtmlrowprepared="gvMaster_HtmlRowPrepared">
		<Columns>
			<dx:GridViewDataColumn FieldName="SALE_TYPE" Caption="<%$ Resources:WebResources, TradeType %>" />
			<dx:GridViewDataColumn FieldName="STDATE" Caption="<%$ Resources:WebResources, AllocationDate %>" />
			<dx:GridViewDataColumn FieldName="SEQNO" Caption="<%$ Resources:WebResources, AllocationNo %>" />
			<dx:GridViewDataColumn FieldName="FROM_STORE" Caption="<%$ Resources:WebResources, OutputStore %>" />
			<dx:GridViewDataColumn FieldName="TO_STORE" Caption="<%$ Resources:WebResources, OrderInputStore %>" />
			<dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
			<dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName%>" />
			<dx:GridViewDataColumn FieldName="TRANOUTQTY" Caption="<%$ Resources:WebResources, InOutQty %>" />
			<dx:GridViewDataColumn FieldName="TRANINQTY" Caption="<%$ Resources:WebResources, AcceptanceQTY %>" />
			<dx:GridViewDataColumn FieldName="ETDATE" Caption="<%$ Resources:WebResources, AcceptanceDate %>" />
			<dx:GridViewDataColumn FieldName="TO_EMPNAME" Caption="<%$ Resources:WebResources, AcceptanceEmp %>" />
			<dx:GridViewDataColumn FieldName="NOTYETQTY" Caption="<%$ Resources:WebResources, UnAcceptanceQTY %>" />
			<dx:GridViewDataTextColumn FieldName="DTM" Caption="<%$ Resources:WebResources, UnAcceptanceDays %>" />
		</Columns>
		<SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
		<SettingsPager PageSize="10">
		</SettingsPager>
	</cc:ASPxGridView>
	<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster">
	</dx:ASPxGridViewExporter>
</asp:Content>
