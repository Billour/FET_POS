function addETCItem() {
	// 新增 ETC 品項
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt('TSAL01_ETCInput.aspx', 400, 200);
	if (r != null && r != '') {
		var arr = r.split('^');
		var tr = createItemRow();
		tr.find(':text[jqKey="PRODNO"]').val(arr[2]);
		var type = parseInt(arr[3]);
		var amount = arr[1];
		if (!ajaxRequestETCInfo(tr, arr[1], (type == 0)))
			return;
		$('#gvMaster').parents('table:first').find('table:first').find(':button').attr('disabled', 'disabled');
		$(':button[jqKey="btnGift"]').removeAttr('disabled');
		$(':button[jqKey="btnDeleteItems"]').removeAttr('disabled');
		if (type == 1) {
			// 0: 現金, 1: ETC 聯名卡
			if (ajaxSaveItemCache(false)) {
				$('#gvCheckout').parents('table:first').find('table:first').find(':button').attr('disabled', 'disabled');
				var r = '';
				do {
					var r = openETC(true);
					if (typeof (r) == 'string') {
						arr = r.split(',');
						if ($.trim(arr[0]) == '0000') {
						    arr[0] = 'ETC_CREDIT';
						    hasETCItemCard = true;
							ajaxSavePaidCache(arr.join(',')+','+amount, false);
							break;
						} else {
							alert('加值失敗');
						}
					} else {
						if (!confirm('ETC 加值失敗, 是否重新加值 ?'))
							break;
					}
				} while (true)
			}
		} else {
			$(':button[jqKey="btnConfirm"]').focus();
		}
	}
}

function checkCancelItemLastIsETC() {
	// 檢查取消品項輸入後, 是否取消的是 ETC
	if (!hasETCItem)
		return;
	var rows = $('#gvMaster tr[jqKey]');
	$.each(rows, function(i, r) {
		var tr = $(r);
		var rowJson = getRowData(tr);
		if (typeof (rowJson.IS_ETC) != 'undefined' && rowJson.IS_ETC == '1' && (typeof (rowJson.BILL_TYPE) == 'undefined' || rowJson.BILL_TYPE == 'ETC'))
			tr.remove();
	});
	reassignElementID($('#gvMaster'));
	$(':button[jqKey="btnConfirm"]').attr('disabled', 'disabled');
	recountTotalAmount();
	hasETCItem = false;
}

function openETC(nextStep) {
	// 開啟 ETC 加值介面
	if (!hasETCItem)
		return;
	var etcAmt = parseInt($('#labTotalAmount').text());
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/ETCCardLoading.aspx?date=' + Date() + "&PaidAmt=" + etcAmt, 500, 500);
	if (r == null || r == '' || typeof (r) == 'undefined')
		return false;
	var resp = r.split(',');
	if (resp[0] != '0000') {
		if (typeof (nextStep) == 'undefined' || !nextStep)
			alert('ETC 加值失敗, 終止結帳動作！');
		return false;
	} else {
		if (typeof (nextStep) != 'undefined' && nextStep)
			return r;
		else
			return true;
	}
}

