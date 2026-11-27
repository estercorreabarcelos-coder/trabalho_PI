using System.Collections.Generic;
using System.Security.Principal;
using MySql.Data.MySqlClient;
using trabalho_PI.Database;
using trabalho_PI.Models;

public class DisciplinaService
{
    private Conexao conexao = new Conexao();
    private List<Disciplina> disciplinas = new List<Disciplina>();
    public void CadastrarDisciplina(string nome, string descricao, int idAluno)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"INSERT INTO disciplinas
                            (nome, descricao, aluno_id)
                            VALUES
                            (@nome, @descricao, @alunoId)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@descricao", descricao);
                    cmd.Parameters.AddWithValue("@alunoId", idAluno);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Disciplina cadastrada no MySQL com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao cadastrar disciplina: " + ex.Message);
        }
    }
    public void ListarDisciplinas(int idAluno)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"SELECT id, nome, descricao
                            FROM disciplinas
                            WHERE aluno_id = @alunoId";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@alunoId", idAluno);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool encontrou = false;

                        while (reader.Read())
                        {
                            encontrou = true;

                            Console.WriteLine("--------------------");
                            Console.WriteLine("ID: " + reader["id"]);
                            Console.WriteLine("Nome: " + reader["nome"]);
                            Console.WriteLine("Descrição: " + reader["descricao"]);
                        }

                        if (!encontrou)
                        {
                            Console.WriteLine("Nenhuma disciplina encontrada.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao listar disciplinas: " + ex.Message);
        }
    }

   public void AtualizarDisciplina(int idDisciplina, int idAluno, string novoNome, string novaDescricao)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"UPDATE disciplinas
                            SET nome = @nome, descricao = @descricao
                            WHERE id = @idDisciplina AND aluno_id = @alunoId";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", novoNome);
                    cmd.Parameters.AddWithValue("@descricao", novaDescricao);
                    cmd.Parameters.AddWithValue("@idDisciplina", idDisciplina);
                    cmd.Parameters.AddWithValue("@alunoId", idAluno);

                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        Console.WriteLine("Disciplina atualizada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Disciplina não encontrada.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao atualizar disciplina: " + ex.Message);
        }
    }

    public void ExcluirDisciplina(int idDisciplina, int idAluno)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"DELETE FROM disciplinas
                            WHERE id = @idDisciplina AND aluno_id = @alunoId";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idDisciplina", idDisciplina);
                    cmd.Parameters.AddWithValue("@alunoId", idAluno);

                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        Console.WriteLine("Disciplina excluída com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Disciplina não encontrada.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao excluir disciplina: " + ex.Message);
        }
    }
}