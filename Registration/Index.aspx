<%@ Page Language="C#" Title="Cadastro" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DataPlus.Registration.Insert" ViewStateEncryptionMode="Never" %>

<asp:Content  ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="nav nav-tabs border-bottom-0 bg-dark border border-primary font-weight-bold" role="tablist">
        <li class="nav-item" role="presentation">
            <a class="nav-link active" data-toggle="tab" role="tab" aria-controls="tab-1" href="#tab-1">Ficha de Cadastro</a>
         </li>
       <%-- <li class="nav-item" role="presentation">
            <a class="nav-link" data-toggle="tab" role="tab" aria-controls="tab-2" href="#tab-2">Registros</a>
        </li>--%>
    </ul>
    <div class="tab-content border-1 border">
        <div class="tab-pane p-3 active" role="tabpanel" aria-labelledby="tab-1-tab" id="tab-1">
            <div class="card card-ligth mb-3">
                <div class="card-header">
                    Dados Pessoais
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-12 col-md-8">
                            <div class="form-group">
                                <label for="Nome">Nome</label>
                                <asp:TextBox CssClass="form-control" ID="Nome" runat="server" MaxLength="256" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label for="Cpf">CPF</label>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox CssClass="form-control" ID="Cpf" runat="server" MaxLength="15" AutoPostBack="True"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-primary mb-3">
                <div class="card-header bg-primary text-white">
                    Endereço
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-12 col-md-9">
                            <div class="form-group">
                                <label for="Endereco_Logradouro">Logradouro</label>
                                <asp:TextBox CssClass="form-control" ID="Endereco_Logradouro" runat="server" MaxLength="256"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 col-md-3">
                            <div class="form-group">
                                <label for="Endereco_Numero">Número</label>
                                <asp:TextBox CssClass="form-control" ID="Endereco_Numero" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-2">
                            <div class="form-group">
                                <label for="Endereco_Cep">CEP</label>
                                <asp:TextBox CssClass="form-control" ID="Endereco_Cep" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 col-md-4">
                            <div class="form-group">
                                <label for="Endereco_Bairro">Bairro</label>
                                <asp:TextBox CssClass="form-control" ID="Endereco_Bairro" runat="server" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 col-md-3">
                            <div class="form-group">
                                <label for="Endereco_Cidade">Cidade</label>
                                <asp:TextBox CssClass="form-control" ID="Endereco_Cidade" runat="server" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 col-md-3">
                            <div class="form-group">
                                <label for="Endereco_Estado">Estado</label>
                                <asp:DropDownList ID="Endereco_Estado" CssClass="custom-select" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-primary mb-3">
                <div class="card-header bg-primary text-white">
                    Contato
                </div>
                <div class="card-body">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-3 col-md-2">
                                    <div class="form-group">
                                        <label for="MainContent_Telefone_DDD">DDD</label>
                                        <asp:TextBox CssClass="form-control" ID="Telefone_DDD" runat="server" MaxLength="4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-9 col-md-4">
                                    <div class="form-group">
                                        <label for="MainContent_Telefone_Numero">Número</label>
                                        <asp:TextBox CssClass="form-control" ID="Telefone_Numero" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-8 col-md-4">
                                    <div class="form-group">
                                        <label for="MainContent_Telefone_Tipo">Tipo</label>
                                        <asp:TextBox CssClass="form-control" ID="Telefone_Tipo" runat="server" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-4 col-md-2">
                                    <div class="form-group">
                                        <label for="btn_adicionar_telefone"></label>
                                        <asp:Button CssClass="btn btn-primary btn-block mt-1 font-weight-bold" runat="server" ID="btn_adicionar_telefone" Text="+ Adicionar" UseSubmitBehavior="False" ValidateRequestMode="Disabled" ViewStateMode="Disabled" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-12">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:GridView runat="server" ID="Telefones"  CssClass="table table-hover" AutoGenerateDeleteButton="True">
                                        <EmptyDataTemplate>Nenhum telefone foi adicionado...</EmptyDataTemplate>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-primary">
                <div class="card-body">
                    <div class="row">
                        <div class="col-12">
                            <div class="btn-group">
                                <button id="btnSave"  runat="server" type="button" class="btn btn-primary font-weight-bold">
                                    <i class="fa fa-save"></i> Gravar</button>
                                <button id="btnClean"  runat="server" type="button" class="btn btn-primary font-weight-bold">
                                    <i class="fa fa-file"></i> Limpar
                                </button>
                                <button id="btnDelete"  runat="server" type="button" class="btn btn-danger font-weight-bold">
                                    <i class="fa fa-trash"></i> Excluir
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%--<div class="tab-2 tab-pane border-1 border p-3" role="tabpanel" aria-labelledby="tab-2-tab" id="tab-2">
            <div class="row">
                <div class="col-12">
                    <div class="input-group">
                        <asp:TextBox CssClass="form-control" runat="server" ClientIDMode="Static" ID="txtSearch"></asp:TextBox>
                        <div class="input-group-append">
                             <button id="btnCleanSearch"  runat="server" type="button" class="btn btn-outlined-secondary font-weight-bold">
                                    <i class="fa fa-times"></i>
                                </button>
                             <button id="btnSearch"  runat="server" type="button" class="btn btn-primary font-weight-bold">
                                    <i class="fa fa-search"></i> Pesquisar
                                </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView runat="server" ID="GridPessoas"  CssClass="table table-hover table-bordered">
                                <EmptyDataTemplate>Nenhum registro...</EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>--%>

    </div>
    <script type="text/javascript" >
        
        function AjustaCardContatoLayout() {
            setTimeout(function () {
                $('#MainContent_Telefones').prepend($('<thead>').append($('#MainContent_Telefones').find('th').parent('tr')));
                $('#MainContent_Telefone_DDD').inputmask('(99)');
                $('#MainContent_Telefone_Numero').blur(function () {
                    var value = $.trim($(this).inputmask('unmaskedvalue'));
                    if (value.length < 9)
                        $(this).inputmask('remove').inputmask('9999-9999');
                }).focusin(function () { $(this).inputmask('remove').inputmask('9.9999-9999'); }).inputmask('9.9999-9999');
                $('#MainContent_Telefones').find('tr').each(function (index) {
                    if (index == 0)
                        $(this).find('th:first').insertAfter($(this).find('th:last'))
                    else
                        $(this).find('td:first').insertAfter($(this).find('td:last'))

                    $(this).find('td a').each(function (i) {
                        $(this).parent().addClass('text-center');
                        $(this).replaceWith($('<button class="btn btn-danger btn-times" type="button" title="Remover">').attr('index', i).attr('onclick', $(this).attr('href').replaceAll("javascript:", "")).append($('<i class="fa fa-times">')).append($('<span class="hide">').text(" Remover")))
                    });
                });
            },500);
            return false;
        }
        $(function () {
            $('#MainContent_Cpf').inputmask('999.999.999-99');
            $('#MainContent_Endereco_Cep').inputmask('99.999-999');
            AjustaCardContatoLayout();
            Sys.WebForms.PageRequestManager._instance.add_pageLoaded(function () {
                AjustaCardContatoLayout();
            })
        });
    </script>
</asp:Content>