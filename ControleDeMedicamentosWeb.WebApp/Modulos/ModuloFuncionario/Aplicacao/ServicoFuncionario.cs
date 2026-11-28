using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao
{
    public class ServicoFuncionario
    {
        private readonly IRepositorioFuncionario repositorioFuncionario;

        public ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario)
        {
            this.repositorioFuncionario = repositorioFuncionario;
        }

    }
}