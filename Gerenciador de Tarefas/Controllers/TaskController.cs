using System;
using System.IO;
using Gerenciador_de_Tarefas.Model;
using System.Text.Json;

namespace Gerenciador_de_Tarefas.Controllers;

class TaskController
{
    private readonly string filePath = "task.json";

    public List<TaskModel> Tasks { get; private set; }

    public TaskController()
    {
        Tasks = Carregamento();
    }

    private string GetJsonFIlePath (){
       string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

        return Path.Combine(currentDirectory, "task.json");
    }

    private List<TaskModel> Carregamento()
    {
        if (!File.Exists(GetJsonFIlePath()))
        {
            return new List<TaskModel>();
        }

        string json = File.ReadAllText(GetJsonFIlePath());
        return JsonSerializer.Deserialize<List<TaskModel>>(json) ?? new List<TaskModel>();
    }

    private void Salvamento()
    {
        var json = JsonSerializer.Serialize(Tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(GetJsonFIlePath() ,json);
    }

    public void AddTask(string titulo, string descricao)
    {
        int id = Tasks.Count > 0 ? Tasks[^1].Id + 1 : 1;

        TaskModel task = new TaskModel { Id = id, Titulo = titulo, Descricao = descricao };
        Tasks.Add(task);
        Salvamento();
    }

    public List<TaskModel> GetTasks(){
        return Tasks;
    }

    public void MarkCompleted(int id){
        var task = Tasks.Find(t => t.Id == id);

        if (task != null){
            task.Status = true;
            Salvamento();
        }
    }

    public void RemoveTask(int id){
        var task = Tasks.FirstOrDefault(t => t.Id == id);

        Tasks.Remove(task);

        for (int i = 0; i < Tasks.Count; i++){
            Tasks[i].Id = i + 1;
        }

        Salvamento();
    }
}
