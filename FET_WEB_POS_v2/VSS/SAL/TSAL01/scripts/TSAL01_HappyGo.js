var HappyGoDiscount_Info = [];

function happyGoDiscount() {
	// HappyGo 折抵
	var amt = parseInt($('#labTotalAmount').text());
	if (amt == 0) {
		alert('無應收總金額，請先填入商品料號');
		return;
	}

	var addProdList = '';
	var rows = $('#gvMaster tr[jqKey]');
	var dataTable = [];
	$.each(rows, function(i, v) {
		var rowJson = getRowData($(v));
		addProdList += rowJson.PRODNO + '^';
		if ((typeof(rowJson.PROMOTION_CODE) != 'undefined' && rowJson.PROMOTION_CODE.length != 0) || (typeof(rowJson.MSISDN) != 'undefined' && rowJson.MSISDN.length != 0))
			dataTable[dataTable.length] = {
				PRODNO: rowJson.PRODNO,
				PROMOTION_CODE: (typeof(rowJson.PROMOTION_CODE) == 'undefined') ? '' : rowJson.PROMOTION_CODE,
				MSISDN: (typeof (rowJson.MSISDN) == 'undefined') ? '' : rowJson.MSISDN
			};
	});
	if (addProdList.length != 0)
		addProdList = addProdList.substr(0, addProdList.length - 1);
	ajaxSaveHappyGoSessionTable(dataTable, addProdList, amt);
}

function openHappyGoDiscountWindow(addProdList, amt) {
    var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutHG.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + amt + '&TRAN_DATE=' + $('span[jqKey="labTRADE_DATE"]').text() + '&addProdList=' + addProdList, 500, 550);
	if (r == null || typeof (r) == 'undefined')
		return;
	var prods = [];
	$.each(r, function(i, s) {
		var arr = s.split(',');
		prods[prods.length] = arr[1];
		if (i == 0) {
			$('#labHG_CARD_NO').text(arr[4]);
			$('#labHG_REMAIN_POINT').text(arr[7]);
		}
	});
	ajaxHappyGoProdList(prods.join(';'), cloneJson(r));
}

function openHappyGoWindow() {
	if (HappyGoDiscount_Info == null || HappyGoDiscount_Info.length == 0)
		return false;
	var totalPoint = 0;
	var totalAmount = 0;
	var cardNo = HappyGoDiscount_Info[0].CARD_NO;
	var leftPoint = HappyGoDiscount_Info[0].HG_LEFT_POINT;
	$.each(HappyGoDiscount_Info, function(i, v) {
		totalPoint += parseInt(v.HG_REDEEM_POINT);
		totalAmount += parseInt(v.TOTAL_AMOUNT);
	});
	var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG4.aspx?date=' + Date()
                                                            + '&HG_CARD_NO=' + cardNo
                                                            + '&HG_REDEEM_POINT=' + totalPoint
                                                            + '&TOTAL_AMOUNT=' + totalAmount
                                                            + '&HG_LEFT_POINT=' + leftPoint
                                                            , 500, 500);
	if (r == '' || r == undefined)
		return false;
	else if (r == 'OK')
		return true;
	else
		return false;
}

function enableFieldForHappyGoAppendPaid() {
	// 加價購的品項中如果有需要輸入 IMEI 的品項, 則開放 IMEI 輸入框
	$.each($('#gvMaster tr[jqKey]'), function(i, r) {
		var tr = $(r);
		var rowJson = getRowData(tr);
		if (typeof (rowJson.IS_HG_APPEND) != 'undefined' && rowJson.IS_HG_APPEND == '1') {
			if (rowJson.IMEI_FLAG != '1') {
				tr.find('td[jqKey="IMEI"] > span[jqKey="DisplayOnly"]').remove();
				tr.find(':text[jqKey="IMEI"]').attr('readonly', 'true').show();
				tr.find(':button[jqKey="DELETE_IMEI"]').hide();
				if (rowJson.QUANTITY == '1') {
					tr.find(':button[jqKey="CHOOSE_IMEI"]').hide();
					tr.find(':text[jqKey="IMEI"]').removeAttr('readonly');
					if (rowJson.IMEI_OK == '1')
						tr.find(':button[jqKey="DELETE_IMEI"]').show();
				} else {
					tr.find(':button[jqKey="CHOOSE_IMEI"]').show();
				}
				setIMEIIcon(tr, rowJson.IMEI_OK == '1');
			}
		}
	});
	$('#gvMaster tr[jqKey] :text[jqKey]:visible').focus();
}

function openHappyGoRewardWindow(cardNo, totalAmount) {
    var r = openDialogWindowByEncrypt('../../CheckOut/CheckOutHG5.aspx?date=' + Date()
                                                            + '&HG_CARD_NO=' + cardNo
                                                            + '&TOTAL_AMOUNT=' + totalAmount
                                                            , 300, 300);
	if (r == '' || r == undefined)
		return false;
	else if (r == 'OK')
		return true;
	else
		return false;
}