using Gerenciador_de_Tarefas.Controllers;

namespace Gerenciador_de_Tarefas.Views;

class TaskView
{
    private readonly TaskController _controller;

    public TaskView()
    {
        _controller = new TaskController();
    }

    public void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Gerenciador de Tarefas ====");
            Console.WriteLine("1. Adicionar Tarefa");
            Console.WriteLine("2. Listar Tarefas");
            Console.WriteLine("3. Marcar como Concluída");
            Console.WriteLine("4. Remover Tarefa");
            Console.WriteLine("5. Sair");

            switch (Console.ReadLine())
            {
                case "1":
                    AdicionarTask();
                    break;
                case "2":
                    ListrarTasks();
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opção inválida! Pressione ENTER para continuar...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void AdicionarTask()
    {
        Console.Clear();
        Console.WriteLine("---------- Adicionar Tarefa -----------\n");
        Console.WriteLine("Digite o Titulo da tarefa");
        string titulo = Console.ReadLine()!;
        Console.WriteLine("Digite a Descrição da tarefa");
        string descricao = Console.ReadLine()!;

        _controller.AddTask(titulo, descricao); 
        Console.WriteLine("Tarefa adicionada! Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    public void ListrarTasks(){
        Console.Clear();
        Console.WriteLine("---------- Lista de Tarefa -----------\n");
        var tasks = _controller.GetTasks();
        foreach (var task in tasks){
            Console.WriteLine($"ID: {task.Id}");
            Console.WriteLine($"Titulo: {task.Titulo}");
            Console.WriteLine($"Descrição: {task.Descricao}");
            Console.WriteLine($"Status: {(task.Status ? "Completo" : "Pendente")}");
            Console.WriteLine("-------------------------------------------------");
        }
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    public void CompleteTask(){
        Console.WriteLine("---------- Marcar Tarefa Completo -----------\n");
        Console.WriteLine("Digite o Id da tarefa que desaja marca completo");
        if (int.TryParse(Console.ReadLine(), out int id)){
            var task = _controller.GetTasks().Find(t => t.Id == id);

            if (task != null){
                _controller.MarkCompleted(id);
                Console.WriteLine("Tarefa concluída! Pressione ENTER para continuar...");
            }else{
                Console.WriteLine("Erro: Nenhuma tarefa encontrada com esse ID.");
            }
        }else{
            Console.WriteLine("Erro: Entrada inválida! Digite um número válido.");
        }
    }
}
