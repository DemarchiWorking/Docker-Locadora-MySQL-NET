using Domain.Model.Request;
using Domain.Model.Response;

namespace Infrastructure.Repository.Interfaces
{
    public interface IFilmeRepository
    {
        Response CadastrarFilme(FilmeRequest filmeRequest);
        Response ListarTodosFilmes();
        Response AlterarFilme(FilmeUpdateRequest filmeUpdateRequest);
        Response ExcluirFilme(int idFilme);
        Response ListarFilmePorId(int idFilme);
    }
}
