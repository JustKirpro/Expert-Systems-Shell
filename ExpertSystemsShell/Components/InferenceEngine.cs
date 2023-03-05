using System.Linq;
using System.Windows.Forms;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Forms;

namespace ExpertSystemsShell.Components;

public class InferenceEngine
{
    private readonly KnowledgeBase _knowledgeBase;

    private bool _isCanceled = false;

    public WorkingMemory WorkingMemory { get; private set; } = null!;

    public InferenceEngine(KnowledgeBase knowledgeBase)
    {
        _knowledgeBase = knowledgeBase;
    }

    /// <summary>
    /// Infers variable value if it is possible.
    /// </summary>
    /// <param name="goalVariable"> Goal variable whose value needs to be inferred. </param>
    /// <returns> true if variable value was inferred, false otherwise. </returns>
    public bool InferGoalVariable(Variable goalVariable)
    {
        WorkingMemory = new WorkingMemory(goalVariable);
        _isCanceled = false;
        return InferVariable(WorkingMemory.GoalVariable);
    }

    private bool InferVariable(Variable variable)
    {
        var rules = _knowledgeBase.Rules.Where(r => r.ActionPart.Select(f => f.Variable).Contains(variable)).ToList();

        foreach (var rule in rules)
        {
            var isInferred = ProcessConditionPart(rule);

            if (isInferred)
            {
                WorkingMemory.VariableValues.AddRange(rule.ActionPart);
                WorkingMemory.FiredRules.Add(rule);
                return true;
            }
            
            if (_isCanceled)
            {
                return false;
            }
        }

        return false;
    }

    private bool ProcessConditionPart(Rule rule)
    {
        foreach (var fact in rule.ConditionPart)
        {
            var currentVariable = fact.Variable;
            var storedValue = GetStoredValue(currentVariable);

            if (storedValue is not null)
            {
                if (AreValuesMatch(storedValue, fact.Value))
                {
                    continue;
                }

                return false;
            }

            if (!ProcessVariable(currentVariable))
            {
                return false;
            }

            storedValue = GetStoredValue(currentVariable)!;

            if (!AreValuesMatch(storedValue, fact.Value))
            {
                return false;
            }
        }

        return true;
    }

    private bool ProcessVariable(Variable variable) => variable.Type switch
    {
        VariableType.Requested => RequestVariable(variable),
        VariableType.Inferred => InferVariable(variable),
        _ => InferVariable(variable) || RequestVariable(variable),
    };

    private bool RequestVariable(Variable variable)
    {
        using var form = new QuestionForm(variable);
        var result = form.ShowDialog();

        if (result == DialogResult.OK)
        {
            var value = form.Value!;
            var fact = new Fact(variable, value);
            WorkingMemory.VariableValues.Add(fact);
            return true;
        }

        _isCanceled = true;
        return false;
    }

    private DomainValue? GetStoredValue(Variable variable) => WorkingMemory.GetVariableValue(variable);

    private static bool AreValuesMatch(DomainValue storedValue, DomainValue currentValue) => storedValue == currentValue;
}