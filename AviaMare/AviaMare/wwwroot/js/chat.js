$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hub/chatMainPage")
        .build();

    init();

    hub.on("newMessageAdded", createNewMessage);

    $(".send-new-message").click(sendMessage);

    $(".new-message").on("keyup", function (e) {
        if (e.which == 13) {
            // 13 is Enter button
            sendMessage();
        }
    });
    function sendMessage() {
        const message = $('.new-message').val();
        hub.invoke('addNewMessage', message);

        $('.new-message').val("");
    };

    hub.start();

    function init() {

        const url = '/api/ApiChat/GetLastMessages';
        $.get(url)
            .then(function (messages) {
                messages.forEach((message) => createNewMessage(message));
            });
    }

    function createNewMessage(message) {
        const messageBlock = $('.message.template').clone();
        messageBlock.removeClass('template');
        messageBlock.text(message);
        $('.messages').append(messageBlock);
    }
});