function ConfirmedOrder_onClick() {

    const inputCoffeeId = document.getElementById('coffeeId');
    const inputOrderId = document.getElementById('orderId');
    const inputCafeId = document.getElementById('cafeId');
    const inputDescription = document.getElementById('description');
    console.log('inputCoffeeId : ', inputCoffeeId.value);
    console.log('inputOrderId : ', inputOrderId.value);
    console.log('inputCafeId : ', inputCafeId.value);
    console.log('inputDescription : ', inputDescription.value);
    alert('fuck');

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
            startCountdown(120);
        })
}