using System.Collections.Generic;
using System.Security.Principal;
using trabalho_PI.Models;

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

   public void AtualizarDisciplina(int IdDisciplina, int idAluno, string novoNome, string novaDescricao)
    {
        foreach (Disciplina disciplina in disciplinas)
        {
            if (disciplina.IdDisciplina == IdDisciplina && disciplina.IdAluno == idAluno)
            {
                disciplina.Nome = novoNome;
                disciplina.Descricao = novaDescricao;
                Console.WriteLine("Disciplina atualizada com sucesso!");
                return;
            }
        }
        Console.WriteLine("Disciplina não encontrada.");
    }

    public void ExcluirDisciplina(int idDisciplina, int idAluno)
    {
    Disciplina? disciplinaEncontrada = null;
    foreach (Disciplina disciplina in disciplinas)
    {
        if (disciplina.IdDisciplina == idDisciplina &&
            disciplina.IdAluno == idAluno)
        {
            disciplinaEncontrada = disciplina;
            break;
        }
    }
    if (disciplinaEncontrada != null)
    {
        disciplinas.Remove(disciplinaEncontrada);
        Console.WriteLine("Disciplina excluída com sucesso!");
    }
    else
    {
        Console.WriteLine("Disciplina não encontrada.");
    }
    }
}

