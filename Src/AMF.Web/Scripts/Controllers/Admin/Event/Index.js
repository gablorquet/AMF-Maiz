require(
    [
        "jquery",
        "datatables",
        "lazy"
    ],
    function ($) {
        $("#events").DataTable({
            serverSide: false,
            ordering: true,
            "pagingType": "full_numbers",
            "pageLength": 25,
            "dom": 'frt<i>p',
            columns: [
                { "orderable": true, "searchable": true },
                { "orderable": false, "searchable": true },
                { "orderable": false, "searchable": false },
                { "orderable": false, "searchable": false }
            ]
        });



    })