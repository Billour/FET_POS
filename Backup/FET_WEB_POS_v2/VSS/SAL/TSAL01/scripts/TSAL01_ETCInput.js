var cardMaxLimitAmount = 10000;	// 儲值卡最大儲值金額

$(document).ready(function() {
	$(document).keydown(function(e) { if (e.keyCode == 27) window.close(); });
	$(':button').addClass('buttonStyle');
	$(':text').addClass('textBoxStyle');
	$(':submit').addClass('buttonStyle');
	$(':button[jqKey="btnCash"]').click(function() { checkRule($('#txtAmount'), 0); });
	$(':button[jqKey="btnCreditCard"]').click(function() { checkRule($('#txtAmount'), 1); });
	$('#labMinLimitAmount').text(hfMinLimitAmount.val());
	$(':text').focus(function() { $(this).select(); });
	$('#btnReady').click(function() { call_FETC_Inquiry(); });
});

function valueReturn(val, type) {
	returnValue = 'ETCAdd^' + val + '^' + hfETC_ProdNo.val() + '^' + type;
	window.close();
}

function checkRule(o, type) {
	if ($(o).val() != '') {
		$(o).focus();
		var amt = parseInt($(o).val());
		if (isNaN(amt) || amt <= 0) {
			alert('輸入字串非數字格式且不允許小於等於0，請重新輸入!');
		} else if (amt.toString() != $(o).val()) {
			alert('折抵金額不為整數，請重新輸入!');
		} else if (amt < parseInt(hfMinLimitAmount.val())) {
			alert('加值金額低於最低加值金額，請重新輸入!');
		} else if (amt % 100 != 0) {
			alert('加值金額需以百元為單位，請重新輸入!');
		} else if (amt > parseInt($('#labMaxLimitAmount').text())) {
			alert('加值金額大於最大加值金額，請重新輸入!');
		} else {
			valueReturn(amt, type);
		}
	}
}

function call_FETC_Inquiry() {
	var oECR = new ActiveXObject("ProjECR.ECRAPI");
	var result = oECR.FETC_Inquiry();
	var arr = result.split(',');
	if (arr[0] == '0001') {
		alert('無法讀取卡片');
	} else {
		$('#labETCCardNo').text(arr[0]);
		var max = cardMaxLimitAmount - parseInt(arr[1]);
		if (isNaN(max))
			max = 0;
		$('#labMaxLimitAmount').text(max);
		changeToAmountLayer();
	}
}

function changeToAmountLayer() {
	$('#ReadETCCard').hide();
	$('#AmountLayer').show();
	$('#txtAmount')
		.keydown(function(e) { if (e.keyCode == 13) checkRule(this); })
		.numeric({
			buttons: false,
			title: '請輸入加值金額',
			keyboard: false,
			minValue: parseInt(hfMinLimitAmount.val()),
			maxValue: parseInt($('#labMaxLimitAmount').text())
		})
		.focus();
}