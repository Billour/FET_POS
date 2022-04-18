<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DIS01.aspx.cs" Inherits="VSS_DIS_DIS01" %>

<%@ Register Src="~/Controls/DISItemChargesAndApply.ascx" TagName="DISItemChargesAndApply" TagPrefix="uc1" %>
<%@ Register Src="DIS01_01_PRODUCT_DISCOUNT.ascx" TagName="PRODUCT_DISCOUNT" TagPrefix="uc2" %>
<%@ Register Src="DIS01_02_STORE_DISCOUNT.ascx" TagName="STORE_DISCOUNT" TagPrefix="uc3" %>
<%@ Register Src="DIS01_03_PROMOTION_DISCOUNT.ascx" TagName="PROMOTION_DISCOUNT" TagPrefix="uc4" %>
<%@ Register Src="DIS01_04_CUST_LEVE_DISCOUNT.ascx" TagName="CUST_LEVE_DISCOUNT" TagPrefix="uc5" %>
<%@ Register Src="DIS01_05_COST_CENTER_DISCOUNT.ascx" TagName="COST_CENTER_DISCOUNT" TagPrefix="uc6" %>
<%@ Register Src="DIS01_06_GIFT_DISCOUNT.ascx" TagName="GIFT_DISCOUNT" TagPrefix="uc7" %>
<%@ Register Src="DIS01_07_ADD_IN_PROD_DISCOUNT.ascx" TagName="ADD_IN_PROD_DISCOUNT" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../../../ClientUtility/jquery.js" type="text/javascript"></script>
    <script src="../../../ClientUtility/Common.js" type="text/javascript"></script>
    <script src="DIS01_COMMON_EVENTS.js" type="text/javascript"></script>

    <style type="text/css">
        ul, li {
	        margin: 0;
	        padding: 0;
	        list-style: none;
        }
        .abgne_tab {
	        clear: left;
	        width:  auto;
	        margin: 10px 0;
        }
        ul.tabs {
	        width: 100%;
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
	        width: 100%;
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

    <script language="javascript" type="text/javascript">
        $(function() {
            $(".abgne_tab").show();
            $(".divImport").hide();

            var strDisType = cbDisType.GetValue();

            if (strDisType == "網購折扣") {
                //**2011/05/04 Tina：類別為「網購折扣」時，只有「成本中心」頁籤可以檢視。

                //預設顯示成中心中Tab
                ChangeTab(5);

                $('ul.tabs li').eq(0).attr("disabled", true); //費率及申辦類型
                $('ul.tabs li').eq(1).attr("disabled", true); //指定商品
                $('ul.tabs li').eq(2).attr("disabled", true); //指定門市
                $('ul.tabs li').eq(3).attr("disabled", true); //指定促銷
                $('ul.tabs li').eq(4).attr("disabled", true); //客戶對象
                $('ul.tabs li').eq(6).attr("disabled", true); //贈品設定
                $('ul.tabs li').eq(7).attr("disabled", true); //加價購

            }
            else {
                //預設顯示第一個Tab
                ChangeTab(0);

                if (strDisType == "1") {
                    //**2011/04/07 Tina：選擇「一般」時，【贈品設定】和【加價購】頁籤須關閉。
                    //$('ul.tabs li').eq(6).hide(); //贈品設定
                    //$('ul.tabs li').eq(7).hide(); //加價購
                    $('ul.tabs li').eq(6).attr("disabled", true); //贈品設定
                    $('ul.tabs li').eq(7).attr("disabled", true); //加價購
                    $(".divImport").show();       //大量匯入按鈕
                    var boolDisCode = txtDisCode.GetEnabled(); //是否是由「查詢修改」來的？ Enabled = ture 表示【否】，false 表示【是】
                    if (boolDisCode) {
                        PageMethods.getDisCountCode(onSuccess);
                    }
                }
                else {
                    //strDisType in ("2", "3", "4", "5")
                    if (strDisType in { 2: 1, 3: 1, 4: 1, 5: 1 }) {
                        //**2011/04/11 Tina：類別為「舊機回收」、「租賃」、「特殊折扣」時，頁籤全部隱藏。
                        //**2011/04/14 Tina：SA文件->1.6.1.3 選取類別為「HapptGo折扣」，頁籤全部為Disable，使用者不可輸入。
                        $(".abgne_tab").hide();
                        if (strDisType == "2") {
                            $(".divImport").show(); //大量匯入按鈕
                        }
                    }
                }

                cbDisType_ValueChanged();


                if (hdSTATUS_DATE.GetText() == "有效") {
                    cbDisType.SetEnabled(false);
                    txtDisName.SetEnabled(false);
                    txtDisAmt.SetEnabled(false);
                    txtDisRate.SetEnabled(false);
                    txtAcct1.SetEnabled(false);
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false);
                    txtLTNDis.SetEnabled(false);
                    SupportStartDateFrom.SetEnabled(false);
                    btnDelete.SetEnabled(false);
                }
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

                    if ($('ul.tabs li', $tab).eq(4)) {  //客戶對像
                        rbCustomer_ValueChanged();
                    }
                    
                    return false;
                }).find('a').focus(function() {
                    this.blur();
                });
            });
        }

        //[類別]切換時，頁籤部分須隱藏
        function cbDisType_ValueChanged() {

            var DisType = cbDisType.GetValue();

            $(".abgne_tab").show();
            $(".divImport").hide();  //大量匯入按鈕

            if (DisType == "6" || DisType == "7") {
                if (gvCCD.pageRowCount > 1) {
                    alert("【加價購】和【贈品設定】只能有一個成本中心");
                    cbDisType.Focus();
                    cbDisType.SetValue(cbDisType.lastChangedValue);
                }
            }

            DisType = cbDisType.GetValue();

            cbLimitTNDis.SetEnabled(true);
            txtLTNDis.SetEnabled(false)         //折扣上限次數輸入欄位
            txtDisAmt.SetEnabled(true);         //折扣金額
            txtDisRate.SetEnabled(true);        //商品折扣比率
            txtAcct1.SetEnabled(true);          //會計科目
            txtAcct2.SetEnabled(true);
            txtAcct3.SetEnabled(true);
            txtAcct4.SetEnabled(true);
            txtAcct5.SetEnabled(true);
            txtAcct6.SetEnabled(true);
            if (typeof (btnCCDAdd) != "undefined") btnCCDAdd.SetEnabled(true);         //【成本中心】頁籤的新增按鈕 

            var boolDisCode = txtDisCode.GetEnabled(); //是否是由「查詢修改」來的？ Enabled = ture 表示【否】，false 表示【是】
            if (boolDisCode) {
                //清空欄位值
                txtDisCode.SetText(null); //折扣料號
            }

            if (cbLimitTNDis.GetValue() != "1") {
                txtLTNDis.SetEnabled(true); //折扣上線次數
            }

            $('.abgne_tab').each(function() {
                // 目前的頁籤區塊
                var $tab = $(this);
                $('ul.tabs li', $tab).show();
                $('ul.tabs li', $tab).removeAttr("disabled"); 
                $('ul.tabs li', $tab).removeClass('active');
            });

            //cbDisType.GetSelectedIndex()
            switch (DisType) {
                case "1":  //一般
                    //**2011/04/07 Tina：選擇「一般」時，【贈品設定】和【加價購】頁籤須關閉。
                    ChangeTab(0);
                    $('ul.tabs li').eq(6).attr("disabled", true); //贈品設定
                    $('ul.tabs li').eq(7).attr("disabled", true); //加價購
                    $(".divImport").show();       //大量匯入按鈕

                    //**2011/05/04 Tina：類別為「一般」時，會計科目不允許輸入
                    txtAcct1.SetText(null);
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtAcct1.SetEnabled(false);   //會計科目
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    
                    //折扣料號自動給值
                    if (boolDisCode) {
                        PageMethods.getDisCountCode(onSuccess);
                    }
                    break;
                case "6":  //贈品設定
                    //**2011/04/07 Tina：選擇「贈品設定」時，【加價購】頁籤須關閉，且【折扣金額】【商品折扣比率】【會計科目】以及【折扣上限次數】皆反灰。
                    ChangeTab(0);
                   // $('ul.tabs li').eq(7).hide(); //加價購
                    $('ul.tabs li').eq(7).attr("disabled", true); //加價購

                    txtDisAmt.SetText(null);  //折扣金額
                    txtDisRate.SetText(null); //商品折扣比率
                    txtAcct1.SetText(null);   //會計科目
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtLTNDis.SetText(null);      //折扣上限次數輸入欄位
                    cbLimitTNDis.SetValue("1");   //折扣上限次數下拉
                    
                    txtDisAmt.SetEnabled(false);  //折扣金額
                    txtDisRate.SetEnabled(false); //商品折扣比率
                    txtAcct1.SetEnabled(false);   //會計科目
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false); //折扣上限次數下拉選單
                    
                    //**2011/04/08 Tina：【加價購】和【贈品設定】只能有一個成本中心
                    if (gvCCD.pageRowCount >= 1) {
                        if (typeof (btnCCDAdd) != "undefined") btnCCDAdd.SetEnabled(false);
                    }
                    break;
                case "7":  //加價購
                    //**2011/04/07 Tina：選擇「加價購」時，【贈品設定】頁籤須關閉，且【折扣金額】【商品折扣比率】【會計科目】以及【折扣上限次數】皆反灰。
                    ChangeTab(0);
                    //$('ul.tabs li').eq(6).hide(); //贈品設定
                    $('ul.tabs li').eq(6).attr("disabled", true); //贈品設定

                    txtDisAmt.SetText(null);  //折扣金額
                    txtDisRate.SetText(null); //商品折扣比率
                    txtAcct1.SetText(null);   //會計科目
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtLTNDis.SetText(null);      //折扣上限次數輸入欄位
                    cbLimitTNDis.SetValue("1");   //折扣上限次數下拉

                    txtDisAmt.SetEnabled(false);  //折扣金額
                    txtDisRate.SetEnabled(false); //商品折扣比率
                    txtAcct1.SetEnabled(false);   //會計科目
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false); //折扣上限次數下拉選單
                    
                    //**2011/04/08 Tina：【加價購】和【贈品設定】只能有一個成本中心
                    if (gvCCD.pageRowCount >= 1) {
                        if (typeof (btnCCDAdd) != "undefined") btnCCDAdd.SetEnabled(false);
                    }
                    break;
                case "2":  //舊機回收
                    //**2011/4/8 Tina：【Happy Go】及【舊機回收】的會計科目是固定的
                    PageMethods.getAccountCodeBySYSPARA("ACCOUNTCODE_OLDPHNOE", onSuccess);

                    txtAcct1.SetText(null);   //會計科目
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtLTNDis.SetText(null);      //折扣上限次數輸入欄位
                    cbLimitTNDis.SetValue("1");   //折扣上限次數下拉
                    
                    txtAcct1.SetEnabled(false);   //會計科目
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false); //折扣上限次數下拉選單
                    
                    ChangeTab(0);
                    $(".divImport").show(); //大量匯入按鈕
                    //**2011/04/11 Tina：類別為「舊機回收」、「租賃」、「特殊折扣」時，頁籤全部隱藏。
                    $(".abgne_tab").hide();
                    break;
                case "3":  //租賃
                    cbLimitTNDis.SetEnabled(false); //折扣上限次數下拉選單
                    ChangeTab(0);
                    //**2011/04/11 Tina：類別為「舊機回收」、「租賃」、「特殊折扣」時，頁籤全部隱藏。
                    $(".abgne_tab").hide();
                    break;
                case "4":  //特殊折扣
                    cbLimitTNDis.SetEnabled(false); //折扣上限次數下拉選單
                    ChangeTab(0);
                    //**2011/04/11 Tina：類別為「舊機回收」、「租賃」、「特殊折扣」時，頁籤全部隱藏。
                    $(".abgne_tab").hide();
                    break;
                case "5":  //HappyGo折扣
                    //**2011/4/8 Tina：【Happy Go】及【舊機回收】的會計科目是固定的
                    PageMethods.getAccountCodeBySYSPARA("ACCOUNTCODE_HAPPYGO", onSuccess);

                    txtDisRate.SetText(null); //商品折扣比率
                    txtAcct1.SetText(null);   //會計科目
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtLTNDis.SetText(null);      //折扣上限次數輸入欄位
                    cbLimitTNDis.SetValue("1");   //折扣上限次數下拉
                                      
                    txtDisRate.SetEnabled(false);   //商品折扣比率
                    txtAcct1.SetEnabled(false);     //會計科目
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false); //折扣上限次數下拉選單
                    
                    ChangeTab(0);
                    //**2011/04/14 Tina：SA文件->1.6.1.3 選取類別為「HapptGo折扣」，頁籤全部為Disable，使用者不可輸入。
                    $(".abgne_tab").hide();
                    break;
                default:
                    ChangeTab(0);
                    break;
            }

        }
        
        //存檔檢查
        function checkForm(s, e) {
            var DisTypeValue = cbDisType.GetValue();
            var s1 = txtDisAmt.GetText();
            var s2 = txtDisRate.GetText();

            //**「類別」為【加價購】和【贈品設定】者，【折扣金額】與【商品折扣比率】會反灰，所以不需檢查是否有填寫。
            //**「類別」為【HappyGo折扣】者，【商品折扣比率】會反灰，所以【折扣金額】是必填欄位。
            //**【折扣金額】與【商品折扣比率】須擇一填寫(只允許其一欄有值)。
            switch (DisTypeValue) {
                case "5":   //HappyGo折扣
                    if (s1 == '') {
                        ShowMessage('【折扣金額】為必填欄位!!');
                        e.processOnServer = false;
                        return false;
                    }
                    break;
                case "6":  //加價購
                case "7":  //贈品設定
                    break;
                default:
                    if (s1 == '' && s2 == '') {
                        ShowMessage('【折扣金額】與【商品折扣比率】一定要填寫一欄!!');
                        e.processOnServer = false;
                        return false;
                    }
                    else if (s1 != '' && s2 != '') {
                        ShowMessage('【折扣金額】與【商品折扣比率】只允許其一欄位有值!!');
                        e.processOnServer = false;
                        return false;
                    }
                    break;
            }


            //**「類別」為【加價購】和【贈品設定】者，【折扣上限次數】會反灰，所以不需檢查是否有填寫。
            //**折扣上限次數DropDownList有選取(並非"無")，則折扣上限次數TextBox為必填欄位。
            if (cbLimitTNDis.GetValue() != "1") {
                if (txtLTNDis.GetText() == "") {
                    ShowMessage("折扣上限次數未填!!");
                    txtLTNDis.Focus();
                    e.processOnServer = false;
                    return false;
                }
            }

            Loading("存檔中...");
        }

        function onOK() {
            var boolDisCode = txtDisCode.GetEnabled(); //是否是由「查詢修改」來的？ Enabled = ture 表示【否】，false 表示【是】
            var DisType = cbDisType.GetValue();
            if (DisType == "1")  //一般
            {
                //折扣料號自動給值
                if (boolDisCode) {
                    PageMethods.getDisCountCode(onSuccess);
                }
            }
        }
        function onOK1() {
            ac1.PerformCallback();
        }
        function onOK2() {
            ac2.PerformCallback(0);
        }
        function onOK3() {
            ac3.PerformCallback(0);
        }
        function onOK4() {
            ac4.PerformCallback(0);
        }
        function onOK5() {
            ac5.PerformCallback(0);
        }
        function onOK6() {
            ac6.PerformCallback(0);
        }
        function onOK7() {
            ac7.PerformCallback(0);
        }

        //折扣金額輸入檢查
        function checkDiscountAmt(s, e) {
            var DisTypeValue = cbDisType.GetValue();
            var s1 = txtDisAmt.GetText();
            var s2 = txtDisRate.GetText();

            //**「類別」為【加價購】和【贈品設定】者，【折扣金額】與【商品折扣比率】會反灰，所以不需檢查是否有填寫。
            //**【折扣金額】與【商品折扣比率】須擇一填寫(只允許其一欄有值)。
            if (DisTypeValue != "6" && DisTypeValue != "7") {
                var s1 = txtDisAmt.GetText();
                var s2 = txtDisRate.GetText();

                if (s1 == '' && s2 == '') {
                    s.SetText('');
                    alert('【折扣金額】與【商品折扣比率】一定要填寫一欄!!');
                    e.processOnServer = false;
                }
                else if (s1 != '' && s2 != '') {
                    s.SetText('');
                    alert('【折扣金額】與【商品折扣比率】只允許其一欄位有值!!');
                    e.processOnServer = false;
                }
            }
        }

        //商品折扣比率輸入檢查
        function checkDiscountRate(s, e) {
            var DisTypeValue = cbDisType.GetValue();
            var s1 = txtDisAmt.GetText();
            var s2 = txtDisRate.GetText();
            var chk = true;
            //**「類別」為【加價購】和【贈品設定】者，【折扣金額】與【商品折扣比率】會反灰，所以不需檢查是否有填寫。
            //**【折扣金額】與【商品折扣比率】須擇一填寫(只允許其一欄有值)。
            if (DisTypeValue != "6" && DisTypeValue != "7") {
                var s1 = txtDisAmt.GetText();
                var s2 = txtDisRate.GetText();

                if (s1 == '' && s2 == '') {
                    alert('【折扣金額】與【商品折扣比率】一定要填寫一欄!!');
                    e.processOnServer = false;
                    chk = false;
                }
                else if (s1 != '' && s2 != '') {
                    s.SetText('');
                    alert('【折扣金額】與【商品折扣比率】只允許其一欄位有值!!');
                    e.processOnServer = false;
                    chk = false;
                }
            }

            if (chk) {
                var tmpDisRate = txtDisRate.GetText();

                if (tmpDisRate != '') {
                    if (tmpDisRate > 100) {
                        txtDisRate.SetText(null);
                        alert("【商品折扣比率】不可超過100!!");
                        e.processOnServer = false;
                    }

                }
            }
        }

        //由【成本中心】的商品分類取得成本中心代號和會計科目
        function getAccountInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getAccountInfo(s.GetValue(), onSuccess);
        }

        //由【成本中心】的成本中心代號取得商品分類
        function getProd_CategInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getProd_CategInfo(s.GetValue(), onSuccess);
        }
        
        function onSuccess(returnData, userContext, methodName) {
            switch (methodName) {
                case "getAccountInfo":  //由【成本中心】的商品分類取得成本中心代號和會計科目
                    if (returnData != '') {
                        var Values = returnData.split(',');
                        txtCCDNo.SetValue(Values[0]);     //成本中心
                        ACCOUNTCODE.SetValue(Values[1]);  //會計科目
                        ACCOUNTCODE.SetEnabled(false);
                    }
                    else {
                        ACCOUNTCODE.SetValue(null);
                        ACCOUNTCODE.SetEnabled(true);
                    }
                    break;
                case "getProd_CategInfo":  //由【成本中心】的成本中心代號取得商品分類
                    if (returnData == 'NoFound') {
                        txtCCDNo.SetValue(null);
                        ACCOUNTCODE.SetValue(null);
                        ACCOUNTCODE.SetEnabled(true);
                        alert("成本中心不存在");
                    }
                    else if (returnData == 'NoItems') {
                        cbPROD_CATEG.ClearItems();
                        cbPROD_CATEG.SetValue("Other");
                        ACCOUNTCODE.SetValue(null);
                        ACCOUNTCODE.SetEnabled(true);
                    }
                    else {
                        cbPROD_CATEG.ClearItems();
                        var Prod_Categ = cbPROD_CATEG.GetValue();
                        var Datas = returnData.split(',');
                        var Items = "";
                        if (Datas.length > 0) {
                            for (var i = 0; i < Datas.length; i++) {
                                if (Datas[i] == "") { continue; }
                                Items = Datas[i].split('|');
                                cbPROD_CATEG.AddItem(Items[0], Items[1]);
                            }
                        }
                        if (Prod_Categ != null) {
                            cbPROD_CATEG.SetValue(Prod_Categ);
                        }
                        else {
                            ACCOUNTCODE.SetValue(null);  //會計科目
                            ACCOUNTCODE.SetEnabled(false);
                        }
                    }
                    break;
                case "getDisCountCode":  //【類別】選取「一般」時，系統自動取得折扣料號
                    if (returnData != '') {
                        txtDisCode.SetText(returnData);
                    }
                    else {
                        txtDisCode.SetText(null);
                    }
                    break;
                case "getAccountCodeBySYSPARA": //【類別】選取「舊機回收」或「Happy Go」時，系統自動代出固定的會計科目
                    if (returnData != '') {
                        txtAcct1.SetText(returnData.substr(0, 2));
                        txtAcct2.SetText(returnData.substr(2, 3));
                        txtAcct3.SetText(returnData.substr(5, 4));
                        txtAcct4.SetText(returnData.substr(9, 6));
                        txtAcct5.SetText(returnData.substr(15, 4));
                        txtAcct6.SetText(returnData.substr(19, 4));
                    }
                    else {
                        txtAcct1.SetText(null);
                        txtAcct2.SetText(null);
                        txtAcct3.SetText(null);
                        txtAcct4.SetText(null);
                        txtAcct5.SetText(null);
                        txtAcct6.SetText(null);
                    }
                    break;
            }

        }
    
        function changeIndex(s, e) {
            txtLTNDis.SetEnabled(true);
            showBtnTimes(0);
            switch (s.GetSelectedIndex()) {
                case 0:
                    txtLTNDis.SetText('');
                    txtLTNDis.SetEnabled(false);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        function InitDisUseCount() {
            showBtnTimes(0);
        }

        //顯示 or 隱藏 【均分次數】此按鈕
        function showBtnTimes(index) {

            if (typeof (btnTimes) != "undefined") {
                switch (cbLimitTNDis.GetValue()) {
                    case "1":  //無
                        btnTimes.SetVisible(false);
                        lblRemainingTimes.SetVisible(false);
                        if (gvStore.IsEditing() == true) {
                            DisUseCount.SetEnabled(false);
                            if (DisUseCount.GetText() == '') DisUseCount.SetText('0');
                        }
                        break;
                    case "2":  //指定
                        btnTimes.SetVisible(false);
                        lblRemainingTimes.SetVisible(false);
                        if (gvStore.IsEditing() == true) {
                            DisUseCount.SetEnabled(true);
                            if (DisUseCount.GetText() == '') DisUseCount.SetText(txtLTNDis.GetText());
                        }
                        break;
                    case "4":  //均分
                        btnTimes.SetVisible(true);
                        lblRemainingTimes.SetVisible(true);
                        if (gvStore.IsEditing() == true) {
                            DisUseCount.SetEnabled(false);
                            if (DisUseCount.GetText() == '') DisUseCount.SetText('0');
                        }
                        break;
                    case "3":  //總量
                        btnTimes.SetVisible(false);
                        lblRemainingTimes.SetVisible(false);
                        if (gvStore.IsEditing() == true) {
                            DisUseCount.SetEnabled(true);
                            DisUseCount.SetText(null);
                        }
                        break;
                }
            }
        }
       
         function getPRODINFO(s, e, p_name) {
            this.s = s;
            this.EventArgs = e;
            this.Sender = s;
            this.p_name = p_name;
            if (s.GetText() != '')
                PageMethods.getPRODINFOExtraSale(Sender.GetText(), getPRODINFO_OnOK);
        }

        function getPRODINFO_OnOK(returnData) {
            var p_name_data = null;
            var p_price_data = null;
            if (returnData == '') {
                EventArgs.processOnServer = false;
                alert("商品料號不存在!");
                
                Sender.Focus();
                Sender.SetValue(null);             
            }
            else {
                if (returnData == "fail") {
                    EventArgs.processOnServer = false;
                    alert("商品料號不允許設定!");

                    Sender.Focus();
                    Sender.SetValue(null);
                }
                else {
                    //**2011/04/01 Tina：[加價購]在查詢商品料號名稱時，也要帶出單機售價。
                    var values = returnData.split(';');
                    p_name_data = values[0];
                    p_price_data = values[1];
                    //p_name_data = returnData;
                }

            }
            switch (p_name) {
                case "PRODNAME":
                    PRODNAME.SetText(p_name_data);
                    break;
                case "PRODNAME1":
                    PRODNAME1.SetText(p_name_data);
                    break;
                case "PRODNAME2":
                    PRODNAME2.SetText(p_name_data);
                    UNIT_PRICE.SetText(p_price_data);
                    break;
            }
        }

        function ModifyImportPopupURL() {
            var url = PopupImport.GetContentUrl();
            var s = url.split('?');
            if (s.length > 1) {
                var ordDisType = s[1];
                var newDisType = "cbDisType=" + cbDisType.GetValue();
                url = url.replace(ordDisType, newDisType);
            }

            //**2011/04/25 Tina：將URL傳遞的參數加密。
            ModifyPopupURLByEncrypt(url, PopupImport);
            //PopupImport.SetContentUrl(url);
            event.returnValue = false;
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <div class="titlef">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left">
                    <!--折扣設定作業-->
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:WebResources, DiscountSetOperations %>"></asp:Literal>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="Button3" runat="server" Text="<%$ Resources:WebResources, QueryEdit %>"
                        AutoPostBack="false" CausesValidation="false">
                        <ClientSideEvents Click="function(s, e){ document.location='DIS02.aspx'; }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </div>
        
    <div class="criteria">
        <table>
            <tr>
                <td width="80px" align="right">
                    <span style="color: Red">*</span>
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="<%$ Resources:WebResources, Category %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxComboBox ID="cbDisType" runat="server" ValueType="System.String" Width="170px"
                        SelectedIndex="0" ClientInstanceName="cbDisType">
                        <ClientSideEvents ValueChanged="function(s,e) { cbDisType_ValueChanged(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td>
                    <div class="divImport">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnTemplate" runat="server" Text="Template" Width="100px" 
                                        CausesValidation="false" OnClick="btnTemplate_Click">
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                   <dx:ASPxButton ID="btnImport" runat="server" Text="大量上傳" CausesValidation="false" AutoPostBack="false"  Width="100px">
                                        <ClientSideEvents Click="function(s,e) { ModifyImportPopupURL(); }" />
                                   </dx:ASPxButton>
                                    <cc:ASPxPopupControl ID="ASPxPopupControl" ClientInstanceName="PopupImport" runat="server" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/DIS/DIS01/DIS01_Import_byDisType.aspx?cbDisType=aa"
                                        Width="640" Height="400" LoadingPanelID="lp" HeaderText="大量上傳" onOKScript="onOK">
                                        <ContentStyle>
                                            <Paddings Padding="4px"></Paddings>
                                        </ContentStyle>
                                    </cc:ASPxPopupControl>
                                    <dx:ASPxLoadingPanel ID="lp" runat="server">
                                    </dx:ASPxLoadingPanel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </td>
                <td width="120px" align="right">
                   
                    &nbsp;
                </td>
                <td width="170px" align="left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="80px" align="right">
                    &nbsp;
                </td>
                <td width="120px" align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="80px" align="right">
                    <span style="color: Red">*</span>
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="<%$ Resources:WebResources, PartNumberOfDiscount %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="txtDisCode" runat="server" Width="170px" MaxLength="8" ClientInstanceName="txtDisCode">
                        <ValidationSettings SetFocusOnError="true">
                            <RegularExpression ValidationExpression="^\d{8}$" ErrorText="格式長度不正確" />
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    <span style="color: Red">*</span>
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="<%$ Resources:WebResources, DiscountName %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="txtDisName" runat="server" Width="170px" ClientInstanceName="txtDisName">
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="必填欄位" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="<%$ Resources:WebResources, Status %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="120px" align="left">
                    <dx:ASPxLabel ID="lblStatus" runat="server" Text="">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td width="80px" align="right">
                    <span style="color: Red">*</span>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="<%$ Resources:WebResources, DiscountAmount %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="txtDisAmt" runat="server" Width="170px" ClientInstanceName="txtDisAmt">
                        <ValidationSettings SetFocusOnError="true">
                            <RegularExpression ValidationExpression="-\d*" ErrorText="格式不正確,請以負值輸入與不可輸入小數點" />
                        </ValidationSettings>
                        <ClientSideEvents TextChanged="function(s,e){checkDiscountAmt(s,e);}" />
                    </dx:ASPxTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    <span style="color: Red">*</span>
                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="<%$ Resources:WebResources, MerchandiseDiscountRate %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="txtDisRate" runat="server" Width="170px" ClientInstanceName="txtDisRate">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="\d*" ErrorText="格式不正確" />
                                    </ValidationSettings>
                                    <ClientSideEvents TextChanged="function(s,e){checkDiscountRate(s,e);}" />
                                </dx:ASPxTextBox>
                                <div style="color: Red; font-size:smaller">例如：20%代表八折</div>
                            </td>
                            <td>
                                % <br />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="<%$ Resources:WebResources, Date %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="120px" align="left">
                    <dx:ASPxLabel ID="lblDate" runat="server" Text="">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td width="80px" align="right">
                    <span style="color: Red">*</span>
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="<%$ Resources:WebResources, AccountingSubject %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct1" runat="server" Width="25" MaxLength="2" ClientInstanceName="txtAcct1">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{2}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct2" runat="server" Width="30" MaxLength="3" ClientInstanceName="txtAcct2">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{3}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct3" runat="server" Width="40" MaxLength="4" ClientInstanceName="txtAcct3">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{4}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct4" runat="server" Width="55" MaxLength="6" ClientInstanceName="txtAcct4">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{6}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct5" runat="server" Width="40" MaxLength="4" ClientInstanceName="txtAcct5">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{4}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct6" runat="server" Width="40" MaxLength="4" ClientInstanceName="txtAcct6">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{4}$" ErrorText="請勿輸入非數值或格式長度不符" />
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    <!--折扣上限次數-->
                    <asp:Literal ID="LimitTheNumberDiscount" runat="server" Text="<%$ Resources:WebResources, LimitTheNumberDiscount %>"></asp:Literal>：
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="cbLimitTNDis" runat="server" SelectedIndex="1" ValueType="System.String"
                                    Width="60px" ClientInstanceName="cbLimitTNDis">
                                    <Items>
                                        <dx:ListEditItem Text="無" Value="1" />
                                        <dx:ListEditItem Text="指定" Value="2" />
                                        <dx:ListEditItem Text="總量" Value="3" />
                                        <dx:ListEditItem Text="均分" Value="4" />
                                    </Items>
                                    <ClientSideEvents SelectedIndexChanged="
                                        function(s,e){
                                           changeIndex(s,e);
                                        }
                                        " />
                                </dx:ASPxComboBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtLTNDis" runat="server" Width="100px" ClientInstanceName="txtLTNDis">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="\d*" ErrorText="格式不符" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="<%$ Resources:WebResources, Staff %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td width="120px" align="left">
                    <dx:ASPxLabel ID="lblStaff" runat="server" Text="">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="<%$ Resources:WebResources, EffectiveDuration %>">
                    </dx:ASPxLabel>
                    ：
                </td>
                <td align="left">
                    <table width="100%">
                        <tr>
                            <td>
                                <span style="color: Red">*</span>
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:WebResources, Start %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="SupportStartDateFrom" runat="server" ClientInstanceName="SupportStartDateFrom" EditFormatString="yyyy/MM/dd">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RequiredField IsRequired="true" ErrorText="必填欄位" />
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s,e) { chkDateS(e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                            <td>
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:WebResources, End %>"></asp:Literal>
                            </td>
                            <td>
                                <dx:ASPxDateEdit ID="SupportStartDateTo" runat="server" ClientInstanceName="SupportStartDateTo" EditFormatString="yyyy/MM/dd">
                                    <ValidationSettings SetFocusOnError="true">
                                    </ValidationSettings>
                                    <ClientSideEvents Validation="function(s,e) { chkDateE(e); }" />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="120px" align="right">
                    &nbsp;
                </td>
                <td width="170px" align="left">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="80px" align="right">
                    &nbsp;
                </td>
                <td width="120px" align="left">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    
    <div class="seperate">
        <dx:ASPxTextBox ID="hdSTATUS_DATE" ClientInstanceName="hdSTATUS_DATE" runat="server" ClientVisible="false" ></dx:ASPxTextBox>
    </div>                   
    
    <div class="abgne_tab" style="width: 98%">
        <ul class="tabs">
            <%--費率及申辦類型1--%>
            <li><a href="#tab1">費率及申辦類型</a></li>
            <%--指定商品2--%>
            <li><a href="#tab2">指定商品</a></li>
            <%--指定門市3--%>
            <li><a href="#tab3">指定門市</a></li>
            <%--指定促銷4--%>
            <li><a href="#tab4">指定促銷</a></li>
            <%--客戶對象5--%>
            <li><a href="#tab5">客戶對象</a></li>
            <%--成本中心6--%>
            <li><a href="#tab6">成本中心</a></li>
            <%--贈品設定7--%>
            <li><a href="#tab7">贈品設定</a></li>
            <%--加價購8--%>
            <li><a href="#tab8">加價購</a></li>
        </ul>
        <div class="tab_container">
            <%--費率及申辦類型1--%>
            <div id="tab1" class="tab_content">
	            <uc1:DISItemChargesAndApply ID="DISItemChargesAndApply1" runat="server" />
            </div>
            <%--指定商品2--%>
            <div id="tab2" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
	                    <uc2:PRODUCT_DISCOUNT ID="PRODUCT_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--指定門市3--%>
            <div id="tab3" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
	                    <uc3:STORE_DISCOUNT ID="STORE_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--指定促銷4--%>
            <div id="tab4" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
	                    <uc4:PROMOTION_DISCOUNT ID="PROMOTION_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--客戶對象5--%>
            <div id="tab5" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
	                    <uc5:CUST_LEVE_DISCOUNT ID="CUST_LEVE_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--成本中心6--%>
            <div id="tab6" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
	                    <uc6:COST_CENTER_DISCOUNT ID="COST_CENTER_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--贈品設定7--%>
            <div id="tab7" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
	                    <uc7:GIFT_DISCOUNT ID="GIFT_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--加價購8--%>
            <div id="tab8" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
	                    <uc8:ADD_IN_PROD_DISCOUNT ID="ADD_IN_PROD_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

   <div class="seperate"></div>
   
   <asp:UpdatePanel ID="UpdatePanel9" runat="server">
    <ContentTemplate>
       <div class="btnPosition">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" ClientInstanceName="btnSave" runat="server" Text="存檔" AutoPostBack="false" OnClick="btnSave_Click">
                            <ClientSideEvents Click="function(s,e){ if(ASPxClientEdit.ValidateEditorsInContainer(null)) { checkForm(s,e); }}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Text="<%$ Resources:WebResources, Cancel %>" SkinID="ResetButton">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnDelete" runat="server" Text="<%$ Resources:WebResources, Delete %>" ClientInstanceName="btnDelete"
                            OnClick="btnDelete_Click">
                            <ClientSideEvents Click="function(s,e){if(!confirm('是否確認刪除'))e.processOnServer = false; return e.processOnServer;} " />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <iframe id="fDownload" style="display: none" src="" runat="server"></iframe>
    
</asp:Content>

