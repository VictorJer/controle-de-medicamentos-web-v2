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
    
        else if (Nome.Length < 2)
            erros.Add("O campo \"Nome\" deve conter mais que 2 caracteres.");
    
        else if (Nome.Length < 100)
            erros.Add("O campo \"Nome\" deve conter no maximo 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");

        else if (!Regex.IsMatch(
            Telefone,
            @"^\s*(?:\+?55\s*)?(?:\([1-9][0-9]\)|[1-9][0-9])[\s.-]*[0-9]{4,5}[\s.-]*[0-9]{4}\s*$"))
            erros.Add("O campo \"Telefone\" deve conter um telefone brasileiro válido com DDD.");
    
        if (string.IsNullOrWhiteSpace(Cnpj))
            erros.Add("O campo \"CNPJ\" deve ser preenchido.");

        else if (!CnpjValido(Cnpj))
            erros.Add("O campo \"CNPJ\" deve conter 14 dígitos válidos, sem espaços ou hífens.");

        return erros;
    }

    #region Validacao Cnpj
    private static bool CnpjValido(string cnpj)
    {
        if (cnpj.Length != 14 || cnpj.Any(digito => digito < '0' || digito > '9'))
            return false;

        //Esse bloco verifica se todos os caracteres do CNPJ são iguais ao primeiro caractere:
        if (cnpj.All(digito => digito == cnpj[0]))
            return false;

        return CalcularDigitoCnpj(cnpj, 12) == cnpj[12] - '0'
            && CalcularDigitoCnpj(cnpj, 13) == cnpj[13] - '0';
    }

    private static int CalcularDigitoCnpj(string cnpj, int quantidadeDigitos)
    {
        var soma = 0;
        var peso = 2;

        for (var indice = quantidadeDigitos - 1; indice >= 0; indice--)
        {
            soma += (cnpj[indice] - '0') * peso;
            peso = peso == 9 ? 2 : peso + 1;
        }

        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
    #endregion
}