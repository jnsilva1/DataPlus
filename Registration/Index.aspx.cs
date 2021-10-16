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
        public List<CadastroPessoaFisica.Telefone> LTelefones = new List<CadastroPessoaFisica.Telefone>();

        protected void Page_Load(object sender, EventArgs e)
        {
            LTelefones.Add(new CadastroPessoaFisica.Telefone { Ddd = 11, Numero = 952311101, Tipo = new CadastroPessoaFisica.TipoTelefone { Tipo = "Celular" } });
            LTelefones.Add(new CadastroPessoaFisica.Telefone { Ddd = 11, Numero = 48546523, Tipo = new CadastroPessoaFisica.TipoTelefone { Tipo = "Fixo" } });
            LTelefones.Add(new CadastroPessoaFisica.Telefone { Ddd = 85, Numero = 963690832, Tipo = new CadastroPessoaFisica.TipoTelefone { Tipo = "Celular" } });

            Endereco_Estado.DataSource = CreateDataSource();
            Endereco_Estado.DataTextField = "Text";
            Endereco_Estado.DataValueField = "Value";
            Endereco_Estado.DataBind();
            Endereco_Estado.SelectedIndex = 0;

            if (IsPostBack ==  false)
            {
                Telefones.DataSource = CreateTelefoneSource();
                Telefones.DataBind();
            }

            btn_adicionar_telefone.Click += (object cSender, EventArgs cE) => {
                AtualizaTelefones();
            };
        }

        private System.Collections.ICollection CreateTelefoneSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DDD", typeof(int)));
            dt.Columns.Add(new DataColumn("Numero", typeof(int)));
            dt.Columns.Add(new DataColumn("Tipo", typeof(string)));

            LTelefones.ForEach(telefone => {
                DataRow row = dt.NewRow();
                row[0] = telefone.Ddd;
                row[1] = telefone.Numero;
                row[2] = telefone.Tipo.Tipo;

                dt.Rows.Add(row);
            });

            return new DataView(dt);
        }

        private System.Collections.ICollection CreateDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Value", typeof(string)));
            dt.Columns.Add(new DataColumn("Text", typeof(string)));

            Estados.ForEach(estado => dt.Rows.Add(CreateRow(value: estado, text:estado, dt: dt)));

            return new DataView(dt);
        }

        private DataRow CreateRow(string value, string text, DataTable dt)
        {
            DataRow row = dt.NewRow();
            row[0] = value;
            row[1] = text;
            return row;
        }

        protected void AtualizaTelefones()
        {
            LTelefones.Add(new CadastroPessoaFisica.Telefone { Ddd = 85, Numero = 65324565, Tipo = new CadastroPessoaFisica.TipoTelefone { Tipo = "Fixo" } });
            Telefones.DataSource = null;
            Telefones.DataBind();
            Telefones.DataSource = CreateTelefoneSource();
            Telefones.DataBind();
        }
    }
}