$(function () {
    // Run this function for all validation error messages
    $('.field-validation-error').each(function () {

        // Get the error text to be displayed
        var errorText = $(this).text();

        // Remove the text from the error message span
        // element and add the classes to display the icon
        $(this).empty();
        $(this).addClass("ui-state-error-icon").addClass("ui-icon-alert");

        // Wire up the tooltip to display the error message
        $(this).qtip({
            overwrite: true,
            content: errorText,
            style: {
                classes: 'ui-tooltip-red'
            }
        });     
    });
});

function SetQtip(src) {
    var errorText = $(src).text();

    // Remove the text from the error message span
    // element and add the classes to display the icon
    $(src).empty();
    $(src).qtip({
        overwrite: true,
        content:  errorText ,
        style: {
            classes: 'ui-tooltip-red'
        }
    });
}

