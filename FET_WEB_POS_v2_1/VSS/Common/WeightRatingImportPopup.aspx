<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeightRatingImportPopup.aspx.cs" Inherits="VSS_Common_WeightRatingImportPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>權重佔比分配檔匯入</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="criteria">
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>" ></dx:ASPxLabel>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server" />
                    </td>                                                
                </tr>                    
            </table>
        </div>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td><dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" OnClick="btnImport_Click" /></td>
                    <td>&nbsp;</td>
                    <td><dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, Reset %>" /></td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>

        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" 
            KeyFieldName="門市編號" Width="100%" 
            onhtmldatacellprepared="gvMaster_HtmlDataCellPrepared" 
            onhtmlfootercellprepared="gvMaster_HtmlFooterCellPrepared">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="門市編號" runat="server" Caption="<%$ Resources:WebResources, StoreNo %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="門市名稱" runat="server" Caption="<%$ Resources:WebResources, StoreName %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="比率" runat="server" Caption="<%$ Resources:WebResources, Ratio %>"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="異常原因" runat="server" Caption="<%$ Resources:WebResources, ErrorDescription %>"></dx:GridViewDataTextColumn>
            </Columns>   
            <Templates>
                 <FooterCell>
                     <dx:ASPxLabel ID="lblFooterTotal" runat="server"></dx:ASPxLabel>
                 </FooterCell>
            </Templates>  
            <SettingsPager PageSize="10"></SettingsPager>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>"></SettingsText>
            <Settings ShowFooter="true" />
        </cc:ASPxGridView>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td><dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>" OnClick="btnCommit_Click" 
                           /></td>
                    <td><dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" >
                        <ClientSideEvents Click="function(s, e) { hidePopupWindow(); }" /> 
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
