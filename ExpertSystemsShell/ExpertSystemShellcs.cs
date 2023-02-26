using System.Threading.Tasks;
using ExpertSystemsShell.Components;
using ExpertSystemsShell.Entities;

namespace ExpertSystemsShell;

public class ExpertSystemShell
{
    private readonly IOComponent _IOComponent = new();

    private WorkingMemory _workingMemory = new();

    public KnowledgeBase KnowledgeBase { get; private set; } = new();

    public async Task LoadKnowledgeBase(string path)
    {
        KnowledgeBase = await _IOComponent.LoadKnowledgeBase(path);
    }

    public async Task SaveKnowledgeBase(string path)
    {
        await _IOComponent.SaveKnowledgeBase(KnowledgeBase, path);
    }

    public Fact? InferVariable(Variable variable)
    {
        var inferenceEngine = new InferenceEngine(KnowledgeBase);
        var isInferred = inferenceEngine.InferVariable(variable);

        if (isInferred)
        {
            _workingMemory = inferenceEngine.WorkingMemory;
            var value = _workingMemory.GetVariableValue(variable)!;
            return new Fact(variable, value);
        }

        return null;
    }

}