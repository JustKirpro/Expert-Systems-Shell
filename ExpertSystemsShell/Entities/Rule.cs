using System;
using System.Collections.Generic;
using System.Text;

namespace ExpertSystemsShell.Entities;

public class Rule
{
    public string Name { get; set; } = null!;

    public string? Explanation { get; set; }

    public List<Fact> CondtionPart { get; set; } = new List<Fact>();
    
    public Fact ActionPart { get; set; } = null!;

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