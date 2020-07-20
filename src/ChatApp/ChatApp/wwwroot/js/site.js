
$(function () {

    $("div.file-container").each(function () {
        const inputFile = $(this).find("input[type=file]").first();
        inputFile.hide();
        const selectedFileSelector = `#${$(inputFile).attr("id").toLowerCase()}-selected`;
        $(selectedFileSelector).text($(this).data("notselected"));

        $(inputFile).on("change",
            function (evt) {
                var id = $(this).attr("id").toLowerCase();
                const files = evt.target.files;
                if (files && files[0]) {
                    const reader = new FileReader();
                    reader.onload = function (e) {
                        $(`#${id}-preview`).attr("src", e.target.result);
                    };
                    reader.readAsDataURL(files[0]);
                }
            });
        $(inputFile).click(function (ev) {
            return ev.stopPropagation();
        });
    });
    $("div.file-container").on("click",
        function () {
            const inputFile = $(this).find("input[type=file]").first();
            $(inputFile).click();

            const inputFileId = $(inputFile).attr("id").toLowerCase();
            const previewSelector = `#${inputFileId}-preview`;
            const fileName = $(inputFile).val();
            $(previewSelector).attr("title", fileName);
            $(previewSelector).show();
            const emptySelector = `#${inputFileId}-selected`;
            $(emptySelector).hide();
            $("button.file-remover").show();
            return false;
        });

    $("button.file-remover").on("click",
        function () {
            const parent = $(this).parent().closest("div.file-container");
            const inputFile = parent.find("input[type=file]").first();
            $(inputFile).val("");
            const inputFileId = $(inputFile).attr("id").toLowerCase();
            const previewSelector = `#${inputFileId}-preview`;
            $(previewSelector).removeAttr("src").removeAttr("title");
            $(previewSelector).hide();
            const emptySelector = `#${inputFileId}-selected`;
            $(emptySelector).show();
            $(this).hide();

            return false;
        });

    $("#btnPostChat").on("click",
        function () {
            postForm("#frmPostChat",
                function (data) {
                    const response = JSON.parse(data);
                    showSummaryErrors("#postChatLogErrors", response.extraData);
                    refreshChatLogs();
                });
        });

    $("#frmPostChat").on("keydown",
        "#txtAreaChatLog",
        function (e) {
            if (e.key === "Enter" && !e.shiftKey) {
                postForm("#frmPostChat",
                    function (data) {
                        const response = JSON.parse(data);
                        showSummaryErrors("#postChatLogErrors", response.extraData);
                        refreshChatLogs();
                    });
            }
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
                $("#txtAreaChatLog").val("");
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