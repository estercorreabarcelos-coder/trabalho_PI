AlunoService alunoService = new  AlunoService();

int opcao = 0;

while (opcao != 3)
{
    Console.Clear();
    Console.WriteLine("==================================================");
    Console.WriteLine("   ORGANIZAÊ - ORGANIZADOR DE TAREFAS ESCOLARES   ");
    Console.WriteLine("==================================================");

    Console.WriteLine("BEM-VINDO AO ORGANIZAÊ, O SEU ORGANIZADOR DE TAREFAS ESCOLARES!");
    Console.WriteLine("Aqui você poderá organizar suas tarefas de forma simples e eficiente.");
    Console.WriteLine("Vamos começar! Por favor, escolha uma das opções abaixo:");
    Console.WriteLine("====================================");
    Console.WriteLine("1: Cadastrar Aluno");
    Console.WriteLine("2: Listar Alunos");
    Console.WriteLine("3: Sair");
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
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Senha: ");
            string senha = Console.ReadLine() ?? "";

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
            Console.WriteLine("Você estásaindodo ORGANIZAÊ, ATÉ A PRÓXIMA!...");
            break;

        default:
            Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
            Console.ReadKey();
            break;
    }
}