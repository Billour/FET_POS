<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductsPopup.aspx.cs" Inherits="VSS_TSAL01_ProductsPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>商品編號查詢</title>

	<script type="text/javascript">
		function OnInit(s, e) {
			s.GetInputElement().name = "radioChoose";
		}
		function PD_OnCloseUp() {
			alert(window.parent.productsPopup);
		}
		function returnChoosed(obj, val) {
			opener.prodChooseDone(obj, val);
		}
	</script>

</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table cellpadding="0" cellspacing="10" border="0">
			<tr>
				<td>
					<!--商品分類-->
					<asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCategory1 %>"></asp:Literal>：
				</td>
				<td>
					<dx:ASPxComboBox ID="cboCategory" runat="server" Width="200">
						<%--<Items>
                                    <dx:ListEditItem Value="" Text="-請選擇-" Selected="true" />
                                    <dx:ListEditItem Value="20" Text="在途" />
                                    <dx:ListEditItem Value="30" Text="已撥入" />
                                </Items>--%>
					</dx:ASPxComboBox>
				</td>
			</tr>
			<tr>
				<td>
					<!--商品編號-->
					<asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
				</td>
				<td>
					<dx:ASPxTextBox ID="TextBox1" runat="server" Width="100">
					</dx:ASPxTextBox>
				</td>
				<td>
					<!--商品名稱-->
					<asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
				</td>
				<td>
					<dx:ASPxTextBox ID="TextBox6" runat="server" Width="100">
					</dx:ASPxTextBox>
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
					<dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
						SkinID="ResetButton" />
				</td>
			</tr>
		</table>
	</div>
	<div class="seperate">
	</div>
	<cc:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="PRODNO"
		Width="100%" OnPageIndexChanged="grid_PageIndexChanged">
		<Columns>
			<dx:GridViewDataCheckColumn VisibleIndex="0" Width="10">
				<DataItemTemplate>
					<input type="radio" name="radioButton" />
				</DataItemTemplate>
			</dx:GridViewDataCheckColumn>
			<dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
			<dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
			<dx:GridViewDataColumn FieldName="PRODTYPENAME" Caption="<%$ Resources:WebResources, ProductCategory1 %>" />
		</Columns>
		<SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="false" />
		<SettingsPager PageSize="5">
		</SettingsPager>
		<SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
		<ClientSideEvents FocusedRowChanged="function(s, e) {
                               if(s.GetFocusedRowIndex() == -1)
                                    return;
                               var row = s.GetRow(s.GetFocusedRowIndex());
                               
                                if(__aspxIE)
                                    row.cells[0].childNodes[0].checked = true;
                                else
                                    row.cells[0].childNodes[1].checked = true;
                            }" />
	</cc:ASPxGridView>
	<div class="seperate">
	</div>
	<div class="btnPosition">
		<table cellpadding="0" cellspacing="0" border="0" align="center">
			<tr>
				<td>
					<dx:ASPxButton ID="okButton" runat="server" Text="<%$ Resources:WebResources, Ok %>"
						OnClick="OkButton_Click" />
				</td>
				<td>
					&nbsp;
				</td>
				<td>
					<dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
						<ClientSideEvents Click="function(s, e) { window.close(); }" />
					</dx:ASPxButton>
				</td>
			</tr>
		</table>
	</div>
	</form>
</body>
</html>
