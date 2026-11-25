using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public class FornecedorController(ServicoFornecedor servicoFornecedor, IMapper mapper) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarFornecedorDto> listarFornecedorDtos = servicoFornecedor.SelecionarTodos();
        List<ListarFornecedoresViewModel> listarVm = mapper.Map<List<ListarFornecedoresViewModel>>(listarFornecedorDtos);

        return View(listarVm);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarFornecedorViewModel cadastrarVm = new CadastrarFornecedorViewModel(
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarFornecedorViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);
        
        CadastrarFornecedorDto dto = mapper.Map<CadastrarFornecedorDto>(cadastrarVm);

        var result = servicoFornecedor.Cadastrar(dto);

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
        var result = servicoFornecedor.SelecionarPorId(id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        var fornecedor = mapper.Map<EditarFornecedorViewModel>(result.Value);

        return View(fornecedor);
    }

    [HttpPost]
    public ActionResult Editar(EditarFornecedorViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var dto = mapper.Map<EditarFornecedorDto>(editarVm);

        var result = servicoFornecedor.Editar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        var result = servicoFornecedor.SelecionarPorId(id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        var vm = mapper.Map<ExcluirFornecedorViewModel>(result.Value);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirFornecedorViewModel excluirVm)
    {
        var result = servicoFornecedor.Excluir(excluirVm.Id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);

            return RedirectToAction(nameof(Listar));
        }

        return RedirectToAction(nameof(Listar));
    }
}