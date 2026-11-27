using trabalho_PI.Database;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


public class TarefaServices
{
    private List<Tarefa> tarefas = new List<Tarefa>();

    private Conexao conexao = new Conexao();

    public void CadastrarTarefa(
        string titulo,
        string descricao,
        string tipo,
        DateTime prazoEntrega,
        int idDisciplina,
        int idAluno)
        {
            try
            {
                using (MySqlConnection conn = conexao.Conectar())
                {
                    conn.Open();

                    string sql = @"INSERT INTO tarefas
                        (titulo, descricao, tipo, data_entrega, status, aluno_id, disciplina_id)
                        SELECT @titulo, @descricao, @tipo, @prazo, 'Pendente', @alunoId, @disciplinaId
                        FROM disciplinas
                        WHERE id = @disciplinaId AND aluno_id = @alunoId";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@titulo", titulo);
                        cmd.Parameters.AddWithValue("@descricao", descricao);
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@prazo", prazoEntrega.Date);
                        cmd.Parameters.AddWithValue("@alunoId", idAluno);
                        cmd.Parameters.AddWithValue("@disciplinaId", idDisciplina);

                        int resultado = cmd.ExecuteNonQuery();

                        if (resultado > 0)
                        {
                            Tarefa novaTarefa = new Tarefa(
                                titulo, descricao, tipo, prazoEntrega, idDisciplina
                            );

                            novaTarefa.IdTarefa = (int)cmd.LastInsertedId;
                            novaTarefa.IdAluno = idAluno;

                            tarefas.Add(novaTarefa);

                            Console.WriteLine("Tarefa cadastrada com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Disciplina não encontrada ou não pertence a você.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar tarefa: " + ex.Message);
            }
        }
        
    public void ListarTarefas(int idDisciplina, int idAluno)
    {
        tarefas.Clear();
        bool encontrada = false;

        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"SELECT id, titulo, descricao, tipo,
                                    data_entrega, status, aluno_id
                            FROM tarefas
                            WHERE disciplina_id = @disciplinaId
                            AND aluno_id = @alunoId";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@disciplinaId", idDisciplina);
                    cmd.Parameters.AddWithValue("@alunoId", idAluno);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tarefa tarefa = new Tarefa(
                                reader["titulo"].ToString() ?? "",
                                reader["descricao"].ToString() ?? "",
                                reader["tipo"].ToString() ?? "",
                                Convert.ToDateTime(reader["data_entrega"]),
                                idDisciplina
                            );

                            tarefa.IdTarefa = Convert.ToInt32(reader["id"]);
                            tarefa.IdAluno = Convert.ToInt32(reader["aluno_id"]);
                            tarefa.Status = reader["status"].ToString() ?? "Pendente";

                            tarefas.Add(tarefa);
                            encontrada = true;

                            Console.WriteLine("--------------------");
                            Console.WriteLine("ID: " + tarefa.IdTarefa);
                            Console.WriteLine("Título: " + tarefa.Titulo);
                            Console.WriteLine("Descrição: " + tarefa.Descricao);
                            Console.WriteLine("Tipo: " + tarefa.Tipo);
                            Console.WriteLine("Prazo de Entrega: " + tarefa.PrazoEntrega.ToShortDateString());
                            Console.WriteLine("Status: " + tarefa.Status);
                        }
                    }
                }
            }

            if (!encontrada)
            {
                Console.WriteLine("Nenhuma tarefa encontrada.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao listar tarefas: " + ex.Message);
        }
    }
    public void AtualizarTarefa(
    int idTarefa,
    int idDisciplina,
    int idAluno,
    string novoTitulo,
    string novaDescricao,
    string novoTipo,
    DateTime novoPrazoEntrega,
    string novoStatus)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"UPDATE tarefas
                            SET titulo = @titulo,
                                descricao = @descricao,
                                tipo = @tipo,
                                data_entrega = @prazo,
                                status = @status
                            WHERE id = @idTarefa
                            AND disciplina_id = @idDisciplina
                            AND aluno_id = @idAluno";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@titulo", novoTitulo);
                    cmd.Parameters.AddWithValue("@descricao", novoDescricao);
                    cmd.Parameters.AddWithValue("@tipo", novoTipo);
                    cmd.Parameters.AddWithValue("@prazo", novoPrazoEntrega.Date);
                    cmd.Parameters.AddWithValue("@status", novoStatus);
                    cmd.Parameters.AddWithValue("@idTarefa", idTarefa);
                    cmd.Parameters.AddWithValue("@idDisciplina", idDisciplina);
                    cmd.Parameters.AddWithValue("@idAluno", idAluno);

                    int resultado = cmd.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        Console.WriteLine("Tarefa atualizada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Tarefa não encontrada ou não pertence a você.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao atualizar tarefa: " + ex.Message);
        }
    }

    public void ExcluirTarefa(int idTarefa, int idDisciplina, int idAluno)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"DELETE FROM tarefas
                            WHERE id = @idTarefa
                            AND disciplina_id = @idDisciplina
                            AND aluno_id = @idAluno";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idTarefa", idTarefa);
                    cmd.Parameters.AddWithValue("@idDisciplina", idDisciplina);
                    cmd.Parameters.AddWithValue("@idAluno", idAluno);

                    int resultado = cmd.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        tarefas.RemoveAll(t =>
                            t.IdTarefa == idTarefa &&
                            t.IdDisciplina == idDisciplina &&
                            t.IdAluno == idAluno
                        );

                        Console.WriteLine("Tarefa excluída com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Tarefa não encontrada ou não pertence a você.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao excluir tarefa: " + ex.Message);
        }
    }    
    public void ConcluirTarefa(int idTarefa, int idDisciplina, int idAluno)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"UPDATE tarefas
                            SET status = 'Concluída'
                            WHERE id = @idTarefa
                            AND disciplina_id = @idDisciplina
                            AND aluno_id = @idAluno";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idTarefa", idTarefa);
                    cmd.Parameters.AddWithValue("@idDisciplina", idDisciplina);
                    cmd.Parameters.AddWithValue("@idAluno", idAluno);

                    int resultado = cmd.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        foreach (Tarefa tarefa in tarefas)
                        {
                            if (tarefa.IdTarefa == idTarefa &&
                                tarefa.IdDisciplina == idDisciplina &&
                                tarefa.IdAluno == idAluno)
                            {
                                tarefa.Status = "Concluída";
                            }
                        }

                        Console.WriteLine("Tarefa concluída com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Tarefa não encontrada ou não pertence a você.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao concluir tarefa: " + ex.Message);
        }
    }
    public void ExibirPainel(int idAluno)
    {
        int pendentes = 0;
        int concluidas = 0;
        int atrasadas = 0;
        int proximasDoPrazo = 0;

        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"SELECT status, data_entrega
                            FROM tarefas
                            WHERE aluno_id = @alunoId";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@alunoId", idAluno);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string status = reader["status"].ToString() ?? "";
                            DateTime prazo = Convert.ToDateTime(reader["data_entrega"]);

                            if (status == "Concluída" || status == "Concluida")
                            {
                                concluidas++;
                            }
                            else if (prazo.Date < DateTime.Today)
                            {
                                atrasadas++;
                            }
                            else if ((prazo.Date - DateTime.Today).Days <= 3)
                            {
                                proximasDoPrazo++;
                            }
                            else
                            {
                                pendentes++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("\n------ PAINEL DE TAREFAS ------");
            Console.WriteLine("Pendentes: " + pendentes);
            Console.WriteLine("Concluídas: " + concluidas);
            Console.WriteLine("Próximas do prazo: " + proximasDoPrazo);
            Console.WriteLine("Atrasadas: " + atrasadas);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao exibir painel: " + ex.Message);
        }
    }
    
    public void ListarTarefasPorStatus(int idAluno)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"SELECT titulo, data_entrega, status
                            FROM tarefas
                            WHERE aluno_id = @alunoId";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@alunoId", idAluno);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n===== TAREFAS ATRASADAS =====");

                        bool encontrouAtrasada = false;

                        while (reader.Read())
                        {
                            string titulo = reader["titulo"].ToString() ?? "";
                            string status = reader["status"].ToString() ?? "";
                            DateTime prazo = Convert.ToDateTime(reader["data_entrega"]);

                            if (status != "Concluída" &&
                                status != "Concluida" &&
                                prazo.Date < DateTime.Today)
                            {
                                Console.WriteLine("Título: " + titulo);
                                Console.WriteLine("Prazo: " + prazo.ToShortDateString());
                                Console.WriteLine("--------------------");
                                encontrouAtrasada = true;
                            }
                        }

                        if (!encontrouAtrasada)
                            Console.WriteLine("Nenhuma tarefa atrasada.");

                        reader.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao listar tarefas: " + ex.Message);
        }
    }
}