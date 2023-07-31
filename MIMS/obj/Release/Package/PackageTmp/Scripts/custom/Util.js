function BoostrapView(show) {
    $('#' + show).modal('show');
}

//close the hide window
//open the show window
//clear the hide window elements
function BoostrapView(show, hide, hideelements) {
    $('#' + hide).modal('hide');
    $('#' + hideelements).html("");
    $('#' + show).modal('show');
}

function BoostrapView(show, hide) {
    $('#' + hide).modal('hide');
    $('#' + show).modal('show');
}

//close the dialog
function CloseDialog(src) {
    $('#' + src).modal('hide');
}

//close the dialog clear the inner html as well
function CloseDialog(src, innerHtml) {
    $('#' + src).modal('hide');
    $('#' + innerHtml).html("");
}

function CloseDeleteDialog() {
    $('#' + 'DeleteDivContainer').modal('hide');
    $('#' + 'DeleteDivContainer-Elements').html("");
}


function updateRole(funcArea, groupId) {

    $("#msg-" + funcArea).html('');

    $.ajax({
        type: "POST",
        url: "./Groups/SetRole",
        data: "funcArea=" + funcArea + "&groupId=" + groupId + "&roleType=" + document.getElementById("role-" + funcArea).value,
        success: function (msg) {
            $("#msg-" + funcArea).html(msg);
        },
        error: function () {
            serverError();
        }
    });
}

function serverError() {
    alert("Server Error");
}

setTimeout(function () {
    $("#msg").fadeOut(2000);
}, 3000);

$('.date').datepicker({
    autoclose: true,
    todayHighlight: true,
    format: "dd/mm/yyyy",
    startDate: new Date()
});


$(document).ready(function() {
    if ($.browser.msie && $.browser.version == 9) {
        //left it if we can come up with a fix
    } else {
        $(".dataTables_scrollBody").mCustomScrollbar({
                theme:"dark-thick",
                advanced: {
                    updateOnBrowserResize: true,
                    updateOnContentResize: true
                }
            });
    }
});

function ClearDate(src) {
    $('#' + src).val('');
}