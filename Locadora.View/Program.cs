using Locadora.Controller;
using Locadora.Models;

Cliente cliente = new Cliente("Novo Cliente Agora Com Transaction", "novo2@email.com");
//Documento documento = new Documento(1, "RG", "123456789", new DateOnly(2020, 1, 1), new DateOnly(2030, 1, 1));

Console.WriteLine(cliente);

var clienteController = new ClienteController();

//try
//{
//    clienteController.AdicionarCliente(cliente);
//}
//catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//try
//{
//    var listaDeClintes = clienteController.ListarTodosClientes();

//    foreach (var clienteDaLista in listaDeClintes)
//    {
//        Console.WriteLine(clienteDaLista);
//    }
//}
//catch(Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

clienteController.AtualizarTelefoneCliente("99999-9999", "ana@a.com");
Console.WriteLine(clienteController.BuscaClientePorEmail("ana@a.com"));