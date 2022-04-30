using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;


namespace TargetInvestimento.Controllers
{
    [Route("locacoes")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ILocacaoService _locacaoService;


        public LocacaoController(
            ILogger logger,
            ILocacaoService locacaoService

            )
        {
            _logger = logger;
            _locacaoService = locacaoService;
        }


        [HttpPost("")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> CadastrarLocacao(LocacaoRequest locacaoRequest)
        {
            try
            {
                if (locacaoRequest.idCliente == null)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o id do cliente!",
                        Status = 400
                    });
                }
                if (locacaoRequest.idFilme == null)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o id do filme!",
                        Status = 400
                    });
                }
                if (locacaoRequest.dataLocacao == null)
                {
                    locacaoRequest.dataLocacao = DateTime.Now;
                }


                var response = _locacaoService.CadastrarLocacao(locacaoRequest);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = response.Title,
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = response.Title,
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoController] Exception in CadastrarLocacao!");
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
        public ActionResult<Response> ListarTodasLocacoes()
        {
            try
            {
                var response = _locacaoService.ListarTodasLocacoes();

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de locações localizada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel localizar a lista de locacões!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoController] Exception in ListarTodasLocacoes!");
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
        public ActionResult<Response> AlterarLocacoes(LocacaoUpdateRequest locacaoUpdateRequest)
        {
            try
            {
                if (locacaoUpdateRequest.idLocacao == 0)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe um id válido !",
                        Status = 400
                    });
                }

                int count = 0;

                if (locacaoUpdateRequest.idFilme != null)
                {
                    count++;
                }

                if (locacaoUpdateRequest.idCliente != null)    
                {
                    count++;
                }

                if (locacaoUpdateRequest.dataLocacao != null) 
                {
                    count++;
                } 
                
                if (locacaoUpdateRequest.dataDevolucao != null) 
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

                var response = _locacaoService.AlterarLocacoes(locacaoUpdateRequest);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = response.Title,
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = response.Title,
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoController] Exception in AlterarLocacoes!");
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
        public ActionResult<Response> ExcluirLocacao(int idLocacao)
        {
            try
            {
                if (idLocacao == 0)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o id de locação válido!",
                        Status = 400
                    });
                }


                var response = _locacaoService.ExcluirLocacao(idLocacao);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Locação excluida com sucesso!",
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel excluir o locação!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoController] Exception in ExcluirLocacao!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

    }
}
