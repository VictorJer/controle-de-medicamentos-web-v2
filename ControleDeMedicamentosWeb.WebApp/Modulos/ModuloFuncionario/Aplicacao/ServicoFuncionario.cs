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
            
            var novoFuncionario = new Funcionario(cadastrarDto.Nome, cadastrarDto.Telefone, cadastrarDto.Cpf);

            var Result = ValidarEntidade(novoFuncionario);

            if (Result.IsFailed)
                return Result;
            
            repositorioFuncionario.Cadastrar(novoFuncionario);

            return Result.Ok();
        }

        public Result Editar(EditarFuncionarioDto editarDto)
        {
            if (ExisteFuncionaroComMesmoNome(editarDto.Nome, editarDto.Id))
                return Falha(nameof(editarDto.Nome), "Já existe um funcionaro com esse nome.");
            
            if (ExiteFuncionarioComMesmoCpf(editarDto.Cpf, editarDto.Id))
                return Falha(nameof(editarDto.Cpf), "Já existe um funcionario com esse CPF.");

            var FuncionarioAtualizado = new Funcionario(editarDto.Nome, editarDto.Telefone, editarDto.Cpf);

            var Result = ValidarEntidade(FuncionarioAtualizado);

            if (Result.IsFailed)
                return Result;  

            var result = repositorioFuncionario.Editar(editarDto.Id, FuncionarioAtualizado);

            if (!result)
                return  Result.Fail("Funcionario não encontrado.");  

            return Result.Ok();  
        }

        public Result Excluir(Guid id)
        {
            var selec = repositorioFuncionario.SelecionarPorId(id);

            if (selec == null)
                return Result.Fail("Funcionario não encontrado.");
            
            var result = repositorioFuncionario.Excluir(id);

            if (!result)
                return Result.Fail("Funcionario não encontrado.");

            return Result.Ok();
        }

        public List<ListarFuncionarioDto> SelecionarTodos()
        {
            return repositorioFuncionario.SelecionarTodos().Select(f =>
                    new ListarFuncionarioDto(f.Id, f.Nome, f.Telefone, f.Cpf)).ToList();
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