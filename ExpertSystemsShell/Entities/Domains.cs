using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExpertSystemsShell.Entities;

public class Domains : IEnumerable<Domain>
{
    private readonly List<Domain> _domains = new();

    public List<string> GetNames() => _domains.Select(d => d.Name).ToList();

    public Domain? GetDomainByName(string name)
    {
        foreach (var domain in _domains)
        {
            if (domain.Name == name)
            {
                return domain;
            }
        }

        return null;
    }

    public void Add(Domain domain) => _domains.Add(domain);

    public void Remove(Domain domain) => _domains.Remove(domain);

    public IEnumerator<Domain> GetEnumerator()
    {
        foreach (var domain in _domains)
        {
            yield return domain;
        }

        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}