using System.Collections.Generic;
using System.Text;

namespace ExpertSystemsShell.Entities;

public class Domain
{
    public string Name { get; init; } = null!;

    public List<string> Values { get; init; } = new List<string>();

    public string FormattedValues
    {
        get
        {
            var stringBuilder = new StringBuilder();

            foreach (var value in Values)
            {
                stringBuilder.Append(value);
                stringBuilder.Append(" / ");
            }

            return stringBuilder.ToString()[..^2];
        }
    }
}