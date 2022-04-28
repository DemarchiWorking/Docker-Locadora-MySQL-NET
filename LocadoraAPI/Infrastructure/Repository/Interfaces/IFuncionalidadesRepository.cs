using Domain.Model.Request;
using Domain.Model.Response;

namespace Infrastructure.Repository.Interfaces
{
    public interface IFuncionalidadesRepository
    {
        Response ClientesAtrasados();
        Response FilmesNaoAlugados();
        Response TopFilmesAlugados();
        Response TopMenosAlugados();
        Response SegundoTopCliente();
    }
}
