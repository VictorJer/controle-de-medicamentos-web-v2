using System.Text.RegularExpressions;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Dominio;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Dominio;

public sealed class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CartaoSus { get; set; } = string.Empty;
    public string Cpf {get; set; } = string.Empty;

    public Paciente() {  }
    public Paciente(string nome, string telefone, string cartaoSus, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSus = cartaoSus;
        Cpf = cpf;
    }   
    public override void Atualizar(Paciente entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
        CartaoSus = entidadeAtualizada.CartaoSus;
        Cpf = entidadeAtualizada.Cpf;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
        
        else if (Nome.Length < 3)
            erros.Add("O campo \"Nome\" deve ter no minimo 3 caracteres.");
        
        else if (Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter no maximo 100 caracteres.");
        
        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O campo \"Telefone\" deve ser preenchido.");
        
        else if (!Regex.IsMatch(Telefone, @"^\d{11}$"))
            erros.Add("O campo \"Telefone\" deve conter exatamente 11 dígitos.");
        
        if (string.IsNullOrWhiteSpace(Cpf))
            erros.Add("O campo \"CPF\" deve ser preenchido.");
        
        else if (Cpf.Length != 11)
            erros.Add("O campo \"CPF\" deve conter 11 digitos.");
        
        return erros;
    }
}