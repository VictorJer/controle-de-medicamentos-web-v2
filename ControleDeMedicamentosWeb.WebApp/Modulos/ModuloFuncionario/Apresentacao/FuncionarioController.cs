using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        var vm = new CadastrarFuncionarioViewModel(string.Empty, string.Empty, string.Empty);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Cadstrar(CadastrarFuncionarioDto cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);
        
        var dto = mapper.Map<CadastrarFuncionarioDto>(cadastrarVm);

        var result = servicoFuncionario.Cadastrar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}