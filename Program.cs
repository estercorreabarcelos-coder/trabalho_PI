using MySqlConnector;
AlunoService alunoService = new AlunoService();

DisciplinaService disciplinaService = new DisciplinaService();

TarefaServices tarefaService = new TarefaServices();

int opcao = 0;
bool isFirstTime = true;

Console.Clear();
Console.WriteLine(" ==================================================");
Console.WriteLine(" |  ORGANIZAÊ - ORGANIZADOR DE TAREFAS ESCOLARES  | ");
Console.WriteLine(" ==================================================");
Console.WriteLine("BEM-VINDO AO ORGANIZAÊ, O SEU ORGANIZADOR DE TAREFAS ESCOLARES!");
Console.WriteLine("Aqui você poderá organizar suas tarefas de forma simples e eficiente.");
Console.WriteLine("Vamos começar! Por favor, escolha uma das opções abaixo:");
while (opcao != 18)
{
    if (!isFirstTime)
    {
        Console.WriteLine(" ==================================================");
        Console.WriteLine(" |  ORGANIZAÊ - ORGANIZADOR DE TAREFAS ESCOLARES  | ");
        Console.WriteLine(" ==================================================");
    }
    isFirstTime = false;
    Console.WriteLine("\n==== OPÇÕES PARA ALUNO =====");
    Console.WriteLine("1: Cadastrar Aluno");
    Console.WriteLine("2: Listar Alunos");
    Console.WriteLine("3: Buscar Aluno por ID");
    Console.WriteLine("4: Buscar Aluno por Nome");
    Console.WriteLine("5: Atualizar Aluno");
    Console.WriteLine("6: Excluir Aluno");
    Console.WriteLine("\n ==== OPÇÕES PARA DISCIPLINA =====");
    Console.WriteLine("7: Cadastrar Disciplina");
    Console.WriteLine("8: Listar Disciplinas");
    Console.WriteLine("9: Atualizar Disciplina");
    Console.WriteLine("10: Excluir Disciplina");
    Console.WriteLine("\n ==== OPÇÕES ARA TAREFA ====");
    Console.WriteLine("11: Cadastrar Tarefa");
    Console.WriteLine("12: Listar Tarefas");
    Console.WriteLine("13: Atualizar Tarefa");
    Console.WriteLine("14: Excluir Tarefa");
    Console.WriteLine("15: Concluir Tarefa");
    Console.WriteLine("16: Painel de tarefas");
    Console.WriteLine("17: Listar tarefas por situação");
    Console.WriteLine("\n18: Sair");
    Console.Write("Escolha uma opção: ");


    if (!int.TryParse(Console.ReadLine(), out opcao))
    {
        Console.WriteLine("Opção inválida. Por favor, digite um número.");
        Console.ReadKey();
        continue;
    }

    switch (opcao)
    {

        case 1:

            Console.Clear();
            Console.WriteLine(" ======= CADASTRO DE ALUNO ======= ");

            string nome;

        do
        {
            Console.Write("Nome: ");
            nome = Console.ReadLine() ?? "";

            if (nome.Trim().Length <= 1 || !nome.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                Console.WriteLine("Nome inválido! Digite um nome com mais de 1 caractere, sem números ou caracteres especiais.");
            }

        } while (nome.Trim().Length <= 1 || !nome.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)));


            // VALIDAÇÃO DO NOME
           

            do
            {
                Console.Write("Nome: ");
                nome = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("\nNome não pode ficar em branco!");
                Console.WriteLine("Digite novamente.\n");
            }

            } while (string.IsNullOrWhiteSpace(nome));

            // VALIDAÇÃO DO EMAIL
            string email;

            do
            {
                Console.Write("Email: ");
                email = (Console.ReadLine() ?? "").Trim();

                string[] partes = email.Split('@');

                bool emailValido =
                    partes.Length == 2 &&
                    partes[0].Length > 0 &&
                    !partes[0].Contains(' ') &&
                    (
                        partes[1].Equals("gmail.com", StringComparison.OrdinalIgnoreCase) ||
                        partes[1].Equals("hotmail.com", StringComparison.OrdinalIgnoreCase) ||
                        partes[1].Equals("organizaê.com", StringComparison.OrdinalIgnoreCase)
                    );

                if (!emailValido)
                {
                    Console.WriteLine("\nEmail inválido!");
                    Console.WriteLine("Utilize @gmail.com, @hotmail.com ou @organizaê.com.");
                    Console.WriteLine("Digite novamente.\n");
                }
                else
                {
                    break;
                }

            } while (true);


            // VALIDAÇÃO DA SENHA
            string senha;

            do
            {
                Console.Write("Senha: ");
                senha = Console.ReadLine() ?? "";

                if (senha.Length < 8)
                {
                    Console.WriteLine("\nSenha com dígitos insuficientes!");
                    Console.WriteLine("A senha deve possuir pelo menos 8 caracteres.");
                    Console.WriteLine("Digite novamente.\n");
                }

            } while (senha.Length < 8);


            // CADASTRAR ALUNO
            alunoService.CadastrarAluno(nome, email, senha);


            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();

            break;


        case 2:

            Console.Clear();
            Console.WriteLine(" ======= LISTA DE ALUNOS ======= ");
            alunoService.ListarAlunos();

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();

            break;

        case 3:
            Console.WriteLine(" ======= BUSCAR ALUNO POR ID ======= ");
            Console.Write("Digite o ID do aluno: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Aluno? aluno = alunoService.BuscarPorId(id);
                if (aluno != null)
                {
                    Console.WriteLine("================================");
                    Console.WriteLine($"ID: " + aluno.IdAluno);
                    Console.WriteLine($"Nome: " + aluno.Nome);
                    Console.WriteLine($"Email: " + aluno.Email);
                    Console.WriteLine($"Status: " + aluno.Status);
                }
                else
                {
                    Console.WriteLine("Aluno não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, digite um número.");
            }

            Console.ReadKey();
            break;

        case 4:
            Console.WriteLine(" ======= BUSCAR ALUNO POR NOME ======= ");
            Console.Write("Digite o nome do aluno: ");
            string nomeBusca = Console.ReadLine() ?? "";

            alunoService.BuscarPorNome(nomeBusca);
            Console.ReadKey();
            break;



        case 5:

            Console.Clear();
            Console.WriteLine(" ======= ATUALIZAR ALUNO ======= ");

            Console.Write("Digite o ID do aluno que deseja atualizar: ");

            if (int.TryParse(Console.ReadLine(), out int idAtualizar))
            {
                Console.Write("Novo Nome: ");
                string novoNome = Console.ReadLine() ?? "";

                // VALIDAÇÃO DO EMAIL
                string novoEmail;

                do
                {
                    Console.Write("Novo Email: ");
                    novoEmail = (Console.ReadLine() ?? "").Trim();

                    string[] partes = novoEmail.Split('@');

                    bool emailValido =
                        partes.Length == 2 &&
                        partes[0].Length > 0 &&
                        !partes[0].Contains(' ') &&
                        (
                            partes[1].Equals("gmail.com", StringComparison.OrdinalIgnoreCase) ||
                            partes[1].Equals("hotmail.com", StringComparison.OrdinalIgnoreCase) ||
                            partes[1].Equals("organizaê.com", StringComparison.OrdinalIgnoreCase)
                        );

                    if (!emailValido)
                    {
                        Console.WriteLine("\nEmail inválido!");
                        Console.WriteLine("Utilize @gmail.com, @hotmail.com ou @organizaê.com.");
                        Console.WriteLine("Digite novamente.\n");
                    }

                } while (
                    !novoEmail.Contains('@') ||
                    novoEmail.Split('@').Length != 2 ||
                    novoEmail.Split('@')[0].Length == 0 ||
                    novoEmail.Contains(' ') ||
                    !(
                        novoEmail.Split('@')[1].Equals("gmail.com", StringComparison.OrdinalIgnoreCase) ||
                        novoEmail.Split('@')[1].Equals("hotmail.com", StringComparison.OrdinalIgnoreCase) ||
                        novoEmail.Split('@')[1].Equals("organizaê.com", StringComparison.OrdinalIgnoreCase)
                    )
                );


                // VALIDAÇÃO DA SENHA
                string novaSenha;

                do
                {
                    Console.Write("Nova Senha: ");
                    novaSenha = Console.ReadLine() ?? "";

                    if (novaSenha.Length < 8)
                    {
                        Console.WriteLine("\nSenha com dígitos insuficientes!");
                        Console.WriteLine("A senha deve possuir pelo menos 8 caracteres.");
                        Console.WriteLine("Digite novamente.\n");
                    }

                } while (novaSenha.Length < 8);


                // ATUALIZAÇÃO DO ALUNO
                bool atualizado = alunoService.AtualizarAluno(
                    idAtualizar,
                    novoNome,
                    novoEmail,
                    novaSenha
                );

                if (atualizado)
                {
                    Console.WriteLine("\nDados do aluno atualizados!");
                }
                else
                {
                    Console.WriteLine("\nAluno não encontrado.");
                    Console.WriteLine("Nenhuma alteração foi realizada.");
                }

            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, digite um número.");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();

            break;

        case 6:
            Console.WriteLine(" ======= EXCLUIR ALUNO ======= ");
            Console.Write("Digite o ID do aluno que deseja excluir: ");
            if (int.TryParse(Console.ReadLine(), out int idExcluir))
            {
                alunoService.ExcluirAluno(idExcluir);
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, digite um número.");
            }
            Console.ReadKey();
            break;

        case 7:
            Console.WriteLine("======= CADASTRO DE DISCIPLINA ======= ");
            Console.Write("ID do Aluno: ");
            if (int.TryParse(Console.ReadLine(), out int idAluno))
            {
                Console.Write("Nome da Disciplina: ");
                string nomeDisciplina = Console.ReadLine() ?? "";

                Console.Write("Descrição da Disciplina: ");
                string descricaoDisciplina = Console.ReadLine() ?? "";

                disciplinaService.CadastrarDisciplina(
                    nomeDisciplina,
                    descricaoDisciplina,
                    idAluno
                );
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, digite um número.");
            }
            Console.ReadKey();
            break;

        case 8:
            Console.WriteLine("======= LISTA DE DISCIPLINAS ======= ");
            Console.Write("Digite o ID do aluno para listar suas disciplinas: ");
            if (int.TryParse(Console.ReadLine(), out int idAlunoLista))
            {
                disciplinaService.ListarDisciplinas(idAlunoLista);
            }
            else
            {
                Console.WriteLine("ID inválido. Por favor, digite um número.");
            }
            Console.ReadKey();
            break;

        case 9:
            Console.WriteLine("======= ATUALIZAR DISCIPLINA ======= ");
            Console.Write("Digite o ID da disciplina que deseja atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int idDisciplinaAtualizar))
            {
                Console.Write("Digite o ID do aluno dono da disciplina: ");
                if (int.TryParse(Console.ReadLine(), out int idAlunoDisciplina))
                {
                    Console.Write("Novo Nome da Disciplina: ");
                    string novoNomeDisciplina = Console.ReadLine() ?? "";

                    Console.Write("Nova Descrição da Disciplina: ");
                    string novaDescricaoDisciplina = Console.ReadLine() ?? "";

                    disciplinaService.AtualizarDisciplina(
                        idDisciplinaAtualizar,
                        idAlunoDisciplina,
                        novoNomeDisciplina,
                        novaDescricaoDisciplina
                    );
                }
                else
                {
                    Console.WriteLine("ID do aluno inválido. Por favor, digite um número.");
                }
            }
            else
            {
                Console.WriteLine("ID da disciplina inválido. Por favor, digite um número.");
            }
            Console.ReadKey();
            break;


        case 10:
            Console.WriteLine("======= EXCLUIR DISCIPLINA ======= ");
            Console.Write("Digite o ID da disciplina que deseja excluir: ");
            if (int.TryParse(Console.ReadLine(), out int idDisciplinaExcluir))
            {
                Console.Write("Digite o ID do aluno dono da disciplina: ");
                if (int.TryParse(Console.ReadLine(), out int idAlunoDisciplinaExcluir))
                {
                    disciplinaService.ExcluirDisciplina(
                        idDisciplinaExcluir,
                        idAlunoDisciplinaExcluir
                    );
                }
                else
                {
                    Console.WriteLine("ID do aluno inválido. Por favor, digite um número.");
                }
            }
            else
            {
                Console.WriteLine("ID da disciplina inválido. Por favor, digite um número.");
            }
            Console.ReadKey();
            break;

        case 11:
            Console.WriteLine("======= CADASTRO DE TAREFA ======= ");
            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine() ?? "";

            Console.Write("Digite a descrição da tarefa: ");
            string descricaoTarefa = Console.ReadLine() ?? "";

            Console.Write("Digite o tipo da tarefa(Ex: Trabalho, Prova, Atividade, etc): ");
            string tipo = Console.ReadLine() ?? "";

            Console.Write("Digite o prazo de entrega (dd/mm/yyyy): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime prazoEntrega))
            {
                Console.Write("Digite o ID da disciplina: ");
                if (int.TryParse(Console.ReadLine(), out int idDisciplina))
                {
                    tarefaService.CadastrarTarefa(titulo, descricaoTarefa, tipo, prazoEntrega, idDisciplina);
                }
                else
                {
                    Console.WriteLine("ID da disciplina inválido. Por favor, digite um número.");
                }
            }
            else
            {
                Console.WriteLine("Prazo de entrega inválido. Por favor, digite uma data válida.");
            }
            Console.ReadKey();
            break;

        case 12:
            Console.WriteLine("======= LISTA DE TAREFAS ======= ");
            Console.Write("Digite o ID da disciplina para listar suas tarefas: ");
            if (int.TryParse(Console.ReadLine(), out int idDisciplinaLista))
            {
                tarefaService.ListarTarefas(idDisciplinaLista);
            }
            else
            {
                Console.WriteLine("ID da disciplina inválido. Por favor, digite um número.");
            }
            Console.ReadKey();
            break;

        case 13:
            Console.WriteLine("======= ATUALIZAR TAREFA ======= ");
            Console.Write("Digite o ID da tarefa que deseja atualizar: ");
            int idTarefaAtualizar = int.Parse(Console.ReadLine()!);

            Console.Write("Digite o ID da disciplina: ");
            int idDisciplinaTarefaAtualizar = int.Parse(Console.ReadLine()!);

            string novoTitulo;

            do
            {
                Console.Write("Novo Título da Tarefa: ");
                novoTitulo = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(novoTitulo))
                {
                    Console.WriteLine("O título não pode ser vazio. Por favor, digite um título válido.");
                }
            } while (string.IsNullOrWhiteSpace(novoTitulo));

            string novaDescricao;

            do
            {
                Console.Write("Nova Descrição da Tarefa: ");
                novaDescricao = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(novaDescricao))
                {
                    Console.WriteLine("A descrição não pode ser vazia. Por favor, digite uma descrição válida.");
                }
            } while (string.IsNullOrWhiteSpace(novaDescricao));

            string novoTipo;

            do
            {
                Console.Write("Novo Tipo da Tarefa(EX: Trabalho, Prova, Atividade, etc): ");
                novoTipo = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(novoTipo))
                {
                    Console.WriteLine("O tipo não pode ser vazio. Por favor, digite um tipo válido.");
                }
            } while (string.IsNullOrWhiteSpace(novoTipo));

            DateTime novoPrazoEntrega;

            while (true)
            {
                Console.WriteLine("Digite o novo prazo de entrega da tarefa (formato: dd/MM/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out novoPrazoEntrega))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Data inválida, por favor, tente novamente.");
                }

            }

            string novoStatus;

            while (true)
            {
                Console.Write("Digite o novo status da tarefa (Pendente, Concluída): ");
                string statusdigitado = Console.ReadLine()!;
                if (statusdigitado.Equals("Pendente", StringComparison.OrdinalIgnoreCase))
                {
                    novoStatus = "Pendente";
                    break;
                }
                else if (statusdigitado.Equals("Concluída", StringComparison.OrdinalIgnoreCase))
                {
                    novoStatus = "Concluída";
                    break;
                }
                else
                {
                    Console.WriteLine("Status inválido. Por favor, digite 'Pendente' ou 'Concluída'.");
                }

            }
            tarefaService.AtualizarTarefa(
                idTarefaAtualizar,
                idDisciplinaTarefaAtualizar,
                novoTitulo,
                novaDescricao,
                novoTipo,
                novoPrazoEntrega,
                novoStatus
            );

            break;

        case 14:
            Console.WriteLine("======= EXCLUIR TAREFA ======= ");
            Console.Write("Digite o ID da tarefa que deseja excluir: ");
            int idTarefaExcluir = int.Parse(Console.ReadLine()!);
            Console.Write("Digite o ID da disciplina da Tarefa: ");
            int idDisciplinaTarefaExcluir = int.Parse(Console.ReadLine()!);

            tarefaService.ExcluirTarefa(idTarefaExcluir, idDisciplinaTarefaExcluir);
            break;

        case 15:
            {
                Console.Write("Digite o ID da tarefa: ");
                int idTarefa = int.Parse(Console.ReadLine()!);

                Console.Write("Digite o ID da disciplina: ");
                int idDisciplina = int.Parse(Console.ReadLine()!);

                tarefaService.ConcluirTarefa(idTarefa, idDisciplina);

                break;
            }

        case 16:
            tarefaService.ExibirPainel();
            break;

        case 17:
            tarefaService.ListarTarefasPorStatus();
            break;

        case 18:
            Console.WriteLine("Você estásaindodo ORGANIZAÊ, ATÉ A PRÓXIMA!...");
            break;

        default:
            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
            Console.ReadKey();
            break;
    }
}




Conexao banco = new Conexao();

try
{
    using (MySqlConnection conexao = banco.Conectar())
    {
        conexao.Open();

        Console.WriteLine("Conectado ao MySQL!");
    }
}
catch (Exception erro)
{
    Console.WriteLine("Erro: " + erro.Message);
}
