namespace ExpertSystemsShell.Entities;

public class Fact
{
    public Variable Variable { get; set; } = null!;

    public string Value { get; set; } = null!;

    public override string ToString()
    {
        return $"{Variable.Name} = {Value}";
    }
}