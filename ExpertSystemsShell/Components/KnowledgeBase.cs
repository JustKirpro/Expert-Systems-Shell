using System.Collections.Generic;
using System.Linq;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Components;

public class KnowledgeBase
{
    public List<Rule> Rules { get; } = new();

    public List<Variable> Variables { get; } = new();

    public List<Domain> Domains { get; } = new();

    #region Rules

    /// <summary>
    /// Generates the next rule name that can be used as a placeholder.
    /// </summary>
    /// <returns> String in the format 'Rule[Integer]'. </returns>
    public string GenerateNextRuleName()
    {
        var number = Rules.Count + 1;

        while (IsRuleNameUsed($"Rule{number}"))
        {
            number++;
        }

        return $"Rule{number}";
    }

    /// <summary>
    /// Checks if there is a rule with the passed name in the knowledge base.
    /// </summary>
    /// <param name="name"> The name of the rule to check. </param>
    /// <returns> True if there is a rule with the passed name, false otherwise. </returns>
    public bool IsRuleNameUsed(string name) => Rules.Any(r => r.Name == name);

    /// <summary>
    /// Gets a list of rules that use the passed variable either in the condition or in the action part.
    /// </summary>
    /// <param name="variable"> Variable, the list of rules with which you want to get. </param>
    /// <returns> List of rules that use passed variable. </returns>
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
    
    #endregion

    #region Variables
    
    /// <summary>
    /// Generates the next variable name that can be used as a placeholder.
    /// </summary>
    /// <returns> String in the format 'Variable[Integer]'. </returns>
    public string GenerateNextVariableName()
    {
        var number = Variables.Count + 1;

        while (IsVariableNameUsed($"Variable{number}"))
        {
            number++;
        }

        return $"Variable{number}";
    }
    
    /// <summary>
    /// Checks if there is a variable with the passed name in the knowledge base.
    /// </summary>
    /// <param name="name"> The name of the variable to check. </param>
    /// <returns> True if there is a rule with the passed name, false otherwise. </returns>
    public bool IsVariableNameUsed(string name) => Variables.Any(v => v.Name == name);
    
    /// <summary>
    /// Checks if the passed variable is used in any rule in the knowledge base.
    /// </summary>
    /// <param name="variable"> Variable whose presence in the rules you want to check. </param>
    /// <returns> True if the passed variable is used any rule in the knowledge base, otherwise false. </returns>
    public bool IsVariableUsed(Variable variable) => Rules.Any(r => r.ConditionPart.Concat(r.ActionPart).Any(f => f.Variable == variable));
    
    /// <summary>
    /// Gets variable by its name.
    /// </summary>
    /// <param name="name"> Name of the variable that you want to get. </param>
    /// <returns> Variable instance, which name property matches the passed name. </returns>
    public Variable GetVariableByName(string name) => Variables.First(v => v.Name == name);

    /// <summary>
    /// Gets a list of variables that can be used as a goal variable.
    /// </summary>
    /// <returns> List of variables whose type either Inferred or InferredRequested. </returns>
    public List<Variable> GetGoalVariables() => Variables.Where(v => v.Type is VariableType.Inferred or VariableType.InferredRequested).ToList();
    
    /// <summary>
    /// Gets a list of variables that use the passed domain.
    /// </summary>
    /// <param name="domain"> Domain, the list of variables with which you want to get. </param>
    /// <returns> List of variables that use the passed domain. </returns>
    public List<Variable> GetVariablesByDomain(Domain domain) => Variables.Where(v => v.Domain == domain).ToList();
    
    #endregion

    #region Domains

    /// <summary>
    /// Generates the next domain name that can be used as a placeholder.
    /// </summary>
    /// <returns> String in the format 'Domain[Integer]'. </returns>
    public string GenerateNextDomainName()
    {
        var number = Domains.Count + 1;

        while (IsDomainNameUsed($"Domain{number}"))
        {
            number++;
        }

        return $"Domain{number}";
    }
    
    /// <summary>
    /// Checks if there is a domain with the passed name in the knowledge base.
    /// </summary>
    /// <param name="name"> The name of the domain to check. </param>
    /// <returns> True if there is a domain with the passed name, false otherwise. </returns>
    public bool IsDomainNameUsed(string name) => Domains.Any(d => d.Name == name);
    
    /// <summary>
    /// Checks if the passed domain is used in any variable in the knowledge base.
    /// </summary>
    /// <param name="domain"> Domain whose presence in the variables you want to check. </param>
    /// <returns> True if the passed domain is used any variable in the knowledge base, otherwise false. </returns>
    public bool IsDomainUsed(Domain domain) => Variables.Any(v => v.Domain == domain);

    /// <summary>
    /// Gets domain by its name.
    /// </summary>
    /// <param name="name"> Name of the domain that you want to get. </param>
    /// <returns> Domain instance, which name property matches the passed name. </returns>
    public Domain GetDomainByName(string name) => Domains.First(d => d.Name == name);
    
    /// <summary>
    /// Checks if the passed domain value is used in any rule in the knowledge base.
    /// </summary>
    /// <param name="domainValue"> Domain value whose presence in the rules you want to check. </param>
    /// <returns> True if the passed domain value is used any rules in the knowledge base, otherwise false. </returns>
    public bool IsDomainValueUsed(DomainValue domainValue) => Rules.Any(r => r.ConditionPart.Concat(r.ActionPart).Any(f => f.Value == domainValue));

    #endregion
}