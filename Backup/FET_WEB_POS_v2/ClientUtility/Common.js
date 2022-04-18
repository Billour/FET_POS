/// <reference path="CheckDate.js" />


//jasonChen 2010/11/17 start
function valid(n) {
	return (n % 10 == 0) ? true : false;
}
function cal(n) {
	var sum = 0;
	while (n != 0) {
		sum += (n % 10);
		n = (n - n % 10) / 10;  // 取整數 
	}
	return sum;
}
String.prototype.Uni_NoCheck = function() {
	var tmp = new String("12121241");
	var sum = 0;
	var idvalue = this;
	re = /^\d{8}$/;
	if (!re.test(idvalue)) {
		//  alert("格式不對！"); 
		return false;
	}
	for (i = 0; i < 8; i++) {
		s1 = parseInt(idvalue.substr(i, 1));
		s2 = parseInt(tmp.substr(i, 1));
		sum += cal(s1 * s2);
	}
	if (!valid(sum)) {
		if (idvalue.substr(6, 1) == "7") return (valid(sum + 1));
	}
	return (valid(sum));
}

if (!Array.prototype.indexOf) {
	Array.prototype.indexOf = function(elt /*, from*/) {
		var len = this.length;
		var from = Number(arguments[1]) || 0;
		from = (from < 0) ? Math.ceil(from) : Math.floor(from);
		if (from < 0)
			from += len;
		for (; from < len; from++) {
			if (from in this && this[from] === elt)
				return from;
		}
		return -1;
	};
}
//jasonChen 2010/11/17 end

var mx;
var my;
$(document).mousemove(function(event) {
	mx = event.pageX;
	my = event.pageY;
});

//顯示浮動視窗
function show(content) {
	var tip = $("#tooltip");
	tip.html(content);
	tip.css({
		display: "",
		left: mx - 150,
		top: my,
		position: "absolute",
		background: "#FFFFFF"
	});

}

//隱藏浮動視窗
function hide() {
	var tip = $("#tooltip");
	tip.css("display", "none");

}

//顯示彈跳視窗
function openwindow(url, width, height) {
	window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
}

//顯示彈跳視窗
function openDialogWindow(url, width, height) {
	return window.showModalDialog(url, self, 'dialogWidth:' + width + 'px;dialogHeight:' + height + 'px;status:no;resizable:yes;scroll:yes');
}

//比較起訖商品類別，結束類別不可早於開始類別
function chkProdType(s, e) {
	var x = ddlPRODTYPENO_S.GetValue();
	var y = ddlPRODTYPENO_E.GetValue();

	if (x == null) { x = ""; }
	if (y == null) { y = ""; }

	if ((x != "" && y != "")) {

		e.isValid = (x <= y);
		if (!e.isValid) {
			alert("訖 商品類別不可早於 起 商品類別!!");
			s.SetValue(null);
			return false;
		}
	}
	else {
		return true;
	}
}


/// <summary>
/// ASPxGridView CheckBox全選；若該Row不可勾選(canSelect = false)，則選取全選時會忽略此列，否則選取此列。
/// </summary>
/// <param name="checktext">輸入字串</param>
/// <returns>是否</returns>
function CheckAll_onclick() {
	if (typeof (gvMaster) != 'undefined') {
		for (var i = 0; i < gvMaster.pageRowCount; i++) {
			if (gvMaster.GetRow(i + gvMaster.visibleStartIndex) != null &&
                gvMaster.GetRow(i + gvMaster.visibleStartIndex).attributes["canSelect"].value == "true") {
				var chk = document.getElementById("checkbox1");
				if (chk.checked) {
					gvMaster.SelectRowOnPage(i + gvMaster.visibleStartIndex, true);

				} else {

					gvMaster.SelectRowOnPage(i + gvMaster.visibleStartIndex, false);
				}
			}
		}
	}
}

/// <summary>
/// 依照物件的類型及ID，取得DevExpress ClientInstance
/// </summary>
/// <param name="checktext">輸入字串</param>
/// <returns>ClientInstance</returns>
function getClientInstance(type, name) {
	var _CI = window[name];
	if (_CI == null) {
		switch (type) {
			case 'TxtBox':
				_CI = new ASPxClientTextBox(name);
				break;
			case 'Button':
				_CI = new ASPxClientButton(name);
				break;
			case 'Label':
				_CI = new ASPxClientLabel(name);
				break;
			case 'Image':
				_CI = new ASPxClientImage(name);
				break;
			case 'Popup':
				_CI = new ASPxClientPopupControl(name);
				break;
		}
		window[name] = _CI;
	}
	return _CI;
}

/// <summary>
/// 取得頁面URL傳來的參數值
/// </summary>
/// <param name="name">參數名稱</param>
/// <returns>參數值</returns>
function QueryString(name) {
	var AllVars = window.location.search.substring(1);
	var Vars = AllVars.split("&");
	for (i = 0; i < Vars.length; i++) {
		var Var = Vars[i].split("=");
		if (Var[0] == name) return Var[1];
	}
	return "";
}

/// <summary>
/// 是否為整數
/// </summary>
/// <param name="str">值</param>
/// <returns>是/否</returns>
function isInteger(str) {
	var regu = /^[-]{0,1}[0-9]{1,}$/;
	return regu.test(str);
}

/// <summary>
/// 傳入信用卡號, 傳回信用卡別
/// </summary>
/// <param name="cardNo">信用卡號</param>
/// <returns>卡別: VISA / MASTER / AE / JCB; 卡號錯誤時,傳回空字串</returns>
function checkCardType(cardNo) {
	var result = '';
	if (cardNo.length == 16) {
		// VISA, MASTER, JCB
		if (parseInt(cardNo.substr(0, 1)) == 4)
			result = "VISA";
		else if (parseInt(cardNo.substr(0, 2)) >= 51 && parseInt(cardNo.substr(0, 2)) <= 55)
			result = "MASTER";
		else if (parseInt(cardNo.substr(0, 3)) >= 300 && parseInt(cardNo.substr(0, 3)) <= 399)
			result = "JCB";
	}
	else if (cardNo.length == 15) {
		// American Express(AE), JCB
		if (parseInt(cardNo.substr(0, 3)) >= 340 && parseInt(cardNo.substr(0, 3)) <= 379)
			result = "AE";
		else if (parseInt(cardNo.substr(0, 4)) == 1800 || parseInt(cardNo.substr(0, 4)) == 2131)
			result = "JCB";
	}
	return result;
}

/// <summary>
/// 檢查信用卡號是否合法
/// </summary>
/// <param name="cardNo">信用卡號</param>
/// <returns>True: 合法, False: 非法卡號</returns>
function checkCreditCardNo(cardNo) {
	// 16 碼的加權數字為 : Math.Abs(index % 2 - 2)
	// 15 碼的加權數字為 : Math.Abs((index + 1) % 2 - 2)
	if (cardNo.length != 15 && cardNo.length != 16)
		return false;
	var total = 0;
	for (var i = 0; i < cardNo.length - 1; i++) {
		var v = 0;
		if (cardNo.length == 16)
			v = Math.abs(i % 2 - 2) * parseInt(cardNo.substr(i, 1));
		else
			v = Math.abs((i + 1) % 2 - 2) * parseInt(cardNo.substr(i, 1));
		if (v >= 10)
			v = Math.floor(v / 10) + v % 10;
		total += v;
	}
	return ((parseInt(cardNo.substr(cardNo.length - 1)) + total) % 10 == 0);
}


/// <summary>
/// 將URL傳遞的參數加密
/// </summary>
/// <param name="windowType">"showModalDialog" / "open"</param>
/// <param name="object">Popup Control</param>
/// <param name="url">URL路徑</param>
/// <param name="Param">傳遞的參數</param>
/// <param name="width">新視窗寬度</param>
/// <param name="height">新視窗高度</param>
/// <returns></returns>
function Encrypt(windowType, object, url, Param, width, height) {
    var r = '';
    $.ajax({
        url: '../../../EncryptPage.aspx',
        type: 'GET',
        data: {
            URLParam: Param
        },
        async: false,  //為了回傳值，必須將 async 設為false
        error: function(xhr) {
            alert('error');
            alert(xhr.responseText);
            //object.SetContentUrl('../../../ErrorPage.htm');
            var NewUrl = "../../../ErrorPage.htm";
            if (object != null) {
                object.SetContentUrl(NewUrl);
            }
            else {
                if (windowType == "open") {
                    //window.open
                    openwindow(NewUrl, width, height);
                }
                else {
                    //window.showModalDialog
                    r = openDialogWindow(NewUrl, width, height);
                }
            }
        },
        success: function(response) {
            var t = new Date().getTime();
            var NewUrl = url + '?t=' + t;
            if (Param != '') {
                NewUrl += '&Param=' + response;
            }

            if (object != null) {
                //object.SetContentUrl(url + '?Param=' + response + '&t=' + t);
                object.SetContentUrl(NewUrl);
            }
            else {
                if (windowType == "open") {
                    //window.open
                    openwindow(NewUrl, width, height);
                }
                else {
                    //window.showModalDialog
                    r = openDialogWindow(NewUrl, width, height);
                }
            }
        }
    });
    return r;
}

/// <summary>
/// 開新視窗(傳遞的參數須先加密)
/// </summary>
/// <param name="url">URL路徑</param>
/// <param name="width">新視窗寬度</param>
/// <param name="height">新視窗高度</param>
/// <returns></returns>
function openwindowByEncrypt(url, width, height) {
    var s = url.split('?');
    if (s.length > 1) {
        Param = s[1];
    }

    //**2011/04/25 Tina：將URL傳遞的參數加密。
    Encrypt("open", null, s[0], Param, width, height);
}

/// <summary>
/// 開新視窗(傳遞的參數須先加密)
/// </summary>
/// <param name="url">URL路徑</param>
/// <param name="width">新視窗寬度</param>
/// <param name="height">新視窗高度</param>
/// <returns></returns>
function openDialogWindowByEncrypt(url, width, height) {

    var Param = "";
    var s = url.split('?');
    if (s.length > 1) {
        Param = s[1];
    }

    //**2011/04/25 Tina：將URL傳遞的參數加密。
    return Encrypt("showModalDialog", null, s[0], Param, width, height);
}

/// <summary>
/// 開新視窗(傳遞的參數須先加密)
/// </summary>
/// <param name="url">URL路徑(包含傳遞的參數)</param>
/// <param name="object">Popup Control</param>
/// <returns></returns>
function ModifyPopupURLByEncrypt(url, object) {
    var Param = "";
    var s = url.split('?');
    if (s.length > 1) {
        Param = s[1];
    }
    
    //**2011/04/25 Tina：將URL傳遞的參數加密。
    Encrypt(null, object, s[0], Param, null, null);
}

/// <summary>
/// Loading遮罩
/// </summary>
/// <param name="str">Loading時顯示的文字描述</param>
/// <returns></returns>
function Loading(str) {
    var msgw, msgh, bordercolor;
    msgw = 300;                 //提示窗口的寬度
    msgh = 90;                  //提示窗口的高度
    titleheight = 25            //提示窗口標題高度
    bordercolor = "#336699";    //提示窗口的邊框顏色
    titlecolor = "#99CCFF";     //提示窗口的標題顏色

    var sWidth, sHeight;
    sWidth = document.body.offsetWidth;
    sHeight = screen.height;

    //遮罩
    var bgObj = document.createElement("div");
    bgObj.setAttribute('id', 'bgDiv');
    bgObj.style.position = "absolute";
    bgObj.style.top = "0";
    bgObj.style.background = "#777";
    bgObj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";
    bgObj.style.opacity = "0.6";
    bgObj.style.left = "0";
    bgObj.style.width = sWidth + "px";
    bgObj.style.height = sHeight + "px";
    bgObj.style.zIndex = "10000";
    document.body.appendChild(bgObj);

    //提示視窗
    var msgObj = document.createElement("div")
    msgObj.setAttribute("id", "msgDiv");
    msgObj.setAttribute("align", "center");
    msgObj.style.background = "white";
    msgObj.style.border = "1px solid " + bordercolor;
    msgObj.style.position = "absolute";
    msgObj.style.left = "50%";
    msgObj.style.top = "50%";
    msgObj.style.font = "12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif";
    msgObj.style.marginLeft = "-225px";
    msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";
    msgObj.style.width = msgw + "px";
    msgObj.style.height = msgh + "px";
    msgObj.style.textAlign = "center";
    msgObj.style.lineHeight = "25px";
    msgObj.style.zIndex = "10001";

    document.body.appendChild(msgObj);
    var txt = document.createElement("p");
    txt.style.margin = "1em 0"
    txt.setAttribute("id", "msgTxt");
    txt.innerHTML = str;
    document.getElementById("msgDiv").appendChild(txt);

}

/// <summary>
/// 提示訊息遮罩
/// </summary>
/// <param name="str">提示訊息的文字描述</param>
/// <returns></returns>
function ShowMessage(str) {
    CloseMessage();
    if (str != "") {
        Loading(str);

        var myElement = document.createElement("BUTTON");
        myElement.id = "btnCloseDiv";
        myElement.name = "btnCloseDiv";
        myElement.value = "Close";
        myElement.width = "20px";
        myElement.height = "20px";
        myElement.onclick = function chk() { CloseMessage(); };
        document.getElementById("msgDiv").appendChild(myElement);

    }
}

/// <summary>
/// 關閉提示視窗遮罩
/// </summary>
/// <returns></returns>
function CloseMessage() {
    var bgObj = document.getElementById("bgDiv");
    var msgObj = document.getElementById("msgDiv");

    if (bgObj != null) document.body.removeChild(bgObj);
    if (msgObj != null) document.body.removeChild(msgObj);
}
