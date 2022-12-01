function CheckForInt(obj, e) {
    var charCode;
    if (window.event) // IE
    {
        charCode = event.keyCode; //e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        charCode = e.which;
    }
    return (charCode >= 48 && charCode <= 57);
}

function CheckForDouble(obj, e) {

    var charCode;
    if (window.event) // IE
    {
        charCode = event.keyCode; //e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        charCode = e.which;
    }

    var value = obj.value;

    if (value != null && value.indexOf('.') > -1) {
        return (charCode >= 48 && charCode <= 57);
    }
    else {
        return ((value.length <= 11) && (charCode >= 48 && charCode <= 57) || charCode == 46);
    }
}

function CheckForNumber(objField) {
    objField.value = objField.value.replace(/^\s*|\s*$/g, "");
    if (objField.value == "") {
        objField.value = "0";

    }
    else
        if (isNaN(objField.value)) {
            objField.style.borderColor = "red";
            var value = objField.value;
            objField.value = "0";
            alert("'" + value + "' Is not not a valid number");
            return false;
        }
    objField.style.borderColor = "";

    return true;
}

function CheckForMoney(objField) {
    objField.value = objField.value.replace(/^\s*|\s*$/g, "");
    if (objField.value == "") {
        objField.value = "0";

    }
    else
        if (isNaN(objField.value)) {
            objField.style.borderColor = "red";
            var value = objField.value;
            objField.value = "0";
            alert("'" + value + "' Is not a valid number");
            return false;
        }
    objField.style.borderColor = "";
    objField.value = parseFloat(objField.value).toFixed(2);

    return true;
}

function PreventEnter(objField, e) {
    var value = objField.value.length;
    var maxLen = parseInt(objField.maxlength);

    if (e.which == 13)
        return false;
    else if (window.event.keyCode == 13)
        return false;
    else if (value >= maxLen)
        return false;
    else
        return true;
}

function PreventPaste(objField, e) {
    var value = objField.value.length;
    var maxLen = parseInt(objField.maxlength);
    var text = $(objField).val();

    if (value > maxLen) {
        $(objField).val(text.substr(0, maxLen));
    } else { // alert the user of the remaining char. I do alert here, but you can do any other thing you like

    }
}



