var ExcludeAjaxLoad = false;
$(document).ready(function () {
    $(document).ajaxSend(function () {
        $("#loader").show();
    }).ajaxComplete(function () {
        $("#loader").hide();
    }).ajaxError(function () {
        $("#loader").hide();
    });

    $('.nav-pills a').click(function () {
        $('.nav-pills a').removeClass('select');
        $(this).addClass('select');
    });

});

function ActiveMenu(id) {
    var targetObj = $(id);
    $('.nav-sidebar').find('.menu-open').removeClass('menu-open');
    $('.nav-sidebar').find('.nav-link').removeClass('selected');
    setTimeout(function () {
        var currObj = targetObj;
        if (currObj.hasClass('nav-link')) {
            currObj.addClass('selected');
        }
        while (!currObj.hasClass('nav-sidebar')) {
            if (currObj.hasClass('nav')) {
                currObj.show();
            }
            if (currObj.hasClass('nav-item')) {

                currObj.addClass('menu-open');
            }
            currObj = currObj.parent();
        }
    }, 100);

}


//$("#navlinknavReports").on('click', function (e) {
//    $("#navReports").addClass("menu-open");
//});

//$("#navlinkRecords").on('click', function (e) {
//    $("#navRecords").addClass("menu-open");
//});

//$("#FileMaintenance").on('click', function (e) {
//    $("#navFM").addClass("menu-open");
//});

//$("#navlinkSM").on('click', function (e) {
//    $("#navSM").addClass("menu-open");
//});


$("#btnLog").on('click', function (e) {
    e.preventDefault();
    //var form = $("#formRegister");
    //SignIn(form);
    SignIn();
});


function SignIn(form) {
    var Username = $("[name=Username]").val();
    var Password = $("[name=Password]").val();
    $.ajax({
        type: 'post',
        url: '../Account/AccountSignIn',
        data: "&Username=" + Username + "&Password=" + Password,//$(form).serialize(),
        success: function (result) {
            if (result.Data != null) {
                var data = result.Data;
                if (data.success) {
                    alert("Successfully Logged In");
                    window.location.href = data.redirect;
                } else {
                    alert(data.error);
                }
            }
        },
        error: function () {
            //error("KYUT SI JORDZ");
        }
    });
}


function MessageBox(Msg, Header, ButtonClass, ButtonText, ModalHeaderBG) {
    $("#MessageBox").modal("show");
    if (ModalHeaderBG != undefined) {
        $("#MessageBox").find(".modal-header").removeClass().addClass("modal-header" + " " + ModalHeaderBG);
    }
    else {
        $("#MessageBox").find(".modal-header").removeClass().addClass("modal-header bg-success");
    }
    $('#MessageBoxTitle').text(Header);
    $('#MessageBoxBody').html(Msg);
    $("#MessageBox").find("button").addClass(ButtonClass).text(ButtonText);
}

function MessageBoxYesNo(Msg, Header, ButtonClassYes, ButtonClassNo, ButtonTextYes, ButtonTextNo) {
    $("#MessageBoxYesNo").modal("show");
    $('#MsgBoxYesNoTitle').text(Header);
    $('#MsgBoxYesNoBody').html(Msg);
    $("#MessageBoxYesNo #btnYes").addClass(ButtonClassYes).text(ButtonTextYes);
    $("#MessageBoxYesNo #btnNo").addClass(ButtonClassNo).text(ButtonTextNo);
}

function fnGetRowData(DataTableID, rowData) {
    var row = $("#" + DataTableID).find(".dtactive");
    if (row.length > 0)
        return $("#" + DataTableID).DataTable().row(row[0]).data()[rowData];
    else
        return 0;
}

function getStringFormatDateForDateField(stringdate) {
    if (stringdate != null) {
        var date = new Date(stringdate);
        var mm = (date.getMonth() + 1).toString();
        var dd = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        if ((date.getMonth() + 1).toString() < 10) {
            mm = "0" + (date.getMonth() + 1).toString();
        }

        var ret = (date.getFullYear()).toString() + "-" + mm + "-" + dd;
        return ret;
    }
    return null;
}

function CheckUnchecAttr(div, attr, TrueFalse) {
    $(div).attr(attr, TrueFalse);
}

function fnValidateForm(formIDs) {
    if (!$(formIDs).parsley().validate()) {
        return false;
    }
    else {
        return true;
    }
}

