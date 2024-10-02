using System;
using biblioteca2024.Data;
using System.ComponentModel;
using biblioteca2024.Model;

class Program
{
    static void Main()
    {
        bool exit = false;

        Console.WriteLine("+--------------------------------+ \n" +
                          "|           bem vindo!           | \n" +
                          "|      +------------------+      | \n" +
                          "|      |   DREAM LIBRARY  |      | \n" +
                          "|      +------------------+      | \n" +
                          "|                                | \n" +
                          "+--------------------by LeoMarks-+ \n");
        Console.WriteLine("Pressione ENTER para continuar!");
        Console.ReadLine();
        
        while (!exit)

        {
            Console.Clear(); // Limpa o console a cada iteração para deixar o menu mais legível
            Console.WriteLine(" Selecione uma ação:");
            Console.WriteLine("+---------------------------+");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("| 1 - Mostrar Livros");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("| 2 - Cadastrar Novo Livro");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("| 3 - Atualizar Livro");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("| 4 - Deletar Livro");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("| 5 - Sair");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("+---------------------------+");
            Console.Write("Digite a sua escolha: ");
            
            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Você escolheu a Ação 1.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("------------------------");
                    MostrarLivros();
                    break;
                case 2:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Você escolheu a Ação 2.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("------------------------");
                    DALBiblioteca.InserirLivros(); //insere uma nova linha
                    break;
                case 3:
                    Console.Clear(); // Limpa o console a cada iteração para deixar o menu mais legível
                    Console.WriteLine(" Selecione uma ação:");
                    Console.WriteLine("+--------------------------------+");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("| 1 - Atualizar Titulo");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("| 2 - Atualizar Autor");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("| 3 - Atualizar Editora");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("| 4 - Atualizar Ano de Publicação");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("| 5 - Sair");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("+--------------------------------+");
                    Console.Write("Digite a sua escolha: ");

                    int inputDois = int.Parse(Console.ReadLine());

                    switch (inputDois)
                    {
                        case 1:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Você escolheu a Ação 1.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("------------------------");
                            DALBiblioteca.AtualizarNomeLivro(); //qual informação vai ser atualizada?
                            break;

                        case 2:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Você escolheu a Ação 2.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("------------------------");
                            DALBiblioteca.AtualizarAutorLivro(); //qual informação vai ser atualizada?
                            break;

                        case 3:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Você escolheu a Ação 3.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("------------------------");
                            DALBiblioteca.AtualizarEditoraLivro(); //qual informação vai ser atualizada?
                            break;

                        case 4:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Você escolheu a Ação 3.");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("------------------------");
                            DALBiblioteca.AtualizarAnoLivro(); //qual informação vai ser atualizada?
                            break;

                        default:
                            Console.WriteLine("Opção inválida no switch secundário.");
                            break;
                    }
                    break;

                case 4:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Você escolheu a Ação 4.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("------------------------");
                    DALBiblioteca.DeletarLivros(); //baseado em qual campo da tabela vai deletar? normalmente é o ID
                    break;
                case 5:
                    exit = true;
                    Console.WriteLine("Saindo...\n" +
                                      "Volte Sempre!");
                    
                    break;
                default:
                    Console.WriteLine("Escolha inválida. Tente novamente.");
                    break;
            }

            if (!exit)
            {
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
                Console.ReadKey();
            }
        }
    }

    //método para exibir os livros

    public static void MostrarLivros()
    {
        //para exibir os resultados da consulta SQL
        List<Livro> Livros = DALBiblioteca.GetLivros();
        foreach (Livro Livro in Livros)
        {
            Console.WriteLine("Id: {0}", Livro.Id);
            //Console.WriteLine("ID: " + livro.Id); // concatenação
            //Console.Writeline($"ID: {livro.Id}"); //string interpolada
            Console.WriteLine("Título: {0}", Livro.Titulo);
            Console.WriteLine("Autor: {0}", Livro.Autor); //forma padrão por índice
            Console.WriteLine("Editora: " + Livro.Editora);
            Console.WriteLine("Ano de Publicação: " + Livro.AnoPublicacao);
            Console.WriteLine("---------------------------");
        }
        
    }


}


