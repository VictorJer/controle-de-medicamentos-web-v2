namespace ControleDeMedicamentosWeb.WebApp.Modulos.ModuloPaciente.Aplicacao;

public record ListarPacienteDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);
public record CadastrarPacienteDto(
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record EditarPacienteDto(
    Guid Id,
    string Telefone,
    string CartaoSus,
    string Cpf
);

public record DetalhesPacienteDto(
    Guid Id,
    string Nome,
    string Telefone,
    string CartaoSus,
    string Cpf
);