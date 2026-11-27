using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public class ServicoPaciente
{
    private readonly IRepositorioPaciente repositorioPaciente;

    public ServicoPaciente(IRepositorioPaciente repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    public Result Cadastrar(CadastrarPacienteDto cadastrarDto)
    {
        if (ExistePacienteComMesmoNome(cadastrarDto.Nome))
            return Falha(nameof(cadastrarDto.Nome), "Já existe um Fornecedor com o mesmo nome.");
        
        if (ExistePacienteComMesmoCpf(cadastrarDto.Cpf))
            return Falha(nameof(cadastrarDto.Cpf), "Já existe um Paciente com esse CPF.");
        
        if (ExistePacienteComMesmoCartaoSUS(cadastrarDto.CartaoSus))
            return Falha(nameof(cadastrarDto.CartaoSus), "Já existe esse Cartão SUS cadastrado.");
        
        var novoPaciente = new Paciente(
            cadastrarDto.Nome,
            cadastrarDto.Telefone,
            cadastrarDto.CartaoSus,
            cadastrarDto.Cpf
        );

        var result = ValidarEntidade(novoPaciente);

        if (result.IsFailed)
            return result;
        
        repositorioPaciente.Cadastrar(novoPaciente);

        return Result.Ok();
    }

    public Result Editar(EditarPacienteDto editarDto)
    {
        Paciente? pacienteSelecionado = repositorioPaciente.SelecionarPorId(editarDto.Id);

        if (pacienteSelecionado == null)
            return Result.Fail("Paciente não encontrado.");
        
        if (ExistePacienteComMesmoNome(editarDto.Nome, editarDto.Id))
            return Falha(nameof(editarDto.Nome), "Já existe esse \"Nome\" cadastrardo.");
        
        if (ExistePacienteComMesmoCpf(editarDto.Cpf, editarDto.Id))
            return Falha(nameof(editarDto.Cpf), "Já existe esse \"Cpf\" cadastrardo.");
        
        if (ExistePacienteComMesmoCartaoSUS(editarDto.CartaoSus, editarDto.Id))
            return Falha(nameof(editarDto.CartaoSus), "Esse cartão já esta cadastrardo.");
        
        Paciente pacienteNovo = new Paciente(
            editarDto.Nome,
            editarDto.Telefone,
            editarDto.CartaoSus,
            editarDto.Cpf
        );

        Result result = ValidarEntidade(pacienteNovo);

        if (result.IsFailed)
            return result;
        
        var conseguioEditar = repositorioPaciente.Editar(editarDto.Id, pacienteNovo);

        if (!conseguioEditar)
            return Result.Fail("Paciente não encontrado");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Paciente? paciente = repositorioPaciente.SelecionarPorId(id);

        if (paciente == null)
            return Result.Fail("Paciente não encontrado");
        
        repositorioPaciente.Excluir(id);

        return Result.Ok();
    }

    private bool ExistePacienteComMesmoCartaoSUS(string cartaoSus, Guid? idIguinorado = null)
    {
        return repositorioPaciente.SelecionarTodos()
            .Any(p => 
            p.Id != idIguinorado &&
            string.Equals(p.CartaoSus, cartaoSus, StringComparison.OrdinalIgnoreCase));
    }

    private bool ExistePacienteComMesmoCpf(string cpf, Guid? idIguinorado = null)
    {
        return repositorioPaciente.SelecionarTodos()
            .Any(p => 
            p.Id != idIguinorado &&
            string.Equals(p.Cpf, cpf, StringComparison.OrdinalIgnoreCase));
    }

    private bool ExistePacienteComMesmoNome(string nome, Guid? idIguinorado = null)
    {
        return repositorioPaciente.SelecionarTodos()
            .Any(p =>
            p.Id != idIguinorado &&
            string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase));
    }

    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    } 

    private Result ValidarEntidade(Paciente paciente)
    {
            List<string> erros = paciente.Validar();

            if (erros.Count == 0)
                return Result.Ok();

            return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

}