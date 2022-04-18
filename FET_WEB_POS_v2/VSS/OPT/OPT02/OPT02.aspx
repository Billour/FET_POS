<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT02.aspx.cs" Inherits="VSS_OPT_OPT02_OPT02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    
    
    <script type="text/javascript">
        function chkSDate(s, e) {
            e.isValid = true;
            var x = txtSDate_S.GetValue();
            var y = txtSDate_E.GetValue();

            var dvalue = s.GetValue();

            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[開始起日訖值]不允許小於[開始起日起值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {

                var Sx = txtEDate_S.GetValue();
                var Sy = txtEDate_E.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue > Sx) || (Sy != "" && dvalue > Sy)) {
                    e.isValid = false;
                    alert("[開始日期]不允許大於[結束日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }

        }

        function chkEDate(s, e) {
            e.isValid = true;
            var x = txtEDate_S.GetValue();
            var y = txtEDate_E.GetValue();

            var dvalue = s.GetValue(); 
            
            if (x == null) { x = ""; }
            if (y == null) { y = ""; }

            if (x != "" && y != "") {

                e.isValid = (x <= y);
                if (!e.isValid) {
                    alert("[結束訖日起值]不允許大於[結束訖日訖值]，請重新輸入!");
                    s.SetValue(null);
                }
            }


            if (e.isValid && dvalue != "") {
                var Sx = txtSDate_S.GetValue();
                var Sy = txtSDate_E.GetValue();
                if (Sx == null) { Sx = ""; }
                if (Sy == null) { Sy = ""; }

                if ((Sx != "" && dvalue < Sx) || (Sy != "" && dvalue < Sy)) {
                    e.isValid = false;
                    alert("[結束日期]不允許小於[開始日期]，請重新輸入!");
                    s.SetValue(null);
                }
            }
        }

        function CheckRateKeydown(s, e)
        {
            var sValue = s.GetValue();
            if (sValue != null && sValue != '')
            {
                var regex = /^-?\d+\.?\d*?$/;
                if (sValue.match(regex) == null)
                {

                    s.SetText(sValue.substr(0, sValue.length - 1));
                    event.returnValue = false;
                    return;
                }
                else
                {
                    var splitNumber = sValue.split('.');
                    if (splitNumber.length == 2)
                    {
                        if (splitNumber[1].length > 1 && (event.keyCode >= 49 && event.keyCode <= 105 || event.keyCode == 190))
                        {
                            event.returnValue = false;
                            return;
                        }

                    }
                }
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="titlef">
        <!--信用卡手續費設定作業 -->
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, CreditCardFeesSetting %> "></asp:Literal>
    </div>
    <div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt">
                        <!--信用卡別-->
                        <asp:Literal ID="Literal31" runat="server" Text="<%$ Resources:WebResources, TypeOfCreditCard %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlCardType" runat="server"></dx:ASPxComboBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
                    </td>
                    <td class="tdtxt">
                        <!--狀態-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, status %>"></asp:Literal>：
                    </td>
                    <td class="tdval">
                        <dx:ASPxComboBox ID="ddlStatus" runat="server"></dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <!--開始日期-->
                        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, StartDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate_S" runat="server" ClientInstanceName="txtSDate_S" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkSDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtSDate_E" runat="server" ClientInstanceName="txtSDate_E" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkSDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right">
                        <!--結束日期-->
                        <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, EndDate %>"></asp:Literal>：
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" style="width:240px">
                            <tr>
                                <td><dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, Start %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtEDate_S" runat="server" ClientInstanceName="txtEDate_S" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkEDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>&nbsp;</td>
                                <td><dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, End %>"></dx:ASPxLabel></td>
                                <td>
                                    <dx:ASPxDateEdit ID="txtEDate_E" runat="server" ClientInstanceName="txtEDate_E" EditFormatString="yyyy/MM/dd">
                                        <ClientSideEvents ValueChanged="function(s, e){ chkEDate(s, e); }"  />
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReset" runat="server" SkinID="ResetButton" Text="<%$ Resources:WebResources, Reset %>"></dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="CCPR_ID"
                    Settings-ShowTitlePanel="true" Width="100%" EnableCallBacks="False"
                    OnPageIndexChanged="gvMaster_PageIndexChanged"
                    OnRowUpdating="gvMaster_RowUpdating" 
                    OnRowInserting="gvMaster_RowInserting" 
                    OnCellEditorInitialize="gvMaster_CellEditorInitialize"
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated"  
                    OnRowValidating="gvMaster_RowValidating"
                    OnInitNewRow="gvMaster_InitNewRow" 
                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectCheckbox="true">
                            <HeaderTemplate>
                                <div style="text-align: center">
                                    <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="1">
                            <EditButton Visible="true">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn runat="server" FieldName="ITEMNO" Caption="<%$ Resources:WebResources, Items %>"
                            VisibleIndex="2" ReadOnly="true" PropertiesTextEdit-Style-HorizontalAlign="Right">
                            <DataItemTemplate>
                                <%#Container.ItemIndex + 1%>
                            </DataItemTemplate>
                            <PropertiesTextEdit>
                                <ReadOnlyStyle>
                                    <Border BorderStyle="None" />
                                </ReadOnlyStyle>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="STATUS" runat="server" Caption="<%$ Resources:WebResources, Status %>"
                            VisibleIndex="3">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="lblSTATUS" runat="server" Text='<%#Bind("STATUS") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="CREDIT_CARD_TYPE_ID" runat="server" VisibleIndex="4">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, TypeOfCreditCard %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <PropertiesComboBox ValueType="System.String">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("CREDIT_CARD_TYPE_NAME") %>'></asp:Label>
                            </DataItemTemplate>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="CHARGE_RATE" runat="server" VisibleIndex="5">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, ServiceCharges %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField Value='<%# Bind("CREATE_USER") %>' runat="server" ID="hidCREATE_USER" />
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxTextBox ID="TextBox1" runat="server" Width="40px" Text='<%# BIND("CHARGE_RATE")%>' 
                                                ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>' MaxLength="5">
                                                <ValidationSettings>
                                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                                    <RegularExpression ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"
                                                        ErrorText="輸入字串不為數字格式或格式不正確，請重新輸入" />
                                                </ValidationSettings>
                                               <ClientSideEvents   KeyDown="function(s,e) {CheckRateKeydown(s, e); }" />       
                                            </dx:ASPxTextBox>
                                            <td>
                                                %
                                            </td>
                                        </td>
                                    </tr>
                                </table>
                                
                            </EditItemTemplate>
                            <DataItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("CHARGE_RATE") %>'></asp:Label>%
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="S_DATE" runat="server" VisibleIndex="6">
                            <HeaderCaptionTemplate>
                                <span style="color: Red">*</span>
                                <dx:ASPxLabel ID="Literal7" runat="server" Text="<%$ Resources:WebResources, startdate %>">
                                </dx:ASPxLabel>
                            </HeaderCaptionTemplate>
                            <EditItemTemplate>
                                <dx:ASPxDateEdit MinDate='<%# DateTime.Today.AddDays(1) %>' ID="txtSDate" Value='<%# Bind("S_DATE") %>'
                                    runat="server" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                    EditFormat="Custom" EditFormatString="yyyy/MM/dd">
                                    <ValidationSettings>
                                        <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn FieldName="E_DATE" runat="server" Caption="<%$ Resources:WebResources, EndDate %>"
                            VisibleIndex="7">
                            <EditItemTemplate>
                                <dx:ASPxDateEdit MinDate='<%# DateTime.Today.AddDays(1) %>' ID="txtEDate" Value='<%# Bind("E_DATE") %>'
                                    runat="server" ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'
                                    EditFormat="Custom" EditFormatString="yyyy/MM/dd">
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" runat="server" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                            VisibleIndex="8" ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" runat="server" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                            VisibleIndex="9" ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnAdd" runat="server" Text="<%$ Resources:WebResources, add %>"
                                            Visible="true" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) {CheckAll_onclick(); gvMaster.AddNewRow(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" SkinID="DeleteBtn" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                            Visible="true" OnClick="btnDelete_Click">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsEditing Mode="Inline" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoMatchesFound %>" />
                    <Settings ShowTitlePanel="True" />
                    <SettingsPager PageSize="10"></SettingsPager>
                </cc:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
