﻿function ConfirmedOrder_onClick() {

    const inputCoffeeId = document.getElementById('coffeeId');
    const inputOrderId = document.getElementById('orderId');
    const inputCafeId = document.getElementById('cafeId');
    const inputDescription = document.getElementById('description');

    fetch("/subsips/UserCustomer/MakeOrder", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            coffeeId: inputCoffeeId.value,
            orderId: inputOrderId.value,
            cafeId: inputCafeId.value,
            description: inputDescription.value,
        }),
    })
        .then(result => {
            console.log('res : ', result);
            if (result.ok) {
                //window.open('/Subsips/UserCustomer/ShowStatusOrder?orderId=' + inputOrderId.value, '_blank');
                location.href = result.url;
                return;
            }

            alert('مشکلی پیش آمده');
        })
}