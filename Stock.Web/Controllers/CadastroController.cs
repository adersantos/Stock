using Stock.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stock.Web.Controllers
{

    public class CadastroController : Controller
    {
        private static List<GrupoProdutoModel> _lstGrupoProdutoModel = new List<GrupoProdutoModel>()
        {
            new GrupoProdutoModel {Id=1, Nome="Tinta Suvinil", Ativo=true },
            new GrupoProdutoModel {Id=2, Nome="Tinta Coral", Ativo=false },
            new GrupoProdutoModel {Id=3, Nome="Tinta Shermamm", Ativo=true },
        };

        [Authorize]
        public ActionResult GrupoProduto()
        {
            return View(_lstGrupoProdutoModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ObterGrupoProduto(int id)
        {
            return Json(_lstGrupoProdutoModel.Find(p => p.Id == id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult IncluirGrupoProduto(GrupoProdutoModel model)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(m => m.Errors).Select(n => n.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var retorno = _lstGrupoProdutoModel.Find(p => p.Id == model.Id);
                    if (retorno == null)
                    {
                        retorno = model;
                        retorno.Id = _lstGrupoProdutoModel.Max(p => p.Id) + 1;
                        _lstGrupoProdutoModel.Add(retorno);
                    }
                    else
                    {
                        retorno.Nome = model.Nome;
                        retorno.Ativo = model.Ativo;
                    }
                    idSalvo = retorno.Id.ToString();
                }
                catch (Exception)
                {
                    resultado = "ERRO";
                }
            }

            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo});
        }

        [HttpPost]
        [Authorize]
        public ActionResult ExcluirGrupoProduto(int id)
        {
            var retorno = _lstGrupoProdutoModel.Find(p => p.Id == id);
            if (retorno != null)
            {
                _lstGrupoProdutoModel.Remove(retorno);
            }
            return Json(retorno);
        }

        [Authorize]
        public ActionResult MarcaProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult LocalProduto()
        {
            return View();
        }

        [Authorize]
        public ActionResult UnidadeMedida()
        {
            return View();
        }

        [Authorize]
        public ActionResult Produto()
        {
            return View();
        }

        [Authorize]
        public ActionResult Pais()
        {
            return View();
        }

        [Authorize]
        public ActionResult Estado()
        {
            return View();
        }

        [Authorize]
        public ActionResult Cidade()
        {
            return View();
        }

        [Authorize]
        public ActionResult Fornecedor()
        {
            return View();
        }

        [Authorize]
        public ActionResult PerfilUsuario()
        {
            return View();
        }

        [Authorize]
        public ActionResult Usuario()
        {
            return View();
        }
    }
}