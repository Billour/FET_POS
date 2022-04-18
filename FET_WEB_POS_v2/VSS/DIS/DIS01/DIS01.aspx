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
	        margin-bottom: -1px;	/* �� li ���U���ӾB�� ul ������ border-bottom */
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

            if (strDisType == "���ʧ馩") {
                //**2011/05/04 Tina�G���O���u���ʧ馩�v�ɡA�u���u�������ߡv���ҥi�H�˵��C

                //�w�]��ܦ����ߤ�Tab
                ChangeTab(5);

                $('ul.tabs li').eq(0).attr("disabled", true); //�O�v�Υӿ�����
                $('ul.tabs li').eq(1).attr("disabled", true); //���w�ӫ~
                $('ul.tabs li').eq(2).attr("disabled", true); //���w����
                $('ul.tabs li').eq(3).attr("disabled", true); //���w�P�P
                $('ul.tabs li').eq(4).attr("disabled", true); //�Ȥ��H
                $('ul.tabs li').eq(6).attr("disabled", true); //�ث~�]�w
                $('ul.tabs li').eq(7).attr("disabled", true); //�[����

            }
            else {
                //�w�]��ܲĤ@��Tab
                ChangeTab(0);

                if (strDisType == "1") {
                    //**2011/04/07 Tina�G��ܡu�@��v�ɡA�i�ث~�]�w�j�M�i�[���ʡj���Ҷ������C
                    //$('ul.tabs li').eq(6).hide(); //�ث~�]�w
                    //$('ul.tabs li').eq(7).hide(); //�[����
                    $('ul.tabs li').eq(6).attr("disabled", true); //�ث~�]�w
                    $('ul.tabs li').eq(7).attr("disabled", true); //�[����
                    $(".divImport").show();       //�j�q�פJ���s
                    var boolDisCode = txtDisCode.GetEnabled(); //�O�_�O�ѡu�d�߭ק�v�Ӫ��H Enabled = ture ��ܡi�_�j�Afalse ��ܡi�O�j
                    if (boolDisCode) {
                        PageMethods.getDisCountCode(onSuccess);
                    }
                }
                else {
                    //strDisType in ("2", "3", "4", "5")
                    if (strDisType in { 2: 1, 3: 1, 4: 1, 5: 1 }) {
                        //**2011/04/11 Tina�G���O���u�¾��^���v�B�u����v�B�u�S��馩�v�ɡA���ҥ������áC
                        //**2011/04/14 Tina�GSA���->1.6.1.3 ������O���uHapptGo�馩�v�A���ҥ�����Disable�A�ϥΪ̤��i��J�C
                        $(".abgne_tab").hide();
                        if (strDisType == "2") {
                            $(".divImport").show(); //�j�q�פJ���s
                        }
                    }
                }

                cbDisType_ValueChanged();


                if (hdSTATUS_DATE.GetText() == "����") {
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

        //����Tab
        function ChangeTab(Index) {
            var _showTab = Index;
            $('.abgne_tab').each(function() {
                // �ثe�����Ұ϶�
                var $tab = $(this);

                $('ul.tabs li', $tab).eq(_showTab).addClass('active');
                $('.tab_content', $tab).hide().eq(_showTab).show();

                // �� li ���ҳQ�I����...
                // �Y�n�令�ƹ����� li ���ҴN������, �� click �令 mouseover
                $('ul.tabs li', $tab).click(function() {
                    // ��X li �����W�s�� href(#id)
                    var $this = $(this),
                    _clickTab = $this.find('a').attr('href');
                    // ��ثe�I���쪺 li ���ҥ[�W .active
                    // �ç�S�̤������� .active �������� class
                    $this.addClass('active').siblings('.active').removeClass('active');
                    // �H�J�۹��������e�����åS�̤���
                    $(_clickTab).stop(false, true).fadeIn().siblings().hide();

                    if ($('ul.tabs li', $tab).eq(4)) {  //�Ȥ�ﹳ
                        rbCustomer_ValueChanged();
                    }
                    
                    return false;
                }).find('a').focus(function() {
                    this.blur();
                });
            });
        }

        //[���O]�����ɡA���ҳ���������
        function cbDisType_ValueChanged() {

            var DisType = cbDisType.GetValue();

            $(".abgne_tab").show();
            $(".divImport").hide();  //�j�q�פJ���s

            if (DisType == "6" || DisType == "7") {
                if (gvCCD.pageRowCount > 1) {
                    alert("�i�[���ʡj�M�i�ث~�]�w�j�u�঳�@�Ӧ�������");
                    cbDisType.Focus();
                    cbDisType.SetValue(cbDisType.lastChangedValue);
                }
            }

            DisType = cbDisType.GetValue();

            cbLimitTNDis.SetEnabled(true);
            txtLTNDis.SetEnabled(false)         //�馩�W�����ƿ�J���
            txtDisAmt.SetEnabled(true);         //�馩���B
            txtDisRate.SetEnabled(true);        //�ӫ~�馩��v
            txtAcct1.SetEnabled(true);          //�|�p���
            txtAcct2.SetEnabled(true);
            txtAcct3.SetEnabled(true);
            txtAcct4.SetEnabled(true);
            txtAcct5.SetEnabled(true);
            txtAcct6.SetEnabled(true);
            if (typeof (btnCCDAdd) != "undefined") btnCCDAdd.SetEnabled(true);         //�i�������ߡj���Ҫ��s�W���s 

            var boolDisCode = txtDisCode.GetEnabled(); //�O�_�O�ѡu�d�߭ק�v�Ӫ��H Enabled = ture ��ܡi�_�j�Afalse ��ܡi�O�j
            if (boolDisCode) {
                //�M������
                txtDisCode.SetText(null); //�馩�Ƹ�
            }

            if (cbLimitTNDis.GetValue() != "1") {
                txtLTNDis.SetEnabled(true); //�馩�W�u����
            }

            $('.abgne_tab').each(function() {
                // �ثe�����Ұ϶�
                var $tab = $(this);
                $('ul.tabs li', $tab).show();
                $('ul.tabs li', $tab).removeAttr("disabled"); 
                $('ul.tabs li', $tab).removeClass('active');
            });

            //cbDisType.GetSelectedIndex()
            switch (DisType) {
                case "1":  //�@��
                    //**2011/04/07 Tina�G��ܡu�@��v�ɡA�i�ث~�]�w�j�M�i�[���ʡj���Ҷ������C
                    ChangeTab(0);
                    $('ul.tabs li').eq(6).attr("disabled", true); //�ث~�]�w
                    $('ul.tabs li').eq(7).attr("disabled", true); //�[����
                    $(".divImport").show();       //�j�q�פJ���s

                    //**2011/05/04 Tina�G���O���u�@��v�ɡA�|�p��ؤ����\��J
                    txtAcct1.SetText(null);
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtAcct1.SetEnabled(false);   //�|�p���
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    
                    //�馩�Ƹ��۰ʵ���
                    if (boolDisCode) {
                        PageMethods.getDisCountCode(onSuccess);
                    }
                    break;
                case "6":  //�ث~�]�w
                    //**2011/04/07 Tina�G��ܡu�ث~�]�w�v�ɡA�i�[���ʡj���Ҷ������A�B�i�馩���B�j�i�ӫ~�馩��v�j�i�|�p��ءj�H�Ρi�馩�W�����ơj�ҤϦǡC
                    ChangeTab(0);
                   // $('ul.tabs li').eq(7).hide(); //�[����
                    $('ul.tabs li').eq(7).attr("disabled", true); //�[����

                    txtDisAmt.SetText(null);  //�馩���B
                    txtDisRate.SetText(null); //�ӫ~�馩��v
                    txtAcct1.SetText(null);   //�|�p���
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtLTNDis.SetText(null);      //�馩�W�����ƿ�J���
                    cbLimitTNDis.SetValue("1");   //�馩�W�����ƤU��
                    
                    txtDisAmt.SetEnabled(false);  //�馩���B
                    txtDisRate.SetEnabled(false); //�ӫ~�馩��v
                    txtAcct1.SetEnabled(false);   //�|�p���
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false); //�馩�W�����ƤU�Կ��
                    
                    //**2011/04/08 Tina�G�i�[���ʡj�M�i�ث~�]�w�j�u�঳�@�Ӧ�������
                    if (gvCCD.pageRowCount >= 1) {
                        if (typeof (btnCCDAdd) != "undefined") btnCCDAdd.SetEnabled(false);
                    }
                    break;
                case "7":  //�[����
                    //**2011/04/07 Tina�G��ܡu�[���ʡv�ɡA�i�ث~�]�w�j���Ҷ������A�B�i�馩���B�j�i�ӫ~�馩��v�j�i�|�p��ءj�H�Ρi�馩�W�����ơj�ҤϦǡC
                    ChangeTab(0);
                    //$('ul.tabs li').eq(6).hide(); //�ث~�]�w
                    $('ul.tabs li').eq(6).attr("disabled", true); //�ث~�]�w

                    txtDisAmt.SetText(null);  //�馩���B
                    txtDisRate.SetText(null); //�ӫ~�馩��v
                    txtAcct1.SetText(null);   //�|�p���
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtLTNDis.SetText(null);      //�馩�W�����ƿ�J���
                    cbLimitTNDis.SetValue("1");   //�馩�W�����ƤU��

                    txtDisAmt.SetEnabled(false);  //�馩���B
                    txtDisRate.SetEnabled(false); //�ӫ~�馩��v
                    txtAcct1.SetEnabled(false);   //�|�p���
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false); //�馩�W�����ƤU�Կ��
                    
                    //**2011/04/08 Tina�G�i�[���ʡj�M�i�ث~�]�w�j�u�঳�@�Ӧ�������
                    if (gvCCD.pageRowCount >= 1) {
                        if (typeof (btnCCDAdd) != "undefined") btnCCDAdd.SetEnabled(false);
                    }
                    break;
                case "2":  //�¾��^��
                    //**2011/4/8 Tina�G�iHappy Go�j�Ρi�¾��^���j���|�p��جO�T�w��
                    PageMethods.getAccountCodeBySYSPARA("ACCOUNTCODE_OLDPHNOE", onSuccess);

                    txtAcct1.SetText(null);   //�|�p���
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtLTNDis.SetText(null);      //�馩�W�����ƿ�J���
                    cbLimitTNDis.SetValue("1");   //�馩�W�����ƤU��
                    
                    txtAcct1.SetEnabled(false);   //�|�p���
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false); //�馩�W�����ƤU�Կ��
                    
                    ChangeTab(0);
                    $(".divImport").show(); //�j�q�פJ���s
                    //**2011/04/11 Tina�G���O���u�¾��^���v�B�u����v�B�u�S��馩�v�ɡA���ҥ������áC
                    $(".abgne_tab").hide();
                    break;
                case "3":  //����
                    cbLimitTNDis.SetEnabled(false); //�馩�W�����ƤU�Կ��
                    ChangeTab(0);
                    //**2011/04/11 Tina�G���O���u�¾��^���v�B�u����v�B�u�S��馩�v�ɡA���ҥ������áC
                    $(".abgne_tab").hide();
                    break;
                case "4":  //�S��馩
                    cbLimitTNDis.SetEnabled(false); //�馩�W�����ƤU�Կ��
                    ChangeTab(0);
                    //**2011/04/11 Tina�G���O���u�¾��^���v�B�u����v�B�u�S��馩�v�ɡA���ҥ������áC
                    $(".abgne_tab").hide();
                    break;
                case "5":  //HappyGo�馩
                    //**2011/4/8 Tina�G�iHappy Go�j�Ρi�¾��^���j���|�p��جO�T�w��
                    PageMethods.getAccountCodeBySYSPARA("ACCOUNTCODE_HAPPYGO", onSuccess);

                    txtDisRate.SetText(null); //�ӫ~�馩��v
                    txtAcct1.SetText(null);   //�|�p���
                    txtAcct2.SetText(null);
                    txtAcct3.SetText(null);
                    txtAcct4.SetText(null);
                    txtAcct5.SetText(null);
                    txtAcct6.SetText(null);
                    txtLTNDis.SetText(null);      //�馩�W�����ƿ�J���
                    cbLimitTNDis.SetValue("1");   //�馩�W�����ƤU��
                                      
                    txtDisRate.SetEnabled(false);   //�ӫ~�馩��v
                    txtAcct1.SetEnabled(false);     //�|�p���
                    txtAcct2.SetEnabled(false);
                    txtAcct3.SetEnabled(false);
                    txtAcct4.SetEnabled(false);
                    txtAcct5.SetEnabled(false);
                    txtAcct6.SetEnabled(false);
                    cbLimitTNDis.SetEnabled(false); //�馩�W�����ƤU�Կ��
                    
                    ChangeTab(0);
                    //**2011/04/14 Tina�GSA���->1.6.1.3 ������O���uHapptGo�馩�v�A���ҥ�����Disable�A�ϥΪ̤��i��J�C
                    $(".abgne_tab").hide();
                    break;
                default:
                    ChangeTab(0);
                    break;
            }

        }
        
        //�s���ˬd
        function checkForm(s, e) {
            var DisTypeValue = cbDisType.GetValue();
            var s1 = txtDisAmt.GetText();
            var s2 = txtDisRate.GetText();

            //**�u���O�v���i�[���ʡj�M�i�ث~�]�w�j�̡A�i�馩���B�j�P�i�ӫ~�馩��v�j�|�ϦǡA�ҥH�����ˬd�O�_����g�C
            //**�u���O�v���iHappyGo�馩�j�̡A�i�ӫ~�馩��v�j�|�ϦǡA�ҥH�i�馩���B�j�O�������C
            //**�i�馩���B�j�P�i�ӫ~�馩��v�j���ܤ@��g(�u���\��@�榳��)�C
            switch (DisTypeValue) {
                case "5":   //HappyGo�馩
                    if (s1 == '') {
                        ShowMessage('�i�馩���B�j���������!!');
                        e.processOnServer = false;
                        return false;
                    }
                    break;
                case "6":  //�[����
                case "7":  //�ث~�]�w
                    break;
                default:
                    if (s1 == '' && s2 == '') {
                        ShowMessage('�i�馩���B�j�P�i�ӫ~�馩��v�j�@�w�n��g�@��!!');
                        e.processOnServer = false;
                        return false;
                    }
                    else if (s1 != '' && s2 != '') {
                        ShowMessage('�i�馩���B�j�P�i�ӫ~�馩��v�j�u���\��@��즳��!!');
                        e.processOnServer = false;
                        return false;
                    }
                    break;
            }


            //**�u���O�v���i�[���ʡj�M�i�ث~�]�w�j�̡A�i�馩�W�����ơj�|�ϦǡA�ҥH�����ˬd�O�_����g�C
            //**�馩�W������DropDownList�����(�ëD"�L")�A�h�馩�W������TextBox���������C
            if (cbLimitTNDis.GetValue() != "1") {
                if (txtLTNDis.GetText() == "") {
                    ShowMessage("�馩�W�����ƥ���!!");
                    txtLTNDis.Focus();
                    e.processOnServer = false;
                    return false;
                }
            }

            Loading("�s�ɤ�...");
        }

        function onOK() {
            var boolDisCode = txtDisCode.GetEnabled(); //�O�_�O�ѡu�d�߭ק�v�Ӫ��H Enabled = ture ��ܡi�_�j�Afalse ��ܡi�O�j
            var DisType = cbDisType.GetValue();
            if (DisType == "1")  //�@��
            {
                //�馩�Ƹ��۰ʵ���
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

        //�馩���B��J�ˬd
        function checkDiscountAmt(s, e) {
            var DisTypeValue = cbDisType.GetValue();
            var s1 = txtDisAmt.GetText();
            var s2 = txtDisRate.GetText();

            //**�u���O�v���i�[���ʡj�M�i�ث~�]�w�j�̡A�i�馩���B�j�P�i�ӫ~�馩��v�j�|�ϦǡA�ҥH�����ˬd�O�_����g�C
            //**�i�馩���B�j�P�i�ӫ~�馩��v�j���ܤ@��g(�u���\��@�榳��)�C
            if (DisTypeValue != "6" && DisTypeValue != "7") {
                var s1 = txtDisAmt.GetText();
                var s2 = txtDisRate.GetText();

                if (s1 == '' && s2 == '') {
                    s.SetText('');
                    alert('�i�馩���B�j�P�i�ӫ~�馩��v�j�@�w�n��g�@��!!');
                    e.processOnServer = false;
                }
                else if (s1 != '' && s2 != '') {
                    s.SetText('');
                    alert('�i�馩���B�j�P�i�ӫ~�馩��v�j�u���\��@��즳��!!');
                    e.processOnServer = false;
                }
            }
        }

        //�ӫ~�馩��v��J�ˬd
        function checkDiscountRate(s, e) {
            var DisTypeValue = cbDisType.GetValue();
            var s1 = txtDisAmt.GetText();
            var s2 = txtDisRate.GetText();
            var chk = true;
            //**�u���O�v���i�[���ʡj�M�i�ث~�]�w�j�̡A�i�馩���B�j�P�i�ӫ~�馩��v�j�|�ϦǡA�ҥH�����ˬd�O�_����g�C
            //**�i�馩���B�j�P�i�ӫ~�馩��v�j���ܤ@��g(�u���\��@�榳��)�C
            if (DisTypeValue != "6" && DisTypeValue != "7") {
                var s1 = txtDisAmt.GetText();
                var s2 = txtDisRate.GetText();

                if (s1 == '' && s2 == '') {
                    alert('�i�馩���B�j�P�i�ӫ~�馩��v�j�@�w�n��g�@��!!');
                    e.processOnServer = false;
                    chk = false;
                }
                else if (s1 != '' && s2 != '') {
                    s.SetText('');
                    alert('�i�馩���B�j�P�i�ӫ~�馩��v�j�u���\��@��즳��!!');
                    e.processOnServer = false;
                    chk = false;
                }
            }

            if (chk) {
                var tmpDisRate = txtDisRate.GetText();

                if (tmpDisRate != '') {
                    if (tmpDisRate > 100) {
                        txtDisRate.SetText(null);
                        alert("�i�ӫ~�馩��v�j���i�W�L100!!");
                        e.processOnServer = false;
                    }

                }
            }
        }

        //�ѡi�������ߡj���ӫ~�������o�������ߥN���M�|�p���
        function getAccountInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getAccountInfo(s.GetValue(), onSuccess);
        }

        //�ѡi�������ߡj���������ߥN�����o�ӫ~����
        function getProd_CategInfo(s, e) {
            if (s.GetText() != '')
                PageMethods.getProd_CategInfo(s.GetValue(), onSuccess);
        }
        
        function onSuccess(returnData, userContext, methodName) {
            switch (methodName) {
                case "getAccountInfo":  //�ѡi�������ߡj���ӫ~�������o�������ߥN���M�|�p���
                    if (returnData != '') {
                        var Values = returnData.split(',');
                        txtCCDNo.SetValue(Values[0]);     //��������
                        ACCOUNTCODE.SetValue(Values[1]);  //�|�p���
                        ACCOUNTCODE.SetEnabled(false);
                    }
                    else {
                        ACCOUNTCODE.SetValue(null);
                        ACCOUNTCODE.SetEnabled(true);
                    }
                    break;
                case "getProd_CategInfo":  //�ѡi�������ߡj���������ߥN�����o�ӫ~����
                    if (returnData == 'NoFound') {
                        txtCCDNo.SetValue(null);
                        ACCOUNTCODE.SetValue(null);
                        ACCOUNTCODE.SetEnabled(true);
                        alert("�������ߤ��s�b");
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
                            ACCOUNTCODE.SetValue(null);  //�|�p���
                            ACCOUNTCODE.SetEnabled(false);
                        }
                    }
                    break;
                case "getDisCountCode":  //�i���O�j����u�@��v�ɡA�t�Φ۰ʨ��o�馩�Ƹ�
                    if (returnData != '') {
                        txtDisCode.SetText(returnData);
                    }
                    else {
                        txtDisCode.SetText(null);
                    }
                    break;
                case "getAccountCodeBySYSPARA": //�i���O�j����u�¾��^���v�ΡuHappy Go�v�ɡA�t�Φ۰ʥN�X�T�w���|�p���
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

        //��� or ���� �i�������ơj�����s
        function showBtnTimes(index) {

            if (typeof (btnTimes) != "undefined") {
                switch (cbLimitTNDis.GetValue()) {
                    case "1":  //�L
                        btnTimes.SetVisible(false);
                        lblRemainingTimes.SetVisible(false);
                        if (gvStore.IsEditing() == true) {
                            DisUseCount.SetEnabled(false);
                            if (DisUseCount.GetText() == '') DisUseCount.SetText('0');
                        }
                        break;
                    case "2":  //���w
                        btnTimes.SetVisible(false);
                        lblRemainingTimes.SetVisible(false);
                        if (gvStore.IsEditing() == true) {
                            DisUseCount.SetEnabled(true);
                            if (DisUseCount.GetText() == '') DisUseCount.SetText(txtLTNDis.GetText());
                        }
                        break;
                    case "4":  //����
                        btnTimes.SetVisible(true);
                        lblRemainingTimes.SetVisible(true);
                        if (gvStore.IsEditing() == true) {
                            DisUseCount.SetEnabled(false);
                            if (DisUseCount.GetText() == '') DisUseCount.SetText('0');
                        }
                        break;
                    case "3":  //�`�q
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
                alert("�ӫ~�Ƹ����s�b!");
                
                Sender.Focus();
                Sender.SetValue(null);             
            }
            else {
                if (returnData == "fail") {
                    EventArgs.processOnServer = false;
                    alert("�ӫ~�Ƹ������\�]�w!");

                    Sender.Focus();
                    Sender.SetValue(null);
                }
                else {
                    //**2011/04/01 Tina�G[�[����]�b�d�߰ӫ~�Ƹ��W�ٮɡA�]�n�a�X�������C
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

            //**2011/04/25 Tina�G�NURL�ǻ����Ѽƥ[�K�C
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
                    <!--�馩�]�w�@�~-->
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
                    �G
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
                                   <dx:ASPxButton ID="btnImport" runat="server" Text="�j�q�W��" CausesValidation="false" AutoPostBack="false"  Width="100px">
                                        <ClientSideEvents Click="function(s,e) { ModifyImportPopupURL(); }" />
                                   </dx:ASPxButton>
                                    <cc:ASPxPopupControl ID="ASPxPopupControl" ClientInstanceName="PopupImport" runat="server" AllowDragging="True" AllowResize="True"
                                        CloseAction="CloseButton" PopupElementID="btnImport" ContentUrl="~/VSS/DIS/DIS01/DIS01_Import_byDisType.aspx?cbDisType=aa"
                                        Width="640" Height="400" LoadingPanelID="lp" HeaderText="�j�q�W��" onOKScript="onOK">
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
                    �G
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="txtDisCode" runat="server" Width="170px" MaxLength="8" ClientInstanceName="txtDisCode">
                        <ValidationSettings SetFocusOnError="true">
                            <RegularExpression ValidationExpression="^\d{8}$" ErrorText="�榡���פ����T" />
                            <RequiredField IsRequired="true" ErrorText="�������" />
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
                    �G
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="txtDisName" runat="server" Width="170px" ClientInstanceName="txtDisName">
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="�������" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td width="80px" align="right">
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="<%$ Resources:WebResources, Status %>">
                    </dx:ASPxLabel>
                    �G
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
                    �G
                </td>
                <td width="170px" align="left">
                    <dx:ASPxTextBox ID="txtDisAmt" runat="server" Width="170px" ClientInstanceName="txtDisAmt">
                        <ValidationSettings SetFocusOnError="true">
                            <RegularExpression ValidationExpression="-\d*" ErrorText="�榡�����T,�ХH�t�ȿ�J�P���i��J�p���I" />
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
                    �G
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="txtDisRate" runat="server" Width="170px" ClientInstanceName="txtDisRate">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="\d*" ErrorText="�榡�����T" />
                                    </ValidationSettings>
                                    <ClientSideEvents TextChanged="function(s,e){checkDiscountRate(s,e);}" />
                                </dx:ASPxTextBox>
                                <div style="color: Red; font-size:smaller">�Ҧp�G20%�N��K��</div>
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
                    �G
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
                    �G
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct1" runat="server" Width="25" MaxLength="2" ClientInstanceName="txtAcct1">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{2}$" ErrorText="�Фſ�J�D�ƭȩή榡���פ���" />
                                        <RequiredField IsRequired="true" ErrorText="�������" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct2" runat="server" Width="30" MaxLength="3" ClientInstanceName="txtAcct2">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{3}$" ErrorText="�Фſ�J�D�ƭȩή榡���פ���" />
                                        <RequiredField IsRequired="true" ErrorText="�������" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct3" runat="server" Width="40" MaxLength="4" ClientInstanceName="txtAcct3">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{4}$" ErrorText="�Фſ�J�D�ƭȩή榡���פ���" />
                                        <RequiredField IsRequired="true" ErrorText="�������" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct4" runat="server" Width="55" MaxLength="6" ClientInstanceName="txtAcct4">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{6}$" ErrorText="�Фſ�J�D�ƭȩή榡���פ���" />
                                        <RequiredField IsRequired="true" ErrorText="�������" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct5" runat="server" Width="40" MaxLength="4" ClientInstanceName="txtAcct5">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{4}$" ErrorText="�Фſ�J�D�ƭȩή榡���פ���" />
                                        <RequiredField IsRequired="true" ErrorText="�������" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtAcct6" runat="server" Width="40" MaxLength="4" ClientInstanceName="txtAcct6">
                                    <ValidationSettings SetFocusOnError="true">
                                        <RegularExpression ValidationExpression="^\d{4}$" ErrorText="�Фſ�J�D�ƭȩή榡���פ���" />
                                        <RequiredField IsRequired="true" ErrorText="�������" />
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
                    <!--�馩�W������-->
                    <asp:Literal ID="LimitTheNumberDiscount" runat="server" Text="<%$ Resources:WebResources, LimitTheNumberDiscount %>"></asp:Literal>�G
                </td>
                <td width="170px" align="left">
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxComboBox ID="cbLimitTNDis" runat="server" SelectedIndex="1" ValueType="System.String"
                                    Width="60px" ClientInstanceName="cbLimitTNDis">
                                    <Items>
                                        <dx:ListEditItem Text="�L" Value="1" />
                                        <dx:ListEditItem Text="���w" Value="2" />
                                        <dx:ListEditItem Text="�`�q" Value="3" />
                                        <dx:ListEditItem Text="����" Value="4" />
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
                                        <RegularExpression ValidationExpression="\d*" ErrorText="�榡����" />
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
                    �G
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
                    �G
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
                                        <RequiredField IsRequired="true" ErrorText="�������" />
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
            <%--�O�v�Υӿ�����1--%>
            <li><a href="#tab1">�O�v�Υӿ�����</a></li>
            <%--���w�ӫ~2--%>
            <li><a href="#tab2">���w�ӫ~</a></li>
            <%--���w����3--%>
            <li><a href="#tab3">���w����</a></li>
            <%--���w�P�P4--%>
            <li><a href="#tab4">���w�P�P</a></li>
            <%--�Ȥ��H5--%>
            <li><a href="#tab5">�Ȥ��H</a></li>
            <%--��������6--%>
            <li><a href="#tab6">��������</a></li>
            <%--�ث~�]�w7--%>
            <li><a href="#tab7">�ث~�]�w</a></li>
            <%--�[����8--%>
            <li><a href="#tab8">�[����</a></li>
        </ul>
        <div class="tab_container">
            <%--�O�v�Υӿ�����1--%>
            <div id="tab1" class="tab_content">
	            <uc1:DISItemChargesAndApply ID="DISItemChargesAndApply1" runat="server" />
            </div>
            <%--���w�ӫ~2--%>
            <div id="tab2" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
	                    <uc2:PRODUCT_DISCOUNT ID="PRODUCT_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--���w����3--%>
            <div id="tab3" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
	                    <uc3:STORE_DISCOUNT ID="STORE_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--���w�P�P4--%>
            <div id="tab4" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
	                    <uc4:PROMOTION_DISCOUNT ID="PROMOTION_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--�Ȥ��H5--%>
            <div id="tab5" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
	                    <uc5:CUST_LEVE_DISCOUNT ID="CUST_LEVE_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--��������6--%>
            <div id="tab6" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
	                    <uc6:COST_CENTER_DISCOUNT ID="COST_CENTER_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--�ث~�]�w7--%>
            <div id="tab7" class="tab_content">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
	                    <uc7:GIFT_DISCOUNT ID="GIFT_DISCOUNT1" runat="server" />
	                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--�[����8--%>
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
                        <dx:ASPxButton ID="btnSave" ClientInstanceName="btnSave" runat="server" Text="�s��" AutoPostBack="false" OnClick="btnSave_Click">
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
                            <ClientSideEvents Click="function(s,e){if(!confirm('�O�_�T�{�R��'))e.processOnServer = false; return e.processOnServer;} " />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <iframe id="fDownload" style="display: none" src="" runat="server"></iframe>
    
</asp:Content>

