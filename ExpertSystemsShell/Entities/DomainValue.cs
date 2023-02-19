namespace ExpertSystemsShell.Entities;

public class DomainValue
{
    public string Value { get; set; }

    public bool IsUsed { get; set; }

    public DomainValue(string value, bool isUsed = false)
    {
        Value = value;
        IsUsed = isUsed;
    }
}