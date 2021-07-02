const systemHub = new signalR.HubConnectionBuilder().withUrl("/systemHub").build();

systemHub.start().catch(function (e) {
    console.log(e);
});

let todayFull = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
let todayDateVN = (todayFull.getDate() < 10 ? "0" + todayFull.getDate() : todayFull.getDate()) + "/" + ((new Date().getMonth() + 1) < 10 ? "0" + (new Date().getMonth() + 1) : (new Date().getMonth() + 1)) + "/" + (new Date().getFullYear());
const topPrintMargin = "2";
const bottomPrintMargin = "0.35";
const leftPrintMargin = "0.35";
const rightPrintMargin = "0.25";

let htmlTag = new function () {
    this.ControlExpand = '#control-expand';
    this.LeftControl = '.left-control';
    this.Main = '.main';
    this.LoadingScreen = ".loading-screen";
    this.DisplayName = "#displayname";
};

let jqItem = new function () {
    this.ControlExpand = $(htmlTag.ControlExpand);
    this.LeftControl = $(htmlTag.LeftControl);
    this.Main = $(htmlTag.Main);
    this.LoadingScreen = $(htmlTag.LoadingScreen);
};

$(document).ready(function () {
    loadUserInformation();
    settingToastr();
    setActiveLeftControl();
    $.fn.toggleDisabled = function () {
        return this.each(function () {
            this.disabled = !this.disabled;
        });
    };
    $.fn.paginationBinding = function (pageIndex, pageSize) {
        pageIndex = parseInt(pageIndex);
        let prev_link = '<li class="page-item"><button bl-tooltip="Trang đầu tiên" class="page-link link-previous"><i class="fa fa-chevron-left" aria-hidden="true"></i></button></li>';
        let html_link = '<li class="page-item"><button class="page-link"></button></li>';
        let next_link = '<li class="page-item"><button br-tooltip="Trang cuối cùng" class="page-link link-next"><i class="fa fa-chevron-right" aria-hidden="true"></i></button></li>';
        let dot_link = '<li class="page-item" disabled><button class="page-link" >...</button></li>';
        let totalPage = $(this).data('pages');

        $(this).empty();
        $(this).append(prev_link);
        if (totalPage > 0) {
            if (pageIndex <= totalPage) {
                if (pageIndex > 3) {
                    $(this).append(dot_link);
                }
                let minPage = pageIndex - 2;
                let maxPage = pageIndex + 2;
                for (let i = minPage; i <= maxPage; i++) {
                    if (i > 0 && i <= totalPage) {
                        if (i === parseInt(pageIndex)) {
                            $(this).append('<li class="page-item active"><button class="page-link link-number" data-value="' + i + '">' + i + '</button></li>');
                        }
                        else {
                            $(this).append('<li class="page-item"><button class="page-link link-number" data-value="' + i + '">' + i + '</button></li>');
                        }
                    }
                }
                if (totalPage - pageIndex >= 3) {
                    $(this).append(dot_link);
                }
            }
        }
        $(this).append(next_link);
    };

    $.fn.LoadingTable = function () {
        $(this).html('<div style="text-align: center; font-size: 14px; color: #999; align-item: center"><i class="fas fa-circle-notch fa-spin fa-2x fa-fw"></i><br/>Đang tải dữ liệu</div>');
    };

    $.fn.LoadedTable = function () {
        $(this).empty();
    };

    $.fn.LoadingContent = function () {
        $(this).html('<div style="text-align: center; font-size: 14px; color: #999; align-item: center"><i class="fas fa-circle-notch fa-spin fa-2x fa-fw"></i></div>');
    };

    $.fn.LoadedContent = function () {
        $(this).empty();
    };
    setLeftControlHover();
    FnButtonLoading();
});

function FnButtonLoading() {
    let currentText = "";
    let settingHidden = true;
    $.fn.buttonLoading = function (text, hidden) {
        if (text === undefined || text === null) text = '<i class="fas fa-spinner-third fa-spin"></i> ' + text;
        if (hidden !== undefined) settingHidden = hidden;
        currentText = $(this).html();
        $(this).html(text);
        $(this).prop('disabled', true);
        console.log(currentText);
    };
    $.fn.buttonLoading = function (hidden) {
        let text = '<i class="fas fa-spinner-third fa-spin"></i> Đang xử lý...';
        if (hidden !== undefined) settingHidden = hidden;
        currentText = $(this).html();
        $(this).html(text);
        $(this).prop('disabled', true);
    };
    $.fn.buttonLoaded = function (hidden) {
        $(this).html(currentText);
        $(this).prop('disabled', false);
        if (hidden === undefined) {
            if (settingHidden === true) $(this).hide();
        }
    };
}

function setLeftControlHover() {
    let hoverLeftControlMode = false;
    $(htmlTag.LeftControl).hover(function () {
        if (window.outerWidth > 768) {
            if (jqItem.LeftControl.outerWidth() === 55) {
                maxtifyLeftControl();
                hoverLeftControlMode = true;
            }
        }
    }, function () {
        if (hoverLeftControlMode === true) {
            mintifyLeftControl();
            hoverLeftControlMode = false;
        }
    });
}

$(document).on("click", ".toggle-screen-modal", function (e) {
    e.preventDefault();
    $(this).parents(".modal-dialog").toggleClass("modal-screen");
    $(this).find("i").toggleClass("fa-compress-arrows-alt fa-expand-arrows");
});

$(document).on("click", ".toggle-super-modal", function (e) {
    e.preventDefault();
    $(this).parents(".modal-dialog").toggleClass("modal-super");
    $(this).find("i").toggleClass("fa-compress-arrows-alt fa-expand-arrows");
});

$(document).on("click", ".toggle-lg-modal", function (e) {
    e.preventDefault();
    $(this).parents(".modal-dialog").toggleClass("modal-lg");
    $(this).find("i").toggleClass("fa-compress-arrows-alt fa-expand-arrows");
});

$(document).on("click", ".control-item .item a", function (e) {
    e.preventDefault();
    if ($(this).parent().hasClass("item-tree")) {
        if (window.location.pathname.toLowerCase() !== $(this).parent().find("li.active > a").attr("href")) {
            $(this).parent().toggleClass("active");
            $(this).parent().find("ul").slideToggle(300);
        }
    }
    else {
        window.location.href = $(this).attr("href");
    }
});

function settingToastr() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-bottom-left",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "200",
        "hideDuration": "200",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
}

function setActiveLeftControl() {
    if (window.location.pathname.toLowerCase() === "/") {
        $(".left-control").find(".item").first().addClass("active");
    }
    else {
        $.each($(".left-control").find(".item"), function () {
            if ($(this).hasClass("item-tree")) {
                $.each($(this).find("li > a"), function () {
                    if (window.location.pathname.toLowerCase() === $(this).attr("href")) {
                        $(this).parents(".item-tree").addClass("active");
                        $(this).parents(".item-tree").find("ul").slideToggle(0);
                        $(this).parent().addClass("active");
                    }
                });
            }
            else {
                if (window.location.pathname.toLowerCase() === $(this).find("a").attr("href")) {
                    $(this).addClass("active");
                }
            }
        });
    }
}

function loadUserInformation() {
    let user_data = JSON.parse(window.localStorage.getItem("user-profile"));
    $(htmlTag.DisplayName).html(user_data.displayName);
}

function mintifyLeftControl() {
    jqItem.ControlExpand.html('<i class="fa fa-indent fa-fw"></i>');
    jqItem.LeftControl.css('width', '55px');
    jqItem.Main.css('width', 'calc(100% - 55px)');
    $(".control-item>.item-tree.active>ul").hide();
}

function maxtifyLeftControl() {
    jqItem.ControlExpand.html('<i class="fa fa-outdent fa-fw"></i>');
    jqItem.LeftControl.css('width', '235px');
    jqItem.Main.css('width', 'calc(100% - 235px)');
    $(".control-item>.item-tree.active>ul").show();
}

jqItem.ControlExpand.click(function () {
    if (window.outerWidth > 768) {
        if (jqItem.LeftControl.outerWidth() === 235) {
            mintifyLeftControl();
        }
        else {
            maxtifyLeftControl();
        }
    }
    else {
        if (jqItem.LeftControl.outerWidth() === 235) {
            jqItem.ControlExpand.html('<i class="fa fa-indent fa-fw"></i>');
            jqItem.LeftControl.css('width', '0');
            jqItem.Main.css('width', '100%');
        }
        else {
            jqItem.ControlExpand.html('<i class="fa fa-outdent fa-fw"></i>');
            jqItem.LeftControl.css('width', '235px');
            jqItem.Main.css('width', 'calc(100% - 235px)');
        }
    }
});

$(document).find('.child-modal').on('hidden.bs.modal', function () {
    $('body').addClass('modal-open');
});

$("#user-setting-modal").on("show.bs.modal", function () {
    $("#user-setting-form")[0].reset();
    let data = JSON.parse(window.localStorage.getItem("user-profile"));
    $(this).find("#username-setting").val(data.username);
    $(this).find("#display-name-setting").val(data.displayName);
});

$("#submit-setting-form").click(function () {
    $.confirm({
        message: "Xác nhận đổi mật khẩu?",
        onOk: function () {
            let username = $("#username-setting").val();

            let oldPassword = $("#old-password-setting").val();
            if (oldPassword.length < 1) {
                $("#old-password-setting").focus();
                toastr.error("Mật khẩu cũ không được để trống!", "Cảnh báo:");
                throw "Old password is required";
            }

            let password = $("#password-setting").val();
            if (password.length < 1) {
                $("#password-setting").focus();
                toastr.error("Mật khẩu không được để trống!", "Cảnh báo:");
                throw "Password is required";
            }
            if (password === oldPassword) {
                $("#password-setting").focus();
                toastr.error("Mật khẩu mới không được trùng mật khẩu cũ!", "Cảnh báo:");
                throw "Password dont match";
            }

            let rePassword = $("#re-password-setting").val();
            if (rePassword.length < 1) {
                $("#re-password-setting").focus();
                toastr.error("Mật khẩu không được để trống!", "Cảnh báo:");
                throw "Password is required";
            }
            if (password !== rePassword) {
                $("#re-password-setting").focus();
                toastr.error("Mật khẩu mới không trùng khớp!", "Cảnh báo:");
                throw "Password don't match";
            }

            $("#submit-setting-form").buttonLoading();

            $.ajax({
                type: "POST",
                data: { Username: username, PasswordOld: oldPassword, PasswordOrigin: password },
                url: "/User/UpdatePasswordAsync",
                success: function () {
                    $("#submit-setting-form").buttonLoaded(false);
                    toastr.success("Đổi mật khẩu thành công!", "Thông báo:");
                },
                error: function (xhr) {
                    $("#submit-setting-form").buttonLoaded(false);
                    toastr.error(xhr.responseText, "Thông báo:");
                }
            });
        }
    });
});

function convertCentimetersToInches(cm) {
    return (cm * 0.39370).toFixed(2);
}

function convertInchesToCentimeters(inch) {
    return (inch / 0.39370).toFixed(2);
}

function ToJavaScriptFullDate(value) {
    var dt = new Date(value);
    let resHour = dt.getHours();
    let resMinute = dt.getMinutes();
    let resSecond = dt.getSeconds();
    let resDate = dt.getDate();
    let resMonth = dt.getMonth() + 1;
    let resYear = dt.getFullYear();
    return (resHour < 10 ? "0" : "") + resHour + ":" + (resMinute < 10 ? "0" : "") + resMinute + ":" + resSecond + " - " + (resDate < 10 ? "0" : "") + resDate + "/" + (resMonth < 10 ? "0" : "") + resMonth + "/" + resYear;
}

$("#modal-print-option").on("show.bs.modal", function () {
    $(this).find("#topPrintMargin").val(convertInchesToCentimeters(topPrintMargin));
    $(this).find("#bottomPrintMargin").val(convertInchesToCentimeters(bottomPrintMargin));
    $(this).find("#leftPrintMargin").val(convertInchesToCentimeters(leftPrintMargin));
    $(this).find("#rightPrintMargin").val(convertInchesToCentimeters(rightPrintMargin));
    $(this).find(".printer-setting-content").css("top", $("#topPrintMargin").val() * 10 + "px");
    $(this).find(".printer-setting-content").css("bottom", $("#bottomPrintMargin").val() * 10 + "px");
    $(this).find(".printer-setting-content").css("left", $("#leftPrintMargin").val() * 10 + "px");
    $(this).find(".printer-setting-content").css("right", $("#rightPrintMargin").val() * 10 + "px");
});

$("#topPrintMargin, #bottomPrintMargin, #leftPrintMargin, #rightPrintMargin").keyup(function () {
    $(".printer-setting-content").css("top", $("#topPrintMargin").val() * 10 + "px");
    $(".printer-setting-content").css("bottom", $("#bottomPrintMargin").val() * 10 + "px");
    $(".printer-setting-content").css("left", $("#leftPrintMargin").val() * 10 + "px");
    $(".printer-setting-content").css("right", $("#rightPrintMargin").val() * 10 + "px");
});

$("#topPrintMargin, #bottomPrintMargin, #leftPrintMargin, #rightPrintMargin").change(function () {
    $(".printer-setting-content").css("top", $("#topPrintMargin").val() * 10 + "px");
    $(".printer-setting-content").css("bottom", $("#bottomPrintMargin").val() * 10 + "px");
    $(".printer-setting-content").css("left", $("#leftPrintMargin").val() * 10 + "px");
    $(".printer-setting-content").css("right", $("#rightPrintMargin").val() * 10 + "px");
});