import { Link } from 'react-router-dom';
import More from '../../../images//more.svg';
import './styles.css';

import React, { useState, useEffect } from 'react';
import api from '../../../services/api';
import Header from '../../../components/Header';

export function ListarLocacao() {

 
    const [locacoes, setLocacoes] = useState([]);
    useEffect(() => {
        api.get(`${process.env.REACT_APP_API}/locacoes`)
            .then((response) => {
                setLocacoes(response.data.list);
                console.log(response.data.list);

            })
            .catch((err) => {
                alert("Erro :"+ err);
                console.log('errado');
            })
    }, []);

    function deletarLocacoes(idLocacao){
        alert(`${process.env.REACT_APP_API}/locacoes/${idLocacao}`)
        api.delete(`${process.env.REACT_APP_API}/locacoes/${idLocacao}`)
            .then((response) =>{
                alert(JSON.stringify(response));
                alert()
                setLocacoes(locacoes.filter(locacao => locacao.idLocacao !== idLocacao ))
            })
            .catch((err) =>{
                
       //         alert(`${process.env.REACT_APP_API}/locacoes/${idLocacao}`);
                alert("Erro: "+ err);
            })
        
    }

    return (
        <div>
               <Header />
            <h1>Locações</h1>

            <div></div>

            <main>
                <div className="cards">
                    <div className="card">

                    {locacoes?.map((locacao, index) => {
                            return (
                                <div key={index}>
                                <header>
                                    {
                                    }
                                    <h2>Id Locação: {locacao.idLocacao} </h2>
                                    <img src={More}/>
                                </header>
                                    <div className="linha"> </div>      
                                    <p>Id Cliente: {locacao.idCliente}</p>
                                    <p>Id Filme: {locacao.idFilme}</p>
                                    <p>Data de Locação :{locacao.dataLocacao}</p>
                                    <p>Data de Devoluçãoz: {locacao.dataDevolucao}</p>
        
        
                                    <div className='btns'>
                                        <div className='btn-editar'>
                                            <Link to={{ pathname: `/locacoes/Editar/${locacao.idLocacao}`}}>
                                                <button>Editar</button>
                                            </Link>
                                        </div>
                                        <div className='btn-deletar'>
                                            <button onClick={ 
                                                () => deletarLocacoes(locacao.idLocacao)
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

