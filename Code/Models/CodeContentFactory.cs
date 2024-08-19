using System.Text.Json;
using CodeAPI.Controllers;
using MongoDB.Bson;

namespace CodeAPI.Models;

public class CodeContentFactory: ICodeContentFactory
{
    private readonly List<CodeContent> _collection;
    public CodeContentFactory()
    {
        _collection = JSONHandler.ReadJSONFromFileAsCarList();
    }
    
    public async Task<CodeContent> GetCodeContentById(ObjectId id)
    {
        return _collection.FirstOrDefault(p => p.Id == id);
    }

    public async Task<List<CodeContent>> GetAllCodeContents()
    {
        return _collection;
    }

    public async Task AddCodeContent(CodeContent codeContent)
    {
        DateTimeOffset now = DateTimeOffset.Now;
        DateTime localDateTime = now.LocalDateTime;
        codeContent.Id = ObjectId.GenerateNewId(localDateTime);
        _collection.Add(codeContent);
        // Serialize the updated list of cars to JSON and write it back to the file
        string jsonData = JsonSerializer.Serialize(_collection);
        File.WriteAllText("./Data/CodeData.json", jsonData);
    }

    public async Task UpdateCodeContent(CodeContent codeContent)
    {
        CodeContent? result = _collection.Find(code => code.Id == codeContent.Id);
        result.Title = codeContent.Title;
        result.Description = codeContent.Description;
        result.Picture = codeContent.Picture;
        result.SubCodeContent = codeContent.SubCodeContent;
        
        // Serialize the updated list of cars to JSON and write it back to the file
        string jsonData = JsonSerializer.Serialize(_collection);
        File.WriteAllText("./Data/CodeData.json", jsonData);
    }

    public async Task<bool> DeleteCodeContent(ObjectId id)
    {
        _collection.Remove(_collection.Find(code => code.Id == id));
        // Serialize the updated list of cars to JSON and write it back to the file
        string jsonData = JsonSerializer.Serialize(_collection);
        File.WriteAllText("./Data/CodeData.json", jsonData);
        
        return true;
    }
}