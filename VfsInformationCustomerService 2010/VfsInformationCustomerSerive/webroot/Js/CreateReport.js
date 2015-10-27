function StyleSuccess(messenge) {
    $("#loading-image").html('<span style="color:green">' + messenge + '</span>');
}
function StyleError(messenge) {
    $("#loading-image").html('<span style="color:red">' + messenge + '</span>');
}

function StyleLoadding() {
    $("#loading-image").html('<img src="_assets/img/uploading.gif" />')
}


function testSMS() {    
    $.ajax({
        url: 'CreateReport.aspx/SendSMSTest',
        type: "POST",
        traditional: true,
        //async: false,
        data: '{mobile:' + $('#CheckSendSMS').val() + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {            
            if (data.d = 'Y') {
                StyleSuccess('Thành công');
            }
            else {
                StyleError('Lỗi');
            }
        },
        beforeSend: function (xhr) {
            StyleLoadding();
        },
        complete: function () {
            //$("#loading-image").html('');
        },
        error: function () {
            StyleError('Lỗi');
        }
    });
}


function saveTemplate(message) {
    $.ajax({
        url: 'CreateReport.aspx/SaveTemplateAjax',
        type: "POST",
        traditional: true,
        //async: false,
        data: '{content: "' + message + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d = 'Y') {
                StyleSuccess('Lưu tin thành công');
            }
            else {
                StyleError('Lỗi từ server');
            }
        },
        beforeSend: function (xhr) {
            StyleLoadding();
        },
        complete: function () {
            //$("#loading-image").html('');
        },
        error: function () {
            StyleError('Lỗi');
        }
    });
}

$(function () {
    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        resizable: false,
        width: 400,
        height:300,
        show: {
            effect: "blind"                    
        },
        hide: {
            effect: "explode"                    
        },
        buttons: {
            "Lưu lại": function () {
                //alert($('.InputBodyMessager').val());
                if ($('.InputBodyMessager').val().length >= 10) {
                    saveTemplate($('.InputBodyMessager').val());
                }
                else {
                    alert('Nội dung không để trống và > 10 ký tự');
                }
                
            },
            "Gửi test": function () {
                if ($('#CheckSendSMS').val().length >= 10) {
                    testSMS();
                }
                else {
                    alert('Nhập đúng số dt để test');
                }                
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    $("a[id=BoxMessengContent]").click(function () {
        $("#dialog").dialog("open");
    });
});
