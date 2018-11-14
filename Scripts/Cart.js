function removeFromCart(iProductID) {
    $.ajax({
        type: "POST",
        url: '/Cart/removeFromCart',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'iProductID': iProductID }),
        dataType: "json",
        success: function (data) {
            window.location = "/Cart/Cart"
        },
        error: function () { alert('A error'); }
    })
}