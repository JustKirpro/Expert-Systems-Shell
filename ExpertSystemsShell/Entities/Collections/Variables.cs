using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExpertSystemsShell.Entities.Collections;

public class Variables : IEnumerable<Variable>
{
    private readonly List<Variable> _variables = new();

    public void Add(Variable variable) => _variables.Add(variable);

    public void Remove(Variable variable) => _variables.Remove(variable);

    public List<string> GetNames() => _variables.Select(v => v.Name).ToList();

    public Variable? GetByName(string name) => _variables.FirstOrDefault(v => v.Name == name);

    public IEnumerator<Variable> GetEnumerator() => (_variables as IEnumerable<Variable>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}