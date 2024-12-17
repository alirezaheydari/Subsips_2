(function () {
    'use strict';

    const form = document.getElementById('otpForm');
    const phoneInput = document.getElementById('PhoneNumber');
    const otpSection = document.getElementById('otpSection');
    const fullNameSection = document.getElementById('fullNameSection');
    const descriptionSection = document.getElementById('descriptionSection');
    const inputCoffeeId = document.getElementById('coffeeId');
    const inputOrderId = document.getElementById('orderId');
    const inputCafeId = document.getElementById('cafeId');
    const otpInput = document.getElementById('OtpCode');
    const fullNameInput = document.getElementById('fullName');
    const submitButton = document.getElementById('submitButton');
    const resendOtpButton = document.getElementById('resendOtpButton');
    const countdownTimer = document.getElementById("countdownTimer");

    form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
            form.classList.add('was-validated');
            return;
        }

        // Handle OTP state
        if (otpSection.classList.contains('visually-hidden')) {
            event.preventDefault(); // Prevent submission to show OTP input

            // Show OTP section
            otpSection.classList.remove('visually-hidden');
            otpSection.classList.remove('d-none');
            descriptionSection.classList.remove('visually-hidden');
            descriptionSection.classList.remove('d-none');
            fullNameSection.classList.remove('visually-hidden');
            fullNameSection.classList.remove('d-none');
            otpInput.setAttribute('required', 'true');
            fullNameInput.setAttribute('required', 'true');
            resendOtpButton.classList.remove('visually-hidden');
            resendOtpButton.classList.remove('d-none');

            // Make phone input readonly
            phoneInput.setAttribute('readonly', 'true');
            submitButton.innerText = 'تایید کد';
            fetch("/Subsips/UserCustomer/SendOtp", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ phoneNumber: phoneInput.value }),
            })
                .then(result => {
                    startCountdown(120); 
                })
        } else {
            //event.preventDefault();
            //sendVerifyOtpCode()
        }
    });

    resendOtpButton.addEventListener('click', function () {
        fetch("/Subsips/UserCustomer/SendOtp", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({ phoneNumber: phoneInput.value }),
        })
            .then(result => {
                startCountdown(120);
            })
    });
    function startCountdown(durationInSeconds) {
        let remainingTime = durationInSeconds;


        resendOtpButton.disabled = true; // Disable the button


        const timerInterval = setInterval(() => {
            const minutes = Math.floor(remainingTime / 60);
            const seconds = remainingTime % 60;

            // Update the countdown display
            countdownTimer.textContent = `${minutes}:${seconds < 10 ? "0" : ""}${seconds}`;

            if (remainingTime === 0) {
                clearInterval(timerInterval); // Stop the timer
                countdownTimer.textContent = ""; // Clear the timer text
                resendOtpButton.disabled = false; // Enable the button
                enableOtpButton();
            }

            remainingTime--;
        }, 1000); // Update every second
    }


    function enableOtpButton() {
        const sendOtpButton = document.getElementById("sendOtpButton");
        const countdownTimer = document.getElementById("countdownTimer");

        sendOtpButton.disabled = false;
        countdownTimer.textContent = "";
    }
})();
