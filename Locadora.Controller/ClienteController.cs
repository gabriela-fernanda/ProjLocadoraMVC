using Locadora.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;

namespace Locadora.Controller
{
    public class ClienteController
    {
        public void AdicionarCliente(Cliente cliente)
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.INSERTCLIENTE, connection, transaction);

                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);

                    cliente.SetClienteID(Convert.ToInt32(command.ExecuteScalar()));

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao adicionar cliente: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Cliente> ListarTodosClientes()
        {
            var connection = new SqlConnection(ConnectionDB.GetConnectionString());

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(Cliente.SELECTALLCLIENTES, connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Cliente> listaClientes = new List<Cliente>();

                while (reader.Read())
                {
                    var cliente = new Cliente(
                        reader["Nome"].ToString()!,
                        reader["Email"].ToString()!,
                        reader["Telefone"] != DBNull.Value ? reader["Telefone"].ToString() : null
                    );
                    cliente.SetClienteID(Convert.ToInt32(reader["ClienteID"]));
                    listaClientes.Add(cliente);
                }
                reader.Close();
                return listaClientes;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao listar clientes: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar clientes: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public Cliente BuscaClientePorEmail(string email)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            try
            {
                SqlCommand command = new SqlCommand(Cliente.SELECTCLIENTEPOREMAIL, connection);

                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var cliente = new Cliente(
                        reader["Nome"].ToString()!,
                        reader["Email"].ToString()!,
                        reader["Telefone"] != DBNull.Value ? reader["Telefone"].ToString() : null
                    );
                    cliente.SetClienteID(Convert.ToInt32(reader["ClienteID"]));
                    reader.Close();
                    return cliente;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao buscar cliente por email: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar cliente por email: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void AtualizarTelefoneCliente(string telefone, string email)
        {
            var clienteEnconntrado = this.BuscaClientePorEmail(email);

            if (clienteEnconntrado is null)
                throw new Exception("Cliente não encontrado.");

            clienteEnconntrado.SetTelefone(telefone);

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.UPDATEFONECLIENTE, connection, transaction);

                    command.Parameters.AddWithValue("@Telefone", clienteEnconntrado.Telefone);
                    command.Parameters.AddWithValue("@IdCliente", clienteEnconntrado.ClienteID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar telefone do cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao atualizar telefone do cliente: " + ex.Message);
                }
                finally
                {
                    connection.Close();

                }
            }
        }

        public void DeletarCliente(string email)
        {
            var clienteEncontrado = this.BuscaClientePorEmail(email);

            if (clienteEncontrado is null)
                throw new Exception("Cliente não encontrado.");

            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());

            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand command = new SqlCommand(Cliente.DELETECLIENTE, connection, transaction);
                    command.Parameters.AddWithValue("@IdCliente", clienteEncontrado.ClienteID);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar cliente: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao deletar cliente: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
