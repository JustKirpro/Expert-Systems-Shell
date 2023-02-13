using System.Collections.Generic;
using System.Text;

namespace ExpertSystemsShell.Entities;

public class Domain
{
    public string Name { get; set; } = string.Empty;

    public List<DomainValue> Values { get; set; } = new List<DomainValue>();

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

    public Domain(string name, List<DomainValue> values)
    {
        Name = name;
        Values = values;
    }
}