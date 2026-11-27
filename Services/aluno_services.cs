using MySql.Data.MySqlClient;
using trabalho_PI.Database;
using System.Security.Cryptography;

public class AlunoService
{
    private Conexao conexao = new Conexao();
    private List<Aluno> alunos = new List<Aluno>();
    private int proximoId = 1;
    public void CadastrarAluno(string nome, string email, string senha)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string senhaHash = GerarHashSenha(senha);
                Console.WriteLine(senhaHash);

                string sql = @"INSERT INTO alunos
                            (nome, email, senha_hash, data_cadastro, status)
                            VALUES
                            (@nome, @email, @senha, @data, @status)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senhaHash);
                    cmd.Parameters.AddWithValue("@data", DateTime.Now);
                    cmd.Parameters.AddWithValue("@status", "Ativo");

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Aluno cadastrado no MySQL com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao cadastrar aluno: " + ex.Message);
        }
    }
    private string GerarHashSenha(string senha)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            senha,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
        );

        return Convert.ToBase64String(salt) + ":" +
            Convert.ToBase64String(hash);
    }
    private bool VerificarSenha(string senhaDigitada, string hashArmazenado)
    {
        string[] partes = hashArmazenado.Split(':');

        byte[] salt = Convert.FromBase64String(partes[0]);
        byte[] hashOriginal = Convert.FromBase64String(partes[1]);

        byte[] hashDigitado = Rfc2898DeriveBytes.Pbkdf2(
            senhaDigitada,
            salt,
            100000,
            HashAlgorithmName.SHA256,
            32
        );

        return CryptographicOperations.FixedTimeEquals(
            hashOriginal,
            hashDigitado
        );
    }
    public Aluno? Login(string email, string senha)
    {
        try
        {
            using (MySqlConnection conn = conexao.Conectar())
            {
                conn.Open();

                string sql = @"SELECT id, nome, email, senha_hash, data_cadastro, status
                            FROM alunos
                            WHERE email = @email AND status = 'Ativo'
                            LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashArmazenado = reader["senha_hash"].ToString()!;

                            if (VerificarSenha(senha, hashArmazenado))
                            {
                                Aluno aluno = new Aluno(
                                    reader["nome"].ToString()!,
                                    reader["email"].ToString()!,
                                    ""
                                );

                                aluno.IdAluno = Convert.ToInt32(reader["id"]);
                                aluno.DataCadastro = Convert.ToDateTime(reader["data_cadastro"]);
                                aluno.Status = reader["status"].ToString()!;

                                return aluno;
                            }
                        }
                    }
                }
            }

        return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao realizar login: " + ex.Message);
            return null;
        }
    } 
    
    public void ListarAlunos()
    {
    try
    {
        using (MySqlConnection conn = conexao.Conectar())
        {
            conn.Open();

            string sql = @"SELECT id, nome, email, data_cadastro, status
                           FROM alunos";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                bool encontrou = false;

                while (reader.Read())
                {
                    encontrou = true;

                    Console.WriteLine("==============================");
                    Console.WriteLine("ID: " + reader["id"]);
                    Console.WriteLine("Nome: " + reader["nome"]);
                    Console.WriteLine("Email: " + reader["email"]);
                    Console.WriteLine("Data de cadastro: " + reader["data_cadastro"]);
                    Console.WriteLine("Status: " + reader["status"]);
                }

                if (!encontrou)
                {
                    Console.WriteLine("Nenhum aluno cadastrado.");
                }
            }
        }
    }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao listar alunos: " + ex.Message);
        }
    }

    public Aluno? BuscarPorId(int id)
    {
        foreach (Aluno aluno in alunos)
        {
            if (aluno.IdAluno == id)
            {
                return aluno;
            }
        }
        return null;
    }
    public void BuscarPorNome(string nome)
    {
        bool alunoEncontrado = false;

        foreach (Aluno aluno in alunos)
        {
            if (aluno.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("================================");
                Console.WriteLine($"ID: " + aluno.IdAluno);
                Console.WriteLine($"Nome: " + aluno.Nome);
                Console.WriteLine($"Email: " + aluno.Email);
                Console.WriteLine($"Status: " + aluno.Status);
                alunoEncontrado = true;
            }
        }

        if (!alunoEncontrado)
        {
            Console.WriteLine("Nenhum aluno não encontrado.");
        }
    }
        public bool AtualizarAluno(int id, string novoNome, string novoEmail, string novaSenha)
        {
            Aluno? aluno = BuscarPorId(id);
            if (aluno != null)
            {
                aluno.Nome = novoNome;
                aluno.Email = novoEmail;
                aluno.Senha = novaSenha;
                Console.WriteLine("Aluno atualizado com sucesso!");
                return true;
            }
            else
            {
                Console.WriteLine("Aluno não encontrado.");
                return false;
            }
        }

        public void ExcluirAluno(int id)
        {
            Aluno? aluno = BuscarPorId(id);
            if (aluno != null)
            {
                alunos.Remove(aluno);
                Console.WriteLine("Aluno excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Aluno não encontrado.");
            }
        }
    }

