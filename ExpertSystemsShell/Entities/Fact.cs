namespace ExpertSystemsShell.Entities;

public class Fact
{
    public Variable Variable { get; set; }

    public string Value { get; set; }

    public Fact(Variable variable, string value)
    {
        Variable = variable;
        Value = value;
    }

    public Fact() { }

    public override string ToString()
    {
        return $"{Variable.Name} = {Value}";
    }
}