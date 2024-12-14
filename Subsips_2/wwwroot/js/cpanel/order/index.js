function sendRequest(action, orderId) {
    const xhr = new XMLHttpRequest();

    const url = `/Cpanel/Order/${action}`;
    xhr.open('POST', url, true);

    xhr.setRequestHeader('Content-Type', 'application/json');

    const data = JSON.stringify(orderId);

    xhr.onload = function () {
        if (xhr.status === 200) {
            location.reload();
        } else {
            alert('Request failed: ' + xhr.statusText);
        }
    };

    xhr.onerror = function () {
        alert('Request failed. Please check your connection or try again.');
    };

    xhr.send(data);
}


window.setTimeout(function () {
    window.location.reload();
}, 45000);


