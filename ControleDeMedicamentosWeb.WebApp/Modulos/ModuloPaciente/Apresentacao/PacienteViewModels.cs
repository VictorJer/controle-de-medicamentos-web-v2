using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public record ListarPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record CadastrarPacienteViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter no minimo 3 a 100 digitos.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo \"Telefone\" deve conter exatamente 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"Cartão SUS\" deve ser preenchido.")]
    [StringLength(14, ErrorMessage = "O campo \"Cartão SUS\" deve conter 14 caracteres.")]
    string CartaoSus,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo \"CPF\" deve conter exatamente 11 dígitos.")]
    string Cpf
);


public record EditarPacienteViewModel(
    Guid Id,
    
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter no minimo 3 a 100 digitos.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo \"Telefone\" deve conter exatamente 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"Cartão SUS\" deve ser preenchido.")]
    [StringLength(14, ErrorMessage = "O campo \"Cartão SUS\" deve conter 14 caracteres.")]
    string CartaoSus,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo \"CPF\" deve conter exatamente 11 dígitos.")]
    string Cpf
);

public record ExcluirPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);