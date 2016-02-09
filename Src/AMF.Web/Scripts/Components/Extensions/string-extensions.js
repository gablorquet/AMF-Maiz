// Implementing C# String.format function in javascript
if (!String.format) {

    String.format = function (format) {
        var args = Array.prototype.slice.call(arguments, 1);
        return format.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
};

if (!String.actionLink) {
    String.actionLink = function(text, action, controller, params, css) {
        String.format("<a href=\"/{0}/{1}?{2}\" class=\"{3}\">{4}</a>",
            controller, action, params, css, text);
    }
}

// Get the age from a date
String.prototype.getAge = function () {
    var dateString = this;
    var today = new Date();
    var birthDate = new Date(dateString);

    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }

    if (isNaN(age)) return '';

    return age;
};