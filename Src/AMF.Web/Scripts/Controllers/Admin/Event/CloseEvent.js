require(
    [
        "jquery",
        "knockout",
        "lazy",
    ],
    function ($, ko, Lazy) {
        var model = {};
        model.EventStats = function() {
            var self = this;

            var eventData = requireConfig.pageOptions.model;

            return eventData;
        }

        ko.applyBindings(model);
    })