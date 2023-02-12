using System.Collections.Generic;
using System.Text;

namespace ExpertSystemsShell.Entities;

public class Domain
{
    public string Name { get; set; } = string.Empty;

    public List<string> Values { get; set; } = new List<string>();

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

    public Domain(string name, List<string> values)
    {
        Name = name;
        Values = values;
    }
}