(function () {

    const orderStatus = [
        { step: 1, label: "ثبت سفارش", completed: true },
        { step: 2, label: "در حال آماده‌سازی", completed: true },
        { step: 3, label: "ارسال به مقصد", completed: false },
        { step: 4, label: "تحویل داده شده", completed: false },
    ];

    const updateStatus = () => {
        const steps = document.querySelectorAll(".status-step");
        const bars = document.querySelectorAll(".status-bar");

        orderStatus.forEach((status, index) => {
            if (status.completed) {
                steps[index].classList.add("active");
                if (bars[index]) {
                    bars[index].classList.add("completed");
                }
            }
        });
    };

    document.addEventListener("DOMContentLoaded", updateStatus);

})();