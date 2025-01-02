var dataTable;

$(document).ready(function () {
    //dataTable = $('#DT_OrderList').DataTable({
    //    "ajax": { //AJAX request
    //        "url": "/api/order",
    //        "type": "GET",
    //        "datatype" : "json"
    //    },
    //    //configurare care vor fi coloanele din data table
    //    "columns": [
    //        { "data": "id", "width": "10%" },
    //        { "data": "name", "width": "25%" },
    //        { "data": "appUser.email", "width": "15%" },
    //        { "data": "orderTotal", "width": "15%" },
    //        { "data": "phoneNumber", "width": "20%" },
    //        {
    //            "data": "id",
    //            "render": function (data) {
    //                return `<div class="d-flex justify-content-center"
    //                            <div class="w-75">
    //                            <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-white mx-2">
    //                            <i class="bi bi-pencil-square">  </i>  </a>
    //                            </div>
    //                        </div>`

    //            },
    //            "width": "10%"
    //        }
    //    ],
    //    "width":"100%"
    //});

    //get the current url
    var url = window.location.search;
    if (url.includes("cancelled")) {
        loadList("canceled");
    }
    else {
        if (url.includes("completed")) {
            loadList("completed");
        }
        else {
            if (url.includes("ready")) {
                loadList("ready");
            }
            else {
                loadList("inprocess")
            }
        }
    }

});

function loadList(param) {
    dataTable = $('#DT_OrderList').DataTable({
        "ajax": { //AJAX request
            "url": "/api/order?status=" + param,
            "type": "GET",
            "datatype": "json"
        },
        //configurare care vor fi coloanele din data table
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "name", "width": "25%" },
            { "data": "appUser.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "phoneNumber", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="d-flex justify-content-center"
                                <div class="w-75">
                                <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-white mx-2">
                                <i class="bi bi-pencil-square">  </i>  </a> 
                                </div>
                            </div>`

                },
                "width": "10%"
            }
        ],
        "width": "100%"
    });
}
