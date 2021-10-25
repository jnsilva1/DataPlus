using CadastroPessoaFisica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataPlus.Registration
{
    public partial class Insert : System.Web.UI.Page
    {
        internal CadastroPessoaFisica.Pessoa CurrentPessoa { get; set; }
        /// <summary>
        /// Lista com os nomes dos estados brasileiros
        /// </summary>
        private List<String> Estados = new List<string>
            {
                "Acre",
                "Alagoas",
                "Amapá",
                "Amazonas",
                "Bahia",
                "Ceará",
                "Distrito Federal*",
                "Espírito Santo",
                "Goiás",
                "Maranhão",
                "Mato Grosso",
                "Mato Grosso do Sul"
                ,"Minas Gerais"
                ,"Pará"
                ,"Paraíba"
                ,"Paraná"
                ,"Pernambuco"
                ,"Piauí"
                ,"Rio de Janeiro"
                ,"Rio Grande do Norte"
                ,"Rio Grande do Sul"
                ,"Rondônia"
                ,"Roraima"
                ,"Santa Catarina"
                ,"São Paulo"
                ,"Sergipe"
                ,"Tocantins"
            }.OrderBy(es => es).ToList();

        public ICollection<CadastroPessoaFisica.Telefone> TelefonesDaPessoa => this.CurrentPessoa.Telefones;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.CurrentPessoa = new Pessoa();
            this.CurrentPessoa.Telefones = new HashSet<Telefone>();
            if (IsPostBack ==  false)
            {
                BindDropdownListEstados();
                BindGridViewTelefones();
            }
            else
            {
                //ProcessEvent(target: Request["__EVENTTARGET"], argument: Request["__EVENTARGUMENT"]);
            }


            btn_adicionar_telefone.Click += (object cSender, EventArgs cE) => {
                AdicionarTelefone();
            };

            Telefones.RowDeleting += Telefones_RowDeleting;
            Cpf.TextChanged += Cpf_TextChanged;
            btnSave.ServerClick += GravarPessoa;
            btnClean.ServerClick += LimparTela;
        }

        private void LimparTela(object sender, EventArgs e)
        {
            LimparTela();
        }

        private void GravarPessoa(object sender, EventArgs e)
        {
            bool insercao = false, sucesso= false;
            long.TryParse(Cpf.Text.Replace(".", "").Replace("-", "").Replace("_", ""), out long cpf);
            CurrentPessoa = PessoaDAO.Consulte(cpf) ?? new Pessoa();
            insercao = CurrentPessoa.Cpf == 0;
            CurrentPessoa.Nome = Nome.Text;
            CurrentPessoa.Cpf = cpf;
            int.TryParse(Endereco_Cep.Text.Replace(".", "").Replace("-", "").Replace("_", ""), out int cep);
            int.TryParse(Endereco_Numero.Text.Replace(".", "").Replace("-", "").Replace("_", ""), out int numero);

            CurrentPessoa.Endereco = new Endereco()
            {
                Cep = cep,
                Logradouro = Endereco_Logradouro.Text,
                Numero = numero,
                Cidade = Endereco_Cidade.Text,
                Bairro = Endereco_Bairro.Text,
                Estado = Endereco_Estado.SelectedValue
            };
            ResetTelefonesDaPessoa();
            if (insercao)
            {
                sucesso = PessoaDAO.Insira(p: CurrentPessoa);
            }
            else
            {
                sucesso = PessoaDAO.Altere(p: CurrentPessoa);
            }
            if (sucesso)
                LimparTela();
        }

        private void LimparTela()
        {
            CurrentPessoa = new Pessoa();
            PopulatePessoa();
        }

        private void Cpf_TextChanged(object sender, EventArgs e)
        {
            if ((Request["__EVENTTARGET"] ?? "").ToLower().Contains("cpf") == false) return;
            if(string.IsNullOrEmpty(value: Cpf.Text) == false)
            {
                string txtCpf = Cpf.Text.Replace(".", "").Replace("-", "").Replace("_", "");
                if (txtCpf.Length == 11)
                {
                    if (long.TryParse(txtCpf, out long cpf))
                    {
                        this.CurrentPessoa = PessoaDAO.Consulte(cpf) ?? new Pessoa() {Cpf = cpf };
                        this.PopulatePessoa();
                    }
                }
            }
        }

        private void PopulatePessoa()
        {
            Nome.Text = CurrentPessoa.Nome;
            Cpf.Text = CurrentPessoa.Cpf > 0 ? CurrentPessoa.Cpf.ToString("000.000.000-00") : "";
            Endereco_Logradouro.Text = CurrentPessoa.Endereco.Logradouro;
            Endereco_Numero.Text = CurrentPessoa.Endereco.Numero > 0 ? CurrentPessoa.Endereco.Numero.ToString("00") : "";
            Endereco_Cep.Text = CurrentPessoa.Endereco.Cep > 0 ? CurrentPessoa.Endereco.Cep.ToString("00.000-000") :"";
            Endereco_Bairro.Text = CurrentPessoa.Endereco.Bairro;
            Endereco_Cidade.Text = CurrentPessoa.Endereco.Cidade;
            BindDropdownListEstados();
            Endereco_Estado.SelectedValue =  CurrentPessoa.Endereco.Estado;
            Telefone_DDD.Text = Telefone_Numero.Text = Telefone_Tipo.Text = "";
            BindGridViewTelefones();
        }

        private void Telefones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ResetTelefonesDaPessoa();
            TelefonesDaPessoa.Remove(TelefonesDaPessoa.ElementAt(index: e.RowIndex));
            BindGridViewTelefones();
        }

        private void BindDropdownListEstados()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Value", typeof(string)));
            dt.Columns.Add(new DataColumn("Text", typeof(string)));

            Estados.ForEach(estado => {
                DataRow row = dt.NewRow();
                row[0] = row[1] = estado;
                dt.Rows.Add(row);
            });

            Endereco_Estado.DataSource = new DataView(dt);
            Endereco_Estado.DataTextField = "Text";
            Endereco_Estado.DataValueField = "Value";
            Endereco_Estado.SelectedIndex = 0;
            Endereco_Estado.DataBind();
        }

        private void BindGridViewTelefones()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DDD", typeof(int)));
            dt.Columns.Add(new DataColumn("Número", typeof(int)));
            dt.Columns.Add(new DataColumn("Tipo", typeof(string)));

            TelefonesDaPessoa.ToList().ForEach(telefone => {
                DataRow row = dt.NewRow();
                row[0] = telefone.Ddd;
                row[1] = telefone.Numero;
                row[2] = telefone.Tipo.Tipo;

                dt.Rows.Add(row);
            });

            Telefones.DataSource = new DataView(dt);
            Telefones.DataBind();
        }

        private void AdicionarTelefone()
        {
            int.TryParse((Telefone_DDD.Text ?? "").Replace("(", "").Replace(")", "").Replace("_", ""), out int ddd);
            int.TryParse((Telefone_Numero.Text ?? "").Replace(".", "").Replace("-", "").Replace("_", ""), out int numero);

            if (ddd > 0 &&
                (numero > 0 && numero.ToString().Length >= 8 && numero.ToString().Length <= 9) &&
                string.IsNullOrEmpty(Telefone_Tipo.Text) == false 
                )
            {
                ResetTelefonesDaPessoa();
                this.TelefonesDaPessoa.Add(new Telefone { Ddd = ddd, Numero = numero, Tipo = new TipoTelefone { Tipo = Telefone_Tipo.Text } });
                BindGridViewTelefones();
                Telefone_DDD.Text = Telefone_Numero.Text = Telefone_Tipo.Text = "";
            }
        }

        private void ResetTelefonesDaPessoa()
        {
            CurrentPessoa.Telefones = new HashSet<Telefone>();
            foreach(GridViewRow row in this.Telefones.Rows)
            {
                TelefonesDaPessoa.Add(new Telefone { Ddd = Convert.ToInt32(row.Cells[1].Text), Numero = Convert.ToInt32(row.Cells[2].Text), Tipo = new TipoTelefone { Tipo = row.Cells[3].Text } });
            }
        }
    }
}