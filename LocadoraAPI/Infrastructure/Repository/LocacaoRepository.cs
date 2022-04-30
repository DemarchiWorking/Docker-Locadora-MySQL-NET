using Domain.Model.Dao;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Serilog;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repository
{
    public class LocacaoRepository : ILocacaoRepository
    {

        private readonly ILogger _logger;
        private readonly string _connectionString;
        private readonly MySqlConnection _connection;

        public LocacaoRepository
            (
                ILogger logger,
                IConfiguration configuration
            )
        {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("conexaoMySQL");
                _connection = new MySqlConnection(_connectionString);
        }

        

        public Response CadastrarLocacao(LocacaoRequest locacaoRequest)
        {
            try
            {
                string query = $@"
                                    INSERT INTO LOCACOES
                                     (
                                                    ID_CLIENTE
                                                    , ID_FILME
                                                    , DATA_LOCACAO
                                                    , DATA_DEVOLUCAO
                                                    )
                                                    VALUES
                                                    (
                                                    {locacaoRequest.idCliente}      
                                                    , {locacaoRequest.idFilme}
                                                    , '{Convert.ToDateTime(locacaoRequest.dataLocacao).ToString("yyyy/MM/dd HH:mm:ss")}'
                                                    , '{Convert.ToDateTime(locacaoRequest.dataDevolucao).ToString("yyyy/MM/dd HH:mm:ss")}'
                                      )";
                                                                    // alterar a data de criacao para adicionar a data da criacao do post demarchialteracao

                var result = _connection.Execute(query);

                if (result != 0) {
                    return new Response()
                    {
                        isSuccess = true,
                        Title = "Locação cadastrada com sucesso !"
                    };
                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Não foi possivel cadastrar locação !"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoRepository] Exception in CadastrarLocacao!");

            }

            return null;
        }

        public Response ListarTodasLocacoes()
        {
            try
            {

                string sql = $@"SELECT
                                        ID_LOCACAO
                                        , ID_CLIENTE
		                                , ID_FILME
		                                , DATA_LOCACAO
		                                , DATA_DEVOLUCAO
                               FROM LOCACOES;";

                var result = _connection.Query<dynamic>(sql);

                if (result != null)
                {
                    List<LocacaoReturn> list = new List<LocacaoReturn>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new LocacaoReturn
                        {
                            idLocacao = Convert.ToInt32(it.ID_LOCACAO),
                            idCliente = Convert.ToInt32(it.ID_CLIENTE),
                            idFilme = Convert.ToInt32(it.ID_FILME),
                            dataDevolucao =it.DATA_DEVOLUCAO,
                            dataLocacao = it.DATA_LOCACAO
                        });
                    });


                    return new Response()
                    {
                        List = list,
                        isSuccess = true                       
                    };
                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoRepository] Exception in ListarTodasLocacoes!");
            }

            return null;
        }


        public Response AlterarLocacoes(LocacaoUpdateRequest locacaoUpdateRequest)
        {
            try
            {
                string sql = $@"                
                                 UPDATE LOCACOES SET ";
                                     if (locacaoUpdateRequest.idCliente != null)
                                {
                                    sql += $"ID_CLIENTE = {locacaoUpdateRequest.idCliente} ";
                                    if(locacaoUpdateRequest.idFilme != null || locacaoUpdateRequest.dataLocacao != null || locacaoUpdateRequest.dataDevolucao != null)
                                    {
                                        sql += " , ";
                                    }
                                }
                                     if (locacaoUpdateRequest.idFilme != null)
                                {
                                    sql += $"ID_FILME = {locacaoUpdateRequest.idFilme} ";
                                    if(locacaoUpdateRequest.dataLocacao != null || locacaoUpdateRequest.dataDevolucao != null)
                                    {
                                        sql += " , ";
                                    }
                                }
                                     if (locacaoUpdateRequest.dataLocacao != null)
                                {
                                    sql += $"DATA_LOCACAO = '{Convert.ToDateTime(locacaoUpdateRequest.dataLocacao).ToString("yyyy/MM/dd HH:mm:ss")}' ";
                                    if(locacaoUpdateRequest.dataDevolucao != null)
                                    {
                                        sql += " , ";
                                    }
                                }
                                     if (locacaoUpdateRequest.dataDevolucao != null)
                                {
                                    sql += $"DATA_DEVOLUCAO = '{Convert.ToDateTime(locacaoUpdateRequest.dataDevolucao).ToString("yyyy/MM/dd HH:mm:ss")}' ";
                                }
                                sql += $"WHERE ID_LOCACAO = { locacaoUpdateRequest.idLocacao}; ";





                var result = _connection.Execute(sql);

                if (result != 0)
                {
                    return new Response()
                    {
                        isSuccess = true,
                        Title = "Locação atualizada com sucesso !"
                    };
                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false,
                        Title = "Não foi possivel atualizar locação !"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoRepository] Exception in AlterarLocacao!");
            }

            return new Response()
            {
                isSuccess = false
            };

        

    }
        public Response ExcluirLocacao(int idLocacao)
        {
            try
            {

                string sql = $@"DELETE FROM LOCACOES WHERE ID_LOCACAO = {idLocacao};";

                var result = _connection.Execute(sql);

                if (result != 0)
                {
                    return new Response()
                    {
                        isSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoRepository] Exception in ExcluirLocacao!");
            }

            return null;
        }

        public Response ListarLocacaoPorId(int idLocacao)
        {
            try
            {

                string sql = $@"SELECT
                                        ID_LOCACAO
		                                , ID_CLIENTE
		                                , ID_FILME
		                                , DATA_LOCACAO
                                        , DATA_DEVOLUCAO
                               FROM LOCACOES WHERE ID_LOCACAO = {idLocacao};";

                var result = _connection.Query<dynamic>(sql);


                if (result != null && result.AsList().Count != 0)
                {
                    List<LocacaoReturn> list = new List<LocacaoReturn>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new LocacaoReturn
                        {
                            idLocacao = Convert.ToInt32(it.ID_LOCACAO),
                            idCliente = Convert.ToInt32(it.ID_CLIENTE),
                            idFilme = Convert.ToInt32(it.ID_FILME),
                            dataDevolucao = it.DATA_DEVOLUCAO,
                            dataLocacao = it.DATA_LOCACAO
                        });
                    });
                    return new Response()
                    {
                        List = result,
                        isSuccess = true,

                    };
                }
                else
                {
                    return new Response()
                    {
                        isSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocacaoRepository] Exception in ListarLocacaoPorId!");
            }
            return new Response()
            {
                isSuccess = false,

            };
        }
    }

}
