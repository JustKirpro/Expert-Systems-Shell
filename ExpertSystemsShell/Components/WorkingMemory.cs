using System.Collections.Generic;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Components;

public class WorkingMemory
{
    public Dictionary<Variable, DomainValue> VariableValues { get; } = new();

    public List<Rule> FiredRules { get; } = new();

    public Variable GoalVariable { get; }

    public WorkingMemory(Variable goalVariable) => GoalVariable = goalVariable;

    /// <summary>
    /// Finds variable value.
    /// </summary>
    /// <param name="variable"> Variable whose value needs to be found. </param>
    /// <returns> Value of variable if it is stored in the working memory, otherwise null. </returns>
    public DomainValue? GetVariableValue(Variable variable) => VariableValues.TryGetValue(variable, out var value) ? value : null;
}