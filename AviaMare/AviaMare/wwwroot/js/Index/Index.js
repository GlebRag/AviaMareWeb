﻿$(document).ready(function () {

    init();

    $(".tag.delete").click(function (event) {
        const ticketBlock = $(this).closest(".ticket");
        const ticketId = $(this).attr("data-id");
        const url = `/api/ApiHome/Remove?id=${ticketId}`;
        $.get(url).then(function (response) {
            if (response) {
                ticketBlock.remove();
            }
        });

        event.preventDefault();
    });

    function init() {
        const url = new URL(document.location.href);
        const perPage = url.searchParams.get('perPage');
        $('[name=perPage]').val(perPage);
    }
});