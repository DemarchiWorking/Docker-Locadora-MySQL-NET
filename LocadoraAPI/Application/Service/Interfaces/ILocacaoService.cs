using Domain.Model.Request;
using Domain.Model.Response;

namespace Application.Service.Interfaces
{
    public interface ILocacaoService
    {
        Response CadastrarLocacao(LocacaoRequest locacaoRequest);
        Response ListarTodasLocacoes();
        Response AlterarLocacoes(LocacaoUpdateRequest locacaoUpdateRequest);
        Response ExcluirLocacao(int idLocadora);


    }
}
