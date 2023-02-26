using System.Collections.Generic;
using System.Linq;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Components;

public class WorkingMemory
{
    public List<Fact> VariableValues { get; } = new();

    public List<Rule> FiredRules { get; } = new();

    public DomainValue? GetVariableValue(Variable variable) => VariableValues.Where(f => f.Variable == variable).Select(f => f.Value).FirstOrDefault();
}