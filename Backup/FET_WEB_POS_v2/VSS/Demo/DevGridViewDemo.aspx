<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DevGridViewDemo.aspx.cs" Inherits="VSS_Demo_DevGridViewDemo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../../ClientUtility/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(function() {
            //因為GridView自已產生的RadioButton，它的Name會不同群組，所以用JQuery把它變為一致
            $("input:radio").attr("name", "SameRadio");
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="func">
        <div>
        </div>
        <div class="seperate" style="height:100px">
            
            <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server"></dx:PopupControlContentControl>
                </ContentCollection>
                <Windows>
                    <dx:PopupWindow Target="_blank">
                        <ContentCollection>
                            <dx:PopupControlContentControl runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:PopupWindow>
                </Windows>
            </dx:ASPxPopupControl>
        </div>
        <div>
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Style="text-align: left"
                Width="100%" CssClass="visoft__tab_xpie7">
                <asp:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>
                        <span>賠償項目</span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="Button1" runat="server" Text="新增" OnClick="Button1_Click" /><asp:Button
                                            ID="Button2" runat="server" Text="刪除" />
                                    </div>
                                    <div class="GridScrollBar" style="height: auto">
                                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css"
                                            CssPostfix="Office2003Blue" KeyFieldName="賠償項目" OnDataBinding="ASPxGridView1_DataBinding"
                                            OnDataBound="ASPxGridView1_DataBound" SettingsPager-PageSize="5" OnPageIndexChanged="ASPxGridView1_PageIndexChanged"
                                            OnCustomUnboundColumnData="ASPxGridView1_CustomUnboundColumnData" SettingsPager-RenderMode="Classic"
                                            OnCustomCallback="ASPxGridView1_CustomCallback" OnRowUpdating="ASPxGridView1_RowUpdating"
                                            OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting">
                                            <Styles CssFilePath="~/App_Themes/Office2003Blue/{0}/styles.css" CssPostfix="Office2003Blue">
                                                <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                </Header>
                                                <LoadingPanel ImageSpacing="10px">
                                                </LoadingPanel>
                                            </Styles>
                                            <SettingsPager PageSize="5">
                                            </SettingsPager>
                                            <ImagesFilterControl>
                                                <LoadingPanel Url="~/App_Themes/Office2003Blue/Editors/Loading.gif">
                                                </LoadingPanel>
                                            </ImagesFilterControl>
                                            <Images SpriteCssFilePath="~/App_Themes/Office2003Blue/{0}/sprite.css">
                                                <LoadingPanelOnStatusBar Url="~/App_Themes/Office2003Blue/GridView/gvLoadingOnStatusBar.gif">
                                                </LoadingPanelOnStatusBar>
                                                <LoadingPanel Url="~/App_Themes/Office2003Blue/GridView/Loading.gif">
                                                </LoadingPanel>
                                            </Images>
                                            <Columns>
                                                <dx:GridViewCommandColumn VisibleIndex="0">
                                                    <NewButton Visible="true" />
                                                    <EditButton Visible="true" />
                                                    <DeleteButton Visible="true" />
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataColumn ExportWidth="100" FieldName="賠償項目" VisibleIndex="1">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="金額" VisibleIndex="2">
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Name="abc" VisibleIndex="3">
                                                    <DataItemTemplate>
                                                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                                        <asp:Button ID="Button6" runat="server" Text="Button" />
                                                        <asp:CheckBox ID="CheckBox3" runat="server" />
                                                        <input id="Radio1" type="radio" value='<%# GetFieldValue(Container.DataItem) %>'
                                                            <%#GetFieldChecked(Container.DataItem)%> name="myradio" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="4">
                                                    <PropertiesTextEdit DisplayFormatString="c" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataColumn FieldName="Quantity" VisibleIndex="5" Name="Quantity" />
                                                <dx:GridViewDataTextColumn FieldName="Total" VisibleIndex="6" UnboundType="Decimal">
                                                    <FooterCellStyle ForeColor="Brown" />
                                                    <PropertiesTextEdit DisplayFormatString="c" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFooter="true" />
                                            <SettingsEditing Mode="EditForm" PopupEditFormWidth="600px" PopupEditFormAllowResize="True" />
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="賠償項目" SummaryType="Count" />
                                                <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" DisplayFormat="c" />
                                                <dx:ASPxSummaryItem FieldName="Quantity" SummaryType="Min" />
                                                <dx:ASPxSummaryItem FieldName="Quantity" SummaryType="Average" />
                                                <dx:ASPxSummaryItem FieldName="Quantity" SummaryType="Max" />
                                            </TotalSummary>
                                            <StylesEditors>
                                                <ProgressBar Height="25px">
                                                </ProgressBar>
                                            </StylesEditors>
                                        </dx:ASPxGridView>
                                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server">
                    <HeaderTemplate>
                        <span>折扣項目</span>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="SubEditBlock">
                                    <div class="SubEditCommand">
                                        <asp:Button ID="Button3" runat="server" Text="新增" /><asp:Button ID="Button4" runat="server"
                                            Text="刪除" /></div>
                                    <div class="GridScrollBar" style="height: auto">
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
    </div>
    </form>
</body>
</html>
