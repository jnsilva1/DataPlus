<%@ Page Language="C#" Title="Cadastro" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Insert.aspx.cs" Inherits="DataPlus.Registration.Insert" %>

<asp:Content  ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <ul class="nav nav-tabs border-bottom-0">
        <li class="nav-item">
            <a class="nav-link active" href="#tab-1">Ficha de Cadastro</a>
         </li>
    </ul>
    <div class="tab-1 tab-content border-1 border p-3" id="tab-1">
        <div class="card card-ligth mb-3">
            <div class="card-header">
                Dados Pessoais
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-md-4">
                        <div class="form-group">
                            <label for="Id">ID</label>
                            <asp:TextBox CssClass="form-control" ID="Id" runat="server" Enabled="false" ></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-12 col-md-8">
                        <div class="form-group">
                            <label for="Nome">Nome</label>
                            <asp:TextBox CssClass="form-control" ID="Nome" runat="server" MaxLength="256" ></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-12 col-md-4">
                        <div class="form-group">
                            <label for="Cpf">CPF</label>
                            <asp:TextBox CssClass="form-control" ID="Cpf" runat="server" MaxLength="15"></asp:TextBox>
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
                <div class="row">
                    <div class="col-3 col-md-2">
                        <div class="form-group">
                            <label for="MainContent_Telefone_DDD">DDD</label>
                            <asp:TextBox CssClass="form-control" ID="Telefone_DDD" runat="server" MaxLength="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-9 col-md-4">
                        <div class="form-group">
                            <label for="MainContent_Telefone_Numero">Número</label>
                            <asp:TextBox CssClass="form-control" ID="Telefone_Numero" runat="server" MaxLength="9"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-8 col-md-4">
                        <div class="form-group">
                            <label for="MainContent_Telefone_Tipo">Tipo</label>
                            <asp:DropDownList ID="Telefone_Tipo" CssClass="custom-select" runat="server" />
                        </div>
                    </div>
                    <div class="col-4 col-md-2">
                        <div class="form-group">
                            <label for="btn_adicionar_telefone" > </label>
                            <button type="button" class="btn btn-primary btn-block mt-1" id="btn_adicionar_telefone">+ Adicionar</button>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <asp:GridView runat="server" ID="Telefones" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" >
        $(function () {
            $('#MainContent_Cpf').inputmask('999.999.999-99');
            $('#MainContent_Endereco_Cep').inputmask('99.999-999');
        });
    </script>
</asp:Content>