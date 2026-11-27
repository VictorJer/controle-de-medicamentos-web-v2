namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Apresentacao;

public record ListarPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record CadastrarPacineteViewModel();

public record EditarPacienteViewModel();

public record ExcluirPacienteViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);