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
});


