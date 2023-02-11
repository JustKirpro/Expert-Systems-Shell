using System.Collections.Generic;
using System.Linq;

namespace ExpertSystemsShell.Entities;

public class Variables
{
    private readonly List<Variable> _variables = new();

    public List<string> GetNames() => _variables.Select(v => v.Name).ToList();

    public void Add(Variable variable) => _variables.Add(variable);

    public void Remove(Variable variable) => _variables.Remove(variable);
}