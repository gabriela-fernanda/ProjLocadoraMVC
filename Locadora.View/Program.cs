using Locadora.Controller;
using Locadora.Models;

#region ClienteDocumento
//Cliente cliente = new Cliente("Novo Cliente Agora Com Documento", "documento@email.com");
//Documento documento = new Documento("RG", "123456789", new DateOnly(2020, 1, 1), new DateOnly(2030, 1, 1));

//Console.WriteLine(cliente);

//var clienteController = new ClienteController();

//try
//{
//    clienteController.AdicionarCliente(cliente, documento);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//try
//{
//    clienteController.AtualizarDocumentoCliente('', documento);
//}
//catch (Exception ex)
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
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//clienteController.AtualizarTelefoneCliente("99999-9999", "ana@a.com");
//Console.WriteLine(clienteController.BuscaClientePorEmail("ana@a.com"));

//clienteController.DeletarCliente("ana@a.com");
#endregion

#region categoria
Categoria categoria = new Categoria("Categoria de Teste", "Categoria criada para teste", 350.00m);

var categoriaController = new CategoriaController();


//try
//{
//    categoriaController.AdicionarCategoria(categoria);
//    Console.WriteLine("Categoria adicionada com sucesso!");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//try
//{
//    var listaCategorias = categoriaController.ListarTodasCategorias();

//    Console.WriteLine("\n--- LISTA DE CATEGORIAS ---\n");

//    foreach (var cat in listaCategorias)
//    {
//        Console.WriteLine(cat);
//        Console.WriteLine("--------------------");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//try
//{
//    categoriaController.BuscarCategoriaPorNome("SUV");

//    if (categorias != null)
//    {
//        Console.WriteLine(categorias);
//    }
//    else
//    {
//        Console.WriteLine("Categoria não encontrada.");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//try
//{
//    var cat = categoriaController.BuscarCategoriaPorNome("SUV");

//    if (cat != null)
//    {
//        var categoriaAtualizada = new Categoria(
//            "SUV Premium",
//            "SUV atualizado – mais conforto",
//            250m
//        );
//        categoriaController.AtualizarCategoria("SUV", categoriaAtualizada);

//        Console.WriteLine("categoria atualizada com sucesso);
//    }
//    else
//    {
//        Console.WriteLine("categoria não encontrada);
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

try
{
    categoriaController.DeletarCategoria("Categoria de Teste");
    Console.WriteLine("Categoria deletada com sucesso");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

#endregion