function createCheckoutRow() {
	// 建立付款方式資料行
	var gv = $('#gvCheckout');
	var rows = $('#gvCheckout tr').length;
	var cols = $('#gvCheckout tr:first-child').children().length;
	var tdClass = 'grid_row_' + (rows % 2);
	var tr, td;

	tr = $('<tr />');
	tr.attr('id', 'gvCheckout_' + rows).attr('jqKey', 'Row');
	// CheckBox
	td = $('<td></td>');
	td.addClass(tdClass);
	td.append('<input type="checkbox" id="gvCheckout_' + rows + '_check"/>');
	td.append('<input type="hidden" id="' + td.attr('id') + '_id" value="" />');
	td.css('width', '20px').css('text-align', 'center');
	tr.append(td);
	// 作廢欄位
	td = $('<td />');
	td.addClass(tdClass);
	tr.append(td);
	// 付款方式
	td = $('<td />');
	td.addClass(tdClass);
	tr.append(td);
	// 金額
	td = $('<td />');
	td.addClass(tdClass);
	tr.append(td);
	// 付款明細
	td = $('<td />');
	td.addClass(tdClass);
	tr.append(td);

	gv.append(tr);

	tr.click(function() {
		if (tr.find(':checkbox:checked').length == 0)
			tr.find(':checkbox').attr('checked', 'true');
		else
			tr.find(':checkbox').removeAttr('checked');
	});
	checkItemCancelButtonEnable();
	
	return tr;
}

function disableCheckoutInterface() {
	// 關閉(Disable)付款方式相關功能
	$('input[jqKey="btnCash"]').attr('disabled', 'disabled');
	$('input[jqKey="btnDeleteCheckout"]').attr('disabled', 'disabled');

	$('input[jqKey="btnCreditCard"]').attr('disabled', 'disabled');
	$('input[jqKey="btnInstalment"]').attr('disabled', 'disabled');
	$('input[jqKey="btnBankCard"]').attr('disabled', 'disabled');
	$('input[jqKey="btnHappyGo"]').attr('disabled', 'disabled');
	$('input[jqKey="btnCoupon"]').attr('disabled', 'disabled');
	$('input[jqKey="btnOffLineCreditCard"]').attr('disabled', 'disabled');
}

function enableCheckoutInterface() {
	// 啟用(Enable)付款方式相關功能
	// 品項中如果有 ETC/NCIC/SEEDNET 等只能使用現金結帳
	$('input[jqKey="btnCash"]').removeAttr('disabled');
	$('input[jqKey="btnDeleteCheckout"]').removeAttr('disabled');
	var rows = $('#gvMaster tr[jqKey="Row"]');
	var rowJson = getRowData($(rows[0]));
	if (rows.length == 1 && typeof (rowJson.IS_ETC) != 'undefined' && rowJson.IS_ETC == '1')
		return;
	$('input[jqKey="btnCreditCard"]').removeAttr('disabled');
	if (creditDivLimitAmount <= parseInt($('#labTotalAmount').text())) {
		// 消費金額需超過一定門檻值才可以使用信用卡分期支付
		$('input[jqKey="btnInstalment"]').removeAttr('disabled');
	}
	$('input[jqKey="btnBankCard"]').removeAttr('disabled');
	//$('input[jqKey="btnHappyGo"]').removeAttr('disabled');
	//$('input[jqKey="btnCoupon"]').removeAttr('disabled');
	$('input[jqKey="btnOffLineCreditCard"]').removeAttr('disabled');
}

function paidCash() {
	// 現金付款
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/Checkout/CheckoutCash.aspx?date=' + Date(), 400, 200);
	if (typeof(r) != 'undefined' && r != null && r != '')
		ajaxSavePaidCache(r);
}

function creditCard() {
	// 信用卡
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutCredit.aspx?date=' + Date(), 400, 300);
	if (typeof (r) != 'undefined' && r != null && r != '')
		ajaxSavePaidCache(r);
}

function instalment() {
	// 信用卡分期
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutCreditStage.aspx?date=' + Date(), 400, 300);
	if (typeof (r) != 'undefined' && r != null && r != '')
		ajaxSavePaidCache(r);
}

function bankCard() {
	// 金融卡
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutDebitCard.aspx?date=' + Date(), 400, 300);
	if (typeof (r) != 'undefined' && r != null && r != '')
		ajaxSavePaidCache(r);
}

function happyGo() {
	// HappyGo
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutHG2.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + $('#labTotalAmount').text() + '&TRAN_DATE=' + $('span[jqKey="labTRADE_DATE"]').text(), 500, 500);
	alert(r);
	//if (typeof (r) != 'undefined' && r != null && r != '')
	//	ajaxSavePaidCache(r);
}

function coupon() {
	// 禮卷
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutGift.aspx', 300, 150);
	alert(r);
	//if (typeof (r) != 'undefined' && r != null && r != '')
	//	ajaxSavePaidCache(r);
}

function offLineCreditCard() {
	// 離線信用卡
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutCreditUnline.aspx?date=' + Date(), 400, 200);
	if (typeof (r) != 'undefined' && r != null && r != '')
		ajaxSavePaidCache(r);
}

function checkAllCheckoutItems() {
	// 核選/取消所有支付項目
	var gv = $('#gvCheckout');
	var chks = gv.find(':checkbox[id$="_check"]');
	var chk = $('#chkAllCheckout');
	$.each(chks, function() {
		$(this).attr('checked', chk.attr('checked'));
	});
}

function deleteCheckoutRow() {
	// 刪除支付項目
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var gv = $('#gvCheckout');
	var chks = gv.find(':checkbox:checked');
	if (chks.length == 0 || !confirm('您是否要刪除已勾選的支付項目 ?'))
		return;
	var ids = [];
	$.each(chks, function(i, v) {
		ids[ids.length] = getRowData($(v).parents('tr').first()).ID;
	});
	ajaxDeletePaidCache(ids.join(';'));
}

function checkItemCancelButtonEnable() {
	// 檢查品項取消紐是否開啟(Enable)
	// 當支付方式清空時, 方可開啟此品項取消鈕
	var gv = $('#gvCheckout');
	var btn = $('input[jqKey="btnCancel"]');
	var rows = gv.find('tr[jqKey]');
	if (rows.length == 0) {
		btn.removeAttr('disabled');
		$('input[jqKey="btnCheckout"]').attr('disabled', 'disabled');
	} else {
		var hasUnlock = false;
		$.each(rows, function(i, v) {
			if (hasUnlock) return;
			var rowJson = getRowData($(v));
			if (rowJson != null && (typeof(rowJson.IS_LOCK) == 'undefined' || rowJson.IS_LOCK != '1')) {
				hasUnlock = true;
				return;
			}
		});
		if (hasUnlock) {
			btn.attr('disabled', 'disabled');
			$('input[jqKey="btnCheckout"]').removeAttr('disabled');
		}
	}
}

function checkOutData() {
	// 結帳前檢核
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var gv = $('#gvCheckout');
	var receivable = parseInt($('#labTotalAmount').text());
	var payable = parseInt($('#labTotalPayable').text());
	var change = parseInt($('#labChange').text());
	var rows = gv.find('tr[jqKey="Row"]').length;
	if (rows == 0) {
		alert('請先選擇支付方式以及輸入金額 !');
		return;
	}
	if (payable != 0 && receivable != payable) {
		alert('「應付總金額」小於「應收總金額」, 請補齊付款方式與金額 !');
		return;
	}
	var txtUNI_NO = $(':text[jqKey="txtUNI_NO"]');
	var txtUNI_TITLE = $(':text[jqKey="txtUNI_TITLE"]');
	// 檢核統一編號與發票抬頭
	if (txtUNI_NO.val().length == 0 && txtUNI_TITLE.val().length != 0) {
		alert('輸入「發票抬頭」時,必須一並輸入「統一編號」 !');
		txtUNI_NO.focus();
		return;
	} else if (txtUNI_NO.val().length == 0 && txtUNI_TITLE.val().length != 0) {
		alert('輸入「統一編號」時,必須一並輸入「發票抬頭」 !');
		txtUNI_TITLE.focus();
		return;
	} else if (txtUNI_NO.val().length != 0 && !checkCompanyId(txtUNI_NO.val())) {
		alert('「統一編號」輸入錯誤, 請重新輸入 !');
		txtUNI_NO.focus();
		return;
	}
	
	if (!confirm('您確定要進行結帳嗎？'))
		return;
	if (change > 0)
		alert('需找零「' + change + '」元');
	$(':button[jqKey="btnCheckout"]').attr('disabled', 'disabled');
	ajaxCheckout();
}

function recountCheckoutTotalAmount() {
	// 重新計算應收總金額
	var gv = $('#gvCheckout');
	var paid = 0;
	$.each(gv.find('tr[jqKey]'), function(i, o) {
	var rowJson = getRowData($(o));
		if (typeof(rowJson.IS_LOCK) == 'undefined' || rowJson.IS_LOCK != '1')
			paid += parseInt($(o).find('td:eq(3)').text());
	});
	var total = parseInt($('#labTotalAmount').text());
	if (total != 0 && paid == 0) {
		$('#labTotalPayable').text(total - paid);
		$('#labChange').text('0');
	} else if (total > paid) {
		$('#labTotalPayable').text(total - paid);
		$('#labChange').text('0');
	} else if (total < paid) {
		$('#labTotalPayable').text('0');
		$('#labChange').text(paid - total);
	} else {
		$('#labTotalPayable').text('0');
		$('#labChange').text('0');
	}
}

function lockCheckoutGrid() {
	// 鎖定結帳表格
	$('#gvCheckout :checkbox').attr('disabled', 'disabled');
	disableCheckoutInterface();
}

function unlockCheckGrid() {
	$('#gvCheckout :checkbox').removeAttr('disabled');
	enableCheckoutInterface();
}

function getPaidType(key) {
	// 將支付類別英文代碼換成數字代碼
	switch (key.toUpperCase()) {
		case 'CASH':
			return 1;
		case 'CREDITCARD':
			return 2;
		case 'OFF_LINE_CREDIT':
			return 3;
		case 'VISADEBIT':
			return 6;
		default:
			return '0';
	}
}

function getPaidTypeName(type) {
	// 取得支付類別名稱
	switch (parseInt(type)) {
		case 1:
			return '現金';
		case 2:
			return '信用卡';
		case 3:
			return '離線信用卡';
		case 4:
			return '分期付款';
		case 5:
			return '禮卷';
		case 6:
			return '分期付款';
		case 7:
			return 'HappyGo';
		case 8:
			return '找零';
		default:
			return '';
	}
}
