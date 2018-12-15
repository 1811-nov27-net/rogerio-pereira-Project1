$('#customer').change(function () {
    if ($(this).val() == "") {
        $('#address').html("");
    }
    else {
        getAddress();
    }
});

$('#address').change(function () {
    checkAddress();
});

$('.pizzaQuantity').change(function () {
    if ($(this).val() == "" || $(this).val() == null)
        $(this).val(0);

    checkPizzasCount();
    CheckValue();

    if ($(this).val() > 0) {
        pizzaId = $(this).data('id');
        quantity = $(this).val();
        
        $.get("/Pizzas/CheckPizzaStock/" + pizzaId + "/" + quantity, function (data) {
            if(data == false)
                $('#ErrorPizzaStock_' + pizzaId).html("Can't add this pizza quantity, Pizza's Ingredient stock is low");
            else
                $('#ErrorPizzaStock_' + pizzaId).html("");
        });
    }
})

$('.pizzaQuantity').blur(function () {
    checkPizzasCount();
    CheckValue();
})

function getAddress() {
    var customerId = $('#customer').val();

    $.get("/Addresses/GetAddressByCustomerId/" + customerId, function (data) {
        $('#address').html(data);
        checkAddress();
    });
}

function checkPizzasCount()
{
    pizzaCount = 0;

    $('.pizzaQuantity').each(function () {
        pizzaCount += parseInt($(this).val());
    });

    if (pizzaCount > 12) {
        $('#ErrorPizzas').html("Maximum quantity of pizzas allowed (12 pizzas)");
        return false;
    }
    else {
        $('#ErrorPizzas').html("");
        return true;
    }
}

function CheckValue()
{
    total = 0;

    $('.pizzaQuantity').each(function () {
        total += parseInt($(this).val()) * $(this).data('price');
    });

    if (total > 500) {
        $('#ErrorValue').html("Maximum Order Amount Allowed ($ 500)");
    }
    else
    {
        $('#ErrorValue').html("");
    }

    $('#Value').val(Number.parseFloat(total).toFixed(2));
}

function checkAddress()
{
    var addressId = Number.parseInt($("#address").val());
    var ret;

    ret = $.get("/Orders/CanOrderFromSameAddress/"+addressId, function (data) {
        ret = data;

        if (ret == false) {
            $('#ErrorAddress').html("Can't order from the same place Within 2 hours.");
        }
        else {
            $('#ErrorAddress').html("");
        }
    });
}

$('form').submit(function () {
    if ((checkPizzasCount() == false) || (CheckValue() == false) || (checkAddress() == false)) {
        return false;
    }
});