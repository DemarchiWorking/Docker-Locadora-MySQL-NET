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
    public class ClienteRepository : IClienteRepository
    {

        private readonly ILogger _logger;
        private readonly string _connectionString;
        private readonly MySqlConnection _connection;

        public ClienteRepository
            (
                ILogger logger,
                IConfiguration configuration
            )
        {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("conexaoMySQL");
                _connection = new MySqlConnection(_connectionString);
        }

        


        public Response CadastrarCliente(ClienteRequest clienteRequest)
        {
            try
            {
                string query = $@"
                                    INSERT INTO CLIENTES
                                     (
                                                    NOME
                                                    , CPF
                                                    , DATA_NASCIMENTO
                                                    )
                                                    VALUES
                                                    (
                                                    '{clienteRequest.nome}'
                                                    , '{clienteRequest.cpf}'
                                                    , '{Convert.ToDateTime(clienteRequest.dataNascimento).ToString("yyyy/MM/dd HH:mm:ss")}'
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
                _logger.Error(ex, $"[ClienteRepository] Exception in CadastrarCliente!");

            }

            return null;
        }

        public Response ListarTodosClientes()
        {
            try
            {

                string sql = $@"SELECT
                                        ID_CLIENTE,
		                                NOME,
		                                CPF,
		                                DATA_NASCIMENTO
                               FROM CLIENTES;";

                var result = _connection.Query<dynamic>(sql);

                if (result != null)
                {
                    List<SegundoMelhorCliente> list = new List<SegundoMelhorCliente>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new SegundoMelhorCliente
                        {
                            idCliente = Convert.ToInt32(it.ID_CLIENTE),
                            nome = Convert.ToString(it.NOME),
                            cpf = Convert.ToString(it.CPF),
                            dataNascimento = Convert.ToDateTime(it.DATA_NASCIMENTO)
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


        public Response AlterarCliente(ClienteUpdateRequest clienteUpdateRequest)
        {
            try
            {
                string sql = $@"                
                                 UPDATE CLIENTES SET ";
                                    if(clienteUpdateRequest.nome != null)
                                    {
                                        sql += $"NOME = '{clienteUpdateRequest.nome}' ";
                                        if(clienteUpdateRequest.cpf != null || clienteUpdateRequest.dataNascimento != null)
                                        {
                                            sql += " , ";
                                        }
                                    }
                                    if(clienteUpdateRequest.cpf != null)
                                    {
                                          sql +=$"CPF = '{clienteUpdateRequest.cpf}' ";
                                        if(clienteUpdateRequest.dataNascimento != null)
                                        {
                                            sql += " , ";
                                        }
                                    }
                                     if(clienteUpdateRequest.dataNascimento != null)
                                    {
                                          sql += $"DATA_NASCIMENTO = '{Convert.ToDateTime(clienteUpdateRequest.dataNascimento).ToString("yyyy/MM/dd HH:mm:ss")} '";
                                    }
                                     sql += $"WHERE ID_CLIENTE = { clienteUpdateRequest.idCliente}; ";

                                         
                                             


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
                _logger.Error(ex, $"[ClienteRepository] Exception in AlterarCliente!");
            }

            return new Response()
            {
                isSuccess = false
            };

        

    }
        public Response ExcluirCliente(int idCliente)
        {
            try
            {

                string sql = $@"DELETE FROM CLIENTES WHERE ID_CLIENTE = {idCliente};";

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
                _logger.Error(ex, $"[ClienteRepository] Exception in ExcluirCliente!");
            }

            return null;
        }

        public Response ListarClientePorId(int idCliente)
        {
            try
            {

                string sql = $@"SELECT
                                        ID_CLIENTE,
		                                NOME,
		                                CPF,
		                                DATA_NASCIMENTO
                               FROM CLIENTES WHERE ID_CLIENTE = {idCliente};";

                var result = _connection.Query<dynamic>(sql);


                if (result != null && result.ToList().Count != 0)
                {
                    List<SegundoMelhorCliente> list = new List<SegundoMelhorCliente>();

                    result.ToList<dynamic>().ForEach(it =>
                    {
                        list.Add(new SegundoMelhorCliente
                        {
                            idCliente = it.ID_CLIENTE,
                            nome = it.NOME,
                            cpf = it.CPF,
                            dataNascimento = it.DATA_NASCIMENTO
                        });
                    });

                    return new Response()
                    {
                        List = list,
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
                _logger.Error(ex, $"[ClienteRepository] Exception in ExisteUsuario!");
            }
            return new Response()
            {
                isSuccess = false,

            };
        }

    }

}

