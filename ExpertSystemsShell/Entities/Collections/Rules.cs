using System.Collections;
using System.Collections.Generic;

namespace ExpertSystemsShell.Entities.Collections;

public class Rules : IEnumerable<Rule>
{
    private readonly List<Rule> _rules = new();

    public void Add(Rule rule) => _rules.Add(rule);

    public void Insert(int index, Rule rule) => _rules.Insert(index, rule);

    public void Remove(Rule rule) => _rules.Remove(rule);

    public void RemoveAt(int index) => _rules.RemoveAt(index);

    public IEnumerator<Rule> GetEnumerator() => (_rules as IEnumerable<Rule>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}