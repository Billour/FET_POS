<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" CodeFile="SAL15.aspx.cs" Inherits="VSS_SAL_SAL015" %>

<%@ Register Src="~/Controls/PopupControl.ascx" TagName="PopupControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<head id="Head1">
<base target="_self"></base>
</head>
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ReturnHappyGo() {
           // var url = "../../CheckOut/CheckOutHG6.aspx";
           // window.open(url, "window", "width:980px;height:450px");
            //funcRet = "123456789|9000";
//            var result = new Array();
//            result = funcRet.split("|");
//            txtHGCardNo.SetText(result[0]);
//            txtHGCardCount.SetText(result[1]);

//            var ActivityId = txtActivityId.GetText();
//            var HGCardNo = txtHGCardNo.GetText();
//            var HGCardCount = txtHGCardCount.GetText();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div>
        <div class="titlef">
            <!--HG翴计传-ㄓ┍搂-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoPointsExchangeStoreGift2 %>"></asp:Literal>
        </div>
        <div class="seperate">
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="SubEditBlock">
                        <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="ACTIVITY_ID"
                            Width="500px"  EnableCallBacks="false" OnHtmlRowPrepared="gvMaster_HtmlRowPrepared"
                            OnPageIndexChanged="gvMaster_PageIndexChanged">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <HeaderTemplate>
                                        <div style="text-align: center">
                                            <input type="checkbox" id="checkbox1" onclick="CheckAll_onclick();" title="Select/Unselect all rows on the page"  />
                                        </div>
                                    </HeaderTemplate>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="ITEMNO" runat="server" Caption="<%$ Resources:WebResources, Items %>"
                                    ReadOnly="true" VisibleIndex="1" CellStyle-HorizontalAlign="Center" Width="30">
                                    <DataItemTemplate>
                                        <%#Container.ItemIndex + 1%>
                                    </DataItemTemplate>
                                    <PropertiesTextEdit>
                                        <ReadOnlyStyle>
                                            <Border BorderStyle="None" />
                                        </ReadOnlyStyle>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ACTIVITY_NAME" Caption="<%$ Resources:WebResources, ActivityName %>"
                                    VisibleIndex="2">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="txtActivityNo" Text='<%# Bind("ACTIVITY_NO") %>' runat="server"
                                            ClientVisible="false">
                                        </dx:ASPxTextBox>
                                        <dx:ASPxTextBox ID="txtTYPE" Text='<%# Bind("TYPE") %>' runat="server" ClientVisible="false">
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel ID="lblDiscountName" Text='<%# Bind("ACTIVITY_NAME") %>' runat="server">
                                        </dx:ASPxLabel>
                                        
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, StartDate %>"
                                    VisibleIndex="3">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EndDate %>"
                                    VisibleIndex="4">
                                </dx:GridViewDataDateColumn>
                                <%--<dx:GridViewDataComboBoxColumn FieldName="TYPE" Caption="<%$ Resources:WebResources, Category %>">
                                    <PropertiesComboBox ValueType="System.String">
                                    </PropertiesComboBox>
                                    <DataItemTemplate>
                                        <dx:ASPxLabel ID="lblTYPE" Text='<%# Bind("TYPE") %>' runat="server">
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                   
                                </dx:GridViewDataComboBoxColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>"
                                    VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DIVIDABLE_POINT" Caption="<%$ Resources:WebResources, Points %>"
                                    VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn FieldName="MEMBER_CHECK_FLAG" Caption="<%$ Resources:WebResources, NameListVerification %>"
                                    VisibleIndex="7">
                                    <DataItemTemplate>
                                        <dx:ASPxCheckBox ID="cbMEMBER_CHECK_FLAG" runat="server" ReadOnly="true">
                                        </dx:ASPxCheckBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn FieldName="USE_COUNT" Caption="传Ω计" VisibleIndex="8">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <TitlePanel>
                                
                                </TitlePanel>
                            </Templates>
                            <SettingsBehavior AllowFocusedRow="false" />
                            <SettingsPager PageSize="5">
                            </SettingsPager>
                            <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                            <Settings ShowTitlePanel="false" />
                            <SettingsEditing Mode='Inline' />
                        </cc:ASPxGridView>
                    </div>
                    <div class="seperate">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="seperate">
        </div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Text="<%$ Resources:WebResources, Ok %>"
                            OnClick="btnSave_Click" >
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Cancel %>"
                            ClientSideEvents-Click="function(s,e){ window.close();}">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <dx:ASPxTextBox ID="txtHGCardNo" runat="server" Width="170px" 
        ClientInstanceName="txtHGCardNo" ClientVisible="false" >
    </dx:ASPxTextBox>
    <dx:ASPxTextBox ID="txtHGCardCount" runat="server" Width="170px" 
        ClientInstanceName="txtHGCardCount" ClientVisible="false">
    </dx:ASPxTextBox>
    <dx:ASPxTextBox ID="txtActivityId" runat="server" Width="170px" 
        ClientInstanceName="txtActivityId" ClientVisible="false">
    </dx:ASPxTextBox>
</asp:Content>
