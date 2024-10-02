using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca2024.Model
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int AnoPublicacao { get; set; }

        public Livro(int Id, string Titulo, string Autor,
                       string Editora, int AnoPublicacao)
        {
            this.Id = Id;
            this.Titulo = Titulo;
            this.Autor = Autor;
            this.Editora = Editora;
            this.AnoPublicacao = AnoPublicacao;
        }

    }
}
