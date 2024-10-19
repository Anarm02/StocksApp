
const token = document.querySelector("#FinnhubToken").value;
const socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);
var stockSymbol = document.getElementById("StockSymbol").value; 
socket.addEventListener('open', function (event) {
    socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': stockSymbol }))
});

socket.addEventListener('message', function (event) {
    if (event.data.type == "error") {
        $(".price").text(event.data.msg);
        return; 
    }

    var eventData = JSON.parse(event.data);
    if (eventData) {
        if (eventData.data) {
            
            var updatedPrice = JSON.parse(event.data).data[0].p;
            $(".price").text(updatedPrice.toFixed(2)); 
        }
    }
});

var unsubscribe = function (symbol) {
    socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': symbol }))
}
window.onunload = function () {
    unsubscribe(stockSsymbol);
};