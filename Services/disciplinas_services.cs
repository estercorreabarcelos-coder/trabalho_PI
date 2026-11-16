using System.Collections.Generic;
public class DisciplinaService
{
   private List<Disciplina> disciplinas = new List<Disciplina>();
   public void CadastrarDisciplina(string nome, string descricao, int idAluno)
   {
       Disciplina novaDisciplina = new Disciplina(nome, descricao, idAluno);
       novaDisciplina.IdDisciplina = disciplinas.Count + 1;
       disciplinas.Add(novaDisciplina);
       Console.WriteLine("Disciplina cadastrada com sucesso!");
   }
   public void ListarDisciplinas(int idAluno)
   {
       bool encontrada = false;
       foreach (Disciplina disciplina in disciplinas)
       {
           if (disciplina.IdAluno == idAluno)
           {
               Console.WriteLine("--------------------");
               Console.WriteLine("ID: " + disciplina.IdDisciplina);
               Console.WriteLine("Nome: " + disciplina.Nome);
               Console.WriteLine("Descrição: " + disciplina.Descricao);
               encontrada = true;
           }
       }
       if (!encontrada)
       {
           Console.WriteLine("Nenhuma disciplina encontrada.");
       }
   }
}