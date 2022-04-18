function createDiscountRow() {
	// 新增折扣資料行
	var gv = $('#gvDiscount');
	var rows = $('#gvDiscount tr').length;
	var cols = $('#gvDiscount tr:first-child').children().length;
	var tdClass = 'grid_row_' + (rows % 2);
	var tr, td;

	tr = $('<tr></tr>');
	tr.attr('id', 'gvDiscount' + rows);
	// 項次 SEQNO
	$('<td></td>')
		.addClass(tdClass)
		.css({ 'width': '30px', 'text-align': 'right' })
		.appendTo(tr);
	// 折扣料號 DISCOUNT_ID
	$('<td></td>')
		.addClass(tdClass)
		.appendTo(tr);
	// 折扣名稱 DISCOUNT_NAME
	$('<td></td>')
		.addClass(tdClass)
		.appendTo(tr);
	// 數量 QUANTITY
	$('<td></td>')
		.addClass(tdClass)
		.css({ 'width': '60px', 'text-align': 'right' })
		.appendTo(tr);
	// 單價 DISCOUNT_PRICE
	$('<td></td>')
		.addClass(tdClass)
		.css({ 'width': '80px', 'text-align': 'right' })
		.appendTo(tr);
	// 總價 DISCOUNT_AMOUNT
	$('<td></td>')
		.addClass(tdClass)
		.css({ 'width': '120px', 'text-align': 'right' })
		.appendTo(tr);

	gv.append(tr);

	return tr;
}

function setDiscountRowData(json, tr) {
	// 設定折扣資料
	setRowData(tr, json);
	tr.find('td:nth-child(1)').text(json.SEQNO);
	tr.find('td:nth-child(2)').text(json.DISCOUNT_ID);
	tr.find('td:nth-child(3)').text(json.DISCOUNT_NAME);
	tr.find('td:nth-child(4)').text(json.QUANTITY);
	tr.find('td:nth-child(5)').text(json.DISCOUNT_PRICE);
	tr.find('td:nth-child(6)').text(json.DISCOUNT_AMOUNT);
}

function showDiscount(json) {
	// 顯示折扣資料
	discountInfo.length = 0;
	if (typeof (json) == 'undefined' || json.length <= 0) {
		$('#divDiscount').hide();
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