<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT13.aspx.cs" Inherits="VSS_OPT_OPT13" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=200,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   
    <div>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--HG活動兌點限制－單商品-->
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, HappyGoRedeemPointsForProduct %>"></asp:Literal>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--活動代號-->
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ActivityNo %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox7" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--活動名稱-->
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, ActivityName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox8" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt">
                    </td>
                    <td class="tdval">
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
                                <td><asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
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
                    <td class="tdval" colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td><asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="txtProductCode1" runat="server"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="btnChooseProduct" runat="server" Text="<%$ Resources:WebResources, Choose %>" AutoPostBack="false" SkinID="PopupButton" /></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="txtProductCode2" runat="server"></dx:ASPxTextBox></td>
                                <td><dx:ASPxButton ID="btnChooseProduct1" runat="server" Text="<%$ Resources:WebResources, Choose %>" AutoPostBack="false" SkinID="PopupButton" /></td>
                            </tr>
                        </table>
                    </td>                   
                    <td class="tdtxt" nowrap="nowrap">
                        <!--商品名稱-->
                        <asp:Literal ID="Literal26" runat="server" Text="<%$ Resources:WebResources, ProductName %>"></asp:Literal>：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox11" runat="server"></dx:ASPxTextBox>
                    </td>
                </tr>                                
            </table>
        </div>

        <div class="seperate"></div>

        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td><dx:ASPxButton ID="btnSearch" runat="server" Text="<%$ Resources:WebResources, Search %>" OnClick="btnSearch_Click" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <dx:ASPxButton ID="btnClear" runat="server" Text="<%$ Resources:WebResources, Reset %>" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s, e){ aspnetForm.reset(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

        <div class="seperate"></div>

        <div>            
            <div id="Div1" class="SubEditBlock">

                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="活動代號" Width="100%" Settings-ShowTitlePanel="true" 
                OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged"
                AutoGenerateColumns="False" OnRowInserting="gvMaster_RowInserting" onrowupdating="gvMaster_RowUpdating">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Button" >
                            <EditButton Visible="true"></EditButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="活動代號" Caption="<%$ Resources:WebResources, ActivityNo %>" HeaderStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text='<%#Eval("[活動代號]") %>'>
                                </dx:ASPxLabel>
                            </EditItemTemplate>
                            <DataItemTemplate>
                                <asp:LinkButton ID="lbtnActivityNo" runat="server" Text='<%# Bind("活動代號") %>' OnClick="lbtnActivityNo_Click" />
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="活動名稱" Caption="<%$ Resources:WebResources, ActivityName %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-HorizontalAlign="Center" >
                            <EditItemTemplate>
                                <table><tr>
                                    <td><dx:ASPxTextBox ID="txtProductCode" runat="server" Width="68px" Text='<%#Eval("[商品料號]") %>'>
                                    </dx:ASPxTextBox></td>
                                    <td><dx:ASPxButton ID="ChooseProductCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>" ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}">
                                    </dx:ASPxButton></td>
                                </tr></table>
                            </EditItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="開始日期" Caption="<%$ Resources:WebResources, StartDate %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="結束日期" Caption="<%$ Resources:WebResources, EndDate %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn FieldName="名單檢核" Caption="<%$ Resources:WebResources, NameListVerification %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                            <DataItemTemplate>
                                <dx:ASPxCheckBox ID="CheckItem" runat="server" ReadOnly="true"/>
                            </DataItemTemplate>
                            <PropertiesCheckEdit ReadOnlyStyle-Border-BorderStyle="None" />
                        </dx:GridViewDataCheckColumn>
                        <dx:GridViewDataTextColumn FieldName="折抵方式" Caption="<%$ Resources:WebResources, RedemptionMethod %>" HeaderStyle-HorizontalAlign="Center" />            
                        <dx:GridViewDataTextColumn FieldName="折抵上限" Caption="<%$ Resources:WebResources, RedemptionLimit %>" HeaderStyle-HorizontalAlign="Center" />            
                        <dx:GridViewDataTextColumn FieldName="折抵次數" Caption="<%$ Resources:WebResources, RedeemingFrequency %>" HeaderStyle-HorizontalAlign="Center" />            
                        <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                        </DetailRow>
                        <TitlePanel>
                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td><dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAdd_Click" /></td>
                                    <td><dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>"/></td>
                                </tr>
                            </table>
                        </TitlePanel>
                        <EmptyDataRow>
                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                        </EmptyDataRow>
                    </Templates>
                    <SettingsEditing Mode="Inline" />
                    <SettingsPager PageSize="5"></SettingsPager>
                </cc:ASPxGridView>

            </div>

            <div class="seperate"></div>


            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Width="100%" Visible="false">
                <TabPages>
                    <dx:TabPage Text="兌點設定">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">

                                <div class="SubEditBlock">
                                <cc:ASPxGridView ID="gvDetail1" runat="server" ClientInstanceName="gvDetail1" Settings-ShowTitlePanel="true"
                                    Width="100%" OnRowInserting="gvDetail1_RowInserting" onrowupdating="gvDetail1_RowUpdating" 
                                    KeyFieldName="項次" AutoGenerateColumns="False" >
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvDetail1.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button">
                                            <EditButton Visible="true"></EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" >
                                                <ReadOnlyStyle>
                                                <Border BorderStyle="None"></Border>
                                                </ReadOnlyStyle>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="兌點名稱" Caption="<%$ Resources:WebResources, RedemptionName %>" HeaderStyle-HorizontalAlign="Center" >            
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="點數" Caption="<%$ Resources:WebResources, Points %>" HeaderStyle-HorizontalAlign="Center" >            
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="兌換金額" Caption="<%$ Resources:WebResources, RedemptionAmount %>" HeaderStyle-HorizontalAlign="Center" >            
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsEditing Mode="Inline" />
                                    <Settings ShowTitlePanel="True"></Settings>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td><dx:ASPxButton ID="btnAddDetail_1" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddDetail1_Click" /></td>
                                                    <td><dx:ASPxButton ID="btnDelDetail_1" runat="server" Text="<%$ Resources:WebResources, Delete %>"/></td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                        <EmptyDataRow>
                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                        </EmptyDataRow>
                                    </Templates>
                                </cc:ASPxGridView>
                                </div>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="指定門市">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">

                                <div class="SubEditBlock">
                                <cc:ASPxGridView ID="gvDetail2" runat="server" ClientInstanceName="gvDetail2" Settings-ShowTitlePanel="true"
                                    Width="100%" OnRowInserting="gvDetail2_RowInserting" onrowupdating="gvDetail2_RowUpdating" 
                                    KeyFieldName="門市編號" AutoGenerateColumns="False" >
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvDetail2.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button">
                                            <EditButton Visible="true"></EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="門市編號" Caption="<%$ Resources:WebResources, StoreNo %>" HeaderStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtStoreNo" runat="server" Width="68px" Text='<%#Eval("[門市編號]") %>' >
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ChooseStoreNoButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                ClientSideEvents-Click="function(s,e){openwindow('../OPT/OPT13_choseStore.aspx',500,400);return false;}">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="門市名稱" Caption="<%$ Resources:WebResources, StoreName %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" >
                                                <ReadOnlyStyle>
                                                <Border BorderStyle="None"></Border>
                                                </ReadOnlyStyle>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="區域別" 
                                            Caption="<%$ Resources:WebResources, ByDistrict %>" 
                                            HeaderStyle-HorizontalAlign="Center" >            
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsEditing Mode="Inline" />
                                    <Settings ShowTitlePanel="True"></Settings>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td><dx:ASPxButton ID="btnAddDetail_2" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddDetail2_Click" /></td>
                                                    <td><dx:ASPxButton ID="btnDelDetail_2" runat="server" Text="<%$ Resources:WebResources, Delete %>"/></td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" Enabled="True">
                                                            <asp:ListItem Text="區域" Value="區域" />
                                                            <asp:ListItem Text="ALL" Value="ALL" />
                                                            <asp:ListItem Text="北一區" Value="北一區" />
                                                            <asp:ListItem Text="中一區" Value="中一區" />
                                                            <asp:ListItem Text="南一區" Value="南一區" />
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td><dx:ASPxButton ID="btnSubmit" runat="server" Text="<%$ Resources:WebResources, SubmitDistrict %>"/></td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                        <EmptyDataRow>
                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                        </EmptyDataRow>
                                    </Templates>
                                </cc:ASPxGridView>
                                </div> 

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="加購價">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">

                                <div class="SubEditBlock">
                                <cc:ASPxGridView ID="gvDetail3" runat="server" ClientInstanceName="gvDetail3" Settings-ShowTitlePanel="true"
                                    Width="100%" OnRowInserting="gvDetail3_RowInserting" onrowupdating="gvDetail3_RowUpdating" 
                                    KeyFieldName="商品料號" AutoGenerateColumns="False" >
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvDetail3.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button">
                                            <EditButton Visible="true"></EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtProductCode" runat="server" Width="68px" Text='<%#Eval("[商品料號]") %>'>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ChooseProductCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>"
                                                                ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true">
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" >
                                                <ReadOnlyStyle>
                                                <Border BorderStyle="None"></Border>
                                                </ReadOnlyStyle>
                                            </PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="兌換點數" 
                                            Caption="<%$ Resources:WebResources, RedemptionPoints %>" 
                                            HeaderStyle-HorizontalAlign="Center" >            
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="加購價" 
                                            Caption="<%$ Resources:WebResources, AdditionalCharges %>" 
                                            HeaderStyle-HorizontalAlign="Center" >            
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsEditing Mode="Inline" />
                                    <Settings ShowTitlePanel="True"></Settings>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td><dx:ASPxButton ID="btnAddDetail_3" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddDetail3_Click" /></td>
                                                    <td><dx:ASPxButton ID="btnDelDetail_3" runat="server" Text="<%$ Resources:WebResources, Delete %>"/></td>
                                                </tr>
                                            </table>
                                        </TitlePanel>
                                        <EmptyDataRow>
                                            <asp:Label ID="emptyDataLabel" runat="server" Text="<%$ Resources:WebResources, NoResultsToDisplay %>"></asp:Label>
                                        </EmptyDataRow>
                                    </Templates>
                                </cc:ASPxGridView>
                                </div>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>


            <div class="seperate"></div>

        </div>
    </div>

    <cc:ASPxPopupControl ID="productsPopup" SkinID="ProductsPopup" runat="server"  
         EnableViewState="False" PopupElementID="btnChooseProduct" TargetElementID="txtProductCode1">
    </cc:ASPxPopupControl>
    <cc:ASPxPopupControl ID="productsPopup1" SkinID="ProductsPopup" runat="server"  
         EnableViewState="False" PopupElementID="btnChooseProduct1" TargetElementID="txtProductCode2">
    </cc:ASPxPopupControl>

</asp:Content>
