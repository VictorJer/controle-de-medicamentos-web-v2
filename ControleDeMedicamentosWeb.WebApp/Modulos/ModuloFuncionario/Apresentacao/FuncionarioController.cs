using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Apresentacao;

public class FuncionarioController(ServicoFuncionario servicoFuncionario, IMapper mapper) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        var dto = servicoFuncionario.SelecionarTodos();

        var vm = mapper.Map<ListarFuncionarioViewModel>(dto);

        return View(vm);
    }
}