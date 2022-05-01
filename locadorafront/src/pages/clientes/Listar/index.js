import { Link } from 'react-router-dom';
import More from '../../../images//more.svg';
import './styles.css';

import React, { useState, useEffect } from 'react';
import api from '../../../services/api';
import Header from '../../../components/Header';

export function ListarCliente() {

    
    
    const [clientes, setClientes] = useState([]);
    useEffect(() => {
        api.get(`${process.env.REACT_APP_API}/clientes`)
            .then((response) => {
                setClientes(response.data.list);
                console.log(response.data.list);

            })
            .catch((err) => {
                alert("Erro :"+ err);
                console.log('errado');
            })
    }, []);

    function deletarCliente(idCliente){
        
        api.delete(`${process.env.REACT_APP_API}/clientes/${idCliente}`)
            .then((response) =>{
                alert(JSON.stringify(response));
                setClientes(clientes.filter(cliente => cliente.idCliente !== idCliente ))
            })
            .catch((err) =>{
                alert("Erro: "+ err);
            })
        
    }

    return (
        <div>
               <Header />
            <h1>Clientes</h1>

            <div></div>

            <main>
                <div className="cards">
                    <div className="card">

                    {clientes?.map((cliente, index) => {
                            return (
                                <div key={index}>
                                <header>
                                    <h2> {cliente.nome} </h2>
                                    <img src={More}/>
                                </header>
                                    <div className="linha"> </div>      
        
                                    <p>Id :{cliente.idCliente}</p>
                                    <p>CPF :{cliente.cpf}</p>
                                    <p>Data de Nascimento: {cliente.dataNascimento}</p>
                                    
        
                                    <div className='btns'>
                                        <div className='btn-editar'>
                                            <Link to={{ pathname: `/clientes/Editar/${cliente.idCliente}`}}>
                                                <button>Editar</button>
                                            </Link>
                                        </div>
                                        
                                        <div className='btn-deletar'>
                                            <button onClick={ 
                                                () => deletarCliente(cliente.idCliente)
                                                 }>Deletar</button>
                                        </div>
        
                                    </div>
                                </div>
                            )
                        })}
                        
                    
                    </div>
                </div>
            </main>

            
        </div>
    )
}

