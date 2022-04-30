using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;


namespace TargetInvestimento.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IClienteService _clienteService;


        public ClienteController(
            ILogger logger,
            IClienteService clienteService

            )
        {
            _logger = logger;
            _clienteService = clienteService;
        }


        [HttpPost("")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> CadastrarCliente(ClienteRequest clienteRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(clienteRequest.nome))
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o nome do cliente!",
                        Status = 400
                    });
                }
                if (string.IsNullOrEmpty(clienteRequest.cpf))
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o CPF do usuário!",
                        Status = 400
                    });
                }
                CPFCNPJ.IMain checkCpfCnpj = new CPFCNPJ.Main();
                if (!checkCpfCnpj.IsValidCPFCNPJ(clienteRequest.cpf))
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe um Documento válido!",
                        Status = 400
                    });
                }

                var response = _clienteService.CadastrarCliente(clienteRequest);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Cliente cadastrado com sucesso!",
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel cadastrar o cliente!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ClienteController] Exception in CadastrarCliente!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> ListarTodosClientes()
            {
            try
            {
                var response = _clienteService.ListarTodosClientes();

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de clientes encontrada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar a lista de clientes!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ClienteController] Exception in ListarTodosClientes!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

        [HttpPut("")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> AlterarCliente(ClienteUpdateRequest clienteUpdateRequest)
        {
            try
            {
                if (clienteUpdateRequest.idCliente == 0)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe um id válido !",
                        Status = 400
                    });
                }
                int count = 0;

                if (clienteUpdateRequest.nome != null)
                {
                    count++;
                }

                if (clienteUpdateRequest.cpf != null)
                {
                    count++;
                }

                if (clienteUpdateRequest.dataNascimento != null)
                {
                    count++;
                }

                if (count < 1)
                {
                    return NotFound(new Response()
                    {
                        Status = 400,
                        Title = "Informe algum parametro para ser alterado.",
                        isSuccess = false
                    });
                }

                var response = _clienteService.AlterarCliente(clienteUpdateRequest);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Usuário alterado com sucesso!",
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel alterar o usuário! (*Id inválido)",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ClienteController] Exception in AlterarCliente!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }


        [HttpDelete("")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> ExcluirCliente(int idCliente)
        {
            try
            {
                if (idCliente == null)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o id do cliente!",
                        Status = 400
                    });
                }

                var response = _clienteService.ExcluirCliente(idCliente);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Usuário excluido com sucesso!",
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel excluir o usuário!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[ClienteController] Exception in ExcluirCliente!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

    }
}
