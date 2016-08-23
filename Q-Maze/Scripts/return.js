$(".returnlink").click(function (event) {
    event.preventDefault();
    var link = this;
    // $(link).text($(link).attr("href"));
    $.ajax($(link).attr("href"),
        {
            method: "POST",
            data: { id: $(link).attr("data-id") },
            success: function (data, status, xhr) {
                if (data.Success === true)
                    $(link).parents(".leasedays").html(data.LeaseDays + " day(s)");
            },
            dataType: "json"
        });
});
