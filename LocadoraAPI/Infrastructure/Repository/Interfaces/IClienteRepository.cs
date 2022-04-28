using Domain.Model.Request;
using Domain.Model.Response;

namespace Infrastructure.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Response CadastrarCliente(ClienteRequest clienteRequest);
        Response ListarTodosClientes();
        Response AlterarCliente(ClienteUpdateRequest clienteUpdateRequest);
        Response ExcluirCliente(int idCliente);
        Response ListarClientePorId(int idCliente);

    }
}
