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
}