var statusCode = {
	404: function() { alert('找不到網頁, 請通知系統管理員 !'); },
	500: function() { alert('程式錯誤, 請通知系統管理員 !'); }
};

function ajaxRequestProductInfo(prodNo, rowId, ignoreQty) {
	// 取得產品資料
	setWaitingMsg('divMasterMsg', '讀取商品資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=prod_info&prodno=' + prodNo + '&storeno=' + storeNo,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法取得商品資料 !');
				$('#' + rowId + '_4_text').focus();
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')')[0];
				if (typeof (json) == 'undefined') {
					alert('查無此料號(' + prodNo + ')之商品資料 !');
					$('#' + rowId + '_4_text').focus();
					return;
				}
			} catch (e) {
				alert(e + '\n\n' + data);
				$('#' + rowId + '_4_text').focus();
				return;
			}
			json.QUANTITY = '1';
			json.IMEI_OK = '0';
			json.CAN_DELETE = '1';
			settingItemRow(json, rowId, ignoreQty);

			recountTotalAmount();

			if (winProdSelecter != null)
				winProdSelecter.close();
			winProdSelecter = null;
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxCheckIMEIExists(imei, rowId) {
	// 檢查 IMEI 是否可使用, 僅提供單筆
	var gv = $('#gvMaster');
	var existsCount = 0;
	var tr = gv.find('#' + rowId);
	var rowJson = getRowData(rowId);
	$.each(gv.find('input[id$="_10_imei"]'), function() {
		if ($.trim($(this).val()).length != 0) {
			var nums = $(this).val().split(',');
			for (var i = 0; i < nums.length; i++) {
				if (nums[i].toUpperCase() == imei.toUpperCase())
					existsCount++;
			}
			if (existsCount > 1)
				return;
		}
	});
	if (existsCount > 1) {
		alert('此 IMEI(' + imei + ') 已選用 !');
		$('#' + rowId + '_10_imei').focus();
		setIMEIIcon(rowId, false);
		rowJson.IMEI_OK = '0';
		setRowData(rowId, rowJson);
		return;
	}
	setWaitingMsg('divMasterMsg', '檢核 IMEI 資料中...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=imei&imei=' + imei + '&detailid=' + rowJson.ID + '&prodno=' + rowJson.PRODNO,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法檢核 IMEI 資料 !');
				return;
			} else if (data.length <= 2) {
				alert('無此 IMEI(' + imei + ') 資料 !');
				$('#' + rowId + '_10_imei').focus();
			} else {
				var json = {};
				try {
					json = eval('(' + data + ')');
				} catch (e) {
					alert(e + '\n\n' + data);
					return;
				}
				if (json.MESSAGE.length != 0) {
					alert(json.MESSAGE);
					$('#' + rowId + '_10_imei').focus();
					rowJson.IMEI_OK = '0';
					rowJson.IMEI = '';
				} else {
					rowJson.IMEI_OK = '1';
					rowJson.IMEI = imei;
					$('#' + rowId + '_10_imei').attr('readonly', 'true');
					if (typeof(rowJson.IS_LOCK) == 'undefined' || rowJson.IS_LOCK != '1')
						$('#' + rowId + '_10_delete').show().focus();
				}
				setIMEIIcon(rowId, rowJson.IMEI_OK == '1');
				setRowData(rowId, rowJson);
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxSaveItemCache() {
	// 儲存品項快取
	setWaitingMsg('divMasterMsg', '儲存快取資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	var para = {
		POSUUID_MASTER: hfPOSUUID_MASTER.val(),
		POSUUID_DETAIL: hfPOSUUID_DETAIL.val(),
		SALE_DETAIL: [],
		DISCOUNT: discountInfo,
		TRADE_DATE: $('span[jqKey="labTRADE_DATE"]').text(),
		SAVE_CACHE: '1'
	};

	var gv = $('#gvMaster');
	var rows = gv.find('tr[id^="gvMaster_"]');
	$.each(rows, function() {
		var tr = $(this);
		var rowId = tr.attr('id');
		var rowJson = getRowData(tr);
		if (typeof (rowJson.IS_LOCK) == 'undefined' || rowJson.IS_LOCK != '1') {
			para.SALE_DETAIL[para.SALE_DETAIL.length] = {
				ID: rowJson.NEW_ID,
				PRODNO: rowJson.PRODNO,
				PRODNAME: rowJson.PRODNAME,
				UNIT: rowJson.UNIT,
				ISCONSIGNMENT: rowJson.ISCONSIGNMENT,
				PRICE: rowJson.PRICE,
				IS_DISCOUNT: rowJson.IS_DISCOUNT,
				INV_TYPE: rowJson.INV_TYPE,
				IMEI_FLAG: rowJson.IMEI_FLAG,
				POSUUID_DETAIL: rowJson.POSUUID_DETAIL,
				ITEM_TYPE: rowJson.ITEM_TYPE,
				QUANTITY: rowJson.QUANTITY,
				MSISDN: ((typeof (rowJson.MSISDN) == 'undefined') ? '' : rowJson.MSISDN),
				SOURCE_TYPE: ((typeof (rowJson.SOURCE_TYPE) == 'undefined') ? '' : rowJson.SOURCE_TYPE),
				SERVICE_SYS_ID: ((typeof (rowJson.SERVICE_SYS_ID) == 'undefined') ? '' : rowJson.SERVICE_SYS_ID),
				BARCODE1: ((typeof (rowJson.BARCODE1) == 'undefined') ? '' : rowJson.BARCODE1),
				BARCODE2: ((typeof (rowJson.BARCODE2) == 'undefined') ? '' : rowJson.BARCODE2),
				BARCODE3: ((typeof (rowJson.BARCODE3) == 'undefined') ? '' : rowJson.BARCODE3),
				BUNDLE_ID: ((typeof (rowJson.BUNDLE_ID) == 'undefined') ? '' : rowJson.BUNDLE_ID),
				// HappyGo
				HG_CARD_NO: ((typeof (rowJson.HG_CARD_NO) == 'undefined') ? '' : rowJson.HG_CARD_NO),
				HG_REDEEM_POINT: ((typeof (rowJson.HG_REDEEM_POINT) == 'undefined') ? '' : rowJson.HG_REDEEM_POINT),
				HG_RULE: ((typeof (rowJson.HG_RULE) == 'undefined') ? '' : rowJson.HG_RULE),
				HG_RULE_COMMON: ((typeof (rowJson.HG_RULE_COMMON) == 'undefined') ? '' : rowJson.HG_RULE_COMMON),
				HG_RULE_PRODUCT: ((typeof (rowJson.HG_RULE_PRODUCT) == 'undefined') ? '' : rowJson.HG_RULE_PRODUCT),
				HG_RULE_PROMOTION: ((typeof (rowJson.HG_RULE_PROMOTION) == 'undefined') ? '' : rowJson.HG_RULE_PROMOTION)
			};
			rowJson.IS_CONFIRM = '1';
			setRowData(tr, rowJson);
		}
	});
	function unlockConfirm() {
		$.each(rows, function() {
			var tr = $(this);
			var rowId = tr.attr('id');
			var rowJson = getRowData(tr);
			if (typeof (rowJson.IS_LOCK) == 'undefined' || rowJson.IS_LOCK != '1') {
				rowJson.IS_CONFIRM = '0';
				setRowData(tr, rowJson);
			}
		});
	}
	$.ajax({
		statusCode: statusCode,
		type: 'POST',
		url: 'TSAL03_SaveCache.aspx',
		data: { JSON: json2String(para) },
		datatype: 'json',
		success: function(data) {
			// JSON { RESULT:'1' } or { RESULT:'Error Message' }
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法儲存快取資料 !');
				unlockConfirm();
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				unlockConfirm();
				return;
			}
			if (json.RESULT != '1') {
				alert(json.RESULT);
				unlockConfirm();
				return;
			} else {
				lockItemGridInterface();
				disableProdItemInterface();
				enableCheckoutInterface();
				showDiscount(json.Discount);
				checkItemCancelButtonEnable();
				recountCheckoutTotalAmount();
				$(':button[jqKey="btnCash"]').focus();
				//$('span[jqKey="labSale_Status"]').text('暫存');
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			unlockConfirm();
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxCancelItemCache() {
	// 刪除已儲存的品項快取
	var para = {
		POSUUID_MASTER: hfPOSUUID_MASTER.val(),
		SAVE_CACHE: '0'
	};

	setWaitingMsg('divMasterMsg', '刪除快取資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'POST',
		url: 'TSAL03_SaveCache.aspx',
		data: { JSON: json2String(para) },
		datatype: 'json',
		success: function(data) {
			// JSON { RESULT:'1' } or { RESULT:'Error Message' }
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法刪除快取資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			if (json.RESULT != '1') {
				alert(json.RESULT);
				return;
			} else {
				rollbackItemGrid();
				$('#labTotalPayable').text('0');
				$('#labChange').text('0');
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxLoadCache() {
	// 載入快取資料
	setWaitingMsg('divMasterMsg', '讀取快取資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=cache&masterid=' + hfPOSUUID_MASTER.val(),
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法讀取快取資料 !');
				return;
			}
			var json = {};
			try {
				//$('#txtJsonReturn').val(data).show();
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			try {
				if (typeof (json.Head) != 'undefined') {
					if (typeof (json.Head.TRADE_DATE) != 'undefined')
						$('span[jqKey="labTRADE_DATE"]').text(json.Head.TRADE_DATE);
				}
				$.each(json.Products, function(i, v) {
					var tr = createItemRow();
					if (v.PRODNO == StoreDiscountProd.PRODNO) {
						with (v) {
							CAN_DELETE = '1';
							ON_HAND_QTY = '1';
							IMEI_FLAG = '1';
							IS_POS_DEF_PRICE = 'N';
						}
						hasStoreDiscount = true;
					}
					settingItemRow(v, tr.attr('id'));
				});
				var gv = $('#gvMaster');
				$.each(json.IMEIs, function(i, v) {
					var tr = gv.find(':hidden[id$="_did"][value="' + v.ID + '"]').parents('tr').first();
					var rowId = tr.attr('id');
					var rowJson = getRowData(rowId);
					rowJson.IMEI = v.IMEI;
					rowJson.IMEI_OK = '1';
					setIMEIIcon(rowId, true);
					tr.find('#' + rowId + '_10_imei').val(v.IMEI);
					setRowData(rowId, rowJson);
				});
				if (typeof (json.PaidCache) != 'undefined') {
					$.each(json.PaidCache, function(i, v) {
						var tr = createCheckoutRow();
						setRowData(tr, v);
						var desc = json.PAID_MODE + ',' + v.PAID_AMOUNT + ',' + v.DESCRIPTION;
						tr.data('Description', desc);
						tr.find('td:eq(0) input[id$="_id"]').val(v.ID);
						tr.find('td:eq(1)').text(' ');
						tr.find('td:eq(2)').text(getPaidTypeName(parseInt(v.PAID_MODE)));
						tr.find('td:eq(3)').text(v.PAID_AMOUNT);
						tr.find('td:eq(4)').text(v.DESCRIPTION);
					});
				}
				showDiscount(json.Discount);

				recountTotalAmount();
				recountCheckoutTotalAmount();
				lockItemGridInterface();
				disableProdItemInterface();
				enableCheckoutInterface();
				if (typeof (json.PaidCache) != 'undefined' && json.PaidCache.length != 0)
					$(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
				$('span[jqKey="labSale_Status"]').text('暫存');
			} catch (e) {
				alert('資料損毀, 無法載入已支付的快取資料!\n\n請與總公司連絡!\n\n' + e);
				$('input').attr('disabled', 'disabled');
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxLoadToClose() {
	// 載入未結資料
	setWaitingMsg('divMasterMsg', '讀取未結資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=to_close&cid=' + hfPOSUUID_DETAIL.val(),
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法讀取未結資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			try {
				$.each(json.Products, function(i, v) {
					var tr = createItemRow();
					var rowId = tr.attr('id');
					settingItemRow(v, rowId);
					var rowJson = getRowData(rowId);
					rowJson.CAN_DELETE = '0';
					rowJson.IS_TO_CLOSE = '1';
					if (rowJson.IMEI_FLAG != '1') {
						rowJson.IMEI = '';
						rowJson.IMEI_OK = '0';
					}
					setRowData(tr, rowJson);
				});
				showDiscount(json.Discount);

				recountTotalAmount();
				lockItemGridInterface();
				disableProdItemInterface();
				rollbackItemGrid();
				$('span[jqKey="labSale_Status"]').text('未結');
			} catch (e) {
				alert('資料損毀, 無法載入未結資料!\n\n請與總公司連絡!\n\n' + e);
				$('input').attr('disabled', 'disabled');
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxDeleteCacheItem(ids) {
	// ids : ID1;ID2;ID3... , ID : SALE_DETAIL.ID
	// 刪除快取中的品項資料, 目前並無使用
	setWaitingMsg('divMasterMsg', '刪除品項資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=del_item&masterid=' + hfPOSUUID_MASTER.val() + '&cacheid=' + ids,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法刪除品項資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			if (json.RESULT != '1')
				alert(json.ERROR);
			else
				deleteItemRowUI();
			setWaitingMsg('divMasterMsg', '');
		},
		error: function(e, xhr, settings, exception) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxDeleteIMEIItemRow(imei, rowId) {
	// 刪除已被鎖定的 IMEI 資料, 單筆用
	setWaitingMsg('divMasterMsg', '解除鎖定 IMEI 資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	var rowJson = getRowData(rowId);
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=del_imei&detailid=' + rowJson.ID + '&imeis=' + imei.replace(',', ';'),
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法解除鎖定資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			if (json.RESULT != '1') {
				alert(json.ERROR);
			} else {
				var tr = $(rowId);
				$('input[id="' + rowId + '_10_imei')
					.removeAttr('readonly')
					.val('')
					.focus();
				$('input[id="' + rowId + '_10_delete').hide();
				rowJson.IMEI = '';
				rowJson.IMEI_OK = '0';
				setRowData(rowId, rowJson);
				setIMEIIcon(rowId, false);
			}
		},
		error: function(e, xhr, settings, exception) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxCheckout() {
	// 結帳
	$('#divCheckoutMsg').text('結帳中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$(':button[jqKey="btnCheckout"]').attr('disabled', 'disabled');
	var para = {
		ORIGINAL_MASTER: hfOriginal_MASTER.val(),
		POSUUID_MASTER: hfPOSUUID_MASTER.val(),
		POSUUID_DETAIL: hfPOSUUID_DETAIL.val(),
		ITEM_TYPE: '2',
		CHECKOUT: [],
		CANCEL_TRANS: '0',
		UNI_NO: $(':text[jqKey="txtUNI_NO"]').val(),
		UNI_TITLE: $(':text[jqKey="txtUNI_TITLE"]').val(),
		REMARK: $(':text[jqKey="txtREMARK"]').val()
	};

	var gv = $('#gvCheckout');
	var rows = gv.find('tr[id^="gvCheckout_"]');
	if ($('#labChange').text() != '0') {
		para.CHECKOUT[para.CHECKOUT.length] = {
			TYPE: '8',
			AMOUNT: '-' + Math.abs(parseInt($('#labChange').text())),
			DESC: '找零'
		};
	}
	$.ajax({
		statusCode: statusCode,
		type: 'POST',
		url: 'TSAL03_Checkout.aspx',
		data: { JSON: json2String(para) },
		datatype: 'json',
		success: function(data) {
			$('#divCheckoutMsg').text('');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法結帳 !');
				$(':button[jqKey="btnCheckout"]').removeAttr('disabled')
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				$(':button[jqKey="btnCheckout"]').removeAttr('disabled')
				return;
			}
			if (json.RESULT != '1') {
				alert(json.RESULT);
				$(':button[jqKey="btnCheckout"]').removeAttr('disabled')
				return;
			} else {
				if (typeof (json.SALE_NO) != 'undefined')
					$('span[jqKey="lbSALE_NO"]').text(json.SALE_NO);
				if (typeof (json.INVOICE_NO) != 'undefined') {
					$('#labInvoice_No').empty();
					if (json.INVOICE_NO.length == 0) {
						json.INVOICE_NO = '收據';
						$('span[jqKey="labVOUCHER_TYPE"]').text('收據');
					} else {
						$('<a/>').attr({ 'href': json.INVOICE_URL, 'target': '_blank' }).text(json.INVOICE_NO).appendTo($('#labInvoice_No'));
					}
				}
				lockCheckoutGrid();
				if (typeof (json.INVOICE_URL) != 'undefined' && json.INVOICE_URL.length != 0)
					$('#pdfFrame').attr('src', json.INVOICE_URL);
				$('table[jqKey="tabHeader"] :text[jqKey]')
					.attr('readonly', 'true')
					.css({ 'border-width': '0px', 'cursor': 'default' });
				$('span[jqKey="labSale_Status"]').text('已結帳');
				hfGetOrigItems.val('4');
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			$('#divCheckoutMsg').text('');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxSavePaidCache(data) {
	// 新增支付快取
	var arr = data.split(',');
	var paidType = getPaidType(arr[0]);
	if (paidType == 2 && arr.length == 8)
		paidType = 4;
	arr[0] = paidType.toString();
	$('#divCheckoutMsg').text('儲存支付快取中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=add_paid&masterid=' + hfPOSUUID_MASTER.val() + '&data=' + arr.join(':'),
		success: function(data) {
			$('#divCheckoutMsg').text('');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法新增支付快取資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			if (json.RESULT != '1') {
				alert(json.ID);
			} else {
				try {
					var tr = createCheckoutRow();
					setRowData(tr, json);
					tr.find('td:eq(0) input[id$="_id"]').val(json.ID);
					tr.find('td:eq(1)').text(' ');
					tr.find('td:eq(2)').text(getPaidTypeName(paidType));
					tr.find('td:eq(3)').text(arr[1]);
					switch (paidType) {
						case 1:
							tr.find('td:eq(4)').text(getPaidTypeName(paidType));
							break;
						case 2:
							tr.find('td:eq(4)').text('信用卡號:' + arr[2] + ',序號:' + arr[3] + ',調閱編號:' + arr[4]);
							break;
						case 3:
							tr.find('td:eq(4)').text('信用卡號:' + arr[2] + ',授權碼:' + arr[3]);
							break;
						case 4:
							tr.find('td:eq(4)').text('信用卡號:' + arr[2] + ',序號:' + arr[3] + ',調閱編號:' + arr[4] + ',銀行別:' + arr[5] + ',分期期數:' + arr[7]);
							break;
						case 6:
							tr.find('td:eq(4)').text('金融卡號:' + arr[2] + ',序號:' + arr[3]);
							break;
						default:
							break;
					}
					var desc = paidType + ',' + arr[1] + ',' + tr.find('td:eq(4)').text();
					tr.data('Description', desc);
					recountCheckoutTotalAmount();
				} catch (e) {
					alert('支付項目無法新增, 請與總公司連絡 !');
					return;
				}
			}
		},
		error: function(e, xhr, settings, exception) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxDeletePaidCache(ids) {
	// 刪除支付快取
	$('#divCheckoutMsg').text('刪除支付快取中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=del_paid&masterid=' + hfPOSUUID_MASTER.val() + '&pid=' + ids,
		success: function(data) {
			$('#divCheckoutMsg').text('');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法刪除支付快取資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			if (json.RESULT != '1') {
				alert(json.RESULT);
			} else {
				$.each(ids.split(';'), function(i, v) {
					$(':hidden[value="' + v + '"]').parents('tr').first().remove();
				});
				reassignElementID($('#gvCheckout'));
				checkItemCancelButtonEnable();
				recountCheckoutTotalAmount();
				$('#chkAllCheckout').removeAttr('checked');
			}
		},
		error: function(e, xhr, settings, exception) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxRequestPromotionProducts(promoCode, detailId, prods) {
	// 取得組合促銷的商品資料
	setWaitingMsg('divMasterMsg', '讀取組合促銷商品資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=promo_prod&pmno=' + promoCode + '&did=' + detailId + '&pids=' + prods,
		success: function(data) {
			$('#divMasterMsg').text('');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法讀取組合促銷商品資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			if (json.RESULT != '1') {
				alert(json.ERROR);
			} else {
				$.each(json.Products, function(i, v) {
					var tr = createItemRow();
					var rowId = tr.attr('id');
					settingItemRow(v, rowId);
					var rowJson = getRowData(rowId);
					rowJson.CAN_DELETE = '0';
					if (rowJson.IMEI_FLAG != '1') {
						rowJson.IMEI = '';
						rowJson.IMEI_OK = '0';
					}
					setRowData(tr, rowJson);
				});
				recountTotalAmount();
				lockItemGridInterface();
				disableProdItemInterface();
				rollbackItemGrid();
				
				showDiscount(json.Discount);
			}
		},
		error: function(e, xhr, settings, exception) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxSaleHistory() {
	// 取得銷售歷史資料
	setWaitingMsg('divMasterMsg', '讀取銷售歷史資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=sale_history&masterid=' + hfOriginal_MASTER.val(),
		success: function(data) {
			$('#divMasterMsg').text('');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法讀取銷售歷史資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			if (typeof (json.ERROR) != 'undefined') {
				alert(json.ERROR);
			} else {
				if (typeof (json.Products) != 'undefined') {
					var editProducts = cloneJson(json.Products);
					for (var x = 0; x <= 1; x++) {
						$.each(json.Products, function(i, jv) {
							var v = cloneJson(jv);
							var tr = createItemRow();
							var rowId = tr.attr('id');
							v.IS_LOCK = (x == 0) ? '1' : '0';
							v.CAN_DELETE = x.toString();
							if (typeof (v.IMEI_FLAG) != 'undefined' && v.IMEI_FLAG != '1') {
								if (typeof (json.IMEIs) != 'undefined') {
									var arr = $.map(json.IMEIs, function(jj) { return (jj.ID == v.ID) ? jj : null; });
									if (arr.length != 0) {
										v.IMEI = arr[0].IMEI;
										v.IMEI_OK = '1';
									}
								}
							} else {
								v.IMEI_OK = '0';
							}
							settingItemRow(v, rowId);
							if (typeof (v.IMEI_FLAG) != 'undefined' && v.IMEI_FLAG != '1')
								setIMEIIcon(rowId, (v.IMEI_OK == '1'));
						});
					}
					//recountTotalAmount();
					$('#gvMaster :text[jqKey]:visible:first').focus();
				}
				showDiscount(json.Discount);
				//lockItemGridInterface();
				//disableProdItemInterface();
				if (typeof (json.PaidCache) != 'undefined') {
					$.each(json.PaidCache, function(i, jv) {
						var v = cloneJson(jv);
						var tr = createCheckoutRow();
						v.IS_LOCK = '1';
						setRowData(tr, v);
						var desc = json.PAID_MODE + ',' + v.PAID_AMOUNT + ',' + v.DESCRIPTION;
						tr.data('Description', desc);
						tr.find('td:eq(0) input[id$="_id"]').val(v.ID);
						tr.find('td:eq(1)').text('作廢');
						tr.find('td:eq(2)').text(getPaidTypeName(parseInt(v.PAID_MODE)));
						tr.find('td:eq(3)').text(v.PAID_AMOUNT);
						tr.find('td:eq(4)').text(v.DESCRIPTION);
						tr.find(':checkbox').hide();
					});
					//lockCheckoutGrid();
				}
				$(':button[jqKey="btnCheckout"]').attr('disabled', 'disabled');
				//$(':button[jqKey="btnAddItem"]').removeAttr('disabled');
				//$(':button[jqKey="btnDeleteItems"]').removeAttr('disabled');
			}
		},
		error: function(e, xhr, settings, exception) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});

}