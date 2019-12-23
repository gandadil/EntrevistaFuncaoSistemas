using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Models;


namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        public ActionResult Index(long Id)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            BoBeneficiario benef = new BoBeneficiario();
            List<Beneficiario> beneficiarios = benef.Listar(Id);
            List<BeneficiarioModel> beneficiariosModel = new List<BeneficiarioModel>();

            foreach (Beneficiario beneficiario in beneficiarios)
            {
                BeneficiarioModel benefModel = new BeneficiarioModel();
                benefModel.CPF = beneficiario.CPF;
                benefModel.Id = beneficiario.Id;
                benefModel.IdCliente = beneficiario.IdCliente;
                benefModel.Nome = beneficiario.Nome;
                beneficiariosModel.Add(benefModel);
            }

            ViewBag.Beneficiarios = beneficiariosModel;
            ViewBag.Beneficiario = new BeneficiarioModel();

            return View();
        }



        [HttpPost]
        public ActionResult Incluir(BeneficiarioModel beneficiario)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (bo.VerificarExistencia(beneficiario.CPF, beneficiario.IdCliente) && beneficiario.Id == 0)
            {
                this.ModelState.AddModelError("CPFJaCadastrado", "CPF já cadastrado");
            }

            if (!this.ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index", "Beneficiario", new { id = beneficiario.IdCliente });
            }
            else
            {
                //Inclusão
                if (beneficiario.Id == 0)
                {
                    beneficiario.Id = bo.Incluir(new Beneficiario()
                    {
                        CPF = beneficiario.CPF,
                        Nome = beneficiario.Nome,
                        IdCliente = beneficiario.IdCliente
                    });
                }
                else //Alteração
                {
                    bo.Alterar(new Beneficiario()
                    {
                        Id = beneficiario.Id,
                        CPF = beneficiario.CPF,
                        Nome = beneficiario.Nome,
                        IdCliente = beneficiario.IdCliente
                    });

                }
                return RedirectToAction("Index","Beneficiario", new { id = beneficiario.IdCliente });
            }
        }

        [HttpPost]
        public ActionResult Excluir(long Id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            bo.Excluir(Id);
            return RedirectToAction("Index/" + Id, "Beneficiario");
        }
    }
}