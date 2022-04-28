using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Serilog;
using System;

namespace Application.Service
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILogger _logger;
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFilmeRepository _filmeRepository;

        public LocacaoService(
            ILogger logger,
            ILocacaoRepository locacaoRepository,
            IClienteRepository clienteRepository,
            IFilmeRepository filmeRepository
            )
        {
            _logger = logger;
            _locacaoRepository = locacaoRepository;
            _filmeRepository = filmeRepository;
            _clienteRepository = clienteRepository;
        }

        public Response CadastrarLocacao(LocacaoRequest locacaoRequest)
        {
            try
            {

                var clienteExiste = _clienteRepository.ListarClientePorId(locacaoRequest.idCliente.GetValueOrDefault());
                if (clienteExiste.isSuccess == false)
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Cliente não existe."
                    };
                }

                var filmeExiste = _filmeRepository.ListarFilmePorId(locacaoRequest.idFilme.GetValueOrDefault());
                if (filmeExiste.isSuccess == false)
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Filme não existe."

                    };
                }

                var response = _locacaoRepository.CadastrarLocacao(locacaoRequest);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoService] Exception in CadastrarLocacao!");
            }
            return null;
        }
        public Response ListarTodasLocacoes()
        {
            try
            {
                var response = _locacaoRepository.ListarTodasLocacoes();
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoService] Exception in ListarTodasLocacoes!");
            }
            return null;
        }

        public Response AlterarLocacoes(LocacaoUpdateRequest locacaoUpdateRequest)
        {
            try
            {
                var clienteExiste = _clienteRepository.ListarClientePorId(locacaoUpdateRequest.idCliente.GetValueOrDefault());
                if (clienteExiste.isSuccess == false)
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Cliente não existe."
                    };
                }

                var filmeExiste = _filmeRepository.ListarFilmePorId(locacaoUpdateRequest.idFilme.GetValueOrDefault());
                if (filmeExiste.isSuccess == false)
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Filme não existe."

                    };
                }

                var LocacaoExiste = _locacaoRepository.ListarLocacaoPorId(locacaoUpdateRequest.idLocacao);
                if (LocacaoExiste.isSuccess == false)
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Cliente não existe."
                    };
                }

                var response = _locacaoRepository.AlterarLocacoes(locacaoUpdateRequest);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoService] Exception in AlterarLocacoes!");
            }
            return null;
        }

        public Response ExcluirLocacao(int idLocadora)
        {
            try
            {
                var response = _locacaoRepository.ExcluirLocacao(idLocadora);
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoService] Exception in ExcluirLocacao!");
            }
            return null;
        }

    }
}
