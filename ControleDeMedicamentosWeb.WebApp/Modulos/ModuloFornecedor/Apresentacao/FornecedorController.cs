using AutoMapper;
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
}