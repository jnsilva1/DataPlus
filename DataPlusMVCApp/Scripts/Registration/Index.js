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
        $(document.Registration).find('input:text, input:hidden, input[type="number"], select').val("").keypress();
        $(document.Registration).data('validator').resetForm();
    });

    //Processo responsável por adicionar o telefone na lista
    $(document.Registration.btn_adicionar_telefone).click(function () {
        $(document.Registration).find('[required]').not('.card-phone-number [required]').addClass('ignore');
        if ($(document.Registration).validateForm(true)) {
            AddTelefone(
                Number($.trim(document.Registration.Telefone_DDD.value).replaceAll("(", "").replaceAll(")", "")),
                Number($.trim(document.Registration.Telefone_Numero.value).replaceAll(".", "").replaceAll("-", "")),
                document.Registration.Telefone_Tipo.value);
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
        BindBtnRemovePhoneClick();

        $(document.Registration).find('#Cpf').change(function () {
            FindByCPF($.trim($(this).val()).replaceAll(".", "").replaceAll("-", "").replaceAll("_", ""));
        });

        $(document.Registration).find('#btnSave').click(function () {
            $(document.Registration).find('.card-phone-number [required]').addClass('ignore');
            if ($(document.Registration).validateForm(true)) {
                SavePessoa(JSON.stringify({
                    "Nome": $(document.Registration).find('#Nome').val(),
                    "Cpf": Number($.trim($(document.Registration).find('#Cpf').val()).replaceAll(".", "").replaceAll("-", "")),
                    "Endereco": {
                        "Logradouro": $(document.Registration).find('#Endereco_Logradouro').val(),
                        "Numero": Number($.trim($(document.Registration).find('#Endereco_Numero').val())),
                        "Cep": Number($.trim($(document.Registration).find('#Endereco_Cep').val()).replaceAll(".", "").replaceAll("-", "")),
                        "Bairro": $(document.Registration).find('#Endereco_Bairro').val(),
                        "Cidade": $(document.Registration).find('#Endereco_Cidade').val(),
                        "Estado": $(document.Registration).find('#Endereco_Estado').val(),
                    },
                    Telefones: Array.from($('#phoneList').find('tbody').find('tr')).filter(function (item) { return $(item).find('td').length > 1; }).map(function (item) {
                        return {
                            "Ddd": Number($.trim($(item).find('[data-ddd]').text())), "Numero": Number($.trim($(item).find('[data-numero]').text())), "Tipo": { "Tipo": $.trim($(item).find('[data-tipo]').text()) }
                        }
                    })
                }));
            }
            $(document.Registration).find('[required]').removeClass('ignore');
        });

        $(document.Registration).find('#btnDelete').click(function () {
            $(document.Registration).find('.card-phone-number [required]').addClass('ignore');
            if ($(document.Registration).validateForm(false)) {

                noty({
                    text: 'Tem certeza que deseja excluir o registro selecionado?',
                    type: 'warning',
                    modal: true,
                    layout: 'center',
                    buttons: [
                        {
                            text: 'Sim', addClass: 'btn btn-info', onClick: function ($noty, event) {
                                $noty.close();
                                DeletePessoa(Number($.trim($(document.Registration).find('#Cpf').val()).replaceAll(".", "").replaceAll("-", "")));
                            }
                        },
                        {
                            text: 'Não', addClass: 'btn btn-danger', onClick: function ($noty, event) {
                                $noty.close();
                            }
                        }
                    ]
                });

            } else {
                noty({ text: 'Selecione um registro para excluir', type: 'warning', layout: 'topRight', closeWith: ['click'] });
                $(document.Registration).data('validator').resetForm();
            }
            $(document.Registration).find('[required]').removeClass('ignore');
        });
    });

    /**
     * Aplica aos botões de remoção do telefone o processo a ser executado
     * */
    function BindBtnRemovePhoneClick() {
        $('#phoneList').find('button.btn-remove-phone').unbind('click').click(function () {
            if ($('#phoneList').find('tbody').find('tr').length == 1) {
                $('#phoneList').find('thead').html("");
                $('#phoneList').find('tbody').html("").append($('<tr>').append($('<td colspan="4">').text(emptyPhoneNumberListMessage)));
            }
            $(this).parents('tr:first').remove();
        });
    }

    /**
     * Carrega os dados da pessoa a partir do CPF
     * @param {number} cpf
     */
    function FindByCPF(cpf) {
        $(document.Registration.btnClean).click();
        if ($.trim(cpf)) {
            $.blockUI();
            $.ajax({
                url: rootPath + 'Registration/Find',
                dataType: 'json',
                data: { cpf: cpf },
                /**
                 * Callback
                 * @param {{"Nome":string,"Cpf":number,"Endereco":{"Logradouro":string,"Numero":number,"Cep":number,"Bairro":string,"Cidade":string,"Estado":string},"Telefones":{"Numero":number,"Ddd":number,"Tipo":{"Tipo":string}}[]}} res
                 */
                success: function (res) {
                    PopulatePessoa(res);
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        }
    }
    /**
     * Adiciona o telefone para a lista | Tabela
     * @param {number} ddd
     * @param {number} numero
     * @param {string} tipo
     */
    function AddTelefone(ddd, numero, tipo) {
        if ($.trim(ddd) != "" && $.trim(numero) != "" && $.trim(tipo) != "") {
            if ($('#phoneList').find('thead').find('tr').length == 0) {
                $('#phoneList').find('thead').append(phoneListHeader);
                $('#phoneList').find('tbody').html("");
            }

            $('#phoneList').find('tbody').append(
                $('<tr>')
                    .append($('<td data-ddd>').text(ddd))
                    .append($('<td data-numero>').text(numero))
                    .append($('<td data-tipo>').text(tipo))
                    .append($('<td>').addClass('d-flex justify-content-center').append($('<button type="button" class="btn btn-danger btn-remove-phone btn-times" title="Remover">').append('<i class="fa fa-times"></i>')))
            );

            $('.card-phone-number').find('input:text').val('');
            BindBtnRemovePhoneClick();
        }
    }

    /**
     * Popula os dados da pessoa para a tela
     * @param {{"Nome":string,"Cpf":number,"Endereco":{"Logradouro":string,"Numero":number,"Cep":number,"Bairro":string,"Cidade":string,"Estado":string},"Telefones":{"Numero":number,"Ddd":number,"Tipo":{"Tipo":string}}[]}} pessoa
     */
    function PopulatePessoa(pessoa) {
        if ($.isPlainObject(pessoa) && $.isEmptyObject(pessoa) == false && $.trim(pessoa["Nome"])) {
            $(document.Registration).find('#Nome').val(pessoa.Nome);
            $(document.Registration).find('#Cpf').val($.trim(pessoa.Cpf).padStart(11, '0')).keypress();
            $(document.Registration).find('#Endereco_Logradouro').val(pessoa.Endereco.Logradouro);
            $(document.Registration).find('#Endereco_Numero').val(pessoa.Endereco.Numero);
            $(document.Registration).find('#Endereco_Cep').val($.trim(pessoa.Endereco.Cep).padStart(8, '0')).keypress();
            $(document.Registration).find('#Endereco_Bairro').val(pessoa.Endereco.Bairro);
            $(document.Registration).find('#Endereco_Cidade').val(pessoa.Endereco.Cidade);
            $(document.Registration).find('#Endereco_Estado').val(pessoa.Endereco.Estado);
            pessoa.Telefones.forEach(function (telefone) {
                AddTelefone(telefone.Ddd, telefone.Numero, telefone.Tipo.Tipo);
            });
        }
    }

    /**
     * Envia os dados da pessoa para gravação
     * @param {{"Nome":string,"Cpf":number,"Endereco":{"Logradouro":string,"Numero":number,"Cep":number,"Bairro":string,"Cidade":string,"Estado":string},"Telefones":{"Numero":number,"Ddd":number,"Tipo":{"Tipo":string}}[]}} pessoa
     */
    function SavePessoa(pessoa) {

        var obj = JSON.parse(pessoa);
        if ($.isPlainObject(obj) && $.isEmptyObject(obj) == false) {
            console.log(pessoa);
            $.blockUI();
            $.ajax({
                url: rootPath + 'Registration/Save',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                data: JSON.stringify({ pessoaJson: pessoa }),
                success: function (res) {
                    noty($.extend(true, res, { closeWith: ['click'], layout: 'topRight' }));
                    if (res["type"] == "success")
                        QuestionUserWantCleanForm();
                },
                complete: function () {
                    $.unblockUI();
                }
            })
        }
    }

    /**
     * Realiza a exclusão da pessoa e seus dados
     * @param {number} cpf
     */
    function DeletePessoa(cpf) {
        cpf = Number($.trim(cpf));
        if (isNaN(cpf) == false) {
            $.blockUI();
            $.ajax({
                url: rootPath + 'Registration/Delete',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'POST',
                data: JSON.stringify({ cpf: cpf}),
                success: function (res) {
                    noty($.extend(true, res, { closeWith: ['click'], layout: 'topRight' }));
                    //Se excluiu com sucesso, limpo a tela
                    if (res["type"] == "success")
                        $(document.Registration.btnClean).click();
                },
                complete: function () {
                    $.unblockUI();
                }
            })
        }
    }
    /**
     * Questiona ao usuário se deseja limpar a tela.
     * */
    function QuestionUserWantCleanForm() {
        noty({
            text: 'Deseja limpar o formulário?',
            type: 'information',
            modal: true,
            layout:'center',
            buttons: [
                {
                    text: 'Sim', addClass: 'btn btn-info', onClick: function ($noty, event) {
                        $noty.close();
                        $(document.Registration.btnClean).click();
                    }
                },
                {
                    text: 'Não', addClass: 'btn btn-danger', onClick: function ($noty, event) {
                        $noty.close();
                    }
                }
            ]
        });
    }

 });