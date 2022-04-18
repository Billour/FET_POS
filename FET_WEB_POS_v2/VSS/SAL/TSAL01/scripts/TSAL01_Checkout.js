function enablePaidButton(btn) {
	if (allowPayMode.indexOf(parseInt(btn.attr('jqMode'))) != -1) {
		// 必須為可支付狀態, 才可開啟支付按鈕
		btn.removeAttr('disabled');
	}
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
	$(':button[jqKey="btnDeleteCheckout"]').removeAttr('disabled');
	enablePaidButton($(':button[jqKey="btnCash"]'));
	var rows = $('#gvMaster tr[jqKey]');
	var rowJson = getRowData($(rows[0]));

	// 品項中如果有 ETC 代收, 則只能使用現金結帳
	var hasETC = (rows.length == 1 && typeof (rowJson.IS_ETC) != 'undefined' && rowJson.IS_ETC == '1');
	$.each(rows, function(i, r) {
		if (hasETC) return;
		rowJson = getRowData($(r));
		hasETC = ((typeof (rowJson.BILL_TYPE) != 'undefined' && rowJson.BILL_TYPE == 'ETC') || hasETCCard);
	});
	if (hasETC) return;
	
	// 品項中如果有 NCIC/SEEDNET, 則只能使用現金結帳
	var ncic = false;
	$.each(rows, function(i, r) {
		if (ncic) return;
		rowJson = getRowData($(r));
		ncic = (typeof (rowJson.BILL_TYPE) != 'undefined' && (rowJson.BILL_TYPE == 'SeedNet' || rowJson.BILL_TYPE == 'NCIC'));
	});
	if (ncic) return;

	enablePaidButton($(':button[jqKey="btnCreditCard"]'));
	enablePaidButton($(':button[jqKey="btnOffLineCreditCard"]'));
	
	if (creditDivLimitAmount <= parseInt($('#labTotalAmount').text())) {
		// 消費金額需超過一定門檻值才可以使用信用卡分期支付
		if (parseInt(hfSALE_TYPE.val()) == 2)   //代收不可分期付款
		    $(':button[jqKey="btnInstalment"]').attr('disabled', 'disabled');
		else
		    enablePaidButton($(':button[jqKey="btnInstalment"]'));
	}
	if (hfActionType.val() == '2' && (parseInt(hfSALE_TYPE.val()) == 2)) {
		// 如果是未結且代收單, 則開啟 HappyGo 支付按鈕
		enablePaidButton($(':button[jqKey="btnHappyGo"]'));
		// 但是必須關閉信用卡分期
		//$(':button[jqKey="btnInstalment"]').attr('disabled', 'disabled');
	}

	$.each($('#gvCheckout tr[jqKey]'), function(i, r) {
		var rowJson = getRowData($(r));
		switch (parseInt(rowJson.PAID_MODE)) {
			case 2:
				$(':button[jqKey="btnInstalment"]').attr('disabled', 'disabled');
				$(':button[jqKey="btnOffLineCreditCard"]').attr('disabled', 'disabled');
				break
			case 3:
				$(':button[jqKey="btnCreditCard"]').attr('disabled', 'disabled');
				$(':button[jqKey="btnInstalment"]').attr('disabled', 'disabled');
				break
			case 4:
				$(':button[jqKey="btnCreditCard"]').attr('disabled', 'disabled');
				$(':button[jqKey="btnOffLineCreditCard"]').attr('disabled', 'disabled');
				break
			default:
				break;
		}
	});
}

function paidCash(o) {
	// 現金付款
	if (allowPayMode.indexOf(parseInt($(o).attr('jqMode'))) == -1) {
		alert('現階段無法使用「' + $(o).val() + '」支付方式 !');
		return;
	}
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/Checkout/CheckoutCash.aspx?ShouldPayAmt=' + $('#labTotalPayable').text() + '&date=' + Date(), 400, 200);
	if (typeof (r) == 'undefined' || r == null || r == '')
		return;
	ajaxSavePaidCache(r);
}

function creditCard(o) {
	// 信用卡
	if (allowPayMode.indexOf(parseInt($(o).attr('jqMode'))) == -1) {
		alert('現階段無法使用「' + $(o).val() + '」支付方式 !');
		return;
	}
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutCredit.aspx?ShouldPayAmt=' + $('#labTotalPayable').text() + '&date=' + Date(), 400, 300);
	if (typeof (r) == 'undefined' || r == null || r == '')
		return;
	ajaxSavePaidCache(r);
}

function instalment(o) {
	// 信用卡分期
	if (allowPayMode.indexOf(parseInt($(o).attr('jqMode'))) == -1) {
		alert('現階段無法使用「' + $(o).val() + '」支付方式 !');
		return;
	}
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutCreditStage.aspx?ShouldPayAmt=' + $('#labTotalPayable').text() + '&date=' + Date(), 400, 300);
	if (typeof (r) == 'undefined' || r == null || r == '')
		return;
	ajaxSavePaidCache('Stage'+r);
}

function bankCard(o) {
	// 金融卡
	if (allowPayMode.indexOf(parseInt($(o).attr('jqMode'))) == -1) {
		alert('現階段無法使用「' + $(o).val() + '」支付方式 !');
		return;
	}
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutDebitCard.aspx?ShouldPayAmt=' + $('#labTotalPayable').text() + '&date=' + Date(), 400, 300);
	if (typeof (r) == 'undefined' || r == null || r == '')
		return;
	ajaxSavePaidCache(r);
}

function happyGo(o) {
	// HappyGo
	if (allowPayMode.indexOf(parseInt($(o).attr('jqMode'))) == -1) {
		alert('現階段無法使用「' + $(o).val() + '」支付方式 !');
		return;
	}
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutHG2.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + $('#labTotalAmount').text() + '&TRAN_DATE=' + $('span[jqKey="labTRADE_DATE"]').text(), 500, 500);
	if (typeof (r) == 'undefined' || r == null || r == '')
		return;
	ajaxSavePaidCache(r);
}

function coupon(o) {
	// 禮卷
	if (allowPayMode.indexOf(parseInt($(o).attr('jqMode'))) == -1) {
		alert('現階段無法使用「' + $(o).val() + '」支付方式 !');
		return;
	}
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutGift.aspx', 300, 150);
	alert(r);
	//if (typeof (r) != 'undefined' && r != null && r != '')
	//	ajaxSavePaidCache(r);
}

function offLineCreditCard(o) {
	// 離線信用卡
	if (allowPayMode.indexOf(parseInt($(o).attr('jqMode'))) == -1) {
		alert('現階段無法使用「' + $(o).val() + '」支付方式 !');
		return;
	}
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutCreditUnline.aspx?ShouldPayAmt=' + $('#labTotalPayable').text() + '&date=' + Date(), 400, 200);
	if (typeof (r) == 'undefined' || r == null || r == '')
		return;
	var arr = r.split(',');
	if (parseInt(arr[1]) > parseInt($('#labTotalPayable').text())) {
		alert('金額不得高於應付總金額 !');
		return;
	}
	ajaxSavePaidCache(r);
}

function checkAllCheckoutItems() {
	// 核選/取消所有支付項目
	var gv = $('#gvCheckout');
	var chks = gv.find(':checkbox[jqKey="CHECK"]');
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
	var chks = gv.find(':checkbox[jqKey="CHECK"]:checked');
	if (chks.length == 0)
		return;
	var ids = [];
	var isCredit = false;
	$.each(chks, function() {
		if (isCredit) return;
		var tr = $(this).parents('tr:first');
		var type = tr.data('Description').split(',')[0];
		if (parseInt(type) != 1) {
			alert(getPaidTypeName(type) + '支付資料不可刪除 !');
			isCredit = true;
			return;
		}
		ids[ids.length] = getRowData(tr).ID;
	});
	if (isCredit) return;
	if (!confirm('您是否要刪除已勾選的支付項目 ?'))
		return;
	ajaxDeletePaidCache(ids.join(';'));
}

function checkHasPaidItem() {
	var rows = $('#gvCheckout tr[jqKey]');
	var hasPaid = false;
	if (hfActionType.val() == '5') {
		// 換貨
		$.each(rows, function(i, r) {
			if (hasPaid) return;
			var tr = $(r);
			var rowJson = getRowData(tr);
			hasPaid = (typeof(rowJson.IS_UNLESS) == 'undefined' || rowJson.IS_UNLESS != '1');
		});
	} else {
		hasPaid = (rows.length != 0);
	}
	return hasPaid;
}

function checkItemCancelButtonEnable() {
	// 檢查品項取消紐是否開啟(Enable)
	// 當支付方式清空時, 方可開啟此品項取消鈕
	var rows = $('#gvCheckout tr[jqKey]');
	var btn = $(':button[jqKey="btnCancel"]');
	if (!checkHasPaidItem()) {
		btn.removeAttr('disabled');
		$(':button[jqKey="btnCheckout"]').attr('disabled', 'disabled');
		//$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
	} else {
		btn.attr('disabled', 'disabled');
		$(':button[jqKey="btnCheckout"]').removeAttr('disabled')
		//$(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
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
	if ((gv.find('tr[jqKey]').length == 0) || (payable != 0 && receivable != payable) || (receivable != 0 && receivable == payable)) {
		alert('支付不合理，請重新輸入 !');
		return;
	}
	var txtUNI_NO = $(':text[jqKey="txtUNI_NO"]');
	var txtUNI_TITLE = $(':text[jqKey="txtUNI_TITLE"]');
	// 檢核統一編號與發票抬頭
	if (txtUNI_NO.val().length == 0 && txtUNI_TITLE.val().length != 0) {
		alert('輸入「發票抬頭」時,必須一併輸入「統一編號」 !');
		$(':text[jqKey="txtUNI_NO"]').focus();
		//getUNINo(txtUNI_NO.val());
		return;
	}
	//4/19改成"發票抬頭"可不輸入
	//} else if (txtUNI_NO.val().length != 0 && txtUNI_TITLE.val().length == 0) {
	//	alert('輸入「統一編號」時,必須一併輸入「發票抬頭」 !');
	//	txtUNI_TITLE.focus();
	//	return;
	//}
	
	if (!confirm('您確定要進行結帳嗎？'))
		return;
	if (change > 0)
	    alert('需找零「' + change + '」元');
	
	//判斷是否為ETC現金儲值
    if (!hasETCItemCard && hasETCItem && !openETC())
		return;
	if (hasHappyGoDiscount && !openHappyGoWindow())
		return;
	$(':button[jqKey="btnCheckout"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
	ajaxCheckout();
}

function recountCheckoutTotalAmount() {
	// 重新計算應收總金額
	var gv = $('#gvCheckout');
	var total = parseInt($('#labTotalAmount').text());
	var discount = Math.abs(getDiscountSum());
	var paid = 0;
	$.each(gv.find('tr[jqKey]'), function(i, o) {
		paid += parseInt($(o).find('td[jqKey="AMOUNT"]').text());
	});
	var needPaid = total - discount - paid;
	if (needPaid == 0) {
		// 應收等於零
		$('#labTotalPayable').text('0');
		$('#labChange').text('0');
	} else if (needPaid > 0) {
		// 應收大於零
		$('#labTotalPayable').text(needPaid);
		$('#labChange').text('0');
	} else if (needPaid < 0) {
		$('#labTotalPayable').text('0');
		// 折扣大於應收總金額時，不需找零
		if (discount < total || total < 0)
			$('#labChange').text(Math.abs(needPaid));
		else
			$('#labChange').text(0);
	}
	var btn = $(':button[jqKey="btnInstalment"]');
	if (creditDivLimitAmount <= needPaid) {
		// 應收總金額需超過一定門檻值才可以使用信用卡分期支付
		if (parseInt(hfSALE_TYPE.val()) == 2)   //代收不可分期付款
		    btn.attr('disabled', 'disabled');
		else
		    enablePaidButton(btn);
	} else {
		btn.attr('disabled', 'disabled');
	}
	return total;
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

function settingCheckoutRow(tr, v) {
	if (hfActionType.val() == '5') {
		tr.find('td[jqKey="UNLESS"]').show();
		if (typeof (v.IS_UNLESS) != 'undefined' && v.IS_UNLESS == '1') {
			tr.find('td:[jqKey="UNLESS"]')
				.text('作廢')
				.attr('title', '換貨作廢');
		}
	}
	if (typeof (v.IS_LOCK) != 'undefined' && v.IS_LOCK == '1')
		tr.find(':checkbox').hide();
	setRowData(tr, v);
	var desc = v.PAID_MODE + ',' + v.PAID_AMOUNT + ',' + v.DESCRIPTION;
	tr.data('Description', desc);
	tr.find(':hidden[jqKey="PAID_ID"]').val(v.ID);
	tr.find('td[jqKey="PAID_MODE_NAME"]').text(getPaidTypeName(parseInt(v.PAID_MODE)));
	tr.find('td[jqKey="AMOUNT"]').text(Math.abs(parseInt(v.PAID_AMOUNT)));
	tr.find('td[jqKey="DESCRIPTION"]').text(v.DESCRIPTION);

	checkItemCancelButtonEnable();
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
		case 'STAGECREDITCARD':
		    return 4;
		case 'VISADEBIT':
			return 6;
		case 'HGPAID':
			return 7;
		case 'ETC_CREDIT':
			return 9;
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
			return '金融卡';
		case 7:
			return 'HappyGo';
		case 8:
			return '找零';
		case 9:
			return 'ETC聯名卡';
		default:
			return '';
	}
}
