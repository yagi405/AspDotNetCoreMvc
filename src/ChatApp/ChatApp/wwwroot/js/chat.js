
$(function () {

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

    setInterval("refreshChatLogs()", 10000);
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