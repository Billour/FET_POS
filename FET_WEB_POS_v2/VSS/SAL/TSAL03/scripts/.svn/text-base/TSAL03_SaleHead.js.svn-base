var SaleHeadDefined = {
	POSUUID_MASTER: '', // 主單號
	STORE_NO: '', 		// 門市編號
	STORE_NAME: '',		// 門市名稱
	TRADE_DATE: '', 	// 交易日期
	SALE_NO: '', 		// 交易流水編號，於交易確認結帳後顯示。
	INVOICE_NO: '', 	// 發票號碼
	UNI_NO: '', 		// 統一編號，公司統一編號或稅籍編號，可以不輸入(空白)
	UNI_TITLE: '', 		// 發票抬頭，輸入公司名稱或個人姓名，可以不輸入(空白)。
	SALE_TYPE: '1', 	// 交易種類
	SALE_STATUS: '1',	// 交易狀態
	CREATE_DTM: '', 	// 新增日期
	CREATE_USER: '', 	// 新增人員，顯示交易維護人員員工編號及姓名，由系統自動代入。
	HG_CARD_NO: '', 	// Happy Go 卡號
	HG_AWARD_POINT: '', // Happy Go 累積點數
	HG_REMAIN_POINT: '', // Happy Go 剩餘點數
	HG_PRODNO: '',		// Happy Go 累點料號';
	ORIGINAL_ID: '', 	// 最原始的ID, 從未作廢的會與ID同，退換貨時會一路帶下來
	SALE_PERSON: '', 	// 銷售人員
	MODI_USER: '', 		// 異動人員
	MODI_DTM: '', 		// 異動時間
	MACHINE_ID: '',		// 機台編號
	INVALID_DATE: '',	// 作廢日期
	INVALID_NO: '',		// 作廢序號
	INVALID_REMARK: ''	// 作廢備註
}

function saleStatusName(status) {
	// 取得銷售狀態名稱
    switch (parseInt(status)) {
        case 1: return '未結帳';
        case 2: return '已結帳'; 
        case 3: return '退貨作廢'; 
        case 4: return '跨月退貨作廢'; 
        case 5: return '換貨作廢'; 
        case 6: return '跨月換貨作廢'; 
        case 7: return '換貨未結帳'; 
        case 8: return '交易補登未結帳'; 
        case 9: return '紙本授權未結帳'; 
        default: return '';
    }
}

function saleTypeName(type) {
	// 取得銷售種類名稱
    switch (parseInt(type)) {
        case 1: return '銷售交易';
        case 2: return '帳單代收';
        case 3: return '直接交易';
        default: return'';
    }
}