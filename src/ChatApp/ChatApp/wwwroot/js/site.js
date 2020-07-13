$("#btnPostChat").click(function () {

    postForm("#frmPostChat",
        function (data) {
            const response = JSON.parse(data);
            showSummaryErrors("#postChatLogErrors", response.extraData);
            refreshChatLogs();
        });

});

function refreshChatLogs() {
    const form = $("#frmPostChat");
    const token = $('input[name="__RequestVerificationToken"]', form).val();
    const url = "/Chat/Refresh/";
    $.post(
        url,
        {
            __RequestVerificationToken: token
        })
        .done(
            function (response) {
                $("#chatLogs").html(response);
            }
        );
}

function postForm(formSelector, success, error) {

    const form = $(formSelector);

    form.validate();
    if (!form.valid()) {
        return false;
    }

    const formData = new FormData(form[0]);

    notifyBeginOfOperation(formSelector);
    $.ajax({
        cache: false,
        url: form.attr("action"),
        type: form.attr("method"),
        dataType: "html",
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) { notifyEndOfOperation(formSelector); success(data); },
        error: function (data) { notifyEndOfOperation(formSelector); error(data); }
    });

    return true;
};

function notifyBeginOfOperation(formSelector) {
    $(formSelector + " input").attr("disabled", "disabled");
    $(formSelector + " button").attr("disabled", "disabled");
    $(formSelector + " a").attr("disabled", "disabled");
}

function notifyEndOfOperation(formSelector) {
    $(formSelector + " button").removeAttr("disabled");
    $(formSelector + " a").removeAttr("disabled");
    $(formSelector + " input").removeAttr("disabled");
}

function showSummaryErrors(formSelector, extraData) {
    var div = $(".validation-errors");
    if (div.length === 0) {
        div = $('<div class="validation-errors text-danger">');
        div.html("<ul></ul>");
        div.appendTo($(formSelector));
    }
    div.find("ul").empty();
    $.each(
        extraData,
        function (index, value) {
            div.find("ul").append($("<li>").text(value));
        }
    );
}