$(function () {
    ///Variáveis uteis
    $.validator.messages.required = "Este campo é obrigatório.";
    var emptyPhoneNumberListMessage = "Nenhum telefone foi adicionado...";
    var phoneListHeader = $('<tr>')
        .append($('<th scope="col">').text("DDD"))
        .append($('<th scope="col">').text("Número"))
        .append($('<th scope="col">').text("Tipo"))
        .append($('<th scope="col">').text(""));

    //Instancio objeto de validação dos campos da tela
    $(document.Registration).validate({
        ignore: '.ignore',
        rules: (function () {
            var rules = {};
            $('[required]').each(function (index, elem) {
                rules[elem.name] = { required: true };
            });
            return rules;
        })(),
        errorClass: 'error text-danger border-danger',
        validClass: 'valid'
    });

    //Limpar a tela
    $(document.Registration.btnClean).click(function () {
        $('#phoneList').find('thead').html("");
        $('#phoneList').find('tbody').html("").append($('<tr>').append($('<td colspan="4">').html(emptyPhoneNumberListMessage)));
        $(document.Registration).data('validator').resetForm();
    });

    //Processo responsável por adicionar o telefone na lista
    $(document.Registration.btn_adicionar_telefone).click(function () {
        $(document.Registration).find('[required]').not('.card-phone-number [required]').addClass('ignore');
        if ($(document.Registration).validateForm(true)) {
            if ($('#phoneList').find('thead').find('tr').length == 0) {
                $('#phoneList').find('thead').append(phoneListHeader);
                $('#phoneList').find('tbody').html("");
            }

            $('#phoneList').find('tbody').append(
                $('<tr>')
                    .append($('<td>').text(document.Registration.Telefone_DDD.value))
                    .append($('<td>').text(document.Registration.Telefone_Numero.value))
                    .append($('<td>').text(document.Registration.Telefone_Tipo.value))
                    .append($('<td>').addClass('d-flex justify-content-center').append($('<button type="button" class="btn btn-danger btn-remove-phone btn-times" title="Remover">').append('<i class="fa fa-times"></i>').click(function () {
                        if ($('#phoneList').find('tbody').find('tr').length == 1) {
                            $('#phoneList').find('thead').html("");
                            $('#phoneList').find('tbody').html("").append($('<tr>').append($('<td colspan="4">').text(emptyPhoneNumberListMessage)));
                        }
                        $(this).parents('tr:first').remove();
                    })))
            );

            $('.card-phone-number').find('input:text').val('');
        }
        $(document.Registration).find('[required]').removeClass('ignore');
    });

    ///Aplica formatações em determinados campos
    $(function () {
        $(document.Registration).find('#Telefone_DDD').inputmask('(99)');
        $(document.Registration).find('#Telefone_Numero').blur(function () {
            var value = $.trim($(this).inputmask('unmaskedvalue'));
            if (value.length < 9)
                $(this).inputmask('remove').inputmask('9999-9999');
        }).focusin(function () { $(this).inputmask('remove').inputmask('9.9999-9999'); }).inputmask('9.9999-9999');
        $(document.Registration).find('#Cpf').inputmask('999.999.999-99');
        $(document.Registration).find('#Endereco_Cep').inputmask('99.999-999');
    })
});