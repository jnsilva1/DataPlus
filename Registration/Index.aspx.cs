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
        }

        private void Cpf_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(value: Cpf.Text) == false)
            {
                if (long.TryParse(Cpf.Text.Replace(".", "").Replace("-", "").Replace("_", ""), out long cpf))
                {
                    this.CurrentPessoa = PessoaDAO.Consulte(cpf) ?? new Pessoa();
                    this.PopulatePessoa();
                }
            }
        }

        private void PopulatePessoa()
        {
            Nome.Text = CurrentPessoa.Nome;
            //TODO: Finalizar método que popula a pessoa.
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

        internal void RemovePhone(int index)
        {

        }

        private void ResetTelefonesDaPessoa()
        {
            foreach(GridViewRow row in this.Telefones.Rows)
            {
                TelefonesDaPessoa.Add(new Telefone { Ddd = Convert.ToInt32(row.Cells[1].Text), Numero = Convert.ToInt32(row.Cells[2].Text), Tipo = new TipoTelefone { Tipo = row.Cells[3].Text } });
            }
        }

        private void ProcessEvent(string target, string argument)
        {
            if (target.ToUpper().Contains("$TELEFONES"))
            {
                if((argument ?? "").ToUpper().Contains("DELETE$"))
                {
                    if (int.TryParse(argument.Split('$')[1], out int index))
                    {
                        if(index >= 0 && index < TelefonesDaPessoa.Count)
                            TelefonesDaPessoa.Remove(TelefonesDaPessoa.ElementAt(index: index));
                    }
                }
            }
        }
    }
}