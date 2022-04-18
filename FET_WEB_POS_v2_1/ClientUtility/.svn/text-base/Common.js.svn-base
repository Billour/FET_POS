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
    //window.showModalDialog(url,'', 'dialogWidth=500px,dialogHeight=400px,top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
    window.open(url, "window", 'width=' + width + ',height=' + height + ',top=260,left=450,resizable=yes,scrollbars=no,location=no,toolbar=no,status=no');
}

function imeicheckbox(con) {
    if (con.checked) {
        openwindow("SAL01_inputIMEIData.aspx");
    }
}

function checkID(s, e) {
    if (e.keyCode == 13) {
        e.returnValue = false;
        var vID = s.GetValue();
        if (vID.length != 8 && vID > 0) {
            openwindow("SAL01_checkIDNumber.aspx", 300, 200);
            return false;
        }
    }
}

////User點選的列(hdNo)，其背景色為  #999999
//function ChangedBgColor(obj) {
//    $(".hdNo").val($(obj)[0].innerHTML);
//    $(obj).closest('tr')[0].style.backgroundColor = "#999999";
//    $(obj).closest('tr')[0].style.color = "#FFFFFF";
//}

////勾選的列，若不是User點選的列(hdNo)，則其背景色為 #FFFFFF
//function clearbgColor() {
//    $("input:checked").each(function() {
//        var ProNo = $(this).closest('tr').find(".commandButton");
//        if ($(".hdNo").val() == "" || $(".hdNo").val() != $(ProNo)[0].innerHTML) {
//            $(this).closest('tr')[0].style.backgroundColor = "#FFFFFF";
//            $(this).closest('tr')[0].style.color = "#000000";
//        }
//    });
//}

////計算折扣次數(DIS01_指定門市)
//function CalcuDiscountTimes() {
//    if ($(".hdItemName").val() == "指定門市") {
//        if ($(".txtDiscountTimes").val() != "") {
//            var SumTimes = 0;
//            $(".cellDiscountTimes").each(function() {
//                SumTimes += $(this).val().toNumber();
//            });
//            $(".lblRemainingTimes").val("剩餘數量：" + $(".txtDiscountTimes").val().toNumber() - SumTimes);
//        }
//    }
//}
