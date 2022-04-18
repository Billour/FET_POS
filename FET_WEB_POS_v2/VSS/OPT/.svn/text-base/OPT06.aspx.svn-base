<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPT06.aspx.cs" Inherits="VSS_OPT_OPT06" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #preview_wrapper
        {
            display: inline-block;
            width: 160px;
            height: 160px;
            background-color: #CCC;
        }
        #preview_fake
        {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale);
        }
        #preview_size_fake
        {
            filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);
            visibility: hidden;
        }
        #preview
        {
            width: 160px;
            height: 160px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        function onUploadImgChange(sender) {
            if (!sender.value.match(/.jpg|.gif|.png|.bmp/i)) {
                alert('圖片格式無效');
                return false;
            }
            var objPreview = document.getElementById('preview');
            var objPreviewFake = document.getElementById('preview_fake');
            var objPreviewSizeFake = document.getElementById('preview_size_fake');

            if (sender.files && sender.files[0]) {
                objPreview.style.display = 'block';
                objPreview.style.width = 'auto';
                objPreview.style.height = 'auto';
                objPreview.src = sender.files[0].getAsDataURL();
            } else if (objPreviewFake.filters) {
                sender.select();
                var imgSrc = document.selection.createRange().text;
                objPreviewFake.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = imgSrc;
                objPreviewSizeFake.filters.item('DXImageTransform.Microsoft.AlphaImageLoader').src = imgSrc;
                autoSizePreview(objPreviewFake, objPreviewSizeFake.offsetWidth, objPreviewSizeFake.offsetHeight);
                objPreview.style.display = 'none';
            }
        }

        function onPreviewLoad(sender) {
            autoSizePreview(sender, sender.offsetWidth, sender.offsetHeight);
        }

        function autoSizePreview(objPre, originalWidth, originalHeight) {
            var zoomParam = clacImgZoomParam(160, 160, originalWidth, originalHeight);
            objPre.style.width = zoomParam.width + 'px';
            objPre.style.height = zoomParam.height + 'px';
            objPre.style.marginTop = zoomParam.top + 'px';
            objPre.style.marginLeft = zoomParam.left + 'px';
        }

        function clacImgZoomParam(maxWidth, maxHeight, width, height) {
            var param = { width: width, height: height, top: 0, left: 0 };

            if (width > maxWidth || height > maxHeight) {
                rateWidth = width / maxWidth;
                rateHeight = height / maxHeight;

                if (rateWidth > rateHeight) {
                    param.width = maxWidth;
                    param.height = height / rateWidth;
                } else {
                    param.width = width / rateHeight;
                    param.height = maxHeight;
                }
            }
            param.left = (maxWidth - param.width) / 2;
            param.top = (maxHeight - param.height) / 2;
            return param;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <div>
        <div class="titlef">
            <!--門市基本資料設定作業-->
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:WebResources, StoreSettings %>"></asp:Literal>
        </div>
        <div>
            <div class="criteria">
                <table>
                    <tr>
                        <td class="tdtxt">
                            <!--區域別-->
                            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, ByDistrict %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Value="1">北區</asp:ListItem>
                                <asp:ListItem Value="2">中區</asp:ListItem>
                                <asp:ListItem Value="3">北區</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdtxt">
                            <!--門市代碼-->
                            <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:WebResources, StoreNo %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem>12101</asp:ListItem>
                                <asp:ListItem>12102</asp:ListItem>
                                <asp:ListItem>12103</asp:ListItem>
                                <asp:ListItem>12104</asp:ListItem>
                                <asp:ListItem>12105</asp:ListItem>
                            </asp:DropDownList>                                                        
                            <asp:Button ID="Button1" runat="server" Text="<%$ Resources:WebResources, Confirm %>" OnClick="Button1_Click" />
                        </td>
                        <td class="tdtxt">
                            <!--門市名稱-->
                            <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:WebResources, StoreName %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label9" runat="server" Text="遠企直營店" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--電子郵件信箱-->
                            <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, EmailAddress %>"></asp:Literal>：
                        </td>                            
                        <td class="tdval">
                            <asp:Label ID="Label6" runat="server" Text="aaa@gmail.com" Visible="false"></asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--聯絡電話-->
                            <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, ContactTelephone %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label7" runat="server" Text="0911236548" Visible="false"></asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--傳真號碼-->
                            <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:WebResources, FaxNumber %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label8" runat="server" Text="02-12365478" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            <!--發票地址-->
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:WebResources, InvoiceAddress %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label1" runat="server" Text="台北市大安區敦化南路XX號1F" Visible="false"></asp:Label>
                        </td>
                        <td class="tdtxt">
                            <!--送貨地址-->
                            <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:WebResources, ShipAddress %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:Label ID="Label3" runat="server" Text="台北市大安區敦化南路XX號1F" Visible="false"></asp:Label>
                        </td>                                                
                        <td class="tdtxt">
                            <!--分級店型-->
                            <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:WebResources, StoreType %>"></asp:Literal>：
                        </td>
                        <td class="tdval">
                            <asp:DropDownList ID="DropDownList4" runat="server">
                                <asp:ListItem>大店</asp:ListItem>
                                <asp:ListItem>小店</asp:ListItem>
                                <asp:ListItem>min店</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdtxt">
                            &nbsp;
                        </td>
                        <td class="tdval" colspan="3">
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
                <div id="editMode" runat="server">
                    <table border="0" cellspacing="0">
                        <tr>
                            <td class="tdtxt">
                                <span style="color: Red">*</span><!--統一編號-->
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:WebResources, UnifiedBusinessNo %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdtxt">
                                <span style="color: Red">*</span><!--稅籍編號-->
                                 <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:WebResources, BusinessTaxSerialNumber %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdtxt">
                                <span style="color: Red">*</span><!--成本中心-->
                                <asp:Literal ID="Literal13" runat="server" Text="<%$ Resources:WebResources, CostCenter %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                    <asp:ListItem>-請選擇-</asp:ListItem>
                                    <asp:ListItem>行銷部</asp:ListItem>
                                    <asp:ListItem>通路管理</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <span style="color: Red">*</span><!--開店日期-->
                                 <asp:Literal ID="Literal14" runat="server" Text="<%$ Resources:WebResources, StoreOpenningDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server"></dx:ASPxDateEdit>                                
                            </td>
                            <td class="tdtxt">
                                <!--撤店日期-->
                                <asp:Literal ID="Literal15" runat="server" Text="<%$ Resources:WebResources, StoreClosingDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdtxt">
                                <span style="color: Red">*</span><!--發票章上傳-->
                                <asp:Literal ID="Literal16" runat="server" Text="<%$ Resources:WebResources, InvoiceStampUpload %>"></asp:Literal>：
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="160px" onchange="javascript:onUploadImgChange(this);" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--暫停起始日-->
                                <asp:Literal ID="Literal17" runat="server" Text="<%$ Resources:WebResources, SuspensionDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server"></dx:ASPxDateEdit> 
                            </td>
                            <td class="tdtxt">
                                <!--暫停結束日-->
                                <asp:Literal ID="Literal18" runat="server" Text="<%$ Resources:WebResources, InvoiceStampUpload %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                               <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server"></dx:ASPxDateEdit> 
                            </td>
                            <td colspan="2" rowspan="5" align="center">
                                <div id="preview_wrapper">
                                    <div id="preview_fake">
                                        <img id="preview" onload="onPreviewLoad(this)" />
                                    </div>
                                </div>
                                <img id="preview_size_fake" style="display: none" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <span style="color: Red">*</span><!--出貨倉別-->
                                <asp:Literal ID="Literal19" runat="server" Text="<%$ Resources:WebResources, ShipmentWarehouse %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:DropDownList ID="DropDownList5" runat="server">
                                    <asp:ListItem>Retail-N</asp:ListItem>
                                    <asp:ListItem>Retail-C</asp:ListItem>
                                    <asp:ListItem>Retail-S</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdtxt">
                                <span style="color: Red">*</span><!--Non出貨倉別-->
                                <asp:Literal ID="Literal20" runat="server" Text="<%$ Resources:WebResources, NonShipmentWarehouse %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:DropDownList ID="DropDownList6" runat="server">
                                    <asp:ListItem>Retail-N</asp:ListItem>
                                    <asp:ListItem>Retail-C</asp:ListItem>
                                    <asp:ListItem>Retail-S</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--國稅局核准號-->
                                <asp:Literal ID="Literal21" runat="server" Text="<%$ Resources:WebResources, NtaApprovalCode %>"></asp:Literal>：
                            </td>
                            <td class="tdval" colspan="3">
                                依<asp:TextBox ID="TextBox8" runat="server" Width="40"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="TextBox9" runat="server" Width="30"></asp:TextBox>年
                                <asp:TextBox ID="TextBox10" runat="server" Width="30"></asp:TextBox>月
                                <asp:TextBox ID="TextBox11" runat="server" Width="30"></asp:TextBox>日
                                <asp:TextBox ID="TextBox13" runat="server" Width="30"></asp:TextBox>字第
                                <asp:TextBox ID="TextBox14" runat="server" Width="60"></asp:TextBox>號函核准使用
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tdtxt">
                                <!--最後更新日期-->
                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:WebResources, LastModifiedDate %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Label ID="Label10" runat="server" Text="2010/07/01 22:00"></asp:Label>
                            </td>
                            <td class="tdtxt">
                                <!--維護人員-->
                                <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, MaintainedBy %>"></asp:Literal>：
                            </td>
                            <td class="tdval">
                                <asp:Label ID="Label11" runat="server" Text="12345 王大寶"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <div id="Div1" runat="server" class="SubEditBlock">
                        <div class="SubEditCommand">
                            <asp:Button ID="btnNew" runat="server" Text="<%$ Resources:WebResources, Add %>" OnClick="btnNew_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" />
                        </div>
                        <div class="GridScrollBar">
                            <asp:GridView ID="gvMaster" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                OnRowCancelingEdit="gvMaster_RowCancelingEdit" OnRowEditing="gvMaster_RowEditing"
                                OnRowUpdating="gvMaster_RowUpdating">
                                <EmptyDataTemplate>
                                    <tr>
                                        <th scope="col">
                                            <!--機台號碼-->
                                            <asp:Literal ID="Literal23" runat="server" Text="<%$ Resources:WebResources, CashRegisterNo %>"></asp:Literal>
                                        </th>
                                        <th scope="col">
                                            <!--機台IP-->
                                            <asp:Literal ID="Literal24" runat="server" Text="<%$ Resources:WebResources, CashRegisterIP %>"></asp:Literal>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="tdEmptyData">
                                            <asp:Literal ID="Literal25" runat="server" Text="<%$ Resources:WebResources, AddNewRowToEmptyGrid %>"></asp:Literal>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="CheckAll" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckItem" runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="false" ShowEditButton="True" ButtonType="Button"
                                        UpdateText="存檔" />
                                    <asp:BoundField DataField="機台號碼" HeaderText="機台號碼" />
                                    <asp:BoundField DataField="機台IP" HeaderText="機台IP" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>                                         
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="seperate"></div>
            <div class="btnPosition">
                <asp:Button ID="btnSave" runat="server" Text="儲存" />
            </div>
        </div>
        </div>
    </form>
</body>
</html>
