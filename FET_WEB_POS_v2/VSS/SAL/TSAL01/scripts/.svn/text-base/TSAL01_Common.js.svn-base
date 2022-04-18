var statusCode = {
	404: function() { alert('找不到網頁, 請通知系統管理員 !'); },
	500: function() { alert('程式錯誤, 請通知系統管理員 !'); }
};

function json2String(o) {
	// 將 JSON 物件轉換成字串
	var r = [];
	if (typeof o == "string" || o == null) {
		return '\'' + o + '\'';
	}
	if (typeof o == 'object') {
		if (!o.sort) {
			r[0] = '{'
			for (var i in o) {
				r[r.length] = i;
				r[r.length] = ':';
				r[r.length] = json2String(o[i]);
				r[r.length] = ',';
			}
			if (r[r.length - 1] == ',')
				r.length--;
			r[r.length] = '}'
		} else {
			r[0] = '['
			for (var i = 0; i < o.length; i++) {
				r[r.length] = json2String(o[i]);
				r[r.length] = ',';
			}
			if (r[r.length - 1] == ',')
				r.length--;
			r[r.length] = ']'
		}
		return r.join('');
	}
	return o.toString();
}

function cloneEmptyObject(o) {
	// 複製 JSON 物件, 並將資料清空
	var r = eval('(' + json2String(o) + ')');
	if (typeof r == "string" || o == null) {
		return '';
	}
	if (typeof r == 'object') {
		if (!r.sort) {
			for (var i in r) {
				r[i] = cloneEmptyObject(o[i]);
			}
		} else {
			r = [];
		}
		return r;
	}
	return r;
}

function cloneJson(o) {
	// 複製 JSON 物件
	return eval('(' + json2String(o) + ')');
}

function disableAllButton() {
	$.each($(':button,:submit'), function() {
		if (!eval($(this).attr('disabled'))) {
			$(this).attr({ 'disabled': 'disabled', 'jqTmp': 'enabled' });
		}
	});
}

function restoreAllButtonStatus() {
	$(':button[jqTmp],:submit[jqTmp]').removeAttr('disabled').removeAttr('jqTmp');
}

function setRowData(row, data) {
	// 設定資料行的資料
	var tr = null;
	if (typeof (row) == 'string')
		tr = $('#' + row);
	else
		tr = $(row);
	tr.data('DataRow', data);
}

function getRowData(row) {
	// 取得資料行的資料
	var tr = null;
	if (typeof (row) == 'string')
		tr = $('#' + row);
	else
		tr = $(row);
	if (typeof (tr.data('DataRow')) == 'undefined')
		return null;
	else
		return tr.data('DataRow');
}

function getAllRowData(grid) {
	var gv = null;
	if (typeof (grid) == 'string')
		gv = $('#' + grid);
	else
		gv = $(grid);
	var result = [];
	$.each(gv.find('tr[jqKey]'), function(i, r) {
		result[result.length] = getRowData($(r));
	});
	return result;
}

function checkCompanyId(id) {
	// REF: http://herolin.mine.nu/entry/is-valid-TW-company-ID
	// 檢核統一編號是否有效
	// 檢核統編的做法大致上為1至8位數分別乘上1,2,1,2,1,2,4,1，將八個相乘結果的十位數與個位數相加在一起，
	// 除10除得盡就OK，若除不盡餘9且第7位是7的話，也算過關。
	var idChkFac = [1, 2, 1, 2, 1, 2, 4, 1];
	if (id.length != 8 || !id.match(/^\d{8}$/))
		return false;
	var sum = 0;
	for (var i = 0; i < 8; i++) {
		var d = (id.charCodeAt(i) - 48) * idChkFac[i];
		sum += Math.floor(d / 10) + d % 10;
	}
	return (sum % 10 == 0 || (id.substr(6, 1) == '7' && sum % 10 == 9))
}

function setWaitingMsg(objId, msg) {
	// 設定等待訊息
	var canvas = $('#' + objId);
	if (msg.length == 0) {
		canvas.empty();
	} else {
		if (typeof (imgLoadingUrl) == 'undefined' || imgLoadingUrl == null || imgLoadingUrl.length == 0)
			canvas.append(msg);
		else
			canvas
				.append(msg)
				.append(
					$('<img src="' + imgLoadingUrl + '" />')
						.css({ 'margin-left': '5px' })
						.width(16)
						.height(16)
				);
	}
}

function swapTextboxToLabel(obj) {
	// 將文字輸入框(:text)轉為顯示欄位(span)
	if (obj.parent().find('span[jqKey]').length >= 1)
		return;
	$('<span />')
		.attr('jqKey', 'DisplayOnly')
		.attr('title', obj.val())
		.text(obj.val())
		.appendTo(obj.parent());
	obj.hide();
}