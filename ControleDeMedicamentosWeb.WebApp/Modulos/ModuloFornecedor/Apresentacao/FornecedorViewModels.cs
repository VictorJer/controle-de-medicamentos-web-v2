using System.ComponentModel.DataAnnotations;

namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloFornecedor.Apresentacao;

public record ListarFornecedoresViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cnpj
);

public record CadastrarFornecedorViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo \"Telefone\" deve conter exatamente 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CNPJ\" deve ser preenchido.")]
    [RegularExpression(@"^\d{14}$|^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "O campo \"CNPJ\" deve conter 14 digitos.")]
    string Cnpj
);

public record EditarFornecedorViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo \"Nome\" deve conter entre 3 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "O campo \"Telefone\" deve conter exatamente 11 dígitos.")]
    string Telefone,

    [Required(ErrorMessage = "O campo \"CNPJ\" deve ser preenchido.")]
    [RegularExpression(@"^\d{14}$|^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "O campo \"CNPJ\" deve conter 14 digitos.")]
    string Cnpj
);

public record ExcluirFornecedorViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cnpj
);