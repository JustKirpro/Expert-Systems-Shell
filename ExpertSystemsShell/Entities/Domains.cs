using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExpertSystemsShell.Entities;

public class Domains : IEnumerable<Domain>
{
    private readonly List<Domain> _domains = new();

    public void Add(Domain domain) => _domains.Add(domain);

    public void Remove(Domain domain) => _domains.Remove(domain);

    public List<string> GetNames() => _domains.Select(d => d.Name).ToList();

    public Domain? GetByName(string name) => _domains.FirstOrDefault(d => d.Name == name);

    public IEnumerator<Domain> GetEnumerator() => (_domains as IEnumerable<Domain>).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}