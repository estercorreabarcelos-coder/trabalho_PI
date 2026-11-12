public class Disciplina
{
    public int IdDisciplina { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int IdAluno { get; set; }
    public Disciplina(string nome, string descricao, int idAluno)
    {
        Nome = nome;
        Descricao = descricao;
        IdAluno = idAluno;
    }
}