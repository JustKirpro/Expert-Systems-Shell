using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace ExpertSystemsShell.Components;

public class IoComponent
{
    private string? _path;

    private readonly JsonSerializerSettings _settings = new()
    {
        PreserveReferencesHandling = PreserveReferencesHandling.Objects
    };

    /// <summary> 
    /// Loads the knowledge base from a file at the given path.
    /// </summary>
    /// <param name="path"> Path to the knowledge base file. </param>
    /// <returns> Loaded knowledge base. </returns>
    /// <exception cref="IOException"></exception>
    public KnowledgeBase LoadKnowledgeBase(string path)
    {
        _path = path;

        var formatter = new BinaryFormatter();
        using var stream = new FileStream(path, FileMode.OpenOrCreate);

        try
        {
            var json = (string)formatter.Deserialize(stream);
            return JsonConvert.DeserializeObject<KnowledgeBase>(json, _settings)!;
        }
        catch
        {
            throw new IOException("An error occurred while loading the knowledge base from a file");
        }
    }

    /// <summary>
    /// Saves the knowledge base to a file at the given path.
    /// </summary>
    /// <param name="knowledgeBase"> Knowledge base to be saved to a file. </param>
    /// <param name="path"> Path to the knowledge base file where the knowledge base will be saved. </param>
    /// <exception cref="IOException"></exception>
    public void SaveKnowledgeBase(KnowledgeBase knowledgeBase, string path)
    {
        _path = path;
        SaveKnowledgeBase(knowledgeBase);
    }

    /// <summary>
    /// Saves the knowledge base to a file at the saved path.
    /// Path is saved after using <see cref="SaveKnowledgeBase(KnowledgeBase, string)"> overloaded method. </see>
    /// If path was not set do nothing.
    /// </summary>
    /// <param name="knowledgeBase"> Knowledge base to be saved to a file. </param>
    /// <exception cref="IOException"></exception>
    public void SaveKnowledgeBase(KnowledgeBase knowledgeBase)
    {
        if (_path is null)
        {
            return;
        }
        
        var formatter = new BinaryFormatter();
        using var stream = new FileStream(_path, FileMode.OpenOrCreate);

        try
        {
            var json = JsonConvert.SerializeObject(knowledgeBase, Formatting.Indented, _settings);
            formatter.Serialize(stream, json);
        }
        catch
        {
            throw new IOException("An error occurred while saving the knowledge base to a file");
        }
    }
}