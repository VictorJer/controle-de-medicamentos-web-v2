using AutoMapper;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<ListarFornecedorDto, ListarFornecedoresViewModel>();
        CreateMap<CadastrarFornecedorViewModel, CadastrarFornecedorDto>();
        CreateMap<EditarFornecedorViewModel, EditarFornecedorDto>();
        CreateMap<DetalhesFornecedorDto, EditarFornecedorViewModel>();
        CreateMap<DetalhesFornecedorDto, ExcluirFornecedorViewModel>();
    }
}