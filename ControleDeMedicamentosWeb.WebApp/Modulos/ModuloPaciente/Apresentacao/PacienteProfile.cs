using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public class PacienteProfile : Profile
{
    PacienteProfile()
    {
        CreateMap<ListarPacienteDto, ListarPacienteViewModel>();
        CreateMap<
    }
}