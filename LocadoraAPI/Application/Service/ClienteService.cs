using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Serilog;
using System;

namespace Application.Service
{
    public class ClienteService : IClienteService
    {
        private readonly ILogger _logger;
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(
            ILogger logger,
            IClienteRepository clienteRepository
            )
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
        }

        public Response CadastrarCliente(ClienteRequest clienteRequest)
        {
            try
            {
                var response = _clienteRepository.CadastrarCliente(clienteRequest);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ClienteService] Exception in CadastrarCliente!");
            }
            return null;
        }
        public Response ListarTodosClientes()
        {
            try
            {
                var response = _clienteRepository.ListarTodosClientes();
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ClienteService] Exception in ListarTodosClientes!");
            }
            return null;
        }

        public Response AlterarCliente(ClienteUpdateRequest clienteUpdateRequest)
        {
            try
            {
                var clienteExiste = _clienteRepository.ListarClientePorId(clienteUpdateRequest.idCliente);
                if (clienteExiste.isSuccess == false)
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Cliente não existe."

                    };
                }

                var response = _clienteRepository.AlterarCliente(clienteUpdateRequest);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ClienteService] Exception in AlterarCliente!");
            }
            return null;
        }

        public Response ExcluirCliente(int idCliente)
        {
            try
            {
                var response = _clienteRepository.ExcluirCliente(idCliente);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ClienteService] Exception in ExcluirCliente!");
            }
            return null;
        }

    }
}
