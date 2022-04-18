function setDiscountRowData(json, tr) {
	// 設定折扣資料
	setRowData(tr, json);
	tr.find('td[jqKey="SEQNO"]').text(json.SEQNO);
	tr.find('td[jqKey="DISCOUNT_ID"]').text(json.DISCOUNT_ID);
	tr.find('td[jqKey="NAME"]').text(json.DISCOUNT_NAME);
	tr.find('td[jqKey="QTY"]').text(json.QUANTITY);
	tr.find('td[jqKey="PRICE"]').text(Math.abs(json.DISCOUNT_PRICE));
	tr.find('td[jqKey="AMOUNT"]').text(Math.abs(json.DISCOUNT_AMOUNT));
}

function showDiscount(json) {
	// 顯示折扣資料
	discountInfo.length = 0;
	$('#gvDiscount tr[jqKey]').remove();
	$('#divDiscount').hide();
	if (typeof (json) == 'undefined' || json == null || json.length <= 0) {
		return;
	} else {
		$('#divDiscount').show();
		$.each(json, function(i, v) {
			discountInfo[discountInfo.length] = v;
			var tr = createDiscountRow();
			setDiscountRowData(v, tr);
		});
	}
}

function getDiscountSum() {
	// 取得折扣總和
	var sum = 0;
	$.each($('#gvDiscount tr[jqKey]'), function(i, r) {
		var tr = $(r);
		var rowJson = getRowData(tr);
		sum += parseInt(rowJson.DISCOUNT_AMOUNT);
	});
	return sum;
}