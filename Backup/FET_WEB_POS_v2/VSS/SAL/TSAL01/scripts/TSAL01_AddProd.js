var parentRowJson = null;
var onAjaxRequest = false;
var discountCode = '';

$(document).ready(function() {
	parentRowJson = window.dialogArguments;
	$('#labProdNo').text(parentRowJson.PRODNO);
	$('#labProdName').text(parentRowJson.PRODNAME);
	$(':button[jqKey="btnConfirm"]').click(feedbackProds);
	$(':button[jqKey="btnCancel"]').click(cancelProds);
	$(':button').addClass('buttonStyle');
	$(':text').addClass('textBoxStyle');
	ajaxGetProdList();
	$(document).keydown(function(e) {
		if (e.keyCode == 27)
			window.close();
	});
});

function feedbackProds() {
	var prods = [];
	$.each($('select option:selected'), function(i, o) {
		var tr = $(o).parents('tr:first')
		var txt = tr.find(':text[jqKey="QTY"]');
		if ($(o).attr('value') != '' && parseInt(txt.val()) != 0) {
			var j = cloneJson($(o).data('ProdInfo'));
			j.QUANTITY = txt.val();
			prods[prods.length] = j;
		}
	});
	
	// 回傳加價購折扣料號
	if (prods.length > 0)
	    prods[prods.length] = discountCode;
	returnValue = prods;
	self.close();
}

function cancelProds() {
	self.close();
}

function importDataToRow(tr, json) {
	var ddl = tr.find('select')
		.empty()
		.change(function() { changeProd($(this)); })
		.append(
			$('<option />')
				.attr({ 'value': '', 'title': '請選擇商品' })
				.text('請選擇商品')
		);
	
	setRowData(tr, json);
	$.each(json.PRODS, function(i, js) {
		ddl.append(
			$('<option />')
				.attr({ 'value': js.PRODNO, 'title': js.PRODNO + ' - ' + js.PRODNAME })
				.text(js.PRODNO + ' - ' + js.PRODNAME)
				.data('ProdInfo', cloneJson(js))
		)
	});
	tr.find('td[jqKey="UNI_PRICE"]').text(0);
	tr.find('td[jqKey="SALE_PRICE"]').text(0);
	tr.find('td[jqKey="AMOUNT"]').text(0);
	tr.find(':text[jqKey="QTY"]')
		.numeric({ 
			buttons: false, 
			keyboard: false, 
			minValue: 1, 
			maxValue: parseInt(parentRowJson.QUANTITY),
			title: '加價購數量最多為 ' + parentRowJson.QUANTITY
		})
		.blur(function() { recountAmount(this); })
		.val(0);
		
	// 取得加價購折扣料號
	discountCode = json.DISCOUNT_CODE;
}

function changeProd(ddl) {
	var tr = ddl.parents('tr:first');
	var opt = ddl.find('option:selected');
	var json = opt.data('ProdInfo');
	if (typeof (json) == 'undefined') {
		tr.find('td[jqKey="UNI_PRICE"]').text(0);
		tr.find('td[jqKey="SALE_PRICE"]').text(0);
		tr.find('td[jqKey="AMOUNT"]').text(0);
		tr.find(':text[jqKey="QTY"]').val(0);
	} else {
		tr.find('td[jqKey="UNI_PRICE"]').text(json.UNI_PRICE);
		tr.find('td[jqKey="SALE_PRICE"]').text(json.SALE_PRICE);
		tr.find('td[jqKey="AMOUNT"]').text(parseInt(tr.find(':text[jqKey="QTY"]').val()) * json.SALE_PRICE);
	}
}

function recountAmount(o) {
	var tr = $(o).parents('tr:first');
	var opt = tr.find('select option:selected');
	var json = opt.data('ProdInfo');
	if (typeof (json) == 'undefined')
		tr.find('td[jqKey="AMOUNT"]').text(0);
	else
		tr.find('td[jqKey="AMOUNT"]').text(parseInt(tr.find(':text[jqKey="QTY"]').val()) * json.SALE_PRICE);
}

function ajaxGetProdList() {
	// 取得產品資料
	setWaitingMsg('divLoadingMsg', '<br />讀取加價購商品資料中, 請稍候...');
	onAjaxRequest = true;
	disableAllButton();
	$.ajax({
		statusCode: statusCode,
		type: 'GET',
		url: 'AjaxQuery.ashx?mode=get_add_prod&prodno=' + parentRowJson.PRODNO + '&did=' + parentRowJson.POSUUID_DETAIL,
		success: function(data) {
			setWaitingMsg('divLoadingMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
			if (data.length == 0) {
				alert('無法取得加價購商品資料-1 !');
				return;
			}
			var json = {};
			try {
				json = eval('(' + data + ')');
				if (typeof (json) == 'undefined') {
					alert('無法取得加價購商品資料-2 !');
					return;
				} else if (json.length == 0) {
					$('#divLoadingMsg').html('<br />此商品並無搭配的加價購商品');
					return;
				}
			} catch (e) {
				alert(e + '\n\n' + data);
				return;
			}
			$('#divLoadingMsg').remove();
			$.each(json, function(i, jq) {
				var tr = createAddProdRow();
				importDataToRow(tr, jq);
			});
		},
		error: function(jqXHR, textStatus, errorThrown) {
			alert('ERROR : \n' + errorThrown);
			setWaitingMsg('divMasterMsg', '');
			onAjaxRequest = false;
			restoreAllButtonStatus();
		}
	});
}