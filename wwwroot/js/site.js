// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function GetMessage() {
    $.get("/Login/Index", function (data) {
        $("p").html(data);
    });
}

$("#login").on('change keydown paste input', function () {
    var login = $("#login").val();
    var request = { data: login };
    showSpinner();
    $.ajax({
        type: "POST",
        cache: false,
        url: "/home/checkUser",
        data: JSON.stringify(request),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            hideSpinner();
            $('#validUser').html(result);
        },
        error: function (result) {
            alert("No Connection to server");
        }
    });
});

function hideSpinner(){
    var spinner = document.getElementById("spinner");
    spinner.style.display = 'none';
    document.getElementById("validUser").style.display = 'block';
}


function showSpinner(){
    var spinner = document.getElementById("spinner");
    spinner.style.display = 'block';
}

function delay(milliseconds){
    return new Promise(resolve => {
        setTimeout(resolve, milliseconds);
    });
}

function delay(milliseconds){
    return new Promise(resolve => {
        setTimeout(resolve, milliseconds);
    });
}