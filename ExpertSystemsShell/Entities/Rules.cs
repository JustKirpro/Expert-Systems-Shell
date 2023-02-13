using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExpertSystemsShell.Entities;

public class Rules :IEnumerable<Rule>
{
    private readonly List<Rule> _rules = new();

    public void Add(Rule rule) => _rules.Add(rule);

    public void Remove(Rule rule) => _rules.Remove(rule);

    public List<string> GetNames() => _rules.Select(r => r.Name).ToList();

    public Rule? GetByName(string name) => _rules.FirstOrDefault(r => r.Name == name);

    public IEnumerator<Rule> GetEnumerator() => (_rules as IEnumerable<Rule>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
