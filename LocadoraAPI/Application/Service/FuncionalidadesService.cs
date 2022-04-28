using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Serilog;
using System;

namespace Application.Service
{
    public class FuncionalidadesService : IFuncionalidadesService
    {
        private readonly ILogger _logger;
        private readonly IFuncionalidadesRepository _funcionalidadesRepository;

        public FuncionalidadesService(
            ILogger logger,
            IFuncionalidadesRepository funcionalidadesRepository
            )
        {
            _logger = logger;
            _funcionalidadesRepository = funcionalidadesRepository;
        }

        public Response ClientesAtrasados()
        {
            try
            {
                var response = _funcionalidadesRepository.ClientesAtrasados();
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesService] Exception in ClientesAtrasados!");
            }
            return null;
        }


        public Response FilmesNaoAlugados()
        {
            try
            {
                var response = _funcionalidadesRepository.FilmesNaoAlugados();
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesService] Exception in ClientesAtrasados!");
            }
            return null;
        }

        public Response TopFilmesAlugados()
        {
            try
            {
                var response = _funcionalidadesRepository.TopFilmesAlugados();
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesService] Exception in ClientesAtrasados!");
            }
            return null;
        }

        public Response TopMenosAlugados()
        {
            try
            {
                var response = _funcionalidadesRepository.TopMenosAlugados();
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesService] Exception in TopMenosAlugados!");
            }
            return null;
        }

        public Response SegundoTopCliente()
        {
            try
            {
                var response = _funcionalidadesRepository.SegundoTopCliente();
                return response;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[FuncionalidadesService] Exception in SegundoTopCliente!");
            }
            return null;
        }

    }
}
