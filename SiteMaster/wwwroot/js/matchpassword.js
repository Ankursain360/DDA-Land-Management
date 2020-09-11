
<script src="https://cdn.jsdelivr.net/npm/jquery-validation@1.19.2/dist/jquery.validate.min.js"></script>
 

        $("#create_user").validate({
            rules: {
            Password: {
            // required: true
        },
            ConfirmPassword: {
            //  required: true,
            equalTo: "#Password"
            },



            messages: {
            Password: {
            // required: "the password is required"

        },
                ConfirmPassword: {
            // required: "the Confirm password is required",
            equalTo: "Password is not match"

                },





            }
        });

