/// <summary>
/// 判斷是否是日期；若是，則回傳true，否則回傳false。
/// </summary>
/// <param name="checktext">輸入字串</param>
/// <returns>是否</returns>
function IsDate(checktext) {
    var datetime;
    var year, month, day;
    var gone, gtwo;

    if (checktext != "") {
        datetime = checktext;
        //        if (datetime.length != 10)
        //            return false;
        var tmpAry = datetime.split('/');
        if (tmpAry.length != 3) { return false; }
        year = parseInt(tmpAry[0], 10);
        if (isNaN(year) == true || year < 1900 || year > 2100) { return false; }
        month = parseInt(tmpAry[1].replace(/^0+/, ''), 10);
        if (isNaN(month) == true || month < 1 || month > 12) { return false; }
        if (tmpAry[2].indexOf('.') >= 0) return false;
        day = parseInt(tmpAry[2], 10); //.replace(/^0+/, '')
        if (month.length < 2) { month = this.padLeft(month, 2) }

        if (day.length < 2) { day = this.padLeft(day, 2) }
        if (isNaN(day) == true || day < 1 || day > 31) { return false; }

        if (month == 2) {
            if (isLeapYear(year) && day > 29) { return false; }
            if (!isLeapYear(year) && day > 28) { return false; }
        }

        if ((month == 4 || month == 6 || month == 9 || month == 11) && (day > 30)) { return false; }

    } else { return true; }
    return true;
}

function isLeapYear(year) {
    if (((year % 4) == 0) && ((year % 100) != 0) || ((year % 400) == 0))
        return (true);
    else
        return (false);
}

/// <summary>
/// 檢查使用者在ASPxDateEdit物件中輸入的字串是否為日期格式
/// </summary>
/// <param name="s"></param>
/// <param name="e"></param>
function ASPxDateEditParseDate(s, e) {
    var sValue = e.value;

    if (sValue != null && sValue != '') {

        var sEditFormatString = s.dateFormatter.mask;

        if (sEditFormatString.toUpperCase() == "YYYY/MM") {
            var month = sValue + '/01';
            if (!IsDate(month)) {
                alert("月份格式為 YYYY/MM");
            }
        }
        else {
            if (!IsDate(sValue)) {
                alert('輸入字串不為日期格式，請重新輸入');
            }
        }
    }
}

//比較起訖日期，結束日期不可早於開始日期
function chkDate(s, e) {
    var x = txtSDate.GetValue();
    var y = txtEDate.GetValue();

    if (x == null) { x = ""; }
    if (y == null) { y = ""; }

    if ((x != "" && y != "")) {

        e.isValid = (x <= y);
        if (!e.isValid) {
            alert("迄止日期不可早於起始日期!!");
            s.SetValue(null);
            return false;
        }
    }
    else {
        return true;
    }
}

//比較起訖日期是否相同
function chkOneDate(s, e) {
    var x = txtSDate.GetValue();
    var y = txtEDate.GetValue();

    if (x == null) { x = ""; }
    if (y == null) { y = ""; }

    if ((x != "" && y != "")) {

        e.isValid = (y - x == 0);
        if (!e.isValid) {
            alert("起訖日期 必須為同一日!!");
            s.SetValue(null);
            return false;
        }
    }
    else {
        return true;
    }
}

//比較起訖月份，結束月份不可早於開始月份
function chkMonth(s, e) {
    var x = txtSMonth.GetText();
    var y = txtEMonth.GetText();

    if (x == null) { x = ""; }
    if (y == null) { y = ""; }

    if ((x != "" && y != "")) {

        x = x + '/01';
        y = y + '/01';
        var startArray = x.split("/");
        var endArray = y.split("/");

        var SDate = new Date(startArray[0], startArray[1], startArray[2]);
        var EDate = new Date(endArray[0], endArray[1], endArray[2]);

        e.isValid = (SDate <= EDate);
        if (!e.isValid) {
            alert("訖 月份不可早於 起 月份!!");
            s.SetValue(null);
            return false;
        }
    }
    else {
        return true;
    }
}

//判斷輸入值是否為月份格式
function chkIsMonth(s, e) {
    var month = s.GetText();

    if (month == null) { month = ""; }

    if (month != "") {

        month = month + '/01';
        var DateArray = month.split("/");

        if (!IsDate(month)) {
            alert("月份格式為 YYYY/MM");
            s.SetValue(null);
            return false;
        }
    }
    else {
        return true;
    }
}
