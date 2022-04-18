var hasETCItem = false; //是否為ETC儲值
var hasETCItemCard = false;//是否為ETC聯名卡
var hasETCCard = false;
var hasStoreDiscount = false; //是否有特殊折扣
var hasHappyGoDiscount = false;//是否有HG折扣
var hasLoyalty = false;
var hasInstead = false;
var discountInfo = [];
var onAjaxRequest = false;
var onSaveCacheMode = false;
var isCancel = true;
var storeDiscountRate = '';

var qtyNumericSettings = {
	buttons: false,
	title: '請輸入銷售數量',
	keyboard: false//,
	//minValue: 1
};

var priceNumericSettings = {
	buttons: false,
	title: '請輸入商品單價',
	keyboard: false,
	minValue: 0,
	emptyValue: 0
};

$(document).ready(function() {
    $(':text').focus(function() { $(this).select(); });
    $(':button').addClass('buttonStyle').attr('disabled', 'disabled');
    $(':text').addClass('textBoxStyle');
    $(':submit').addClass('buttonStyle');
    try {
        if (!isCheckInvSetting) {
            $(':text').attr('disabled', 'disabled');
            $(':button').attr('disabled', 'disabled');
            $(':text').attr('disabled', 'disabled');
            $(':submit').attr('disabled', 'disabled');
            alert('門市未設定電子發票, 請與總部聯絡!');
            window.location = baseUrl + '/content.htm';
            return;
        }
    } catch (err) {
    }
    // 來電禮 Button Always Enable
    $(':button[jqKey="btnGift"]').removeAttr('disabled');
    $(':button[jqKey="btnCheckout"]').click(checkOutData);
    $(':button[jqKey="btnCancelTransaction"]').click(cancelTransaction);
    $(':submit[jqKey="btnSearch"]').click(function() { checkItemAdded(); });
    $(':submit[jqKey="btnUnClose"]').click(function() { checkItemAdded(); });
    checkAndLoadCache();
    $(':text[jqKey="txtUNI_NO"]')
		.blur(function() {
		    if ($(this).val().length == 0)
		        return;
		    if (!checkCompanyId($(this).val())) {
		        getUNINo($(this).val());
		        event.returnValue = false;
		        return false;
		    } else {
		        $(':text[jqKey="txtREMARK"]').val('');
		        //4/19改成"發票抬頭"可不輸入
		        //if ($.trim($(':text[jqKey="txtUNI_TITLE"]').val())  == '') {
		        //    alert('請記得輸入發票抬頭 !');
		        //}
		    }
		})
		.numeric({
		    buttons: false,
		    title: '請輸入統一編號',
		    keyboard: false
		});
    enableFunctionButton();
    $(document).keydown(function(e) {
        if (e.keyCode == 27) {
            // 鎖文字框按下 Esc 時, 會全部清除的狀況
            event.returnValue = false;
            return false;
        }
        else if (e.keyCode == 8) {
            // 鎖非文字框按下 BackSpace 時, 會跳回上一頁的狀況
            if ($(e.target).get(0).tagName.toUpperCase() == 'INPUT') {
                if ($(e.target).attr('readonly'))
                    return false;
                else
                    return true;
            } else {
                return false;
            }
        }
    });
});

function checkAndLoadCache() {
	// 檢查並載入快取資料
	switch (hfActionType.val()) {
		case '1': // 快取資料
			alert('系統偵測發現有已付款但未正常結帳資料, 將載入未結帳資料 !');
			ajaxLoadCache();
			break;
		case '2': // 未結資料
			//alert('系統偵測發現有未結資料, 將載入未結資料 !');
			ajaxLoadToClose();
			break;
		case '3': // 銷售歷史資料
			ajaxSaleHistory();
			break;
		case '4': // 結帳
			break;
		case '5': // 換貨
			$('td[jqKey="ChangeProdCell"]').show();
			$('#chkAllRow').show();
			$(':button[jqKey="btnCommunications"]').hide();
			$(':button[jqKey="btnConfirm"]').val('換貨商品確認');
			$(':button[jqKey="btnCheckout"]').val('換貨結帳');
			$(':button[jqKey="btnCancelTransaction"]').val('換貨取消');
			$('span[jqKey="labSale_Status"]').text(saleStatusName(7));
			ajaxSaleHistory();
			break;
		default:
			break;
	}
}

function checkAllCheckboxs() {
	// 選擇或取消核選所有資料列
	var gv = $('#gvMaster');
	var chk = $('#chkAllRow');
	gv.find(':checkbox:visible[jqKey="CHECK"]')
		.attr('checked', chk.attr('checked'));
}

function deleteItemRow() {
	// 刪除選擇項目
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var gv = $('#gvMaster');
	var chks = gv.find(':checkbox[jqKey="CHECK"]:visible:checked');
	if (chks.length == 0) return;
	var beExits = false;
	var hasCheckedStoreDiccount = false;
	var hasCheckedHappyGoDiccount = false;
	var hasCheckedAddProd = false;
	var hasCheckedByAddProd = false;
	var hasCheckedCommProd = false;
	var hasCheckedETCCard = false;
	var isCheckAll = (gv.find('tr[jqKey] :checkbox:checked').length == gv.find('tr[jqKey]').length);
	var isConfirmAddProd = false;
	var isConfirmCommProd = false;
	var beDeleteMSISDN = [];
	$.each(chks, function() {
		if (beExits) return;
		var rowJson = getRowData($(this).parents('tr:first'));
		if (rowJson != null && typeof (rowJson.IS_TO_CLOSE) != 'undefined' && rowJson.IS_TO_CLOSE == '1') {
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
		else if (rowJson != null && rowJson.ITEM_TYPE == '4' && hasHappyGoDiscount)
			hasCheckedHappyGoDiccount = true;
		else if (rowJson != null && typeof (rowJson.HAS_ADDPROD) != 'undefined' && rowJson.HAS_ADDPROD == '1') {
			hasCheckedAddProd = true;
			if (isCheckAll)
				isConfirmAddProd = true;
			else if (confirm('欲刪除加價購主體商品, 需一併刪除附屬的商品項目, 是否繼續 ?')) {
				isConfirmAddProd = true;
				$.each(gv.find('tr[jqKey]'), function() {
					var js = getRowData($(this));
					if (js != null && typeof (js.ADD_BY) != 'undefined' && js.ADD_BY == rowJson.PRODNO)
						$(this).find(':checkbox').attr('checked', 'true');
				});
			}
		} else if (rowJson != null && typeof (rowJson.IS_ADDPROD) != 'undefined' && rowJson.IS_ADDPROD == '1') {
			hasCheckedByAddProd = true;
			var strconfirm;
			if (rowJson.IS_DISCOUNT == 'N')
			    strconfirm = '欲刪除加價購的「' + rowJson.PRODNAME + '」, 將會一併刪除此加價購的折扣, 是否繼續 ?';
			else
			    strconfirm = '欲刪除加價購的折扣「' + rowJson.PRODNAME + '」, 將會一併刪除此加價購, 是否繼續 ?'
			if (confirm(strconfirm)) {
			    $.each(gv.find('tr[jqKey]'), function() {
					var js = getRowData($(this));
					if (js != null && typeof (js.IS_ADDPROD) != 'undefined' && js.IS_ADDPROD == '1')
						$(this).find(':checkbox').attr('checked', 'true');
				});
			}
		} else if (rowJson != null && typeof (rowJson.IS_COMMPROD) != 'undefined' && rowJson.IS_COMMPROD == '1') {
			hasCheckedCommProd = true;
			if (isCheckAll)
				isConfirmCommProd = true;
			else if (confirm('欲刪除授信通聯的「' + rowJson.PRODNAME + '」, 將會一併刪除另一筆資料, 是否繼續 ?')) {
				isConfirmCommProd = true;
				$.each(gv.find('tr[jqKey]'), function() {
					var js = getRowData($(this));
					if (js != null && typeof (js.MSISDN) != 'undefined' && js.MSISDN == rowJson.MSISDN)
						$(this).find(':checkbox').attr('checked', 'true');
				});
			}
		} else if (rowJson != null && typeof (rowJson.IS_ETC_CARD) != 'undefined' && rowJson.IS_ETC_CARD == '1') {
			hasCheckedETCCard = true;
			if (isCheckAll)
				isConfirmCommProd = true;
			else if (confirm('欲刪除 ETC 加值卡的「' + rowJson.PRODNAME + '」, 將會一併刪除另一筆資料, 是否繼續 ?')) {
				isConfirmCommProd = true;
				$.each(gv.find('tr[jqKey]'), function() {
					var js = getRowData($(this));
					if (js != null && typeof (js.CARD_NO) != 'undefined' && js.CARD_NO == rowJson.CARD_NO)
						$(this).find(':checkbox').attr('checked', 'true');
				});
			}
		}
	});
	if (beExits) return;
	// 重新取得勾選的項目
	chks = gv.find(':checkbox[jqKey="CHECK"]:visible:checked');
	if (!isCheckAll && hasStoreDiscount && !hasCheckedStoreDiccount) {
		alert('欲刪除品項資料, 必須一併刪除「' + $(':button[jqKey="btnStoreDiscount"]').val() + '」(' + StoreDiscountProd.PRODNO + '/' + StoreDiscountProd.PRODNAME + ') !');
		return;
	}
	if (!isCheckAll && hasHappyGoDiscount && !hasCheckedHappyGoDiccount) {
		alert('欲刪除品項資料, 必須一併刪除「' + $(':button[jqKey="btnHappyGoNet"]').val() + '」 !');
		return;
	}
	if ((hasCheckedAddProd && !isConfirmAddProd) || (hasCheckedCommProd && !isConfirmCommProd) || chks.length == 0 || !confirm('您是否要刪除已勾選的項目 ?'))
		return;
	if (hasCheckedByAddProd) {
		// 處理刪除加價購商品時, 要一併清除加價購主體商品的 Flag
		$.each(chks, function() {
			var rowJson = getRowData($(this).parents('tr:first'));
			if (rowJson != null && typeof (rowJson.IS_ADDPROD) != 'undefined' && rowJson.IS_ADDPROD == '1') {
				var addMainProd = rowJson.ADD_BY;
				var idx = -1;
				var hasExists = false;
				$.each(gv.find('tr[jqKey]'), function(i, r) {
					if (hasExists) return;
					var tr = $(r);
					var js = getRowData(tr);
					//alert(js.PRODNO + ' / ' + rowJson.PRODNO + ' / ' + js.ADD_BY + ' / ' + addMainProd);
					if (js.PRODNO != rowJson.PRODNO && typeof (js.ADD_BY) != 'undefined' && js.ADD_BY == addMainProd && !eval(tr.find(':checkbox').attr('checked'))) {
						hasExists = true;
					}
				});
				if (!hasExists) {
					hasExists = false;
					$.each(gv.find('tr[jqKey]'), function(i, r) {
						if (hasExists) return;
						var tr = $(r);
						var js = getRowData(tr);
						if (js.PRODNO == addMainProd) {
							delete js.HAS_ADDPROD;
							setRowData(tr, js);
						}
					});
				}
			}
		});
	}
	var beRollbackItemGrid = false;
	// 刪除 TR
	$.each(chks, function() {
		var tr = $(this).parents('tr:first');
		var rowJson = getRowData(tr);
		if (hasStoreDiscount && rowJson.ITEM_TYPE == '6') {
			hasStoreDiscount = false;
			beRollbackItemGrid = true;
		} else if (hasHappyGoDiscount && rowJson.ITEM_TYPE == '4') {
			hasHappyGoDiscount = false;
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
		$('input[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
		hasETCItem = false;
		hasETCCard = false;
		hasStoreDiscount = false;
		hasHappyGoDiscount = false;
		hasLoyalty = false;
		hasInstead = false;
		HappyGoDiscount_Info.length = 0;
		discountInfo.length = 0;
		onAjaxRequest = false;
		onSaveCacheMode = false;
	} else {
		$('input[jqKey="btnConfirm"]').removeAttr('disabled');
		$('input[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	}
	$('#chkAllRow').removeAttr('checked');
}

function chooseProdNo(o) {
	// Popup 產品選擇視窗
    var r = openDialogWindowByEncrypt('ProductsPopup.aspx?date=' + Date(), 550, 400);
	if (r == null || r == '' || typeof (r) == 'undefined')
		return;
	var json = eval('(' + r + ')');
	ajaxRequestProductInfo(json.ProdNo, $(o).parents('tr:first'));
}

function prodChooseDone(rowId, v) {
	// 供產品選擇窗回傳資料用
	var json = eval('(' + v + ')');
	ajaxRequestProductInfo(json.ProdNo, $('#' + rowId));
}

function checkAndRequestProdNo(o) {
	// 料號欄位 Blur Event
	// Issue : 自動儲存快取時, 會跑進來
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	if ($(o).val().length == 0)
		return;
	var rowJson = getRowData($(o).parents('tr:first'));
	if ((rowJson != null && rowJson.PRODNO == $(o).val()) || ($(o).data('LastSearch') != 'undefined' && $(o).data('LastSearch') == $(o).val()))
		return;
	$(o).data('LastSearch', $(o).val());
	ajaxRequestProductInfo($(o).val(), $(o).parents('tr').first());
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
	var qty = parseInt(tr.find(':text[jqKey="QTY"]').val());
	var price = parseInt(tr.find(':text[jqKey="PRICE"]').val());
	if (isNaN(qty)) {
		qty = 0;
		tr.find(':text[jqKey="QTY"]').val(0);
	}
	if (isNaN(price)) {
		price = 0;
		tr.find(':text[jqKey="PRICE"]').val(0);
	}
	var total = qty * price;
	if (isNaN(total)) total = 0;
	tr.find('div[jqKey="ITEM_AMOUNT"]').text(Math.abs(total));
	var rowJson = getRowData(tr);
	if ($(o).attr('jqKey') == 'QTY') {
		if (typeof (rowJson.ISSTOCK) != 'undefined' && rowJson.ISSTOCK == '1' && qty > parseInt(rowJson.ON_HAND_QTY)) {
			alert('輸入數量大於目前庫存量(' + rowJson.ON_HAND_QTY + '), 請重新輸入');
			$(o).focus();
			return;
		}
		rowJson.QUANTITY = qty.toString();
		setRowData(tr, rowJson);

		var txtIMEI = tr.find(':text[jqKey="IMEI"]');
		var btnIMEI = tr.find(':button[jqKey="CHOOSE_IMEI"]');
		var btnDelIMEI = tr.find(':button[jqKey="DELETE_IMEI"]');
		if (rowJson.IMEI_FLAG != '1' && parseInt(hfActionType.val()) <= 2) {
			if (qty == 1) {
				if (rowJson.IMEI_OK == '1' || (typeof(rowJson.IMEI) != 'undefined' && rowJson.IMEI.length != 0)) {
					txtIMEI.attr('readonly', 'true');
					btnIMEI.hide();
					if (!onSaveCacheMode)
						btnDelIMEI.show();
				} else {
					txtIMEI.removeAttr('readonly');
					btnIMEI.hide();
					btnDelIMEI.hide();
				}
			} else {
				txtIMEI.attr('readonly', 'true');
				btnIMEI.show();
				btnDelIMEI.hide();
			}
			checkIMEI($(':text[jqKey="IMEI"]'), false);
		}
	} else {
		if (rowJson.IS_DISCOUNT != 'Y' && parseInt(rowJson.PRICE) != price && rowJson.PRODNO != StoreDiscountProd.PRODNO) {
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
			if (typeof (rowJson.IS_UNLESS) != 'undefined' && rowJson.IS_UNLESS == '1')
				origTotal += parseInt(rowJson.QUANTITY) * parseInt(rowJson.PRICE);
			else
				total += parseInt(rowJson.QUANTITY) * parseInt(rowJson.PRICE);
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
	var gv = $('#gvMaster');
	var rows = $('#gvMaster tr').length;
	var objs = gv.find('tr[jqKey]');
	if (objs.length == 0) {
		alert('無商品資料, 請先新增商品品項 !');
		return;
	}

	var idx = 0;
	var checkOK = true;
	$.each(objs, function() {
		if (!checkOK) return;
		var tr = $(this);
		var txtProd = tr.find(':text[jqKey="PRODNO"]');
		var prod = '「' + tr.find('td[jqKey="PRODNAME"]').text() + '(' + txtProd.val() + ')」';
		var rowJson = getRowData(tr);
		// 檢查料號與 JSON
		if (rowJson == null || txtProd.val().length == 0) {
			alert('未選擇產品品項 !!');
			txtProd.focus();
			checkOK = false;
			return;
		}
		// 檢查數量是否為 0
		if (parseInt(rowJson.QUANTITY) <= 0) {
			alert('「' + prod + '」的數量不得為 0 !');
			tr.find(':text[jqKey="QTY"]').focus();
			checkOK = false;
			return;
		}
		// 檢查庫存與銷售量
		if (rowJson.ISSTOCK == '1' && parseInt(rowJson.ON_HAND_QTY) < parseInt(rowJson.QUANTITY)) {
			// 當庫存量小於銷售量時
			alert(prod + '銷售數量大於目前庫存量(' + rowJson.ON_HAND_QTY + ') !!');
			txtProd.focus();
			checkOK = false;
			return;
		}
		// 檢查 IMEI
		var txtIMEI = tr.find(':text[jqKey="IMEI"]');
		if (rowJson.IMEI_FLAG != '1' && $.trim(txtIMEI.val()).length == 0) {
			alert(prod + '未輸入 IMEI !!');
			txtIMEI.focus();
			checkOK = false;
			return;
		} else if (rowJson.IMEI_FLAG != '1' && parseInt(rowJson.QUANTITY) != txtIMEI.val().split(',').length) {
			alert(prod + '輸入的 IMEI 數量與銷售數量不符合 !!');
			checkOK = false;
			return;
		} else if (rowJson.IMEI_FLAG != '1' && rowJson.IMEI_OK != '1') {
			alert(prod + '的 IMEI 資料不完全 !!');
			checkOK = false;
			txtIMEI.focus();
			return;
		}
		// 檢查 PRODNO 是否為 SIM 關鍵字
		if (txtProd.val().toUpperCase() == 'SIM' || rowJson.PRODNO.toUpperCase() == 'SIM') {
			alert('未重新選擇 SIM 卡料號 !');
			txtProd.focus();
			checkOK = false;
			return;
		}
		rowJson.IMEI = txtIMEI.val();
		setRowData(tr, rowJson);
	});
	if (!checkOK) return;

	ajaxSaveItemCache();
}

function lockItemRow(tr) {
	tr.hide();
	$.each(tr.find(':text'), function() {
		if ($(this).parent().find('span[jqKey="DisplayOnly"]').length != 0)
			return;
		swapTextboxToLabel($(this));
	});
	tr.find(':button').hide();
	tr.show();
}

function cancelItems() {
	// 取消品項鎖定
	var rows = $('#gvCheckout tr[jqKey]');
	if (checkHasPaidItem()) {
		alert('必須取消所有支付款項, 才能此本取消程序 !');
		return;
	}
	if (!hasETCItem && !confirm('請確定是否取消 ?'))
		return;
	if (hfActionType.val() != '2' && hasETCItem && !confirm('取消後, 將會自動清除先前加值的金額!\n請確定是否取消 ?'))
		return;
	ajaxCancelItemCache();
}

function rollbackItemGrid() {
	// 回復品項輸入相關功能
	var gv = $('#gvMaster');
	var rows = gv.find('tr[jqKey]');
	if (rows.length <= 0)
		return;
	else
		$(':button[jqKey="btnConfirm"]').removeAttr('disabled');
	$(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnAddItem"]').removeAttr('disabled');
	$(':button[jqKey="btnDeleteItems"]').removeAttr('disabled');
	$(':button[jqKey="btnAddProd"]').removeAttr('disabled');
	$(':button[jqKey="btnETCCard"]').removeAttr('disabled');
	$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnCommunications"]').removeAttr('disabled');
	$('#chkAllRow').show();
	if (!hasStoreDiscount && !hasHappyGoDiscount) {
		$(':button[jqKey="btnStoreDiscount"]').removeAttr('disabled');
		if (hfActionType.val() != '2')
			$(':button[jqKey="btnHappyGoNet"]').removeAttr('disabled');
		$.each(rows, function(i, row) {
			var tr = $(row);
			var rowJson = getRowData(tr);
			var isLocked = (typeof (rowJson.IS_LOCK) != 'undefined' && rowJson.IS_LOCK == '1');
			var isUnless = (typeof (rowJson.IS_UNLESS) != 'undefined' && rowJson.IS_UNLESS == '1');
			if (typeof (rowJson.CAN_DELETE) == 'undefined' || rowJson.CAN_DELETE == '1')
				tr.find(':checkbox').show();
			else
				tr.find(':checkbox').hide();
			if (rowJson.ITEM_TYPE == '1') {
				if (!isLocked) {
					tr.find('span[jqKey="DisplayOnly"]').remove();
					tr.find(':text').removeClass('gridTextBoxNoBorder').removeAttr('readonly').show();
					tr.find(':button[jqKey="CHOOSE_ITEM"]').show().removeAttr('disabled');
				}
			} else if (rowJson.ITEM_TYPE == '6') {
				$(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
			} else if (rowJson.ITEM_TYPE == '4') {
				$(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
			}
			var txtPrice = tr.find(':text[jqKey="PRICE"]');
			// 是否可變更單價
			if (!isLocked && (typeof (rowJson.IS_OPEN_PRICE) == 'undefined' || rowJson.IS_OPEN_PRICE == 'Y')) {
				txtPrice.val(rowJson.PRICE).show();
				txtPrice.parent().find('span[jqKey="DisplayOnly"]').remove();
			} else {
				txtPrice.val(rowJson.PRICE).hide();
				swapTextboxToLabel(txtPrice);
			}
			// IMEI
			tr.find(':text[jqKey="IMEI"]').hide();
			tr.find(':button[jqKey="CHOOSE_IMEI"]').hide();
			tr.find(':button[jqKey="DELETE_IMEI"]').hide();
			if (!isLocked && rowJson.IMEI_FLAG != '1') {
				tr.find(':text[jqKey="IMEI"]').attr('readonly', 'true').show();
				if (rowJson.QUANTITY == '1') {
					tr.find(':button[jqKey="CHOOSE_IMEI"]').hide();
					if (rowJson.IMEI_OK == '1')
						tr.find(':button[jqKey="DELETE_IMEI"]').show();
				} else {
					tr.find(':button[jqKey="CHOOSE_IMEI"]').show();
					tr.find(':button[jqKey="DELETE_IMEI"]').hide();
				}
			}
		});
		$('#gvMaster :text[jqKey]:visible:first').focus();
		if (parseInt(hfActionType.val()) == 2 && hasInstead) {
			// 未結且有代收單
			$(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
			$(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
		}

	} else {
		$('#gvMaster tr[jqKey] :checkbox').show();
	}
	disableCheckoutInterface();
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

function disableProdGridButtons() {
	// 關閉(Disable)品項輸入相關功能
	$(':button[jqKey="btnConfirm"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnCancel"]').removeAttr('disabled');
	$(':button[jqKey="btnAddItem"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnAddETC"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnETCCard"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnAddProd"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnDeleteItems"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnMixPromotion"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
	if (isCancel)
	    $(':button[jqKey="btnCancelTransaction"]').removeAttr('disabled');
	else
	    $(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnCommunications"]').attr('disabled', 'disabled');
}

function disableAllProdGridButtonExcludeDelete() {
	disableProdGridButtons();
	$(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
	$('#gvMaster :checkbox').show();
	$(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnDeleteItems"]').removeAttr('disabled');
	$(':button[jqKey="btnConfirm"]').removeAttr('disabled').focus();
}

function disableProdGridEditMode() {
	var gv = $('#gvMaster');
	gv.find('.chooseButton').hide();
	gv.find(':button').hide();
	gv.find(':checkbox').hide();
	gv.find(':text').addClass('gridTextBoxNoBorder').attr('readonly', 'true');
}

function checkIMEI(o, check) {
	// 檢查 IMEI 的是否合法
	var tr = $(o).parents('tr').first();
	var rowJson = getRowData(tr);
	if (rowJson == null || rowJson.IMEI_FLAG == '1')
		return;
	var qty = parseInt(tr.find(':text[jqKey="QTY"]').val());
	if (qty != 1 || !check || $.trim($(o).val()).length == 0) {
		if ($.trim(rowJson.IMEI).length == 0 || rowJson.IMEI.split(',').length != qty) {
			rowJson.IMEI_OK = '0';
		} else {
			rowJson.IMEI_OK = '1';
		}
		setIMEIIcon(tr, rowJson.IMEI_OK == '1');
		setRowData(tr, rowJson);
	} else {
		// 先檢查 IMEI 是否已在本次銷售輸入過
		var imei = $(o).val().toUpperCase();
		if (typeof (rowJson.IMEI) == 'undefined' || rowJson.IMEI.length == 0) {
			ajaxCheckIMEIExists(imei, tr);
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
				ajaxCheckIMEIExists(imei, tr);
		}
	}
}

function cancelTransaction() {
	// 取消交易
	var canBrCancelTrans = true;
	$.each($('#gvCheckout tr[jqKey]'), function(i, r) {
		if (!canBrCancelTrans) return;
		var payMode = $(r).data('Description').split(',')[0];
		canBrCancelTrans = (parseInt(payMode) == 1);
	});
	if (!canBrCancelTrans) {
		alert('資料中尚有非現金的支付資料, 故無法取消交易 !');
		return;
	}
	if (!confirm('是否取消此次交易 ?'))
		return;
	ajaxCancelTransaction();
}

function settingItemRow(json, row) {
	// 依 JSON 設定品項資料列
    var tr = $(row);
    json.IS_ETC = (json.PRODNO == ETC_ProdNo) ? '1' : '0';
	if (hfActionType.val() == '5') {
		tr.find('td[jqKey="UNLESS"]').show();
		if (typeof(json.IS_UNLESS) != 'undefined' && json.IS_UNLESS == '1')
			tr.find('td:[jqKey="UNLESS"]')
			.text('作廢')
			.attr('title', '換貨作廢');
	}
	tr.find(':hidden[jqKey="ITEM_ID"]').val(json.ID);
	if (typeof (json.PROMOTION_CODE) != 'undefined' && json.PROMOTION_CODE.length != 0) {
		tr.find('td:[jqKey="ITEM_MODE"]')
			.text('促')
			.attr('title', '促銷');
} else if ((typeof (json.SOURCE_TYPE) != 'undefined' && (json.SOURCE_TYPE) == '3' || json.SOURCE_TYPE) == '5') {
		tr.find('td:[jqKey="ITEM_MODE"]')
			.text('代')
			.attr('title', '代收');
		hasLoyalty = true;
	} else if (typeof (json.IS_DISCOUNT) != 'undefined' && json.IS_DISCOUNT == 'Y') {
		tr.find('td:[jqKey="ITEM_MODE"]')
			.text('折')
			.attr('title', '折扣或折抵');
	} else if (typeof (json.IS_ADDPROD) != 'undefined' && json.IS_ADDPROD == '1') {
		tr.find('td:[jqKey="ITEM_MODE"]')
			.text('加')
			.attr('title', '加價購商品');
	} else if (typeof (json.ITEM_TYPE) != 'undefined' && json.ITEM_TYPE == '13') {
		tr.find('td:[jqKey="ITEM_MODE"]')
			.text('贈')
			.attr('title', '贈品');
	}
	if (typeof (json.PROMOTION_NAME) != 'undefined' && json.PROMOTION_NAME.length != 0)
		tr.find('div[jqKey="PROMOTION_NAME"]')
			.text(json.PROMOTION_NAME)
			.attr('title', json.PROMOTION_NAME);
	tr.find(':text[jqKey="PRODNO"]').val(json.PRODNO);
	tr.find('div[jqKey="PRODNAME"]')
		.text(json.PRODNAME)
		.attr('title', json.PRODNAME);

	var txtPrice = tr.find(':text[jqKey="PRICE"]');
	var txtQty = tr.find(':text[jqKey="QTY"]').val(json.QUANTITY);
	txtPrice.val(Math.abs(parseInt(json.PRICE))).show();
	txtPrice.parent().find('span[jqKey="DisplayOnly"]').remove();

	if (json.ISSTOCK == '1') {
		if (json.IS_ETC == '0' && parseInt(json.ON_HAND_QTY) <= 0 && json.PRODNO != StoreDiscountProd.PRODNO) {
			alert('「' + json.PRODNAME + '(' + json.PRODNO + ')」目前無庫存量 !');
			json.QUANTITY = '0';
			txtQty.val('0')
				.numeric('option', 'maxValue', false)
				.numeric('option', 'title', '目前無庫存')
				.attr('disabled', 'disabled');
			txtPrice.attr('disabled', 'disabled');
			tr.find(':text[jqKey="PRODNO"]').focus();
		} else if (json.IS_ETC == "1") {
			json.ON_HAND_QTY = '1';
		} else if (json.ISSTOCK != '1' || parseInt(json.ON_HAND_QTY) > 0) {
			txtQty
				.numeric('option', 'maxValue', (parseInt(json.ON_HAND_QTY) > 0) ? parseInt(json.ON_HAND_QTY) : 0)
				.numeric('option', 'title', '請輸入銷售數量, 目前庫存:' + json.ON_HAND_QTY)
				.val(json.QUANTITY)
				.removeAttr('disabled')
				.focus();
			txtPrice.removeAttr('disabled');
		} else {
			txtQty.val(json.QUANTITY).removeAttr('disabled');
			txtPrice.removeAttr('disabled');
			txtQty.focus();
		}
	} else {
		txtQty
			.numeric('option', 'maxValue', false)
			.numeric('option', 'title', '請輸入銷售數量');
	}

	if (typeof (json.IS_OPEN_PRICE) != 'undefined' && json.IS_OPEN_PRICE == 'N') {
		// 如果不為自定單價, 則關閉單價輸入欄
		txtPrice.val(Math.abs(parseInt(json.PRICE))).hide();
		swapTextboxToLabel(txtPrice);
	}
	tr.find('div[jqKey="ITEM_AMOUNT"]').text(Math.abs(parseInt(json.PRICE) * parseInt(json.QUANTITY))).show();
	setRowData(tr, json);

	if ((typeof (json.IS_UNLESS) != 'undefined' && json.IS_UNLESS == '1') || (typeof (json.IS_TO_CLOSE) != 'undefined' && json.IS_TO_CLOSE == '1') || (typeof (json.IS_LOCK) != 'undefined' && json.IS_LOCK == '1')) {
		// 如果是作廢(IS_UNLESS)、未結(IS_TO_CLOSE)或鎖定(IS_LOCK), 則關閉所有欄位的輸入狀態
		lockItemRow(tr);
		tr.find(':checkbox[jqKey="CHECK"]').hide();
		if ((typeof (json.CAN_DELETE) == 'undefined' || json.CAN_DELETE == '1') || (typeof (json.IS_TO_CLOSE) != 'undefined' && json.IS_TO_CLOSE == '1')) {
			// 如果資料行可刪除或是未結, 則顯示核選框
			tr.find(':checkbox[jqKey="CHECK"]').show();
		}
			
	}
	
	if (typeof (json.IS_SIM) != 'undefined' && json.IS_SIM == '1') {
		// 如果料號為 SIM, 則開放料號輸入欄
		var td = tr.find('td[jqKey="PRODNO"]');
		td.find(':text').show();
		td.find(':button').show(); 
		td.find('span[jqKey="DisplayOnly"]').remove();
	}

	// 處理 IMEI 欄位
	tr.find(':text[jqKey="IMEI"]').val('').hide();
	tr.find(':button[jqKey="CHOOSE_IMEI"]').hide();
	tr.find(':button[jqKey="DELETE_IMEI"]').hide();
	tr.find('td[jqKey="IMEI_ICON"]').empty();
	if (json.IMEI_FLAG != '1') {
		// 檢核 IMEI
		if (typeof (json.IMEI) != 'undefined' && json.IMEI.length != 0)
			tr.find(':text[jqKey="IMEI"]').val(json.IMEI).removeAttr('title').show();
		else
			tr.find(':text[jqKey="IMEI"]').val('').removeAttr('title').show();
		setIMEIIcon(tr, false);
		if (parseInt(json.QUANTITY) == 1) {
			if (json.IMEI_OK == '0') {
				tr.find(':text[jqKey="IMEI"]').removeAttr('readonly');
			} else {
				tr.find(':text[jqKey="IMEI"]').attr('readonly', 'readonly');
				if (!onSaveCacheMode && (typeof (json.IS_SIM) == 'undefined' || json.IS_SIM != '1'))
					tr.find(':button[jqKey="DELETE_IMEI"]').show();
			}
		} else {
			tr.find(':text[jqKey="IMEI"]').attr('readonly', 'readonly');
			tr.find(':button[jqKey="CHOOSE_IMEI"]').show();
			tr.find(':button[jqKey="DELETE_IMEI"]').hide();
		}
	}
}

function openInputIMEIDialog(o) {
	// IMEI 輸入視窗
	var tr = $(o).parents('tr').first();
	var rowJson = getRowData(tr);
	var r = window.showModalDialog('TSAL01_InputIMEI.aspx', rowJson, 'dialogWidth:450px;dialogHeight:300px;status:no;resizable:yes;scroll:yes');
	if (r == null || typeof (r) == 'undefined' || r.length == 0)
		return;
	if (r == '<EMPTY>') {
		tr.find(':text[jqKey="IMEI"]').val('');
		rowJson.IMEI_OK = '0';
		rowJson.IMEI = '';
	} else {
		tr.find(':text[jqKey="IMEI"]').val(r).attr('title', r);
		rowJson.IMEI_OK = (r.split(',').length == parseInt(rowJson.QUANTITY)) ? '1' : '0';
		rowJson.IMEI = r;
	}
	setRowData(tr, rowJson);
	setIMEIIcon(tr, rowJson.IMEI_OK == '1');
}

function deleteIMEICache(o) {
	// 刪除 IMEI, 單筆用
	if (!confirm('您是否要清除此筆 IMEI 資料 ?'))
		return;
	var tr = $(o).parents('tr').first();
	ajaxDeleteIMEIItemRow(tr.find(':text[jqKey="IMEI"]').val(), tr);
}

function setIMEIIcon(tr, status) {
	// 設定 IMEI 圖示
	if (status)
		tr.find('td[jqKey="IMEI_ICON"]:first').empty().append('<img src="../../../Icon/check.png" />');
	else
		tr.find('td[jqKey="IMEI_ICON"]:first').empty().append('<img src="../../../Icon/non_complete.png" />');
}

function reassignElementID(gv) {
	// 重新指定 Grid 中所有元素的 ID
	var gvId = gv.attr('id');
	var rows = gv.find('tr[jqKey]');
	if (rows.length == 0) {
		gv.parents('div').first().find(':button[jqKey="btnAddItem"]').removeAttr('disabled');
		gv.parents('div').first().find(':button[jqKey="btnAddProd"]').removeAttr('disabled');
		gv.parents('div').first().find(':button[jqKey="btnAddETC"]').removeAttr('disabled');
		gv.parents('div').first().find(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
		gv.parents('div').first().find(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
		gv.parents('div').first().find(':button[jqKey="btnCommunications"]').removeAttr('disabled');
		hasETCItem = false;
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
			if (hfActionType.val() != '2')
				gv.parents('div').first().find(':button[jqKey="btnHappyGoNet"]').removeAttr('disabled');
			gv.parents('div').first().find(':button[jqKey="btnStoreDiscount"]').removeAttr('disabled');
		}
	}
}

function enableFunctionButton() {
	// 開啟可使用之按鈕
	switch (hfActionType.val()) {
		case '0':
			// 單品
			break;
		case '1':
			// 快取
			$(':button[jqKey="btnAddETC"]').attr('disabled', 'disabled');
			break;
		case '2':
			// 未結
			$(':button[jqKey="btnMixPromotion"]').removeAttr('disabled');
			break;
		case '3':
			// 歷史銷售資料
			break;
	}
	var items = $('#gvMaster tr[jqKey]');
	var paids = $('#gvCheckout tr[jqKey]');
	if (hfActionType.val() != 3 || !hasETCItem) {
		$(':button[jqKey="btnAddItem"]').removeAttr('disabled');
		$(':button[jqKey="btnAddProd"]').removeAttr('disabled');
		$(':button[jqKey="btnAddETC"]').removeAttr('disabled');
		$(':button[jqKey="btnETCCard"]').removeAttr('disabled');
		$(':button[jqKey="btnCommunications"]').removeAttr('disabled');
	}
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

function mixPromotion() {
	// 組合促銷
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	if (hfActionType.val() != 2) {
		alert('非未結狀態, 不可執行「組合促銷」!');
		return false;
	}
	var chks = $('#gvMaster tr[jqKey] :checkbox:checked');
	if (chks.length == 0) {
		alert('請先點選促銷組合商品!!');
		return false;
	} else if (chks.length >= 2) {
		alert('一次只能執行一筆組合促銷 !');
		return false;
	}
	var rowJson = getRowData($(chks[0]).parents('tr:first'));
	if (rowJson == null) {
		alert('請輸入商品編號');
		$(chks[0]).parents('tr:first').find(':text[jqKey="PRODNO"]').focus();
		return;
	}
	if (typeof (rowJson.PROMOTION_CODE) == 'undefined' || rowJson.PROMOTION_CODE.length == 0) {
		alert('此商品不能組合促銷 !');
		return false;
	}

	var promoCode = rowJson.PROMOTION_CODE;
	var detailId = rowJson.POSUUID_DETAIL;
	var prods = '';
	$.each($('#gvMaster tr[jqKey]'), function(i, r) {
		var json = getRowData($(r));
		if (json == null) return;
		if (json.PROMOTION_CODE == rowJson.PROMOTION_CODE && json.POSUUID_DETAIL == rowJson.POSUUID_DETAIL)
			prods += rowJson.PROMOTION_CODE + '|' + json.PRODNO + ';';
	});
	if (prods.length != 0)
		prods = prods.substr(0, prods.length - 1)
	var strWin = '../SAL01/SAL01_choosePromotions.aspx?Promotion_Code=' + rowJson.PROMOTION_CODE + '&Posuuid_Detail=' + rowJson.POSUUID_DETAIL + '&PromotionProdList=' + prods;
	var r = openDialogWindowByEncrypt(strWin, 700, 420);
	if (r == null || r == '' || typeof (r) == 'undefined')
		return false;

	var arr = r.split(';');
	var rows = $('#gvMaster tr[jqKey]');
	$.each(rows, function(i, o) {
		var tr = $(o);
		rowJson = getRowData(tr);
		if (rowJson.PROMOTION_CODE == promoCode && rowJson.POSUUID_DETAIL == detailId)
			tr.remove();
	});
	reassignElementID($('#gvMaster'));

	var prods = [];
	for (var i = 1; i < arr.length; i++)
		prods[prods.length] = arr[i].split('^')[0];

	ajaxRequestPromotionProducts(promoCode, detailId, prods.join(';'));
}

function storeDiscount() {
	// 特殊抱怨折扣
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var amt = parseInt($('#labTotalAmount').text());
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
	// STOREDISCOUNT,料號,料名,金額,折扣%,STORE_DIS_REASON_ID.STORE_DIS_REASON_DESC,ROLE
	hasStoreDiscount = true;
	tr = $($('#gvMaster tr[jqKey]')[0]);
	var newJson = cloneEmptyObject(getRowData(tr));
	var arr = r.split(',');
	var discount = 0;
	if (arr[3].length == 0 && arr[4].length != 0)
		discount = -Math.abs(Math.floor(amt / 100 * parseFloat(arr[4])));
	else
		discount = -Math.abs(parseInt(arr[3]));
	with (newJson) {
	    StoreDiscountProd.PRODNO = arr[1];
	    StoreDiscountProd.PRODNAME = arr[2];
		PRODNO = StoreDiscountProd.PRODNO;
		PRODNAME = StoreDiscountProd.PRODNAME;
		QUANTITY = '1';
		PRICE = discount.toString();
		ITEM_TYPE = '6';
		CAN_DELETE = '1';
		IS_ETC = '0';
		ON_HAND_QTY = '1';
		IMEI_FLAG = '1';
		IS_OPEN_PRICE = 'N';
		IS_LOCK = '1';
		IS_DISCOUNT = 'Y';
		SH_DISCOUNT_REASON = arr[5];
		SH_DISCOUNT_DESC = arr[7] == "1" ? '[使用店長權限]' + arr[6] : arr[6];
	}
	tr = createItemRow();
	settingItemRow(newJson, tr);

	recountTotalAmount();
	lockItemGridInterface();
	setIMEIColumnStatus();	
	disableProdGridButtons();
	$('#gvMaster :button').hide();
	$('#gvMaster :checkbox').show();
	$(':button[jqKey="btnStoreDiscount"]').attr('disabled', 'disabled');
	if (arr[4].length != 0 || hasHappyGoDiscount)  // 使用"特殊抱怨折扣"的金額,仍可使用"HappyGo折抵"
	    $(':button[jqKey="btnHappyGoNet"]').attr('disabled', 'disabled');
	else
	    $(':button[jqKey="btnHappyGoNet"]').removeAttr('disabled');
	$(':button[jqKey="btnCancel"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnCancelTransaction"]').attr('disabled', 'disabled');
	$(':button[jqKey="btnDeleteItems"]').removeAttr('disabled');
	$(':button[jqKey="btnConfirm"]').removeAttr('disabled').focus();
	
	// 若特殊抱怨折扣是比率，直接SaveCache
	if (arr[4].length != 0) {
	    storeDiscountRate = parseFloat(arr[4] / 100).toString();
	    ajaxSaveItemCache(false);
	    ReloadCache();
	}
}

function checkItemAdded() {
	var rows = $('#gvMaster tr[jqKey]');
	if (rows.length == 0 || hfActionType.val() == '3' || hfActionType.val() == '4')
		return;
	alert('資料未儲存, 請先儲存後方可執行此功能 !');
	event.returnValue = false;
	return false;
}

function communications() {
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/CreditCommunications.aspx?date=' + Date(), 500, 500);
	if (r == null || r == '' || typeof (r) == 'undefined')
		return;
	// CommAdd^料號^電話號碼^金額|CommAdd^料號^電話號碼^金額|...;
	ajaxRequestCommunications(r);
}

function addProd() {
	// 加價購
	var rows = $('#gvMaster tr[jqKey]');
	if (rows.length == 0) {
		alert('請先選擇商品 !');
		return;
	}
	var chks = $('#gvMaster :checkbox[jqKey="CHECK"]:visible:checked');
	if (chks.length == 0) {
		alert('請先選擇商品 !');
		return;
	} else if (chks.length >= 2) {
		alert('只能選擇一個商品 !');
		return;
	}
	var tr = chks.first().parents('tr:first');
	var rowJson = getRowData(tr);
	if (typeof (rowJson.HAS_ADDPROD) != 'undefined' && rowJson.HAS_ADDPROD == '1') {
		alert('此商品已選擇加價購的商品 !');
		return;
	}
	if (typeof (rowJson.IS_ADDPROD) != 'undefined' && rowJson.IS_ADDPROD == '1') {
		alert('加價購的商品不可再選擇加價購 !');
		return;
	}
	var r = window.showModalDialog('TSAL01_AddProds.aspx', rowJson, 'dialogWidth:500px;dialogHeight:450px;status:no;resizable:yes;scroll:yes');
	if (r == null || typeof (r) == 'undefined' || r.length == 0)
		return;
	rowJson.HAS_ADDPROD = '1';
	setRowData(tr, rowJson);
	var prods = cloneJson(r);
	ajaxGetAddProds(prods, rowJson.PRODNO);
}

function checkBillType(rowJson) {
	// 取得代收單據種類
	var code = $.trim(rowJson.BARCODE1);
	var lastThreeCode = code.substr(code.length - 3).toUpperCase();
	if (code.length == 10 && $.trim(rowJson.BARCODE2).length == 0 && $.trim(rowJson.BARCODE3).length == 0)
		return 'FET';
	else if (code.length == 14 && $.trim(rowJson.BARCODE2).length == 0 && $.trim(rowJson.BARCODE3).length == 0)
		return 'KGT';
	else if (lastThreeCode == '6F2' && $.trim(rowJson.BARCODE2).length != 0 && $.trim(rowJson.BARCODE3).length != 0)
		return 'ETC';
	else if (lastThreeCode == '010' && $.trim(rowJson.BARCODE2).length != 0 && $.trim(rowJson.BARCODE3).length != 0)
		return 'SeedNet';
	else if (lastThreeCode == '021' && $.trim(rowJson.BARCODE2).length != 0 && $.trim(rowJson.BARCODE3).length != 0)
		return 'NCIC';
	else
		return '';
}

function addETCCard() {
	if (onAjaxRequest) {
		alert('資料聯繫中, 請稍後在執行本程序...');
		return;
	}
	var r = openDialogWindowByEncrypt(baseUrl + '/VSS/CheckOut/ETCCarNumber.aspx?date=' + Date(), 300, 100);
	if (r == null || r == '' || typeof (r) == 'undefined')
		return;
	// ETC加值卡料號(209900003)^車號^單價|e通卡保證金料號(700300008)^車號^單價
	ajaxRequestETCAddValue(r);
}

function openGift() {
    openDialogWindowByEncrypt(baseUrl + '/VSS/SAL/SAL15/SAL15.aspx?date=' + Date(), 600, 400);
}

function setIMEIColumnStatus() {
    var gv = $('#gvMaster');
    var rows = gv.find('tr[jqKey]');
    $.each(rows, function() {
        var tr = $(this);
        var json = getRowData(tr);
        // 處理 IMEI 欄位
        tr.find(':text[jqKey="IMEI"]').val('').hide();
        tr.find(':button[jqKey="CHOOSE_IMEI"]').hide();
        tr.find(':button[jqKey="DELETE_IMEI"]').hide();
        tr.find('td[jqKey="IMEI_ICON"]').empty();
        if (json.IMEI_FLAG != '1') {
            // 檢核 IMEI
            if (typeof (json.IMEI) != 'undefined' && json.IMEI.length != 0)
                tr.find(':text[jqKey="IMEI"]').val(json.IMEI).removeAttr('title').show();
            else
                tr.find(':text[jqKey="IMEI"]').val('').removeAttr('title').show();
            setIMEIIcon(tr, false);
            if (parseInt(json.QUANTITY) == 1) {
                if (json.IMEI_OK == '0') {
	                tr.find(':text[jqKey="IMEI"]').removeAttr('readonly');
                } else {
	                tr.find(':text[jqKey="IMEI"]').attr('readonly', 'readonly');
	                if (!onSaveCacheMode && (typeof (json.IS_SIM) == 'undefined' || json.IS_SIM != '1'))
		                tr.find(':button[jqKey="DELETE_IMEI"]').show();
                }
            } else {
                tr.find(':text[jqKey="IMEI"]').attr('readonly', 'readonly');
                tr.find(':button[jqKey="CHOOSE_IMEI"]').show();
                tr.find(':button[jqKey="DELETE_IMEI"]').hide();
            }
        }
    });
}

function ReloadCache() {
    var rows = $('#gvMaster tr[jqKey]');
    $.each(rows, function(i, o) {
	    var tr = $(o);
		tr.remove();
    });
    ajaxLoadCache();
}