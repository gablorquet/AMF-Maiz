require(
    [
        "jquery",
        "knockout",
        "lazy"
    ],
    function ($, knockout, Lazy) {
        var model = {};

        model.Debriefing = function() {
            var self = this;

            var data = requireConfig.pageOptions.model;

            self.attendees = ko.observableArray(model.attendees);


        }

        ko.applyBindings(model);
    }) 