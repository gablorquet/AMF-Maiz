require(
    [
        "jquery",
        'knockout',
        "datatables",
        "lazy"
    ],
    function ($, ko) {

        var model = {};
        model.eventNumber = requireConfig.pageOptions.eventNumber;
        ko.applyBindings(model);

        $("#characters").DataTable({
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