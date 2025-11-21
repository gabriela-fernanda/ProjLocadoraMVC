using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locadora.Models
{
    public class Categoria
    {
        public readonly static string INSERTCATEGORIA = "INSERT INTO tblCategorias (Nome, Descricao, Diaria) VALUES (@Nome, @Descricao, @Diaria);" +
                                                  "SELECT SCOPE_IDENTITY();";

        public readonly static string SELECTALLCATEGORIAS = "SELECT CategoriaID, Nome, Descricao, Diaria FROM tblCategorias;";

        public readonly static string SELECTCATEGORIAPORNOME = @"SELECT CategoriaID, Nome, Descricao, Diaria 
                                                                FROM tblCategorias 
                                                                WHERE Nome = @Nome;";

        public readonly static string UPDATECATEGORIA = @"UPDATE tblCategorias 
                                                         SET Nome = @Nome, 
                                                         Descricao = @Descricao, 
                                                         Diaria = @Diaria 
                                                         WHERE CategoriaID = @IdCategoria;";

        public readonly static string DELETECATEGORIA = "DELETE FROM tblCategorias WHERE CategoriaID = @IdCategoria;";

        public int CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Diaria { get; private set; }

        public Categoria(string nome, string descricao, decimal diaria)
        {
            Nome = nome;
            Descricao = descricao;
            Diaria = diaria;
        }

        public void SetCategoriaID(int id)
        {
            CategoriaId = id;
        }

        public override string ToString()
        {
            return $"Categoria: {Nome}\nDescrição: {Descricao}\nDiária: {Diaria:C}"; ;
        }
    }
}
