$(document).ready(function() {
    $('#sign-in-form').on('submit', function(event) {
        event.preventDefault();
        var email = $('#email').val();
        var password = $('#password').val();
        if (email && password) {
            alert('Sign in with email: ' + email + ' and password: ' + password);
        } else {
            alert('Please enter both email and password.');
        }
    });

    $('#register-form').on('submit', function(event) {
        event.preventDefault();
        var fullName = $('#full-name').val();
        var email = $('#email').val();
        var password = $('#password').val();
        var passportId = $('#passport-id').val();
        if (fullName && email && password && passportId) {
            alert('Registered with full name: ' + fullName + ', email: ' + email + ', password: ' + password + ', and passport/ID number: ' + passportId);
        } else {
            alert('Please fill in all fields.');
        }
    });
});
