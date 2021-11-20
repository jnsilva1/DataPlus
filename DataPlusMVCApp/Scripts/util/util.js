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
            noty({ type: 'warning', modal:true, layout: 'center', closeWith: ['click'], text: notyMessage, template: '<div class="noty_message text-left pl-3"><span class="noty_text"></span><div class="noty_close"></div></div>' });
        }

        return isValid;
    }
    return true;
};

//Over write blockUI
$(function () {
    $.blockUI.defaults.css = $.extend(true, {}, $.blockUI.defaults.css, {
        border: 'none',
        padding: '15px',
        backgroundColor: '#000',
        '-webkit-border-radius': '10px',
        '-moz-border-radius': '10px',
        opacity: .5,
        color: '#fff'
    });
    $.blockUI.defaults.message = '<div class="d-flex justify-content-center"><div class="spinner-border text-primary" role="status"><span class="sr-only"></span></div><div><span class="ml-3 align-middle">Carregando...</span></div></div>';
});