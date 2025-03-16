$(document).ready(function () {
    $(".profile .update-avatar-input").click(function () {
        const profileBlock = $(this).closest(".profile");
        profileBlock.find(".update-avatar-button").css("display", "inline");

    });
})