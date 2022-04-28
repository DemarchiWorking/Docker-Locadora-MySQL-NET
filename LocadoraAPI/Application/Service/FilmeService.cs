using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Serilog;
using System;

namespace Application.Service
{
    public class FilmeService : IFilmeService
    {
        private readonly ILogger _logger;
        private readonly IFilmeRepository _filmeRepository;

        public FilmeService(
            ILogger logger,
            IFilmeRepository filmeRepository
            )
        {
            _logger = logger;
            _filmeRepository = filmeRepository;
        }


        public Response CadastrarFilme(FilmeRequest filmeRequest)
        {
            try
            {
                var response = _filmeRepository.CadastrarFilme(filmeRequest);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeService] Exception in CadastrarFilme!");
            }
            return null;
        }
        public Response ListarTodosFilmes()
        {
            try
            {
                var response = _filmeRepository.ListarTodosFilmes();
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeService] Exception in ListarTodosFilmes!");
            }
            return null;
        }

        public Response AlterarFilme(FilmeUpdateRequest filmeUpdateRequest)
        {
            try
            {
                var filmeExiste = _filmeRepository.ListarFilmePorId(filmeUpdateRequest.idFilme);
                if (filmeExiste.isSuccess == false)
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Filme não existe."

                    };
                }

                var response = _filmeRepository.AlterarFilme(filmeUpdateRequest);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeService] Exception in AlterarFilme!");
            }
            return null;
        }

        public Response ExcluirFilme(int idCliente)
        {
            try
            {
                var response = _filmeRepository.ExcluirFilme(idCliente);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FilmeService] Exception in ExcluirFilme!");
            }
            return null;
        }

    }
}
