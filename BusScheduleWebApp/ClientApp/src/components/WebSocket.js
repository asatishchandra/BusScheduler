var uri = "ws://" + window.location.host + "/ws";
function connect() {
    socket = new WebSocket(uri);
    socket.onopen = function (event) {
        console.log("opened connection to " + uri);
    };
    socket.onclose = function (event) {
        console.log("closed connection from " + uri);
    };
    socket.onmessage = function (event) {
        appendItem(list, event.data);
        console.log(event.data);
    };
    socket.onerror = function (event) {
        console.log("error: " + event.data);
    };
}
connect();

function sendMessage(message) {
    console.log("Sending: ");

    $.ajax({
        url: "/api/messages/time",
        method: 'GET'
    });
}
