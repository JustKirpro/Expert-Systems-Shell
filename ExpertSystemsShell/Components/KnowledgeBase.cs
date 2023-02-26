using System.Collections.Generic;
using System.Linq;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Components;

public class KnowledgeBase
{
    public List<Rule> Rules { get; } = new();

    public List<Variable> Variables { get; } = new();

    public List<Domain> Domains { get; } = new();

    public string GetNextRuleName() => $"R{Rules.Count + 1}";

    public List<Rule> GetRulesByVariable(Variable variable)
    {
        var rules = new List<Rule>();

        foreach (var rule in Rules)
        {
            rules.AddRange(from fact in rule.ConditionPart where fact.Variable == variable select rule);
            rules.AddRange(from fact in rule.ActionPart where fact.Variable == variable select rule);
        }

        return rules;
    }

    public string GetNextVariableName() => $"Variable{Variables.Count + 1}";

    public Variable GetVariableByName(string name) => Variables.First(variable => variable.Name == name);

    public List<Variable> GetVariablesByDomain(Domain domain) => Variables.Where(variable => variable.Domain == domain).ToList();

    public List<Variable> GetGoalVariables() => Variables.Where(variable => variable.Type is VariableType.Inferred or VariableType.InferredRequested).ToList();

    public bool IsVariableUsed(Variable variable) => Rules.Any(rule => rule.ConditionPart.Concat(rule.ActionPart).Any(fact => fact.Variable == variable));

    public bool IsVariableNameUsed(string name) => Variables.Any(variable => variable.Name == name);

    public string GetNextDomainName() => $"Domain{Domains.Count + 1}";

    public Domain GetDomainByName(string name) => Domains.First(domain => domain.Name == name);
    
    public bool IsDomainUsed(Domain domain) => Variables.Any(variable => variable.Domain == domain);

    public bool IsDomainNameUsed(string name) => Domains.Any(domain => domain.Name == name);

    public bool IsDomainValueUsed(DomainValue domainValue) => Rules.Any(rule => rule.ConditionPart.Concat(rule.ActionPart).Any(fact => fact.Value == domainValue));
}