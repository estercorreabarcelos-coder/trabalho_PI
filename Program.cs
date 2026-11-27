using System;
using System.Linq;
using trabalho_PI.Database;

Conexao conexao = new Conexao();
conexao.TestarConexao();

AlunoService alunoService = new AlunoService();
DisciplinaService disciplinaService = new DisciplinaService();
TarefaServices tarefaService = new TarefaServices();

bool encerrarPrograma = false;

while (!encerrarPrograma)
{
    Aluno? alunoLogado = null;
    int opcaoAcesso = -1;

    // TELA INICIAL
    while (opcaoAcesso != 0 && opcaoAcesso != 1 && opcaoAcesso != 2)
    {
        Console.Clear();

        Console.WriteLine(" ==================================================");
        Console.WriteLine(" |  ORGANIZAÊ - ORGANIZADOR DE TAREFAS ESCOLARES  |");
        Console.WriteLine(" ==================================================");
        Console.WriteLine("BEM-VINDO AO ORGANIZAÊ, O SEU ORGANIZADOR DE TAREFAS ESCOLARES!");
        Console.WriteLine("Aqui você poderá organizar suas tarefas de forma simples e eficiente.");
        Console.WriteLine("Vamos começar! Por favor, escolha uma das opções abaixo:");
        Console.WriteLine("\n1 - Entrar");
        Console.WriteLine("2 - Cadastrar-se");
        Console.WriteLine("0 - Sair");

        Console.Write("\nEscolha uma opção: ");

        if (!int.TryParse(Console.ReadLine(), out opcaoAcesso) ||
            (opcaoAcesso != 0 && opcaoAcesso != 1 && opcaoAcesso != 2))
        {
            Console.WriteLine("Opção inválida!");
            Console.ReadKey();
            opcaoAcesso = -1;
        }
    }

    if (opcaoAcesso == 0)
    {
        encerrarPrograma = true;
        break;
    }

    // CADASTRO DE ALUNO
    if (opcaoAcesso == 2)
    {
        Console.Clear();
        Console.WriteLine("======= CADASTRO DE ALUNO =======");
        Console.WriteLine("Digite 0 em qualquer campo para voltar.");

        string nomeCadastro;

        do
        {
            Console.Write("\nNome: ");
            nomeCadastro = Console.ReadLine() ?? "";

            if (nomeCadastro == "0")
            {
                break;
            }

            if (nomeCadastro.Trim().Length <= 1 ||
                !nomeCadastro.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                Console.WriteLine("Nome inválido! Digite um nome válido.");
            }

        } while (nomeCadastro.Trim().Length <= 1 ||
                !nomeCadastro.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)));

        if (nomeCadastro == "0")
        {
            continue;
        }

        string emailCadastro;
        bool emailValido;

        do
        {
            Console.Write("\nEmail: ");
            emailCadastro = (Console.ReadLine() ?? "").Trim();

            if (emailCadastro == "0")
            {
                break;
            }

            string[] partes = emailCadastro.Split('@');

            emailValido =
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
            }

        } while (!emailValido);

        if (emailCadastro == "0")
        {
            continue;
        }

        string senhaCadastro;

        do
        {
            Console.Write("\nSenha (mínimo 8 caracteres): ");
            senhaCadastro = Console.ReadLine() ?? "";

            if (senhaCadastro == "0")
            {
                break;
            }

            if (senhaCadastro.Length < 8)
            {
                Console.WriteLine("A senha precisa ter pelo menos 8 caracteres!");
            }

        } while (senhaCadastro.Length < 8);

        if (senhaCadastro == "0")
        {
            continue;
        }

        alunoService.CadastrarAluno(
            nomeCadastro,
            emailCadastro,
            senhaCadastro
        );

        Console.WriteLine("\nCadastro realizado! Agora faça seu login.");
        Console.ReadKey();

        continue;
    }

    // LOGIN
    bool loginRealizado = false;
    bool voltarParaInicio = false;

    while (!loginRealizado && !voltarParaInicio)
    {
        Console.Clear();
        Console.WriteLine("======= LOGIN =======");
        Console.WriteLine("Digite 0 no campo de email para voltar.");
        
        Console.Write("\nEmail: ");
        string emailLogin = Console.ReadLine() ?? "";

        if (emailLogin == "0")
        {
            voltarParaInicio = true;
            continue;
        }

        Console.Write("Senha: ");
        string senhaLogin = LerSenha();

        alunoLogado = alunoService.Login(emailLogin, senhaLogin);

        if (alunoLogado == null)
        {
            Console.WriteLine("\nEmail ou senha incorretos, ou conta inativa.");
            Console.WriteLine("Tente novamente.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine($"\nBem-vindo(a), {alunoLogado.Nome}!");
            loginRealizado = true;
            Console.ReadKey();
        }
    }

    if (voltarParaInicio)
    {
        continue;
    }
    int idAlunoLogado = alunoLogado!.IdAluno;

    // MENU PRINCIPAL
    int opcao = -1;

    while (opcao != 0 && alunoLogado != null)
    {
        Console.Clear();

        Console.WriteLine(" ==================================================");
        Console.WriteLine(" |  ORGANIZAÊ - ORGANIZADOR DE TAREFAS ESCOLARES  |");
        Console.WriteLine(" ==================================================");

        Console.WriteLine($"Aluno: {alunoLogado.Nome}");

        Console.WriteLine("\n===== OPÇÕES PARA DISCIPLINA =====");
        Console.WriteLine("1 - Cadastrar Disciplina");
        Console.WriteLine("2 - Listar Minhas Disciplinas");
        Console.WriteLine("3 - Atualizar Disciplina");
        Console.WriteLine("4 - Excluir Disciplina");

        Console.WriteLine("\n===== OPÇÕES PARA TAREFA =====");
        Console.WriteLine("5 - Cadastrar Tarefa");
        Console.WriteLine("6 - Listar Tarefas");
        Console.WriteLine("7 - Atualizar Tarefa");
        Console.WriteLine("8 - Excluir Tarefa");
        Console.WriteLine("9 - Concluir Tarefa");
        Console.WriteLine("10 - Painel de Tarefas");
        Console.WriteLine("11 - Listar Tarefas por Situação");

        Console.WriteLine("\n===== CONTA =====");
        Console.WriteLine("12 - Excluir Minha Conta");
        Console.WriteLine("0 - Sair da Conta");

        Console.Write("\nEscolha uma opção: ");

        if (!int.TryParse(Console.ReadLine(), out opcao))
        {
            Console.WriteLine("Opção inválida. Digite um número.");
            Console.ReadKey();
            continue;
        }

        switch (opcao)
        {
            case 1:
            {
                Console.Clear();
                Console.WriteLine("======= CADASTRO DE DISCIPLINA =======");
                Console.WriteLine("Digite 0 em qualquer campo para cancelar.");

                Console.Write("\nNome da Disciplina: ");
                string nomeDisciplina = Console.ReadLine() ?? "";

                if (nomeDisciplina == "0")
                {
                    Console.WriteLine("\nOperação cancelada.");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Descrição da Disciplina: ");
                string descricaoDisciplina = Console.ReadLine() ?? "";

                if (descricaoDisciplina == "0")
                {
                    Console.WriteLine("\nOperação cancelada.");
                    Console.ReadKey();
                    break;
                }

                disciplinaService.CadastrarDisciplina(
                    nomeDisciplina,
                    descricaoDisciplina,
                    idAlunoLogado
                );

                Console.ReadKey();
                break;
            }

            case 2:
            {
                Console.Clear();
                Console.WriteLine("======= MINHAS DISCIPLINAS =======");
                Console.WriteLine("Digite 0 para voltar ao menu principal.");
                Console.Write("\nPressione ENTER para listar ou digite 0: ");

                string escolha = Console.ReadLine() ?? "";

                if (escolha == "0")
                {
                    break;
                }

                disciplinaService.ListarDisciplinas(idAlunoLogado);

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
            }

            case 3:
            {
                Console.Clear();
                Console.WriteLine("======= ATUALIZAR DISCIPLINA =======");
                Console.WriteLine("Digite 0 em qualquer campo para cancelar.");

                Console.Write("\nDigite o ID da disciplina: ");
                string entradaId = Console.ReadLine() ?? "";

                if (entradaId == "0")
                {
                    break;
                }

                if (!int.TryParse(entradaId, out int idDisciplinaAtualizar))
                {
                    Console.WriteLine("ID inválido!");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Novo Nome da Disciplina: ");
                string novoNomeDisciplina = Console.ReadLine() ?? "";

                if (novoNomeDisciplina == "0")
                {
                    break;
                }

                Console.Write("Nova Descrição da Disciplina: ");
                string novaDescricaoDisciplina = Console.ReadLine() ?? "";

                if (novaDescricaoDisciplina == "0")
                {
                    break;
                }

                disciplinaService.AtualizarDisciplina(
                    idDisciplinaAtualizar,
                    idAlunoLogado,
                    novoNomeDisciplina,
                    novaDescricaoDisciplina
                );

                Console.ReadKey();
                break;
            }
            case 4:
            {
                Console.Clear();
                Console.WriteLine("======= EXCLUIR DISCIPLINA =======");
                Console.WriteLine("Digite 0 para cancelar e voltar ao menu principal.");

                Console.Write("\nDigite o ID da disciplina: ");
                string entradaId = Console.ReadLine() ?? "";

                if (entradaId == "0")
                {
                    Console.WriteLine("\nOperação cancelada.");
                    Console.ReadKey();
                    break;
                }

                if (int.TryParse(entradaId, out int idDisciplinaExcluir))
                {
                    disciplinaService.ExcluirDisciplina(
                        idDisciplinaExcluir,
                        idAlunoLogado
                    );
                }
                else
                {
                    Console.WriteLine("ID inválido!");
                }

                Console.ReadKey();
                break;
            }

           case 5:
            {
                Console.Clear();
                Console.WriteLine("======= CADASTRO DE TAREFA =======");
                Console.WriteLine("Digite 0 em qualquer campo para cancelar.");

                Console.Write("\nDigite o título da tarefa: ");
                string titulo = Console.ReadLine() ?? "";

                if (titulo == "0")
                {
                    break;
                }

                Console.Write("Digite a descrição da tarefa: ");
                string descricaoTarefa = Console.ReadLine() ?? "";

                if (descricaoTarefa == "0")
                {
                    break;
                }

                Console.Write("Digite o tipo da tarefa: ");
                string tipo = Console.ReadLine() ?? "";

                if (tipo == "0")
                {
                    break;
                }

                Console.Write("Digite o prazo de entrega (dd/MM/yyyy): ");
                string entradaPrazo = Console.ReadLine() ?? "";

                if (entradaPrazo == "0")
                {
                    break;
                }

                if (!DateTime.TryParse(entradaPrazo, out DateTime prazoEntrega))
                {
                    Console.WriteLine("Prazo de entrega inválido!");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Digite o ID da disciplina: ");
                string entradaDisciplina = Console.ReadLine() ?? "";

                if (entradaDisciplina == "0")
                {
                    break;
                }

                if (int.TryParse(entradaDisciplina, out int idDisciplina))
                {
                    tarefaService.CadastrarTarefa(
                        titulo,
                        descricaoTarefa,
                        tipo,
                        prazoEntrega,
                        idDisciplina,
                        idAlunoLogado
                    );
                }
                else
                {
                    Console.WriteLine("ID da disciplina inválido!");
                }

                Console.ReadKey();
                break;
            }

            case 6:
            {
                Console.Clear();
                Console.WriteLine("======= LISTA DE TAREFAS =======");
                Console.WriteLine("Digite 0 para voltar ao menu principal.");

                Console.Write("\nDigite o ID da disciplina: ");
                string entradaId = Console.ReadLine() ?? "";

                if (entradaId == "0")
                {
                    break;
                }

                if (int.TryParse(entradaId, out int idDisciplinaLista))
                {
                    tarefaService.ListarTarefas(
                        idDisciplinaLista,
                        idAlunoLogado
                    );
                }
                else
                {
                    Console.WriteLine("ID da disciplina inválido!");
                }

                Console.ReadKey();
                break;
            }

            case 7:
            {
                Console.Clear();
                Console.WriteLine("======= ATUALIZAR TAREFA =======");
                Console.WriteLine("Digite 0 em qualquer campo para cancelar.");

                Console.Write("\nDigite o ID da tarefa: ");
                string entradaTarefa = Console.ReadLine() ?? "";

                if (entradaTarefa == "0")
                    break;

                if (!int.TryParse(entradaTarefa, out int idTarefaAtualizar))
                {
                    Console.WriteLine("ID inválido!");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Digite o ID da disciplina: ");
                string entradaDisciplina = Console.ReadLine() ?? "";

                if (entradaDisciplina == "0")
                    break;

                if (!int.TryParse(entradaDisciplina, out int idDisciplinaTarefaAtualizar))
                {
                    Console.WriteLine("ID inválido!");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Novo Título da Tarefa: ");
                string novoTitulo = Console.ReadLine() ?? "";

                if (novoTitulo == "0")
                    break;

                if (string.IsNullOrWhiteSpace(novoTitulo))
                {
                    Console.WriteLine("O título não pode ser vazio.");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Nova Descrição da Tarefa: ");
                string novaDescricao = Console.ReadLine() ?? "";

                if (novaDescricao == "0")
                    break;

                if (string.IsNullOrWhiteSpace(novaDescricao))
                {
                    Console.WriteLine("A descrição não pode ser vazia.");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Novo Tipo da Tarefa: ");
                string novoTipo = Console.ReadLine() ?? "";

                if (novoTipo == "0")
                    break;

                if (string.IsNullOrWhiteSpace(novoTipo))
                {
                    Console.WriteLine("O tipo não pode ser vazio.");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Novo prazo de entrega (dd/MM/yyyy): ");
                string entradaPrazo = Console.ReadLine() ?? "";

                if (entradaPrazo == "0")
                    break;

                if (!DateTime.TryParse(entradaPrazo, out DateTime novoPrazoEntrega))
                {
                    Console.WriteLine("Data inválida!");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Novo status (Pendente ou Concluída): ");
                string statusDigitado = Console.ReadLine() ?? "";

                if (statusDigitado == "0")
                    break;

                string novoStatus;

                if (statusDigitado.Equals("Pendente", StringComparison.OrdinalIgnoreCase))
                {
                    novoStatus = "Pendente";
                }
                else if (
                    statusDigitado.Equals("Concluída", StringComparison.OrdinalIgnoreCase) ||
                    statusDigitado.Equals("Concluida", StringComparison.OrdinalIgnoreCase))
                {
                    novoStatus = "Concluída";
                }
                else
                {
                    Console.WriteLine("Status inválido!");
                    Console.ReadKey();
                    break;
                }

                tarefaService.AtualizarTarefa(
                    idTarefaAtualizar,
                    idDisciplinaTarefaAtualizar,
                    idAlunoLogado,
                    novoTitulo,
                    novaDescricao,
                    novoTipo,
                    novoPrazoEntrega,
                    novoStatus
                );

                Console.ReadKey();
                break;
            }

            case 8:
            {
                Console.Clear();
                Console.WriteLine("======= EXCLUIR TAREFA =======");
                Console.WriteLine("Digite 0 em qualquer campo para cancelar.");

                Console.Write("\nDigite o ID da tarefa: ");
                string entradaTarefa = Console.ReadLine() ?? "";

                if (entradaTarefa == "0")
                    break;

                if (!int.TryParse(entradaTarefa, out int idTarefaExcluir))
                {
                    Console.WriteLine("ID da tarefa inválido!");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Digite o ID da disciplina: ");
                string entradaDisciplina = Console.ReadLine() ?? "";

                if (entradaDisciplina == "0")
                    break;

                if (!int.TryParse(entradaDisciplina, out int idDisciplinaTarefaExcluir))
                {
                    Console.WriteLine("ID da disciplina inválido!");
                    Console.ReadKey();
                    break;
                }

                tarefaService.ExcluirTarefa(
                    idTarefaExcluir,
                    idDisciplinaTarefaExcluir,
                    idAlunoLogado
                );

                Console.ReadKey();
                break;
            }

            case 9:
            {
                Console.Clear();
                Console.WriteLine("======= CONCLUIR TAREFA =======");
                Console.WriteLine("Digite 0 em qualquer campo para cancelar.");

                Console.Write("\nDigite o ID da tarefa: ");
                string entradaTarefa = Console.ReadLine() ?? "";

                if (entradaTarefa == "0")
                    break;

                if (!int.TryParse(entradaTarefa, out int idTarefa))
                {
                    Console.WriteLine("ID da tarefa inválido!");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Digite o ID da disciplina: ");
                string entradaDisciplina = Console.ReadLine() ?? "";

                if (entradaDisciplina == "0")
                    break;

                if (!int.TryParse(entradaDisciplina, out int idDisciplina))
                {
                    Console.WriteLine("ID da disciplina inválido!");
                    Console.ReadKey();
                    break;
                }

                tarefaService.ConcluirTarefa(
                    idTarefa,
                    idDisciplina,
                    idAlunoLogado
                );

                Console.ReadKey();
                break;
            }

            case 10:
            {
                Console.Clear();
                Console.WriteLine("======= PAINEL DE TAREFAS =======");
                Console.WriteLine("Digite 0 para voltar ao menu principal.");
                Console.Write("\nPressione ENTER para visualizar o painel ou digite 0: ");

                string escolha = Console.ReadLine() ?? "";

                if (escolha == "0")
                    break;

                tarefaService.ExibirPainel(idAlunoLogado);

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
            }

            case 11:
            {
                Console.Clear();
                Console.WriteLine("======= TAREFAS POR SITUAÇÃO =======");
                Console.WriteLine("Digite 0 para voltar ao menu principal.");
                Console.Write("\nPressione ENTER para listar ou digite 0: ");

                string escolha = Console.ReadLine() ?? "";

                if (escolha == "0")
                    break;

                tarefaService.ListarTarefasPorStatus(idAlunoLogado);

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;
            }

            case 12:
            {
                Console.Clear();
                Console.WriteLine("======= EXCLUIR MINHA CONTA =======");
                Console.WriteLine("Tem certeza de que deseja excluir sua conta?");
                Console.WriteLine("Essa ação não poderá ser desfeita.");
                Console.WriteLine("\nDigite 0 para voltar ao menu principal.");

                Console.Write("\nDigite S para confirmar ou N para cancelar: ");
                string confirmacao = Console.ReadLine() ?? "";

                if (confirmacao == "0")
                {
                    Console.WriteLine("\nOperação cancelada.");
                    Console.ReadKey();
                    break;
                }

                if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    alunoService.ExcluirAluno(idAlunoLogado);

                    alunoLogado = null;
                    opcao = 0;

                    Console.WriteLine("\nSolicitação de exclusão realizada.");
                }
                else
                {
                    Console.WriteLine("\nExclusão cancelada.");
                }

                Console.ReadKey();
                break;
            }

            case 0:
            {
                Console.WriteLine("Você saiu da sua conta!");
                alunoLogado = null;
                break;
            }

            default:
            {
                Console.WriteLine("Opção inválida!");
                Console.ReadKey();
                break;
            }
        }
    }
}

Console.Clear();
Console.WriteLine("Obrigado por utilizar o ORGANIZAÊ!");
Console.WriteLine("Até a próxima!");


// LEITURA DA SENHA COM ASTERISTICOS (SOMENTE NO LOGIN)
string LerSenha()
{
    string senha = "";

    while (true)
    {
        ConsoleKeyInfo tecla = Console.ReadKey(true);

        if (tecla.Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            break;
        }

        if (tecla.Key == ConsoleKey.Backspace)
        {
            if (senha.Length > 0)
            {
                senha = senha.Substring(0, senha.Length - 1);
                Console.Write("\b \b");
            }
        }
        else if (!char.IsControl(tecla.KeyChar))
        {
            senha += tecla.KeyChar;
            Console.Write("*");
        }
    }

    return senha;
}