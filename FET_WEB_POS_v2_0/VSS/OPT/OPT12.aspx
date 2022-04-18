<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OPT12.aspx.cs" Inherits="VSS_OPT12_OPT12" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AdvTekUserCtrl" Namespace="AdvTekUserCtrl" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AdvTekUserCtrl" %>
<%@ Register src="~/Controls/PopupWindow.ascx" tagname="PopupWindow" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../ClientUtility/jquery.js"></script>
    <script type="text/javascript" src="../../ClientUtility/jquery.checkboxes.js"></script> 

    <script type="text/javascript" language="javascript">
        function openwindow(url, width, height) {
            window.open(url, "window", 'width=' + width + ',height=' + height + ',top=230,left=350,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">   

        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="titlef">
                <tr>
                    <td align="left">
                        <!--HappyGo點數累點設定-->
                        <asp:Literal ID="Literal10" runat="server" Text="HappyGo點數累點設定"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div class="criteria">
            <table>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--累點名稱-->
                        累點名稱：
                    </td>
                    <td class="tdval" nowrap="nowrap">
                        <dx:ASPxTextBox ID="TextBox1" runat="server"></dx:ASPxTextBox>
                    </td>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--開始日期-->
                        開始日期：
                    </td>
                    <td class="tdval" colspan="3" nowrap="nowrap">
                         <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td><asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit></td>
                            </tr>
                         </table>
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--累點點數-->
                        累點點數：
                    </td>
                    <td class="tdval" colspan="5" nowrap="nowrap">

                         <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td><asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="TextBox2" runat="server"></dx:ASPxTextBox></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="TextBox3" runat="server"></dx:ASPxTextBox></td>
                            </tr>
                         </table>

                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdtxt" nowrap="nowrap">
                        <!--累點金額-->
                        累點金額：
                    </td>
                    <td class="tdval" colspan="5" nowrap="nowrap">

                         <table cellpadding="0" cellspacing="0" border="0"> 
                            <tr>
                                <td><asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="TextBox4" runat="server"></dx:ASPxTextBox></td>
                                <td>&nbsp;</td>
                                <td><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal></td>
                                <td><dx:ASPxTextBox ID="TextBox5" runat="server"></dx:ASPxTextBox></td>
                            </tr>
                         </table>

                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                    <td class="tdtxt">
                        &nbsp;
                    </td>
                    <td class="tdval">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
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
        
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%">
                <TabPages>
                    <dx:TabPage Text="累點設定">
                        <ContentCollection>
                            <dx:ContentControl>

                            <div>
                                <cc:ASPxGridView ID="gvMaster" ClientInstanceName="gvMaster" runat="server" KeyFieldName="累點代號" Width="100%" 
                                OnHtmlRowPrepared="gvMaster_HtmlRowPrepared" OnHtmlRowCreated="gvMaster_HtmlRowCreated" OnPageIndexChanged="gvMaster_PageIndexChanged"
                                AutoGenerateColumns="False" OnRowInserting="gvMaster_RowInserting" onrowupdating="gvMaster_RowUpdating" Settings-ShowTitlePanel="true">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvMaster.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" >
                                            <EditButton Visible="true"></EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="累點代號" Caption="累點代號" HeaderStyle-HorizontalAlign="Center" />
                                        <dx:GridViewDataTextColumn FieldName="累點名稱" Caption="累點名稱" HeaderStyle-HorizontalAlign="Center" />
                                        <dx:GridViewDataDateColumn FieldName="開始日期" Caption="開始日期" HeaderStyle-HorizontalAlign="Center" />
                                        <dx:GridViewDataDateColumn FieldName="結束日期" Caption="結束日期" HeaderStyle-HorizontalAlign="Center" />
                                        <dx:GridViewDataTextColumn FieldName="累點金額" Caption="累點金額" HeaderStyle-HorizontalAlign="Center" />
                                        <dx:GridViewDataTextColumn FieldName="累點點數" Caption="累點點數" HeaderStyle-HorizontalAlign="Center" />
                                        <dx:GridViewDataTextColumn FieldName="更新人員" Caption="<%$ Resources:WebResources, ModifiedBy %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="更新日期" Caption="<%$ Resources:WebResources, ModifiedDate %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >            
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td><dx:ASPxButton ID="btnAddNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddNew_Click" /></td>
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

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    
                    <dx:TabPage Text="會員日期設定">
                        <ContentCollection>
                            <dx:ContentControl>

                            <div>
                                <cc:ASPxGridView ID="gvMember" ClientInstanceName="gvMember" runat="server" KeyFieldName="項次" Width="100%" 
                                OnHtmlRowPrepared="gvMember_HtmlRowPrepared" OnHtmlRowCreated="gvMember_HtmlRowCreated" OnPageIndexChanged="gvMember_PageIndexChanged"
                                AutoGenerateColumns="False" OnRowInserting="gvMember_RowInserting" onrowupdating="gvMember_RowUpdating" Settings-ShowTitlePanel="true" >
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvMember.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" >
                                            <EditButton Visible="true"></EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataDateColumn FieldName="會員起日" Caption="會員起日" HeaderStyle-HorizontalAlign="Center" />
                                        <dx:GridViewDataDateColumn FieldName="會員訖日" Caption="會員訖日" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td><dx:ASPxButton ID="btnAddNew_1" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddNew_Click1" /></td>
                                                    <td><dx:ASPxButton ID="btnDelete_1" runat="server" Text="<%$ Resources:WebResources, Delete %>"/></td>
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

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    
                    <dx:TabPage Text="排外條件">
                        <ContentCollection>
                            <dx:ContentControl>
                            
                            <div>
                                <cc:ASPxGridView ID="gvCondition" ClientInstanceName="gvCondition" runat="server" KeyFieldName="商品料號" Width="100%" 
                                OnHtmlRowPrepared="gvCondition_HtmlRowPrepared" OnHtmlRowCreated="gvCondition_HtmlRowCreated" OnPageIndexChanged="gvCondition_PageIndexChanged"
                                AutoGenerateColumns="False" OnRowInserting="gvCondition_RowInserting" onrowupdating="gvCondition_RowUpdating" Settings-ShowTitlePanel="true" >
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" onclick="gvCondition.SelectAllRowsOnPage(this.checked);" title="Select/Unselect all rows on the page" />
                                            </HeaderTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn ButtonType="Button" >
                                            <EditButton Visible="true"></EditButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="項次" Caption="<%$ Resources:WebResources, Items %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品料號" Caption="<%$ Resources:WebResources, ProductCode %>" HeaderStyle-HorizontalAlign="Center" >
                                            <EditItemTemplate>
                                                <table><tr>
                                                    <td><dx:ASPxTextBox ID="txtProductCode" runat="server" Width="68px" Text='<%#Eval("[商品料號]") %>' >
                                                    </dx:ASPxTextBox></td>
                                                    <td><dx:ASPxButton ID="ChooseProductCodeButton" runat="server" Text="<%$ Resources:WebResources, Choose %>" ClientSideEvents-Click="function(s,e){openwindow('../ORD/ORD01_searchProductNo.aspx',500,400);return false;}">
                                                    </dx:ASPxButton></td>
                                                </tr></table>
                                            </EditItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="商品名稱" Caption="<%$ Resources:WebResources, ProductName %>" HeaderStyle-HorizontalAlign="Center" ReadOnly="true" >            
                                            <PropertiesTextEdit ReadOnlyStyle-Border-BorderStyle="None" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Templates>
                                        <TitlePanel>
                                            <table align="left" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td><dx:ASPxButton ID="btnAddNew_2" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnAddNew_Click2" /></td>
                                                    <td><dx:ASPxButton ID="btnDelete_2" runat="server" Text="<%$ Resources:WebResources, Delete %>"/></td>
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

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>

        </div>

        <div class="seperate"></div>

</asp:Content>
