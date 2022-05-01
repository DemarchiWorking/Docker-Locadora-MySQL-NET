import { Link } from 'react-router-dom';
import More from '../../../images//more.svg';
import './styles.css';
import React, { useState, useEffect } from 'react';
import api from '../../../services/api';
import Header from '../../../components/Header';

export function ListarFilme() {

    
    
    const [filmes, setFilmes] = useState([]);
    useEffect(() => {
        api.get(`${process.env.REACT_APP_API}/filmes`)
            .then((response) => {
                setFilmes(response.data.list);
                console.log(response.data.list);

            })
            .catch((err) => {
                alert("Erro :"+ err);
                console.log('errado');
            })
    }, []);

    function deletarFilme(idFilme){
        
        api.delete(`${process.env.REACT_APP_API}/filmes/${idFilme}`)
            .then((response) =>{
                alert(JSON.stringify(response));
                setFilmes(filmes.filter(filme => filme.ID_FILME !== idFilme ))
            })
            .catch((err) =>{
                
                alert(`${process.env.REACT_APP_API}/filmes/${idFilme}`);
                alert("Erro: "+ err);
            })
        
    }

    return (
        <div>
               <Header />
            <h1>Filmes</h1>

            <div></div>

            <main>
                <div className="cards">
                    <div className="card">

                    {filmes?.map((filme, index) => {
                            return (
                                <div key={index}>
                                <header>
                                    {
                                    }
                                    <h2> {filme.TITULO} </h2>
                                    <img src={More}/>
                                </header>
                                    <div className="linha"> </div>      
        
                                    <p>Id :{filme.ID_FILME}</p>
                                    <p>Classificação Indicativa :{filme.CLASSIFICACAO_INDICATIVA}</p>
                                    <p>Lançamento: {(filme.LANCAMENTO==0)?'nao':'sim'}</p>
        
        
                                    <div className='btns'>
                                        <div className='btn-editar'>
                                            <Link to={{ pathname: `/filmes/Editar/${filme.ID_FILME}`}}>
                                                <button>Editar</button>
                                            </Link>
                                        </div>
                                        <div className='btn-deletar'>
                                            <button onClick={ 
                                                () => deletarFilme(filme.ID_FILME)
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

