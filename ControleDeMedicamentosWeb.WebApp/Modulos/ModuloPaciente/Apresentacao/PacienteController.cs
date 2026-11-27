using AutoMapper;
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

    []
}