function chkDateE(e) {
    var x = SupportStartDateFrom.GetValue();
    var y = SupportStartDateTo.GetValue();

    if (x == null) { x = ""; }
    if (y == null) { y = ""; }

    if (x != "" && y != "") {
        var SDate = new Date(x);
        var EDate = new Date(y);

        e.isValid = (EDate >= SDate);
        if (!e.isValid) {
            e.errorText = '訖 日期不可小於 起 日期!!';
        }

    } else {
        return true;
    }
}
function chkDateS(e) {
    var x = SupportStartDateFrom.GetValue();
    if (x == null) { x = ""; }
    if (x != "") {
        var today = new Date();

        var EDate = new Date(today.getFullYear() + "/" + today.getMonth() + "/" + today.getDate());

         var SDate = new Date(x);
        //e.isValid = (SDate >= today);
        
        e.isValid = (SDate >= EDate);
        if (!e.isValid) {
            e.errorText = '起日必須大於或等於今日!!';
        }
    } else {
        return true;
    }
}
function getProductsInfo(s, e) {
    _gvEventArgs = e;
    _gvSender = s;
    if (s.GetText() != '')
        PageMethods.getProductsInfo(_gvSender.GetText(), getProductsInfo_OnOK);
}
function getProductsInfo_OnOK(returnData) {
    if (returnData != '') {
        PRODNAME.SetValue(returnData);
        _gvEventArgs.processOnServer = false;
        _gvSender.Focus();
    }
    else {
        PRODNAME.SetValue(null);
    }
}
function getStoreInfo(s, e) {
    _gvEventArgs = e;
    _gvSender = s;
    if (s.GetText() != '')
        PageMethods.getStoreInfo(_gvSender.GetText(), getStoreInfo_OnOK);
}
function getStoreInfo_OnOK(returnData) {
    if (returnData != '') {
        var values = returnData.split(';');
        STORENAME.SetValue(values[0]);
        ZONE.SetValue(values[1]);
        _gvEventArgs.processOnServer = false;
        _gvSender.Focus();
    }
    else {
        STORENAME.SetValue(null);
        ZONE.SetValue(null);
    }
}
function getPromoInfo(s, e) {
    _gvEventArgs = e;
    _gvSender = s;
    if (s.GetText() != '')
        PageMethods.getPromoInfo(_gvSender.GetText(), getPromoInfo_OnOK);
}
function getPromoInfo_OnOK(returnData) {
    if (returnData != '') {
        PROMONAME.SetValue(returnData);
        _gvEventArgs.processOnServer = false;
        _gvSender.Focus();
    }
    else {
        PROMONAME.SetValue(null);
    }
}
function getSetProductsInfo(s, e) {
    _gvEventArgs = e;
    _gvSender = s;
    if (s.GetText() != '')
        PageMethods.getSetProductsInfo(_gvSender.GetText(), getSetProductsInfo_OnOK);
}
function getSetProductsInfo_OnOK(returnData) {
    if (returnData != '') {
        PRODNAME1.SetValue(returnData);
        _gvEventArgs.processOnServer = false;
        _gvSender.Focus();
    }
    else {
        PRODNAME1.SetValue(null);
    }
}
function getAddProdInfo(s, e) {
    _gvEventArgs = e;
    _gvSender = s;
    if (s.GetText() != '')
        PageMethods.getAddProdInfo(_gvSender.GetText(), getAddProdInfo_OnOK);
}
function getAddProdInfo_OnOK(returnData) {
    if (returnData != '') {
        PRODNAME2.SetValue(returnData);
        _gvEventArgs.processOnServer = false;
        _gvSender.Focus();
    }
    else {
        PRODNAME2.SetValue(null);
    }
}
function CheckStoreAll_onclick() {
    for (var i = 0; i < gvStore.pageRowCount; i++) {
        if (gvStore.GetRow(i + gvStore.visibleStartIndex) != null &&
         gvStore.GetRow(i + gvStore.visibleStartIndex).attributes["canSelect"].value == "true") {
            var chk = document.getElementById("checkbox1");
            if (chk.checked) {
                gvStore.SelectRowOnPage(i + gvStore.visibleStartIndex, true);

            } else {

                gvStore.SelectRowOnPage(i + gvStore.visibleStartIndex, false);
            }
        }
    }
}
function CheckProdAll_onclick() {
    for (var i = 0; i < gvProd.pageRowCount; i++) {
        if (gvProd.GetRow(i + gvProd.visibleStartIndex) != null &&
        gvProd.GetRow(i + gvProd.visibleStartIndex).attributes["canSelect"].value == "true") {
            var chk = document.getElementById("checkbox2");
            if (chk.checked) {
                gvProd.SelectRowOnPage(i + gvProd.visibleStartIndex, true);

            } else {

                gvProd.SelectRowOnPage(i + gvProd.visibleStartIndex, false);
            }
        }
    }
}
function CheckPromoAll_onclick() {
    for (var i = 0; i < gvPromo.pageRowCount; i++) {
        if (gvPromo.GetRow(i + gvPromo.visibleStartIndex) != null &&
        gvPromo.GetRow(i + gvPromo.visibleStartIndex).attributes["canSelect"].value == "true") {
            var chk = document.getElementById("checkbox3");
            if (chk.checked) {
                gvPromo.SelectRowOnPage(i + gvPromo.visibleStartIndex, true);

            } else {

                gvPromo.SelectRowOnPage(i + gvPromo.visibleStartIndex, false);
            }
        }
    }
}
function Checkgv1All_onclick() {
    for (var i = 0; i < gv1.pageRowCount; i++) {
        if (gv1.GetRow(i + gv1.visibleStartIndex) != null &&
        gv1.GetRow(i + gv1.visibleStartIndex).attributes["canSelect"].value == "true") {
            var chk = document.getElementById("checkbox4");
            if (chk.checked) {
                gv1.SelectRowOnPage(i + gv1.visibleStartIndex, true);

            } else {

                gv1.SelectRowOnPage(i + gv1.visibleStartIndex, false);
            }
        }
    }
}
function Checkgv2All_onclick() {
    for (var i = 0; i < gv2.pageRowCount; i++) {
        if (gv2.GetRow(i + gv2.visibleStartIndex) != null &&
        gv2.GetRow(i + gv2.visibleStartIndex).attributes["canSelect"].value == "true") {
            var chk = document.getElementById("checkbox5");
            if (chk.checked) {
                gv2.SelectRowOnPage(i + gv2.visibleStartIndex, true);

            } else {

                gv2.SelectRowOnPage(i + gv2.visibleStartIndex, false);
            }
        }
    }
}
function CheckgvCCDAll_onclick() {
    for (var i = 0; i < gvCCD.pageRowCount; i++) {
        if (gvCCD.GetRow(i + gvCCD.visibleStartIndex) != null &&
        gvCCD.GetRow(i + gvCCD.visibleStartIndex).attributes["canSelect"].value == "true") {
            var chk = document.getElementById("checkbox6");
            if (chk.checked) {
                gvCCD.SelectRowOnPage(i + gvCCD.visibleStartIndex, true);

            } else {

                gvCCD.SelectRowOnPage(i + gvCCD.visibleStartIndex, false);
            }
        }
    }
}

function CheckSetAll_onclick() {
    for (var i = 0; i < gvSet.pageRowCount; i++) {
        if (gvSet.GetRow(i + gvSet.visibleStartIndex) != null &&
        gvSet.GetRow(i + gvSet.visibleStartIndex).attributes["canSelect"].value == "true") {
            var chk = document.getElementById("checkbox7");
            if (chk.checked) {
                gvSet.SelectRowOnPage(i + gvSet.visibleStartIndex, true);

            } else {

                gvSet.SelectRowOnPage(i + gvSet.visibleStartIndex, false);
            }
        }
    }
}
function CheckAddProdAll_onclick() {
    for (var i = 0; i < gvAddProd.pageRowCount; i++) {
        if (gvAddProd.GetRow(i + gvAddProd.visibleStartIndex) != null &&
        gvAddProd.GetRow(i + gvAddProd.visibleStartIndex).attributes["canSelect"].value == "true") {
            var chk = document.getElementById("checkbox8");
            if (chk.checked) {
                gvAddProd.SelectRowOnPage(i + gvAddProd.visibleStartIndex, true);

            } else {

                gvAddProd.SelectRowOnPage(i + gvAddProd.visibleStartIndex, false);
            }
        }
    }
}    