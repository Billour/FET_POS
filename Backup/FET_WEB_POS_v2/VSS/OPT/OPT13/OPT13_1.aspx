<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT13_1.aspx.cs" Inherits="VSS_OPT_OPT13_OPT13_1" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>   

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
   <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

 <script type= "text/javascript">
     _gvEventArgs = null;
     _gvSender = null;
     
     function getStoreInfo(s, e) {
            _gvEventArgs = e;
            _gvSender = s;
         if (s.GetText() != '')
             PageMethods.getStoreInfo(s.GetText(), onSuccess);
     }

     function getPromoteInfo(s, e) {
         if (s.GetText() != '')
             PageMethods.getPromoteInfo(s.GetText(), onSuccess);
     }

     function onSuccess(returnData, userContext, methodName) {

         if (methodName == "getStoreInfo") {
             if (returnData != '') {
                 var values = returnData.split(';');
                 STORENAME.SetValue(values[0]);
                 ZONE.SetValue(values[1]);
             }
             else {
                 alert("�����s�����s�b!");
                 STORENAME.SetValue(null);
                 ZONE.SetValue(null);
                _gvEventArgs.processOnServer=false;
                _gvSender.Focus();
             }
         }
         else if (methodName == "getPromoteInfo") {
             if (returnData != '') {
                 PROMOTENAME.SetValue(returnData);
             }
             else {
                 PROMOTENAME.SetValue(null);
                 alert("�i" + _gvSender.GetText() + "�j���s�b�A�Э��s��J!");
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
                 alert("�}�l��W�����\�p��}�l��_�A�Э��s��J!");
                 s.SetValue(null);
             }
         }
    }
        
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

    <input type="hidden" id="hdUploadBatchNo" runat="server" />
    <div>
        <div class="titlef">
            <!--HG���ʧI�I����ЫP�P����-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoRedeemPointsForEvent %>"></asp:Literal>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--�馩�Ƹ�-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>"></asp:Literal>�G
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtActivityId" runat="server" AutoPostBack="true" Visible="false">
                        </dx:ASPxTextBox>
                        <uc1:PopupControl ID="PopupControl1" runat="server" PopupControlName="DiscountPopup" 
                                     AutoPostBack="false" SetClientValidationEvent="getPRODINFO1(s,e);" />
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--�馩�W��-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, DiscountName %>"></asp:Literal>�G
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
                        <!--�}�l���-->
                        <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>�G
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
                        <!--�P�P�N��-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>�G
                    </td>
                    <td colspan="3">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                                </td>
                                <td>                                    
                                    <uc1:PopupControl ID="popupSPRODNO" runat="server" PopupControlName="PromotionsPopupOnly" 
                                     AutoPostBack="false" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                                </td>
                                <td>
                                    <uc1:PopupControl ID="popupEPRODNO" runat="server" PopupControlName="PromotionsPopupOnly" 
                                     AutoPostBack="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--�P�P�W��-->
                        <asp:Literal ID="litProdName" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>�G
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="txtProdName" runat="server">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate">
        <script type="text/javascript">
              function getPRODINFO(s, e) {
                  this.s = s;
                  this.EventArgs = e;
                  this.Sender = s;
                  if (s.GetText() != '')
                      PageMethods.getPRODINFO(Sender.GetText(), getPRODINFO_OnOK);
              }

              function getPRODINFO_OnOK(returnData) {
                  var fName = "editnew_3_ACTIVITY_NO_txtControl";
                  txtPRODNAME = getClientInstance('TxtBox', s.name.replace(fName, "DXEditor4"));
                  if (returnData == '') {
                      EventArgs.processOnServer = false;
                      alert("�馩�Ƹ����s�b!");
                      Sender.Focus();
                  }
                  else {
                      //0 �~�W 1 I MEM_FLAGE
                      txtPRODNAME.SetText(returnData);
                  }
              }
              function getPRODINFO1(s, e) {
                  this.EventArgs = e;
                  this.Sender = s;
                  if (s.GetText() != '')
                      PageMethods.getPRODINFO(Sender.GetText(), getPRODINFO_OnOK1);
              }

              function getPRODINFO_OnOK1(returnData) {
                  var fName = "PopupControl1";
                  if (returnData == '') {
                      EventArgs.processOnServer = false;
                      alert("�馩�Ƹ����s�b!");
                      Sender.Focus();
                  }
                  else {
                      //0 �~�W 1 I MEM_FLAGE
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

              function getPRODINFO_OnOK2(returnData) {
                  var fName = "popupPRODNO";
                  
                  if (returnData == '') {
                      EventArgs.processOnServer = false;
                      alert("�ӫ~�Ƹ����s�b!");
                      Sender.Focus();
                      txtPRODNAME.SetText(''); 
                  }
                  else {
                      if (returnData == "fail")
                      {
                          EventArgs.processOnServer = false;
                          alert("�ӫ~�Ƹ������\�]�w!");
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
         </script>
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearchActivity" runat="server" Text="<%$ Resources:WebResources, Search %>"
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
        
        <div class="seperate"></div>
              
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
        
            <div id="Div1" class="SubEditBlock">
                <cc:ASPxGridView ID="gvActivity" runat="server" 
                    ClientInstanceName="gvMaster"
                    KeyFieldName="ACTIVITY_ID" 
                    EnableCallBacks="False"
                    Width="100%" Settings-ShowTitlePanel="true"
                    AutoGenerateColumns="False" 
                    oninitnewrow="gvActivity_InitNewRow"
                    OnRowInserting="gvActivity_RowInserting" 
                    OnRowUpdating="gvAcitvity_RowUpdating" 
                    OnHtmlRowPrepared="gvActivity_HtmlRowPrepared"
                    OnHtmlRowCreated="gvActivity_HtmlRowCreated" 
                    OnPageIndexChanged="gvActivity_PageIndexChanged"       
                    OnCellEditorInitialize="gvActivity_CellEditorInitialize"                                  
                    onfocusedrowchanged="gvActivity_FocusedRowChanged"
                    oncommandbuttoninitialize="gvActivity_CommandButtonInitialize"
                    onrowvalidating="gvActivity_RowValidating"
                    OnStartRowEditing="gvActivity_StartRowEditing" 
                    oncancelrowediting="gvActivity_CancelRowEditing">                    
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <HeaderTemplate>
                                <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
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
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ACTIVITY_NO" HeaderStyle-HorizontalAlign="Center" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>"> 
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
                        <dx:GridViewDataTextColumn FieldName="ACTIVITY_NAME" HeaderStyle-HorizontalAlign="Center" Caption="<%$ Resources:WebResources, DiscountName %>"  
                            PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ValidationSettings-ErrorText="�������">
                          <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, DiscountName %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="S_DATE" HeaderStyle-HorizontalAlign="Center" Caption="<%$ Resources:WebResources, StartDate %>"  
                            PropertiesDateEdit-ValidationSettings-RequiredField-IsRequired="true"
                            PropertiesDateEdit-ValidationSettings-ErrorText="�������" >                        
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>">
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="MEMBER_CHECK_FLAG" Caption="<%$ Resources:WebResources, NameListVerification %>" >
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="PAY_OFF_TYPE_NAME" runat="server" Caption="<%$ Resources:WebResources, DiscountMethod %>">
                            <PropertiesComboBox ValueType="System.String">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="�������" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                      <dx:GridViewDataTextColumn FieldName="U_BOUND" Caption="<%$ Resources:WebResources, RedemptionLimit %>">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionLimit %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <PropertiesTextEdit Style-HorizontalAlign="Left" MaxLength="9">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="�������" />
                                    <RegularExpression ValidationExpression="\d*" ErrorText="��J�r��D�Ʀr�榡�B�����\�p��0�A�Э��s��J�C"/>
                                </ValidationSettings>
                                <Style HorizontalAlign="Left">
                                </Style>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER" Caption="<%$ Resources:WebResources, ModifiedBy %>" EditFormSettings-Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>" EditFormSettings-Visible="False">
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
                                        <dx:ASPxButton ID="btnDelActivity" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                            OnClick="btnDelActivities_Click">
                                            <ClientSideEvents Click = "function(s,e){if (!confirm('�t�αN�R���Ŀ蠟��ơA�T�{�R���H')){e.processOnServer=false;}}"/>
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
            
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%"
                Visible="false" onprerender="ASPxPageControl1_PreRender" AutoPostBack="true" 
                   onactivetabchanged="ASPxPageControl1_ActiveTabChanged">
                <TabPages>
                    <%--�P�P�]�w--%>
                    <dx:TabPage Text="�P�P�]�w">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl4" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="gvProduct" runat="server" ClientInstanceName="gvProduct" EnableCallBacks="true"
                                        Width="100%"  KeyFieldName="SID"  AutoGenerateColumns="False"  
                                        OnRowInserting="gvProduct_RowInserting"
                                        OnRowUpdating="gvProduct_RowUpdating"
                                        OnPageIndexChanged="gvProduct_PageIndexChanged"
                                        OnRowValidating="gvProduct_RowValidating" 
                                        OnStartRowEditing="gvProduct_StartRowEditing">
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvProduct.SelectAllRowsOnPage(this.checked);"
                                                        title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center"
                                                ReadOnly="true">
                                                <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None">
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None"></Border>
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <DataItemTemplate>
                                                    <%#Container.ItemIndex + 1%>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" 
                                                        Text="<%$ Resources:WebResources, Items %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PROMOTE_CODE" HeaderStyle-HorizontalAlign="Center">
                                                <PropertiesTextEdit>
                                                    <ReadOnlyStyle>
                                                        <Border BorderStyle="None" />
                                                    </ReadOnlyStyle>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PROMOTE_NAME" Caption="<%$ Resources:WebResources, PromotionName %>">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <TitlePanel>
                                                <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnAddDetail_3" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                                                AutoPostBack="False" ClientSideEvents-Click="function(s, e) {gvProduct.SelectAllRowsOnPage(false); gvProduct.AddNewRow();}" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDelDetail_3"  SkinID="DeleteBtn" OnClick="btnDelDisActProd_click" runat="server" Text="<%$ Resources:WebResources, Delete %>">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                            <EditForm>
						                        <table width="80%" align="center">
                                                    <tr>
                                                        <%--�P�P�Ƹ�--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, PromotionCode %>"></asp:Literal>�G
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
                                                            <uc1:PopupControl ID="PopupPROMOTE" runat="server" PopupControlName="PromotionsPopupOnly" SetClientValidationEvent="getPromoteInfo" 
								                            Text='<%# Bind("PROMOTE_CODE") %>' />
                                                        </td>
                                                        <%--�P�P�W��--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, PromotionName %>"></asp:Literal>�G
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
							                                <dx:ASPxLabel ID="PROMOTENAME" runat="server" Text='<%#Bind("PROMOTE_NAME") %>' ClientInstanceName="PROMOTENAME" >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                        </td>
                                                        <td class="tdval" nowrap="nowrap">
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
                                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />         
                                        <Settings ShowTitlePanel="True"></Settings>
                                        <SettingsEditing />
                                        <SettingsPager PageSize="5" />                    
                                    </cc:ASPxGridView>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <%--�I�I�]�w--%>
                    <dx:TabPage Text="�I�I�]�w">
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
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvHCR_Exchange.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>"
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
                                                        <RequiredField IsRequired="True" ErrorText="�������" />
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
                                                        <RequiredField IsRequired="True" ErrorText="�������" />
                                                        <RegularExpression ValidationExpression="\d*" ErrorText="��J�r��D�Ʀr�榡�B�����\�p��0�A�Э��s��J�C"/>
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
                                                        <RequiredField IsRequired="True" ErrorText="�������" />
                                                        <RegularExpression ValidationExpression="\d*" ErrorText="��J�r��D�Ʀr�榡�B�����\�p��0�A�Э��s��J�C"/>
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
                    <%--���w����--%>
                    <dx:TabPage Text="���w����">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">
                                <div class="SubEditBlock">
                                    <cc:ASPxGridView ID="gvHCR_Store" runat="server" ClientInstanceName="gvHCR_Store" EnableCallBacks="false"
                                        Settings-ShowTitlePanel="true" Width="100%"                                        
                                        KeyFieldName="SID" AutoGenerateColumns="False"
                                        OnRowInserting="gvHCR_Store_RowInserting"                                         
                                        OnRowUpdating="gvHCR_Store_RowUpdating" 
                                        OnPageIndexChanged="gvHCR_Store_PageIndexChanged" 
                                        OnRowValidating="gvHCR_Store_RowValidating" 
                                        OnStartRowEditing="gvHCR_Store_StartRowEditing">                                        
                                        <Columns>
                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderTemplate>
                                                    <input type="checkbox" onclick="gvHCR_Store.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                                </HeaderTemplate>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Button">
                                                <EditButton Visible="true">
                                                </EditButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>"
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
                                                        <%--�����s��--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="lblStoreNo" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>�G
                                                        </td>                                                      
                                                        <td class="tdval" nowrap="nowrap">
                                                           <uc1:PopupControl ID="popupSTORE" runat="server" PopupControlName="StoresPopup" Text='<%# Bind("STORE_NO") %>'
                                                            SetClientValidationEvent="getStoreInfo" />
                                                        </td>
                                                        <%--�����W��--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>�G
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="STORENAME" runat="server" Text='<%#Bind("STORENAME") %>' ClientInstanceName="STORENAME" >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                         <%--�ϰ�O--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>�G
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
                    <%--�[�ʻ�--%>
                    <dx:TabPage Text="�[�ʻ�">
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
                                                        <RequiredField IsRequired="True" ErrorText="�������" />
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
                                                        <RequiredField IsRequired="True" ErrorText="�������"/>
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <HeaderCaptionTemplate>
                                                    <span style="color: Red">*</span><dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, RedemptionPoints %>">
                                                    </dx:ASPxLabel>
                                                </HeaderCaptionTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="EXTRA_SALE_PRICE" HeaderStyle-HorizontalAlign="Center" >
                                                <PropertiesTextEdit>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="True" ErrorText="�������" />
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
                                                            <dx:ASPxButton ID="btnDelHGCES" runat="server" Text="<%$ Resources:WebResources, Delete %>" 
                                                             OnClick="btnDelHGCES_click">
                                                             <ClientSideEvents Click = "function(s,e){if (!confirm('�t�αN�R���Ŀ蠟��ơA�T�{�R���H')){e.processOnServer=false;}}"/>
                                                             </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel> 
                                            <EditForm>
                                                <table width="80%" align="center">
                                                    <tr>
                                                        <%--�ӫ~�Ƹ�--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="lblStoreNo" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>�G
                                                        </td>                                                      
                                                        <td class="tdval" nowrap="nowrap">
                                                           <uc1:PopupControl ID="popupPRODNO" runat="server" PopupControlName="ProductsPopup" Text='<%# Bind("PRODNO") %>'
                                                            AutoPostBack="false" KeyFieldValue1="extrasale"
                                                              IsValidation="true" ValidationGroup='<%# Container.ValidationGroup %>'
                                                             OnClientTextChanged="function(s,e){ getPRODINFO2(s,e);}" />                                                                                                                     
                                                        </td>
                                                        <%--�ӫ~�W��--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>�G
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtPRODNAME" ClientInstanceName="txtPRODNAME" runat="server" Text='<%# Bind("PRODNAME") %>' 
                                                            Enabled="false" Width="170px">
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>                                                                    
                                                        <%--�I���I��--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, RedemptionPoints %>"></asp:Literal>�G
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtDIVIDABLE_POINT" runat="server" Text='<%# Bind("DIVIDABLE_POINT") %>' Width="170px"
                                                             ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="9">
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="�������" />
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="��J�r��D�Ʀr�榡�B�����\�p��0�A�Э��s��J�C"/>
                                                            </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <%--�[�ʻ�--%>
                                                        <td class="tdtxt" nowrap="nowrap">
                                                            <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, AdditionalCharges %>"></asp:Literal>�G
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtEXTRA_SALE_PRICE" runat="server" Text='<%# Bind("EXTRA_SALE_PRICE") %>' Width="170px"
                                                             ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="7">
                                                            <ValidationSettings>
                                                                <RequiredField IsRequired="true" ErrorText="�������" />
                                                                <RegularExpression ValidationExpression="\d*" ErrorText="��J�r��D�Ʀr�榡�B�����\�p��0�A�Э��s��J�C"/>
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
                            
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="titlef">
                                            <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>" AutoPostBack="false"/>
                                                
                                            <dx:ASPxTextBox ID="txtBatchNO" runat="server" Width="170px" ClientVisible="false">
                                            </dx:ASPxTextBox>
                                            <cc:ASPxPopupControl ID="ASPxPopupControl1" runat="server"
                                                AllowDragging="True" AllowResize="True" CloseAction="CloseButton"
                                                PopupHorizontalAlign="Center" PopupVerticalAlign="WindowCenter" ShowFooter="false" onOKScript="onOK"
                                                Width="600px" Height="550px" TargetElementID="txtBatchNO" Modal="true"
                                                HeaderText="<%$ Resources:WebResources, HappyGoPointListUpload %>" 
                                                ClientInstanceName="dataImportPopup" PopupElementID="btnImport" LoadingPanelID="lp">            
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
                                            <dx:ASPxLoadingPanel ID="lp" runat="server"></dx:ASPxLoadingPanel> 
                                        </td>
                                    </tr>
                                    <tr><td>&nbsp;</td></tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="lblImportStatus" runat="server"></dx:ASPxLabel>
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


</asp:Content>

