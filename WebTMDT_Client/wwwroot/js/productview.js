

var apiUrl = "https://localhost:7099/api/"
var clientUrl = "https://localhost:7094/"

var genresFilterList = []
var priceRange = "0,9999999"

function LoadNewProductList(pageNumber, pageSize) {

    if (pageNumber <= 0) {
        pageNumber = 1
    }

    var keyword = document.getElementById("searchInput").value
    var genresString = getFilterGenreString(genresFilterList)

    fetch(`${clientUrl}View/GetProductList?pageNumber=${pageNumber}&pageSize=${pageSize}&keyword=${keyword}
	${genresString != null ? `&genreFilter=${genresString}` : ""}
	${priceRange != null ? `&priceRange=${priceRange}` : ""}
	`).then(function (response) {
        // The API call was successful!
        return response.text();
    }).then(function (html) {
        // This is the HTML from our response as a text string
        /*console.log(html);*/
        var productList = document.getElementById("productList")
        productList.innerHTML = html
        document.getElementById("searchInput").value = keyword
    }).catch(function (err) {
        // There was an error
        console.warn('Something went wrong.', err);
    });

    var scrollDiv = document.getElementById("searchBarProduct").offsetTop;
    window.scrollTo({ top: scrollDiv, behavior: 'smooth' });
}

function onSearchFilterChange_Btn() {
    LoadNewProductList(1, 8)
}
function onSearchFilterChange_Input(e) {
    if (e.keyCode === 13) {
        e.preventDefault(); // Ensure it is only this code that runs

        LoadNewProductList(1, 8)
    }

}
function onGenreFilterChange(genre, id) {

    var selected_genre = { name: genre, id: id }
    //console.log(selected_genre)
    if (genre != "all") {
        if (!genresFilterList.some(g => g.name == genre)) {
            genresFilterList.push(selected_genre)
        }
        else {
            genresFilterList = genresFilterList.filter(q => q.id != id)
        }
    }
    else {
        genresFilterList.forEach(item => {
            document.getElementById(`genre_${item.id}`).checked = false;
        })
        genresFilterList = []
    }
    if (genresFilterList.length > 0) {
        document.getElementById(`genre_all`).checked = false;
    }
    if (genresFilterList.length == 0) {
        document.getElementById(`genre_all`).checked = true;
    }
    //console.log(getFilterGenreString(genresFilterList))
    LoadNewProductList(1, 8)
}
function onPriceRangeChange(value) {
    priceRange = value
    LoadNewProductList(1, 8)
}
function applyPriceRange() {
    var maxPrice = document.getElementById("maxPrice").value
    var minPrice = document.getElementById("minPrice").value
    if (minPrice == "" || maxPrice == "") {
        alert("Khoảng giá chưa hợp lệ");
    }
    else if (Number.parseInt(minPrice) > Number.parseInt(maxPrice)) {
        alert("Khoảng giá chưa hợp lệ");
    }
    else {
        document.getElementById("priceRangeAll").checked = true
        priceRange = (minPrice + "," + maxPrice);
        LoadNewProductList(1, 8)
    }

}

function getFilterGenreString(genres) {
    if (genres.length == 0) {
        return null;
    }
    var string_genre = "";
    genres.forEach((element) => {
        string_genre += element.name;
        string_genre += ",";
    });
    return string_genre.slice(0, string_genre.length - 1);
}
function resetFilter() {
    genresFilterList.forEach((genre) => {
        document.getElementById(`genre_${genre.id}`).checked = false
    })
    document.getElementById("genre_all").checked = true
    genresFilterList = []
    priceRange = "0,9999999"
    document.getElementById("searchInput").value = null
    document.getElementById("priceRangeAll").checked = true
    LoadNewProductList(1, 8)
}

function addToCart(item) {
    //console.log(item)
    var reloadCart = false
    var cartItem = {
        BookId: item.id,
        ImgUrl: item.imgUrl != null ? item.imgUrl : item.ImgUrl,
        Title: item.title != null ? item.title : item.Title,
        Price: item.price != null ? item.price : item.Price,
        Quantity: 1,
        PromotionPercent: item.promotionInfo == null ? null : item.promotionInfo.promotionPercent,
        PromotionAmount: item.promotionInfo == null ? null : item.promotionInfo.promotionAmount
    }
    if (item.PromotionAmount != null || item.PromotionPercent != null) {
        cartItem.PromotionAmount = item.PromotionAmount
        cartItem.promotionPercent = item.PromotionPercent
        reloadCart = true
    }
    console.log(cartItem)

    fetch(`${clientUrl}Cart/AddToCart`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(cartItem)
    }).then(
        (res) => {
            LoadCartIcon();

            LoadCart();

        }
    ).catch((e) => { console.log(e) })
}
function RemoveFromCart(item) {

    var cartItem = {
        ImgUrl: item.imgUrl != null ? item.imgUrl : item.ImgUrl,
        Title: item.title != null ? item.title : item.Title,
        Price: item.price != null ? item.price : item.Price,
        Quantity: 1,
        PromotionPercent: item.promotionInfo == null ? null : item.promotionInfo.promotionPercent,
        PromotionAmount: item.promotionInfo == null ? null : item.promotionInfo.promotionAmount
    }
    fetch(`${clientUrl}Cart/RemoveFromCart`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(cartItem)
    }).then(
        (res) => {
            LoadCartIcon();
            LoadCart();
        }
    ).catch((e) => { console.log(e) })
}
function DeleteFromCart(item) {
    var cartItem = {
        ImgUrl: item.imgUrl != null ? item.imgUrl : item.ImgUrl,
        Title: item.title != null ? item.title : item.Title,
        Price: item.price != null ? item.price : item.Price,
        Quantity: 1,
        PromotionPercent: item.promotionInfo == null ? null : item.promotionInfo.promotionPercent,
        PromotionAmount: item.promotionInfo == null ? null : item.promotionInfo.promotionAmount
    }
    fetch(`${clientUrl}Cart/DeleteFromCart`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(cartItem)
    }).then(
        (res) => {
            LoadCartIcon();
            LoadCart();
        }
    ).catch((e) => { console.log(e) })
}

function MyAlert(msg, duration) {
    var el = document.createElement("div");
    el.setAttribute("style", "position: fixed;top:1%;left:40%;background-color:#41b232;color: white;height: 50px;width: 20%;text-align: center;line-height: 50px;");
    el.innerHTML = msg
    setTimeout(function () {
        el.parentNode.removeChild(el);
    }, duration);
    document.body.appendChild(el);
}

function LoadCartIcon() {

    fetch(`${clientUrl}View/ReloadCartIcon`).then(function (response) {
        // The API call was successful!
        return response.text();
    }).then(function (html) {
        // This is the HTML from our response as a text string
        /*console.log(html);*/
        var cartIcon = document.getElementById("cartIcon")
        cartIcon.innerHTML = html
        MyAlert("Thêm vào giỏ hàng thành công !", 1000);
    }).catch(function (err) {
        // There was an error
        console.warn('Something went wrong.', err);
    });
}
function LoadCart() {

    fetch(`${clientUrl}View/ReloadCart`).then(function (response) {
        // The API call was successful!
        return response.text();
    }).then(function (html) {
        // This is the HTML from our response as a text string
        /*console.log(html);*/
        var cart = document.getElementById("cartPage")
        cart.innerHTML = html
        // MyAlert("Thêm vào giỏ hàng thành công !", 1000);
    }).catch(function (err) {
        // There was an error
        console.warn('Something went wrong.', err);
    });
}

function PostReview(pid, uid, token) {
    console.log(pid)
    console.log(uid)
    console.log(token)
    var review = {
        bookId: pid,
        star: 0,
        content: "string",
        recomended: true,
        date: new Date().toISOString(),
        userID: uid
    }
    console.log(review)
}


function OrderSubmit(e) {
    e.preventDefault()
    var order_contactName = document.getElementById("order_contactName").value;
    var order_email = document.getElementById("order_email").value;
    var order_phoneNumber = document.getElementById("order_phoneNumber").value;
    var order_addressNo = document.getElementById("order_addressNo").value;
    var order_street = document.getElementById("order_street").value;
    var order_ward = document.getElementById("order_ward").value;
    var order_district = document.getElementById("order_district").value;
    var order_city = document.getElementById("order_city").value;
    var order_note = document.getElementById("order_note").value;
    var order_paymentMethod = document.getElementById("order_paymentMethod").value;

}
