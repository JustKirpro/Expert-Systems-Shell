namespace ExpertSystemsShell.Entities;

public class Variable
{
    public string Name { get; set; } = null!;

    public string? Question { get; set; }

    public VariableType Type { get; set; }

    public string FormattedType { get { return GetTypeAsString(); } }

    public Domain Domain { get; set; } = null!;

    private string GetTypeAsString() =>
        Type switch
        {
            VariableType.Requested => "Запрашимаемая",
            VariableType.Inferred => "Выводимая",
            VariableType.RequestedInferred => "Запрашиваемо-выводимая",
            _ => "Неизвестный"
        };

}