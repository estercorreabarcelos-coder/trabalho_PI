public class AlunoService
{
    private List<Aluno> alunos = new List<Aluno>();
    private int proximoId = 1;
    public void CadastrarAluno(string nome, string email, string senha)
    {
        Aluno novoAluno = new Aluno(nome, email, senha);
        alunos.Add(novoAluno);
        proximoId++;

        Console.WriteLine("Aluno cadastrado com sucesso!");
    }
    public void ListarAlunos()
    {
        foreach (Aluno aluno in alunos)
        {
            Console.WriteLine("================================");
            Console.WriteLine($"ID: " + aluno.IdAluno);
            Console.WriteLine($"Nome: " + aluno.Nome);
            Console.WriteLine($"Email: " + aluno.Email);
            Console.WriteLine($"Status: " + aluno.Status);
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
        public void AtualizarAluno(int id, string novoNome, string novoEmail, string novaSenha)
        {
            Aluno? aluno = BuscarPorId(id);
            if (aluno != null)
            {
                aluno.Nome = novoNome;
                aluno.Email = novoEmail;
                aluno.Senha = novaSenha;
                Console.WriteLine("Aluno atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Aluno não encontrado.");
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
