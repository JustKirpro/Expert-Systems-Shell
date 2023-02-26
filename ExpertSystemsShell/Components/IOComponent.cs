using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace ExpertSystemsShell.Components;

public class IOComponent
{
    private readonly JsonSerializerSettings _settings = new()
    {
        PreserveReferencesHandling = PreserveReferencesHandling.Objects
    };

    public async Task<KnowledgeBase> LoadKnowledgeBase(string path)
    {
        var json = await File.ReadAllTextAsync(path);
        return JsonConvert.DeserializeObject<KnowledgeBase>(json, _settings)!;
    }

    public async Task SaveKnowledgeBase(KnowledgeBase knowledgeBase, string path)
    {
        var json = JsonConvert.SerializeObject(knowledgeBase, Formatting.Indented, _settings);
        await File.WriteAllTextAsync(path, json);
    }
}