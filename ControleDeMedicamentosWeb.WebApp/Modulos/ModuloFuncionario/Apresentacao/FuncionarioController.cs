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
    public ActionResult Cadastrar(CadastrarFuncionarioViewModel cadastrarVm)
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

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        var result = servicoFuncionario.SelecionarPorId(id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        var vm = mapper.Map<EditarFuncionarioViewModel>(result.Value);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(EditarFuncionarioViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);
        
        var dto = mapper.Map<EditarFuncionarioDto>(editarVm);

        var result = servicoFuncionario.Editar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);

            return View(editarVm);
        }

        return RedirectToAction(nameof(editarVm));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        var result = servicoFuncionario.SelecionarPorId(id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        var vm = mapper.Map<ExcluirFuncionarioViewModel>(result.Value);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFuncionarioViewModel excluirVm)
    {
        var result = servicoFuncionario.Excluir(excluirVm.Id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        return RedirectToAction(nameof(Listar));
    }
}