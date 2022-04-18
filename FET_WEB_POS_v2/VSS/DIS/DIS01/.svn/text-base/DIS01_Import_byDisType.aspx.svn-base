<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DIS01_Import_byDisType.aspx.cs" Inherits="VSS_DIS_DIS01_DIS01_Import_byDisType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>大量上傳折扣料號</title>
    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    
    <style type="text/css">
    ul, li {
        margin: 0;
        padding: 0;
        list-style: none;
    }
    .abgne_tab {
        clear: left;
        width:  100%;
        margin: 10px 0;
        
        height: 200px; 
        overflow: auto; 
       /* margin:1.6em 0; 
        padding: 0 1.6em; */
        position: relative;
    }
    ul.tabs {
        width: 800px;
        height: 28px;
        border-bottom: 1px solid #999;
        border-left: 1px solid #999;
    }
    ul.tabs li {
        float: left;
        height: 28px;
        line-height: 28px;
        overflow: hidden;
        /*position: relative;*/
        margin-bottom: -1px;	/* 讓 li 往下移來遮住 ul 的部份 border-bottom */
        border: 1px solid #999;
        border-left: none;
        background: #e1e1e1;
    }
    ul.tabs li a {
        display: block;
        padding: 0 20px;
        color: #000;
        border: 1px solid #fff;
        text-decoration: none;
    }
    ul.tabs li a:hover {
        background: #ccc;
    }
    ul.tabs li.active  {
        background: #fff;
        border-bottom: 1px solid#fff;
    }
    ul.tabs li.active a:hover {
        background: #fff;
    }
    div.tab_container {
        clear: left;
        width: auto;
        border: 1px solid #999;
        border-top: none;
        background: #fff;
    }
    div.tab_container .tab_content {
        padding: 20px;
    }
    div.tab_container .tab_content h2 {
        margin: 0 0 20px;
    }
</style>
    
    <script type="text/javascript">

        $(function() {

            var strDisType = $("#hdDisType").val();
            if (strDisType == '2') {
                //預設顯示第一個Tab
                ChangeTab(1);

                //**2011/04/11 Tina：類別為「舊機回收」，頁籤全部隱藏。
                $('ul.tabs li').eq(0).hide(); //Header_一般
                $('ul.tabs li').eq(2).hide(); //費率及申辦類型
                $('ul.tabs li').eq(3).hide(); //指定商品
                $('ul.tabs li').eq(4).hide(); //指定門市
                $('ul.tabs li').eq(5).hide(); //指定促銷
                $('ul.tabs li').eq(6).hide(); //客戶對象_客戶等級
                $('ul.tabs li').eq(7).hide(); //客戶對象_名單
                $('ul.tabs li').eq(8).hide(); //成本中心
            }
            else {
                //預設顯示第一個Tab
                ChangeTab(0);
                $('ul.tabs li').eq(1).hide(); //Header_舊機回收
            }
        });

        //切換Tab
        function ChangeTab(Index) {
            var _showTab = Index;
            $('.abgne_tab').each(function() {
                // 目前的頁籤區塊
                var $tab = $(this);

                $('ul.tabs li', $tab).eq(_showTab).addClass('active');
                $('.tab_content', $tab).hide().eq(_showTab).show();
          
                // 當 li 頁籤被點擊時...
                // 若要改成滑鼠移到 li 頁籤就切換時, 把 click 改成 mouseover
                $('ul.tabs li', $tab).click(function() {
                    // 找出 li 中的超連結 href(#id)
                    var $this = $(this),
                    _clickTab = $this.find('a').attr('href');
                    // 把目前點擊到的 li 頁籤加上 .active
                    // 並把兄弟元素中有 .active 的都移除 class
                    $this.addClass('active').siblings('.active').removeClass('active');
                    // 淡入相對應的內容並隱藏兄弟元素
                    $(_clickTab).stop(false, true).fadeIn().siblings().hide();

                    return false;
                }).find('a').focus(function() {
                    this.blur();
                });
            });
        }
        
        function DisabledAndRunButton(s, e) {
            var file = document.form1.FileUpload.value;
            var reOKFiles = /^([a-zA-Z].*|[1-9].*)\.(xls|XLS)$/;
            if (!(reOKFiles.test(file))) {
                alert("匯入的檔案不正確,須為Excel檔!!");
                var t = document.getElementById('FileUpload');
                t.outerHTML = t.outerHTML;
                e.processOnServer = false;
            }      //return false;
            else {
                if (s.GetEnabled()) {
                    s.SendPostBack('Click');
                    s.SetEnabled(false);
                }
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>      
        <input type="hidden" id="hdUploadBatchNo" runat="server" />
        <input type="hidden" id="hdDisType" runat="server" />

        <div class="criteria">
            <table>
                <tr>
                    <td align="right">
                        <span style="color: Red">*</span>
                        <dx:ASPxLabel ID="Literal1" runat="server" Text="<%$ Resources:WebResources, FilePath %>">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server" Width="450" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnImport" runat="server" Text="<%$ Resources:WebResources, Import %>"
                            OnClick="btnImport_Click">
                            <ClientSideEvents Click="function(s, e) {DisabledAndRunButton(s, e);}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnReset" runat="server" 
                            Text="<%$ Resources:WebResources, Reset %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
        <div class="seperate"></div>
        <div class="abgne_tab">
            <ul class="tabs">
                <%--Header_一般0--%>
                <li><a href="#tab0">Header</a></li>
                <%--Header_舊機回收1--%>
                <li><a href="#tab1">Header</a></li>
                <%--費率及申辦類型2--%>
                <li><a href="#tab2">費率及申辦類型</a></li>
                <%--指定商品3--%>
                <li><a href="#tab3">指定商品</a></li>
                <%--指定門市4--%>
                <li><a href="#tab4">指定門市</a></li>
                <%--指定促銷5--%>
                <li><a href="#tab5">指定促銷</a></li>
                <%--客戶對象_客戶等級6--%>
                <li><a href="#tab6">客戶對象</a></li>
                <%--客戶對象_名單7--%>
                <li><a href="#tab7">名單</a></li>
                <%--成本中心8--%>
                <li><a href="#tab8">成本中心</a></li>
            </ul>
            <div class="tab_container">
                <%--Header_一般0--%>
                <div id="tab0" class="tab_content">
	                <cc:ASPxGridView ID="gvHeader0" ClientInstanceName="gvHeader0" runat="server" 
                        KeyFieldName="SID" Width="100%" onpageindexchanged="gvHeader0_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="序號" />
                            <dx:GridViewDataColumn FieldName="DISCOUNT_NAME" Caption="<%$ Resources:WebResources, DiscountName %>" />
                            <dx:GridViewDataColumn FieldName="DISCOUNT_MONEY" Caption="<%$ Resources:WebResources, DiscountAmount %>" />
                            <dx:GridViewDataColumn FieldName="DISCOUNT_RATE" Caption="<%$ Resources:WebResources, MerchandiseDiscountRate %>" />
                            <dx:GridViewDataColumn FieldName="ACCOUNTCODE" Caption="<%$ Resources:WebResources, AccountingSubject %>" />
                            <dx:GridViewDataColumn FieldName="DIS_USE_COUNTTYPE" Caption="折扣上限次數類型" />
                            <dx:GridViewDataColumn FieldName="DIS_USE_COUNT" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>" />
                            <dx:GridViewDataColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, EffectiveStartDate %>" />
                            <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EffectiveEndDate %>" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView>
                </div>
                <%--Header_舊機回收1--%>
                <div id="tab1" class="tab_content">
	                <cc:ASPxGridView ID="gvHeader1" ClientInstanceName="gvHeader1" runat="server" 
                        KeyFieldName="SID" Width="100%" onpageindexchanged="gvHeader1_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="DISCOUNT_NO" Caption="<%$ Resources:WebResources, PartNumberOfDiscount %>" />
                            <dx:GridViewDataColumn FieldName="DISCOUNT_NAME" Caption="<%$ Resources:WebResources, DiscountName %>" />
                            <dx:GridViewDataColumn FieldName="DISCOUNT_MONEY" Caption="<%$ Resources:WebResources, DiscountAmount %>" />
                            <dx:GridViewDataColumn FieldName="DISCOUNT_RATE" Caption="<%$ Resources:WebResources, MerchandiseDiscountRate %>" />
<%--                            <dx:GridViewDataColumn FieldName="DIS_USE_COUNTTYPE" Caption="折扣上限次數類型" />
                            <dx:GridViewDataColumn FieldName="DIS_USE_COUNT" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>" />
--%>                            <dx:GridViewDataColumn FieldName="S_DATE" Caption="<%$ Resources:WebResources, EffectiveStartDate %>" />
                            <dx:GridViewDataColumn FieldName="E_DATE" Caption="<%$ Resources:WebResources, EffectiveEndDate %>" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView>
                </div>
                <%--費率及申辦類型2--%>
                <div id="tab2" class="tab_content">
	                <cc:ASPxGridView ID="gvRatePlan" ClientInstanceName="gvRatePlan" runat="server" 
                        KeyFieldName="SID" Width="100%" 
                        onpageindexchanged="gvRatePlan_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="序號" />
                            <dx:GridViewDataColumn FieldName="RATES" Caption="<%$ Resources:WebResources, Rates %>" />
                            <dx:GridViewDataColumn FieldName="GA" Caption="GA" />
                            <dx:GridViewDataColumn FieldName="LOYALTY" Caption="Loyalty" />
                            <dx:GridViewDataColumn FieldName="TWOTOTHREE" Caption="2轉3" />
                            <dx:GridViewDataColumn FieldName="MNP" Caption="MNP" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView>
                </div>
                <%--指定商品3--%>
                <div id="tab3" class="tab_content">
	                <cc:ASPxGridView ID="gvProduct" ClientInstanceName="gvProduct" runat="server" 
                        KeyFieldName="SID" Width="100%" onpageindexchanged="gvProduct_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="序號" />
                            <dx:GridViewDataColumn FieldName="PRODNO" Caption="<%$ Resources:WebResources, ProductCode %>" />
                            <dx:GridViewDataColumn FieldName="PRODNAME" Caption="<%$ Resources:WebResources, ProductName %>" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView>                
                </div>
                <%--指定門市4--%>
                <div id="tab4" class="tab_content">
	                <cc:ASPxGridView ID="gvStore" ClientInstanceName="gvStore" runat="server" 
                        KeyFieldName="SID" Width="100%" onpageindexchanged="gvStore_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="序號" />
                            <dx:GridViewDataColumn FieldName="STORE_NO" Caption="<%$ Resources:WebResources, StoreNo %>" />
                            <dx:GridViewDataColumn FieldName="STORENAME" Caption="<%$ Resources:WebResources, StoreName %>" />
                            <dx:GridViewDataColumn FieldName="ZONE_NAME" Caption="<%$ Resources:WebResources, ByDistrict %>" />
                            <dx:GridViewDataColumn FieldName="DIS_USE_COUNT" Caption="<%$ Resources:WebResources, LimitTheNumberDiscount %>" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView> 
                </div>
                <%--指定促銷5--%>
                <div id="tab5" class="tab_content">
	                <cc:ASPxGridView ID="gvPromotion" ClientInstanceName="gvStore" runat="server" 
                        KeyFieldName="SID" Width="100%" 
                        onpageindexchanged="gvPromotion_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="序號" />
                            <dx:GridViewDataColumn FieldName="PROMO_NO" Caption="促銷代號" />
                            <dx:GridViewDataColumn FieldName="PROMO_NAME" Caption="促銷名稱" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView> 
                </div>
                <%--客戶對象_客戶等級6--%>
                <div id="tab6" class="tab_content">
	                <cc:ASPxGridView ID="gvCustomer" ClientInstanceName="gvCustomer" runat="server" 
                        KeyFieldName="SID" Width="100%" 
                        onpageindexchanged="gvCustomer_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="序號" />
                            <dx:GridViewDataColumn FieldName="ARPB_S" Caption="ARPB金額(起)" />
                            <dx:GridViewDataColumn FieldName="ARPB_E" Caption="ARPB金額(訖)" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView> 
                </div>
                <%--客戶對象_名單7--%>
                <div id="tab7" class="tab_content">
	                <cc:ASPxGridView ID="gvList" ClientInstanceName="gvList" runat="server" 
                        KeyFieldName="SID" Width="100%" onpageindexchanged="gvList_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="序號" />
                            <dx:GridViewDataColumn FieldName="MSISDN" Caption="客戶門號" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView> 
                </div>
                <%--成本中心8--%>
                <div id="tab8" class="tab_content">
	                <cc:ASPxGridView ID="gvCCD" ClientInstanceName="gvCCD" runat="server" 
                        KeyFieldName="SID" Width="100%" onpageindexchanged="gvCCD_PageIndexChanged">
                        <Columns>
                            <dx:GridViewDataColumn FieldName="NO" Caption="序號" />
                            <dx:GridViewDataColumn FieldName="COSTCENTERNO" Caption="<%$ Resources:WebResources, CostCenter %>" />
                            <dx:GridViewDataColumn FieldName="PROD_CATEG" Caption="<%$ Resources:WebResources, ProductCategory1 %>" />
                            <dx:GridViewDataColumn FieldName="ACCOUNTCODE" Caption="<%$ Resources:WebResources, AccountingSubject %>" />
                            <dx:GridViewDataColumn FieldName="AMT" Caption="<%$ Resources:WebResources, Amount %>" />
                            <dx:GridViewDataColumn FieldName="REMARK" Caption="<%$ Resources:WebResources, Remark %>" />
                            <dx:GridViewDataColumn FieldName="RESULT" Caption="<%$ Resources:WebResources, ErrorDescription %>" CellStyle-ForeColor="Red" />
                        </Columns>
                        <SettingsText EmptyDataRow="<%$ Resources:WebResources, NoResultsToDisplay %>" />
                        <SettingsPager PageSize="10"></SettingsPager>
                    </cc:ASPxGridView>   
                </div>
            </div>
        </div>
        <div class="seperate"></div>
        <div class="btnPosition">
            <table align="center">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnCommit" runat="server" Text="<%$ Resources:WebResources, CommitUpload %>"
                            OnClick="btnCommit_Click" />
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>">
                            <ClientSideEvents Click="function(s, e) { hidePopupWindow(); }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>

    </div>
    </form>
</body>
</html>
