(function () {
    'use strict';

    const form = document.getElementById('otpForm');
    const phoneInput = document.getElementById('PhoneNumber');
    const otpSection = document.getElementById('otpSection');
    const otpInput = document.getElementById('OtpCode');
    const submitButton = document.getElementById('submitButton');
    const resendOtpButton = document.getElementById('resendOtpButton');

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
            otpInput.setAttribute('required', 'true');
            resendOtpButton.classList.remove('visually-hidden');

            // Make phone input readonly
            phoneInput.setAttribute('readonly', 'true');
            submitButton.innerText = 'تایید کد';

            // Simulate OTP sent message
            alert('کد تایید به شماره ' + phoneInput.value + ' ارسال شد!');
        } else {
            // Submit OTP
            alert('OTP Submitted: ' + otpInput.value);
        }
    });

    resendOtpButton.addEventListener('click', function () {
        alert('کد تایید مجددا ارسال شد!');
    });
})();
