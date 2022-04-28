using Domain.Model.Request;
using Domain.Model.Response;

namespace Infrastructure.Repository.Interfaces
{
    public interface ILocacaoRepository
    {
        Response CadastrarLocacao(LocacaoRequest locacaoRequest);
        Response ListarTodasLocacoes();
        Response AlterarLocacoes(LocacaoUpdateRequest locacaoUpdateRequest);
        Response ExcluirLocacao(int idLocacao);
        Response ListarLocacaoPorId(int idLocacao);

    }
}
