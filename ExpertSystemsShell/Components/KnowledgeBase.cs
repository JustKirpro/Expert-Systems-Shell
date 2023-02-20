using System.Collections.Generic;
using System.Linq;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Components;

public class KnowledgeBase
{
    public List<Rule> Rules { get; } = new();

    public List<Variable> Variables { get; } = new();

    public List<Domain> Domains { get; } = new();

    public List<Variable> GetGoalVariables() => Variables.Where(varible => varible.Type is VariableType.Inferred or VariableType.RequestedInferred).ToList();
    
    public Variable GetVariableByName(string name) => Variables.First(variable => variable.Name == name);

    public bool IsVariableUsed(Variable variable) => Rules.Any(rule => rule.ConditionPart.Concat(rule.ActionPart)
        .Any(fact => fact.Variable == variable));

    public bool IsVariableNameUsed(string name) => Variables.Any(variable => variable.Name == name);

    public Domain GetDomainByName(string name) => Domains.First(domain => domain.Name == name);

    public string GetNextDomainName() => $"Domain{Domains.Count + 1}";
    
    public bool IsDomainUsed(Domain domain) => Variables.Any(variable => variable.Domain == domain);

    public bool IsDomainNameUsed(string name) => Domains.Any(domain => domain.Name == name);

    public bool IsDomainValueUsed(DomainValue domainValue) => Rules.Any(rule => rule.ConditionPart.Concat(rule.ActionPart)
        .Any(fact => fact.Value == domainValue));
}