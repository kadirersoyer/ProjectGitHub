$(document).ready(function () {

    var orderform = $("#orderform");
    var btnSelect = $("#btnSelect");
    var productContent = $("#productContent");
    var modal = $("#ProductModal");
    var btnGetCheckedData = $("#btnGetCheckedData");
    btnSelect.on("click", function () {

        debugger;

        $.ajax({
            url: "/order/GetProductList",
            dataType: "html",
            type: "GET",
            success: function (response) {
                debugger;
                productContent.html(response);
                modal.modal('show');
            },
            error: function (response) {
                alert(response.responseText);
            }
        })
    })
    btnGetCheckedData.on("click", function () {

        var pruductDataList = [];

        var tbody = $("#OrderList tbody");

        var trs = $("#Productlist tbody tr");

        trs.each(function (index, element) {

            var isChecked = $(this).find("input[type=checkbox]:checked");

            if (isChecked.length > 0) {
                pruductDataList.push(
                    {
                        OrderId: 0,
                        id: isChecked.val(),
                        name: $(this).find("td[id=name]").text(),
                        price: $(this).find("td[id=price]").text(),
                        catname: $(this).find("td[id=catname]").text()
                    });
            }
        })
        var HTML = "";

        $.each(pruductDataList, function (index, element) {

            HTML += "<tr>";

            HTML += "<td id='OrderNo' style='display: none' data-id='0'>";
            HTML += "</td>";

            HTML += "<td id='OrderId' style='display: none'>" + element.OrderId + "</td>";
            HTML += "</td>";

            HTML += "<td id='ProductId' style='display: none'>";
            HTML += element.id;
            HTML += "</td>";

            HTML += "<td>";
            HTML += element.name;
            HTML += "</td>";

            HTML += "<td>";
            HTML += element.catname;
            HTML += "</td>";

            HTML += "<td>";
            HTML += "<input type='number' value = '1' />";
            HTML += "</td>";

            HTML += "<td>";
            HTML += "<input type='text' value='" + element.price + "' disabled />"
            HTML += "</td>";

            HTML += "<td>";
            HTML += " <button onclick='DeleteRow(this)' class='btn btn-danger'><span class='glyphicon glyphicon-minus' ></span > Sil </button>"
            HTML += "</td>";

            HTML += "</tr>";

        })

        tbody.append(HTML);

        modal.modal('hide');
    })

    orderform.submit(function (e) {

        e.preventDefault();

        var tbody = $("#OrderList tbody");

        var Rows = tbody.find("tr");

        var message = "";
        message += Rows.length == 0 ? "Satır Girmediniz" : "";
        message += $("#customerid").val() == "" ? "Müşteri Seçmediniz" : "";

        if (message != "") {

            alert(message);

            return;
        }
        var orders = [];

        Rows.each(function (index, element) {

            orders.push({

                id: parseInt($(this).find("td[id='OrderId']").text()),
                TotalPrice: parseFloat($(this).find("input[type='number']").val()) * parseFloat($(this).find("input[type='text']").val()),
                Quantity: $(this).find("input[type='number']").val(),
                CustomerID: $("#customerid").val(),
                ProductID: parseInt( $(this).find("td[id='ProductId']").text() ),
                name: "",
                CreateDate: null,
                UpdatedDate: null,
                OrderNo: $(this).find("td[id='OrderNo']").attr("data-id")

            });

        });
      
        $.ajax({
            url: "/order/CreateOrder",
            dataType: "json",
            type: "post",
            data: JSON.stringify( orders ),
            contentType: "application/json; charset=utf8",
            success: function (response) {
                debugger;
                alert(response.message);
                window.location.href = "/order/index";
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });

    })
})
function DeleteRow(data) {
    var tr = $(data).parent().parent();
    tr.remove();
}