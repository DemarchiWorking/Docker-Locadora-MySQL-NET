using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;


namespace TargetInvestimento.Controllers
{
    [Route("filmes")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IFilmeService _filmeService;


        public FilmeController(
            ILogger logger,
            IFilmeService filmeService

            )
        {
            _logger = logger;
            _filmeService = filmeService;
        }


        [HttpPost("")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> CadastrarFilme(FilmeRequest filmeRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(filmeRequest.titulo))
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o titulo do filme!",
                        Status = 400
                    });
                }


                var response = _filmeService.CadastrarFilme(filmeRequest);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Filme cadastrado com sucesso!",
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel cadastrar o filme!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeController] Exception in CadastrarFilme!");
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
        public ActionResult<Response> ListarTodosFilmes()
        {
            try
            {
                var response = _filmeService.ListarTodosFilmes();

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Lista de filmes encontrada com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar a lista de filmes!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeController] Exception in ListarTodosFilmes!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }


        [HttpPut("{idFilme}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> AlterarFilme([FromRoute] int idFilme,FilmeUpdateRequest filmeUpdateRequest)
        {
            try
            {
                filmeUpdateRequest.idFilme = idFilme;

                if (filmeUpdateRequest.idFilme == 0)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe um id válido !",
                        Status = 400
                    });
                }

                int count = 0;

                if (filmeUpdateRequest.titulo != null)
                {
                    count++;
                }

                if (filmeUpdateRequest.classificacaoIndicativa != null)     // adicionar nullable (sempre vai alterar o campo ) demarchialteracao
                {
                    count++;
                }

                if (filmeUpdateRequest.lancamento != null) // adicionar nullable (sempre vai alterar o campo ) demarchialteracao
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

                var response = _filmeService.AlterarFilme(filmeUpdateRequest);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Filme alterado com sucesso!",
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel alterar o filme! (*Id inválido)",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeController] Exception in AlterarFilme!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }



        [HttpDelete("{idFilme}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> ExcluirFilme([FromRoute] int idFilme)
        {
            try
            {
                if (idFilme == null)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o id do filme!",
                        Status = 400
                    });
                }

                var response = _filmeService.ExcluirFilme(idFilme);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Filme excluido com sucesso!",
                        Status = 200
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel excluir o filme!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeController] Exception in ExcluirFilme!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }

        [HttpGet("{idFilme}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> ListarFilmePorId([FromRoute] int idFilme)
        {
            try
            {
                var response = _filmeService.ListarFilmePorId(idFilme);

                if (response.isSuccess == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Filme encontrado  com sucesso!",
                        Status = 200,
                        List = response.List
                    });
                }
                if (response.isSuccess == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel encontrar filme!",
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeController] Exception in ListarFilmePorId!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500
              });
        }


    }
}
