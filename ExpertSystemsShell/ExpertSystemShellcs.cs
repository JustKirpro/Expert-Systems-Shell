using ExpertSystemsShell.Components;

namespace ExpertSystemsShell;

public class ExpertSystemShell
{
    public KnowledgeBase KnowledgeBase { get; set; } = new();

    private readonly InferenceEngine _inferenceEngine = new();

    private readonly IOComponent _IOComponent = new();

    private readonly ExplanationComponent _explanationComponent = new();

    private WorkingMemory _workingMemory = new();


}