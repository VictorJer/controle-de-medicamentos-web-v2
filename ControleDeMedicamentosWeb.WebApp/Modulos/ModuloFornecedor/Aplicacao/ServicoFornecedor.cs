using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;
using ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;
using FluentResults;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Aplicacao;

public class ServicoFornecedor
{
    private readonly IRepositorioFornecedor repositorioFornecedor;

    public ServicoFornecedor(IRepositorioFornecedor repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public Result Cadastrar(CadastrarFornecedorDto cadastrarDto)
    {
        if (ExisteFornecedorComMesmoNome(cadastrarDto.Nome))
            return Falha(nameof(cadastrarDto.Nome), "Já existe um \"Fornecedor\" com o mesmo nome.");
        
        if (ExisteFornecedorComMesmoCnpj(cadastrarDto.Cnpj))
            return Falha(nameof(cadastrarDto.Cnpj), "Já existe esse \"CNPJ\".");

        Fornecedor novoFornecedor = new Fornecedor(
            cadastrarDto.Nome,
            cadastrarDto.Telefone,
            cadastrarDto.Cnpj
        );

        Result resultValidacao = ValidarEntidade(novoFornecedor);

        if (resultValidacao.IsFailed)
            return resultValidacao;
        
        repositorioFornecedor.Cadastrar(novoFornecedor);

        return Result.Ok();
    }

    public Result Editar(EditarFornecedorDto editarDto)
    {
        if (ExisteFornecedorComMesmoNome(editarDto.Nome))
            return Falha(nameof(editarDto.Nome), "Já existe um \"Fornecedor\" com o mesmo nome.");
        
        if (ExisteFornecedorComMesmoCnpj(editarDto.Cnpj))
            return Falha(nameof(editarDto.Cnpj), "Já existe esse \"CNPJ\".");

        Fornecedor fornecedorAtualizado = new Fornecedor(
            editarDto.Nome,
            editarDto.Telefone,
            editarDto.Cnpj
        );

        Result resultValidacao = ValidarEntidade(fornecedorAtualizado);

        if (resultValidacao.IsFailed)
            return resultValidacao;
        
        var conseguioEditar = repositorioFornecedor.Editar(editarDto.Id, fornecedorAtualizado);

        if (!conseguioEditar)
            return Result.Fail("Fornecedor não encontrado");
        
        return Result.Ok();
    }

    

    private Result ValidarEntidade(Fornecedor fornecedor)
    {
            List<string> erros = fornecedor.Validar();

            if (erros.Count == 0)
                return Result.Ok();

            return Result.Fail(new Error(erros.First()).WithMetadata("Campo", string.Empty));
    }

    private bool ExisteFornecedorComMesmoCnpj(string cnpj, Guid? idIgnorado = null)
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                string.Equals(f.Cnpj, cnpj, StringComparison.Ordinal));
    }
    private bool ExisteFornecedorComMesmoNome(string nome, Guid? idIgnorado = null)
    {
        return repositorioFornecedor
            .SelecionarTodos()
            .Any(f =>
                f.Id != idIgnorado &&
                string.Equals(f.Nome, nome, StringComparison.OrdinalIgnoreCase)
            );
    }
    private static Result Falha(string campo, string mensagem)
    {
        return Result.Fail(new Error(mensagem).WithMetadata("Campo", campo));
    }
}