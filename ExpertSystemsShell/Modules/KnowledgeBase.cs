using System.Collections.Generic;
using System.Linq;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Modules;

public class KnowledgeBase
{
    public List<Rule> Rules { get; } = new();

    public List<Variable> Variables { get; } = new();

    public List<Domain> Domains { get; } = new();
    
    public void AddRule(Rule rule) => Rules.Add(rule);

    public void InsertRule(int index, Rule rule) => Rules.Insert(index, rule);

    public void RemoveRule(Rule rule) => Rules.Remove(rule);
    
    public void AddVariable(Variable variable) => Variables.Add(variable);
    
    public void RemoveVariable(Variable variable) => Variables.Remove(variable);
    
    public Variable GetVariableByName(string name) => Variables.First(variable => variable.Name == name);

    public bool IsVariableUsed(Variable variable) => Rules.Any(rule => rule.ConditionPart.Concat(rule.ActionPart)
        .Any(fact => fact.Variable == variable));

    public bool IsVariableNameUsed(string name) => Variables.Any(variable => variable.Name == name);

    public void AddDomain(Domain domain) => Domains.Add(domain);
    
    public void RemoveDomain(Domain domain) => Domains.Remove(domain);

    public Domain GetDomainByName(string name) => Domains.First(domain => domain.Name == name);
    
    public bool IsDomainNameUsed(string name) => Domains.Any(domain => domain.Name == name);
    

    public bool IsDomainUsed(Domain domain) => Variables.Any(variable => variable.Domain == domain);

    public bool IsDomainValueUsed(DomainValue domainValue) => Rules.Any(rule => rule.ConditionPart.Concat(rule.ActionPart)
        .Any(fact => fact.Value == domainValue));
}