$(function () {
    var dates = $("#StartDate, #EndDate").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        changeYear: true,
        numberOfMonths: 1,
        minDate:0,
        dateFormat: 'dd/mm/yy',
        onSelect: function (selectedDate) {
            var option = this.id == "StartDate" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
            dates.not(this).datepicker("option", option, date);
        }
        
    });
});



