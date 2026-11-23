public class TarefaServices
{
    private List<Tarefa> tarefas = new List<Tarefa>();

    public void CadastrarTarefa(
        string titulo,
        string descricao,
        string tipo,
        DateTime prazoEntrega,
        int idDisciplina)
    {
        Tarefa novaTarefa = new Tarefa(
            titulo, descricao, 
            tipo, prazoEntrega, 
            idDisciplina
        );
        novaTarefa.IdTarefa = tarefas.Count + 1;
        tarefas.Add(novaTarefa);
        Console.WriteLine("Tarefa cadastrada com sucesso!");
    }
    
    public void ListarTarefas(int idDisciplina)
    {
        bool encontrada = false;
        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.IdDisciplina == idDisciplina)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("ID: " + tarefa.IdTarefa);
                Console.WriteLine("Título: " + tarefa.Titulo);
                Console.WriteLine("Descrição: " + tarefa.Descricao);
                Console.WriteLine("Tipo: " + tarefa.Tipo);;
                Console.WriteLine("Prazo de Entrega: " + tarefa.PrazoEntrega);
                
                string statusExibicao;

                if (tarefa.Status == "Concluida")
                {
                    statusExibicao = "Concluida";
                }
                else if ((tarefa.PrazoEntrega.Date - DateTime.Today).Days <= 1)
                {
                    statusExibicao = "Proxima do prazo";
                }
                else
                {
                    statusExibicao = "No prazo";
                }

                Console.WriteLine("Status: " + statusExibicao);
                
                encontrada = true;
            }
        }
        if (!encontrada)
        {
            Console.WriteLine("Nenhuma tarefa encontrada.");
        }
    }

    public void AtualizarTarefa(
        int idTarefa, 
        int idDisciplina, 
        string novoTitulo, 
        string novaDescricao, 
        string novoTipo, 
        DateTime novoPrazoEntrega, 
        string novoStatus)
    {
        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.IdTarefa == idTarefa && tarefa.IdDisciplina == idDisciplina)
            {
                tarefa.Titulo = novoTitulo;
                tarefa.Descricao = novaDescricao;
                tarefa.Tipo = novoTipo;
                tarefa.PrazoEntrega = novoPrazoEntrega;
                tarefa.Status = novoStatus;
                Console.WriteLine("Tarefa atualizada com sucesso!");
                return;
            }
        }
        Console.WriteLine("Tarefa não encontrada.");
    }

    public void ExcluirTarefa(int idTarefa, int idDisciplina)
    {
        Tarefa? tarefaEncontrada = null;
        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.IdTarefa == idTarefa && tarefa.IdDisciplina == idDisciplina)
            {
                tarefaEncontrada = tarefa;
                break;
            }
        }
        if (tarefaEncontrada != null)
        {
            tarefas.Remove(tarefaEncontrada);
            Console.WriteLine("Tarefa excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
    }

    public void ConcluirTarefa(int idTarefa, int idDisciplina)
    {
        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.IdTarefa == idTarefa && tarefa.IdDisciplina == idDisciplina)
            {
                tarefa.Status = "Concluída";
                Console.WriteLine("Tarefa concluída com sucesso!");
                return;
            }
        }
        Console.WriteLine("Tarefa não encontrada.");
    }
    public void ExibirPainel()
    {
        int pendentes = 0;
        int concluidas = 0;
        int atrasadas = 0;
        int proximasDoPrazo = 0;

        foreach (Tarefa tarefa in tarefas)
        {
            if(tarefa.Status == "Concluida")
            {
                concluidas++;
            }
            else if (tarefa.PrazoEntrega.Date < DateTime.Today)
            {
                atrasadas++;
            }
            else if((tarefa.PrazoEntrega.Date - DateTime.Today).Days <= 3)
            {
                proximasDoPrazo++;
            }
            else
            {
                pendentes++;
            }
        }

        Console.WriteLine("\n------ PAINEL DE TAREFAS ----------");
        Console.WriteLine("Pendentes: " + pendentes);
        Console.WriteLine("Concluidas: " + concluidas);
        Console.WriteLine("Proximas do Prazo: " + proximasDoPrazo);
        Console.WriteLine("Atrasadas: " + atrasadas);
    }

    public void ListarTarefasPorStatus()
    {
        Console.WriteLine("\n ===== TAREFAS ATRASADAS =======");

        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.Status != "Concluida" && tarefa.PrazoEntrega.Date < DateTime.Today)
            {
                Console.WriteLine("Título: " + tarefa.Titulo);
                Console.WriteLine("Prazo: " + tarefa.PrazoEntrega.ToShortDateString());
                Console.WriteLine("========================");
            }
        }

        Console.WriteLine("\n===== PRÓXIMAS DO PRAZO =====");
        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.Status != "Concluída" &&
                tarefa.PrazoEntrega.Date >= DateTime.Today &&
                (tarefa.PrazoEntrega.Date - DateTime.Today).Days <= 3)
            {
                Console.WriteLine("Título: " + tarefa.Titulo);
                Console.WriteLine("Prazo: " + tarefa.PrazoEntrega.ToShortDateString());
                Console.WriteLine("======================");
            }
        }
    }
}