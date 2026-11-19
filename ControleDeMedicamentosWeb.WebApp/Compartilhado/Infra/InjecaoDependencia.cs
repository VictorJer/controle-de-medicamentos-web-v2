using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });

        services.AddScoped<IRepositorioFornecedor, RepositorioFornecedorEmArquivo>();
    }
}
