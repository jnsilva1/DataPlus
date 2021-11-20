using CadastroPessoaFisica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace DataPlusMVCApp.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index(long? cpf = null)
        {
            return View(cpf);
        }

        // GET: Registration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            return View();
        }

        public PartialViewResult PartialViewDetail(long? cpf = null)
        {
            Pessoa pessoa = PessoaDAO.Consulte(cpf: cpf ?? 0) ?? new Pessoa();
            if (pessoa.Endereco == null) pessoa.Endereco = new Endereco();
            if (pessoa.Telefones == null) pessoa.Telefones = new HashSet<Telefone>();
            return PartialView(model: pessoa);
        }

        [HttpPost]
        public JsonResult Save(string pessoaJson)
        {
            try
            {
                //Tentei receber o objeto por parâmetro mas não funcionou, para agilizar o tempo então passei a receber por string para deserializar
                Pessoa pessoaAtualizacao = JsonConvert.DeserializeObject<Pessoa>(pessoaJson),
                    pessoa = PessoaDAO.Consulte(cpf: pessoaAtualizacao.Cpf);//Verifico se existe a pessoa que estou tentando cadastras

                bool sucesso;

                if (pessoa == null)
                {//Utilizo a nova pessoa que já está populada
                    sucesso = PessoaDAO.Insira(p: pessoaAtualizacao);
                }
                else
                {
                    //Atualizo as informações da pessoa aqui
                    pessoa.Nome = pessoaAtualizacao.Nome;
                    if(pessoa.Endereco.Equals(pessoaAtualizacao.Endereco) == false)
                    {
                        pessoa.Endereco = pessoaAtualizacao.Endereco;
                    }
                    pessoa.Telefones = pessoaAtualizacao.Telefones;
                    sucesso = PessoaDAO.Altere(p: pessoa);
                }
                if(sucesso)
                    return Json(data: new { type="success", text="Alterações gravadas com sucesso!"}, behavior: JsonRequestBehavior.AllowGet);
                else
                    return Json(data: new { type="warning", text="Ocorreu um erro inesperado ao gravar as alterações."}, behavior: JsonRequestBehavior.AllowGet);

            }
            catch(Exception e)
            {
                return Json(data: new { type = "error", text = e.Message }, behavior: JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Registration/Delete
        [HttpPost]
        public JsonResult Delete(long cpf)
        {
            try
            {
                Pessoa pessoa = null;
                if ((pessoa = PessoaDAO.Consulte(cpf: cpf)) == null)
                {
                    return Json(data: new { type = "warning", text = "Registro não encontrado!" }, behavior: JsonRequestBehavior.AllowGet);
                }

                if(PessoaDAO.Exclua(p: pessoa))
                    return Json(data: new { type = "success", text = "Registro excluído com sucesso!" }, behavior: JsonRequestBehavior.AllowGet);
                else
                    return Json(data: new { type = "warning", text = "Ocorreu um erro ao tentar excluir o registro." }, behavior: JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(data: new { type = "error", text = e.Message }, behavior: JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Find(long cpf)
        {
            return Json(PessoaDAO.Consulte(cpf) ?? new Pessoa { Cpf = cpf }, JsonRequestBehavior.AllowGet);
        }

        public static List<string> GetStates()
        => new List<string>
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

        protected override JsonResult Json(object data, string contentType,
            Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue
            };
        }

    }
}
