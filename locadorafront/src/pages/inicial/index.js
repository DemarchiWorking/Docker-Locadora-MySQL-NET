import { Link } from 'react-router-dom';
import HeaderMain from '../../components/HeaderMain';
import More from '../../images/more.svg';
import './styles.css';
import api from '../../services/api';
import Header from '../../components/Header';
import { useState, useEffect } from 'react';


export function Inicial() {
    return (
        <div>
            <HeaderMain/>
            
            
        <p></p>
            <main>
                <div className="cards">
                    <div className="card">

                        <header>
                            <h2> Cliente </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-editar'>
                                    <Link to="/clientes/cadastrar">
                                        <button>Cadastrar</button>
                                    </Link>
                                </div>
                                <div className='btn-ler'>
                                    <Link to="/clientes/listar">
                                        <button>Listar</button>
                                    </Link>
                                </div>
                            </div>
                    </div>
                    <div className="card">

                        <header>
                            <h2> Filmes </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-editar'>
                                    <Link to="/filmes/cadastrar">
                                        <button>Cadastrar</button>
                                    </Link>
                                </div>
                                <div className='btn-ler'>
                                    <Link to="/filmes/listar">
                                        <button>Listar</button>
                                    </Link>
                                </div>
                            </div>
                    </div>
                    <div className="card">

                        <header>
                            <h2> Locacões </h2>
                            <img src={More}/>
                        </header>
                            <div className="linha"> </div>
                            <p></p>
                            <div className='btns'>
                                <div className='btn-editar'>
                                    <Link to="/locacoes/cadastrar">
                                        <button>Cadastrar</button>
                                    </Link>
                                </div>
                                <div className='btn-ler'>
                                <Link to="/locacoes/listar">
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
