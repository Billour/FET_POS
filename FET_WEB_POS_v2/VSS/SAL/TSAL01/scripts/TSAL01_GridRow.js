function createItemRow() {
	// 新增品項輸入行
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}

	var gv = $('#gvMaster');
	var rows = gv.find('tr').length;
	var cols = gv.find('tr:first-child').children().length;
	var tdClass = 'grid_row_' + (rows % 2);
	var tr, td;

	tr = $('<tr />')
		.attr({ 'id': 'gvMaster_' + rows, 'jqKey': 'Row' })
		.css('cursor', 'default');
	// CheckBox
	$('<td />')
		.attr({'jqKey': 'CHECK' })
		.addClass(tdClass)
		.append('<input type="checkbox" id="' + tr.attr('id') + '_0_check" jqKey="CHECK"/>')
		.append('<input type="hidden" id="' + tr.attr('id') + '_0_did" value="" jqKey="ITEM_ID" />')
		.css({ 'width': '20px', 'text-align': 'center' })
		.appendTo(tr);
	// 作廢欄位
	$('<td />')
		.attr({ 'jqKey': 'UNLESS' })
		.addClass(tdClass)
		.css({ 'width': '30px', 'text-align': 'center' })
		.hide()
		.appendTo(tr);
	// 類別
	$('<td />')
		.attr({ 'title': '單品', 'jqKey': 'ITEM_MODE' })
		.addClass(tdClass)
		.css({ 'width': '30px', 'text-align': 'center' })
		.text('單')
		.appendTo(tr);
	// 促銷名稱
	$('<td />')
		.attr({ 'jqKey': 'PROMOTION_NAME' })
		.addClass(tdClass)
		.append('<div id="' + tr.attr('id') + '_2_label" class="gridLabelCell" style="width:80px" jqKey="PROMOTION_NAME" />')
		.appendTo(tr);
	// 商品料號
	$('<td />')
		.attr({ 'jqKey': 'PRODNO' })
		.addClass(tdClass)
		.css('width', '120px')
		.append(
			$('<input type="text" id="' + tr.attr('id') + '_text" size="9" title="請輸入商品料號" jqKey="PRODNO" />')
				.click(function() { event.returnValue = false; return false; })
				.blur(function() { checkAndRequestProdNo(this); })
		)
		.append('&nbsp;')
		.append($('<input type="button" id="' + tr.attr('id') + '_button" value="選" class="chooseButton" jqKey="CHOOSE_ITEM" />')
			.click(function(e) { chooseProdNo(this); event.returnValue = false; return false; }))
		.appendTo(tr);
	// 商品名稱
	$('<td />')
		.attr({ 'jqKey': 'PRODNAME' })
		.addClass(tdClass)
		.append($('<div id="' + tr.attr('id') + '_label" jqKey="PRODNAME" />').addClass('gridLabelCell'))
		.appendTo(tr);
	// SIM卡卡號 (only for紙本授權)
	$('<td />')
		.attr({ 'jqKey': 'SIM_CARD_NO' })
		.addClass(tdClass)
		.css({ 'width': '140px', 'text-align': 'center' })
		.append(
		    $('<input type="text" id="' + tr.attr('id') + '_text" size="10" jqKey="SIM_CARD_NO" />')
		        .click(function() { event.returnValue = false; return false; })
		        .blur(function() { getSIMCardNoData(this); })
		)
		.hide()
		.appendTo(tr);
	// 數量
	$('<td />')
		.attr({ 'jqKey': 'QTY' })
		.addClass(tdClass)
		.append($('<input type="text" id="' + tr.attr('id') + '_text" size="3" jqKey="QTY" />')
			.click(function() { event.returnValue = false; return false; })
			.blur(function() { recountItemTotalPrice(this); })
			.numeric(qtyNumericSettings)
			)
		.appendTo(tr);
	// 單價
	$('<td />')
		.attr({ 'jqKey': 'PRICE' })
		.addClass(tdClass)
		.append($('<input type="text" id="' + tr.attr('id') + '_text" size="6" jqKey="PRICE" />')
			.click(function() { event.returnValue = false; return false; })
			.blur(function() { recountItemTotalPrice(this); })
			.numeric(priceNumericSettings))
		.appendTo(tr);
	// 小計
	$('<td />')
		.attr({ 'jqKey': 'ITEM_AMOUNT' })
		.addClass(tdClass)
		.append($('<div id="' + tr.attr('id') + '_label" jqKey="ITEM_AMOUNT" />')
			.addClass('gridNumberCell'))
		.appendTo(tr);
	// IMEI Icon
	$('<td />')
		.attr({ 'jqKey': 'IMEI_ICON' })
		.addClass(tdClass)
		.css('text-align', 'center')
		.appendTo(tr);
	// IMEI
	$('<td />')
		.attr({ 'jqKey': 'IMEI' })
		.addClass(tdClass)
		.css('width', '27%')
		.append($('<input type="text" id="' + tr.attr('id') + '_imei" size="30" jqKey="IMEI" />')
			.click(function() { event.returnValue = false; return false; })
			.keydown(function(e) {
				if (e.keyCode == 13 && $(this).val().length != 0) {
					checkIMEI(this, true); event.returnValue = false; return false;
				}
			})
			.hide())
		.append('&nbsp;')
		.append($('<input type="button" id="' + tr.attr('id') + '_choose" value="選" jqKey="CHOOSE_IMEI" />')
			.addClass('chooseButton')
			.click(function(e) { openInputIMEIDialog(this); event.returnValue = false; return false; })
			.hide())
		.append($('<input type="button" id="' + tr.attr('id') + '_delete" value="清" jqKey="DELETE_IMEI" />')
			.addClass('chooseButton')
			.click(function(e) { deleteIMEICache(this); event.returnValue = false; return false; })
			.hide())
		.appendTo(tr);

	tr.find(':text').addClass('gridTextBox');
	gv.append(tr);

	$(':button[jqKey="btnConfirm"]').removeAttr('disabled');
	tr.find(':text[jqKey="PRODNO"]').focus();
	tr.find(':text').focus(function() { $(this).select(); });

	//$(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	$(':button[jqKey="btnAddETC"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnHappyGoNet"]').removeAttr('disabled');
	$(':button[jqKey="btnStoreDiscount"]').removeAttr('disabled');

	tr.find('td').click(function() {
		if (tr.find(':checkbox:checked').length == 0)
			tr.find(':checkbox').attr('checked', 'true');
		else
			tr.find(':checkbox').removeAttr('checked');
	});
	tr.find('td:first-child').unbind('click');
	return tr;
}

function createCheckoutRow() {
	// 建立付款方式資料行
	var gv = $('#gvCheckout');
	var rows = gv.find('tr').length;
	var tdClass = 'grid_row_' + (rows % 2);
	var tr, td;

	tr = $('<tr />')
		.attr({ 'id': 'gvCheckout_' + rows, 'jqKey': 'Row' });
	// CheckBox
	$('<td />')
		.attr({ 'jqKey': 'CHECK' })
		.css({ 'width': '20px', 'text-align': 'center' })
		.addClass(tdClass)
		.append('<input type="checkbox" id="' + tr.attr('id') + '_check" jqKey="CHECK"/>')
		.append('<input type="hidden" id="' + tr.attr('id') + '_id" jqKey="PAID_ID" />')
		.appendTo(tr);
	// 作廢欄位
	$('<td />')
		.attr({ 'jqKey': 'UNLESS' })
		.addClass(tdClass)
		.css({ 'width': '30px', 'text-align': 'center' })
		.hide()
		.appendTo(tr);
	// 付款方式
	$('<td />')
		.attr({ 'jqKey': 'PAID_MODE_NAME' })
		.addClass(tdClass)
		.css({ 'width': '10%', 'text-align': 'center' })
		.appendTo(tr);
	// 金額
	$('<td />')
		.attr({ 'jqKey': 'AMOUNT' })
		.addClass(tdClass)
		.css({ 'width': '10%', 'text-align': 'right', 'padding-right': '5px' })
		.appendTo(tr);
	// 付款明細
	$('<td />')
		.attr({ 'jqKey': 'DESCRIPTION' })
		.addClass(tdClass)
		.css({ 'padding-left': '5px' })
		.appendTo(tr);

	gv.append(tr);

	tr.find('td').click(function() {
		if (tr.find(':checkbox:checked').length == 0)
			tr.find(':checkbox').attr('checked', 'true');
		else
			tr.find(':checkbox').removeAttr('checked');
	});
	tr.find('td:first-child').unbind('click');

	return tr;
}

function createDiscountRow() {
	// 新增折扣資料行
	var gv = $('#gvDiscount');
	var rows = gv.find('tr').length;
	var cols = gv.find('tr:first-child').children().length;
	var tdClass = 'grid_row_' + (rows % 2);
	var tr, td;

	tr = $('<tr />')
		.attr({ 'id': 'gvDiscount' + rows, 'jqKey': 'Row' });
	// 項次 SEQNO
	$('<td />')
		.attr({ 'id': tr.attr('id') + '_0', 'jqKey': 'SEQNO' })
		.addClass(tdClass)
		.css({ 'width': '30px', 'text-align': 'right' })
		.appendTo(tr);
	// 折扣料號 DISCOUNT_ID
	$('<td />')
		.attr({ 'id': tr.attr('id') + '_1', 'jqKey': 'DISCOUNT_ID' })
		.addClass(tdClass)
		.appendTo(tr);
	// 折扣名稱 DISCOUNT_NAME
	$('<td />')
		.attr({ 'id': tr.attr('id') + '_2', 'jqKey': 'NAME' })
		.addClass(tdClass)
		.appendTo(tr);
	// 數量 QUANTITY
	$('<td />')
		.attr({ 'id': tr.attr('id') + '_3', 'jqKey': 'QTY' })
		.addClass(tdClass)
		.css({ 'width': '60px', 'text-align': 'right' })
		.appendTo(tr);
	// 單價 DISCOUNT_PRICE
	$('<td />')
		.attr({ 'id': tr.attr('id') + '_4', 'jqKey': 'PRICE' })
		.addClass(tdClass)
		.css({ 'width': '80px', 'text-align': 'right' })
		.appendTo(tr);
	// 總價 DISCOUNT_AMOUNT
	$('<td />')
		.attr({ 'id': tr.attr('id') + '_5', 'jqKey': 'AMOUNT' })
		.addClass(tdClass)
		.css({ 'width': '120px', 'text-align': 'right' })
		.appendTo(tr);

	gv.append(tr);

	return tr;
}

function createIMEIItemRow() {
	var gv = $('#gvIMEI');
	var rows = gv.find('tr[jqKey]').length;
	if (rows >= parseInt(rowJson.QUANTITY))
		return null;
	var cols = gv.find('tr:first-child').children().length;
	var tdClass = 'grid_row_' + ((rows + 1) % 2);
	var tr, td;

	tr = $('<tr />')
		.attr({ 'id': 'gvIMEI_' + rows, 'jqKey': 'Row' });
	// CheckBox
	$('<td />')
		.attr('id', tr.attr('id') + '_0')
		.css({ 'width': '20px', 'text-align': 'center' })
		.addClass(tdClass)
		.append('<input type="checkbox" id="' + tr.attr('id') + '_check" jqKey="CHECK"/>')
		.appendTo(tr);
	// IMEI
	$('<td />')
		.attr({ 'id': tr.attr('id') + '_1', 'jqKey': 'IMEI' })
		.addClass(tdClass)
		.appendTo(tr);

	gv.append(tr);

	return tr;
}

function createAddProdRow() {
	var gv = $('#gvProds');
	var rows = gv.find('tr').length;
	var cols = gv.find('tr:first-child').children().length;
	var tdClass = 'grid_row_' + (rows % 2);
	var tr, td;

	tr = $('<tr />')
		.attr({ 'jqKey': 'Row' });
	// Prod Selector Cell
	$('<td />')
		.attr({ 'jqKey': 'SELECT' })
		.addClass(tdClass)
		.css({ 'width': '60%', 'text-align': 'left' })
		.append(
			$('<select />')
				.attr({ 'jqKey': 'PRODS' })
				.css({ 'border-width': '1px', 'width': '100%' })
		)
		.appendTo(tr);
	// UNI_PRICE
	$('<td />')
		.attr({ 'jqKey': 'UNI_PRICE' })
		.addClass(tdClass)
		.css({ 'text-align': 'right', 'width': '10%' })
		.appendTo(tr);
	// SALE_PRICE
	$('<td />')
		.attr({ 'jqKey': 'SALE_PRICE' })
		.addClass(tdClass)
		.css({ 'text-align': 'right', 'width': '10%' })
		.appendTo(tr);
	// QTY
	$('<td />')
		.attr({ 'jqKey': 'QTY' })
		.addClass(tdClass)
		.css({ 'width': '10%' })
		.append(
			$('<input />')
				.attr({ 'type': 'text', 'size': '3', 'value': '0', 'jqKey': 'QTY' })
				.css({ 'border-width': '1px', 'text-align': 'right' })
		)
		.appendTo(tr);
	// AMOUNT
	$('<td />')
		.attr({ 'jqKey': 'AMOUNT' })
		.addClass(tdClass)
		.css({ 'text-align': 'right', 'width': '10%' })
		.appendTo(tr);

	tr.find(':text').focus(function() { $(this).select(); });

	gv.append(tr);

	return tr;
}