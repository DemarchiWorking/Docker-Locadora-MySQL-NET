using Domain.Model.Request;
using Domain.Model.Response;

namespace Application.Service.Interfaces
{
    public interface IFilmeService
    {
        Response CadastrarFilme(FilmeRequest filmeRequest);
        Response ListarTodosFilmes();
        Response AlterarFilme(FilmeUpdateRequest filmeUpdateRequest);
        Response ExcluirFilme(int idFilme);
        Response ListarFilmePorId(int idFilme);


    }
}


