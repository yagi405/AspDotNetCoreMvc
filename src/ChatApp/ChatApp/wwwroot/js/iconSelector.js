
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
});