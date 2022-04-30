import React, { useState, useEffect } from 'react';
import api from '../../../services/api';
import global from '../../../config/global'
import { Link } from 'react-router-dom';

export function ListarCliente() {

    const [clientes, setClientes] = useState([]);
    alert(`${process.env.REACT_APP_API}/clientes`);
    useEffect(() => {
        api.get(`${process.env.REACT_APP_API}/clientes`)
            .then((response) => {
                setClientes(response.data.list);
                console.log(response.data.list);

            })
            .catch((err) => {
                alert(global.msgErro + err);
                console.log('errado');
            })
    }, []);


    return (
        <>
        <p><Link to="/">Home </Link></p> 
            <div className='containerPrincipal'>
                    <div className='containerSecundario'>

                   
                <table>
                    <thead>
                        <tr>
                            <th style={{ width: '170px', textAlign: 'center' }}>Nome</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>CPF</th>
                            <th style={{ width: '170px', textAlign: 'center' }}>Data Nascimento</th>
                            <th style={{ width: '170px', textAlign: 'center' }}> Ações</th>
                        </tr>
                    </thead>
                    <tbody>
                        {clientes?.map((cliente, index) => {
                            return (
                                <tr key={index} style={{ flex: 1, flexDirection: 'row', backgroundColor: index % 2 === 0 ? '#ccc' : '#fff' }}>

                                    <td style={{ width: '170px', textAlign: 'center' }}> {cliente.nome}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.cpf}</td>
                                    <td style={{ width: '170px', textAlign: 'center' }}>{cliente.dataNascimento}</td>
                                    <td style={{ display: 'flex', justifyContent: 'space-around' }}>
                                        <button> Editar </button>
                                        <button> Excluir </button>
                                    </td>
                                </tr>


                            )
                        })}

                    </tbody>
                </table>
            </div> 
            </div>


        </>
    );
}

