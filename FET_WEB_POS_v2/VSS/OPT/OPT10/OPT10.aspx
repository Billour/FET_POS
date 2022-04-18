<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    ValidateRequest="false" CodeFile="OPT10.aspx.cs" Inherits="VSS_OPT_OPT10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
            <!--商品主檔設定-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, ProductDataManagement %>"></asp:Literal>
    </div>

    <div class="criteria">
        <table>
            <tr>
                <td class="tdtxt">
                    <!--商品類別-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ProductCategory %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                    </dx:ASPxComboBox>
                </td>
                <td class="tdtxt">
                </td>
                <td class="tdval">
                </td>
                <td class="tdtxt">
                    <!--商品狀態-->
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ProductStatus %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                        <Items>
                            <dx:ListEditItem Text="ALL" Value="" Selected="true" />
                            <dx:ListEditItem Text="有效" Value="0" />
                            <dx:ListEditItem Text="已過期" Value="2" />
                            <dx:ListEditItem Text="尚未生效" Value="1" />
                        </Items>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdtxt">
                    <!--商品料號-->
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, ProductCode %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox1" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--商品名稱-->
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxTextBox ID="TextBox2" runat="server">
                    </dx:ASPxTextBox>
                </td>
                <td class="tdtxt">
                    <!--檢核IMEI-->
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, VerifyImei %>"></asp:Literal>：
                </td>
                <td class="tdval">
                    <dx:ASPxComboBox ID="ASPxComboBox3" runat="server">
                        <%--<Items>
                    <dx:ListEditItem Text="請選擇" Value="請選擇" Selected="true" />
                    <dx:ListEditItem Text="不控管" Value="1" />
                    <dx:ListEditItem Text="銷售時控管" Value="2" />
                    <dx:ListEditItem Text="全部要控管" Value="3" />
                </Items>--%>
                    </dx:ASPxComboBox>
                </td>
            </tr>
        </table>
    </div>    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="btnPosition">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>"
                               CausesValidation="false"  OnClick="btnSearch_Click" />
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
            
            <div>
                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="PRODNO"
                    Width="100%"  EnableCallBacks="true"  
                    OnRowInserting="gvMaster_RowInserting" 
                    OnRowUpdating="gvMaster_RowUpdating"
                    OnPageIndexChanged="gvMaster_PageIndexChanged" 
                    OnCellEditorInitialize="gvMaster_CellEditorInitialize"
                    OnRowValidating="gvMaster_RowValidating" 
                    OnCommandButtonInitialize="gvMaster_CommandButtonInitialize"
                    OnStartRowEditing="gvMaster_StartRowEditing"
                    OnInitNewRow="gvMaster_InitNewRow" 
                    OnHtmlRowCreated="gvMaster_HtmlRowCreated"
                    OnHtmlRowPrepared="gvMaster_HtmlRowPrepared">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" ShowSelectCheckbox="true">
                            <HeaderTemplate>
                                <div style="text-align: center">
                                    <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page" />
                                </div>
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn VisibleIndex="1" ButtonType="Button" Caption=" ">
                            <EditButton Visible="True">
                            </EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="2" FieldName="STATUS" HeaderStyle-HorizontalAlign="Center"
                            Caption="<%$ Resources:WebResources, Status %>">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="lblSTATUS" runat="server" Text='<%#Bind("STATUS") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRODNO" runat="server" Caption="<%$ Resources:WebResources, ProductCode %>" VisibleIndex="3">
                            <PropertiesTextEdit MaxLength="8" Width="80" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <%--<RegularExpression ValidationExpression="^\d{9}$" />--%>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRODNAME" runat="server" Caption="<%$ Resources:WebResources, ProductName %>"
                            VisibleIndex="4">
                            <PropertiesTextEdit MaxLength="50" Width="200" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <%--<RegularExpression ValidationExpression="^\d{50}$" />--%>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="PRODTYPENO"  Width="160" runat="server" Caption="<%$ Resources:WebResources, ProductCategory %>"
                            VisibleIndex="5">
                            <PropertiesComboBox ValueType="System.String">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("PRODTYPENAME") %>' Width="160"></asp:Label>
                            </DataItemTemplate>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="UNIT" Caption="<%$ Resources:WebResources, Unit %>"
                            VisibleIndex="6">
                            <PropertiesTextEdit MaxLength="10" Width="100" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="[a-zA-z0-9]*" />
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PRICE" Caption="<%$ Resources:WebResources, StandAlonePrice %>"
                            CellStyle-HorizontalAlign="Right" PropertiesTextEdit-MaxLength="9" VisibleIndex="7">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Width="90" Style-HorizontalAlign="Right">
                                <ValidationSettings>
                                    <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="S_DATE" runat="server" Caption="<%$ Resources:WebResources, StartDate %>"
                            VisibleIndex="8">
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="txtSDATE" runat="server" Value='<%# Bind("S_DATE") %>' AutoPostBack="false" MinDate='<%# DateTime.Today.AddDays(1) %>'>
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn VisibleIndex="9" FieldName="E_DATE" HeaderStyle-HorizontalAlign="Center"
                            Caption="<%$ Resources:WebResources, EndDate %>">
                            <EditItemTemplate>
                                <dx:ASPxDateEdit ID="txtEDATE" runat="server" Value='<%#Bind("E_DATE")%>' MinDate='<%# DateTime.Today.AddDays(1) %>'>
                                </dx:ASPxDateEdit>
                            </EditItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataCheckColumn FieldName="ISSTOCK" Caption="<%$ Resources:WebResources, ReduceInventory %>"
                            VisibleIndex="10" />
                        <dx:GridViewDataComboBoxColumn FieldName="IMEI_FLAG"  Width="90" Caption="<%$ Resources:WebResources, VerifyImei %>"
                            VisibleIndex="11">
                            <PropertiesComboBox ValueType="System.String">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesComboBox>
                            <DataItemTemplate>
                                <asp:Label ID="Label23" runat="server" Text='<%# Eval("CHECK_IMEI_TYPE_NAME") %>'
                                    Width="90"></asp:Label>
                            </DataItemTemplate>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataCheckColumn FieldName="IS_OPEN_PRICE" Caption="<%$ Resources:WebResources, CustomPrice %>"
                            VisibleIndex="12" />
                        <dx:GridViewDataTextColumn FieldName="ACC1" runat="server" Caption="<%$ Resources:WebResources, Subject1 %>"
                            ReadOnly="false" VisibleIndex="13">
                            <PropertiesTextEdit MaxLength="2" Width="25" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="^\d{2}$" />
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ACC2" runat="server" Caption="<%$ Resources:WebResources, Subject2 %>"
                            ReadOnly="false" VisibleIndex="14">
                            <PropertiesTextEdit MaxLength="3" Width="30" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="^\d{3}$" />
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ACC3" runat="server" Caption="<%$ Resources:WebResources, Subject3 %>"
                            ReadOnly="false" VisibleIndex="15">
                            <PropertiesTextEdit MaxLength="4" Width="40" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="^\d{4}$" />
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ACC4" runat="server" Caption="<%$ Resources:WebResources, Subject4 %>"
                            ReadOnly="false" VisibleIndex="16">
                            <PropertiesTextEdit MaxLength="6" Width="60" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="^\d{6}$" />
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ACC5" runat="server" Caption="<%$ Resources:WebResources, Subject5 %>"
                            ReadOnly="false" VisibleIndex="17">
                            <PropertiesTextEdit MaxLength="4" Width="40" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="^\d{4}$" />
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ACC6" runat="server" Caption="<%$ Resources:WebResources, Subject6 %>"
                            ReadOnly="false" VisibleIndex="18">
                            <PropertiesTextEdit MaxLength="4" Width="40" Style-HorizontalAlign="Left">
                                <ValidationSettings>
                                    <RegularExpression ValidationExpression="^\d{4}$" />
                                    <RequiredField IsRequired="True" ErrorText="必填欄位" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_DTM" Caption="<%$ Resources:WebResources, ModifiedDate %>"
                            ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MODI_USER_NAME" Caption="<%$ Resources:WebResources, ModifiedBy %>"
                            ReadOnly="true">
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" Style-HorizontalAlign="Left" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <TitlePanel>
                            <table cellpadding="0" cellspacing="0" align="left">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="addButton" runat="server" Text="<%$ Resources:WebResources, Add %>"
                                            OnClick="btnAdd_Click">
                                            <%--AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { gvMaster.AddNewRow(); }" />--%>
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"
                                            OnClick="btnDelete_Click" SkinID="DeleteBtn" />
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <SettingsPager PageSize="10">
                    </SettingsPager>
                    <SettingsEditing Mode="Inline" />
                    <SettingsBehavior AllowFocusedRow="false" ProcessFocusedRowChangedOnServer="false" />
                    <Settings ShowTitlePanel="true" />
                    <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                </cc:ASPxGridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
