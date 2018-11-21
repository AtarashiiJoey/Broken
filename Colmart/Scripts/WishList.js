

function removeFromWishList(iProductID) {
    $.ajax({
        type: "POST",
        url: '/Wishlist/removeFromWishlist',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'iProductID': iProductID }),
        dataType: "json",
        success: function (data) {
            window.location = "/Wishlist/Wishlist"
        },
        error: function () { alert('A error'); }
    })
}
