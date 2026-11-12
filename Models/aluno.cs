public class Aluno
{
    public int IdAluno { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public DateTime DataCadastro { get; set; }
    public string Status { get; set; }
    public Aluno(string nome, string email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        DataCadastro = DateTime.Now;
        Status = "Ativo";
    }
}