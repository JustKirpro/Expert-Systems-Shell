using System.Collections.Generic;
using System.Text;

namespace ExpertSystemsShell.Entities;

public class Rule
{
    public string Name { get; set; }

    public string? Reason { get; set; }

    public List<Fact> CondtionPart { get; set; }
    
    public List<Fact> ActionPart { get; set; }

    public Rule(string name, List<Fact> condtionPart, List<Fact> actionPart)
    {
        Name = name;
        CondtionPart = condtionPart;
        ActionPart = actionPart;
    }

    public Rule(string name, string reason, List<Fact> condtionPart, List<Fact> actionPart) : this(name, condtionPart, actionPart) => Reason = reason;

    public Rule() { }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder("ЕСЛИ ");

        for (var i = 0; i < CondtionPart.Count; i++)
        {
            stringBuilder.Append(CondtionPart[i].ToString());

            if (i < CondtionPart.Count - 1)
            {
                stringBuilder.Append(" И ");
            }
        }

        stringBuilder.Append("\r\n");
        stringBuilder.Append("ТО ");
        stringBuilder.Append(ActionPart.ToString());

        return stringBuilder.ToString();
    }
}