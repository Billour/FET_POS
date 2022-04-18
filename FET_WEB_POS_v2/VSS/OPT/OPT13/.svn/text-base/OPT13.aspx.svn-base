 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT13.aspx.cs" Inherits="VSS_OPT_OPT13" MasterPageFile="~/MasterPage.master" %>
    
<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

 <script type= "text/javascript">
    function CheckAll_clear() {
        for (var i = 0; i < gvActivity.pageRowCount; i++) {
            gvActivity.SelectRowOnPage(i + gvActivity.visibleStartIndex, false);
      }
    }
 
     function getStoreInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
         if (s.GetText() != '')
             PageMethods.getStoreInfo(s.GetText(), onSuccess);
     }

     function getProductInfo(s, e) {
         if (s.GetText() != '')
             PageMethods.getProductInfo(s.GetText(), onSuccess);
     }

     function onSuccess(returnData, userContext, methodName) {

         if (methodName == "getStoreInfo") {
             if (returnData != '') {
                 var values = returnData.split(';');
                 STORENAME.SetValue(values[0]);
                 ZONE.SetValue(values[1]);
             }
             else {
                 alert("門市編號不存在!");
                 STORENAME.SetValue(null);
                 ZONE.SetValue(null);
                _gvEventArgs.processOnServer=false;
                _gvSender.Focus();
             }
         }
         else if (methodName == "getProductInfo") {
             if (returnData != '') {
                 txtDiscountName.SetValue(returnData);
             }
             else {
                 txtDiscountName.SetValue(null);
             }
         }
     }


     function onOK() {
         __doPostBack('<%= txtBatchNO.UniqueID %>', 'AAA');
     }

     function checkDate(s, e) {
         var x = txtSDate.GetValue();
         var y = txtEDate.GetValue();

         if (x == null) { x = ""; }
         if (y == null) { y = ""; }

         if (x != "" && y != "") {

             e.isValid = (x <= y);
             if (!e.isValid) {
                 alert("開始日訖不允許小於開始日起，請重新輸入!");
                 s.SetValue(null);
             }
         }
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <input type="hidden" id="hdUploadBatchNo" runat="server" />
    <div>
        <div class="titlef">
            <!--HG活動兌點限制－商品料號-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoRedeemPointsForProduct %>"></asp:Literal>
        </div>
        
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--折扣料號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtActivityId" runat="server"  AutoPostBack="true" Visible="false">
                        </dx:ASPxTextBox>
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="DiscountPopup" 
                                     AutoPostBack="false" SetClientValidationEvent="getPRODINFO1(s,e);" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--折扣名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtActivityName" ClientInstanceName="txtActivityName" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開始日期-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dateEditSDate" runat="server" ClientInstanceName="txtSDate">
                                        <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dateEditEDate" runat="server" ClientInstanceName="txtEDate">
                                        <ClientSideEvents ValueChanged="function(s, e){ checkDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品料號-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                    </td>
                    <td colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>                                    
                                    <uc1:PopupControl ID="popupSPRODNO" runat="server" PopupControlName="ProductsPopup" 
                                     AutoPostBack="false" KeyFieldValue1="extrasale" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="popupEPRODNO" runat="server" PopupControlName="ProductsPopup" 
                                     AutoPostBack="false" KeyFieldValue1="extrasale" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品名稱-->
                        <asp:Literal ID="litProdName" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtProdName" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate"></div>
        
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearchActivity" runat="server" 
                            Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" CausesValidation="false">                                                 
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        
        <div class="seperate">
          <script type="text/javascript">
              function getPRODINFO(s, e) {
                  this.s = s;
                 // alert(s.GetText());
                  this.EventArgs = e;
                  this.Sender = s;
                  if (s.GetText() != '')
                      PageMethods.getPRODINFO(Sender.GetText(), getPRODINFO_OnOK);
              }

              function getPRODINFO_OnOK(returnData) {
                  //var fName = "3_ACTIVITY_NO_txtControl";
                  //txtPRODNAME = getClientInstance('TxtBox', s.name.replace(fName, "4_ACTIVITY_NAME"));
                  var fName = "editnew_3_ACTIVITY_NO_txtControl";
                  txtPRODNAME = getClientInstance('TxtBox', s.name.replace(fName, "DXEditor4"));
                  if (returnData == '') {
                      EventArgs.processOnServer = false;
                      alert("折扣料號不存在!");
                      Sender.Focus();
                  }
                  else {
                      //0 品名 1 I MEM_FLAGE
                      txtPRODNAME.SetText(returnData);
                   
                  }
              }
              function getPRODINFO1(s, e) {
                 // alert(s.GetText());
                  this.EventArgs = e;
                  this.Sender = s;
                  if (s.GetText() != '')
                      PageMethods.getPRODINFO(Sender.GetText(), getPRODINFO_OnOK1);
              }

              function getPRODINFO_OnOK1(returnData) {
                  var fName = "PopupControl1";
                  if (returnData == '') {
                      EventArgs.processOnServer = false;
                      alert("折扣料號不存在!");
                      Sender.Focus();
                  }
                  else {
                      //0 品名 1 I MEM_FLAGE
                      if (Sender.GetText() == '')
                        txtActivityName.SetText('');
                      else
                        txtActivityName.SetText(returnData);
                  
                  }
              }

              function getPRODINFO2(s, e) {
                  this.s = s;
                  this.EventArgs = e;
                  this.Sender = s;
                  if (s.GetText() != '')
                      PageMethods.getPRODINFOExtraSale(Sender.GetText(), getPRODINFO_OnOK2);
              }
              function getPRODINFO3(s, e) {
                  this.s = s;
                  this.EventArgs = e;
                  this.Sender = s;
                  if (s.GetText() != '')
                      PageMethods.getPRODINFOExtraSale(Sender.GetText(), getPRODINFO_OnOK3);
              }
              

              function getPRODINFO_OnOK2(returnData, userContext, methodName) {
                  var fName;
                    fName = "popupPRODNO";
                      if (returnData == '') {
                          EventArgs.processOnServer = false;
                          alert("商品料號不存在!");
                          Sender.Focus();
                          txtPRODNAME.SetText(''); 
                      }
                      else {
                          if (returnData == "fail")
                          {
                              EventArgs.processOnServer = false;
                              alert("商品料號不允許設定!");
                              Sender.Focus();  
                              txtPRODNAME.SetText('');                      
                          }
                          else
                          {
                              if (Sender.GetText() == '')
                                txtActivityName.SetText('');
                              else
                                txtPRODNAME.SetText(returnData);
                          }
                      }
              }
              function getPRODINFO_OnOK3(returnData, userContext, methodName) {
                  var fName;
                fName = "tpopupPRODNO";
                  if (returnData == '') {
                      EventArgs.processOnServer = false;
                      alert("商品料號不存在!");
                      Sender.Focus();
                      txttPRODNAME.SetText(''); 
                  }
                  else {
                      if (returnData == "fail")
                      {
                          EventArgs.processOnServer = false;
                          alert("商品料號不允許設定!");
                          Sender.Focus();  
                          txttPRODNAME.SetText('');                      
                      }
                      else
                      {
                          if (Sender.GetText() == '')
                            txtActivityName.SetText('');
                          else
                            txttPRODNAME.SetText(returnData);
                      }
                  }
              }
              
              function getClientInstance(type, name) {
                  var _CI = window[name];
                  if (_CI == null) {
                      switch (type) {
                          case 'TxtBox':
                              _CI = new ASPxClientTextBox(name);
                              break;
                          case 'Button':
                              _CI = new ASPxClientButton(name);
                              break;
                          case 'Label':
                              _CI = new ASPxClientLabel(name);
                              break;
                          case 'Image':
                              _CI = new ASPxClientImage(name);
                              break;
                          case 'Popup':
                              _CI = new ASPxClientPopupControl(name);
                              break;
                      }
                      window[name] = _CI;
                  }
                  return _CI;
              }
              
                //不選取DISENABLED的CHECKBOX
                function CheckAll_onclick() {
                    for (var i = 0; i < gvActivity.pageRowCount; i++) {
                        if (gvActivity.GetRow(i + gvActivity.visibleStartIndex) != null && gvActivity.GetRow(i + gvActivity.visibleStartIndex).attributes["canSelect"].value == "true") {
                            var chk = document.getElementById("checkbox1");
                            if (chk.checked) {
                                gvActivity.SelectRowOnPage(i + gvActivity.visibleStartIndex, true);
                            } else {
                               gvActivity.SelectRowOnPage(i + gvActivity.visibleStartIndex, false);
                            }
                        }
                    }
                }         
        </script>
        </div>
        
        <div>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
            <div id="Div1" class="SubEditBlock">
                <cc:ASPxGridView ID="gvActivity" runat="server" 
                    ClientInstanceName="gvActivity"
                    KeyFieldName="ACTIVITY_ID" 
                    EnableCallBacks="False"
                    Width="100%" Settings-ShowTitlePanel="true"
                    AutoGenerateColumns="False" 
                    Oninitnewrow="gvActivity_InitNewRow"
                    OnRowInserting="gvActivity_RowInserting" 
                    OnRowUpdating="gvAcitvity_RowUpdating" 
                    OnHtmlRowPrepared="gvActivity_HtmlRowPrepared"
                    OnHtmlRowCreated="gvActivity_HtmlRowCreated" 
                    OnPageIndexChanged="gvActivity_PageIndexChanged"                                         
                    Onfocusedrowchanged="gvActivity_FocusedRowChanged" 
                    Onrowvalidating="gvActivity_RowValidating"
                    OnCellEditorInitialize="gvActivity_CellEditorInitialize"
                    Oncommandbuttoninitialize="gvActivity_CommandButtonInitialize"                   
                    SettingsBehavior-AllowFocusedRow="true"
                     OnStartRowEditing="gvActivity_StartRowEditing">                    
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <HeaderTemplate>
                                <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="2">
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="ITEMNO" VisibleIndex="3" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                            ReadOnly="true" EditFormSettings-Visible="False">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None"></Border>
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ACTIVITY_NO" HeaderStyle-HorizontalAlign="Center" VisibleIndex="5" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" > 
                          <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                             <uc1:PopupControl ID="ACTIVITY_NO" runat="server" PopupControlName="DiscountPopup" 
                                AutoPostBack="false"
                                  IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                 OnClientTextChanged="function(s,e){ getPRODINFO(s,e);}" Text='<%# Bind("ACTIVITY_NO") %>' />
                            </EditItemTemplate>
           
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ACTIVITY_NAME" HeaderStyle-HorizontalAlign="Center" Caption="<%$ Resources:WebResources, DiscountName %>" VisibleIndex="6" 
                        PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-ErrorText="必填欄位">
                          <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, DiscountName %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="S_DATE" HeaderStyle-HorizontalAlign="Center" Caption="<%$ Resources:WebResources, StartDate %>" VisibleIndex="7" 
                        PropertiesDateEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesDateEdit-ValidationSettings-ErrorText="必填欄位">
                        <HeaderCaptionTemplate>
                          <span style="color: Red">*</span>
                          <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></dx:ASPxLabel>
                       </HeaderCaptionTemplate>      
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>" VisibleIndex="8" >
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="MEMBER_CHECK_FLAG" Caption="<%$ Resources:WebResources, NameListVerification %>" VisibleIndex="9" >
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="PAY_OFF_TYPE_NAME" runat="server" VisibleIndex="10" Caption="<%$ Resources:WebResources, DiscountMethod %>">
                   
                        <PropertiesComboBox ValueType="System.String">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                        <dx:GridViewDataTextColumn FieldName="U_BOUND" Caption="<%$ Resources:WebResources, RedemptionLimit %>" VisibleIndex="11" >
                        
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionLimit %>" VisibleIndex="12" >
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <PropertiesTextEdit Style-HorizontalAlign="Left" MaxLength="9">
                            <ValidationSettings>
                               <RegularExpression ValidationExpression="^\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                               <RequiredField IsRequired="True" ErrorText="必填欄位" />
                            </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="USE_COUNT" Caption="<%$ Resources:WebResources, RedeemingFrequency %>" VisibleIndex="13" >
                        <PropertiesTextEdit Style-HorizontalAlign="Left" MaxLength="3">
                           <ValidationSettings>
                               <RegularExpression ValidationExpression="^\d*"  ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                            </ValidationSettings>
                           </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>" EditFormSettings-Visible="False" VisibleIndex="14">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" EditFormSettings-Visible="False" VisibleIndex="15" >
                        </dx:GridViewDataTextColumn>                                              
                    </Columns>
                    <Templates>
                        <DetailRow>
                        </DetailRow>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAddNewActivity" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnAddNewActivity_Click" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelActivity" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                            OnClick="btnDelActivities_Click">
                                            </dx:ASPxButton>
                                    </td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>                     
                                </tr>
                            </table>
                        </TitlePanel>                        
                    </Templates>
                    <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="True" />
                    <SettingsPager PageSize="5" />                    
                    <SettingsEditing EditFormColumnCount="3" />
                    <Settings ShowTitlePanel="True" />
                    <SettingsEditing Mode="EditForm" />                    
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                    <Styles>
                        <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>
                    </Styles>
                </cc:ASPxGridView>
            </div>
            <div class="seperate"></div>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="2" 
                    onactivetabchanged="ASPxPageControl1_ActiveTabChanged"  AutoPostBack="true"
                    onprerender="ASPxPageControl1_PreRender" Visible="false" Width="100%">
                    <TabPages>
                        <%--商品設定--%>
                        <dx:TabPage Text="商品設定">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl4" runat="server">
                                    <div class="SubEditBlock">
                                        <cc:ASPxGridView ID="gvProduct" runat="server" AutoGenerateColumns="False" 
                                            ClientInstanceName="gvProduct" EnableCallBacks="False" KeyFieldName="SID" 
                                            OnPageIndexChanged="gvProduct_PageIndexChanged" 
                                            OnRowInserting="gvProduct_RowInserting" 
                                            OnRowUpdating="gvProduct_RowUpdating" 
                                            onrowvalidating="gvProduct_RowValidating" 
                                            Settings-ShowTitlePanel="true" Width="100%" 
                                            OnStartRowEditing="gvProduct_StartRowEditing">
                                            <Columns>
                                                <dx:GridViewCommandColumn HeaderStyle-HorizontalAlign="Center" 
                                                    ShowSelectCheckbox="True" VisibleIndex="1">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <HeaderTemplate>
                                                        <input onclick="gvProduct.SelectAllRowsOnPage(this.checked);" 
                                                            title="Select/Unselect all rows on the page" type="checkbox" />
                                                    </HeaderTemplate>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="2">
                                                    <EditButton Visible="true">
                                                    </EditButton>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" 
                                                    EditFormSettings-Visible="False" FieldName="ITEMNO" 
                                                    HeaderStyle-HorizontalAlign="Center" VisibleIndex="3">
                                                    <EditFormSettings Visible="False" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="PRODNO" 
                                                    HeaderStyle-HorizontalAlign="Center" VisibleIndex="4">
                                                    <HeaderCaptionTemplate>
                                                        <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" 
                                                            Text="<%$ Resources:WebResources, ProductCode %>">
                                                        </dx:ASPxLabel>
                                                    </HeaderCaptionTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, ProductName %>" 
                                                    FieldName="PRODNAME" VisibleIndex="5">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Templates>
                                                <TitlePanel>
                                                    <table align="left" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="btnAddDisActProd" runat="server" 
                                                                    OnClick="btnAddDisActProd_click" Text="<%$ Resources:WebResources, Add %>" />
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <dx:ASPxButton ID="btnDelDisActProd" SkinID="DeleteBtn" runat="server" 
                                                                    OnClick="btnDelDisActProd_click" Text="<%$ Resources:WebResources, Delete %>">
                                                                    </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </TitlePanel>
                                                <EditForm>
                                                    <div class="criteria">
                                                        <table align="center" width="80%">
                                                            <tr>
                                                                <%--商品料號--%>
                                                                <td class="tdtxt" nowrap="nowrap">
                                                                    <asp:Literal ID="lblStoreNo" runat="server" 
                                                                        Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>
                                                                    ： 
                                                                </td>
                                                                <td class="tdval" nowrap="nowrap">
                                                                    <uc1:PopupControl ID="tpopupPRODNO" runat="server" AutoPostBack="false" 
                                                                        PopupControlName="ProductsPopup" SetClientValidationEvent="getPRODINFO3(s,e);" 
                                                                        Text='<%# Bind("PRODNO") %>' KeyFieldValue1="extrasale" />
                                                                </td>
                                                                <%--商品名稱--%>
                                                                <td class="tdtxt" nowrap="nowrap">
                                                                    <asp:Literal ID="Literal6" runat="server" 
                                                                        Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>
                                                                    ： 
                                                                </td>
                                                                <td>
                                                                    <dx:ASPxTextBox ID="txttPRODNAME" ClientInstanceName="txttPRODNAME" runat="server" Text='<%# Bind("PRODNAME") %>' 
                                                            Enabled="false" Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" 
                                                            ReplacementType="EditFormUpdateButton">
                                                        </dx:ASPxGridViewTemplateReplacement>
                                                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" 
                                                            ReplacementType="EditFormCancelButton">
                                                        </dx:ASPxGridViewTemplateReplacement>
                                                    </div>
                                                </EditForm>
                                                <EmptyDataRow>
                                                    <asp:Label ID="emptyDataLabel" runat="server" 
                                                        Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                                </EmptyDataRow>
                                            </Templates>
                                            <SettingsPager PageSize="5" />
                                            <SettingsEditing EditFormColumnCount="3" />
                                            <SettingsEditing Mode="EditForm" />
                                            <Settings ShowTitlePanel="True" />
                                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                            <Styles>
                                                <EditFormColumnCaption Wrap="False">
                                                </EditFormColumnCaption>
                                            </Styles>
                                        </cc:ASPxGridView>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <%--兌點設定--%>
                        <dx:TabPage Text="兌點設定">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="gvHCR_Exchange" runat="server" 
                                        KeyFieldName="SID" AutoGenerateColumns="False"
                                        ClientInstanceName="gvHCR_Exchange"
                                        EnableCallBacks="true"
                                        Settings-ShowTitlePanel="true" Width="100%"                                    
                                        OnRowInserting="gvHCR_Exchange_RowInserting" 
                                        OnRowUpdating="gvHCR_Exchange_RowUpdating" 
                                        OnPageIndexChanged="gvHCR_Exchange_PageIndexChanged" 
                                        OnRowValidating="gvHCR_Exchange_RowValidating" 
                                        OnStartRowEditing="gvHCR_Exchange_StartRowEditing">                                        
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvHCR_Exchange.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                                                ReadOnly="true" EditFormSettings-Visible="False">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <EditFormSettings Visible="False" />
                                                <DataItemTemplate>
                                                    <%#Container.ItemIndex + 1%>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="EXCHANGE_NAME" Caption="<%$ Resources:WebResources, RedemptionName %>">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionName %>" >
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <PropertiesTextEdit Style-HorizontalAlign="Left">
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                    </ValidationSettings>
                                                    <Style HorizontalAlign="Left">
                                                    </Style>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="DIVIDABLE_POINT" Caption="<%$ Resources:WebResources, Points %>">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Points %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <PropertiesTextEdit Style-HorizontalAlign="Left" MaxLength="9">
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                                                    </ValidationSettings>
                                                    <Style HorizontalAlign="Left">
                                                    </Style>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="CONVERT_CURRENCY" Caption="<%$ Resources:WebResources, RedemptionAmount %>">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionAmount %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <PropertiesTextEdit Style-HorizontalAlign="Left" MaxLength="9">
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                        <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                                                    </ValidationSettings>
                                                    <Style HorizontalAlign="Left">
                                                    </Style>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddHGCRE" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                             OnClick="btnAddHGCRE_click" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelHGCRE" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                                             OnClick="btnDelHGCRE_click">
                                                             </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>                                  
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                        </Templates>
                                        <SettingsBehavior AllowFocusedRow="false"  ProcessFocusedRowChangedOnServer="false" />                                        
                                        <SettingsPager PageSize="5" />                    
                                        <SettingsEditing EditFormColumnCount="3" />
                                        <SettingsEditing Mode="EditForm" />                    
                                        <Settings ShowTitlePanel="True"></Settings>
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <Styles>
                                            <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>
                                        </Styles>
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                        <%--指定門市--%>
                        <dx:TabPage Text="指定門市">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="gvHCR_Store" runat="server" 
                                        ClientInstanceName="gvHCR_Store" 
                                        EnableCallBacks="false"
                                        Settings-ShowTitlePanel="true" Width="100%"                                        
                                        KeyFieldName="SID" AutoGenerateColumns="False"
                                        OnRowInserting="gvHCR_Store_RowInserting"                                         
                                        OnRowUpdating="gvHCR_Store_RowUpdating" 
                                        OnPageIndexChanged="gvHCR_Store_PageIndexChanged" 
                                        OnRowValidating="gvHCR_Store_RowValidating" 
                                        OnStartRowEditing="gvHCR_Store_StartRowEditing"  
                                        OnPreRender="gvHCR_Store_PreRender">                                        
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvHCR_Store.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="STORE_NO" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, StoreNo %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn FieldName="ZONE_NAME" Caption="<%$ Resources:WebResources, ByDistrict %>">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                        </Columns>
                                        <SettingsEditing />
                                        <Settings ShowTitlePanel="True"></Settings>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddHGCRS" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                             OnClick="btnAddHGCRS_click" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelHGCRS" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                                             OnClick="btnDelHGCRS_click">
                                                             </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxComboBox ID="ddlDistrict" runat="server" Enabled="True">
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDistrictSubmit" runat="server" Text="<%$ Resources:WebResources, SubmitDistrict %>"
                                                             OnClick="btnDistrictSubmit_click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            <EditForm>
                                                <table width="80%" align="center">
                                                    <tr>
                                                        <%--門市編號--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="lblStoreNo" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                                                        </td>                                                      
                                                        <td class="tdval" nowrap="nowrap">
                                                           <uc1:PopupControl ID="popupSTORE" runat="server" PopupControlName="StoresPopup" Text='<%# Bind("STORE_NO") %>'
                                                            SetClientValidationEvent="getStoreInfo" />
                                                        </td>
                                                        <%--門市名稱--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="STORENAME" runat="server" Text='<%#Bind("STORENAME") %>' ClientInstanceName="STORENAME" >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                         <%--區域別--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="lbZONE_NAME" runat="server" Text='<%#Bind("ZONE_NAME") %>' ClientInstanceName="ZONE" >
                                                            </dx:ASPxLabel>
                                                        </td>                                                                    
                                                    </tr>            
                                                </table>
                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                                    runat="server">
                                                                </dx:ASPxGridViewTemplateReplacement>
                                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                                    runat="server">
                                                                </dx:ASPxGridViewTemplateReplacement>
                                                            </div>
                                            </EditForm>                                           
                                        </Templates>                                        
                                        <SettingsBehavior AllowFocusedRow="false" ProcessFocusedRowChangedOnServer="false" />
                                        <SettingsPager PageSize="5" />                    
                                        <SettingsEditing EditFormColumnCount="3" />                                        
                                        <SettingsEditing Mode="EditForm" />                    
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <Styles>
                                            <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>
                                        </Styles>
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                        <%--加價購--%>
                        <dx:TabPage Text="加購價">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="gvHC_ExtraSale" runat="server" 
                                        ClientInstanceName="gvHC_ExtraSale" 
                                        EnableCallBacks="true"                                        
                                        KeyFieldName="SID" AutoGenerateColumns="False"                                        
                                        Settings-ShowTitlePanel="true" Width="100%" 
                                        OnRowInserting="gvHC_ExtraSale_RowInserting" 
                                        OnRowUpdating="gvHC_ExtraSale_RowUpdating" 
                                        OnPageIndexChanged="gvHC_ExtraSale_PageIndexChanged" 
                                        OnRowValidating="gvHC_ExtraSale_RowValidating" 
                                        OnStartRowEditing="gvHC_ExtraSale_StartRowEditing">                                        
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvHC_ExtraSale.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRODNO">
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ProductCode %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                                <PropertiesTextEdit Style-HorizontalAlign="Left">
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                    </ValidationSettings>
                                                    <Style HorizontalAlign="Left">
                                                    </Style>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                                HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="DIVIDABLE_POINT" HeaderStyle-HorizontalAlign="Center">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionPoints %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="EXTRA_SALE_PRICE" HeaderStyle-HorizontalAlign="Center">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, AdditionalCharges %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddHGCES" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                 AutoPostBack="true" OnClick="btnAddHGCES_click" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelHGCES" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                                             OnClick="btnDelHGCES_click">
                                                             </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel> 
                                            <EditForm>
                                                <table width="80%" align="center">
                                                    <tr>
                                                        <%--商品料號--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="lblStoreNo" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                                                        </td>                                                      
                                                        <td class="tdval" nowrap="nowrap">
                                                           <uc1:PopupControl ID="popupPRODNO" runat="server" PopupControlName="ProductsPopup" Text='<%# Bind("PRODNO") %>'
                                                            AutoPostBack="false" KeyFieldValue1="extrasale"
                                                              IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                                             OnClientTextChanged="function(s,e){ getPRODINFO2(s,e);}" />                                                                                                                     
                                                        </td>
                                                        <%--商品名稱--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtPRODNAME" ClientInstanceName="txtPRODNAME" runat="server" Text='<%# Bind("PRODNAME") %>' 
                                                            Enabled="false" Width="170px">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                                    
                                                        <%--兌換點數--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, RedemptionPoints %>"></asp:Literal>：
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtDIVIDABLE_POINT" runat="server" Text='<%# Bind("DIVIDABLE_POINT") %>' Width="170px"
                                                             ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="9">
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                                                            </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <%--加價購--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, AdditionalCharges %>"></asp:Literal>：
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtEXTRA_SALE_PRICE" runat="server" Text='<%# Bind("EXTRA_SALE_PRICE") %>' Width="170px"
                                                             ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="7">
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="輸入字串非數字格式且不允許小於0，請重新輸入。"/>
                                                            </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server">
                                                </dx:ASPxGridViewTemplateReplacement>
                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server">
                                                </dx:ASPxGridViewTemplateReplacement>
                                            </div>
                                            </EditForm>                                           
                                            <EmptyDataRow>
                                                <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                            </EmptyDataRow>
                                        </Templates>
                                        <SettingsBehavior AllowFocusedRow="false" ProcessFocusedRowChangedOnServer="false" />
                                        <SettingsPager PageSize="5" />                    
                                        <SettingsEditing EditFormColumnCount="2" />                                        
                                        <SettingsEditing Mode="EditForm" />   
                                        <Settings ShowTitlePanel="True"></Settings>
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                                        <Styles>
                                            <EditFormColumnCaption Wrap="False"></EditFormColumnCaption>
                                        </Styles>
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                        <dx:TabPage Text="<%$ Resources:WebResources, List %>">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td class="titlef">
                                                <dx:ASPxButton ID="btnImport" runat="server"  AutoPostBack="false"
                                                    Text="<%$ Resources:WebResources, Import %>" />
                                                <dx:ASPxTextBox ID="txtBatchNO" runat="server" ClientVisible="false" 
                                                    Width="170px">
                                                </dx:ASPxTextBox>                                       
                                                <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" 
                                                    AllowResize="True" ClientInstanceName="dataImportPopup" 
                                                    CloseAction="CloseButton" 
                                                    HeaderText="<%$ Resources:WebResources, HappyGoPointListUpload %>" 
                                                    Width="600px" Height="550px" LoadingPanelID="lp" Modal="true" onOKScript="onOK" 
                                                    PopupElementID="btnImport" PopupHorizontalAlign="Center" 
                                                    PopupVerticalAlign="WindowCenter" ShowFooter="false" 
                                                    TargetElementID="txtBatchNO">
                                                    <ClientSideEvents Init="function(s, e) {
                                                                    var iframe = s.GetContentIFrame();                   
                                                                    iframe.popupArguments = {};
                                                                    iframe.contentLoaded = false;
                                                                    var controlCollection = ASPxClientControl.GetControlCollection();                
                                                                    iframe.popupArguments.popupContainer = controlCollection.Get('ASPxPageControl1_ASPxPopupControl1');
                                                                    ASPxClientUtils.AttachEventToElement(iframe, 'load', function(e) 
                                                                    {
                                                                        if (!controlCollection.Get('ASPxPageControl1_ASPxPopupControl1').GetClientVisible()) 
                                                                            return; 
                                                                        controlCollection.Get('ASPxPageControl1_lp').Hide(); 
                                                                        iframe.contentLoaded = true;
                                                                    });
                                                                                                                                   
                                                                    var targetElementId = 'ASPxPageControl1_txtBatchNO';                                                                                        
                                                                    iframe.popupArguments.controlToAssign = controlCollection.Get(targetElementId) 
                                                                        || document.getElementById(targetElementId);
                                                                                                                                       
                                                                    var onOKScript = onOK;                                                                                        
                                                                    iframe.popupArguments.okscript = onOKScript;
                                                                    }" Shown="function(s, e) {  
                                                                    if (!s.GetContentIFrame().contentLoaded)   
                                                                    ASPxClientControl.GetControlCollection().Get('ASPxPageControl1_lp')
                                                                        .ShowInElement(s.GetContentIFrame());}" />
                                                    <ContentCollection>
                                                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                                        </dx:PopupControlContentControl>
                                                    </ContentCollection>
                                                </cc:ASPxPopupControl>
                                                <dx:ASPxLoadingPanel ID="lp" runat="server">
                                                </dx:ASPxLoadingPanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lblImportStatus" runat="server">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </ContentTemplate>
         </asp:UpdatePanel>
       </div>
    </div>
 </asp:Content>
