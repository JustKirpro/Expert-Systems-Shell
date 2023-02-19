namespace ExpertSystemsShell.Entities;

public class Fact
{
    public Variable Variable { get; set; }

    public DomainValue Value { get; set; }

    public string FormattedFact => $"{Variable.Name} = {Value.Value}";

    public Fact(Variable variable, DomainValue value)
    {
        Variable = variable;
        Value = value;
    }
}