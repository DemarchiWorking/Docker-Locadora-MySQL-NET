import { Link } from 'react-router-dom';
import HeaderMain from '../../../components/HeaderMain';
import React, { useState, useEffect } from 'react';
import More from '../../../images/more.svg';
import api from '../../../services/api';
import './styles.css';
import clientesAtrasados from '../resultado';


function ClientesAtrasados(){
   
    const [clientesAtrasados, setClientesAtrasados] = useState([]);
    useEffect(() => {
        alert(`${process.env.REACT_APP_API}/funcionalidades/atrasados`);
        api.get(`${process.env.REACT_APP_API}/funcionalidades/atrasados`)
            .then((response) => {
                console.log(JSON.stringify(response.data))
                setClientesAtrasados(response.data.list);
                alert(JSON.stringify(response.data));

            })
            .catch((err) => {
                alert("Erro :"+ err);
                console.log('errado');
            })
    }, []);
    return clientesAtrasados
}

export function Funcionalidades() {
   
    return (
        <div>
            <HeaderMain/>
            
        <p></p>
            <main>
                <div className="cards">
                    <div className="card">

                        <header>
                            <h2> Clientes atrasados </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-editar'>
                                    <Link to="/funcionalidades/Resultado">
                                        <button> Listar </button>
                                    </Link>
                                </div>
                            </div>
                    </div>
                    <div className="card">

                        <header>
                            <h2> Filmes não alugados </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-editar'>
                                    <Link to="/filmes/cadastrar">
                                        <button> Listar </button>
                                    </Link>
                                </div>
                                
                            </div>
                    </div>
                    <div className="card">

                        <header>
                            <h2> Top Alugados </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-editar'>
                                    <Link to="/locacoes/cadastrar">
                                        <button> Listar </button>
                                    </Link>
                                </div>
                                
                            </div>
                    </div>

                    <div className="card">

                        <header>
                            <h2> Menos Alugados </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-editar'>
                                    <Link to="/locacoes/cadastrar">
                                        <button> Listar </button>
                                    </Link>
                                </div>
                                <div className='btn-ler'>
                                
                                </div>
                            </div>
                    </div>

                    <div className="card">

                        <header>
                            <h2> Segundo melhor cliente </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-editar'>
                                    <Link to="/locacoes/cadastrar">
                                        <button>Listar</button>
                                    </Link>
                                </div>
                                
                            </div>
                    </div>
                </div>
            </main>

            
        </div>
    )
}