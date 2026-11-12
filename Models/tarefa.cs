public class Tarefa
{
    public int IdTarefa { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Tipo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime PrazoEntrega { get; set; }
    public string Status { get; set; }
    public int IdDisciplina { get; set; }
    public Tarefa(string titulo, string descricao, string tipo, DateTime prazoEntrega, int idDisciplina)
    {
        Titulo = titulo;
        Descricao = descricao;
        Tipo = tipo;
        DataCadastro = DateTime.Now;
        PrazoEntrega = prazoEntrega;
        Status = "Pendente";
    }
}