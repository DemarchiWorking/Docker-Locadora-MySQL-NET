using Domain.Model.Request;
using Domain.Model.Response;

namespace Application.Service.Interfaces
{
    public interface IFuncionalidadesService
    {
        Response ClientesAtrasados();
        Response FilmesNaoAlugados();
        Response TopFilmesAlugados();
        Response MenosAlugados();
        Response SegundoTopCliente();

    }
}