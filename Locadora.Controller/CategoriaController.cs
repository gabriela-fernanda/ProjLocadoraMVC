using Locadora.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Databases;

namespace Locadora.Controller
{
    public class CategoriaController
    {
        public void AdicionarCategoria(Categoria categoria)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using(SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Categoria.INSERTCATEGORIA, connection, transaction);

                    command.Parameters.AddWithValue("@Nome", categoria.Nome);
                    command.Parameters.AddWithValue("@Descricao", categoria.Descricao);
                    command.Parameters.AddWithValue("@Diaria", categoria.Diaria);

                    categoria.SetCategoriaID(Convert.ToInt32(command.ExecuteScalar()));

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar categoria: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar categoria: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Categoria> ListarTodasCategorias()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Categoria.SELECTALLCATEGORIAS, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Categoria> listaCategorias = new List<Categoria>();

                while (reader.Read())
                {
                    var categoria = new Categoria(
                        reader["Nome"].ToString(),
                        reader["Descricao"].ToString(),
                        Convert.ToDecimal(reader["Diaria"])
                    );
                    categoria.SetCategoriaID(Convert.ToInt32(reader["CategoriaID"]));
                    listaCategorias.Add(categoria);
                }
                reader.Close();
                return listaCategorias;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao listar categorias: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar categorias: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public Categoria BuscarCategoriaPorNome(string nome)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand(Categoria.SELECTCATEGORIAPORNOME, connection);

                command.Parameters.AddWithValue("@Nome", nome);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var categoria = new Categoria(
                        reader["Nome"].ToString(),
                        reader["Descricao"].ToString(),
                        Convert.ToDecimal(reader["Diaria"])
                    );
                    categoria.SetCategoriaID(Convert.ToInt32(reader["CategoriaID"]));
                    reader.Close();
                    return categoria;
                }
                else
                {
                    reader.Close();
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar categoria: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar categoria: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void AtualizarCategoria(string nome, Categoria categoriaAtualizada)
        {
            var categoriaEncontrada = BuscarCategoriaPorNome(nome);

            if (categoriaEncontrada is null)
                throw new Exception("Categoria não encontrada.");

            categoriaAtualizada.SetCategoriaID(categoriaEncontrada.CategoriaId);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Categoria.UPDATECATEGORIA, connection, transaction);

                    command.Parameters.AddWithValue("@IdCategoria", categoriaAtualizada.CategoriaId);
                    command.Parameters.AddWithValue("@Nome", categoriaAtualizada.Nome);
                    command.Parameters.AddWithValue("@Descricao", categoriaAtualizada.Descricao);
                    command.Parameters.AddWithValue("@Diaria", categoriaAtualizada.Diaria);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar categoria: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeletarCategoria(string nome)
        {
            var categoriaEncontrada = BuscarCategoriaPorNome(nome);

            if (categoriaEncontrada is null)
                throw new Exception("Categoria não encontrada.");

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Categoria.DELETECATEGORIA, connection, transaction);

                    command.Parameters.AddWithValue("@IdCategoria", categoriaEncontrada.CategoriaId);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar categoria: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar categoria: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


    }
}
