using ExpertSystemsShell.Components;
using ExpertSystemsShell.Entities;
using ExpertSystemsShell.Forms;

namespace ExpertSystemsShell;

public class ExpertSystemShell
{
    private readonly IoComponent _ioComponent = new();

    private WorkingMemory? _workingMemory;

    public KnowledgeBase KnowledgeBase { get; private set; } = new();

    public bool IsExplanationAvailable => _workingMemory is not null;

    /// <summary> 
    /// Loads the knowledge base from a file at the given path.
    /// </summary>
    /// <param name="path"> Path to the knowledge base file. </param>
    /// <exception cref="IOException"></exception>
    public void LoadKnowledgeBase(string path) => KnowledgeBase = _ioComponent.LoadKnowledgeBase(path);

    /// <summary>
    /// Saves the knowledge base to a file at the given path.
    /// </summary>
    /// <param name="path"> Path to the knowledge base file where the knowledge base will be saved. </param>
    /// <exception cref="IOException"></exception>
    public void SaveKnowledgeBase(string path) => _ioComponent.SaveKnowledgeBase(KnowledgeBase, path);

    /// <summary>
    /// Saves the knowledge base to a file at the saved path.
    /// Path is saved after using <see cref="SaveKnowledgeBase(string)"> overloaded method. </see>.
    /// If path was not set do nothing.
    /// </summary>
    /// <exception cref="IOException"></exception>
    public void SaveKnowledgeBase() => _ioComponent.SaveKnowledgeBase(KnowledgeBase);

    /// <summary>
    /// Infers variable value if it is possible.
    /// </summary>
    /// <param name="goalVariable"> Goal variable whose value needs to be inferred. </param>
    /// <returns> Fact containing goal variable and its value if it was inferred, null otherwise. </returns>    
    public Fact? InferVariable(Variable goalVariable)
    {
        var inferenceEngine = new InferenceEngine(KnowledgeBase);
        var isInferred = inferenceEngine.InferGoalVariable(goalVariable);

        if (!isInferred)
        {
            return null;
        }

        _workingMemory = inferenceEngine.WorkingMemory;
        var value = _workingMemory.GetVariableValue(goalVariable)!;
        return new Fact(goalVariable, value);
    }

    /// <summary>
    /// If Working Memory is set, shows Explanation Form, otherwise do nothing.
    /// </summary>
    public void ShowExplanation()
    {
        if (_workingMemory is null)
        {
            return;
        }

        using var explanationForm = new ExplanationForm(_workingMemory);
        explanationForm.ShowDialog();
    }
}