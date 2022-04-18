<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL062.aspx.cs" Inherits="VSS_RPT_RPL062"
    MasterPageFile="~/MasterPage.master" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef" align="left">
        <!--調出簽收單-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL062 %>"></asp:Literal>
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            </table>
    </div>
    <div class="seperate">
    </div>
    <div class="seperate">
    </div>
    <div class="criteria">
       <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tdtxt">
                        <!--移撥單號-->
                    <asp:Literal ID="Literal3"  runat="server" Text="<%$ Resources:WebResources, TransferSlipNo %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="txtTransferSlipNo" MaxLength="20" Width="150px" runat='server' CssClass="tbSpanWidth" >
                    </dx:ASPxTextBox>
                </td>
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
				 <td>
				 
				     &nbsp;</td>
            </tr>
			<tr>
			    <td class="tdtxt">
                <!-- 調出日期-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, OutDate %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxDateEdit ID="txtOutDate" ClientInstanceName="txtOutDate"  runat="server" EditFormatString="yyyy/MM/dd">
                    </dx:ASPxDateEdit>
                </td>
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
				<td>
				    &nbsp;</td>
			</tr>
			<tr><td>&nbsp;</td></tr>
		</table>
    </div>
    <table align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnSearch" runat="server" 
                    Text="<%$ Resources:WebResources, Search %>" onclick="btnSearch_Click">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                    AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false" 
                    onclick="btnReset_Click">
                    <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" /></dx:ASPxButton></td><td> 
                <dx:ASPxButton ID="btnExport" runat="server" Text="匯出" 
                    onclick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table align="center">
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="RTNNO"
        Width="100%" onpageindexchanged="gvMaster_PageIndexChanged">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName%>" />
            <dx:GridViewDataColumn FieldName="PROD_QTY" Caption="<%$ Resources:WebResources, Quantity %>" />
            <dx:GridViewDataColumn FieldName="IMEI" Caption="IMEI" />
        </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>
    <div class="seperate">
    </div>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" ></dx:ASPxGridViewExporter>   
</asp:Content>
