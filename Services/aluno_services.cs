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
}