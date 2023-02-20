using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell.Components;

public class InferenceEngine
{
    private KnowledgeBase _knowledgeBase;

    public  Variable? GoalVariabe { get; set; }

    public WorkingMemory StartConsultation(KnowledgeBase knowledgeBase)
    {
        _knowledgeBase = knowledgeBase;

        var workingMemory = new WorkingMemory();
        var goalVariable = GetGoalVariable();

        return default!;
    }

    private Variable GetGoalVariable()
    {
        return default!;
    }


}