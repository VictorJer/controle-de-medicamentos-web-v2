using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Dominio;

public sealed class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;

    public Fornecedor() {  }
    public Fornecedor(string nome, string telefone, string cnpj)
    {
        Nome = nome;
        Telefone = telefone;
        Cnpj = cnpj;
    }

    public override void Atualizar(Fornecedor entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        Cnpj = entidadeAtualizada.Cnpj;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido>");
    
        else if (Nome.Length < 3)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");
    
        else if (Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter no máximo 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");

        else if (!Regex.IsMatch(Telefone, @"^\d{11}$"))
            erros.Add("O campo \"Telefone\" deve conter exatamente 11 dígitos.");
    
        if (string.IsNullOrWhiteSpace(Cnpj))
            erros.Add("O campo \"CNPJ\" deve ser preenchido.");

        else if (!CnpjValido(Cnpj))
            erros.Add("O campo \"CNPJ\" deve conter 14 dígitos.");

        return erros;
    }

    #region Validacao Cnpj
    private static bool CnpjValido(string cnpj)
    {
        var cnpjSemMascara = cnpj
            .Replace(".", string.Empty)
            .Replace("/", string.Empty)
            .Replace("-", string.Empty);

        return cnpjSemMascara.Length == 14
            && cnpjSemMascara.All(char.IsDigit);
    }
    #endregion
}