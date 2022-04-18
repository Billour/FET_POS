<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPL024.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_RPT_RPL024" %>
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
   
    <div class="titlef" align="left">
        <!--�վ���Ӫ�-->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, RPL024 %>"></asp:Literal>
    </div>
    
    <div class="seperate"></div>

    <div class="criteria">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
           <tr>
                <td class="tdtxt">
                    <!--�վ����-->
                    <asp:Literal ID="lblSTORE_NO" runat="server" Text="<%$ Resources:WebResources, AdjustmentStore %>"></asp:Literal>�G
                </td>
                 
                <td class="tdval">
                     <table style="width: 250px">
                        <tr>
                            <td>
                                  <asp:Literal ID="lblSTORE_NO_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td width="100px">
                            <uc1:PopupControl ID="txtSTORE_NO_S" runat="server" IsValidation="false" PopupControlName="StoresPopup" />
                                  <%--<dx:ASPxTextBox ID="txtSTORE_NO_S" runat="server" Width="100px"></dx:ASPxTextBox>--%>
                            </td>
                            <td>
                                  <asp:Literal ID="lblSTORE_NO_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td width="100px"> 
                            <uc1:PopupControl ID="txtSTORE_NO_E" runat="server" IsValidation="false" PopupControlName="StoresPopup" />    
                                  <%--<dx:ASPxTextBox ID="txtSTORE_NO_E" runat="server" Width="100px"></dx:ASPxTextBox>--%>
                            </td>
                         </tr>
                     </table>
                </td>
                
                <td class="tdtxt">
                    <!--�վ����G-->
                    <asp:Literal ID="lblADJDATE" runat="server" Text="<%$ Resources:WebResources, AdjustmentDate %>"></asp:Literal>�G
                </td>
                
                <td class="tdval">
                    <table style="width: 250px">
                        <tr>
                            <td>
                                <asp:Literal ID="lblADJDATE_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtADJDATE_S" runat="server" ClientInstanceName="txtSDate">
                                    <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="lblADJDATE_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="txtADJDATE_E" runat="server" ClientInstanceName="txtEDate">
                                   <ClientSideEvents ValueChanged="function(s, e){ chkDate(s, e); }"  />
                                </dx:ASPxDateEdit>
                            </td>
                            
                        </tr>
                    </table>
                </td>
           </tr>
           <tr>
                <td class="tdtxt">
                    <!--�վ�ܧO-->
                    <asp:Literal ID="lblStock" runat="server" Text="<%$ Resources:WebResources, AdjustmentStock %>"></asp:Literal>�G
                </td>
                 
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlStock" runat="server" Width="120px" SelectedIndex="0" ValueType="System.String"></dx:ASPxComboBox>
                </td>
           </tr>
           <tr>
                <td class="tdtxt">
                    <!--�վ��]-->
                    <asp:Literal ID="lblReason" runat="server" Text="<%$ Resources:WebResources, AdjustmentReason %>"></asp:Literal>�G
                </td>
                     
                <td class="tdval">
                    <dx:ASPxComboBox ID="ddlReason" runat="server" Width="120px" SelectedIndex="0" ValueType="System.String"></dx:ASPxComboBox>
                </td>
                 
                <td class="tdtxt">
                    <!--�ӫ~�s��-->
                    <asp:Literal ID="lblPRODNO" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>�G
                </td>
                 
                <td class="tdval">
                     <table style="width: 250px">
                        <tr>
                            <td><asp:Literal ID="lblPRODNO_S" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                            <td width="130px"><uc1:PopupControl ID="txtPRODNO_S" runat="server" IsValidation="false" PopupControlName="ProductsPopup"  /></td>
                            <td><asp:Literal ID="lblPRODNO_E" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                            <td width="130px"><uc1:PopupControl ID="txtPRODNO_E" runat="server" IsValidation="false" PopupControlName="ProductsPopup"  /></td>
                        </tr>
                     </table>
                </td>
           </tr>         
        </table>
    </div>
    
    <div class="seperate"></div>
    
    <table align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnSearch" runat="server" 
                    Text="<%$ Resources:WebResources, Search %>" onclick="btnSearch_Click" ></dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"/>
                
            </td>
            <td> 
                <dx:ASPxButton ID="btnExport" runat="server" Text="<%$ Resources:WebResources, Export %>" 
                    onclick="btnExport_Click"></dx:ASPxButton>
            </td>
        </tr>
    </table>
                
    <div class="seperate"></div> 
          
    <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" Width="100%" 
        onpageindexchanged="gvMaster_PageIndexChanged" >
          <Columns>
                   <dx:GridViewDataColumn FieldName="<%$ Resources:WebResources, StoreName %>" Caption="<%$ Resources:WebResources, StoreName %>" />
                <dx:GridViewDataTextColumn FieldName="�վ���" Caption="�վ���"></dx:GridViewDataTextColumn>
                     <dx:GridViewDataTextColumn FieldName="�վ�渹" Caption="�վ�渹"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="�ӫ~�s��" Caption="�ӫ~�s��"></dx:GridViewDataTextColumn>
            
                <dx:GridViewDataTextColumn FieldName="�ӫ~�W��" Caption="�ӫ~�W��"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="�վ�ܧO" Caption="�վ�ܧO"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="�վ�ƶq" Caption="�վ�ƶq"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="�վ��]" Caption="�վ��]"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="�|�p���" Caption="�|�p���"></dx:GridViewDataTextColumn>
          </Columns>
        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        <SettingsPager PageSize="10">
        </SettingsPager>
    </cc:ASPxGridView>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvMaster" ></dx:ASPxGridViewExporter>   

</asp:Content>
