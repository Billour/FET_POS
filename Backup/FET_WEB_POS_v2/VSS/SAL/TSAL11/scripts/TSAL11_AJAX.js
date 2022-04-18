﻿function ajaxRequestProductInfo(prodNo, tr) {
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
	            tr.find(':text[jqKey="PRODNO"]').focus();
	            return;
	        }
	        var json = {};
	        try {
	            json = eval('(' + data + ')')[0];
	            if (typeof (json) == 'undefined') {
	                alert('查無此料號(' + prodNo + ')之商品資料 !');
	                tr.find(':text[jqKey="PRODNO"]').focus();
	                return;
	            }
	        } catch (e) {
	            alert(e + '\n\n' + data);
	            tr.find(':text[jqKey="PRODNO"]').focus();
	            return;
	        }
	        json.QUANTITY = '1';
	        json.IMEI_OK = '0';
	        json.IS_ETC = '0';
	        json.CAN_DELETE = '1';
	        var rowJson = getRowData(tr);
	        if (rowJson != null && typeof (rowJson.POSUUID_DETAIL) != 'undefined' && rowJson.POSUUID_DETAIL.length != 0)
	            json.POSUUID_DETAIL = rowJson.POSUUID_DETAIL;
	        if (hfActionType.val() == '2' && rowJson != null && ((typeof (rowJson.IS_SIM) != 'undefined' && rowJson.IS_SIM == '1') || (typeof (rowJson.PRODNO) != 'undefined' && rowJson.PRODNO == 'SIM'))) {
	            var tmp = cloneJson(rowJson);
	            tmp.PRODNO = json.PRODNO;
	            tmp.PRODNAME = json.PRODNAME;
	            tmp.IS_SIM = '1';
	            json = cloneJson(tmp);
	        }
	        settingItemRow(json, tr);

	        recountTotalAmount();
	        tr.find(':text[jqKey="QTY"]').focus();
	    },
	    error: function(jqXHR, textStatus, errorThrown) {
	        alert('ERROR : \n' + errorThrown);
	        setWaitingMsg('divMasterMsg', '');
	        onAjaxRequest = false;
	        restoreAllButtonStatus();
	    }
	});
}

function ajaxCheckIMEIExists(imei, tr) {
	// 檢查 IMEI 是否可使用, 僅提供單筆
	var gv = $('#gvMaster');
	var existsCount = 0;
	var rowJson = getRowData(tr);
	$.each(gv.find(':text[jqKey="IMEI"]'), function() {
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
		tr.find(':text[jqKey="IMEI"]').focus();
		setIMEIIcon(tr, false);
		rowJson.IMEI_OK = '0';
		setRowData(tr, rowJson);
		return;
	}
	setWaitingMsg('divMasterMsg', '檢核 IMEI 資料中...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=imei&imei=' + imei + '&did=' + rowJson.ID + '&prodno=' + rowJson.PRODNO,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法檢核 IMEI 資料 !');
				return;
			} else if (data.length <= 2) {
				alert('無此 IMEI(' + imei + ') 資料 !');
				tr.find(':text[jqKey="IMEI"]').focus();
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
					tr.find(':text[jqKey="IMEI"]').focus();
					rowJson.IMEI_OK = '0';
					rowJson.IMEI = '';
				} else {
					rowJson.IMEI_OK = '1';
					rowJson.IMEI = imei;
					tr.find(':text[jqKey="IMEI"]').attr('readonly', 'true');
					tr.find(':button[jqKey="DELETE_IMEI"]').show().focus();
				}
				setIMEIIcon(tr, rowJson.IMEI_OK == '1');
				setRowData(tr, rowJson);
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

function ajaxSaveItemCache(async) {
	// 儲存品項快取(Ajax 同步執行, 會等待回傳結果)
	// async : 是否使用非同步執行, true: 非同步; false: 同步(會等待)
	if (typeof (async) == 'undefined')
		async = true;
	//setWaitingMsg('divMasterMsg', '儲存快取資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	var para = {
		POSUUID_MASTER: hfPOSUUID_MASTER.val(),
		POSUUID_DETAIL: hfPOSUUID_DETAIL.val(),
		SALE_TYPE: hfSALE_TYPE.val(),
		SALE_DETAIL: [],
		DISCOUNT: discountInfo,
		TRADE_DATE: $('span[jqKey="labTRADE_DATE"]').text(),
		SAVE_CACHE: '1',
		SALE_STATUS: '8'
	};
    
	var gv = $('#gvMaster');
	var rows = gv.find('tr[jqKey]');
	$.each(rows, function() {
		var tr = $(this);
		var rowJson = getRowData(tr);
		if (typeof (rowJson.IS_UNLESS) == 'undefined' || rowJson.IS_UNLESS != '1') {
		    // DISCOUNT_TYPE:2舊機回收, DISCOUNT_TYPE:3租賃
		    if (rowJson.DISCOUNT_TYPE == '2') {
		        rowJson.ITEM_TYPE = '12';
		    }
		    else if (rowJson.DISCOUNT_TYPE == '3') {
		        rowJson.ITEM_TYPE = '11';
		    }
			para.SALE_DETAIL[para.SALE_DETAIL.length] = {
				ID: (hfActionType.val() == '5') ? rowJson.NEW_ID : rowJson.ID,
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
				FUN_ID: ((typeof (rowJson.FUN_ID) == 'undefined') ? '' : rowJson.FUN_ID),
				CARD_NO: ((typeof (rowJson.CARD_NO) == 'undefined') ? '' : rowJson.CARD_NO),
				MSISDN: ((typeof (rowJson.MSISDN) == 'undefined') ? '' : rowJson.MSISDN),
				SOURCE_TYPE: ((typeof (rowJson.SOURCE_TYPE) == 'undefined') ? '' : rowJson.SOURCE_TYPE),
				SERVICE_SYS_ID: ((typeof (rowJson.SERVICE_SYS_ID) == 'undefined') ? '' : rowJson.SERVICE_SYS_ID),
				BARCODE1: ((typeof (rowJson.BARCODE1) == 'undefined') ? '' : rowJson.BARCODE1),
				BARCODE2: ((typeof (rowJson.BARCODE2) == 'undefined') ? '' : rowJson.BARCODE2),
				BARCODE3: ((typeof (rowJson.BARCODE3) == 'undefined') ? '' : rowJson.BARCODE3),
				BUNDLE_ID: ((typeof (rowJson.BUNDLE_ID) == 'undefined') ? '' : rowJson.BUNDLE_ID),
				PROMOTION_CODE: ((typeof (rowJson.PROMOTION_CODE) == 'undefined') ? '' : rowJson.PROMOTION_CODE),
				SIM_CARD_NO: ((typeof (rowJson.SIM_CARD_NO) == 'undefined') ? '' : rowJson.SIM_CARD_NO),
				ORI_UNIT_PRICE: ((typeof (rowJson.ORI_UNIT_PRICE) == 'undefined') ? '' : rowJson.ORI_UNIT_PRICE),
				BILLING_ACCOUNT_ID: ((typeof (rowJson.BILLING_ACCOUNT_ID) == 'undefined') ? '' : rowJson.BILLING_ACCOUNT_ID),
				SUBSCRIBE_NO: ((typeof (rowJson.SUBSCRIBE_NO) == 'undefined') ? '' : rowJson.SUBSCRIBE_NO),
				HRS_NO: ((typeof (rowJson.HRS_NO) == 'undefined') ? '' : rowJson.HRS_NO),
				//TO_CLOSE
				TRANS_TYPE: ((typeof (rowJson.TRANS_TYPE) == 'undefined') ? '' : rowJson.TRANS_TYPE),
				DATA: ((typeof (rowJson.DATA) == 'undefined') ? '' : rowJson.DATA),
				VOICE: ((typeof (rowJson.VOICE) == 'undefined') ? '' : rowJson.VOICE),
				R_RATE: ((typeof (rowJson.R_RATE) == 'undefined') ? '' : rowJson.R_RATE),
				// HappyGo
				HG_CARD_NO: ((typeof (rowJson.HG_CARD_NO) == 'undefined') ? '' : rowJson.HG_CARD_NO),
				HG_REDEEM_POINT: ((typeof (rowJson.HG_REDEEM_POINT) == 'undefined') ? '' : rowJson.HG_REDEEM_POINT),
				HG_RULE: ((typeof (rowJson.HG_RULE) == 'undefined') ? '' : rowJson.HG_RULE),
				HG_RULE_COMMON: ((typeof (rowJson.HG_RULE_COMMON) == 'undefined') ? '' : rowJson.HG_RULE_COMMON),
				HG_RULE_PRODUCT: ((typeof (rowJson.HG_RULE_PRODUCT) == 'undefined') ? '' : rowJson.HG_RULE_PRODUCT),
				HG_RULE_PROMOTION: ((typeof (rowJson.HG_RULE_PROMOTION) == 'undefined') ? '' : rowJson.HG_RULE_PROMOTION),
				// 特殊抱怨折扣
				SH_DISCOUNT_REASON: ((typeof (rowJson.SH_DISCOUNT_REASON) == 'undefined') ? '' : rowJson.SH_DISCOUNT_REASON),
				SH_DISCOUNT_DESC: ((typeof (rowJson.SH_DISCOUNT_DESC) == 'undefined') ? '' : rowJson.SH_DISCOUNT_DESC)		
			};
		}
	});
	var result = false;
	$.ajax({
		statusCode: statusCode,
		type: 'POST',
		url: '../TSAL01/TSAL01_SaveCache.aspx',
		data: { JSON: json2String(para) },
		datatype: 'json',
		async: async,
		success: function(data) {
			// JSON { RESULT:'1' } or { RESULT:'Error Message' }
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法儲存快取資料 !');
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
				onSaveCacheMode = true;
				showDiscount(json.Discount);
				lockItemGridInterface();
				disableProdGridButtons();
				disableProdGridEditMode();
				enableCheckoutInterface();
				var paid = recountCheckoutTotalAmount();
				$(':button[jqKey="btnCash"]').focus();
				$('span[jqKey="labSale_Status"]').text('暫存');
				if (paid <= 0) {
					ajaxSavePaidCache('CASH,0', false);
					$('#gvCheckout tr[jqKey] :checkbox').hide();
					disableCheckoutInterface();
				}
				result = true;
	            ReloadCache();
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
	    
	return result;
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
		url: '../TSAL01/TSAL01_SaveCache.aspx',
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
				onSaveCacheMode = false;
				showDiscount(null);
				rollbackItemGrid();
				$('#labTotalPayable').text('0');
				$('#labChange').text('0');
				if (hasETCItem)
					checkCancelItemLastIsETC();
				if (hfActionType.val() == '2')
					$('span[jqKey="labSale_Status"]').text('未結');
				else
					$('span[jqKey="labSale_Status"]').text('');
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
				onSaveCacheMode = true;
				$.each(json.Products, function(i, v) {
					var tr = createItemRow();
					if (v.PRODNO == ETC_ProdNo) {
						hasETCItem = true;
						v.IS_ETC = '1';
					}
					if (v.PRODNO == StoreDiscountProd.PRODNO) {
						with (v) {
							CAN_DELETE = '1';
							IS_ETC = '0';
							ON_HAND_QTY = '1';
							IMEI_FLAG = '1';
							IS_OPEN_PRICE = 'N';
						}
					}
					if (v.BARCODE1.length != 0)
						v.BILL_TYPE = checkBillType(v);
					settingItemRow(v, tr);
				});
				var gv = $('#gvMaster');
				$.each(json.IMEIs, function(i, v) {
					var tr = gv.find(':hidden[jqKey="ITEM_ID"][value="' + v.ID + '"]').parents('tr').first();
					var rowJson = getRowData(tr);
					rowJson.IMEI = v.IMEI;
					rowJson.IMEI_OK = '1';
					setIMEIIcon(tr, true);
					tr.find(':text[jqKey="IMEI"]').val(v.IMEI);
					setRowData(tr, rowJson);
				});
				if (typeof (json.PaidCache) != 'undefined') {
					$.each(json.PaidCache, function(i, v) {
						if (parseInt(v.PAID_MODE) == 3) {
							v.IS_LOCK = '1';
							enablePaidButton($(':button[jqKey="btnOffLineCreditCard"]'));
						}
						var tr = createCheckoutRow();
						settingCheckoutRow(tr, v);
					});
				}
				showDiscount(json.Discount);

				recountTotalAmount();
				recountCheckoutTotalAmount();
				lockItemGridInterface();
				disableProdGridButtons();
				disableProdGridEditMode();
				enableCheckoutInterface();
				if (typeof (json.PaidCache) != 'undefined' && json.PaidCache.length != 0) {
					$(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
					$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
				}
				$('span[jqKey="labSale_Status"]').text('暫存');
			} catch (e) {
				alert('無法載入快取資料, 資料可能已損毀 !\n\n請與總公司連絡!\n\n' + e);
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
				var hasSIMIndex = -1;
				hasInstead = false;
				$.each(json.Products, function(i, v) {
					v.CAN_DELETE = '0';
					v.IS_TO_CLOSE = '1';
					v.IS_OPEN_PRICE = 'N';
					if (v.IMEI_FLAG != '1') {
						v.IMEI = '';
						v.IMEI_OK = '0';
					}
					if (v.PRODNO == 'SIM') {
						v.PRODNAME = '請輸入 SIM 卡料號';
						v.IS_LOCK = '1';
						v.IS_SIM = '1';
						hasSIMIndex = i;
					}
					if (v.BARCODE1.length != 0) {
						v.BILL_TYPE = checkBillType(v);
						hasInstead = true;
						v.IS_LOCK = '1';
					}
					if (v.PRODNO == ETC_ProdNo || (typeof (v.BILL_TYPE) != 'undefined' && v.BILL_TYPE == 'ETC')) {
						v.IS_LOCK = '1';
						v.IS_ETC = '1';
					}
					var tr = createItemRow();
					settingItemRow(v, tr);
					if (hasInstead)
						tr.find(':checkbox[jqKey="CHECK"]').hide();
				});
				showDiscount(json.Discount);
				recountTotalAmount();

				$('span[jqKey="labSale_Status"]').text('未結');
				if (hasSIMIndex != -1) {
					alert('SIM 卡料號尚未輸入, 請先輸入 !');
					$('#gvMaster tr[jqKey]:eq(' + hasSIMIndex + ') :text[jqKey="PRODNO"]').focus();
				}
				$(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
				if (json.Products.length <= 0) {
					$(':button[jqKey="btnConfirm"]').attr('disabled', 'disabled');
					$(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
					$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
					alert('本筆未結交易, 並無任何品項 !');
				}
				if (parseInt(hfSALE_TYPE.val()) == 10) {
					disableProdGridButtons();
					$(':button[jqKey="btnConfirm"]').removeAttr('disabled');
					$('#gvMaster :checkbox[jqKey="CHECK"]').hide();
				}
				var isLock = (typeof (json.LOCK) != 'undefined' && json.LOCK == '1');
				if (hasInstead) {
					// 只要有代收, 則 All Button Disable
					$('#gvMaster').parents('table:first').find('table:first').find(':button').attr('disabled', 'disabled');
					$('#chkAllRow').hide();
				}
				if (isLock)
					ajaxSaveItemCache(false);
			} catch (e) {
				alert('無法載入未結資料, 資料可能已經損毀 !\n\n請與總公司連絡!\n\n' + e);
				$('input').attr('disabled', 'disabled');
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			//$('#txtJsonReturn').val('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxDeleteIMEIItemRow(imei, tr) {
	// 刪除已被鎖定的 IMEI 資料, 單筆用
	var gv = $('#gvMaster');
	setWaitingMsg('divMasterMsg', '解除鎖定 IMEI 資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	var rowJson = getRowData(tr);
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=del_imei&did=' + rowJson.ID + '&imeis=' + imei.replace(',', ';'),
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
				tr.find(':text[jqKey="IMEI"]')
					.removeAttr('readonly')
					.val('')
					.focus();
				tr.find(':button[jqKey="DELETE_IMEI"]').hide();
				rowJson.IMEI = '';
				rowJson.IMEI_OK = '0';
				setRowData(tr, rowJson);
				setIMEIIcon(tr, false);
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
	setWaitingMsg('divMasterMsg', '結帳中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$(':button[jqKey="btnCheckout"]').attr('disabled', 'disabled');
	//↓取得發票日期
	//$('#ctl00_MainContentPlaceHolder_txtInv_Date_I').val(); 
	var invoiceType = '';
	if ($(':radio[jqKey="rbINVOICE_TYPE2"]').attr('checked')==true) {
	    invoiceType = '03';
	}
	else if ($(':radio[jqKey="rbINVOICE_TYPE3"]').attr('checked')==true) {
	    invoiceType = '04';
	}
	var para = {
		ORIGINAL_MASTER: hfOriginal_MASTER.val(),
		POSUUID_MASTER: hfPOSUUID_MASTER.val(),
		POSUUID_DETAIL: hfPOSUUID_DETAIL.val(),
		CHECKOUT: [],
		CANCEL_TRANS: '0',
		UNI_NO: $(':text[jqKey="txtUNI_NO"]').val(),
		UNI_TITLE: $(':text[jqKey="txtUNI_TITLE"]').val(),
		REMARK: $(':text[jqKey="txtREMARK"]').val(),
		INVOICE_NO: $(':text[jqKey="txtInvoice_No"]').val(),
		INVOICE_DATE: $('#ctl00_MainContentPlaceHolder_txtInv_Date_I').val(),
		INVOICE_TYPE: invoiceType
	};
	/*
	if (hfActionType.val() == '5') {
		para.ITEM_TYPE = '2';
	}
	*/
	var gv = $('#gvCheckout');
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
		url: 'TSAL11_Checkout.aspx',
		data: { JSON: json2String(para) },
		datatype: 'json',
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
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
				// 回傳的 HG_POINT 大於 0 時, 執行累點程序
				if (typeof (json.HG_POINT) != 'undefined' && parseInt(json.HG_POINT) != 0) {
					if (parseInt(hfSALE_TYPE.val()) != 2) {
						// 如果不是代收品項, 則進行累點判斷
						if ($('#labHG_CARD_NO').text().length != 0) {
							// 如有 HappyGo 卡號, 則開啟累點介面
							openHappyGoRewardWindow($('#labHG_CARD_NO').text(), json.HG_POINT);
						} else if (confirm('請詢問消費者是否持有 HappyGo 卡, 以便累積消費點數 ?')) {
							// 如沒有 HappyGo 卡號, 則詢問是否需要累點, 並開啟累點介面
							openHappyGoRewardWindow('', json.HG_POINT);
						}
					}
				}
				// 回填交易序號
				if (typeof (json.SALE_NO) != 'undefined')
					$('span[jqKey="labSALE_NO"]').text(json.SALE_NO);
				// 回填發票號碼
				if (typeof (json.INVOICE_NO) != 'undefined') {
					$('#labInvoice_No').empty();
					if (json.INVOICE_NO.length == 0) {
						json.INVOICE_NO = '收據';
						$('span[jqKey="labVOUCHER_TYPE"]').text('收據');
					} else {
					    $('span[jqKey="labInvoice_No"]').text(json.INVOICE_NO);
						//$('<a/>')
						//	.attr({ 'href': json.INVOICE_URL, 'target': '_blank' })
						//	.text(json.INVOICE_NO)
						//	.appendTo($('#labInvoice_No'));
					}
				}
				lockCheckoutGrid();
				var hasInvoice = (typeof (json.INVOICE_URL) != 'undefined' && json.INVOICE_URL.length != 0);
				var hasReceipt = (typeof (json.RECEIPT_URL) != 'undefined' && json.RECEIPT_URL.length != 0);
				if (hasInvoice && !hasReceipt)
					$('#pdfFrame').attr('src', json.INVOICE_URL);
				else if (!hasInvoice && hasReceipt)
					$('#pdfFrame').attr('src', json.RECEIPT_URL);
				else if (hasInvoice && hasReceipt)
				    openDialogWindowByEncrypt('../../PRE/PRE01/PREPrint.aspx?Receipt=' + json.RECEIPT_URL + '&Invoice=' + json.INVOICE_URL + '&date=' + Date(), 400, 200);

				$('table[jqKey="tabHeader"] :text[jqKey]')
					.attr('readonly', 'true')
					.css({ 'border-width': '0px', 'cursor': 'default' });
				$('span[jqKey="labSale_Status"]').text('已結帳');
				hfActionType.val('4');
				alert('結帳完成 !');
				$(':button').attr('disabled', 'disabled');
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			$(':button[jqKey="btnCheckout"]').removeAttr('disabled')
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxCancelTransaction() {
	// 取消交易
	setWaitingMsg('divMasterMsg', '取消交易中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$(':button[jqKey="btnCheckout"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
	var para = {
		POSUUID_MASTER: hfPOSUUID_MASTER.val(),
		POSUUID_DETAIL: hfPOSUUID_DETAIL.val(),
		CANCEL_TRANS: '1'
	};

	var gv = $('#gvCheckout');
	var rows = gv.find('tr[jqKey]');
	$.ajax({
	    statusCode: statusCode,
	    type: 'POST',
	    url: 'TSAL11_Checkout.aspx',
	    data: { JSON: json2String(para) },
	    datatype: 'json',
	    success: function(data) {
	        setWaitingMsg('divMasterMsg', '');
	        onAjaxRequest = false;
	        restoreAllButtonStatus();
	        if (data.length == 0) {
	            alert('無法取消交易 !');
	            $(':button[jqKey="btnCheckout"]').removeAttr('disabled')
	            $(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	            return;
	        }
	        var json = {};
	        try {
	            json = eval('(' + data + ')');
	        } catch (e) {
	            alert(e + '\n\n' + data);
	            $(':button[jqKey="btnCheckout"]').removeAttr('disabled')
	            $(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	            return;
	        }
	        if (json.RESULT == '0') {
	            alert(json.ERROR);
	            $(':button[jqKey="btnCheckout"]').removeAttr('disabled')
	            $(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	            return;
	        } else if (json.RESULT == '2') {
	            if ($('#gvCheckout tr[jqKey]').length > 0)
	                alert('暫存資料中尚有支付資料, 故無法取消交易 !');
	            $(':button[jqKey="btnCheckout"]').removeAttr('disabled')
	            $(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	            return;
	        } else {
	            if (typeof (json.SALE_NO) == 'undefined' || json.SALE_NO.length == 0)
	                alert('您已取消交易, 請同步取消或更正該門號於業務園地的狀態 !');
	            else
	                alert('您已取消交易(' + json.SALE_NO + '), 請同步取消或更正該門號於業務園地的狀態 !');

	            //window.location.reload();
	            window.location.href = 'TSAL11.aspx';
	        }
	    },
	    error: function(jqXHR, textStatus, errorThrown) {
	        alert('ERROR : \n' + errorThrown);
	        setWaitingMsg('divMasterMsg', '');
	        $(':button[jqKey="btnCheckout"]').removeAttr('disabled')
	        $(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	        onAjaxRequest = false;
	        restoreAllButtonStatus();
	    }
	});
}

function ajaxSavePaidCache(data, async) {
	// 新增支付快取
	// data : 由 Dialog 傳回的值帶入
	// async : 是否使用非同步執行, true: 非同步; false: 同步(會等待)
	if (typeof (async) == 'undefined')
		async = true;
	var arr = data.split(',');
	var paidType = getPaidType(arr[0]);
	if (paidType == 0) {
		alert('未知的關鍵字: ' + arr[0].toUpperCase());
		return;
	}
	arr[0] = paidType.toString();
	setWaitingMsg('divMasterMsg', '儲存支付快取中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	var result = false;
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		async: async,
		url: '../TSAL01/AjaxQuery.ashx?mode=add_paid&masterid=' + hfPOSUUID_MASTER.val() + '&data=' + arr.join(':'),
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
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
					tr.find(':hidden[jqKey="PAID_ID"]').val(json.ID);
					tr.find('td[jqKey="PAID_MODE_NAME"]').text(getPaidTypeName(paidType));
					tr.find('td[jqKey="AMOUNT"]').text(arr[1]);
					var canBeCancelTrans = false;
					switch (paidType) {
						case 1:
							tr.find('td[jqKey="DESCRIPTION"]').text(getPaidTypeName(paidType));
							canBeCancelTrans = true;
							break;
						case 3: // 離線信用卡
							// 信用卡分期與離線信用卡不允許混合支付
							tr.find('td[jqKey="DESCRIPTION"]').text('信用卡號:' + arr[2] + ',授權碼:' + arr[3]);
							$(':button[jqKey="btnOffLineCreditCard"]').attr('disabled', 'disabled');
							break;
						default:
							break;
					}
					var desc = paidType + ',' + arr[1] + ',' + tr.find('td[jqKey="DESCRIPTION"]').text();
					tr.data('Description', desc);
					if (!canBeCancelTrans) {
						tr.find(':checkbox[jqKey="CHECK"]').hide();
						$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
					}
					recountCheckoutTotalAmount();
					checkItemCancelButtonEnable();
					result = true;
				} catch (e) {
					alert('支付項目無法新增, 請與總公司連絡 !\n\n' + e);
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
	return result;
}

function ajaxDeletePaidCache(ids) {
	// 刪除支付快取
	setWaitingMsg('divMasterMsg', '刪除支付快取中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=del_paid&masterid=' + hfPOSUUID_MASTER.val() + '&pid=' + ids,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
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
				var gv = $('#gvCheckout');
				$.each(ids.split(';'), function(i, v) {
					gv.find(':hidden[jqKey="PAID_ID"][value="' + v + '"]').parents('tr').first().remove();
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

function ajaxRequestETCInfo(tr, price, async) {
	// 取得 ETC 相關資料
	// data : 由 Dialog 傳回的值帶入
	// tr : Grid Row(tr)
	// price : 加值金額
	// async : 是否使用非同步執行, true: 非同步; false: 同步(會等待)
	if (typeof (async) == 'undefined')
		async = true;
	setWaitingMsg('divMasterMsg', '讀取 ETC 品項資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	var result = false;
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		async: async,
		url: '../TSAL01/AjaxQuery.ashx?mode=etc',
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法取得 ETC 品項資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')')[0];
				if (typeof (json) == 'undefined') {
					alert('查無 ETC 品項資料 !');
					return;
				}
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			json.IMEI_OK = '0';
			json.PRICE = price.toString();
			json.IS_ETC = '1';
			settingItemRow(json, tr);

			lockItemRow(tr);
			hasETCItem = true;
			recountTotalAmount();
			result = true;
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
	return result;
}

function ajaxRequestPromotionProducts(promoCode, detailId, prods, price) {
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
				var priceArray = price.split(';');
				$.each(json.Products, function(i, v) {
					var tr = createItemRow();
					v.PRICE = priceArray[i];
					v.CAN_DELETE = '0';
					v.IS_LOCK = '1';
					if (v.IMEI_FLAG != '1') {
						v.IMEI = '';
						v.IMEI_OK = '0';
					}
					settingItemRow(v, tr);
				});
				recountTotalAmount();
				lockItemGridInterface();
				disableProdGridButtons();
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
	var masterId = '';
	if (hfActionType.val() == '5')
		isChangeProd = true;
	else
		isChangeProd = false;
$.ajax({
    statusCode: statusCode,
    type: 'GET',
    url: '../TSAL01/AjaxQuery.ashx?mode=sale_history&masterid=' + ((isChangeProd) ? hfOriginal_MASTER.val() : hfPOSUUID_MASTER.val()),
    success: function(data) {
        setWaitingMsg('divMasterMsg', '');
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
            if (!isChangeProd && typeof (json.Header) != 'undefined') {
                if (typeof (json.Header.SALE_NO) != 'undefined')
                    $('span[jqKey="labSALE_NO"]').text(json.Header.SALE_NO);
                if (typeof (json.Header.INVOICE_NO) != 'undefined') {
                    $('#labInvoice_No').empty();
                    if (json.Header.INVOICE_NO.length == 0 && json.Header.M_INVOICE_NO.length == 0) {
                        json.Header.INVOICE_NO = '收據';
                        $('span[jqKey="labVOUCHER_TYPE"]').text('收據');
                        $(':button[jqKey="btnReprint"]').click(
							    function() {
							            $('#pdfFrame').attr('src', json.Header.RECEIPT_URL);
							    }
							);
                    } else {
                        if (json.Header.INVOICE_NO.length == 0 && json.Header.M_INVOICE_NO.length != 0) {
                            $('span[jqKey="labVOUCHER_TYPE"]').text('手開發票');
                            $('span[jqKey="labInvoice_No"]').text(json.Header.M_INVOICE_NO);
                        } else {
                            if (json.Header.INVOICE_URL.length == 0) {
                                $('span[jqKey="labInvoice_No"]').text(json.Header.INVOICE_NO);
                            } else {
                                $('<a/>')
							        .attr({ 'href': json.Header.INVOICE_URL, 'target': '_blank' })
							        .text(json.Header.INVOICE_NO)
							        .appendTo($('#labInvoice_No'));
							}
                        }
                        $(':button[jqKey="btnReprint"]').click(
							    function() {
							        var hasInvoice = (typeof (json.Header.INVOICE_URL) != 'undefined' && json.Header.INVOICE_URL.length != 0);
							        var hasReceipt = (typeof (json.Header.RECEIPT_URL) != 'undefined' && json.Header.RECEIPT_URL.length != 0);
							      
							        if (hasInvoice && !hasReceipt)
							            $('#pdfFrame').attr('src', json.Header.INVOICE_URL);
							        else if (!hasInvoice && hasReceipt)
							            $('#pdfFrame').attr('src', json.Header.RECEIPT_URL);
							        else if (hasInvoice && hasReceipt)
							            openDialogWindowByEncrypt('../../PRE/PRE01/PREPrint.aspx?Receipt=' + json.Header.RECEIPT_URL + '&Invoice=' + json.Header.INVOICE_URL + '&date=' + Date(), 400, 200);
							    }  
							);
                    }
                }
                $('table[jqKey="tabHeader"] :text[jqKey]')
						.attr('readonly', 'true')
						.css({ 'border-width': '0px', 'cursor': 'default' });
				if (json.Header.SALE_STATUS == "3") {
				    $('span[jqKey="labSale_Status"]').text('退貨作廢');
				} else if (json.Header.SALE_STATUS == "4") {
				        $('span[jqKey="labSale_Status"]').text('跨月退貨作廢');
				} else if (json.Header.SALE_STATUS == "5") {
				        $('span[jqKey="labSale_Status"]').text('換貨作廢');
				} else if (json.Header.SALE_STATUS == "6") {
				        $('span[jqKey="labSale_Status"]').text('跨月換貨作廢');
				} else {
                    $('span[jqKey="labSale_Status"]').text('已結帳');
                }
                if (typeof (json.Header.TRADE_DATE) != 'undefined')
                    $('span[jqKey="labTRADE_DATE"]').text(json.Header.TRADE_DATE);
                if (typeof (json.Header.UNI_NO) != 'undefined')
                    $(':text[jqKey="txtUNI_NO"]').val(json.Header.UNI_NO);
                if (typeof (json.Header.UNI_TITLE) != 'undefined')
                    $(':text[jqKey="txtUNI_TITLE"]').val(json.Header.UNI_TITLE);
                if (typeof (json.Header.REMARK) != 'undefined')
                    $(':text[jqKey="txtREMARK"]').val(json.Header.REMARK);
                if (typeof (json.Header.MODI_USER) != 'undefined')
                    $('span[jqKey="labMODI_USER"]').text(json.Header.MODI_USER);
                if (typeof (json.Header.MODI_DTM) != 'undefined')
                    $('span[jqKey="labMODI_DTM"]').text(json.Header.MODI_DTM);
            }
            if (typeof (json.Products) != 'undefined') {
                for (var x = 0; x <= (hfActionType.val() == '5') ? 1 : 0; x++) {
                    $.each(json.Products, function(i, jv) {
                        var v = cloneJson(jv);
                        var tr = createItemRow();
                        v.CAN_DELETE = x.toString();
                        v.IS_LOCK = (1 - x).toString();
                        v.IS_UNLESS = (1 - x).toString();
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
                        if (v.BARCODE1.length != 0)
                            v.BILL_TYPE = checkBillType(v);
                        settingItemRow(v, tr);
                        if (typeof (v.IMEI_FLAG) != 'undefined' && v.IMEI_FLAG != '1')
                            setIMEIIcon(tr, (v.IMEI_OK == '1'));
                    });
                }
                recountTotalAmount();
            }
            showDiscount(json.Discount);
            if (typeof (json.PaidCache) != 'undefined') {
                $.each(json.PaidCache, function(i, v) {
                    if (isChangeProd)
                        v.IS_UNLESS = '1';
                    v.IS_LOCK = '1';
                    if (v.PAID_MODE == '8') {
                        $('#labChange').text(Math.abs(parseInt(v.PAID_AMOUNT)));
                    }
                    else {
                        var tr = createCheckoutRow();
                        settingCheckoutRow(tr, v);
                    }
                });
                lockCheckoutGrid();
            }
            if (!isChangeProd) {
                $(':button').attr('disabled', 'disabled');
                // 已作廢交易或手開發票無法「重印單據」
                if (json.Header.SALE_STATUS != "3" && json.Header.SALE_STATUS != "4" && json.Header.SALE_STATUS != "5" && json.Header.SALE_STATUS != "6"
                    && json.Header.M_INVOICE_NO.length == 0) 
                    $(':button[jqKey="btnReprint"]').removeAttr('disabled')
            } else {
                $('#gvMaster :text[jqKey]:visible:first').focus();
                $(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
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

function ajaxRequestCommunications(dat) {
	// 取得 ETC 相關資料
	setWaitingMsg('divMasterMsg', '讀取授信通聯品項資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	// CommAdd^料號^電話號碼^金額|CommAdd^料號^電話號碼^金額|...;
	var items = dat.split('|');
	var prodNo = [];
	$.each(items, function(i, v) {
		prodNo[prodNo.length] = v.split('^')[1];
	});
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL01/AjaxQuery.ashx?mode=prods&prodno=' + prodNo.join(';') + '&storeno=' + storeNo,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法取得授信通聯品項資料 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
				if (typeof (json) == 'undefined') {
					alert('查無授信通聯品項資料 !');
					return;
				}
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			var rows = $('#gvMaster tr[jqKey]');
			$.each(items, function(i, p) {
				var found = false;
				var arr = p.split('^');
				$.each(arr, function(i, v) {
					arr[i] = $.trim(arr[i]);
				});
				var idx = -1;
				$.each(rows, function(j, r) {
					if (idx != -1) return;
					var tr = $(r);
					var rowJson = getRowData(tr);
					if (arr[1] == rowJson.PRODNO && arr[1] == CommunicationsProdNo.BASIC_PRODNO && arr[2] == rowJson.MSISDN) {
						// 申請費
						idx = j;
					} else if (arr[1] == rowJson.PRODNO && arr[1] == CommunicationsProdNo.PRODNO && arr[2] == rowJson.MSISDN) {
						// 查詢費
						idx = j;
						rowJson.PRICE = parseInt(rowJson.PRICE) + parseInt(arr[3]) + '';
						settingItemRow(rowJson, tr);
					}
				});
				if (idx == -1) {
					$.each(json, function(j, v) {
						if (found) return;
						if (arr[1] == v.PRODNO) {
							var tr = createItemRow();
							v.PRODNAME += '[' + arr[2] + ']';
							v.ITEM_TYPE = '9';
							v.PRICE = arr[3];
							v.MSISDN = arr[2];
							v.QUANTITY = '1';
							v.CAN_DELETE = '1';
							v.IS_LOCK = '1';
							v.IS_COMMPROD = '1';
							settingItemRow(v, tr);
							found = true;
						}
					});
				}
			});
			recountTotalAmount();
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}

function ajaxcheckManualInvoiceNo(invNo, INVOICE_TYPE) {
	// invno : 發票號碼
	//setWaitingMsg('divMasterMsg', '檢查 發票號碼中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	var result = false;
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: '../TSAL11/AjaxQuery.ashx?mode=invoice_no&invno=' + invNo + '&invtype=' + INVOICE_TYPE,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			var json = {};
			try {
				json = eval('(' + data + ')');
				if (json.RESULT != '1') {
				    alert('發票號碼不存在或不允許使用 ! !'); 
				    $(':text[jqKey="txtInvoice_No"]').val('');
					return;
				}
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			result = true;
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}