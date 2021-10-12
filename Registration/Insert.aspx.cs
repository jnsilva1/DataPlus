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
        protected void Page_Load(object sender, EventArgs e)
        {
            Id.Enabled = false;
            Endereco_Estado.DataSource = CreateDataSource();
            Endereco_Estado.DataTextField = "Text";
            Endereco_Estado.DataValueField = "Value";
            Endereco_Estado.DataBind();
            Endereco_Estado.SelectedIndex = 0;
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
    }
}