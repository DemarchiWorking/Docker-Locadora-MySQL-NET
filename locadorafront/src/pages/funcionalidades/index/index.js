import { Link } from 'react-router-dom';
import HeaderMain from '../../../components/HeaderMain'

import React, { useState, useEffect } from 'react';
import './styles.css';
import More from '../../../images/more.svg';
import api from '../../../services/api';
import Header from '../../../components/Header';



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
                                    <Link to="/funcionalidades/atrasados">
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
                                    <Link to="/funcionalidades/nao-alugados">
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
                                    <Link to="/funcionalidades/top-alugados">
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
                                    <Link to="/funcionalidades/menos-alugados">
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
                                    <Link to="/funcionalidades/segundo-melhor-cliente">
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