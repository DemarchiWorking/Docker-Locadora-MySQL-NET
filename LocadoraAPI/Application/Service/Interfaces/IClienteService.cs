using Domain.Model.Request;
using Domain.Model.Response;

namespace Application.Service.Interfaces
{
    public interface IClienteService
    {
        Response CadastrarCliente(ClienteRequest clienteRequest);
        Response ListarTodosClientes();
        Response AlterarCliente(ClienteUpdateRequest clienteUpdateRequest);
        Response ExcluirCliente(int idCliente);


    }
}