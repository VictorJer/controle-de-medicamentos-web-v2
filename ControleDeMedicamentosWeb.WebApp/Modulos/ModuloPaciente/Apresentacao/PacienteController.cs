using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Extensions;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public class PacienteController(ServicoPaciente servicoPaciente, IMapper mapper) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarPacienteDto> pacientes = servicoPaciente.SelecionarTodos();

        List<ListarPacienteViewModel> vm = mapper.Map<List<ListarPacienteViewModel>>(pacientes);

        return View(vm);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarPacienteViewModel cadastrarVm = new CadastrarPacienteViewModel(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPacienteViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);
        
        CadastrarPacienteDto dto = mapper.Map<CadastrarPacienteDto>(cadastrarVm);

        Result result = servicoPaciente.Cadastrar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}