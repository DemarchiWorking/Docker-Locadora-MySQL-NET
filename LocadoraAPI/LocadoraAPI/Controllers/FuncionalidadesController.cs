using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;


namespace TargetInvestimento.Controllers
{
    [Route("funcionalidades")]
    [ApiController]
    public class FuncionalidadesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IFuncionalidadesService _funcionalidadesService;


        public FuncionalidadesController(
            ILogger logger,
            IFuncionalidadesService funcionalidadesService

            )
        {
            _logger = logger;
            _funcionalidadesService = funcionalidadesService;
        }

        [HttpGet("atrasados")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> ListarTodosClientes()
        {
            try
            {
                var response = _funcionalidadesService.ClientesAtrasados();

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de clientes atrasados encontrada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar a lista de clientes atrasados!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesController] Exception in ListarTodosClientes!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

        [HttpGet("nao-alugados")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> FilmesNaoAlugados()
        {
            try
            {
                var response = _funcionalidadesService.FilmesNaoAlugados();

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de filmes não alugados encontrada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar a lista de filmes não alugados!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesController] Exception in FilmesNaoAlugados!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

        [HttpGet("top-alugados")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> TopFilmesAlugados()
        {
            try
            {
                var response = _funcionalidadesService.TopFilmesAlugados();

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de filme mais alugados encontrada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar a lista de filmes mais alugados!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesController] Exception in TopFilmesAlugados!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

        [HttpGet("menos-alugados")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> TopMenosAlugados()
        {
            try
            {
                var response = _funcionalidadesService.MenosAlugados();

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de filme menos alugados encontrada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar a lista de filmes menos alugados!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesController] Exception in TopFilmesAlugados!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

        [HttpGet("segundo-melhor-cliente")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> SegundoTopCliente()
        {
            try
            {
                var response = _funcionalidadesService.SegundoTopCliente();

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de filme mais alugados encontrada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar a lista de filmes mais alugados!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesController] Exception in TopFilmesAlugados!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }
    }
}
