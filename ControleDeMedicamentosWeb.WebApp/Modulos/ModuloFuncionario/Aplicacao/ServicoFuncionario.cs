using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Aplicacao
{
    public class ServicoFuncionario
    {
        private readonly IRepositorioFuncionario repositorioFuncionario;

        public ServicoFuncionario(IRepositorioFuncionario repositorioFuncionario)
        {
            this.repositorioFuncionario = repositorioFuncionario;
        }

        public Result Cadastrar(CadastrarFuncionarioDto cadastrarDto)
        {
            if (ExisteFuncionaroComMesmoNome(cadastrarDto.Nome))
                return Falha(nameof(cadastrarDto.Nome), "Já existe um funcionaro com esse nome.");
            
            if (ExiteFuncionarioComMesmoCpf(cadastrarDto.Cpf))
                return Falha(nameof(cadastrarDto.Cpf), "Já existe um funcionario com esse CPF.");
        }

        private bool ExiteFuncionarioComMesmoCpf(string cpf, Guid? idIguinorado = null)
        {
            return repositorioFuncionario.SelecionarTodos()
                .Any(f =>
                    f.Id != idIguinorado &&
                    string.Equals(f.Cpf, cpf, StringComparison.OrdinalIgnoreCase));
        }

        private bool ExisteFuncionaroComMesmoNome(string nome, Guid? idIguinorado = null)
        {
            return repositorioFuncionario.SelecionarTodos()
                .Any(f =>
                    f.Id != idIguinorado &&
                    string.Equals(f.Nome, nome, StringComparison.OrdinalIgnoreCase));
        }

        private static Result Falha(string campo, string mensagem)
        {
            return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
        }
        private Result ValidarEntidade(Funcionario funcionario)
        {
            List<string> erros = funcionario.Validar();

            if (erros.Count == 0)
                return Result.Ok();

            return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
        }
    }
}