using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public class FornecedorController : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        

        return View(listarVm);
    }
}