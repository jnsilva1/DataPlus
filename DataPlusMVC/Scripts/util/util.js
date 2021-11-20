//Extensão para realizar a validação customizada
$.fn.validateForm = function validateForm(showMessage) {

    if ($(this).data('validator')) {
        var notyMessage = "";

        var isValid = $(this).valid();
        var validator = $(this).data('validator');

        Array.from(validator.toShow).forEach(function (element) {
            $(element).hide();
        });

        validator.errorList.forEach(function (error) {
            if ($.trim(notyMessage) != "") notyMessage += "<br/>";
            notyMessage += ("<strong> " + $(error.element.labels).not('.error').text() + "</strong>: ") + error.message;
        });

        if (showMessage && isValid == false) {
            noty({ type: 'warning', layout: 'center', closeWith: ['click'], text: notyMessage, template: '<div class="noty_message text-left pl-3"><span class="noty_text"></span><div class="noty_close"></div></div>' });
        }

        return isValid;
    }
    return true;
};