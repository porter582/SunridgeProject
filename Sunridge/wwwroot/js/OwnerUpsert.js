let valid = true;

function validate(elem) {

    if ($(elem).length && $(elem).val().length) {

        $(elem).removeClass('invalid');
    } else {

        $(elem).addClass('invalid');
    }
}

$('#txtFirstName').change(function () {
    validate('#txtFirstName');
});

$('#txtLastName').change(function () {
    validate('#txtLastName');
});

$('#txtEmail').change(function () {
    validate('#txtEmail');
});

$('#txtPhone').change(function () {
    validate('#txtPhone');
});

$('#txtStAddress').change(function () {
    validate('#txtStAddress');
});

$('#txtCity').change(function () {
    validate('#txtCity');
});

$('#txtState').change(function () {
    validate('#txtState');
});

$('#txtZip').change(function () {
    validate('#txtZip');
});

$(".submission").click(function () {

    validate('#txtFirstName');
    validate('#txtLastName');
    validate('#txtEmail');
    validate('#txtPhone');
    validate('#txtStAddress');
    validate('#txtCity');
    validate('#txtState');
    validate('#txtZip');

    if ($('.invalid').length > 0) {
        valid = false;
    } else {
        valid = true;
    }

    console.log($('.invalid').length);

    if (valid) {
        if ($('#OwnerRB').is(':checked')) {

            $('#ownerType').val('Owner');
        } else {

            $('#ownerType').val('Admin');
        }

        $('#realEmail').val($('#txtEmail').val());

        var conf = confirm("OWNER IS BEING CREATED. Would you like to create another?");

        if (conf == true) {

            $("#another").val(true);

        } else {

            $("#another").val(false);

            var conf2 = confirm("Would you like to be redirected to Hoa Lots to add/remove Owners to a lot?");

            if (conf2 == true) {

                $('#redirect').val(true);

            } else {

                $('#redirect').val(false);
            }
        }
    } else {
        event.preventDefault();
    }
});