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
                /*
                string sqllancamentos = $@"                                
                                        SELECT f.ID_FILME AS ID_FILME, f.TITULO AS TITULO, c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO AS DATA_LOCACAO, l.DATA_DEVOLUCAO AS DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00', (SELECT  NOW()), l.DATA_DEVOLUCAO)), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                        FROM LOCACOES l 
                                        INNER JOIN FILMES f 
                                        ON l.ID_FILME  = f.ID_FILME
                                        INNER JOIN CLIENTES c  
                                        ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 1
                                        ORDER BY l.DATA_LOCACAO ;";


                string sqlcomuns = $@"
                                    SELECT f.ID_FILME AS ID_FILME, f.TITULO AS TITULO, c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO AS DATA_LOCACAO, l.DATA_DEVOLUCAO AS DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00', (SELECT  NOW()), l.DATA_DEVOLUCAO)), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                    FROM LOCACOES l 
                                    INNER JOIN FILMES f 
                                    ON l.ID_FILME  = f.ID_FILME
                                    INNER JOIN CLIENTES c  
                                    ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 0
                                    ORDER BY l.DATA_LOCACAO ;";
             
                string sqllancamentos = $@"                                
                                        SELECT f.ID_FILME AS ID_FILME, f.TITULO AS TITULO, c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO AS DATA_LOCACAO, l.DATA_DEVOLUCAO AS DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00', (SELECT  NOW()), l.DATA_DEVOLUCAO)), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                        FROM LOCACOES l 
                                        INNER JOIN FILMES f 
                                        ON l.ID_FILME  = f.ID_FILME
                                        INNER JOIN CLIENTES c  
                                        ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 1
                                        AND l.DATA_LOCACAO 
                                        <= ((
                                        SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00',
                                        (SELECT  NOW()), l.DATA_DEVOLUCAO)))
                                        ORDER BY l.DATA_LOCACAO ;";


                string sqlcomuns = $@"
                                    SELECT f.ID_FILME AS ID_FILME, f.TITULO AS TITULO, c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO AS DATA_LOCACAO, l.DATA_DEVOLUCAO AS DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00', (SELECT  NOW()), l.DATA_DEVOLUCAO)), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                    FROM LOCACOES l 
                                    INNER JOIN FILMES f 
                                    ON l.ID_FILME  = f.ID_FILME
                                    INNER JOIN CLIENTES c  
                                    ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 0
                                    AND l.DATA_LOCACAO 
                                    <= ((
                                    SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00',
                                    (SELECT  NOW()), l.DATA_DEVOLUCAO)))
                                    ORDER BY l.DATA_LOCACAO ;";
                  
                
                // DIAS DE ATRASO APARTIR DA DATA DE HOJE
                */
                string sqllancamentos = $@"                                
                                        SELECT l.ID_LOCACAO AS ID_LOCACAO, f.ID_FILME AS ID_FILME, f.TITULO AS TITULO, c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO AS DATA_LOCACAO, l.DATA_DEVOLUCAO AS DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT  NOW()), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                        FROM LOCACOES l 
                                        INNER JOIN FILMES f 
                                        ON l.ID_FILME  = f.ID_FILME
                                        INNER JOIN CLIENTES c  
                                        ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 1
                                        AND l.DATA_LOCACAO 
                                        <= ((
                                        SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00',
                                        (SELECT  NOW()), l.DATA_DEVOLUCAO)))
                                        ORDER BY l.DATA_LOCACAO ";



                string sqlcomuns = $@"
                                    SELECT l.ID_LOCACAO AS ID_LOCACAO, f.ID_FILME AS ID_FILME, f.TITULO AS TITULO, c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO AS DATA_LOCACAO, l.DATA_DEVOLUCAO AS DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT  NOW()), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                    FROM LOCACOES l 
                                    INNER JOIN FILMES f 
                                    ON l.ID_FILME  = f.ID_FILME
                                    INNER JOIN CLIENTES c  
                                    ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 0
                                    AND l.DATA_LOCACAO 
                                    <= ((
                                    SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00',
                                    (SELECT  NOW()), l.DATA_DEVOLUCAO)))
                                    ORDER BY l.DATA_LOCACAO ";
                
                /*
                string sqllancamentos = $@"                                
                                        SELECT l.ID_LOCACAO AS ID_LOCACAO, f.ID_FILME AS ID_FILME, f.TITULO AS TITULO, c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO AS DATA_LOCACAO, l.DATA_DEVOLUCAO AS DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00', (SELECT  NOW()), l.DATA_DEVOLUCAO)), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                        FROM LOCACOES l 
                                        INNER JOIN FILMES f 
                                        ON l.ID_FILME  = f.ID_FILME
                                        INNER JOIN CLIENTES c  
                                        ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 1
                                        AND l.DATA_LOCACAO 
                                        <= (SELECT  NOW())
                                        ORDER BY l.DATA_LOCACAO";

                                      


                string sqlcomuns = $@"
                                        SELECT l.ID_LOCACAO AS ID_LOCACAO, f.ID_FILME AS ID_FILME, f.TITULO AS TITULO, c.ID_CLIENTE AS ID_CLIENTE, c.NOME AS NOME_CLIENTE, c.CPF AS CPF_CLIENTE, c.DATA_NASCIMENTO AS DATA_NASCIMENTO_CLIENTE , f.LANCAMENTO  , l.DATA_LOCACAO AS DATA_LOCACAO, l.DATA_DEVOLUCAO AS DATA_DEVOLUCAO, (SELECT DATEDIFF((SELECT IF(l.DATA_DEVOLUCAO  = '0001-01-01 00:00:00', (SELECT  NOW()), l.DATA_DEVOLUCAO)), l.DATA_LOCACAO)) AS TEMPO_LOCACAO
                                        FROM LOCACOES l 
                                        INNER JOIN FILMES f 
                                        ON l.ID_FILME  = f.ID_FILME
                                        INNER JOIN CLIENTES c  
                                        ON l.ID_CLIENTE  = c.ID_CLIENTE WHERE  f.LANCAMENTO = 0
                                        AND l.DATA_LOCACAO 
                                        <= (SELECT  NOW())
                                        ORDER BY l.DATA_LOCACAO";
                
                */
                var result = _connection.Query<dynamic>(sqllancamentos);

                var result2 = _connection.Query<dynamic>(sqlcomuns);


                if (result != null)
                {
                    List<ClienteAtrasadoReturn> list = new List<ClienteAtrasadoReturn>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        if(it.LANCAMENTO == 1) { 

                            if (it.TEMPO_LOCACAO >= 2) { 
                       
                            list.Add(new ClienteAtrasadoReturn
                            {
                                idLocacao = it.ID_LOCACAO,
                                idFilme = it.ID_FILME,
                                titulo = it.TITULO,
                                idCliente = it.ID_CLIENTE,
                                nome = it.NOME_CLIENTE,
                                cpf = it.CPF_CLIENTE,
                                tempoAtraso = it.TEMPO_LOCACAO,
                                dataNascimento  = it.DATA_NASCIMENTO_CLIENTE,
                                dataLocacao = it.DATA_LOCACAO,
                                dataDevolucao = it.DATA_DEVOLUCAO
                            });
                         }
                        }
                    });


                    result2.ToList<dynamic>().ForEach(it =>
                    {
                        if(it.LANCAMENTO == 0) {
                            if (it.TEMPO_LOCACAO >= 3)
                            {
                                list.Add(new ClienteAtrasadoReturn
                                {
                                    idLocacao = it.ID_LOCACAO,
                                    idFilme = it.ID_FILME,
                                    titulo = it.TITULO,
                                    idCliente = it.ID_CLIENTE,
                                    nome = it.NOME_CLIENTE,
                                    cpf = it.CPF_CLIENTE,
                                    tempoAtraso = it.TEMPO_LOCACAO,
                                    dataNascimento = it.DATA_NASCIMENTO_CLIENTE,
                                    dataLocacao = it.DATA_LOCACAO,
                                    dataDevolucao = it.DATA_DEVOLUCAO

                                });
                            }
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
                              SELECT f.ID_FILME AS ID_FILME, TITULO , CLASSIFICACAO_INDICATIVA , LANCAMENTO  FROM FILMES f 
                              WHERE f.ID_FILME  NOT IN (SELECT ID_FILME  FROM LOCACOES)"; 



                var result = _connection.Query<dynamic>(sql);


                if (result != null)
                {
                    List<TopAlugados> list = new List<TopAlugados>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new TopAlugados
                        {
                            idFilme = it.ID_FILME,
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
                                    , count(*) AS VENDAS from LOCACOES l
                                    INNER JOIN FILMES f 
                                    ON l.ID_FILME  = f.ID_FILME
                                    WHERE DATA_LOCACAO  BETWEEN NOW() - INTERVAL 360 DAY AND NOW()
                                    group by l.ID_FILME
                                    order by count(*)  desc 
                                    limit 5";


                var result = _connection.Query<dynamic>(sql);


                if (result != null)
                {
                    List<TopAlugados> list = new List<TopAlugados>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new TopAlugados
                        {
                            idFilme = it.ID_FILME,
                            titulo = it.TITULO,
                            classificacaoIndicativa = Convert.ToInt32(it.CLASSIFICACAO_INDICATIVA),
                            lancamento = Convert.ToBoolean(it.LANCAMENTO),
                            vendas = Convert.ToInt32(it.VENDAS)

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

        public Response MenosAlugados()
        {
            try
            {

                string sql = $@"
                                SELECT F.ID_FILME AS ID_FILME, F.TITULO AS TITULO, F.CLASSIFICACAO_INDICATIVA AS CLASSIFICACAO_INDICATIVA, F.LANCAMENTO AS LANCAMENTO 
                                , COUNT(L.ID_FILME) AS VENDAS 
                                FROM FILMES F 
                                LEFT JOIN LOCACOES L ON F.ID_FILME  = L.ID_FILME  
                                GROUP BY F.ID_FILME, F.TITULO, F.CLASSIFICACAO_INDICATIVA, F.LANCAMENTO 
                                ORDER BY VENDAS
                                LIMIT 3";
                    

                var result = _connection.Query<dynamic>(sql);


                if (result != null)
                {
                    List<TopAlugados> listDesc = new List<TopAlugados>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        listDesc.Add(new TopAlugados
                        {
                            idFilme = it.ID_FILME,
                            titulo = it.TITULO,
                            classificacaoIndicativa = Convert.ToInt32(it.CLASSIFICACAO_INDICATIVA),
                            lancamento = Convert.ToBoolean(it.LANCAMENTO),
                            vendas = Convert.ToInt32(it.VENDAS)

                        });
                    });


                    return new Response()
                    {
                        List = listDesc,
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
                _logger.Error(ex, $"[ClienteRepository] Exception in MenosAlugados!");
            }

            return null;
        }

        public Response SegundoTopCliente()
        {
            try
            {

                string sql = $@"
                                   select l.ID_CLIENTE as ID_CLIENTE, c.NOME AS NOME, c.CPF AS CPF, c.DATA_NASCIMENTO 
   									, count(*) AS VENDAS from LOCACOES l
                                    INNER JOIN CLIENTES c 
                                    ON l.ID_CLIENTE  = c.ID_CLIENTE                                 
                                    group by l.ID_CLIENTE 
                                    order by count(*) desc
                                    limit 1 offset 1";


                var result = _connection.Query<dynamic>(sql);


                if (result != null)
                {
                    List<SegundoMelhorCliente> list = new List<SegundoMelhorCliente>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new SegundoMelhorCliente
                        {
                            idCliente = it.ID_CLIENTE,
                            nome = it.NOME,
                            cpf = it.CPF,
                            dataNascimento = it.DATA_NASCIMENTO,
                            vendas = it.VENDAS                            
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

