﻿using Domain.Model.Dao;
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
    public class FuncionalidadesRepository : IFuncionalidadesRepository
    {

        private readonly ILogger _logger;
        private readonly string _connectionString;
        private readonly MySqlConnection _connection;

        public FuncionalidadesRepository
            (
                ILogger logger,
                IConfiguration configuration
            )
        {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("conexaoMySQL");
                _connection = new MySqlConnection(_connectionString);
        }
      
        public Response ClientesAtrasados()
        {
            try
            {

                string sqllancamentos = $@"                                
                                        SELECT c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO , l.DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00', (SELECT  NOW()), l.DATA_DEVOLUCAO)), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                        FROM LOCACOES l 
                                        INNER JOIN FILMES f 
                                        ON l.ID_FILME  = f.ID_FILME
                                        INNER JOIN CLIENTES c  
                                        ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 1";


                string sqlcomuns = $@"
                                    SELECT c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO , l.DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00', (SELECT  NOW()), l.DATA_DEVOLUCAO)), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                    FROM LOCACOES l 
                                    INNER JOIN FILMES f 
                                    ON l.ID_FILME  = f.ID_FILME
                                    INNER JOIN CLIENTES c  
                                    ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 0";


                var result = _connection.Query<dynamic>(sqllancamentos);

                var result2 = _connection.Query<dynamic>(sqlcomuns);


                if (result != null)
                {
                    List<Cliente> list = new List<Cliente>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        if (it.TEMPO_LOCACAO >= 2) { 
                        list.Add(new Cliente
                        {
                            nome = it.NOME_CLIENTE,
                            cpf = it.CPF_CLIENTE,
                            dataNascimento  = it.DATA_NASCIMENTO_CLIENTE
                        });
                        }
                    });


                    result.ToList<dynamic>().ForEach(it =>
                    {
                        if (it.TEMPO_LOCACAO >= 3)
                        {
                            list.Add(new Cliente
                            {
                                nome = it.NOME_CLIENTE,
                                cpf = it.CPF_CLIENTE,
                                dataNascimento = it.DATA_NASCIMENTO_CLIENTE
                            });
                        }
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
                _logger.Error(ex, $"[ClienteRepository] Exception in ListarTodosClientes!");
            }

            return null;
        }

        public Response FilmesNaoAlugados()
        {
            try
            {

                string sql = $@"  
                              SELECT ID_FILME , TITULO , CLASSIFICACAO_INDICATIVA , LANCAMENTO  FROM FILMES f 
                              WHERE f.ID_FILME  NOT IN (SELECT ID_FILME  FROM LOCACOES)"; 



                var result = _connection.Query<dynamic>(sql);


                if (result != null)
                {
                    List<Filme> list = new List<Filme>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new Filme
                        {
                            titulo = it.TITULO,
                            classificacaoIndicativa = Convert.ToInt32(it.CLASSIFICACAO_INDICATIVA),
                            lancamento = Convert.ToBoolean(it.LANCAMENTO)

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
                _logger.Error(ex, $"[ClienteRepository] Exception in ListarTodosClientes!");
            }

            return null;
        }

        public Response TopFilmesAlugados()
        {
            try
            {

                string sql = $@"  
                              select l.ID_FILME, f.ID_FILME as ID_FILME,f.TITULO AS TITULO
                                    , f.CLASSIFICACAO_INDICATIVA AS CLASSIFICACAO_INDICATIVA, f.LANCAMENTO AS LANCAMENTO
                                    , count(*) from LOCACOES l
                                    INNER JOIN FILMES f 
                                    ON l.ID_FILME  = f.ID_FILME
                                    WHERE DATA_LOCACAO  BETWEEN NOW() - INTERVAL 360 DAY AND NOW()
                                    group by l.ID_FILME
                                    order by count(*) desc
                                    limit 5";


                var result = _connection.Query<dynamic>(sql);


                if (result != null)
                {
                    List<Filme> list = new List<Filme>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new Filme
                        {
                            titulo = it.TITULO,
                            classificacaoIndicativa = Convert.ToInt32(it.CLASSIFICACAO_INDICATIVA),
                            lancamento = Convert.ToBoolean(it.LANCAMENTO)

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
                _logger.Error(ex, $"[ClienteRepository] Exception in ListarTodosClientes!");
            }

            return null;
        }

        public Response TopMenosAlugados()
        {
            try
            {

                string sql = $@"  
                              select l.ID_FILME, f.ID_FILME as ID_FILME,f.TITULO AS TITULO
                                    , f.CLASSIFICACAO_INDICATIVA AS CLASSIFICACAO_INDICATIVA,
                                    f.LANCAMENTO AS LANCAMENTO , count(*) from LOCACOES l
                                    INNER JOIN FILMES f 
                                    ON l.ID_FILME  = f.ID_FILME
                                    WHERE DATA_LOCACAO  BETWEEN NOW() - INTERVAL 7 DAY AND NOW()
                                    group by l.ID_FILME
                                    order by count(*) ASC                                  
                                    limit 5";

                var result = _connection.Query<dynamic>(sql);


                if (result != null)
                {
                    List<Filme> list = new List<Filme>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new Filme
                        {
                            titulo = it.TITULO,
                            classificacaoIndicativa = Convert.ToInt32(it.CLASSIFICACAO_INDICATIVA),
                            lancamento = Convert.ToBoolean(it.LANCAMENTO)

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
                _logger.Error(ex, $"[ClienteRepository] Exception in TopMenosAlugados!");
            }

            return null;
        }

        public Response SegundoTopCliente()
        {
            try
            {

                string sql = $@"
                                   select l.ID_CLIENTE as ID_CLIENTE, c.NOME AS NOME, c.CPF AS CPF, c.DATA_NASCIMENTO 
   									, count(*) from LOCACOES l
                                    INNER JOIN CLIENTES c 
                                    ON l.ID_CLIENTE  = c.ID_CLIENTE                                 
                                    group by l.ID_CLIENTE 
                                    order by count(*) desc
                                    limit 1 offset 1";


                var result = _connection.Query<dynamic>(sql);


                if (result != null)
                {
                    List<Cliente> list = new List<Cliente>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new Cliente
                        {
                            nome = it.NOME,
                            cpf = it.CPF,
                            dataNascimento = it.DATA_NASCIMENTO
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
                _logger.Error(ex, $"[ClienteRepository] Exception in SegundoTopCliente!");
            }

            return null;
        }
    }

}

