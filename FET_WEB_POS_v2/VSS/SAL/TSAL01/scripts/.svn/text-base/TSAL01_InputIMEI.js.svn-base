var rowJson = null;
var txtIMEI = null;
var imeiLog = []; 	// 紀錄本次操作的 IMEI 歷程
/*
	{ TYPE: 0, IMEI:'AAAAA', ORIG: 0 }
	TYPE : 0 = 原始
	TYPE : 1 = 本次新增
	TYPE : 2 = 本次刪除
*/

$(document).ready(function() {
	$(':button').addClass('buttonStyle');
	$(':text').addClass('textBoxStyle');
	$(':submit').addClass('buttonStyle');
	txtIMEI = $('#txtIMEI');
	txtIMEI.keydown(function(e) { txtIMEI_KeyDown(e, this); }).focus(function() { $(this).select(); }).focus();
	rowJson = window.dialogArguments;
	$('#labProdNo').text(rowJson.PRODNO);
	$('#labProdName').text(rowJson.PRODNAME);
	$(':button[jqKey="btnOK"]').click(feedbackIMEI);
	$(':button[jqKey="btnCancel"]').click(cancelIMEI);
	loadIMEI();
});

function loadIMEI() {
	if (typeof (rowJson.IMEI) != 'undefined' && rowJson.IMEI.length != 0) {
		$.each(rowJson.IMEI.split(','), function(i, v) {
			imeiLog[imeiLog.length] = { TYPE: 0, IMEI: v, ORIG: 0 };
			var tr = createIMEIItemRow();
			if (tr != null) {
				setRowData(tr, v);
				tr.find('td[jqKey="IMEI"]').text(v);
				if (imeiLog.length >= parseInt(rowJson.QUANTITY)) {
					$(':button[jqKey="btnInput"]').attr('disabled', 'disabled');
					txtIMEI.attr('readonly', 'true');
				}
			} else {
				alert('無法新增資料列 !');
				return;
			}
		});
		if ($('#gvIMEI tr[jqKey]').length >= parseInt(rowJson.QUANTITY)) {
			$('#btnInput').attr('disabled', 'disabled');
			txtIMEI.attr('readonly', 'true');
		}
	}
}

function txtIMEI_KeyDown(e, o) {
	// IMEI 欄位 Keydown Event
	if (e.keyCode == 13) {
		if ($('#gvIMEI tr[jqKey]').length >= parseInt(rowJson.QUANTITY) && txtIMEI.val().length == 0)
			feedbackIMEI();
		else
			checkIMEI();
		event.returnValue = false;
		return false;
	}
}

function checkIMEI() {
	// 檢查 IMEI 是否鎖定
	var imei = $.trim(txtIMEI.val());
	var idx = searchIMEILog(imei);
	if (idx == -1) {
		ajaxCheckIMEIExists(imei);
	}  else if (imeiLog[idx].TYPE <= 1) {
		alert('此 IMEI(' + imei + ')已於本次操作中新增 !');
	} else if (imeiLog[idx].TYPE == 2) {
		var tr = createIMEIItemRow();
		if (tr != null) {
			if (imeiLog[idx].ORIG == 0)
				imeiLog[idx].TYPE = 0;
			else
				imeiLog[idx].TYPE = 1;
			//tr.find('td:nth-child(1) input[id$="_data"]').val(imeiLog[idx].IMEI);
			setRowData(tr.attr('id'), imeiLog[idx].IMEI);
			tr.find('td[jqKey="IMEI"]').text(imeiLog[idx].IMEI);
			txtIMEI.val('');
			if ($('#gvIMEI tr[jqKey]').length >= parseInt(rowJson.QUANTITY)) {
				$('#btnInput').attr('disabled', 'disabled');
				txtIMEI.attr('readonly', 'true');
			}
		} else {
			alert('無法新增資料列 !');
		}
	}
}

function checkAllCheckboxs() {
	// 選擇或取消核選所有資料列
	var gv = $('#gvIMEI');
	var chk = $('#chkAllRow');
	var chks = gv.find(':checkbox:visible').attr('checked', chk.attr('checked'));
}

function deleteIMEIItemRow() {
	// 刪除選擇項目
	var gv = $('#gvIMEI');
	var rows = gv.find('tr[jqKey]');
	var chks = gv.find(':checkbox[jqKey]:visible:checked');
	if (chks.length == 0 || !confirm('您是否要刪除已勾選的 IMEI ?'))
		return;
	var imeis = [];
	setWaitingMsg('divMasterMsg', '清除本次預存的 IMEI 資料中, 請稍後...');
	$.each(chks, function() {
		var tr = $(this).parents('tr').first();
		var idx = searchIMEILog(getRowData(tr));
		if (idx != -1) {
			imeiLog[idx].TYPE = 2;
		}
		tr.remove();
	});
	reassignElementID(gv);
	setWaitingMsg('divMasterMsg', '');
}

function reassignElementID(gv) {
	// 重新指定 Grid 中所有元素的 ID
	var gvId = gv.attr('id');
	var rows = gv.find('tr[jqKey]');
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
}
function feedbackIMEI() {
	// 回傳已輸入的 IMEI 資料
	var backIMEI = [];	// 回傳的 IMEI
	var deleteIMEI = [];	// 刪除的 IMEI

	// 先刪除暫存鎖定的 IMEI
	$.each(imeiLog, function(i, v) {
		if (v.TYPE == 0 || v.TYPE == 1)
			backIMEI[backIMEI.length] = v.IMEI;
		else if (v.TYPE == 2)
			deleteIMEI[deleteIMEI.length] = v.IMEI;
	});
	if (deleteIMEI.length != 0) {
		ajaxDeleteLockedIMEI(deleteIMEI.join(';'), function() {
			if (backIMEI.length != 0)
				returnValue = backIMEI.join(',');
			else
				returnValue = '<EMPTY>';
			self.close();
			//opener.imeiInputDone(hfRowID.val(), backIMEI.join(','));
			//window.close();
		});
	} else {
		if (backIMEI.length != 0)
			returnValue = backIMEI.join(',');
		else
			returnValue = '<EMPTY>';
		self.close();
		//opener.imeiInputDone(hfRowID.val(), backIMEI.join(','));
		//window.close();
	}
}

function cancelIMEI() {
	// 取消輸入 IMEI
	var backIMEI = []; // 回傳的 IMEI
	var deleteIMEI = []; // 刪除的 IMEI

	// 先刪除暫存鎖定的 IMEI
	$.each(imeiLog, function(i, v) {
		if (v.ORIG == 0)
			backIMEI[backIMEI.length] = v.IMEI;
		else (v.ORIG != 0 || v.TYPE == 2)
			deleteIMEI[deleteIMEI.length] = v.IMEI;
	});
	if (deleteIMEI.length != 0) {
		ajaxDeleteLockedIMEI(deleteIMEI.join(';'), function() {
			self.close();
		});
	} else {
		self.close();
	}
}

function ajaxCheckIMEIExists(imei) {
	// 檢查 IMEI 是否可使用, 供多筆檢查用
	var gv = $('#gvIMEI');
	var isExists = false;
	$.each(gv.find('input[type="hidden"]'), function() {
		if ($.trim($(this).val()) == imei) {
			isExists = true;
			return;
		}
	});
	if (isExists) {
		alert('此 IMEI(' + imei + ') 已選用');
		txtIMEI.focus();
		return;
	}
	setWaitingMsg('divMasterMsg', '檢核 IMEI 資料中, 請稍後...');
	$.ajax({
		type: 'GET',
		url: 'AjaxQuery.ashx?mode=imei&imei=' + imei + '&did=' + rowJson.ID + '&prodno=' + rowJson.PRODNO,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			if (data.length == 0) {
				alert('無法取得資料');
				return;
			} else if (data.length <= 2) {
				alert('無此 IMEI(' + imei + ') 資料');
				txtIMEI.focus();
			} else {
				var json = eval('(' + data + ')');
				if (json.MESSAGE.length != 0) {
					alert(json.MESSAGE);
					txtIMEI.focus();
				} else {
					var tr = createIMEIItemRow();
					if (tr != null) {
						var idx = searchIMEILog(json.IMEI);
						if (idx == -1)
							imeiLog[imeiLog.length] = { TYPE: 1, IMEI: json.IMEI, ORIG: 1 };
						else
							imeiLog[idx].TYPE = 1;
						txtIMEI.val('');
						setRowData(tr.attr('id'), json.IMEI);
						tr.find('td[jqKey="IMEI"]').text(json.IMEI);
						if (gv.find('tr[jqKey]').length >= parseInt(rowJson.QUANTITY)) {
							$('#btnInput').attr('disabled', 'disabled');
							txtIMEI.attr('readonly', 'true');
						}
					}
				}
			}
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
		}
	});
}

function ajaxDeleteLockedIMEI(imeis, callback) {
	// imeis : IMEI1;IMEI2;IMEI3...
	// 刪除已被鎖定的 IMEI 資料
	// callback : 回呼函式
	setWaitingMsg('divMasterMsg', '解除鎖定 IMEI 資料中, 請稍後...');
	$.ajax({
		type: 'GET',
		url: 'AjaxQuery.ashx?mode=del_imei&detailid=' + rowJson.ID + '&imeis=' + imeis,
		success: function(data) {
			setWaitingMsg('divMasterMsg', '');
			if (data.length == 0) {
				alert('無法解除鎖定資料');
				return;
			}
			var json = eval('(' + data + ')');
			if (json.RESULT != '1')
				alert(json.ERROR);
			else {
				if (callback != null)
					callback();
			}
		},
		error: function(e, xhr, settings, exception) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
		}
	});
}

function searchIMEILog(imei) {
	// 搜尋 IMEI 在本次操作紀錄中的 Index
	var idx = -1;
	$.each(imeiLog, function(i, v) {
		if (idx != -1) return;
		if (v.IMEI == imei) {
			idx = i;
			return;
		}
	});
	return idx;
}