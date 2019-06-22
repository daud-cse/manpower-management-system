function DeleteConfirm() {
    if (confirm("Do you want to delete?"))
        return true;
    else
        return false;
}
function EditConfirm() {
    if (confirm("Do you want to edit?"))
        return true;
    else
        return false;
}
function UpdateConfirm() {
    if (confirm("Do you want to Update Location?"))
        return true;
    else
        return false;
}
function AddConfirm() {
    if (confirm("Do you want to create?"))
        return true;
    else
        return false;
}
function StatusChangeConfirm() {
    if (confirm("Do you want to change Project status?"))
        return true;
    else
        return false;
}
function SubmitRQConfirm() {
    if (confirm("Do you want to save?"))
        return true;
    else
        return false;
}
function ApprovedRQConfirm() {
    if (confirm("Do you want to approve?"))
        return true;
    else
        return false;
}
function RejectRQConfirm() {
    if (confirm("Do you want to reject?"))
        return true;
    else
        return false;
}
function formatNumber(num) {
    //alert(num.value.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));    
     num.value=num.value.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function SendSMSConfirm() {
    if (confirm("Do you want to send SMS?"))
        return true;
    else
        return false;
}
function InputOnlyDecimalNumber(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = "";
    }
}
function InputOnlyDecimalNumberSetEmpty(el) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(el.value) == false) {
        el.value = "0"//el.value.substring(0, el.value.length - 1);
        //  el.value =el.value.substring(0, el.value.length - 1);
    }
}
function LoadDropDownList(objDDLId, url, optionalLabel, objDataTableToReload) {
    var objDDL = $("#" + objDDLId);
    objDDL.empty();
    if (optionalLabel != null) {
        objDDL.append($('<option/>', {
            value: "",
            text: optionalLabel
        }));
    }
    $.ajax({
        cache: false,
        url: url,
        type: "GET",
        beforeSend: function () {
        },
        complete: function () {
        },
        success: function (data) {
           
            $.each(data, function (index, itemData) {
               
                objDDL.append($('<option/>', {
                    value: data[index]["Key"],
                    text: data[index]["Value"]
                }));
            });

            if (objDataTableToReload != null) {
               // DataTableRedraw(objDataTableToReload);
            }
        },
        error: function (x, e) {
            //alert('error');
        }
    }); //end ajax call
}
$(document).ready(function () {
   
    $(".alert button").click(function () {
        $("div.alert").fadeOut(400);
    });
    var interval = setInterval(function () {
        $(".alert button").trigger("click");
    }, 3);

    $(".dl-horizontal dd").each(function (index, value) {
        if (value.innerHTML.trim() == '') {
            $(this).html("NILL");
        }
    });
    //$(document).ajaxStart(function () {
    //    $('body').addClass('modal');
    //})
    //$(document).ajaxstop(function () {
    //    $('body').removeClass('modal');
    //});
    $body = $("body");

    $(document).on({
        ajaxStart: function () { $body.addClass("loading"); },
        ajaxStop: function () { $body.removeClass("loading"); }
    });
});