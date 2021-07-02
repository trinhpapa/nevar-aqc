const systemHub = new signalR.HubConnectionBuilder().withUrl("/systemHub").build();

systemHub.start().catch(function (e) {
    console.log(e);
});

this.htmlTag = new function () {
    this.LoginForm = "#credentials-form";
    this.LoginButton = "#login-button";
};

$(document).ready(function () {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "200",
        "hideDuration": "200",
        "timeOut": "2000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
});

$(htmlTag.LoginForm).submit(function (e) {
    e.preventDefault();
    var data = $(htmlTag.LoginForm).serialize();
    $(htmlTag.LoginButton).prop('disabled', true);
    $(htmlTag.LoginButton).html('<i style="font-size: 20px" class="fas fa-circle-notch fa-spin fa-fw"></i>');
    $.ajax({
        type: "POST",
        data: data,
        url: "/login/loginnow",
        timeout: 10000,
        statusCode: {
            200: function (data) {
                window.localStorage.setItem("user-profile", JSON.stringify(data));
                systemHub.invoke("UserLogin").catch(function (err) {
                    return console.error(err.toString());
                });
                window.location.href = "/";
            },
            400: function (xhr, status, error) {
                $(htmlTag.LoginButton).prop('disabled', false);
                $(htmlTag.LoginButton).html('ĐĂNG NHẬP');
                toastr.error(xhr.responseText, "Thông báo");
            },
            500: function (xhr, status, error) {
                $(htmlTag.LoginButton).prop('disabled', false);
                $(htmlTag.LoginButton).html('ĐĂNG NHẬP');
                toastr.error(xhr.responseText, "Thông báo");
            }
        }
    });
});