using System.Text.RegularExpressions;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFuncionario.Dominio;

public class Funcionario : EntidadeBase<Funcionario>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public Funcionario() {  }
    public Funcionario(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override void Atualizar(Funcionario entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        Cpf = entidadeAtualizada.Cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
        
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 a 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");
        
        else if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)? ?\d{4,5}-?\d{4}$"))
            erros.Add("O campo \"Telefone\" deve informar um formato válido.");
    
        if (string.IsNullOrWhiteSpace(Cpf))
            erros.Add("O campo \"CPF\" deve ser preenchido.");
        
        else if (!Regex.IsMatch(Cpf, @"^\d{11}$"))
            erros.Add("O campo \"CPF\" deve conter exatamente 11 dígitos.");

        return erros;
    }
}