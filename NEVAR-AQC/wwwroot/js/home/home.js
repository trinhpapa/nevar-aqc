window.onload = function () {
    HomeStatisticAsync();
    GetLogLoginAsync();
    GetLineStatisticAsync();
};

function HandleData(data) {
    if (!data || data.length < 1) {
        throw "Data null";
    }
    const firstItem = data[0];
    const lastItem = data[data.length - 1];
    if (firstItem.month > 1) {
        for (let i = 1; i < firstItem.month; i++) {
            data.push({
                month: i,
                year: firstItem.year,
                testRequirement: null,
                calibrationRequirement: null
            });
        }
    }
    if (lastItem.month < 12) {
        for (let i = lastItem.month + 1; i <= 12; i++) {
            data.push({
                month: i,
                year: lastItem.year,
                testRequirement: null,
                calibrationRequirement: null
            });
        }
    }

    return data.orderBy(function (e) { return e.month; });
}

function GetLineStatisticAsync() {
    $.ajax({
        type: "GET",
        url: "/Statistic/LineStatisticAsync",
        success: function (data) {

            data = HandleData(data);

            $("#line-title").html("Thống kê phiếu yêu cầu năm " + data[0].year);

            let ctx = document.getElementById("myChart");
            let myChart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
                    datasets: [{
                        data: data.map(item => {
                            return item.testRequirement;
                        }),
                        label: "Yêu cầu thử nghiệm",
                        borderColor: "#FF6384",
                        fill: false
                    },
                    {
                        data: data.map(item => {
                            return item.calibrationRequirement;
                        }),
                        label: "Yêu cầu hiệu chuẩn",
                        borderColor: "#36A2EB",
                        fill: false
                    }
                    ]
                },
                options: {
                    scales: {
                        xAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: 'Tháng'
                            }
                        }],
                        yAxes: [{
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: 'Số phiếu'
                            }
                        }]
                    }
                },
            });
        }
    });
}

function GetLogLoginAsync() {
    $.ajax({
        type: "GET",
        url: "/SystemLog/GetLogLogin",
        success: function (data) {
            $("#show-login-log").html("");
            $.each(data, function (i, e) {
                let date = new Date(Date.parse(e.loginTime));
                $("#show-login-log").append('<div class="login-item">- '
                    + '<span class="login-username">' + e.username + '</span> đã đăng nhập lúc <span class="login-time"> ' + ToJavaScriptFullDate(date) + '</span>'
                    + '</div >');
            });
        }
    });
}

function HomeStatisticAsync() {
    $.ajax({
        type: "GET",
        url: "/Statistic/HomeStatisticAsync",
        success: function (data) {
            let invoiceCount = new CountUp('invoiceCount', data.invoiceCount);
            let customerCount = new CountUp('customerCount', data.customerCount);
            let fieldCount = new CountUp('fieldCount', data.fieldCount);
            let departmentCount = new CountUp('departmentCount', data.departmentCount);
            invoiceCount.start();
            customerCount.start();
            fieldCount.start();
            departmentCount.start();
        }
    });
}


systemHub.on("userLogin", function () {
    GetLogLoginAsync();
});
