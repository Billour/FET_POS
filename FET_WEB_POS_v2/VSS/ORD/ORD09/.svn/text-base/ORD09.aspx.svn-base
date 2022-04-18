<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ORD09.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="VSS_ORD_ORD09_ORD09" %>

<%@ Register Src="../../../Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
     

        function DisabledAndRunButton(s, e) {
            var file = document.getElementById("ctl00_MainContentPlaceHolder_FileUpload1").value
                      
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file))) {
                alert("匯入的檔案不正確,須為Excel檔!!");

                var t = document.getElementById('"ctl00_MainContentPlaceHolder_FileUpload1');
                e.processOnServer = false;
            }    
            else {
                if (s.GetEnabled()) {
                    s.SendPostBack('Click');
                    s.SetEnabled(false);
                }
            }
        }

        _s = null;
        function changeStore(s, e) {
         
            _s = s;
            
            if (s.GetText() != '') {
                PageMethods.getStoreInfo(_s.GetText(), ReturnOK);
            }
            

        }

        function ReturnOK(returnValue) {
            if (returnValue != '') {
                var fName = '0_PopupControl1_txtControl';

                var txtStoreName = getClientInstance('TxtBox', _s.name.replace(fName, "1_txtStoreName"));
                txtStoreName.SetText(returnValue);
            }

        }

        _ss = null;
        function changePROD(s, e) {
           
            _ss = s;

            if (s.GetText() != '') {
                PageMethods.getPRODINFO(_ss.GetText(), ReturnOKPROD);
               
            }


        }

        function ReturnOKPROD(returnValue) {
            if (returnValue != '') {
                var fName = '2_PopupControl1_txtControl';

                var txtStoreName = getClientInstance('TxtBox', _ss.name.replace(fName, "3_txtProdName"));
                txtStoreName.SetText(returnValue);
            }

        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
<input type="hidden" id="hdUploadBatchNo" runat="server" />
<asp:HiddenField ID="Temp_Check" runat="server" />
<asp:HiddenField ID="Tcheckagain" runat="server" />
<asp:HiddenField ID="hidSubmit" runat="server" Value="" />
    <div>
        <div class="titlef">
            <!--DropShipment主配上傳-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, DropShipmentPrimaryUpload %>"></asp:Literal></div>
         
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdval">
                        <!--匯入檔案名稱-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, FilePath %>"></asp:Literal>：
                    </td>
                    <td colspan="5">
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="60%" />
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" 
                            OnClick="btnImport_Click">
                         <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReset" runat="server" Text="<%$ Resources:WebResources, Reset %>"
                         AutoPostBack="false" UseSubmitBehavior="false" CausesValidation="false">
                            <ClientSideEvents Click="function(s, e){ resetForm(aspnetForm); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <dx:ASPxGridView ID="gvMaster" KeyFieldName="STORE_NO" ClientInstanceName="gvMaster"
            runat="server" Width="100%" EnableCallBacks="false"
            OnHtmlDataCellPrepared="gvMasterDV_HtmlDataCellPrepared" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared">
            <Columns>
                <dx:GridViewDataColumn FieldName="STORE" Caption="<%$ Resources:WebResources, StoreNo %>">
                    <DataItemTemplate>
                       <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="StoresPopup" Text='<%# Bind("STORE_NO") %>' OnClientTextChanged="function(s,e) { changeStore(s, e); }" />
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="STORE_NAME" Caption="<%$ Resources:WebResources, StoreName %>">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="txtStoreName" runat="server"  ReadOnly="true" Text='<%# Bind("STORE_NAME") %>' Border-BorderStyle="None"></dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>">
                    <DataItemTemplate>
                      <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="ProductsPopup" Text='<%# Bind("PRODNO") %>' OnClientTextChanged="function(s,e) { changePROD(s, e); }" />
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>">
                 <DataItemTemplate>
                        <dx:ASPxTextBox ID="txtProdName" runat="server"  ReadOnly="true" Text='<%# Bind("PRODNAME") %>' Border-BorderStyle="None"></dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="DIS_QTY" Caption="<%$ Resources:WebResources, DistributionQuantity %>">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="ASPxTextBox1" Text='<%# Bind("DIS_QTY") %>' runat="server" Width="80px" >
                          <ValidationSettings>
                               <RegularExpression ValidationExpression="^\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                               <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn FieldName="Result" Caption="<%$ Resources:WebResources, ErrorDescription %>">
                  <DataItemTemplate>
                        <dx:ASPxTextBox ID="txtResult" Text='<%# Bind("Result") %>' runat="server" Width="80px"  Border-BorderStyle="None"
                                                        ReadOnly="true">                       
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
            </Columns>
            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
        </dx:ASPxGridView>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table align="center"">
                <tr>
                    <td>
                    <!--上傳確認-->
                        <dx:ASPxButton ID="btnCommitUpload" runat="server" 
                            Text="<%$ Resources:WebResources, CommitUpload %>" onclick="btnCommitUpload_Click" >
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" 
                            Text="<%$ Resources:WebResources, Cancel %>" onclick="btnCancel_Click">
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnSave1" ClientInstanceName ="btnSave1" ClientVisible ="false"  runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                            OnClick="btnSave1_Click">
                        </dx:ASPxButton>
                        <dx:ASPxTextBox ID="SaveItem" ClientInstanceName ="SaveItem" runat="server" ReadOnly="false" Text=''
                            Border-BorderStyle="None" ClientVisible="false">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        if (document.getElementById('ctl00_MainContentPlaceHolder_hidSubmit').value == "Y") {
            document.getElementById('ctl00_MainContentPlaceHolder_hidSubmit').value = "";
            if (confirm('是否確認送出?')) { SaveItem.SetText('Y'); btnSave1.SendPostBack('Click');}
        }
    </script>
</asp:Content>
