var winProdSelecter = null;
var winInputIMEI = null;
var hasStoreDiscount = false;
var hasHappyGoDiscount = false;
var discountInfo = [];
var onAjaxRequest = false;

var qtyNumericSettings = {
	buttons: false,
	title: '請輸入銷售數量',
	keyboard: false,
	minValue: 1
};

var priceNumericSettings = {
	buttons: false,
	title: '請輸入商品單價',
	keyboard: false,
	minValue: 0
};

$(document).ready(function() {
	$(':text').focus(function() { $(this).select(); });
	$(':button').addClass('buttonStyle').attr('disabled', 'disabled');
	$(':text').addClass('textBoxStyle');
	$(':submit').addClass('buttonStyle');
	if (!isCheckInvSetting) {
		$(':text').attr('disabled', 'disabled');
		$(':button').attr('disabled', 'disabled');
		$(':text').attr('disabled', 'disabled');
		$(':submit').attr('disabled', 'disabled');
		alert('門市未設定電子發票, 請與總部聯絡!');
		window.location = baseUrl + '/content.htm';
		return;
	}
	$(':button[jqKey="btnCheckout"]').click(checkOutData);
	$(':button[jqKey="btnCancelTransaction"]').click(cancelTransaction);
	$(':submit[jqKey="btnSearch"]').click(function() { checkItemAdded(); });
	$(':submit[jqKey="btnUnClose"]').click(function() { checkItemAdded(); });
	checkAndLoadCache();
	$(':text[jqKey="txtUNI_NO"]')
		.keydown(function(e) {
			if (e.keyCode == 13) {
				if ($(this).val().length == 0)
					return;
				if (!checkCompanyId($(this).val())) {
					alert('統一編號驗證錯誤, 請重新輸入');
					$(this).focus();
					event.returnValue = false;
					return false;
				} else {
					alert('請記得輸入發票抬頭 !');
				}
			}
		})
		.numeric({
			buttons: false,
			title: '請輸入統一編號',
			keyboard: false
		});
	enableFunctionButton();
	$(document).keydown(function(e) { if (e.keyCode == 27) { event.returnValue = false; return false; } });
});

function createItemRow() {
	// 新增品項輸入行
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}

	var gv = $('#gvMaster');
	var rows = $('#gvMaster tr').length;
	var cols = $('#gvMaster tr:first-child').children().length;
	var tdClass = 'grid_row_' + (rows % 2);
	var tr, td;

	tr = $('<tr />')
		.attr({ 'id': 'gvMaster_' + rows, 'jqKey': 'Row' })
		.css('cursor', 'default');
	// CheckBox
	$('<td />')
		.attr('id', 'gvMaster_' + rows + '_0')
		.addClass(tdClass)
		.append('<input type="checkbox" id="gvMaster_' + rows + '_0_check"/>')
		.append('<input type="hidden" id="gvMaster_' + rows + '_0_did" value="" />')
		.css({ 'width': '20px', 'text-align': 'center' })
		.appendTo(tr);
	// 作廢欄位
	$('<td>&nbsp;</td>')
		.attr({ id: 'gvMaster_' + rows + '_1', title: '' })
		.addClass(tdClass)
		.css({ 'width': '30px', 'text-align': 'center' })
		.appendTo(tr);
	// 類別
	$('<td>單</td>')
		.attr({ id: 'gvMaster_' + rows + '_2', title: '單品' })
		.addClass(tdClass)
		.css({ 'width': '30px', 'text-align': 'center' })
		.appendTo(tr);
	// 促銷名稱
	$('<td />')
		.attr('id', 'gvMaster_' + rows + '_3')
		.addClass(tdClass)
		.append('<div id="gvMaster_' + rows + '_3_label" class="gridLabelCell" style="width:80px" />')
		.appendTo(tr);
	// 商品料號
	td = $('<td />')
		.attr('id', 'gvMaster_' + rows + '_4')
		.addClass(tdClass)
		.css('width', '140px');
	$('<input type="text" id="' + td.attr('id') + '_text" size="10" title="請輸入商品料號" jqKey="PRODNO" />')
		.click(function() { event.returnValue = false; return false; })
		.blur(function() { checkAndRequestProdNo(this); })
		//.keydown(function(e) { if (e.keyCode == 13) { checkAndRequestProdNo(this); event.returnValue = false; return false; } })
		.appendTo(td);
	td.append('&nbsp;');
	$('<input type="button" id="' + td.attr('id') + '_button" value="選" class="chooseButton" />')
		.click(function (e) { chooseProdNo(this); event.returnValue = false; return false; })
		.appendTo(td);
	tr.append(td);
	// 商品名稱
	td = $('<td />')
		.attr('id', 'gvMaster_' + rows + '_5')
		.addClass(tdClass);
	$('<div id="' + td.attr('id') + '_label" />')
		.addClass('gridLabelCell')
		.appendTo(td);
	tr.append(td);
	// 數量
	td = $('<td />')
		.attr('id', 'gvMaster_' + rows + '_6')
		.addClass(tdClass);
	$('<input type="text" id="' + td.attr('id') + '_text" size="3" jqKey="QTY" />')
		.click(function() { event.returnValue = false; return false; })
		.blur(function() { recountItemTotalPrice(this); })
		.numeric(qtyNumericSettings)
		.appendTo(td);
	tr.append(td);
	// 單價
	td = $('<td />')
		.attr('id', 'gvMaster_' + rows + '_7')
		.addClass(tdClass);
	$('<input type="text" id="' + td.attr('id') + '_text" size="6" jqKey="PRICE" />')
		.click(function() { event.returnValue = false; return false; })
		.blur(function() { recountItemTotalPrice(this); })
		.numeric(priceNumericSettings)
		.appendTo(td);
	tr.append(td);
	// 小計
	td = $('<td />')
		.attr('id', 'gvMaster_' + rows + '_8')
		.addClass(tdClass);
	$('<div id="' + td.attr('id') + '_label" />')
		.addClass('gridNumberCell')
		.appendTo(td);
	tr.append(td);
	// IMEI Icon
	$('<td />')
		.attr('id', 'gvMaster_' + rows + '_9')
		.addClass(tdClass)
		.css('text-align', 'center')
		.appendTo(tr);
	// IMEI
	td = $('<td />')
		.attr('id', 'gvMaster_' + rows + '_10')
		.addClass(tdClass)
		.css('width', '27%');
	$('<input type="text" id="' + td.attr('id') + '_imei" size="30" />')
		.click(function() { event.returnValue = false; return false; })
		.keydown(function(e) { if (e.keyCode == 13 && $(this).val().length != 0) { checkIMEI(this, true); event.returnValue = false; return false; } })
		.appendTo(td);
	td.append('&nbsp;');
	$('<input type="button" id="' + td.attr('id') + '_choose" value="選" />')
		.addClass('chooseButton')
		.click(function(e) { openInputIMEIDialog(this); event.returnValue = false; return false; })
		.appendTo(td);
	$('<input type="button" id="' + td.attr('id') + '_delete" value="清" />')
		.addClass('chooseButton')
		.click(function(e) { deleteIMEICache(this); event.returnValue = false; return false; })
		.appendTo(td);
	td.find('input').hide();
	tr.append(td);

	tr.find(':text').addClass('gridTextBox');
	gv.append(tr);

	$(':button[jqKey="btnConfirm"]').removeAttr('disabled');
	tr.find(':text[jqKey="PRODNO"]').focus();
	tr.find(':text').focus(function() { $(this).select(); });
	
	//$('input[jqKey="btnCancelTransaction"]').removeAttr('disabled');
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

function checkAllCheckboxs() {
	// 選擇或取消核選所有資料列
	var gv = $('#gvMaster');
	var chk = $('#chkAllRow');
	gv.find(':checkbox:visible[id$="_check"]')
		.attr('checked', chk.attr('checked'));
}

function deleteItemRow() {
	// 刪除選擇項目
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var gv = $('#gvMaster');
	var chks = gv.find(':checkbox:checked:visible[id$="_check"]');
	var beExits = false;
	var hasCheckedStoreDiccount = false;
	var hasCheckedHappyGoDiccount = false;
	var isCheckAll = (gv.find('tr[jqKey] :checkbox:checked').length == gv.find('tr[jqKey]').length);
	$.each(chks, function() {
		if (beExits) return;
		var rowJson = getRowData($(this).parents('tr').first());
		if (rowJson != null && typeof (rowJson.IS_TO_CLOSE) != 'undefined' && rowJson.IS_TO_CLOSE == '0') {
			alert('未結資料不可刪除, 請先取消勾選未結資料 !');
			beExits = true;
			return;
		} else if (rowJson != null && typeof (rowJson.CAN_DELETE) != 'undefined' && rowJson.CAN_DELETE == '0') {
			alert('此筆資料不可刪除, 請先取消勾選資料 !');
			beExits = true;
			return;
		}
		if (rowJson != null && rowJson.ITEM_TYPE == '6' && hasStoreDiscount)
			hasCheckedStoreDiccount = true;
		if (rowJson != null && rowJson.ITEM_TYPE == '4' && hasHappyGoDiscount)
			hasCheckedHappyGoDiccount = true;
	});
	if (beExits) return;
	if (!isCheckAll && hasStoreDiscount && !hasCheckedStoreDiccount) {
		alert('欲刪除品項資料, 必須一併刪除「' + $(':button[jqKey="btnStoreDiscount"]').val() + '」(' + StoreDiscountProd.PRODNO + '/' + StoreDiscountProd.PRODNAME + ') !');
		return;
	}
	if (!isCheckAll && hasHappyGoDiscount && !hasCheckedHappyGoDiccount) {
		alert('欲刪除品項資料, 必須一併刪除「' + $(':button[jqKey="btnHappyGoNet"]').val() + '」 !');
		return;
	}
	if (chks.length == 0 || !confirm('您是否要刪除已勾選的項目 ?'))
		return;
	var beRollbackItemGrid = false;
	// 刪除 TR
	$.each(chks, function() {
		var tr = $(this).parents('tr').first();
		var rowJson = getRowData(tr);
		if (hasStoreDiscount && rowJson.ITEM_TYPE == '6') {
			hasStoreDiscount = false;
			beRollbackItemGrid = true;
		}
		tr.remove();
	});
	// 重新指定 Element ID
	reassignElementID(gv);
	recountTotalAmount();
	if (beRollbackItemGrid)
		rollbackItemGrid();
	if (gv.find('tr[jqKey]').length == 0) {
		$('input[jqKey="btnConfirm"]').attr('disabled', 'disabled');
		//$('input[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
		hasStoreDiscount = false;
		hasHappyGoDiscount = false;
	} else {
		$('input[jqKey="btnConfirm"]').removeAttr('disabled');
		//$('input[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	}
	$('#chkAllRow').removeAttr('checked');
}

function chooseProdNo(o) {
	// Popup 產品選擇視窗
	if (winProdSelecter != null)
	    winProdSelecter.focus();

	winProdSelecter = openwindowByEncrypt('ProductsPopup.aspx?oid=' + $(o).parents('tr').first().attr('id'), 350, 550); 
	//window.open('ProductsPopup.aspx?oid=' + $(o).parents('tr').first().attr('id'), 'ChooseProductNo', 'height=350px,width=550px,menubar=no,resizable=no,scrollbars=no,status=no,toolbar=no,location=no');
}

function prodChooseDone(o, v) {
	// 供產品選擇窗回傳資料用
	var json = eval('(' + v + ')');
	ajaxRequestProductInfo(json.ProdNo, o);
}

function checkAndRequestProdNo(o) {
	// 料號欄位 Blur Event
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	if ($(o).val().length == 0)
		return;
	var rowJson = getRowData($(o).parents('tr').first());
	if ((rowJson != null && rowJson.PRODNO == $(o).val()) || ($(o).data('LastSearch') != 'undefined' && $(o).data('LastSearch') == $(o).val()))
		return;
	$(o).data('LastSearch', $(o).val());
	ajaxRequestProductInfo($(o).val(), $(o).parents('tr').first().attr('id'));
}

function recountItemTotalPrice(o) {
	// 計算單品小計
	var tr = $(o).parents('tr').first();
	var rowJson = getRowData(tr);
	if (rowJson == null) {
		alert('請先選擇商品料號 !');
		tr.find(':text[jqKey="PRODNO"]').focus();
		return;
	}
	var rowId = tr.attr('id');
	var qty = parseInt(tr.find(':text[jqKey="QTY"]').val());
	var price = parseInt(tr.find(':text[jqKey="PRICE"]').val());
	var total = qty * price;
	if (isNaN(total))
		total = 0;
	tr.find('#' + rowId + '_8_label').text(Math.abs(total));
	if ($(o).attr('jqKey') == 'QTY') {
		if (typeof (rowJson.ISSTOCK) != 'undefined' && rowJson.ISSTOCK == '1' && qty > parseInt(rowJson.ON_HAND_QTY)) {
			alert('輸入數量大於目前庫存量, 請重新輸入');
			$(o).focus();
			return;
		}
		rowJson.QUANTITY = qty.toString();
		setRowData(rowId, rowJson);
		var isLock = (typeof (rowJson.IS_LOCK) != 'undefined' && rowJson.IS_LOCK == '1');
		if (!isLock) {
			var txtIMEI = tr.find('#' + rowId + '_10_imei');
			var btnIMEI = tr.find('#' + rowId + '_10_choose');
			var btnDelIMEI = tr.find('#' + rowId + '_10_delete');
			var isConfirm = (typeof (rowJson.IS_CONFIRM) != 'undefined' && rowJson.IS_CONFIRM == '1');
			if (!isConfirm && rowJson.IMEI_FLAG != 1 && parseInt(hfGetOrigItems.val()) >= 2) {
				if (qty == 1) {
					if (rowJson.IMEI_OK == '1') {
						txtIMEI.attr('readonly', 'true');
						btnIMEI.hide();
						if (!isLock)
							btnDelIMEI.show();
					} else {
						txtIMEI.removeAttr('readonly');
						btnIMEI.hide();
						btnDelIMEI.hide();
					}
				} else {
					txtIMEI.attr('readonly', 'true');
					if (!isLock)
						btnIMEI.show();
					btnDelIMEI.hide();
				}
				checkIMEI($('#' + rowId + '_10_imei'), false);
			}
		}
		
	} else {
		if (parseInt(rowJson.PRICE) != price && rowJson.PRODNO != StoreDiscountProd.PRODNO) {
			rowJson.PRICE = price.toString();
			setRowData(tr, rowJson)
		}
	}
	recountTotalAmount();
}

function recountTotalAmount() {
	// 重新計算應收總金額
	var gv = $('#gvMaster');
	var rows = $('#gvMaster tr[jqKey]');
	var total = 0;
	var origTotal = 0;
	$.each(rows, function(i, o) {
		var rowJson = getRowData($(o));
		if (rowJson != null) {
			if (typeof (rowJson.IS_LOCK) == 'undefined' || rowJson.IS_LOCK != '1')
				total += parseInt(rowJson.QUANTITY) * parseInt(rowJson.PRICE);
			else
				origTotal += parseInt(rowJson.QUANTITY) * parseInt(rowJson.PRICE);
		}
	});
	$('#labTotalAmount').text(total);
	$('#labOrigTotal').text(origTotal);
	if (total >= origTotal) {
		$('#labDiffText').text('應補金額');
		$('#labDiffValue').text(total - origTotal);
	} else {
		$('#labDiffText').text('應退金額');
		$('#labDiffValue').text(origTotal - total);
	}
}

function confirmItems() {
	// 單品輸入完畢
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	$(this).attr('disabled', 'disabled');
	var gv = $('#gvMaster');
	var rows = $('#gvMaster tr').length;
	var objs = gv.find('tr[jqKey="Row"]');

	var idx = 0;
	var checkOK = true;
	$.each(objs, function() {
		if (!checkOK) return;
		var tr = $(this);
		var rowId = tr.attr('id');
		var txtProd = tr.find(':text[jqKey="PRODNO"]');
		var prod = tr.find('#' + rowId + '_5_label').text() + '(' + txtProd.val() + ')';
		var rowJson = getRowData(rowId); // eval('(' + $(this).val() + ')');
		// 檢查料號與 JSON
		if (rowJson == null || txtProd.val().length == 0) {
			alert('未選擇產品品項 !!');
			txtProd.focus();
			checkOK = false;
			return;
		}
		// 檢查庫存與銷售量
		if (parseInt(rowJson.ON_HAND_QTY) < parseInt(rowJson.QUANTITY)) {
			// 當庫存量小於銷售量時
			alert(prod + ' 銷售數量大於目前庫存量(' + rowJson.ON_HAND_QTY + ') !!');
			txtProd.focus();
			checkOK = false;
			return;
		}
		// 檢查 IMEI
		if (typeof (rowJson.IS_LOCK) == 'undefined' || rowJson.IS_LOCK == '0') {
			var txtIMEI = tr.find('#' + rowId + '_10_imei');
			if (rowJson.IMEI_FLAG != '1' && $.trim(txtIMEI.val()).length == 0) {
				alert(prod + ' 未輸入 IMEI !!');
				txtIMEI.focus();
				checkOK = false;
				return;
			} else if (rowJson.IMEI_FLAG != '1' && parseInt(rowJson.QUANTITY) != txtIMEI.val().split(',').length) {
				alert(prod + ' 輸入的 IMEI 數量與銷售數量不符合 !!');
				checkOK = false;
				return;
			} else if (rowJson.IMEI_FLAG != '1' && rowJson.IMEI_OK != '1') {
				alert(prod + ' 的 IMEI 資料不完全 !!');
				checkOK = false;
				txtIMEI.focus();
				return;
			}
			rowJson.IMEI = txtIMEI.val();
			setRowData(rowId, rowJson);
		}
	});
	if (!checkOK) return;

	ajaxSaveItemCache();
}

function lockItemRow(rowId) {
	var gv = $('#gvMaster');
	var tr = $('#' + rowId);
	tr.hide();
	$.each(tr.find(':text'), function() {
		if ($(this).parent().find('span[jqKey="DisplayOnly"]').length != 0)
			return;
		swapTextboxToLabel($(this));
	});
	tr.find(':button').blur().hide();
	tr.show();
}

function lockItemGridInterface() {
	// 鎖定品項輸入介面
	var gv = $('#gvMaster');
	gv.hide();
	$.each(gv.find(':text'), function() {
		if ($(this).parent().find('span[jqKey="DisplayOnly"]').length != 0)
			return;
		swapTextboxToLabel($(this));
	});
	gv.show();
}

function cancelItems() {
	// 取消品項鎖定
	var rows = $('#gvCheckout tr[jqKey]');
	var hasUnlock = false;
	$.each(rows, function(i, v) {
		if (hasUnlock) return;
		var rowJson = getRowData($(v));
		if (rowJson != null && (typeof (rowJson.IS_LOCK) == 'undefined' || rowJson.IS_LOCK != '1')) {
			hasUnlock = true;
			return;
		}
	});
	if (hasUnlock) {
		alert('必須取消所有支付款項, 才能此本取消程序 !');
		return;
	}
	var rows = $('#gvMaster tr[jqKey="Row"]');
	if (!confirm('請確定是否取消 ?'))
		return;
	ajaxCancelItemCache();
}

function rollbackItemGrid() {
	// 回復品項輸入相關功能
	$(':button[jqKey="btnConfirm"]').removeAttr('disabled');
	$(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnAddItem"]').removeAttr('disabled');
	$(':button[jqKey="btnDeleteItems"]').removeAttr('disabled');
	$(':checkbox[id="chkAllRow"]').show();

	var gv = $('#gvMaster');
	var rows = gv.find('tr[id^="gvMaster"]');
	if (!hasStoreDiscount && !hasHappyGoDiscount) {
		$(':button[jqKey="btnStoreDiscount"]').removeAttr('disabled');
		$(':button[jqKey="btnHappyGoNet"]').removeAttr('disabled');
		$.each(rows, function(i, row) {
			var tr = $(row);
			var rowId = tr.attr('id');
			var rowJson = getRowData(rowId);
			if (typeof (rowJson.IS_LOCK) != 'undefined' && rowJson.IS_LOCK == '1')
				return;
			tr.find(':checkbox').show();
			if (rowJson.ITEM_TYPE == '1') {
				tr.find('span[jqKey="DisplayOnly"]').remove();
				tr.find(':text[id$="_text"]').removeClass('gridTextBoxNoBorder').removeAttr('readonly').show();
				tr.find(':button[id$="_button"]').show();
			} else if (rowJson.ITEM_TYPE == '6') {
				$(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
			} else if (rowJson.ITEM_TYPE == '4') {
				$(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
			}
			var txtPrice = tr.find('#' + rowId + '_7_text');
			// 是否可變更單價
			if (typeof (rowJson.IS_POS_DEF_PRICE) == 'undefined' || rowJson.IS_POS_DEF_PRICE == 'Y') {
				txtPrice.val(rowJson.PRICE).show();
				txtPrice.parent().find('span[jqKey="DisplayOnly"]').remove();
			} else {
				txtPrice.val(rowJson.PRICE).hide();
				swapTextboxToLabel(txtPrice);
			}
			// IMEI
			if (rowJson.IMEI_FLAG != '1') {
				tr.find(':text[id$="_10_imei"]').attr('readonly', 'true').show();
				if (rowJson.QUANTITY == '1') {
					tr.find(':button[id$="_10_choose"]').hide();
					tr.find(':button[id$="_10_delete"]').show();
				} else {
					tr.find(':button[id$="_10_choose"]').show();
					tr.find(':button[id$="_10_delete"]').hide();
				}
			} else {
				tr.find(':text[id$="_10_imei"]').hide();
				tr.find(':button[id$="_10_choose"]').hide();
				tr.find(':button[id$="_10_delete"]').hide();
			}
		});
		$('#gvMaster :text[jqKey]:visible:first').focus();
	} else {
		$('#gvMaster tr[jqKey] :checkbox').show();
	}
	disableCheckoutInterface();
}

function disableProdItemInterface() {
	// 關閉(Disable)品項輸入相關功能
	$(':button[jqKey="btnConfirm"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnCancel"]').removeAttr('disabled');
	$(':button[jqKey="btnAddItem"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnDeleteItems"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnMixPromotion"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
	var gv = $('#gvMaster');
	gv.find('.chooseButton').blur().hide();
	gv.find(':button').blur().hide();
	gv.find(':checkbox').blur().hide();
	gv.find(':text').blur().addClass('gridTextBoxNoBorder').attr('readonly', 'true');
}

function checkIMEI(o, check) {
	// 檢查 IMEI 的是否合法
	var tr = $(o).parents('tr').first();
	var rowJson = getRowData(tr.attr('id'));
	var rowId = tr.attr('id');
	var qty = parseInt(tr.find('#' + rowId + '_6_text').val());

	if (qty != 1 || !check || $.trim($(o).val()).length == 0) {
		if ($.trim($(o).val()).length == 0 || $(o).val().split(',').length != qty) {
			rowJson.IMEI_OK = '0';
		} else {
			rowJson.IMEI_OK = '1';
		}
		setIMEIIcon(rowId, rowJson.IMEI_OK == '1');
		setRowData(rowId, rowJson);
	} else {
		// 先檢查 IMEI 是否已在本次銷售輸入過
		var imei = $(o).val().toUpperCase();
		if (typeof (rowJson.IMEI) == 'undefined' || rowJson.IMEI.length == 0) {
			ajaxCheckIMEIExists(imei, rowId);
		} else {
			var arr = rowJson.IMEI.split(',');
			var isExists = false;
			$.each(arr, function(i, v) {
				if (v.toUpperCase() == imei) {
					isExists = true;
					return;
				}
			});
			if (!isExists)
				ajaxCheckIMEIExists(imei, rowId);
		}
	}
}

function cancelTransaction() {
	// 取消交易
	var result = confirm('是否取消此次交易 ?');
	event.returnValue = result;
	return result;
}

function checkAndLoadCache() {
	// 檢查並載入快取資料
	switch (hfGetOrigItems.val()) {
		case '5':	// 載入歷史銷售資料
			ajaxSaleHistory();
			break;
		case '4':	// 結帳
			break;
		default:
			break;
	}
}

function settingItemRow(json, rowId, ignoreQty) {
	// 依 JSON 設定品項資料列
	var tr = $('#' + rowId);
	setRowData(tr, json);
	if (typeof (ignoreQty) == 'undefined' || ignoreQty == null || !ignoreQty) {
		if (typeof (json.ISSTOCK) != 'undefined' && json.ISSTOCK == '1' && parseInt(json.ON_HAND_QTY) <= 0 && json.PRODNO != StoreDiscountProd.PRODNO) {
			alert('此產品目前無庫存量');
			json.QUANTITY = '0';
			tr.find(':text[jqKey="PRODNO"]').focus();
		}
	}
	tr.find(':hidden[id$="_0_did"]').val(json.ID);
	if (typeof (json.IS_LOCK) != 'undefined' && json.IS_LOCK == '1')
		tr.find('td:eq(1)')
			.text('作廢')
			.attr('title', '作廢');
	if (typeof (json.PROMOTION_CODE) != 'undefined' && json.PROMOTION_CODE.length != 0)
		tr.find('td:eq(2)')
			.text('促')
			.attr('title', '促銷');
	else if (typeof (json.BARCODE1) != 'undefined' && json.BARCODE1.length != 0)
		tr.find('td:eq(2)')
			.text('代')
			.attr('title', '代收');
	if (typeof (json.PROMOTION_NAME) != 'undefined' && json.PROMOTION_NAME.length != 0)
		tr.find('#' + rowId + '_3_label')
			.text(json.PROMOTION_NAME)
			.attr('title', json.PROMOTION_NAME);
	tr.find(':text[jqKey="PRODNO"]').val(json.PRODNO);
	tr.find('#' + rowId + '_5_label').text(json.PRODNAME);
	tr.find('#' + rowId + '_5_label').attr('title', json.PRODNAME);
	var txtPrice = tr.find(':text[jqKey="PRICE"]');
	var txtQty = tr.find(':text[jqKey="QTY"]');
	txtPrice.val(Math.abs(parseInt(json.PRICE))).show();
	txtPrice.parent().find('span[jqKey="DisplayOnly"]').remove();
	if (typeof (json.IS_POS_DEF_PRICE) != 'undefined' && json.IS_POS_DEF_PRICE == 'N') {
		txtPrice.val(Math.abs(parseInt(json.PRICE))).hide();
		swapTextboxToLabel(txtPrice);
	}
	$('#' + rowId + '_8_label').text(parseInt(json.PRICE) * parseInt(json.QUANTITY)).show();

	if (json.IMEI_FLAG != '1') {
		if (typeof (json.IMEI) != 'undefined' && json.IMEI.length != 0)
			$('#' + rowId + '_10_imei').val(json.IMEI).removeAttr('title').show();
		else
			$('#' + rowId + '_10_imei').val('').removeAttr('title').show();
		$('#' + rowId + '_10_choose').hide();
		$('#' + rowId + '_10_delete').hide();
		setIMEIIcon(rowId, false);
	} else {
		$('#' + rowId + '_10_imei').val('').hide();
		$('#' + rowId + '_10_choose').hide();
		$('#' + rowId + '_10_delete').hide();
		$('#' + rowId + '_9').empty();
	}
	if (typeof (json.ISSTOCK) != 'undefined' && json.ISSTOCK == '1' && parseInt(json.ON_HAND_QTY) <= 0) {
		txtQty.val('0').attr('disabled', 'disabled');
		txtPrice.attr('disabled', 'disabled');
		$('#' + rowId + '_10_imei').val('').hide();
		$('#' + rowId + '_10_choose').hide();
		$('#' + rowId + '_10_delete').hide();
		$('#' + rowId + '_9').empty();
	} else {
		txtQty
			.numeric('option', 'maxValue', parseInt(json.ON_HAND_QTY))
			.numeric('option', 'title', '請輸入銷售數量, 目前庫存:' + json.ON_HAND_QTY)
			.val(json.QUANTITY)
			.removeAttr('disabled')
			.focus();
		txtPrice.removeAttr('disabled');
		if (parseInt(json.QUANTITY) == 1) {
			$('#' + rowId + '_10_choose').hide();
			$('#' + rowId + '_10_delete').hide();
			if (json.IMEI_OK == '0') {
				$('#' + rowId + '_10_imei').removeAttr('readonly');
			} else {
				$('#' + rowId + '_10_imei').attr('readonly', 'readonly');
				if (typeof(json.IS_LOCK) != 'undefined' && json.IS_LOCK == '1')
					$('#' + rowId + '_10_delete').hide();
				else
					$('#' + rowId + '_10_delete').show();
			}
		} else {
			$('#' + rowId + '_10_imei').attr('readonly', 'readonly');
			$('#' + rowId + '_10_choose').show();
			$('#' + rowId + '_10_delete').hide();
		}
	}
	if (typeof (json.IS_LOCK) != 'undefined' && json.IS_LOCK == '1') {
		tr.find(':checkbox').hide();
		tr.find(':button').hide();
		$.each(tr.find(':text'), function(i, o) {
			swapTextboxToLabel($(o));
		});
	}
}

function openInputIMEIDialog(o) {
	// IMEI 輸入視窗
	if (winInputIMEI != null)
		winInputIMEI.focus();
	var tr = $(o).parents('tr').first();
	var rowId = tr.attr('id');
	var rowJson = getRowData(rowId);
	setRowData(rowId, rowJson);
	winInputIMEI = openwindowByEncrypt('TSAL01_InputIMEI.aspx?rowid=' + rowId, 300, 450);
	//window.open('TSAL01_InputIMEI.aspx?rowid=' + rowId, 'InputIMEI', 'height=300px,width=450px,menubar=no,resizable=no,scrollbars=no,status=no,toolbar=no,location=no');
}

function imeiInputDone(rowId, v) {
	// 供 IMEI 輸入窗回傳資料用
	var tr = $('#' + rowId);
	tr.find('input[id="' + rowId + '_10_imei').val(v).attr('title', v);
	var rowJson = getRowData(rowId);
	rowJson.IMEI_OK = (v.split(',').length == parseInt(rowJson.QUANTITY)) ? '1' : '0';
	rowJson.IMEI = v;
	setRowData(rowId, rowJson);
	setIMEIIcon(rowId, rowJson.IMEI_OK == '1');
}

function deleteIMEICache(o) {
	// 刪除 IMEI, 單筆用
	if (!confirm('您是否要清除此筆 IMEI 資料 ?'))
		return;
	var tr = $(o).parents('tr').first();
	var rowId = tr.attr('id');
	ajaxDeleteIMEIItemRow(tr.find('input[id="' + rowId + '_10_imei').val(), rowId);
}

function setIMEIIcon(rowId, status) {
	// 設定 IMEI 圖示
	if (status)
		$('#' + rowId + '_9').empty().append('<img src="../../../Icon/check.png" />');
	else
		$('#' + rowId + '_9').empty().append('<img src="../../../Icon/non_complete.png" />');
}

function reassignElementID(gv) {
	// 重新指定 Grid 中所有元素的 ID
	var gvId = gv.attr('id');
	var rows = gv.find('tr[id^="' + gvId + '_"]');
	if (rows.length == 0) {
		gv.parents('div').first().find(':button[jqKey="btnAddItem"]').removeAttr('disabled');
		gv.parents('div').first().find(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
		gv.parents('div').first().find(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
	} else {
		$.each(rows, function(i, r) {
			var row = $(r);
			var rowId = gvId + '_' + (i + 1);
			row.attr('id', rowId);
			$.each(row.find('td'), function(j, o) {
				var cellId = rowId + '_' + j;
				var td = $(o);
				td.attr('id', cellId).removeClass().addClass('grid_row_' + ((i + 1) % 2));
				$.each(td.find('*[id]'), function(i, oo) {
					var elm = $(oo);
					var sp = elm.attr('id').split('_');
					elm.attr('id', cellId + '_' + sp[sp.length - 1]);
				});
			});
		});
		if (rows.length == 1) {
			gv.parents('div').first().find(':button[jqKey="btnHappyGoNet"]').removeAttr('disabled');
			gv.parents('div').first().find(':button[jqKey="btnStoreDiscount"]').removeAttr('disabled');
		}
	}
}

function enableFunctionButton() {
	// 開啟可使用之按鈕
	switch (hfGetOrigItems.val()) {
		case '5':
			// 歷史銷售資料
			break;
	}
	$(':button[jqKey="btnAddItem"]').removeAttr('disabled');
	$(':button[jqKey="btnDeleteItems"]').removeAttr('disabled');
}

function orderToSale() {
	// 預收轉銷售
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt('../SAL01/SAL01_inputPreOrderNumber.aspx?date=' + Date(), 520, 380);
	if (r == null || r == '' || typeof (r) == 'undefined')
		return false;
	alert(r);
}

function happyGoDiscount() {
	// HappyGo 折抵
	var amt = parseInt($('span[id="labTotalAmount"]').text());
	if (amt == 0) {
		alert('無應收總金額，請先填入商品料號');
		return;
	}

	var addProdList = '';
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutHG.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + amt + '&TRAN_DATE=' + $('span[jqKey="labTRADE_DATE"]').text() + '&addProdList=' + addProdList, 500, 550);
	if (r == null || r == '' || typeof (r) == 'undefined')
		return false;
	alert(r);
	if (r.length <= 0)
		return false;
	var content = "";
	for (i = 0; i < r.length; i++) {
		content += r[i] + "|";
	}
	return content;
}

function storeDiscount() {
	// 特殊抱怨折扣
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var amt = parseInt($('span[id="labTotalAmount"]').text());
	var tr = null;
	if (amt == 0) {
		alert('無應收總金額，請先選擇商品料號 !');
		var rows = $('#gvMaster tr[jqKey]');
		if (rows.length == 0)
			tr = createItemRow();
		else
			tr = $(rows[0]);
		tr.find(':text[jqKey="PRODNO"]').focus();
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CheckOutSM.aspx?date=' + Date() + '&TOTAL_AMOUNT=' + amt + '&TRAN_DATE=' + $('span[jqKey="labTRADE_DATE"]').text(), 500, 250);
	if (r == null || r == '' || typeof (r) == 'undefined')
		return false;
	// STOREDISCOUNT,料號,料名,金額,折扣%,STORE_DIS_REASON.STORE_DIS_REASON_ID,其他原因
	hasStoreDiscount = true;
	tr = $($('#gvMaster tr[jqKey]')[0]);
	var newJson = cloneEmptyObject(getRowData(tr));
	var arr = r.split(',');
	var discount = 0;
	if (arr[3].length == 0 && arr[4].length != 0)
		discount = -Math.floor(amt / 100 * parseFloat(arr[4]));
	else
		discount = -parseInt(arr[3]);
	with (newJson) {
		PRODNO = StoreDiscountProd.PRODNO;
		PRODNAME = StoreDiscountProd.PRODNAME;
		QUANTITY = '1';
		PRICE = discount.toString();
		ITEM_TYPE = '6';
		CAN_DELETE = '1';
		ON_HAND_QTY = '1';
		IMEI_FLAG = '1';
		IS_POS_DEF_PRICE = 'N';
	}

	tr = createItemRow();
	settingItemRow(newJson, tr.attr('id'));

	recountTotalAmount();
	lockItemGridInterface();
	disableProdItemInterface();
	//rollbackItemGrid();
	$(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
	$('#gvMaster :checkbox').show();
	$(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnDeleteItems"]').removeAttr('disabled');
	$(':button[jqKey="btnConfirm"]').removeAttr('disabled').focus();
}

function checkItemAdded() {
	var rows = $('#gvMaster tr[jqKey]');
	if (rows.length == 0 || hfGetOrigItems.val() == '5' || hfGetOrigItems.val() == '4')
		return;
	alert('資料未儲存, 請先儲存後方可執行此功能 !');
	event.returnValue = false;
	return false;
}
