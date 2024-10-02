using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biblioteca2024.Model;

namespace biblioteca2024.Data
{
    public class DALBiblioteca
    {

        /* para poder fazer uma consulta ao banco de dados, precisamos:
         
        1 - conexão com o banco de dados
            encontrar onde tá o arquivo de banco de dados
            criar um método que faz a conexão

        2 - criar um método que faz a consulta com o bd
            este método faz parte da classe dal também
            e é chamado no Program.cs
            fazer primeiro com DataTable (tipo de objeto para exibir dados)
            fazer depois com lista (que é mais fácil de manipular)

        3 - exibir os resultados que vieram da consulta
            receber os resultados de alguma forma
            formatar os resultados
            dar prints na tela 
         
         */


        //propriedade de conexão com o BD
        private static SQLiteConnection sqLiteConnection;

        //encontrar onde está o BD
        public static string path = Directory.GetCurrentDirectory() + "\\biblioteca2024.db";
        // o diretório do banco é: "C:\Users\leona\source\repos\biblioteca2024\biblioteca2024\bin\Debug\net6.0
        //método que realiza a conexão com o BD
        private static SQLiteConnection DbConnection()
        {
            sqLiteConnection = new SQLiteConnection("Data Source=" + path);
            sqLiteConnection.Open();
            return sqLiteConnection;
        }

        //método que realiza a consulta no banco de dados e retorna os dados encontrados
        public static List<Livro> GetLivros()
        {
            //criar uma lista onde cada linha da tabela livros vai ficar armazenada em uma posição da lista
            List<Livro> Livros = new List<Livro>();

            //try catch pq estamos trabalhando com banco de dados
            try
            {

                //criando um contexto para executar um comando de BD
                using (var comando = DbConnection().CreateCommand())
                {
                    //definindo o comando sql que vai ser executado
                    comando.CommandText = "SELECT * FROM livros";

                    //criando um contexto de leitura de dado
                    using (var leitor = comando.ExecuteReader())
                    {

                        //acessando cada linha da tabela
                        //"enquanto tiver coisas para ler nos contextos previamente definidos"
                        while (leitor.Read())
                        {
                            //enquanto tiver livros para ler, vamos criar um objeto de livro
                            Livro livro = new Livro(Convert.ToInt32(leitor["Id"]),
                                                     leitor["Titulo"].ToString(),
                                                     leitor["Autor"].ToString(),
                                                     leitor["Editora"].ToString(),
                                                     Convert.ToInt32(leitor["anoPublicacao"]));
                            Livros.Add(livro);
                        }


                    }

                }

                return Livros;

            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro SQL ao realizar consulta SQL. Detalhes: " + e);
                throw; //retorno em caso de erro
            }
            catch (Exception e1)
            {
                Console.WriteLine("Erro genérico ao realizar consulta SQL. Detalhes: " + e1);
                throw; //retorno em caso de erro
            }
        }







        public static void InserirLivros() {

            try
            {
                //primeiro de tudo, tem que fazer
                //a leitura dos dados que vão ser inseridos
                Console.WriteLine("Digite o ID do livro: ");
                int Id = Convert.ToInt32(Console.ReadLine());
                // outra forma de fazer:
                // int id = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o Título do livro: ");
                string Titulo = Console.ReadLine();
                Console.WriteLine("Digite o Autor do livro: ");
                string Autor = Console.ReadLine();
                Console.WriteLine("Digite a Editora do livro: ");
                string Editora = Console.ReadLine();
                Console.WriteLine("Digite o Ano de Publicação do livro: ");
                int AnoPublicacao = Convert.ToInt32(Console.ReadLine());

                //criando o contexto de conexão ao BD para executar um comando
                using (var comando = DbConnection().CreateCommand())
                {
                    //definir o comando SQL com o "insert" para inserir dados
                    comando.CommandText = " INSERT INTO livros" + 
                                          " (id, titulo, autor, editora, anoPublicacao)" + 
                                          " VALUES" + 
                                          " (@id, @titulo, @autor, @editora, @anoPublicacao)";
                    //atribuir valores para os parâmetros do insert
                    //na esquerda, nome da coluna no banco de dados
                    //na direita, nome da variável que o usuário digitou o valor
                    comando.Parameters.AddWithValue("@id", Id);
                    comando.Parameters.AddWithValue("@titulo", Titulo);
                    comando.Parameters.AddWithValue("@autor", Autor);
                    comando.Parameters.AddWithValue("@editora", Editora);
                    comando.Parameters.AddWithValue("@anoPublicacao", AnoPublicacao);
                    
                    //executar o comando
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0) { Console.WriteLine("\nO livro foi inserido com sucesso!\n"); 
                    } else { Console.WriteLine("\nNenhuma linha foi afetada! Algum erro aconteceu.\n"); }


                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("Erro SQL ao realizar consulta SQL. Detalhes: " + e);
                throw; //retorno em caso de erro
            }
            catch (Exception e1)
            {
                Console.WriteLine("Erro genérico ao realizar consulta SQL. Detalhes: " + e1);
                throw; //retorno em caso de erro
            }
        

        
        }

		public static void DeletarLivros()
        {

            try
            {

                //ler as informações do teclado do livro que o usuário quer deletar
                Console.WriteLine("Digite o id do livro que vai ser deletado: ");
                int id = int.Parse(Console.ReadLine());

                //conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    //criar a query (consulta) sql
                    cmd.CommandText = "DELETE FROM livros WHERE id = @id";
                    //definir os valores que vão ser substituídos pelos parâmetros
                    cmd.Parameters.AddWithValue("@id", id);
                    //executar a query sql
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        public static void AtualizarNomeLivro()
        {

            //esse método apenas atualiza o título de um livro baseado no seu id
            //o ideal é que o usuário consiga atualizar todas as informações do registro do BD
            //o que significa ter um método grande que contempla todas essas atualizações de campos diferentes
            //ou vários métodos, onde cada um lida com um campo do BD

            try
            {

                //ler as informações do teclado da pessoa que o usuário quer atualizar
                Console.WriteLine("Digite o id do livro que vai ter o seu título atualizado: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o novo título do livro: ");
                string novoTitulo = Console.ReadLine();

                //conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    //criar a query (consulta) sql
                    cmd.CommandText = "UPDATE livros SET titulo = @novoTitulo WHERE id = @id";
                    //definir os valores que vão ser substituídos pelos parâmetros
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@novoTitulo", novoTitulo);
                    //executar a query sql
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        public static void AtualizarAutorLivro()
        {

            //esse método apenas atualiza o Autor de um livro baseado no seu id
            //o ideal é que o usuário consiga atualizar todas as informações do registro do BD
            //o que significa ter um método grande que contempla todas essas atualizações de campos diferentes
            //ou vários métodos, onde cada um lida com um campo do BD

            try
            {

                //ler as informações do teclado da pessoa que o usuário quer atualizar
                Console.WriteLine("Digite o id do livro que vai ter o seu Autor atualizado: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o novo Autor do livro: ");
                string novoAutor = Console.ReadLine();

                //conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    //criar a query (consulta) sql
                    cmd.CommandText = "UPDATE livros SET autor = @novoAutor WHERE id = @id";
                    //definir os valores que vão ser substituídos pelos parâmetros
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@novoAutor", novoAutor);
                    //executar a query sql
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        public static void AtualizarEditoraLivro()
        {

            //esse método apenas atualiza a Editora de um livro baseado no seu id
            //o ideal é que o usuário consiga atualizar todas as informações do registro do BD
            //o que significa ter um método grande que contempla todas essas atualizações de campos diferentes
            //ou vários métodos, onde cada um lida com um campo do BD

            try
            {

                //ler as informações do teclado da pessoa que o usuário quer atualizar
                Console.WriteLine("Digite o id do livro que vai ter a sua Editora atualizada: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite a nova Editora do livro: ");
                string novaEditora = Console.ReadLine();

                //conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    //criar a query (consulta) sql
                    cmd.CommandText = "UPDATE livros SET editora = @novoAutor WHERE id = @id";
                    //definir os valores que vão ser substituídos pelos parâmetros
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@novaEditora", novaEditora);
                    //executar a query sql
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        
        public static void AtualizarAnoLivro()
        {

            //esse método apenas atualiza a Editora de um livro baseado no seu id
            //o ideal é que o usuário consiga atualizar todas as informações do registro do BD
            //o que significa ter um método grande que contempla todas essas atualizações de campos diferentes
            //ou vários métodos, onde cada um lida com um campo do BD

            try
            {

                //ler as informações do teclado da pessoa que o usuário quer atualizar
                Console.WriteLine("Digite o id do livro que vai ter seu Ano de Publicação atualizado: ");
                int id = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o novo Ano de Publicação do livro: ");
                string novaAno = Console.ReadLine();

                //conexão com o banco de dados 
                using (var cmd = DbConnection().CreateCommand())
                {
                    //criar a query (consulta) sql
                    cmd.CommandText = "UPDATE livros SET anoPublicacao = @novoAno WHERE id = @id";
                    //definir os valores que vão ser substituídos pelos parâmetros
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@novoAno", novaAno);
                    //executar a query sql
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }



        public static void GetLivrosId()
        {

            try
            {
                Console.WriteLine("Digite o id do livro que você quer consultar: ");
                int id = int.Parse(Console.ReadLine());
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM livros WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //aqui não está sendo atribuído na classe, está exibindo diretamente na tela
                            Console.WriteLine("ID: " + reader["id"]);
                            Console.WriteLine("Titulo: " + reader["titulo"]);
                            Console.WriteLine("Autor: " + reader["autor"]);
                            Console.WriteLine("Editora: " + reader["editora"]);
                            Console.WriteLine("Ano de publicação: " + reader["anoPublicacao"]);
                            Console.WriteLine();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

    }
}
