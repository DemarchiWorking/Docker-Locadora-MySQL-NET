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
    public class FilmeRepository : IFilmeRepository
    {

        private readonly ILogger _logger;
        private readonly string _connectionString;
        private readonly MySqlConnection _connection;

        public FilmeRepository
            (
                ILogger logger,
                IConfiguration configuration
            )
        {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("conexaoMySQL");
                _connection = new MySqlConnection(_connectionString);
        }


        public Response CadastrarFilme(FilmeRequest filmeRequest)
        {
            try
            {
                string query = $@"
                                    INSERT INTO FILMES
                                     (
                                                    TITULO
                                                    , CLASSIFICACAO_INDICATIVA
                                                    , LANCAMENTO
                                                    )
                                                    VALUES
                                                    (
                                                    '{filmeRequest.titulo}'
                                                    , {filmeRequest.classificacaoIndicativa}
                                                    , '{Convert.ToInt32(filmeRequest.lancamento)}'
                                      )";

                var result = _connection.Execute(query);

                if (result != 0) { 
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
                _logger.Error(ex, $"[FilmeRepository] Exception in CadastrarFilme!");

            }

            return null;
        }

        public Response ListarTodosFilmes()
        {
            try
            {

                string sql = $@"SELECT
                                        ID_FILME
		                                , TITULO
		                                , CLASSIFICACAO_INDICATIVA
		                                , LANCAMENTO
                               FROM FILMES;";

                var result = _connection.Query<dynamic>(sql);

                if (result != null)
                {
                    List<TopAlugados> list = new List<TopAlugados>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new TopAlugados
                        {
                            idFilme = Convert.ToInt32(it.ID_FILME),
                            titulo = Convert.ToString(it.TITULO),
                            classificacaoIndicativa = Convert.ToInt32(it.CLASSIFICACAO_INDICATIVA),
                            lancamento = Convert.ToBoolean(it.LANCAMENTO)

                        });
                    });

                    return new Response()
                    {
                        List = result,
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
                _logger.Error(ex, $"[FilmeRepository] Exception in ListarTodosFilmes!");
            }

            return null;
        }

        public Response AlterarFilme(FilmeUpdateRequest filmeUpdateRequest)
        {
            try
            {

                string sql = $@"                
                                 UPDATE FILMES SET ";
                                 if (filmeUpdateRequest.titulo != null)
                                {
                                    sql += $"TITULO = '{filmeUpdateRequest.titulo}' ";
                                    if(filmeUpdateRequest.classificacaoIndicativa != null || filmeUpdateRequest.lancamento != null)
                                    {
                                        sql += " , ";
                                    }
                                }
                                  if (filmeUpdateRequest.classificacaoIndicativa != null)
                                {
                                   sql += $"CLASSIFICACAO_INDICATIVA = {filmeUpdateRequest.classificacaoIndicativa} ";
                                   if(filmeUpdateRequest.classificacaoIndicativa != null || filmeUpdateRequest.lancamento != null)
                                   {
                                        sql += " , ";
                                   }
                                }
                                   if (filmeUpdateRequest.lancamento != null)
                                {
                                    sql += $"LANCAMENTO = '{Convert.ToInt32(filmeUpdateRequest.lancamento)}' ";
                                }
                                sql += $"WHERE ID_FILME = { filmeUpdateRequest.idFilme}; ";
                                
                                            

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
                _logger.Error(ex, $"[FilmeRepository] Exception in AlterarFilme!");
            }

            return new Response()
            {
                isSuccess = false
            };

        

    }


        public Response ExcluirFilme(int idFilme)
        {
            try
            {

                string sql = $@"DELETE FROM FILMES WHERE ID_FILME = {idFilme};";

                var result = _connection.Query(sql);

                if (result != null)
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
                _logger.Error(ex, $"[FilmeRepository] Exception in ExcluirFilme!");
            }

            return null;
        }

        public Response ListarFilmePorId(int idFilme)
        {
            try
            {

                string sql = $@"SELECT
                                        ID_FILME,
		                                TITULO,
		                                CLASSIFICACAO_INDICATIVA,
		                                LANCAMENTO
                               FROM FILMES WHERE ID_FILME = {idFilme};";

                var result = _connection.Query<dynamic>(sql);

                if (result != null && result.AsList().Count != 0)
                {
                    List<TopAlugados> list = new List<TopAlugados>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new TopAlugados
                        {
                            titulo = it.TITULO,
                            classificacaoIndicativa = Convert.ToInt32(it.CLASSIFICACAO_INDICATIVA),
                            lancamento = Convert.ToBoolean(it.LANCAMENTO)

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
                _logger.Error(ex, $"[FilmeRepository] Exception in ExisteFilme!");
            }
            return new Response()
            {
                isSuccess = false
            };
        }


    }
}
